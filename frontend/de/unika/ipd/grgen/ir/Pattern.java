/*
 GrGen: graph rewrite generator tool.
 Copyright (C) 2005  IPD Goos, Universit"at Karlsruhe, Germany

 This library is free software; you can redistribute it and/or
 modify it under the terms of the GNU Lesser General Public
 License as published by the Free Software Foundation; either
 version 2.1 of the License, or (at your option) any later version.

 This library is distributed in the hope that it will be useful,
 but WITHOUT ANY WARRANTY; without even the implied warranty of
 MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the GNU
 Lesser General Public License for more details.

 You should have received a copy of the GNU Lesser General Public
 License along with this library; if not, write to the Free Software
 Foundation, Inc., 51 Franklin St, Fifth Floor, Boston, MA  02110-1301  USA
 */


/**
 * @author shack, Daniel Grund
 * @version $Id: Rule.java 17880 2008-02-26 18:29:31Z moritz $
 */
package de.unika.ipd.grgen.ir;

import java.util.Collection;
import java.util.Collections;
import java.util.HashSet;
import java.util.LinkedHashMap;
import java.util.LinkedHashSet;
import java.util.List;
import java.util.Map;

/**
 * A replacement subpattern.
 */
public class Pattern extends MatchingAction {
	/** Names of the children of this node. */
	private static final String[] childrenNames = {
		"left", "right", "eval"
	};

	/** The right hand side of the pattern. */
	private final List<PatternGraph> right;

	/** The evaluation assignments of this pattern. */
	private final Map<Integer, Collection<Assignment>> evals = new LinkedHashMap<Integer, Collection<Assignment>>();

	/**
	 * Make a new pattern.
	 * @param ident The identifier with which the pattern was declared.
	 * @param left The left side graph of the pattern.
	 * @param right The right side graph of the pattern.
	 */
	public Pattern(Ident ident, PatternGraph left, List<PatternGraph> right) {
		super("pattern", ident, left);
		setChildrenNames(childrenNames);
		this.right = right;
		left.setName("L");
		for (int i = 0; i < right.size(); i++) {
			right.get(i).setName("R");
		}
	}

	/** @return A collection containing all eval assignments of this pattern. */
	public Collection<Assignment> getEvals(int index) {
		return Collections.unmodifiableCollection(evals.get(index));
	}

	/** Add an assignment to the list of evaluations. */
	public void addEval(int index, Assignment a) {
		if(evals.containsKey(index)) {
			Collection<Assignment> ret= evals.get(index);
			ret.add(a);
		} else {
			Collection<Assignment> ret= new LinkedHashSet<Assignment>();
			ret.add(a);
			evals.put(index, ret);
		}
	}

	/** @return A set with nodes, that occur on the left _and_ on the right side of the pattern. */
	public Collection<Node> getCommonNodes(int index) {
		Collection<Node> common = new HashSet<Node>(pattern.getNodes());
		common.retainAll(right.get(index).getNodes());
		return common;
	}

	/** @return A set with edges, that occur on the left _and_ on the right side of the pattern. */
	public Collection<Edge> getCommonEdges(int index) {
		Collection<Edge> common = new HashSet<Edge>(pattern.getEdges());
		common.retainAll(right.get(index).getEdges());
		return common;
	}

	/** @return The left hand side graph. */
	public PatternGraph getLeft() {
		return pattern;
	}

	/** @return The right hand side graph. */
	public Collection<PatternGraph> getRight() {
		return right;
	}
}
