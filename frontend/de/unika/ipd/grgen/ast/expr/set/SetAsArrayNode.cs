/*
 * GrGen: graph rewrite generator tool -- release GrGen.NET 8.1
 * Copyright (C) 2003-2026 Universitaet Karlsruhe, Institut fuer Programmstrukturen und Datenorganisation, LS Goos; and free programmers
 * licensed under LGPL v3, some components/parts use different licenses (see LICENSE.txt included in the packaging of this file)
 * www.grgen.de / www.grgen.net
 */

/// <summary>
/// @author Edgar Jakumeit
/// </summary>

namespace de.unika.ipd.grgen.ast.expr.set
{
	using ExprNode = de.unika.ipd.grgen.ast.expr.ExprNode;
	using TypeNode = de.unika.ipd.grgen.ast.type.TypeNode;
	using ArrayTypeNode = de.unika.ipd.grgen.ast.type.container.ArrayTypeNode;
	using IR = de.unika.ipd.grgen.ir.IR;
	using Expression = de.unika.ipd.grgen.ir.expr.Expression;
	using SetAsArrayExpr = de.unika.ipd.grgen.ir.expr.set.SetAsArrayExpr;
	using Coords = de.unika.ipd.grgen.parser.Coords;

	public class SetAsArrayNode : SetFunctionMethodInvocationBaseExprNode
	{
		static SetAsArrayNode()
		{
			SetClassName(typeof(SetAsArrayNode), "set as array expression");
		}

		private ArrayTypeNode arrayTypeNode;

		public SetAsArrayNode(Coords coords, ExprNode targetExpr)
			: base(coords, targetExpr)
		{
		}

		protected internal override bool ResolveLocal()
		{
			// target type already checked during resolving into this node
			arrayTypeNode = new ArrayTypeNode(TargetTypeExact.valueTypeUnresolved);
			return arrayTypeNode.Resolve();
		}

		public override TypeNode Type
		{
			get
			{
				return arrayTypeNode;
			}
		}

		protected internal override IR ConstructIR()
		{
			targetExpr = targetExpr.Evaluate();
			return new SetAsArrayExpr(targetExpr.CheckIR(typeof(Expression)), Type.IRType);
		}
	}

}
