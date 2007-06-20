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
 * @author adam
 * @version $Id$
 */
package de.unika.ipd.grgen.ast.util;

import de.unika.ipd.grgen.ast.BaseNode;
import de.unika.ipd.grgen.ast.DeclNode;
import de.unika.ipd.grgen.util.report.ErrorReporter;
import de.unika.ipd.grgen.util.Util;

/**
 * A type checker, that checks if the declaration node is of a certain type
 */
public class TypeChecker implements Checker {

	/// The classes the decl type is to checked for
	private Class[] validTypes;

	public TypeChecker(Class[] types) {
		this.validTypes = types;
	}
	
	public TypeChecker(Class c) {
		this(new Class[] { c });
	}

  /**
   * Check, if node is an instance of DeclNode and then check, if the declaration
   * has the right type
   * @see de.unika.ipd.grgen.ast.check.Checker#check(de.unika.ipd.grgen.ast.BaseNode, de.unika.ipd.grgen.util.report.ErrorReporter)
   */
  public boolean check(BaseNode node, ErrorReporter reporter) {
  	boolean res = (node instanceof DeclNode);
  	
  	if(!res) {
  		node.reportError("not a " + DeclNode.getKindStr());
  	} else {
  		BaseNode type = ((DeclNode)node).getDeclType();
 
  		res = false;
  	  	for(Class c : this.validTypes) {
  	  		if(c.isInstance(type)) {
  	  			res = true;
  	  			break;
  	  		}
  	  	}
  		
  		if(!res) {
			try {
				String list = Util.getStrListWithOr(validTypes, BaseNode.class,	"getUseStr");

				node.reportError("this declaration should declare a " + list);
			}
			catch(Exception e) {}
		}
	}
  	return res;
  }
}
