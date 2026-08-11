/*
 * GrGen: graph rewrite generator tool -- release GrGen.NET 8.1
 * Copyright (C) 2003-2026 Universitaet Karlsruhe, Institut fuer Programmstrukturen und Datenorganisation, LS Goos; and free programmers
 * licensed under LGPL v3, some components/parts use different licenses (see LICENSE.txt included in the packaging of this file)
 * www.grgen.de / www.grgen.net
 */

/// <summary>
/// @author shack, Daniel Grund
/// </summary>

namespace de.unika.ipd.grgen.ir.executable
{

using System;
using System.Collections.Generic;
using System.Diagnostics;

using ContainedInPackage = de.unika.ipd.grgen.ir.ContainedInPackage;
using Entity = de.unika.ipd.grgen.ir.Entity;
using Exec = de.unika.ipd.grgen.ir.Exec;
using Ident = de.unika.ipd.grgen.ir.Ident;
using NeededEntities = de.unika.ipd.grgen.ir.NeededEntities;
using Needs = de.unika.ipd.grgen.ir.NeededEntities.Needs;
using Expression = de.unika.ipd.grgen.ir.expr.Expression;
using GraphEntityExpression = de.unika.ipd.grgen.ir.expr.GraphEntityExpression;
using Qualification = de.unika.ipd.grgen.ir.expr.Qualification;
using Alternative = de.unika.ipd.grgen.ir.pattern.Alternative;
using Edge = de.unika.ipd.grgen.ir.pattern.Edge;
using GraphEntity = de.unika.ipd.grgen.ir.pattern.GraphEntity;
using IndexAccessOrdering = de.unika.ipd.grgen.ir.pattern.IndexAccessOrdering;
using Node = de.unika.ipd.grgen.ir.pattern.Node;
using OrderedReplacement = de.unika.ipd.grgen.ir.pattern.OrderedReplacement;
using OrderedReplacements = de.unika.ipd.grgen.ir.pattern.OrderedReplacements;
using PatternGraphLhs = de.unika.ipd.grgen.ir.pattern.PatternGraphLhs;
using PatternGraphRhs = de.unika.ipd.grgen.ir.pattern.PatternGraphRhs;
using RetypedEdge = de.unika.ipd.grgen.ir.pattern.RetypedEdge;
using RetypedNode = de.unika.ipd.grgen.ir.pattern.RetypedNode;
using SubpatternDependentReplacement = de.unika.ipd.grgen.ir.pattern.SubpatternDependentReplacement;
using SubpatternUsage = de.unika.ipd.grgen.ir.pattern.SubpatternUsage;
using EvalStatements = de.unika.ipd.grgen.ir.stmt.EvalStatements;
using ImperativeStmt = de.unika.ipd.grgen.ir.stmt.ImperativeStmt;
using DefinedMatchType = de.unika.ipd.grgen.ir.type.DefinedMatchType;
using BaseNode = de.unika.ipd.grgen.ast.BaseNode;

/// <summary>
/// A graph rewrite rule or subrule, with none, one, or arbitrary many (not yet) replacements.
/// </summary>
public class Rule : MatchingAction, ContainedInPackage
{
	/// <summary>
	/// Names of the children of this node. </summary>
	private static readonly string[] childrenNames = new string[] { "left", "right", "eval" };

	private string packageContainedIn;

	/// <summary>
	/// The right hand side of the rule. </summary>
	private PatternGraphRhs right;

	/// <summary>
	/// The match classes that get implemented </summary>
	private readonly List<DefinedMatchType> implementedMatchClasses = new List<DefinedMatchType>();

	/// <summary>
	/// The evaluation assignments of this rule (RHS). </summary>
	private readonly List<EvalStatements> evalStatements = new List<EvalStatements>();

	/// <summary>
	/// How often the pattern is to be matched in case this is an iterated. </summary>
	private int minMatches;
	private int maxMatches;

	/// <summary>
	/// Was the replacement code already called by means of an iterated replacement declaration? (in case this is an iterated.) </summary>
	public bool wasReplacementAlreadyCalled;

