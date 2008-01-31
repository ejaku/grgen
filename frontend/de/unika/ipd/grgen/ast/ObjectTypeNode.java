/*
 GrGen: graph rewrite generator tool.
 Copyright (C) 2008  IPD Goos, Universit"at Karlsruhe, Germany

 This library is free software; you can redistribute it and/or
 modify it under the terms of the GNU Lesser General Public
 License as published by the Free Software Foundation; either
 version 2.1 of the License, or (at your option) any later version.

 This library is distributed in the hope that it will be useful,
 but WITHOUT ANY WARRANTY; without even the implied warranty of
 MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the GNU
 Lesser General Public License for more details.

 You should have received a copy of the GNU Lesser General Public
 License along with this library; if not, write to the Free Software
 Foundation, Inc., 51 Franklin St, Fifth Floor, Boston, MA  02110-1301  USA
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

