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


import de.unika.ipd.grgen.ir.PatternGraph;
import java.util.Collection;
import java.util.Collections;
import java.util.LinkedHashSet;
import java.util.Set;
import java.util.Vector;


/**
 * AST node for a replacement right-hand side.
 */
public class ReplaceDeclNode extends RhsDeclNode {
	static {
		setName(ReplaceDeclNode.class, "replace declaration");
	}

	// Cache variables
	private Set<DeclNode> deletedElements;
	private Set<BaseNode> reusedNodes;

	/**
	 * Make a new replace right-hand side.
	 * @param id The identifier of this RHS.
	 * @param graph The right hand side graph.
	 * @param eval The evaluations.
	 */
	public ReplaceDeclNode(IdentNode id, GraphNode graph, CollectNode<EvalStatementNode> eval) {
		super(id, graph, eval);
	}

	/** returns children of this node */
	public Collection<BaseNode> getChildren() {
		Vector<BaseNode> children = new Vector<BaseNode>();
		children.add(ident);
		children.add(getValidVersion(typeUnresolved, type));
		children.add(graph);
		children.add(eval);
		return children;
	}

	/** returns names of the children, same order as in getChildren */
	public Collection<String> getChildrenNames() {
		Vector<String> childrenNames = new Vector<String>();
		childrenNames.add("ident");
		childrenNames.add("type");
		childrenNames.add("right");
		childrenNames.add("eval");
		return childrenNames;
	}

	protected PatternGraph getPatternGraph(PatternGraph left) {
		return graph.getGraph();
	}

	protected Set<DeclNode> getDelete(PatternGraphNode pattern) {
		if(deletedElements != null) return deletedElements;

		LinkedHashSet<DeclNode> coll = new LinkedHashSet<DeclNode>();

		Set<EdgeDeclNode> rhsEdges = new LinkedHashSet<EdgeDeclNode>();
		Set<NodeDeclNode> rhsNodes = new LinkedHashSet<NodeDeclNode>();

		for (EdgeDeclNode decl : graph.getEdges()) {
			while (decl instanceof EdgeTypeChangeNode) {
				decl = ((EdgeTypeChangeNode) decl).getOldEdge();
			}
			rhsEdges.add(decl);
		}
		for (EdgeDeclNode edge : pattern.getEdges()) {
			if (!rhsEdges.contains(edge)) {
				coll.add(edge);
			}
		}

		for (NodeDeclNode decl : graph.getNodes()) {
			while (decl instanceof NodeTypeChangeNode) {
				decl = ((NodeTypeChangeNode) decl).getOldNode();
			}
			rhsNodes.add(decl);
		}
		for (NodeDeclNode node : pattern.getNodes()) {
			if (!rhsNodes.contains(node) && !node.isDummy()) {
				coll.add(node);
			}
		}
		// parameters are no special case, since they are treat like normal
		// graph elements

		deletedElements = Collections.unmodifiableSet(coll);
		return deletedElements;
	}

	/**
	 * Return all reused edges (with their nodes), that excludes new edges of
	 * the right-hand side.
	 */
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
				if (lhs.contains(edge)) {
					res.add(conn);
				}
			}
        }

		return res;
	}

	/**
	 * Return all reused nodes, that excludes new nodes of the right-hand side.
	 */
	protected Set<BaseNode> getReusedNodes(PatternGraphNode pattern) {
		if(reusedNodes != null) return reusedNodes;

		LinkedHashSet<BaseNode> coll = new LinkedHashSet<BaseNode>();
		Set<NodeDeclNode> patternNodes = pattern.getNodes();
		Set<NodeDeclNode> rhsNodes = graph.getNodes();

		for (BaseNode node : patternNodes) {
			if ( rhsNodes.contains(node) )
				coll.add(node);
		}

		reusedNodes = Collections.unmodifiableSet(coll);
		return reusedNodes;
	}

	protected void warnElemAppearsInsideAndOutsideDelete(PatternGraphNode pattern) {
		// nothing to do
	}

	@Override
    protected Collection<ConnectionNode> getResultingConnections(PatternGraphNode pattern)
    {
	    Collection<ConnectionNode> res = new LinkedHashSet<ConnectionNode>();

		for (BaseNode conn : graph.getConnections()) {
	        if (conn instanceof ConnectionNode) {
	        	res.add((ConnectionNode) conn);
	        }
        }
	    return res;
    }
}

