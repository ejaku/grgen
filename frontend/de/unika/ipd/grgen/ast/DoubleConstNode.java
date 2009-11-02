/*
 * GrGen: graph rewrite generator tool -- release GrGen.NET 2.6
 * Copyright (C) 2003-2009 Universitaet Karlsruhe, Institut fuer Programmstrukturen und Datenorganisation, LS Goos
 * licensed under LGPL v3 (see LICENSE.txt included in the packaging of this file)
 */

/**
 * @version $Id$
 */
package de.unika.ipd.grgen.ast;

import de.unika.ipd.grgen.parser.Coords;

/**
 * An double precision floating point constant.
 */
public class DoubleConstNode extends ConstNode
{
	public DoubleConstNode(Coords coords, double v) {
		super(coords, "double", new Double(v));
	}

	@Override
	public TypeNode getType() {
		return BasicTypeNode.doubleType;
	}

	@Override
	protected ConstNode doCastTo(TypeNode type) {
		Double value = (Double) getValue();

		if (type.isEqual(BasicTypeNode.intType)){
			return new IntConstNode(getCoords(), (int)(double) value);
		} else if (type.isEqual(BasicTypeNode.floatType)) {
			return new FloatConstNode(getCoords(), (float)(double) value);
		} else if (type.isEqual(BasicTypeNode.stringType)) {
			return new StringConstNode(getCoords(), value.toString());
		} else throw new UnsupportedOperationException();
	}
}
