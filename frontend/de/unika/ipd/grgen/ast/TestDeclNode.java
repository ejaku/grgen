/*
 * GrGen: graph rewrite generator tool -- release GrGen.NET 2.7
 * Copyright (C) 2003-2011 Universitaet Karlsruhe, Institut fuer Programmstrukturen und Datenorganisation, LS Goos
 * licensed under LGPL v3 (see LICENSE.txt included in the packaging of this file)
 * www.grgen.net
 */

/**
 * @author shack
 * @version $Id$
 */
package de.unika.ipd.grgen.ast;

import de.unika.ipd.grgen.ast.util.CollectResolver;
import de.unika.ipd.grgen.ast.util.DeclarationTypeResolver;
import de.unika.ipd.grgen.ir.Edge;
import de.unika.ipd.grgen.ir.Entity;
import de.unika.ipd.grgen.ir.Expression;
import de.unika.ipd.grgen.ir.IR;
import de.unika.ipd.grgen.ir.MatchingAction;
import de.unika.ipd.grgen.ir.PatternGraph;
import de.unika.ipd.grgen.ir.Rule;
import java.util.Collection;
import java.util.HashSet;
import java.util.LinkedList;
import java.util.Set;
import java.util.Vector;


/**
 * AST node class representing tests
 */
public class TestDeclNode extends ActionDeclNode {
	static {
		setName(TestDeclNode.class, "test declaration");
	}

	protected CollectNode<BaseNode> returnFormalParametersUnresolved;
	protected CollectNode<TypeNode> returnFormalParameters;
	private TestTypeNode type;
	protected PatternGraphNode pattern;

	private static final TypeNode testType = new TestTypeNode();

	protected TestDeclNode(IdentNode id, TypeNode type, PatternGraphNode pattern,
						   CollectNode<BaseNode> rets) {
		super(id, type);
		this.returnFormalParametersUnresolved = rets;
		becomeParent(this.returnFormalParametersUnresolved);
		this.pattern = pattern;
		becomeParent(this.pattern);
	}

	public TestDeclNode(IdentNode id, PatternGraphNode pattern,
						CollectNode<BaseNode> rets) {
		this(id, testType, pattern, rets);
	}

	/** returns children of this node */
	@Override
	public Collection<BaseNode> getChildren() {
		Vector<BaseNode> children = new Vector<BaseNode>();
		children.add(ident);
		children.add(getValidVersion(typeUnresolved, type));
		children.add(getValidVersion(returnFormalParametersUnresolved, returnFormalParameters));
		children.add(pattern);
		return children;
	}

	/** returns names of the children, same order as in getChildren */
	@Override
	public Collection<String> getChildrenNames() {
		Vector<String> childrenNames = new Vector<String>();
		childrenNames.add("ident");
		childrenNames.add("type");
		childrenNames.add("ret");
		childrenNames.add("pattern");
		return childrenNames;
	}

	private static final DeclarationTypeResolver<TestTypeNode> typeResolver = new DeclarationTypeResolver<TestTypeNode>(TestTypeNode.class);
	private static final CollectResolver<TypeNode> retTypeResolver = new CollectResolver<TypeNode>(
    		new DeclarationTypeResolver<TypeNode>(TypeNode.class));

	/** @see de.unika.ipd.grgen.ast.BaseNode#resolveLocal() */
	@Override
	protected boolean resolveLocal() {
		type = typeResolver.resolve(typeUnresolved, this);
		returnFormalParameters = retTypeResolver.resolve(returnFormalParametersUnresolved, this);

		return type != null && returnFormalParameters != null;
	}

