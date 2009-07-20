/*
 * GrGen: graph rewrite generator tool -- release GrGen.NET 2.5
 * Copyright (C) 2009 Universitaet Karlsruhe, Institut fuer Programmstrukturen und Datenorganisation, LS Goos
 * licensed under LGPL v3 (see LICENSE.txt included in the packaging of this file)
 */

/**
 * @author Sebastian Buchwald
 * @version $Id$
 */
package de.unika.ipd.grgen.ast;


import de.unika.ipd.grgen.ast.util.CollectTripleResolver;
import de.unika.ipd.grgen.ast.util.DeclarationTripleResolver;
import de.unika.ipd.grgen.ast.util.Triple;
import de.unika.ipd.grgen.ir.Edge;
import de.unika.ipd.grgen.ir.Entity;
import de.unika.ipd.grgen.ir.Node;
import de.unika.ipd.grgen.ir.PatternGraph;
import de.unika.ipd.grgen.ir.SubpatternDependentReplacement;
import de.unika.ipd.grgen.ir.SubpatternUsage;
import java.util.Collection;
import java.util.Collections;
import java.util.HashSet;
import java.util.LinkedHashSet;
import java.util.Set;
import java.util.Vector;


/**
 * AST node for a modify right-hand side.
 */
public class ModifyDeclNode extends RhsDeclNode {
	static {
		setName(ModifyDeclNode.class, "modify declaration");
	}

	private CollectNode<IdentNode> deleteUnresolved;
	private CollectNode<DeclNode> delete = new CollectNode<DeclNode>();

	// Cache variables
	private Set<DeclNode> deletedElements;
	private Set<BaseNode> reusedNodes;


	/**
	 * Make a new modify right-hand side.
	 * @param id The identifier of this RHS.
	 * @param graph The right hand side graph.
	 * @param eval The evaluations.
	 */
	public ModifyDeclNode(IdentNode id, GraphNode graph, CollectNode<EvalStatementNode> eval,
			CollectNode<IdentNode> dels) {
		super(id, graph, eval);
		this.deleteUnresolved = dels;
		becomeParent(this.deleteUnresolved);
	}

	/** returns children of this node */
	public Collection<BaseNode> getChildren() {
		Vector<BaseNode> children = new Vector<BaseNode>();
		children.add(ident);
		children.add(getValidVersion(typeUnresolved, type));
		children.add(graph);
		children.add(eval);
		children.add(getValidVersion(deleteUnresolved, delete));
		return children;
	}

	/** returns names of the children, same order as in getChildren */
	public Collection<String> getChildrenNames() {
		Vector<String> childrenNames = new Vector<String>();
		childrenNames.add("ident");
		childrenNames.add("type");
		childrenNames.add("right");
		childrenNames.add("eval");
		childrenNames.add("delete");
		return childrenNames;
	}

	private static final CollectTripleResolver<NodeDeclNode, EdgeDeclNode, SubpatternUsageNode> deleteResolver =
		new CollectTripleResolver<NodeDeclNode, EdgeDeclNode, SubpatternUsageNode>(
			new DeclarationTripleResolver<NodeDeclNode, EdgeDeclNode, SubpatternUsageNode>(
				NodeDeclNode.class, EdgeDeclNode.class, SubpatternUsageNode.class));

	/** @see de.unika.ipd.grgen.ast.BaseNode#resolveLocal() */
	@Override
	protected boolean resolveLocal() {
		Triple<CollectNode<NodeDeclNode>, CollectNode<EdgeDeclNode>, CollectNode<SubpatternUsageNode>> resolve =
			deleteResolver.resolve(deleteUnresolved);

		if (resolve != null) {
			if (resolve.first != null) {
    			for (NodeDeclNode node : resolve.first.getChildren()) {
                    delete.addChild(node);
                }
			}

        	if (resolve.second != null) {
            	for (EdgeDeclNode edge : resolve.second.getChildren()) {
                    delete.addChild(edge);
                }
			}

        	if (resolve.third != null) {
        		for (SubpatternUsageNode sub : resolve.third.getChildren()) {
       				delete.addChild(sub);
                }
    		}

        	becomeParent(delete);
        }

		return super.resolveLocal() && resolve != null;
	}

	@Override
	protected PatternGraph getPatternGraph(PatternGraph left)
	{
	    PatternGraph right = graph.getGraph();

		Collection<Entity> deleteSet = new HashSet<Entity>();
		for(BaseNode n : delete.getChildren()) {
			if(!(n instanceof SubpatternUsageNode))
				deleteSet.add(n.checkIR(Entity.class));
		}

		for(Node n : left.getNodes()) {
			if(!deleteSet.contains(n)) {
				right.addSingleNode(n);
			}
		}
		for(Edge e : left.getEdges()) {
			if(        !deleteSet.contains(e)
			   		&& !deleteSet.contains(left.getSource(e))
			   		&& !deleteSet.contains(left.getTarget(e))) {
				right.addConnection(left.getSource(e), e, left.getTarget(e), e.hasFixedDirection());
			}
		}

		for(SubpatternUsage sub : left.getSubpatternUsages()) {
			boolean subHasDepModify = false;
			for(SubpatternDependentReplacement subRepl: right.getSubpatternDependentReplacements()) {
				if(sub==subRepl.getSubpatternUsage()) {
					subHasDepModify = true;
					break;
				}
			}
			boolean subInDeleteSet = false;
			for(BaseNode n : delete.getChildren()) {
				if(n instanceof SubpatternUsageNode) {
					SubpatternUsage su = n.checkIR(SubpatternUsage.class);
					if(sub==su) {
						subInDeleteSet = true;
					}
				}
			}

			if(!subHasDepModify && !subInDeleteSet) {
				right.addSubpatternUsage(sub);
			}
		}
		
		insertElementsFromEvalIntoRhs(left, right);

	    return right;
	}

