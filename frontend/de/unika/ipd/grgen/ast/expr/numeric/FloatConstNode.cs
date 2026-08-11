/*
 * GrGen: graph rewrite generator tool -- release GrGen.NET 8.1
 * Copyright (C) 2003-2026 Universitaet Karlsruhe, Institut fuer Programmstrukturen und Datenorganisation, LS Goos; and free programmers
 * licensed under LGPL v3, some components/parts use different licenses (see LICENSE.txt included in the packaging of this file)
 * www.grgen.de / www.grgen.net
 */

namespace de.unika.ipd.grgen.ast.expr.numeric
{
using ConstNode = de.unika.ipd.grgen.ast.expr.ConstNode;
using StringConstNode = de.unika.ipd.grgen.ast.expr.@string.StringConstNode;
using TypeNode = de.unika.ipd.grgen.ast.type.TypeNode;
using BasicTypeNode = de.unika.ipd.grgen.ast.type.basic.BasicTypeNode;
using Coords = de.unika.ipd.grgen.parser.Coords;

/// <summary>
/// An single precision floating point constant.
/// </summary>
public class FloatConstNode : ConstNode
{
	public FloatConstNode(Coords coords, double v)
		: base(coords, "float", new float?(v))
	{
	}

	public override TypeNode Type
	{
		get
		{
		return BasicTypeNode.floatType;
		}
	}

	protected internal override ConstNode DoCastTo(TypeNode type)
	{
		float? value = (float?)Value;
		float unboxed = value.Value;

		if(type.IsEqual(BasicTypeNode.byteType))
			return new ByteConstNode(Coords, (sbyte)unboxed);
		else if(type.IsEqual(BasicTypeNode.shortType))
			return new ShortConstNode(Coords, (short)unboxed);
		else if(type.IsEqual(BasicTypeNode.intType))
			return new IntConstNode(Coords, (int)unboxed);
		else if(type.IsEqual(BasicTypeNode.longType))
			return new LongConstNode(Coords, (long)unboxed);
		else if(type.IsEqual(BasicTypeNode.doubleType))
			return new DoubleConstNode(Coords, unboxed);
		else if(type.IsEqual(BasicTypeNode.stringType))
			return new StringConstNode(Coords, value.ToString());
		else
			throw new System.NotSupportedException();
	}
}

}
