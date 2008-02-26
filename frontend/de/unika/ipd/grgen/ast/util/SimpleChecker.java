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

import de.unika.ipd.grgen.ast.BaseNode;
import de.unika.ipd.grgen.util.report.ErrorReporter;

/**
 * A checker that checks if the AST node is an instance of one of the specified types.
 */
public class SimpleChecker implements Checker
{
	/** The types the node is to be checked against. */
	private Class<?>[] validTypes;

	/** Create checker with one type to check the AST node against */
	public SimpleChecker(Class<?> validType)
	{
		this.validTypes = new Class[] { validType };
	}

	/** Create checker with the types to check the AST node against */
	public SimpleChecker(Class<?>[] validTypes)
	{
		this.validTypes = validTypes;
	}

	/**
	 * Just check whether the node is an instance of one of the valid types
	 * @see de.unika.ipd.grgen.ast.check.Checker#check(de.unika.ipd.grgen.ast.BaseNode, de.unika.ipd.grgen.util.report.ErrorReporter)
	 */
	public boolean check(BaseNode node, ErrorReporter reporter)
	{
		boolean res = false;

		// If the declaration's type is an instance of the desired class
		// everything's fine, else report errors

		for(int i = 0; i < validTypes.length; i++) {
			if(validTypes[i].isInstance(node)) {
				res = true;
				break;
			}
		}

		if(!res) {
			if(validTypes.length==1) {
				node.reportError("AST node " + node.getName() + " must be an instance of type " + shortClassName(validTypes[0]));
			} else {
				node.reportError("AST node " + node.getName() + " - Unknown type");
			}
		}

		return res;
	}

	/**
	 * Strip the package name from the class name.
	 * @param cls The class.
	 * @return stripped class name.
	 */
	protected static String shortClassName(Class<?> cls) {
		String s = cls.getName();
		return s.substring(s.lastIndexOf('.') + 1);
	}
}
