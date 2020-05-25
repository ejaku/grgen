/*
 * GrGen: graph rewrite generator tool -- release GrGen.NET 5.0
 * Copyright (C) 2003-2020 Universitaet Karlsruhe, Institut fuer Programmstrukturen und Datenorganisation, LS Goos; and free programmers
 * licensed under LGPL v3 (see LICENSE.txt included in the packaging of this file)
 * www.grgen.net
 */

/**
 * @author Sebastian Buchwald
 */

package de.unika.ipd.grgen.ast.decl.pattern;

import de.unika.ipd.grgen.ir.pattern.Edge;
import de.unika.ipd.grgen.ir.pattern.Node;
import de.unika.ipd.grgen.ir.pattern.PatternGraph;
import de.unika.ipd.grgen.ast.BaseNode;
import de.unika.ipd.grgen.ast.IdentNode;
import de.unika.ipd.grgen.ast.decl.DeclNode;
import de.unika.ipd.grgen.ast.pattern.ConnectionCharacter;
import de.unika.ipd.grgen.ast.pattern.ConnectionNode;
import de.unika.ipd.grgen.ast.pattern.GraphNode;
import de.unika.ipd.grgen.ast.pattern.PatternGraphNode;

import java.util.Collection;
import java.util.Collections;
import java.util.LinkedHashSet;
import java.util.Set;
import java.util.Vector;

/**
 * AST node for a replacement right-hand side.
 */
public class ReplaceDeclNode extends RhsDeclNode
{
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
	 */
	public ReplaceDeclNode(IdentNode id, GraphNode graph)
	{
		super(id, graph);
	}

	/** returns children of this node */
	public Collection<BaseNode> getChildren()
	{
		Vector<BaseNode> children = new Vector<BaseNode>();
		children.add(ident);
		children.add(getValidVersion(typeUnresolved, type));
		children.add(graph);
		return children;
	}

	/** returns names of the children, same order as in getChildren */
	public Collection<String> getChildrenNames()
	{
		Vector<String> childrenNames = new Vector<String>();
		childrenNames.add("ident");
		childrenNames.add("type");
		childrenNames.add("right");
		return childrenNames;
	}

	@Override
	public PatternGraph getPatternGraph(PatternGraph left)
	{
		PatternGraph right = graph.getGraph();
		insertElementsFromEvalIntoRhs(left, right);
		insertElementsFromOrderedReplacementsIntoRhs(left, right);
		insertElementsFromLeftToRightIfTheyAreFromNestingPattern(left, right);
		return right;
	}

	@Override
	public Set<DeclNode> getDeleted(PatternGraphNode pattern)
	{
		if(deletedElements != null)
			return deletedElements;

		LinkedHashSet<DeclNode> coll = new LinkedHashSet<DeclNode>();

		Set<EdgeDeclNode> rhsEdges = new LinkedHashSet<EdgeDeclNode>();
		Set<NodeDeclNode> rhsNodes = new LinkedHashSet<NodeDeclNode>();

		for(EdgeDeclNode decl : graph.getEdges()) {
			while(decl instanceof EdgeTypeChangeNode) {
				decl = ((EdgeTypeChangeNode)decl).getOldEdge();
			}
			rhsEdges.add(decl);
		}
		for(EdgeDeclNode edge : pattern.getEdges()) {
			if(!rhsEdges.contains(edge)) {
				coll.add(edge);
			}
		}

		for(NodeDeclNode decl : graph.getNodes()) {
			while(decl instanceof NodeTypeChangeNode) {
				decl = ((NodeTypeChangeNode)decl).getOldNode();
			}
			rhsNodes.add(decl);
		}
		for(NodeDeclNode node : pattern.getNodes()) {
			if(!rhsNodes.contains(node) && !node.isDummy()) {
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
	@Override
	public Collection<ConnectionNode> getReusedConnections(PatternGraphNode pattern)
	{
		Collection<ConnectionNode> res = new LinkedHashSet<ConnectionNode>();
		Collection<EdgeDeclNode> lhs = pattern.getEdges();

		for(ConnectionCharacter connectionCharacter : graph.getConnections()) {
			if(connectionCharacter instanceof ConnectionNode) {
				ConnectionNode connection = (ConnectionNode)connectionCharacter;
				EdgeDeclNode edge = connection.getEdge();
				while(edge instanceof EdgeTypeChangeNode) {
					edge = ((EdgeTypeChangeNode)edge).getOldEdge();
				}
				if(lhs.contains(edge)) {
					res.add(connection);
				}
			}
		}

		return res;
	}

	/**
	 * Return all reused nodes, that excludes new nodes of the right-hand side.
	 */
	@Override
	public Set<BaseNode> getReusedNodes(PatternGraphNode pattern)
	{
		if(reusedNodes != null)
			return reusedNodes;

		LinkedHashSet<BaseNode> coll = new LinkedHashSet<BaseNode>();
		Set<NodeDeclNode> patternNodes = pattern.getNodes();
		Set<NodeDeclNode> rhsNodes = graph.getNodes();

		for(NodeDeclNode node : patternNodes) {
			if(rhsNodes.contains(node))
				coll.add(node);
		}

		reusedNodes = Collections.unmodifiableSet(coll);
		return reusedNodes;
	}

	@Override
	public void warnElemAppearsInsideAndOutsideDelete(PatternGraphNode pattern)
	{
		// nothing to do
	}

	@Override
	protected Collection<ConnectionNode> getResultingConnections(PatternGraphNode pattern)
	{
		Collection<ConnectionNode> res = new LinkedHashSet<ConnectionNode>();

		for(ConnectionCharacter connectionCharacter : graph.getConnections()) {
			if(connectionCharacter instanceof ConnectionNode) {
				ConnectionNode connection = (ConnectionNode)connectionCharacter;
				res.add(connection);
			}
		}

		return res;
	}

	private void insertElementsFromLeftToRightIfTheyAreFromNestingPattern(PatternGraph left, PatternGraph right)
	{
		for(Node node : left.getNodes()) {
			if(node.directlyNestingLHSGraph != left && !right.hasNode(node)) {
				right.addSingleNode(node);
			}
		}
		for(Edge edge : left.getEdges()) {
			if(edge.directlyNestingLHSGraph != left && !right.hasEdge(edge)) {
				right.addSingleEdge(edge);
			}
		}
	}
}