/**
 * @author shack
 * @version $Id$
 */
package de.unika.ipd.grgen.ast.util;

import de.unika.ipd.grgen.ast.*;
import de.unika.ipd.grgen.util.report.ErrorReporter;

/**
 * A checker which checks, if the type of a declaration is in a given set.
 */
public class MultChecker implements Checker {

	/** The array of Class objects determining the types. */ 
	private Class[] validTypes;

	/**
	 * Make a new decl mult type checker giving an array of classes.
	 * The type child of the declaration must be instance of one of the classes
	 * specified in the array
	 * @param validTypes The classes to check for.
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
    
		return res;
  }

}
