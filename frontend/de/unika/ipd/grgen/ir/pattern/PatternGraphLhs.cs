/*
 * GrGen: graph rewrite generator tool -- release GrGen.NET 8.1
 * Copyright (C) 2003-2026 Universitaet Karlsruhe, Institut fuer Programmstrukturen und Datenorganisation, LS Goos; and free programmers
 * licensed under LGPL v3, some components/parts use different licenses (see LICENSE.txt included in the packaging of this file)
 * www.grgen.de / www.grgen.net
 */

/// <summary>
/// PatternGraph.java
/// 
/// @author Sebastian Hack
/// </summary>

namespace de.unika.ipd.grgen.ir.pattern
{

	using System.Collections.Generic;

	using Entity = de.unika.ipd.grgen.ir.Entity;
	using NeededEntities = de.unika.ipd.grgen.ir.NeededEntities;
	using Needs = de.unika.ipd.grgen.ir.NeededEntities.Needs;
	using Rule = de.unika.ipd.grgen.ir.executable.Rule;
	using Constant = de.unika.ipd.grgen.ir.expr.Constant;
	using Expression = de.unika.ipd.grgen.ir.expr.Expression;
	using EvalStatement = de.unika.ipd.grgen.ir.stmt.EvalStatement;
	using EvalStatements = de.unika.ipd.grgen.ir.stmt.EvalStatements;
	using BaseNode = de.unika.ipd.grgen.ast.BaseNode; // for the context constants
	using PatternGraphLhsNode = de.unika.ipd.grgen.ast.pattern.PatternGraphLhsNode;

	/// <summary>
	/// A pattern graph lhs is a graph pattern as it occurs on the left hand side of rules.
	/// It includes nested alternative-case and iterated rules, as well as nested patterns (negative and independent).
	/// It extends the pattern graph base class, additionally offering conditions that restrict the set of possible matches, 
	/// lhs yield statements, homomorphy handling and further things.
	/// </summary>
	public class PatternGraphLhs : PatternGraphBase
	{
		private void InitializeInstanceFields()
		{
			mayPatternBeEmptyComputationState = PATTERN_NOT_YET_VISITED;
		}

		/// <summary>
		/// The alternative statements of the pattern graph </summary>
		private readonly List<Alternative> alts = new List<Alternative>();

		/// <summary>
		/// The iterated statements of the pattern graph </summary>
		private readonly List<Rule> iters = new List<Rule>();

		/// <summary>
		/// The negative patterns(NAC) of the rule. </summary>
		private readonly List<PatternGraphLhs> negs = new List<PatternGraphLhs>();

		/// <summary>
		/// The independent patterns(PAC) of the rule. </summary>
		private readonly List<PatternGraphLhs> idpts = new List<PatternGraphLhs>();

		/// <summary>
		/// A list of all condition expressions. </summary>
		private readonly List<Expression> conds = new List<Expression>();

		/// <summary>
		/// A list of all yield assignments. </summary>
		private readonly List<EvalStatements> yields = new List<EvalStatements>();

		/// <summary>
		/// A list of all potentially homomorphic node sets. </summary>
		private readonly IList<ICollection<Node>> homNodesLists = new List<ICollection<Node>>();

		/// <summary>
		/// A list of all potentially homomorphic edge sets. </summary>
		private readonly IList<ICollection<Edge>> homEdgesLists = new List<ICollection<Edge>>();

		/// <summary>
		/// A map of nodes which will be matched homomorphically to any other node
		///  to the isomorphy exceptions, requested by independent(node); 
		/// </summary>
		private readonly Dictionary<Node, HashSet<Node>> totallyHomNodes = new Dictionary<Node, HashSet<Node>>();

		/// <summary>
		/// A map of edges which will be matched homomorphically to any other edge
		///  to the isomorphy exceptions, requested by independent(edge); 
		/// </summary>
		private readonly Dictionary<Edge, HashSet<Edge>> totallyHomEdges = new Dictionary<Edge, HashSet<Edge>>();

		/// <summary>
		/// modifiers of pattern as defined in PatternGraphNode, only pattern locked, pattern path locked relevant </summary>
		internal int modifiers;

		internal readonly int PATTERN_NOT_YET_VISITED = 0;
		internal readonly int PATTERN_MAYBE_EMPTY = 1;
		internal readonly int PATTERN_NOT_EMPTY = 2;
		internal int mayPatternBeEmptyComputationState;

		// if this pattern graph is a negative or independent nested inside an iterated
		// it might break the iterated instead of only the current iterated case, if specified
		private bool iterationBreaking = false;


		/// <summary>
		/// Make a new pattern graph. </summary>
		public PatternGraphLhs(string nameOfGraph, int modifiers)
			: base(nameOfGraph)
		{
			InitializeInstanceFields();
			this.modifiers = modifiers;
		}

		public virtual void AddAlternative(Alternative alternative)
		{
			alts.Add(alternative);
		}

		public virtual ICollection<Alternative> Alts
		{
			get
			{
				return alts.AsReadOnly();
			}
		}

		public virtual void AddIterated(Rule iter)
		{
			iters.Add(iter);
		}

		public virtual ICollection<Rule> Iters
		{
			get
			{
				return iters.AsReadOnly();
			}
		}

		public virtual void AddNegGraph(PatternGraphLhs neg)
		{
			int patternNameNumber = negs.Count;
			neg.Name = "N" + patternNameNumber;
			negs.Add(neg);
		}

		/// <returns> The negative graphs of the rule. </returns>
		public virtual ICollection<PatternGraphLhs> Negs
		{
			get
			{
				return negs.AsReadOnly();
			}
		}

