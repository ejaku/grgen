/**
 * @author shack
 * @version $Id$
 */
package de.unika.ipd.grgen.ast.util;

import de.unika.ipd.grgen.ast.BaseNode;
import de.unika.ipd.grgen.ast.CollectNode;
import de.unika.ipd.grgen.util.report.ErrorReporter;

/**
 * A checker that checks if the node is a collection node and
 * applies a second checker to all the children
 */
public class CollectChecker implements Checker {

	/// The checker to apply to the children of the checked collect node
	private Checker childChecker;
	
	public CollectChecker(Checker childChecker) {
		this.childChecker = childChecker;
	}
	
  /**
   * Check if the node is a collect node and apply the child checker to
   * all children.
   * @see de.unika.ipd.grgen.ast.check.Checker#check(de.unika.ipd.grgen.ast.BaseNode, de.unika.ipd.grgen.util.report.ErrorReporter)
   */
  public boolean check(BaseNode node, ErrorReporter reporter) {
  	boolean res = false;
  	
  	if(node instanceof CollectNode) 
  		res = node.checkAllChildren(childChecker);
  	else
  		node.reportError("not a collect node");
  		
  	return res;
  }
}
