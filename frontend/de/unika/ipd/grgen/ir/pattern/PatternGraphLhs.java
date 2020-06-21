/*
 * GrGen: graph rewrite generator tool -- release GrGen.NET 5.0
 * Copyright (C) 2003-2020 Universitaet Karlsruhe, Institut fuer Programmstrukturen und Datenorganisation, LS Goos; and free programmers
 * licensed under LGPL v3 (see LICENSE.txt included in the packaging of this file)
 * www.grgen.net
 */

/**
 * PatternGraph.java
 *
 * @author Sebastian Hack
 */

package de.unika.ipd.grgen.ir.pattern;

import java.util.Collection;
import java.util.Collections;
import java.util.HashMap;
import java.util.HashSet;
import java.util.LinkedHashSet;
import java.util.LinkedList;
import java.util.List;

import de.unika.ipd.grgen.ir.Entity;
import de.unika.ipd.grgen.ir.NeededEntities;
import de.unika.ipd.grgen.ir.executable.Rule;
import de.unika.ipd.grgen.ir.expr.Constant;
import de.unika.ipd.grgen.ir.expr.Expression;
import de.unika.ipd.grgen.ir.stmt.EvalStatement;
import de.unika.ipd.grgen.ir.stmt.EvalStatements;
import de.unika.ipd.grgen.ast.BaseNode; // for the context constants
import de.unika.ipd.grgen.ast.pattern.PatternGraphLhsNode;

/**
 * A pattern graph lhs is a graph pattern as it occurs on the left hand side of rules.
 * It includes nested alternative-case and iterated rules, as well as nested patterns (negative and independent).
 * It extends the pattern graph base class, additionally offering conditions that restrict the set of possible matches, 
 * lhs yield statements, homomorphy handling and further things.
 */
public class PatternGraphLhs extends PatternGraphBase
{
	/** The alternative statements of the pattern graph */
	private final Collection<Alternative> alts = new LinkedList<Alternative>();

	/** The iterated statements of the pattern graph */
	private final Collection<Rule> iters = new LinkedList<Rule>();

	/** The negative patterns(NAC) of the rule. */
	private final Collection<PatternGraphLhs> negs = new LinkedList<PatternGraphLhs>();

	/** The independent patterns(PAC) of the rule. */
	private final Collection<PatternGraphLhs> idpts = new LinkedList<PatternGraphLhs>();

	/** A list of all condition expressions. */
	private final List<Expression> conds = new LinkedList<Expression>();

	/** A list of all yield assignments. */
	private final List<EvalStatements> yields = new LinkedList<EvalStatements>();

	/** A list of all potentially homomorphic node sets. */
	private final List<Collection<Node>> homNodesLists = new LinkedList<Collection<Node>>();

	/** A list of all potentially homomorphic edge sets. */
	private final List<Collection<Edge>> homEdgesLists = new LinkedList<Collection<Edge>>();

	/** A map of nodes which will be matched homomorphically to any other node
	 *  to the isomorphy exceptions, requested by independent(node); */
	private final HashMap<Node, HashSet<Node>> totallyHomNodes = new HashMap<Node, HashSet<Node>>();

	/** A map of edges which will be matched homomorphically to any other edge
	 *  to the isomorphy exceptions, requested by independent(edge); */
	private final HashMap<Edge, HashSet<Edge>> totallyHomEdges = new HashMap<Edge, HashSet<Edge>>();

	/** modifiers of pattern as defined in PatternGraphNode, only pattern locked, pattern path locked relevant */
	int modifiers;

	final int PATTERN_NOT_YET_VISITED = 0;
	final int PATTERN_MAYBE_EMPTY = 1;
	final int PATTERN_NOT_EMPTY = 2;
	int mayPatternBeEmptyComputationState = PATTERN_NOT_YET_VISITED;

	// if this pattern graph is a negative or independent nested inside an iterated
	// it might break the iterated instead of only the current iterated case, if specified
	private boolean iterationBreaking = false;


	/** Make a new pattern graph. */
	public PatternGraphLhs(String nameOfGraph, int modifiers)
	{
		super(nameOfGraph);
		this.modifiers = modifiers;
	}

	public void addAlternative(Alternative alternative)
	{
		alts.add(alternative);
	}

	public Collection<Alternative> getAlts()
	{
		return Collections.unmodifiableCollection(alts);
	}

	public void addIterated(Rule iter)
	{
		iters.add(iter);
	}

	public Collection<Rule> getIters()
	{
		return Collections.unmodifiableCollection(iters);
	}

	public void addNegGraph(PatternGraphLhs neg)
	{
		int patternNameNumber = negs.size();
		neg.setName("N" + patternNameNumber);
		negs.add(neg);
	}

	/** @return The negative graphs of the rule. */
	public Collection<PatternGraphLhs> getNegs()
	{
		return Collections.unmodifiableCollection(negs);
	}

	public void addIdptGraph(PatternGraphLhs idpt)
	{
		int patternNameNumber = idpts.size();
		idpt.setName("I" + patternNameNumber);
		idpts.add(idpt);
	}

