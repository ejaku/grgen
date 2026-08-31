/*
 * GrGen: graph rewrite generator tool -- release GrGen.NET 8.1
 * Copyright (C) 2003-2026 Universitaet Karlsruhe, Institut fuer Programmstrukturen und Datenorganisation, LS Goos; and free programmers
 * licensed under LGPL v3, some components/parts use different licenses (see LICENSE.txt included in the packaging of this file)
 * www.grgen.de / www.grgen.net
 */

/// <summary>
/// @author Sebastian Hack
/// </summary>

namespace de.unika.ipd.grgen.ast.expr
{

	using System.Collections.Generic;

	using de.unika.ipd.grgen.ast;
	using Operator = de.unika.ipd.grgen.ast.decl.executable.Operator;
	using OperatorDeclNode = de.unika.ipd.grgen.ast.decl.executable.OperatorDeclNode;
	using TypeNode = de.unika.ipd.grgen.ast.type.TypeNode;
	using BasicTypeNode = de.unika.ipd.grgen.ast.type.basic.BasicTypeNode;
	using IR = de.unika.ipd.grgen.ir.IR;
	using Constant = de.unika.ipd.grgen.ir.expr.Constant;
	using Coords = de.unika.ipd.grgen.parser.Coords;

	/// <summary>
	/// Constant expressions.
	/// A constant is 0-ary operator.
	/// </summary>
	public abstract class ConstNode : OperatorNode
	{
		/// <summary>
		/// The value of the constant. </summary>
		protected internal object value;

		/// <summary>
		/// A name for the constant. </summary>
		protected internal string name;

		private static readonly ConstNode INVALID = new InvalidConstNode(
			Coords.Builtin, "invalid const", "invalid value");

		public static new ConstNode Invalid
		{
			get
			{
				return INVALID;
			}
		}

		/// <param name="coords"> The source code coordinates. </param>
		public ConstNode(Coords coords, string name, object value)
			: base(coords, Operator.CONST)
		{
			this.value = value;
			this.name = name;
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

		/// <summary>
		/// Get the value of the constant. </summary>
		/// <returns> The value. </returns>
		public virtual object Value
		{
			get
			{
				return value;
			}
		}

		/// <summary>
		/// Include the constants value in its string representation. </summary>
		/// <seealso cref="java.lang.Object.toString()"/>
		public override string ToString()
		{
			return OperatorDeclNode.GetName(Operator) + " " + value.ToString();
		}

		public override string NodeLabel
		{
			get
			{
				return ToString();
			}
		}

		/// <summary>
		/// Just a convenience function. </summary>
		/// <returns> The IR object. </returns>
		public virtual Constant IRConstant
		{
			get
			{
				return CheckIR<Constant>(typeof(Constant));
			}
		}

		/// <seealso cref="de.unika.ipd.grgen.ast.BaseNode.constructIR()"/>
		protected internal override IR ConstructIR()
		{
			return new Constant(Type.IRType, value);
		}

		/// <seealso cref="de.unika.ipd.grgen.ast.expr.ExprNode.getType()"/>
		public override TypeNode Type
		{
			get
			{
				return BasicTypeNode.errorType;
			}
		}

		/// <summary>
		/// Cast this constant to a new type. </summary>
		/// <param name="type"> The new type. </param>
		/// <returns> A new constant with the corresponding value and a new type. </returns>
		public ConstNode CastTo(TypeNode type)
		{
			ConstNode res = Invalid;

			try
			{
				if(Type.IsEqual(type))
					res = this;
				else if(Type.IsCastableTo(type))
					res = DoCastTo(type);
			}
			catch(System.NotSupportedException)
			{
				ReportError("The cast from " + ToString() + " to type " + type.ToStringWithDeclarationCoords() + " is failing.");
			}

			return res;
		}

		/// <summary>
		/// Implement this method to implement casting.
		/// You don't have to check for types that are not castable to the
		/// type of this constant. </summary>
		/// <param name="type"> The new type. </param>
		/// <returns> A constant of the new type. </returns>
		protected internal abstract ConstNode DoCastTo(TypeNode type);
	}

}