	/// <summary>
	/// Have deferred execs been added by using this top level rule, so we have to execute the exec queue? </summary>
	public bool mightThereBeDeferredExecs;

	public enum RuleKind
	{
		Rule,
		Test,
		Subpattern,
		AlternativeCase,
		Iterated
	}

	public static string toString(RuleKind ruleKind)
	{
		switch(ruleKind)
		{
		case de.unika.ipd.grgen.ir.executable.Rule.RuleKind.Rule:
			return "rule";
		case de.unika.ipd.grgen.ir.executable.Rule.RuleKind.Test:
			return "test";
		case de.unika.ipd.grgen.ir.executable.Rule.RuleKind.Subpattern:
			return "(sub)pattern";
		case de.unika.ipd.grgen.ir.executable.Rule.RuleKind.AlternativeCase:
			return "alternative case";
		case de.unika.ipd.grgen.ir.executable.Rule.RuleKind.Iterated:
			return "iterated";
		default:
			throw new Exception("Unexpected case");
		}
	}
	public RuleKind ruleKind;

	/// <summary>
	/// Make a new rule. </summary>
	/// <param name="ident"> The identifier with which the rule was declared. </param>
	public Rule(Ident ident, RuleKind ruleKind)
		: base("rule", ident)
	{
		ChildrenNames = childrenNames;
		this.ruleKind = ruleKind;
		this.minMatches = -1;
		this.maxMatches = -1;
		mightThereBeDeferredExecs = false;
	}

	/// <summary>
	/// Make a new iterated rule. </summary>
	/// <param name="ident"> The identifier with which the rule was declared. </param>
	public Rule(Ident ident, int minMatches, int maxMatches)
		: base("rule", ident)
	{
		ChildrenNames = childrenNames;
		this.ruleKind = RuleKind.Iterated;
		this.minMatches = minMatches;
		this.maxMatches = maxMatches;
		mightThereBeDeferredExecs = false;
	}

	/// <param name="pattern"> The left side graph of the rule. </param>
	/// <param name="right"> The right side graph of the rule. </param>
	public virtual void Initialize(PatternGraphLhs pattern, PatternGraphRhs right)
	{
		base.Pattern = pattern;
		this.right = right;
		if(right == null)
			pattern.NameSuffix = "test";
		else
		{
			pattern.Name = "L";
			right.Name = "R";
		}
	}

	public virtual string PackageContainedIn
	{
		get
		{
			return packageContainedIn;
		}
		set
		{
			this.packageContainedIn = value;
		}
	}


	public virtual bool IsSubpattern()
	{
		return ruleKind == RuleKind.Subpattern;
	}

	/// <returns> A collection containing all eval assignments of this rule. </returns>
	public virtual ICollection<EvalStatements> Evals
	{
		get
		{
			return evalStatements.AsReadOnly();
		}
	}

	/// <summary>
	/// Add an assignment to the list of evaluations. </summary>
	public virtual void AddEval(EvalStatements a)
	{
		evalStatements.Add(a);
	}

	///  <returns> A set with nodes, that occur on the left _and_ on the right side of the rule.
	///  		The set also contains retyped nodes. </returns>
	public virtual ICollection<Node> CommonNodes
	{
		get
		{
			ICollection<Node> common = new HashSet<Node>(pattern.Nodes);
			common.RetainAll(right.Nodes);
			return common;
		}
	}

	/// <returns> A set with edges, that occur on the left _and_ on the right side of the rule.
	///         The set also contains all retyped edges. </returns>
	public virtual ICollection<Edge> CommonEdges
	{
		get
		{
			ICollection<Edge> common = new HashSet<Edge>(pattern.Edges);
			common.RetainAll(right.Edges);
			return common;
		}
	}

