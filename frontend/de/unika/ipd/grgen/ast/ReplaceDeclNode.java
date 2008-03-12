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
 * @author Sebastian Buchwald
 * @version $Id: RhsDeclNode.java 18021 2008-03-09 12:13:04Z buchwald $
 */
package de.unika.ipd.grgen.ast;


import java.util.Collection;
import java.util.LinkedHashSet;
import java.util.Set;
import java.util.Vector;

import de.unika.ipd.grgen.ir.PatternGraph;


/**
 * AST node for a replacement right-hand side.
 */
public class ReplaceDeclNode extends RhsDeclNode {
	static {
		setName(ReplaceDeclNode.class, "replace declaration");
	}

	/**
	 * Make a new replace right-hand side.
	 * @param id The identifier of this RHS.
	 * @param graph The right hand side graph.
	 * @param eval The evaluations.
	 */
	public ReplaceDeclNode(IdentNode id, GraphNode graph, CollectNode<AssignNode> eval) {
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
		Set<DeclNode> res = new LinkedHashSet<DeclNode>();

		Set<EdgeDeclNode> rhsEdges = new LinkedHashSet<EdgeDeclNode>();
		Set<NodeDeclNode> rhsNodes = new LinkedHashSet<NodeDeclNode>();

		for (BaseNode x : graph.getEdges()) {
			EdgeDeclNode decl = (EdgeDeclNode) x;

			while (decl instanceof EdgeTypeChangeNode) {
				decl = ((EdgeTypeChangeNode) decl).getOldEdge();
			}
			rhsEdges.add(decl);
		}
		for (BaseNode x : pattern.getEdges()) {
			assert (x instanceof DeclNode);
			if ( ! rhsEdges.contains(x) ) {
				res.add((DeclNode)x);
			}
		}

		for (BaseNode x : graph.getNodes()) {
			NodeDeclNode decl = (NodeDeclNode) x;

			while (decl instanceof NodeTypeChangeNode) {
				decl = ((NodeTypeChangeNode) decl).getOldNode();
			}
			rhsNodes.add(decl);
		}
		for (BaseNode x : pattern.getNodes()) {
			assert (x instanceof DeclNode);
			if ( ! rhsNodes.contains(x) ) {
				res.add((DeclNode)x);
			}
		}
		// parameters are no special case, since they are treat like normal
		// graph elements

		return res;
	}

	/**
	 * Return all reused edges (with their nodes), that excludes new edges of
	 * the right-hand side.
	 */
	protected Collection<ConnectionNode> getReusedConnections(PatternGraphNode pattern) {
		Collection<ConnectionNode> res = new LinkedHashSet<ConnectionNode>();
		Collection<BaseNode> lhs = pattern.getEdges();

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
		Set<BaseNode> res = new LinkedHashSet<BaseNode>();
		Set<BaseNode> patternNodes = pattern.getNodes();
		Set<BaseNode> rhsNodes = graph.getNodes();

		for (BaseNode node : patternNodes) {
			if ( rhsNodes.contains(node) ) {
				res.add(node);
			}
		}

		return res;
	}

	protected void warnElemAppearsInsideAndOutsideDelete(PatternGraphNode pattern) {
		// nothing to do
	}
}

