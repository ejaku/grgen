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
	using DequeTypeNode = de.unika.ipd.grgen.ast.type.container.DequeTypeNode;
	using IR = de.unika.ipd.grgen.ir.IR;
	using Expression = de.unika.ipd.grgen.ir.expr.Expression;
	using ArrayAsDequeExpr = de.unika.ipd.grgen.ir.expr.array.ArrayAsDequeExpr;
	using Coords = de.unika.ipd.grgen.parser.Coords;

	public class ArrayAsDequeNode : ArrayFunctionMethodInvocationBaseExprNode
	{
		static ArrayAsDequeNode()
		{
			SetClassName(typeof(ArrayAsDequeNode), "array as deque expression");
		}

		private DequeTypeNode dequeTypeNode;

		public ArrayAsDequeNode(Coords coords, ExprNode targetExpr)
			: base(coords, targetExpr)
		{
		}

		protected internal override bool ResolveLocal()
		{
			// target type already checked during resolving into this node
			dequeTypeNode = new DequeTypeNode(TargetTypeExact.valueTypeUnresolved);
			return dequeTypeNode.Resolve();
		}

		public override TypeNode Type
		{
			get
			{
				return dequeTypeNode;
			}
		}

		protected internal override IR ConstructIR()
		{
			targetExpr = targetExpr.Evaluate();
			return new ArrayAsDequeExpr(targetExpr.CheckIR(typeof(Expression)), Type.IRType);
		}
	}

}
