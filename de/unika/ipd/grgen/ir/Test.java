/**
 * @author shack
 * @version $Id$
 */
package de.unika.ipd.grgen.ir;

import java.util.HashSet;
import java.util.Iterator;

/**
 * A Subgraph Test.
 */
public class Test extends MatchingAction {

	/**
	 * @param ident Identifier of the Test.
	 * @param patternThe test subgraph.
	 */
	public Test(Ident ident, PatternGraph pattern) {
		super("test", ident, pattern);
		pattern.setNameSuffix("test");
		coalesceAnonymousEdges();
	}


}
