/*
 * GrGen: graph rewrite generator tool -- release GrGen.NET 5.0
 * Copyright (C) 2003-2020 Universitaet Karlsruhe, Institut fuer Programmstrukturen und Datenorganisation, LS Goos; and free programmers
 * licensed under LGPL v3 (see LICENSE.txt included in the packaging of this file)
 * www.grgen.net
 */

/**
 * @author Sebastian Buchwald
 */

package de.unika.ipd.grgen.ast;

import de.unika.ipd.grgen.ast.util.CollectTripleResolver;
import de.unika.ipd.grgen.ast.util.DeclarationTripleResolver;
import de.unika.ipd.grgen.ast.util.Triple;
import de.unika.ipd.grgen.ir.Edge;
import de.unika.ipd.grgen.ir.Entity;
import de.unika.ipd.grgen.ir.Node;
import de.unika.ipd.grgen.ir.OrderedReplacements;
import de.unika.ipd.grgen.ir.PatternGraph;
import de.unika.ipd.grgen.ir.OrderedReplacement;
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
public class ModifyDeclNode extends RhsDeclNode
{
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
	 */
	public ModifyDeclNode(IdentNode id, GraphNode graph, CollectNode<IdentNode> dels)
	{
		super(id, graph);
		this.deleteUnresolved = dels;
		becomeParent(this.deleteUnresolved);
	}

	/** returns children of this node */
	public Collection<BaseNode> getChildren()
	{
		Vector<BaseNode> children = new Vector<BaseNode>();
		children.add(ident);
		children.add(getValidVersion(typeUnresolved, type));
		children.add(graph);
		children.add(getValidVersion(deleteUnresolved, delete));
		return children;
	}

	/** returns names of the children, same order as in getChildren */
	public Collection<String> getChildrenNames()
	{
		Vector<String> childrenNames = new Vector<String>();
		childrenNames.add("ident");
		childrenNames.add("type");
		childrenNames.add("right");
		childrenNames.add("delete");
		return childrenNames;
	}

	private static final CollectTripleResolver<NodeDeclNode, EdgeDeclNode, SubpatternUsageNode> deleteResolver =
		new CollectTripleResolver<NodeDeclNode, EdgeDeclNode, SubpatternUsageNode>(
			new DeclarationTripleResolver<NodeDeclNode, EdgeDeclNode, SubpatternUsageNode>(
				NodeDeclNode.class, EdgeDeclNode.class, SubpatternUsageNode.class));

