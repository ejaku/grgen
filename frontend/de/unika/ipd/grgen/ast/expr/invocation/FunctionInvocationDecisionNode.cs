/*
 * GrGen: graph rewrite generator tool -- release GrGen.NET 8.1
 * Copyright (C) 2003-2026 Universitaet Karlsruhe, Institut fuer Programmstrukturen und Datenorganisation, LS Goos; and free programmers
 * licensed under LGPL v3, some components/parts use different licenses (see LICENSE.txt included in the packaging of this file)
 * www.grgen.de / www.grgen.net
 */

/// <summary>
/// @author Edgar Jakumeit
/// </summary>

namespace de.unika.ipd.grgen.ast.expr.invocation
{

using System.Collections.Generic;

using de.unika.ipd.grgen.ast;
using BuiltinFunctionInvocationBaseNode = de.unika.ipd.grgen.ast.expr.BuiltinFunctionInvocationBaseNode;
using CopyExprNode = de.unika.ipd.grgen.ast.expr.CopyExprNode;
using ExprNode = de.unika.ipd.grgen.ast.expr.ExprNode;
using IdentExprNode = de.unika.ipd.grgen.ast.expr.IdentExprNode;
using AdjacentNodeExprNode = de.unika.ipd.grgen.ast.expr.graph.AdjacentNodeExprNode;
using BoundedReachableEdgeExprNode = de.unika.ipd.grgen.ast.expr.graph.BoundedReachableEdgeExprNode;
using BoundedReachableNodeExprNode = de.unika.ipd.grgen.ast.expr.graph.BoundedReachableNodeExprNode;
using BoundedReachableNodeWithRemainingDepthExprNode = de.unika.ipd.grgen.ast.expr.graph.BoundedReachableNodeWithRemainingDepthExprNode;
using CanonizeExprNode = de.unika.ipd.grgen.ast.expr.graph.CanonizeExprNode;
using CountAdjacentNodeExprNode = de.unika.ipd.grgen.ast.expr.graph.CountAdjacentNodeExprNode;
using CountBoundedReachableEdgeExprNode = de.unika.ipd.grgen.ast.expr.graph.CountBoundedReachableEdgeExprNode;
using CountBoundedReachableNodeExprNode = de.unika.ipd.grgen.ast.expr.graph.CountBoundedReachableNodeExprNode;
using CountEdgesExprNode = de.unika.ipd.grgen.ast.expr.graph.CountEdgesExprNode;
using CountIncidentEdgeExprNode = de.unika.ipd.grgen.ast.expr.graph.CountIncidentEdgeExprNode;
using CountNodesExprNode = de.unika.ipd.grgen.ast.expr.graph.CountNodesExprNode;
using CountReachableEdgeExprNode = de.unika.ipd.grgen.ast.expr.graph.CountReachableEdgeExprNode;
using CountReachableNodeExprNode = de.unika.ipd.grgen.ast.expr.graph.CountReachableNodeExprNode;
using DefinedSubgraphExprNode = de.unika.ipd.grgen.ast.expr.graph.DefinedSubgraphExprNode;
using EdgeByNameExprNode = de.unika.ipd.grgen.ast.expr.graph.EdgeByNameExprNode;
using EdgeByUniqueExprNode = de.unika.ipd.grgen.ast.expr.graph.EdgeByUniqueExprNode;
using EdgesExprNode = de.unika.ipd.grgen.ast.expr.graph.EdgesExprNode;
using EmptyExprNode = de.unika.ipd.grgen.ast.expr.graph.EmptyExprNode;
using EqualsAnyExprNode = de.unika.ipd.grgen.ast.expr.graph.EqualsAnyExprNode;
using GetEquivalentExprNode = de.unika.ipd.grgen.ast.expr.graph.GetEquivalentExprNode;
using GraphofExprNode = de.unika.ipd.grgen.ast.expr.graph.GraphofExprNode;
using IncidentEdgeExprNode = de.unika.ipd.grgen.ast.expr.graph.IncidentEdgeExprNode;
using InducedSubgraphExprNode = de.unika.ipd.grgen.ast.expr.graph.InducedSubgraphExprNode;
using IsAdjacentNodeExprNode = de.unika.ipd.grgen.ast.expr.graph.IsAdjacentNodeExprNode;
using IsBoundedReachableEdgeExprNode = de.unika.ipd.grgen.ast.expr.graph.IsBoundedReachableEdgeExprNode;
using IsBoundedReachableNodeExprNode = de.unika.ipd.grgen.ast.expr.graph.IsBoundedReachableNodeExprNode;
using IsIncidentEdgeExprNode = de.unika.ipd.grgen.ast.expr.graph.IsIncidentEdgeExprNode;
using IsReachableEdgeExprNode = de.unika.ipd.grgen.ast.expr.graph.IsReachableEdgeExprNode;
using IsReachableNodeExprNode = de.unika.ipd.grgen.ast.expr.graph.IsReachableNodeExprNode;
using NodeByNameExprNode = de.unika.ipd.grgen.ast.expr.graph.NodeByNameExprNode;
using NodeByUniqueExprNode = de.unika.ipd.grgen.ast.expr.graph.NodeByUniqueExprNode;
using NodesExprNode = de.unika.ipd.grgen.ast.expr.graph.NodesExprNode;
using OppositeExprNode = de.unika.ipd.grgen.ast.expr.graph.OppositeExprNode;
using ReachableEdgeExprNode = de.unika.ipd.grgen.ast.expr.graph.ReachableEdgeExprNode;
using ReachableNodeExprNode = de.unika.ipd.grgen.ast.expr.graph.ReachableNodeExprNode;
using SizeExprNode = de.unika.ipd.grgen.ast.expr.graph.SizeExprNode;
using SourceExprNode = de.unika.ipd.grgen.ast.expr.graph.SourceExprNode;
using TargetExprNode = de.unika.ipd.grgen.ast.expr.graph.TargetExprNode;
using UniqueofExprNode = de.unika.ipd.grgen.ast.expr.graph.UniqueofExprNode;
using RandomNode = de.unika.ipd.grgen.ast.expr.procenv.RandomNode;
using TypeNode = de.unika.ipd.grgen.ast.type.TypeNode;
using FunctionTypeNode = de.unika.ipd.grgen.ast.type.executable.FunctionTypeNode;
using ResolvingEnvironment = de.unika.ipd.grgen.ast.util.ResolvingEnvironment;
using IR = de.unika.ipd.grgen.ir.IR;
using ParserEnvironment = de.unika.ipd.grgen.parser.ParserEnvironment;
using Direction = de.unika.ipd.grgen.util.Direction;

public class FunctionInvocationDecisionNode : FunctionInvocationBaseNode
{
	static FunctionInvocationDecisionNode()
	{
		SetClassName(typeof(FunctionInvocationDecisionNode), "function invocation decision expression");
	}

