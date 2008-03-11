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
import java.util.HashSet;
import java.util.LinkedHashSet;
import java.util.Set;
import java.util.Vector;

import de.unika.ipd.grgen.ast.util.CollectPairResolver;
import de.unika.ipd.grgen.ast.util.DeclarationPairResolver;
import de.unika.ipd.grgen.ir.Edge;
import de.unika.ipd.grgen.ir.Entity;
import de.unika.ipd.grgen.ir.IR;
import de.unika.ipd.grgen.ir.Node;
import de.unika.ipd.grgen.ir.PatternGraph;


/**
 * AST node for a replacement right-hand side.
 */
public class ModifyRhsDeclNode extends RhsDeclNode {
	static {
		setName(ModifyRhsDeclNode.class, "right-hand side declaration");
	}

	CollectNode<IdentNode> deleteUnresolved;
	CollectNode<ConstraintDeclNode> delete;

	/**
	 * Make a new right-hand side.
	 * @param id The identifier of this RHS.
	 * @param graph The right hand side graph.
	 * @param eval The evaluations.
	 */
	public ModifyRhsDeclNode(IdentNode id, GraphNode graph, CollectNode<AssignNode> eval,
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

	private static final CollectPairResolver<ConstraintDeclNode> deleteResolver = new CollectPairResolver<ConstraintDeclNode>(
			new DeclarationPairResolver<NodeDeclNode, EdgeDeclNode>(NodeDeclNode.class, EdgeDeclNode.class));

	/** @see de.unika.ipd.grgen.ast.BaseNode#resolveLocal() */
	protected boolean resolveLocal() {
		delete = deleteResolver.resolve(deleteUnresolved);
		type = typeResolver.resolve(typeUnresolved, this);

		return delete != null && type != null;
	}

	/**
	 * @see de.unika.ipd.grgen.ast.BaseNode#checkLocal()
	 */
	protected boolean checkLocal() {
		return true;
	}

	/**
	 * @see de.unika.ipd.grgen.ast.BaseNode#constructIR()
	 */
	protected IR constructIR() {
		assert false;

		return null;
	}

	@Override
	protected PatternGraph getPatternGraph(PatternGraph left)
	{
	    PatternGraph right = graph.getGraph();

		Collection<Entity> deleteSet = new HashSet<Entity>();
		for(BaseNode n : delete.getChildren()) {
			deleteSet.add((Entity)n.checkIR(Entity.class));
		}

		for(Node n : left.getNodes()) {
			if(!deleteSet.contains(n)) {
				right.addSingleNode(n);
			}
		}
		for(Edge e : left.getEdges()) {
			if(!deleteSet.contains(e)
			   && !deleteSet.contains(left.getSource(e))
			   && !deleteSet.contains(left.getTarget(e))) {
				right.addConnection(left.getSource(e), e, left.getTarget(e));
			}
		}

	    return right;
	}

	@Override
	protected Set<DeclNode> getDelete(PatternGraphNode pattern) {
		assert isResolved();

		Set<DeclNode> res = new LinkedHashSet<DeclNode>();

		for (ConstraintDeclNode x : delete.getChildren()) {
			res.add(x);
		}

		// add edges with deleted source or target
		for (BaseNode n : pattern.getConnections()) {
	        if (n instanceof ConnectionNode) {
	        	ConnectionNode conn = (ConnectionNode) n;
	        	if (res.contains(conn.getSrc()) || res.contains(conn.getTgt())) {
	        		res.add(conn.getEdge());
	        	}
	        }
        }
		for (BaseNode n : graph.getConnections()) {
	        if (n instanceof ConnectionNode) {
	        	ConnectionNode conn = (ConnectionNode) n;
	        	if (res.contains(conn.getSrc()) || res.contains(conn.getTgt())) {
	        		res.add(conn.getEdge());
	        	}
	        }
        }

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

				// add connection only if source and target are reused
				if (lhs.contains(edge) && !sourceOrTargetNodeDeleted(pattern, edge)) {
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
				if (!delete.getChildren().contains(edge) && !sourceOrTargetNodeDeleted(pattern, edge)) {
					res.add(conn);
				}
			}
        }

		return res;
	}

	private boolean sourceOrTargetNodeDeleted(PatternGraphNode pattern, EdgeDeclNode edgeDecl) {
		for (BaseNode n : pattern.getConnections()) {
	        if (n instanceof ConnectionNode) {
	        	ConnectionNode conn = (ConnectionNode) n;
	        	if (conn.getEdge().equals(edgeDecl)) {
	        		if (delete.getChildren().contains(conn.getSrc())
	        				|| delete.getChildren().contains(conn.getTgt())) {
	        			return true;
	        		}
	        	}
	        }
        }
		return false;
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
			if (!delete.getChildren().contains(node)) {
				res.add(node);
			}
		}

		return res;
	}
}

