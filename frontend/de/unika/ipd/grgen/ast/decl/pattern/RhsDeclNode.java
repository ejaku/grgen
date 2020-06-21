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

import java.util.Collection;
import java.util.Collections;
import java.util.HashSet;
import java.util.LinkedHashSet;
import java.util.Set;
import java.util.Vector;

import de.unika.ipd.grgen.ast.BaseNode;
import de.unika.ipd.grgen.ast.IdentNode;
import de.unika.ipd.grgen.ast.decl.DeclNode;
import de.unika.ipd.grgen.ast.pattern.ConnectionCharacter;
import de.unika.ipd.grgen.ast.pattern.ConnectionNode;
import de.unika.ipd.grgen.ast.pattern.PatternGraphRhsNode;
import de.unika.ipd.grgen.ast.pattern.PatternGraphLhsNode;
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
import de.unika.ipd.grgen.ir.pattern.PatternGraphLhs;
import de.unika.ipd.grgen.ir.pattern.PatternGraphRhs;
import de.unika.ipd.grgen.ir.pattern.Variable;

/**
 * AST node for a replacement right-hand side.
 */
public abstract class RhsDeclNode extends DeclNode
{
	static {
		setName(RhsDeclNode.class, "right-hand side declaration");
	}

	public PatternGraphRhsNode patternGraph;
	protected RhsTypeNode type;

	/** Type for this declaration. */
	protected static final TypeNode rhsType = new RhsTypeNode();

