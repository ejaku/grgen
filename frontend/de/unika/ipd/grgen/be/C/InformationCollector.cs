/*
 * GrGen: graph rewrite generator tool -- release GrGen.NET 8.1
 * Copyright (C) 2003-2026 Universitaet Karlsruhe, Institut fuer Programmstrukturen und Datenorganisation, LS Goos; and free programmers
 * licensed under LGPL v3, some components/parts use different licenses (see LICENSE.txt included in the packaging of this file)
 * www.grgen.de / www.grgen.net
 */

/// <summary>
/// Extracts all the information needed by the FrameBasedBackend
/// from the GrGen-internal IR
/// @author Veit Batz
/// </summary>

namespace de.unika.ipd.grgen.be.C
{

	using System;
	using System.Collections.Generic;
	using System.Diagnostics;

	using de.unika.ipd.grgen.ir;
	using MatchingAction = de.unika.ipd.grgen.ir.executable.MatchingAction;
	using Rule = de.unika.ipd.grgen.ir.executable.Rule;
	using Expression = de.unika.ipd.grgen.ir.expr.Expression;
	using Operator = de.unika.ipd.grgen.ir.expr.Operator;
	using OperatorCode = de.unika.ipd.grgen.ir.expr.OperatorCode;
	using Qualification = de.unika.ipd.grgen.ir.expr.Qualification;
	using EnumItem = de.unika.ipd.grgen.ir.model.EnumItem;
	using EdgeType = de.unika.ipd.grgen.ir.model.type.EdgeType;
	using EnumType = de.unika.ipd.grgen.ir.model.type.EnumType;
	using InheritanceType = de.unika.ipd.grgen.ir.model.type.InheritanceType;
	using NodeType = de.unika.ipd.grgen.ir.model.type.NodeType;
	using Edge = de.unika.ipd.grgen.ir.pattern.Edge;
	using PatternGraphBase = de.unika.ipd.grgen.ir.pattern.PatternGraphBase;
	using Node = de.unika.ipd.grgen.ir.pattern.Node;
	using PatternGraphLhs = de.unika.ipd.grgen.ir.pattern.PatternGraphLhs;
	using BooleanType = de.unika.ipd.grgen.ir.type.basic.BooleanType;
	using IntType = de.unika.ipd.grgen.ir.type.basic.IntType;
	using StringType = de.unika.ipd.grgen.ir.type.basic.StringType;
	using Annotations = de.unika.ipd.grgen.util.Annotations;

	public class InformationCollector : CBackend
	{
		/* some information extracted from the grg-fiel, collected
		 during the generation process */
		/* overall number of types, attrs, ... */
		protected internal int n_node_types;
		protected internal int n_edge_types;
		protected internal int n_enum_types;
		protected internal int n_node_attrs;
		protected internal int n_edge_attrs;

		/* inheritance information of node and edge types */
		protected internal short[][] node_is_a_matrix;
		protected internal short[][] edge_is_a_matrix;

		/* the number of attributes a node/edge type has */
		protected internal int[] n_attr_of_node_type;
		protected internal int[] n_attr_of_edge_type;

		/* information describing node and edge attributes */
		protected internal AttrTypeDescriptor[] node_attr_info;
		protected internal AttrTypeDescriptor[] edge_attr_info;

		/* during gen process an attr layout of each node/edge type will
		 be computed, the result is stored in these two arrays:
		 node_attr_index[nt_id][attr_id] = n  means that for the given
		 node type and given attr the attr value is stored at pos n*/
		protected internal int[][] node_attr_index;
		protected internal int[][] edge_attr_index;

		/* array of objects describing the rnum types declared in the grg file */
		protected internal EnumDescriptor[] enum_type_descriptors;

		/* the overall number of graph actions */
		protected internal int n_graph_actions;

		/* overall number off conditions of all pattern graphs */
		protected internal int n_conditions;

		/* overall max number of some things */
		protected internal int max_n_pattern_nodes;
		protected internal int max_n_pattern_edges;
		protected internal int max_n_replacement_nodes;
		protected internal int max_n_replacement_edges;

		/* a map  action_id --> node --> pattern_node_num, e.g
		 pattern_node_num[act_id].get(someNode)
		 yields an Integer object wrapping node number for an fb_acts_graph_t */
		protected internal IList<IDictionary<Node, int>> pattern_node_num;
		/* the same, but edges */
		protected internal IList<IDictionary<Edge, int>> pattern_edge_num;
		/* just like above, but for the replacement graph if the given action has one
		 otherwise the array yields a null pointer instead of a map */
		protected internal IList<IDictionary<Node, int>> replacement_node_num;
		protected internal IList<IDictionary<Edge, int>> replacement_edge_num;

		/* realizes a map
		 cond_num -> pattern_node_num -> Collection_of_attr_ids,
		 i.e. yields a Collection of attr-ids occuring in Qualification expressions
		 of the given condition together with the given pattern node. Usage:
		 involvedPatternNodeAttrIds[cond_num].get(pattern_node_num) */
		protected internal IDictionary<Expression, IDictionary<Node, ICollection<int>>> involvedPatternNodeAttrIds;
		/* just the same, but edge attrs */
		protected internal IDictionary<Expression, IDictionary<Edge, ICollection<int>>> involvedPatternEdgeAttrIds;

