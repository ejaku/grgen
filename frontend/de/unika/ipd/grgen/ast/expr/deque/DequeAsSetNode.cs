/*
 * GrGen: graph rewrite generator tool -- release GrGen.NET 8.1
 * Copyright (C) 2003-2026 Universitaet Karlsruhe, Institut fuer Programmstrukturen und Datenorganisation, LS Goos; and free programmers
 * licensed under LGPL v3, some components/parts use different licenses (see LICENSE.txt included in the packaging of this file)
 * www.grgen.de / www.grgen.net
 */

/// <summary>
/// @author Edgar Jakumeit
/// </summary>

namespace de.unika.ipd.grgen.ast.expr.deque
{
	using ExprNode = de.unika.ipd.grgen.ast.expr.ExprNode;
	using TypeNode = de.unika.ipd.grgen.ast.type.TypeNode;
	using SetTypeNode = de.unika.ipd.grgen.ast.type.container.SetTypeNode;
	using IR = de.unika.ipd.grgen.ir.IR;
	using Expression = de.unika.ipd.grgen.ir.expr.Expression;
	using DequeAsSetExpr = de.unika.ipd.grgen.ir.expr.deque.DequeAsSetExpr;
	using Coords = de.unika.ipd.grgen.parser.Coords;

	public class DequeAsSetNode : DequeFunctionMethodInvocationBaseExprNode
	{
		static DequeAsSetNode()
		{
			SetClassName(typeof(DequeAsSetNode), "deque as set expression");
		}

		private SetTypeNode setTypeNode;

		public DequeAsSetNode(Coords coords, ExprNode targetExpr)
			: base(coords, targetExpr)
		{
		}

		protected internal override bool ResolveLocal()
		{
			// target type already checked during resolving into this node
			setTypeNode = new SetTypeNode(TargetTypeExact.valueTypeUnresolved);
			return setTypeNode.Resolve();
		}

		public override TypeNode Type
		{
			get
			{
				return setTypeNode;
			}
		}

		protected internal override IR ConstructIR()
		{
			targetExpr = targetExpr.Evaluate();
			return new DequeAsSetExpr(targetExpr.CheckIR<Expression>(typeof(Expression)), Type.IRType);
		}
	}

}