	// Cache variables
	private Set<ConstraintDeclNode> elementsToDelete;
	private Set<NodeDeclNode> nodesToReuse;
	private Set<ConnectionNode> connectionsToReuse; // edgesToReuse in connection form

	
	/**
	 * Make a new right-hand side.
	 * @param id The identifier of this RHS.
	 * @param patternGraph The right hand side graph.
	 */
	protected RhsDeclNode(IdentNode id, PatternGraphRhsNode patternGraph)
	{
		super(id, rhsType);
		this.patternGraph = patternGraph;
		becomeParent(this.patternGraph);
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

	public PatternGraphRhsNode getRhsGraph()
	{
		return patternGraph;
	}

	public Set<ConstraintDeclNode> getMaybeDeletedElements(PatternGraphLhsNode pattern)
	{
		// add deleted entities
		Set<ConstraintDeclNode> maybeDeletedElements = new LinkedHashSet<ConstraintDeclNode>();
		maybeDeletedElements.addAll(getElementsToDelete(pattern));

		// extract deleted nodes, then add homomorphic nodes
		Set<NodeDeclNode> nodes = new LinkedHashSet<NodeDeclNode>();
		for(ConstraintDeclNode maybeDeletedElement : maybeDeletedElements) {
			if(maybeDeletedElement instanceof NodeDeclNode) {
				nodes.add((NodeDeclNode)maybeDeletedElement);
			}
		}
		for(NodeDeclNode node : nodes) {
			maybeDeletedElements.addAll(pattern.getHomomorphic(node));
		}

		// add edges resulting from deleted nodes (only needed if a deleted node exists)
		if(nodes.size() > 0) {
			maybeDeletedElements.addAll(getMaybeDeletedEdgesResultingFromMaybeDeletedNodes(maybeDeletedElements, pattern));
		}

		// extract deleted edges, then add homomorphic edges
		Set<EdgeDeclNode> edges = new LinkedHashSet<EdgeDeclNode>();
		for(ConstraintDeclNode maybeDeletedElement : maybeDeletedElements) {
			if(maybeDeletedElement instanceof EdgeDeclNode) {
				edges.add((EdgeDeclNode)maybeDeletedElement);
			}
		}
		for(EdgeDeclNode edge : edges) {
			maybeDeletedElements.addAll(pattern.getHomomorphic(edge));
		}

		return maybeDeletedElements;
	}

	private Set<ConstraintDeclNode> getMaybeDeletedEdgesResultingFromMaybeDeletedNodes(Set<ConstraintDeclNode> maybeDeletedNodes, PatternGraphLhsNode pattern)
	{
		Set<ConstraintDeclNode> edgesResultingFromMaybeDeletedNodes = new HashSet<ConstraintDeclNode>();
		
		// edges of deleted nodes are deleted, too --> add them
		Set<ConnectionNode> connections = getConnectionsNotDeleted(pattern);
		for(ConnectionNode connection : connections) {
			if(sourceOrTargetNodeIncluded(connection.getEdge(), pattern, maybeDeletedNodes)) {
				edgesResultingFromMaybeDeletedNodes.add(connection.getEdge());
			}
		}

		// nodes of dangling edges are homomorphic to all other nodes,
		// especially the deleted ones :-)
		for(ConnectionNode connection : connections) {
			EdgeDeclNode edge = connection.getEdge();
			while(edge instanceof EdgeTypeChangeDeclNode) {
				edge = ((EdgeTypeChangeDeclNode)edge).getOldEdge();
			}
			boolean srcIsDummy = true;
			boolean tgtIsDummy = true;
			for(ConnectionNode innerConn : connections) {
				if(edge.equals(innerConn.getEdge())) {
					srcIsDummy &= innerConn.getSrc().isDummy();
					tgtIsDummy &= innerConn.getTgt().isDummy();
				}
			}

			// so maybe the dangling edge is deleted by one of the node deletions --> add it
			if(srcIsDummy || tgtIsDummy) {
				edgesResultingFromMaybeDeletedNodes.add(edge);
			}
		}
		
		return edgesResultingFromMaybeDeletedNodes;
	}

	protected abstract Set<ConnectionNode> getConnectionsNotDeleted(PatternGraphLhsNode pattern);

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
	 * Edges as replacement parameters are not really needed but very troublesome, keep them out for now.
	 */
	private boolean checkEdgeParameters()
	{
		boolean res = true;

		for(DeclNode replParam : patternGraph.getParamDecls()) {
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
	
	public abstract boolean checkAgainstLhsPattern(PatternGraphLhsNode pattern);
	
	/**
	 * @see de.unika.ipd.grgen.ast.BaseNode#constructIR()
	 */
	@Override
	protected IR constructIR()
	{
		assert false;

		return null;
	}

	protected void insertElementsFromEvalsIntoRhs(PatternGraphLhs left, PatternGraphRhs right)
	{
		// insert all elements, which are used in eval statements (of the right hand side) and
		// neither declared on the local left hand nor on the right hand side to the right hand side
		// further code (PatternGraph::insertElementsFromRhsDeclaredInNestingLhsToLocalLhs)
		// will add them to the left hand side, too

		NeededEntities needs = new NeededEntities(true, true, true, false, false, false, false, false);
		Collection<EvalStatements> evalStatements = patternGraph.getEvalStatements();
		for(EvalStatements evalStatement : evalStatements) {
			evalStatement.collectNeededEntities(needs);
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
						right.addSingleEdge(neededEdge);
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

	protected void insertElementsFromOrderedReplacementsIntoRhs(PatternGraphLhs left, PatternGraphRhs right)
	{
		// insert all elements, which are used in ordered replacements (of the right hand side) and
		// neither declared on the local left hand nor on the right hand side to the right hand side
		// further code (PatternGraph::insertElementsFromRhsDeclaredInNestingLhsToLocalLhs)
		// will add them to the left hand side, too

		NeededEntities needs = new NeededEntities(true, true, true, false, false, false, false, false);
		Collection<OrderedReplacements> evalStatements = patternGraph.getOrderedReplacements();
		for(OrderedReplacements evalStatement : evalStatements) {
			for(OrderedReplacement orderedReplacement : evalStatement.orderedReplacements) {
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
						right.addSingleEdge(neededEdge);
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

	public abstract PatternGraphRhs getPatternGraph(PatternGraphLhs left);

	@Override
	public RhsTypeNode getDeclType()
	{
		assert isResolved();

		return type;
	}

	/**
	 * Returns all elements that are to be deleted.
	 */
	public Set<ConstraintDeclNode> getElementsToDelete(PatternGraphLhsNode pattern)
	{
		if(elementsToDelete == null) {
			elementsToDelete = Collections.unmodifiableSet(getElementsToDeleteImpl(pattern));
		}
		return elementsToDelete;
	}

	protected abstract Set<ConstraintDeclNode> getElementsToDeleteImpl(PatternGraphLhsNode pattern);

	/**
	 * Returns all to be reused edges (with their nodes, in the form of a connection),
	 * that excludes new edges of the right-hand side, those are to be created.
	 */
	public Set<ConnectionNode> getConnectionsToReuse(PatternGraphLhsNode pattern)
	{
		if(connectionsToReuse == null) {
			connectionsToReuse = Collections.unmodifiableSet(getConnectionsToReuseImpl(pattern));
		}
		return connectionsToReuse;
	}
	
	protected abstract Set<ConnectionNode> getConnectionsToReuseImpl(PatternGraphLhsNode pattern);

	/**
	 * Returns all to be reused nodes, that excludes new nodes of the right-hand side, those are to be created.
	 */
	public Set<NodeDeclNode> getNodesToReuse(PatternGraphLhsNode pattern)
	{
		if(nodesToReuse == null) {
			nodesToReuse = Collections.unmodifiableSet(getNodesToReuseImpl(pattern));
		}
		return nodesToReuse;
	}

	protected abstract Set<NodeDeclNode> getNodesToReuseImpl(PatternGraphLhsNode pattern);


	protected static boolean sourceOrTargetNodeIncluded(EdgeDeclNode edge, PatternGraphLhsNode pattern, 
			Collection<? extends BaseNode> collection)
	{
		for(ConnectionCharacter connectionCharacter : pattern.getConnections()) {
			if(connectionCharacter instanceof ConnectionNode) {
				ConnectionNode connection = (ConnectionNode)connectionCharacter;
				if(connection.getEdge().equals(edge)) {
					if(collection.contains(connection.getSrc()) || collection.contains(connection.getTgt())) {
						return true;
					}
				}
			}
		}
		return false;
	}
}
