/*
 * GrGen: graph rewrite generator tool -- release GrGen.NET 2.6
 * Copyright (C) 2003-2010 Universitaet Karlsruhe, Institut fuer Programmstrukturen und Datenorganisation, LS Goos
 * licensed under LGPL v3 (see LICENSE.txt included in the packaging of this file)
 * www.grgen.net
 */

/**
 * @author Sebastian Buchwald
 * @version $Id$
 */
package de.unika.ipd.grgen.ast;


import java.util.Collection;
import java.util.LinkedHashSet;
import java.util.Set;
import java.util.Vector;

import de.unika.ipd.grgen.ast.util.DeclarationTypeResolver;
import de.unika.ipd.grgen.ir.Edge;
import de.unika.ipd.grgen.ir.EvalStatement;
import de.unika.ipd.grgen.ir.IR;
import de.unika.ipd.grgen.ir.NeededEntities;
import de.unika.ipd.grgen.ir.Node;
import de.unika.ipd.grgen.ir.PatternGraph;
import de.unika.ipd.grgen.ir.Variable;


/**
 * AST node for a replacement right-hand side.
 */
public abstract class RhsDeclNode extends DeclNode {
	static {
		setName(RhsDeclNode.class, "right-hand side declaration");
	}

	protected GraphNode graph;
	protected CollectNode<EvalStatementNode> eval;
	protected RhsTypeNode type;

	/** Type for this declaration. */
	protected static final TypeNode rhsType = new RhsTypeNode();

	/**
	 * Make a new right-hand side.
	 * @param id The identifier of this RHS.
	 * @param graph The right hand side graph.
	 * @param eval The evaluations.
	 */
	public RhsDeclNode(IdentNode id, GraphNode graph, CollectNode<EvalStatementNode> eval) {
		super(id, rhsType);
		this.graph = graph;
		becomeParent(this.graph);
		this.eval = eval;
		becomeParent(this.eval);
	}

	protected Collection<DeclNode> getMaybeDeleted(PatternGraphNode pattern) {
		Collection<DeclNode> ret = new LinkedHashSet<DeclNode>();
		ret.addAll(getDelete(pattern));

		// check if a deleted node exists
		Collection<NodeDeclNode> nodes = new LinkedHashSet<NodeDeclNode>();
		for (DeclNode declNode : ret) {

			if (declNode instanceof NodeDeclNode) {
	        	nodes.add((NodeDeclNode) declNode);
	        }
        }


		if (nodes.size() > 0) {
			// add homomorphic nodes
			for (NodeDeclNode node : nodes) {
				ret.addAll(pattern.getHomomorphic(node));
            }

    		Collection<ConnectionNode> conns = getResultingConnections(pattern);
    		for (ConnectionNode conn : conns) {
    			if (sourceOrTargetNodeIncluded(pattern, ret, conn.getEdge())) {
    				ret.add(conn.getEdge());
    			}
            }

			// nodes of dangling edges are homomorphic to all other nodes,
    		// especially the deleted ones :-)
    		for (ConnectionNode conn : conns) {
    			EdgeDeclNode edge = conn.getEdge();
    			while (edge instanceof EdgeTypeChangeNode) {
    				edge = ((EdgeTypeChangeNode) edge).getOldEdge();
    			}
    			boolean srcIsDummy = true;
    			boolean tgtIsDummy = true;
    			for (ConnectionNode innerConn : conns) {
        			if (edge.equals(innerConn.getEdge())) {
        				srcIsDummy &= innerConn.getSrc().isDummy();
        				tgtIsDummy &= innerConn.getTgt().isDummy();
        			}
                }

    			if (srcIsDummy || tgtIsDummy) {
    				ret.add(edge);
    			}
            }
		}

		// add homomorphic edges
		Collection<EdgeDeclNode> edges = new LinkedHashSet<EdgeDeclNode>();
		for (DeclNode declNode : ret) {
	        if (declNode instanceof EdgeDeclNode) {
	        	edges.add((EdgeDeclNode) declNode);
	        }
        }
		for (EdgeDeclNode edge : edges) {
			ret.addAll(pattern.getHomomorphic(edge));
        }

		return ret;
	}

	/** only used in checks against usage of deleted elements */
	protected abstract Collection<ConnectionNode> getResultingConnections(PatternGraphNode pattern);

	/** returns children of this node */
	@Override
	public Collection<BaseNode> getChildren() {
		Vector<BaseNode> children = new Vector<BaseNode>();
		children.add(ident);
		children.add(getValidVersion(typeUnresolved, type));
		children.add(graph);
		children.add(eval);
		return children;
	}