	/** @return The independent graphs of the rule. */
	public Collection<PatternGraphLhs> getIdpts()
	{
		return Collections.unmodifiableCollection(idpts);
	}

	/** Add a condition given by it's expression expr to the graph. */
	public void addCondition(Expression expr)
	{
		conds.add(expr);
	}

	/** Add an assignment to the list of evaluations. */
	public void addYield(EvalStatements stmts)
	{
		yields.add(stmts);
	}

	/** Add a potentially homomorphic set to the graph. */
	public void addHomomorphicNodes(Collection<Node> hom)
	{
		homNodesLists.add(hom);
	}

	/** Add a potentially homomorphic set to the graph. */
	public void addHomomorphicEdges(Collection<Edge> hom)
	{
		homEdgesLists.add(hom);
	}

	public void addTotallyHomomorphic(Node node, HashSet<Node> isoNodes)
	{
		totallyHomNodes.put(node, isoNodes);
	}

	public void addTotallyHomomorphic(Edge edge, HashSet<Edge> isoEdges)
	{
		totallyHomEdges.put(edge, isoEdges);
	}

	public void setIterationBreaking(boolean value)
	{
		iterationBreaking = value;
	}

	public boolean isIterationBreaking()
	{
		return iterationBreaking;
	}

	/** Get a collection with all conditions in this graph. */
	public Collection<Expression> getConditions()
	{
		return Collections.unmodifiableCollection(conds);
	}

	/** @return A collection containing all yield assignments of this graph. */
	public Collection<EvalStatements> getYields()
	{
		return Collections.unmodifiableCollection(yields);
	}

	/** Get all potentially homomorphic sets in this graph. */
	public Collection<Collection<? extends GraphEntity>> getHomomorphic()
	{
		Collection<Collection<? extends GraphEntity>> ret = new LinkedHashSet<Collection<? extends GraphEntity>>();
		ret.addAll(homEdgesLists);
		ret.addAll(homNodesLists);

		return Collections.unmodifiableCollection(ret);
	}

	public Collection<Node> getHomomorphic(Node node)
	{
		Collection<Node> homNodesOfNode = new LinkedList<Node>();

		for(Collection<Node> homNodes : homNodesLists) {
			if(homNodes.contains(node)) {
				homNodesOfNode.addAll(homNodes);
			}
		}
		homNodesOfNode.add(node);
		
		return homNodesOfNode;
	}

	public Collection<Edge> getHomomorphic(Edge edge)
	{
		Collection<Edge> homEdgesOfEdge = new LinkedList<Edge>();

		for(Collection<Edge> homEdges : homEdgesLists) {
			if(homEdges.contains(edge)) {
				homEdgesOfEdge.addAll(homEdges);
			}
		}
		homEdgesOfEdge.add(edge);
		
		return homEdgesOfEdge;
	}

	public boolean isHomomorphic(Node node1, Node node2)
	{
		if(isTotallyHomomorphic(node1, node2))
			return true;
		return homToAllNodes.contains(node1) || homToAllNodes.contains(node2)
				|| getHomomorphic(node1).contains(node2);
	}

	public boolean isHomomorphic(Edge edge1, Edge edge2)
	{
		if(isTotallyHomomorphic(edge1, edge2))
			return true;
		return homToAllEdges.contains(edge1) || homToAllEdges.contains(edge2)
				|| getHomomorphic(edge1).contains(edge2);
	}

	public boolean isHomomorphicGlobal(HashMap<Entity, String> alreadyDefinedEntityToName, Node node1, Node node2)
	{
		if(isTotallyHomomorphic(node1, node2))
			return true;
		if(!getHomomorphic(node1).contains(node2))
			return false;
		return alreadyDefinedEntityToName.containsKey(node1) != alreadyDefinedEntityToName.containsKey(node2);
	}

	public boolean isHomomorphicGlobal(HashMap<Entity, String> alreadyDefinedEntityToName, Edge edge1, Edge edge2)
	{
		if(isTotallyHomomorphic(edge1, edge2))
			return true;
		if(!getHomomorphic(edge1).contains(edge2))
			return false;
		return alreadyDefinedEntityToName.containsKey(edge1) != alreadyDefinedEntityToName.containsKey(edge2);
	}

	public boolean isTotallyHomomorphic(Node node1, Node node2)
	{
		if(isTotallyHomomorphic(node1)) {
			if(totallyHomNodes.get(node1).contains(node2))
				return false;
		}
		if(isTotallyHomomorphic(node2)) {
			if(totallyHomNodes.get(node2).contains(node1))
				return false;
		}
		if(isTotallyHomomorphic(node1) || isTotallyHomomorphic(node2)) {
			return true;
		}
		return false;
	}

	public boolean isTotallyHomomorphic(Edge edge1, Edge edge2)
	{
		if(isTotallyHomomorphic(edge1)) {
			if(totallyHomEdges.get(edge1).contains(edge2))
				return false;
		}
		if(isTotallyHomomorphic(edge2)) {
			if(totallyHomEdges.get(edge2).contains(edge1))
				return false;
		}
		if(isTotallyHomomorphic(edge1) || isTotallyHomomorphic(edge2)) {
			return true;
		}
		return false;
	}

