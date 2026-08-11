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
using MapRemoveItem = de.unika.ipd.grgen.ir.stmt.map.MapRemoveItem;
using MapVarRemoveItem = de.unika.ipd.grgen.ir.stmt.map.MapVarRemoveItem;
using Coords = de.unika.ipd.grgen.parser.Coords;

public class MapRemoveItemNode : MapProcedureMethodInvocationBaseNode
{
	static MapRemoveItemNode()
	{
		SetClassName(typeof(MapRemoveItemNode), "map remove item statement");
	}

	private ExprNode keyExpr;

	public MapRemoveItemNode(Coords coords, QualIdentNode target, ExprNode keyExpr)
		: base(coords, target)
	{
		this.keyExpr = BecomeParent(keyExpr);
	}

	public MapRemoveItemNode(Coords coords, VarDeclNode targetVar, ExprNode keyExpr)
		: base(coords, targetVar)
	{
		this.keyExpr = BecomeParent(keyExpr);
	}

	public override ICollection<BaseNode> Children
	{
		get
		{
		IList<BaseNode> children = new List<BaseNode>();
		children.Add(ValidTarget);
		children.Add(keyExpr);
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
		return childrenNames;
		}
	}

	protected internal override bool ResolveLocal()
	{
		// target type already checked during resolving into this node
		return true;
	}

	protected internal override bool CheckLocal()
	{
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
					keyExprOld.ReportError("The map rem item procedure expects as argument (key)"
							+ " a value of type " + targetKeyType.ToStringWithDeclarationCoords()
							+ " (but is given a value of type " + keyType.ToStringWithDeclarationCoords() + ").");
					return false;
				}
			}
			return true;
		}
		else
		{
			TypeNode targetKeyType = targetType.keyType;
			return CheckType(keyExpr, targetKeyType, "map rem item procedure", "key");
		}
	}

	protected internal override IR ConstructIR()
	{
		keyExpr = keyExpr.Evaluate();
		if(target != null)
		{
			return new MapRemoveItem(target.CheckIR(typeof(Qualification)),
					keyExpr.CheckIR(typeof(Expression)));
		}
		else
		{
			return new MapVarRemoveItem(targetVar.CheckIR(typeof(Variable)),
					keyExpr.CheckIR(typeof(Expression)));
		}
	}
}

}
