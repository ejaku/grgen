/*
 * GrGen: graph rewrite generator tool -- release GrGen.NET 8.1
 * Copyright (C) 2003-2026 Universitaet Karlsruhe, Institut fuer Programmstrukturen und Datenorganisation, LS Goos; and free programmers
 * licensed under LGPL v3, some components/parts use different licenses (see LICENSE.txt included in the packaging of this file)
 * www.grgen.de / www.grgen.net
 */

/// <summary>
/// @author Edgar Jakumeit
/// </summary>

namespace de.unika.ipd.grgen.ast.stmt.array
{

	using System.Collections.Generic;

	using de.unika.ipd.grgen.ast;
	using VarDeclNode = de.unika.ipd.grgen.ast.decl.pattern.VarDeclNode;
	using ExprNode = de.unika.ipd.grgen.ast.expr.ExprNode;
	using ArrayTypeNode = de.unika.ipd.grgen.ast.type.container.ArrayTypeNode;
	using IR = de.unika.ipd.grgen.ir.IR;
	using Expression = de.unika.ipd.grgen.ir.expr.Expression;
	using Variable = de.unika.ipd.grgen.ir.pattern.Variable;
	using ArrayVarAddAll = de.unika.ipd.grgen.ir.stmt.array.ArrayVarAddAll;
	using Coords = de.unika.ipd.grgen.parser.Coords;

	public class ArrayAddAllNode : ArrayProcedureMethodInvocationBaseNode
	{
		static ArrayAddAllNode()
		{
			SetClassName(typeof(ArrayAddAllNode), "array add all statement");
		}

		private ExprNode valueExpr;

		public ArrayAddAllNode(Coords coords, VarDeclNode targetVar, ExprNode valueExpr)
			: base(coords, targetVar)
		{
			this.valueExpr = BecomeParent(valueExpr);
		}

		public override ICollection<BaseNode> Children
		{
			get
			{
				IList<BaseNode> children = new List<BaseNode>();
				children.Add(ValidTarget);
				children.Add(valueExpr);
				return children;
			}
		}

		public override ICollection<string> ChildrenNames
		{
			get
			{
				IList<string> childrenNames = new List<string>();
				childrenNames.Add("target");
				childrenNames.Add("valueExpr");
				return childrenNames;
			}
		}

		protected internal override bool CheckLocal()
		{
			// target type already checked during resolving into this node
			ArrayTypeNode targetType = TargetTypeExact;
			bool success = true;
			success &= CheckType(valueExpr, targetType, "array add all statement", "value");
			return success;
		}

		protected internal override IR ConstructIR()
		{
			valueExpr = valueExpr.Evaluate();
			return new ArrayVarAddAll(targetVar.CheckIR<Variable>(typeof(Variable)), valueExpr.CheckIR<Expression>(typeof(Expression)));
		}
	}

}