	/**
	 * Check if actual return arguments are conformant to the formal return parameters.
	 */
	// TODO is this method called twice for RuleDeclNode?
	protected boolean checkReturns(CollectNode<ExprNode> returnArgs) {
		boolean res = true;

		int declaredNumRets = returnFormalParameters.size();
		int actualNumRets = returnArgs.children.size();
retLoop:for (int i = 0; i < Math.min(declaredNumRets, actualNumRets); i++) {
			ExprNode retExpr = returnArgs.children.get(i);
			TypeNode retExprType = retExpr.getType();

			TypeNode retDeclType = returnFormalParameters.get(i);
			if(!retExprType.isCompatibleTo(retDeclType)) {
				res = false;
				String exprTypeName;
				if(retExprType instanceof InheritanceTypeNode)
					exprTypeName = ((InheritanceTypeNode) retExprType).getIdentNode().toString();
				else
					exprTypeName = retExprType.toString();
				ident.reportError("Cannot convert " + (i + 1) + ". return parameter from \""
						+ exprTypeName + "\" to \"" + returnFormalParameters.get(i).toString() + "\"");
				continue;
			}

			if(!(retExpr instanceof DeclExprNode)) continue;
			ConstraintDeclNode retElem = ((DeclExprNode) retExpr).getConstraintDeclNode();
			if(retElem == null) continue;

			InheritanceTypeNode declaredRetType = retElem.getDeclType();

			Set<? extends ConstraintDeclNode> homSet;
			if(retElem instanceof NodeDeclNode)
				homSet = pattern.getHomomorphic((NodeDeclNode) retElem);
			else
				homSet = pattern.getHomomorphic((EdgeDeclNode) retElem);

			for(ConstraintDeclNode homElem : homSet) {
				if(homElem == retElem) continue;

				ConstraintDeclNode retypedElem = homElem.getRetypedElement();
				if(retypedElem == null) continue;

				InheritanceTypeNode retypedElemType = retypedElem.getDeclType();
				if(retypedElemType.isA(declaredRetType)) continue;

				res = false;
				returnArgs.reportError("Return parameter \"" + retElem.getIdentNode() + "\" is homomorphic to \""
						+ homElem.getIdentNode() + "\", which gets retyped to the incompatible type \""
						+ retypedElemType.getIdentNode() + "\"");
				continue retLoop;
			 }
		}

		//check the number of returned elements
		if (actualNumRets != declaredNumRets) {
			res = false;
			if (declaredNumRets == 0) {
				returnArgs.reportError("No return values declared for rule \"" + ident + "\"");
			} else if(actualNumRets == 0) {
				reportError("Missing return statement for rule \"" + ident + "\"");
			} else {
				returnArgs.reportError("Return statement has wrong number of parameters");
			}
		}
		return res;
	}

	private boolean SameNumberOfRewriteParts() {
		boolean res = true;

		for(AlternativeNode alt : pattern.alts.getChildren()) {
			for(AlternativeCaseNode altCase : alt.getChildren()) {
				if(altCase.right.getChildren().size() != 0) {
					error.error(getCoords(), "Different number of replacement patterns/rewrite parts in test " + ident.toString()
							+ " and nested alternative case " + altCase.ident.toString());
					res = false;
					continue;
				}
			}
		}
		
		for(IteratedNode iter : pattern.iters.getChildren()) {
			if(iter.right.getChildren().size() != 0) {
				error.error(getCoords(), "Different number of replacement patterns/rewrite parts in test " + ident.toString()
						+ " and nested iterated/multiple/optional " + iter.ident.toString());
				res = false;
				continue;
			}
		}

		return res;
	}

