/*
 * GrGen: graph rewrite generator tool -- release GrGen.NET 8.1
 * Copyright (C) 2003-2026 Universitaet Karlsruhe, Institut fuer Programmstrukturen und Datenorganisation, LS Goos; and free programmers
 * licensed under LGPL v3, some components/parts use different licenses (see LICENSE.txt included in the packaging of this file)
 * www.grgen.de / www.grgen.net
 */

/// <summary>
/// Extracts all the information needed by the FrameBasedBackend
/// from the GrGen-internal IR
/// @author Adam Szalkowski
/// </summary>

namespace de.unika.ipd.grgen.be.C
{

	using System;
	using System.Collections.Generic;
	using System.Diagnostics;
	using System.Linq;

	using de.unika.ipd.grgen.ir;
	using Action = de.unika.ipd.grgen.ir.executable.Action;
	using MatchingAction = de.unika.ipd.grgen.ir.executable.MatchingAction;
	using Rule = de.unika.ipd.grgen.ir.executable.Rule;
	using Expression = de.unika.ipd.grgen.ir.expr.Expression;
	using Operator = de.unika.ipd.grgen.ir.expr.Operator;
	using OperatorCode = de.unika.ipd.grgen.ir.expr.OperatorCode;
	using Qualification = de.unika.ipd.grgen.ir.expr.Qualification;
	using InheritanceType = de.unika.ipd.grgen.ir.model.type.InheritanceType;
	using Edge = de.unika.ipd.grgen.ir.pattern.Edge;
	using PatternGraphBase = de.unika.ipd.grgen.ir.pattern.PatternGraphBase;
	using Node = de.unika.ipd.grgen.ir.pattern.Node;
	using PatternGraphLhs = de.unika.ipd.grgen.ir.pattern.PatternGraphLhs;
	using Assignment = de.unika.ipd.grgen.ir.stmt.Assignment;
	using EvalStatement = de.unika.ipd.grgen.ir.stmt.EvalStatement;
	using EvalStatements = de.unika.ipd.grgen.ir.stmt.EvalStatements;
	using PrimitiveType = de.unika.ipd.grgen.ir.type.basic.PrimitiveType;
	using Annotations = de.unika.ipd.grgen.util.Annotations;

	public class MoreInformationCollector : InformationCollector
	{
		/* maps an eval list to the action_id it belong to */
		protected internal IDictionary<ICollection<Assignment>, int> evalListMap = new Dictionary<ICollection<Assignment>, int>();

		/* replacement and pattern nodes/edges involved in an eval */
		protected internal IDictionary<ICollection<Assignment>, ICollection<Node>> evalInvolvedNodes = new Dictionary<ICollection<Assignment>, ICollection<Node>>();
		protected internal IDictionary<ICollection<Assignment>, ICollection<Edge>> evalInvolvedEdges = new Dictionary<ICollection<Assignment>, ICollection<Edge>>();

		/* maps action id to eval list */
		protected internal IDictionary<ICollection<Assignment>, Action> evalActions = new Dictionary<ICollection<Assignment>, Action>();

		/* edge and node attributes involved in that eval */
		protected internal IList<IDictionary<Node, ICollection<int>>> involvedEvalNodeAttrIds;
		protected internal IList<IDictionary<Edge, ICollection<int>>> involvedEvalEdgeAttrIds;

		//returns id of corresponding pattern edge id if edge is kept
		//else -1 if edge is new one
		//usage: replacementEdgeIsPresevedNode[act_id][replacement_edge_num]
		protected internal int[][] replacementEdgeIsPreservedEdge;

		//returns id of corresponding replacement edge id if edge is kept
		//else -1 if edge is to be deleted
		//usage: patternEdgeIsToBeKept[act_id][pattern_edge_num]
		protected internal int[][] patternEdgeIsToBeKept;

		private const int min_subgraph_size = 4;

