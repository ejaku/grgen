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
	static {
		setName(RangeSpecNode.class, "range spec");
	}
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
	
	public String getName() {
		return super.getName() + " [" + lower + ":" + upper + "]";
	}
	
	public boolean isBoundedUp() {
		return upper != UNBOUND;
	}
	
	public boolean isBoundedLow() {
		return lower != UNBOUND;
	}
	
	/**
	 * @return the lower bound of the range.
	 */
	public int getLower() {
		return lower;
	}
	
	/**
	 * @return the upper bound of the range.
	 */
	public int getUpper() {
		return upper;
	}
	
}
