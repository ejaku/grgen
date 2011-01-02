/*
 * GrGen: graph rewrite generator tool -- release GrGen.NET 2.7
 * Copyright (C) 2003-2011 Universitaet Karlsruhe, Institut fuer Programmstrukturen und Datenorganisation, LS Goos
 * licensed under LGPL v3 (see LICENSE.txt included in the packaging of this file)
 * www.grgen.net
 */

/**
 * Extracts all the information needed by the FrameBasedBackend
 * from the GrGen-internal IR
 *
 * @author Adam Szalkowski
 * @version $Id$
 */

package de.unika.ipd.grgen.be.C;

import java.util.Collection;
import java.util.Collections;
import java.util.HashMap;
import java.util.HashSet;
import java.util.LinkedList;
import java.util.Map;
import java.util.Vector;

import de.unika.ipd.grgen.ir.Action;
import de.unika.ipd.grgen.ir.Assignment;
import de.unika.ipd.grgen.ir.Edge;
import de.unika.ipd.grgen.ir.EvalStatement;
import de.unika.ipd.grgen.ir.Expression;
import de.unika.ipd.grgen.ir.Graph;
import de.unika.ipd.grgen.ir.IR;
import de.unika.ipd.grgen.ir.InheritanceType;
import de.unika.ipd.grgen.ir.MatchingAction;
import de.unika.ipd.grgen.ir.Node;
import de.unika.ipd.grgen.ir.Operator;
import de.unika.ipd.grgen.ir.PatternGraph;
import de.unika.ipd.grgen.ir.PrimitiveType;
import de.unika.ipd.grgen.ir.Qualification;
import de.unika.ipd.grgen.ir.Rule;
import de.unika.ipd.grgen.util.Annotations;

public class MoreInformationCollector extends InformationCollector {

	/* maps an eval list to the action_id it belong to */
	protected Map<Collection<Assignment>, Integer> evalListMap = new HashMap<Collection<Assignment>, Integer>();

	/* replacement and pattern nodes/edges involved in an eval */
	protected Map<Collection<Assignment>, Collection<Node>> evalInvolvedNodes = new HashMap<Collection<Assignment>, Collection<Node>>();
	protected Map<Collection<Assignment>, Collection<Edge>> evalInvolvedEdges = new HashMap<Collection<Assignment>, Collection<Edge>>();

	/* maps action id to eval list */
	protected Map<Collection<Assignment>, Action> evalActions = new HashMap<Collection<Assignment>, Action>();

	/* edge and node attributes involved in that eval */
	protected Vector<Map<Node, Collection<Integer>>> involvedEvalNodeAttrIds;
	protected Vector<Map<Edge, Collection<Integer>>> involvedEvalEdgeAttrIds;

	//returns id of corresponding pattern edge id if edge is kept
	//else -1 if edge is new one
	//usage: replacementEdgeIsPresevedNode[act_id][replacement_edge_num]
	protected int[][] replacementEdgeIsPreservedEdge;

	//returns id of corresponding replacement edge id if edge is kept
	//else -1 if edge is to be deleted
	//usage: patternEdgeIsToBeKept[act_id][pattern_edge_num]
	protected int[][] patternEdgeIsToBeKept;

	private static final int min_subgraph_size = 4;


