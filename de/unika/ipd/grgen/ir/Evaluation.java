/**
 * @author Rubino Geiss, Michael Beck
 * @version $Id$
 */
package de.unika.ipd.grgen.ir;
import java.util.Collection;
import java.util.LinkedList;

public class Evaluation extends IR {
	/**
	 * The evaluations constituing a Evaluation of Rule.
	 * They are orgnized in a list, since their order is vital.
	 * Applying them in a random order will lead to different results.
	 */
	private LinkedList<IR> evaluations = new LinkedList<IR>();
	
	/**
	 * Constructor
	 *
	 */
	Evaluation() {
		super("eval");
	}
	
	/**
	 * Method add adds an element to the list of evaluations.
	 *
	 * @param    aeval               an IR
	 */
	public void add(IR aeval) {
		evaluations.add(aeval);
	}
	
	/**
	 * Method iterator returns a Iterator of its elements.
	 *
	 * @return   an Iterator
	 */
	public Collection<?extends IR> getWalkableChildren() {
		return evaluations;
	}
}

