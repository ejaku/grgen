/*
 * GrGen: graph rewrite generator tool -- release GrGen.NET 7.2
 * Copyright (C) 2003-2025 Universitaet Karlsruhe, Institut fuer Programmstrukturen und Datenorganisation, LS Goos; and free programmers
 * licensed under LGPL v3 (see LICENSE.txt included in the packaging of this file)
 * www.grgen.net
 */

/**
 * @author shack, Edgar Jakumeit
 */
package de.unika.ipd.grgen.ast.pattern;

import de.unika.ipd.grgen.ast.type.basic.BasicTypeNode;
import de.unika.ipd.grgen.ast.type.container.ContainerTypeNode;
import de.unika.ipd.grgen.ast.BaseNode;
import de.unika.ipd.grgen.ast.CollectNode;
import de.unika.ipd.grgen.ast.decl.DeclNode;
import de.unika.ipd.grgen.ast.decl.pattern.ConstraintDeclNode;
import de.unika.ipd.grgen.ast.decl.pattern.DummyNodeDeclNode;
import de.unika.ipd.grgen.ast.decl.pattern.EdgeDeclNode;
import de.unika.ipd.grgen.ast.decl.pattern.NodeDeclNode;
import de.unika.ipd.grgen.ast.decl.pattern.SubpatternUsageDeclNode;
import de.unika.ipd.grgen.ast.decl.pattern.VarDeclNode;
import de.unika.ipd.grgen.ast.expr.ExprNode;
import de.unika.ipd.grgen.ast.model.type.EnumTypeNode;
import de.unika.ipd.grgen.ast.model.type.ExternalObjectTypeNode;
import de.unika.ipd.grgen.ast.model.type.InternalObjectTypeNode;
import de.unika.ipd.grgen.ast.model.type.InternalTransientObjectTypeNode;
import de.unika.ipd.grgen.ast.util.CollectTripleResolver;
import de.unika.ipd.grgen.ast.util.DeclarationTripleResolver;
import de.unika.ipd.grgen.ast.util.Triple;
import de.unika.ipd.grgen.parser.Coords;
import java.util.Collection;
import java.util.Collections;
import java.util.HashSet;
import java.util.LinkedHashSet;
import java.util.Set;
import java.util.Vector;
import java.util.stream.Collectors;

/**
 * AST node that represents a base graph pattern with nodes, edges, variables, subpattern usages, and further things
 * Serves as base class for lhs and rhs pattern graph nodes
 */
public abstract class PatternGraphBaseNode extends BaseNode
{
	static {
		setName(PatternGraphBaseNode.class, "pattern graph base");
	}

	protected CollectNode<BaseNode> connectionsUnresolved;
	protected CollectNode<ConnectionCharacter> connections = new CollectNode<ConnectionCharacter>();
	protected CollectNode<SubpatternUsageDeclNode> subpatterns;
	public CollectNode<ExprNode> returns;
	public CollectNode<BaseNode> params;
	public CollectNode<VarDeclNode> defVariablesToBeYieldedTo;

	// Cache variables
	protected Set<NodeDeclNode> nodes;
	protected Set<EdgeDeclNode> edges;
	protected Set<VarDeclNode> variables;
	protected Set<DeclNode> entities;

	/** context(action or pattern, lhs not rhs) in which this node occurs*/
	protected int context = 0;

	PatternGraphLhsNode directlyNestingLHSGraph;

	public String nameOfGraph;

	/**
	 * A new pattern node
	 * @param connections A collection containing connection nodes
	 */
	public PatternGraphBaseNode(String nameOfGraph, Coords coords,
			CollectNode<BaseNode> connections, CollectNode<BaseNode> params,
			CollectNode<SubpatternUsageDeclNode> subpatterns, CollectNode<ExprNode> returns,
			int context)
	{
		super(coords);
		this.nameOfGraph = nameOfGraph;
		this.connectionsUnresolved = connections;
		becomeParent(this.connectionsUnresolved);
		this.subpatterns = subpatterns;
		becomeParent(this.subpatterns);
		this.returns = returns;
		becomeParent(this.returns);
		this.params = params;
		becomeParent(this.params);
		this.context = context;
	}

	public void addDefVariablesToBeYieldedTo(CollectNode<VarDeclNode> defVariablesToBeYieldedTo)
	{
		this.defVariablesToBeYieldedTo = defVariablesToBeYieldedTo;
	}

	/** returns children of this node */
	@Override
	public Collection<BaseNode> getChildren()
	{
		Vector<BaseNode> children = new Vector<BaseNode>();
		children.add(getValidVersion(connectionsUnresolved, connections));
		children.add(params);
		children.add(defVariablesToBeYieldedTo);
		children.add(subpatterns);
		children.add(returns);
		return children;
	}

	/** returns names of the children, same order as in getChildren */
	@Override
	public Collection<String> getChildrenNames()
	{
		Vector<String> childrenNames = new Vector<String>();
		childrenNames.add("connections");
		childrenNames.add("params");
		childrenNames.add("defVariablesToBeYieldedTo");
		childrenNames.add("subpatterns");
		childrenNames.add("orderedReplacements");
		childrenNames.add("returns");
		return childrenNames;
	}

