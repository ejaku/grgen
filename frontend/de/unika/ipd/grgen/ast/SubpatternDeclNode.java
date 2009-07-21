/*
 * GrGen: graph rewrite generator tool -- release GrGen.NET 2.5
 * Copyright (C) 2009 Universitaet Karlsruhe, Institut fuer Programmstrukturen und Datenorganisation, LS Goos
 * licensed under LGPL v3 (see LICENSE.txt included in the packaging of this file)
 */

/**
 * @version $Id$
 */

package de.unika.ipd.grgen.ast;


import java.util.Collection;
import java.util.Collections;
import java.util.HashSet;
import java.util.LinkedList;
import java.util.Set;
import java.util.Vector;

import de.unika.ipd.grgen.ast.util.DeclarationTypeResolver;
import de.unika.ipd.grgen.ir.Edge;
import de.unika.ipd.grgen.ir.Entity;
import de.unika.ipd.grgen.ir.Variable;
import de.unika.ipd.grgen.ir.EvalStatement;
import de.unika.ipd.grgen.ir.IR;
import de.unika.ipd.grgen.ir.Node;
import de.unika.ipd.grgen.ir.PatternGraph;
import de.unika.ipd.grgen.ir.Rule;


/**
 * AST node for a pattern with replacements.
 */
public class SubpatternDeclNode extends ActionDeclNode  {
	static {
		setName(SubpatternDeclNode.class, "subpattern declaration");
	}

	protected PatternGraphNode pattern;
	protected CollectNode<RhsDeclNode> right;
	private SubpatternTypeNode type;

	/** Type for this declaration. */
	private static final TypeNode subpatternType = new SubpatternTypeNode();

	/**
	 * Make a new rule.
	 * @param id The identifier of this rule.
	 * @param left The left hand side (The pattern to match).
	 * @param right The right hand side(s).
	 */
	public SubpatternDeclNode(IdentNode id, PatternGraphNode left, CollectNode<RhsDeclNode> right) {
		super(id, subpatternType);
		this.pattern = left;
		becomeParent(this.pattern);
		this.right = right;
		becomeParent(this.right);
	}

	/** returns children of this node */
	@Override
	public Collection<BaseNode> getChildren() {
		Vector<BaseNode> children = new Vector<BaseNode>();
		children.add(ident);
		children.add(getValidVersion(typeUnresolved, type));
		children.add(pattern);
		children.add(right);
		return children;
	}

	/** returns names of the children, same order as in getChildren */
	@Override
	public Collection<String> getChildrenNames() {
		Vector<String> childrenNames = new Vector<String>();
		childrenNames.add("ident");
		childrenNames.add("type");
		childrenNames.add("pattern");
		childrenNames.add("right");
		return childrenNames;
	}

	private static DeclarationTypeResolver<SubpatternTypeNode> typeResolver =
		new DeclarationTypeResolver<SubpatternTypeNode>(SubpatternTypeNode.class);

	/** @see de.unika.ipd.grgen.ast.BaseNode#resolveLocal() */
	@Override
	protected boolean resolveLocal() {
		type = typeResolver.resolve(typeUnresolved, this);

		return type != null;
	}

	private Set<DeclNode> getDelete(int index) {
		return right.children.get(index).getDelete(pattern);
	}

	/** Check that only graph elements are returned, that are not deleted. */
	private boolean checkExecParamsNotDeleted() {
		boolean res = true;

		for (int i = 0; i < right.getChildren().size(); i++) {
    		Set<DeclNode> dels = getDelete(i);
    		for (BaseNode x : right.children.get(i).graph.imperativeStmts.getChildren()) {
    			if(x instanceof ExecNode) {
    				ExecNode exec = (ExecNode)x;
    				for(CallActionNode callAction : exec.callActions.getChildren())
    					if(!Collections.disjoint(callAction.params.getChildren(), dels)) {
    						// FIXME error message
    						callAction.reportError("Parameter of call \"" + callAction.getName() + "\"");
    						// TODO ...
    						res = false;
    					}


    			}
    		}
		}
		return res;
	}

