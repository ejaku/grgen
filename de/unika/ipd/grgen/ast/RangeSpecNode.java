/**
 * @author Sebastian Hack
 * @version $Id$
 */
package de.unika.ipd.grgen.ast;

import de.unika.ipd.grgen.parser.Coords;

/**
 * Specification of a range. 
 */
public class RangeSpecNode extends BaseNode {

	/** Constant, signalling if one bound is bounded. */
	public static final int UNBOUND = Integer.MAX_VALUE;

	/** The upper and lower bound. */
	private int lower, upper;

  /**
   * @param coords
   */
  public RangeSpecNode(Coords coords, int lower, int upper) {
		super(coords);
		this.lower = lower;
		this.upper = upper;
  }

	public boolean isBoundedUp() {
		return upper != UNBOUND;
	}
	
	public boolean isBoundedLow() {
		return lower != UNBOUND;
	}

  /**
   * @return
   */
  public int getLower() {
    return lower;
  }

  /**
   * @return
   */
  public int getUpper() {
    return upper;
  }

}
