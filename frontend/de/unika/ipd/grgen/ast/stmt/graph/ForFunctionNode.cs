/*
 * GrGen: graph rewrite generator tool -- release GrGen.NET 8.1
 * Copyright (C) 2003-2026 Universitaet Karlsruhe, Institut fuer Programmstrukturen und Datenorganisation, LS Goos; and free programmers
 * licensed under LGPL v3, some components/parts use different licenses (see LICENSE.txt included in the packaging of this file)
 * www.grgen.de / www.grgen.net
 */

/// <summary>
/// @author Edgar Jakumeit
/// </summary>

namespace de.unika.ipd.grgen.ast.stmt.graph
{

	using System.Collections.Generic;

	using de.unika.ipd.grgen.ast;
	using DeclNode = de.unika.ipd.grgen.ast.decl.DeclNode;
	using AdjacentNodeExprNode = de.unika.ipd.grgen.ast.expr.graph.AdjacentNodeExprNode;
	using BoundedReachableEdgeExprNode = de.unika.ipd.grgen.ast.expr.graph.BoundedReachableEdgeExprNode;
	using BoundedReachableNodeExprNode = de.unika.ipd.grgen.ast.expr.graph.BoundedReachableNodeExprNode;
	using EdgesExprNode = de.unika.ipd.grgen.ast.expr.graph.EdgesExprNode;
	using EdgesFromIndexAccessFromToAsArrayExprNode = de.unika.ipd.grgen.ast.expr.graph.EdgesFromIndexAccessFromToAsArrayExprNode;
	using EdgesFromIndexAccessMultipleFromToExprNode = de.unika.ipd.grgen.ast.expr.graph.EdgesFromIndexAccessMultipleFromToExprNode;
	using EdgesFromIndexAccessSameExprNode = de.unika.ipd.grgen.ast.expr.graph.EdgesFromIndexAccessSameExprNode;
	using IncidentEdgeExprNode = de.unika.ipd.grgen.ast.expr.graph.IncidentEdgeExprNode;
	using NodesExprNode = de.unika.ipd.grgen.ast.expr.graph.NodesExprNode;
	using NodesFromIndexAccessFromToAsArrayExprNode = de.unika.ipd.grgen.ast.expr.graph.NodesFromIndexAccessFromToAsArrayExprNode;
	using NodesFromIndexAccessMultipleFromToExprNode = de.unika.ipd.grgen.ast.expr.graph.NodesFromIndexAccessMultipleFromToExprNode;
	using NodesFromIndexAccessSameExprNode = de.unika.ipd.grgen.ast.expr.graph.NodesFromIndexAccessSameExprNode;
	using ReachableEdgeExprNode = de.unika.ipd.grgen.ast.expr.graph.ReachableEdgeExprNode;
	using ReachableNodeExprNode = de.unika.ipd.grgen.ast.expr.graph.ReachableNodeExprNode;
	using FunctionInvocationDecisionNode = de.unika.ipd.grgen.ast.expr.invocation.FunctionInvocationDecisionNode;
	using FunctionOrBuiltinFunctionInvocationBaseNode = de.unika.ipd.grgen.ast.expr.invocation.FunctionOrBuiltinFunctionInvocationBaseNode;
	using IndexFunctionInvocationDecisionNode = de.unika.ipd.grgen.ast.expr.invocation.IndexFunctionInvocationDecisionNode;
	using EvalStatementNode = de.unika.ipd.grgen.ast.stmt.EvalStatementNode;
	using EvalStatement = de.unika.ipd.grgen.ir.stmt.EvalStatement;
	using ForFunction = de.unika.ipd.grgen.ir.stmt.graph.ForFunction;
	using IR = de.unika.ipd.grgen.ir.IR;
	using Expression = de.unika.ipd.grgen.ir.expr.Expression;
	using Variable = de.unika.ipd.grgen.ir.pattern.Variable;
	using Coords = de.unika.ipd.grgen.parser.Coords;

	/// <summary>
	/// AST node representing a for lookup of a neighborhood function.
	/// </summary>
	public class ForFunctionNode : ForGraphQueryNode
	{
		static ForFunctionNode()
		{
			SetClassName(typeof(ForFunctionNode), "ForFunction");
		}

		internal FunctionInvocationDecisionNode function;
		internal IndexFunctionInvocationDecisionNode indexFunction;


		public ForFunctionNode(Coords coords, BaseNode iterationVariable, FunctionOrBuiltinFunctionInvocationBaseNode function,
				CollectNode<EvalStatementNode> loopedStatements)
			: base(coords, iterationVariable, loopedStatements)
		{
			if(function is FunctionInvocationDecisionNode)
				this.function = BecomeParent((FunctionInvocationDecisionNode)function);
			else
				this.indexFunction = BecomeParent((IndexFunctionInvocationDecisionNode)function);
		}

		/// <summary>
		/// returns children of this node </summary>
		public override ICollection<BaseNode> Children
		{
			get
			{
				IList<BaseNode> children = new List<BaseNode>();
				children.Add(GetValidVersion(iterationVariableUnresolved, iterationVariable));
				children.Add(ValidFunction);
				children.Add(statements);
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
				childrenNames.Add("iterationVariable");
				childrenNames.Add("function");
				childrenNames.Add("loopedStatements");
				return childrenNames;
			}
		}

