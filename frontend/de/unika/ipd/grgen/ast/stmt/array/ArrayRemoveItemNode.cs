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
using ConstNode = de.unika.ipd.grgen.ast.expr.ConstNode;
using ExprNode = de.unika.ipd.grgen.ast.expr.ExprNode;
using QualIdentNode = de.unika.ipd.grgen.ast.expr.QualIdentNode;
using TypeNode = de.unika.ipd.grgen.ast.type.TypeNode;
using IntTypeNode = de.unika.ipd.grgen.ast.type.basic.IntTypeNode;
using IR = de.unika.ipd.grgen.ir.IR;
using Expression = de.unika.ipd.grgen.ir.expr.Expression;
using Qualification = de.unika.ipd.grgen.ir.expr.Qualification;
using Variable = de.unika.ipd.grgen.ir.pattern.Variable;
using ArrayRemoveItem = de.unika.ipd.grgen.ir.stmt.array.ArrayRemoveItem;
using ArrayVarRemoveItem = de.unika.ipd.grgen.ir.stmt.array.ArrayVarRemoveItem;
using Coords = de.unika.ipd.grgen.parser.Coords;

public class ArrayRemoveItemNode : ArrayProcedureMethodInvocationBaseNode
{
	static ArrayRemoveItemNode()
	{
		SetClassName(typeof(ArrayRemoveItemNode), "array remove item statement");
	}

	private ExprNode valueExpr;

	public ArrayRemoveItemNode(Coords coords, QualIdentNode target, ExprNode valueExpr)
		: base(coords, target)
	{
		if(valueExpr != null)
			this.valueExpr = BecomeParent(valueExpr);
	}

	public ArrayRemoveItemNode(Coords coords, VarDeclNode targetVar, ExprNode valueExpr)
		: base(coords, targetVar)
	{
		if(valueExpr != null)
			this.valueExpr = BecomeParent(valueExpr);
	}

	public override ICollection<BaseNode> Children
	{
		get
		{
		IList<BaseNode> children = new List<BaseNode>();
		children.Add(ValidTarget);
		if(valueExpr != null)
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
		if(valueExpr != null)
			childrenNames.Add("valueExpr");
		return childrenNames;
		}
	}

	protected internal override bool CheckLocal()
	{
		// target type already checked during resolving into this node
		//ArrayTypeNode targetType = getTargetType();
		if(target != null)
		{
			//TypeNode targetValueType = targetType.valueType;
			if(valueExpr != null)
			{
				TypeNode valueType = valueExpr.Type;
				if(!valueType.IsEqual(IntTypeNode.intType))
				{
					ExprNode valueExprOld = valueExpr;
					valueExpr = BecomeParent(valueExpr.AdjustType(IntTypeNode.intType, Coords));
					if(valueExpr == ConstNode.Invalid)
					{
						valueExprOld.ReportError("The array rem item procedure expects as argument (index)"
								+ " a value of type int"
								+ " (but is given a value of type " + valueType.ToStringWithDeclarationCoords() + ").");
						return false;
					}
				}
			}
			return true;
		}
		else
		{
			//TypeNode targetValueType = targetType.valueType;
			if(valueExpr != null)
				return CheckType(valueExpr, IntTypeNode.intType, "index value", "array rem item procedure");
			else
				return true;
		}
	}

	protected internal override IR ConstructIR()
	{
		if(valueExpr != null)
			valueExpr = valueExpr.Evaluate();
		if(target != null)
		{
			return new ArrayRemoveItem(target.CheckIR(typeof(Qualification)),
					valueExpr != null ? valueExpr.CheckIR(typeof(Expression)) : null);
		}
		else
		{
			return new ArrayVarRemoveItem(targetVar.CheckIR(typeof(Variable)),
					valueExpr != null ? valueExpr.CheckIR(typeof(Expression)) : null);
		}
	}
}

}