	private static final CollectTripleResolver<ConnectionNode, SingleNodeConnNode, SingleGraphEntityNode> connectionsResolver =
			new CollectTripleResolver<ConnectionNode, SingleNodeConnNode, SingleGraphEntityNode>(
					new DeclarationTripleResolver<ConnectionNode, SingleNodeConnNode, SingleGraphEntityNode>(
							ConnectionNode.class, SingleNodeConnNode.class, SingleGraphEntityNode.class));

	/** @see de.unika.ipd.grgen.ast.BaseNode#resolveLocal() */
	@Override
	protected boolean resolveLocal()
	{
		Triple<CollectNode<ConnectionNode>, CollectNode<SingleNodeConnNode>, CollectNode<SingleGraphEntityNode>> resolve =
				connectionsResolver.resolve(connectionsUnresolved);

		if(resolve != null) {
			if(resolve.first != null) {
				for(ConnectionNode conn : resolve.first.getChildren()) {
					if(!conn.resolve())
						return false;
					connections.addChild(conn);
				}
			}

			if(resolve.second != null) {
				for(SingleNodeConnNode conn : resolve.second.getChildren()) {
					if(!conn.resolve())
						return false;
					connections.addChild(conn);
				}
			}

			if(resolve.third != null) {
				for(SingleGraphEntityNode ent : resolve.third.getChildren()) {
					// resolve the entity
					if(!ent.resolve()) {
						return false;
					}

					// add reused single node to connections
					if(ent.getEntityNode() != null) {
						SingleNodeConnNode conn = new SingleNodeConnNode(ent.getEntityNode());
						if(!conn.resolve())
							return false;
						connections.addChild(conn);
					}

					// add reused subpattern to subpatterns
					if(ent.getEntitySubpattern() != null) {
						subpatterns.addChild(ent.getEntitySubpattern());
					}
				}
			}

			becomeParent(connections);
			becomeParent(subpatterns);
		}

		boolean paramsOK = resolveParamVars();

		boolean subUsagesOK = resolveSubpatternUsages();

		return resolve != null && paramsOK && subUsagesOK;
	}

	private boolean resolveParamVars()
	{
		boolean paramsOK = true;

		for(BaseNode param : params.getChildren()) {
			if(!(param instanceof VarDeclNode))
				continue;

			VarDeclNode paramVar = (VarDeclNode)param;
			if(paramVar.resolve()) {
				if(!(paramVar.getDeclType() instanceof BasicTypeNode)
						&& !(paramVar.getDeclType() instanceof EnumTypeNode)
						&& !(paramVar.getDeclType() instanceof ContainerTypeNode)
						&& !(paramVar.getDeclType() instanceof InternalObjectTypeNode)
						&& !(paramVar.getDeclType() instanceof InternalTransientObjectTypeNode)
						&& !(paramVar.getDeclType() instanceof ExternalObjectTypeNode)) {
					paramVar.typeUnresolved.reportError("The type of variable " + paramVar.getIdentNode()
							+ " must be a basic type (like int or string), or an enum, or a container type (set|map|array|deque), or an object type (class) "
							+ ("(but it is " + paramVar.getDeclType().toStringWithDeclarationCoords() + ")."));
					paramsOK = false;
				}
			} else
				paramsOK = false;
		}
		
		return paramsOK;
	}

	private boolean resolveSubpatternUsages()
	{
		boolean subUsagesOK = true;
		
		if((context & CONTEXT_LHS_OR_RHS) == CONTEXT_RHS) {
			for(SubpatternUsageDeclNode subUsage : subpatterns.getChildren()) {
				if(subUsage.resolve()) {
					PatternGraphLhsNode pattern = subUsage.getSubpatternDeclNode().getPattern();
					if(pattern.hasAbstractElements) {
						subUsage.reportError("Cannot instantiate a pattern with abstract elements"
								+ " (attempted with " + subUsage.emptyWhenAnonymous(subUsage.getIdentNode() + " of (sub)pattern type ") + pattern.toStringWithDeclarationCoords() + ").");
						subUsagesOK = false;
					}
				} else
					subUsagesOK = false;
			}
		}
		
		return subUsagesOK;
	}

	//check, that each named edge is only used once in a pattern
	protected boolean isEdgeReuseOk()
	{
		boolean edgeUsage = true;
		HashSet<EdgeDeclNode> edges = new HashSet<EdgeDeclNode>();
		for(ConnectionCharacter connection : connections.getChildren()) {
			EdgeDeclNode edge = connection.getEdge();

			// add() returns false iff edges already contains ec
			if(edge != null
					&& !(connection instanceof ConnectionNode
							&& connection.getSrc() instanceof DummyNodeDeclNode
							&& connection.getTgt() instanceof DummyNodeDeclNode)
					&& !edges.add(edge)) {
				EdgeDeclNode edgeDecl = (EdgeDeclNode)edge;
				edgeDecl.reportError("The edge " + edgeDecl.getIdentNode() + " is used more than once in a pattern graph of this action (" + nameOfGraph +").");
				edgeUsage = false;
			}
		}
		return edgeUsage;
	}