		/*
		 * collect some information about evals
		 */
		protected internal virtual void CollectEvalInfo()
		{
			involvedEvalNodeAttrIds = new List<IDictionary<Node, ICollection<int>>>(actionRuleMap.Count);
			involvedEvalEdgeAttrIds = new List<IDictionary<Edge, ICollection<int>>>(actionRuleMap.Count);

			foreach(Rule act in actionRuleMap.Keys)
			{
				if(act.Right != null)
				{
					int act_id = actionRuleMap[act];

					ICollection<Assignment> rule_evals = new List<Assignment>();
					foreach(EvalStatements evalStmts in act.Evals)
					{
						foreach(EvalStatement evalStmt in evalStmts.evalStatements)
						{
							if(evalStmt is Assignment)
								rule_evals.Add((Assignment)evalStmt);
						}
					}

					evalListMap[rule_evals] = act_id;
					evalActions[rule_evals] = act;

					ISet<Node> involvedNodes = new HashSet<Node>();
					ISet<Edge> involvedEdges = new HashSet<Edge>();
					involvedEvalNodeAttrIds[act_id] = new Dictionary<Node, ICollection<int>>();
					involvedEvalEdgeAttrIds[act_id] = new Dictionary<Edge, ICollection<int>>();

					foreach(Assignment eval in rule_evals)
					{
						Expression targetExpr = eval.Target;
						if(!(targetExpr is Qualification))
							throw new System.NotSupportedException("The C backend only supports assignments to qualified expressions, yet!");
						Qualification target = (Qualification)targetExpr;
						Expression expr = eval.Expression;

						/* generate an expression that consists of both parts of the Assignment to use the already implemented methods for gathering InvolvedNodes/Edges etc. */
						Operator op = new Operator((PrimitiveType)target.Type, OperatorCode.EQ);
						op.AddOperand(target);
						op.AddOperand(expr);

						//...extract the pattern nodes and edges involved in the evaluation
						involvedNodes.AddAll(CollectInvolvedNodes(op));
						involvedEdges.AddAll(CollectInvolvedEdges(op));

						/* for all evaluations the pairs (pattern_node_num, attr_id), which occur
						 in qualifications at the leaves of the eval, are needed.
						 To obtain that compute a map
						 act_id -> pattern_node_num_ -> { attr_ids }
						 implemented by an Array of Maps; usage is:
						 involvedPatternNodeAttrIds[act_id].get(pattern_node_num)
						 which yields a Collection of attr-ids.
						 */

						//collect the attr ids in dependency of evaluation and the pattern node

						//descent to the conditions leaves and look for qualifications
						_recursiveQualCollect(act_id, involvedEvalNodeAttrIds[act_id], involvedEvalEdgeAttrIds[act_id], op);
					}

					//add Collections of involved Nodes/Edges to prepared Maps
					evalInvolvedNodes[rule_evals] = involvedNodes;
					evalInvolvedEdges[rule_evals] = involvedEdges;
				}
			}
		}

		/// <summary>
		/// Method collectReplacementEdgeIsPreservedEdgeInfo
		/// </summary>
		private void CollectReplacementEdgeIsPreservedEdgeInfo()
		{
			replacementEdgeIsPreservedEdge = RectangularArrays.RectangularIntArray(n_graph_actions, max_n_replacement_edges);

			//init the array with -1
			for(int i = 0; i < n_graph_actions; i++)
			{
				for(int j = 0; j < max_n_replacement_edges; j++)
					replacementEdgeIsPreservedEdge[i][j] = -1;
			}

			//for all edges preserved set the corresponding array entry to the
			//appropriate pattern edge number
			foreach(Rule action in actionRuleMap.Keys)
			{
				int act_id = actionRuleMap[action];

				if(action.Right != null)
				{
					//compute the set of replacement edges preserved by this action
					ISet<Edge> replacement_edges_preserved = new HashSet<Edge>();
					replacement_edges_preserved.AddAll(action.Right.Edges);
					replacement_edges_preserved.RetainAll(action.Pattern.Edges);
					//for all those preserved replacement edges store the
					//corresponding pattern edge
					foreach(Edge edge in replacement_edges_preserved)
					{
						int edge_num = replacement_edge_num[act_id][edge];
						replacementEdgeIsPreservedEdge[act_id][edge_num] = pattern_edge_num[act_id][edge];
					}
				}
			}
		}

