/*
 * GrGen: graph rewrite generator tool -- release GrGen.NET 3.0
 * Copyright (C) 2003-2011 Universitaet Karlsruhe, Institut fuer Programmstrukturen und Datenorganisation, LS Goos
 * licensed under LGPL v3 (see LICENSE.txt included in the packaging of this file)
 * www.grgen.net
 */

/**
 * @author Edgar Jakumeit
 * @version $Id$
 */
package de.unika.ipd.grgen.ast;

import de.unika.ipd.grgen.parser.Coords;

/**
 * A long constant.
 */
public class LongConstNode extends ConstNode
{
	public LongConstNode(Coords coords, long v) {
		super(coords, "long", new Long(v));
	}

	@Override
	public TypeNode getType() {
		return BasicTypeNode.longType;
	}

	@Override
	protected ConstNode doCastTo(TypeNode type) {
		Long value = (Long) getValue();

		if (type.isEqual(BasicTypeNode.byteType)) {
			return new ByteConstNode(getCoords(), (byte)(long)value);
		} else if (type.isEqual(BasicTypeNode.shortType)) {
			return new ShortConstNode(getCoords(), (short)(long)value);
		} else if (type.isEqual(BasicTypeNode.intType)) {
			return new IntConstNode(getCoords(), (int)(long)value);
		} else if (type.isEqual(BasicTypeNode.floatType)) {
			return new FloatConstNode(getCoords(), value);
		} else if (type.isEqual(BasicTypeNode.doubleType)) {
			return new DoubleConstNode(getCoords(), value);
		} else if (type.isEqual(BasicTypeNode.stringType)) {
			return new StringConstNode(getCoords(), value.toString());
		} else throw new UnsupportedOperationException();
	}
	
	public static String removeSuffix(String longLiteral) {
		if(longLiteral.endsWith("l") || longLiteral.endsWith("L"))
			return longLiteral.substring(0, longLiteral.length()-1);
		else
			return longLiteral;
	}
}