	/// <returns> A set with subpatterns, that occur on the left _and_ on the right side of the rule. </returns>
	public virtual ICollection<SubpatternUsage> CommonSubpatternUsages
	{
		get
		{
			ICollection<SubpatternUsage> common = new HashSet<SubpatternUsage>(pattern.SubpatternUsages);
			common.RetainAll(right.SubpatternUsages);
			return common;
		}
	}

	/// <returns> The left hand side graph. </returns>
	public virtual PatternGraphLhs Left
	{
		get
		{
			return pattern;
		}
	}

	/// <returns> The right hand side graph. </returns>
	public virtual PatternGraphRhs Right
	{
		get
		{
			return right;
		}
	}

	public virtual ICollection<DefinedMatchType> ImplementedMatchClasses
	{
		get
		{
			return implementedMatchClasses.AsReadOnly();
		}
	}

	public virtual void AddImplementedMatchClass(DefinedMatchType implementedMatchClass)
	{
		implementedMatchClasses.Add(implementedMatchClass);
	}

	/// <returns> Minimum number of how often the pattern must get matched. </returns>
	public virtual int MinMatches
	{
		get
		{
			return minMatches;
		}
	}

	/// <returns> Maximum number of how often the pattern must get matched. 0 means unlimited </returns>
	public virtual int MaxMatches
	{
		get
		{
			return maxMatches;
		}
	}

	public virtual void CheckForRhsElementsUsedOnLhs()
	{
		PatternGraphLhs left = Left;
		foreach(Node node in left.Nodes)
		{
			if((node.context & BaseNode.CONTEXT_LHS_OR_RHS) == BaseNode.CONTEXT_RHS)
				error.Error(node.Ident.GetCoords(), "Nodes declared in the rewrite part cannot be accessed in the pattern part (as is the case for " + node.Ident + ").");
		}
		foreach(Edge edge in left.Edges)
		{
			if((edge.context & BaseNode.CONTEXT_LHS_OR_RHS) == BaseNode.CONTEXT_RHS)
				error.Error(edge.Ident.GetCoords(), "Edges declared in the rewrite part cannot be accessed in the pattern part (as is the case for " + edge.Ident + ").");
		}
	}

	public virtual void ComputeUsageDependencies(Dictionary<Rule, HashSet<Rule>> subpatternsDefToUse, Rule subpattern)
	{
		foreach(SubpatternUsage sub in pattern.SubpatternUsages)
		{
			HashSet<Rule> uses = subpatternsDefToUse[sub.subpatternAction];
			uses.Add(subpattern);
		}

		foreach(Alternative alternative in pattern.Alts)
		{
			foreach(Rule altCase in alternative.AlternativeCases)
				altCase.ComputeUsageDependencies(subpatternsDefToUse, subpattern);
		}

		foreach(Rule iterated in pattern.Iters)
			iterated.ComputeUsageDependencies(subpatternsDefToUse, subpattern);
	}

