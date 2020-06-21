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

import de.unika.ipd.grgen.ast.BaseNode;
import de.unika.ipd.grgen.ast.CollectNode;
import de.unika.ipd.grgen.ast.IdentNode;
import de.unika.ipd.grgen.ast.decl.DeclNode;
import de.unika.ipd.grgen.ast.pattern.ConnectionCharacter;
import de.unika.ipd.grgen.ast.pattern.ConnectionNode;
import de.unika.ipd.grgen.ast.pattern.PatternGraphRhsNode;
import de.unika.ipd.grgen.ast.pattern.PatternGraphLhsNode;
import de.unika.ipd.grgen.ast.pattern.SingleNodeConnNode;
import de.unika.ipd.grgen.ast.util.CollectTripleResolver;
import de.unika.ipd.grgen.ast.util.DeclarationTripleResolver;
import de.unika.ipd.grgen.ast.util.Triple;
import de.unika.ipd.grgen.ir.Entity;
import de.unika.ipd.grgen.ir.pattern.Edge;
import de.unika.ipd.grgen.ir.pattern.Node;
import de.unika.ipd.grgen.ir.pattern.OrderedReplacement;
import de.unika.ipd.grgen.ir.pattern.OrderedReplacements;
import de.unika.ipd.grgen.ir.pattern.PatternGraphBase;
import de.unika.ipd.grgen.ir.pattern.PatternGraphLhs;
import de.unika.ipd.grgen.ir.pattern.PatternGraphRhs;
import de.unika.ipd.grgen.ir.pattern.SubpatternDependentReplacement;
import de.unika.ipd.grgen.ir.pattern.SubpatternUsage;

import java.util.Collection;
import java.util.HashSet;
import java.util.LinkedHashSet;
import java.util.Set;
import java.util.Vector;

/**
 * AST node for a modify right-hand side.
 */
public class ModifyDeclNode extends RhsDeclNode
{
	static {
		setName(ModifyDeclNode.class, "modify declaration");
	}

	private CollectNode<IdentNode> deletesUnresolved;
	private CollectNode<DeclNode> deletes = new CollectNode<DeclNode>();


	/**
	 * Make a new modify right-hand side.
	 * @param id The identifier of this RHS.
	 * @param patternGraph The right hand side graph.
	 */
	public ModifyDeclNode(IdentNode id, PatternGraphRhsNode patternGraph, CollectNode<IdentNode> deletes)
	{
		super(id, patternGraph);
		this.deletesUnresolved = deletes;
		becomeParent(this.deletesUnresolved);
	}

	/** returns children of this node */
	@Override
	public Collection<BaseNode> getChildren()
	{
		Vector<BaseNode> children = new Vector<BaseNode>();
		children.add(ident);
		children.add(getValidVersion(typeUnresolved, type));
		children.add(patternGraph);
		children.add(getValidVersion(deletesUnresolved, deletes));
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
		childrenNames.add("delete");
		return childrenNames;
	}

	private static final CollectTripleResolver<NodeDeclNode, EdgeDeclNode, SubpatternUsageDeclNode> deleteResolver =
		new CollectTripleResolver<NodeDeclNode, EdgeDeclNode, SubpatternUsageDeclNode>(
			new DeclarationTripleResolver<NodeDeclNode, EdgeDeclNode, SubpatternUsageDeclNode>(
				NodeDeclNode.class, EdgeDeclNode.class, SubpatternUsageDeclNode.class));

	/** @see de.unika.ipd.grgen.ast.BaseNode#resolveLocal() */
	@Override
	protected boolean resolveLocal()
	{
		Triple<CollectNode<NodeDeclNode>, CollectNode<EdgeDeclNode>, CollectNode<SubpatternUsageDeclNode>> resolve =
			deleteResolver.resolve(deletesUnresolved);

		if(resolve != null) {
			if(resolve.first != null) {
				for(NodeDeclNode node : resolve.first.getChildren()) {
					deletes.addChild(node);
				}
			}

			if(resolve.second != null) {
				for(EdgeDeclNode edge : resolve.second.getChildren()) {
					deletes.addChild(edge);
				}
			}

			if(resolve.third != null) {
				for(SubpatternUsageDeclNode sub : resolve.third.getChildren()) {
					deletes.addChild(sub);
				}
			}

			becomeParent(deletes);
		}

		return super.resolveLocal() && resolve != null;
	}

	@Override
	public boolean checkAgainstLhsPattern(PatternGraphLhsNode pattern)
	{
		warnIfElementAppearsInsideAndOutsideOfDelete(pattern);
		return true;
	}

