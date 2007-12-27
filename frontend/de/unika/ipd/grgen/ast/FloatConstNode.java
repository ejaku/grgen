/*
  GrGen: graph rewrite generator tool.
  Copyright (C) 2005  IPD Goos, Universit"at Karlsruhe, Germany

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
 * @author Sebastian Hack
 * @version $Id$
 */
package de.unika.ipd.grgen.ast;

import de.unika.ipd.grgen.parser.Coords;

/**
 * An single precision floating point constant.
 */
public class FloatConstNode extends ConstNode
{
	/**
	 * @param coords The coordinates.
	 * @param value The float value
	 */
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
