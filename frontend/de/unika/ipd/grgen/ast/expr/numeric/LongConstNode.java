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
 * A long constant.
 */
public class LongConstNode extends ConstNode
{
	public LongConstNode(Coords coords, long v)
	{
		super(coords, "long", new Long(v));
	}

	@Override
	public TypeNode getType()
	{
		return BasicTypeNode.longType;
	}

	@Override
	protected ConstNode doCastTo(TypeNode type)
	{
		Long value = (Long)getValue();
		long unboxed = value.longValue();

		if(type.isEqual(BasicTypeNode.byteType)) {
			return new ByteConstNode(getCoords(), (byte)unboxed);
		} else if(type.isEqual(BasicTypeNode.shortType)) {
			return new ShortConstNode(getCoords(), (short)unboxed);
		} else if(type.isEqual(BasicTypeNode.intType)) {
			return new IntConstNode(getCoords(), (int)unboxed);
		} else if(type.isEqual(BasicTypeNode.floatType)) {
			return new FloatConstNode(getCoords(), unboxed);
		} else if(type.isEqual(BasicTypeNode.doubleType)) {
			return new DoubleConstNode(getCoords(), unboxed);
		} else if(type.isEqual(BasicTypeNode.stringType)) {
			return new StringConstNode(getCoords(), value.toString());
		} else
			throw new UnsupportedOperationException();
	}

	public static String removeSuffix(String longLiteral)
	{
		if(longLiteral.endsWith("l") || longLiteral.endsWith("L"))
			return longLiteral.substring(0, longLiteral.length() - 1);
		else
			return longLiteral;
	}
}
