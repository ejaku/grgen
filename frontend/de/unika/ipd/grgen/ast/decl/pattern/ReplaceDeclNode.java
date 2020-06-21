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
import de.unika.ipd.grgen.ir.pattern.PatternGraphBase;
import de.unika.ipd.grgen.ir.pattern.PatternGraphLhs;
import de.unika.ipd.grgen.ir.pattern.PatternGraphRhs;
import de.unika.ipd.grgen.ast.BaseNode;
import de.unika.ipd.grgen.ast.IdentNode;
import de.unika.ipd.grgen.ast.pattern.ConnectionCharacter;
import de.unika.ipd.grgen.ast.pattern.ConnectionNode;
import de.unika.ipd.grgen.ast.pattern.PatternGraphRhsNode;
import de.unika.ipd.grgen.ast.pattern.PatternGraphLhsNode;

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
	 * @param patternGraph The right hand side graph.
	 */
	public ReplaceDeclNode(IdentNode id, PatternGraphRhsNode patternGraph)
	{
		super(id, patternGraph);
	}

	/** returns children of this node */
	@Override
	public Collection<BaseNode> getChildren()
	{
		Vector<BaseNode> children = new Vector<BaseNode>();
		children.add(ident);
		children.add(getValidVersion(typeUnresolved, type));
		children.add(patternGraph);
		return children;
	}

	/** returns names of the children, same order as in getChildren */
	@Override
	public Collection<String> getChildrenNames()
	{
		Vector<String> childrenNames = new Vector<String>();
		childrenNames.add("ident");
		childrenNames.add("type");
		childrenNames.add("right");
		return childrenNames;
	}

	@Override
	public PatternGraphRhs getPatternGraph(PatternGraphLhs left)
	{
		PatternGraphRhs right = patternGraph.getGraph();
		insertElementsFromEvalsIntoRhs(left, right);
		insertElementsFromOrderedReplacementsIntoRhs(left, right);
		insertElementsFromLeftToRightIfTheyAreFromNestingPattern(left, right);
		return right;
	}

	@Override
	public boolean checkAgainstLhsPattern(PatternGraphLhsNode pattern)
	{
		return true; // nothing to do as of now
	}

	@Override
	public Set<ConstraintDeclNode> getElementsToDeleteImpl(PatternGraphLhsNode pattern)
	{
		LinkedHashSet<ConstraintDeclNode> elementsToDelete = new LinkedHashSet<ConstraintDeclNode>();

		Set<EdgeDeclNode> rhsEdges = new LinkedHashSet<EdgeDeclNode>();
		Set<NodeDeclNode> rhsNodes = new LinkedHashSet<NodeDeclNode>();

		for(EdgeDeclNode rhsEdge : patternGraph.getEdges()) {
			while(rhsEdge instanceof EdgeTypeChangeDeclNode) {
				rhsEdge = ((EdgeTypeChangeDeclNode)rhsEdge).getOldEdge();
			}
			rhsEdges.add(rhsEdge);
		}
		for(EdgeDeclNode lhsEdge : pattern.getEdges()) {
			if(!rhsEdges.contains(lhsEdge)) {
				elementsToDelete.add(lhsEdge);
			}
		}

		for(NodeDeclNode rhsNode : patternGraph.getNodes()) {
			while(rhsNode instanceof NodeTypeChangeDeclNode) {
				rhsNode = ((NodeTypeChangeDeclNode)rhsNode).getOldNode();
			}
			rhsNodes.add(rhsNode);
		}
		for(NodeDeclNode lhsNode : pattern.getNodes()) {
			if(!rhsNodes.contains(lhsNode) && !lhsNode.isDummy()) {
				elementsToDelete.add(lhsNode);
			}
		}
		
		// parameters are no special case, since they are treated like normal graph elements
		return elementsToDelete;
	}

	@Override
	public Set<ConnectionNode> getConnectionsToReuseImpl(PatternGraphLhsNode pattern)
	{
		Set<ConnectionNode> connectionsToReuse = new LinkedHashSet<ConnectionNode>();

		Set<EdgeDeclNode> lhsEdges = pattern.getEdges();
		for(ConnectionCharacter connectionCharacter : patternGraph.getConnections()) {
			if(connectionCharacter instanceof ConnectionNode) {
				ConnectionNode connection = (ConnectionNode)connectionCharacter;
				EdgeDeclNode rhsEdge = connection.getEdge();
				while(rhsEdge instanceof EdgeTypeChangeDeclNode) {
					rhsEdge = ((EdgeTypeChangeDeclNode)rhsEdge).getOldEdge();
				}
				if(lhsEdges.contains(rhsEdge)) {
					connectionsToReuse.add(connection);
				}
			}
		}

		return connectionsToReuse;
	}

	@Override
	public Set<NodeDeclNode> getNodesToReuseImpl(PatternGraphLhsNode pattern)
	{
		LinkedHashSet<NodeDeclNode> nodesToReuse = new LinkedHashSet<NodeDeclNode>();
		
		Set<NodeDeclNode> lhsNodes = pattern.getNodes();
		Set<NodeDeclNode> rhsNodes = patternGraph.getNodes();
		for(NodeDeclNode lhsNode : lhsNodes) {
			if(rhsNodes.contains(lhsNode))
				nodesToReuse.add(lhsNode);
		}

		return nodesToReuse;
	}

	@Override
	protected Set<ConnectionNode> getConnectionsNotDeleted(PatternGraphLhsNode pattern)
	{
		Set<ConnectionNode> connectionsNotDeleted = new LinkedHashSet<ConnectionNode>();

		for(ConnectionCharacter connectionCharacter : patternGraph.getConnections()) {
			if(connectionCharacter instanceof ConnectionNode) {
				ConnectionNode connection = (ConnectionNode)connectionCharacter;
				connectionsNotDeleted.add(connection);
			}
		}

		return connectionsNotDeleted;
	}

	private static void insertElementsFromLeftToRightIfTheyAreFromNestingPattern(PatternGraphLhs left, PatternGraphBase right)
	{
		for(Node lhsNode : left.getNodes()) {
			if(lhsNode.directlyNestingLHSGraph != left && !right.hasNode(lhsNode)) {
				right.addSingleNode(lhsNode);
			}
		}
		for(Edge lhsEdge : left.getEdges()) {
			if(lhsEdge.directlyNestingLHSGraph != left && !right.hasEdge(lhsEdge)) {
				right.addSingleEdge(lhsEdge);
			}
		}
	}
}