		/// <summary>
		/// Method collectPatternEdgesToBeKeptInfo
		/// </summary>
		private void CollectPatternEdgesToBeKeptInfo()
		{
			patternEdgeIsToBeKept = RectangularArrays.RectangularIntArray(n_graph_actions, max_n_pattern_edges);

			//init the arrays with -1
			for(int i = 0; i < n_graph_actions; i++)
			{
				for(int j = 0; j < max_n_pattern_edges; j++)
					patternEdgeIsToBeKept[i][j] = -1;
			}

			//for all edges to be kept set the corresponding array entry to the
			//appropriate replacement edge number
			foreach(Rule action in actionRuleMap.Keys)
			{
				int act_id = actionRuleMap[action];

				//compute the set of pattern edges to be kept for this action
				ISet<Edge> pattern_edges_to_keep = new HashSet<Edge>();
				pattern_edges_to_keep.AddAll(action.Pattern.Edges);
				if(action.Right != null)
				{
					PatternGraphBase replacement = action.Right;
					pattern_edges_to_keep.RetainAll(replacement.Edges);
					//iterate over the pattern edges to be kept and store their
					//corresponding replacement edge number
					foreach(Edge edge in pattern_edges_to_keep)
					{
						int edge_num = pattern_edge_num[act_id][edge];
						patternEdgeIsToBeKept[act_id][edge_num] = replacement_edge_num[act_id][edge];
					}
				}
			}
		}

		protected internal int max_n_negative_nodes = 0;
		protected internal int max_n_negative_edges = 0;
		protected internal int max_n_negative_patterns = 0;
		protected internal int[] n_negative_patterns;
		//	action_id --> neg_id --> pattern_node_num
		protected internal IList<IList<IDictionary<Node, int>>> negative_node_num;
		//	action_id --> neg_id --> pattern_edge_num
		protected internal IList<IList<IDictionary<Edge, int>>> negative_edge_num;
		protected internal IList<IDictionary<PatternGraphLhs, int>> negMap;

		protected internal int[][][] patternNodeIsNegativeNode;
		protected internal int[][][] patternEdgeIsNegativeEdge;

		private void CollectNegativeInfo()
		{
			/* get the overall maximum numbers of nodes and edges of all pattern
			 and replacement graphs respectively */
			max_n_negative_nodes = 0;
			max_n_negative_edges = 0;
			max_n_negative_patterns = 0;

			n_negative_patterns = new int[n_graph_actions];
			negMap = new List<IDictionary<PatternGraphLhs, int>>(n_graph_actions);

			foreach(Rule act in actionRuleMap.Keys)
			{
				int act_id = actionRuleMap[act];
				int negs = 0;

				negMap[act_id] = new Dictionary<PatternGraphLhs, int>();

				//check whether its graphs node and edge set sizes are greater
				int size;

				foreach(PatternGraphLhs negPattern in act.Pattern.Negs)
				{
					negMap[act_id][negPattern] = negs++;

					size = negPattern.Nodes.Count;
					if(size > max_n_negative_nodes)
						max_n_negative_nodes = size;
					size = negPattern.Edges.Count;
					if(size > max_n_negative_edges)
						max_n_negative_edges = size;
				}

				n_negative_patterns[act_id] = negs;
				if(negs > max_n_negative_patterns)
					max_n_negative_patterns = negs;
			}

			/* compute the numbers of nodes/edges of all negative-pattern-graphs */
			negative_node_num = new List<IList<IDictionary<Node, int>>>(n_graph_actions);
			negative_edge_num = new List<IList<IDictionary<Edge, int>>>(n_graph_actions);

			foreach(Rule act in actionRuleMap.Keys)
			{
				int act_id = actionRuleMap[act];

				negative_node_num[act_id] = new List<IDictionary<Node, int>>(max_n_negative_patterns);

				/* if action has negative pattern graphs, compute node/edge numbers */
				foreach(PatternGraphLhs neg_pattern in negMap[act_id].Keys)
				{
					int neg_num = negMap[act_id][neg_pattern];
					negative_node_num[act_id][neg_num] = new Dictionary<Node, int>();
					negative_edge_num[act_id][neg_num] = new Dictionary<Edge, int>();

					/* fill the map with pairs (node, node_num) */
					int node_num = 0;
					foreach(Node node in neg_pattern.Nodes)
						negative_node_num[act_id][neg_num][node] = node_num++;
					Debug.Assert(node_num == neg_pattern.Nodes.Count, "Wrong number of node_nums was created");

					/* fill the map with pairs (edge, edge_num) */
					int edge_num = 0;
					foreach(Edge edge in neg_pattern.Edges)
						negative_edge_num[act_id][neg_num][edge] = edge_num++;
					Debug.Assert(edge_num == neg_pattern.Edges.Count, "Wrong number of edge_nums was created");
				}
			}
		}

