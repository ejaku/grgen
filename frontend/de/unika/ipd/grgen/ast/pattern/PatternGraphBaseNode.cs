/*
 * GrGen: graph rewrite generator tool -- release GrGen.NET 8.1
 * Copyright (C) 2003-2026 Universitaet Karlsruhe, Institut fuer Programmstrukturen und Datenorganisation, LS Goos; and free programmers
 * licensed under LGPL v3, some components/parts use different licenses (see LICENSE.txt included in the packaging of this file)
 * www.grgen.de / www.grgen.net
 */

/// <summary>
/// @author shack, Edgar Jakumeit
/// </summary>
namespace de.unika.ipd.grgen.ast.pattern
{
using System;
using System.Collections.Generic;
using System.Diagnostics;

using BasicTypeNode = de.unika.ipd.grgen.ast.type.basic.BasicTypeNode;
using ContainerTypeNode = de.unika.ipd.grgen.ast.type.container.ContainerTypeNode;
using BaseNode = de.unika.ipd.grgen.ast.BaseNode;
using de.unika.ipd.grgen.ast;
using DeclNode = de.unika.ipd.grgen.ast.decl.DeclNode;
using ConstraintDeclNode = de.unika.ipd.grgen.ast.decl.pattern.ConstraintDeclNode;
using DummyNodeDeclNode = de.unika.ipd.grgen.ast.decl.pattern.DummyNodeDeclNode;
using EdgeDeclNode = de.unika.ipd.grgen.ast.decl.pattern.EdgeDeclNode;
using NodeDeclNode = de.unika.ipd.grgen.ast.decl.pattern.NodeDeclNode;
using SubpatternUsageDeclNode = de.unika.ipd.grgen.ast.decl.pattern.SubpatternUsageDeclNode;
using VarDeclNode = de.unika.ipd.grgen.ast.decl.pattern.VarDeclNode;
using ExprNode = de.unika.ipd.grgen.ast.expr.ExprNode;
using EnumTypeNode = de.unika.ipd.grgen.ast.model.type.EnumTypeNode;
using ExternalObjectTypeNode = de.unika.ipd.grgen.ast.model.type.ExternalObjectTypeNode;
using InternalObjectTypeNode = de.unika.ipd.grgen.ast.model.type.InternalObjectTypeNode;
using InternalTransientObjectTypeNode = de.unika.ipd.grgen.ast.model.type.InternalTransientObjectTypeNode;
using de.unika.ipd.grgen.ast.util;
using de.unika.ipd.grgen.ast.util;
using de.unika.ipd.grgen.ast.util;
using Coords = de.unika.ipd.grgen.parser.Coords;

/// <summary>
/// AST node that represents a base graph pattern with nodes, edges, variables, subpattern usages, and further things
/// Serves as base class for lhs and rhs pattern graph nodes
/// </summary>
public abstract class PatternGraphBaseNode : BaseNode
{
	static PatternGraphBaseNode()
	{
		SetClassName(typeof(PatternGraphBaseNode), "pattern graph base");
	}

	protected internal CollectNode<BaseNode> connectionsUnresolved;
	protected internal CollectNode<ConnectionCharacter> connections = new CollectNode<ConnectionCharacter>();
	protected internal CollectNode<SubpatternUsageDeclNode> subpatterns;
	public CollectNode<ExprNode> returns;
	public CollectNode<BaseNode> @params;
	public CollectNode<VarDeclNode> defVariablesToBeYieldedTo;

	// Cache variables
	protected internal ISet<NodeDeclNode> nodes;
	protected internal ISet<EdgeDeclNode> edges;
	protected internal ISet<VarDeclNode> variables;
	protected internal ISet<DeclNode> entities;

	/// <summary>
	/// context(action or pattern, lhs not rhs) in which this node occurs </summary>
	protected internal int context = 0;

	internal PatternGraphLhsNode directlyNestingLHSGraph;

	public string nameOfGraph;

