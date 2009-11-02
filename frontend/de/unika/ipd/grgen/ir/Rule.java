/*
 * GrGen: graph rewrite generator tool -- release GrGen.NET 2.5
 * Copyright (C) 2009 Universitaet Karlsruhe, Institut fuer Programmstrukturen und Datenorganisation, LS Goos
 * licensed under LGPL v3 (see LICENSE.txt included in the packaging of this file)
 */

/**
 * @author shack, Daniel Grund
 * @version $Id$
 */
package de.unika.ipd.grgen.ir;

import java.util.Collection;
import java.util.Collections;
import java.util.HashSet;
import java.util.LinkedList;

/**
 * A graph rewrite rule or subrule, with none, one, or arbitrary many (not yet) replacements.
 */
public class Rule extends MatchingAction {
	/** Names of the children of this node. */
	private static final String[] childrenNames = {
		"left", "right", "eval"
	};

	/** The right hand side of the rule. */
	private final PatternGraph right;

	/** The evaluation assignments of this rule. */
	private final Collection<EvalStatement> evals = new LinkedList<EvalStatement>();

	/** How often the pattern is to be matched in case this is an iterated. */
	private int minMatches;
	private int maxMatches;

	/**
	 * Make a new rule.
	 * @param ident The identifier with which the rule was declared.
	 * @param left The left side graph of the rule.
	 * @param right The right side graph of the rule.
	 */
	public Rule(Ident ident, PatternGraph left, PatternGraph right) {
		super("rule", ident, left);
		setChildrenNames(childrenNames);
		this.right = right;
		if(right==null) {
			left.setNameSuffix("test");
		}
		else {
			left.setName("L");
			right.setName("R");
		}
		this.minMatches = -1;
		this.maxMatches = -1;
	}

	/**
	 * Make a new iterated rule.
	 * @param ident The identifier with which the rule was declared.
	 * @param left The left side graph of the rule.
	 * @param right The right side graph of the rule.
	 */
	public Rule(Ident ident, PatternGraph left, PatternGraph right,
			int minMatches, int maxMatches) {
		super("rule", ident, left);
		setChildrenNames(childrenNames);
		this.right = right;
		if(right==null) {
			left.setNameSuffix("test");
		}
		else {
			left.setName("L");
			right.setName("R");
		}
		this.minMatches = minMatches;
		this.maxMatches = maxMatches;
	}

	/** @return A collection containing all eval assignments of this rule. */
	public Collection<EvalStatement> getEvals() {
		return Collections.unmodifiableCollection(evals);
	}

	/** Add an assignment to the list of evaluations. */
	public void addEval(EvalStatement a) {
		evals.add(a);
	}

	/**
	 *  @return A set with nodes, that occur on the left _and_ on the right side of the rule.
	 *  		The set also contains retyped nodes.
	 */
	public Collection<Node> getCommonNodes() {
		Collection<Node> common = new HashSet<Node>(pattern.getNodes());
		common.retainAll(right.getNodes());
		return common;
	}

	/**
	 * @return A set with edges, that occur on the left _and_ on the right side of the rule.
	 *         The set also contains all retyped edges.
	 */
	public Collection<Edge> getCommonEdges() {
		Collection<Edge> common = new HashSet<Edge>(pattern.getEdges());
		common.retainAll(right.getEdges());
		return common;
	}

	/** @return A set with subpatterns, that occur on the left _and_ on the right side of the rule. */
	public Collection<SubpatternUsage> getCommonSubpatternUsages() {
		Collection<SubpatternUsage> common = new HashSet<SubpatternUsage>(pattern.getSubpatternUsages());
		common.retainAll(right.getSubpatternUsages());
		return common;
	}

	/** @return The left hand side graph. */
	public PatternGraph getLeft() {
		return pattern;
	}

	/** @return The right hand side graph. */
	public PatternGraph getRight() {
		return right;
	}

	/** @return Minimum number of how often the pattern must get matched. */
	public int getMinMatches() {
		return minMatches;
	}

	/** @return Maximum number of how often the pattern must get matched. 0 means unlimited */
	public int getMaxMatches() {
		return maxMatches;
	}
}