	public boolean isTotallyHomomorphic(Node node)
	{
		if(totallyHomNodes.containsKey(node))
			return true;
		else
			return false;
	}

	public boolean isTotallyHomomorphic(Edge edge)
	{
		if(totallyHomEdges.containsKey(edge))
			return true;
		else
			return false;
	}

	public boolean isPatternpathLocked()
	{
		return (modifiers & PatternGraphLhsNode.MOD_PATTERNPATH_LOCKED) == PatternGraphLhsNode.MOD_PATTERNPATH_LOCKED;
	}

	public void resolvePatternLockedModifier()
	{
		// in pre-order walk: add all elements of parent to child if child requests so by pattern locked modifier

		// if nested negative requests so, add all of our elements to it
		for(PatternGraphLhs negative : getNegs()) {
			if((negative.modifiers & PatternGraphLhsNode.MOD_PATTERN_LOCKED) != PatternGraphLhsNode.MOD_PATTERN_LOCKED)
				continue;

			for(Node node : getNodes()) {
				if(!negative.hasNode(node)) {
					negative.addSingleNode(node);
				}
			}
			for(Edge edge : getEdges()) {
				if(!negative.hasEdge(edge)) {
					negative.addSingleEdge(edge);
				}
			}
		}

		// if nested independent requests so, add all of our elements to it
		for(PatternGraphLhs independent : getIdpts()) {
			if((independent.modifiers & PatternGraphLhsNode.MOD_PATTERN_LOCKED) != PatternGraphLhsNode.MOD_PATTERN_LOCKED)
				continue;

			for(Node node : getNodes()) {
				if(!independent.hasNode(node)) {
					independent.addSingleNode(node);
				}
			}
			for(Edge edge : getEdges()) {
				if(!independent.hasEdge(edge)) {
					independent.addSingleEdge(edge);
				}
			}
		}

		// recursive descend
		for(PatternGraphLhs negative : getNegs()) {
			negative.resolvePatternLockedModifier();
		}
		for(PatternGraphLhs independent : getIdpts()) {
			independent.resolvePatternLockedModifier();
		}
	}

