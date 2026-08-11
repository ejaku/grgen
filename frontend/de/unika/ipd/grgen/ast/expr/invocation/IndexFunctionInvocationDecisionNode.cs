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

using System;
using System.Collections.Generic;

using de.unika.ipd.grgen.ast;
using BuiltinFunctionInvocationBaseNode = de.unika.ipd.grgen.ast.expr.BuiltinFunctionInvocationBaseNode;
using ExprNode = de.unika.ipd.grgen.ast.expr.ExprNode;
using CountEdgesFromIndexAccessFromToExprNode = de.unika.ipd.grgen.ast.expr.graph.CountEdgesFromIndexAccessFromToExprNode;
using CountEdgesFromIndexAccessSameExprNode = de.unika.ipd.grgen.ast.expr.graph.CountEdgesFromIndexAccessSameExprNode;
using CountIncidenceFromIndexExprNode = de.unika.ipd.grgen.ast.expr.graph.CountIncidenceFromIndexExprNode;
using CountNodesFromIndexAccessFromToExprNode = de.unika.ipd.grgen.ast.expr.graph.CountNodesFromIndexAccessFromToExprNode;
using CountNodesFromIndexAccessSameExprNode = de.unika.ipd.grgen.ast.expr.graph.CountNodesFromIndexAccessSameExprNode;
using EdgesFromIndexAccessFromToAsArrayExprNode = de.unika.ipd.grgen.ast.expr.graph.EdgesFromIndexAccessFromToAsArrayExprNode;
using EdgesFromIndexAccessFromToExprNode = de.unika.ipd.grgen.ast.expr.graph.EdgesFromIndexAccessFromToExprNode;
using EdgesFromIndexAccessMultipleFromToExprNode = de.unika.ipd.grgen.ast.expr.graph.EdgesFromIndexAccessMultipleFromToExprNode;
using EdgesFromIndexAccessSameAsArrayExprNode = de.unika.ipd.grgen.ast.expr.graph.EdgesFromIndexAccessSameAsArrayExprNode;
using EdgesFromIndexAccessSameExprNode = de.unika.ipd.grgen.ast.expr.graph.EdgesFromIndexAccessSameExprNode;
using FromIndexAccessFromToPartExprNode = de.unika.ipd.grgen.ast.expr.graph.FromIndexAccessFromToPartExprNode;
using FromIndexAccessMultipleFromToExprNode = de.unika.ipd.grgen.ast.expr.graph.FromIndexAccessMultipleFromToExprNode;
using IndexSizeExprNode = de.unika.ipd.grgen.ast.expr.graph.IndexSizeExprNode;
using IsInEdgesFromIndexAccessFromToExprNode = de.unika.ipd.grgen.ast.expr.graph.IsInEdgesFromIndexAccessFromToExprNode;
using IsInEdgesFromIndexAccessSameExprNode = de.unika.ipd.grgen.ast.expr.graph.IsInEdgesFromIndexAccessSameExprNode;
using IsInNodesFromIndexAccessFromToExprNode = de.unika.ipd.grgen.ast.expr.graph.IsInNodesFromIndexAccessFromToExprNode;
using IsInNodesFromIndexAccessSameExprNode = de.unika.ipd.grgen.ast.expr.graph.IsInNodesFromIndexAccessSameExprNode;
using MinMaxEdgeFromIndexExprNode = de.unika.ipd.grgen.ast.expr.graph.MinMaxEdgeFromIndexExprNode;
using MinMaxNodeFromIndexExprNode = de.unika.ipd.grgen.ast.expr.graph.MinMaxNodeFromIndexExprNode;
using NodesFromIndexAccessFromToAsArrayExprNode = de.unika.ipd.grgen.ast.expr.graph.NodesFromIndexAccessFromToAsArrayExprNode;
using NodesFromIndexAccessFromToExprNode = de.unika.ipd.grgen.ast.expr.graph.NodesFromIndexAccessFromToExprNode;
using NodesFromIndexAccessMultipleFromToExprNode = de.unika.ipd.grgen.ast.expr.graph.NodesFromIndexAccessMultipleFromToExprNode;
using NodesFromIndexAccessSameAsArrayExprNode = de.unika.ipd.grgen.ast.expr.graph.NodesFromIndexAccessSameAsArrayExprNode;
using NodesFromIndexAccessSameExprNode = de.unika.ipd.grgen.ast.expr.graph.NodesFromIndexAccessSameExprNode;
using TypeNode = de.unika.ipd.grgen.ast.type.TypeNode;
using FunctionTypeNode = de.unika.ipd.grgen.ast.type.executable.FunctionTypeNode;
using ResolvingEnvironment = de.unika.ipd.grgen.ast.util.ResolvingEnvironment;
using IR = de.unika.ipd.grgen.ir.IR;
using ParserEnvironment = de.unika.ipd.grgen.parser.ParserEnvironment;

