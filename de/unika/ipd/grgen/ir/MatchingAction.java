/**
 * @author Sebastian Hack
 * @version $Id$
 */
package de.unika.ipd.grgen.ir;

import java.util.Collection;
import java.util.Iterator;
import java.util.LinkedList;

import de.unika.ipd.grgen.util.ArrayIterator;

/**
 * An action that represents something that does graph matching.
 */
public abstract class MatchingAction extends Action {

	/** Children names of this node. */
	private static final String[] childrenNames = {
	  "pattern", "negative"
	};

	/** The graph pattern to match against. */
	protected Graph pattern;

	/** The NAC part of the rule. */
	protected Collection negs = new LinkedList(); //holds Objects of type Graph

	/** The condition of this rule. */
	protected Condition condition = new Condition();


	/**
	 * @param name The name of this action.
	 * @param ident The identifier that identifies this object.
	 * @param pattern The graph pattern to match against.
	 */
	public MatchingAction(String name, Ident ident, Graph pattern) {
		super(name, ident, null);
		this.pattern = pattern;
		pattern.setNameSuffix("pattern");
        setChildrenNames(childrenNames);
	}
  
	/**
	* Get the graph pattern.
	* @return The graph pattern.
	*/
	public Graph getPattern() {
		return pattern;
	}
  

	public void addNegGraph(Graph neg) {
		if (neg.getNodes().hasNext()) negs.add(neg);
	}
	
	/**
	 * Get the NAC part.
	 * @return The NAC graph of the rule.
	 */
 	public Iterator getNegs() {
		return negs.iterator();  	
 	}

	/**
	 * Return a list of conditions.
	 * @return The conditions.
	 */
	public Condition getCondition() {
		return condition;
	}

	/**
	 * @see de.unika.ipd.grgen.util.Walkable#getWalkableChildren()
	 */
	public Iterator getWalkableChildren() {
		//TODO dg ???
		return new ArrayIterator(new Object[] { pattern, condition/*, negs*/ });
	}
}