	/** returns names of the children, same order as in getChildren */
	@Override
	public Collection<String> getChildrenNames() {
		Vector<String> childrenNames = new Vector<String>();
		childrenNames.add("ident");
		childrenNames.add("type");
		childrenNames.add("right");
		childrenNames.add("eval");
		return childrenNames;
	}

	protected static final DeclarationTypeResolver<RhsTypeNode> typeResolver =	new DeclarationTypeResolver<RhsTypeNode>(RhsTypeNode.class);

	/** @see de.unika.ipd.grgen.ast.BaseNode#resolveLocal() */
	@Override
	protected boolean resolveLocal() {
		type = typeResolver.resolve(typeUnresolved, this);

		return type != null;
	}

	/**
	 * Edges as replacement parameters are not really needed but very troublesome,
	 * keep them out for now.
	 */
	private boolean checkEdgeParameters() {
		boolean res = true;

		for (DeclNode replParam : graph.getParamDecls()) {
			if (replParam instanceof EdgeDeclNode) {
				replParam.reportError("edges not supported as replacement parameters: "
								+ replParam.ident.toString());
				res = false;
			}
		}

		return res;
	}

	/**
	 * @see de.unika.ipd.grgen.ast.BaseNode#checkLocal()
	 */
	@Override
	protected boolean checkLocal() {
		return checkEdgeParameters();
	}

	/**
	 * @see de.unika.ipd.grgen.ast.BaseNode#constructIR()
	 */
	@Override
	protected IR constructIR() {
		assert false;

		return null;
	}

	protected Collection<EvalStatement> getEvalStatements() {
		Collection<EvalStatement> ret = new LinkedHashSet<EvalStatement>();

		for (EvalStatementNode n : eval.getChildren()) {
			ret.add(n.checkIR(EvalStatement.class));
		}

		return ret;
	}

	protected void insertElementsFromEvalIntoRhs(PatternGraph left, PatternGraph right)
	{
		// insert all elements, which are used in eval statements (of the right hand side) and
		// neither declared on the local left hand nor on the right hand side to the right hand side
		// further code (PatternGraph::insertElementsFromRhsDeclaredInNestingLhsToLocalLhs)
		// will add them to the left hand side, too

		NeededEntities needs = new NeededEntities(true, true, true, false, false, false);
		Collection<EvalStatement> evalStatements = getEvalStatements();
		for(EvalStatement eval : evalStatements) {
			eval.collectNeededEntities(needs);
		}

		for(Node neededNode : needs.nodes) {
			if(neededNode.directlyNestingLHSGraph!=left) {
				if(!right.hasNode(neededNode)) {
					right.addSingleNode(neededNode);
					right.addHomToAll(neededNode);
				}
			}
		}
		for(Edge neededEdge : needs.edges) {
			if(neededEdge.directlyNestingLHSGraph!=left) {
				if(!right.hasEdge(neededEdge)) {
					right.addSingleEdge(neededEdge);	// TODO: maybe we lose context here
					right.addHomToAll(neededEdge);
				}
			}
		}
		for(Variable neededVariable : needs.variables) {
			if(neededVariable.directlyNestingLHSGraph!=left) {
				if(!right.hasVar(neededVariable)) {
					right.addVariable(neededVariable);
				}
			}
		}
	}

	protected abstract PatternGraph getPatternGraph(PatternGraph left);

	@Override
	public RhsTypeNode getDeclType() {
		assert isResolved();

		return type;
	}

	/** only used in checks against usage of deleted elements */
	protected abstract Set<DeclNode> getDelete(PatternGraphNode pattern);

	/**
	 * Return all reused edges (with their nodes), that excludes new edges of
	 * the right-hand side.
	 */
	protected abstract Collection<ConnectionNode> getReusedConnections(PatternGraphNode pattern);

	/**
	 * Return all reused nodes, that excludes new nodes of the right-hand side.
	 */
	protected abstract Set<BaseNode> getReusedNodes(PatternGraphNode pattern);

	protected abstract void warnElemAppearsInsideAndOutsideDelete(PatternGraphNode pattern);

	protected boolean sourceOrTargetNodeIncluded(PatternGraphNode pattern, Collection<? extends BaseNode> coll,
            EdgeDeclNode edgeDecl)
    {
    	for (BaseNode n : pattern.getConnections()) {
            if (n instanceof ConnectionNode) {
            	ConnectionNode conn = (ConnectionNode) n;
            	if (conn.getEdge().equals(edgeDecl)) {
            		if (coll.contains(conn.getSrc())
            				|| coll.contains(conn.getTgt())) {
            			return true;
            		}
            	}
            }
        }
    	return false;
    }
}