	/**
	 * Get an iterator iterating over all connections characters in this pattern.
	 * These are the children of the collect node at position 0.
	 * @return The iterator.
	 */
	public Collection<ConnectionCharacter> getConnections()
	{
		assert isResolved();

		return connections.getChildren();
	}

	/**
	 * Get a set of all nodes in this pattern.
	 * (Use this function after this node has been checked with {@link #checkLocal()}
	 * to ensure, that the children have the right type.)
	 * @return A set containing the declarations of all nodes occurring
	 * in this graph pattern.
	 */
	public Set<NodeDeclNode> getNodes()
	{
		if(nodes == null) {
			nodes = Collections.unmodifiableSet(getNodesImpl());
		}
		return nodes;
	}

	protected Set<NodeDeclNode> getNodesImpl()
	{
		assert isResolved();

		LinkedHashSet<NodeDeclNode> tempNodes = new LinkedHashSet<NodeDeclNode>();

		for(ConnectionCharacter connection : connections.getChildren()) {
			connection.addNodes(tempNodes);
		}

		return tempNodes;
	}

	/** Get a set of all edges in this pattern. */
	public Set<EdgeDeclNode> getEdges()
	{
		if(edges == null) {
			edges = Collections.unmodifiableSet(getEdgesImpl());
		}
		return edges;
	}

	protected Set<EdgeDeclNode> getEdgesImpl()
	{
		assert isResolved();

		LinkedHashSet<EdgeDeclNode> tempEdges = new LinkedHashSet<EdgeDeclNode>();

		for(ConnectionCharacter connection : connections.getChildren()) {
			connection.addEdge(tempEdges);
		}

		return tempEdges;
	}

	public CollectNode<VarDeclNode> getDefVariablesToBeYieldedTo()
	{
		return defVariablesToBeYieldedTo;
	}

	/** Get a set of all variables in this pattern. */
	public Set<VarDeclNode> getVariables()
	{
		if(variables == null) {
			variables = Collections.unmodifiableSet(getVariablesImpl());
		}
		return variables;
	}

	protected Set<VarDeclNode> getVariablesImpl()
	{
		assert isResolved();

		LinkedHashSet<VarDeclNode> tempVariables = new LinkedHashSet<VarDeclNode>();

		for(BaseNode param : params.getChildren()) {
			if(param instanceof VarDeclNode) {
				tempVariables.add((VarDeclNode)param);
			}
		}
		
		for(VarDeclNode defVar : defVariablesToBeYieldedTo.getChildren()) {
			tempVariables.add(defVar);
		}

		return tempVariables;
	}

	public Set<DeclNode> getEntities()
	{
		if(entities == null) {
			LinkedHashSet<DeclNode> tempEntities = new LinkedHashSet<DeclNode>();
			tempEntities.addAll(getNodes());
			tempEntities.addAll(getEdges());
			tempEntities.addAll(getVariables());
			entities = Collections.unmodifiableSet(tempEntities);
		}
		return entities;
	}

	protected void addParamsToConnections(CollectNode<BaseNode> params)
	{
		for(BaseNode param : params.getChildren()) {
			// directly nesting lhs pattern is null for parameters of lhs/rhs pattern
			// because it doesn't exist at the time the parameters are parsed -> patch it in here
			if(param instanceof VarDeclNode) {
				((VarDeclNode)param).directlyNestingLHSGraph = directlyNestingLHSGraph;
				continue;
			} else if(param instanceof SingleNodeConnNode) {
				SingleNodeConnNode sncn = (SingleNodeConnNode)param;
				((NodeDeclNode)sncn.nodeUnresolved).directlyNestingLHSGraph = directlyNestingLHSGraph;
			} else if(param instanceof ConstraintDeclNode) {
				((ConstraintDeclNode)param).directlyNestingLHSGraph = directlyNestingLHSGraph;
			} else { //if(param instanceof ConnectionNode)
				// don't need to adapt left/right nodes as only dummies
				ConnectionNode cn = (ConnectionNode)param;
				((EdgeDeclNode)cn.edgeUnresolved).directlyNestingLHSGraph = directlyNestingLHSGraph;
			}

			connectionsUnresolved.addChild(param);
		}
	}

	public Vector<DeclNode> getParamDecls()
	{
		Vector<DeclNode> res = new Vector<DeclNode>();

		for(BaseNode param : params.getChildren()) {
			if(param instanceof ConnectionNode) {
				ConnectionNode conn = (ConnectionNode)param;
				res.add(conn.getEdge().getDecl());
			} else if(param instanceof SingleNodeConnNode) {
				NodeDeclNode node = ((SingleNodeConnNode)param).getNode();
				res.add(node);
			} else if(param instanceof VarDeclNode) {
				res.add((VarDeclNode)param);
			} else
				throw new UnsupportedOperationException("Unsupported parameter (" + param + ").");
		}

		return res;
	}
	
	public Set<String> getNamesOfEntities()
	{
		return getEntities().stream()
				.map((DeclNode entity) -> entity.ident.toString())
				.filter((String name) -> !name.startsWith("$"))
				.collect(Collectors.toSet());
	}
}
