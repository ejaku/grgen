/*
 * GrGen: graph rewrite generator tool -- release GrGen.NET 5.0
 * Copyright (C) 2003-2020 Universitaet Karlsruhe, Institut fuer Programmstrukturen und Datenorganisation, LS Goos; and free programmers
 * licensed under LGPL v3 (see LICENSE.txt included in the packaging of this file)
 * www.grgen.net
 */

/**
 * @author Edgar Jakumeit
 */

package de.unika.ipd.grgen.ast.expr.numeric;

import de.unika.ipd.grgen.ast.expr.ConstNode;
import de.unika.ipd.grgen.ast.expr.string.StringConstNode;
import de.unika.ipd.grgen.ast.type.TypeNode;
import de.unika.ipd.grgen.ast.type.basic.BasicTypeNode;
import de.unika.ipd.grgen.parser.Coords;

/**
 * A byte constant.
 */
public class ByteConstNode extends ConstNode
{
	public ByteConstNode(Coords coords, byte v)
	{
		super(coords, "byte", new Byte(v));
	}

	@Override
	public TypeNode getType()
	{
		return BasicTypeNode.byteType;
	}

	@Override
	protected ConstNode doCastTo(TypeNode type)
	{
		Byte value = (Byte)getValue();
		byte unboxed = (byte)value;

		if(type.isEqual(BasicTypeNode.shortType)) {
			return new ShortConstNode(getCoords(), unboxed);
		} else if(type.isEqual(BasicTypeNode.intType)) {
			return new IntConstNode(getCoords(), unboxed);
		} else if(type.isEqual(BasicTypeNode.longType)) {
			return new LongConstNode(getCoords(), unboxed);
		} else if(type.isEqual(BasicTypeNode.floatType)) {
			return new FloatConstNode(getCoords(), unboxed);
		} else if(type.isEqual(BasicTypeNode.doubleType)) {
			return new DoubleConstNode(getCoords(), unboxed);
		} else if(type.isEqual(BasicTypeNode.stringType)) {
			return new StringConstNode(getCoords(), value.toString());
		} else
			throw new UnsupportedOperationException();
	}

	public static String removeSuffix(String byteLiteral)
	{
		if(byteLiteral.endsWith("y") || byteLiteral.endsWith("Y"))
			return byteLiteral.substring(0, byteLiteral.length() - 1);
		else
			return byteLiteral;
	}
}
