/**
 * @author shack, Daniel Grund
 * @version $Id$
 */
package de.unika.ipd.grgen.ir;

import de.unika.ipd.grgen.util.ArrayIterator;
import de.unika.ipd.grgen.util.ReadOnlyCollection;
import java.util.Collection;
import java.util.HashSet;
import java.util.Iterator;
import java.util.LinkedList;

/**
 * A replacement rule.
 */
public class Rule extends MatchingAction {
	/** Names of the children of this node. */
	private static final String[] childrenNames = {
		"left", "right", "eval"
	};

	/** The right hand side of the rule. */
	private final Graph right;

	/** The evaluation assignments of this rule. */
	private final Collection evals = new LinkedList();

	/**
	 * Make a new rule.
	 * @param ident The identifier with which the rule was declared.
	 * @param left The left side graph of the rule.
	 * @param right The right side graph of the rule.
	 */
	public Rule(Ident ident, PatternGraph left, Graph right) {
		super("rule", ident, left);
		setChildrenNames(childrenNames);
		this.right = right;
		right.setNameSuffix("replace");
		// coalesceAnonymousEdges(); not here, because neg-graphs not added yet.
	}

	/**
	 * Get the eval assignments of this rule.
	 * @return A collection containing all eval assignments.
	 */
	public Collection getEvals() {
		return ReadOnlyCollection.getSingle(evals);
	}

	/**
	 * Add an assignment to the list of evaluations.
	 * @param a The assignment.
	 */
	public void addEval(Assignment a) {
		evals.add(a);
	}

	/**
	 * Get the set of nodes the left and right side have in common.
	 * @return A set with nodes, that occur on the left and on the right side
	 * of the rule.
	 */
	public Collection getCommonNodes() {
		Collection common = pattern.getNodes(new HashSet());
		Collection rightNodes = right.getNodes(new HashSet());
		common.retainAll(rightNodes);
		return common;
	}

	/**
	 * Get the set of edges that are common to both sides of the rule.
	 * @return The set containing all common edges.
	 */
	public Collection getCommonEdges() {
		Collection common = pattern.getEdges(new HashSet());
		Collection rightEdges = right.getEdges(new HashSet());
		right.getEdges(rightEdges);
		common.retainAll(rightEdges);
		return common;
	}

	/**
	 * Get all graphs that are involved in this rule besides
	 * the pattern part.
	 * For an ordinary matching actions, these are the negative ones.
	 * @return A collection holding all additional graphs in this
	 * matching action.
	 */
	public Collection getAdditionalGraphs() {
		Collection res = new LinkedList(super.getAdditionalGraphs());
		res.add(right);
		return res;
	}


	/**
	 * Get the left hand side.
	 * @return The left hand side graph.
	 */
	public Graph getLeft() {
		return pattern;
	}

	/**
	 * Get the right hand side.
	 * @return The right hand side graph.
	 */
	public Graph getRight() {
		return right;
	}
}
