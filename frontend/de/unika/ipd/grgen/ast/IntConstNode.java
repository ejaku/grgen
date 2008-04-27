/*
 * GrGen: graph rewrite generator tool -- release GrGen.NET v2 beta
 * Copyright (C) 2008 Universität Karlsruhe, Institut für Programmstrukturen und Datenorganisation, LS Goos
 * licensed under GPL v3 (see LICENSE.txt included in the packaging of this file)
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

	public TypeNode getType() {
		return BasicTypeNode.intType;
	}

	protected ConstNode doCastTo(TypeNode type) {
		int value = ((Integer) getValue()).intValue();
		ConstNode res = ConstNode.getInvalid();

		if (type.isEqual(BasicTypeNode.booleanType)) {
			res = new BoolConstNode(getCoords(), value != 0 ? true : false);
		} else if (type.isEqual(BasicTypeNode.stringType)) {
			res = new StringConstNode(getCoords(), "" + value);
		}

		return res;
	}
}