		public virtual void AddIdptGraph(PatternGraphLhs idpt)
		{
			int patternNameNumber = idpts.Count;
			idpt.Name = "I" + patternNameNumber;
			idpts.Add(idpt);
		}

		/// <returns> The independent graphs of the rule. </returns>
		public virtual ICollection<PatternGraphLhs> Idpts
		{
			get
			{
				return idpts.AsReadOnly();
			}
		}

		/// <summary>
		/// Add a condition given by it's expression expr to the graph. </summary>
		public virtual void AddCondition(Expression expr)
		{
			conds.Add(expr);
		}

		/// <summary>
		/// Add an assignment to the list of evaluations. </summary>
		public virtual void AddYield(EvalStatements stmts)
		{
			yields.Add(stmts);
		}

		/// <summary>
		/// Add a potentially homomorphic set to the graph. </summary>
		public virtual void AddHomomorphicNodes(ICollection<Node> hom)
		{
			homNodesLists.Add(hom);
		}

		/// <summary>
		/// Add a potentially homomorphic set to the graph. </summary>
		public virtual void AddHomomorphicEdges(ICollection<Edge> hom)
		{
			homEdgesLists.Add(hom);
		}

		public virtual void AddTotallyHomomorphic(Node node, HashSet<Node> isoNodes)
		{
			totallyHomNodes[node] = isoNodes;
		}

		public virtual void AddTotallyHomomorphic(Edge edge, HashSet<Edge> isoEdges)
		{
			totallyHomEdges[edge] = isoEdges;
		}

		public virtual bool IterationBreaking
		{
			set
			{
				iterationBreaking = value;
			}
		}

		public virtual bool IsIterationBreaking()
		{
			return iterationBreaking;
		}

		/// <summary>
		/// Get a collection with all conditions in this graph. </summary>
		public virtual ICollection<Expression> Conditions
		{
			get
			{
				return conds.AsReadOnly();
			}
		}

		/// <returns> A collection containing all yield assignments of this graph. </returns>
		public virtual ICollection<EvalStatements> Yields
		{
			get
			{
				return yields.AsReadOnly();
			}
		}

		/// <summary>
		/// Get all potentially homomorphic sets in this graph. </summary>
		public virtual ICollection<ICollection<GraphEntity>> Homomorphic
		{
			get
			{
				ISet<ICollection<GraphEntity>> homs = new LinkedHashSet<ICollection<GraphEntity>>();
				foreach(ICollection<Edge> edges in homEdgesLists)
					homs.Add(GetGraphEntities(edges));
				foreach(ICollection<Node> nodes in homNodesLists)
					homs.Add(GetGraphEntities(nodes));
				return Collections.UnmodifiableSet(homs);
			}
		}

		private static ICollection<GraphEntity> GetGraphEntities<T1>(ICollection<T1> graphEntities) where T1 : GraphEntity
		{
			return new HashSet<GraphEntity>(graphEntities);
		}

		public virtual ICollection<Node> GetHomomorphic(Node node)
		{
			List<Node> homNodesOfNode = new List<Node>();

			foreach(ICollection<Node> homNodes in homNodesLists)
			{
				if(homNodes.Contains(node))
					homNodesOfNode.AddRange(homNodes);
			}
			homNodesOfNode.Add(node);

			return homNodesOfNode.AsReadOnly();
		}

		public virtual ICollection<Edge> GetHomomorphic(Edge edge)
		{
			List<Edge> homEdgesOfEdge = new List<Edge>();

			foreach(ICollection<Edge> homEdges in homEdgesLists)
			{
				if(homEdges.Contains(edge))
					homEdgesOfEdge.AddRange(homEdges);
			}
			homEdgesOfEdge.Add(edge);

			return homEdgesOfEdge.AsReadOnly();
		}

		public virtual bool IsHomomorphic(Node node1, Node node2)
		{
			if(IsTotallyHomomorphic(node1, node2))
				return true;
			return homToAllNodes.Contains(node1) || homToAllNodes.Contains(node2)
					|| GetHomomorphic(node1).Contains(node2);
		}

		public virtual bool IsHomomorphic(Edge edge1, Edge edge2)
		{
			if(IsTotallyHomomorphic(edge1, edge2))
				return true;
			return homToAllEdges.Contains(edge1) || homToAllEdges.Contains(edge2)
					|| GetHomomorphic(edge1).Contains(edge2);
		}

		public virtual bool IsHomomorphicGlobal(Dictionary<Entity, string> alreadyDefinedEntityToName, Node node1, Node node2)
		{
			if(IsTotallyHomomorphic(node1, node2))
				return true;
			if(!GetHomomorphic(node1).Contains(node2))
				return false;
			return alreadyDefinedEntityToName.ContainsKey(node1) != alreadyDefinedEntityToName.ContainsKey(node2);
		}

		public virtual bool IsHomomorphicGlobal(Dictionary<Entity, string> alreadyDefinedEntityToName, Edge edge1, Edge edge2)
		{
			if(IsTotallyHomomorphic(edge1, edge2))
				return true;
			if(!GetHomomorphic(edge1).Contains(edge2))
				return false;
			return alreadyDefinedEntityToName.ContainsKey(edge1) != alreadyDefinedEntityToName.ContainsKey(edge2);
		}

		public virtual bool IsTotallyHomomorphic(Node node1, Node node2)
		{
			if(IsTotallyHomomorphic(node1))
			{
				if(totallyHomNodes[node1].Contains(node2))
					return false;
			}
			if(IsTotallyHomomorphic(node2))
			{
				if(totallyHomNodes[node2].Contains(node1))
					return false;
			}
			if(IsTotallyHomomorphic(node1) || IsTotallyHomomorphic(node2))
				return true;
			return false;
		}