public class IndexFunctionInvocationDecisionNode : FunctionOrBuiltinFunctionInvocationBaseNode
{
	static IndexFunctionInvocationDecisionNode()
	{
		SetClassName(typeof(IndexFunctionInvocationDecisionNode), "index function invocation decision expression");
	}

	internal static TypeNode functionTypeNode = new FunctionTypeNode();

	public IdentNode functionIdent;
	private BuiltinFunctionInvocationBaseNode result;

	internal ParserEnvironment env;

	private CollectNode<BaseNode> arguments; // I prefer to keep the namespaces of indices and entities disjoint, so this special node is required with base node children instead of expression children (alternative would be to merge indices into entities, resolve the IdentExprNode also to an index, and remove this special handling as well as the special handling in the parser)


	public IndexFunctionInvocationDecisionNode(IdentNode functionIdent,
			CollectNode<BaseNode> arguments, ParserEnvironment env)
		: base(functionIdent.Coords)
	{
		this.functionIdent = BecomeParent(functionIdent);
		this.env = env;
		this.arguments = BecomeParent(arguments);
	}

	public override ICollection<BaseNode> Children
	{
		get
		{
			IList<BaseNode> children = new List<BaseNode>();
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

	private static BuiltinFunctionInvocationBaseNode Decide(string functionName, CollectNode<BaseNode> arguments,
			ResolvingEnvironment env)
	{
		switch(functionName)
		{
		case "nodesFromIndex":
			if(arguments.Size() != 1)
			{
				env.ReportError("nodesFromIndex() expects 1 argument (given are " + arguments.Size() + " arguments).");
				return null;
			}
			else
				return new NodesFromIndexAccessFromToExprNode(env.Coords, arguments.Get(0), null, false, null, false);
		case "nodesFromIndexSame":
			if(arguments.Size() != 2)
			{
				env.ReportError("nodesFromIndexSame() expects 2 arguments (given are " + arguments.Size() + " arguments).");
				return null;
			}
			else
				return new NodesFromIndexAccessSameExprNode(env.Coords, arguments.Get(0), (ExprNode)arguments.Get(1));
		case "nodesFromIndexFrom":
		case "nodesFromIndexFromExclusive":
		case "nodesFromIndexTo":
		case "nodesFromIndexToExclusive":
			if(arguments.Size() != 2)
			{
				env.ReportError(functionName + "() expects 2 arguments (given are " + arguments.Size() + " arguments).");
				return null;
			}
			else
			{
				if(functionName.StartsWith("nodesFromIndexFrom", StringComparison.Ordinal))
					return new NodesFromIndexAccessFromToExprNode(env.Coords, arguments.Get(0), (ExprNode)arguments.Get(1), functionName.EndsWith("Exclusive", StringComparison.Ordinal), null, false);
				else
					return new NodesFromIndexAccessFromToExprNode(env.Coords, arguments.Get(0), null, false, (ExprNode)arguments.Get(1), functionName.EndsWith("Exclusive", StringComparison.Ordinal));
			}
			goto case "nodesFromIndexFromTo";
		case "nodesFromIndexFromTo":
		case "nodesFromIndexFromExclusiveTo":
		case "nodesFromIndexFromToExclusive":
		case "nodesFromIndexFromExclusiveToExclusive":
			if(arguments.Size() != 3)
			{
				env.ReportError(functionName + "() expects 3 arguments (given are " + arguments.Size() + " arguments).");
				return null;
			}
			else
				return new NodesFromIndexAccessFromToExprNode(env.Coords, arguments.Get(0), (ExprNode)arguments.Get(1), functionName.Contains("FromExclusive"), (ExprNode)arguments.Get(2), functionName.Contains("ToExclusive"));
		case "edgesFromIndex":
			if(arguments.Size() != 1)
			{
				env.ReportError("edgesFromIndex() expects 1 argument (given are " + arguments.Size() + " arguments).");
				return null;
			}
			else
				return new EdgesFromIndexAccessFromToExprNode(env.Coords, arguments.Get(0), null, false, null, false);
		case "edgesFromIndexSame":
			if(arguments.Size() != 2)
			{
				env.ReportError("edgesFromIndexSame() expects 2 arguments (given are " + arguments.Size() + " arguments).");
				return null;
			}
			else
				return new EdgesFromIndexAccessSameExprNode(env.Coords, arguments.Get(0), (ExprNode)arguments.Get(1));
		case "edgesFromIndexFrom":
		case "edgesFromIndexFromExclusive":
		case "edgesFromIndexTo":
		case "edgesFromIndexToExclusive":
			if(arguments.Size() != 2)
			{
				env.ReportError(functionName + "() expects 2 arguments (given are " + arguments.Size() + " arguments).");
				return null;
			}
			else
			{
				if(functionName.StartsWith("edgesFromIndexFrom", StringComparison.Ordinal))
					return new EdgesFromIndexAccessFromToExprNode(env.Coords, arguments.Get(0), (ExprNode)arguments.Get(1), functionName.EndsWith("Exclusive", StringComparison.Ordinal), null, false);
				else
					return new EdgesFromIndexAccessFromToExprNode(env.Coords, arguments.Get(0), null, false, (ExprNode)arguments.Get(1), functionName.EndsWith("Exclusive", StringComparison.Ordinal));
			}
			goto case "edgesFromIndexFromTo";
		case "edgesFromIndexFromTo":
		case "edgesFromIndexFromExclusiveTo":
		case "edgesFromIndexFromToExclusive":
		case "edgesFromIndexFromExclusiveToExclusive":
			if(arguments.Size() != 3)
			{
				env.ReportError(functionName + "() expects 3 arguments (given are " + arguments.Size() + " arguments).");
				return null;
			}
			else
				return new EdgesFromIndexAccessFromToExprNode(env.Coords, arguments.Get(0), (ExprNode)arguments.Get(1), functionName.Contains("FromExclusive"), (ExprNode)arguments.Get(2), functionName.Contains("ToExclusive"));
		case "countNodesFromIndex":
			if(arguments.Size() != 1)
			{
				env.ReportError("countNodesFromIndex() expects 1 argument (given are " + arguments.Size() + " arguments).");
				return null;
			}
			else
				return new CountNodesFromIndexAccessFromToExprNode(env.Coords, arguments.Get(0), null, false, null, false);
		case "countNodesFromIndexSame":
			if(arguments.Size() != 2)
			{
				env.ReportError("countNodesFromIndexSame() expects 2 arguments (given are " + arguments.Size() + " arguments).");
				return null;
			}
			else
				return new CountNodesFromIndexAccessSameExprNode(env.Coords, arguments.Get(0), (ExprNode)arguments.Get(1));
		case "countNodesFromIndexFrom":
		case "countNodesFromIndexFromExclusive":
		case "countNodesFromIndexTo":
		case "countNodesFromIndexToExclusive":
			if(arguments.Size() != 2)
			{
				env.ReportError(functionName + "() expects 2 arguments (given are " + arguments.Size() + " arguments).");
				return null;
			}
			else
			{
				if(functionName.StartsWith("countNodesFromIndexFrom", StringComparison.Ordinal))
					return new CountNodesFromIndexAccessFromToExprNode(env.Coords, arguments.Get(0), (ExprNode)arguments.Get(1), functionName.EndsWith("Exclusive", StringComparison.Ordinal), null, false);
				else
					return new CountNodesFromIndexAccessFromToExprNode(env.Coords, arguments.Get(0), null, false, (ExprNode)arguments.Get(1), functionName.EndsWith("Exclusive", StringComparison.Ordinal));
			}
			goto case "countNodesFromIndexFromTo";
		case "countNodesFromIndexFromTo":
		case "countNodesFromIndexFromExclusiveTo":
		case "countNodesFromIndexFromToExclusive":
		case "countNodesFromIndexFromExclusiveToExclusive":
			if(arguments.Size() != 3)
			{
				env.ReportError(functionName + "() expects 3 arguments (given are " + arguments.Size() + " arguments).");
				return null;
			}
			else
				return new CountNodesFromIndexAccessFromToExprNode(env.Coords, arguments.Get(0), (ExprNode)arguments.Get(1), functionName.Contains("FromExclusive"), (ExprNode)arguments.Get(2), functionName.Contains("ToExclusive"));
		case "countEdgesFromIndex":
			if(arguments.Size() != 1)
			{
				env.ReportError("countEdgesFromIndex() expects 1 argument (given are " + arguments.Size() + " arguments).");
				return null;
			}
			else
				return new CountEdgesFromIndexAccessFromToExprNode(env.Coords, arguments.Get(0), null, false, null, false);
		case "countEdgesFromIndexSame":
			if(arguments.Size() != 2)
			{
				env.ReportError("countEdgesFromIndexSame() expects 2 arguments (given are " + arguments.Size() + " arguments).");
				return null;
			}
			else
				return new CountEdgesFromIndexAccessSameExprNode(env.Coords, arguments.Get(0), (ExprNode)arguments.Get(1));
		case "countEdgesFromIndexFrom":
		case "countEdgesFromIndexFromExclusive":
		case "countEdgesFromIndexTo":
		case "countEdgesFromIndexToExclusive":
			if(arguments.Size() != 2)
			{
				env.ReportError(functionName + "() expects 2 arguments (given are " + arguments.Size() + " arguments).");
				return null;
			}
			else
			{
				if(functionName.StartsWith("countEdgesFromIndexFrom", StringComparison.Ordinal))
					return new CountEdgesFromIndexAccessFromToExprNode(env.Coords, arguments.Get(0), (ExprNode)arguments.Get(1), functionName.EndsWith("Exclusive", StringComparison.Ordinal), null, false);
				else
					return new CountEdgesFromIndexAccessFromToExprNode(env.Coords, arguments.Get(0), null, false, (ExprNode)arguments.Get(1), functionName.EndsWith("Exclusive", StringComparison.Ordinal));
			}
			goto case "countEdgesFromIndexFromTo";
		case "countEdgesFromIndexFromTo":
		case "countEdgesFromIndexFromExclusiveTo":
		case "countEdgesFromIndexFromToExclusive":
		case "countEdgesFromIndexFromExclusiveToExclusive":
			if(arguments.Size() != 3)
			{
				env.ReportError(functionName + "() expects 3 arguments (given are " + arguments.Size() + " arguments).");
				return null;
			}
			else
				return new CountEdgesFromIndexAccessFromToExprNode(env.Coords, arguments.Get(0), (ExprNode)arguments.Get(1), functionName.Contains("FromExclusive"), (ExprNode)arguments.Get(2), functionName.Contains("ToExclusive"));
		case "isInNodesFromIndex":
			if(arguments.Size() != 2)
			{
				env.ReportError("isInNodesFromIndex() expects 2 arguments (given are " + arguments.Size() + " arguments).");
				return null;
			}
			else
				return new IsInNodesFromIndexAccessFromToExprNode(env.Coords, (ExprNode)arguments.Get(0), arguments.Get(1), null, false, null, false);
		case "isInNodesFromIndexSame":
			if(arguments.Size() != 3)
			{
				env.ReportError("isInNodesFromIndexSame() expects 3 arguments (given are " + arguments.Size() + " arguments).");
				return null;
			}
			else
				return new IsInNodesFromIndexAccessSameExprNode(env.Coords, (ExprNode)arguments.Get(0), arguments.Get(1), (ExprNode)arguments.Get(2));
		case "isInNodesFromIndexFrom":
		case "isInNodesFromIndexFromExclusive":
		case "isInNodesFromIndexTo":
		case "isInNodesFromIndexToExclusive":
			if(arguments.Size() != 3)
			{
				env.ReportError(functionName + "() expects 3 arguments (given are " + arguments.Size() + " arguments).");
				return null;
			}
			else
			{
				if(functionName.StartsWith("isInNodesFromIndexFrom", StringComparison.Ordinal))
					return new IsInNodesFromIndexAccessFromToExprNode(env.Coords, (ExprNode)arguments.Get(0), arguments.Get(1), (ExprNode)arguments.Get(2), functionName.EndsWith("Exclusive", StringComparison.Ordinal), null, false);
				else
					return new IsInNodesFromIndexAccessFromToExprNode(env.Coords, (ExprNode)arguments.Get(0), arguments.Get(1), null, false, (ExprNode)arguments.Get(2), functionName.EndsWith("Exclusive", StringComparison.Ordinal));
			}
			goto case "isInNodesFromIndexFromTo";
		case "isInNodesFromIndexFromTo":
		case "isInNodesFromIndexFromExclusiveTo":
		case "isInNodesFromIndexFromToExclusive":
		case "isInNodesFromIndexFromExclusiveToExclusive":
			if(arguments.Size() != 4)
			{
				env.ReportError(functionName + "() expects 4 arguments (given are " + arguments.Size() + " arguments).");
				return null;
			}
			else
				return new IsInNodesFromIndexAccessFromToExprNode(env.Coords, (ExprNode)arguments.Get(0), arguments.Get(1), (ExprNode)arguments.Get(2), functionName.Contains("FromExclusive"), (ExprNode)arguments.Get(3), functionName.Contains("ToExclusive"));
		case "isInEdgesFromIndex":
			if(arguments.Size() != 2)
			{
				env.ReportError("isInEdgesFromIndex() expects 2 argument (given are " + arguments.Size() + " arguments).");
				return null;
			}
			else
				return new IsInEdgesFromIndexAccessFromToExprNode(env.Coords, (ExprNode)arguments.Get(0), arguments.Get(1), null, false, null, false);
		case "isInEdgesFromIndexSame":
			if(arguments.Size() != 3)
			{
				env.ReportError("isInEdgesFromIndexSame() expects 3 arguments (given are " + arguments.Size() + " arguments).");
				return null;
			}
			else
				return new IsInEdgesFromIndexAccessSameExprNode(env.Coords, (ExprNode)arguments.Get(0), arguments.Get(1), (ExprNode)arguments.Get(2));
		case "isInEdgesFromIndexFrom":
		case "isInEdgesFromIndexFromExclusive":
		case "isInEdgesFromIndexTo":
		case "isInEdgesFromIndexToExclusive":
			if(arguments.Size() != 3)
			{
				env.ReportError(functionName + "() expects 3 arguments (given are " + arguments.Size() + " arguments).");
				return null;
			}
			else
			{
				if(functionName.StartsWith("isInEdgesFromIndexFrom", StringComparison.Ordinal))
					return new IsInEdgesFromIndexAccessFromToExprNode(env.Coords, (ExprNode)arguments.Get(0), arguments.Get(1), (ExprNode)arguments.Get(2), functionName.EndsWith("Exclusive", StringComparison.Ordinal), null, false);
				else
					return new IsInEdgesFromIndexAccessFromToExprNode(env.Coords, (ExprNode)arguments.Get(0), arguments.Get(1), null, false, (ExprNode)arguments.Get(2), functionName.EndsWith("Exclusive", StringComparison.Ordinal));
			}
			goto case "isInEdgesFromIndexFromTo";
		case "isInEdgesFromIndexFromTo":
		case "isInEdgesFromIndexFromExclusiveTo":
		case "isInEdgesFromIndexFromToExclusive":
		case "isInEdgesFromIndexFromExclusiveToExclusive":
			if(arguments.Size() != 4)
			{
				env.ReportError(functionName + "() expects 4 arguments (given are " + arguments.Size() + " arguments).");
				return null;
			}
			else
				return new IsInEdgesFromIndexAccessFromToExprNode(env.Coords, (ExprNode)arguments.Get(0), arguments.Get(1), (ExprNode)arguments.Get(2), functionName.Contains("FromExclusive"), (ExprNode)arguments.Get(3), functionName.Contains("ToExclusive"));
		case "nodesFromIndexAsArrayAscending":
		case "nodesFromIndexAsArrayDescending":
		case "nodesFromIndexAscending":
		case "nodesFromIndexDescending":
			if(arguments.Size() != 1)
			{
				env.ReportError(functionName + "() expects 1 argument (given are " + arguments.Size() + " arguments).");
				return null;
			}
			else
				return new NodesFromIndexAccessFromToAsArrayExprNode(env.Coords, arguments.Get(0), functionName.Contains("Ascending"), null, false, null, false);
		case "nodesFromIndexSameAsArray":
			if(arguments.Size() != 2)
			{
				env.ReportError("nodesFromIndexSameAsArray() expects 2 arguments (given are " + arguments.Size() + " arguments).");
				return null;
			}
			else
				return new NodesFromIndexAccessSameAsArrayExprNode(env.Coords, arguments.Get(0), (ExprNode)arguments.Get(1));
		case "nodesFromIndexFromAsArrayAscending":
		case "nodesFromIndexFromExclusiveAsArrayAscending":
		case "nodesFromIndexToAsArrayAscending":
		case "nodesFromIndexToExclusiveAsArrayAscending":
		case "nodesFromIndexFromAsArrayDescending":
		case "nodesFromIndexFromExclusiveAsArrayDescending":
		case "nodesFromIndexToAsArrayDescending":
		case "nodesFromIndexToExclusiveAsArrayDescending":
		case "nodesFromIndexFromAscending":
		case "nodesFromIndexFromExclusiveAscending":
		case "nodesFromIndexToAscending":
		case "nodesFromIndexToExclusiveAscending":
		case "nodesFromIndexFromDescending":
		case "nodesFromIndexFromExclusiveDescending":
		case "nodesFromIndexToDescending":
		case "nodesFromIndexToExclusiveDescending":
			if(arguments.Size() != 2)
			{
				env.ReportError(functionName + "() expects 2 arguments (given are " + arguments.Size() + " arguments).");
				return null;
			}
			else
			{
				if(functionName.StartsWith("nodesFromIndexFrom", StringComparison.Ordinal))
					return new NodesFromIndexAccessFromToAsArrayExprNode(env.Coords, arguments.Get(0), functionName.Contains("Ascending"), (ExprNode)arguments.Get(1), functionName.Contains("Exclusive"), null, false);
				else
					return new NodesFromIndexAccessFromToAsArrayExprNode(env.Coords, arguments.Get(0), functionName.Contains("Ascending"), null, false, (ExprNode)arguments.Get(1), functionName.Contains("Exclusive"));
			}
			goto case "nodesFromIndexFromToAsArrayAscending";
		case "nodesFromIndexFromToAsArrayAscending":
		case "nodesFromIndexFromExclusiveToAsArrayAscending":
		case "nodesFromIndexFromToExclusiveAsArrayAscending":
		case "nodesFromIndexFromExclusiveToExclusiveAsArrayAscending":
		case "nodesFromIndexFromToAsArrayDescending":
		case "nodesFromIndexFromExclusiveToAsArrayDescending":
		case "nodesFromIndexFromToExclusiveAsArrayDescending":
		case "nodesFromIndexFromExclusiveToExclusiveAsArrayDescending":
		case "nodesFromIndexFromToAscending":
		case "nodesFromIndexFromExclusiveToAscending":
		case "nodesFromIndexFromToExclusiveAscending":
		case "nodesFromIndexFromExclusiveToExclusiveAscending":
		case "nodesFromIndexFromToDescending":
		case "nodesFromIndexFromExclusiveToDescending":
		case "nodesFromIndexFromToExclusiveDescending":
		case "nodesFromIndexFromExclusiveToExclusiveDescending":
			if(arguments.Size() != 3)
			{
				env.ReportError(functionName + "() expects 3 arguments (given are " + arguments.Size() + " arguments).");
				return null;
			}
			else
				return new NodesFromIndexAccessFromToAsArrayExprNode(env.Coords, arguments.Get(0), functionName.Contains("Ascending"), (ExprNode)arguments.Get(1), functionName.Contains("FromExclusive"), (ExprNode)arguments.Get(2), functionName.Contains("ToExclusive"));
		case "edgesFromIndexAsArrayAscending":
		case "edgesFromIndexAsArrayDescending":
		case "edgesFromIndexAscending":
		case "edgesFromIndexDescending":
			if(arguments.Size() != 1)
			{
				env.ReportError(functionName + "() expects 1 argument (given are " + arguments.Size() + " arguments).");
				return null;
			}
			else
				return new EdgesFromIndexAccessFromToAsArrayExprNode(env.Coords, arguments.Get(0), functionName.Contains("Ascending"), null, false, null, false);
		case "edgesFromIndexSameAsArray":
			if(arguments.Size() != 2)
			{
				env.ReportError("edgesFromIndexSameAsArray() expects 2 arguments (given are " + arguments.Size() + " arguments).");
				return null;
			}
			else
				return new EdgesFromIndexAccessSameAsArrayExprNode(env.Coords, arguments.Get(0), (ExprNode)arguments.Get(1));
		case "edgesFromIndexFromAsArrayAscending":
		case "edgesFromIndexFromExclusiveAsArrayAscending":
		case "edgesFromIndexToAsArrayAscending":
		case "edgesFromIndexToExclusiveAsArrayAscending":
		case "edgesFromIndexFromAsArrayDescending":
		case "edgesFromIndexFromExclusiveAsArrayDescending":
		case "edgesFromIndexToAsArrayDescending":
		case "edgesFromIndexToExclusiveAsArrayDescending":
		case "edgesFromIndexFromAscending":
		case "edgesFromIndexFromExclusiveAscending":
		case "edgesFromIndexToAscending":
		case "edgesFromIndexToExclusiveAscending":
		case "edgesFromIndexFromDescending":
		case "edgesFromIndexFromExclusiveDescending":
		case "edgesFromIndexToDescending":
		case "edgesFromIndexToExclusiveDescending":
			if(arguments.Size() != 2)
			{
				env.ReportError(functionName + "() expects 2 arguments (given are " + arguments.Size() + " arguments).");
				return null;
			}
			else
			{
				if(functionName.StartsWith("edgesFromIndexFrom", StringComparison.Ordinal))
					return new EdgesFromIndexAccessFromToAsArrayExprNode(env.Coords, arguments.Get(0), functionName.Contains("Ascending"), (ExprNode)arguments.Get(1), functionName.Contains("Exclusive"), null, false);
				else
					return new EdgesFromIndexAccessFromToAsArrayExprNode(env.Coords, arguments.Get(0), functionName.Contains("Ascending"), null, false, (ExprNode)arguments.Get(1), functionName.Contains("Exclusive"));
			}
			goto case "edgesFromIndexFromToAsArrayAscending";
		case "edgesFromIndexFromToAsArrayAscending":
		case "edgesFromIndexFromExclusiveToAsArrayAscending":
		case "edgesFromIndexFromToExclusiveAsArrayAscending":
		case "edgesFromIndexFromExclusiveToExclusiveAsArrayAscending":
		case "edgesFromIndexFromToAsArrayDescending":
		case "edgesFromIndexFromExclusiveToAsArrayDescending":
		case "edgesFromIndexFromToExclusiveAsArrayDescending":
		case "edgesFromIndexFromExclusiveToExclusiveAsArrayDescending":
		case "edgesFromIndexFromToAscending":
		case "edgesFromIndexFromExclusiveToAscending":
		case "edgesFromIndexFromToExclusiveAscending":
		case "edgesFromIndexFromExclusiveToExclusiveAscending":
		case "edgesFromIndexFromToDescending":
		case "edgesFromIndexFromExclusiveToDescending":
		case "edgesFromIndexFromToExclusiveDescending":
		case "edgesFromIndexFromExclusiveToExclusiveDescending":
			if(arguments.Size() != 3)
			{
				env.ReportError(functionName + "() expects 3 arguments (given are " + arguments.Size() + " arguments).");
				return null;
			}
			else
				return new EdgesFromIndexAccessFromToAsArrayExprNode(env.Coords, arguments.Get(0), functionName.Contains("Ascending"), (ExprNode)arguments.Get(1), functionName.Contains("FromExclusive"), (ExprNode)arguments.Get(2), functionName.Contains("ToExclusive"));
		case "nodesFromIndexMultipleFromTo":
		case "edgesFromIndexMultipleFromTo":
			if(arguments.Size() % 3 != 0)
			{
				env.ReportError(functionName + "() expects a multiple of 3 arguments (given are " + arguments.Size() + " arguments).");
				return null;
			}
			else
			{
				FromIndexAccessMultipleFromToExprNode indexAccessMultiple = functionName.Equals("nodesFromIndexMultipleFromTo") ? (FromIndexAccessMultipleFromToExprNode)new NodesFromIndexAccessMultipleFromToExprNode(env.Coords) : new EdgesFromIndexAccessMultipleFromToExprNode(env.Coords);
				for(int i = 0; i < arguments.Size(); i += 3)
				{
					BaseNode index = arguments.Get(i);
					ExprNode fromExpr = (ExprNode)arguments.Get(i + 1);
					ExprNode toExpr = (ExprNode)arguments.Get(i + 2);
					indexAccessMultiple.AddIndexAccessExpr(new FromIndexAccessFromToPartExprNode(index.Coords, index, fromExpr, false, toExpr, false, i, indexAccessMultiple));
				}
				return indexAccessMultiple;
			}
		case "countFromIndex":
			if(arguments.Size() != 2)
			{
				env.ReportError("countFromIndex() expects 2 arguments (given are " + arguments.Size() + " arguments).");
				return null;
			}
			else
				return new CountIncidenceFromIndexExprNode(env.Coords, arguments.Get(0), (ExprNode)arguments.Get(1));
		case "minNodeFromIndex":
			if(arguments.Size() != 1)
			{
				env.ReportError("minNodeFromIndex() expects 1 argument (given are " + arguments.Size() + " arguments).");
				return null;
			}
			else
				return new MinMaxNodeFromIndexExprNode(env.Coords, arguments.Get(0), true);
		case "maxNodeFromIndex":
			if(arguments.Size() != 1)
			{
				env.ReportError("maxNodeFromIndex() expects 1 argument (given are " + arguments.Size() + " arguments).");
				return null;
			}
			else
				return new MinMaxNodeFromIndexExprNode(env.Coords, arguments.Get(0), false);
		case "minEdgeFromIndex":
			if(arguments.Size() != 1)
			{
				env.ReportError("minEdgeFromIndex() expects 1 argument (given are " + arguments.Size() + " arguments).");
				return null;
			}
			else
				return new MinMaxEdgeFromIndexExprNode(env.Coords, arguments.Get(0), true);
		case "maxEdgeFromIndex":
			if(arguments.Size() != 1)
			{
				env.ReportError("maxEdgeFromIndex() expects 1 argument (given are " + arguments.Size() + " arguments).");
				return null;
			}
			else
				return new MinMaxEdgeFromIndexExprNode(env.Coords, arguments.Get(0), false);
		case "indexSize":
			if(arguments.Size() != 1)
			{
				env.ReportError("indexSize() expects 1 argument (given are " + arguments.Size() + " arguments).");
				return null;
			}
			else
				return new IndexSizeExprNode(env.Coords, arguments.Get(0));
		default:
			env.ReportError("An index function of name " + functionName + " is not known.");
			return null;
		}
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
