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
	using BasicTypeNode = de.unika.ipd.grgen.ast.type.basic.BasicTypeNode;
	using IR = de.unika.ipd.grgen.ir.IR;
	using Expression = de.unika.ipd.grgen.ir.expr.Expression;
	using CountReachableEdgeExpr = de.unika.ipd.grgen.ir.expr.graph.CountReachableEdgeExpr;
	using Coords = de.unika.ipd.grgen.parser.Coords;
	using Direction = de.unika.ipd.grgen.util.Direction;

	/// <summary>
	/// A node yielding the count of the reachable incident/incoming/outgoing edges of a node.
	/// </summary>
	public class CountReachableEdgeExprNode : NeighborhoodQueryExprNode
	{
		static CountReachableEdgeExprNode()
		{
			SetClassName(typeof(CountReachableEdgeExprNode), "count reachable edge expr");
		}

		public CountReachableEdgeExprNode(Coords coords,
				ExprNode startNodeExpr,
				ExprNode incidentTypeExpr, Direction direction,
				ExprNode adjacentTypeExpr)
			: base(coords, startNodeExpr, incidentTypeExpr, direction, adjacentTypeExpr)
		{
		}

		/// <seealso cref="de.unika.ipd.grgen.ast.BaseNode.resolveLocal() "/>
		protected internal override bool ResolveLocal()
		{
			return Type.Resolve();
		}

		protected internal override string ShortSignature()
		{
			return "countReachableEdges(.,.,.)";
		}

		protected internal override IR ConstructIR()
		{
			startNodeExpr = startNodeExpr.Evaluate();
			incidentTypeExpr = incidentTypeExpr.Evaluate();
			adjacentTypeExpr = adjacentTypeExpr.Evaluate();
			// assumes that the direction:int of the AST node uses the same values as the direction of the IR expression
			return new CountReachableEdgeExpr(startNodeExpr.CheckIR<Expression>(typeof(Expression)),
					incidentTypeExpr.CheckIR<Expression>(typeof(Expression)), direction,
					adjacentTypeExpr.CheckIR<Expression>(typeof(Expression)));
		}

		public override TypeNode Type
		{
			get
			{
				return BasicTypeNode.intType;
			}
		}
	}

}
