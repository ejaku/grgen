/*
 * GrGen: graph rewrite generator tool -- release GrGen.NET 8.1
 * Copyright (C) 2003-2026 Universitaet Karlsruhe, Institut fuer Programmstrukturen und Datenorganisation, LS Goos; and free programmers
 * licensed under LGPL v3, some components/parts use different licenses (see LICENSE.txt included in the packaging of this file)
 * www.grgen.de / www.grgen.net
 */

namespace de.unika.ipd.grgen.ast.expr.procenv
{

using System.Collections.Generic;

using de.unika.ipd.grgen.ast;
using BuiltinFunctionInvocationBaseNode = de.unika.ipd.grgen.ast.expr.BuiltinFunctionInvocationBaseNode;
using ExprNode = de.unika.ipd.grgen.ast.expr.ExprNode;
using TypeNode = de.unika.ipd.grgen.ast.type.TypeNode;
using BasicTypeNode = de.unika.ipd.grgen.ast.type.basic.BasicTypeNode;
using IR = de.unika.ipd.grgen.ir.IR;
using Expression = de.unika.ipd.grgen.ir.expr.Expression;
using RandomExpr = de.unika.ipd.grgen.ir.expr.procenv.RandomExpr;
using Coords = de.unika.ipd.grgen.parser.Coords;

public class RandomNode : BuiltinFunctionInvocationBaseNode
{
	static RandomNode()
	{
		SetClassName(typeof(RandomNode), "random");
	}

	private ExprNode numExpr;

	public RandomNode(Coords coords, ExprNode numExpr)
		: base(coords)
	{

		this.numExpr = numExpr;
		BecomeParent(numExpr);
	}

	public override ICollection<BaseNode> Children
	{
		get
		{
			IList<BaseNode> children = new List<BaseNode>();
			if(numExpr != null)
				children.Add(numExpr);
			return children;
		}
	}

	public override ICollection<string> ChildrenNames
	{
		get
		{
			IList<string> childrenNames = new List<string>();
			if(numExpr != null)
				childrenNames.Add("maximum random number");
			return childrenNames;
		}
	}

	protected internal override bool CheckLocal()
	{
		if(numExpr != null
				&& !numExpr.Type.IsEqual(BasicTypeNode.intType))
		{
			ReportError("The function random() expects as argument (maximumRandomNumber) a value of type int"
					+ " (but is given a value of type " + numExpr.Type.TypeName + ").");
			return false;
		}
		return true;
	}

	protected internal override IR ConstructIR()
	{
		if(numExpr != null)
			numExpr = numExpr.Evaluate();
		return new RandomExpr(numExpr != null ? numExpr.CheckIR(typeof(Expression)) : null);
	}

	public override TypeNode Type
	{
		get
		{
			// if a parameter was given random returns an random integer number from 0 up to excluding numExpr,
			// otherwise a random double in the range [0,1] is returned
			return numExpr != null ? BasicTypeNode.intType : BasicTypeNode.doubleType;
		}
	}
}

}