		/// <seealso cref="de.unika.ipd.grgen.ast.BaseNode.resolveLocal() "/>
		protected internal override bool ResolveLocal()
		{
			return ResolveIterationVariable("function");
		}

		protected internal override bool CheckLocal()
		{
			if(!CheckIterationVariable("function"))
				return false;

			if(function != null)
			{
				if(function.Result is IncidentEdgeExprNode)
					return true;
				else if(function.Result is AdjacentNodeExprNode)
					return true;
				else if(function.Result is ReachableEdgeExprNode)
					return true;
				else if(function.Result is ReachableNodeExprNode)
					return true;
				else if(function.Result is BoundedReachableEdgeExprNode)
					return true;
				else if(function.Result is BoundedReachableNodeExprNode)
					return true;
				else if(function.Result is NodesExprNode)
					return true;
				else if(function.Result is EdgesExprNode)
					return true;
				else
				{
					ReportError("Unkonwn function " + function.functionIdent + " in for function loop"
							+ " (expected is one of "
							+ "incident, incoming, outgoing, "
							+ "adjacent, adjacentIncoming, adjacentOutgoing, "
							+ "reachableEdges, reachableEdgesIncoming, reachableEdgesOutgoing, "
							+ "reachable, reachableIncoming, reachableOutgoing, "
							+ "boundedReachableEdges, boundedReachableEdgesIncoming, boundedReachableEdgesOutgoing, "
							+ "boundedReachable, boundedReachableIncoming, boundedReachableOutgoing, "
							+ "nodes, edges"
							+ ").");
					return false;
				}
			}
			else
			{
				if(indexFunction.Result is NodesFromIndexAccessSameExprNode)
					return true;
				else if(indexFunction.Result is EdgesFromIndexAccessSameExprNode)
					return true;
				else if(indexFunction.Result is NodesFromIndexAccessFromToAsArrayExprNode)
					return true;
				else if(indexFunction.Result is EdgesFromIndexAccessFromToAsArrayExprNode)
					return true;
				else if(indexFunction.Result is NodesFromIndexAccessMultipleFromToExprNode)
					return true;
				else if(indexFunction.Result is EdgesFromIndexAccessMultipleFromToExprNode)
					return true;
				else
				{
					ReportError("Unkonwn index function " + function.functionIdent + " in for function loop"
							+ " (expected is one of "
							+ "nodesFromIndexSame, edgesFromIndexSame, "
							+ "nodesFromIndexAscending, nodesFromIndexDescending, edgesFromIndexAscending, edgesFromIndexDescending, "
							+ "nodesFromIndexFromAscending, nodesFromIndexFromExclusiveAscending, nodesFromIndexToAscending, nodesFromIndexToExclusiveAscending, "
							+ "nodesFromIndexFromDescending, nodesFromIndexFromExclusiveDescending, nodesFromIndexToDescending, nodesFromIndexToExclusiveDescending, "
							+ "nodesFromIndexFromToAscending, nodesFromIndexFromExclusiveToAscending, nodesFromIndexFromToExclusiveAscending, nodesFromIndexFromExclusiveToExclusiveAscending, "
							+ "nodesFromIndexFromToDescending, nodesFromIndexFromExclusiveToDescending, nodesFromIndexFromToExclusiveDescending, nodesFromIndexFromExclusiveToExclusiveDescending, "
							+ "edgesFromIndexFromAscending, edgesFromIndexFromExclusiveAscending, edgesFromIndexToAscending, edgesFromIndexToExclusiveAscending, "
							+ "edgesFromIndexFromDescending, edgesFromIndexFromExclusiveDescending, edgesFromIndexToDescending, edgesFromIndexToExclusiveDescending, "
							+ "edgesFromIndexFromToAscending, edgesFromIndexFromExclusiveToAscending, edgesFromIndexFromToExclusiveAscending, edgesFromIndexFromExclusiveToExclusiveAscending, "
							+ "edgesFromIndexFromToDescending, edgesFromIndexFromExclusiveToDescending, edgesFromIndexFromToExclusiveDescending, edgesFromIndexFromExclusiveToExclusiveDescending, "
							+ "nodesFromIndexMultipleFromTo, edgesFromIndexMultipleFromTo"
							+ ").");
					return false;
				}
			}
		}

		public override bool CheckStatementLocal(bool isLHS, DeclNode root, EvalStatementNode enclosingLoop)
		{
			return true;
		}

		protected internal virtual FunctionOrBuiltinFunctionInvocationBaseNode ValidFunction
		{
			get
			{
				return function != null ? (FunctionOrBuiltinFunctionInvocationBaseNode)function : (FunctionOrBuiltinFunctionInvocationBaseNode)indexFunction;
			}
		}

		protected internal override IR ConstructIR()
		{
			ForFunction ff = new ForFunction(iterationVariable.CheckIR<Variable>(typeof(Variable)), ValidFunction.CheckIR<Expression>(typeof(Expression)));
			foreach(EvalStatementNode accumulationStatement in statements.ChildrenExact)
				ff.AddLoopedStatement(accumulationStatement.CheckIR<EvalStatement>(typeof(EvalStatement)));
			return ff;
		}
	}

}
