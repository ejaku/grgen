/*
 GrGen: graph rewrite generator tool.
 Copyright (C) 2007  IPD Goos, Universit"at Karlsruhe, Germany

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
		if ( type.isEqual(BasicTypeNode.objectType) ) {
			return this;
		}
		return ConstNode.getInvalid();
	}

	public String toString() {
		return "Const (object)null";
	}
}

