/*
 * GrGen: graph rewrite generator tool -- release GrGen.NET 5.0
 * Copyright (C) 2003-2020 Universitaet Karlsruhe, Institut fuer Programmstrukturen und Datenorganisation, LS Goos; and free programmers
 * licensed under LGPL v3 (see LICENSE.txt included in the packaging of this file)
 * www.grgen.net
 */

/**
 * @author Sebastian Buchwald, Edgar Jakumeit
 */

package de.unika.ipd.grgen.ast.decl.pattern;

import de.unika.ipd.grgen.ir.pattern.Edge;
import de.unika.ipd.grgen.ir.pattern.Node;
import de.unika.ipd.grgen.ir.pattern.PatternGraph;
import de.unika.ipd.grgen.ast.BaseNode;
import de.unika.ipd.grgen.ast.IdentNode;
import de.unika.ipd.grgen.ast.pattern.ConnectionCharacter;
import de.unika.ipd.grgen.ast.pattern.ConnectionNode;
import de.unika.ipd.grgen.ast.pattern.GraphNode;
import de.unika.ipd.grgen.ast.pattern.PatternGraphNode;

import java.util.Collection;
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
	public Set<ConstraintDeclNode> getDeletedImpl(PatternGraphNode pattern)
	{
		LinkedHashSet<ConstraintDeclNode> deletedElements = new LinkedHashSet<ConstraintDeclNode>();

		Set<EdgeDeclNode> rhsEdges = new LinkedHashSet<EdgeDeclNode>();
		Set<NodeDeclNode> rhsNodes = new LinkedHashSet<NodeDeclNode>();

		for(EdgeDeclNode decl : graph.getEdges()) {
			while(decl instanceof EdgeTypeChangeDeclNode) {
				decl = ((EdgeTypeChangeDeclNode)decl).getOldEdge();
			}
			rhsEdges.add(decl);
		}
		for(EdgeDeclNode edge : pattern.getEdges()) {
			if(!rhsEdges.contains(edge)) {
				deletedElements.add(edge);
			}
		}

		for(NodeDeclNode decl : graph.getNodes()) {
			while(decl instanceof NodeTypeChangeDeclNode) {
				decl = ((NodeTypeChangeDeclNode)decl).getOldNode();
			}
			rhsNodes.add(decl);
		}
		for(NodeDeclNode node : pattern.getNodes()) {
			if(!rhsNodes.contains(node) && !node.isDummy()) {
				deletedElements.add(node);
			}
		}
		// parameters are no special case, since they are treat like normal
		// graph elements

		return deletedElements;
	}

	@Override
	public Set<ConnectionNode> getReusedConnectionsImpl(PatternGraphNode pattern)
	{
		Set<ConnectionNode> reusedConnections = new LinkedHashSet<ConnectionNode>();

		Set<EdgeDeclNode> lhs = pattern.getEdges();
		for(ConnectionCharacter connectionCharacter : graph.getConnections()) {
			if(connectionCharacter instanceof ConnectionNode) {
				ConnectionNode connection = (ConnectionNode)connectionCharacter;
				EdgeDeclNode edge = connection.getEdge();
				while(edge instanceof EdgeTypeChangeDeclNode) {
					edge = ((EdgeTypeChangeDeclNode)edge).getOldEdge();
				}
				if(lhs.contains(edge)) {
					reusedConnections.add(connection);
				}
			}
		}

		return reusedConnections;
	}

	@Override
	public Set<NodeDeclNode> getReusedNodesImpl(PatternGraphNode pattern)
	{
		LinkedHashSet<NodeDeclNode> reusedNodes = new LinkedHashSet<NodeDeclNode>();
		
		Set<NodeDeclNode> patternNodes = pattern.getNodes();
		Set<NodeDeclNode> rhsNodes = graph.getNodes();
		for(NodeDeclNode node : patternNodes) {
			if(rhsNodes.contains(node))
				reusedNodes.add(node);
		}

		return reusedNodes;
	}

	@Override
	public void warnElemAppearsInsideAndOutsideDelete(PatternGraphNode pattern)
	{
		// nothing to do
	}

	@Override
	protected Set<ConnectionNode> getResultingConnections(PatternGraphNode pattern)
	{
		Set<ConnectionNode> res = new LinkedHashSet<ConnectionNode>();

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