	internal static TypeNode functionTypeNode = new FunctionTypeNode();

	public IdentNode functionIdent;
	private BuiltinFunctionInvocationBaseNode result;

	internal ParserEnvironment env;

	public FunctionInvocationDecisionNode(IdentNode functionIdent,
			CollectNode<ExprNode> arguments, ParserEnvironment env)
		: base(functionIdent.Coords, arguments)
	{
		this.functionIdent = BecomeParent(functionIdent);
		this.env = env;
	}

	public override ICollection<BaseNode> Children
	{
		get
		{
			IList<BaseNode> children = new List<BaseNode>();
			//children.add(methodIdent);	// HACK: We don't have a declaration, so avoid failure during check phase
			children.Add(arguments);
			if(IsResolved())
				children.Add(result);
			return children;
		}
	}

	public override ICollection<string> ChildrenNames
	{
		get
		{
			IList<string> childrenNames = new List<string>();
			//childrenNames.add("methodIdent");
			childrenNames.Add("params");
			if(IsResolved())
				childrenNames.Add("result");
			return childrenNames;
		}
	}

	protected internal override bool ResolveLocal()
	{
		ResolvingEnvironment resolvingEnvironment = new ResolvingEnvironment(env, error, Coords);
		result = Decide(functionIdent.ToString(), arguments, resolvingEnvironment);
		return result != null;
	}