		public virtual bool IsTotallyHomomorphic(Edge edge1, Edge edge2)
		{
			if(IsTotallyHomomorphic(edge1))
			{
				if(totallyHomEdges[edge1].Contains(edge2))
					return false;
			}
			if(IsTotallyHomomorphic(edge2))
			{
				if(totallyHomEdges[edge2].Contains(edge1))
					return false;
			}
			if(IsTotallyHomomorphic(edge1) || IsTotallyHomomorphic(edge2))
				return true;
			return false;
		}

		public virtual bool IsTotallyHomomorphic(Node node)
		{
			if(totallyHomNodes.ContainsKey(node))
				return true;
			else
				return false;
		}

		public virtual bool IsTotallyHomomorphic(Edge edge)
		{
			if(totallyHomEdges.ContainsKey(edge))
				return true;
			else
				return false;
		}

		public virtual bool IsPatternpathLocked()
		{
			return (modifiers & PatternGraphLhsNode.MOD_PATTERNPATH_LOCKED) == PatternGraphLhsNode.MOD_PATTERNPATH_LOCKED;
		}

		public virtual void ResolvePatternLockedModifier()
		{
			// in pre-order walk: add all elements of parent to child if child requests so by pattern locked modifier

			// if nested negative requests so, add all of our elements to it
			foreach(PatternGraphLhs negative in Negs)
			{
				if((negative.modifiers & PatternGraphLhsNode.MOD_PATTERN_LOCKED) != PatternGraphLhsNode.MOD_PATTERN_LOCKED)
					continue;

				foreach(Node node in Nodes)
				{
					if(!negative.HasNode(node))
						negative.AddSingleNode(node);
				}
				foreach(Edge edge in Edges)
				{
					if(!negative.HasEdge(edge))
						negative.AddSingleEdge(edge);
				}
			}

			// if nested independent requests so, add all of our elements to it
			foreach(PatternGraphLhs independent in Idpts)
			{
				if((independent.modifiers & PatternGraphLhsNode.MOD_PATTERN_LOCKED) != PatternGraphLhsNode.MOD_PATTERN_LOCKED)
					continue;

				foreach(Node node in Nodes)
				{
					if(!independent.HasNode(node))
						independent.AddSingleNode(node);
				}
				foreach(Edge edge in Edges)
				{
					if(!independent.HasEdge(edge))
						independent.AddSingleEdge(edge);
				}
			}

			// recursive descend
			foreach(PatternGraphLhs negative in Negs)
				negative.ResolvePatternLockedModifier();
			foreach(PatternGraphLhs independent in Idpts)
				independent.ResolvePatternLockedModifier();
		}

