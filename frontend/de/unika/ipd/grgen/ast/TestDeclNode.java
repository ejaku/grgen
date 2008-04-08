/*
 GrGen: graph rewrite generator tool.
 Copyright (C) 2005  IPD Goos, Universit"at Karlsruhe, Germany

 This library is free software; you can redistribute it and/or
 modify it under the terms of the GNU Lesser General Public
 License as published by the Free Software Foundation; either
 version 2.1 of the License, or (at your option) any later version.

 This library is distributed in the hope that it will be useful,
 but WITHOUT ANY WARRANTY; without even the implied warranty of
 MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the GNU
 Lesser General Public License for more details.

 You should have received a copy of the GNU Lesser General Public
 License along with this library; if not, write to the Free Software
 Foundation, Inc., 51 Franklin St, Fifth Floor, Boston, MA  02110-1301  USA
 */


/**
 * @author shack
 * @version $Id$
 */
package de.unika.ipd.grgen.ast;

import java.util.Collection;
import java.util.HashSet;
import java.util.Iterator;
import java.util.LinkedList;
import java.util.Vector;

import de.unika.ipd.grgen.ast.util.Checker;
import de.unika.ipd.grgen.ast.util.CollectChecker;
import de.unika.ipd.grgen.ast.util.DeclarationTypeResolver;
import de.unika.ipd.grgen.ir.Edge;
import de.unika.ipd.grgen.ir.Entity;
import de.unika.ipd.grgen.ir.IR;
import de.unika.ipd.grgen.ir.InheritanceType;
import de.unika.ipd.grgen.ir.MatchingAction;
import de.unika.ipd.grgen.ir.PatternGraph;
import de.unika.ipd.grgen.ir.Rule;
import de.unika.ipd.grgen.ir.Variable;
import de.unika.ipd.grgen.util.report.ErrorReporter;


/**
 * AST node class representing tests
 */
public class TestDeclNode extends ActionDeclNode {
	static {
		setName(TestDeclNode.class, "test declaration");
	}

	CollectNode<IdentNode> returnFormalParameters;
	TestTypeNode type;
	PatternGraphNode pattern;

	private static final TypeNode testType = new TestTypeNode();

	protected TestDeclNode(IdentNode id, TypeNode type, PatternGraphNode pattern,
						   CollectNode<IdentNode> rets) {
		super(id, type);
		this.returnFormalParameters = rets;
		becomeParent(this.returnFormalParameters);
		this.pattern = pattern;
		becomeParent(this.pattern);
	}

	public TestDeclNode(IdentNode id, PatternGraphNode pattern,
						CollectNode<IdentNode> rets) {
		this(id, testType, pattern, rets);
	}

	/** returns children of this node */
	public Collection<BaseNode> getChildren() {
		Vector<BaseNode> children = new Vector<BaseNode>();
		children.add(ident);
		children.add(getValidVersion(typeUnresolved, type));
		children.add(returnFormalParameters);
		children.add(pattern);
		return children;
	}

	/** returns names of the children, same order as in getChildren */
	public Collection<String> getChildrenNames() {
		Vector<String> childrenNames = new Vector<String>();
		childrenNames.add("ident");
		childrenNames.add("type");
		childrenNames.add("ret");
		childrenNames.add("pattern");
		return childrenNames;
	}

	private static final DeclarationTypeResolver<TestTypeNode> typeResolver = new DeclarationTypeResolver<TestTypeNode>(TestTypeNode.class);

	/** @see de.unika.ipd.grgen.ast.BaseNode#resolveLocal() */
	protected boolean resolveLocal() {
		type = typeResolver.resolve(typeUnresolved, this);

		return type != null;
	}

	/**
	 * check if actual return entities are conformant
	 * to the formal return parameters.
	 */
	// TODO: check types
	protected boolean checkReturnParams(CollectNode<IdentNode> typeReturns, CollectNode<ConstraintDeclNode> actualReturns) {
		boolean returnTypes = true;

		/*
		 System.out.println("\n*** this          = " + this.getClass());
		 System.out.println("    this          = " + this.getChildren());
		 System.out.println("*** typeReturns   = "   + typeReturns);
		 System.out.println("    typeReturns   = "   + typeReturns.getChildren());
		 System.out.println("*** actualReturns = " + actualReturns);
		 System.out.println("    actualReturns = " + actualReturns.getChildren());
		 */

		if(actualReturns.children.size() != typeReturns.children.size()) {
			error.error(getCoords(), "Actual and formal return-parameter count mismatch (" +
							actualReturns.children.size() + " vs. " + typeReturns.children.size() +")");
			returnTypes = false;
		} else {
			Iterator<ConstraintDeclNode> itAR = actualReturns.children.iterator();

			for(BaseNode n : typeReturns.getChildren()) {
				IdentNode       tReturnAST  = (IdentNode)n;
				InheritanceType tReturn     = (InheritanceType)tReturnAST.getDecl().getDeclType().checkIR(InheritanceType.class);

				ConstraintDeclNode aReturnAST  = itAR.next();
				InheritanceType    aReturnType = (InheritanceType)aReturnAST.getDeclType().checkIR(InheritanceType.class);

				if(!aReturnType.isCastableTo(tReturn)) {
					error.error(aReturnAST.getCoords(), "Actual return-parameter is not conformant to formal parameter (" +
									aReturnType + " not castable to " + tReturn + ")");
					returnTypes = false;
				}
			}
		}

		return returnTypes;
	}

	private static final Checker retDeclarationChecker = new CollectChecker(
		new Checker() {
			public boolean check(BaseNode node, ErrorReporter reporter) {
				boolean res = true;

				if ( ! (node instanceof IdentNode) ) {
					//this should never be reached
					node.reportError("Not an identifier");
					return false;
				}
				if ( ((IdentNode)node).getDecl().equals(DeclNode.getInvalid()) ) {
					res = false;
					node.reportError("\"" + node + "\" is undeclared");
				} else {
					TypeNode type = ((IdentNode)node).getDecl().getDeclType();
					res = (type instanceof NodeTypeNode) || (type instanceof EdgeTypeNode);
					if (!res) {
						node.reportError("\"" + node + "\" is neither a node nor an edge type");
					}
				}
				return res;
			}
		}
	);

	/**
	 * Method check
	 *
	 * @return   a boolean
	 *
	 */
	protected boolean checkLocal() {
		boolean childs = retDeclarationChecker.check(returnFormalParameters, error);

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
		if(! (this instanceof RuleDeclNode)) {
			returnParams = checkReturnParams(returnFormalParameters, pattern.returns);
		}

		return childs && edgeReUse && returnParams;
	}


	protected void constructIRaux(MatchingAction ma, CollectNode<ConstraintDeclNode> aReturns) {
		PatternGraph patternGraph = ma.getPattern();

		// add Params to the IR
		for(DeclNode decl : pattern.getParamDecls()) {
			ma.addParameter((Entity) decl.checkIR(Entity.class));
			if(decl instanceof NodeCharacter) {
				patternGraph.addSingleNode(((NodeCharacter)decl).getNode());
			} else if (decl instanceof EdgeCharacter) {
				Edge e = ((EdgeCharacter)decl).getEdge();
				patternGraph.addSingleEdge(e);
			} else if(decl instanceof VarDeclNode) {
				patternGraph.addVariable((Variable) decl.getIR());
			} else {
				throw new IllegalArgumentException("unknown Class: " + decl);
			}
		}

		// add Return-Params to the IR
		for(ConstraintDeclNode aReturnAST : aReturns.getChildren()) {
			Entity aReturn = (Entity)aReturnAST.checkIR(Entity.class);
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


