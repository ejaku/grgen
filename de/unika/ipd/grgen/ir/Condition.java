/**
 * Condition.java
 *
 * @author Rubino Geiss, Michael Beck
 */

package de.unika.ipd.grgen.ir;
import java.util.LinkedList;
import java.util.Iterator;



public class Condition extends IR
{
	/**
	 * The conditions constituing a Condition of Rule.
	 * They are orgnized in a list, since their order may be vital.
	 * Applying them in a random order may lead to different results.
	 */
	private LinkedList conditions = new LinkedList();
	
	/**
	 * Constructor
	 */
	protected Condition()
	{
		super("cond");
	}
	
	/**
	 * Method add adds an element to the list of conditions.
	 *
	 * @param    acond               an IR
	 */
	public void add(IR acond)
	{
		conditions.add(acond);
	}
	
	/**
	 * Method iterator returns a Iterator of its elements.
	 *
	 * @return   an Iterator
	 */
	public Iterator getWalkableChildren()
	{
		return conditions.iterator();
	}
}