	public virtual bool CheckForMultipleDeletesOrRetypes(Dictionary<Entity, Rule> entitiesToTheirDeletingOrRetypingPattern,
			Dictionary<Rule, Dictionary<Entity, Rule>> subpatternsToParametersToTheirDeletingOrRetypingPattern)
	{
		if(right == null)
			return false;

		foreach(Node node in pattern.Nodes)
		{
			foreach(Node homNode in pattern.GetHomomorphic(node))
			{
				if(!right.HasNode(homNode))
				{
					if(entitiesToTheirDeletingOrRetypingPattern.ContainsKey(node)
							&& entitiesToTheirDeletingOrRetypingPattern[node] != this)
					{
						ReportMultipleDeleteOrRetype(node, entitiesToTheirDeletingOrRetypingPattern[node], this);
					}
					else
						entitiesToTheirDeletingOrRetypingPattern[node] = this;
				}
				if(homNode.ChangesType(right))
				{
					if(entitiesToTheirDeletingOrRetypingPattern.ContainsKey(node)
							&& entitiesToTheirDeletingOrRetypingPattern[node] != this)
					{
						ReportMultipleDeleteOrRetype(node, entitiesToTheirDeletingOrRetypingPattern[node], this);
					}
					else
						entitiesToTheirDeletingOrRetypingPattern[node] = this;
				}
			}
		}
		foreach(Edge edge in pattern.Edges)
		{
			foreach(Edge homEdge in pattern.GetHomomorphic(edge))
			{
				if(!right.HasEdge(homEdge))
				{
					if(entitiesToTheirDeletingOrRetypingPattern.ContainsKey(edge)
							&& entitiesToTheirDeletingOrRetypingPattern[edge] != this)
					{
						ReportMultipleDeleteOrRetype(edge, entitiesToTheirDeletingOrRetypingPattern[edge], this);
					}
					else
						entitiesToTheirDeletingOrRetypingPattern[edge] = this;
				}
				if(homEdge.ChangesType(right))
				{
					if(entitiesToTheirDeletingOrRetypingPattern.ContainsKey(edge)
							&& entitiesToTheirDeletingOrRetypingPattern[edge] != this)
					{
						ReportMultipleDeleteOrRetype(edge, entitiesToTheirDeletingOrRetypingPattern[edge], this);
					}
					else
						entitiesToTheirDeletingOrRetypingPattern[edge] = this;
				}
			}
		}

		foreach(SubpatternUsage sub in pattern.SubpatternUsages)
		{
			bool isDependentReplacementUsed = false;
			foreach(OrderedReplacements ors in right.OrderedReplacements)
			{
				foreach(OrderedReplacement or in ors.orderedReplacements)
				{
					if(!(or is SubpatternDependentReplacement))
						continue;
					if(((SubpatternDependentReplacement)or).SubpatternUsage == sub)
						isDependentReplacementUsed = true;
				}
			}
			if(!isDependentReplacementUsed)
				continue;

			IList<Entity> parameters = sub.subpatternAction.Parameters;
			IEnumerator<Entity> parametersIt = parameters.GetEnumerator();
			IList<Expression> arguments = sub.subpatternConnections;
			IEnumerator<Expression> argumentsIt = arguments.GetEnumerator();
			while(argumentsIt.MoveNext())
			{
// JAVA TO C# CONVERTER TASK: Java iterators are only converted within the context of 'while' and 'for' loops:
				Debug.Assert(parametersIt.HasNext());
				Expression argument = argumentsIt.Current;
// JAVA TO C# CONVERTER TASK: Java iterators are only converted within the context of 'while' and 'for' loops:
				Entity parameter = parametersIt.Next();
				if(argument is GraphEntityExpression)
				{
					GraphEntity argumentEntity = ((GraphEntityExpression)argument).GraphEntity;
					Dictionary<Entity, Rule> parametersToTheirDeletingOrRetypingPattern =
							subpatternsToParametersToTheirDeletingOrRetypingPattern[sub.subpatternAction];
					Rule deletingOrRetypingPattern = parametersToTheirDeletingOrRetypingPattern[parameter];
					if(deletingOrRetypingPattern != null)
					{
						if(entitiesToTheirDeletingOrRetypingPattern.ContainsKey(argumentEntity))
						{
							ReportMultipleDeleteOrRetype(argumentEntity,
									entitiesToTheirDeletingOrRetypingPattern[argumentEntity],
									deletingOrRetypingPattern);
						}
						else
							entitiesToTheirDeletingOrRetypingPattern[argumentEntity] = deletingOrRetypingPattern;
					}
				}
			}
		}

		foreach(Alternative alternative in pattern.Alts)
		{
			List<Dictionary<Entity, Rule>> entitiesToTheirDeletingOrRetypingPatternOfAlternativCases = new List<Dictionary<Entity, Rule>>();
			foreach(Rule altCase in alternative.AlternativeCases)
			{
				Dictionary<Entity, Rule> entitiesToTheirDeletingOrRetypingPatternClone =
						new Dictionary<Entity, Rule>(entitiesToTheirDeletingOrRetypingPattern);
				altCase.CheckForMultipleDeletesOrRetypes(entitiesToTheirDeletingOrRetypingPatternClone,
						subpatternsToParametersToTheirDeletingOrRetypingPattern);
				entitiesToTheirDeletingOrRetypingPatternOfAlternativCases.Add(entitiesToTheirDeletingOrRetypingPatternClone);
			}
			foreach(Dictionary<Entity, Rule> entitiesToTheirDeletingOrRetypingPatternOfAlternativCase in entitiesToTheirDeletingOrRetypingPatternOfAlternativCases)
			{
				foreach(Entity entityOfAlternativeCase in entitiesToTheirDeletingOrRetypingPatternOfAlternativCase.Keys)
				{
					Rule deletingOrRetypingPatternOld = entitiesToTheirDeletingOrRetypingPattern[entityOfAlternativeCase];
					Rule deletingOrRetypingPatternNew = entitiesToTheirDeletingOrRetypingPatternOfAlternativCase[entityOfAlternativeCase];
					if(deletingOrRetypingPatternOld == null && deletingOrRetypingPatternNew != null)
						entitiesToTheirDeletingOrRetypingPattern[entityOfAlternativeCase] = deletingOrRetypingPatternNew;
				}
			}
		}

		foreach(Rule iterated in pattern.Iters)
		{
			iterated.CheckForMultipleDeletesOrRetypes(entitiesToTheirDeletingOrRetypingPattern,
					subpatternsToParametersToTheirDeletingOrRetypingPattern);
		}

		bool changed = false;
		if(subpatternsToParametersToTheirDeletingOrRetypingPattern.ContainsKey(this))
		{
			Dictionary<Entity, Rule> parametersToTheirDeletingOrRetypingPattern =
					subpatternsToParametersToTheirDeletingOrRetypingPattern[this];
			foreach(Entity parameter in parametersToTheirDeletingOrRetypingPattern.Keys)
			{
				Rule deletingOrRetypingPatternOld = parametersToTheirDeletingOrRetypingPattern[parameter];
				Rule deletingOrRetypingPatternNew = entitiesToTheirDeletingOrRetypingPattern[parameter];
				if(deletingOrRetypingPatternOld == null && deletingOrRetypingPatternNew != null)
				{
					parametersToTheirDeletingOrRetypingPattern[parameter] = deletingOrRetypingPatternNew;
					changed = true;
				}
			}
		}
		return changed;
	}

