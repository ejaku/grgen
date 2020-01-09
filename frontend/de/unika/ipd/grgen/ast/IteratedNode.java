/*
 * GrGen: graph rewrite generator tool -- release GrGen.NET 4.5
 * Copyright (C) 2003-2020 Universitaet Karlsruhe, Institut fuer Programmstrukturen und Datenorganisation, LS Goos; and free programmers
 * licensed under LGPL v3 (see LICENSE.txt included in the packaging of this file)
 * www.grgen.net
 */

/**
 * @author Edgar Jakumeit
 */

package de.unika.ipd.grgen.ast;

import java.util.Collection;
import java.util.HashMap;
import java.util.HashSet;
import java.util.LinkedList;
import java.util.Set;
import java.util.Vector;

import de.unika.ipd.grgen.ast.exprevals.*;
import de.unika.ipd.grgen.ast.util.DeclarationTypeResolver;
import de.unika.ipd.grgen.ir.Alternative;
import de.unika.ipd.grgen.ir.Edge;
import de.unika.ipd.grgen.ir.Entity;
import de.unika.ipd.grgen.ir.exprevals.EvalStatements;
import de.unika.ipd.grgen.ir.IR;
import de.unika.ipd.grgen.ir.Node;
import de.unika.ipd.grgen.ir.PatternGraph;
import de.unika.ipd.grgen.ir.Rule;
import de.unika.ipd.grgen.ir.Variable;


/**
 * AST node for an iterated pattern, maybe including replacements.
 */
public class IteratedNode extends ActionDeclNode  {
	static {
		setName(IteratedNode.class, "iterated");
	}

	protected PatternGraphNode pattern;
	protected CollectNode<RhsDeclNode> right;
	private IteratedTypeNode type;
	private int minMatches;
	private int maxMatches;

	/** Type for this declaration. */
	private static IteratedTypeNode iteratedType = new IteratedTypeNode();