	/// <summary>
	/// A new pattern node </summary>
	/// <param name="connections"> A collection containing connection nodes </param>
	public PatternGraphBaseNode(string nameOfGraph, Coords coords,
			CollectNode<BaseNode> connections, CollectNode<BaseNode> @params,
			CollectNode<SubpatternUsageDeclNode> subpatterns, CollectNode<ExprNode> returns,
			int context)
		: base(coords)
	{
		this.nameOfGraph = nameOfGraph;
		this.connectionsUnresolved = connections;
		BecomeParent(this.connectionsUnresolved);
		this.subpatterns = subpatterns;
		BecomeParent(this.subpatterns);
		this.returns = returns;
		BecomeParent(this.returns);
		this.@params = @params;
		BecomeParent(this.@params);
		this.context = context;
	}

	public virtual void AddDefVariablesToBeYieldedTo(CollectNode<VarDeclNode> defVariablesToBeYieldedTo)
	{
		this.defVariablesToBeYieldedTo = defVariablesToBeYieldedTo;
	}

	/// <summary>
	/// returns children of this node </summary>
	public override ICollection<BaseNode> Children
	{
		get
		{
		IList<BaseNode> children = new List<BaseNode>();
		children.Add(GetValidVersionCollectNode(connectionsUnresolved, connections));
		children.Add(@params);
		children.Add(defVariablesToBeYieldedTo);
		children.Add(subpatterns);
		children.Add(returns);
		return children;
		}
	}

	/// <summary>
	/// returns names of the children, same order as in getChildren </summary>
	public override ICollection<string> ChildrenNames
	{
		get
		{
		IList<string> childrenNames = new List<string>();
		childrenNames.Add("connections");
		childrenNames.Add("params");
		childrenNames.Add("defVariablesToBeYieldedTo");
		childrenNames.Add("subpatterns");
		childrenNames.Add("orderedReplacements");
		childrenNames.Add("returns");
		return childrenNames;
		}
	}

	private static readonly CollectTripleResolver<ConnectionNode, SingleNodeConnNode, SingleGraphEntityNode> connectionsResolver =
			new CollectTripleResolver<ConnectionNode, SingleNodeConnNode, SingleGraphEntityNode>(
					new DeclarationTripleResolver<ConnectionNode, SingleNodeConnNode, SingleGraphEntityNode>(
							typeof(ConnectionNode), typeof(SingleNodeConnNode), typeof(SingleGraphEntityNode)));

	/// <seealso cref="de.unika.ipd.grgen.ast.BaseNode.resolveLocal() "/>
	protected internal override bool ResolveLocal()
	{
		Triple<CollectNode<ConnectionNode>, CollectNode<SingleNodeConnNode>, CollectNode<SingleGraphEntityNode>> resolve =
				connectionsResolver.Resolve(connectionsUnresolved);

		if(resolve != null)
		{
			if(resolve.first != null)
			{
				foreach(ConnectionNode conn in resolve.first.ChildrenExact)
				{
					if(!conn.Resolve())
						return false;
					connections.AddChild(conn);
				}
			}

			if(resolve.second != null)
			{
				foreach(SingleNodeConnNode conn in resolve.second.ChildrenExact)
				{
					if(!conn.Resolve())
						return false;
					connections.AddChild(conn);
				}
			}

			if(resolve.third != null)
			{
				foreach(SingleGraphEntityNode ent in resolve.third.ChildrenExact)
				{
					// resolve the entity
					if(!ent.Resolve())
						return false;

					// add reused single node to connections
					if(ent.EntityNode != null)
					{
						SingleNodeConnNode conn = new SingleNodeConnNode(ent.EntityNode);
						if(!conn.Resolve())
							return false;
						connections.AddChild(conn);
					}

					// add reused subpattern to subpatterns
					if(ent.EntitySubpattern != null)
						subpatterns.AddChild(ent.EntitySubpattern);
				}
			}

			BecomeParent(connections);
			BecomeParent(subpatterns);
		}

		bool paramsOK = ResolveParamVars();

		bool subUsagesOK = ResolveSubpatternUsages();

		return resolve != null && paramsOK && subUsagesOK;
	}

