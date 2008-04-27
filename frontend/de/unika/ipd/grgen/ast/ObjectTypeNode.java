/*
 * GrGen: graph rewrite generator tool -- release GrGen.NET v2 beta
 * Copyright (C) 2008 Universität Karlsruhe, Institut für Programmstrukturen und Datenorganisation, LS Goos
 * licensed under GPL v3 (see LICENSE.txt included in the packaging of this file)
 */

/**
 * Represents the basic type 'object'
 *
 * @author G. Veit Batz
 */

package de.unika.ipd.grgen.ast;
import de.unika.ipd.grgen.ir.IR;
import de.unika.ipd.grgen.ir.ObjectType;


public class ObjectTypeNode extends BasicTypeNode {
	static {
		setName(ObjectTypeNode.class, "object type");
	}

	/**
	 * Singleton class representing the only constant value 'null' that
	 * the basic type 'object' has.
	 */
	public static class Value {
		public static Value NULL = new Value() {
			public String toString() { return "Const null"; }
		};

		private Value() {}

		public boolean equals(Object val) {
			return (this == val);
		}
	}

	protected static ObjectTypeNode OBJECT_TYPE = new ObjectTypeNode();

	private ObjectTypeNode() {}

	protected IR constructIR() {
		return new ObjectType(getIdentNode().getIdent());
	}

	public String toString() {
		return "object";
	}
}

