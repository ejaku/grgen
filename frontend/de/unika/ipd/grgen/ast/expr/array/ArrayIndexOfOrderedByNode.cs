/*
 * GrGen: graph rewrite generator tool -- release GrGen.NET 8.1
 * Copyright (C) 2003-2026 Universitaet Karlsruhe, Institut fuer Programmstrukturen und Datenorganisation, LS Goos; and free programmers
 * licensed under LGPL v3, some components/parts use different licenses (see LICENSE.txt included in the packaging of this file)
 * www.grgen.de / www.grgen.net
 */

/// <summary>
/// @author Edgar Jakumeit
/// </summary>

namespace de.unika.ipd.grgen.ast.expr.array
{

using System.Collections.Generic;

using de.unika.ipd.grgen.ast;
using DeclNode = de.unika.ipd.grgen.ast.decl.DeclNode;
using ConstNode = de.unika.ipd.grgen.ast.expr.ConstNode;
using ExprNode = de.unika.ipd.grgen.ast.expr.ExprNode;
using EnumTypeNode = de.unika.ipd.grgen.ast.model.type.EnumTypeNode;
using InheritanceTypeNode = de.unika.ipd.grgen.ast.model.type.InheritanceTypeNode;
using MatchTypeNode = de.unika.ipd.grgen.ast.type.MatchTypeNode;
using TypeNode = de.unika.ipd.grgen.ast.type.TypeNode;
using BasicTypeNode = de.unika.ipd.grgen.ast.type.basic.BasicTypeNode;
using ArrayTypeNode = de.unika.ipd.grgen.ast.type.container.ArrayTypeNode;
using de.unika.ipd.grgen.ast.util;
using Expression = de.unika.ipd.grgen.ir.expr.Expression;
using ArrayIndexOfOrderedByExpr = de.unika.ipd.grgen.ir.expr.array.ArrayIndexOfOrderedByExpr;
using Entity = de.unika.ipd.grgen.ir.Entity;
using IR = de.unika.ipd.grgen.ir.IR;
using Coords = de.unika.ipd.grgen.parser.Coords;

public class ArrayIndexOfOrderedByNode : ArrayFunctionMethodInvocationBaseExprNode
{
	static ArrayIndexOfOrderedByNode()
	{
		SetClassName(typeof(ArrayIndexOfOrderedByNode), "array index of ordered by");
	}

	internal IdentNode attribute;
	private DeclNode member;
	private ExprNode valueExpr;

	public ArrayIndexOfOrderedByNode(Coords coords, ExprNode targetExpr, IdentNode attribute, ExprNode valueExpr)
		: base(coords, targetExpr)
	{
		this.attribute = attribute;
		this.valueExpr = BecomeParent(valueExpr);
	}

	public override ICollection<BaseNode> Children
	{
		get
		{
		IList<BaseNode> children = new List<BaseNode>();
		children.Add(targetExpr);
		children.Add(valueExpr);
		return children;
		}
	}

	public override ICollection<string> ChildrenNames
	{
		get
		{
		IList<string> childrenNames = new List<string>();
		childrenNames.Add("targetExpr");
		childrenNames.Add("valueExpr");
		return childrenNames;
		}
	}

	protected internal override bool CheckLocal()
	{
		// target type already checked during resolving into this node
		ArrayTypeNode arrayType = TargetTypeExact;
		if(!(arrayType.valueType is InheritanceTypeNode)
				&& !(arrayType.valueType is MatchTypeNode))
		{
			targetExpr.ReportError("The array function method indexOfOrderedBy can only be employed on an object of type array<nodes, edges, class objects, transient class objects, match types, match class types>"
					+ " (but is employed on an object of type " + arrayType.TypeName + ").");
			return false;
		}

		member = Resolver.ResolveMember(arrayType.valueType, attribute);
		if(member == null)
			return false;

		TypeNode memberType = member.DeclType;
		if(!(memberType.Equals(BasicTypeNode.byteType))
				&& !(memberType.Equals(BasicTypeNode.shortType))
				&& !(memberType.Equals(BasicTypeNode.intType))
				&& !(memberType.Equals(BasicTypeNode.longType))
				&& !(memberType.Equals(BasicTypeNode.floatType))
				&& !(memberType.Equals(BasicTypeNode.doubleType))
				&& !(memberType.Equals(BasicTypeNode.stringType))
				&& !(memberType.Equals(BasicTypeNode.booleanType))
				&& !(memberType is EnumTypeNode))
		{
			targetExpr.ReportError("The array function method indexOfOrderedBy is only available for attributes of type byte, short, int, long, float, double, string, boolean, enum of a graph element"
					+ " (but is of type " + memberType.TypeName + ")");
		}

		TypeNode valueType = valueExpr.Type;
		if(!valueType.IsEqual(memberType))
		{
			ExprNode valueExprOld = valueExpr;
			valueExpr = BecomeParent(valueExpr.AdjustType(memberType, Coords));
			if(valueExpr == ConstNode.Invalid)
			{
				valueExprOld.ReportError("The array function method indexOfOrderedBy expects as 1. argument (valueToSearchFor) a value of type " + memberType.TypeName
						+ " (but is given a value of type " + valueType.TypeName + ").");
				return false;
			}
		}
		return true;
	}

	public override TypeNode Type
	{
		get
		{
		return BasicTypeNode.intType;
		}
	}

	protected internal override IR ConstructIR()
	{
		targetExpr = targetExpr.Evaluate();
		valueExpr = valueExpr.Evaluate();
		return new ArrayIndexOfOrderedByExpr(targetExpr.CheckIR(typeof(Expression)),
				member.CheckIR(typeof(Entity)),
				valueExpr.CheckIR(typeof(Expression)));
	}
}

}