	private bool ResolveParamVars()
	{
		bool paramsOK = true;

		foreach(BaseNode param in @params.ChildrenExact)
		{
			if(!(param is VarDeclNode))
				continue;

			VarDeclNode paramVar = (VarDeclNode)param;
			if(paramVar.Resolve())
			{
				if(!(paramVar.DeclType is BasicTypeNode)
						&& !(paramVar.DeclType is EnumTypeNode)
						&& !(paramVar.DeclType is ContainerTypeNode)
						&& !(paramVar.DeclType is InternalObjectTypeNode)
						&& !(paramVar.DeclType is InternalTransientObjectTypeNode)
						&& !(paramVar.DeclType is ExternalObjectTypeNode))
				{
					paramVar.typeUnresolved.ReportError("The type of variable " + paramVar.Ident
							+ " must be a basic type (like int or string), or an enum, or a container type (set|map|array|deque), or an object type (class) "
							+ ("(but it is " + paramVar.DeclType.ToStringWithDeclarationCoords() + ")."));
					paramsOK = false;
				}
			}
			else
				paramsOK = false;
		}

		return paramsOK;
	}

	private bool ResolveSubpatternUsages()
	{
		bool subUsagesOK = true;

		if((context & CONTEXT_LHS_OR_RHS) == CONTEXT_RHS)
		{
			foreach(SubpatternUsageDeclNode subUsage in subpatterns.ChildrenExact)
			{
				if(subUsage.Resolve())
				{
					PatternGraphLhsNode pattern = subUsage.SubpatternDecl.Pattern;
					if(pattern.hasAbstractElements)
					{
						subUsage.ReportError("Cannot instantiate a pattern with abstract elements"
								+ " (attempted with " + subUsage.EmptyWhenAnonymous(subUsage.Ident + " of (sub)pattern type ") + pattern.ToStringWithDeclarationCoords() + ").");
						subUsagesOK = false;
					}
				}
				else
					subUsagesOK = false;
			}
		}

		return subUsagesOK;
	}

	//check, that each named edge is only used once in a pattern
	protected internal virtual bool IsEdgeReuseOk()
	{
		bool edgeUsage = true;
		HashSet<EdgeDeclNode> edges = new HashSet<EdgeDeclNode>();
		foreach(ConnectionCharacter connection in connections.ChildrenExact)
		{
			EdgeDeclNode edge = connection.Edge;

			// add() returns false iff edges already contains ec
			if(edge != null
					&& !(connection is ConnectionNode
							&& connection.Src is DummyNodeDeclNode
							&& connection.Tgt is DummyNodeDeclNode)
					&& !edges.Add(edge))
			{
				EdgeDeclNode edgeDecl = (EdgeDeclNode)edge;
				edgeDecl.ReportError("The edge " + edgeDecl.Ident + " is used more than once in a pattern graph of this action (" + nameOfGraph + ").");
				edgeUsage = false;
			}
		}
		return edgeUsage;
	}

	/// <summary>
	/// Get an iterator iterating over all connections characters in this pattern.
	/// These are the children of the collect node at position 0. </summary>
	/// <returns> The iterator. </returns>
	public virtual ICollection<ConnectionCharacter> Connections
	{
		get
		{
		Debug.Assert(IsResolved());

		return connections.ChildrenExact;
		}
	}

	/// <summary>
	/// Get a set of all nodes in this pattern.
	/// (Use this function after this node has been checked with <seealso cref="checkLocal()"/>
	/// to ensure, that the children have the right type.) </summary>
	/// <returns> A set containing the declarations of all nodes occurring
	/// in this graph pattern. </returns>
	public virtual ISet<NodeDeclNode> Nodes
	{
		get
		{
		if(nodes == null)
			nodes = Collections.UnmodifiableSet(NodesImpl);
		return nodes;
		}
	}

	protected internal virtual ISet<NodeDeclNode> NodesImpl
	{
		get
		{
		Debug.Assert(IsResolved());

		LinkedHashSet<NodeDeclNode> tempNodes = new LinkedHashSet<NodeDeclNode>();

		foreach(ConnectionCharacter connection in connections.ChildrenExact)
			connection.AddNodes(tempNodes);

		return tempNodes;
		}
	}

	/// <summary>
	/// Get a set of all edges in this pattern. </summary>
	public virtual ISet<EdgeDeclNode> Edges
	{
		get
		{
		if(edges == null)
			edges = Collections.UnmodifiableSet(EdgesImpl);
		return edges;
		}
	}