	/*
	 * collect some information about evals
	 *
	 */
	protected void collectEvalInfo() {
		involvedEvalNodeAttrIds = new Vector<Map<Node,Collection<Integer>>>(actionRuleMap.size());
		involvedEvalEdgeAttrIds = new Vector<Map<Edge,Collection<Integer>>>(actionRuleMap.size());

		for(Rule act : actionRuleMap.keySet()) {
			if (act.getRight() != null) {
				Integer act_id = actionRuleMap.get(act);

				Collection<Assignment> rule_evals = new LinkedList<Assignment>();
				for(EvalStatement evalStmt : act.getEvals()) {
					if(evalStmt instanceof Assignment)
						rule_evals.add((Assignment) evalStmt);
				}

				evalListMap.put( rule_evals, act_id );
				evalActions.put( rule_evals, act );

				Collection<Node> involvedNodes = new HashSet<Node>();
				Collection<Edge> involvedEdges = new HashSet<Edge>();
				involvedEvalNodeAttrIds.set(act_id.intValue(), new HashMap<Node, Collection<Integer>>());
				involvedEvalEdgeAttrIds.set(act_id.intValue(), new HashMap<Edge, Collection<Integer>>());

				for(Assignment eval : rule_evals) {
					Expression targetExpr = eval.getTarget();
					if(!(targetExpr instanceof Qualification))
						throw new UnsupportedOperationException(
							"The C backend only supports assignments to qualified expressions, yet!");
					Qualification target = (Qualification) targetExpr;
					Expression expr = eval.getExpression();

					/* generate an expression that consists of both parts of the Assignment to use the already implemented methods for gathering InvolvedNodes/Edges etc. */
					Operator op = new Operator( (PrimitiveType)target.getType(), Operator.EQ );
					op.addOperand( target );
					op.addOperand( expr );

					//...extract the pattern nodes and edges involved in the evaluation
					involvedNodes.addAll( collectInvolvedNodes( op ) );
					involvedEdges.addAll( collectInvolvedEdges( op ) );

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
					__recursive_qual_collect( act_id.intValue(),
											 involvedEvalNodeAttrIds.get(act_id.intValue()),
											 involvedEvalEdgeAttrIds.get(act_id.intValue()),
											 op );
				}

				//add Collections of involved Nodes/Edges to prepared Maps
				evalInvolvedNodes.put( rule_evals, involvedNodes );
				evalInvolvedEdges.put( rule_evals, involvedEdges );
			}
		}
	}

	/**
	 * Method collectReplacementEdgeIsPreservedEdgeInfo
	 *
	 */
	private void collectReplacementEdgeIsPreservedEdgeInfo() {
		replacementEdgeIsPreservedEdge =
			new int[n_graph_actions][max_n_replacement_edges];

		//init the array with -1
		for (int i = 0; i < n_graph_actions; i++)
			for (int j = 0; j < max_n_replacement_edges; j++)
				replacementEdgeIsPreservedEdge[i][j] = -1;

		//for all edges preserved set the corresponding array entry to the
		//appropriate pattern edge number
		for (Rule action :  actionRuleMap.keySet()) {
			int act_id = actionRuleMap.get(action).intValue();

			if (action.getRight() != null) {
				//compute the set of replacement edges preserved by this action
				Collection<Edge> replacement_edges_preserved = new HashSet<Edge>();
				replacement_edges_preserved.addAll(action.getRight().getEdges());
				replacement_edges_preserved.retainAll(action.getPattern().getEdges());
				//for all those preserved replacement edges store the
				//corresponding pattern edge
				for (Edge edge : replacement_edges_preserved) {
					int edge_num =
						replacement_edge_num.get(act_id).get(edge).intValue();
					replacementEdgeIsPreservedEdge[act_id][edge_num] =
						pattern_edge_num.get(act_id).get(edge).intValue();
				}
			}
		}
	}

	/**
	 * Method collectPatternEdgesToBeKeptInfo
	 *
	 */
	private void collectPatternEdgesToBeKeptInfo() {
		patternEdgeIsToBeKept = new int[n_graph_actions][max_n_pattern_edges];

		//init the arrays with -1
		for (int i = 0; i < n_graph_actions; i++)
			for (int j = 0; j < max_n_pattern_edges; j++)
				patternEdgeIsToBeKept[i][j] = -1;

		//for all edges to be kept set the corresponding array entry to the
		//appropriate replacement edge number
		for (Rule action : actionRuleMap.keySet()) {
			int act_id = actionRuleMap.get(action).intValue();

			//compute the set of pattern edges to be kept for this action
			Collection<Edge> pattern_edges_to_keep = new HashSet<Edge>();
			pattern_edges_to_keep.addAll(action.getPattern().getEdges());
			if (action.getRight() != null) {
				Graph replacement = action.getRight();
				pattern_edges_to_keep.retainAll(replacement.getEdges());
				//iterate over the pattern edges to be kept and store their
				//corresponding replacement edge number
				for (Edge edge : pattern_edges_to_keep) {
					int edge_num =
						pattern_edge_num.get(act_id).get(edge).intValue();
					patternEdgeIsToBeKept[act_id][edge_num] =
						replacement_edge_num.get(act_id).get(edge).intValue();
				}
			}
		}
	}

	protected int max_n_negative_nodes = 0;
	protected int max_n_negative_edges = 0;
	protected int max_n_negative_patterns = 0;
	protected int[] n_negative_patterns;
	//	action_id --> neg_id --> pattern_node_num
	protected Vector<Vector<Map<Node,Integer>>> negative_node_num;
	//	action_id --> neg_id --> pattern_edge_num
	protected Vector<Vector<Map<Edge,Integer>>> negative_edge_num;
	protected Vector<Map<PatternGraph,Integer>> negMap;

