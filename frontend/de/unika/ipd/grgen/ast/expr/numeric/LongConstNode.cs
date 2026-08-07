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

using ConstNode = de.unika.ipd.grgen.ast.expr.ConstNode;
using StringConstNode = de.unika.ipd.grgen.ast.expr.@string.StringConstNode;
using TypeNode = de.unika.ipd.grgen.ast.type.TypeNode;
using BasicTypeNode = de.unika.ipd.grgen.ast.type.basic.BasicTypeNode;
using Coords = de.unika.ipd.grgen.parser.Coords;

/// <summary>
/// A long constant.
/// </summary>
public class LongConstNode : ConstNode
{
	public LongConstNode(Coords coords, long v)
		: base(coords, "long", new long?(v))
	{
	}

	public override TypeNode Type
	{
		get
		{
		return BasicTypeNode.longType;
		}
	}

	protected internal override ConstNode DoCastTo(TypeNode type)
	{
		long? value = (long?)Value;
		long unboxed = value.Value;

		if(type.IsEqual(BasicTypeNode.byteType))
			return new ByteConstNode(Coords, (sbyte)unboxed);
		else if(type.IsEqual(BasicTypeNode.shortType))
			return new ShortConstNode(Coords, (short)unboxed);
		else if(type.IsEqual(BasicTypeNode.intType))
			return new IntConstNode(Coords, (int)unboxed);
		else if(type.IsEqual(BasicTypeNode.floatType))
			return new FloatConstNode(Coords, unboxed);
		else if(type.IsEqual(BasicTypeNode.doubleType))
			return new DoubleConstNode(Coords, unboxed);
		else if(type.IsEqual(BasicTypeNode.stringType))
			return new StringConstNode(Coords, value.ToString());
		else
			throw new System.NotSupportedException();
	}

	public static string RemoveSuffix(string longLiteral)
	{
		if(longLiteral.EndsWith("l", StringComparison.Ordinal) || longLiteral.EndsWith("L", StringComparison.Ordinal))
			return longLiteral.Substring(0, longLiteral.Length - 1);
		else
			return longLiteral;
	}
}

}
