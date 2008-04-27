/*
 * GrGen: graph rewrite generator tool -- release GrGen.NET v2 beta
 * Copyright (C) 2008 Universität Karlsruhe, Institut für Programmstrukturen und Datenorganisation, LS Goos
 * licensed under GPL v3 (see LICENSE.txt included in the packaging of this file)
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

	public TypeNode getType() {
		return BasicTypeNode.doubleType;
	}

	protected ConstNode doCastTo(TypeNode type) {
		double value = ((Double) getValue()).doubleValue();
		ConstNode res = ConstNode.getInvalid();

		if (type.isEqual(BasicTypeNode.booleanType)) {
			res = new BoolConstNode(getCoords(), value != 0 ? true : false);
		} else if (type.isEqual(BasicTypeNode.stringType)) {
			res = new StringConstNode(getCoords(), "" + value);
		} else if (type.isEqual(BasicTypeNode.floatType)) {
			res = new FloatConstNode(getCoords(), (float) value);
		} else if (type.isEqual(BasicTypeNode.intType)){
			res = new IntConstNode(getCoords(), (int) value);
		}

		return res;
	}
}