	/* Checks, whether the reused nodes and edges of the RHS are consistent with the LHS.
	 * If consistent, replace the dummy nodes with the nodes the pattern edge is
	 * incident to (if these aren't dummy nodes themselves, of course). */
	private boolean checkRhsReuse() {
		boolean res = true;
		for (int i = 0; i < right.getChildren().size(); i++) {
    		Collection<EdgeDeclNode> alreadyReported = new HashSet<EdgeDeclNode>();
    		for (ConnectionNode rConn : right.children.get(i).getReusedConnections(pattern)) {
    			boolean occursInLHS = false;
    			EdgeDeclNode re = rConn.getEdge();

    			if (re instanceof EdgeTypeChangeNode) {
    				re = ((EdgeTypeChangeNode)re).getOldEdge();
    			}

    			for (BaseNode lc : pattern.getConnections()) {
    				if (!(lc instanceof ConnectionNode)) {
    					continue;
    				}

    				ConnectionNode lConn = (ConnectionNode) lc;

    				EdgeDeclNode le = lConn.getEdge();

    				if ( ! le.equals(re) ) {
    					continue;
    				}
    				occursInLHS = true;

    				if (lConn.getConnectionKind() != rConn.getConnectionKind()) {
    					res = false;
    					rConn.reportError("Reused edge does not have the same connection kind");
    					// if you don't add to alreadyReported erroneous errors can occur,
    					// e.g. lhs=x-e->y, rhs=y-e-x
    					alreadyReported.add(re);
    				}

    				NodeDeclNode lSrc = lConn.getSrc();
    				NodeDeclNode lTgt = lConn.getTgt();
    				NodeDeclNode rSrc = rConn.getSrc();
    				NodeDeclNode rTgt = rConn.getTgt();

					HashSet<BaseNode> rhsNodes = new HashSet<BaseNode>();
					rhsNodes.addAll(right.children.get(i).getReusedNodes(pattern));

    				if (rSrc instanceof NodeTypeChangeNode) {
    					rSrc = ((NodeTypeChangeNode)rSrc).getOldNode();
    					rhsNodes.add(rSrc);
    				}
    				if (rTgt instanceof NodeTypeChangeNode) {
    					rTgt = ((NodeTypeChangeNode)rTgt).getOldNode();
    					rhsNodes.add(rTgt);
    				}

    				if ( ! lSrc.isDummy() ) {
    					if ( rSrc.isDummy() ) {
    						if ( rhsNodes.contains(lSrc) ) {
    							//replace the dummy src node by the src node of the pattern connection
    							rConn.setSrc(lSrc);
    						} else if ( ! alreadyReported.contains(re) ) {
    							res = false;
    							rConn.reportError("The source node of reused edge \"" + le + "\" must be reused, too");
    							alreadyReported.add(re);
    						}
    					} else if (lSrc != rSrc && ! alreadyReported.contains(re)) {
    						res = false;
    						rConn.reportError("Reused edge \"" + le + "\" does not connect the same nodes");
    						alreadyReported.add(re);
    					}
    				}

    				if ( ! lTgt.isDummy() ) {
    					if ( rTgt.isDummy() ) {
    						if ( rhsNodes.contains(lTgt) ) {
    							//replace the dummy tgt node by the tgt node of the pattern connection
    							rConn.setTgt(lTgt);
    						} else if ( ! alreadyReported.contains(re) ) {
    							res = false;
    							rConn.reportError("The target node of reused edge \"" + le + "\" must be reused, too");
    							alreadyReported.add(re);
    						}
    					} else if ( lTgt != rTgt && ! alreadyReported.contains(re)) {
    						res = false;
    						rConn.reportError("Reused edge \"" + le + "\" does not connect the same nodes");
    						alreadyReported.add(re);
    					}
    				}

    				//check, whether RHS "adds" a node to a dangling end of a edge
    				if ( ! alreadyReported.contains(re) ) {
    					if ( lSrc.isDummy() && ! rSrc.isDummy() ) {
    						res = false;
    						rConn.reportError("Reused edge dangles on LHS, but has a source node on RHS");
    						alreadyReported.add(re);
    					}
    					if ( lTgt.isDummy() && ! rTgt.isDummy() ) {
    						res = false;
    						rConn.reportError("Reused edge dangles on LHS, but has a target node on RHS");
    						alreadyReported.add(re);
    					}
    				}
    			}
    			if (!occursInLHS) {
    				// alreadyReported can not be set here
    				if (rConn.getConnectionKind() == ConnectionNode.ARBITRARY) {
    					res = false;
    					rConn.reportError("New instances of ?--? are not allowed in RHS");
    				}
    				if (rConn.getConnectionKind() == ConnectionNode.ARBITRARY_DIRECTED) {
    					res = false;
    					rConn.reportError("New instances of <--> are not allowed in RHS");
    				}
    			}
    		}
		}
		return res;
	}