	/**
	 * Make a new iterated rule.
	 * @param left The left hand side (The pattern to match).
	 * @param right The right hand side(s).
	 */
	public IteratedNode(IdentNode id, PatternGraphNode left, CollectNode<RhsDeclNode> right,
			int minMatches, int maxMatches) {
		super(id, iteratedType);
		this.pattern = left;
		becomeParent(this.pattern);
		this.right = right;
		becomeParent(this.right);
		this.minMatches = minMatches;
		this.maxMatches = maxMatches;
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

	protected static final DeclarationTypeResolver<IteratedTypeNode> typeResolver =
		new DeclarationTypeResolver<IteratedTypeNode>(IteratedTypeNode.class);

	/** @see de.unika.ipd.grgen.ast.BaseNode#resolveLocal() */
	@Override
	protected boolean resolveLocal() {
		type = typeResolver.resolve(typeUnresolved, this);

		return type != null;
	}

	// TODO: pull this and the other code duplications up to ActionDeclNode
	/** Checks, whether the reused nodes and edges of the RHS are consistent with the LHS.
	 * If consistent, replace the dummy nodes with the nodes the pattern edge is
	 * incident to (if these aren't dummy nodes themselves, of course). */
	private boolean checkRhsReuse() {
		boolean res = true;
		HashMap<EdgeDeclNode, NodeDeclNode> redirectedFrom = new HashMap<EdgeDeclNode, NodeDeclNode>();
		HashMap<EdgeDeclNode, NodeDeclNode> redirectedTo = new HashMap<EdgeDeclNode, NodeDeclNode>();
		for (int i = 0; i < right.getChildren().size(); i++) {
    		Collection<EdgeDeclNode> alreadyReported = new HashSet<EdgeDeclNode>();
    		for (ConnectionNode rConn : right.children.get(i).getReusedConnections(pattern)) {
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
    					} else if (lSrc != rSrc && (rConn.getRedirectionKind() & ConnectionNode.REDIRECT_SOURCE)!=ConnectionNode.REDIRECT_SOURCE && ! alreadyReported.contains(re)) {
    						res = false;
    						rConn.reportError("Reused edge \"" + le + "\" does not connect the same nodes (and is not declared to redirect source)");
    						alreadyReported.add(re);
    					}
    				}
    				
    				if ( (rConn.getRedirectionKind() & ConnectionNode.REDIRECT_SOURCE)==ConnectionNode.REDIRECT_SOURCE ) {
    					if(rSrc.isDummy()) {
							res = false;
							rConn.reportError("An edge with source redirection must be given a source node.");
    					}
    					
    					if(lSrc.equals(rSrc)) {
    						rConn.reportWarning("Redirecting edge to same source again.");
    					}
    					
    					if(redirectedFrom.containsKey(le)) {
							res = false;
							rConn.reportError("Can't redirect edge source more than once.");
    					}
    					redirectedFrom.put(le, rSrc);
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
    					} else if ( lTgt != rTgt && (rConn.getRedirectionKind() & ConnectionNode.REDIRECT_TARGET)!=ConnectionNode.REDIRECT_TARGET && ! alreadyReported.contains(re)) {
    						res = false;
    						rConn.reportError("Reused edge \"" + le + "\" does not connect the same nodes (and is not declared to redirect target)");
    						alreadyReported.add(re);
    					}
    				}
    				
    				if ( (rConn.getRedirectionKind() & ConnectionNode.REDIRECT_TARGET)==ConnectionNode.REDIRECT_TARGET ) {
    					if(rTgt.isDummy()) {
							res = false;
							rConn.reportError("An edge with target redirection must be given a target node.");
    					}
    					
    					if(lTgt.equals(rTgt)) {
    						rConn.reportWarning("Redirecting edge to same target again.");
    					}
    					
    					if(redirectedTo.containsKey(le)) {
							res = false;
							rConn.reportError("Can't redirect edge target more than once.");
    					}
    					redirectedTo.put(le, rSrc);
					}

    				//check, whether RHS "adds" a node to a dangling end of a edge
    				if ( ! alreadyReported.contains(re) ) {
    					if ( lSrc.isDummy() && ! rSrc.isDummy() && (rConn.getRedirectionKind() & ConnectionNode.REDIRECT_SOURCE)!=ConnectionNode.REDIRECT_SOURCE ) {
    						res = false;
    						rConn.reportError("Reused edge dangles on LHS, but has a source node on RHS");
    						alreadyReported.add(re);
    					}
    					if ( lTgt.isDummy() && ! rTgt.isDummy() && (rConn.getRedirectionKind() & ConnectionNode.REDIRECT_TARGET)!=ConnectionNode.REDIRECT_TARGET ) {
    						res = false;
    						rConn.reportError("Reused edge dangles on LHS, but has a target node on RHS");
    						alreadyReported.add(re);
    					}
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

	private boolean SameNumberOfRewritePartsAndNoNestedRewriteParameters() {
		boolean res = true;

		for(AlternativeNode alt : pattern.alts.getChildren()) {
			for(AlternativeCaseNode altCase : alt.getChildren()) {
				if(right.getChildren().size()!=altCase.right.getChildren().size()) {
					error.error(getCoords(), "Different number of replacement patterns/rewrite parts in iterated/multiple/optional " + ident.toString()
							+ " and nested alternative case " + altCase.ident.toString());
					res = false;
					continue;
				}

				if(right.getChildren().size()==0) continue;

				Vector<DeclNode> parametersInNestedAlternativeCase =
					altCase.right.children.get(0).graph.getParamDecls(); // todo: choose the right one

				if(parametersInNestedAlternativeCase.size()!=0) {
					error.error(altCase.getCoords(), "No replacement parameters allowed in nested alternative cases; given in " + altCase.ident.toString());
					res = false;
					continue;
				}
			}
		}

		for(IteratedNode iter : pattern.iters.getChildren()) {
			if(right.getChildren().size()!=iter.right.getChildren().size()) {
				error.error(getCoords(), "Different number of replacement patterns/rewrite parts in iterated/multiple/optional " + ident.toString()
						+ " and nested iterated/multiple/optional " + iter.ident.toString());
				res = false;
				continue;
			}

			if(right.getChildren().size()==0) continue;

			Vector<DeclNode> parametersInNestedIterated =
				iter.right.children.get(0).graph.getParamDecls(); // todo: choose the right one

			if(parametersInNestedIterated.size()!=0) {
				error.error(iter.getCoords(), "No replacement parameters allowed in nested iterated/multiple/optional; given in " + iter.ident.toString());
				res = false;
				continue;
			}
		}

		return res;
	}

	/**
	 * Check that exec parameters are not deleted.
	 *
	 * The check consider the case that parameters are deleted due to
	 * homomorphic matching.
	 */
	private boolean checkExecParamsNotDeleted() {
		assert isResolved();

		boolean valid = true;

		if(right.getChildren().size()==0)
			return valid;

		Set<DeclNode> delete = right.children.get(0).getDelete(pattern);
		Collection<DeclNode> maybeDeleted = right.children.get(0).getMaybeDeleted(pattern);

		for (BaseNode x : right.children.get(0).graph.imperativeStmts.getChildren()) {
			if(!(x instanceof ExecNode)) continue;

			ExecNode exec = (ExecNode) x;
			for(CallActionNode callAction : exec.callActions.getChildren()) {
				for(ExprNode arg : callAction.params.getChildren()) {
					if(!(arg instanceof DeclExprNode)) continue;

					ConstraintDeclNode declNode = ((DeclExprNode) arg).getConstraintDeclNode();
					if(declNode != null) {
						if(delete.contains(declNode)) {
							arg.reportError("The deleted " + declNode.getUseString()
									+ " \"" + declNode.ident + "\" must not be passed to an exec statement");
							valid = false;
						}
						else if (maybeDeleted.contains(declNode)) {
							declNode.maybeDeleted = true;

							if(!declNode.getIdentNode().getAnnotations().isFlagSet("maybeDeleted")) {
								valid = false;

								String errorMessage = "Parameter \"" + declNode.ident + "\" of exec statement may be deleted"
										+ ", possibly it's homomorphic with a deleted " + declNode.getUseString();
								errorMessage += " (use a [maybeDeleted] annotation if you think that this does not cause problems)";

								if(declNode instanceof EdgeDeclNode) {
									errorMessage += " or \"" + declNode.ident + "\" is a dangling " + declNode.getUseString()
											+ " and a deleted node exists";
								}
								arg.reportError(errorMessage);
							}
						}
					}
				}
			}
		}
		return valid;
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

		boolean noReturnInAlterntiveCaseReplacement = true;
		for (int i = 0; i < right.getChildren().size(); i++) {
			if(right.children.get(i).graph.returns.children.size() > 0) {
				error.error(getCoords(), "No return statements in alternative cases allowed");
				noReturnInAlterntiveCaseReplacement = false;
			}
		}

		boolean abstr = true;

		for (int i = 0; i < right.getChildren().size(); i++) {
    		GraphNode right = this.right.children.get(i).graph;

nodeAbstrLoop:
            for (NodeDeclNode node : right.getNodes()) {
                if (!node.inheritsType() && node.getDeclType().isAbstract()) {
                    if ((node.context & CONTEXT_PARAMETER) == CONTEXT_PARAMETER) {
                        continue;
                    }
                    for (PatternGraphNode pattern = this.pattern; pattern != null;
                            pattern = getParentPatternGraph(pattern)) {
                        if (pattern.getNodes().contains(node)) {
                            continue nodeAbstrLoop;
                        }
                    }
    				error.error(node.getCoords(), "Instances of abstract nodes are not allowed");
    				abstr = false;
    			}
    		}

edgeAbstrLoop:
            for (EdgeDeclNode edge : right.getEdges()) {
                if (!edge.inheritsType() && edge.getDeclType().isAbstract()) {
                    if ((edge.context & CONTEXT_PARAMETER) == CONTEXT_PARAMETER) {
                        continue;
                    }
                    for (PatternGraphNode pattern = this.pattern; pattern != null;
                            pattern = getParentPatternGraph(pattern)) {
                        if (pattern.getEdges().contains(edge)) {
                            continue edgeAbstrLoop;
                        }
                    }
    				error.error(edge.getCoords(), "Instances of abstract edges are not allowed");
    				abstr = false;
    			}
    		}
		}

		return leftHandGraphsOk & SameNumberOfRewritePartsAndNoNestedRewriteParameters()
			& checkRhsReuse() & noReturnInPatternOk & noReturnInAlterntiveCaseReplacement
			& checkExecParamsNotDeleted() & abstr;
	}

	private void constructIRaux(Rule rule) {
		PatternGraph patternGraph = rule.getPattern();

		// add Params to the IR
		for(DeclNode decl : pattern.getParamDecls()) {
			rule.addParameter(decl.checkIR(Entity.class));
			if(decl instanceof NodeCharacter) {
				patternGraph.addSingleNode(((NodeCharacter)decl).getNode());
			} else if (decl instanceof EdgeCharacter) {
				Edge e = ((EdgeCharacter)decl).getEdge();
				patternGraph.addSingleEdge(e);
			} else {
				throw new IllegalArgumentException("unknown Class: " + decl);
			}
		}

		// add replacement parameters to the IR
		// TODO choose the right one
		PatternGraph right = null;
		if(this.right.children.size() > 0) {
			right = this.right.children.get(0).getPatternGraph(pattern.getPatternGraph());
		} else {
			return;
		}

		// add replacement parameters to the current graph
		for(DeclNode decl : this.right.children.get(0).graph.getParamDecls()) {
			if(decl instanceof NodeCharacter) {
				right.addReplParameter(decl.checkIR(Node.class));
				right.addSingleNode(((NodeCharacter) decl).getNode());
			} else if(decl instanceof VarDeclNode) {
				right.addReplParameter(decl.checkIR(Variable.class));
				right.addVariable(((VarDeclNode) decl).getVariable());
			} else {
				throw new IllegalArgumentException("unknown Class: " + decl);
			}
		}

		// and also to the nested alternatives and iterateds
		addReplacementParamsToNestedAlternativesAndIterateds(rule);
	}

	private void addReplacementParamsToNestedAlternativesAndIterateds(Rule rule) {
		// TODO choose the right one
		if(right.children.size()==0) {
			return;
		}

		// add replacement parameters to the nested alternatives and iterateds
		PatternGraph patternGraph = rule.getPattern();
		for(DeclNode decl : this.right.children.get(0).graph.getParamDecls()) {
			if(decl instanceof NodeCharacter) {
				for(Alternative alt : patternGraph.getAlts()) {
					for(Rule altCase : alt.getAlternativeCases()) {
						altCase.getRight().addReplParameter(decl.checkIR(Node.class));
						altCase.getRight().addSingleNode(((NodeCharacter) decl).getNode());
					}
				}
				for(Rule iter : patternGraph.getIters()) {
					iter.getRight().addReplParameter(decl.checkIR(Node.class));
					iter.getRight().addSingleNode(((NodeCharacter) decl).getNode());
				}
			} else if(decl instanceof VarDeclNode) {
				for(Alternative alt : patternGraph.getAlts()) {
					for(Rule altCase : alt.getAlternativeCases()) {
						altCase.getRight().addReplParameter(decl.checkIR(Variable.class));
						altCase.getRight().addVariable(((VarDeclNode) decl).getVariable());
					}
				}
				for(Rule iter : patternGraph.getIters()) {
					iter.getRight().addReplParameter(decl.checkIR(Variable.class));
					iter.getRight().addVariable(((VarDeclNode) decl).getVariable());
				}
			} else {
				throw new IllegalArgumentException("unknown Class: " + decl);
			}
		}
	}

	public PatternGraphNode getLeft()
	{
		return pattern;
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
			addReplacementParamsToNestedAlternativesAndIterateds((Rule)getIR());
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
			addReplacementParamsToNestedAlternativesAndIterateds((Rule)getIR());
			return getIR();
		}

		Rule iteratedRule = new Rule(getIdentNode().getIdent(), left, right,
				minMatches, maxMatches);

		constructImplicitNegs(left);
		constructIRaux(iteratedRule);

		// add Eval statements to the IR
		// TODO choose the right one
		if(this.right.children.size() > 0) {
			for (EvalStatements n : this.right.children.get(0).getRHSGraph().getYieldEvalStatements()) {
				iteratedRule.addEval(n);
			}
		}

		return iteratedRule;
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
	public IteratedTypeNode getDeclType() {
		assert isResolved();

		return iteratedType;
	}

	public static String getKindStr() {
		return "iterated node";
	}

	public static String getUseStr() {
		return "iterated";
	}
}
