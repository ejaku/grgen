/*
 * GrGen: graph rewrite generator tool -- release GrGen.NET 8.1
 * Copyright (C) 2003-2026 Universitaet Karlsruhe, Institut fuer Programmstrukturen und Datenorganisation, LS Goos; and free programmers
 * licensed under LGPL v3, some components/parts use different licenses (see LICENSE.txt included in the packaging of this file)
 * www.grgen.de / www.grgen.net
 */

/// <summary>
/// @author Rubino Geiss
/// </summary>

namespace de.unika.ipd.grgen.ast.expr
{
	using TypeNode = de.unika.ipd.grgen.ast.type.TypeNode;
	using BasicTypeNode = de.unika.ipd.grgen.ast.type.basic.BasicTypeNode;
	using IR = de.unika.ipd.grgen.ir.IR;
	using Constant = de.unika.ipd.grgen.ir.expr.Constant;
	using Coords = de.unika.ipd.grgen.parser.Coords;

	/// <summary>
	/// The null constant.
	/// </summary>
	public class NullConstNode : ConstNode
	{
		private TypeNode type;

		public NullConstNode(Coords coords)
			: base(coords, "null", Value.NULL)
		{
			type = BasicTypeNode.nullType;
		}

		/// <summary>
		/// Singleton class representing the only constant value 'null' that
		/// the basic type 'object' has.
		/// </summary>
		public class Value
		{
			public static Value NULL = new ValueAnonymousInnerClass();

			private class ValueAnonymousInnerClass : Value
			{
				public override string ToString()
				{
					return "Const null";
				}
			}

			internal Value()
			{
			}
		}

		public override TypeNode Type
		{
			get
			{
				return type;
			}
		}

		public override string ToString()
		{
			return "Const (" + type + ") null";
		}

		protected internal override IR ConstructIR()
		{
			return new Constant(Type.IRType, null);
		}

		/// <seealso cref="de.unika.ipd.grgen.ast.expr.ConstNode.doCastTo(de.unika.ipd.grgen.ast.type.TypeNode) "/>
		protected internal override ConstNode DoCastTo(TypeNode type)
		{
			NullConstNode castedNull = new NullConstNode(Coords);
			castedNull.type = type;
			return castedNull;
		}
	}

}
