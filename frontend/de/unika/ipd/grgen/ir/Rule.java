/*
 * GrGen: graph rewrite generator tool -- release GrGen.NET 3.0
 * Copyright (C) 2003-2011 Universitaet Karlsruhe, Institut fuer Programmstrukturen und Datenorganisation, LS Goos
 * licensed under LGPL v3 (see LICENSE.txt included in the packaging of this file)
 * www.grgen.net
 */

/**
 * @author shack, Daniel Grund
 * @version $Id$
 */
package de.unika.ipd.grgen.ir;

import java.util.ArrayList;
import java.util.Collection;
import java.util.Collections;
import java.util.HashMap;
import java.util.HashSet;
import java.util.Iterator;
import java.util.LinkedList;
import java.util.List;

import de.unika.ipd.grgen.ast.BaseNode;

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

	/** The evaluation assignments of this rule (RHS). */
	private final Collection<EvalStatement> evals = new LinkedList<EvalStatement>();

	/** How often the pattern is to be matched in case this is an iterated. */
	private int minMatches;
	private int maxMatches;
	
	/** Was the replacement code already called by means of an iterated replacement declaration? (in case this is an iterated.) */
	public boolean wasReplacementAlreadyCalled;
	
	/** Have deferred execs been added by using this top level rule, so we have to execute the exec queue? */
	public boolean mightThereBeDeferredExecs;

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
		mightThereBeDeferredExecs = false;
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
		mightThereBeDeferredExecs = false;
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
	
	public void checkForRhsElementsUsedOnLhs()
	{
		PatternGraph left = getLeft();
		for(Node node : left.getNodes()) {
			if((node.context&BaseNode.CONTEXT_LHS_OR_RHS)==BaseNode.CONTEXT_RHS) {
				error.error(node.getIdent().getCoords(), "Nodes declared in rewrite part can't be accessed in pattern");
			}
		}
		for(Edge edge : left.getEdges()) {
			if((edge.context&BaseNode.CONTEXT_LHS_OR_RHS)==BaseNode.CONTEXT_RHS) {
				error.error(edge.getIdent().getCoords(), "Edges declared in rewrite part can't be accessed in pattern");
			}
		}
	}
	
	public void computeUsageDependencies(HashMap<Rule, HashSet<Rule>> subpatternsDefToUse, Rule subpattern)
	{
		for(SubpatternUsage sub : pattern.getSubpatternUsages()) {
			subpatternsDefToUse.get(sub.subpatternAction).add(subpattern);
		}

		for(Alternative alternative : pattern.getAlts()) {
			for(Rule altCase : alternative.getAlternativeCases()) {
				altCase.computeUsageDependencies(subpatternsDefToUse, subpattern);
			}
		}

		for(Rule iterated : pattern.getIters()) {
			iterated.computeUsageDependencies(subpatternsDefToUse, subpattern);
		}
	}
		
	public boolean checkForMultipleDeletesOrRetypes(HashMap<Entity, Rule> entitiesToTheirDeletingOrRetypingPattern,
						HashMap<Rule, HashMap<Entity, Rule>> subpatternsToParametersToTheirDeletingOrRetypingPattern)
	{		
		if(right==null) {
			return false;
		}
		
		for(Node node : pattern.getNodes()) {
			for(Node homNode : pattern.getHomomorphic(node)) {
				if(!right.hasNode(homNode)) {
					if(entitiesToTheirDeletingOrRetypingPattern.containsKey(node) && entitiesToTheirDeletingOrRetypingPattern.get(node)!=this) {
						reportMultipleDeleteOrRetype(node, entitiesToTheirDeletingOrRetypingPattern.get(node), this);
					} else {
						entitiesToTheirDeletingOrRetypingPattern.put(node, this);
					}
				}
				if(homNode.changesType(right)) {
					if(entitiesToTheirDeletingOrRetypingPattern.containsKey(node)) {
						reportMultipleDeleteOrRetype(node, entitiesToTheirDeletingOrRetypingPattern.get(node), this);
					} else {
						entitiesToTheirDeletingOrRetypingPattern.put(node, this);
					}
				}
			}
		}
		for(Edge edge : pattern.getEdges()) {
			for(Edge homEdge : pattern.getHomomorphic(edge)) {
				if(!right.hasEdge(homEdge)) {
					if(entitiesToTheirDeletingOrRetypingPattern.containsKey(edge) && entitiesToTheirDeletingOrRetypingPattern.get(edge)!=this) {
						reportMultipleDeleteOrRetype(edge, entitiesToTheirDeletingOrRetypingPattern.get(edge), this);
					} else {
						entitiesToTheirDeletingOrRetypingPattern.put(edge, this);
					}
				}
				if(homEdge.changesType(right)) {
					if(entitiesToTheirDeletingOrRetypingPattern.containsKey(edge)) {
						reportMultipleDeleteOrRetype(edge, entitiesToTheirDeletingOrRetypingPattern.get(edge), this);
					} else {
						entitiesToTheirDeletingOrRetypingPattern.put(edge, this);
					}
				}
			}
		}

		for(SubpatternUsage sub : pattern.getSubpatternUsages()) {
			boolean isDependentReplacementUsed = false;
			for(OrderedReplacement or : right.getOrderedReplacements()) {
				if(!(or instanceof SubpatternDependentReplacement))
					continue;
				if(((SubpatternDependentReplacement)or).getSubpatternUsage()==sub) {
					isDependentReplacementUsed = true;
				}
			}
			if(!isDependentReplacementUsed)
				continue;
			
			List<Entity> parameters = sub.subpatternAction.getParameters();
			Iterator<Entity> parametersIt = parameters.iterator();
			List<Expression> arguments = sub.subpatternConnections;
			Iterator<Expression> argumentsIt = arguments.iterator();
			while(argumentsIt.hasNext()) {
				assert parametersIt.hasNext();
				Expression argument = argumentsIt.next();
				Entity parameter = parametersIt.next();
				if(argument instanceof GraphEntityExpression) {
					GraphEntity argumentEntity = ((GraphEntityExpression)argument).getGraphEntity();
					HashMap<Entity, Rule> parametersToTheirDeletingOrRetypingPattern = 
						subpatternsToParametersToTheirDeletingOrRetypingPattern.get(sub.subpatternAction);
					Rule deletingOrRetypingPattern = parametersToTheirDeletingOrRetypingPattern.get(parameter);
					if(deletingOrRetypingPattern!=null) {
						if(entitiesToTheirDeletingOrRetypingPattern.containsKey(argumentEntity)) {
							reportMultipleDeleteOrRetype(argumentEntity, entitiesToTheirDeletingOrRetypingPattern.get(argumentEntity), deletingOrRetypingPattern);
						} else {
							entitiesToTheirDeletingOrRetypingPattern.put(argumentEntity, deletingOrRetypingPattern);
						}
					}
				}
			}
		}

		for(Alternative alternative : pattern.getAlts()) {
			ArrayList<HashMap<Entity, Rule>> entitiesToTheirDeletingOrRetypingPatternOfAlternativCases = new ArrayList<HashMap<Entity, Rule>>();
			for(Rule altCase : alternative.getAlternativeCases()) {
				HashMap<Entity, Rule> entitiesToTheirDeletingOrRetypingPatternClone = new HashMap<Entity, Rule>(entitiesToTheirDeletingOrRetypingPattern);
				altCase.checkForMultipleDeletesOrRetypes(entitiesToTheirDeletingOrRetypingPatternClone,
						subpatternsToParametersToTheirDeletingOrRetypingPattern);
				entitiesToTheirDeletingOrRetypingPatternOfAlternativCases.add(entitiesToTheirDeletingOrRetypingPatternClone);
			}
			for(HashMap<Entity, Rule> entitiesToTheirDeletingOrRetypingPatternOfAlternativCase : entitiesToTheirDeletingOrRetypingPatternOfAlternativCases) {
				for(Entity entityOfAlternativeCase : entitiesToTheirDeletingOrRetypingPatternOfAlternativCase.keySet()) {
					Rule deletingOrRetypingPatternOld = entitiesToTheirDeletingOrRetypingPattern.get(entityOfAlternativeCase);
					Rule deletingOrRetypingPatternNew = entitiesToTheirDeletingOrRetypingPatternOfAlternativCase.get(entityOfAlternativeCase);
					if(deletingOrRetypingPatternOld==null && deletingOrRetypingPatternNew!=null) {
						entitiesToTheirDeletingOrRetypingPattern.put(entityOfAlternativeCase, deletingOrRetypingPatternNew);
					}
				}
			}
		}

		for(Rule iterated : pattern.getIters()) {
			iterated.checkForMultipleDeletesOrRetypes(entitiesToTheirDeletingOrRetypingPattern,
					subpatternsToParametersToTheirDeletingOrRetypingPattern);
		}

		boolean changed = false;
		if(subpatternsToParametersToTheirDeletingOrRetypingPattern.containsKey(this)) {
			HashMap<Entity, Rule> parametersToTheirDeletingOrRetypingPattern = subpatternsToParametersToTheirDeletingOrRetypingPattern.get(this);
			for(Entity parameter : parametersToTheirDeletingOrRetypingPattern.keySet()) {
				Rule deletingOrRetypingPatternOld = parametersToTheirDeletingOrRetypingPattern.get(parameter);
				Rule deletingOrRetypingPatternNew = entitiesToTheirDeletingOrRetypingPattern.get(parameter);
				if(deletingOrRetypingPatternOld==null && deletingOrRetypingPatternNew!=null) {
					parametersToTheirDeletingOrRetypingPattern.put(parameter, deletingOrRetypingPatternNew);
					changed = true;
				}
			}
		}
		return changed;
	}
	
	void reportMultipleDeleteOrRetype(Entity entity, Rule first, Rule second) {
		error.error(entity.getIdent().getCoords(), "The entity " + entity.getIdent() + " (or a hom entity)"
				+ " may get deleted or retyped in pattern " + first.getIdent() + " starting at " + first.getIdent().getCoords()
				+ " and in pattern " + second.getIdent() + " starting at " + second.getIdent().getCoords() + " (only one such place allowed, provable at compile time)");
	}
	
	boolean isUsingNonDirectExec(boolean isTopLevelRule) {
		if(right==null) {
			return false;
		}
		
		if(!isTopLevelRule) {
			for(ImperativeStmt is : right.getImperativeStmts()) {
				if(is instanceof Exec) {
					return true;
				}
			}
		}
		
		for(Alternative alternative : pattern.getAlts()) {
			for(Rule altCase : alternative.getAlternativeCases()) {
				if(altCase.isUsingNonDirectExec(false)) {
					return true;
				}
			}
		}

		for(Rule iterated : pattern.getIters()) {
			if(iterated.isUsingNonDirectExec(false)) {
				return true;
			}
		}

		return false;
	}

	public void setDependencyLevelByStorageMapAccess() {
		PatternGraph left = getLeft();
		final int MAX_CHAINING_FOR_STORAGE_MAP_ACCESS = 1000;
		int dependencyLevel=0;
		boolean somethingChanged;
		do {
			somethingChanged = false;
			
			for(Node node : left.getNodes()) {
				if(node.getAccessor()!=null) {
					if(node.getDependencyLevel()<=node.getAccessor().getDependencyLevel()) {
						node.incrementDependencyLevel();
						dependencyLevel = Math.max(node.getDependencyLevel(), dependencyLevel);
						somethingChanged = true;
					}
				}
				if(node.getStorageAttribute()!=null) {
					if(node.getDependencyLevel()<=((GraphEntity)node.getStorageAttribute().getOwner()).getDependencyLevel()) {
						node.incrementDependencyLevel();
						dependencyLevel = Math.max(node.getDependencyLevel(), dependencyLevel);
						somethingChanged = true;
					}
				}
			}
			for(Edge edge : left.getEdges()) {
				if(edge.getAccessor()!=null) {
					if(edge.getDependencyLevel()<=edge.getAccessor().getDependencyLevel()) {
						edge.incrementDependencyLevel();
						dependencyLevel = Math.max(edge.getDependencyLevel(), dependencyLevel);
						somethingChanged = true;
					}
				}
				if(edge.getStorageAttribute()!=null) {
					if(edge.getDependencyLevel()<=((GraphEntity)edge.getStorageAttribute().getOwner()).getDependencyLevel()) {
						edge.incrementDependencyLevel();
						dependencyLevel = Math.max(edge.getDependencyLevel(), dependencyLevel);
						somethingChanged = true;
					}
				}
			}
			if(dependencyLevel>=MAX_CHAINING_FOR_STORAGE_MAP_ACCESS) {
				error.error("Cycle in match node/edge by storage map access or storage attribute.");
				break;
			}
		} while(somethingChanged);
		
		for(Alternative alternative : pattern.getAlts()) {
			for(Rule altCase : alternative.getAlternativeCases()) {
				altCase.setDependencyLevelByStorageMapAccess();
			}
		}

		for(Rule iterated : pattern.getIters()) {
			iterated.setDependencyLevelByStorageMapAccess();
		}
	}
}
