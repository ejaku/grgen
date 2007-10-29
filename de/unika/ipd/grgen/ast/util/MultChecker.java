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
 * @author shack
 * @version $Id$
 */
package de.unika.ipd.grgen.ast.util;

import de.unika.ipd.grgen.ast.*;
import de.unika.ipd.grgen.util.report.ErrorReporter;

/**
 * A checker which checks, if a given AST node ist instance of some types.
 */
public class MultChecker implements Checker {
	/** The array of Class objects determining the types. */
	private Class[] validTypes;
	
	/**
	 * Make a new decl mult type checker giving an array of classes.
	 * The type child of the declaration must be instance of one of the classes
	 * specified in the array.
	 * @param validTypes The classes a given node is to be checked against.
	 */
	public MultChecker(Class[] validTypes) {
		this.validTypes = validTypes;
	}
	
	/**
	 * @see de.unika.ipd.grgen.ast.check.Checker#check(de.unika.ipd.grgen.ast.BaseNode, de.unika.ipd.grgen.util.report.ErrorReporter)
	 */
	public boolean check(BaseNode node, ErrorReporter reporter) {
		boolean res = false;
		
		// If the declaration's type is an instance of the desired class
		// everything's fine, else report errors
		for(int i = 0; i < validTypes.length; i++)
			if(validTypes[i].isInstance(node)) {
				res = true;
				break;
			}
		
		if(!res) {
			node.reportError("Unknown type");
		}
		
		return res;
	}
}
