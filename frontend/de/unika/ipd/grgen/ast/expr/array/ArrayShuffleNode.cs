/*
 * GrGen: graph rewrite generator tool -- release GrGen.NET 8.1
 * Copyright (C) 2003-2026 Universitaet Karlsruhe, Institut fuer Programmstrukturen und Datenorganisation, LS Goos; and free programmers
 * licensed under LGPL v3, some components/parts use different licenses (see LICENSE.txt included in the packaging of this file)
 * www.grgen.de / www.grgen.net
 */

/// <summary>
/// @author Edgar Jakumeit
/// </summary>

namespace de.unika.ipd.grgen.ast.expr.array
{
	using ExprNode = de.unika.ipd.grgen.ast.expr.ExprNode;
	using TypeNode = de.unika.ipd.grgen.ast.type.TypeNode;
	using Expression = de.unika.ipd.grgen.ir.expr.Expression;
	using ArrayShuffleExpr = de.unika.ipd.grgen.ir.expr.array.ArrayShuffleExpr;
	using IR = de.unika.ipd.grgen.ir.IR;
	using Coords = de.unika.ipd.grgen.parser.Coords;

	public class ArrayShuffleNode : ArrayFunctionMethodInvocationBaseExprNode
	{
		static ArrayShuffleNode()
		{
			SetClassName(typeof(ArrayShuffleNode), "array shuffle");
		}

		public ArrayShuffleNode(Coords coords, ExprNode targetExpr)
			: base(coords, targetExpr)
		{
		}

		public override TypeNode Type
		{
			get
			{
				return TargetType;
			}
		}

		protected internal override IR ConstructIR()
		{
			targetExpr = targetExpr.Evaluate();
			return new ArrayShuffleExpr(targetExpr.CheckIR(typeof(Expression)));
		}
	}

}
