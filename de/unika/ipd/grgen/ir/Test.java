/**
 * @author shack
 * @version $Id$
 */
package de.unika.ipd.grgen.ir;

/**
 * A Subgraph Test.
 */
public class Test extends MatchingAction {

	
  /**
   * @param ident Identifier of the Test.
   * @param patternThe test subgraph.
   */
  public Test(Ident ident, Graph pattern, Graph neg) {
    super("test", ident, pattern, neg);
    pattern.setNameSuffix("test");

    //TODO DG coalesceAnonymousEdges() needed? Yes. Insert below after checkin
    
  }
}