		protected internal virtual void CollectPatternNodeIsNegativeNodeInfo()
		{
			patternNodeIsNegativeNode = RectangularArrays.RectangularIntArray(n_graph_actions, max_n_negative_patterns, max_n_pattern_nodes);

			//init the array with -1
			for(int i = 0; i < n_graph_actions; i++)
			{
				for(int j = 0; j < max_n_negative_patterns; j++)
				{
					for(int k = 0; k < max_n_pattern_nodes; k++)
						patternNodeIsNegativeNode[i][j][k] = -1;
				}
			}

			//for all negative patterns insert the correspondig negative node numbers
			//for the pattern nodes that are also present in that negative pattern
			foreach(Rule action in actionRuleMap.Keys)
			{
				int act_id = actionRuleMap[action];

				foreach(PatternGraphLhs neg_pattern in negMap[act_id].Keys)
				{
					int neg_num = negMap[act_id][neg_pattern];

					ISet<Node> negatives_also_in_pattern = new HashSet<Node>();
					negatives_also_in_pattern.AddAll(neg_pattern.Nodes);
					negatives_also_in_pattern.RetainAll(action.Pattern.Nodes);

					foreach(Node node in negatives_also_in_pattern)
					{
						int node_num = pattern_node_num[act_id][node];

						patternNodeIsNegativeNode[act_id][neg_num][node_num] = negative_node_num[act_id][neg_num][node];
					}
				}
			}
		}

		protected internal virtual void CollectPatternEdgeIsNegativeEdgeInfo()
		{
			patternEdgeIsNegativeEdge = RectangularArrays.RectangularIntArray(n_graph_actions, max_n_negative_patterns, max_n_pattern_edges);

			//init the array with -1
			for(int i = 0; i < n_graph_actions; i++)
			{
				for(int j = 0; j < max_n_negative_patterns; j++)
				{
					for(int k = 0; k < max_n_pattern_edges; k++)
						patternEdgeIsNegativeEdge[i][j][k] = -1;
				}
			}

			//for all negative patterns insert the correspondig negative edge numbers
			//for the pattern edges that are also present in that negative pattern
			foreach(Rule action in actionRuleMap.Keys)
			{
				int act_id = actionRuleMap[action];

				foreach(PatternGraphLhs neg_pattern in negMap[act_id].Keys)
				{
					int neg_num = negMap[act_id][neg_pattern];

					ISet<Edge> negatives_also_in_pattern = new HashSet<Edge>();
					negatives_also_in_pattern.AddAll(neg_pattern.Edges);
					negatives_also_in_pattern.RetainAll(action.Pattern.Edges);

					foreach(Edge edge in negatives_also_in_pattern)
					{
						int edge_num = pattern_edge_num[act_id][edge];

						patternEdgeIsNegativeEdge[act_id][neg_num][edge_num] = negative_edge_num[act_id][neg_num][edge];
					}
				}
			}
		}

		protected internal IDictionary<ICollection<InheritanceType>, int> typeConditionsPatternNum = new Dictionary<ICollection<InheritanceType>, int>();
		protected internal IDictionary<Expression, int> conditionsPatternNum = new Dictionary<Expression, int>();

