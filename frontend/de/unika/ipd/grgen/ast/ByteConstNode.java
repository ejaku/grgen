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
 * A byte constant.
 */
public class ByteConstNode extends ConstNode
{
	public ByteConstNode(Coords coords, byte v) {
		super(coords, "byte", new Byte(v));
	}

	@Override
	public TypeNode getType() {
		return BasicTypeNode.byteType;
	}

	@Override
	protected ConstNode doCastTo(TypeNode type) {
		Byte value = (Byte) getValue();

		if (type.isEqual(BasicTypeNode.shortType)) {
			return new ShortConstNode(getCoords(), value);
		} else if (type.isEqual(BasicTypeNode.intType)) {
			return new IntConstNode(getCoords(), value);
		} else if (type.isEqual(BasicTypeNode.longType)) {
			return new LongConstNode(getCoords(), value);
		} else if (type.isEqual(BasicTypeNode.floatType)) {
			return new FloatConstNode(getCoords(), value);
		} else if (type.isEqual(BasicTypeNode.doubleType)) {
			return new DoubleConstNode(getCoords(), value);
		} else if (type.isEqual(BasicTypeNode.stringType)) {
			return new StringConstNode(getCoords(), value.toString());
		} else throw new UnsupportedOperationException();
	}
	
	public static String removeSuffix(String byteLiteral) {
		if(byteLiteral.endsWith("y") || byteLiteral.endsWith("Y"))
			return byteLiteral.substring(0, byteLiteral.length()-1);
		else
			return byteLiteral;
	}
}
