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
 * An double precision floating point constant.
 */
public class DoubleConstNode extends ConstNode {

  /**
   * @param coords The coordinates.
   * @param value The double value
   */
  public DoubleConstNode(Coords coords, double v) {
    super(coords, "double", new Double(v));
  }
  
  public TypeNode getType() {
  	return BasicTypeNode.doubleType;
  }
  
	protected ConstNode doCastTo(TypeNode type) {
		double value = ((Double) getValue()).doubleValue();
		ConstNode res = ConstNode.getInvalid();
  	
  	if(type.isEqual(BasicTypeNode.booleanType)) 
  		res = new BoolConstNode(getCoords(), value != 0 ? true : false); 
	else if(type.isEqual(BasicTypeNode.stringType)) 
			res = new StringConstNode(getCoords(), "" + value);
	else if(type.isEqual(BasicTypeNode.floatType)) 
		res = new FloatConstNode(getCoords(), (float)value);
	else if(type.isEqual(BasicTypeNode.intType)) 
		res = new IntConstNode(getCoords(), (int)value);
  		
		return res;
	}


}
