/*
 * GrGen: graph rewrite generator tool -- release GrGen.NET 7.1
 * Copyright (C) 2003-2025 Universitaet Karlsruhe, Institut fuer Programmstrukturen und Datenorganisation, LS Goos; and free programmers
 * licensed under LGPL v3 (see LICENSE.txt included in the packaging of this file)
 * www.grgen.net
 */

/**
 * @author shack, Daniel Grund
 */

package de.unika.ipd.grgen.ir.executable;

import java.util.ArrayList;
import java.util.Collection;
import java.util.Collections;
import java.util.EnumSet;
import java.util.HashMap;
import java.util.HashSet;
import java.util.Iterator;
import java.util.LinkedList;
import java.util.List;

import de.unika.ipd.grgen.ir.ContainedInPackage;
import de.unika.ipd.grgen.ir.Entity;
import de.unika.ipd.grgen.ir.Exec;
import de.unika.ipd.grgen.ir.Ident;
import de.unika.ipd.grgen.ir.NeededEntities;
import de.unika.ipd.grgen.ir.NeededEntities.Needs;
import de.unika.ipd.grgen.ir.expr.Expression;
import de.unika.ipd.grgen.ir.expr.GraphEntityExpression;
import de.unika.ipd.grgen.ir.expr.Qualification;
import de.unika.ipd.grgen.ir.pattern.Alternative;
import de.unika.ipd.grgen.ir.pattern.Edge;
import de.unika.ipd.grgen.ir.pattern.GraphEntity;
import de.unika.ipd.grgen.ir.pattern.IndexAccessOrdering;
import de.unika.ipd.grgen.ir.pattern.Node;
import de.unika.ipd.grgen.ir.pattern.OrderedReplacement;
import de.unika.ipd.grgen.ir.pattern.OrderedReplacements;
import de.unika.ipd.grgen.ir.pattern.PatternGraphLhs;
import de.unika.ipd.grgen.ir.pattern.PatternGraphRhs;
import de.unika.ipd.grgen.ir.pattern.RetypedEdge;
import de.unika.ipd.grgen.ir.pattern.RetypedNode;
import de.unika.ipd.grgen.ir.pattern.SubpatternDependentReplacement;
import de.unika.ipd.grgen.ir.pattern.SubpatternUsage;
import de.unika.ipd.grgen.ir.stmt.EvalStatements;
import de.unika.ipd.grgen.ir.stmt.ImperativeStmt;
import de.unika.ipd.grgen.ir.type.DefinedMatchType;
import de.unika.ipd.grgen.ast.BaseNode;

/**
 * A graph rewrite rule or subrule, with none, one, or arbitrary many (not yet) replacements.
 */
public class Rule extends MatchingAction implements ContainedInPackage
{
	/** Names of the children of this node. */
	private static final String[] childrenNames = {
			"left", "right", "eval"
	};

	private String packageContainedIn;

	/** The right hand side of the rule. */
	private PatternGraphRhs right;

	/** The match classes that get implemented */
	private final List<DefinedMatchType> implementedMatchClasses = new LinkedList<DefinedMatchType>();

	/** The evaluation assignments of this rule (RHS). */
	private final Collection<EvalStatements> evalStatements = new LinkedList<EvalStatements>();

	/** How often the pattern is to be matched in case this is an iterated. */
	private int minMatches;
	private int maxMatches;

	/** Was the replacement code already called by means of an iterated replacement declaration? (in case this is an iterated.) */
	public boolean wasReplacementAlreadyCalled;

	/** Have deferred execs been added by using this top level rule, so we have to execute the exec queue? */
	public boolean mightThereBeDeferredExecs;
	
	public enum RuleKind { Rule, Test, Subpattern, AlternativeCase, Iterated };
	public static String toString(RuleKind ruleKind)
	{
		switch(ruleKind)
		{
		case Rule: return "rule";
		case Test: return "test";
		case Subpattern: return "(sub)pattern";
		case AlternativeCase: return "alternative case";
		case Iterated: return "iterated";
		default: throw new RuntimeException("Unexpected case");
		}
	}
	public RuleKind ruleKind;

	/**
	 * Make a new rule.
	 * @param ident The identifier with which the rule was declared.
	 */
	public Rule(Ident ident, RuleKind ruleKind)
	{
		super("rule", ident);
		setChildrenNames(childrenNames);
		this.ruleKind = ruleKind;
		this.minMatches = -1;
		this.maxMatches = -1;
		mightThereBeDeferredExecs = false;
	}