		/* the conditions of the pattern graphs are decomposed into subexpression
		 down to sub expressions, which are not AND-Operations. These "conjunctive"
		 parts are the units the fb backend is working with */
		/* collects all these conjuctive parts; to assure that the conditions
		 can easily be processed in the order of their condition numbers, this
		 Collection will be initialized with an TreeSet parametrised with a
		 Comparator comparing by condition numbers */
		protected internal Dictionary<int, ICollection<Expression>> conditions;
		/* maps a subcondition to the condition number created for it */
		protected internal IDictionary<Expression, int> conditionNumbers = new Dictionary<Expression, int>();
		/* maps a subcondition to a Collection of nodes involved in */
		protected internal IDictionary<Expression, ICollection<Node>> conditionsInvolvedNodes = new Dictionary<Expression, ICollection<Node>>();
		/* maps asubconditoin to a Collection of edges involved in */
		protected internal IDictionary<Expression, ICollection<Edge>> conditionsInvolvedEdges = new Dictionary<Expression, ICollection<Edge>>();

		protected internal IList<ICollection<ICollection<InheritanceType>>> typeConditions;
		/* maps a subcondition to the condition number created for it */
		protected internal IDictionary<ICollection<InheritanceType>, int> typeConditionNumbers = new Dictionary<ICollection<InheritanceType>, int>();
		/* maps a subcondition to a Collection of nodes involved in */
		protected internal IDictionary<ICollection<InheritanceType>, ICollection<Node>> typeConditionsInvolvedNodes = new Dictionary<ICollection<InheritanceType>, ICollection<Node>>();
		/* maps a subcondition to a Collection of edges involved in */
		protected internal IDictionary<ICollection<InheritanceType>, ICollection<Edge>> typeConditionsInvolvedEdges = new Dictionary<ICollection<InheritanceType>, ICollection<Edge>>();

		/* maps an action id to a Node Object which is that actions start node  */
		protected internal Node[] start_node;

		//tells whether two pattern nodes of a given action are pot hom or not
		//e.g. : potHomMatrices[act_id][node_1][node_2]
		protected internal int[][][] potHomNodeMatrices;

		//Tells whether a pattern node is to be kept. If so, the value indexed by
		//the pattern node number is the node number of the corresponding replacement
		//node, and a negative value otherwise
		//usage: patternNodeIsToBeKept[act_id][node_num]
		protected internal int[][] patternNodeIsToBeKept;

		//Tells whether a replacement nodes is a is a node preserved by the
		//replacement.  If so, the value indexed by the replacement node number
		//is the node number of the corresponding pattern node, and a negative
		//value otherwise
		//usage: replacementNodeIsPresevedNode[act_id][node_num]
		protected internal int[][] replacementNodeIsPreservedNode;

		//Tells whether a replacement nodes is a is a node preserved by the
		//replacement.  If so, the value indexed by the replacement node number
		//is the node number of the corresponding pattern node, and a negative
		//value otherwise
		//usage: replacementNodeIsPresevedNode[act_id][node_num]
		protected internal int[][] replacementNodeChangesTypeTo;

		//yields the replacement edge numbers to be newly inserted by
		//the replacement step according to the given action
		protected internal List<ISet<Edge>> newEdgesOfAction;

		/* compares conditions by their condition numbers */
		protected internal IComparer<Expression> conditionsComparator = new ComparatorAnonymousInnerClass();

		private class ComparatorAnonymousInnerClass : IComparer<Expression>
		{
			private readonly InformationCollector outerInstance;

			public int Compare(Expression expr1, Expression expr2)
			{
				int cmp = outerInstance.conditionNumbers[expr1].CompareTo(outerInstance.conditionNumbers[expr2]);
				if(cmp == 0 && expr1 != expr2)
					return 1;
				return cmp;
			}
		}

		protected internal IComparer<ICollection<InheritanceType>> typeConditionsComparator = new ComparatorAnonymousInnerClass2();

		private class ComparatorAnonymousInnerClass2 : IComparer<ICollection<InheritanceType>>
		{
			private readonly InformationCollector outerInstance;

			public int Compare(ICollection<InheritanceType> type_col1, ICollection<InheritanceType> type_col2)
			{
				//if o1 and o2 are Collections, the the conditions represented
				//by this collections are type conditions, which are conditions
				//about the types of nodes and edges. The collections contain
				//exactly all types, which the corresponding node or edge needs
				//not to be of!
				int cmp = outerInstance.typeConditionNumbers[type_col1].CompareTo(outerInstance.typeConditionNumbers[type_col2]);
				if(cmp == 0 && type_col1 != type_col2)
					return 1;
				return cmp;
			}
		}

		/* compares integer objects */
		protected internal IComparer<int> integerComparator = new ComparatorAnonymousInnerClass3();

		private class ComparatorAnonymousInnerClass3 : IComparer<int>
		{
			private readonly InformationCollector outerInstance;

			public int Compare(int? i1, int? i2)
			{
				return i1.CompareTo(i2);
			}
		}