	@Override
	protected Set<DeclNode> getDelete(PatternGraphNode pattern) {
		assert isResolved();

		if(deletedElements != null) return deletedElements;

		LinkedHashSet<DeclNode> coll = new LinkedHashSet<DeclNode>();

		for (DeclNode x : delete.getChildren()) {
			if(!(x instanceof SubpatternDeclNode))
				coll.add(x);
		}

		// add edges with deleted source or target
		for (BaseNode n : pattern.getConnections()) {
			if (n instanceof ConnectionNode) {
				ConnectionNode conn = (ConnectionNode) n;
				if (coll.contains(conn.getSrc()) || coll.contains(conn.getTgt()))
					coll.add(conn.getEdge());
			}
		}
		for (BaseNode n : graph.getConnections()) {
			if (n instanceof ConnectionNode) {
				ConnectionNode conn = (ConnectionNode) n;
				if (coll.contains(conn.getSrc()) || coll.contains(conn.getTgt()))
					coll.add(conn.getEdge());
			}
		}

		deletedElements = Collections.unmodifiableSet(coll);

		return deletedElements;
	}

	/**
	 * Return all reused edges (with their nodes), that excludes new edges of
	 * the right-hand side.
	 */
	@Override
	protected Collection<ConnectionNode> getReusedConnections(PatternGraphNode pattern) {
		Collection<ConnectionNode> res = new LinkedHashSet<ConnectionNode>();
		Collection<EdgeDeclNode> lhs = pattern.getEdges();

		for (BaseNode node : graph.getConnections()) {
			if (node instanceof ConnectionNode) {
				ConnectionNode conn = (ConnectionNode) node;
				EdgeDeclNode edge = conn.getEdge();
				while (edge instanceof EdgeTypeChangeNode) {
					edge = ((EdgeTypeChangeNode) edge).getOldEdge();
				}

				// add connection only if source and target are reused
				if (lhs.contains(edge) && !sourceOrTargetNodeIncluded(pattern, delete.getChildren(), edge)) {
					res.add(conn);
				}
			}
        }

		for (BaseNode node : pattern.getConnections()) {
			if (node instanceof ConnectionNode) {
				ConnectionNode conn = (ConnectionNode) node;
				EdgeDeclNode edge = conn.getEdge();
				while (edge instanceof EdgeTypeChangeNode) {
					edge = ((EdgeTypeChangeNode) edge).getOldEdge();
				}

				// add connection only if source and target are reused
				if (!delete.getChildren().contains(edge) && !sourceOrTargetNodeIncluded(pattern, delete.getChildren(), edge)) {
					res.add(conn);
				}
			}
        }

		return res;
	}

	/**
	 * Return all reused nodes, that excludes new nodes of the right-hand side.
	 */
	@Override
	protected Set<BaseNode> getReusedNodes(PatternGraphNode pattern) {
		if(reusedNodes != null) return reusedNodes;

		LinkedHashSet<BaseNode> coll = new LinkedHashSet<BaseNode>();
		Set<NodeDeclNode> patternNodes = pattern.getNodes();
		Set<NodeDeclNode> rhsNodes = graph.getNodes();

		for (BaseNode node : patternNodes) {
			if(rhsNodes.contains(node) || !delete.getChildren().contains(node))
				coll.add(node);
		}

		reusedNodes = Collections.unmodifiableSet(coll);
		return reusedNodes;
	}

	@Override
	protected void warnElemAppearsInsideAndOutsideDelete(PatternGraphNode pattern) {
		Set<DeclNode> deletes = getDelete(pattern);

		Set<BaseNode> alreadyReported = new HashSet<BaseNode>();
		for (BaseNode x : graph.getConnections()) {
			BaseNode elem = BaseNode.getErrorNode();
			if (x instanceof SingleNodeConnNode) {
				elem = ((SingleNodeConnNode)x).getNode();
			} else if (x instanceof ConnectionNode) {
				elem = ((ConnectionNode)x).getEdge();
			}

			if (alreadyReported.contains(elem)) {
				continue;
			}

			for (BaseNode y : deletes) {
				if (elem.equals(y)) {
					x.reportWarning("\"" + y + "\" appears inside as well as outside a delete statement");
					alreadyReported.add(elem);
				}
			}
		}
	}

	@Override
    protected Collection<ConnectionNode> getResultingConnections(PatternGraphNode pattern)
    {
	    Collection<ConnectionNode> res = new LinkedHashSet<ConnectionNode>();

	    Collection<DeclNode> delete = getDelete(pattern);

	    for (BaseNode n : pattern.getConnections()) {
	        if (n instanceof ConnectionNode) {
	        	ConnectionNode conn = (ConnectionNode) n;
	        	if (!delete.contains(conn.getEdge())
	        			&& !delete.contains(conn.getSrc())
	        			&& !delete.contains(conn.getTgt())) {
	        		res.add(conn);
	        	}
	        }
        }

	    return res;
    }
}