	protected int[][][] patternNodeIsNegativeNode;
	protected int[][][] patternEdgeIsNegativeEdge;


	private void collectNegativeInfo() {
		/* get the overall maximum numbers of nodes and edges of all pattern
		 and replacement graphs respectively */
		max_n_negative_nodes = 0;
		max_n_negative_edges = 0;
		max_n_negative_patterns = 0;

		n_negative_patterns = new int[n_graph_actions];
		negMap = new Vector<Map<PatternGraph,Integer>>(n_graph_actions);

		for(Rule act : actionRuleMap.keySet()) {
			int act_id = actionRuleMap.get(act).intValue();
			int negs = 0;

			negMap.set(act_id, new HashMap<PatternGraph,Integer>());

			//check whether its graphs node and edge set sizes are greater
			int size;

			for(PatternGraph negPattern : act.getPattern().getNegs()) {
				negMap.get(act_id).put(negPattern, new Integer(negs++));

				size = negPattern.getNodes().size();
				if (size > max_n_negative_nodes)	max_n_negative_nodes = size;
				size = negPattern.getEdges().size();
				if (size > max_n_negative_edges) max_n_negative_edges = size;
			}

			n_negative_patterns[act_id] = negs;
			if (negs > max_n_negative_patterns) max_n_negative_patterns = negs;
		}


		/* compute the numbers of nodes/edges of all negative-pattern-graphs */
		negative_node_num = new Vector<Vector<Map<Node,Integer>>>(n_graph_actions);
		negative_edge_num = new Vector<Vector<Map<Edge,Integer>>>(n_graph_actions);

		for(Rule act : actionRuleMap.keySet()) {
			int act_id = actionRuleMap.get(act).intValue();

			negative_node_num.set(act_id, new Vector<Map<Node,Integer>>(max_n_negative_patterns));

			/* if action has negative pattern graphs, compute node/edge numbers */
			if (act instanceof MatchingAction) {

				for(PatternGraph neg_pattern : negMap.get(act_id).keySet()) {
					int neg_num = negMap.get(act_id).get(neg_pattern).intValue();
					negative_node_num.get(act_id).set(neg_num, new HashMap<Node,Integer>());
					negative_edge_num.get(act_id).set(neg_num, new HashMap<Edge,Integer>());

					/* fill the map with pairs (node, node_num) */
					int node_num = 0;
					for (Node node : neg_pattern.getNodes()) {
						negative_node_num.get(act_id).get(neg_num).put(node, new Integer(node_num++));
					}
					assert node_num == neg_pattern.getNodes().size():
						"Wrong number of node_nums was created";

					/* fill the map with pairs (edge, edge_num) */
					int edge_num = 0;
					for (Edge edge :neg_pattern.getEdges()) {
						negative_edge_num.get(act_id).get(neg_num).put(edge, new Integer(edge_num++));
					}
					assert edge_num == neg_pattern.getEdges().size():
						"Wrong number of edge_nums was created";
				}
			} else {
				//negative_node_num[act_id][neg_num] = null;
				//negative_edge_num[act_id][neg_num] = null;
			}
		}
	}

	protected void collectPatternNodeIsNegativeNodeInfo() {
		patternNodeIsNegativeNode =
			new int[n_graph_actions][max_n_negative_patterns][max_n_pattern_nodes];

		//init the array with -1
		for (int i = 0; i < n_graph_actions; i++)
			for (int j = 0; j < max_n_negative_patterns; j++)
				for (int k = 0; k < max_n_pattern_nodes; k++)
					patternNodeIsNegativeNode[i][j][k] = -1;

		//for all negative patterns insert the correspondig negative node numbers
		//for the pattern nodes that are also present in that negative pattern
		for (Rule action : actionRuleMap.keySet()) {
			int act_id = actionRuleMap.get(action).intValue();

			for(PatternGraph neg_pattern : negMap.get(act_id).keySet()) {
				int neg_num = negMap.get(act_id).get(neg_pattern).intValue();

				Collection<Node> negatives_also_in_pattern = new HashSet<Node>();
				negatives_also_in_pattern.addAll( neg_pattern.getNodes() );
				negatives_also_in_pattern.retainAll( action.getPattern().getNodes() );

				for(Node node : negatives_also_in_pattern) {
					int node_num = pattern_node_num.get(act_id).get(node).intValue();

					patternNodeIsNegativeNode[act_id][neg_num][node_num] =
						negative_node_num.get(act_id).get(neg_num).get(node).intValue();
				}
			}
		}
	}

