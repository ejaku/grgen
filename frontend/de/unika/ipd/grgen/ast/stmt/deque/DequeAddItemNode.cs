/*
 * GrGen: graph rewrite generator tool -- release GrGen.NET 8.1
 * Copyright (C) 2003-2026 Universitaet Karlsruhe, Institut fuer Programmstrukturen und Datenorganisation, LS Goos; and free programmers
 * licensed under LGPL v3, some components/parts use different licenses (see LICENSE.txt included in the packaging of this file)
 * www.grgen.de / www.grgen.net
 */

/// <summary>
/// @author Edgar Jakumeit
/// </summary>

namespace de.unika.ipd.grgen.ast.stmt.deque
{

using System.Collections.Generic;

using de.unika.ipd.grgen.ast;
using VarDeclNode = de.unika.ipd.grgen.ast.decl.pattern.VarDeclNode;
using ConstNode = de.unika.ipd.grgen.ast.expr.ConstNode;
using ExprNode = de.unika.ipd.grgen.ast.expr.ExprNode;
using QualIdentNode = de.unika.ipd.grgen.ast.expr.QualIdentNode;
using TypeNode = de.unika.ipd.grgen.ast.type.TypeNode;
using IntTypeNode = de.unika.ipd.grgen.ast.type.basic.IntTypeNode;
using DequeTypeNode = de.unika.ipd.grgen.ast.type.container.DequeTypeNode;
using IR = de.unika.ipd.grgen.ir.IR;
using Expression = de.unika.ipd.grgen.ir.expr.Expression;
using Qualification = de.unika.ipd.grgen.ir.expr.Qualification;
using Variable = de.unika.ipd.grgen.ir.pattern.Variable;
using DequeAddItem = de.unika.ipd.grgen.ir.stmt.deque.DequeAddItem;
using DequeVarAddItem = de.unika.ipd.grgen.ir.stmt.deque.DequeVarAddItem;
using Coords = de.unika.ipd.grgen.parser.Coords;

public class DequeAddItemNode : DequeProcedureMethodInvocationBaseNode
{
	static DequeAddItemNode()
	{
		SetClassName(typeof(DequeAddItemNode), "Deque add item statement");
	}

	private ExprNode valueExpr;
	private ExprNode indexExpr;

	public DequeAddItemNode(Coords coords, QualIdentNode target, ExprNode valueExpr, ExprNode indexExpr)
		: base(coords, target)
	{
		this.valueExpr = BecomeParent(valueExpr);
		if(indexExpr != null)
			this.indexExpr = BecomeParent(indexExpr);
	}

	public DequeAddItemNode(Coords coords, VarDeclNode targetVar, ExprNode valueExpr, ExprNode indexExpr)
		: base(coords, targetVar)
	{
		this.valueExpr = BecomeParent(valueExpr);
		if(indexExpr != null)
			this.indexExpr = BecomeParent(indexExpr);
	}

	public override ICollection<BaseNode> Children
	{
		get
		{
		IList<BaseNode> children = new List<BaseNode>();
		children.Add(ValidTarget);
		children.Add(valueExpr);
		if(indexExpr != null)
			children.Add(indexExpr);
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
		if(indexExpr != null)
			childrenNames.Add("indexExpr");
		return childrenNames;
		}
	}

	protected internal override bool CheckLocal()
	{
		// target type already checked during resolving into this node
		DequeTypeNode targetType = TargetTypeExact;
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
					valueExprOld.ReportError("The deque add item procedure expects as 1. argument (value)"
							+ " a value of type " + targetValueType.ToStringWithDeclarationCoords()
							+ " (but is given a value of type " + valueType.ToStringWithDeclarationCoords() + ").");
					return false;
				}
			}
			if(indexExpr != null)
			{
				TypeNode indexType = indexExpr.Type;
				if(!indexType.IsEqual(IntTypeNode.intType))
				{
					ExprNode indexExprOld = indexExpr;
					indexExpr = BecomeParent(indexExpr.AdjustType(IntTypeNode.intType, Coords));
					if(indexExpr == ConstNode.Invalid)
					{
						indexExprOld.ReportError("The deque add item procedure expects as 2. argument (index)"
								+ " a value of type int"
								+ " (but is given a value of type " + indexType.ToStringWithDeclarationCoords() + ").");
						return false;
					}
				}
			}
			return true;
		}
		else
		{
			bool success = true;
			TypeNode targetValueType = targetType.valueType;
			if(indexExpr != null)
				success &= CheckType(indexExpr, IntTypeNode.intType, "deque add item with index procedure", "index");
			success &= CheckType(valueExpr, targetValueType, "deque add item procedure", "value");
			return success;
		}
	}

	protected internal override IR ConstructIR()
	{
		valueExpr = valueExpr.Evaluate();
		if(indexExpr != null)
			indexExpr = indexExpr.Evaluate();
		if(target != null)
		{
			return new DequeAddItem(target.CheckIR(typeof(Qualification)),
					valueExpr.CheckIR(typeof(Expression)),
					indexExpr != null ? indexExpr.CheckIR(typeof(Expression)) : null);
		}
		else
		{
			return new DequeVarAddItem(targetVar.CheckIR(typeof(Variable)),
					valueExpr.CheckIR(typeof(Expression)),
					indexExpr != null ? indexExpr.CheckIR(typeof(Expression)) : null);
		}
	}
}

}
