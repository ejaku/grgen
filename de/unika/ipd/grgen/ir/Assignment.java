/**
 * @author Rubino Geiss
 * @version $Id$
 */
package de.unika.ipd.grgen.ir;


import java.util.Iterator;

import de.unika.ipd.grgen.util.ArrayIterator;

/**
 * Abstract base class for all constants.
 */
public class Assignment extends IR {
	
	private static final String[] childrenNames = { "lhs", "rhs" };
	
	/** The lhs of the assignment. */
	private IR lhs;
	
	/** The rhs of the assignment. */
	private IR rhs;
	
	public Assignment(Qualification lhs, IR rhs) {
		super("assign");
		setChildrenNames(childrenNames);
		this.lhs = lhs;
		this.rhs = rhs;
	}
	
	public Object getLhs() {
		return lhs;
	}
	
	public Object getRhs() {
		return rhs;
	}
	
	/**
	 * @see de.unika.ipd.grgen.util.GraphDumpable#getNodeLabel()
	 */
	public String getNodeLabel() {
		return getName() + " " + lhs + " := " + rhs;
	}
	
	/**
	 * @see de.unika.ipd.grgen.util.Walkable#getWalkableChildren()
	 */
	public Iterator getWalkableChildren() {
		return new ArrayIterator(new Object[] {lhs, rhs });
	}
	
}