	protected void collectPatternEdgeIsNegativeEdgeInfo() {
		patternEdgeIsNegativeEdge =
			new int[n_graph_actions][max_n_negative_patterns][max_n_pattern_edges];

		//init the array with -1
		for (int i = 0; i < n_graph_actions; i++)
			for (int j = 0; j < max_n_negative_patterns; j++)
				for (int k = 0; k < max_n_pattern_edges; k++)
					patternEdgeIsNegativeEdge[i][j][k] = -1;

		//for all negative patterns insert the correspondig negative edge numbers
		//for the pattern edges that are also present in that negative pattern
		for (Rule action : actionRuleMap.keySet()) {
			int act_id = actionRuleMap.get(action).intValue();

			if (action instanceof MatchingAction) {

				for(PatternGraph neg_pattern : negMap.get(act_id).keySet()) {
					int neg_num = negMap.get(act_id).get(neg_pattern).intValue();

					Collection<Edge> negatives_also_in_pattern = new HashSet<Edge>();
					negatives_also_in_pattern.addAll( neg_pattern.getEdges() );
					negatives_also_in_pattern.retainAll( action.getPattern().getEdges() );

					for(Edge edge : negatives_also_in_pattern) {
						int edge_num = pattern_edge_num.get(act_id).get(edge).intValue();

						patternEdgeIsNegativeEdge[act_id][neg_num][edge_num] =
							negative_edge_num.get(act_id).get(neg_num).get(edge).intValue();
					}
				}
			}
		}
	}

	protected Map<Collection<InheritanceType>, Integer> typeConditionsPatternNum = new HashMap<Collection<InheritanceType>, Integer>();
	protected Map<Expression, Integer> conditionsPatternNum = new HashMap<Expression, Integer>();