	internal static void ReportMultipleDeleteOrRetype(Entity entity, Rule first, Rule second)
	{
		error.Error(entity.Ident.GetCoords(), "The " + entity.Kind + " " + entity.Ident + " or a hom " + entity.Kind
				+ " may get deleted or retyped in " + toString(first.ruleKind) + " " + first.Ident + " [declared at " + first.Ident.GetCoords() + "]"
				+ " and in " + toString(second.ruleKind) + " " + second.Ident + " [declared at " + second.Ident.GetCoords() + "]"
				+ " (only one such place is allowed, determinable at compile time).");
	}

	public virtual void CheckForMultipleRetypesLocal()
	{
		if(right == null)
			return;

		foreach(Node node in pattern.Nodes)
		{
			foreach(Node homNode in pattern.GetHomomorphic(node))
			{
				if(node == homNode)
					continue;
				if(node.ChangesType(right) && homNode.ChangesType(right))
					ReportMultipleRetype(node, homNode);
			}
		}
		foreach(Edge edge in pattern.Edges)
		{
			foreach(Edge homEdge in pattern.GetHomomorphic(edge))
			{
				if(edge == homEdge)
					continue;
				if(edge.ChangesType(right) && homEdge.ChangesType(right))
					ReportMultipleRetype(edge, homEdge);
			}
		}

		foreach(Alternative alternative in pattern.Alts)
		{
			foreach(Rule altCase in alternative.AlternativeCases)
				altCase.CheckForMultipleRetypesLocal();
		}

		foreach(Rule iterated in pattern.Iters)
			iterated.CheckForMultipleRetypesLocal();
	}