	protected internal virtual ISet<EdgeDeclNode> EdgesImpl
	{
		get
		{
		Debug.Assert(IsResolved());

		LinkedHashSet<EdgeDeclNode> tempEdges = new LinkedHashSet<EdgeDeclNode>();

		foreach(ConnectionCharacter connection in connections.ChildrenExact)
			connection.AddEdge(tempEdges);

		return tempEdges;
		}
	}

	public virtual CollectNode<VarDeclNode> DefVariablesToBeYieldedTo
	{
		get
		{
		return defVariablesToBeYieldedTo;
		}
	}

	/// <summary>
	/// Get a set of all variables in this pattern. </summary>
	public virtual ISet<VarDeclNode> Variables
	{
		get
		{
		if(variables == null)
			variables = Collections.UnmodifiableSet(VariablesImpl);
		return variables;
		}
	}

	protected internal virtual ISet<VarDeclNode> VariablesImpl
	{
		get
		{
		Debug.Assert(IsResolved());

		LinkedHashSet<VarDeclNode> tempVariables = new LinkedHashSet<VarDeclNode>();

		foreach(BaseNode param in @params.ChildrenExact)
		{
			if(param is VarDeclNode)
				tempVariables.Add((VarDeclNode)param);
		}

		foreach(VarDeclNode defVar in defVariablesToBeYieldedTo.ChildrenExact)
			tempVariables.Add(defVar);

		return tempVariables;
		}
	}

	public virtual ISet<DeclNode> Entities
	{
		get
		{
		if(entities == null)
		{
			LinkedHashSet<DeclNode> tempEntities = new LinkedHashSet<DeclNode>();
			tempEntities.AddAll(Nodes);
			tempEntities.AddAll(Edges);
			tempEntities.AddAll(Variables);
			entities = Collections.UnmodifiableSet(tempEntities);
		}
		return entities;
		}
	}

	protected internal virtual void AddParamsToConnections(CollectNode<BaseNode> @params)
	{
		foreach(BaseNode param in @params.ChildrenExact)
		{
			// directly nesting lhs pattern is null for parameters of lhs/rhs pattern
			// because it doesn't exist at the time the parameters are parsed -> patch it in here
			if(param is VarDeclNode)
			{
				((VarDeclNode)param).directlyNestingLHSGraph = directlyNestingLHSGraph;
				continue;
			}
			else if(param is SingleNodeConnNode)
			{
				SingleNodeConnNode sncn = (SingleNodeConnNode)param;
				((NodeDeclNode)sncn.nodeUnresolved).directlyNestingLHSGraph = directlyNestingLHSGraph;
			}
			else if(param is ConstraintDeclNode)
				((ConstraintDeclNode)param).directlyNestingLHSGraph = directlyNestingLHSGraph;
			else
			{ //if(param instanceof ConnectionNode)
				// don't need to adapt left/right nodes as only dummies
				ConnectionNode cn = (ConnectionNode)param;
				((EdgeDeclNode)cn.edgeUnresolved).directlyNestingLHSGraph = directlyNestingLHSGraph;
			}

			connectionsUnresolved.AddChild(param);
		}
	}

	public virtual IList<DeclNode> ParamDecls
	{
		get
		{
		IList<DeclNode> res = new List<DeclNode>();

		foreach(BaseNode param in @params.ChildrenExact)
		{
			if(param is ConnectionNode)
			{
				ConnectionNode conn = (ConnectionNode)param;
				res.Add(conn.Edge.Decl);
			}
			else if(param is SingleNodeConnNode)
			{
				NodeDeclNode node = ((SingleNodeConnNode)param).Node;
				res.Add(node);
			}
			else if(param is VarDeclNode)
				res.Add((VarDeclNode)param);
			else
				throw new System.NotSupportedException("Unsupported parameter (" + param + ").");
		}

		return res;
		}
	}

	public virtual ISet<string> NamesOfEntities
	{
		get
		{
		ISet<string> set = new HashSet<string>();
		foreach(DeclNode entity in Entities)
		{
			string name = entity.ident.ToString();
			if(!name.StartsWith("$", StringComparison.Ordinal))
				set.Add(name);
		}
		return set;
		}
	}
}

}
