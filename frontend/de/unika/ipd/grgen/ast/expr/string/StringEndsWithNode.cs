/*
 * GrGen: graph rewrite generator tool -- release GrGen.NET 8.1
 * Copyright (C) 2003-2026 Universitaet Karlsruhe, Institut fuer Programmstrukturen und Datenorganisation, LS Goos; and free programmers
 * licensed under LGPL v3, some components/parts use different licenses (see LICENSE.txt included in the packaging of this file)
 * www.grgen.de / www.grgen.net
 */

/// <summary>
/// @author Edgar Jakumeit
/// </summary>

namespace de.unika.ipd.grgen.ast.expr.@string
{

	using System.Collections.Generic;

	using de.unika.ipd.grgen.ast;
	using BuiltinFunctionInvocationBaseNode = de.unika.ipd.grgen.ast.expr.BuiltinFunctionInvocationBaseNode;
	using ExprNode = de.unika.ipd.grgen.ast.expr.ExprNode;
	using TypeNode = de.unika.ipd.grgen.ast.type.TypeNode;
	using BasicTypeNode = de.unika.ipd.grgen.ast.type.basic.BasicTypeNode;
	using IR = de.unika.ipd.grgen.ir.IR;
	using Expression = de.unika.ipd.grgen.ir.expr.Expression;
	using StringEndsWith = de.unika.ipd.grgen.ir.expr.@string.StringEndsWith;
	using Coords = de.unika.ipd.grgen.parser.Coords;

	public class StringEndsWithNode : BuiltinFunctionInvocationBaseNode
	{
		static StringEndsWithNode()
		{
			SetClassName(typeof(StringEndsWithNode), "string endsWith");
		}

		private ExprNode stringExpr;
		private ExprNode stringToSearchForExpr;

		public StringEndsWithNode(Coords coords, ExprNode stringExpr, ExprNode stringToSearchForExpr)
			: base(coords)
		{

			this.stringExpr = BecomeParent(stringExpr);
			this.stringToSearchForExpr = BecomeParent(stringToSearchForExpr);
		}

		public override ICollection<BaseNode> Children
		{
			get
			{
				IList<BaseNode> children = new List<BaseNode>();
				children.Add(stringExpr);
				children.Add(stringToSearchForExpr);
				return children;
			}
		}

		public override ICollection<string> ChildrenNames
		{
			get
			{
				IList<string> childrenNames = new List<string>();
				childrenNames.Add("string");
				childrenNames.Add("stringToSearchFor");
				return childrenNames;
			}
		}

		protected internal override bool CheckLocal()
		{
			if(!stringExpr.Type.IsEqual(BasicTypeNode.stringType))
			{
				stringExpr.ReportError("The string function method endsWith can only be employed on an object of type string"
						+ " (but is employed on an object of type " + stringExpr.Type.TypeName + ").");
				return false;
			}
			if(!stringToSearchForExpr.Type.IsEqual(BasicTypeNode.stringType))
			{
				stringToSearchForExpr.ReportError("The string function method endsWith expects as argument (stringToSearchFor) a value of type string"
						+ " (but is given a value of type " + stringToSearchForExpr.Type.TypeName + ").");
				return false;
			}
			return true;
		}

		protected internal override IR ConstructIR()
		{
			stringExpr = stringExpr.Evaluate();
			stringToSearchForExpr = stringToSearchForExpr.Evaluate();
			return new StringEndsWith(stringExpr.CheckIR(typeof(Expression)),
					stringToSearchForExpr.CheckIR(typeof(Expression)));
		}

		public override TypeNode Type
		{
			get
			{
				return BasicTypeNode.booleanType;
			}
		}
	}

}