		public virtual void EnsureDirectlyNestingPatternContainsAllNonLocalElementsOfNestedPattern(
				HashSet<Node> alreadyDefinedNodes, HashSet<Edge> alreadyDefinedEdges,
				HashSet<Variable> alreadyDefinedVariables,
				PatternGraphRhs right)
		{
			// first local corrections, then global consistency
			if(right != null)
				InsertElementsFromRhsDeclaredInNestingRhsToReplParams(right);
			if(right != null)
				InsertElementsFromRhsDeclaredInNestingLhsToLocalLhs(right);

			///////////////////////////////////////////////////////////////////////////////
			// pre: add locally referenced/defined elements to already referenced/defined elements

			foreach(Node node in Nodes)
				alreadyDefinedNodes.Add(node);
			foreach(Edge edge in Edges)
				alreadyDefinedEdges.Add(edge);
			foreach(Variable var in Vars)
				alreadyDefinedVariables.Add(var);

			///////////////////////////////////////////////////////////////////////////////
			// depth first walk over IR-pattern-graph tree structure
			foreach(Alternative alternative in Alts)
			{
				foreach(Rule altCase in alternative.AlternativeCases)
				{
					PatternGraphLhs altCasePattern = altCase.Left;
					HashSet<Node> alreadyDefinedNodesClone = new HashSet<Node>(alreadyDefinedNodes);
					HashSet<Edge> alreadyDefinedEdgesClone = new HashSet<Edge>(alreadyDefinedEdges);
					HashSet<Variable> alreadyDefinedVariablesClone = new HashSet<Variable>(alreadyDefinedVariables);
					altCasePattern.EnsureDirectlyNestingPatternContainsAllNonLocalElementsOfNestedPattern(
							alreadyDefinedNodesClone, alreadyDefinedEdgesClone, alreadyDefinedVariablesClone,
							altCase.Right);
				}
			}

			foreach(Rule iterated in Iters)
			{
				PatternGraphLhs iteratedPattern = iterated.Left;
				HashSet<Node> alreadyDefinedNodesClone = new HashSet<Node>(alreadyDefinedNodes);
				HashSet<Edge> alreadyDefinedEdgesClone = new HashSet<Edge>(alreadyDefinedEdges);
				HashSet<Variable> alreadyDefinedVariablesClone = new HashSet<Variable>(alreadyDefinedVariables);
				iteratedPattern.EnsureDirectlyNestingPatternContainsAllNonLocalElementsOfNestedPattern(
						alreadyDefinedNodesClone, alreadyDefinedEdgesClone, alreadyDefinedVariablesClone,
						iterated.Right);
			}

			foreach(PatternGraphLhs negative in Negs)
			{
				HashSet<Node> alreadyDefinedNodesClone = new HashSet<Node>(alreadyDefinedNodes);
				HashSet<Edge> alreadyDefinedEdgesClone = new HashSet<Edge>(alreadyDefinedEdges);
				HashSet<Variable> alreadyDefinedVariablesClone = new HashSet<Variable>(alreadyDefinedVariables);
				negative.EnsureDirectlyNestingPatternContainsAllNonLocalElementsOfNestedPattern(
						alreadyDefinedNodesClone, alreadyDefinedEdgesClone, alreadyDefinedVariablesClone,
						null);
			}

			foreach(PatternGraphLhs independent in Idpts)
			{
				HashSet<Node> alreadyDefinedNodesClone = new HashSet<Node>(alreadyDefinedNodes);
				HashSet<Edge> alreadyDefinedEdgesClone = new HashSet<Edge>(alreadyDefinedEdges);
				HashSet<Variable> alreadyDefinedVariablesClone = new HashSet<Variable>(alreadyDefinedVariables);
				independent.EnsureDirectlyNestingPatternContainsAllNonLocalElementsOfNestedPattern(
						alreadyDefinedNodesClone, alreadyDefinedEdgesClone, alreadyDefinedVariablesClone,
						null);
			}

			///////////////////////////////////////////////////////////////////////////////
			// post: add elements of subpatterns not defined there to our nodes'n'edges

			// add elements needed in alternative cases, which are not defined there and are neither defined nor used here
			// they must get handed down as preset from the defining nesting pattern to here
			foreach(Alternative alternative in Alts)
			{
				foreach(Rule altCase in alternative.AlternativeCases)
				{
					PatternGraphLhs altCasePattern = altCase.Left;
					foreach(Node node in altCasePattern.Nodes)
					{
						if(!HasNode(node) && alreadyDefinedNodes.Contains(node))
						{
							AddSingleNode(node);
							AddHomToAll(node);
							PatternGraphBase altCaseReplacement = altCase.Right;
							if(altCaseReplacement != null && !altCaseReplacement.HasNode(node))
							{
								// prevent deletion of elements inserted for pattern completion
								altCaseReplacement.AddSingleNode(node);
							}
							if(right != null && !right.HasNode(node) && !right.DeletedElements.Contains(node))
								right.AddSingleNode(node);
						}
					}
					foreach(Edge edge in altCasePattern.Edges)
					{
						if(!HasEdge(edge) && alreadyDefinedEdges.Contains(edge))
						{
							AddSingleEdge(edge);
							AddHomToAll(edge);
							PatternGraphBase altCaseReplacement = altCase.Right;
							if(altCaseReplacement != null && !altCaseReplacement.HasEdge(edge))
							{
								// prevent deletion of elements inserted for pattern completion
								altCaseReplacement.AddSingleEdge(edge);
							}
							if(right != null && !right.HasEdge(edge) && !right.DeletedElements.Contains(edge))
								right.AddSingleEdge(edge);
						}
					}
					foreach(Variable var in altCasePattern.Vars)
					{
						if(!HasVar(var) && alreadyDefinedVariables.Contains(var))
							AddVariable(var);
					}

					// add rhs parameters from nested alternative cases if they are not used or defined here to our rhs parameters,
					// so we get them, so we're able to forward them
					if(right != null)
					{
						IList<Entity> altCaseReplParameters = altCase.Right.ReplParameters;
						foreach(Entity entity in altCaseReplParameters)
						{
							if(entity is Node)
							{
								Node node = (Node)entity;
								if(node.directlyNestingLHSGraph != this)
								{
									if(!right.ReplParameters.Contains(node))
										right.AddReplParameter(node);
								}
							}
							if(entity is Edge)
							{
								Edge edge = (Edge)entity;
								if(edge.directlyNestingLHSGraph != this)
								{
									if(!right.ReplParameters.Contains(edge))
										right.AddReplParameter(edge);
								}
							}
							if(entity is Variable)
							{
								Variable var = (Variable)entity;
								if(var.directlyNestingLHSGraph != this)
								{
									if(!right.ReplParameters.Contains(var))
										right.AddReplParameter(var);
								}
							}
						}
					}
				}
			}

			// add elements needed in iterated, which are not defined there and are neither defined nor used here
			// they must get handed down as preset from the defining nesting pattern to here
			foreach(Rule iterated in Iters)
			{
				PatternGraphLhs iteratedPattern = iterated.Left;
				foreach(Node node in iteratedPattern.Nodes)
				{
					if(!HasNode(node) && alreadyDefinedNodes.Contains(node))
					{
						AddSingleNode(node);
						AddHomToAll(node);
						PatternGraphBase allReplacement = iterated.Right;
						if(allReplacement != null && !allReplacement.HasNode(node))
						{
							// prevent deletion of elements inserted for pattern completion
							allReplacement.AddSingleNode(node);
						}
						if(right != null && !right.HasNode(node) && !right.DeletedElements.Contains(node))
							right.AddSingleNode(node);
					}
				}
				foreach(Edge edge in iteratedPattern.Edges)
				{
					if(!HasEdge(edge) && alreadyDefinedEdges.Contains(edge))
					{
						AddSingleEdge(edge);
						AddHomToAll(edge);
						PatternGraphBase allReplacement = iterated.Right;
						if(allReplacement != null && !allReplacement.HasEdge(edge))
						{
							// prevent deletion of elements inserted for pattern completion
							allReplacement.AddSingleEdge(edge);
						}
						if(right != null && !right.HasEdge(edge) && !right.DeletedElements.Contains(edge))
							right.AddSingleEdge(edge);
					}
				}
				foreach(Variable var in iteratedPattern.Vars)
				{
					if(!HasVar(var) && alreadyDefinedVariables.Contains(var))
						AddVariable(var);
				}

				// add rhs parameters from nested iterateds if they are not used or defined here to our rhs parameters,
				// so we get them, so we're able to forward them
				if(right != null)
				{
					IList<Entity> iteratedReplParameters = iterated.Right.ReplParameters;
					foreach(Entity iteratedReplParameter in iteratedReplParameters)
					{
						if(iteratedReplParameter is Node)
						{
							Node node = (Node)iteratedReplParameter;
							if(node.directlyNestingLHSGraph != this)
							{
								if(!right.ReplParameters.Contains(node))
									right.AddReplParameter(node);
							}
						}
						if(iteratedReplParameter is Edge)
						{
							Edge edge = (Edge)iteratedReplParameter;
							if(edge.directlyNestingLHSGraph != this)
							{
								if(!right.ReplParameters.Contains(edge))
									right.AddReplParameter(edge);
							}
						}
						if(iteratedReplParameter is Variable)
						{
							Variable var = (Variable)iteratedReplParameter;
							if(var.directlyNestingLHSGraph != this)
							{
								if(!right.ReplParameters.Contains(var))
									right.AddReplParameter(var);
							}
						}
					}
				}
			}

			// add elements needed in nested neg, which are not defined there and are neither defined nor used here
			// they must get handed down as preset from the defining nesting pattern to here
			foreach(PatternGraphLhs negative in Negs)
			{
				foreach(Node node in negative.Nodes)
				{
					if(!HasNode(node) && alreadyDefinedNodes.Contains(node))
					{
						AddSingleNode(node);
						AddHomToAll(node);
						if(right != null && !right.HasNode(node) && !right.DeletedElements.Contains(node))
							right.AddSingleNode(node);
					}
				}
				foreach(Edge edge in negative.Edges)
				{
					if(!HasEdge(edge) && alreadyDefinedEdges.Contains(edge))
					{
						AddSingleEdge(edge);
						AddHomToAll(edge);
						if(right != null && !right.HasEdge(edge) && !right.DeletedElements.Contains(edge))
							right.AddSingleEdge(edge);
					}
				}
				foreach(Variable var in negative.Vars)
				{
					if(!HasVar(var) && alreadyDefinedVariables.Contains(var))
						AddVariable(var);
				}
			}

			// add elements needed in nested idpt, which are not defined there and are neither defined nor used here
			// they must get handed down as preset from the defining nesting pattern to here
			foreach(PatternGraphLhs independent in Idpts)
			{
				foreach(Node node in independent.Nodes)
				{
					if(!HasNode(node) && alreadyDefinedNodes.Contains(node))
					{
						AddSingleNode(node);
						AddHomToAll(node);
						if(right != null && !right.HasNode(node) && !right.DeletedElements.Contains(node))
							right.AddSingleNode(node);
					}
				}
				foreach(Edge edge in independent.Edges)
				{
					if(!HasEdge(edge) && alreadyDefinedEdges.Contains(edge))
					{
						AddSingleEdge(edge);
						AddHomToAll(edge);
						if(right != null && !right.HasEdge(edge) && !right.DeletedElements.Contains(edge))
							right.AddSingleEdge(edge);
					}
				}
				foreach(Variable var in independent.Vars)
				{
					if(!HasVar(var) && alreadyDefinedVariables.Contains(var))
						AddVariable(var);
				}
			}
		}

