/*
 * GrGen: graph rewrite generator tool -- release GrGen.NET 8.1
 * Copyright (C) 2003-2026 Universitaet Karlsruhe, Institut fuer Programmstrukturen und Datenorganisation, LS Goos; and free programmers
 * licensed under LGPL v3, some components/parts use different licenses (see LICENSE.txt included in the packaging of this file)
 * www.grgen.de / www.grgen.net
 */

/// <summary>
/// @author Sebastian Hack
/// </summary>

namespace de.unika.ipd.grgen.ast.model.decl
{

using System.Collections.Generic;
using System.Diagnostics;

using de.unika.ipd.grgen.ast;
using ConstNode = de.unika.ipd.grgen.ast.expr.ConstNode;
using EnumConstNode = de.unika.ipd.grgen.ast.expr.EnumConstNode;
using EnumExprNode = de.unika.ipd.grgen.ast.expr.EnumExprNode;
using ExprNode = de.unika.ipd.grgen.ast.expr.ExprNode;
using InvalidConstNode = de.unika.ipd.grgen.ast.expr.InvalidConstNode;
using EnumTypeNode = de.unika.ipd.grgen.ast.model.type.EnumTypeNode;
using TypeNode = de.unika.ipd.grgen.ast.type.TypeNode;
using BasicTypeNode = de.unika.ipd.grgen.ast.type.basic.BasicTypeNode;
using de.unika.ipd.grgen.ast.util;
using IR = de.unika.ipd.grgen.ir.IR;
using EnumItem = de.unika.ipd.grgen.ir.model.EnumItem;
using Walkable = de.unika.ipd.grgen.util.Walkable;

/// <summary>
/// A class for enum items.
/// </summary>
public class EnumItemDeclNode : MemberDeclNode
{
	static EnumItemDeclNode()
	{
		SetClassName(typeof(EnumItemDeclNode), "enum item decl");
	}

	private ExprNode value;
	private EnumConstNode constValue;

	/// <summary>
	/// Position of this item in the enum. </summary>
	private readonly int pos;

	/// <summary>
	/// Make a new enum item decl node.
	/// </summary>
	public EnumItemDeclNode(IdentNode identifier, IdentNode type, ExprNode value, int pos)
		: base(identifier, type, true)
	{
		this.value = value;
		BecomeParent(this.value);
		this.pos = pos;
	}

	/// <summary>
	/// returns children of this node </summary>
	public override ICollection<BaseNode> Children
	{
		get
		{
			IList<BaseNode> children = new List<BaseNode>();
			children.Add(ident);
			children.Add(GetValidVersion(typeUnresolved, type));
			children.Add(value);
			return children;
		}
	}

	/// <summary>
	/// returns names of the children, same order as in getChildren </summary>
	public override ICollection<string> ChildrenNames
	{
		get
		{
			IList<string> childrenNames = new List<string>();
			childrenNames.Add("ident");
			childrenNames.Add("type");
			childrenNames.Add("value");
			return childrenNames;
		}
	}

	private static readonly DeclarationTypeResolver<EnumTypeNode> typeResolver =
			new DeclarationTypeResolver<EnumTypeNode>(typeof(EnumTypeNode));

	/// <seealso cref="de.unika.ipd.grgen.ast.BaseNode.resolveLocal() "/>
	protected internal override bool ResolveLocal()
	{
		type = typeResolver.Resolve(typeUnresolved, this);
		return type != null;
	}

	/// <summary>
	/// Check the validity of the initialisation expression. </summary>
	/// <returns> true, if the init expression is ok, false if not. </returns>
	protected internal override bool CheckLocal()
	{
		bool res = base.CheckLocal();
		// Check, if this enum item was defined with a latter one.
		// This may not be.
		HashSet<EnumItemDeclNode> visitedEnumItems = new HashSet<EnumItemDeclNode>();
		if(!CheckValue(value, visitedEnumItems))
			return false;

		ExprNode newValue = value.Evaluate();
		if(!(newValue is ConstNode))
		{
			ReportError("The enum item " + ident + " expects a constant initialization expression.");
			return false;
		}

		// Adjust the values type to int, else emit an error.
		if(!newValue.Type.IsCompatibleTo(BasicTypeNode.intType))
		{
			ReportError("The enum item " + ident + " expects an initialization expression of type int, but is given an expression of type " + newValue.Type.TypeName + ".");
			return false;
		}

		newValue = ((ConstNode)newValue).CastTo(BasicTypeNode.intType);
		if(value != newValue)
		{
			if(newValue is InvalidConstNode)
			{
				ReportError("The enum item " + ident + " cannot be casted to int (INTERNAL FAILURE).");
				return false;
			}
			SwitchParenthood(value, newValue);
			value = newValue;
		}

		return res;
	}

	/// <summary>
	/// Used to check the value of an EnumItemNode for circular dependencies
	/// and accesses to enum item declared after use
	/// @returns false, if an illegal use has been found
	/// </summary>
	private bool CheckValue(Walkable cur, HashSet<EnumItemDeclNode> visitedEnumItems)
	{
		EnumItemDeclNode enumItem = null;
		if(cur is EnumItemDeclNode)
		{
			enumItem = (EnumItemDeclNode)cur;
			if(pos == enumItem.pos)
			{
				ReportError("The enum item " + ident + " is not allowed to depend on its own value.");
				return false;
			}
			else if(pos < enumItem.pos)
			{
				ReportError("The enum item " + ident + " is not allowed to depend on a following one.");
				return false;
			}
			else if(visitedEnumItems.Contains(enumItem))
			{
				ReportError("Circular dependency found on value of enum item " + enumItem.Ident + ".");
				return false;
			}
			visitedEnumItems.Add(enumItem);
		}
		else if(cur is EnumTypeNode) // EnumTypeNode has all EnumItemNodes as children => don't check them
			return true;
		else if(cur is EnumExprNode) // Enum item from another, already declared enum => skip it
			return true;

		foreach(Walkable child in cur.WalkableChildren)
		{
			if(!CheckValue(child, visitedEnumItems))
				return false;
		}

		// If cur is an EnumItemNode, mark it as unvisited again
		// (needed for "a, b, c = a * b")
		if(enumItem != null)
			visitedEnumItems.Remove(enumItem);

		return true;
	}

	/// <returns> The type node of the declaration </returns>
	public override TypeNode DeclType
	{
		get
		{
			Debug.Assert(IsResolved());

			return type;
		}
	}

	public virtual ExprNode Value
	{
		get
		{
			if(constValue != null)
				return constValue;

			if(!(value is ConstNode))
				return value;

			object obj = ((ConstNode)value).Value;
			int v = ((int?)obj).Value;
			debug.Report(NOTE, "result: " + value);

			constValue = new EnumConstNode(Coords, Ident, v);
			return constValue;
		}
	}

	public EnumItem Item
	{
		get
		{
			return CheckIR(typeof(EnumItem));
		}
	}

	/// <seealso cref="de.unika.ipd.grgen.ast.BaseNode.constructIR()"/>
	protected internal override IR ConstructIR()
	{
		EnumConstNode c = (EnumConstNode)Value;

		return new EnumItem(ident.IRIdent, c.IREnumExpression);
	}

	public static string KindStr
	{
		get
		{
			return "enum item";
		}
	}
}

}