	/** @see de.unika.ipd.grgen.ast.BaseNode#resolveLocal() */
	@Override
	protected boolean resolveLocal()
	{
		Triple<CollectNode<NodeDeclNode>, CollectNode<EdgeDeclNode>, CollectNode<SubpatternUsageNode>> resolve =
			deleteResolver.resolve(deleteUnresolved);

		if(resolve != null) {
			if(resolve.first != null) {
				for(NodeDeclNode node : resolve.first.getChildren()) {
					delete.addChild(node);
				}
			}

			if(resolve.second != null) {
				for(EdgeDeclNode edge : resolve.second.getChildren()) {
					delete.addChild(edge);
				}
			}

			if(resolve.third != null) {
				for(SubpatternUsageNode sub : resolve.third.getChildren()) {
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
		for(BaseNode del : delete.getChildren()) {
			if(!(del instanceof SubpatternUsageNode)) {
				ConstraintDeclNode element = (ConstraintDeclNode)del;
				Entity entity = element.checkIR(Entity.class);
				deleteSet.add(entity);
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
		}

		for(Node node : left.getNodes()) {
			if(!deleteSet.contains(node)) {
				right.addSingleNode(node);
			} else {
				right.addDeletedElement(node);
			}
		}
		for(Edge edge : left.getEdges()) {
			if(!deleteSet.contains(edge)
					&& !deleteSet.contains(left.getSource(edge))
					&& !deleteSet.contains(left.getTarget(edge))) {
				right.addConnection(left.getSource(edge), edge, left.getTarget(edge),
						edge.hasFixedDirection(), false, false);
			} else {
				right.addDeletedElement(edge);
			}
		}

		// add elements only mentioned in typeof to the pattern
		for(Node node : right.getNodes()) {
			if(node.inheritsType()) {
				Node nodeFromTypeof = (Node)node.getTypeof();
				if(!deleteSet.contains(nodeFromTypeof)) {
					graph.addNodeIfNotYetContained(right, nodeFromTypeof);
				}
			}
		}
		for(Edge edge : right.getEdges()) {
			if(edge.inheritsType()) {
				Edge edgeFromTypeof = (Edge)edge.getTypeof();
				if(!deleteSet.contains(edgeFromTypeof)) {
					graph.addEdgeIfNotYetContained(right, edgeFromTypeof);
				}
			}
		}

		for(SubpatternUsage sub : left.getSubpatternUsages()) {
			boolean subHasDepModify = false;
			for(OrderedReplacements orderedRepls : right.getOrderedReplacements()) {
				for(OrderedReplacement orderedRepl : orderedRepls.orderedReplacements) {
					if(!(orderedRepl instanceof SubpatternDependentReplacement))
						continue;
					SubpatternDependentReplacement subRepl = (SubpatternDependentReplacement)orderedRepl;
					if(sub == subRepl.getSubpatternUsage()) {
						subHasDepModify = true;
						break;
					}
				}
			}
			boolean subInDeleteSet = false;
			for(BaseNode del : delete.getChildren()) {
				if(del instanceof SubpatternUsageNode) {
					SubpatternUsage delSub = del.checkIR(SubpatternUsage.class);
					if(sub == delSub) {
						subInDeleteSet = true;
					}
				}
			}

			if(!subHasDepModify && !subInDeleteSet) {
				right.addSubpatternUsage(sub);
			}
		}

		insertElementsFromEvalIntoRhs(left, right);
		insertElementsFromOrderedReplacementsIntoRhs(left, right);

		return right;
	}

	@Override
	protected Set<DeclNode> getDeleted(PatternGraphNode pattern)
	{
		assert isResolved();

		if(deletedElements != null)
			return deletedElements;

		LinkedHashSet<DeclNode> deleted = new LinkedHashSet<DeclNode>();

		for(DeclNode del : delete.getChildren()) {
			if(!(del instanceof SubpatternDeclNode))
				deleted.add(del);
		}

		// add edges with deleted source or target
		for(ConnectionCharacter connectionCharacter : pattern.getConnections()) {
			if(connectionCharacter instanceof ConnectionNode) {
				ConnectionNode connection = (ConnectionNode)connectionCharacter;
				if(deleted.contains(connection.getSrc()) || deleted.contains(connection.getTgt()))
					deleted.add(connection.getEdge());
			}
		}
		for(ConnectionCharacter connectionCharacter : graph.getConnections()) {
			if(connectionCharacter instanceof ConnectionNode) {
				ConnectionNode connection = (ConnectionNode)connectionCharacter;
				if(deleted.contains(connection.getSrc()) || deleted.contains(connection.getTgt()))
					deleted.add(connection.getEdge());
			}
		}

		deletedElements = Collections.unmodifiableSet(deleted);

		return deletedElements;
	}

	/**
	 * Return all reused edges (with their nodes), that excludes new edges of
	 * the right-hand side.
	 */
	@Override
	protected Collection<ConnectionNode> getReusedConnections(PatternGraphNode pattern)
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

				// add connection only if source and target are reused
				if(lhs.contains(edge) && !sourceOrTargetNodeIncluded(pattern, delete.getChildren(), edge)) {
					res.add(connection);
				}
			}
		}

		for(ConnectionCharacter connectionCharacter : pattern.getConnections()) {
			if(connectionCharacter instanceof ConnectionNode) {
				ConnectionNode connection = (ConnectionNode)connectionCharacter;
				EdgeDeclNode edge = connection.getEdge();
				while(edge instanceof EdgeTypeChangeNode) {
					edge = ((EdgeTypeChangeNode)edge).getOldEdge();
				}

				// add connection only if source and target are reused
				if(!delete.getChildren().contains(edge)
						&& !sourceOrTargetNodeIncluded(pattern, delete.getChildren(), edge)) {
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
	protected Set<BaseNode> getReusedNodes(PatternGraphNode pattern)
	{
		if(reusedNodes != null)
			return reusedNodes;

		LinkedHashSet<BaseNode> coll = new LinkedHashSet<BaseNode>();
		Set<NodeDeclNode> patternNodes = pattern.getNodes();
		Set<NodeDeclNode> rhsNodes = graph.getNodes();

		for(NodeDeclNode node : patternNodes) {
			if(rhsNodes.contains(node) || !delete.getChildren().contains(node))
				coll.add(node);
		}

		reusedNodes = Collections.unmodifiableSet(coll);
		return reusedNodes;
	}

	@Override
	protected void warnElemAppearsInsideAndOutsideDelete(PatternGraphNode pattern)
	{
		Set<DeclNode> deletes = getDeleted(pattern);

		Set<BaseNode> alreadyReported = new HashSet<BaseNode>();
		for(ConnectionCharacter connectionCharacter : graph.getConnections()) {
			BaseNode elem = BaseNode.getErrorNode();
			if(connectionCharacter instanceof SingleNodeConnNode) {
				SingleNodeConnNode singleNodeConnection = (SingleNodeConnNode)connectionCharacter;
				elem = singleNodeConnection.getNode();
			} else if(connectionCharacter instanceof ConnectionNode) {
				ConnectionNode connection = (ConnectionNode)connectionCharacter;
				elem = connection.getEdge();
			}

			if(alreadyReported.contains(elem)) {
				continue;
			}

			for(DeclNode del : deletes) {
				if(elem.equals(del)) {
					if(elem instanceof ConstraintDeclNode && ((ConstraintDeclNode)elem).defEntityToBeYieldedTo)
						continue;
					connectionCharacter.reportWarning("\"" + del + "\" appears inside as well as outside a delete statement");
					alreadyReported.add(elem);
				}
			}
		}
	}

	@Override
	protected Collection<ConnectionNode> getResultingConnections(PatternGraphNode pattern)
	{
		Collection<ConnectionNode> res = new LinkedHashSet<ConnectionNode>();

		Collection<DeclNode> delete = getDeleted(pattern);

		for(ConnectionCharacter connectionCharacter : pattern.getConnections()) {
			if(connectionCharacter instanceof ConnectionNode) {
				ConnectionNode connection = (ConnectionNode)connectionCharacter;
				if(!delete.contains(connection.getEdge())
						&& !delete.contains(connection.getSrc())
						&& !delete.contains(connection.getTgt())) {
					res.add(connection);
				}
			}
		}

		return res;
	}
}