	/**
	 * Make a new iterated rule.
	 * @param ident The identifier with which the rule was declared.
	 */
	public Rule(Ident ident, int minMatches, int maxMatches)
	{
		super("rule", ident);
		setChildrenNames(childrenNames);
		this.ruleKind = RuleKind.Iterated;
		this.minMatches = minMatches;
		this.maxMatches = maxMatches;
		mightThereBeDeferredExecs = false;
	}

	/**
	 * @param pattern The left side graph of the rule.
	 * @param right The right side graph of the rule.
	 */
	public void initialize(PatternGraphLhs pattern, PatternGraphRhs right)
	{
		super.setPattern(pattern);
		this.right = right;
		if(right == null) {
			pattern.setNameSuffix("test");
		} else {
			pattern.setName("L");
			right.setName("R");
		}
	}

	@Override
	public String getPackageContainedIn()
	{
		return packageContainedIn;
	}

	public void setPackageContainedIn(String packageContainedIn)
	{
		this.packageContainedIn = packageContainedIn;
	}

	public boolean isSubpattern()
	{
		return ruleKind == RuleKind.Subpattern;
	}
	
	/** @return A collection containing all eval assignments of this rule. */
	public Collection<EvalStatements> getEvals()
	{
		return Collections.unmodifiableCollection(evalStatements);
	}

	/** Add an assignment to the list of evaluations. */
	public void addEval(EvalStatements a)
	{
		evalStatements.add(a);
	}

	/**
	 *  @return A set with nodes, that occur on the left _and_ on the right side of the rule.
	 *  		The set also contains retyped nodes.
	 */
	public Collection<Node> getCommonNodes()
	{
		Collection<Node> common = new HashSet<Node>(pattern.getNodes());
		common.retainAll(right.getNodes());
		return common;
	}

	/**
	 * @return A set with edges, that occur on the left _and_ on the right side of the rule.
	 *         The set also contains all retyped edges.
	 */
	public Collection<Edge> getCommonEdges()
	{
		Collection<Edge> common = new HashSet<Edge>(pattern.getEdges());
		common.retainAll(right.getEdges());
		return common;
	}

	/** @return A set with subpatterns, that occur on the left _and_ on the right side of the rule. */
	public Collection<SubpatternUsage> getCommonSubpatternUsages()
	{
		Collection<SubpatternUsage> common = new HashSet<SubpatternUsage>(pattern.getSubpatternUsages());
		common.retainAll(right.getSubpatternUsages());
		return common;
	}

	/** @return The left hand side graph. */
	public PatternGraphLhs getLeft()
	{
		return pattern;
	}

	/** @return The right hand side graph. */
	public PatternGraphRhs getRight()
	{
		return right;
	}

	public Collection<DefinedMatchType> getImplementedMatchClasses()
	{
		return implementedMatchClasses;
	}

	public void addImplementedMatchClass(DefinedMatchType implementedMatchClass)
	{
		implementedMatchClasses.add(implementedMatchClass);
	}

	/** @return Minimum number of how often the pattern must get matched. */
	public int getMinMatches()
	{
		return minMatches;
	}

	/** @return Maximum number of how often the pattern must get matched. 0 means unlimited */
	public int getMaxMatches()
	{
		return maxMatches;
	}

	public void checkForRhsElementsUsedOnLhs()
	{
		PatternGraphLhs left = getLeft();
		for(Node node : left.getNodes()) {
			if((node.context & BaseNode.CONTEXT_LHS_OR_RHS) == BaseNode.CONTEXT_RHS) {
				error.error(node.getIdent().getCoords(), "Nodes declared in the rewrite part cannot be accessed in the pattern part (as is the case for " + node.getIdent() + ").");
			}
		}
		for(Edge edge : left.getEdges()) {
			if((edge.context & BaseNode.CONTEXT_LHS_OR_RHS) == BaseNode.CONTEXT_RHS) {
				error.error(edge.getIdent().getCoords(), "Edges declared in the rewrite part cannot be accessed in the pattern part (as is the case for " + edge.getIdent() + ").");
			}
		}
	}

