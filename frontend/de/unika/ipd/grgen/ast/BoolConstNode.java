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
 * A boolean constant.
 */
public class BoolConstNode extends ConstNode
{
	public BoolConstNode(Coords coords, boolean value) {
		super(coords, "boolean", new Boolean(value));
	}

	public TypeNode getType() {
		return BasicTypeNode.booleanType;
	}

	/** @see de.unika.ipd.grgen.ast.ConstNode#doCastTo(de.unika.ipd.grgen.ast.TypeNode) */
	protected ConstNode doCastTo(TypeNode type) {
		boolean value = ((Boolean) getValue()).booleanValue();
		ConstNode res = ConstNode.getInvalid();

		if (type.isEqual(BasicTypeNode.intType)) {
			res = new IntConstNode(getCoords(), value ? 1 : 0);
		} else if (type.isEqual(BasicTypeNode.stringType)) {
			res = new StringConstNode(getCoords(), "" + value);
		}

		return res;
	}
}
