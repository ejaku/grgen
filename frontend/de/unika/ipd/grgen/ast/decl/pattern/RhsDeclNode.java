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

import java.util.Collection;
import java.util.LinkedHashSet;
import java.util.Set;
import java.util.Vector;

import de.unika.ipd.grgen.ast.BaseNode;
import de.unika.ipd.grgen.ast.IdentNode;
import de.unika.ipd.grgen.ast.decl.DeclNode;
import de.unika.ipd.grgen.ast.pattern.ConnectionCharacter;
import de.unika.ipd.grgen.ast.pattern.ConnectionNode;
import de.unika.ipd.grgen.ast.pattern.GraphNode;
import de.unika.ipd.grgen.ast.pattern.PatternGraphNode;
import de.unika.ipd.grgen.ast.type.RhsTypeNode;
import de.unika.ipd.grgen.ast.type.TypeNode;
import de.unika.ipd.grgen.ast.util.DeclarationTypeResolver;
import de.unika.ipd.grgen.ir.IR;
import de.unika.ipd.grgen.ir.NeededEntities;
import de.unika.ipd.grgen.ir.stmt.EvalStatement;
import de.unika.ipd.grgen.ir.stmt.EvalStatements;
import de.unika.ipd.grgen.ir.Emit;
import de.unika.ipd.grgen.ir.pattern.Edge;
import de.unika.ipd.grgen.ir.pattern.Node;
import de.unika.ipd.grgen.ir.pattern.OrderedReplacement;
import de.unika.ipd.grgen.ir.pattern.OrderedReplacements;
import de.unika.ipd.grgen.ir.pattern.PatternGraph;
import de.unika.ipd.grgen.ir.pattern.Variable;

/**
 * AST node for a replacement right-hand side.
 */
public abstract class RhsDeclNode extends DeclNode
{
	static {
		setName(RhsDeclNode.class, "right-hand side declaration");
	}

	public GraphNode graph;
	protected RhsTypeNode type;

	/** Type for this declaration. */
	protected static final TypeNode rhsType = new RhsTypeNode();

	/**
	 * Make a new right-hand side.
	 * @param id The identifier of this RHS.
	 * @param graph The right hand side graph.
	 */
	public RhsDeclNode(IdentNode id, GraphNode graph)
	{
		super(id, rhsType);
		this.graph = graph;
		becomeParent(this.graph);
	}

