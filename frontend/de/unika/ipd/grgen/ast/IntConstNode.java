/*
 * GrGen: graph rewrite generator tool -- release GrGen.NET 3.0
 * Copyright (C) 2003-2011 Universitaet Karlsruhe, Institut fuer Programmstrukturen und Datenorganisation, LS Goos
 * licensed under LGPL v3 (see LICENSE.txt included in the packaging of this file)
 * www.grgen.net
 */

/**
 * @author Sebastian Hack
 * @version $Id$
 */
package de.unika.ipd.grgen.ast;

import de.unika.ipd.grgen.parser.Coords;

/**
 * An integer constant.
 */
public class IntConstNode extends ConstNode
{
	public IntConstNode(Coords coords, int v) {
		super(coords, "integer", new Integer(v));
	}

	@Override
	public TypeNode getType() {
		return BasicTypeNode.intType;
	}

	@Override
	protected ConstNode doCastTo(TypeNode type) {
		Integer value = (Integer) getValue();

		if (type.isEqual(BasicTypeNode.byteType)) {
			return new ByteConstNode(getCoords(), (byte)(int)value);
		} else if (type.isEqual(BasicTypeNode.shortType)) {
			return new ShortConstNode(getCoords(), (short)(int)value);
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
}
