/**
 * @author shack, Daniel Grund
 * @version $Id$
 */
package de.unika.ipd.grgen.ir;

import java.util.Collection;
import java.util.HashSet;
import java.util.Iterator;
import java.util.LinkedList;
import java.util.List;

import de.unika.ipd.grgen.util.ArrayIterator;

/**
 * A replacement rule.
 */
public class Rule extends MatchingAction
{
	/** Names of the children of this node. */
	private static final String[] childrenNames =
	{
		"left", "neg", "right", "cond", "eval"
	};
	
	/** The right hand side of the rule. */
	private Graph right;
	
	/**
	 * The redirections of the rule.
	 * They are orgnized in a list, since their order is vital.
	 * Applying them in a random order will lead to different results.
	 */
	private List redirections = new LinkedList();
	
	/**
	 * The evaluation of this rule.
	 */
	private Evaluation evaluation = new Evaluation();
	
	/**
	 * Make a new rule.
	 * @param ident The identifier with which the rule was declared.
	 * @param left The left side graph of the rule.
	 * @param right The right side graph of the rule.
	 */
	public Rule(Ident ident, Graph left, Graph neg, Graph right)
	{
		super("rule", ident, left, neg);
		setChildrenNames(childrenNames);
		this.right = right;
		left.setNameSuffix("left");
		right.setNameSuffix("right");
		coalesceAnonymousEdges();
	}
	
	/**
	 * Add a new redirection to the rule.
	 * @param from The node, which's edges should be redirected.
	 * @param to The node to which the edges of <code>from</code> shall
	 * belong now.
	 * @param et The edge type of which the edges must be, if they shall
	 * be redirected.
	 * @param nt The node type of which the the other end nodes of the edges
	 * must be that the redirection takes place.
	 * @param incoming Specifies if the edges are leaving or hitting
	 * <code>from</code>.
	 * An exmple:
	 * <code>from</code>: a <br>
	 * <code>to</code>: b <br>
	 * <code>et</code>: E1 <br>
	 * <code>nt</code>: N1 <br>
	 * <code>incoming</code>: <code>false</code><br>
	 * This selects all edges which leave the node a, are of
	 * type E1 and hit nodes of type N1. The source of these edges is
	 * rewritten to be b after the redirection.
	 */
	public void addRedirection(Node from, Node to, EdgeType et, NodeType nt,
							   boolean incoming)
	{
		redirections.add(new Redirection(from, to, et, nt, incoming));
	}
	
	/**
	 * Return a list of redirections.
	 * @return The redirections.
	 */
	public List getRedirections()
	{
		return redirections;
	}
	
	/**
	 * Return a list of evaluations.
	 * @return The evaluations.
	 */
	public Evaluation getEvaluation()
	{
		return evaluation;
	}
	
	
	/**
	 * Anonymous edges that connect the same nodes on both sides of rule
	 * shall also become the same Edge node. This not the case when
	 * the Rule is constructed, since the equality of edges is established
	 * by the coincidence of their identifiers. Anonymous edges have no
	 * identifiers, so they have to be coalesced right now, when both
	 * sides of the rule are known and set up.
	 */
	private void coalesceAnonymousEdges()
	{
		for(Iterator it = pattern.getEdges(new HashSet()).iterator(); it.hasNext();)
		{
			Edge e = (Edge) it.next();
			
			if (e.isAnonymous()) {
				right.replaceSimilarEdges(pattern, e);
				//TOODO Is this right?
				neg.replaceSimilarEdges(pattern, e);
			}
		}
	}
	
	/**
	 * Get the set of nodes the left and right side have in common.
	 * @return A set with nodes, that occur on the left and on the right side
	 * of the rule.
	 */
	public Collection getCommonNodes()
	{
		Collection common = pattern.getNodes(new HashSet());
		Collection rightNodes = right.getNodes(new HashSet());
		common.retainAll(rightNodes);
		return common;
	}
	
	/**
	 * Get the set of nodes the graph g1 and g2 have in common.
	 * @return A set with nodes, that occur in both graphs.
	 */
	//TODO DG move this into graph and adapt it
	public Collection getCommonNodes(Graph g1, Graph g2) {
		Collection c1 = g1.getNodes(new HashSet());
		Collection c2 = g2.getNodes(new HashSet());
		c1.retainAll(c2);
		return c1;
	}	
	
	/**
	 * Get the set of edges that are common to both sides of the rule.
	 * @return The set containing all common edges.
	 */
	public Collection getCommonEdges()
	{
		Collection common = pattern.getEdges(new HashSet());
		Collection rightEdges = right.getEdges(new HashSet());
		right.getEdges(rightEdges);
		common.retainAll(rightEdges);
		return common;
	}
	
	/**
	 * Get the set of edges the graph g1 and g2 have in common.
	 * @return A set with edges, that occur in both graphs.
	 */
	public Collection getCommonEdges(Graph g1, Graph g2) {
		Collection c1 = g1.getEdges(new HashSet());
		Collection c2 = g2.getEdges(new HashSet());
		c1.retainAll(c2);
		return c1;
	}

	/**
	 * Get the left hand side.
	 * @return The left hand side graph.
	 */
	public Graph getLeft()
	{
		return pattern;
	}

	/**
	 * Get the right hand side.
	 * @return The right hand side graph.
	 */
	public Graph getRight()
	{
		return right;
	}
	
	/**
	 * @see de.unika.ipd.grgen.util.Walkable#getWalkableChildren()
	 */
	public Iterator getWalkableChildren()
	{
		return new ArrayIterator(new Object[] { pattern, neg, right,
				condition, evaluation });
	}
}