		/* it is a little bit stupid to do this again. so merge it into InformationCollector if it really works */
		protected internal virtual void CollectNegativePatternConditionsInfo()
		{
			//init a subexpression counter
			int subConditionCounter = n_conditions;

			//iterate over all actions
			foreach(Rule act in actionRuleMap.Keys)
			{
				int act_id = actionRuleMap[act];

				//iterate over negative patterns
				foreach(PatternGraphLhs neg_pattern in negMap[act_id].Keys)
				{
					int neg_num = negMap[act_id][neg_pattern];

					//iterate over all conditions of the current action
					foreach(Expression condition in neg_pattern.Conditions)
					{
						// divide the expression to all AND-connected parts, which do
						//not have an AND-Operator as root themselves
						ICollection<Expression> subConditions = DecomposeAndParts(condition);

						//for all the subconditions just computed...
						foreach(Expression sub_condition in subConditions)
						{
							Debug.Assert(conditionNumbers[sub_condition] == null);

							//...create condition numbers
							conditionNumbers[sub_condition] = subConditionCounter++;

							//...extract the pattern nodes and edges involved in the condition
							ICollection<Node> involvedNodes = CollectInvolvedNodes(sub_condition);
							ICollection<Edge> involvedEdges = CollectInvolvedEdges(sub_condition);
							//and at these Collections to prepared Maps
							conditionsInvolvedNodes[sub_condition] = involvedNodes;
							conditionsInvolvedEdges[sub_condition] = involvedEdges;

							//..store the negative pattern num the conditions belongs to
							conditionsPatternNum[sub_condition] = neg_num + 1;

							//store the subcondition in an ordered Collection
							conditions[Convert.ToInt32(act_id)].Add(sub_condition);
						}
					}
				}
			}
			//store the overall number of (sub)conditions
			n_conditions = subConditionCounter;

			/* for all conditions (not type conditions!) the pairs
			 (pattern_node_num, attr_id), which occur
			 in qualifications at the leaves of the condition, are needed.
			 To obtain that compute a map
			 condition_num -> pattern_node_num_ -> { attr_ids }
			 implemented by an Array of Maps; usage is:
			 involvedPatternNodeAttrIds[cond_num].get(pattern_node_num)
			 which yields a Collection of attr-ids.
			 */

			involvedPatternNodeAttrIds = new Dictionary<Expression, IDictionary<Node, ICollection<int>>>();
			involvedPatternEdgeAttrIds = new Dictionary<Expression, IDictionary<Edge, ICollection<int>>>();

			foreach(Rule act in actionRuleMap.Keys)
			{
				int act_id = actionRuleMap[act];

				//collect the attr ids in dependency of condition and the pattern node
				foreach(Expression cond in conditions[Convert.ToInt32(act_id)])
				{
					// TODO use or remove it
					// int cond_num = conditionNumbers.get(cond).intValue();

					//descent to the conditions leaves and look for qualifications
					IDictionary<Node, ICollection<int>> node_map = new Dictionary<Node, ICollection<int>>();
					IDictionary<Edge, ICollection<int>> edge_map = new Dictionary<Edge, ICollection<int>>();
					_recursiveQualCollect(act_id, node_map, edge_map, cond);
					involvedPatternNodeAttrIds[cond] = node_map;
					involvedPatternEdgeAttrIds[cond] = edge_map;
				}
			}
		}