	private boolean checkLeft() {
		// check if reused names of edges connect the same nodes in the same direction with the same edge kind for each usage
		boolean edgeReUse = false;
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

		return edgeReUse;
	}

	private boolean SameParametersInNestedAlternativeReplacementsAsInReplacement() {
		boolean res = true;

		for(AlternativeNode alt : pattern.alts.getChildren()) {
			for(AlternativeCaseNode altCase : alt.getChildren()) {
				if(right.getChildren().size()!=altCase.right.getChildren().size()) {
					error.error(getCoords(), "Different number of replacement patterns in subpattern " + ident.toString()
							+ " and nested alternative case " + altCase.ident.toString());
					res = false;
					continue;
				}

				if(right.getChildren().size()==0) continue;

				Vector<DeclNode> parameters = right.children.get(0).graph.getParamDecls(); // todo: choose the right one
				Vector<DeclNode> parametersInNestedAlternativeCase =
					altCase.right.children.get(0).graph.getParamDecls(); // todo: choose the right one

				if(parameters.size()!=parametersInNestedAlternativeCase.size()) {
					error.error(getCoords(), "Different number of replacement parameters in subpattern " + ident.toString()
							+ " and nested alternative case " + altCase.ident.toString());
					res = false;
					continue;
				}

				// check if the types of the parameters are the same
				for (int i = 0; i < parameters.size(); ++i) {
					DeclNode parameter = (DeclNode)parameters.get(i);
					DeclNode parameterInNestedAlternativeCase = (DeclNode)parametersInNestedAlternativeCase.get(i);
					TypeNode parameterType = parameter.getDeclType();
					TypeNode parameterInNestedAlternativeCaseType = parameterInNestedAlternativeCase.getDeclType();

					if(!parameterType.isEqual(parameterInNestedAlternativeCaseType)) {
						parameterInNestedAlternativeCase.ident.reportError("Different type of replacement parameter in nested alternative case " + altCase.ident.toString()
								+ " at parameter " + parameterInNestedAlternativeCase.ident.toString() + " compared to replacement parameter in subpattern " + ident.toString());
						res = false;
					}
				}
			}
		}

		return res;
	}

	/**
	 * Check, if the rule type node is right.
	 * The children of a rule type are
	 * 1) a pattern for the left side.
	 * 2) a pattern for the right side.
	 * @see de.unika.ipd.grgen.ast.BaseNode#checkLocal()
	 */
	@Override
	protected boolean checkLocal() {
		for (int i = 0; i < right.getChildren().size(); i++) {
			right.children.get(i).warnElemAppearsInsideAndOutsideDelete(pattern);
		}

		boolean leftHandGraphsOk = checkLeft();

		boolean noReturnInPatternOk = true;
		if(pattern.returns.children.size() > 0) {
			error.error(getCoords(), "No return statements in pattern parts of rules allowed");
			noReturnInPatternOk = false;
		}

		boolean noDeleteOfPatternParameters = true;
		boolean abstr = true;

		for (int i = 0; i < right.getChildren().size(); i++) {
    		GraphNode right = this.right.children.get(i).graph;

    		// check if parameters of patterns are deleted
    		Collection<DeclNode> deletedEnities = getDelete(i);
    		for (DeclNode p : pattern.getParamDecls()) {
    			if (deletedEnities.contains(p)) {
    				error.error(getCoords(), "Deletion of parameters in patterns are not allowed");
    				noDeleteOfPatternParameters = false;
    			}
            }

    		for(NodeDeclNode node : right.getNodes()) {
    			if(!node.inheritsType() && node.getDeclType().isAbstract() && !pattern.getNodes().contains(node)) {
    				error.error(node.getCoords(), "Instances of abstract nodes are not allowed");
    				abstr = false;
    			}
    		}
    		for(EdgeDeclNode edge : right.getEdges()) {
    			if(!edge.inheritsType() && edge.getDeclType().isAbstract() && !pattern.getEdges().contains(edge)) {
    				error.error(edge.getCoords(), "Instances of abstract edges are not allowed");
    				abstr = false;
    			}
    		}
		}

		return leftHandGraphsOk & noDeleteOfPatternParameters
			& SameParametersInNestedAlternativeReplacementsAsInReplacement()
			& checkRhsReuse() & noReturnInPatternOk & abstr
			& checkExecParamsNotDeleted();
	}