		/// <summary>
		/// Method genMatch </summary>
		/// <param name="sb">                  a  PrintStream </param>
		/// <param name="a">                   a  MatchingAction </param>
		/// <param name="id">                  an int </param>
		protected internal override void GenMatch(PrintStream sb, MatchingAction a, int id)
		{
			//TODO
		}

		/// <summary>
		/// Method genFinish </summary>
		/// <param name="sb">                  a  PrintStream </param>
		/// <param name="a">                   a  MatchingAction </param>
		/// <param name="id">                  an int </param>
		protected internal override void GenFinish(PrintStream sb, MatchingAction a, int id)
		{
			// TODO
		}

		/// <summary>
		/// Generate some extra stuff.
		/// This function is called after everything else is generated.
		/// </summary>
		protected internal override void GenExtra()
		{
			// TODO
		}

		/*
		 * collect some information needed for code gen process of the data
		 * structures representing the graph actions
		 */
		protected internal virtual void CollectActionInfo()
		{
			/* get the overall number of graph actions */
			n_graph_actions = actionRuleMap.Keys.Count;

			/* get the overall maximum numbers of nodes and edges of all pattern
			 and replacement graphs respectively */
			max_n_pattern_nodes = 0;
			max_n_pattern_edges = 0;
			max_n_replacement_nodes = 0;
			max_n_replacement_edges = 0;

			foreach(Rule act in actionRuleMap.Keys)
			{
				//check whether its graphs node and edge set sizes are greater
				int size;

				size = act.Pattern.GetNodes().Size();
				if(size > max_n_pattern_nodes)
					max_n_pattern_nodes = size;
				size = act.Pattern.GetEdges().Size();
				if(size > max_n_pattern_edges)
					max_n_pattern_edges = size;

				if(act.Right != null)
				{
					size = act.Right.Nodes.Count;
					if(size > max_n_replacement_nodes)
						max_n_replacement_nodes = size;
					size = act.Right.Edges.Count;
					if(size > max_n_replacement_edges)
						max_n_replacement_edges = size;
				}
			}

			/* compute the numbers of nodes/edges of all pattern/replacement-graphs */
			pattern_node_num = new List<IDictionary<Node, int>>(n_graph_actions);
			pattern_edge_num = new List<IDictionary<Edge, int>>(n_graph_actions);
			replacement_node_num = new List<IDictionary<Node, int>>(n_graph_actions);
			replacement_edge_num = new List<IDictionary<Edge, int>>(n_graph_actions);
			foreach(Rule act in actionRuleMap.Keys)
			{
				int act_id = actionRuleMap[act];
				Debug.Assert(act_id < n_graph_actions, "action id found which was greater than the number of graph actions");

				// compute node/edge numbers
				pattern_node_num[act_id] = new Dictionary<Node, int>();
				pattern_edge_num[act_id] = new Dictionary<Edge, int>();

				//fill the map with pairs (node, node_num)
				int node_num = 0;

				foreach(Node node in act.Pattern.GetNodes())
					pattern_node_num[act_id][node] = new int?(node_num++);
				Debug.Assert(node_num == act.Pattern.GetNodes().Size(), "Wrong number of node_nums was created");

				//fill the map with pairs (edge, edge_num)
				int edge_num = 0;

				foreach(Edge edge in act.Pattern.GetEdges())
					pattern_edge_num[act_id][edge] = new int?(edge_num++);
				Debug.Assert(edge_num == act.Pattern.GetEdges().Size(), "Wrong number of edge_nums was created");

				// if action has a replacement graph, compute node/edge numbers
				if(act.Right != null)
				{
					replacement_node_num[act_id] = new Dictionary<Node, int>();
					replacement_edge_num[act_id] = new Dictionary<Edge, int>();

					//fill the map with pairs (node, node_num)
					node_num = 0;

					foreach(Node node in act.Right.Nodes)
						replacement_node_num[act_id][node] = new int?(node_num++);
					Debug.Assert(node_num == act.Right.Nodes.Count, "Wrong number of node_nums was created");

					//fill the map with pairs (edge, edge_num)
					edge_num = 0;

					foreach(Edge edge in act.Right.Edges)
						replacement_edge_num[act_id][edge] = new int?(edge_num++);
					Debug.Assert(edge_num == act.Right.Edges.Count, "Wrong number of edge_nums was created");
				}
				else
				{
					replacement_node_num[act_id] = null;
					replacement_edge_num[act_id] = null;
				}
			}

			/* for all actions decompose the conditions into conjunctive parts,
			 give all these subexpessions a number, and setup some maps keeping
			 information about them */
			//init a subexpression counter
			int subConditionCounter = 0;

			//setup the array for conditions
			conditions = new Dictionary<int, ICollection<Expression>>();

			//iterate over all actions
			foreach(Rule act in actionRuleMap.Keys)
			{
				int act_id = actionRuleMap[act];

				conditions[Convert.ToInt32(act_id)] = new SortedSet<Expression>(conditionsComparator);

				//iterate over all conditions of the current action
				foreach(Expression condition in act.Pattern.GetConditions())
				{
					// divide the expression to all AND-connected parts, which do
					//not have an AND-Operator as root themselves
					ICollection<Expression> subConditions = DecomposeAndParts(condition);

					//for all the subconditions just computed...
					foreach(Expression sub_condition in subConditions)
					{
						//...create condition numbers
						conditionNumbers[sub_condition] = new int?(subConditionCounter++);

						//...extract the pattern nodes and edges involved in the condition
						ICollection<Node> involvedNodes = CollectInvolvedNodes(sub_condition);
						ICollection<Edge> involvedEdges = CollectInvolvedEdges(sub_condition);
						//and at these Collections to prepared Maps
						conditionsInvolvedNodes[sub_condition] = involvedNodes;
						conditionsInvolvedEdges[sub_condition] = involvedEdges;

						//store the subcondition in an ordered Collection
						conditions[Convert.ToInt32(act_id)].Add(sub_condition);
					}
				}
			}
			//store the overall number of (sub)conditions
			n_conditions = subConditionCounter;

			/* collect the type constraints of the node of all actions pattern graphs */
			int typeConditionCounter = n_conditions;
			typeConditions = new List<ICollection<ICollection<InheritanceType>>>(n_graph_actions);

			foreach(Rule act in actionRuleMap.Keys)
			{
				int act_id = actionRuleMap[act];

				typeConditions[act_id] = new SortedSet<ICollection<InheritanceType>>(typeConditionsComparator);

				/* for all nodes of the current MatchingActions pattern graph
				 extract that nodes type constraints */
				PatternGraphLhs pattern = act.Pattern;
				foreach(Node node in pattern.Nodes)
				{
					//if node has type constraints, register the as conditions
					if(node.GetConstraints().Count > 0)
					{
						//note that a type condition is the set of all types,
						//the corresponding node/edge is not allowed to be of
						ICollection<InheritanceType> type_condition = node.GetConstraints();

						//...create condition numbers
						typeConditionNumbers[type_condition] = new int?(typeConditionCounter++);

						//...extract the pattern nodes and edges involved in the condition
						ICollection<Node> involvedNodes = new HashSet<Node>();
						involvedNodes.Add(node);
						//and at these Collections to prepared Maps
						typeConditionsInvolvedNodes[type_condition] = involvedNodes;
						ICollection<Edge> empty = Collections.EmptySet();
						typeConditionsInvolvedEdges[type_condition] = empty;

						//store the subcondition in an ordered Collection
						typeConditions[act_id].Add(type_condition);
					}
				}
				//do the same thing for all edges of the current pattern
				foreach(Edge edge in pattern.Edges)
				{
					//if node has type constraints, register the as conditions
					if(edge.GetConstraints().Count > 0)
					{
						//note that a type condition is the set of all types,
						//the corresponding edge is not allowed to be of
						ICollection<InheritanceType> type_condition = edge.GetConstraints();

						//...create condition numbers
						typeConditionNumbers[type_condition] = new int?(typeConditionCounter++);

						//...extract the pattern edges and edges involved in the condition
						ICollection<Edge> involvedEdges = new HashSet<Edge>();
						involvedEdges.Add(edge);
						//and at these Collections to prepared Maps
						ICollection<Node> empty = Collections.EmptySet();
						typeConditionsInvolvedNodes[type_condition] = empty;
						typeConditionsInvolvedEdges[type_condition] = involvedEdges;

						//store the subcondition in an ordered Collection
						typeConditions[act_id].Add(type_condition);
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

			/* for each action compute the start node used in the matching process */
			//init the array of start nodes
			start_node = new Node[n_graph_actions];
			// for all actions gen matcher programs
			foreach(Rule action in actionRuleMap.Keys)
			{
				PatternGraphBase pattern = action.Pattern;

				//pick out the node with the highest priority as start node
				int max_prio = 0;
				//get any node as initial node
				Node max_prio_node = null;
				if(pattern.Nodes.GetEnumerator().HasNext())
					max_prio_node = pattern.Nodes.GetEnumerator().Next();
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
				start_node[actionRuleMap[action]] = max_prio_node;
			}
			//collect information about potential homomorphic pattern graph nodes,
			//i.e. nodes that are allowed to be identified by the matcher during the
			//matching process
			CollectPotHomInfo();
			CollectPatternNodesToBeKeptInfo();
			CollectReplacementNodeIsPreservedNodeInfo();
			CollectReplacementNodeChangesTypeToInfo();
			CollectNewInsertEdgesInfo();
		}

		/// <summary>
		/// Method collectNewInsertEdgesInfo
		/// </summary>
		private void CollectNewInsertEdgesInfo()
		{
			//Collection[] new_edges_of_action;
			newEdgesOfAction = new List<ISet<Edge>>(n_graph_actions);

			//init the array with empty HashSets
			for(int i = 0; i < n_graph_actions; i++)
				newEdgesOfAction[i] = new HashSet<Edge>();

			//for all actions collect the edges to be newly inserted
			foreach(Rule action in actionRuleMap.Keys)
			{
				int act_id = actionRuleMap[action];

				if(action.Right != null)
				{
					PatternGraphBase replacement = action.Right;
					//compute the set of newly inserted edges
					newEdgesOfAction[act_id].AddAll(replacement.Edges);
					newEdgesOfAction[act_id].RemoveAll(action.Pattern.GetEdges());
				}
			}
		}

		/// <summary>
		/// Method collectReplacementNodeChangesTypeToInfo
		/// </summary>
		private void CollectReplacementNodeChangesTypeToInfo()
		{
			replacementNodeChangesTypeTo = RectangularArrays.RectangularIntArray(n_graph_actions, max_n_replacement_nodes);

			//init the array with -1
			for(int i = 0; i < n_graph_actions; i++)
			{
				for(int j = 0; j < max_n_replacement_nodes; j++)
					replacementNodeChangesTypeTo[i][j] = -1;
			}

			//for all nodes preserved set the corresponding array entry to the
			//appropriate node type id
			foreach(Rule action in actionRuleMap.Keys)
			{
				int act_id = actionRuleMap[action];

				if(action.Right != null)
				{
					foreach(Node node in action.Right.Nodes)
					{
						if(!node.ChangesType(action.Right))
							continue;

						int node_num = replacement_node_num[act_id][node];

						NodeType old_type = node.NodeType;
						NodeType new_type = node.GetRetypedNode(action.Right).GetNodeType();

						if(!nodeTypeMap[old_type].Equals(nodeTypeMap[new_type]))
							replacementNodeChangesTypeTo[act_id][node_num] = nodeTypeMap[new_type];
					}
				}
			}
		}

		/// <summary>
		/// Method collectReplacementNodeIsPreservedNodeInfo
		/// </summary>
		private void CollectReplacementNodeIsPreservedNodeInfo()
		{
			replacementNodeIsPreservedNode = RectangularArrays.RectangularIntArray(n_graph_actions, max_n_replacement_nodes);

			//init the array with -1
			for(int i = 0; i < n_graph_actions; i++)
			{
				for(int j = 0; j < max_n_replacement_nodes; j++)
					replacementNodeIsPreservedNode[i][j] = -1;
			}

			//for all nodes preserved set the corresponding array entry to the
			//appropriate pattern node number
			foreach(Rule action in actionRuleMap.Keys)
			{
				int act_id = actionRuleMap[action];

				if(action.Right != null)
				{
					//compute the set of replacement nodes preserved by this action
					ISet<Node> replacement_nodes_preserved = new HashSet<Node>();
					replacement_nodes_preserved.AddAll(action.Right.Nodes);
					replacement_nodes_preserved.RetainAll(action.Pattern.GetNodes());

					//for all those preserved replacement nodes store the
					//corresponding pattern node
					foreach(Node node in replacement_nodes_preserved)
					{
						int node_num = replacement_node_num[act_id][node];
						replacementNodeIsPreservedNode[act_id][node_num] = pattern_node_num[act_id][node];
					}
				}
			}

		}

		/// <summary>
		/// Method coolectPatternNodesToBeKeptInfo
		/// </summary>
		private void CollectPatternNodesToBeKeptInfo()
		{
			patternNodeIsToBeKept = RectangularArrays.RectangularIntArray(n_graph_actions, max_n_pattern_nodes);

			//init the arrays with -1
			for(int i = 0; i < n_graph_actions; i++)
			{
				for(int j = 0; j < max_n_pattern_nodes; j++)
					patternNodeIsToBeKept[i][j] = -1;
			}

			//for all nodes to be kept set the corresponding array entry to the
			//appropriate replacement node number
			foreach(Rule action in actionRuleMap.Keys)
			{
				int act_id = actionRuleMap[action];

				//compute the set of pattern nodes to be kept for this action
				ISet<Node> pattern_nodes_to_keep = new HashSet<Node>();
				pattern_nodes_to_keep.AddAll(action.Pattern.GetNodes());
				if(action.Right != null)
				{
					PatternGraphBase replacement = action.Right;
					pattern_nodes_to_keep.RetainAll(replacement.Nodes);
					//iterate over the pattern nodes to be kept and store their
					//corresponding replacement node number
					foreach(Node node in pattern_nodes_to_keep)
					{
						int node_num = pattern_node_num[act_id][node];
						patternNodeIsToBeKept[act_id][node_num] = replacement_node_num[act_id][node];
					}
				}
			}
		}

		/// <summary>
		/// Decompose the given expression into all subexpressions, which are not
		/// AND-operators, and store the roots of these subexpression into a
		/// <tt>Collection</tt>. </summary>
		/// <param name="expr">                an Expression </param>
		/// <returns>   the <tt>Collection</tt> of all subexpressions
		/// 				not being an AND-Operator </returns>
		protected internal virtual ICollection<Expression> DecomposeAndParts(Expression expr)
		{
			ICollection<Expression> ret = new HashSet<Expression>();
			//step recursive into the expression tree
			_recursiveDecomposeAnd(ret, expr);

			return ret;
		}

		// decomposeAndParts() is only a wrapper method for this recursive method
		private void _recursiveDecomposeAnd(ICollection<Expression> col, Expression expr)
		{
			if(expr is Operator && ((Operator)expr).OpCode == OperatorCode.LOG_AND)
			{
				//step into subexpressions
				Operator andOp = (Operator)expr;
				for(int i = 0; i < andOp.Arity(); i++)
					_recursiveDecomposeAnd(col, andOp.GetOperand(i));
			}
			else
			{
				//expr is not an AND-Operator...
				//...so add the expr to the Collection
				col.Add(expr);
			}
		}

		/// <summary>
		/// Collects all pairs (node_num. attr_id) occuring in the qualifications
		/// at the leafes of the given expression and stores them in a map, which
		/// map node_numbers to collections of attr_ids </summary>
		/// <param name="act_id">              the id of the action the expr is condition of </param>
		/// <param name="map">                 a  Map </param>
		/// <param name="expr">                an Expression </param>
		protected internal virtual void _recursiveQualCollect(int act_id, IDictionary<Node, ICollection<int>> node_map, IDictionary<Edge, ICollection<int>> edge_map, Expression expr)
		{
			if(expr == null)
				return;

			//recursive descent
			if(expr is Operator)
			{
				for(int i = 0; i < ((Operator)expr).Arity(); i++)
					_recursiveQualCollect(act_id, node_map, edge_map, ((Operator)expr).GetOperand(i));
			}

			//get (node_num, attr_id) pairs from qualifications
			if(expr is Qualification)
			{
				Qualification qual = (Qualification)expr;
				Entity owner = qual.Owner;
				Entity member = qual.Member;

				//if owner is a node, add to the node_map
				if(owner is Node)
				{
					Node node = (Node)owner;
					//Integer node_num = (Integer) pattern_node_num[ act_id ].get( owner );
					int? attr_id = nodeAttrMap[member];

					//add the pair (node_num, attr_id to the map)
					if(node_map.ContainsKey(node))
						node_map[node].Add(attr_id);
					else
					{
						ICollection<int> newCol = new HashSet<int>();
						newCol.Add(attr_id);
						node_map[node] = newCol;
					}
				}

				//if owner is an edge. add to the edge_map
				if(owner is Edge)
				{
					Edge egde = (Edge)owner;
					//Integer edge_num = (Integer) pattern_edge_num[act_id].get(owner);
					int? attr_id = edgeAttrMap[member];

					//add the pair (edge_num, attr_id to the map)
					if(edge_map.ContainsKey(egde))
						edge_map[egde].Add(attr_id);
					else
					{
						ICollection<int> newCol = new SortedSet<int>(integerComparator);
						newCol.Add(attr_id);
						edge_map[egde] = newCol;
					}
				}
			}
		}

		/// <summary>
		/// Collects all nodes attributes of which occur in the given expression </summary>
		/// <param name="expr">                the expression </param>
		/// <returns>   a Collection of all that nodes </returns>
		protected internal virtual ICollection<Node> CollectInvolvedNodes(Expression expr)
		{
			ICollection<Node> ret = new HashSet<Node>(); // the Collection to be returned
			//step down into the expression and collect all involved graph nodes
			_recursiveNodeCollect(ret, expr);

			return ret;
		}

		private void _recursiveNodeCollect(ICollection<Node> col, Expression expr)
		{
			if(expr == null)
				return;

			if(expr is Operator)
			{
				for(int i = 0; i < ((Operator)expr).Arity(); i++)
					_recursiveNodeCollect(col, ((Operator)expr).GetOperand(i));
			}

			if(expr is Qualification)
			{
				Entity ent = ((Qualification)expr).Owner;
				// if the qualification selects an attr from a node, add that node
				if(ent is Node)
					col.Add((Node)ent);
			}
		}

		/// <summary>
		/// Collects all edges attributes of which occur in the given expresion </summary>
		/// <param name="expr">                the expression </param>
		/// <returns>   a Collection of all that edges </returns>
		protected internal virtual ICollection<Edge> CollectInvolvedEdges(Expression expr)
		{
			ICollection<Edge> ret = new HashSet<Edge>(); // the Collection to be returned
			//step down into the expression and collect all involved graph nodes
			_recursiveEdgeCollect(ret, expr);

			return ret;
		}

		private void _recursiveEdgeCollect(ICollection<Edge> col, Expression expr)
		{
			if(expr == null)
				return;

			if(expr is Operator)
			{
				for(int i = 0; i < ((Operator)expr).Arity(); i++)
					_recursiveEdgeCollect(col, ((Operator)expr).GetOperand(i));
			}

			if(expr is Qualification)
			{
				Entity ent = ((Qualification)expr).Owner;
				// if the qualification selects an attr from an edge, add that edge
				if(ent is Edge)
					col.Add((Edge)ent);
			}
		}

		/*
		 * collect some information needed for code gen process of the graph
		 * type model data structures
		 */
		protected internal virtual void CollectGraphTypeModelInfo()
		{
			/* overall number of node and edge types */
			n_node_types = GetIDs(true).Length;
			n_edge_types = GetIDs(false).Length;

			/* overall number of enum types */
			n_enum_types = enumMap.Count;

			/* overall number of node and edge attributes declared in the grg file */
			n_node_attrs = nodeAttrMap.Count;
			n_edge_attrs = edgeAttrMap.Count;

			/* get the inheritance information of the node and edge types */
			node_is_a_matrix = GetIsAMatrix(true);
			edge_is_a_matrix = GetIsAMatrix(false);

			/* count the number of attrs a node type has */
			n_attr_of_node_type = new int[n_node_types];
			//fill that array with 0
			for(int i = 0; i < n_node_types; i++)
				n_attr_of_node_type[i] = 0;

			//count number of attributes
			foreach(Entity attr in nodeAttrMap.Keys)
			{
				Debug.Assert(attr.HasOwner(), "Thought, that the Entity represented a node class attr and that\n" + "thus there had to be a type that owned the entity, but there was non.");
				NodeType node_type = (NodeType)attr.Owner;
				//get the id of the node type, where the attr is declared in
				int node_type_id = nodeTypeMap[node_type];
				Debug.Assert(node_type_id < n_node_types, "Tried to use a node-type-id as array index, " + "but the id exceeded the number of node types");
				//increment the number of attributes for the declaring type...
				n_attr_of_node_type[node_type_id]++;
				//...but the attr is also contained in all sub types, i.e. increment there too
				for(int nt_id = 0; nt_id < n_node_types; nt_id++)
				{
					if(node_is_a_matrix[nt_id][node_type_id] > 0)
						n_attr_of_node_type[nt_id]++;
				}
			}

			/* count the number of attrs an edge type has */
			n_attr_of_edge_type = new int[n_edge_types];
			//fill that array with 0
			for(int i = 0; i < n_edge_types; i++)
				n_attr_of_edge_type[i] = 0;

			//count number of attributes
			foreach(Entity attr in edgeAttrMap.Keys)
			{
				Debug.Assert(attr.HasOwner(), "Thought, that the Entity represented an edge class attr and that\n" + "thus there had to be a type that owned the entity, but there was non.");
				EdgeType edge_type = (EdgeType)attr.Owner;
				//get the id of the edge type, where the attr is declared in
				int edge_type_id = edgeTypeMap[edge_type];
				Debug.Assert(edge_type_id < n_edge_types, "Tried to use an edge-type-id as array index," + "but the id exceeded the number of edge types");
				//increment the number of attributes for the declaring type...
				n_attr_of_edge_type[edge_type_id]++;
				//...but the attr is also contained in all sub types, i.e. increment there too
				for(int et_id = 0; et_id < n_edge_types; et_id++)
				{
					if(edge_is_a_matrix[et_id][edge_type_id] > 0)
						n_attr_of_edge_type[et_id]++;
				}
			}

			/* collect all needed information about node attributes */
			node_attr_info = new AttrTypeDescriptor[n_node_attrs];
			foreach(Entity attr in nodeAttrMap.Keys)
			{
				Debug.Assert(attr.HasOwner(), "Thought, that the Entity represented an node attr and that thus\n" + "there had to be a type that owned the entity, but there was non.");
				NodeType node_type = (NodeType)attr.Owner;
				int node_type_id = nodeTypeMap[node_type];
				int attr_id = nodeAttrMap[attr];

				node_attr_info[attr_id] = new AttrTypeDescriptor();
				//set the attr id
				node_attr_info[attr_id].attr_id = attr_id;
				//get the attributes name
				node_attr_info[attr_id].name = attr.Ident.ToString();
				//get the owners type id
				node_attr_info[attr_id].decl_owner_type_id = node_type_id;
				//get the attributes kind
				if(attr.Type is IntType)
					node_attr_info[attr_id].kind = AttrTypeDescriptor.INTEGER;
				else if(attr.Type is BooleanType)
					node_attr_info[attr_id].kind = AttrTypeDescriptor.BOOLEAN;
				else if(attr.Type is StringType)
					node_attr_info[attr_id].kind = AttrTypeDescriptor.STRING;
				else if(attr.Type is EnumType)
				{
					node_attr_info[attr_id].kind = AttrTypeDescriptor.ENUM;
					node_attr_info[attr_id].enum_id = enumMap[(EnumType)attr.Type];
				}
				else
				{
					Console.Error.WriteLine("Key element of AttrNodeMap has a type, which is " + "neither one of 'int', 'boolean', 'string' nor an enumeration type.");
					Environment.Exit(0);
				}
			}

			/* collect all needed information about edge attributes */
			edge_attr_info = new AttrTypeDescriptor[n_edge_attrs];
			foreach(Entity attr in edgeAttrMap.Keys)
			{
				Debug.Assert(attr.HasOwner(), "Thought, that the Entity represented an edge attr and that thus\n" + "there had to be a type that owned the entity, but there was non.");
				EdgeType edge_type = (EdgeType)attr.Owner;
				int edge_type_id = edgeTypeMap[edge_type];
				int attr_id = edgeAttrMap[attr];

				edge_attr_info[attr_id] = new AttrTypeDescriptor();
				//set the attr id
				edge_attr_info[attr_id].attr_id = attr_id;
				//get the attributes name
				edge_attr_info[attr_id].name = attr.Ident.ToString();
				//get the owners type id
				edge_attr_info[attr_id].decl_owner_type_id = edge_type_id;
				//get the attributes kind
				if(attr.Type is IntType)
					edge_attr_info[attr_id].kind = AttrTypeDescriptor.INTEGER;
				else if(attr.Type is BooleanType)
					edge_attr_info[attr_id].kind = AttrTypeDescriptor.BOOLEAN;
				else if(attr.Type is StringType)
					edge_attr_info[attr_id].kind = AttrTypeDescriptor.STRING;
				else if(attr.Type is EnumType)
				{
					edge_attr_info[attr_id].kind = AttrTypeDescriptor.ENUM;
					edge_attr_info[attr_id].enum_id = enumMap[(EnumType)attr.Type];
				}
				else
				{
					Console.Error.WriteLine("Key element of AttrEdgeMap has a type, which is " + "neither one of 'int', 'boolean', 'string' nor an enumeration type.");
					Environment.Exit(0);
				}
			}

			/* compute the attr layout of the node types given in the grg file */
			node_attr_index = RectangularArrays.RectangularIntArray(n_node_types, n_node_attrs);
			//for all node types...
			for(int nt = 0; nt < n_node_types; nt++)
			{
				//the index the current attr will get in the current node layout, if it's a member
				int attr_index = 0;
				//...and all node attribute IDs...
				for(int attr_id = 0; attr_id < n_node_attrs; attr_id++)
				{
					//...check whether the attr is owned by the node type or one of its supertype
					int owner = node_attr_info[attr_id].decl_owner_type_id;
					if(owner == nt || node_is_a_matrix[nt][owner] > 0)
					{
						//setup the attrs index in the layout of the current node type
						node_attr_index[nt][attr_id] = attr_index++;
					}
					else
					{
						//-1 means that the current attr is not a member of the current node type
						node_attr_index[nt][attr_id] = -1;
					}
				}
			}

			/* compute the attr layout of the edge types given in the grg file */
			edge_attr_index = RectangularArrays.RectangularIntArray(n_edge_types, n_edge_attrs);
			//for all edge types...
			for(int et = 0; et < n_edge_types; et++)
			{
				//the index the current attr will get in the current edge layout, if it's a member
				int attr_index = 0;
				//...and all edge attribute IDs...
				for(int attr_id = 0; attr_id < n_edge_attrs; attr_id++)
				{
					//...check whether the attr is owned by the edge type or one of its supertype
					int owner = edge_attr_info[attr_id].decl_owner_type_id;
					if(owner == et || edge_is_a_matrix[et][owner] > 0)
					{
						//setup the attrs index in the layout of the current node type
						edge_attr_index[et][attr_id] = attr_index++;
					}
					else
					{
						//-1 means that the current attr is not a member of the current node type
						edge_attr_index[et][attr_id] = -1;
					}
				}
			}

			//collect the information about the enumeration types
			enum_type_descriptors = new EnumDescriptor[n_enum_types];
			for(int et = 0; et < n_enum_types; et++)
				enum_type_descriptors[et] = new EnumDescriptor();

			foreach(EnumType enum_type in enumMap.Keys)
			{
				//store the info about the current enum type in an array...
				//...type id
				int enum_type_id = enumMap[enum_type];
				enum_type_descriptors[enum_type_id].type_id = enum_type_id;
				//...the identifier used in the grg-file to declare thar enum type
				enum_type_descriptors[enum_type_id].name = enum_type.Ident.ToString();
				//..the items in this enumeration type
				foreach(EnumItem item in enum_type.Items)
					enum_type_descriptors[enum_type_id].items.Add(item);
				//...the number of items
				enum_type_descriptors[enum_type_id].n_items = enum_type_descriptors[enum_type_id].items.Count;
			}
		}

		/// <summary>
		///  computes matrices for all actions which show whether two pattern nodes
		///   are allowed to be identified by the matcher 
		/// </summary>
		protected internal virtual void CollectPotHomInfo()
		{
			//tells whether two pattern nodes of a given action are pot hom or not
			//e.g. : potHomMatrices[act_id][node_1][node_2]
			//protected int potHomMatrices[][][];
			potHomNodeMatrices = RectangularArrays.RectangularIntArray(n_graph_actions, max_n_pattern_nodes, max_n_pattern_nodes);

			for(int i = 0; i < n_graph_actions; i++)
			{
				for(int j = 0; j < max_n_pattern_nodes; j++)
				{
					for(int k = 0; k < max_n_pattern_nodes; k++)
						potHomNodeMatrices[i][j][k] = 0;
				}
			}

			//got through that m,atrices and set cells to '1' if two nodes
			//are potentialy homomorphic
			foreach(Rule action in actionRuleMap.Keys)
			{
				PatternGraphLhs pattern = action.Pattern;
				foreach(Node node_1 in pattern.Nodes)
				{
					ICollection<Node> hom_of_node_1 = new HashSet<Node>();
					hom_of_node_1 = pattern.GetHomomorphic(node_1);

					foreach(Node node_2 in pattern.Nodes)
					{
						//check whether these to nodes are potentially homomorphic
						//the pattern graph of the currrent action
						if(hom_of_node_1.Contains(node_2))
						{
							int act_id = actionRuleMap[action];
							int node_1_num = pattern_node_num[act_id][node_1];
							int node_2_num = pattern_node_num[act_id][node_2];
							potHomNodeMatrices[act_id][node_1_num][node_2_num] = 1;
						}
					}
				}
			}
		}
	}

}
