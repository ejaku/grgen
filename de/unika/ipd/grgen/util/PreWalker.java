/**
 * @author Sebastian Hack
 * @version $Id$
 */
package de.unika.ipd.grgen.util;

/**
 * Just a Pre Walker
 */
public class PreWalker extends PrePostWalker {

	/**
	 * Make a new pre walker.
	 * @param v The visitor to use before descending to the children of a node.
	 */
	public PreWalker(Visitor v) {
		super(v, new Visitor() {
			public void visit(Walkable w) {
			}
		});
	}

}
