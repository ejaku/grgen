/*
 * GrGen: graph rewrite generator tool -- release GrGen.NET 8.1
 * Copyright (C) 2003-2026 Universitaet Karlsruhe, Institut fuer Programmstrukturen und Datenorganisation, LS Goos; and free programmers
 * licensed under LGPL v3, some components/parts use different licenses (see LICENSE.txt included in the packaging of this file)
 * www.grgen.de / www.grgen.net
 */

/// <summary>
/// @author Moritz Kroll, Edgar Jakumeit
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
	using StringSubstring = de.unika.ipd.grgen.ir.expr.@string.StringSubstring;
	using Coords = de.unika.ipd.grgen.parser.Coords;

	public class StringSubstringNode : BuiltinFunctionInvocationBaseNode
	{
		static StringSubstringNode()
		{
			SetClassName(typeof(StringSubstringNode), "string substring");
		}

		private ExprNode stringExpr;
		private ExprNode startExpr;
		private ExprNode lengthExpr;

		public StringSubstringNode(Coords coords, ExprNode stringExpr, ExprNode startExpr, ExprNode lengthExpr)
			: base(coords)
		{

			this.stringExpr = BecomeParent(stringExpr);
			this.startExpr = BecomeParent(startExpr);
			this.lengthExpr = BecomeParent(lengthExpr);
		}

		public StringSubstringNode(Coords coords, ExprNode stringExpr, ExprNode startExpr)
			: base(coords)
		{

			this.stringExpr = BecomeParent(stringExpr);
			this.startExpr = BecomeParent(startExpr);
			this.lengthExpr = null;
		}

		public override ICollection<BaseNode> Children
		{
			get
			{
				IList<BaseNode> children = new List<BaseNode>();
				children.Add(stringExpr);
				children.Add(startExpr);
				if(lengthExpr != null)
					children.Add(lengthExpr);
				return children;
			}
		}

		public override ICollection<string> ChildrenNames
		{
			get
			{
				IList<string> childrenNames = new List<string>();
				childrenNames.Add("string");
				childrenNames.Add("start");
				if(lengthExpr != null)
					childrenNames.Add("length");
				return childrenNames;
			}
		}

		protected internal override bool CheckLocal()
		{
			if(!stringExpr.Type.IsEqual(BasicTypeNode.stringType))
			{
				stringExpr.ReportError("The string function method substring can only be employed on an object of type string"
						+ " (but is employed on an object of type " + stringExpr.Type.TypeName + ").");
				return false;
			}
			if(!startExpr.Type.IsEqual(BasicTypeNode.intType))
			{
				startExpr.ReportError("The string function method substring expects as 1. argument (startPosition) a value of type int"
						+ " (but is given a value of type " + startExpr.Type.TypeName + ").");
				return false;
			}
			if(lengthExpr != null)
			{
				if(!lengthExpr.Type.IsEqual(BasicTypeNode.intType))
				{
					lengthExpr.ReportError("The string function method substring expects as 2. argument (length) a value of type int"
							+ " (but is given a value of type " + lengthExpr.Type.TypeName + ").");
					return false;
				}
			}
			return true;
		}

		protected internal override IR ConstructIR()
		{
			stringExpr = stringExpr.Evaluate();
			startExpr = startExpr.Evaluate();
			if(lengthExpr != null)
				lengthExpr = lengthExpr.Evaluate();
			return new StringSubstring(stringExpr.CheckIR(typeof(Expression)),
					startExpr.CheckIR(typeof(Expression)),
					lengthExpr != null ? lengthExpr.CheckIR(typeof(Expression)) : null);
		}

		public override TypeNode Type
		{
			get
			{
				return BasicTypeNode.stringType;
			}
		}
	}

}