	internal static void ReportMultipleRetype(Entity entity, Entity homEntity)
	{
		error.Error(entity.Ident.GetCoords(), "The " + entity.Kind + " " + entity.Ident
				+ " and the hom " + entity.Kind + " " + homEntity.Ident + homEntity.Ident.GetCoords().GetDeclarationCoords(false)
				+ " are both retyped, so a homomorphically matched graph element may get retyped multiple times.");
	}

	public virtual bool IsUsingNonDirectExec(bool isTopLevelRule)
	{
		if(right == null)
			return false;

		if(!isTopLevelRule)
		{
			foreach(ImperativeStmt @is in right.ImperativeStmts)
			{
				if(@is is Exec)
					return true;
			}
		}

		foreach(Alternative alternative in pattern.Alts)
		{
			foreach(Rule altCase in alternative.AlternativeCases)
			{
				if(altCase.IsUsingNonDirectExec(false))
					return true;
			}
		}

		foreach(Rule iterated in pattern.Iters)
		{
			if(iterated.IsUsingNonDirectExec(false))
				return true;
		}

		return false;
	}

	public virtual void SetDependencyLevelOfInterElementDependencies()
	{
		PatternGraphLhs left = Left;
		const int MAX_CHAINING_FOR_STORAGE_MAP_ACCESS = 1000;
		int dependencyLevel = 0;
		bool somethingChanged;
		do
		{
			somethingChanged = false;

			foreach(Node node in left.Nodes)
			{
				if(node.storageAccessIndex != null && node.storageAccessIndex.indexGraphEntity != null)
				{
					GraphEntity indexGraphEntity = node.storageAccessIndex.indexGraphEntity;
					if(node.DependencyLevel <= indexGraphEntity.DependencyLevel)
					{
						node.IncrementDependencyLevel();
						dependencyLevel = Math.Max(node.DependencyLevel, dependencyLevel);
						somethingChanged = true;
					}
				}
				if(node.storageAccess != null && node.storageAccess.storageAttribute != null)
				{
					Qualification storageAttribute = node.storageAccess.storageAttribute;
					if(node.DependencyLevel <= ((GraphEntity)storageAttribute.Owner).DependencyLevel)
					{
						node.IncrementDependencyLevel();
						dependencyLevel = Math.Max(node.DependencyLevel, dependencyLevel);
						somethingChanged = true;
					}
				}
				if(node.indexAccess != null)
				{
					NeededEntities needs = new NeededEntities(EnumSet.Of(NeededEntities.Needs.NODES, NeededEntities.Needs.EDGES, NeededEntities.Needs.CONTAINER_EXPRS));
					node.indexAccess.CollectNeededEntities(needs);
					GraphEntity indexGraphEntity = GetAtMostOneNeededGraphElement(needs, node);
					if(indexGraphEntity != null)
					{
						if(node.DependencyLevel <= indexGraphEntity.DependencyLevel)
						{
							node.IncrementDependencyLevel();
							dependencyLevel = Math.Max(node.DependencyLevel, dependencyLevel);
							somethingChanged = true;
						}
					}
				}
				if(node.multipleIndexAccesses.Count > 0)
				{
					NeededEntities needs = new NeededEntities(EnumSet.Of(NeededEntities.Needs.NODES, NeededEntities.Needs.EDGES, NeededEntities.Needs.CONTAINER_EXPRS));
					foreach(IndexAccessOrdering indexAccess in node.multipleIndexAccesses)
						indexAccess.CollectNeededEntities(needs);
					GraphEntity indexGraphEntity = GetAtMostOneNeededGraphElement(needs, node);
					if(indexGraphEntity != null)
					{
						if(node.DependencyLevel <= indexGraphEntity.DependencyLevel)
						{
							node.IncrementDependencyLevel();
							dependencyLevel = Math.Max(node.DependencyLevel, dependencyLevel);
							somethingChanged = true;
						}
					}
				}
				if(node.nameMapAccess != null)
				{
					NeededEntities needs = new NeededEntities(EnumSet.Of(NeededEntities.Needs.NODES, NeededEntities.Needs.EDGES, NeededEntities.Needs.CONTAINER_EXPRS));
					node.nameMapAccess.CollectNeededEntities(needs);
					GraphEntity indexGraphEntity = GetAtMostOneNeededGraphElement(needs, node);
					if(indexGraphEntity != null)
					{
						if(node.DependencyLevel <= indexGraphEntity.DependencyLevel)
						{
							node.IncrementDependencyLevel();
							dependencyLevel = Math.Max(node.DependencyLevel, dependencyLevel);
							somethingChanged = true;
						}
					}
				}
				if(node.uniqueIndexAccess != null)
				{
					NeededEntities needs = new NeededEntities(EnumSet.Of(NeededEntities.Needs.NODES, NeededEntities.Needs.EDGES, NeededEntities.Needs.CONTAINER_EXPRS));
					node.uniqueIndexAccess.CollectNeededEntities(needs);
					GraphEntity indexGraphEntity = GetAtMostOneNeededGraphElement(needs, node);
					if(indexGraphEntity != null)
					{
						if(node.DependencyLevel <= indexGraphEntity.DependencyLevel)
						{
							node.IncrementDependencyLevel();
							dependencyLevel = Math.Max(node.DependencyLevel, dependencyLevel);
							somethingChanged = true;
						}
					}
				}
				if(node is RetypedNode)
				{
					if(node.DependencyLevel <= ((RetypedNode)node).CombinedDependencyLevel)
					{
						node.IncrementDependencyLevel();
						dependencyLevel = Math.Max(node.DependencyLevel, dependencyLevel);
						somethingChanged = true;
					}
				}
			}
			foreach(Edge edge in left.Edges)
			{
				if(edge.storageAccessIndex != null && edge.storageAccessIndex.indexGraphEntity != null)
				{
					GraphEntity indexGraphEntity = edge.storageAccessIndex.indexGraphEntity;
					if(edge.DependencyLevel <= indexGraphEntity.DependencyLevel)
					{
						edge.IncrementDependencyLevel();
						dependencyLevel = Math.Max(edge.DependencyLevel, dependencyLevel);
						somethingChanged = true;
					}
				}
				if(edge.storageAccess != null && edge.storageAccess.storageAttribute != null)
				{
					Qualification storageAttribute = edge.storageAccess.storageAttribute;
					if(edge.DependencyLevel <= ((GraphEntity)storageAttribute.Owner).DependencyLevel)
					{
						edge.IncrementDependencyLevel();
						dependencyLevel = Math.Max(edge.DependencyLevel, dependencyLevel);
						somethingChanged = true;
					}
				}
				if(edge.indexAccess != null)
				{
					NeededEntities needs = new NeededEntities(EnumSet.Of(NeededEntities.Needs.NODES, NeededEntities.Needs.EDGES, NeededEntities.Needs.CONTAINER_EXPRS));
					edge.indexAccess.CollectNeededEntities(needs);
					GraphEntity indexGraphEntity = GetAtMostOneNeededGraphElement(needs, edge);
					if(indexGraphEntity != null)
					{
						if(edge.DependencyLevel <= indexGraphEntity.DependencyLevel)
						{
							edge.IncrementDependencyLevel();
							dependencyLevel = Math.Max(edge.DependencyLevel, dependencyLevel);
							somethingChanged = true;
						}
					}
				}
				if(edge.multipleIndexAccesses.Count > 0)
				{
					NeededEntities needs = new NeededEntities(EnumSet.Of(NeededEntities.Needs.NODES, NeededEntities.Needs.EDGES, NeededEntities.Needs.CONTAINER_EXPRS));
					foreach(IndexAccessOrdering indexAccess in edge.multipleIndexAccesses)
						indexAccess.CollectNeededEntities(needs);
					GraphEntity indexGraphEntity = GetAtMostOneNeededGraphElement(needs, edge);
					if(indexGraphEntity != null)
					{
						if(edge.DependencyLevel <= indexGraphEntity.DependencyLevel)
						{
							edge.IncrementDependencyLevel();
							dependencyLevel = Math.Max(edge.DependencyLevel, dependencyLevel);
							somethingChanged = true;
						}
					}
				}
				if(edge.nameMapAccess != null)
				{
					NeededEntities needs = new NeededEntities(EnumSet.Of(NeededEntities.Needs.NODES, NeededEntities.Needs.EDGES, NeededEntities.Needs.CONTAINER_EXPRS));
					edge.nameMapAccess.CollectNeededEntities(needs);
					GraphEntity indexGraphEntity = GetAtMostOneNeededGraphElement(needs, edge);
					if(indexGraphEntity != null)
					{
						if(edge.DependencyLevel <= indexGraphEntity.DependencyLevel)
						{
							edge.IncrementDependencyLevel();
							dependencyLevel = Math.Max(edge.DependencyLevel, dependencyLevel);
							somethingChanged = true;
						}
					}
				}
				if(edge.uniqueIndexAccess != null)
				{
					NeededEntities needs = new NeededEntities(EnumSet.Of(NeededEntities.Needs.NODES, NeededEntities.Needs.EDGES, NeededEntities.Needs.CONTAINER_EXPRS));
					edge.uniqueIndexAccess.CollectNeededEntities(needs);
					GraphEntity indexGraphEntity = GetAtMostOneNeededGraphElement(needs, edge);
					if(indexGraphEntity != null)
					{
						if(edge.DependencyLevel <= indexGraphEntity.DependencyLevel)
						{
							edge.IncrementDependencyLevel();
							dependencyLevel = Math.Max(edge.DependencyLevel, dependencyLevel);
							somethingChanged = true;
						}
					}
				}
				if(edge is RetypedEdge)
				{
					if(edge.DependencyLevel <= ((RetypedEdge)edge).oldEdge.GetDependencyLevel())
					{
						edge.IncrementDependencyLevel();
						dependencyLevel = Math.Max(edge.DependencyLevel, dependencyLevel);
						somethingChanged = true;
					}
				}
			}
			if(dependencyLevel >= MAX_CHAINING_FOR_STORAGE_MAP_ACCESS)
			{
				error.Error(Ident.GetCoords(), "Cycle in match node/edge by storage map access or storage attribute detected.");
				break;
			}
		} while(somethingChanged);

		foreach(Alternative alternative in pattern.Alts)
		{
			foreach(Rule altCase in alternative.AlternativeCases)
				altCase.SetDependencyLevelOfInterElementDependencies();
		}

		foreach(Rule iterated in pattern.Iters)
			iterated.SetDependencyLevelOfInterElementDependencies();
	}

