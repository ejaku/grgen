/*
 * GrGen: graph rewrite generator tool -- release GrGen.NET 8.1
 * Copyright (C) 2003-2026 Universitaet Karlsruhe, Institut fuer Programmstrukturen und Datenorganisation, LS Goos; and free programmers
 * licensed under LGPL v3, some components/parts use different licenses (see LICENSE.txt included in the packaging of this file)
 * www.grgen.de / www.grgen.net
 */

/// <summary>
/// @author Rubino Geiss
/// </summary>
namespace de.unika.ipd.grgen.ast.model
{

using System.Collections.Generic;

using BaseNode = de.unika.ipd.grgen.ast.BaseNode;
using IdentNode = de.unika.ipd.grgen.ast.IdentNode;
using DeclNode = de.unika.ipd.grgen.ast.decl.DeclNode;
using ConstNode = de.unika.ipd.grgen.ast.expr.ConstNode;
using ExprNode = de.unika.ipd.grgen.ast.expr.ExprNode;
using ArrayInitNode = de.unika.ipd.grgen.ast.expr.array.ArrayInitNode;
using DequeInitNode = de.unika.ipd.grgen.ast.expr.deque.DequeInitNode;
using MapInitNode = de.unika.ipd.grgen.ast.expr.map.MapInitNode;
using SetInitNode = de.unika.ipd.grgen.ast.expr.set.SetInitNode;
using TypeNode = de.unika.ipd.grgen.ast.type.TypeNode;
using de.unika.ipd.grgen.ast.util;
using Entity = de.unika.ipd.grgen.ir.Entity;
using IR = de.unika.ipd.grgen.ir.IR;
using Expression = de.unika.ipd.grgen.ir.expr.Expression;
using ArrayInit = de.unika.ipd.grgen.ir.expr.array.ArrayInit;
using DequeInit = de.unika.ipd.grgen.ir.expr.deque.DequeInit;
using MapInit = de.unika.ipd.grgen.ir.expr.map.MapInit;
using SetInit = de.unika.ipd.grgen.ir.expr.set.SetInit;
using MemberInit = de.unika.ipd.grgen.ir.model.MemberInit;
using Coords = de.unika.ipd.grgen.parser.Coords;

/// <summary>
/// AST node representing a member initialization.
/// children: LHS:IdentNode, RHS:ExprNode
/// </summary>
public class MemberInitNode : BaseNode
{
	static MemberInitNode()
	{
		SetClassName(typeof(MemberInitNode), "member init");
	}

	private BaseNode lhsUnresolved;
	private DeclNode lhs;
	private ExprNode rhs;

	/// <param name="coords"> The source code coordinates of = operator. </param>
	/// <param name="member"> The member to be initialized. </param>
	/// <param name="expr"> The expression, that is assigned. </param>
	public MemberInitNode(Coords coords, IdentNode member, ExprNode expr)
		: base(coords)
	{
		this.lhsUnresolved = member;
		BecomeParent(this.lhsUnresolved);
		this.rhs = expr;
		BecomeParent(this.rhs);
	}

	/// <summary>
	/// returns children of this node </summary>
	public override ICollection<BaseNode> Children
	{
		get
		{
		IList<BaseNode> children = new List<BaseNode>();
		children.Add(GetValidVersion(lhsUnresolved, lhs));
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

	private static readonly MemberResolver<DeclNode> lhsResolver = new MemberResolver<DeclNode>();

	/// <seealso cref="de.unika.ipd.grgen.ast.BaseNode.resolveLocal() "/>
	protected internal override bool ResolveLocal()
	{
		//Resolver rhsResolver = new OneOfResolver(new Resolver[] {new DeclResolver(DeclNode.class), new MemberInitResolver(DeclNode.class)});
		//successfullyResolved = rhsResolver.resolve(this, RHS) && successfullyResolved;
		if(!lhsResolver.Resolve(lhsUnresolved))
			return false;
		lhs = lhsResolver.GetResult(typeof(DeclNode));
		return lhsResolver.Finish();
	}

	/// <seealso cref="de.unika.ipd.grgen.ast.BaseNode.checkLocal()"/>
	protected internal override bool CheckLocal()
	{
		return TypeCheckLocal();
	}

	/// <summary>
	/// Checks whether the expression has a type equal, compatible or castable
	/// to the type of the target. Inserts implicit cast if compatible. </summary>
	/// <returns> true, if the types are equal or compatible, false otherwise </returns>
	private bool TypeCheckLocal()
	{
		TypeNode targetType = lhs.DeclType;
		TypeNode exprType = rhs.Type;

		if(exprType.IsEqual(targetType))
			return true;

		rhs = BecomeParent(rhs.AdjustType(targetType, Coords));
		return rhs != ConstNode.Invalid;
	}

	/// <summary>
	/// Construct the intermediate representation from a member init. </summary>
	/// <seealso cref="de.unika.ipd.grgen.ast.BaseNode.constructIR()"/>
	protected internal override IR ConstructIR()
	{
		if(rhs is MapInitNode)
		{
			MapInit mapInit = rhs.CheckIR(typeof(MapInit));
			mapInit.Member = lhs.CheckIR(typeof(Entity));
			return mapInit;
		}
		else if(rhs is SetInitNode)
		{
			SetInit setInit = rhs.CheckIR(typeof(SetInit));
			setInit.Member = lhs.CheckIR(typeof(Entity));
			return setInit;
		}
		else if(rhs is ArrayInitNode)
		{
			ArrayInit arrayInit = rhs.CheckIR(typeof(ArrayInit));
			arrayInit.Member = lhs.CheckIR(typeof(Entity));
			return arrayInit;
		}
		else if(rhs is DequeInitNode)
		{
			DequeInit dequeInit = rhs.CheckIR(typeof(DequeInit));
			dequeInit.Member = lhs.CheckIR(typeof(Entity));
			return dequeInit;
		}
		else
		{
			rhs = rhs.Evaluate();
			return new MemberInit(lhs.CheckIR(typeof(Entity)), rhs.CheckIR(typeof(Expression)));
		}
	}

	public static string KindStr
	{
		get
		{
		return "member initialization";
		}
	}
}

}