		protected internal virtual void CollectNegativePatternTypeConditionsInfo()
		{
			/* collect the type constraints of the node of all actions pattern graphs */
			int typeConditionCounter = n_conditions;

			foreach(Rule act in actionRuleMap.Keys)
			{
				int act_id = actionRuleMap[act];

				//iterate over negative patterns
				foreach(PatternGraphLhs neg_pattern in negMap[act_id].Keys)
				{
					int neg_num = negMap[act_id][neg_pattern];

					/* for all nodes of the current MatchingActions negative pattern graphs
					 extract that nodes type constraints */
					foreach(Node node in neg_pattern.Nodes)
					{
						//if node has type constraints, register the as conditions
						if(node.Constraints.Count > 0)
						{
							//note that a type condition is the set of all types,
							//the corresponding node/edge is not allowed to be of
							ICollection<InheritanceType> type_condition = node.Constraints;

							//...create condition numbers
							typeConditionNumbers[type_condition] = typeConditionCounter++;

							//...extract the pattern nodes and edges involved in the condition
							ICollection<Node> involvedNodes = new HashSet<Node>();
							involvedNodes.Add(node);
							//and at these Collections to prepared Maps
							typeConditionsInvolvedNodes[type_condition] = involvedNodes;
							ICollection<Edge> empty = Collections.EmptySet();
							typeConditionsInvolvedEdges[type_condition] = empty;

							//..store the negative pattern num the conditions belongs to
							typeConditionsPatternNum[type_condition] = neg_num + 1;

							//store the subcondition in an ordered Collection
							typeConditions[act_id].Add(type_condition);
						}
					}
					//do the same thing for all edges of the current pattern
					foreach(Edge edge in neg_pattern.Edges)
					{
						//if node has type constraints, register the as conditions
						if(edge.Constraints.Count > 0)
						{
							//note that a type condition is the set of all types,
							//the corresponding edge is not allowed to be of
							ICollection<InheritanceType> type_condition = edge.Constraints;

							//...create condition numbers
							typeConditionNumbers[type_condition] = typeConditionCounter++;

							//...extract the pattern edges and edges involved in the condition
							ICollection<Edge> involvedEdges = new HashSet<Edge>();
							involvedEdges.Add(edge);
							//and at these Collections to prepared Maps
							ICollection<Node> empty = Collections.EmptySet();
							typeConditionsInvolvedNodes[type_condition] = empty;
							typeConditionsInvolvedEdges[type_condition] = involvedEdges;

							//..store the negative pattern num the conditions belongs to
							typeConditionsPatternNum[type_condition] = neg_num + 1;

							//store the subcondition in an ordered Collection
							typeConditions[act_id].Add(type_condition);
						}
					}
				}
			}
			//update the overall number of conditions, such that type
			//conditions are also included
			n_conditions = typeConditionCounter;
		}

		protected internal int[] n_subgraphs;
		protected internal int[] first_subgraph;
		protected internal int max_n_subgraphs;
		//protected Map[] subGraphMap;
		protected internal IList<LinkedList<ISet<Node>>> nodesOfSubgraph;
		protected internal IList<LinkedList<ISet<Edge>>> edgesOfSubgraph;
		protected internal IDictionary<Node, int> subgraphOfNode;
		protected internal IDictionary<Edge, int> subgraphOfEdge;