	public virtual GraphEntity GetAtMostOneNeededGraphElement(NeededEntities needs, GraphEntity entity)
	{
		HashSet<GraphEntity> neededEntities = new HashSet<GraphEntity>();
		foreach(Node node in needs.nodes)
		{
			if(Parameters.IndexOf(node) != -1)
				continue;
			if(node.IsDefToBeYieldedTo())
			{
				error.Error(entity.Ident.GetCoords(), "Cannot use a def node (" + node.Ident + ")" 
						+ " for an index access or name map access of " + entity.Ident + ".");
			}
			neededEntities.Add(node);
		}
		foreach(Edge edge in needs.edges)
		{
			if(Parameters.IndexOf(edge) != -1)
				continue;
			if(edge.IsDefToBeYieldedTo())
			{
				error.Error(entity.Ident.GetCoords(), "Cannot use a def edge (" + edge.Ident + ")"
						+ " for an index access or name map access of " + entity.Ident + ".");
			}
			neededEntities.Add(edge);
		}
		if(neededEntities.Count == 1)
			return neededEntities.GetEnumerator().Next();
		else if(neededEntities.Count > 1)
		{
			error.Error(entity.Ident.GetCoords(), "There are " + neededEntities.Count + " entities specified in an index access or name map access of "
						+ entity.Ident + " (only one is allowed).");
		}
		return null;
	}
}

}
