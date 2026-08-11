/*
 * GrGen: graph rewrite generator tool -- release GrGen.NET 8.1
 * Copyright (C) 2003-2026 Universitaet Karlsruhe, Institut fuer Programmstrukturen und Datenorganisation, LS Goos; and free programmers
 * licensed under LGPL v3, some components/parts use different licenses (see LICENSE.txt included in the packaging of this file)
 * www.grgen.de / www.grgen.net
 */

namespace de.unika.ipd.grgen.ast.type
{

using System.Collections.Generic;
using System.Diagnostics;

using BaseNode = de.unika.ipd.grgen.ast.BaseNode;
using IR = de.unika.ipd.grgen.ir.IR;
using TypeExpr = de.unika.ipd.grgen.ir.type.TypeExpr;
using TypeExprSetOperator = de.unika.ipd.grgen.ir.type.TypeExprSetOperator;
using Coords = de.unika.ipd.grgen.parser.Coords;

/// <summary>
/// AST node representing binary type expressions.
/// </summary>
public class TypeBinaryExprNode : TypeExprNode
{
	static TypeBinaryExprNode()
	{
		SetClassName(typeof(TypeBinaryExprNode), "type binary expr");
	}

	private TypeExprNode lhs;
	private TypeExprNode rhs;

	public TypeBinaryExprNode(Coords coords, TypeOperator op, TypeExprNode op0, TypeExprNode op1)
		: base(coords, op)
	{
		this.lhs = op0;
		BecomeParent(this.lhs);
		this.rhs = op1;
		BecomeParent(this.rhs);
	}

	/// <summary>
	/// returns children of this node </summary>
	public override ICollection<BaseNode> Children
	{
		get
		{
			IList<BaseNode> children = new List<BaseNode>();
			children.Add(lhs);
			children.Add(rhs);
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
			childrenNames.Add("lhs");
			childrenNames.Add("rhs");
			return childrenNames;
		}
	}

	/// <seealso cref="de.unika.ipd.grgen.ast.BaseNode.resolveLocal() "/>
	protected internal override bool ResolveLocal()
	{
		return true;
	}

	protected internal override bool CheckLocal()
	{
		return true;
	}

	protected internal override IR ConstructIR()
	{
		TypeExpr lhs = this.lhs.CheckIR(typeof(TypeExpr));
		TypeExpr rhs = this.rhs.CheckIR(typeof(TypeExpr));

		TypeExprSetOperator expr = new TypeExprSetOperator(GetSetOperator(op));
		expr.AddOperand(lhs);
		expr.AddOperand(rhs);

		return expr;
	}

	private static TypeExprSetOperator.SetOperator GetSetOperator(TypeExprNode.TypeOperator op)
	{
		switch(op)
		{
		case de.unika.ipd.grgen.ast.type.TypeExprNode.TypeOperator.UNION:
			return TypeExprSetOperator.SetOperator.UNION;
		case de.unika.ipd.grgen.ast.type.TypeExprNode.TypeOperator.DIFFERENCE:
			return TypeExprSetOperator.SetOperator.DIFFERENCE;
		case de.unika.ipd.grgen.ast.type.TypeExprNode.TypeOperator.INTERSECT:
			return TypeExprSetOperator.SetOperator.INTERSECT;
		default: // case SET - not used, only the set operators are mapped, internal error
			Debug.Assert((false));
			return TypeExprSetOperator.SetOperator.UNION;
		}
	}
}

}
