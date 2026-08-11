/*
 * GrGen: graph rewrite generator tool -- release GrGen.NET 8.1
 * Copyright (C) 2003-2026 Universitaet Karlsruhe, Institut fuer Programmstrukturen und Datenorganisation, LS Goos; and free programmers
 * licensed under LGPL v3, some components/parts use different licenses (see LICENSE.txt included in the packaging of this file)
 * www.grgen.de / www.grgen.net
 */

namespace de.unika.ipd.grgen.ast.expr.graph
{
	using ExprNode = de.unika.ipd.grgen.ast.expr.ExprNode;
	using TypeNode = de.unika.ipd.grgen.ast.type.TypeNode;
	using BooleanTypeNode = de.unika.ipd.grgen.ast.type.basic.BooleanTypeNode;
	using IR = de.unika.ipd.grgen.ir.IR;
	using Expression = de.unika.ipd.grgen.ir.expr.Expression;
	using IsReachableEdgeExpr = de.unika.ipd.grgen.ir.expr.graph.IsReachableEdgeExpr;
	using Coords = de.unika.ipd.grgen.parser.Coords;
	using Direction = de.unika.ipd.grgen.util.Direction;

	/// <summary>
	/// An ast node telling whether an end edge can be reached from a start node, via incoming/outgoing/incident edges of given type, from/to a node of given type.
	/// </summary>
	public class IsReachableEdgeExprNode : IsInEdgeNeighborhoodQueryExprNode
	{
		static IsReachableEdgeExprNode()
		{
			SetClassName(typeof(IsReachableEdgeExprNode), "is reachable edge expr");
		}

		public IsReachableEdgeExprNode(Coords coords,
				ExprNode startNodeExpr, ExprNode endEdgeExpr,
				ExprNode incidentTypeExpr, Direction direction,
				ExprNode adjacentTypeExpr)
			: base(coords, startNodeExpr, endEdgeExpr, incidentTypeExpr, direction, adjacentTypeExpr)
		{
		}

		/// <seealso cref="de.unika.ipd.grgen.ast.BaseNode.resolveLocal() "/>
		protected internal override bool ResolveLocal()
		{
			return true;
		}

		protected internal override string ShortSignature()
		{
			return "isReachableEdge(.,.,.,.)";
		}

		protected internal override IR ConstructIR()
		{
			startNodeExpr = startNodeExpr.Evaluate();
			endEdgeExpr = endEdgeExpr.Evaluate();
			incidentTypeExpr = incidentTypeExpr.Evaluate();
			adjacentTypeExpr = adjacentTypeExpr.Evaluate();
			// assumes that the direction:int of the AST node uses the same values as the direction of the IR expression
			return new IsReachableEdgeExpr(startNodeExpr.CheckIR(typeof(Expression)),
					endEdgeExpr.CheckIR(typeof(Expression)),
					incidentTypeExpr.CheckIR(typeof(Expression)), direction,
					adjacentTypeExpr.CheckIR(typeof(Expression)),
					Type.IRType);
		}

		public override TypeNode Type
		{
			get
			{
				return BooleanTypeNode.booleanType;
			}
		}
	}

}