		// construct implicit rhs replace parameters
		public virtual void InsertElementsFromRhsDeclaredInNestingRhsToReplParams(PatternGraphRhs right)
		{
			if(right == null)
				return;

			// insert all nodes and variables, which are used (not declared) on the right hand side and not declared on left hand side,
			// and are declared in some nesting right hand side,
			// to the replacement parameters (so that they get handed down from the nesting replacement)

			foreach(Node node in right.Nodes)
			{
				if(node.directlyNestingLHSGraph != this && !right.ReplParametersContain(node))
				{
					if((node.context & BaseNode.CONTEXT_LHS_OR_RHS) == BaseNode.CONTEXT_RHS)
						right.AddReplParameter(node);
				}
			}

			foreach(Variable var in right.Vars)
			{
				if(var.directlyNestingLHSGraph != this && !right.ReplParametersContain(var))
				{
					if((var.context & BaseNode.CONTEXT_LHS_OR_RHS) == BaseNode.CONTEXT_RHS)
						right.AddReplParameter(var);
				}
			}

			// emit error for edges which would have be to be handled like this,
			// because they are not available in the nested replacement;
			// as they are only created after the replacement code of the nested pattern is left, 
			// that's because node retyping occurs only afterwards, 
			// and we maybe want to create edges in between retyped=newly created nodes
			foreach(Edge edge in right.Edges)
			{
				if(edge.directlyNestingLHSGraph != this)
				{
					if((edge.context & BaseNode.CONTEXT_LHS_OR_RHS) == BaseNode.CONTEXT_RHS)
						error.Error(edge.Ident.Coords, "Cannot access a newly created edge (" + edge.Ident + ")" + " in a nested rewrite part.");
				}
			}

			// some check which is easier on ir
			CheckThatEvalhereIsNotAccessingCreatedEdges(right);
		}