	@Override
	public PatternGraphRhs getPatternGraph(PatternGraphLhs left)
	{
		PatternGraphRhs right = patternGraph.getGraph();

		Set<Entity> elementsToDelete = insertElementsToDeleteToLhsIfNotFromLhs(left, right);

		insertLhsElementsToRhs(left, elementsToDelete, right);

		insertElementsFromTypeofToRhsIfNotYetContained(right, elementsToDelete);

		for(SubpatternUsage sub : left.getSubpatternUsages()) {
			if(!isSubpatternRewritePartUsed(sub, right) && !isSubpatternUsageToBeDeleted(sub)) {
				right.addSubpatternUsage(sub); // keep subpattern
			}
		}

		insertElementsFromEvalsIntoRhs(left, right);
		insertElementsFromOrderedReplacementsIntoRhs(left, right);

		return right;
	}

	private Set<Entity> insertElementsToDeleteToLhsIfNotFromLhs(PatternGraphLhs left, PatternGraphBase right)
	{
		HashSet<Entity> elementsToDelete = new HashSet<Entity>();
		
		for(DeclNode delete : deletes.getChildren()) {
			if(delete instanceof SubpatternUsageDeclNode)
				continue;

			ConstraintDeclNode element = (ConstraintDeclNode)delete;
			Entity entity = element.checkIR(Entity.class);
			elementsToDelete.add(entity);
			
			if(element.defEntityToBeYieldedTo)
				entity.setPatternGraphDefYieldedIsToBeDeleted(right);
			
			if(entity instanceof Node) {
				Node node = element.checkIR(Node.class);
				if(!left.hasNode(node) && node.directlyNestingLHSGraph != left) {
					left.addSingleNode(node);
					left.addHomToAll(node);
				}
			} else {
				Edge edge = element.checkIR(Edge.class);
				if(!left.hasEdge(edge) && edge.directlyNestingLHSGraph != left) {
					left.addSingleEdge(edge);
					left.addHomToAll(edge);
				}
			}
		}
		
		return elementsToDelete;
	}

	// inserts to be kept nodes/edges and to be deleted nodes/edges, to be created nodes/edges are already contained
	private static void insertLhsElementsToRhs(PatternGraphLhs left, Set<Entity> elementsToDelete, PatternGraphRhs right)
	{
		for(Node lhsNode : left.getNodes()) {
			if(!elementsToDelete.contains(lhsNode)) {
				right.addSingleNode(lhsNode);
			} else {
				right.addDeletedElement(lhsNode);
			}
		}
		for(Edge lhsEdge : left.getEdges()) {
			if(!elementsToDelete.contains(lhsEdge)
					&& !elementsToDelete.contains(left.getSource(lhsEdge))
					&& !elementsToDelete.contains(left.getTarget(lhsEdge))) {
				right.addConnection(left.getSource(lhsEdge), lhsEdge, left.getTarget(lhsEdge),
						lhsEdge.hasFixedDirection(), false, false);
			} else {
				right.addDeletedElement(lhsEdge);
			}
		}
	}

	private static void insertElementsFromTypeofToRhsIfNotYetContained(PatternGraphRhs right, Set<Entity> elementsToDelete)
	{
		for(Node rhsNode : right.getNodes()) {
			if(rhsNode.inheritsType()) {
				Node nodeFromTypeof = (Node)rhsNode.getTypeof();
				if(!elementsToDelete.contains(nodeFromTypeof)) {
					right.addNodeIfNotYetContained(nodeFromTypeof);
				}
			}
		}
		for(Edge rhsEdge : right.getEdges()) {
			if(rhsEdge.inheritsType()) {
				Edge edgeFromTypeof = (Edge)rhsEdge.getTypeof();
				if(!elementsToDelete.contains(edgeFromTypeof)) {
					right.addEdgeIfNotYetContained(edgeFromTypeof);
				}
			}
		}
	}

	private static boolean isSubpatternRewritePartUsed(SubpatternUsage sub, PatternGraphRhs right)
	{
		for(OrderedReplacements orderedRepls : right.getOrderedReplacements()) {
			for(OrderedReplacement orderedRepl : orderedRepls.orderedReplacements) {
				if(!(orderedRepl instanceof SubpatternDependentReplacement))
					continue;
				
				SubpatternDependentReplacement subRepl = (SubpatternDependentReplacement)orderedRepl;
				if(sub == subRepl.getSubpatternUsage())
					return true;
			}
		}
		return false;
	}

	private boolean isSubpatternUsageToBeDeleted(SubpatternUsage subpatternUsage)
	{
		for(DeclNode delete : deletes.getChildren()) {
			if(!(delete instanceof SubpatternUsageDeclNode))
				continue;

			SubpatternUsage subpatternUsageToBeDeleted = delete.checkIR(SubpatternUsage.class);
			if(subpatternUsage == subpatternUsageToBeDeleted)
				return true;
		}
		return false;
	}