	public void computeUsageDependencies(HashMap<Rule, HashSet<Rule>> subpatternsDefToUse, Rule subpattern)
	{
		for(SubpatternUsage sub : pattern.getSubpatternUsages()) {
			HashSet<Rule> uses = subpatternsDefToUse.get(sub.subpatternAction);
			uses.add(subpattern);
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
		if(right == null) {
			return false;
		}

		for(Node node : pattern.getNodes()) {
			for(Node homNode : pattern.getHomomorphic(node)) {
				if(!right.hasNode(homNode)) {
					if(entitiesToTheirDeletingOrRetypingPattern.containsKey(node)
							&& entitiesToTheirDeletingOrRetypingPattern.get(node) != this) {
						reportMultipleDeleteOrRetype(node, entitiesToTheirDeletingOrRetypingPattern.get(node), this);
					} else {
						entitiesToTheirDeletingOrRetypingPattern.put(node, this);
					}
				}
				if(homNode.changesType(right)) {
					if(entitiesToTheirDeletingOrRetypingPattern.containsKey(node)
							&& entitiesToTheirDeletingOrRetypingPattern.get(node) != this) {
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
					if(entitiesToTheirDeletingOrRetypingPattern.containsKey(edge)
							&& entitiesToTheirDeletingOrRetypingPattern.get(edge) != this) {
						reportMultipleDeleteOrRetype(edge, entitiesToTheirDeletingOrRetypingPattern.get(edge), this);
					} else {
						entitiesToTheirDeletingOrRetypingPattern.put(edge, this);
					}
				}
				if(homEdge.changesType(right)) {
					if(entitiesToTheirDeletingOrRetypingPattern.containsKey(edge)
							&& entitiesToTheirDeletingOrRetypingPattern.get(edge) != this) {
						reportMultipleDeleteOrRetype(edge, entitiesToTheirDeletingOrRetypingPattern.get(edge), this);
					} else {
						entitiesToTheirDeletingOrRetypingPattern.put(edge, this);
					}
				}
			}
		}

		for(SubpatternUsage sub : pattern.getSubpatternUsages()) {
			boolean isDependentReplacementUsed = false;
			for(OrderedReplacements ors : right.getOrderedReplacements()) {
				for(OrderedReplacement or : ors.orderedReplacements) {
					if(!(or instanceof SubpatternDependentReplacement))
						continue;
					if(((SubpatternDependentReplacement)or).getSubpatternUsage() == sub) {
						isDependentReplacementUsed = true;
					}
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
					if(deletingOrRetypingPattern != null) {
						if(entitiesToTheirDeletingOrRetypingPattern.containsKey(argumentEntity)) {
							reportMultipleDeleteOrRetype(argumentEntity,
									entitiesToTheirDeletingOrRetypingPattern.get(argumentEntity),
									deletingOrRetypingPattern);
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
				HashMap<Entity, Rule> entitiesToTheirDeletingOrRetypingPatternClone =
						new HashMap<Entity, Rule>(entitiesToTheirDeletingOrRetypingPattern);
				altCase.checkForMultipleDeletesOrRetypes(entitiesToTheirDeletingOrRetypingPatternClone,
						subpatternsToParametersToTheirDeletingOrRetypingPattern);
				entitiesToTheirDeletingOrRetypingPatternOfAlternativCases.add(entitiesToTheirDeletingOrRetypingPatternClone);
			}
			for(HashMap<Entity, Rule> entitiesToTheirDeletingOrRetypingPatternOfAlternativCase : entitiesToTheirDeletingOrRetypingPatternOfAlternativCases) {
				for(Entity entityOfAlternativeCase : entitiesToTheirDeletingOrRetypingPatternOfAlternativCase.keySet()) {
					Rule deletingOrRetypingPatternOld = entitiesToTheirDeletingOrRetypingPattern.get(entityOfAlternativeCase);
					Rule deletingOrRetypingPatternNew = entitiesToTheirDeletingOrRetypingPatternOfAlternativCase.get(entityOfAlternativeCase);
					if(deletingOrRetypingPatternOld == null && deletingOrRetypingPatternNew != null) {
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
			HashMap<Entity, Rule> parametersToTheirDeletingOrRetypingPattern =
					subpatternsToParametersToTheirDeletingOrRetypingPattern.get(this);
			for(Entity parameter : parametersToTheirDeletingOrRetypingPattern.keySet()) {
				Rule deletingOrRetypingPatternOld = parametersToTheirDeletingOrRetypingPattern.get(parameter);
				Rule deletingOrRetypingPatternNew = entitiesToTheirDeletingOrRetypingPattern.get(parameter);
				if(deletingOrRetypingPatternOld == null && deletingOrRetypingPatternNew != null) {
					parametersToTheirDeletingOrRetypingPattern.put(parameter, deletingOrRetypingPatternNew);
					changed = true;
				}
			}
		}
		return changed;
	}

	static void reportMultipleDeleteOrRetype(Entity entity, Rule first, Rule second)
	{
		error.error(entity.getIdent().getCoords(), "The " + entity.getKind() + " " + entity.getIdent() + " or a hom " + entity.getKind()
				+ " may get deleted or retyped in " + toString(first.ruleKind) + " " + first.getIdent() + " [declared at " + first.getIdent().getCoords() + "]"
				+ " and in " + toString(second.ruleKind) + " " + second.getIdent() + " [declared at " + second.getIdent().getCoords() + "]"
				+ " (only one such place is allowed, determinable at compile time).");
	}

	public void checkForMultipleRetypesLocal()
	{
		if(right == null) {
			return;
		}

		for(Node node : pattern.getNodes()) {
			for(Node homNode : pattern.getHomomorphic(node)) {
				if(node == homNode)
					continue;
				if(node.changesType(right) && homNode.changesType(right)) {
					reportMultipleRetype(node, homNode);
				}
			}
		}
		for(Edge edge : pattern.getEdges()) {
			for(Edge homEdge : pattern.getHomomorphic(edge)) {
				if(edge == homEdge)
					continue;
				if(edge.changesType(right) && homEdge.changesType(right)) {
					reportMultipleRetype(edge, homEdge);
				}
			}
		}
		
		for(Alternative alternative : pattern.getAlts()) {
			for(Rule altCase : alternative.getAlternativeCases()) {
				altCase.checkForMultipleRetypesLocal();
			}
		}

		for(Rule iterated : pattern.getIters()) {
			iterated.checkForMultipleRetypesLocal();
		}
	}

	static void reportMultipleRetype(Entity entity, Entity homEntity)
	{
		error.error(entity.getIdent().getCoords(), "The " + entity.getKind() + " " + entity.getIdent()
				+ " and the hom " + entity.getKind() + " " + homEntity.getIdent() + homEntity.getIdent().getCoords().getDeclarationCoords(false)
				+ " are both retyped, so a homomorphically matched graph element may get retyped multiple times.");
	}

	public boolean isUsingNonDirectExec(boolean isTopLevelRule)
	{
		if(right == null) {
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

	public void setDependencyLevelOfInterElementDependencies()
	{
		PatternGraphLhs left = getLeft();
		final int MAX_CHAINING_FOR_STORAGE_MAP_ACCESS = 1000;
		int dependencyLevel = 0;
		boolean somethingChanged;
		do {
			somethingChanged = false;

			for(Node node : left.getNodes()) {
				if(node.storageAccessIndex != null && node.storageAccessIndex.indexGraphEntity != null) {
					GraphEntity indexGraphEntity = node.storageAccessIndex.indexGraphEntity;
					if(node.getDependencyLevel() <= indexGraphEntity.getDependencyLevel()) {
						node.incrementDependencyLevel();
						dependencyLevel = Math.max(node.getDependencyLevel(), dependencyLevel);
						somethingChanged = true;
					}
				}
				if(node.storageAccess != null && node.storageAccess.storageAttribute != null) {
					Qualification storageAttribute = node.storageAccess.storageAttribute;
					if(node.getDependencyLevel() <= ((GraphEntity)storageAttribute.getOwner()).getDependencyLevel()) {
						node.incrementDependencyLevel();
						dependencyLevel = Math.max(node.getDependencyLevel(), dependencyLevel);
						somethingChanged = true;
					}
				}
				if(node.indexAccess != null) {
					NeededEntities needs = new NeededEntities(EnumSet.of(Needs.NODES, Needs.EDGES, Needs.CONTAINER_EXPRS));
					node.indexAccess.collectNeededEntities(needs);
					GraphEntity indexGraphEntity = getAtMostOneNeededGraphElement(needs, node);
					if(indexGraphEntity != null) {
						if(node.getDependencyLevel() <= indexGraphEntity.getDependencyLevel()) {
							node.incrementDependencyLevel();
							dependencyLevel = Math.max(node.getDependencyLevel(), dependencyLevel);
							somethingChanged = true;
						}
					}
				}
				if(!node.multipleIndexAccesses.isEmpty()) {
					NeededEntities needs = new NeededEntities(EnumSet.of(Needs.NODES, Needs.EDGES, Needs.CONTAINER_EXPRS));
					for(IndexAccessOrdering indexAccess : node.multipleIndexAccesses) {
						indexAccess.collectNeededEntities(needs);
					}
					GraphEntity indexGraphEntity = getAtMostOneNeededGraphElement(needs, node);
					if(indexGraphEntity != null) {
						if(node.getDependencyLevel() <= indexGraphEntity.getDependencyLevel()) {
							node.incrementDependencyLevel();
							dependencyLevel = Math.max(node.getDependencyLevel(), dependencyLevel);
							somethingChanged = true;
						}
					}
				}
				if(node.nameMapAccess != null) {
					NeededEntities needs = new NeededEntities(EnumSet.of(Needs.NODES, Needs.EDGES, Needs.CONTAINER_EXPRS));
					node.nameMapAccess.collectNeededEntities(needs);
					GraphEntity indexGraphEntity = getAtMostOneNeededGraphElement(needs, node);
					if(indexGraphEntity != null) {
						if(node.getDependencyLevel() <= indexGraphEntity.getDependencyLevel()) {
							node.incrementDependencyLevel();
							dependencyLevel = Math.max(node.getDependencyLevel(), dependencyLevel);
							somethingChanged = true;
						}
					}
				}
				if(node.uniqueIndexAccess != null) {
					NeededEntities needs = new NeededEntities(EnumSet.of(Needs.NODES, Needs.EDGES, Needs.CONTAINER_EXPRS));
					node.uniqueIndexAccess.collectNeededEntities(needs);
					GraphEntity indexGraphEntity = getAtMostOneNeededGraphElement(needs, node);
					if(indexGraphEntity != null) {
						if(node.getDependencyLevel() <= indexGraphEntity.getDependencyLevel()) {
							node.incrementDependencyLevel();
							dependencyLevel = Math.max(node.getDependencyLevel(), dependencyLevel);
							somethingChanged = true;
						}
					}
				}
				if(node instanceof RetypedNode) {
					if(node.getDependencyLevel() <= ((RetypedNode)node).getCombinedDependencyLevel()) {
						node.incrementDependencyLevel();
						dependencyLevel = Math.max(node.getDependencyLevel(), dependencyLevel);
						somethingChanged = true;
					}
				}
			}
			for(Edge edge : left.getEdges()) {
				if(edge.storageAccessIndex != null && edge.storageAccessIndex.indexGraphEntity != null) {
					GraphEntity indexGraphEntity = edge.storageAccessIndex.indexGraphEntity;
					if(edge.getDependencyLevel() <= indexGraphEntity.getDependencyLevel()) {
						edge.incrementDependencyLevel();
						dependencyLevel = Math.max(edge.getDependencyLevel(), dependencyLevel);
						somethingChanged = true;
					}
				}
				if(edge.storageAccess != null && edge.storageAccess.storageAttribute != null) {
					Qualification storageAttribute = edge.storageAccess.storageAttribute;
					if(edge.getDependencyLevel() <= ((GraphEntity)storageAttribute.getOwner()).getDependencyLevel()) {
						edge.incrementDependencyLevel();
						dependencyLevel = Math.max(edge.getDependencyLevel(), dependencyLevel);
						somethingChanged = true;
					}
				}
				if(edge.indexAccess != null) {
					NeededEntities needs = new NeededEntities(EnumSet.of(Needs.NODES, Needs.EDGES, Needs.CONTAINER_EXPRS));
					edge.indexAccess.collectNeededEntities(needs);
					GraphEntity indexGraphEntity = getAtMostOneNeededGraphElement(needs, edge);
					if(indexGraphEntity != null) {
						if(edge.getDependencyLevel() <= indexGraphEntity.getDependencyLevel()) {
							edge.incrementDependencyLevel();
							dependencyLevel = Math.max(edge.getDependencyLevel(), dependencyLevel);
							somethingChanged = true;
						}
					}
				}
				if(!edge.multipleIndexAccesses.isEmpty()) {
					NeededEntities needs = new NeededEntities(EnumSet.of(Needs.NODES, Needs.EDGES, Needs.CONTAINER_EXPRS));
					for(IndexAccessOrdering indexAccess : edge.multipleIndexAccesses) {
						indexAccess.collectNeededEntities(needs);
					}
					GraphEntity indexGraphEntity = getAtMostOneNeededGraphElement(needs, edge);
					if(indexGraphEntity != null) {
						if(edge.getDependencyLevel() <= indexGraphEntity.getDependencyLevel()) {
							edge.incrementDependencyLevel();
							dependencyLevel = Math.max(edge.getDependencyLevel(), dependencyLevel);
							somethingChanged = true;
						}
					}
				}
				if(edge.nameMapAccess != null) {
					NeededEntities needs = new NeededEntities(EnumSet.of(Needs.NODES, Needs.EDGES, Needs.CONTAINER_EXPRS));
					edge.nameMapAccess.collectNeededEntities(needs);
					GraphEntity indexGraphEntity = getAtMostOneNeededGraphElement(needs, edge);
					if(indexGraphEntity != null) {
						if(edge.getDependencyLevel() <= indexGraphEntity.getDependencyLevel()) {
							edge.incrementDependencyLevel();
							dependencyLevel = Math.max(edge.getDependencyLevel(), dependencyLevel);
							somethingChanged = true;
						}
					}
				}
				if(edge.uniqueIndexAccess != null) {
					NeededEntities needs = new NeededEntities(EnumSet.of(Needs.NODES, Needs.EDGES, Needs.CONTAINER_EXPRS));
					edge.uniqueIndexAccess.collectNeededEntities(needs);
					GraphEntity indexGraphEntity = getAtMostOneNeededGraphElement(needs, edge);
					if(indexGraphEntity != null) {
						if(edge.getDependencyLevel() <= indexGraphEntity.getDependencyLevel()) {
							edge.incrementDependencyLevel();
							dependencyLevel = Math.max(edge.getDependencyLevel(), dependencyLevel);
							somethingChanged = true;
						}
					}
				}
				if(edge instanceof RetypedEdge) {
					if(edge.getDependencyLevel() <= ((RetypedEdge)edge).oldEdge.getDependencyLevel()) {
						edge.incrementDependencyLevel();
						dependencyLevel = Math.max(edge.getDependencyLevel(), dependencyLevel);
						somethingChanged = true;
					}
				}
			}
			if(dependencyLevel >= MAX_CHAINING_FOR_STORAGE_MAP_ACCESS) {
				error.error(getIdent().getCoords(), "Cycle in match node/edge by storage map access or storage attribute detected.");
				break;
			}
		} while(somethingChanged);

		for(Alternative alternative : pattern.getAlts()) {
			for(Rule altCase : alternative.getAlternativeCases()) {
				altCase.setDependencyLevelOfInterElementDependencies();
			}
		}

		for(Rule iterated : pattern.getIters()) {
			iterated.setDependencyLevelOfInterElementDependencies();
		}
	}

	public GraphEntity getAtMostOneNeededGraphElement(NeededEntities needs, GraphEntity entity)
	{
		HashSet<GraphEntity> neededEntities = new HashSet<GraphEntity>();
		for(Node node : needs.nodes) {
			if(getParameters().indexOf(node) != -1)
				continue;
			if(node.isDefToBeYieldedTo()) {
				error.error(entity.getIdent().getCoords(), "Cannot use a def node (" + node.getIdent() + ")"
						+ " for an index access or name map access of " + entity.getIdent() + ".");
			}
			neededEntities.add(node);
		}
		for(Edge edge : needs.edges) {
			if(getParameters().indexOf(edge) != -1)
				continue;
			if(edge.isDefToBeYieldedTo()) {
				error.error(entity.getIdent().getCoords(), "Cannot use a def edge (" + edge.getIdent() + ")"
						+ " for an index access or name map access of " + entity.getIdent() + ".");
			}
			neededEntities.add(edge);
		}
		if(neededEntities.size() == 1)
			return neededEntities.iterator().next();
		else if(neededEntities.size() > 1) {
			error.error(entity.getIdent().getCoords(), "There are " + neededEntities.size() + " entities needed for an index access or name map access of "
						+ entity.getIdent() + " (only one is allowed).");
		}
		return null;
	}
}
