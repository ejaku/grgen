/*
 * GrGen: graph rewrite generator tool -- release GrGen.NET 8.1
 * Copyright (C) 2003-2026 Universitaet Karlsruhe, Institut fuer Programmstrukturen und Datenorganisation, LS Goos; and free programmers
 * licensed under LGPL v3, some components/parts use different licenses (see LICENSE.txt included in the packaging of this file)
 * www.grgen.de / www.grgen.net
 */

/// <summary>
/// @author Edgar Jakumeit
/// </summary>

namespace de.unika.ipd.grgen.ast.stmt.set
{

	using System.Collections.Generic;

	using de.unika.ipd.grgen.ast;
	using VarDeclNode = de.unika.ipd.grgen.ast.decl.pattern.VarDeclNode;
	using ConstNode = de.unika.ipd.grgen.ast.expr.ConstNode;
	using ExprNode = de.unika.ipd.grgen.ast.expr.ExprNode;
	using QualIdentNode = de.unika.ipd.grgen.ast.expr.QualIdentNode;
	using TypeNode = de.unika.ipd.grgen.ast.type.TypeNode;
	using SetTypeNode = de.unika.ipd.grgen.ast.type.container.SetTypeNode;
	using IR = de.unika.ipd.grgen.ir.IR;
	using Expression = de.unika.ipd.grgen.ir.expr.Expression;
	using Qualification = de.unika.ipd.grgen.ir.expr.Qualification;
	using Variable = de.unika.ipd.grgen.ir.pattern.Variable;
	using SetAddItem = de.unika.ipd.grgen.ir.stmt.set.SetAddItem;
	using SetVarAddItem = de.unika.ipd.grgen.ir.stmt.set.SetVarAddItem;
	using Coords = de.unika.ipd.grgen.parser.Coords;

	public class SetAddItemNode : SetProcedureMethodInvocationBaseNode
	{
		static SetAddItemNode()
		{
			SetClassName(typeof(SetAddItemNode), "set add item statement");
		}

		private ExprNode valueExpr;

		public SetAddItemNode(Coords coords, QualIdentNode target, ExprNode valueExpr)
			: base(coords, target)
		{
			this.valueExpr = BecomeParent(valueExpr);
		}

		public SetAddItemNode(Coords coords, VarDeclNode targetVar, ExprNode valueExpr)
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
			SetTypeNode targetType = TargetTypeExact;
			if(target != null)
			{
				TypeNode targetValueType = targetType.valueType;
				TypeNode valueType = valueExpr.Type;
				if(!valueType.IsEqual(targetValueType))
				{
					ExprNode valueExprOld = valueExpr;
					valueExpr = BecomeParent(valueExpr.AdjustType(targetValueType, Coords));
					if(valueExpr == ConstNode.Invalid)
					{
						valueExprOld.ReportError("The set add item procedure expects as argument (value)"
								+ " a value of type " + targetValueType.ToStringWithDeclarationCoords()
								+ " (but is given a value of type " + valueType.ToStringWithDeclarationCoords() + ").");
						return false;
					}
				}
				return true;
			}
			else
			{
				TypeNode targetValueType = targetType.valueType;
				return CheckType(valueExpr, targetValueType, "set add item procedure", "value");
			}
		}

		protected internal override IR ConstructIR()
		{
			valueExpr = valueExpr.Evaluate();
			if(target != null)
			{
				return new SetAddItem(target.CheckIR<Qualification>(typeof(Qualification)),
						valueExpr.CheckIR<Expression>(typeof(Expression)));
			}
			else
			{
				return new SetVarAddItem(targetVar.CheckIR<Variable>(typeof(Variable)),
						valueExpr.CheckIR<Expression>(typeof(Expression)));
			}
		}
	}

}