	@Override
	protected boolean checkLocal() {
		boolean childs = true;

		// check if reused names of edges connect the same nodes in the same direction with the same edge kind for each usage
		boolean edgeReUse = false;
		if (childs) {
			edgeReUse = true;

			//get the negative graphs and the pattern of this TestDeclNode
			// NOTE: the order affect the error coords
			Collection<PatternGraphNode> leftHandGraphs = new LinkedList<PatternGraphNode>();
			leftHandGraphs.add(pattern);
			for (PatternGraphNode pgn : pattern.negs.getChildren()) {
				leftHandGraphs.add(pgn);
			}

			GraphNode[] graphs = leftHandGraphs.toArray(new GraphNode[0]);
			Collection<EdgeCharacter> alreadyReported = new HashSet<EdgeCharacter>();

			for (int i=0; i<graphs.length; i++) {
				for (int o=i+1; o<graphs.length; o++) {
					for (BaseNode iBN : graphs[i].getConnections()) {
						if (! (iBN instanceof ConnectionNode)) {
							continue;
						}
						ConnectionNode iConn = (ConnectionNode)iBN;

						for (BaseNode oBN : graphs[o].getConnections()) {
							if (! (oBN instanceof ConnectionNode)) {
								continue;
							}
							ConnectionNode oConn = (ConnectionNode)oBN;

							if (iConn.getEdge().equals(oConn.getEdge()) && !alreadyReported.contains(iConn.getEdge())) {
								NodeCharacter oSrc, oTgt, iSrc, iTgt;
								oSrc = oConn.getSrc();
								oTgt = oConn.getTgt();
								iSrc = iConn.getSrc();
								iTgt = iConn.getTgt();

								assert ! (oSrc instanceof NodeTypeChangeNode):
									"no type changes in test actions";
								assert ! (oTgt instanceof NodeTypeChangeNode):
									"no type changes in test actions";
								assert ! (iSrc instanceof NodeTypeChangeNode):
									"no type changes in test actions";
								assert ! (iTgt instanceof NodeTypeChangeNode):
									"no type changes in test actions";

								//check only if there's no dangling edge
								if ( !((iSrc instanceof NodeDeclNode) && ((NodeDeclNode)iSrc).isDummy())
									&& !((oSrc instanceof NodeDeclNode) && ((NodeDeclNode)oSrc).isDummy())
									&& iSrc != oSrc ) {
									alreadyReported.add(iConn.getEdge());
									iConn.reportError("Reused edge does not connect the same nodes");
									edgeReUse = false;
								}

								//check only if there's no dangling edge
								if ( !((iTgt instanceof NodeDeclNode) && ((NodeDeclNode)iTgt).isDummy())
									&& !((oTgt instanceof NodeDeclNode) && ((NodeDeclNode)oTgt).isDummy())
									&& iTgt != oTgt && !alreadyReported.contains(iConn.getEdge())) {
									alreadyReported.add(iConn.getEdge());
									iConn.reportError("Reused edge does not connect the same nodes");
									edgeReUse = false;
								}


								if (iConn.getConnectionKind() != oConn.getConnectionKind()) {
									alreadyReported.add(iConn.getEdge());
									iConn.reportError("Reused edge does not have the same connection kind");
									edgeReUse = false;
								}
							}
						}
					}
				}
			}
		}

		boolean returnParams = true;
		if(!(this instanceof RuleDeclNode))
			returnParams = checkReturns(pattern.returns);
		
		boolean noRewriteParts = true;
		if(!(this instanceof RuleDeclNode))
			noRewriteParts = SameNumberOfRewriteParts();

		return noRewriteParts && childs && edgeReUse && returnParams;
	}

	protected void constructIRaux(MatchingAction ma, CollectNode<ExprNode> aReturns) {
		PatternGraph patternGraph = ma.getPattern();

		// add Params to the IR
		for(DeclNode decl : pattern.getParamDecls()) {
			ma.addParameter(decl.checkIR(Entity.class));
			
			// TODO: parameters were already added to the graph -> needed here again?
			if(decl instanceof NodeCharacter) {
				patternGraph.addSingleNode(((NodeCharacter)decl).getNode());
			} else if (decl instanceof EdgeCharacter) {
				Edge e = ((EdgeCharacter)decl).getEdge();
				patternGraph.addSingleEdge(e);
			} else if(decl instanceof VarDeclNode) {
				patternGraph.addVariable(((VarDeclNode) decl).getVariable());
			} else {
				throw new IllegalArgumentException("unknown Class: " + decl);
			}
		}

		// add Return-Params to the IR
		for(ExprNode aReturnAST : aReturns.getChildren()) {
			Expression aReturn = aReturnAST.checkIR(Expression.class);
			// actual return-parameter
			ma.addReturn(aReturn);
		}
	}

	@Override
	public TypeNode getDeclType() {
		assert isResolved();

		return type;
	}

	public static String getKindStr() {
		return "action declaration";
	}

	public static String getUseStr() {
		return "action";
	}

	@Override
	protected IR constructIR() {
		PatternGraph left = pattern.getPatternGraph();

		// return if the pattern graph already constructed the IR object
		// that may happens in recursive patterns
		if (isIRAlreadySet()) {
			return getIR();
		}

		Rule testRule = new Rule(getIdentNode().getIdent(), left, null);

		constructImplicitNegs(left);
		constructIRaux(testRule, pattern.returns);

		return testRule;
	}

	/**
     * add NACs for induced- or DPO-semantic
     */
    protected void constructImplicitNegs(PatternGraph left)
    {
    	PatternGraphNode leftNode = pattern;
    	for (PatternGraph neg : leftNode.getImplicitNegGraphs()) {
    		left.addNegGraph(neg);
    	}
    }
}