	/* it is a little bit stupid to do this again. so merge it into InformationCollector if it really works */
	protected void collectNegativePatternConditionsInfo() {

		//init a subexpression counter
		int subConditionCounter = n_conditions;

		//iterate over all actions
		for(Rule act : actionRuleMap.keySet()) {
			int act_id = actionRuleMap.get(act).intValue();

			//iterate over negative patterns
			for(PatternGraph neg_pattern : negMap.get(act_id).keySet()) {
				int neg_num = negMap.get(act_id).get(neg_pattern).intValue();

				//iterate over all conditions of the current action
				for (Expression condition : neg_pattern.getConditions()) {
					// divide the expression to all AND-connected parts, which do
					//not have an AND-Operator as root themselves
					Collection<Expression> subConditions = decomposeAndParts(condition);

					//for all the subconditions just computed...
					for (Expression sub_condition : subConditions) {
						assert conditionNumbers.get(sub_condition) == null;

						//...create condition numbers
						conditionNumbers.put(sub_condition, new Integer(subConditionCounter++));

						//...extract the pattern nodes and edges involved in the condition
						Collection<Node> involvedNodes = collectInvolvedNodes(sub_condition);
						Collection<Edge> involvedEdges = collectInvolvedEdges(sub_condition);
						//and at these Collections to prepared Maps
						conditionsInvolvedNodes.put(sub_condition, involvedNodes);
						conditionsInvolvedEdges.put(sub_condition, involvedEdges);

						//..store the negative pattern num the conditions belongs to
						conditionsPatternNum.put(sub_condition, new Integer(neg_num+1));

						//store the subcondition in an ordered Collection
						conditions.get(act_id).add(sub_condition);
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

		involvedPatternNodeAttrIds = new HashMap<Expression, Map<Node,Collection<Integer>>>();
		involvedPatternEdgeAttrIds = new HashMap<Expression, Map<Edge,Collection<Integer>>>();

		for(Rule act : actionRuleMap.keySet())
		{
			int act_id = actionRuleMap.get(act).intValue();

			//collect the attr ids in dependency of condition and the pattern node
			for (Expression cond : conditions.get(act_id))
			{
				// TODO use or remove it
				// int cond_num = conditionNumbers.get(cond).intValue();

				//descent to the conditions leaves and look for qualifications
				Map<Node, Collection<Integer>> node_map = new HashMap<Node, Collection<Integer>>();
				Map<Edge, Collection<Integer>> edge_map = new HashMap<Edge, Collection<Integer>>();
				__recursive_qual_collect(act_id, node_map, edge_map, cond);
				involvedPatternNodeAttrIds.put(cond,node_map);
				involvedPatternEdgeAttrIds.put(cond,edge_map);
			}
		}
	}

	protected void collectNegativePatternTypeConditionsInfo() {
		/* collect the type constraints of the node of all actions pattern graphs */
		int typeConditionCounter = n_conditions;

		for(Rule act : actionRuleMap.keySet()) {
			int act_id = actionRuleMap.get(act).intValue();

			if (act instanceof MatchingAction) {

				//iterate over negative patterns
				for(PatternGraph neg_pattern : negMap.get(act_id).keySet()) {
					int neg_num = negMap.get(act_id).get(neg_pattern).intValue();

					/* for all nodes of the current MatchingActions negative pattern graphs
					 extract that nodes type constraints */
					for (Node node : neg_pattern.getNodes()) {
						//if node has type constraints, register the as conditions
						if (! node.getConstraints().isEmpty()) {

							//note that a type condition is the set of all types,
							//the corresponding node/edge is not allowed to be of
							Collection<InheritanceType> type_condition = node.getConstraints();

							//...create condition numbers
							typeConditionNumbers.put(type_condition, new Integer(typeConditionCounter++));

							//...extract the pattern nodes and edges involved in the condition
							Collection<Node> involvedNodes = new HashSet<Node>();
							involvedNodes.add(node);
							//and at these Collections to prepared Maps
							typeConditionsInvolvedNodes.put(type_condition, involvedNodes);
							Collection<Edge> empty = Collections.emptySet();
							typeConditionsInvolvedEdges.put(type_condition, empty);

							//..store the negative pattern num the conditions belongs to
							typeConditionsPatternNum.put(type_condition, new Integer(neg_num+1));

							//store the subcondition in an ordered Collection
							typeConditions.get(act_id).add(type_condition);
						}
					}
					//do the same thing for all edges of the current pattern
					for (Edge edge : neg_pattern.getEdges()) {
						//if node has type constraints, register the as conditions
						if (! edge.getConstraints().isEmpty()) {

							//note that a type condition is the set of all types,
							//the corresponding edge is not allowed to be of
							Collection<InheritanceType> type_condition = edge.getConstraints();

							//...create condition numbers
							typeConditionNumbers.put(type_condition, new Integer(typeConditionCounter++));

							//...extract the pattern edges and edges involved in the condition
							Collection<Edge> involvedEdges = new HashSet<Edge>();
							involvedEdges.add(edge);
							//and at these Collections to prepared Maps
							Collection<Node> empty = Collections.emptySet();
							typeConditionsInvolvedNodes.put(type_condition, empty);
							typeConditionsInvolvedEdges.put(type_condition, involvedEdges);

							//..store the negative pattern num the conditions belongs to
							typeConditionsPatternNum.put(type_condition, new Integer(neg_num+1));

							//store the subcondition in an ordered Collection
							typeConditions.get(act_id).add(type_condition);
						}
					}
				}
			}
		}
		//update the overall number of conditions, such that type
		//conditions are also included
		n_conditions = typeConditionCounter;

	}

	protected int[] n_subgraphs;
	protected int[] first_subgraph;
	protected int max_n_subgraphs;
	//protected Map[] subGraphMap;
	protected Vector<LinkedList<Collection<Node>>> nodesOfSubgraph;
	protected Vector<LinkedList<Collection<Edge>>> edgesOfSubgraph;
	protected Map<Node, Integer> subgraphOfNode;
	protected Map<Edge, Integer> subgraphOfEdge;

	private void collectSubGraphInfo() {
		n_subgraphs = new int[actionRuleMap.size()];
		first_subgraph = new int[actionRuleMap.size()];
		//subGraphMap = new HashMap[actionMap.size()];
		nodesOfSubgraph = new Vector<LinkedList<Collection<Node>>>(actionRuleMap.size());
		edgesOfSubgraph = new Vector<LinkedList<Collection<Edge>>>(actionRuleMap.size());
		subgraphOfNode = new HashMap<Node, Integer>();
		subgraphOfEdge = new HashMap<Edge, Integer>();

		max_n_subgraphs = 0;


		for (Rule action : actionRuleMap.keySet()) {
			PatternGraph pattern = action.getPattern();
			int act_id = actionRuleMap.get(action).intValue();

			int subgraph = 0;

			Collection<IR> remainingNodes = new HashSet<IR>();
			Collection<IR> remainingEdges = new HashSet<IR>();

			remainingNodes.addAll( pattern.getNodes() );
			remainingEdges.addAll( pattern.getEdges() );

			nodesOfSubgraph.set(act_id, new LinkedList<Collection<Node>>());
			edgesOfSubgraph.set(act_id, new LinkedList<Collection<Edge>>());

			n_subgraphs[act_id] = 0;
			//subGraphMap[act_id] = new HashMap();


			while( !remainingNodes.isEmpty() ) {
				Node node;
				Collection<Node> currentSubgraphNodes = new HashSet<Node>();
				Collection<Edge> currentSubgraphEdges = new HashSet<Edge>();

				nodesOfSubgraph.get(act_id).addLast( currentSubgraphNodes );
				edgesOfSubgraph.get(act_id).addLast( currentSubgraphEdges );


				node = (Node)remainingNodes.iterator().next();
				remainingNodes.remove(node);

				subgraphOfNode.put( node, new Integer(subgraph) );
				currentSubgraphNodes.add(node);

				__deep_first_collect_subgraph_info(remainingNodes, remainingEdges, currentSubgraphNodes, currentSubgraphEdges, subgraph, node, action, pattern);

				subgraph++;
			}


			if(nodesOfSubgraph.get(act_id).size() > 1) {
				//if a subgraph is smaller than 5 then merge it with the next smallest one
				Collection<Node> smallest_subgraph;
				Collection<Edge> smallest_subgraph_edges;
				do {
					smallest_subgraph = nodesOfSubgraph.get(act_id).getFirst();
					smallest_subgraph_edges = edgesOfSubgraph.get(act_id).getFirst();
					assert nodesOfSubgraph.get(act_id).size() ==  edgesOfSubgraph.get(act_id).size();
					for(int i=0; i<nodesOfSubgraph.get(act_id).size(); i++) {
						if((nodesOfSubgraph.get(act_id).get(i)).size() < smallest_subgraph.size()) {
							smallest_subgraph = nodesOfSubgraph.get(act_id).get(i);
							smallest_subgraph_edges = edgesOfSubgraph.get(act_id).get(i);
						}
					}

					if(smallest_subgraph.size() >= min_subgraph_size)
						break;

					boolean succ = nodesOfSubgraph.get(act_id).remove(smallest_subgraph);
					assert succ;
					succ = edgesOfSubgraph.get(act_id).remove(smallest_subgraph_edges);
					assert succ;



					Collection<Node> next_smallest_subgraph = nodesOfSubgraph.get(act_id).getFirst();
					Collection<Edge> next_smallest_subgraph_edges = edgesOfSubgraph.get(act_id).getFirst();
					assert nodesOfSubgraph.get(act_id).size() ==  edgesOfSubgraph.get(act_id).size();
					for(int i=0; i<nodesOfSubgraph.get(act_id).size(); i++) {
						if((nodesOfSubgraph.get(act_id).get(i)).size() < next_smallest_subgraph.size()) {
							next_smallest_subgraph = nodesOfSubgraph.get(act_id).get(i);
							next_smallest_subgraph_edges = edgesOfSubgraph.get(act_id).get(i);
						}
					}

					//merge the two subgraphs
					next_smallest_subgraph.addAll(smallest_subgraph);
					next_smallest_subgraph_edges.addAll(smallest_subgraph_edges);
				} while(nodesOfSubgraph.get(act_id).size() > 1);

				//move smallest subgraph to the beginning of the list
				smallest_subgraph = nodesOfSubgraph.get(act_id).getFirst();
				smallest_subgraph_edges = edgesOfSubgraph.get(act_id).getFirst();
				for(int i=0; i<nodesOfSubgraph.get(act_id).size(); i++) {
					if((nodesOfSubgraph.get(act_id).get(i)).size() < smallest_subgraph.size()) {
						smallest_subgraph = nodesOfSubgraph.get(act_id).get(i);
						smallest_subgraph_edges = edgesOfSubgraph.get(act_id).get(i);
					}
				}
				nodesOfSubgraph.get(act_id).remove(smallest_subgraph);
				edgesOfSubgraph.get(act_id).remove(smallest_subgraph_edges);
				nodesOfSubgraph.get(act_id).addFirst(smallest_subgraph);
				edgesOfSubgraph.get(act_id).addFirst(smallest_subgraph_edges);
			}

			n_subgraphs[act_id] = nodesOfSubgraph.get(act_id).size();


			if(max_n_subgraphs < n_subgraphs[act_id])
				max_n_subgraphs = n_subgraphs[act_id];

			for(subgraph = 0; subgraph < edgesOfSubgraph.get(act_id).size(); subgraph++) {
				Collection<Edge> subgraph_edges = edgesOfSubgraph.get(act_id).get(subgraph);
				for(Edge edge : subgraph_edges) {
					subgraphOfEdge.put( edge, new Integer(subgraph) );
				}
			}

			for(subgraph = 0; subgraph < nodesOfSubgraph.get(act_id).size(); subgraph++) {
				Collection<Node> subgraph_nodes = nodesOfSubgraph.get(act_id).get(subgraph);
				for(Node node : subgraph_nodes) {
					subgraphOfNode.put( node, new Integer(subgraph) );
				}
			}



			int max_prio = 0;
			if(pattern.getNodes().size() > 0) {
				//get any node as initial node
				Node max_prio_node = pattern.getNodes().iterator().next();
				for (Node node : pattern.getNodes()) {
					//get the nodes priority
					int prio = 0;
					Annotations a = node.getAnnotations();
					if (a != null)
						if (a.containsKey("prio") && a.isInteger("prio"))
							prio = ((Integer) a.get("prio")).intValue();

					//if the current priority is greater, update the maximum priority node
					if (prio > max_prio) {
						max_prio = prio;
						max_prio_node = node;
					}
				}
				first_subgraph[act_id] = ((Integer)subgraphOfNode.get(max_prio_node)).intValue();
			} else {
				first_subgraph[act_id] = 0;
			}
		}
	}

	private void __deep_first_collect_subgraph_info(
		Collection<IR> remainingNodes, Collection<IR> remainingEdges,
		Collection<Node> currentSubgraphNodes, Collection<Edge> currentSubgraphEdges,
		int subgraph,
		final Node node, MatchingAction action,
		final PatternGraph pattern) {
		//final PatternGraph pattern = action.getPattern();

		//a collection of all edges incident to the current node. The collection
		//is ordered by the priority of the nodes at the far end of each edge.
		//nodes without priority get the priority 0.
		Collection<Edge> incidentEdges = new HashSet<Edge>();

		//put all edges incident to the current node in that collection
		pattern.getOutgoing(node, incidentEdges);
		pattern.getIncoming(node, incidentEdges);

		//iterate over all those incident edges...
		for (Edge edge : incidentEdges) {
			//...and check whether the current edge has already been visited
			if ( remainingEdges.contains(edge) ) {

				//if the edge has not been visited yet mark it as visited
				currentSubgraphEdges.add(edge);

				//mark the current edge as visited
				remainingEdges.remove(edge);

				//if the far node is not yet visited follow the current edge to
				//continue the deep first traversal
				if ( remainingNodes.contains(getFarEndNode(edge, node, pattern)) ) {

					//mark the edge and the far end node as visited
					currentSubgraphNodes.add(getFarEndNode(edge, node, pattern));

					remainingNodes.remove(getFarEndNode(edge, node, pattern));
					//continue recursicly the deep fisrt traversal of the pattern graph
					__deep_first_collect_subgraph_info(remainingNodes, remainingEdges, currentSubgraphNodes, currentSubgraphEdges, subgraph, getFarEndNode(edge, node, pattern), action, pattern);
				}
			}
		}
	}

	private Node getFarEndNode(Edge e, Node fromNode, Graph graph) {
		Node farEndNode = null;
		if (graph.getTarget(e) == fromNode)
			farEndNode = graph.getSource(e);
		if (graph.getSource(e) == fromNode)
			farEndNode = graph.getTarget(e);

		return farEndNode;
	}

	protected void collectActionInfo() {
		super.collectActionInfo();
		collectPatternEdgesToBeKeptInfo();
		collectReplacementEdgeIsPreservedEdgeInfo();
		collectNegativeInfo();
		collectPatternNodeIsNegativeNodeInfo();
		collectPatternEdgeIsNegativeEdgeInfo();
		collectNegativePatternConditionsInfo();
		collectNegativePatternTypeConditionsInfo();
		collectSubGraphInfo();
    }
}