	public void ensureDirectlyNestingPatternContainsAllNonLocalElementsOfNestedPattern(
			HashSet<Node> alreadyDefinedNodes, HashSet<Edge> alreadyDefinedEdges,
			HashSet<Variable> alreadyDefinedVariables,
			PatternGraphRhs right)
	{
		// first local corrections, then global consistency
		if(right != null)
			insertElementsFromRhsDeclaredInNestingRhsToReplParams(right);
		if(right != null)
			insertElementsFromRhsDeclaredInNestingLhsToLocalLhs(right);

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
				PatternGraphLhs altCasePattern = altCase.getLeft();
				HashSet<Node> alreadyDefinedNodesClone = new HashSet<Node>(alreadyDefinedNodes);
				HashSet<Edge> alreadyDefinedEdgesClone = new HashSet<Edge>(alreadyDefinedEdges);
				HashSet<Variable> alreadyDefinedVariablesClone = new HashSet<Variable>(alreadyDefinedVariables);
				altCasePattern.ensureDirectlyNestingPatternContainsAllNonLocalElementsOfNestedPattern(
						alreadyDefinedNodesClone, alreadyDefinedEdgesClone, alreadyDefinedVariablesClone,
						altCase.getRight());
			}
		}

		for(Rule iterated : getIters()) {
			PatternGraphLhs iteratedPattern = iterated.getLeft();
			HashSet<Node> alreadyDefinedNodesClone = new HashSet<Node>(alreadyDefinedNodes);
			HashSet<Edge> alreadyDefinedEdgesClone = new HashSet<Edge>(alreadyDefinedEdges);
			HashSet<Variable> alreadyDefinedVariablesClone = new HashSet<Variable>(alreadyDefinedVariables);
			iteratedPattern.ensureDirectlyNestingPatternContainsAllNonLocalElementsOfNestedPattern(
					alreadyDefinedNodesClone, alreadyDefinedEdgesClone, alreadyDefinedVariablesClone,
					iterated.getRight());
		}

		for(PatternGraphLhs negative : getNegs()) {
			HashSet<Node> alreadyDefinedNodesClone = new HashSet<Node>(alreadyDefinedNodes);
			HashSet<Edge> alreadyDefinedEdgesClone = new HashSet<Edge>(alreadyDefinedEdges);
			HashSet<Variable> alreadyDefinedVariablesClone = new HashSet<Variable>(alreadyDefinedVariables);
			negative.ensureDirectlyNestingPatternContainsAllNonLocalElementsOfNestedPattern(
					alreadyDefinedNodesClone, alreadyDefinedEdgesClone, alreadyDefinedVariablesClone,
					null);
		}

		for(PatternGraphLhs independent : getIdpts()) {
			HashSet<Node> alreadyDefinedNodesClone = new HashSet<Node>(alreadyDefinedNodes);
			HashSet<Edge> alreadyDefinedEdgesClone = new HashSet<Edge>(alreadyDefinedEdges);
			HashSet<Variable> alreadyDefinedVariablesClone = new HashSet<Variable>(alreadyDefinedVariables);
			independent.ensureDirectlyNestingPatternContainsAllNonLocalElementsOfNestedPattern(
					alreadyDefinedNodesClone, alreadyDefinedEdgesClone, alreadyDefinedVariablesClone,
					null);
		}

		///////////////////////////////////////////////////////////////////////////////
		// post: add elements of subpatterns not defined there to our nodes'n'edges

		// add elements needed in alternative cases, which are not defined there and are neither defined nor used here
		// they must get handed down as preset from the defining nesting pattern to here
		for(Alternative alternative : getAlts()) {
			for(Rule altCase : alternative.getAlternativeCases()) {
				PatternGraphLhs altCasePattern = altCase.getLeft();
				for(Node node : altCasePattern.getNodes()) {
					if(!hasNode(node) && alreadyDefinedNodes.contains(node)) {
						addSingleNode(node);
						addHomToAll(node);
						PatternGraphBase altCaseReplacement = altCase.getRight();
						if(altCaseReplacement != null && !altCaseReplacement.hasNode(node)) {
							// prevent deletion of elements inserted for pattern completion
							altCaseReplacement.addSingleNode(node);
						}
						if(right != null && !right.hasNode(node) && !right.getDeletedElements().contains(node)) {
							right.addSingleNode(node);
						}
					}
				}
				for(Edge edge : altCasePattern.getEdges()) {
					if(!hasEdge(edge) && alreadyDefinedEdges.contains(edge)) {
						addSingleEdge(edge);
						addHomToAll(edge);
						PatternGraphBase altCaseReplacement = altCase.getRight();
						if(altCaseReplacement != null && !altCaseReplacement.hasEdge(edge)) {
							// prevent deletion of elements inserted for pattern completion
							altCaseReplacement.addSingleEdge(edge);
						}
						if(right != null && !right.hasEdge(edge) && !right.getDeletedElements().contains(edge)) {
							right.addSingleEdge(edge);
						}
					}
				}
				for(Variable var : altCasePattern.getVars()) {
					if(!hasVar(var) && alreadyDefinedVariables.contains(var)) {
						addVariable(var);
					}
				}

				// add rhs parameters from nested alternative cases if they are not used or defined here to our rhs parameters,
				// so we get them, so we're able to forward them
				if(right != null) {
					List<Entity> altCaseReplParameters = altCase.getRight().getReplParameters();
					for(Entity entity : altCaseReplParameters) {
						if(entity instanceof Node) {
							Node node = (Node)entity;
							if(node.directlyNestingLHSGraph != this) {
								if(!right.getReplParameters().contains(node)) {
									right.addReplParameter(node);
								}
							}
						}
						if(entity instanceof Edge) {
							Edge edge = (Edge)entity;
							if(edge.directlyNestingLHSGraph != this) {
								if(!right.getReplParameters().contains(edge)) {
									right.addReplParameter(edge);
								}
							}
						}
						if(entity instanceof Variable) {
							Variable var = (Variable)entity;
							if(var.directlyNestingLHSGraph != this) {
								if(!right.getReplParameters().contains(var)) {
									right.addReplParameter(var);
								}
							}
						}
					}
				}
			}
		}

		// add elements needed in iterated, which are not defined there and are neither defined nor used here
		// they must get handed down as preset from the defining nesting pattern to here
		for(Rule iterated : getIters()) {
			PatternGraphLhs iteratedPattern = iterated.getLeft();
			for(Node node : iteratedPattern.getNodes()) {
				if(!hasNode(node) && alreadyDefinedNodes.contains(node)) {
					addSingleNode(node);
					addHomToAll(node);
					PatternGraphBase allReplacement = iterated.getRight();
					if(allReplacement != null && !allReplacement.hasNode(node)) {
						// prevent deletion of elements inserted for pattern completion
						allReplacement.addSingleNode(node);
					}
					if(right != null && !right.hasNode(node) && !right.getDeletedElements().contains(node)) {
						right.addSingleNode(node);
					}
				}
			}
			for(Edge edge : iteratedPattern.getEdges()) {
				if(!hasEdge(edge) && alreadyDefinedEdges.contains(edge)) {
					addSingleEdge(edge);
					addHomToAll(edge);
					PatternGraphBase allReplacement = iterated.getRight();
					if(allReplacement != null && !allReplacement.hasEdge(edge)) {
						// prevent deletion of elements inserted for pattern completion
						allReplacement.addSingleEdge(edge);
					}
					if(right != null && !right.hasEdge(edge) && !right.getDeletedElements().contains(edge)) {
						right.addSingleEdge(edge);
					}
				}
			}
			for(Variable var : iteratedPattern.getVars()) {
				if(!hasVar(var) && alreadyDefinedVariables.contains(var)) {
					addVariable(var);
				}
			}

			// add rhs parameters from nested iterateds if they are not used or defined here to our rhs parameters,
			// so we get them, so we're able to forward them
			if(right != null) {
				List<Entity> iteratedReplParameters = iterated.getRight().getReplParameters();
				for(Entity iteratedReplParameter : iteratedReplParameters) {
					if(iteratedReplParameter instanceof Node) {
						Node node = (Node)iteratedReplParameter;
						if(node.directlyNestingLHSGraph != this) {
							if(!right.getReplParameters().contains(node)) {
								right.addReplParameter(node);
							}
						}
					}
					if(iteratedReplParameter instanceof Edge) {
						Edge edge = (Edge)iteratedReplParameter;
						if(edge.directlyNestingLHSGraph != this) {
							if(!right.getReplParameters().contains(edge)) {
								right.addReplParameter(edge);
							}
						}
					}
					if(iteratedReplParameter instanceof Variable) {
						Variable var = (Variable)iteratedReplParameter;
						if(var.directlyNestingLHSGraph != this) {
							if(!right.getReplParameters().contains(var)) {
								right.addReplParameter(var);
							}
						}
					}
				}
			}
		}

		// add elements needed in nested neg, which are not defined there and are neither defined nor used here
		// they must get handed down as preset from the defining nesting pattern to here
		for(PatternGraphLhs negative : getNegs()) {
			for(Node node : negative.getNodes()) {
				if(!hasNode(node) && alreadyDefinedNodes.contains(node)) {
					addSingleNode(node);
					addHomToAll(node);
					if(right != null && !right.hasNode(node) && !right.getDeletedElements().contains(node)) {
						right.addSingleNode(node);
					}
				}
			}
			for(Edge edge : negative.getEdges()) {
				if(!hasEdge(edge) && alreadyDefinedEdges.contains(edge)) {
					addSingleEdge(edge);
					addHomToAll(edge);
					if(right != null && !right.hasEdge(edge) && !right.getDeletedElements().contains(edge)) {
						right.addSingleEdge(edge);
					}
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
		for(PatternGraphLhs independent : getIdpts()) {
			for(Node node : independent.getNodes()) {
				if(!hasNode(node) && alreadyDefinedNodes.contains(node)) {
					addSingleNode(node);
					addHomToAll(node);
					if(right != null && !right.hasNode(node) && !right.getDeletedElements().contains(node)) {
						right.addSingleNode(node);
					}
				}
			}
			for(Edge edge : independent.getEdges()) {
				if(!hasEdge(edge) && alreadyDefinedEdges.contains(edge)) {
					addSingleEdge(edge);
					addHomToAll(edge);
					if(right != null && !right.hasEdge(edge) && !right.getDeletedElements().contains(edge)) {
						right.addSingleEdge(edge);
					}
				}
			}
			for(Variable var : independent.getVars()) {
				if(!hasVar(var) && alreadyDefinedVariables.contains(var)) {
					addVariable(var);
				}
			}
		}
	}

	// construct implicit rhs replace parameters
	public void insertElementsFromRhsDeclaredInNestingRhsToReplParams(PatternGraphRhs right)
	{
		if(right == null) {
			return;
		}

		// insert all nodes and variables, which are used (not declared) on the right hand side and not declared on left hand side,
		// and are declared in some nesting right hand side,
		// to the replacement parameters (so that they get handed down from the nesting replacement)

		for(Node node : right.getNodes()) {
			if(node.directlyNestingLHSGraph != this && !right.replParametersContain(node)) {
				if((node.context & BaseNode.CONTEXT_LHS_OR_RHS) == BaseNode.CONTEXT_RHS) {
					right.addReplParameter(node);
				}
			}
		}

		for(Variable var : right.getVars()) {
			if(var.directlyNestingLHSGraph != this && !right.replParametersContain(var)) {
				if((var.context & BaseNode.CONTEXT_LHS_OR_RHS) == BaseNode.CONTEXT_RHS) {
					right.addReplParameter(var);
				}
			}
		}

		// emit error for edges which would have be to be handled like this,
		// because they are not available in the nested replacement;
		// as they are only created after the replacement code of the nested pattern is left, 
		// that's because node retyping occurs only afterwards, 
		// and we maybe want to create edges in between retyped=newly created nodes
		for(Edge edge : right.getEdges()) {
			if(edge.directlyNestingLHSGraph != this) {
				if((edge.context & BaseNode.CONTEXT_LHS_OR_RHS) == BaseNode.CONTEXT_RHS) {
					error.error(edge.getIdent().getCoords(),
							"Can't access a newly created edge (" + edge.getIdent() + ") in a nested replacement");
				}
			}
		}

		// some check which is easier on ir
		checkThatEvalhereIsNotAccessingCreatedEdges(right);
	}

	public static void checkThatEvalhereIsNotAccessingCreatedEdges(PatternGraphRhs right)
	{
		if(right == null) {
			return;
		}

		// emit error on accessing freshly created edges from evalhere statements as they are not available there
		// because they are only created after the evalhere statements are evaluated 

		for(OrderedReplacements orderedRepls : right.getOrderedReplacements()) {
			for(OrderedReplacement orderedRepl : orderedRepls.orderedReplacements) {
				if(orderedRepl instanceof EvalStatement) {
					EvalStatement evalStmt = (EvalStatement)orderedRepl;
					NeededEntities needs = new NeededEntities(false, true, false, false, true, false, false, false);
					evalStmt.collectNeededEntities(needs);
					for(Edge edge : needs.edges) {
						if((edge.context & BaseNode.CONTEXT_LHS_OR_RHS) == BaseNode.CONTEXT_RHS) {
							error.error(edge.getIdent().getCoords(), "Can't access a newly created edge ("
									+ edge.getIdent() + ") from an evalhere statement");
						}
					}
					for(Edge edge : needs.attrEdges) {
						if((edge.context & BaseNode.CONTEXT_LHS_OR_RHS) == BaseNode.CONTEXT_RHS) {
							error.error(edge.getIdent().getCoords(), "Can't access a newly created edge ("
									+ edge.getIdent() + ") from an evalhere statement");
						}
					}
				}
			}
		}
	}

	// constructs implicit lhs elements
	public void insertElementsFromRhsDeclaredInNestingLhsToLocalLhs(PatternGraphRhs right)
	{
		if(right == null) {
			return;
		}

		// insert all elements, which are used (not declared) on the right hand side and not declared on left hand side,
		//   and are not amongst the replacement parameters
		// which means they are declared in some pattern the left hand side is nested in,
		// to the left hand side (so that they get handed down from the nesting pattern;
		// otherwise they would be created (code generation by locally comparing lhs and rhs))

		for(Node node : right.getNodes()) {
			if(node.directlyNestingLHSGraph != this && !right.replParametersContain(node)) {
				if(!hasNode(node)) {
					addSingleNode(node);
					addHomToAll(node);
				}
			}
		}

		for(Edge edge : right.getEdges()) {
			if(edge.directlyNestingLHSGraph != this && !right.replParametersContain(edge)) {
				if(!hasEdge(edge)) {
					addSingleEdge(edge);
					addHomToAll(edge);
				}
			}
		}

		for(Variable var : right.getVars()) {
			if(var.directlyNestingLHSGraph != this && !right.replParametersContain(var)) {
				addVariable(var);
			}
		}
	}

	public void checkForEmptyPatternsInIterateds()
	{
		if(mayPatternBeEmptyComputationState != PATTERN_NOT_YET_VISITED)
			return;

		mayPatternBeEmptyComputationState = PATTERN_MAYBE_EMPTY;

		///////////////////////////////////////////////////
		// have a look at the local pattern

nodeHom:
		for(Node node : getNodes()) {
			if(node.directlyNestingLHSGraph != this)
				continue nodeHom;
			for(Node homNode : getHomomorphic(node)) {
				if(homNode.directlyNestingLHSGraph != this)
					continue nodeHom;
			}
			mayPatternBeEmptyComputationState = PATTERN_NOT_EMPTY;
			break;
		}
		if(mayPatternBeEmptyComputationState != PATTERN_NOT_EMPTY) {
edgeHom:
			for(Edge edge : getEdges()) {
				if(edge.directlyNestingLHSGraph != this)
					continue edgeHom;
				for(Edge homEdge : getHomomorphic(edge)) {
					if(homEdge.directlyNestingLHSGraph != this)
						continue edgeHom;
				}
				mayPatternBeEmptyComputationState = PATTERN_NOT_EMPTY;
				break;
			}
		}

		///////////////////////////////////////////////////
		// go through the nested patterns, check the iterateds

		for(Alternative alternative : getAlts()) {
			boolean allCasesNonEmpty = true;
			for(Rule altCase : alternative.getAlternativeCases()) {
				altCase.pattern.checkForEmptyPatternsInIterateds();
				if(altCase.pattern.mayPatternBeEmptyComputationState == PATTERN_MAYBE_EMPTY) {
					allCasesNonEmpty = false;
				}
			}
			if(allCasesNonEmpty) {
				mayPatternBeEmptyComputationState = PATTERN_NOT_EMPTY;
			}
		}

		for(Rule iterated : getIters()) {
			iterated.pattern.checkForEmptyPatternsInIterateds();
			if(iterated.pattern.mayPatternBeEmptyComputationState == PATTERN_MAYBE_EMPTY) {
				// emit error if the iterated pattern might be empty
				if(iterated.getMaxMatches() == 0) {
					error.error(iterated.getIdent().getCoords(),
							"An unbounded pattern cardinality construct (iterated, multiple, [*])"
									+ " must contain at least one locally defined node or edge (not being homomorphic to an enclosing element)"
									+ " or a nested subpattern or alternative not being empty");
				} else if(iterated.getMaxMatches() > 1) {
					error.warning(iterated.getIdent().getCoords(),
							"Maybe empty pattern in pattern cardinality construct (you must expect empty matches)");
				}
			} else {
				if(iterated.getMinMatches() > 0) {
					mayPatternBeEmptyComputationState = PATTERN_NOT_EMPTY;
				}
			}
		}

		for(SubpatternUsage sub : getSubpatternUsages()) {
			sub.subpatternAction.pattern.checkForEmptyPatternsInIterateds();
			if(sub.subpatternAction.pattern.mayPatternBeEmptyComputationState == PATTERN_NOT_EMPTY) {
				mayPatternBeEmptyComputationState = PATTERN_NOT_EMPTY;
			}
		}

		for(PatternGraphLhs negative : getNegs()) {
			negative.checkForEmptyPatternsInIterateds();
		}

		for(PatternGraphLhs independent : getIdpts()) {
			independent.checkForEmptyPatternsInIterateds();
		}
	}

	public void checkForEmptySubpatternRecursions(HashSet<PatternGraphLhs> subpatternsAlreadyVisited)
	{
nodeHom:
		for(Node node : getNodes()) {
			if(node.directlyNestingLHSGraph != this)
				continue nodeHom;
			for(Node homNode : getHomomorphic(node)) {
				if(homNode.directlyNestingLHSGraph != this)
					continue nodeHom;
			}
			return; // node which must get matched found -> can't build empty path
		}
edgeHom:
		for(Edge edge : getEdges()) {
			if(edge.directlyNestingLHSGraph != this)
				continue edgeHom;
			for(Edge homEdge : getHomomorphic(edge)) {
				if(homEdge.directlyNestingLHSGraph != this)
					continue edgeHom;
			}
			return; // edge which must get matched found -> can't build empty path
		}

		for(Expression cond : getConditions()) {
			if(cond instanceof Constant) {
				if(((Constant)cond).value instanceof Boolean) {
					Constant constCond = (Constant)cond;
					if(((Boolean)constCond.value).booleanValue()) {
						continue;
					}
				}
			}
			return; // a non-const or non-true const condition is a sign that the recursion will terminate
		}

		for(Alternative alternative : getAlts()) {
			for(Rule altCase : alternative.getAlternativeCases()) {
				HashSet<PatternGraphLhs> subpatternsAlreadyVisitedClone =
						new HashSet<PatternGraphLhs>(subpatternsAlreadyVisited);
				altCase.pattern.checkForEmptySubpatternRecursions(subpatternsAlreadyVisitedClone);
			}
		}

		for(Rule iterated : getIters()) {
			HashSet<PatternGraphLhs> subpatternsAlreadyVisitedClone = new HashSet<PatternGraphLhs>(subpatternsAlreadyVisited);
			iterated.pattern.checkForEmptySubpatternRecursions(subpatternsAlreadyVisitedClone);
		}

		for(PatternGraphLhs negative : getNegs()) {
			HashSet<PatternGraphLhs> subpatternsAlreadyVisitedClone = new HashSet<PatternGraphLhs>(subpatternsAlreadyVisited);
			negative.checkForEmptySubpatternRecursions(subpatternsAlreadyVisitedClone);
		}

		for(PatternGraphLhs independent : getIdpts()) {
			HashSet<PatternGraphLhs> subpatternsAlreadyVisitedClone = new HashSet<PatternGraphLhs>(subpatternsAlreadyVisited);
			independent.checkForEmptySubpatternRecursions(subpatternsAlreadyVisitedClone);
		}

		for(SubpatternUsage sub : getSubpatternUsages()) {
			if(!subpatternsAlreadyVisited.contains(sub.subpatternAction.pattern)) {
				HashSet<PatternGraphLhs> subpatternsAlreadyVisitedClone =
						new HashSet<PatternGraphLhs>(subpatternsAlreadyVisited);
				subpatternsAlreadyVisitedClone.add(sub.subpatternAction.pattern);
				sub.subpatternAction.pattern.checkForEmptySubpatternRecursions(subpatternsAlreadyVisitedClone);
			} else {
				// we're on path of only (maybe) empty patterns and see a subpattern already on it again
				// -> endless loop of this subpattern matching only empty patterns until it gets matched again 
				error.error(sub.subpatternAction.getIdent().getCoords(), "The subpattern "
						+ sub.subpatternAction.getIdent()
						+ " (potentially) calls itself again with only empty patterns in between yielding an endless loop");
			}
		}
	}

	public boolean isNeverTerminatingSuccessfully(HashSet<PatternGraphLhs> subpatternsAlreadyVisited)
	{
		boolean neverTerminatingSuccessfully = false;

		for(Alternative alternative : getAlts()) {
			boolean allCasesNotTerminating = true;
			for(Rule altCase : alternative.getAlternativeCases()) {
				HashSet<PatternGraphLhs> subpatternsAlreadyVisitedClone =
						new HashSet<PatternGraphLhs>(subpatternsAlreadyVisited);
				allCasesNotTerminating &= altCase.pattern.isNeverTerminatingSuccessfully(subpatternsAlreadyVisitedClone);
			}
			neverTerminatingSuccessfully |= allCasesNotTerminating;
		}

		for(Rule iterated : getIters()) {
			HashSet<PatternGraphLhs> subpatternsAlreadyVisitedClone = new HashSet<PatternGraphLhs>(subpatternsAlreadyVisited);
			if(iterated.getMinMatches() > 0)
				neverTerminatingSuccessfully |= iterated.pattern.isNeverTerminatingSuccessfully(subpatternsAlreadyVisitedClone);
		}

		for(PatternGraphLhs negative : getNegs()) {
			HashSet<PatternGraphLhs> subpatternsAlreadyVisitedClone = new HashSet<PatternGraphLhs>(subpatternsAlreadyVisited);
			neverTerminatingSuccessfully |= negative.isNeverTerminatingSuccessfully(subpatternsAlreadyVisitedClone);
		}

		for(PatternGraphLhs independent : getIdpts()) {
			HashSet<PatternGraphLhs> subpatternsAlreadyVisitedClone = new HashSet<PatternGraphLhs>(subpatternsAlreadyVisited);
			neverTerminatingSuccessfully |= independent.isNeverTerminatingSuccessfully(subpatternsAlreadyVisitedClone);
		}

		for(SubpatternUsage sub : getSubpatternUsages()) {
			if(!subpatternsAlreadyVisited.contains(sub.subpatternAction.pattern)) {
				HashSet<PatternGraphLhs> subpatternsAlreadyVisitedClone =
						new HashSet<PatternGraphLhs>(subpatternsAlreadyVisited);
				subpatternsAlreadyVisitedClone.add(sub.subpatternAction.pattern);
				neverTerminatingSuccessfully |= sub.subpatternAction.pattern.isNeverTerminatingSuccessfully(subpatternsAlreadyVisitedClone);
			} else {
				return true;
			}
		}

		return neverTerminatingSuccessfully;
	}

	public void checkForMultipleRetypes(HashSet<Node> alreadyDefinedNodes, HashSet<Edge> alreadyDefinedEdges,
			PatternGraphBase right)
	{
		for(Node node : getNodes()) {
			alreadyDefinedNodes.add(node);
		}
		for(Edge edge : getEdges()) {
			alreadyDefinedEdges.add(edge);
		}

		for(Alternative alternative : getAlts()) {
			for(Rule altCase : alternative.getAlternativeCases()) {
				PatternGraphLhs altCasePattern = altCase.getLeft();
				HashSet<Node> alreadyDefinedNodesClone = new HashSet<Node>(alreadyDefinedNodes);
				HashSet<Edge> alreadyDefinedEdgesClone = new HashSet<Edge>(alreadyDefinedEdges);
				altCasePattern.checkForMultipleRetypes(
						alreadyDefinedNodesClone, alreadyDefinedEdgesClone, altCase.getRight());
			}
		}

		for(Rule iterated : getIters()) {
			PatternGraphLhs iteratedPattern = iterated.getLeft();
			HashSet<Node> alreadyDefinedNodesClone = new HashSet<Node>(alreadyDefinedNodes);
			HashSet<Edge> alreadyDefinedEdgesClone = new HashSet<Edge>(alreadyDefinedEdges);
			iteratedPattern.checkForMultipleRetypes(
					alreadyDefinedNodesClone, alreadyDefinedEdgesClone, iterated.getRight());

			if(iterated.getMaxMatches() != 1) {
				iteratedPattern.checkForMultipleRetypesDoCheck(alreadyDefinedNodes, alreadyDefinedEdges,
						iterated.getRight());
			}
		}
	}

	public void checkForMultipleRetypesDoCheck(HashSet<Node> alreadyDefinedNodes, HashSet<Edge> alreadyDefinedEdges,
			PatternGraphBase right)
	{
		for(Node node : right.getNodes()) {
			if(node.getRetypedNode(right) == null)
				continue;
			if(alreadyDefinedNodes.contains(node)) {
				error.error(node.getIdent().getCoords(),
						"Retype of nodes from outside is forbidden if contained in construct which can get matched more than once (due to some kind of iterated)");
			} else {
				for(Node homToRetypedNode : getHomomorphic(node)) {
					if(alreadyDefinedNodes.contains(homToRetypedNode)) {
						error.error(node.getIdent().getCoords(),
								"Retype of nodes which might be hom to nodes from outside is forbidden if contained in construct which can get matched more than once (due to some kind of iterated)");
					}
				}
			}
		}
		for(Edge edge : right.getEdges()) {
			if(edge.getRetypedEdge(right) == null)
				continue;
			if(alreadyDefinedEdges.contains(edge)) {
				error.error(edge.getIdent().getCoords(),
						"Retype of edges from outside is forbidden if contained in construct which can get matched more than once (due to some kind of iterated)");
			} else {
				for(Edge homToRetypedEdge : getHomomorphic(edge)) {
					if(alreadyDefinedEdges.contains(homToRetypedEdge)) {
						error.error(edge.getIdent().getCoords(),
								"Retype of edges which might be hom to edges from outside is forbidden if contained in construct which can get matched more than once (due to some kind of iterated)");
					}
				}
			}
		}

		for(Alternative alternative : getAlts()) {
			for(Rule altCase : alternative.getAlternativeCases()) {
				PatternGraphLhs altCasePattern = altCase.getLeft();
				altCasePattern.checkForMultipleRetypesDoCheck(
						alreadyDefinedNodes, alreadyDefinedEdges, altCase.getRight());
			}
		}

		for(Rule iterated : getIters()) {
			PatternGraphLhs iteratedPattern = iterated.getLeft();
			iteratedPattern.checkForMultipleRetypesDoCheck(
					alreadyDefinedNodes, alreadyDefinedEdges, iterated.getRight());
		}
	}
}
