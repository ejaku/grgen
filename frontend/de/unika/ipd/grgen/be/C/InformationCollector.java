/*
 * GrGen: graph rewrite generator tool -- release GrGen.NET 3.0
 * Copyright (C) 2003-2011 Universitaet Karlsruhe, Institut fuer Programmstrukturen und Datenorganisation, LS Goos
 * licensed under LGPL v3 (see LICENSE.txt included in the packaging of this file)
 * www.grgen.net
 */

/**
 * Extracts all the information needed by the FrameBasedBackend
 * from the GrGen-internal IR
 *
 * @author Veit Batz
 * @version $Id$
 */

package de.unika.ipd.grgen.be.C;

import java.io.PrintStream;
import java.util.Collection;
import java.util.Collections;
import java.util.Comparator;
import java.util.HashMap;
import java.util.HashSet;
import java.util.Map;
import java.util.TreeSet;
import java.util.Vector;

import de.unika.ipd.grgen.ir.BooleanType;
import de.unika.ipd.grgen.ir.Edge;
import de.unika.ipd.grgen.ir.EdgeType;
import de.unika.ipd.grgen.ir.Entity;
import de.unika.ipd.grgen.ir.EnumItem;
import de.unika.ipd.grgen.ir.EnumType;
import de.unika.ipd.grgen.ir.Expression;
import de.unika.ipd.grgen.ir.Graph;
import de.unika.ipd.grgen.ir.InheritanceType;
import de.unika.ipd.grgen.ir.IntType;
import de.unika.ipd.grgen.ir.MatchingAction;
import de.unika.ipd.grgen.ir.Node;
import de.unika.ipd.grgen.ir.NodeType;
import de.unika.ipd.grgen.ir.Operator;
import de.unika.ipd.grgen.ir.PatternGraph;
import de.unika.ipd.grgen.ir.Qualification;
import de.unika.ipd.grgen.ir.Rule;
import de.unika.ipd.grgen.ir.StringType;
import de.unika.ipd.grgen.ir.Type;
import de.unika.ipd.grgen.util.Annotations;

public class InformationCollector extends CBackend {


	/* some information extracted from the grg-fiel, collected
	 during the generation process */
	/* overall number of types, attrs, ... */
	protected int n_node_types;
	protected int n_edge_types;
	protected int n_enum_types;
	protected int n_node_attrs;
	protected int n_edge_attrs;

	/* inheritance information of node and edge types */
	protected short[][] node_is_a_matrix;
	protected short[][] edge_is_a_matrix;

	/* the number of attributes a node/edge type has */
	protected int[] n_attr_of_node_type;
	protected int[] n_attr_of_edge_type;

	/* information describing node and edge attributes */
	protected AttrTypeDescriptor[] node_attr_info;
	protected AttrTypeDescriptor[] edge_attr_info;

	/* during gen process an attr layout of each node/edge type will
	 be computed, the result is stored in these two arrays:
	 node_attr_index[nt_id][attr_id] = n  means that for the given
	 node type and given attr the attr value is stored at pos n*/
	protected int[][] node_attr_index;
	protected int[][] edge_attr_index;

	/* array of objects describing the rnum types declared in the grg file */
	protected EnumDescriptor[] enum_type_descriptors;

	/* the overall number of graph actions */
	protected int n_graph_actions;

	/* overall number off conditions of all pattern graphs */
	protected int n_conditions;

	/* overall max number of some things */
	protected int max_n_pattern_nodes;
	protected int max_n_pattern_edges;
	protected int max_n_replacement_nodes;
	protected int max_n_replacement_edges;

	/* a map  action_id --> node --> pattern_node_num, e.g
	 pattern_node_num[act_id].get(someNode)
	 yields an Integer object wrapping node number for an fb_acts_graph_t */
	protected Vector<Map<Node,Integer>> pattern_node_num;
	/* the same, but edges */
	protected Vector<Map<Edge,Integer>> pattern_edge_num;
	/* just like above, but for the replacement graph if the given action has one
	 otherwise the array yields a null pointer instead of a map */
	protected Vector<Map<Node,Integer>> replacement_node_num;
	protected Vector<Map<Edge,Integer>> replacement_edge_num;

	/* realizes a map
	 cond_num -> pattern_node_num -> Collection_of_attr_ids,
	 i.e. yields a Collection of attr-ids occuring in Qualification expressions
	 of the given condition together with the given pattern node. Usage:
	 involvedPatternNodeAttrIds[cond_num].get(pattern_node_num) */
	protected Map<Expression, Map<Node,Collection<Integer>>> involvedPatternNodeAttrIds;
	/* just the same, but edge attrs */
	protected Map<Expression, Map<Edge,Collection<Integer>>> involvedPatternEdgeAttrIds;

