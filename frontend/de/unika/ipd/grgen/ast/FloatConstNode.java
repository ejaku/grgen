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
 * An single precision floating point constant.
 */
public class FloatConstNode extends ConstNode
{
	public FloatConstNode(Coords coords, double v) {
		super(coords, "float", new Float(v));
	}

	public TypeNode getType() {
		return BasicTypeNode.floatType;
	}

	protected ConstNode doCastTo(TypeNode type) {
		float value = ((Float) getValue()).floatValue();
		ConstNode res = ConstNode.getInvalid();

		if (type.isEqual(BasicTypeNode.booleanType)) {
			res = new BoolConstNode(getCoords(), value != 0 ? true : false);
		} else if (type.isEqual(BasicTypeNode.stringType)) {
			res = new StringConstNode(getCoords(), "" + value);
		} else if (type.isEqual(BasicTypeNode.doubleType)) {
			res = new DoubleConstNode(getCoords(), (double) value);
		} else if (type.isEqual(BasicTypeNode.intType)) {
			res = new IntConstNode(getCoords(), (int) value);
		}

		return res;
	}
}
