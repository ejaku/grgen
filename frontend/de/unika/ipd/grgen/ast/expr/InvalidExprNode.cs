/*
 * GrGen: graph rewrite generator tool -- release GrGen.NET 8.1
 * Copyright (C) 2003-2026 Universitaet Karlsruhe, Institut fuer Programmstrukturen und Datenorganisation, LS Goos; and free programmers
 * licensed under LGPL v3, some components/parts use different licenses (see LICENSE.txt included in the packaging of this file)
 * www.grgen.de / www.grgen.net
 */

namespace de.unika.ipd.grgen.ast.expr
{

	using System.Collections.Generic;

	using de.unika.ipd.grgen.ast;
	using TypeNode = de.unika.ipd.grgen.ast.type.TypeNode;
	using BasicTypeNode = de.unika.ipd.grgen.ast.type.basic.BasicTypeNode;
	using Coords = de.unika.ipd.grgen.parser.Coords;

	/// <summary>
	/// representing invalid expressions.
	/// </summary>
	public class InvalidExprNode : ExprNode
	{
		static InvalidExprNode()
		{
			SetClassName(typeof(InvalidExprNode), "invalid expression");
		}

		public InvalidExprNode()
			: base(Coords.Invalid)
		{
		}

		/// <summary>
		/// returns children of this node </summary>
		public override ICollection<BaseNode> Children
		{
			get
			{
				IList<BaseNode> children = new List<BaseNode>();
				// no children
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
				// no children
				return childrenNames;
			}
		}

		/// <seealso cref="de.unika.ipd.grgen.ast.BaseNode.resolveLocal() "/>
		protected internal override bool ResolveLocal()
		{
			return true;
		}

		/// <seealso cref="de.unika.ipd.grgen.ast.BaseNode.checkLocal() "/>
		protected internal override bool CheckLocal()
		{
			return true;
		}

		public override TypeNode Type
		{
			get
			{
				return BasicTypeNode.errorType;
			}
		}

		public override string ToString()
		{
			return "invalid expression";
		}

		public static new string KindStr
		{
			get
			{
				return "invalid expression";
			}
		}
	}

}