		private void CollectSubGraphInfo()
		{
			n_subgraphs = new int[actionRuleMap.Count];
			first_subgraph = new int[actionRuleMap.Count];
			//subGraphMap = new HashMap[actionMap.size()];
			nodesOfSubgraph = new List<LinkedList<ISet<Node>>>(actionRuleMap.Count);
			edgesOfSubgraph = new List<LinkedList<ISet<Edge>>>(actionRuleMap.Count);
			subgraphOfNode = new Dictionary<Node, int>();
			subgraphOfEdge = new Dictionary<Edge, int>();

			max_n_subgraphs = 0;

			foreach(Rule action in actionRuleMap.Keys)
			{
				PatternGraphLhs pattern = action.Pattern;
				int act_id = actionRuleMap[action];

				int subgraph = 0;

				ISet<IR> remainingNodes = new HashSet<IR>();
				ISet<IR> remainingEdges = new HashSet<IR>();

				remainingNodes.AddAll(pattern.Nodes);
				remainingEdges.AddAll(pattern.Edges);

				nodesOfSubgraph[act_id] = new LinkedList<ISet<Node>>();
				edgesOfSubgraph[act_id] = new LinkedList<ISet<Edge>>();

				n_subgraphs[act_id] = 0;
				//subGraphMap[act_id] = new HashMap();

				while(remainingNodes.Count > 0)
				{
					Node node;
					ISet<Node> currentSubgraphNodes = new HashSet<Node>();
					ISet<Edge> currentSubgraphEdges = new HashSet<Edge>();

					nodesOfSubgraph[act_id].AddLast(currentSubgraphNodes);
					edgesOfSubgraph[act_id].AddLast(currentSubgraphEdges);

					node = (Node)EnumeratorHelper.GetFirstElement(remainingNodes);
					remainingNodes.Remove(node);

					subgraphOfNode[node] = subgraph;
					currentSubgraphNodes.Add(node);

					_deepFirstCollectSubgraphInfo(remainingNodes, remainingEdges, currentSubgraphNodes, currentSubgraphEdges, subgraph, node, action, pattern);

					subgraph++;
				}

				if(nodesOfSubgraph[act_id].Count > 1)
				{
					//if a subgraph is smaller than 5 then merge it with the next smallest one
					ISet<Node> smallest_subgraph;
					ISet<Edge> smallest_subgraph_edges;
					do
					{
						smallest_subgraph = nodesOfSubgraph[act_id].First.Value;
						smallest_subgraph_edges = edgesOfSubgraph[act_id].First.Value;
						Debug.Assert(nodesOfSubgraph[act_id].Count == edgesOfSubgraph[act_id].Count);
						for(int i = 0; i < nodesOfSubgraph[act_id].Count; i++)
						{
							if((nodesOfSubgraph[act_id].ToList()[i]).Count < smallest_subgraph.Count)
							{
								smallest_subgraph = nodesOfSubgraph[act_id].ToList()[i];
								smallest_subgraph_edges = edgesOfSubgraph[act_id].ToList()[i];
							}
						}

						if(smallest_subgraph.Count >= min_subgraph_size)
							break;

	// JAVA TO C# CONVERTER TASK: There is no .NET LinkedList equivalent to the Java 'remove' method:
						bool succ = nodesOfSubgraph[act_id].Remove(smallest_subgraph);
						Debug.Assert(succ);
	// JAVA TO C# CONVERTER TASK: There is no .NET LinkedList equivalent to the Java 'remove' method:
						succ = edgesOfSubgraph[act_id].Remove(smallest_subgraph_edges);
						Debug.Assert(succ);

						ICollection<Node> next_smallest_subgraph = nodesOfSubgraph[act_id].First.Value;
						ICollection<Edge> next_smallest_subgraph_edges = edgesOfSubgraph[act_id].First.Value;
						Debug.Assert(nodesOfSubgraph[act_id].Count == edgesOfSubgraph[act_id].Count);
						for(int i = 0; i < nodesOfSubgraph[act_id].Count; i++)
						{
							if((nodesOfSubgraph[act_id].ToList()[i]).Count < next_smallest_subgraph.Count)
							{
								next_smallest_subgraph = nodesOfSubgraph[act_id].ToList()[i];
								next_smallest_subgraph_edges = edgesOfSubgraph[act_id].ToList()[i];
							}
						}

						//merge the two subgraphs
						next_smallest_subgraph.AddAll(smallest_subgraph);
						next_smallest_subgraph_edges.AddAll(smallest_subgraph_edges);
					} while(nodesOfSubgraph[act_id].Count > 1);

					//move smallest subgraph to the beginning of the list
					smallest_subgraph = nodesOfSubgraph[act_id].First.Value;
					smallest_subgraph_edges = edgesOfSubgraph[act_id].First.Value;
					for(int i = 0; i < nodesOfSubgraph[act_id].Count; i++)
					{
						if((nodesOfSubgraph[act_id].ToList()[i]).Count < smallest_subgraph.Count)
						{
							smallest_subgraph = nodesOfSubgraph[act_id].ToList()[i];
							smallest_subgraph_edges = edgesOfSubgraph[act_id].ToList()[i];
						}
					}
	// JAVA TO C# CONVERTER TASK: There is no .NET LinkedList equivalent to the Java 'remove' method:
					nodesOfSubgraph[act_id].Remove(smallest_subgraph);
	// JAVA TO C# CONVERTER TASK: There is no .NET LinkedList equivalent to the Java 'remove' method:
					edgesOfSubgraph[act_id].Remove(smallest_subgraph_edges);
					nodesOfSubgraph[act_id].AddFirst(smallest_subgraph);
					edgesOfSubgraph[act_id].AddFirst(smallest_subgraph_edges);
				}

				n_subgraphs[act_id] = nodesOfSubgraph[act_id].Count;

				if(max_n_subgraphs < n_subgraphs[act_id])
					max_n_subgraphs = n_subgraphs[act_id];

				for(subgraph = 0; subgraph < edgesOfSubgraph[act_id].Count; subgraph++)
				{
					ICollection<Edge> subgraph_edges = edgesOfSubgraph[act_id].ToList()[subgraph];
					foreach(Edge edge in subgraph_edges)
						subgraphOfEdge[edge] = subgraph;
				}

				for(subgraph = 0; subgraph < nodesOfSubgraph[act_id].Count; subgraph++)
				{
					ICollection<Node> subgraph_nodes = nodesOfSubgraph[act_id].ToList()[subgraph];
					foreach(Node node in subgraph_nodes)
						subgraphOfNode[node] = subgraph;
				}

				int max_prio = 0;
				if(pattern.Nodes.Count > 0)
				{
					//get any node as initial node
					Node max_prio_node = EnumeratorHelper.GetFirstElement(pattern.Nodes);
					foreach(Node node in pattern.Nodes)
					{
						//get the nodes priority
						int prio = 0;
						Annotations a = node.Annotations;
						if(a != null)
						{
							if(a.ContainsKey("prio") && a.IsInteger("prio"))
								prio = ((int?)a.Get("prio")).Value;
						}

						//if the current priority is greater, update the maximum priority node
						if(prio > max_prio)
						{
							max_prio = prio;
							max_prio_node = node;
						}
					}
					first_subgraph[act_id] = subgraphOfNode[max_prio_node];
				}
				else
					first_subgraph[act_id] = 0;
			}
		}