	private static BuiltinFunctionInvocationBaseNode Decide(string functionName, CollectNode<ExprNode> arguments,
			ResolvingEnvironment env)
	{
		switch(functionName)
		{
		case "random":
			if(arguments.Size() == 1)
				return new RandomNode(env.Coords, arguments.Get(0));
			else if(arguments.Size() == 0)
				return new RandomNode(env.Coords, null);
			else
			{
				env.ReportError("random() expects 1 or 0 arguments (given are " + arguments.Size() + " arguments).");
				return null;
			}
		case "nodes":
			if(arguments.Size() > 1)
			{
				env.ReportError("nodes() expects 1 or 0 arguments (given are " + arguments.Size() + " arguments).");
				return null;
			}
			else
			{
				return new NodesExprNode(env.Coords,
						arguments.Size() == 1 ? arguments.Get(0) : new IdentExprNode(env.ParserEnvironment.NodeRoot));
			}
		case "edges":
			if(arguments.Size() > 1)
			{
				env.ReportError("edges() expects 1 or 0 arguments (given are " + arguments.Size() + " arguments).");
				return null;
			}
			else
			{
				return new EdgesExprNode(env.Coords,
						arguments.Size() == 1 ? arguments.Get(0) : new IdentExprNode(env.ParserEnvironment.ArbitraryEdgeRoot));
			}
		case "countNodes":
			if(arguments.Size() > 1)
			{
				env.ReportError("countNodes() expects 1 or 0 arguments (given are " + arguments.Size() + " arguments).");
				return null;
			}
			else
			{
				return new CountNodesExprNode(env.Coords,
						arguments.Size() == 1 ? arguments.Get(0) : new IdentExprNode(env.ParserEnvironment.NodeRoot));
			}
		case "countEdges":
			if(arguments.Size() > 1)
			{
				env.ReportError("countEdges() expects 1 or 0 arguments (given are " + arguments.Size() + " arguments).");
				return null;
			}
			else
			{
				return new CountEdgesExprNode(env.Coords,
						arguments.Size() == 1 ? arguments.Get(0) : new IdentExprNode(env.ParserEnvironment.ArbitraryEdgeRoot));
			}
		case "empty":
			if(arguments.Size() > 0)
			{
				env.ReportError("empty() expects 0 arguments (given are " + arguments.Size() + " arguments).");
				return null;
			}
			else
				return new EmptyExprNode(env.Coords);
		case "size":
			if(arguments.Size() > 0)
			{
				env.ReportError("size() expects 0 arguments (given are " + arguments.Size() + " arguments).");
				return null;
			}
			else
				return new SizeExprNode(env.Coords);
		case "source":
			if(arguments.Size() == 1)
				return new SourceExprNode(env.Coords, arguments.Get(0), env.ParserEnvironment.NodeRoot);
			else
			{
				env.ReportError(functionName + "() expects 1 arguments (given are " + arguments.Size() + " arguments).");
				return null;
			}
		case "target":
			if(arguments.Size() == 1)
				return new TargetExprNode(env.Coords, arguments.Get(0), env.ParserEnvironment.NodeRoot);
			else
			{
				env.ReportError(functionName + "() expects 1 arguments (given are " + arguments.Size() + " arguments).");
				return null;
			}
		case "opposite":
			if(arguments.Size() == 2)
				return new OppositeExprNode(env.Coords, arguments.Get(0), arguments.Get(1), env.ParserEnvironment.NodeRoot);
			else
			{
				env.ReportError(functionName + "() expects 2 arguments (given are " + arguments.Size() + " arguments).");
				return null;
			}
		case "nodeByName":
			if(arguments.Size() >= 1 && arguments.Size() <= 2)
			{
				return new NodeByNameExprNode(env.Coords, arguments.Get(0),
						arguments.Size() == 2 ? arguments.Get(1) : new IdentExprNode(env.ParserEnvironment.NodeRoot));
			}
			else
			{
				env.ReportError(functionName + "() expects 1 or 2 arguments (given are " + arguments.Size() + " arguments).");
				return null;
			}
		case "edgeByName":
			if(arguments.Size() >= 1 && arguments.Size() <= 2)
			{
				return new EdgeByNameExprNode(env.Coords, arguments.Get(0),
						arguments.Size() == 2 ? arguments.Get(1) : new IdentExprNode(env.ParserEnvironment.ArbitraryEdgeRoot));
			}
			else
			{
				env.ReportError(functionName + "() expects 1 or 2 arguments (given are " + arguments.Size() + " arguments).");
				return null;
			}
		case "nodeByUnique":
			if(arguments.Size() >= 1 && arguments.Size() <= 2)
			{
				return new NodeByUniqueExprNode(env.Coords, arguments.Get(0),
						arguments.Size() == 2 ? arguments.Get(1) : new IdentExprNode(env.ParserEnvironment.NodeRoot));
			}
			else
			{
				env.ReportError(functionName + "() expects 1 or 2 arguments (given are " + arguments.Size() + " arguments).");
				return null;
			}
		case "edgeByUnique":
			if(arguments.Size() >= 1 && arguments.Size() <= 2)
			{
				return new EdgeByUniqueExprNode(env.Coords, arguments.Get(0),
						arguments.Size() == 2 ? arguments.Get(1) : new IdentExprNode(env.ParserEnvironment.ArbitraryEdgeRoot));
			}
			else
			{
				env.ReportError(functionName + "() expects 1 or 2 arguments (given are " + arguments.Size() + " arguments).");
				return null;
			}
		case "incoming":
		case "outgoing":
		case "incident":
		{
			Direction direction = GetDirection(functionName);
			if(arguments.Size() == 1)
			{
				return new IncidentEdgeExprNode(env.Coords, arguments.Get(0),
						new IdentExprNode(env.ParserEnvironment.ArbitraryEdgeRoot), direction, new IdentExprNode(env.ParserEnvironment.NodeRoot));
			}
			else if(arguments.Size() == 2)
			{
				return new IncidentEdgeExprNode(env.Coords, arguments.Get(0), arguments.Get(1), direction,
						new IdentExprNode(env.ParserEnvironment.NodeRoot));
			}
			else if(arguments.Size() == 3)
				return new IncidentEdgeExprNode(env.Coords, arguments.Get(0), arguments.Get(1), direction, arguments.Get(2));
			else
			{
				env.ReportError(functionName + "() expects 1-3 arguments (given are " + arguments.Size() + " arguments).");
				return null;
			}
		}
			goto case "adjacentIncoming";
		case "adjacentIncoming":
		case "adjacentOutgoing":
		case "adjacent":
		{
			Direction direction = GetDirection(functionName);
			if(arguments.Size() == 1)
			{
				return new AdjacentNodeExprNode(env.Coords, arguments.Get(0),
						new IdentExprNode(env.ParserEnvironment.ArbitraryEdgeRoot), direction, new IdentExprNode(env.ParserEnvironment.NodeRoot));
			}
			else if(arguments.Size() == 2)
			{
				return new AdjacentNodeExprNode(env.Coords, arguments.Get(0), arguments.Get(1), direction,
						new IdentExprNode(env.ParserEnvironment.NodeRoot));
			}
			else if(arguments.Size() == 3)
				return new AdjacentNodeExprNode(env.Coords, arguments.Get(0), arguments.Get(1), direction, arguments.Get(2));
			else
			{
				env.ReportError(functionName + "() expects 1-3 arguments (given are " + arguments.Size() + " arguments).");
				return null;
			}
		}
			goto case "countIncoming";
		case "countIncoming":
		case "countOutgoing":
		case "countIncident":
		{
			Direction direction = GetDirection(functionName);
			if(arguments.Size() == 1)
			{
				return new CountIncidentEdgeExprNode(env.Coords, arguments.Get(0),
						new IdentExprNode(env.ParserEnvironment.ArbitraryEdgeRoot), direction, new IdentExprNode(env.ParserEnvironment.NodeRoot));
			}
			else if(arguments.Size() == 2)
			{
				return new CountIncidentEdgeExprNode(env.Coords, arguments.Get(0), arguments.Get(1), direction,
						new IdentExprNode(env.ParserEnvironment.NodeRoot));
			}
			else if(arguments.Size() == 3)
			{
				return new CountIncidentEdgeExprNode(env.Coords, arguments.Get(0), arguments.Get(1), direction,
						arguments.Get(2));
			}
			else
			{
				env.ReportError(functionName + "() expects 1-3 arguments (given are " + arguments.Size() + " arguments).");
				return null;
			}
		}
			goto case "countAdjacentIncoming";
		case "countAdjacentIncoming":
		case "countAdjacentOutgoing":
		case "countAdjacent":
		{
			Direction direction = GetDirection(functionName);
			if(arguments.Size() == 1)
			{
				return new CountAdjacentNodeExprNode(env.Coords, arguments.Get(0),
						new IdentExprNode(env.ParserEnvironment.ArbitraryEdgeRoot), direction, new IdentExprNode(env.ParserEnvironment.NodeRoot));
			}
			else if(arguments.Size() == 2)
			{
				return new CountAdjacentNodeExprNode(env.Coords, arguments.Get(0), arguments.Get(1), direction,
						new IdentExprNode(env.ParserEnvironment.NodeRoot));
			}
			else if(arguments.Size() == 3)
			{
				return new CountAdjacentNodeExprNode(env.Coords, arguments.Get(0), arguments.Get(1), direction,
						arguments.Get(2));
			}
			else
			{
				env.ReportError(functionName + "() expects 1-3 arguments (given are " + arguments.Size() + " arguments).");
				return null;
			}
		}
			goto case "isIncoming";
		case "isIncoming":
		case "isOutgoing":
		case "isIncident":
		{
			Direction direction = GetDirection(functionName);
			if(arguments.Size() == 2)
			{
				return new IsIncidentEdgeExprNode(env.Coords, arguments.Get(0), arguments.Get(1),
						new IdentExprNode(env.ParserEnvironment.ArbitraryEdgeRoot), direction, new IdentExprNode(env.ParserEnvironment.NodeRoot));
			}
			else if(arguments.Size() == 3)
			{
				return new IsIncidentEdgeExprNode(env.Coords, arguments.Get(0), arguments.Get(1), arguments.Get(2), direction,
						new IdentExprNode(env.ParserEnvironment.NodeRoot));
			}
			else if(arguments.Size() == 4)
			{
				return new IsIncidentEdgeExprNode(env.Coords, arguments.Get(0), arguments.Get(1), arguments.Get(2), direction,
						arguments.Get(3));
			}
			else
			{
				env.ReportError(functionName + "() expects 2-4 arguments (given are " + arguments.Size() + " arguments).");
				return null;
			}
		}
			goto case "isAdjacentIncoming";
		case "isAdjacentIncoming":
		case "isAdjacentOutgoing":
		case "isAdjacent":
		{
			Direction direction = GetDirection(functionName);
			if(arguments.Size() == 2)
			{
				return new IsAdjacentNodeExprNode(env.Coords, arguments.Get(0), arguments.Get(1),
						new IdentExprNode(env.ParserEnvironment.ArbitraryEdgeRoot), direction, new IdentExprNode(env.ParserEnvironment.NodeRoot));
			}
			else if(arguments.Size() == 3)
			{
				return new IsAdjacentNodeExprNode(env.Coords, arguments.Get(0), arguments.Get(1), arguments.Get(2), direction,
						new IdentExprNode(env.ParserEnvironment.NodeRoot));
			}
			else if(arguments.Size() == 4)
			{
				return new IsAdjacentNodeExprNode(env.Coords, arguments.Get(0), arguments.Get(1), arguments.Get(2), direction,
						arguments.Get(3));
			}
			else
			{
				env.ReportError(functionName + "() expects 2-4 arguments (given are " + arguments.Size() + " arguments).");
				return null;
			}
		}
			goto case "reachableEdgesIncoming";
		case "reachableEdgesIncoming":
		case "reachableEdgesOutgoing":
		case "reachableEdges":
		{
			Direction direction = GetDirection(functionName);
			if(arguments.Size() == 1)
			{
				return new ReachableEdgeExprNode(env.Coords, arguments.Get(0),
						new IdentExprNode(env.ParserEnvironment.ArbitraryEdgeRoot), direction, new IdentExprNode(env.ParserEnvironment.NodeRoot));
			}
			else if(arguments.Size() == 2)
			{
				return new ReachableEdgeExprNode(env.Coords, arguments.Get(0), arguments.Get(1), direction,
						new IdentExprNode(env.ParserEnvironment.NodeRoot));
			}
			else if(arguments.Size() == 3)
				return new ReachableEdgeExprNode(env.Coords, arguments.Get(0), arguments.Get(1), direction, arguments.Get(2));
			else
			{
				env.ReportError(functionName + "() expects 1-3 arguments (given are " + arguments.Size() + " arguments).");
				return null;
			}
		}
			goto case "reachableIncoming";
		case "reachableIncoming":
		case "reachableOutgoing":
		case "reachable":
		{
			Direction direction = GetDirection(functionName);
			if(arguments.Size() == 1)
			{
				return new ReachableNodeExprNode(env.Coords, arguments.Get(0),
						new IdentExprNode(env.ParserEnvironment.ArbitraryEdgeRoot), direction, new IdentExprNode(env.ParserEnvironment.NodeRoot));
			}
			else if(arguments.Size() == 2)
			{
				return new ReachableNodeExprNode(env.Coords, arguments.Get(0), arguments.Get(1), direction,
						new IdentExprNode(env.ParserEnvironment.NodeRoot));
			}
			else if(arguments.Size() == 3)
				return new ReachableNodeExprNode(env.Coords, arguments.Get(0), arguments.Get(1), direction, arguments.Get(2));
			else
			{
				env.ReportError(functionName + "() expects 1-3 arguments (given are " + arguments.Size() + " arguments).");
				return null;
			}
		}
			goto case "countReachableEdgesIncoming";
		case "countReachableEdgesIncoming":
		case "countReachableEdgesOutgoing":
		case "countReachableEdges":
		{
			Direction direction = GetDirection(functionName);
			if(arguments.Size() == 1)
			{
				return new CountReachableEdgeExprNode(env.Coords, arguments.Get(0),
					new IdentExprNode(env.ParserEnvironment.ArbitraryEdgeRoot), direction, new IdentExprNode(env.ParserEnvironment.NodeRoot));
			}
			else if(arguments.Size() == 2)
			{
				return new CountReachableEdgeExprNode(env.Coords, arguments.Get(0), arguments.Get(1), direction,
					new IdentExprNode(env.ParserEnvironment.NodeRoot));
			}
			else if(arguments.Size() == 3)
			{
				return new CountReachableEdgeExprNode(env.Coords, arguments.Get(0), arguments.Get(1), direction,
					arguments.Get(2));
			}
			else
			{
				env.ReportError(functionName + "() expects 1-3 arguments (given are " + arguments.Size() + " arguments).");
				return null;
			}
		}
			goto case "countReachableIncoming";
		case "countReachableIncoming":
		case "countReachableOutgoing":
		case "countReachable":
		{
			Direction direction = GetDirection(functionName);
			if(arguments.Size() == 1)
			{
				return new CountReachableNodeExprNode(env.Coords, arguments.Get(0),
						new IdentExprNode(env.ParserEnvironment.ArbitraryEdgeRoot), direction, new IdentExprNode(env.ParserEnvironment.NodeRoot));
			}
			else if(arguments.Size() == 2)
			{
				return new CountReachableNodeExprNode(env.Coords, arguments.Get(0), arguments.Get(1), direction,
						new IdentExprNode(env.ParserEnvironment.NodeRoot));
			}
			else if(arguments.Size() == 3)
			{
				return new CountReachableNodeExprNode(env.Coords, arguments.Get(0), arguments.Get(1), direction,
						arguments.Get(2));
			}
			else
			{
				env.ReportError(functionName + "() expects 1-3 arguments (given are " + arguments.Size() + " arguments).");
				return null;
			}
		}
			goto case "isReachableIncoming";
		case "isReachableIncoming":
		case "isReachableOutgoing":
		case "isReachable":
		{
			Direction direction = GetDirection(functionName);
			if(arguments.Size() == 2)
			{
				return new IsReachableNodeExprNode(env.Coords, arguments.Get(0), arguments.Get(1),
						new IdentExprNode(env.ParserEnvironment.ArbitraryEdgeRoot), direction, new IdentExprNode(env.ParserEnvironment.NodeRoot));
			}
			else if(arguments.Size() == 3)
			{
				return new IsReachableNodeExprNode(env.Coords, arguments.Get(0), arguments.Get(1), arguments.Get(2),
						direction, new IdentExprNode(env.ParserEnvironment.NodeRoot));
			}
			else if(arguments.Size() == 4)
			{
				return new IsReachableNodeExprNode(env.Coords, arguments.Get(0), arguments.Get(1), arguments.Get(2),
						direction, arguments.Get(3));
			}
			else
			{
				env.ReportError(functionName + "() expects 2-4 arguments (given are " + arguments.Size() + " arguments).");
				return null;
			}
		}
			goto case "isReachableEdgesIncoming";
		case "isReachableEdgesIncoming":
		case "isReachableEdgesOutgoing":
		case "isReachableEdges":
		{
			Direction direction = GetDirection(functionName);
			if(arguments.Size() == 2)
			{
				return new IsReachableEdgeExprNode(env.Coords, arguments.Get(0), arguments.Get(1),
						new IdentExprNode(env.ParserEnvironment.ArbitraryEdgeRoot), direction, new IdentExprNode(env.ParserEnvironment.NodeRoot));
			}
			else if(arguments.Size() == 3)
			{
				return new IsReachableEdgeExprNode(env.Coords, arguments.Get(0), arguments.Get(1), arguments.Get(2),
						direction, new IdentExprNode(env.ParserEnvironment.NodeRoot));
			}
			else if(arguments.Size() == 4)
			{
				return new IsReachableEdgeExprNode(env.Coords, arguments.Get(0), arguments.Get(1), arguments.Get(2),
						direction, arguments.Get(3));
			}
			else
			{
				env.ReportError(functionName + "() expects 2-4 arguments (given are " + arguments.Size() + " arguments).");
				return null;
			}
		}
			goto case "boundedReachableEdgesIncoming";
		case "boundedReachableEdgesIncoming":
		case "boundedReachableEdgesOutgoing":
		case "boundedReachableEdges":
		{
			Direction direction = GetDirection(functionName);
			if(arguments.Size() == 2)
			{
				return new BoundedReachableEdgeExprNode(env.Coords, arguments.Get(0), arguments.Get(1),
						new IdentExprNode(env.ParserEnvironment.ArbitraryEdgeRoot), direction, new IdentExprNode(env.ParserEnvironment.NodeRoot));
			}
			else if(arguments.Size() == 3)
			{
				return new BoundedReachableEdgeExprNode(env.Coords, arguments.Get(0), arguments.Get(1), arguments.Get(2),
						direction, new IdentExprNode(env.ParserEnvironment.NodeRoot));
			}
			else if(arguments.Size() == 4)
			{
				return new BoundedReachableEdgeExprNode(env.Coords, arguments.Get(0), arguments.Get(1), arguments.Get(2),
						direction, arguments.Get(3));
			}
			else
			{
				env.ReportError(functionName + "() expects 2-4 arguments (given are " + arguments.Size() + " arguments).");
				return null;
			}
		}
			goto case "boundedReachableIncoming";
		case "boundedReachableIncoming":
		case "boundedReachableOutgoing":
		case "boundedReachable":
		{
			Direction direction = GetDirection(functionName);
			if(arguments.Size() == 2)
			{
				return new BoundedReachableNodeExprNode(env.Coords, arguments.Get(0), arguments.Get(1),
						new IdentExprNode(env.ParserEnvironment.ArbitraryEdgeRoot), direction, new IdentExprNode(env.ParserEnvironment.NodeRoot));
			}
			else if(arguments.Size() == 3)
			{
				return new BoundedReachableNodeExprNode(env.Coords, arguments.Get(0), arguments.Get(1), arguments.Get(2),
						direction, new IdentExprNode(env.ParserEnvironment.NodeRoot));
			}
			else if(arguments.Size() == 4)
			{
				return new BoundedReachableNodeExprNode(env.Coords, arguments.Get(0), arguments.Get(1), arguments.Get(2),
						direction, arguments.Get(3));
			}
			else
			{
				env.ReportError(functionName + "() expects 2-4 arguments (given are " + arguments.Size() + " arguments).");
				return null;
			}
		}
			goto case "boundedReachableWithRemainingDepthIncoming";
		case "boundedReachableWithRemainingDepthIncoming":
		case "boundedReachableWithRemainingDepthOutgoing":
		case "boundedReachableWithRemainingDepth":
		{
			Direction direction = GetDirection(functionName);
			if(arguments.Size() == 2)
			{
				return new BoundedReachableNodeWithRemainingDepthExprNode(env.Coords, arguments.Get(0), arguments.Get(1),
						new IdentExprNode(env.ParserEnvironment.ArbitraryEdgeRoot), direction, new IdentExprNode(env.ParserEnvironment.NodeRoot));
			}
			else if(arguments.Size() == 3)
			{
				return new BoundedReachableNodeWithRemainingDepthExprNode(env.Coords, arguments.Get(0), arguments.Get(1),
						arguments.Get(2), direction, new IdentExprNode(env.ParserEnvironment.NodeRoot));
			}
			else if(arguments.Size() == 4)
			{
				return new BoundedReachableNodeWithRemainingDepthExprNode(env.Coords, arguments.Get(0), arguments.Get(1),
						arguments.Get(2), direction, arguments.Get(3));
			}
			else
			{
				env.ReportError(functionName + "() expects 2-4 arguments (given are " + arguments.Size() + " arguments).");
				return null;
			}
		}
			goto case "countBoundedReachableEdgesIncoming";
		case "countBoundedReachableEdgesIncoming":
		case "countBoundedReachableEdgesOutgoing":
		case "countBoundedReachableEdges":
		{
			Direction direction = GetDirection(functionName);
			if(arguments.Size() == 2)
			{
				return new CountBoundedReachableEdgeExprNode(env.Coords, arguments.Get(0), arguments.Get(1),
						new IdentExprNode(env.ParserEnvironment.ArbitraryEdgeRoot), direction, new IdentExprNode(env.ParserEnvironment.NodeRoot));
			}
			else if(arguments.Size() == 3)
			{
				return new CountBoundedReachableEdgeExprNode(env.Coords, arguments.Get(0), arguments.Get(1), arguments.Get(2),
						direction, new IdentExprNode(env.ParserEnvironment.NodeRoot));
			}
			else if(arguments.Size() == 4)
			{
				return new CountBoundedReachableEdgeExprNode(env.Coords, arguments.Get(0), arguments.Get(1), arguments.Get(2),
						direction, arguments.Get(3));
			}
			else
			{
				env.ReportError(functionName + "() expects 2-4 arguments (given are " + arguments.Size() + " arguments).");
				return null;
			}
		}
			goto case "countBoundedReachableIncoming";
		case "countBoundedReachableIncoming":
		case "countBoundedReachableOutgoing":
		case "countBoundedReachable":
		{
			Direction direction = GetDirection(functionName);
			if(arguments.Size() == 2)
			{
				return new CountBoundedReachableNodeExprNode(env.Coords, arguments.Get(0), arguments.Get(1),
						new IdentExprNode(env.ParserEnvironment.ArbitraryEdgeRoot), direction, new IdentExprNode(env.ParserEnvironment.NodeRoot));
			}
			else if(arguments.Size() == 3)
			{
				return new CountBoundedReachableNodeExprNode(env.Coords, arguments.Get(0), arguments.Get(1), arguments.Get(2),
						direction, new IdentExprNode(env.ParserEnvironment.NodeRoot));
			}
			else if(arguments.Size() == 4)
			{
				return new CountBoundedReachableNodeExprNode(env.Coords, arguments.Get(0), arguments.Get(1), arguments.Get(2),
						direction, arguments.Get(3));
			}
			else
			{
				env.ReportError(functionName + "() expects 2-4 arguments (given are " + arguments.Size() + " arguments).");
				return null;
			}
		}
			goto case "isBoundedReachableIncoming";
		case "isBoundedReachableIncoming":
		case "isBoundedReachableOutgoing":
		case "isBoundedReachable":
		{
			Direction direction = GetDirection(functionName);
			if(arguments.Size() == 3)
			{
				return new IsBoundedReachableNodeExprNode(env.Coords, arguments.Get(0), arguments.Get(1), arguments.Get(2),
						new IdentExprNode(env.ParserEnvironment.ArbitraryEdgeRoot), direction, new IdentExprNode(env.ParserEnvironment.NodeRoot));
			}
			else if(arguments.Size() == 4)
			{
				return new IsBoundedReachableNodeExprNode(env.Coords, arguments.Get(0), arguments.Get(1), arguments.Get(2),
						arguments.Get(3), direction, new IdentExprNode(env.ParserEnvironment.NodeRoot));
			}
			else if(arguments.Size() == 5)
			{
				return new IsBoundedReachableNodeExprNode(env.Coords, arguments.Get(0), arguments.Get(1), arguments.Get(2),
						arguments.Get(3), direction, arguments.Get(4));
			}
			else
			{
				env.ReportError(functionName + "() expects 3-5 arguments (given are " + arguments.Size() + " arguments).");
				return null;
			}
		}
			goto case "isBoundedReachableEdgesIncoming";
		case "isBoundedReachableEdgesIncoming":
		case "isBoundedReachableEdgesOutgoing":
		case "isBoundedReachableEdges":
		{
			Direction direction = GetDirection(functionName);
			if(arguments.Size() == 3)
			{
				return new IsBoundedReachableEdgeExprNode(env.Coords, arguments.Get(0), arguments.Get(1), arguments.Get(2),
						new IdentExprNode(env.ParserEnvironment.ArbitraryEdgeRoot), direction, new IdentExprNode(env.ParserEnvironment.NodeRoot));
			}
			else if(arguments.Size() == 4)
			{
				return new IsBoundedReachableEdgeExprNode(env.Coords, arguments.Get(0), arguments.Get(1), arguments.Get(2),
						arguments.Get(3), direction, new IdentExprNode(env.ParserEnvironment.NodeRoot));
			}
			else if(arguments.Size() == 5)
			{
				return new IsBoundedReachableEdgeExprNode(env.Coords, arguments.Get(0), arguments.Get(1), arguments.Get(2),
						arguments.Get(3), direction, arguments.Get(4));
			}
			else
			{
				env.ReportError(functionName + "() expects 3-5 arguments (given are " + arguments.Size() + " arguments).");
				return null;
			}
		}
			goto case "inducedSubgraph";
		case "inducedSubgraph":
			if(arguments.Size() != 1)
			{
				env.ReportError("inducedSubgraph() expects 1 argument (given are " + arguments.Size() + " arguments).");
				return null;
			}
			else
				return new InducedSubgraphExprNode(env.Coords, arguments.Get(0));
		case "definedSubgraph":
			if(arguments.Size() != 1)
			{
				env.ReportError("definedSubgraph() expects 1 argument (given are " + arguments.Size() + " arguments).");
				return null;
			}
			else
				return new DefinedSubgraphExprNode(env.Coords, arguments.Get(0));
		case "equalsAny":
			if(arguments.Size() != 2)
			{
				env.ReportError("equalsAny() expects 2 arguments (given are " + arguments.Size() + " arguments).");
				return null;
			}
			else
				return new EqualsAnyExprNode(env.Coords, arguments.Get(0), arguments.Get(1), true);
		case "equalsAnyStructurally":
			if(arguments.Size() != 2)
			{
				env.ReportError("equalsAnyStructurally() expects 2 arguments (given are " + arguments.Size() + " arguments).");
				return null;
			}
			else
				return new EqualsAnyExprNode(env.Coords, arguments.Get(0), arguments.Get(1), false);
		case "getEquivalent":
			if(arguments.Size() != 2)
			{
				env.ReportError("getEquivalent() expects 2 arguments (given are " + arguments.Size() + " arguments).");
				return null;
			}
			else
				return new GetEquivalentExprNode(env.Coords, arguments.Get(0), arguments.Get(1), true);
		case "getEquivalentStructurally":
			if(arguments.Size() != 2)
			{
				env.ReportError("getEquivalentStructurally() expects 2 arguments (given are " + arguments.Size() + " arguments).");
				return null;
			}
			else
				return new GetEquivalentExprNode(env.Coords, arguments.Get(0), arguments.Get(1), false);
		case "copy":
			if(arguments.Size() != 1)
			{
				env.ReportError("copy() expects 1 argument (given are " + arguments.Size() + " arguments).");
				return null;
			}
			else
				return new CopyExprNode(env.Coords, arguments.Get(0), true);
		case "clone":
			if(arguments.Size() != 1)
			{
				env.ReportError("clone() expects 1 argument (given are " + arguments.Size() + " arguments).");
				return null;
			}
			else
				return new CopyExprNode(env.Coords, arguments.Get(0), false);
		case "canonize":
			if(arguments.Size() != 1)
			{
				env.ReportError("canonize() expects 1 argument (given are " + arguments.Size() + " arguments).");
				return null;
			}
			else
				return new CanonizeExprNode(env.Coords, arguments.Get(0));
		case "uniqueof":
			if(arguments.Size() > 1)
			{
				env.ReportError("uniqueof() expects 1 or 0 arguments (given are " + arguments.Size() + " arguments).");
				return null;
			}
			else if(arguments.Size() == 1)
				return new UniqueofExprNode(env.Coords, arguments.Get(0));
			else
				return new UniqueofExprNode(env.Coords, null);
		case "graphof":
			if(arguments.Size() != 1)
			{
				env.ReportError("graphof() expects 1 argument (given are " + arguments.Size() + " arguments).");
				return null;
			}
			else
				return new GraphofExprNode(env.Coords, arguments.Get(0));
		default:
			env.ReportError("A function of name " + functionName + " is not known.");
			return null;
		}
	}