		public static void CheckThatEvalhereIsNotAccessingCreatedEdges(PatternGraphRhs right)
		{
			if(right == null)
				return;

			// emit error on accessing freshly created edges from evalhere statements as they are not available there
			// because they are only created after the evalhere statements are evaluated 

			foreach(OrderedReplacements orderedRepls in right.OrderedReplacements)
			{
				foreach(OrderedReplacement orderedRepl in orderedRepls.orderedReplacements)
				{
					if(orderedRepl is EvalStatement)
					{
						EvalStatement evalStmt = (EvalStatement)orderedRepl;
						NeededEntities needs = new NeededEntities(EnumSet.Of(NeededEntities.Needs.EDGES, NeededEntities.Needs.ALL_ATTRIBUTES));
						evalStmt.CollectNeededEntities(needs);
						foreach(Edge edge in needs.edges)
						{
							if((edge.context & BaseNode.CONTEXT_LHS_OR_RHS) == BaseNode.CONTEXT_RHS)
							{
								error.Error(edge.Ident.Coords, "Cannot access a newly created edge (" + edge.Ident + ")"
										+ " from an evalhere statement.");
							}
						}
						foreach(Edge edge in needs.attrEdges)
						{
							if((edge.context & BaseNode.CONTEXT_LHS_OR_RHS) == BaseNode.CONTEXT_RHS)
							{
								error.Error(edge.Ident.Coords, "Cannot access a newly created edge (" + edge.Ident + ")"
										+ " from an evalhere statement.");
							}
						}
					}
				}
			}
		}

		// constructs implicit lhs elements
		public virtual void InsertElementsFromRhsDeclaredInNestingLhsToLocalLhs(PatternGraphRhs right)
		{
			if(right == null)
				return;

			// insert all elements, which are used (not declared) on the right hand side and not declared on left hand side,
			//   and are not amongst the replacement parameters
			// which means they are declared in some pattern the left hand side is nested in,
			// to the left hand side (so that they get handed down from the nesting pattern;
			// otherwise they would be created (code generation by locally comparing lhs and rhs))

			foreach(Node node in right.Nodes)
			{
				if(node.directlyNestingLHSGraph != this && !right.ReplParametersContain(node))
				{
					if(!HasNode(node))
					{
						AddSingleNode(node);
						AddHomToAll(node);
					}
				}
			}

			foreach(Edge edge in right.Edges)
			{
				if(edge.directlyNestingLHSGraph != this && !right.ReplParametersContain(edge))
				{
					if(!HasEdge(edge))
					{
						AddSingleEdge(edge);
						AddHomToAll(edge);
					}
				}
			}

			foreach(Variable var in right.Vars)
			{
				if(var.directlyNestingLHSGraph != this && !right.ReplParametersContain(var))
					AddVariable(var);
			}
		}

		public virtual void CheckForEmptyPatternsInIterateds()
		{
			if(mayPatternBeEmptyComputationState != PATTERN_NOT_YET_VISITED)
				return;

			mayPatternBeEmptyComputationState = PATTERN_MAYBE_EMPTY;

			///////////////////////////////////////////////////
			// have a look at the local pattern

			foreach(Node node in Nodes)
			{
				if(node.directlyNestingLHSGraph != this)
					goto nodeHomContinue;
				foreach(Node homNode in GetHomomorphic(node))
				{
					if(homNode.directlyNestingLHSGraph != this)
						goto nodeHomContinue;
				}
				mayPatternBeEmptyComputationState = PATTERN_NOT_EMPTY;
				break;
		nodeHomContinue:;
			}
	nodeHomBreak:
			if(mayPatternBeEmptyComputationState != PATTERN_NOT_EMPTY)
			{
				foreach(Edge edge in Edges)
				{
					if(edge.directlyNestingLHSGraph != this)
						goto edgeHomContinue;
					foreach(Edge homEdge in GetHomomorphic(edge))
					{
						if(homEdge.directlyNestingLHSGraph != this)
							goto edgeHomContinue;
					}
					mayPatternBeEmptyComputationState = PATTERN_NOT_EMPTY;
					break;
		edgeHomContinue:;
				}
	edgeHomBreak:;
			}

			///////////////////////////////////////////////////
			// go through the nested patterns, check the iterateds

			foreach(Alternative alternative in Alts)
			{
				bool allCasesNonEmpty = true;
				foreach(Rule altCase in alternative.AlternativeCases)
				{
					altCase.pattern.CheckForEmptyPatternsInIterateds();
					if(altCase.pattern.mayPatternBeEmptyComputationState == PATTERN_MAYBE_EMPTY)
						allCasesNonEmpty = false;
				}
				if(allCasesNonEmpty)
					mayPatternBeEmptyComputationState = PATTERN_NOT_EMPTY;
			}

			foreach(Rule iterated in Iters)
			{
				iterated.pattern.CheckForEmptyPatternsInIterateds();
				if(iterated.pattern.mayPatternBeEmptyComputationState == PATTERN_MAYBE_EMPTY)
				{
					// emit error if the iterated pattern might be empty
					if(iterated.MaxMatches == 0)
					{
						error.Error(iterated.Ident.Coords,
								"An unbounded pattern cardinality construct (iterated, multiple, [*])"
										+ " must contain at least one locally defined node or edge (not being homomorphic to an enclosing element)"
										+ " or a nested subpattern or alternative not being empty.");
					}
					else if(iterated.MaxMatches > 1)
					{
						error.Warning(iterated.Ident.Coords,
								"Maybe empty pattern in pattern cardinality construct (you must expect empty matches).");
					}
				}
				else
				{
					if(iterated.MinMatches > 0)
						mayPatternBeEmptyComputationState = PATTERN_NOT_EMPTY;
				}
			}

			foreach(SubpatternUsage sub in SubpatternUsages)
			{
				sub.subpatternAction.pattern.CheckForEmptyPatternsInIterateds();
				if(sub.subpatternAction.pattern.mayPatternBeEmptyComputationState == PATTERN_NOT_EMPTY)
					mayPatternBeEmptyComputationState = PATTERN_NOT_EMPTY;
			}

			foreach(PatternGraphLhs negative in Negs)
				negative.CheckForEmptyPatternsInIterateds();

			foreach(PatternGraphLhs independent in Idpts)
				independent.CheckForEmptyPatternsInIterateds();
		}