	/* the conditions of the pattern graphs are decomposed into subexpression
	 down to sub expressions, which are not AND-Operations. These "conjunctive"
	 parts are the units the fb backend is working with */
	/* collects all these conjuctive parts; to assure that the conditions
	 can easily be processed in the order of their condition numbers, this
	 Collection will be initialized with an TreeSet parametrised with a
	 Comparator comparing by condition numbers */
	protected HashMap<Integer, Collection<Expression>> conditions;
	/* maps a subcondition to the condition number created for it */
	protected Map<Expression, Integer> conditionNumbers = new HashMap<Expression, Integer>();
	/* maps a subcondition to a Collection of nodes involved in */
	protected Map<Expression, Collection<Node>> conditionsInvolvedNodes = new HashMap<Expression, Collection<Node>>();
	/* maps asubconditoin to a Collection of edges involved in */
	protected Map<Expression, Collection<Edge>> conditionsInvolvedEdges = new HashMap<Expression, Collection<Edge>>();

	protected Vector<Collection<Collection<InheritanceType>>> typeConditions;
	/* maps a subcondition to the condition number created for it */
	protected Map<Collection<InheritanceType>, Integer> typeConditionNumbers = new HashMap<Collection<InheritanceType>, Integer>();
	/* maps a subcondition to a Collection of nodes involved in */
	protected Map<Collection<InheritanceType>, Collection<Node>> typeConditionsInvolvedNodes = new HashMap<Collection<InheritanceType>, Collection<Node>>();
	/* maps a subcondition to a Collection of edges involved in */
	protected Map<Collection<InheritanceType>, Collection<Edge>> typeConditionsInvolvedEdges = new HashMap<Collection<InheritanceType>, Collection<Edge>>();



	/* maps an action id to a Node Object which is that actions start node  */
	protected Node[] start_node;

	//tells whether two pattern nodes of a given action are pot hom or not
	//e.g. : potHomMatrices[act_id][node_1][node_2]
	protected int[][][] potHomNodeMatrices;

	//Tells whether a pattern node is to be kept. If so, the value indexed by
	//the pattern node number is the node number of the corresponding replacement
	//node, and a negative value otherwise
	//usage: patternNodeIsToBeKept[act_id][node_num]
	protected int[][] patternNodeIsToBeKept;

	//Tells whether a replacement nodes is a is a node preserved by the
	//replacement.  If so, the value indexed by the replacement node number
	//is the node number of the corresponding pattern node, and a negative
	//value otherwise
	//usage: replacementNodeIsPresevedNode[act_id][node_num]
	protected int[][] replacementNodeIsPreservedNode;

	//Tells whether a replacement nodes is a is a node preserved by the
	//replacement.  If so, the value indexed by the replacement node number
	//is the node number of the corresponding pattern node, and a negative
	//value otherwise
	//usage: replacementNodeIsPresevedNode[act_id][node_num]
	protected int[][] replacementNodeChangesTypeTo;

	//yields the replacement edge numbers to be newly inserted by
	//the replacement step according to the given action
	protected Vector<Collection<Edge>> newEdgesOfAction;

	/* compares conditions by their condition numbers */
	protected Comparator<Expression> conditionsComparator = new Comparator<Expression>() {
		public int compare(Expression expr1, Expression expr2) {
			int cmp =
				conditionNumbers.get(expr1).compareTo(conditionNumbers.get(expr2));
			if (cmp == 0 && expr1 != expr2)
				return 1;
			return cmp;
		}
	};

	protected Comparator<Collection<InheritanceType>> typeConditionsComparator = new Comparator<Collection<InheritanceType>>() {
		public int compare(Collection<InheritanceType> type_col1, Collection<InheritanceType> type_col2) {
			//if o1 and o2 are Collections, the the conditions represented
			//by this collections are type conditions, which are conditions
			//about the types of nodes and edges. The collections contain
			//exactly all types, which the corresponding node or edge needs
			//not to be of!
			int cmp =
				typeConditionNumbers.get(type_col1).compareTo(
				typeConditionNumbers.get(type_col2));
			if (cmp == 0 && type_col1 != type_col2)
				return 1;
			return cmp;
		}
	};

	/* compares integer objects */
	protected Comparator<Integer> integerComparator = new Comparator<Integer>() {
		public int compare(Integer i1, Integer i2) {
			return i1.compareTo(i2);
		}
	};


	/**
	 * Method genMatch
	 *
	 * @param    sb                  a  PrintStream
	 * @param    a                   a  MatchingAction
	 * @param    id                  an int
	 *
	 */
	protected void genMatch(PrintStream sb, MatchingAction a, int id) {
		//TODO
	}

	/**
	 * Method genFinish
	 *
	 * @param    sb                  a  PrintStream
	 * @param    a                   a  MatchingAction
	 * @param    id                  an int
	 *
	 */
	protected void genFinish(PrintStream sb, MatchingAction a, int id) {
		// TODO
	}

