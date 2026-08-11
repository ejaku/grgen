/*
 * GrGen: graph rewrite generator tool -- release GrGen.NET 8.1
 * Copyright (C) 2003-2026 Universitaet Karlsruhe, Institut fuer Programmstrukturen und Datenorganisation, LS Goos; and free programmers
 * licensed under LGPL v3, some components/parts use different licenses (see LICENSE.txt included in the packaging of this file)
 * www.grgen.de / www.grgen.net
 */

/// <summary>
/// @author Edgar Jakumeit
/// </summary>

namespace de.unika.ipd.grgen.ast.expr.numeric
{

	using System;
	using System.Collections.Generic;

	using de.unika.ipd.grgen.ast;
	using BuiltinFunctionInvocationBaseNode = de.unika.ipd.grgen.ast.expr.BuiltinFunctionInvocationBaseNode;
	using ExprNode = de.unika.ipd.grgen.ast.expr.ExprNode;
	using TypeNode = de.unika.ipd.grgen.ast.type.TypeNode;
	using BasicTypeNode = de.unika.ipd.grgen.ast.type.basic.BasicTypeNode;
	using IR = de.unika.ipd.grgen.ir.IR;
	using Expression = de.unika.ipd.grgen.ir.expr.Expression;
	using SinCosTanExpr = de.unika.ipd.grgen.ir.expr.numeric.SinCosTanExpr;
	using Coords = de.unika.ipd.grgen.parser.Coords;

	public class SinCosTanExprNode : BuiltinFunctionInvocationBaseNode
	{
		static SinCosTanExprNode()
		{
			SetClassName(typeof(SinCosTanExprNode), "sincostan expr");
		}

		public enum TrigonometryFunctionType
		{
			sin,
			cos,
			tan
		}

		internal TrigonometryFunctionType which;
		private ExprNode argumentExpr;

		public SinCosTanExprNode(Coords coords, TrigonometryFunctionType which, ExprNode argumentExpr)
			: base(coords)
		{

			this.which = which;
			this.argumentExpr = BecomeParent(argumentExpr);
		}

		public override ICollection<BaseNode> Children
		{
			get
			{
				IList<BaseNode> children = new List<BaseNode>();
				children.Add(argumentExpr);
				return children;
			}
		}

		public override ICollection<string> ChildrenNames
		{
			get
			{
				IList<string> childrenNames = new List<string>();
				childrenNames.Add("arg");
				return childrenNames;
			}
		}

		protected internal override bool CheckLocal()
		{
			if(argumentExpr.Type.IsEqual(BasicTypeNode.doubleType))
				return true;
			ReportError("The function Math::" + which + "() expects as argument a value of type double"
					+ " (but is given a value of type " + argumentExpr.Type.TypeName + ").");
			return false;
		}

		protected internal override IR ConstructIR()
		{
			argumentExpr = argumentExpr.Evaluate();
			return new SinCosTanExpr(FunctionType, argumentExpr.CheckIR(typeof(Expression)));
		}

		private SinCosTanExpr.TrigonometryFunctionType FunctionType
		{
			get
			{
				switch(which)
				{
				case de.unika.ipd.grgen.ast.expr.numeric.SinCosTanExprNode.TrigonometryFunctionType.sin:
					return SinCosTanExpr.TrigonometryFunctionType.sin;
				case de.unika.ipd.grgen.ast.expr.numeric.SinCosTanExprNode.TrigonometryFunctionType.cos:
					return SinCosTanExpr.TrigonometryFunctionType.cos;
				case de.unika.ipd.grgen.ast.expr.numeric.SinCosTanExprNode.TrigonometryFunctionType.tan:
					return SinCosTanExpr.TrigonometryFunctionType.tan;
				default:
					throw new Exception("internal compiler error");
				}
			}
		}

		public override TypeNode Type
		{
			get
			{
				return argumentExpr.Type;
			}
		}
	}

}
