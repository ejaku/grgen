/*
 * GrGen: graph rewrite generator tool -- release GrGen.NET 8.1
 * Copyright (C) 2003-2026 Universitaet Karlsruhe, Institut fuer Programmstrukturen und Datenorganisation, LS Goos; and free programmers
 * licensed under LGPL v3, some components/parts use different licenses (see LICENSE.txt included in the packaging of this file)
 * www.grgen.de / www.grgen.net
 */

/// <summary>
/// TypeConstraintExprNode.java
/// 
/// @author Sebastian Hack
/// </summary>

namespace de.unika.ipd.grgen.ast.type
{

	using BaseNode = de.unika.ipd.grgen.ast.BaseNode;
	using de.unika.ipd.grgen.ast;
	using IdentNode = de.unika.ipd.grgen.ast.IdentNode;
	using Coords = de.unika.ipd.grgen.parser.Coords;
	using Color = de.unika.ipd.grgen.util.Color;

	/// <summary>
	/// AST node representing type expressions.
	/// A lot more general than what is really used as of now (a non-empty constraint defines a set difference which is not modeled explicitly, and contains one or a union of types).
	/// </summary>
	public abstract class TypeExprNode : BaseNode
	{
		public enum TypeOperator
		{
			SET,
			UNION,
			DIFFERENCE,
			INTERSECT,
		}

		/// <summary>
		/// Opcode of the set operation. </summary>
		protected internal readonly TypeOperator op;

		private static readonly TypeExprNode EMPTY = new TypeConstraintNode(Coords.Invalid, new CollectNode<IdentNode>());

		public static TypeExprNode Empty
		{
			get
			{
				return EMPTY;
			}
		}

		protected internal TypeExprNode(Coords coords, TypeOperator op)
			: base(coords)
		{
			this.op = op;
		}

		/// <seealso cref="de.unika.ipd.grgen.util.GraphDumpable.getNodeColor() "/>
		public override Color NodeColor
		{
			get
			{
				return Color.CYAN;
			}
		}

		public override string NodeLabel
		{
			get
			{
				return "type expr " + op;
			}
		}
	}

}
