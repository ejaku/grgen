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
	public Test(Ident ident, Graph pattern) {
		super("test", ident, pattern);
		pattern.setNameSuffix("test");
		coalesceAnonymousEdges();
	}

	/**
	 * Anonymous edges that connect the same nodes on both sides of rule
	 * shall also become the same Edge node. This not the case when
	 * the Rule is constructed, since the equality of edges is established
	 * by the coincidence of their identifiers. Anonymous edges have no
	 * identifiers, so they have to be coalesced right now, when both
	 * sides of the rule are known and set up.
	 */
	private void coalesceAnonymousEdges()
	{
		for(Iterator it = pattern.getEdges(new HashSet()).iterator(); it.hasNext();) {
			Edge e = (Edge) it.next();
			
			if (e.isAnonymous()) {
				for(Iterator nIt = getNegs(); nIt.hasNext();) {
					Graph neg = (Graph) nIt.next();
					neg.replaceSimilarEdges(pattern, e);
				}
			}
		}
	}

}