		private void _deepFirstCollectSubgraphInfo(ICollection<IR> remainingNodes, ICollection<IR> remainingEdges, ICollection<Node> currentSubgraphNodes, ICollection<Edge> currentSubgraphEdges, int subgraph, in Node node, MatchingAction action, in PatternGraphLhs pattern)
		{
			//final PatternGraph pattern = action.getPattern();

			//a collection of all edges incident to the current node. The collection
			//is ordered by the priority of the nodes at the far end of each edge.
			//nodes without priority get the priority 0.
			ISet<Edge> incidentEdges = new HashSet<Edge>();

			//put all edges incident to the current node in that collection (edges that are incoming and outgoing occur only once)
			pattern.GetOutgoing(node, incidentEdges);
			pattern.GetIncoming(node, incidentEdges);

			//iterate over all those incident edges...
			foreach(Edge edge in incidentEdges)
			{
				//...and check whether the current edge has already been visited
				if(remainingEdges.Contains(edge))
				{
					//if the edge has not been visited yet mark it as visited
					currentSubgraphEdges.Add(edge);

					//mark the current edge as visited
					remainingEdges.Remove(edge);

					//if the far node is not yet visited follow the current edge to
					//continue the deep first traversal
					if(remainingNodes.Contains(GetFarEndNode(edge, node, pattern)))
					{
						//mark the edge and the far end node as visited
						currentSubgraphNodes.Add(GetFarEndNode(edge, node, pattern));

						remainingNodes.Remove(GetFarEndNode(edge, node, pattern));
						//continue recursicly the deep fisrt traversal of the pattern graph
						_deepFirstCollectSubgraphInfo(remainingNodes, remainingEdges, currentSubgraphNodes, currentSubgraphEdges, subgraph, GetFarEndNode(edge, node, pattern), action, pattern);
					}
				}
			}
		}

		private static Node GetFarEndNode(Edge e, Node fromNode, PatternGraphBase graph)
		{
			Node farEndNode = null;
			if(graph.GetTarget(e) == fromNode)
				farEndNode = graph.GetSource(e);
			if(graph.GetSource(e) == fromNode)
				farEndNode = graph.GetTarget(e);

			return farEndNode;
		}

		protected internal override void CollectActionInfo()
		{
			base.CollectActionInfo();
			CollectPatternEdgesToBeKeptInfo();
			CollectReplacementEdgeIsPreservedEdgeInfo();
			CollectNegativeInfo();
			CollectPatternNodeIsNegativeNodeInfo();
			CollectPatternEdgeIsNegativeEdgeInfo();
			CollectNegativePatternConditionsInfo();
			CollectNegativePatternTypeConditionsInfo();
			CollectSubGraphInfo();
		}
	}

}
