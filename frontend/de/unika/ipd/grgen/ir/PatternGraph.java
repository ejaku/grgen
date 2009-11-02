/*
 * GrGen: graph rewrite generator tool -- release GrGen.NET 2.5
 * Copyright (C) 2009 Universitaet Karlsruhe, Institut fuer Programmstrukturen und Datenorganisation, LS Goos
 * licensed under LGPL v3 (see LICENSE.txt included in the packaging of this file)
 */

/**
 * PatternGraph.java
 *
 * @author Sebastian Hack
 */

package de.unika.ipd.grgen.ir;

import de.unika.ipd.grgen.ast.PatternGraphNode; // for the MOD_... - constants
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
	private final Collection<Variable> vars = new HashSet<Variable>();

	/** The alternative statements of the pattern graph */
	private final Collection<Alternative> alts = new LinkedList<Alternative>();

	/** The iterated statements of the pattern graph */
	private final Collection<Rule> iters = new LinkedList<Rule>();

	/** The negative patterns(NAC) of the rule. */
	private final Collection<PatternGraph> negs = new LinkedList<PatternGraph>();

	/** The independent patterns(PAC) of the rule. */
	private final Collection<PatternGraph> idpts = new LinkedList<PatternGraph>();

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

	/** modifiers of pattern as defined in PatternGraphNode, only pattern locked, pattern path locked relevant */
	int modifiers;

	/**
	 * A list of the replacement parameters 
	 */
	private final List<Entity> replParams = new LinkedList<Entity>();


	/** Make a new pattern graph. */
	public PatternGraph(String nameOfGraph, int modifiers) {
		super(nameOfGraph);
		this.modifiers = modifiers;
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
	
	public boolean hasVar(Variable var) {
		return vars.contains(var);
	}

	public void addAlternative(Alternative alternative) {
		alts.add(alternative);
	}

	public Collection<Alternative> getAlts() {
		return Collections.unmodifiableCollection(alts);
	}

	public void addIterated(Rule iter) {
		iters.add(iter);
	}

	public Collection<Rule> getIters() {
		return Collections.unmodifiableCollection(iters);
	}

	public void addNegGraph(PatternGraph neg) {
		int patternNameNumber = negs.size();
		neg.setName("N" + patternNameNumber);
		negs.add(neg);
	}

	/** @return The negative graphs of the rule. */
	public Collection<PatternGraph> getNegs() {
		return Collections.unmodifiableCollection(negs);
	}

	public void addIdptGraph(PatternGraph idpt) {
		int patternNameNumber = idpts.size();
		idpt.setName("I" + patternNameNumber);
		idpts.add(idpt);
	}

	/** @return The independent graphs of the rule. */
	public Collection<PatternGraph> getIdpts() {
		return Collections.unmodifiableCollection(idpts);
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

	/** Add a replacement parameter to the rule. */
	public void addReplParameter(Entity entity) {
		replParams.add(entity);
	}

	/** Get all replacement parameters of this rule (may currently contain only nodes). */
	public List<Entity> getReplParameters() {
		return Collections.unmodifiableList(replParams);
	}
	
	public boolean replParametersContain(Entity entity) {
		return replParams.contains(entity);
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

	public boolean isPatternpathLocked() {
		return (modifiers&PatternGraphNode.MOD_PATTERNPATH_LOCKED)==PatternGraphNode.MOD_PATTERNPATH_LOCKED;
	}

	public void resolvePatternLockedModifier() {
		// in pre-order walk: add all elements of parent to child if child requests so by pattern locked modifier

		// if nested negative requests so, add all of our elements to it
		for (PatternGraph negative : getNegs()) {
			if((negative.modifiers&PatternGraphNode.MOD_PATTERN_LOCKED)!=PatternGraphNode.MOD_PATTERN_LOCKED)
				continue;

			for(Node node : getNodes()) {
				if(!negative.hasNode(node)) {
					negative.addSingleNode(node);
				}
			}
			for(Edge edge : getEdges()) {
				if(!negative.hasEdge(edge)) {
					negative.addSingleEdge(edge); // TODO: maybe we loose context here
				}
			}
		}

		// if nested independent requests so, add all of our elements to it
		for (PatternGraph independent : getIdpts()) {
			if((independent.modifiers&PatternGraphNode.MOD_PATTERN_LOCKED)!=PatternGraphNode.MOD_PATTERN_LOCKED)
				continue;

			for(Node node : getNodes()) {
				if(!independent.hasNode(node)) {
					independent.addSingleNode(node);
				}
			}
			for(Edge edge : getEdges()) {
				if(!independent.hasEdge(edge)) {
					independent.addSingleEdge(edge); // TODO: maybe we loose context here
				}
			}
		}

		// recursive descend
		for (PatternGraph negative : getNegs()) {
			negative.resolvePatternLockedModifier();
		}
		for (PatternGraph independent : getIdpts()) {
			independent.resolvePatternLockedModifier();
		}
	}

	public void ensureDirectlyNestingPatternContainsAllNonLocalElementsOfNestedPattern(
			HashSet<Node> alreadyDefinedNodes, HashSet<Edge> alreadyDefinedEdges, HashSet<Variable> alreadyDefinedVariables) {
		///////////////////////////////////////////////////////////////////////////////
		// pre: add locally referenced/defined elements to already referenced/defined elements

		for(Node node : getNodes()) {
			alreadyDefinedNodes.add(node);
		}
		for(Edge edge : getEdges()) {
			alreadyDefinedEdges.add(edge);
		}
		for(Variable var : getVars()) {
			alreadyDefinedVariables.add(var);
		}

		///////////////////////////////////////////////////////////////////////////////
		// depth first walk over IR-pattern-graph tree structure
		for(Alternative alternative : getAlts()) {
			for(Rule altCase : alternative.getAlternativeCases()) {
				altCase.getLeft().insertElementsFromRhsDeclaredInNestingLhsToLocalLhs(altCase.getRight());

				PatternGraph altCasePattern = altCase.getLeft();
				HashSet<Node> alreadyDefinedNodesClone = new HashSet<Node>(alreadyDefinedNodes);
				HashSet<Edge> alreadyDefinedEdgesClone = new HashSet<Edge>(alreadyDefinedEdges);
				HashSet<Variable> alreadyDefinedVariablesClone = new HashSet<Variable>(alreadyDefinedVariables);
				altCasePattern.ensureDirectlyNestingPatternContainsAllNonLocalElementsOfNestedPattern(
						alreadyDefinedNodesClone, alreadyDefinedEdgesClone, alreadyDefinedVariablesClone);
			}
		}

		for(Rule iterated : getIters()) {
			iterated.getLeft().insertElementsFromRhsDeclaredInNestingLhsToLocalLhs(iterated.getRight());

			PatternGraph iteratedPattern = iterated.getLeft();
			HashSet<Node> alreadyDefinedNodesClone = new HashSet<Node>(alreadyDefinedNodes);
			HashSet<Edge> alreadyDefinedEdgesClone = new HashSet<Edge>(alreadyDefinedEdges);
			HashSet<Variable> alreadyDefinedVariablesClone = new HashSet<Variable>(alreadyDefinedVariables);
			iteratedPattern.ensureDirectlyNestingPatternContainsAllNonLocalElementsOfNestedPattern(
					alreadyDefinedNodesClone, alreadyDefinedEdgesClone, alreadyDefinedVariablesClone);
		}

		for (PatternGraph negative : getNegs()) {
			HashSet<Node> alreadyDefinedNodesClone = new HashSet<Node>(alreadyDefinedNodes);
			HashSet<Edge> alreadyDefinedEdgesClone = new HashSet<Edge>(alreadyDefinedEdges);
			HashSet<Variable> alreadyDefinedVariablesClone = new HashSet<Variable>(alreadyDefinedVariables);
			negative.ensureDirectlyNestingPatternContainsAllNonLocalElementsOfNestedPattern(
					alreadyDefinedNodesClone, alreadyDefinedEdgesClone, alreadyDefinedVariablesClone);
		}

		for (PatternGraph independent : getIdpts()) {
			HashSet<Node> alreadyDefinedNodesClone = new HashSet<Node>(alreadyDefinedNodes);
			HashSet<Edge> alreadyDefinedEdgesClone = new HashSet<Edge>(alreadyDefinedEdges);
			HashSet<Variable> alreadyDefinedVariablesClone = new HashSet<Variable>(alreadyDefinedVariables);
			independent.ensureDirectlyNestingPatternContainsAllNonLocalElementsOfNestedPattern(
					alreadyDefinedNodesClone, alreadyDefinedEdgesClone, alreadyDefinedVariablesClone);
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
				for(Variable var : altCasePattern.getVars()) {
					if(!hasVar(var) && alreadyDefinedVariables.contains(var)) {
						addVariable(var);
					}
				}
			}
		}

		// add elements needed in iterated, which are not defined there and are neither defined nor used here
		// they must get handed down as preset from the defining nesting pattern to here
		for(Rule iterated : getIters()) {
			PatternGraph iteratedPattern = iterated.getLeft();
			for(Node node : iteratedPattern.getNodes()) {
				if(!hasNode(node) && alreadyDefinedNodes.contains(node)) {
					addSingleNode(node);
					addHomToAll(node);
					PatternGraph allReplacement = iterated.getRight();
					if(allReplacement!=null && !allReplacement.hasNode(node)) {
						// prevent deletion of elements inserted for pattern completion
						allReplacement.addSingleNode(node);
					}
				}
			}
			for(Edge edge : iteratedPattern.getEdges()) {
				if(!hasEdge(edge) && alreadyDefinedEdges.contains(edge)) {
					addSingleEdge(edge); // TODO: maybe we loose context here
					addHomToAll(edge);
					PatternGraph allReplacement = iterated.getRight();
					if(iterated!=null && !allReplacement.hasEdge(edge)) {
						// prevent deletion of elements inserted for pattern completion
						allReplacement.addSingleEdge(edge);
					}
				}
			}
			for(Variable var : iteratedPattern.getVars()) {
				if(!hasVar(var) && alreadyDefinedVariables.contains(var)) {
					addVariable(var);
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
			for(Variable var : negative.getVars()) {
				if(!hasVar(var) && alreadyDefinedVariables.contains(var)) {
					addVariable(var);
				}
			}
		}

		// add elements needed in nested idpt, which are not defined there and are neither defined nor used here
		// they must get handed down as preset from the defining nesting pattern to here
		for (PatternGraph independent : getIdpts()) {
			for(Node node : independent.getNodes()) {
				if(!hasNode(node) && alreadyDefinedNodes.contains(node)) {
					addSingleNode(node);
					addHomToAll(node);
				}
			}
			for(Edge edge : independent.getEdges()) {
				if(!hasEdge(edge) && alreadyDefinedEdges.contains(edge)) {
					addSingleEdge(edge); // TODO: maybe we loose context here
					addHomToAll(edge);
				}
			}
			for(Variable var : independent.getVars()) {
				if(!hasVar(var) && alreadyDefinedVariables.contains(var)) {
					addVariable(var);
				}
			}
		}
	}

	// constructs implicit lhs elements
	public void insertElementsFromRhsDeclaredInNestingLhsToLocalLhs(PatternGraph right) {
		if(right==null) {
			return;
		}

		// insert all elements, which are used (not declared) on the right hand side and not declared on left hand side,
		// (which means they are declared in some pattern the left hand side is nested in,)
		// to the left hand side (so that they get handed down from the nesting pattern;
		// otherwise they would be created (code generation by locally comparing lhs and rhs))

		for(Node n : right.getNodes()) {
			if(n.directlyNestingLHSGraph!=this && !right.replParametersContain(n)) {
				if(!hasNode(n)) {
					addSingleNode(n);
					addHomToAll(n);
				}
			}
		}

		for(Edge e : right.getEdges()) {
			if(e.directlyNestingLHSGraph!=this && !right.replParametersContain(e)) {
				if(!hasEdge(e)) {
					addSingleEdge(e);	// TODO: maybe we loose context here
					addHomToAll(e);
				}
			}
		}
		
		for(Variable v : right.getVars()) {
			if(v.directlyNestingLHSGraph!=this && !right.replParametersContain(v)) {
				addVariable(v);
			}
		}
	}
}

