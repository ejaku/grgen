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
 * A short constant.
 */
public class ShortConstNode extends ConstNode
{
	public ShortConstNode(Coords coords, short v) {
		super(coords, "short", new Short(v));
	}

	@Override
	public TypeNode getType() {
		return BasicTypeNode.shortType;
	}

	@Override
	protected ConstNode doCastTo(TypeNode type) {
		Short value = (Short) getValue();

		if (type.isEqual(BasicTypeNode.byteType)) {
			return new ByteConstNode(getCoords(), (byte)(short)value);
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
	
	public static String removeSuffix(String shortLiteral) {
		if(shortLiteral.endsWith("s") || shortLiteral.endsWith("S"))
			return shortLiteral.substring(0, shortLiteral.length()-1);
		else
			return shortLiteral;
	}
}
