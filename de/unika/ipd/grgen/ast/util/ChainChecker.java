/**
 * @author Sebastian Hack
 * @version $Id$
 */
package de.unika.ipd.grgen.ast.util;

import de.unika.ipd.grgen.ast.BaseNode;
import de.unika.ipd.grgen.util.report.ErrorReporter;

/**
 * 
 */
public class ChainChecker implements Checker {

	private Checker[] checkers;

  /**
   * 
   */
  public ChainChecker(Checker[] checkers) {
    super();
    this.checkers = checkers;
  }

  /**
   * @see de.unika.ipd.grgen.ast.util.Checker#check(de.unika.ipd.grgen.ast.BaseNode, de.unika.ipd.grgen.util.report.ErrorReporter)
   */
  public boolean check(BaseNode node, ErrorReporter reporter) {
  	boolean res = true;
  	
  	for(int i = 0; i < checkers.length; i++) {
  		boolean r = checkers[i].check(node, reporter);
  		
  		res = res && r;
  	}
  	
  	return res;
  }

}