	@Override
	public Set<ConstraintDeclNode> getElementsToDeleteImpl(PatternGraphLhsNode pattern)
	{
		assert isResolved();

		LinkedHashSet<ConstraintDeclNode> elementsToDelete = new LinkedHashSet<ConstraintDeclNode>();

		for(DeclNode delete : deletes.getChildren()) {
			if(!(delete instanceof SubpatternUsageDeclNode))
				elementsToDelete.add((ConstraintDeclNode)delete);
		}

		// add edges with deleted source or target
		for(ConnectionCharacter connectionCharacter : pattern.getConnections()) {
			if(!(connectionCharacter instanceof ConnectionNode))
				continue;
			
			ConnectionNode connection = (ConnectionNode)connectionCharacter;
			if(elementsToDelete.contains(connection.getSrc()) || elementsToDelete.contains(connection.getTgt()))
				elementsToDelete.add(connection.getEdge());
		}
		for(ConnectionCharacter connectionCharacter : patternGraph.getConnections()) {
			if(!(connectionCharacter instanceof ConnectionNode))
				continue;
			
			ConnectionNode connection = (ConnectionNode)connectionCharacter;
			if(elementsToDelete.contains(connection.getSrc()) || elementsToDelete.contains(connection.getTgt()))
				elementsToDelete.add(connection.getEdge());
		}

		return elementsToDelete;
	}

	@Override
	public Set<ConnectionNode> getConnectionsToReuseImpl(PatternGraphLhsNode pattern)
	{
		Set<ConnectionNode> connectionsToReuse = new LinkedHashSet<ConnectionNode>();

		Set<EdgeDeclNode> lhsEdges = pattern.getEdges();
		for(ConnectionCharacter connectionCharacter : patternGraph.getConnections()) {
			if(!(connectionCharacter instanceof ConnectionNode))
				continue;

			ConnectionNode connection = (ConnectionNode)connectionCharacter;
			EdgeDeclNode rhsEdge = connection.getEdge();
			while(rhsEdge instanceof EdgeTypeChangeDeclNode) {
				rhsEdge = ((EdgeTypeChangeDeclNode)rhsEdge).getOldEdge();
			}

			// add connection only if source and target are reused
			if(lhsEdges.contains(rhsEdge) && !sourceOrTargetNodeIncluded(rhsEdge, pattern, deletes.getChildren())) {
				connectionsToReuse.add(connection);
			}
		}

		for(ConnectionCharacter connectionCharacter : pattern.getConnections()) {
			if(!(connectionCharacter instanceof ConnectionNode))
				continue;
			
			ConnectionNode connection = (ConnectionNode)connectionCharacter;
			EdgeDeclNode lhsEdge = connection.getEdge();
			while(lhsEdge instanceof EdgeTypeChangeDeclNode) {
				lhsEdge = ((EdgeTypeChangeDeclNode)lhsEdge).getOldEdge();
			}

			// add connection only if source and target are reused
			if(!deletes.getChildren().contains(lhsEdge)
					&& !sourceOrTargetNodeIncluded(lhsEdge, pattern, deletes.getChildren())) {
				connectionsToReuse.add(connection);
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
			if(rhsNodes.contains(lhsNode) || !deletes.getChildren().contains(lhsNode))
				nodesToReuse.add(lhsNode);
		}
		
		return nodesToReuse;
	}

	private void warnIfElementAppearsInsideAndOutsideOfDelete(PatternGraphLhsNode pattern)
	{
		Set<ConstraintDeclNode> elementsToDelete = getElementsToDelete(pattern);

		Set<ConstraintDeclNode> alreadyReported = new HashSet<ConstraintDeclNode>();
		for(ConnectionCharacter connectionCharacter : patternGraph.getConnections()) {
			ConstraintDeclNode element = null;
			if(connectionCharacter instanceof SingleNodeConnNode) {
				SingleNodeConnNode singleNodeConnection = (SingleNodeConnNode)connectionCharacter;
				element = singleNodeConnection.getNode();
			} else { //if(connectionCharacter instanceof ConnectionNode)
				ConnectionNode connection = (ConnectionNode)connectionCharacter;
				element = connection.getEdge();
			}

			if(alreadyReported.contains(element)) {
				continue;
			}

			for(ConstraintDeclNode elementToDelete : elementsToDelete) {
				if(element.equals(elementToDelete)) {
					if(element.defEntityToBeYieldedTo)
						continue;
					
					connectionCharacter.reportWarning("\"" + elementToDelete + "\" appears inside as well as outside a delete statement");
					alreadyReported.add(element);
				}
			}
		}
	}

	@Override
	protected Set<ConnectionNode> getConnectionsNotDeleted(PatternGraphLhsNode pattern)
	{
		Set<ConnectionNode> connectionsNotDeleted = new LinkedHashSet<ConnectionNode>();

		Set<ConstraintDeclNode> elementsToDelete = getElementsToDelete(pattern);

		for(ConnectionCharacter connectionCharacter : pattern.getConnections()) {
			if(!(connectionCharacter instanceof ConnectionNode))
				continue;
			
			ConnectionNode connection = (ConnectionNode)connectionCharacter;
			if(!elementsToDelete.contains(connection.getEdge())
					&& !elementsToDelete.contains(connection.getSrc())
					&& !elementsToDelete.contains(connection.getTgt())) {
				connectionsNotDeleted.add(connection);
			}
		}

		return connectionsNotDeleted;
	}
}
