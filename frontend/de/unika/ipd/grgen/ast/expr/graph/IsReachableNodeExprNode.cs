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
	using IsReachableNodeExpr = de.unika.ipd.grgen.ir.expr.graph.IsReachableNodeExpr;
	using Coords = de.unika.ipd.grgen.parser.Coords;
	using Direction = de.unika.ipd.grgen.util.Direction;

	/// <summary>
	/// An ast node telling whether an end node can be reached from a start node, via incoming/outgoing/incident edges of given type, from/to a node of given type.
	/// </summary>
	public class IsReachableNodeExprNode : IsInNodeNeighborhoodQueryExprNode
	{
		static IsReachableNodeExprNode()
		{
			SetClassName(typeof(IsReachableNodeExprNode), "is reachable node expr");
		}

		public IsReachableNodeExprNode(Coords coords,
				ExprNode startNodeExpr, ExprNode endNodeExpr,
				ExprNode incidentTypeExpr, Direction direction,
				ExprNode adjacentTypeExpr)
			: base(coords, startNodeExpr, endNodeExpr, incidentTypeExpr, direction, adjacentTypeExpr)
		{
			this.endNodeExpr = endNodeExpr;
			BecomeParent(this.endNodeExpr);
		}

		/// <seealso cref="de.unika.ipd.grgen.ast.BaseNode.resolveLocal() "/>
		protected internal override bool ResolveLocal()
		{
			return true;
		}

		protected internal override string ShortSignature()
		{
			return "isReachableNode(.,.,.,.)";
		}

		protected internal override IR ConstructIR()
		{
			startNodeExpr = startNodeExpr.Evaluate();
			endNodeExpr = endNodeExpr.Evaluate();
			incidentTypeExpr = incidentTypeExpr.Evaluate();
			adjacentTypeExpr = adjacentTypeExpr.Evaluate();
			// assumes that the direction:int of the AST node uses the same values as the direction of the IR expression
			return new IsReachableNodeExpr(startNodeExpr.CheckIR<Expression>(typeof(Expression)),
					endNodeExpr.CheckIR<Expression>(typeof(Expression)),
					incidentTypeExpr.CheckIR<Expression>(typeof(Expression)), direction,
					adjacentTypeExpr.CheckIR<Expression>(typeof(Expression)),
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
