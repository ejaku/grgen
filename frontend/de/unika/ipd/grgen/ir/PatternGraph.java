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
 * PatternGraph.java
 *
 * @author Sebastian Hack
 */

package de.unika.ipd.grgen.ir;

import java.util.ArrayList;
import java.util.Collection;
import java.util.Collections;
import java.util.HashSet;
import java.util.LinkedHashSet;
import java.util.LinkedList;
import java.util.List;

/**
 * A pattern graph is a graph as it occurs in left hand rule sides and negative parts.
 * Additionally it can have conditions referring to its items that restrict the set of possible matchings.
 */
public class PatternGraph extends Graph {
	/** The alternative statements of the pattern graph */
	private final Collection<Alternative> alts = new LinkedList<Alternative>();

	/** The NAC part of the rule. */
	private final Collection<PatternGraph> negs = new LinkedList<PatternGraph>();

	/** A list of all condition expressions. */
	private final List<Expression> conds = new LinkedList<Expression>();

	/** A list of all potentially homomorphic node sets. */
	private final List<Collection<Node>> homNodes = new LinkedList<Collection<Node>>();

	/** A list of all potentially homomorphic edge sets. */
	private final List<Collection<Edge>> homEdges = new LinkedList<Collection<Edge>>();

	/** A set of all pattern nodes, which may be homomorphically matched to any other pattern nodes. */
	private final HashSet<Node> homToAllNodes = new HashSet<Node>();

    /** A set of all pattern edges, which may be homomorphically matched to any other pattern edges. */
	private final HashSet<Edge> homToAllEdges = new HashSet<Edge>();

	private List<ImperativeStmt> imperativeStmts = new ArrayList<ImperativeStmt>();

	/** Make a new pattern graph. */
	public PatternGraph(String nameOfGraph) {
		super(nameOfGraph);
	}

	public void addImperativeStmt(ImperativeStmt emit) {
		imperativeStmts.add(emit);
	}

	public List<ImperativeStmt> getImperativeStmts() {
		return imperativeStmts;
	}

	public void addAlternative(Alternative alternative) {
		alts.add(alternative);
	}

	public Collection<Alternative> getAlts() {
		return Collections.unmodifiableCollection(alts);
	}

	public void addNegGraph(PatternGraph neg) {
		int patternNameNumber = negs.size();
		neg.setName("N" + patternNameNumber);
		negs.add(neg);
	}

	/** @return The NAC graphs of the rule. */
	public Collection<PatternGraph> getNegs() {
		return Collections.unmodifiableCollection(negs);
	}

	/** Add a condition given by it's expression expr to the graph. */
	public void addCondition(Expression expr) {
		conds.add(expr);
	}

	/** Add a potentially homomorphic set to the graph. */
	public void addHomomorphicNodes(Collection<Node> hom) {
		homNodes.add(hom);
	}

	/** Add a potentially homomorphic set to the graph. */
	public void addHomomorphicEdges(Collection<Edge> hom) {
		homEdges.add(hom);
	}

	public void addHomToAll(Node node) {
		homToAllNodes.add(node);
	}

	public void addHomToAll(Edge edge) {
		homToAllEdges.add(edge);
	}

	/** Get a collection with all conditions in this graph. */
	public Collection<Expression> getConditions() {
		return Collections.unmodifiableCollection(conds);
	}

	/** Get all potentially homomorphic sets in this graph. */
	public Collection<Collection<? extends GraphEntity>> getHomomorphic() {
		Collection<Collection<? extends GraphEntity>> ret = new LinkedHashSet<Collection<? extends GraphEntity>>();
		ret.addAll(homEdges);
		ret.addAll(homNodes);

		return Collections.unmodifiableCollection(ret);
	}

	public Collection<Node> getHomomorphic(Node n) {
		for(Collection<Node> c : homNodes) {
			if (c.contains(n)) {
				return c;
			}
		}

		Collection<Node> c = new LinkedList<Node>();
		c.add(n);
		return c;
	}

	public Collection<Edge> getHomomorphic(Edge e) {
		for(Collection<Edge> c : homEdges) {
			if (c.contains(e)) {
				return c;
			}
		}

		Collection<Edge> c = new LinkedList<Edge>();
		c.add(e);
		return c;
	}

	public Collection<Node> getElemsHomToAllNodes() {
		return homToAllNodes;
	}

	public Collection<Edge> getElemsHomToAllEdges() {
		return homToAllEdges;
	}

	public boolean isHomomorphic(Node n1, Node n2) {
		return getHomomorphic(n1).contains(n2);
	}

	public boolean isHomomorphic(Edge e1, Edge e2) {
		return getHomomorphic(e1).contains(e2);
	}

	public boolean isHomToAll(Node node) {
		return homToAllNodes.contains(node);
	}

	public boolean isHomToAll(Edge edge) {
		return homToAllEdges.contains(edge);
	}

	public boolean isIsoToAll(Node node) {
		for(Collection<? extends GraphEntity> c : homNodes) {
			if (c.contains(node)) {
				return false;
			}
		}
		return true;
	}

	public boolean isIsoToAll(Edge edge) {
		for(Collection<? extends GraphEntity> c : homEdges) {
			if (c.contains(edge)) {
				return false;
			}
		}
		return true;
	}
}