		public virtual void CheckForEmptySubpatternRecursions(HashSet<PatternGraphLhs> subpatternsAlreadyVisited)
		{
			foreach(Node node in Nodes)
			{
				if(node.directlyNestingLHSGraph != this)
					goto nodeHomContinue;
				foreach(Node homNode in GetHomomorphic(node))
				{
					if(homNode.directlyNestingLHSGraph != this)
						goto nodeHomContinue;
				}
				return; // node which must get matched found -> can't build empty path
		nodeHomContinue:;
			}
	nodeHomBreak:
			foreach(Edge edge in Edges)
			{
				if(edge.directlyNestingLHSGraph != this)
					goto edgeHomContinue;
				foreach(Edge homEdge in GetHomomorphic(edge))
				{
					if(homEdge.directlyNestingLHSGraph != this)
						goto edgeHomContinue;
				}
				return; // edge which must get matched found -> can't build empty path
		edgeHomContinue:;
			}
	edgeHomBreak:

			foreach(Expression cond in Conditions)
			{
				if(cond is Constant)
				{
					if(((Constant)cond).value is bool?)
					{
						Constant constCond = (Constant)cond;
						if(((bool?)constCond.value).Value)
							continue;
					}
				}
				return; // a non-const or non-true const condition is a sign that the recursion will terminate
			}

			foreach(Alternative alternative in Alts)
			{
				foreach(Rule altCase in alternative.AlternativeCases)
				{
					HashSet<PatternGraphLhs> subpatternsAlreadyVisitedClone =
							new HashSet<PatternGraphLhs>(subpatternsAlreadyVisited);
					altCase.pattern.CheckForEmptySubpatternRecursions(subpatternsAlreadyVisitedClone);
				}
			}

			foreach(Rule iterated in Iters)
			{
				HashSet<PatternGraphLhs> subpatternsAlreadyVisitedClone = new HashSet<PatternGraphLhs>(subpatternsAlreadyVisited);
				iterated.pattern.CheckForEmptySubpatternRecursions(subpatternsAlreadyVisitedClone);
			}

			foreach(PatternGraphLhs negative in Negs)
			{
				HashSet<PatternGraphLhs> subpatternsAlreadyVisitedClone = new HashSet<PatternGraphLhs>(subpatternsAlreadyVisited);
				negative.CheckForEmptySubpatternRecursions(subpatternsAlreadyVisitedClone);
			}

			foreach(PatternGraphLhs independent in Idpts)
			{
				HashSet<PatternGraphLhs> subpatternsAlreadyVisitedClone = new HashSet<PatternGraphLhs>(subpatternsAlreadyVisited);
				independent.CheckForEmptySubpatternRecursions(subpatternsAlreadyVisitedClone);
			}

			foreach(SubpatternUsage sub in SubpatternUsages)
			{
				if(!subpatternsAlreadyVisited.Contains(sub.subpatternAction.pattern))
				{
					HashSet<PatternGraphLhs> subpatternsAlreadyVisitedClone =
							new HashSet<PatternGraphLhs>(subpatternsAlreadyVisited);
					subpatternsAlreadyVisitedClone.Add(sub.subpatternAction.pattern);
					sub.subpatternAction.pattern.CheckForEmptySubpatternRecursions(subpatternsAlreadyVisitedClone);
				}
				else
				{
					// we're on path of only (maybe) empty patterns and see a subpattern already on it again
					// -> endless loop of this subpattern matching only empty patterns until it gets matched again 
					error.Error(sub.subpatternAction.Ident.Coords, "The (sub)pattern " + sub.subpatternAction.Ident
							+ " (potentially) calls itself again with only empty patterns in between, yielding an endless loop during pattern matching.");
				}
			}
		}

		public virtual bool IsNeverTerminatingSuccessfully(HashSet<PatternGraphLhs> subpatternsAlreadyVisited)
		{
			bool neverTerminatingSuccessfully = false;

			foreach(Alternative alternative in Alts)
			{
				bool allCasesNotTerminating = true;
				foreach(Rule altCase in alternative.AlternativeCases)
				{
					HashSet<PatternGraphLhs> subpatternsAlreadyVisitedClone =
							new HashSet<PatternGraphLhs>(subpatternsAlreadyVisited);
					allCasesNotTerminating &= altCase.pattern.IsNeverTerminatingSuccessfully(subpatternsAlreadyVisitedClone);
				}
				neverTerminatingSuccessfully |= allCasesNotTerminating;
			}

			foreach(Rule iterated in Iters)
			{
				HashSet<PatternGraphLhs> subpatternsAlreadyVisitedClone = new HashSet<PatternGraphLhs>(subpatternsAlreadyVisited);
				if(iterated.MinMatches > 0)
					neverTerminatingSuccessfully |= iterated.pattern.IsNeverTerminatingSuccessfully(subpatternsAlreadyVisitedClone);
			}

			foreach(PatternGraphLhs negative in Negs)
			{
				HashSet<PatternGraphLhs> subpatternsAlreadyVisitedClone = new HashSet<PatternGraphLhs>(subpatternsAlreadyVisited);
				neverTerminatingSuccessfully |= negative.IsNeverTerminatingSuccessfully(subpatternsAlreadyVisitedClone);
			}

			foreach(PatternGraphLhs independent in Idpts)
			{
				HashSet<PatternGraphLhs> subpatternsAlreadyVisitedClone = new HashSet<PatternGraphLhs>(subpatternsAlreadyVisited);
				neverTerminatingSuccessfully |= independent.IsNeverTerminatingSuccessfully(subpatternsAlreadyVisitedClone);
			}

			foreach(SubpatternUsage sub in SubpatternUsages)
			{
				if(!subpatternsAlreadyVisited.Contains(sub.subpatternAction.pattern))
				{
					HashSet<PatternGraphLhs> subpatternsAlreadyVisitedClone =
							new HashSet<PatternGraphLhs>(subpatternsAlreadyVisited);
					subpatternsAlreadyVisitedClone.Add(sub.subpatternAction.pattern);
					neverTerminatingSuccessfully |= sub.subpatternAction.pattern.IsNeverTerminatingSuccessfully(subpatternsAlreadyVisitedClone);
				}
				else
					return true;
			}

			return neverTerminatingSuccessfully;
		}