	/**
	 * Generate some extra stuff.
	 * This function is called after everything else is generated.
	 */
	protected void genExtra() {
		// TODO
	}





	/*
	 * collect some information needed for code gen process of the data
	 * structures representing the graph actions
	 *
	 */
	protected void collectActionInfo() {
		/* get the overall number of graph actions */
		n_graph_actions = actionRuleMap.keySet().size();

		/* get the overall maximum numbers of nodes and edges of all pattern
		 and replacement graphs respectively */
		max_n_pattern_nodes = 0;
		max_n_pattern_edges = 0;
		max_n_replacement_nodes = 0;
		max_n_replacement_edges = 0;

		for(Rule act : actionRuleMap.keySet()) {
			//check whether its graphs node and edge set sizes are greater
			int size;

			size = act.getPattern().getNodes().size();
			if (size > max_n_pattern_nodes)	max_n_pattern_nodes = size;
			size = act.getPattern().getEdges().size();
			if (size > max_n_pattern_edges) max_n_pattern_edges = size;

			if (act.getRight() != null) {
				size = act.getRight().getNodes().size();
				if (size > max_n_replacement_nodes)
					max_n_replacement_nodes = size;
				size = act.getRight().getEdges().size();
				if (size > max_n_replacement_edges)
					max_n_replacement_edges = size;
				}
		}

		/* compute the numbers of nodes/edges of all pattern/replacement-graphs */
		pattern_node_num = new Vector<Map<Node,Integer>>(n_graph_actions);
		pattern_edge_num = new Vector<Map<Edge,Integer>>(n_graph_actions);
		replacement_node_num = new Vector<Map<Node,Integer>>(n_graph_actions);
		replacement_edge_num = new Vector<Map<Edge,Integer>>(n_graph_actions);
		for(Rule act : actionRuleMap.keySet()) {
			int act_id = actionRuleMap.get(act).intValue();
			assert act_id < n_graph_actions:
				"action id found which was greater than the number of graph actions";

			// compute node/edge numbers
			pattern_node_num.set(act_id, new HashMap<Node,Integer>());
			pattern_edge_num.set(act_id, new HashMap<Edge,Integer>());

			//fill the map with pairs (node, node_num)
			int node_num = 0;

			for (Node node : act.getPattern().getNodes()) {
				pattern_node_num.get(act_id).put(node, new Integer(node_num++));
			}
			assert node_num == act.getPattern().getNodes().size():
				"Wrong number of node_nums was created";

			//fill the map with pairs (edge, edge_num)
			int edge_num = 0;

			for (Edge edge : act.getPattern().getEdges()) {
				pattern_edge_num.get(act_id).put(edge, new Integer(edge_num++));
			}
			assert edge_num == act.getPattern().getEdges().size():
				"Wrong number of edge_nums was created";

			// if action has a replacement graph, compute node/edge numbers
			if (act.getRight() != null) {
				replacement_node_num.set(act_id, new HashMap<Node,Integer>());
				replacement_edge_num.set(act_id, new HashMap<Edge,Integer>());

				//fill the map with pairs (node, node_num)
				node_num = 0;

				for (Node node : act.getRight().getNodes()) {
					replacement_node_num.get(act_id).put(node, new Integer(node_num++));
				}
				assert node_num == act.getRight().getNodes().size():
					"Wrong number of node_nums was created";

				//fill the map with pairs (edge, edge_num)
				edge_num = 0;

				for (Edge edge : act.getRight().getEdges()) {
					replacement_edge_num.get(act_id).put(edge, new Integer(edge_num++));
				}
				assert edge_num == act.getRight().getEdges().size():
					"Wrong number of edge_nums was created";
			}
			else {
				replacement_node_num.set(act_id, null);
				replacement_edge_num.set(act_id, null);
			}
		}

		/* for all actions decompose the conditions into conjunctive parts,
		 give all these subexpessions a number, and setup some maps keeping
		 information about them */
		//init a subexpression counter
		int subConditionCounter = 0;

		//setup the array for conditions
		conditions = new HashMap<Integer, Collection<Expression>>();

		//iterate over all actions
		for(Rule act : actionRuleMap.keySet()) {
			int act_id = actionRuleMap.get(act).intValue();

			conditions.put(act_id, new TreeSet<Expression>(conditionsComparator));

			//iterate over all conditions of the current action
			for(Expression condition : act.getPattern().getConditions()) {

				// divide the expression to all AND-connected parts, which do
				//not have an AND-Operator as root themselves
				Collection<Expression> subConditions = decomposeAndParts(condition);

				//for all the subconditions just computed...
				for (Expression sub_condition : subConditions) {

					//...create condition numbers
					conditionNumbers.put(sub_condition, new Integer(subConditionCounter++));

					//...extract the pattern nodes and edges involved in the condition
					Collection<Node> involvedNodes = collectInvolvedNodes(sub_condition);
					Collection<Edge> involvedEdges = collectInvolvedEdges(sub_condition);
					//and at these Collections to prepared Maps
					conditionsInvolvedNodes.put(sub_condition, involvedNodes);
					conditionsInvolvedEdges.put(sub_condition, involvedEdges);

					//store the subcondition in an ordered Collection
					conditions.get(act_id).add(sub_condition);
				}
			}
		}
		//store the overall number of (sub)conditions
		n_conditions = subConditionCounter;


		/* collect the type constraints of the node of all actions pattern graphs */
		int typeConditionCounter = n_conditions;
		typeConditions = new Vector<Collection<Collection<InheritanceType>>>(n_graph_actions);

		for(Rule act : actionRuleMap.keySet()) {
			int act_id = actionRuleMap.get(act).intValue();

			typeConditions.set(act_id, new TreeSet<Collection<InheritanceType>>(typeConditionsComparator));

			/* for all nodes of the current MatchingActions pattern graph
			 extract that nodes type constraints */
			PatternGraph pattern = act.getPattern();
			for (Node node : pattern.getNodes()) {
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

					//store the subcondition in an ordered Collection
					typeConditions.get(act_id).add(type_condition);
				}
			}
			//do the same thing for all edges of the current pattern
			for (Edge edge : pattern.getEdges()) {

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

					//store the subcondition in an ordered Collection
					typeConditions.get(act_id).add(type_condition);
				}
			}
		}
		//update the overall number of conditions, such that type
		//conditions are also included
		n_conditions = typeConditionCounter;


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

		for(Rule act : actionRuleMap.keySet()) {
			int act_id = actionRuleMap.get(act).intValue();

			//collect the attr ids in dependency of condition and the pattern node
			for (Expression cond : conditions.get(act_id)) {
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

		/* for each action compute the start node used in the matching process */
		//init the array of start nodes
		start_node = new Node[n_graph_actions];
		// for all actions gen matcher programs
		for (Rule action : actionRuleMap.keySet()) {
			Graph pattern = action.getPattern();

			//pick out the node with the highest priority as start node
			int max_prio = 0;
			//get any node as initial node
			Node max_prio_node = null;
			if(pattern.getNodes().iterator().hasNext()) {
				max_prio_node = pattern.getNodes().iterator().next();
			}
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
			start_node[actionRuleMap.get(action).intValue()] = max_prio_node;
		}
		//collect information about potential homomorphic pattern graph nodes,
		//i.e. nodes that are allowed to be identified by the matcher during the
		//matching process
		collectPotHomInfo();
		collectPatternNodesToBeKeptInfo();
		collectReplacementNodeIsPreservedNodeInfo();
		collectReplacementNodeChangesTypeToInfo();
		collectNewInsertEdgesInfo();

	}

	/**
	 * Method collectNewInsertEdgesInfo
	 *
	 */
	private void collectNewInsertEdgesInfo() {
		//Collection[] new_edges_of_action;
		newEdgesOfAction = new Vector<Collection<Edge>>(n_graph_actions);

		//init the array with empty HashSets
		for (int i = 0; i < n_graph_actions; i++)
			newEdgesOfAction.set(i, new HashSet<Edge>());

		//for all actions collect the edges to be newly inserted
		for (Rule action : actionRuleMap.keySet()) {
			int act_id = actionRuleMap.get(action).intValue();

			if (action.getRight() != null) {
				Graph replacement = action.getRight();
				//compute the set of newly inserted edges
				newEdgesOfAction.get(act_id).addAll(replacement.getEdges());
				newEdgesOfAction.get(act_id).removeAll(action.getPattern().getEdges());
			}
		}

	}

	/**
	 * Method collectReplacementNodeChangesTypeToInfo
	 *
	 */
	private void collectReplacementNodeChangesTypeToInfo() {
		replacementNodeChangesTypeTo =
			new int[n_graph_actions][max_n_replacement_nodes];

		//init the array with -1
		for (int i = 0; i < n_graph_actions; i++)
			for (int j = 0; j < max_n_replacement_nodes; j++)
				replacementNodeChangesTypeTo[i][j] = -1;

		//for all nodes preserved set the corresponding array entry to the
		//appropriate node type id
		for (Rule action : actionRuleMap.keySet()) {
			int act_id = actionRuleMap.get(action).intValue();

			if (action.getRight() != null) {
				for ( Node node : action.getRight().getNodes() ) {
					if(!node.changesType(action.getRight())) continue;

					int node_num =
						replacement_node_num.get(act_id).get(node).intValue();

					NodeType old_type = node.getNodeType();
					NodeType new_type = node.getRetypedNode(action.getRight()).getNodeType();

					if ( ! nodeTypeMap.get(old_type).equals(nodeTypeMap.get(new_type)) )
						replacementNodeChangesTypeTo[act_id][node_num] =
							nodeTypeMap.get(new_type).intValue();
				}
			}
		}
	}

	/**
	 * Method collectReplacementNodeIsPreservedNodeInfo
	 *
	 */
	private void collectReplacementNodeIsPreservedNodeInfo() {
		replacementNodeIsPreservedNode =
			new int[n_graph_actions][max_n_replacement_nodes];

		//init the array with -1
		for (int i = 0; i < n_graph_actions; i++)
			for (int j = 0; j < max_n_replacement_nodes; j++)
				replacementNodeIsPreservedNode[i][j] = -1;

		//for all nodes preserved set the corresponding array entry to the
		//appropriate pattern node number
		for (Rule action : actionRuleMap.keySet()) {
			int act_id = actionRuleMap.get(action).intValue();

			if (action.getRight() != null) {
				//compute the set of replacement nodes preserved by this action
				Collection<Node> replacement_nodes_preserved = new HashSet<Node>();
				replacement_nodes_preserved.addAll(action.getRight().getNodes());
				replacement_nodes_preserved.retainAll(action.getPattern().getNodes());

				//for all those preserved replacement nodes store the
				//corresponding pattern node
				for (Node node : replacement_nodes_preserved) {
					int node_num =
						replacement_node_num.get(act_id).get(node).intValue();
					replacementNodeIsPreservedNode[act_id][node_num] =
						pattern_node_num.get(act_id).get(node).intValue();
				}
			}
		}

	}

	/**
	 * Method coolectPatternNodesToBeKeptInfo
	 *
	 */
	private void collectPatternNodesToBeKeptInfo() {
		patternNodeIsToBeKept = new int[n_graph_actions][max_n_pattern_nodes];

		//init the arrays with -1
		for (int i = 0; i < n_graph_actions; i++)
			for (int j = 0; j < max_n_pattern_nodes; j++)
				patternNodeIsToBeKept[i][j] = -1;

		//for all nodes to be kept set the corresponding array entry to the
		//appropriate replacement node number
		for (Rule action : actionRuleMap.keySet()) {
			int act_id = actionRuleMap.get(action).intValue();

			//compute the set of pattern nodes to be kept for this action
			Collection<Node> pattern_nodes_to_keep = new HashSet<Node>();
			pattern_nodes_to_keep.addAll(action.getPattern().getNodes());
			if (action.getRight() != null) {
				Graph replacement = action.getRight();
				pattern_nodes_to_keep.retainAll(replacement.getNodes());
				//iterate over the pattern nodes to be kept and store their
				//corresponding replacement node number
				for (Node node : pattern_nodes_to_keep) {
					int node_num =
						pattern_node_num.get(act_id).get(node).intValue();
					patternNodeIsToBeKept[act_id][node_num] =
						replacement_node_num.get(act_id).get(node).intValue();
				}
			}
		}
	}

	/**
	 * Decompose the given expression into all subexpressions, which are not
	 * AND-operators, and store the roots of these subexpression into a
	 * <tt>Collection</tt>.
	 *
	 * @param    expr                an Expression
	 *
	 * @return   the <tt>Collection</tt> of all subexpressions
	 * 				not being an AND-Operator
	 */
	protected Collection<Expression> decomposeAndParts(Expression expr) {
		Collection<Expression> ret = new HashSet<Expression>();
		//step recursive into the expression tree
		__recursive_decompose_and(ret, expr);

		return ret;
	}
	// decomposeAndParts() is only a wrapper method for this recursive method
	private void __recursive_decompose_and(
		Collection<Expression> col, Expression expr) {

		if (expr instanceof Operator &&
				((Operator)expr).getOpCode() == Operator.LOG_AND ) {
			//step into subexpressions
			Operator andOp = (Operator)expr;
			for (int i=0; i < andOp.arity(); i++)
				__recursive_decompose_and(col, andOp.getOperand(i));
		}
		else
			//expr is not an AND-Operator...
			//...so add the expr to the Collection
			col.add(expr);
	}


	/**
	 * Collects all pairs (node_num. attr_id) occuring in the qualifications
	 * at the leafes of the given expression and stores them in a map, which
	 * map node_numbers to collections of attr_ids
	 *
	 * @param    act_id              the id of the action the expr is condition of
	 * @param    map                 a  Map
	 * @param    expr                an Expression
	 *
	 */
	protected void __recursive_qual_collect(int act_id, Map<Node, Collection<Integer>> node_map, Map<Edge, Collection<Integer>> edge_map, Expression expr) {
		if (expr == null) return;

		//recursive descent
		if (expr instanceof Operator)
			for (int i = 0; i < ((Operator)expr).arity(); i++)
				__recursive_qual_collect(act_id, node_map, edge_map, ((Operator)expr).getOperand(i));

		//get (node_num, attr_id) pairs from qualifications
		if (expr instanceof Qualification) {
			Qualification qual = (Qualification) expr;
			Entity owner = qual.getOwner();
			Entity member = qual.getMember();

			//if owner is a node, add to the node_map
			if (owner instanceof Node) {
				Node node = (Node) owner;
				//Integer node_num = (Integer) pattern_node_num[ act_id ].get( owner );
				Integer attr_id = nodeAttrMap.get( member );

				//add the pair (node_num, attr_id to the map)
				if (node_map.containsKey( node ))
					node_map.get( node ).add( attr_id );
				else {
					Collection<Integer> newCol = new HashSet<Integer>();
					newCol.add( attr_id );
					node_map.put( node, newCol );
				}
			}

			//if owner is an edge. add to the edge_map
			if (owner instanceof Edge) {
				Edge egde = (Edge) owner;
				//Integer edge_num = (Integer) pattern_edge_num[act_id].get(owner);
				Integer attr_id = edgeAttrMap.get(member);

				//add the pair (edge_num, attr_id to the map)
				if (edge_map.containsKey( egde ))
					edge_map.get(egde).add(attr_id);
				else {
					Collection<Integer> newCol = new TreeSet<Integer>(integerComparator);
					newCol.add(attr_id);
					edge_map.put(egde, newCol);
				}
			}
		}
	}


	/**
	 * Collects all nodes attributes of which occur in the given expression
	 *
	 * @param    expr                the expression
	 *
	 * @return   a Collection of all that nodes
	 *
	 */
	protected Collection<Node> collectInvolvedNodes(Expression expr) {

		Collection<Node> ret = new HashSet<Node>(); /* the Collection to be returned */
		//step down into the expression and collect all involved graph nodes
		__recursive_node_collect(ret, expr);

		return ret;
	}
	private void __recursive_node_collect(Collection<Node> col, Expression expr) {
		if (expr == null) return;

		if (expr instanceof Operator)
			for (int i = 0; i < ((Operator)expr).arity(); i++)
				__recursive_node_collect(col, ((Operator)expr).getOperand(i));

		if (expr instanceof Qualification) {
			Entity ent = ((Qualification)expr).getOwner();
			// if the qualification selects an attr from a node, add that node
			if (ent instanceof Node) {
				col.add((Node)ent);
			}
		}
	}

	/**
	 * Collects all edges attributes of which occur in the given expresion
	 *
	 * @param    expr                the expression
	 *
	 * @return   a Collection of all that edges
	 *
	 */
	protected Collection<Edge> collectInvolvedEdges(Expression expr) {

		Collection<Edge> ret = new HashSet<Edge>(); /* the Collection to be returned */
		//step down into the expression and collect all involved graph nodes
		__recursive_edge_collect(ret, expr);

		return ret;
	}
	private void __recursive_edge_collect(Collection<Edge> col, Expression expr) {

		if (expr == null) return;

		if (expr instanceof Operator)
			for (int i = 0; i < ((Operator) expr).arity(); i++)
				__recursive_edge_collect(col, ((Operator) expr).getOperand(i));

		if (expr instanceof Qualification) {
			Entity ent = ((Qualification) expr).getOwner();
			// if the qualification selects an attr from an edge, add that edge
			if (ent instanceof Edge) {
				col.add((Edge) ent);
			}
		}
	}




	/*
	 * collect some information needed for code gen process of the graph
	 * type model data structures
	 *
	 */
	protected void collectGraphTypeModelInfo() {
		/* overall number of node and edge types */
		n_node_types = getIDs(true).length;
		n_edge_types = getIDs(false).length;

		/* overall number of enum types */
		n_enum_types = enumMap.size();

		/* overall number of node and edge attributes declared in the grg file */
		n_node_attrs = nodeAttrMap.size();
		n_edge_attrs = edgeAttrMap.size();

		/* get the inheritance information of the node and edge types */
		node_is_a_matrix = getIsAMatrix(true);
		edge_is_a_matrix = getIsAMatrix(false);



		/* count the number of attrs a node type has */
		n_attr_of_node_type = new int[n_node_types];
		//fill that array with 0
		for (int i=0; i < n_node_types; i++) n_attr_of_node_type[i] = 0;
		//count number of attributes
		for (Entity attr : nodeAttrMap.keySet()) {
			assert attr.hasOwner():
				"Thought, that the Entity represented a node class attr and that\n" +
				"thus there had to be a type that owned the entity, but there was non.";
			Type node_type = attr.getOwner();
			//get the id of the node type, where the attr is declared in
			int node_type_id = nodeTypeMap.get(node_type).intValue();
			assert node_type_id < n_node_types:
				"Tried to use a node-type-id as array index, " +
				"but the id exceeded the number of node types";
			//increment the number of attributes for the declaring type...
			n_attr_of_node_type[node_type_id]++;
			//...but the attr is also contained in all sub types, i.e. increment there too
			for (int nt_id = 0; nt_id < n_node_types; nt_id++)
				if (node_is_a_matrix[nt_id][node_type_id] > 0)
					n_attr_of_node_type[nt_id]++;
		}




		/* count the number of attrs an edge type has */
		n_attr_of_edge_type = new int[n_edge_types];
		//fill that array with 0
		for (int i=0; i < n_edge_types; i++) n_attr_of_edge_type[i] = 0;
		//count number of attributes
		for (Entity attr : edgeAttrMap.keySet()) {
			assert attr.hasOwner():
				"Thought, that the Entity represented an edge class attr and that\n" +
				"thus there had to be a type that owned the entity, but there was non.";
			Type edge_type = attr.getOwner();
			//get the id of the edge type, where the attr is declared in
			int edge_type_id = edgeTypeMap.get(edge_type).intValue();
			assert edge_type_id < n_edge_types:
				"Tried to use an edge-type-id as array index," +
				"but the id exceeded the number of edge types";
			//increment the number of attributes for the declaring type...
			n_attr_of_edge_type[edge_type_id]++;
			//...but the attr is also contained in all sub types, i.e. increment there too
			for (int et_id = 0; et_id < n_edge_types; et_id++)
				if (edge_is_a_matrix[et_id][edge_type_id] > 0)
					n_attr_of_edge_type[et_id]++;
		}



		/* collect all needed information about node attributes */
		node_attr_info = new AttrTypeDescriptor[n_node_attrs];
		for (Entity attr : nodeAttrMap.keySet()) {
			assert attr.hasOwner():
				"Thought, that the Entity represented an node attr and that thus\n" +
				"there had to be a type that owned the entity, but there was non.";
			NodeType node_type = (NodeType) attr.getOwner();
			int node_type_id = nodeTypeMap.get(node_type).intValue();
			int attr_id = nodeAttrMap.get(attr).intValue();

			node_attr_info[attr_id] = new AttrTypeDescriptor();
			//set the attr id
			node_attr_info[attr_id].attr_id = attr_id;
			//get the attributes name
			node_attr_info[attr_id].name = attr.getIdent().toString();
			//get the owners type id
			node_attr_info[attr_id].decl_owner_type_id = node_type_id;
			//get the attributes kind
			if (attr.getType() instanceof IntType)
				node_attr_info[attr_id].kind = AttrTypeDescriptor.INTEGER;
			else if (attr.getType() instanceof BooleanType)
				node_attr_info[attr_id].kind= AttrTypeDescriptor.BOOLEAN;
			else if (attr.getType() instanceof StringType)
				node_attr_info[attr_id].kind = AttrTypeDescriptor.STRING;
			else if (attr.getType() instanceof EnumType) {
				node_attr_info[attr_id].kind = AttrTypeDescriptor.ENUM;
				node_attr_info[attr_id].enum_id = enumMap.get(attr.getType()).intValue();
			}
			else {
				System.err.println("Key element of AttrNodeMap has a type, which is " +
									   "neither one of 'int', 'boolean', 'string' nor an enumeration type.");
				System.exit(0);
			}
		}




		/* collect all needed information about edge attributes */
		edge_attr_info = new AttrTypeDescriptor[n_edge_attrs];
		for (Entity attr : edgeAttrMap.keySet()) {
			assert attr.hasOwner():
				"Thought, that the Entity represented an edge attr and that thus\n" +
				"there had to be a type that owned the entity, but there was non.";
			EdgeType edge_type = (EdgeType) attr.getOwner();
			int edge_type_id = edgeTypeMap.get(edge_type).intValue();
			int attr_id = edgeAttrMap.get(attr).intValue();

			edge_attr_info[attr_id] = new AttrTypeDescriptor();
			//set the attr id
			edge_attr_info[attr_id].attr_id = attr_id;
			//get the attributes name
			edge_attr_info[attr_id].name = attr.getIdent().toString();
			//get the owners type id
			edge_attr_info[attr_id].decl_owner_type_id = edge_type_id;
			//get the attributes kind
			if (attr.getType() instanceof IntType)
				edge_attr_info[attr_id].kind = AttrTypeDescriptor.INTEGER;
			else if (attr.getType() instanceof BooleanType)
				edge_attr_info[attr_id].kind= AttrTypeDescriptor.BOOLEAN;
			else if (attr.getType() instanceof StringType)
				edge_attr_info[attr_id].kind = AttrTypeDescriptor.STRING;
			else if (attr.getType() instanceof EnumType) {
				edge_attr_info[attr_id].kind = AttrTypeDescriptor.ENUM;
				edge_attr_info[attr_id].enum_id = enumMap.get(attr.getType()).intValue();
			}
			else {
				System.err.println("Key element of AttrEdgeMap has a type, which is " +
									   "neither one of 'int', 'boolean', 'string' nor an enumeration type.");
				System.exit(0);
			}
		}


		/* compute the attr layout of the node types given in the grg file */
		node_attr_index = new int[n_node_types][n_node_attrs];
		//for all node types...
		for (int nt = 0; nt < n_node_types; nt++) {
			//the index the current attr will get in the current node layout, if it's a member
			int attr_index = 0;
			//...and all node attribute IDs...
			for (int attr_id = 0; attr_id < n_node_attrs; attr_id++) {
				//...check whether the attr is owned by the node type or one of its supertype
				int owner = node_attr_info[attr_id].decl_owner_type_id;
				if ( owner == nt || node_is_a_matrix[nt][owner] > 0)
					//setup the attrs index in the layout of the current node type
					node_attr_index[nt][attr_id] = attr_index++;
				else
					//-1 means that the current attr is not a member of the current node type
					node_attr_index[nt][attr_id] = -1;
			}
		}


		/* compute the attr layout of the edge types given in the grg file */
		edge_attr_index = new int[n_edge_types][n_edge_attrs];
		//for all edge types...
		for (int et = 0; et < n_edge_types; et++) {
			//the index the current attr will get in the current edge layout, if it's a member
			int attr_index = 0;
			//...and all edge attribute IDs...
			for (int attr_id = 0; attr_id < n_edge_attrs; attr_id++) {
				//...check whether the attr is owned by the edge type or one of its supertype
				int owner = edge_attr_info[attr_id].decl_owner_type_id;
				if ( owner == et || edge_is_a_matrix[et][owner] > 0)
					//setup the attrs index in the layout of the current node type
					edge_attr_index[et][attr_id] = attr_index++;
				else
					//-1 means that the current attr is not a member of the current node type
					edge_attr_index[et][attr_id] = -1;
			}
		}



		//collect the information about the enumeration types
		enum_type_descriptors = new EnumDescriptor[n_enum_types];
		for (int et = 0; et < n_enum_types; et++)
			enum_type_descriptors[et] = new EnumDescriptor();

		for (EnumType enum_type : enumMap.keySet()) {
			//store the info about the current enum type in an array...
			//...type id
			int enum_type_id = enumMap.get(enum_type).intValue();
			enum_type_descriptors[enum_type_id].type_id = enum_type_id;
			//...the identifier used in the grg-file to declare thar enum type
			enum_type_descriptors[enum_type_id].name = enum_type.getIdent().toString();
			//..the items in this enumeration type
			for (EnumItem item : enum_type.getItems()) {
				enum_type_descriptors[enum_type_id].items.add(item);
			}
			//...the number of items
			enum_type_descriptors[enum_type_id].n_items =
				enum_type_descriptors[enum_type_id].items.size();
		}
	}
	/**  computes matrices for all actions which show whether two pattern nodes
	 *   are allowed to be identified by the matcher */
	protected void collectPotHomInfo () {

		//tells whether two pattern nodes of a given action are pot hom or not
		//e.g. : potHomMatrices[act_id][node_1][node_2]
		//protected int potHomMatrices[][][];
		potHomNodeMatrices =
			new int[n_graph_actions][max_n_pattern_nodes][max_n_pattern_nodes];

		for (int i = 0; i < n_graph_actions; i++)
			for (int j =0; j < max_n_pattern_nodes; j++)
				for (int k = 0; k < max_n_pattern_nodes; k++)
					potHomNodeMatrices[i][j][k] = 0;

		//got through that m,atrices and set cells to '1' if two nodes
		//are potentialy homomorphic
		for (Rule action : actionRuleMap.keySet()) {
			PatternGraph pattern = action.getPattern();
			for (Node node_1 : pattern.getNodes()) {
				Collection<Node> hom_of_node_1 = new HashSet<Node>();
				hom_of_node_1 = pattern.getHomomorphic(node_1);

				for (Node node_2 : pattern.getNodes()) {
					//check whether these to nodes are potentially homomorphic
					//the pattern graph of the currrent action
					if (hom_of_node_1.contains(node_2)) {
						int act_id = actionRuleMap.get(action).intValue();
						int node_1_num =
							pattern_node_num.get(act_id).get(node_1).intValue();
						int node_2_num =
							pattern_node_num.get(act_id).get(node_2).intValue();
						potHomNodeMatrices[act_id][node_1_num][node_2_num] = 1;
					}
				}
			}
		}
	}
}
