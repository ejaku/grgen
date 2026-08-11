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
	using de.unika.ipd.grgen.ast;
	using ByteConstNode = de.unika.ipd.grgen.ast.expr.numeric.ByteConstNode;
	using DoubleConstNode = de.unika.ipd.grgen.ast.expr.numeric.DoubleConstNode;
	using FloatConstNode = de.unika.ipd.grgen.ast.expr.numeric.FloatConstNode;
	using IntConstNode = de.unika.ipd.grgen.ast.expr.numeric.IntConstNode;
	using LongConstNode = de.unika.ipd.grgen.ast.expr.numeric.LongConstNode;
	using ShortConstNode = de.unika.ipd.grgen.ast.expr.numeric.ShortConstNode;
	using StringConstNode = de.unika.ipd.grgen.ast.expr.@string.StringConstNode;
	using TypeNode = de.unika.ipd.grgen.ast.type.TypeNode;
	using BasicTypeNode = de.unika.ipd.grgen.ast.type.basic.BasicTypeNode;
	using IR = de.unika.ipd.grgen.ir.IR;
	using EnumExpression = de.unika.ipd.grgen.ir.expr.EnumExpression;
	using Coords = de.unika.ipd.grgen.parser.Coords;

	/// <summary>
	/// An enum item value.
	/// </summary>
	public class EnumConstNode : ConstNode
	{
		/// <summary>
		/// The name of the enum item. </summary>
		private IdentNode id;

		/// <param name="coords"> The source code coordinates. </param>
		/// <param name="id"> The name of the enum item. </param>
		/// <param name="value"> The value of the enum item. </param>
		public EnumConstNode(Coords coords, IdentNode id, int value)
			: base(coords, "enum item", new int?(value))
		{
			this.id = id;
		}

		/// <seealso cref="de.unika.ipd.grgen.ast.expr.ConstNode.doCastTo(de.unika.ipd.grgen.ast.type.TypeNode) "/>
		protected internal override ConstNode DoCastTo(TypeNode type)
		{
			int? value = (int?)Value;
			int unboxed = value.Value;

			if(type.IsEqual(BasicTypeNode.byteType))
				return new ByteConstNode(Coords, (sbyte)unboxed);
			else if(type.IsEqual(BasicTypeNode.shortType))
				return new ShortConstNode(Coords, (short)unboxed);
			else if(type.IsEqual(BasicTypeNode.intType))
				return new IntConstNode(Coords, unboxed);
			else if(type.IsEqual(BasicTypeNode.longType))
				return new LongConstNode(Coords, unboxed);
			else if(type.IsEqual(BasicTypeNode.floatType))
				return new FloatConstNode(Coords, unboxed);
			else if(type.IsEqual(BasicTypeNode.doubleType))
				return new DoubleConstNode(Coords, unboxed);
			else if(type.IsEqual(BasicTypeNode.stringType))
				return new StringConstNode(Coords, id.ToString());
			else
				throw new System.NotSupportedException();
		}

		/// <seealso cref="de.unika.ipd.grgen.ast.expr.ExprNode.getType() "/>
		public override TypeNode Type
		{
			get
			{
				return BasicTypeNode.enumItemType;
			}
		}

		public virtual EnumExpression IREnumExpression
		{
			get
			{
				return CheckIR<EnumExpression>(typeof(EnumExpression));
			}
		}

		protected internal override IR ConstructIR()
		{
			// The EnumExpression is initialized later in EnumTypeNode.constructIR()
			// to break the circular dependency.
			return new EnumExpression(((int?)value).Value);
		}
	}

}
