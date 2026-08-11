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
	using MapTypeNode = de.unika.ipd.grgen.ast.type.container.MapTypeNode;
	using IR = de.unika.ipd.grgen.ir.IR;
	using Expression = de.unika.ipd.grgen.ir.expr.Expression;
	using ArrayAsMapExpr = de.unika.ipd.grgen.ir.expr.array.ArrayAsMapExpr;
	using Coords = de.unika.ipd.grgen.parser.Coords;

	public class ArrayAsMapNode : ArrayFunctionMethodInvocationBaseExprNode
	{
		static ArrayAsMapNode()
		{
			SetClassName(typeof(ArrayAsMapNode), "array as map expression");
		}

		private MapTypeNode mapTypeNode;

		public ArrayAsMapNode(Coords coords, ExprNode targetExpr)
			: base(coords, targetExpr)
		{
		}

		protected internal override bool ResolveLocal()
		{
			// target type already checked during resolving into this node
			mapTypeNode = new MapTypeNode(BasicTypeNode.intType.GetIdent(),
					TargetTypeExact.valueTypeUnresolved);
			return mapTypeNode.Resolve();
		}

		public override TypeNode Type
		{
			get
			{
				return mapTypeNode;
			}
		}

		protected internal override IR ConstructIR()
		{
			targetExpr = targetExpr.Evaluate();
			return new ArrayAsMapExpr(targetExpr.CheckIR(typeof(Expression)), Type.IRType);
		}
	}

}
