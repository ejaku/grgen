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

import de.unika.ipd.grgen.ast.BaseNode;
import de.unika.ipd.grgen.ast.CollectNode;
import de.unika.ipd.grgen.ast.IdentNode;
import de.unika.ipd.grgen.ast.decl.DeclNode;
import de.unika.ipd.grgen.ast.decl.executable.SubpatternDeclNode;
import de.unika.ipd.grgen.ast.pattern.ConnectionCharacter;
import de.unika.ipd.grgen.ast.pattern.ConnectionNode;
import de.unika.ipd.grgen.ast.pattern.GraphNode;
import de.unika.ipd.grgen.ast.pattern.PatternGraphNode;
import de.unika.ipd.grgen.ast.pattern.SingleNodeConnNode;
import de.unika.ipd.grgen.ast.util.CollectTripleResolver;
import de.unika.ipd.grgen.ast.util.DeclarationTripleResolver;
import de.unika.ipd.grgen.ast.util.Triple;
import de.unika.ipd.grgen.ir.Entity;
import de.unika.ipd.grgen.ir.pattern.Edge;
import de.unika.ipd.grgen.ir.pattern.Node;
import de.unika.ipd.grgen.ir.pattern.OrderedReplacement;
import de.unika.ipd.grgen.ir.pattern.OrderedReplacements;
import de.unika.ipd.grgen.ir.pattern.PatternGraph;
import de.unika.ipd.grgen.ir.pattern.SubpatternDependentReplacement;
import de.unika.ipd.grgen.ir.pattern.SubpatternUsage;

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

	private CollectNode<IdentNode> deletesUnresolved;
	private CollectNode<DeclNode> deletes = new CollectNode<DeclNode>();

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
		this.deletesUnresolved = dels;
		becomeParent(this.deletesUnresolved);
	}

	/** returns children of this node */
	public Collection<BaseNode> getChildren()
	{
		Vector<BaseNode> children = new Vector<BaseNode>();
		children.add(ident);
		children.add(getValidVersion(typeUnresolved, type));
		children.add(graph);
		children.add(getValidVersion(deletesUnresolved, deletes));
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
	public PatternGraph getPatternGraph(PatternGraph left)
	{
		PatternGraph right = graph.getGraph();

		HashSet<Entity> deleteSet = insertToBeDeletedElementsToLhsIfNotFromLhs(left, right);

		insertLhsElementsToRhs(left, deleteSet, right);

		insertElementsFromTypeofToRhsIfNotYetContained(right, deleteSet);

		for(SubpatternUsage sub : left.getSubpatternUsages()) {
			if(!isSubpatternRewritePartUsed(sub, right) && !isSubpatternDeleted(sub)) {
				right.addSubpatternUsage(sub); // keep subpattern
			}
		}

		insertElementsFromEvalIntoRhs(left, right);
		insertElementsFromOrderedReplacementsIntoRhs(left, right);

		return right;
	}

	private HashSet<Entity> insertToBeDeletedElementsToLhsIfNotFromLhs(PatternGraph left, PatternGraph right)
	{
		HashSet<Entity> deleteSet = new HashSet<Entity>();
		
		for(DeclNode del : deletes.getChildren()) {
			if(del instanceof SubpatternUsageDeclNode)
				continue;

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
		
		return deleteSet;
	}

	// inserts to be kept nodes/edges and to be deleted nodes/edges, to be created nodes/edges are already contained
	private void insertLhsElementsToRhs(PatternGraph left, HashSet<Entity> deleteSet, PatternGraph right)
	{
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
	}

	private void insertElementsFromTypeofToRhsIfNotYetContained(PatternGraph right, HashSet<Entity> deleteSet)
	{
		for(Node node : right.getNodes()) {
			if(node.inheritsType()) {
				Node nodeFromTypeof = (Node)node.getTypeof();
				if(!deleteSet.contains(nodeFromTypeof)) {
					right.addNodeIfNotYetContained(nodeFromTypeof);
				}
			}
		}
		for(Edge edge : right.getEdges()) {
			if(edge.inheritsType()) {
				Edge edgeFromTypeof = (Edge)edge.getTypeof();
				if(!deleteSet.contains(edgeFromTypeof)) {
					right.addEdgeIfNotYetContained(edgeFromTypeof);
				}
			}
		}
	}

	private boolean isSubpatternRewritePartUsed(SubpatternUsage sub, PatternGraph right)
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

	private boolean isSubpatternDeleted(SubpatternUsage sub)
	{
		for(DeclNode del : deletes.getChildren()) {
			if(!(del instanceof SubpatternUsageDeclNode))
				continue;

			SubpatternUsage delSub = del.checkIR(SubpatternUsage.class);
			if(sub == delSub)
				return true;
		}
		return false;
	}

	@Override
	public Set<DeclNode> getDeleted(PatternGraphNode pattern)
	{
		assert isResolved();

		if(deletedElements != null)
			return deletedElements;

		LinkedHashSet<DeclNode> deleted = new LinkedHashSet<DeclNode>();

		for(DeclNode del : deletes.getChildren()) {
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
	public Collection<ConnectionNode> getReusedConnections(PatternGraphNode pattern)
	{
		Collection<ConnectionNode> res = new LinkedHashSet<ConnectionNode>();
		Collection<EdgeDeclNode> lhs = pattern.getEdges();

		for(ConnectionCharacter connectionCharacter : graph.getConnections()) {
			if(connectionCharacter instanceof ConnectionNode) {
				ConnectionNode connection = (ConnectionNode)connectionCharacter;
				EdgeDeclNode edge = connection.getEdge();
				while(edge instanceof EdgeTypeChangeDeclNode) {
					edge = ((EdgeTypeChangeDeclNode)edge).getOldEdge();
				}

				// add connection only if source and target are reused
				if(lhs.contains(edge) && !sourceOrTargetNodeIncluded(pattern, deletes.getChildren(), edge)) {
					res.add(connection);
				}
			}
		}

		for(ConnectionCharacter connectionCharacter : pattern.getConnections()) {
			if(connectionCharacter instanceof ConnectionNode) {
				ConnectionNode connection = (ConnectionNode)connectionCharacter;
				EdgeDeclNode edge = connection.getEdge();
				while(edge instanceof EdgeTypeChangeDeclNode) {
					edge = ((EdgeTypeChangeDeclNode)edge).getOldEdge();
				}

				// add connection only if source and target are reused
				if(!deletes.getChildren().contains(edge)
						&& !sourceOrTargetNodeIncluded(pattern, deletes.getChildren(), edge)) {
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
			if(rhsNodes.contains(node) || !deletes.getChildren().contains(node))
				coll.add(node);
		}

		reusedNodes = Collections.unmodifiableSet(coll);
		return reusedNodes;
	}

	@Override
	public void warnElemAppearsInsideAndOutsideDelete(PatternGraphNode pattern)
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