	public static Direction GetDirection(string functionName)
	{
		switch(functionName)
		{
		case "adjacentIncoming":
		case "countAdjacentIncoming":
		case "isAdjacentIncoming":
		case "reachableIncoming":
		case "countReachableIncoming":
		case "isReachableIncoming":
		case "boundedReachableIncoming":
		case "boundedReachableWithRemainingDepthIncoming":
		case "countBoundedReachableIncoming":
		case "isBoundedReachableIncoming":
			return Direction.INCOMING;
		case "adjacentOutgoing":
		case "countAdjacentOutgoing":
		case "isAdjacentOutgoing":
		case "reachableOutgoing":
		case "countReachableOutgoing":
		case "isReachableOutgoing":
		case "boundedReachableOutgoing":
		case "boundedReachableWithRemainingDepthOutgoing":
		case "countBoundedReachableOutgoing":
		case "isBoundedReachableOutgoing":
			return Direction.OUTGOING;
		case "adjacent":
		case "countAdjacent":
		case "isAdjacent":
		case "reachable":
		case "countReachable":
		case "isReachable":
		case "boundedReachable":
		case "boundedReachableWithRemainingDepth":
		case "countBoundedReachable":
		case "isBoundedReachable":
			return Direction.INCIDENT;
		case "incoming":
		case "countIncoming":
		case "isIncoming":
		case "reachableEdgesIncoming":
		case "countReachableEdgesIncoming":
		case "isReachableEdgesIncoming":
		case "boundedReachableEdgesIncoming":
		case "countBoundedReachableEdgesIncoming":
		case "isBoundedReachableEdgesIncoming":
			return Direction.INCOMING;
		case "outgoing":
		case "countOutgoing":
		case "isOutgoing":
		case "reachableEdgesOutgoing":
		case "countReachableEdgesOutgoing":
		case "isReachableEdgesOutgoing":
		case "boundedReachableEdgesOutgoing":
		case "countBoundedReachableEdgesOutgoing":
		case "isBoundedReachableEdgesOutgoing":
			return Direction.OUTGOING;
		case "incident":
		case "countIncident":
		case "isIncident":
		case "reachableEdges":
		case "countReachableEdges":
		case "isReachableEdges":
		case "boundedReachableEdges":
		case "countBoundedReachableEdges":
		case "isBoundedReachableEdges":
			return Direction.INCIDENT;
		}

		return Direction.INVALID;
	}

	protected internal override bool CheckLocal()
	{
		return true;
	}

	public override TypeNode Type
	{
		get
		{
			return result.Type;
		}
	}

	public virtual ExprNode Result
	{
		get
		{
			return result;
		}
	}

	protected internal override IR ConstructIR()
	{
		return result.IR;
	}
}

}