	/** returns children of this node */
	@Override
	public Collection<BaseNode> getChildren()
	{
		Vector<BaseNode> children = new Vector<BaseNode>();
		children.add(ident);
		children.add(getValidVersion(typeUnresolved, type));
		children.add(graph);
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

	public GraphNode getRHSGraph()
	{
		return graph;
	}

	public Collection<DeclNode> getMaybeDeleted(PatternGraphNode pattern)
	{
		// add deleted entities
		Collection<DeclNode> ret = new LinkedHashSet<DeclNode>();
		ret.addAll(getDeleted(pattern));

		// extract deleted nodes, then add homomorphic nodes
		Collection<NodeDeclNode> nodes = new LinkedHashSet<NodeDeclNode>();
		for(DeclNode declNode : ret) {
			if(declNode instanceof NodeDeclNode) {
				nodes.add((NodeDeclNode)declNode);
			}
		}
		for(NodeDeclNode node : nodes) {
			ret.addAll(pattern.getHomomorphic(node));
		}

		// add edges resulting from deleted nodes (only needed if a deleted node exists)
		if(nodes.size() > 0) {
			addEdgesResultingFromDeletedNodes(ret, pattern);
		}

		// extract deleted edges, then add homomorphic edges
		Collection<EdgeDeclNode> edges = new LinkedHashSet<EdgeDeclNode>();
		for(DeclNode declNode : ret) {
			if(declNode instanceof EdgeDeclNode) {
				edges.add((EdgeDeclNode)declNode);
			}
		}
		for(EdgeDeclNode edge : edges) {
			ret.addAll(pattern.getHomomorphic(edge));
		}

		return ret;
	}

	private void addEdgesResultingFromDeletedNodes(Collection<DeclNode> ret, PatternGraphNode pattern)
	{
		Collection<ConnectionNode> conns = getResultingConnections(pattern);

		// edges of deleted nodes are deleted, too --> add them
		for(ConnectionNode conn : conns) {
			if(sourceOrTargetNodeIncluded(pattern, ret, conn.getEdge())) {
				ret.add(conn.getEdge());
			}
		}

		// nodes of dangling edges are homomorphic to all other nodes,
		// especially the deleted ones :-)
		for(ConnectionNode conn : conns) {
			EdgeDeclNode edge = conn.getEdge();
			while(edge instanceof EdgeTypeChangeNode) {
				edge = ((EdgeTypeChangeNode)edge).getOldEdge();
			}
			boolean srcIsDummy = true;
			boolean tgtIsDummy = true;
			for(ConnectionNode innerConn : conns) {
				if(edge.equals(innerConn.getEdge())) {
					srcIsDummy &= innerConn.getSrc().isDummy();
					tgtIsDummy &= innerConn.getTgt().isDummy();
				}
			}

			// so maybe the dangling edge is deleted by one of the node deletions --> add it
			if(srcIsDummy || tgtIsDummy) {
				ret.add(edge);
			}
		}
	}

	/** only used in checks against usage of deleted elements */
	protected abstract Collection<ConnectionNode> getResultingConnections(PatternGraphNode pattern);

	protected static final DeclarationTypeResolver<RhsTypeNode> typeResolver =
			new DeclarationTypeResolver<RhsTypeNode>(RhsTypeNode.class);

	/** @see de.unika.ipd.grgen.ast.BaseNode#resolveLocal() */
	@Override
	protected boolean resolveLocal()
	{
		type = typeResolver.resolve(typeUnresolved, this);

		return type != null;
	}

	/**
	 * Edges as replacement parameters are not really needed but very troublesome,
	 * keep them out for now.
	 */
	private boolean checkEdgeParameters()
	{
		boolean res = true;

		for(DeclNode replParam : graph.getParamDecls()) {
			if(replParam instanceof EdgeDeclNode) {
				replParam.reportError("edges not supported as replacement parameters: " + replParam.ident.toString());
				res = false;
			}
		}

		return res;
	}

	/**
	 * @see de.unika.ipd.grgen.ast.BaseNode#checkLocal()
	 */
	@Override
	protected boolean checkLocal()
	{
		return checkEdgeParameters();
	}

	/**
	 * @see de.unika.ipd.grgen.ast.BaseNode#constructIR()
	 */
	@Override
	protected IR constructIR()
	{
		assert false;

		return null;
	}

	protected void insertElementsFromEvalIntoRhs(PatternGraph left, PatternGraph right)
	{
		// insert all elements, which are used in eval statements (of the right hand side) and
		// neither declared on the local left hand nor on the right hand side to the right hand side
		// further code (PatternGraph::insertElementsFromRhsDeclaredInNestingLhsToLocalLhs)
		// will add them to the left hand side, too

		NeededEntities needs = new NeededEntities(true, true, true, false, false, false, false, false);
		Collection<EvalStatements> evalStatements = graph.getYieldEvalStatements();
		for(EvalStatements eval : evalStatements) {
			eval.collectNeededEntities(needs);
		}

		for(Node neededNode : needs.nodes) {
			if(neededNode.directlyNestingLHSGraph != left) {
				if(!right.getDeletedElements().contains(neededNode)) {
					if(!right.hasNode(neededNode)) {
						right.addSingleNode(neededNode);
						right.addHomToAll(neededNode);
					}
				}
			}
		}
		for(Edge neededEdge : needs.edges) {
			if(neededEdge.directlyNestingLHSGraph != left) {
				if(!right.getDeletedElements().contains(neededEdge)) {
					if(!right.hasEdge(neededEdge)) {
						right.addSingleEdge(neededEdge); // TODO: maybe we lose context here
						right.addHomToAll(neededEdge);
					}
				}
			}
		}
		for(Variable neededVariable : needs.variables) {
			if(neededVariable.directlyNestingLHSGraph != left) {
				if(!right.hasVar(neededVariable)) {
					right.addVariable(neededVariable);
				}
			}
		}
	}

	protected void insertElementsFromOrderedReplacementsIntoRhs(PatternGraph left, PatternGraph right)
	{
		// insert all elements, which are used in ordered replacements (of the right hand side) and
		// neither declared on the local left hand nor on the right hand side to the right hand side
		// further code (PatternGraph::insertElementsFromRhsDeclaredInNestingLhsToLocalLhs)
		// will add them to the left hand side, too

		NeededEntities needs = new NeededEntities(true, true, true, false, false, false, false, false);
		Collection<OrderedReplacements> evalStatements = graph.getOrderedReplacements();
		for(OrderedReplacements eval : evalStatements) {
			for(OrderedReplacement orderedReplacement : eval.orderedReplacements) {
				if(orderedReplacement instanceof EvalStatement)
					((EvalStatement)orderedReplacement).collectNeededEntities(needs);
				else if(orderedReplacement instanceof Emit)
					((Emit)orderedReplacement).collectNeededEntities(needs);
			}
		}

		for(Node neededNode : needs.nodes) {
			if(neededNode.directlyNestingLHSGraph != left) {
				if(!right.getDeletedElements().contains(neededNode)) {
					if(!right.hasNode(neededNode)) {
						right.addSingleNode(neededNode);
						right.addHomToAll(neededNode);
					}
				}
			}
		}
		for(Edge neededEdge : needs.edges) {
			if(neededEdge.directlyNestingLHSGraph != left) {
				if(!right.getDeletedElements().contains(neededEdge)) {
					if(!right.hasEdge(neededEdge)) {
						right.addSingleEdge(neededEdge); // TODO: maybe we lose context here
						right.addHomToAll(neededEdge);
					}
				}
			}
		}
		for(Variable neededVariable : needs.variables) {
			if(neededVariable.directlyNestingLHSGraph != left) {
				if(!right.hasVar(neededVariable)) {
					right.addVariable(neededVariable);
				}
			}
		}
	}

	public abstract PatternGraph getPatternGraph(PatternGraph left);

	@Override
	public RhsTypeNode getDeclType()
	{
		assert isResolved();

		return type;
	}

	/** only used in checks against usage of deleted elements */
	public abstract Set<DeclNode> getDeleted(PatternGraphNode pattern);

	/**
	 * Return all reused edges (with their nodes), that excludes new edges of
	 * the right-hand side.
	 */
	public abstract Collection<ConnectionNode> getReusedConnections(PatternGraphNode pattern);

	/**
	 * Return all reused nodes, that excludes new nodes of the right-hand side.
	 */
	public abstract Set<BaseNode> getReusedNodes(PatternGraphNode pattern);

	public abstract void warnElemAppearsInsideAndOutsideDelete(PatternGraphNode pattern);

	protected boolean sourceOrTargetNodeIncluded(PatternGraphNode pattern, Collection<? extends BaseNode> coll,
			EdgeDeclNode edgeDecl)
	{
		for(ConnectionCharacter connectionCharacter : pattern.getConnections()) {
			if(connectionCharacter instanceof ConnectionNode) {
				ConnectionNode connection = (ConnectionNode)connectionCharacter;
				if(connection.getEdge().equals(edgeDecl)) {
					if(coll.contains(connection.getSrc()) || coll.contains(connection.getTgt())) {
						return true;
					}
				}
			}
		}
		return false;
	}
}