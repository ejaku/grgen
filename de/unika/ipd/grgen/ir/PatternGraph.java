/**
 * PatternGraph.java
 *
 * @author Sebastian Hack
 */

package de.unika.ipd.grgen.ir;

import de.unika.ipd.grgen.util.ReadOnlyCollection;
import java.util.Collection;
import java.util.LinkedList;
import java.util.List;

/**
 * A pattern graph is a graph as it occurs in left hand rule
 * sides and negative parts. Additionally it can have
 * conditions referring to its items that restrict the set
 * of possible matchings.
 */
public class PatternGraph extends Graph {
	
	/** A list of all condition expressions. */
	private final List conds = new LinkedList();
	
	/**
	 * Add a condition to the graph.
	 * @param expr The condition's expression.
	 */
	public void addCondition(Expression expr) {
		conds.add(expr);
	}
	
	/**
	 * Get all conditions in this graph.
	 * @return A collection containing all conditions in this graph.
	 */
	public Collection getConditions() {
		return ReadOnlyCollection.get(conds);
	}
	
	
}