	private void constructIRaux(Rule rule) {
		// add parameters to the IR
		PatternGraph patternGraph = rule.getPattern();
		for(DeclNode decl : pattern.getParamDecls()) {
			rule.addParameter(decl.checkIR(Entity.class));
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

		// add replacement parameters to the IR
		// TODO choose the right one
		PatternGraph right = null;
		if(this.right.children.size() > 0) {
			right = this.right.children.get(0).getPatternGraph(pattern.getPatternGraph());
		}
		else {
			return;
		}

		for(DeclNode decl : this.right.children.get(0).graph.getParamDecls()) {
			if(decl instanceof NodeCharacter) {
				rule.addReplParameter(decl.checkIR(Node.class));
				right.addSingleNode(((NodeCharacter) decl).getNode());
			} else if(decl instanceof VarDeclNode) {
				rule.addReplParameter(decl.checkIR(Variable.class));
				right.addVariable(((VarDeclNode) decl).getVariable());
			} else {
				throw new IllegalArgumentException("unknown Class: " + decl);
			}
		}
	}

	/**
	 * @see de.unika.ipd.grgen.ast.BaseNode#constructIR()
	 */
	// TODO support only one rhs
	@Override
	protected IR constructIR() {
		PatternGraph left = pattern.getPatternGraph();

		// return if the pattern graph already constructed the IR object
		// that may happens in recursive patterns
		if (isIRAlreadySet()) {
			return getIR();
		}

		// TODO choose the right one
		PatternGraph right = null;
		if(this.right.children.size() > 0) {
			right = this.right.children.get(0).getPatternGraph(left);
		}

		// return if the pattern graph already constructed the IR object
		// that may happens in recursive patterns
		if (isIRAlreadySet()) {
			return getIR();
		}

		Rule rule = new Rule(getIdentNode().getIdent(), left, right);

		constructImplicitNegs(left);
		constructIRaux(rule);

		// add Eval statements to the IR
		// TODO choose the right one
		if(this.right.children.size() > 0) {
			for (EvalStatement n : this.right.children.get(0).getEvalStatements()) {
				rule.addEval(n);
			}
		}

		return rule;
	}


	// TODO use this to create IR patterns, that is currently not supported by
	//      any backend
	/*private IR constructPatternIR() {
		PatternGraph left = pattern.getPatternGraph();

		// return if the pattern graph already constructed the IR object
		// that may happens in recursive patterns
		if (isIRAlreadySet()) {
			return getIR();
		}

		Vector<PatternGraph> right = new Vector<PatternGraph>();
		for (int i = 0; i < this.right.children.size(); i++) {
			right.add(this.right.children.get(i).getPatternGraph(left));
		}

		// return if the pattern graph already constructed the IR object
		// that may happens in recursive patterns
		if (isIRAlreadySet()) {
			return getIR();
		}

		Pattern pattern = new Pattern(getIdentNode().getIdent(), left, right);

		constructImplicitNegs(left);
		constructIRaux(pattern);

		// add Eval statements to the IR
		for (int i = 0; i < this.right.children.size(); i++) {
    		for (Assignment n : this.right.children.get(i).getAssignments()) {
    			pattern.addEval(i,n);
    		}
		}

		return pattern;
	}*/

	/**
	 * add NACs for induced- or DPO-semantic
	 */
	private void constructImplicitNegs(PatternGraph left) {
		PatternGraphNode leftNode = pattern;
		for (PatternGraph neg : leftNode.getImplicitNegGraphs()) {
			left.addNegGraph(neg);
		}
	}

	@Override
	public SubpatternTypeNode getDeclType() {
		assert isResolved();

		return type;
	}

	protected PatternGraphNode getPattern() {
		assert isResolved();
		return pattern;
	}

	public static String getKindStr() {
		return "subpattern declaration";
	}

	public static String getUseStr() {
		return "subpattern";
	}
}
