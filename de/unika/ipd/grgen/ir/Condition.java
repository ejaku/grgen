/**
 * @author Rubino Geiss, Michael Beck
 * @version $Id$
 */
package de.unika.ipd.grgen.ir;
import java.util.Iterator;
import java.util.LinkedList;

public class Condition extends IR {
	/**
	 * The conditions constituing a Condition of Rule.
	 * They are orgnized in a list, since their order may be vital.
	 * Applying them in a random order may lead to different results.
	 */
	private LinkedList conditions = new LinkedList();
	
	/**
	 * Constructor
	 */
	protected Condition() {
		super("cond");
	}
	
	/**
	 * Method add adds an element to the list of conditions.
	 * @param expression An expression.
	 */
	public void add(Expression expression) {
		conditions.add(expression);
	}
	
	/**
	 * Get an iterator over all conditions.
	 * @return The iterator.
	 */
	public Iterator get() {
		return conditions.iterator();
	}
	
	/**
	 * Method iterator returns a Iterator of its elements.
	 *
	 * @return   an Iterator
	 */
	public Iterator getWalkableChildren() {
		return conditions.iterator();
	}
}

