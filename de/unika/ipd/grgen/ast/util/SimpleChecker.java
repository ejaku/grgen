/**
 * @author shack
 * @version $Id$
 */
package de.unika.ipd.grgen.ast.util;

import de.unika.ipd.grgen.ast.BaseNode;
import de.unika.ipd.grgen.util.report.ErrorReporter;

/**
 * A simple checker, that checks if the node is instance of a certain class 
 */
public class SimpleChecker implements Checker {

	/// The class the node is to checked for
	private Class c;

	public SimpleChecker(Class c) {
		this.c = c;
	}

  /**
   * Just check, if node is an instance of c
   * @see de.unika.ipd.grgen.ast.check.Checker#check(de.unika.ipd.grgen.ast.BaseNode, de.unika.ipd.grgen.util.report.ErrorReporter)
   */
  public boolean check(BaseNode node, ErrorReporter reporter) {
  	boolean res = c.isInstance(node);
  	
  	if(!res)
  		node.reportError("not of type " + c.getName());
  		
  	return res;	
  }
}
