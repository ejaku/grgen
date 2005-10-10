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
package de.unika.ipd.grgen.util;

/**
 * A visitor that takes a parameter array.
 */
public abstract class ParamVisitor implements Visitor {

	private Object[] parameters;

	/**
	 * Get the i-th parameter.
	 * @param i The number of the parameter.
	 * @return The i-th parameter, null, if i was greater than the number of
	 * parameters.
	 */
	protected Object getParameter(int i) {
		return i < parameters.length ? parameters[i] : null; 
	}

  /**
   * Make a new parameter visitor.
   * @param params The parameter for the visitor.
   */
  public ParamVisitor(Object[] params) {
    parameters = params;
  }
  
  /**
   * Make a new parameter visitor with one parameter. 
   * @param param The parameter.
   */
  public ParamVisitor(Object param) {
  	this(new Object[] { param });
  }

}
