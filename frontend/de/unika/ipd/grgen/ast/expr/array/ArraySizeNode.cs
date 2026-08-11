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
	using BasicTypeNode = de.unika.ipd.grgen.ast.type.basic.BasicTypeNode;
	using IR = de.unika.ipd.grgen.ir.IR;
	using Expression = de.unika.ipd.grgen.ir.expr.Expression;
	using ArraySizeExpr = de.unika.ipd.grgen.ir.expr.array.ArraySizeExpr;
	using Coords = de.unika.ipd.grgen.parser.Coords;

	public class ArraySizeNode : ArrayFunctionMethodInvocationBaseExprNode
	{
		static ArraySizeNode()
		{
			SetClassName(typeof(ArraySizeNode), "array size expression");
		}

		public ArraySizeNode(Coords coords, ExprNode targetExpr)
			: base(coords, targetExpr)
		{
		}

		public override TypeNode Type
		{
			get
			{
				return BasicTypeNode.intType;
			}
		}

		protected internal override IR ConstructIR()
		{
			targetExpr = targetExpr.Evaluate();
			return new ArraySizeExpr(targetExpr.CheckIR<Expression>(typeof(Expression)));
		}
	}

}
