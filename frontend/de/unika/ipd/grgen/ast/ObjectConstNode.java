/*
 * GrGen: graph rewrite generator tool -- release GrGen.NET v2 beta
 * Copyright (C) 2008 Universitaet Karlsruhe, Institut fuer Programmstrukturen und Datenorganisation, LS Goos
 * licensed under GPL v3 (see LICENSE.txt included in the packaging of this file)
 */

/**
 * A const node of type object.
 * There is exactly one possible value for constants of type object, namely
 * the value 'null' represented by {@see #NULL_VALUE NULL_VALUE}.
 * @see de.unika.ipd.grgen.ast.ObjectConstNode.NullObject
 *
 * @author G. Veit Batz
 */

package de.unika.ipd.grgen.ast;

import de.unika.ipd.grgen.parser.Coords;

public class ObjectConstNode extends ConstNode {
	public ObjectConstNode(Coords coords) {
		super(coords, "object", ObjectTypeNode.Value.NULL);
	}

	public TypeNode getType() {
		return BasicTypeNode.objectType;
	}

	protected ConstNode doCastTo(TypeNode type)	{
		if (type.isEqual(BasicTypeNode.stringType) ) {
			return new StringConstNode(getCoords(), getValue().toString());
		} else throw new UnsupportedOperationException();
	}

	public String toString() {
		return "Const (object)null";
	}
}

