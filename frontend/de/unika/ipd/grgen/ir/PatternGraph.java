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
import java.util.HashMap;
import java.util.HashSet;
import java.util.LinkedHashSet;
import java.util.LinkedList;
import java.util.List;

/**
 * A pattern graph is a graph as it occurs in left hand rule sides and negative parts.
 * Additionally it can have conditions referring to its items that restrict the set of possible matchings.
 */
public class PatternGraph extends Graph {
	private final Collection<Variable> vars = new LinkedList<Variable>();

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

	/** A set of nodes which will be matched homomorphically to any other node in the pattern.
	 *  they appear if they're not referenced within the pattern, but some nested component uses them */
	private final HashSet<Node> homToAllNodes = new HashSet<Node>();

    /** A set of edges which will be matched homomorphically to any other edge in the pattern.
     *  they appear if they're not referenced within the pattern, but some nested component uses them  */
	private final HashSet<Edge> homToAllEdges = new HashSet<Edge>();

	private List<ImperativeStmt> imperativeStmts = new ArrayList<ImperativeStmt>();

	/** if graph is a subpattern or a negative this member tells us whether
	 *  it should be matched independent from already matched enclosing subpatterns*/
	boolean isIndependent;


	/** Make a new pattern graph. */
	public PatternGraph(String nameOfGraph, boolean isIndependent) {
		super(nameOfGraph);
		this.isIndependent = isIndependent;
	}

	public void addImperativeStmt(ImperativeStmt emit) {
		imperativeStmts.add(emit);
	}

	public List<ImperativeStmt> getImperativeStmts() {
		return imperativeStmts;
	}

	public void addVariable(Variable var) {
		vars.add(var);
	}

	public Collection<Variable> getVars() {
		return Collections.unmodifiableCollection(vars);
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

	public boolean isHomomorphic(Node n1, Node n2) {
		return homToAllNodes.contains(n1) || homToAllNodes.contains(n2)
				|| getHomomorphic(n1).contains(n2);
	}

	public boolean isHomomorphic(Edge e1, Edge e2) {
		return homToAllEdges.contains(e1) || homToAllEdges.contains(e2)
				|| getHomomorphic(e1).contains(e2);
	}

	public boolean isHomomorphicGlobal(HashMap<Entity, String> alreadyDefinedEntityToName, Node n1, Node n2) {
		if(!getHomomorphic(n1).contains(n2)) {
			return false;
		}
		return alreadyDefinedEntityToName.containsKey(n1) != alreadyDefinedEntityToName.containsKey(n2);
	}

	public boolean isHomomorphicGlobal(HashMap<Entity, String> alreadyDefinedEntityToName, Edge e1, Edge e2) {
		if(!getHomomorphic(e1).contains(e2)) {
			return false;
		}
		return alreadyDefinedEntityToName.containsKey(e1) != alreadyDefinedEntityToName.containsKey(e2);
	}

	public boolean isIndependent() {
		return isIndependent;
	}

	public void ensureDirectlyNestingPatternContainsAllNonLocalElementsOfNestedPattern(
			HashSet<Node> alreadyDefinedNodes, HashSet<Edge> alreadyDefinedEdges) {
		///////////////////////////////////////////////////////////////////////////////
		// pre: add locally referenced/defined elements to already referenced/defined elements

		for(Node node : getNodes()) {
			alreadyDefinedNodes.add(node);
		}
		for(Edge edge : getEdges()) {
			alreadyDefinedEdges.add(edge);
		}

		///////////////////////////////////////////////////////////////////////////////
		// depth first walk over IR-pattern-graph tree structure
		for(Alternative alternative : getAlts()) {
			for(Rule altCase : alternative.getAlternativeCases()) {
				PatternGraph altCasePattern = altCase.getLeft();
				HashSet<Node> alreadyDefinedNodesClone = new HashSet<Node>(alreadyDefinedNodes);
				HashSet<Edge> alreadyDefinedEdgesClone = new HashSet<Edge>(alreadyDefinedEdges);
				altCasePattern.ensureDirectlyNestingPatternContainsAllNonLocalElementsOfNestedPattern(
						alreadyDefinedNodesClone, alreadyDefinedEdgesClone);
			}
		}

		for (PatternGraph negative : getNegs()) {
			HashSet<Node> alreadyDefinedNodesClone = new HashSet<Node>(alreadyDefinedNodes);
			HashSet<Edge> alreadyDefinedEdgesClone = new HashSet<Edge>(alreadyDefinedEdges);
			negative.ensureDirectlyNestingPatternContainsAllNonLocalElementsOfNestedPattern(
					alreadyDefinedNodesClone, alreadyDefinedEdgesClone);
		}

		///////////////////////////////////////////////////////////////////////////////
		// post: add elements of subpatterns not defined there to our nodes'n'edges

		// add elements needed in alternative cases, which are not defined there and are neither defined nor used here
		// they must get handed down as preset from the defining nesting pattern to here
		for(Alternative alternative : getAlts()) {
			for(Rule altCase : alternative.getAlternativeCases()) {
				PatternGraph altCasePattern = altCase.getLeft();
				for(Node node : altCasePattern.getNodes()) {
					if(!hasNode(node) && alreadyDefinedNodes.contains(node)) {
						addSingleNode(node);
						addHomToAll(node);
						PatternGraph altCaseReplacement = altCase.getRight();
						if(altCaseReplacement!=null && !altCaseReplacement.hasNode(node)) {
							// prevent deletion of elements inserted for pattern completion
							altCaseReplacement.addSingleNode(node);
						}
					}
				}
				for(Edge edge : altCasePattern.getEdges()) {
					if(!hasEdge(edge) && alreadyDefinedEdges.contains(edge)) {
						addSingleEdge(edge); // TODO: maybe we loose context here
						addHomToAll(edge);
						PatternGraph altCaseReplacement = altCase.getRight();
						if(altCaseReplacement!=null && !altCaseReplacement.hasEdge(edge)) {
							// prevent deletion of elements inserted for pattern completion
							altCaseReplacement.addSingleEdge(edge);
						}
					}
				}
			}
		}

		// add elements needed in nested neg, which are not defined there and are neither defined nor used here
		// they must get handed down as preset from the defining nesting pattern to here
		for (PatternGraph negative : getNegs()) {
			for(Node node : negative.getNodes()) {
				if(!hasNode(node) && alreadyDefinedNodes.contains(node)) {
					addSingleNode(node);
					addHomToAll(node);
				}
			}
			for(Edge edge : negative.getEdges()) {
				if(!hasEdge(edge) && alreadyDefinedEdges.contains(edge)) {
					addSingleEdge(edge); // TODO: maybe we loose context here
					addHomToAll(edge);
				}
			}
		}
	}
}

