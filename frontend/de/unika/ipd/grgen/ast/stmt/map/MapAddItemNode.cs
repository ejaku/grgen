/*
 * GrGen: graph rewrite generator tool -- release GrGen.NET 8.1
 * Copyright (C) 2003-2026 Universitaet Karlsruhe, Institut fuer Programmstrukturen und Datenorganisation, LS Goos; and free programmers
 * licensed under LGPL v3, some components/parts use different licenses (see LICENSE.txt included in the packaging of this file)
 * www.grgen.de / www.grgen.net
 */

/// <summary>
/// @author Edgar Jakumeit
/// </summary>

namespace de.unika.ipd.grgen.ast.stmt.map
{

	using System.Collections.Generic;

	using de.unika.ipd.grgen.ast;
	using VarDeclNode = de.unika.ipd.grgen.ast.decl.pattern.VarDeclNode;
	using ConstNode = de.unika.ipd.grgen.ast.expr.ConstNode;
	using ExprNode = de.unika.ipd.grgen.ast.expr.ExprNode;
	using QualIdentNode = de.unika.ipd.grgen.ast.expr.QualIdentNode;
	using TypeNode = de.unika.ipd.grgen.ast.type.TypeNode;
	using MapTypeNode = de.unika.ipd.grgen.ast.type.container.MapTypeNode;
	using IR = de.unika.ipd.grgen.ir.IR;
	using Expression = de.unika.ipd.grgen.ir.expr.Expression;
	using Qualification = de.unika.ipd.grgen.ir.expr.Qualification;
	using Variable = de.unika.ipd.grgen.ir.pattern.Variable;
	using MapAddItem = de.unika.ipd.grgen.ir.stmt.map.MapAddItem;
	using MapVarAddItem = de.unika.ipd.grgen.ir.stmt.map.MapVarAddItem;
	using Coords = de.unika.ipd.grgen.parser.Coords;

	public class MapAddItemNode : MapProcedureMethodInvocationBaseNode
	{
		static MapAddItemNode()
		{
			SetClassName(typeof(MapAddItemNode), "map add item statement");
		}

		private ExprNode keyExpr;
		private ExprNode valueExpr;

		public MapAddItemNode(Coords coords, QualIdentNode target, ExprNode keyExpr, ExprNode valueExpr)
			: base(coords, target)
		{
			this.keyExpr = BecomeParent(keyExpr);
			this.valueExpr = BecomeParent(valueExpr);
		}

		public MapAddItemNode(Coords coords, VarDeclNode targetVar, ExprNode keyExpr, ExprNode valueExpr)
			: base(coords, targetVar)
		{
			this.keyExpr = BecomeParent(keyExpr);
			this.valueExpr = BecomeParent(valueExpr);
		}

		public override ICollection<BaseNode> Children
		{
			get
			{
				IList<BaseNode> children = new List<BaseNode>();
				children.Add(ValidTarget);
				children.Add(keyExpr);
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
				childrenNames.Add("keyExpr");
				childrenNames.Add("valueExpr");
				return childrenNames;
			}
		}

		protected internal override bool CheckLocal()
		{
			// target type already checked during resolving into this node
			MapTypeNode targetType = TargetTypeExact;
			if(target != null)
			{
				TypeNode targetKeyType = targetType.keyType;
				TypeNode keyType = keyExpr.Type;
				if(!keyType.IsEqual(targetKeyType))
				{
					ExprNode keyExprOld = keyExpr;
					keyExpr = BecomeParent(keyExpr.AdjustType(targetKeyType, Coords));
					if(keyExpr == ConstNode.Invalid)
					{
						keyExprOld.ReportError("The map add item procedure expects as 1. argument (key)"
								+ " a value of type " + targetKeyType.ToStringWithDeclarationCoords()
								+ " (but is given a value of type " + keyType.ToStringWithDeclarationCoords() + ").");
						return false;
					}
				}
				TypeNode targetValueType = targetType.valueType;
				TypeNode valueType = valueExpr.Type;
				if(!valueType.IsEqual(targetValueType))
				{
					ExprNode valueExprOld = valueExpr;
					valueExpr = BecomeParent(valueExpr.AdjustType(targetValueType, Coords));
					if(valueExpr == ConstNode.Invalid)
					{
						valueExprOld.ReportError("The map add item procedure expects as 2. argument (value)"
								+ " a value of type " + targetValueType.ToStringWithDeclarationCoords()
								+ " (but is given a value of type " + valueType.ToStringWithDeclarationCoords() + ").");
						return false;
					}
				}
			}
			else
			{
				TypeNode targetKeyType = targetType.keyType;
				TypeNode targetValueType = targetType.valueType;
				return CheckType(keyExpr, targetKeyType, "map add item procedure", "key")
						&& CheckType(valueExpr, targetValueType, "map add item procedure", "value");
			}
			return true;
		}

		protected internal override IR ConstructIR()
		{
			keyExpr = keyExpr.Evaluate();
			valueExpr = valueExpr.Evaluate();
			if(target != null)
			{
				return new MapAddItem(target.CheckIR<Qualification>(typeof(Qualification)),
						keyExpr.CheckIR<Expression>(typeof(Expression)),
						valueExpr.CheckIR<Expression>(typeof(Expression)));
			}
			else
			{
				return new MapVarAddItem(targetVar.CheckIR<Variable>(typeof(Variable)),
						keyExpr.CheckIR<Expression>(typeof(Expression)),
						valueExpr.CheckIR<Expression>(typeof(Expression)));
			}
		}
	}

}