		public virtual void CheckForMultipleRetypes(HashSet<Node> alreadyDefinedNodes, HashSet<Edge> alreadyDefinedEdges,
				PatternGraphBase right)
		{
			foreach(Node node in Nodes)
				alreadyDefinedNodes.Add(node);
			foreach(Edge edge in Edges)
				alreadyDefinedEdges.Add(edge);

			foreach(Alternative alternative in Alts)
			{
				foreach(Rule altCase in alternative.AlternativeCases)
				{
					PatternGraphLhs altCasePattern = altCase.Left;
					HashSet<Node> alreadyDefinedNodesClone = new HashSet<Node>(alreadyDefinedNodes);
					HashSet<Edge> alreadyDefinedEdgesClone = new HashSet<Edge>(alreadyDefinedEdges);
					altCasePattern.CheckForMultipleRetypes(
							alreadyDefinedNodesClone, alreadyDefinedEdgesClone, altCase.Right);
				}
			}

			foreach(Rule iterated in Iters)
			{
				PatternGraphLhs iteratedPattern = iterated.Left;
				HashSet<Node> alreadyDefinedNodesClone = new HashSet<Node>(alreadyDefinedNodes);
				HashSet<Edge> alreadyDefinedEdgesClone = new HashSet<Edge>(alreadyDefinedEdges);
				iteratedPattern.CheckForMultipleRetypes(
						alreadyDefinedNodesClone, alreadyDefinedEdgesClone, iterated.Right);

				if(iterated.MaxMatches != 1)
				{
					iteratedPattern.CheckForMultipleRetypesDoCheck(alreadyDefinedNodes, alreadyDefinedEdges,
							iterated.Right);
				}
			}
		}

		public virtual void CheckForMultipleRetypesDoCheck(HashSet<Node> alreadyDefinedNodes, HashSet<Edge> alreadyDefinedEdges,
				PatternGraphBase right)
		{
			foreach(Node node in right.Nodes)
			{
				if(node.GetRetypedNode(right) == null)
					continue;
				if(alreadyDefinedNodes.Contains(node))
				{
					error.Error(node.Ident.Coords, "A retyping of nodes from a nesting pattern is forbidden"
							+ " if they are contained in a construct which can get matched more than once (due to some kind of iterated)"
							+ " (this occurs for " + node + ").");
				}
				else
				{
					foreach(Node homToRetypedNode in GetHomomorphic(node))
					{
						if(alreadyDefinedNodes.Contains(homToRetypedNode))
						{
							error.Error(node.Ident.Coords, "A retyping of nodes which might be hom to nodes from a nesting pattern is forbidden"
									+ " if they are contained in a construct which can get matched more than once (due to some kind of iterated)"
									+ " (this occurs for " + node + ").");
						}
					}
				}
			}
			foreach(Edge edge in right.Edges)
			{
				if(edge.GetRetypedEdge(right) == null)
					continue;
				if(alreadyDefinedEdges.Contains(edge))
				{
					error.Error(edge.Ident.Coords, "A retyping of edges from a nesting pattern is forbidden"
							+ " if they are contained in a construct which can get matched more than once (due to some kind of iterated)"
							+ " (this occurs for " + edge + ").");
				}
				else
				{
					foreach(Edge homToRetypedEdge in GetHomomorphic(edge))
					{
						if(alreadyDefinedEdges.Contains(homToRetypedEdge))
						{
							error.Error(edge.Ident.Coords, "A retyping of edges which might be hom to edges from a nesting pattern is forbidden"
									+ " if they are contained in construct which can get matched more than once (due to some kind of iterated)"
									+ " (this occurs for " + edge + ").");
						}
					}
				}
			}

			foreach(Alternative alternative in Alts)
			{
				foreach(Rule altCase in alternative.AlternativeCases)
				{
					PatternGraphLhs altCasePattern = altCase.Left;
					altCasePattern.CheckForMultipleRetypesDoCheck(
							alreadyDefinedNodes, alreadyDefinedEdges, altCase.Right);
				}
			}

			foreach(Rule iterated in Iters)
			{
				PatternGraphLhs iteratedPattern = iterated.Left;
				iteratedPattern.CheckForMultipleRetypesDoCheck(
						alreadyDefinedNodes, alreadyDefinedEdges, iterated.Right);
			}
		}
	}

}
