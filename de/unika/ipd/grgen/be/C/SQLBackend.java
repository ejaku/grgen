/**
 * @author Sebastian Hack
 * @version $Id$
 */
package de.unika.ipd.grgen.be.C;

import java.util.Arrays;
import java.util.Collection;
import java.util.Comparator;
import java.util.HashMap;
import java.util.HashSet;
import java.util.Iterator;
import java.util.LinkedList;
import java.util.List;
import java.util.Map;
import java.util.Set;

import de.unika.ipd.grgen.be.rewrite.RewriteGenerator;
import de.unika.ipd.grgen.be.rewrite.RewriteHandler;
import de.unika.ipd.grgen.be.rewrite.SPORewriteGenerator;
import de.unika.ipd.grgen.be.sql.ExplicitJoinGenerator;
import de.unika.ipd.grgen.be.sql.PreferencesSQLParameters;
import de.unika.ipd.grgen.be.sql.SQLGenerator;
import de.unika.ipd.grgen.be.sql.SQLParameters;
import de.unika.ipd.grgen.be.sql.stmt.AttributeTable;
import de.unika.ipd.grgen.be.sql.stmt.DefaultGraphTableFactory;
import de.unika.ipd.grgen.be.sql.stmt.DefaultStatementFactory;
import de.unika.ipd.grgen.be.sql.stmt.TypeStatementFactory;
import de.unika.ipd.grgen.ir.Edge;
import de.unika.ipd.grgen.ir.Entity;
import de.unika.ipd.grgen.ir.Graph;
import de.unika.ipd.grgen.ir.Identifiable;
import de.unika.ipd.grgen.ir.MatchingAction;
import de.unika.ipd.grgen.ir.Node;
import de.unika.ipd.grgen.ir.Redirection;
import de.unika.ipd.grgen.ir.Rule;
import de.unika.ipd.grgen.ir.Type;
import de.unika.ipd.grgen.ir.Unit;
import de.unika.ipd.grgen.util.report.ErrorReporter;

/**
 * A generator to generate SQL statements for a grgen specification.
 */
public abstract class SQLBackend extends CBackend {
	
	/** if 0, the query should not be limited. */
	protected int limitQueryResults;
	
	/** Name of the database. */  
	protected String dbName;
	
	protected String dbNamePrefix = "gr_";
	
	protected String stmtPrefix = "stmt_";
	
	protected String nodeTypeIsAFunc = "node_type_is_a";
	
	protected String edgeTypeIsAFunc = "edge_type_is_a";
	
	private RewriteGenerator rewriteGenerator = new SPORewriteGenerator();
		
	protected SQLParameters parameters = new PreferencesSQLParameters();
	
	protected TypeStatementFactory factory = new DefaultStatementFactory(); 
	
	protected final SQLGenerator sqlGen = enableNT  
			? new ExplicitJoinGenerator(parameters, this)
			: new SQLGenerator(parameters, this); 
	
	protected Map matchMap = new HashMap();

	protected DefaultGraphTableFactory tableFactory;
	
	/**
	 * Make a new SQL Generator.
	 */
	public SQLBackend() {
	}
	
	/**
	 * Add a string define to a string buffer.
	 * The define must define a string, since the value parameter is
	 * formatted like a string.
	 * @param sb The string buffer.
	 * @param name The name of the define.
	 * @param value The define's value (must be a string constant).
	 */
	protected void addStringDefine(StringBuffer sb, String name, String value) {
		addDefine(sb, name, formatString(value));
	}
	
	/**
	 * Add a define to a string buffer.
	 * @param sb The string buffer.
	 * @param name The name of the define.
	 * @param value The define's value.
	 */
	protected void addDefine(StringBuffer sb, String name, String value) {
		sb.append("#define " + formatId(name) + " " + value + "\n");
	}
	
	/**
	 * Add C defines with all settings to a string buffer.
	 * @param sb The string buffer to add to.
	 */
	protected void addSettings(StringBuffer sb) {
		addStringDefine(sb, "DBNAME", dbName);
		addStringDefine(sb, "DBNAME_PREFIX", dbNamePrefix);
		addStringDefine(sb, "STMT_PREFIX", stmtPrefix);
		addStringDefine(sb, "NODE_TYPE_IS_A_FUNC", nodeTypeIsAFunc);
		addStringDefine(sb, "EDGE_TYPE_IS_A_FUNC", edgeTypeIsAFunc);
		addStringDefine(sb, "TABLE_NODES", parameters.getTableNodes());
		addStringDefine(sb, "TABLE_EDGES", parameters.getTableEdges());
		addStringDefine(sb, "TABLE_NODE_ATTRS", parameters.getTableNodeAttrs());
		addStringDefine(sb, "TABLE_EDGE_ATTRS", parameters.getTableEdgeAttrs());
		addStringDefine(sb, "COL_NODES_ID", parameters.getColNodesId());
		addStringDefine(sb, "COL_NODES_TYPE_ID", parameters.getColNodesTypeId());
		addStringDefine(sb, "COL_EDGES_ID", parameters.getColEdgesId());
		addStringDefine(sb, "COL_EDGES_TYPE_ID", parameters.getColEdgesTypeId());
		addStringDefine(sb, "COL_EDGES_SRC_ID", parameters.getColEdgesSrcId());
		addStringDefine(sb, "COL_EDGES_TGT_ID", parameters.getColEdgesTgtId());
		addStringDefine(sb, "COL_NODE_ATTR_NODE_ID", parameters.getColNodeAttrNodeId());
		addStringDefine(sb, "COL_EDGE_ATTR_EDGE_ID", parameters.getColEdgeAttrEdgeId());
		
		// Dump the databases type corresponding to the ID type.
		addStringDefine(sb, "DB_ID_TYPE", getIdType());
	}
	
	/**
	 * Get the SQL type to use for ids.
	 * @return The SQL type for ids.
	 */
	protected abstract String getIdType();
	
	/**
	 * Get the SQL type to use for the GRGEN type "int".
	 * @return The SQL type for int.
	 */
	protected abstract String getIntType();
	
	/**
	 * Get the SQL type to use for the GRGEN type "boolean".
	 * @return The SQL type for boolean.
	 */
	protected abstract String getBooleanType();
	
	/**
	 * Get the SQL "true" value for use with the SQL boolean
	 * type.
	 * @return The SQL true value for boolean.
	 */
	protected abstract String getTrueValue();
	
	/**
	 * Get the SQL "false" value for use with the SQL boolean
	 * type.
	 * @return The SQL false value for boolean.
	 */
	protected abstract String getFalseValue();
	
	/**
	 * Get the SQL type to use for the GRGEN type "string".
	 * @return The SQL type for string.
	 */
	protected abstract String getStringType();
	
	/**
	 * Generate code, that sends a query to the SQL server.
	 * @param sb The string buffer to put the code to.
	 * @param query The query.
	 */
	protected abstract void genQuery(StringBuffer sb, String query);
	
	
	/*
	private void makeJoin(StringBuffer sb, Graph gr, Edge e1, Edge e2) {
		Node[] nodes =
		{ gr.getSource(e1), gr.getTarget(e1), gr.getSource(e2), gr.getTarget(e2)};
		
		String[] names = {
			getEdgeCol(e1, colEdgesSrcId),
				getEdgeCol(e1, colEdgesTgtId),
				getEdgeCol(e2, colEdgesSrcId),
				getEdgeCol(e2, colEdgesTgtId),
		};
		
		for (int i = 0; i < nodes.length; i++)
			for (int j = i + 1; j < nodes.length; j++)
				addTo(
					sb,
					"",
					" AND ",
					names[i]
						+ (nodes[i].equals(nodes[j]) ? "=" : "<>")
						+ names[j]
						+ BREAK_LINE);
	}
	*/

	/*
	protected String genMatchStatement(
		MatchingAction act,
		List matchedNodes,
		List matchedEdges) {
		
		debug.entering();
		
		Graph gr = act.getPattern();
		StringBuffer nodeCols = new StringBuffer();
		StringBuffer edgeCols = new StringBuffer();
		StringBuffer nodeTables = new StringBuffer();
		StringBuffer edgeTables = new StringBuffer();
		StringBuffer nodeWhere = new StringBuffer();
		StringBuffer edgeWhere = new StringBuffer();
		Collection nodes = gr.getNodes(new HashSet());
		Collection edges = new HashSet();
		
		// Two sets for incoming/outgoing edges.
		Set[] incidentSets = new Set[] {
			new HashSet(), new HashSet()
		};
		
		// Edge table column for incoming/outgoing edges.
		final String[] incidentCols = new String[] {
			colEdgesSrcId, colEdgesTgtId
		};
		
		Set workset = new HashSet();
		workset.addAll(nodes);
		HashMap edgeNotEx = new HashMap();
		
		for (Iterator it = nodes.iterator(); it.hasNext();) {
			
			Node n = (Node) it.next();
			String mangledNode = mangleNode(n);
			String nodeCol = getNodeCol(n, colNodesId);
			
			int typeId = getTypeId(nodeTypeMap, n.getType());
			
			workset.remove(n);
			
			debug.report(NOTE, "node: " + n);
			
			// Add this node to the table and column list
			addToList(nodeTables, tableNodes + " AS " + mangledNode);
			addToList(nodeCols, nodeCol);
			
			// Add it also to the result list.
			matchedNodes.add(n);
			
			// Add node type constraint
			addToCond(nodeWhere, nodeTypeIsAFunc + "("
						  + getNodeCol(n, colNodesTypeId) + ", " + typeId + ")" + BREAK_LINE);
			
			// Make this node unequal to all other nodes.
			for (Iterator iter = workset.iterator(); iter.hasNext();) {
				Node other = (Node) iter.next();
				
				// Just add an <>, if the other node is not homomorphic to n
				// If it was, we cannot node, if it is equal or not equal to n
				if(!n.isHomomorphic(other))
					addToCond(nodeWhere, nodeCol + " <> "
								  + getNodeCol(other, colNodesId) + BREAK_LINE);
			}
			
			// TODO check for conditions of nodes.
			if (act instanceof Rule) {
				Rule r = (Rule)act;
				Condition condition = r.getCondition();
				for(Iterator conds = condition.getWalkableChildren(); conds.hasNext(); ) {
					IR cond = (IR)conds.next();
					if(cond instanceof Operator) {
						Operator operator = (Operator)cond;
						System.out.print(operator + " opcode = "  + operator.getOpCode());
						String sqlOp = getOpSQL(operator.getOpCode());
						System.out.println(" sqlOp = " + sqlOp);
						for(Iterator ops = operator.getWalkableChildren(); ops.hasNext(); ) {
							IR operand = (IR)ops.next();
							System.out.println(" operand = " + operand);
						}
					}
				}
			}
			
			incidentSets[0].clear();
			incidentSets[1].clear();
			gr.getOutgoing(n, incidentSets[0]);
			gr.getIncoming(n, incidentSets[1]);
			
			String lastJoinOn = nodeCol;
			
			
			// Make this node equal to all source and target nodes of the
			// outgoing and incoming edges.
			for(int i = 0; i < incidentSets.length; i++) {
				
				for (Iterator iter = incidentSets[i].iterator(); iter.hasNext();) {
					Edge e = (Edge) iter.next();
					String mangledEdge = mangleEdge(e);
					String edgeCol = getEdgeCol(e, incidentCols[i]);
					int edgeTypeId = getTypeId(edgeTypeMap, e.getType());
					
					debug.report(NOTE, "incident edge: " + e);
					
					// Ignore negated edges for now.
					// TODO Implement negated edges.
					if (e.isNegated()) {
						String condition =
							mangledEdge +
							(i==0?"."+colEdgesSrcId+" = ":"."+colEdgesTgtId+" = ") +
							// mangledNode == src | tgt
							mangledNode + "." + colNodesId +
							
							BREAK_LINE + "  AND " +
							edgeTypeIsAFunc + "("
							+ getEdgeCol(e, colEdgesTypeId) + ", "
							+ edgeTypeId + ")" + BREAK_LINE;
						
						if(edgeNotEx.containsKey(mangledEdge))
							edgeNotEx.put(mangledEdge,
										  edgeNotEx.get(mangledEdge)+
											  "  AND " + condition);
						else edgeNotEx.put(mangledEdge, condition);
						
						continue;
					}
					
					// TODO check for conditions of edges.
					
					
					// Just add the edge to the columns and tables,
					// if it didn't occur before.
					if (!edges.contains(e)) {
						addToList(edgeTables, tableEdges + " AS " + mangledEdge);
						addToList(edgeCols, getEdgeCol(e, colEdgesId));
						edges.add(e);
						
						// Add edge type constraint
						addToCond(edgeWhere, edgeTypeIsAFunc + "("
									  + getEdgeCol(e, colEdgesTypeId) + ", "
									  + edgeTypeId + ")" + BREAK_LINE);
						
						// Add it also to the edge result list.
						matchedEdges.add(e);
					}
					
					// Add = for all edges, that are incident to the current node.
					addToCond(nodeWhere, lastJoinOn + " = " + edgeCol + BREAK_LINE);
					lastJoinOn = edgeCol;
				}
			}
		}
		
		for (Iterator iter = edgeNotEx.keySet().iterator();iter.hasNext();) {
			String mangledEdge=(String)iter.next();
			addToCond(edgeWhere, "NOT EXISTS (" + BREAK_LINE +
						  "  SELECT " + mangledEdge +
						  ".edge_id "+ BREAK_LINE +
						  "  FROM edges AS " + mangledEdge +
						  BREAK_LINE + "  WHERE "+
						  edgeNotEx.get(mangledEdge)+
						  ")"
					 );
		}
		
		debug.leaving();
		
		return "SELECT "
			+ join(nodeCols, edgeCols, ", ")
			+ BREAK_LINE
			+ " FROM "
			+ join(nodeTables, edgeTables, ", ")
			+ BREAK_LINE
			+ " WHERE "
			+ join(nodeWhere, edgeWhere, " AND ")
			+ (limitQueryResults != 0 ? " LIMIT " + limitQueryResults : "");
	}
	
	private String getOpSQL(int opCode) {
		switch (opCode) {
			case Operator.COND:      assert false : "NYI"; break;
			case Operator.LOG_OR:    return "OR";
			case Operator.LOG_AND:   return "AND";
			case Operator.BIT_OR:    assert false : "NYI"; break;
			case Operator.BIT_XOR:   assert false : "NYI"; break;
			case Operator.BIT_AND:   assert false : "NYI"; break;
			case Operator.EQ:        return "=";
			case Operator.NE:        return "<>";
			case Operator.LT:        return "<";
			case Operator.LE:        return "<=";
			case Operator.GT:        return ">";
			case Operator.GE:        return ">=";
			case Operator.SHL:       assert false : "NYI"; break;
			case Operator.SHR:       assert false : "NYI"; break;
			case Operator.BIT_SHR:   assert false : "NYI"; break;
			case Operator.ADD:       return "+";
			case Operator.SUB:       return "-";
			case Operator.MUL:       return "*";
			case Operator.DIV:       return "/";
			case Operator.MOD:       return "%";
			case Operator.LOG_NOT:   return "NOT";
			case Operator.BIT_NOT:   assert false : "NYI"; break;
			case Operator.NEG:       return "-";
			case Operator.CAST:      assert false : "NYI"; break;
		}
		// TODO
		return null;
	}
	*/
	
	/**
	 * Output some data structures needed especially by the SQL backend.
	 * @param sb The string buffer to put it to.
	 */
	protected void makeActionTypes(StringBuffer sb) {
		sb.append(
			"typedef struct {\n"
				+ "  MATCH_PROTOTYPE((*matcher));\n"
				+ "  FINISH_PROTOTYPE((*finisher));\n"
				+ "  const char **stmt;\n"
				+ "  const char *name;\n"
				+ "} action_impl_t;\n");
	}
	
	/**
	 * An auxillary class for match processing.
	 * It just stores some data needed in the
	 * {@link SQLBackend#genMatch(StringBuffer, MatchingAction, int)}
	 * {@link SQLBackend#genMatchStatement(MatchingAction, List, List)}
	 * routines.
	 */
	private static class Match {
		protected int id;
		protected Map nodeIndexMap = new HashMap();
		protected Map edgeIndexMap = new HashMap();
		protected String matchIdent;
		protected String finishIdent;
		protected String stmtIdent;
		
		protected Match(int id, List nodes, List edges) {
			this.id = id;
			
			int i;
			Iterator it;
			
			for (i = 0, it = nodes.iterator(); it.hasNext(); i++)
				nodeIndexMap.put(it.next(), new Integer(i));
			
			for (i = 0, it = edges.iterator(); it.hasNext(); i++)
				edgeIndexMap.put(it.next(), new Integer(i));
		}
		
		protected static final Comparator comparator = new Comparator() {
			public int compare(Object x, Object y) {
				Match m = (Match) x;
				Match n = (Match) y;
				
				if (m.id < n.id)
					return -1;
				if (m.id > n.id)
					return 1;
				
				return 0;
			}
			
			public boolean equals(Object obj) {
				return obj == this;
			}
		};
	}
	
	private class SQLRewriteHandler implements RewriteHandler {
		
		private Match match;
		private StringBuffer sb;
		private Rule rule;
		private Map insertedNodesIndexMap = new HashMap();
		private Collection nodesToInsert = null;
				
		SQLRewriteHandler(Match match, StringBuffer sb) {
			this.match = match;
			this.sb = sb;
		}
		
		public void start(Rule rule) {
			this.rule = rule;
		}
		
		
		public void finish() {
		}
		
		/**
		 * @see de.unika.ipd.grgen.be.rewrite.RewriteHandler#changeNodeTypes(java.util.Map)
		 */
		public void changeNodeTypes(Map nodeTypeMap) {
			for(Iterator it = nodeTypeMap.keySet().iterator(); it.hasNext();) {
				Node n = (Node) it.next();
				Integer nid = (Integer) match.nodeIndexMap.get(n);
				int tid = getId(n.getReplaceType());
				sb.append("  CHANGE_NODE_TYPE(GET_MATCH_NODE(" + nid + "), " + tid + ");\n");
			}
		}

		/**
		 * @see de.unika.ipd.grgen.be.rewrite.RewriteHandler#deleteEdges(java.util.Collection)
		 */
		public void deleteEdges(Collection edges) {
			for (Iterator it = edges.iterator(); it.hasNext();) {
				Edge e = (Edge) it.next();
				sb.append("  DELETE_EDGE(GET_MATCH_EDGE(" + match.edgeIndexMap.get(e) + "));\n");
			}
		}

		/**
		 * @see de.unika.ipd.grgen.be.rewrite.RewriteHandler#deleteEdgesOfNodes(java.util.Collection)
		 */
		public void deleteEdgesOfNodes(Collection nodes) {
			for (Iterator it = nodes.iterator(); it.hasNext();) {
				Node n = (Node) it.next();
				Integer nid = (Integer) match.nodeIndexMap.get(n);
				sb.append("  DELETE_NODE_EDGES(GET_MATCH_NODE(" + nid + "));\n");
			}
		}

		/**
		 * @see de.unika.ipd.grgen.be.rewrite.RewriteHandler#deleteNodes(java.util.Collection)
		 */
		public void deleteNodes(Collection nodes) {
			for (Iterator it = nodes.iterator(); it.hasNext();) {
				Node n = (Node) it.next();
				Integer nid = (Integer) match.nodeIndexMap.get(n);
				sb.append("  DELETE_NODE(GET_MATCH_NODE(" + nid + "));\n");
			}
		}

		/**
		 * @see de.unika.ipd.grgen.be.rewrite.RewriteHandler#insertEdges(java.util.Collection)
		 */
		public void insertEdges(Collection edges) {
			Graph right = rule.getRight();
			
			for (Iterator it = edges.iterator(); it.hasNext();) {
				Edge e = (Edge) it.next();
				
				int etid = getId(e.getEdgeType());
				Node src = right.getSource(e);
				Node tgt = right.getTarget(e);
				String leftNode, rightNode;
				
				if (nodesToInsert.contains(src))
					leftNode = "inserted_nodes[" + insertedNodesIndexMap.get(src) + "]";
				else
					leftNode = "GET_MATCH_NODE(" + match.nodeIndexMap.get(src) + ")";
				
				if (nodesToInsert.contains(tgt))
					rightNode = "inserted_nodes[" + insertedNodesIndexMap.get(tgt) + "]";
				else
					rightNode = "GET_MATCH_NODE(" + match.nodeIndexMap.get(tgt) + ")";
				
				sb.append(
					"  INSERT_EDGE(" + etid + ", " + leftNode + ", " + rightNode + ");\n");
			}

		}

		/**
		 * @see de.unika.ipd.grgen.be.rewrite.RewriteHandler#insertNodes(java.util.Collection)
		 */
		public void insertNodes(Collection nodes) {
			nodesToInsert = nodes;
			
			/*
			 * We need an array to save the IDs of the inserted nodes, since
			 * they might be needed when inserting the new edges further down
			 * this routine.
			 */
			sb.append("  gr_id_t inserted_nodes[" + nodes.size() + "];\n");
			
			/*
			 * Generate node creation statements and save the newly created
			 * IDs in the array.
			 */
			int i = 0;
			for (Iterator it = nodes.iterator(); it.hasNext(); i++) {
				Node n = (Node) it.next();
				sb.append("  inserted_nodes[" + i + "] = INSERT_NODE("
							  + getId(n.getNodeType()) + ");\n");
				insertedNodesIndexMap.put(n, new Integer(i));
			}

		}
		/**
		 * @see de.unika.ipd.grgen.be.rewrite.RewriteHandler#getRequiredRewriteGenerator()
		 */
		public Class getRequiredRewriteGenerator() {
			return SPORewriteGenerator.class;
		}
	}
	
	/**
	 * Make the finish code of a rule
	 * @param sb The string buffer to put the code to.
	 * @param r The rule to make the finish code for.
	 * @param id The id number of the rule.
	 * @param m The match structure as supplied by
	 * {@link #genMatch(StringBuffer, MatchingAction, int)}.
	 * @deprecated We use the {@link RewriteGenerator}/{@link RewriteHandler} mechanism.
	 */
	protected void genRuleFinishOld(StringBuffer sb, Rule r, int id, Match m) {
		Collection commonNodes = r.getCommonNodes();
		Collection commonEdges = r.getCommonEdges();
		Graph right = r.getRight();
		Graph left = r.getLeft();
		Set negatedEdges = left.getNegatedEdges();
		Map insertedNodesIndexMap = new HashMap();
		Collection w, nodesToInsert;
		int i;
		
		/*
		 * First of all, add the nodes that have to be inserted.
		 * This makes the redirections possible. They can only be applied,
		 * if all nodes (the ones to be deleted, and the ones to be inserted)
		 * are present.
		 */
		nodesToInsert = right.getNodes(new HashSet());
		nodesToInsert.removeAll(commonNodes);
		
		/*
		 * Only consider redirections and node insertions, if we truly have
		 * to insert some nodes, i.e. The nodesToInsert set has elements
		 */
		if (nodesToInsert.size() > 0) {
			/*
			 * We need an array to save the IDs of the inserted nodes, since
			 * they might be needed when inserting the new edges further down
			 * this routine.
			 */
			sb.append("  gr_id_t inserted_nodes[" + nodesToInsert.size() + "];\n");
			
			/*
			 * Generate node creation statements and save the newly created
			 * IDs in the array.
			 */
			i = 0;
			for (Iterator it = nodesToInsert.iterator(); it.hasNext(); i++) {
				Node n = (Node) it.next();
				sb.append("  inserted_nodes[" + i + "] = INSERT_NODE("
							  + getId(n.getNodeType()) + ");\n");
				insertedNodesIndexMap.put(n, new Integer(i));
			}
			
			/*
			 * Now we can launch the redirections.
			 */
			for (Iterator it = r.getRedirections().iterator(); it.hasNext();) {
				Redirection redir = (Redirection) it.next();
				String dir = (redir.incoming ? "INCOMING" : "OUTGOING");
				
				// The "from" node must me in the matched nodes, since it is a left
				// hand side node.
				Integer fromId = (Integer) m.nodeIndexMap.get(redir.from);
				assert fromId != null : "\"From\" node must be available";
				
				// The "to" node must be in the nodesToInsert set, since it
				// must be a right hand side node.
				Integer toId = (Integer) insertedNodesIndexMap.get(redir.to);
				assert toId != null : "\"To\" node must be available";
				
				sb.append("  REDIR_" + dir + "(GET_MATCH_NODE("
							  + fromId + ")" + ", inserted_nodes[" + toId + "], "
							  + getId(redir.edgeType) + ", "
							  + getId(redir.nodeType) + ");\n");
			}
		}
		
		/*
		 * All edges, that occur only on the left side or are negated
		 * edges have to be removed.
		 */
		w = left.getEdges(new HashSet());
		w.removeAll(commonEdges);
		w.removeAll(left.getNegatedEdges());
		
		for (Iterator it = w.iterator(); it.hasNext();) {
			Edge e = (Edge) it.next();
			if (!e.isNegated())
				sb.append(
					"  DELETE_EDGE(GET_MATCH_EDGE(" + m.edgeIndexMap.get(e) + "));\n");
		}
		
		w = left.getNodes(new HashSet());
		for (Iterator it = w.iterator(); it.hasNext();) {
			Node n = (Node) it.next();
			Integer nid = (Integer) m.nodeIndexMap.get(n);
			if (n.typeChanges()) {
				int tid = getId(n.getReplaceType());
				sb.append(
					"  CHANGE_NODE_TYPE(GET_MATCH_NODE(" + nid + "), " + tid + ");\n");
			}
		}
		
		w.removeAll(commonNodes);
		for (Iterator it = w.iterator(); it.hasNext();) {
			Node n = (Node) it.next();
			Integer nid = (Integer) m.nodeIndexMap.get(n);
			sb.append("  DELETE_NODE_EDGES(GET_MATCH_NODE(" + nid + "));\n");
			sb.append("  DELETE_NODE(GET_MATCH_NODE(" + nid + "));\n");
		}
		
		// Right side edges cannot be negated. That is checked by
		// the semantic analysis
		w = right.getEdges(new HashSet());
		w.removeAll(commonEdges);
		
		for (Iterator it = w.iterator(); it.hasNext();) {
			Edge e = (Edge) it.next();
			
			if (e.isNegated())
				// TODO ### nyi implement negated edges!
				// What does it mean anyway in a replace statment???
				continue;
			
			int etid = getId(e.getEdgeType());
			Node src = right.getSource(e);
			Node tgt = right.getTarget(e);
			String leftNode, rightNode;
			
			if (nodesToInsert.contains(src))
				leftNode = "inserted_nodes[" + insertedNodesIndexMap.get(src) + "]";
			else
				leftNode = "GET_MATCH_NODE(" + m.nodeIndexMap.get(src) + ")";
			
			if (nodesToInsert.contains(tgt))
				rightNode = "inserted_nodes[" + insertedNodesIndexMap.get(tgt) + "]";
			else
				rightNode = "GET_MATCH_NODE(" + m.nodeIndexMap.get(tgt) + ")";
			
			sb.append(
				"  INSERT_EDGE(" + etid + ", " + leftNode + ", " + rightNode + ");\n");
		}
	}
	
	/**
	 * @see de.unika.ipd.grgen.be.C.CBackend#genFinish(java.lang.StringBuffer, de.unika.ipd.grgen.ir.MatchingAction, int)
	 */
	protected void genFinish(StringBuffer sb, MatchingAction a, int id) {
		String actionIdent = formatId(a.getIdent().toString());
		String finishIdent = "finish_" + actionIdent;
		
		Match m = (Match) matchMap.get(a);
		m.finishIdent = finishIdent;
		
		assert m != null : "A match must have been produced for " + a;
		
		sb.append("static FINISH_PROTOTYPE(" + finishIdent + ")\n{\n");
		
		if(a instanceof Rule) {
			Rule r = (Rule) a;
			rewriteGenerator.rewrite(r, new SQLRewriteHandler(m, sb));
		}
			
		// genRuleFinish(sb, (Rule) a, id, m);
		
		sb.append("  return 1;\n}\n\n");
	}
	
	/**
	 * @see de.unika.ipd.grgen.be.C.CBackend#genMatch(java.lang.StringBuffer, de.unika.ipd.grgen.ir.MatchingAction, int)
	 */
	protected void genMatch(StringBuffer sb, MatchingAction a, int id) {
		System.out.println(a.getIdent().toString());
		
		String actionIdent = mangle(a);
		String stmtIdent = "stmt_" + actionIdent;
		String matchIdent = "match_" + actionIdent;
		String nodeNamesIdent = "node_names_" + actionIdent;
		String edgeNamesIdent = "edge_names_" + actionIdent;
		List nodes = new LinkedList();
		List edges = new LinkedList();
		Iterator it;
		
		// Dump the SQL statement
		sb.append("static const char *stmt_" + actionIdent + " = \n");
		sb.append(formatString(sqlGen.genMatchStatement(a, nodes, edges, tableFactory, factory)) + ";\n\n");
		
		// Make an array of strings that contains the node names.
		sb.append("static const char *" + nodeNamesIdent + "[] = {\n");
		for (it = nodes.iterator(); it.hasNext();) {
			Identifiable node = (Identifiable) it.next();
			sb.append("  " + formatString(node.getIdent().toString()) + ", \n");
		}
		sb.append("};\n\n");
		
		// Make an array of strings that contains the edge names.
		sb.append("static const char *" + edgeNamesIdent + "[] = {\n");
		for (it = edges.iterator(); it.hasNext();) {
			Identifiable edge = (Identifiable) it.next();
			sb.append("  " + formatString(edge.getIdent().toString()) + ", \n");
		}
		sb.append("};\n\n");
		
		// Make the function that invokes the SQL statement.
		sb.append("static MATCH_PROTOTYPE(" + matchIdent + ")\n{\n");
		sb.append("  QUERY(" + id + ", " + stmtIdent + ");\n");
		sb.append(
			"  MATCH_GET_RES("
				+ nodes.size()
				+ ", "
				+ edges.size()
				+ ", "
				+ nodeNamesIdent
				+ ", "
				+ edgeNamesIdent
				+ ");\n");
		sb.append("}\n\n");
		
		Match m = new Match(id, nodes, edges);
		m.matchIdent = matchIdent;
		m.stmtIdent = stmtIdent;
		matchMap.put(a, m);
	}
	
	/**
	 * All generated statements in the statement map {@link statements}
	 * are emitted in an extra file.
	 */
	protected void genExtra() {
		StringBuffer sb = new StringBuffer();
		
		// Emit an include file for Makefiles
		sb = new StringBuffer();
		sb.append("#\n# generated by grgen, don't edit\n#\n");
		sb.append("UNIT_NAME = " + formatId(unit.getIdent().toString()) + "\n");
		sb.append("DB_NAME = " + dbName + "\n");
		writeFile("unit.mak", sb);
		
		// Make some additional types needed for the action implementation.
		sb = new StringBuffer();
		makeActionTypes(sb);
		writeFile("action_types" + incExtension, sb);
		
		// Make action information
		sb = new StringBuffer();
		sb.append("static const action_impl_t action_impl_map[] = {\n");
		
		Object[] matches = matchMap.values().toArray();
		Arrays.sort(matches, Match.comparator);
		for (int i = 0; i < matches.length; i++) {
			Match m = (Match) matches[i];
			sb.append(
				"  { "
					+ m.matchIdent
					+ ", "
					+ m.finishIdent
					+ ", &"
					+ m.stmtIdent
					+ ", "
					+ formatString(m.stmtIdent)
					+ " },\n");
		}
		sb.append("};\n");
		writeFile("action_impl_map" + incExtension, sb);
		
		// Emit the settings specified in the grgen config file.
		// these contain table and column names, etc.
		sb = new StringBuffer();
		addSettings(sb);
		writeFile("settings" + incExtension, sb);
		
		// creation of ATTR tables
		genAttrTableCmd();
	}
	
	/**
	 * Do some additional stuff on initialization.
	 */
	public void init(Unit unit, ErrorReporter reporter, String outputPath) {
		super.init(unit, reporter, outputPath);
		this.dbName = dbNamePrefix + unit.getIdent().toString();
		makeTypes();
		
		tableFactory = new DefaultGraphTableFactory(parameters, nodeAttrMap.keySet(),
				edgeAttrMap.keySet());
	}
	
	/**
	 * returns the SQL type to a given IR type
	 * @param ty The IR type
	 * @return The SQL type
	 */
	private String getSQLType(Type ty) {
		switch (ty.classify()) {
			case Type.IS_INTEGER:
				return getIntType();
			case Type.IS_STRING:
				return getStringType();
			case Type.IS_BOOLEAN:
				return getBooleanType();
			default:
				// bad, unknown type
				error.error(ty.getIdent().getCoords(), "Type " + ty + " cannot be represented in SQL.");
				return "???";
		}
	}
	
	protected void genAttrTableGetAndSet(StringBuffer sb, String name, AttributeTable table) {
		
		sb.append("#define GR_HAVE_").append(name.toUpperCase()).append("_ATTR 1\n\n");

		sb.append("static const char *cmd_create_" + name + "_attr = \n\"");
		table.dumpDecl(sb);
		sb.append("\";\n\n");

		sb.append("static prepared_query_t cmd_get_").append(name).append("_attr[] = {\n");
		for(int i = 0; i < table.columnCount(); i++) {
			sb.append("  { \"");
			table.genGetStmt(sb, table.getColumn(i));
			sb.append("\", -1 },\n");
		}
		sb.append("  { NULL, -1 }\n");
		sb.append("};\n\n");
		
		sb.append("static prepared_query_t cmd_set_").append(name).append("_attr[] = {\n");
		for(int i = 0; i < table.columnCount(); i++) {
			sb.append("  { \"");
			table.genUpdateStmt(sb, table.getColumn(i));
			sb.append("\", -1 },\n");
		}
		sb.append("  { NULL, -1 }\n");
		sb.append("};\n\n");
	}
	
	protected void genAttrTableCmd() {
		StringBuffer sb = new StringBuffer();

		sb.append("\n/** The boolean True Value */\n");
		addStringDefine(sb, "GR_BOOLEAN_TRUE", getTrueValue());
		
		sb.append("\n/** The boolean False Value */\n");
		addStringDefine(sb, "GR_BOOLEAN_FALSE", getFalseValue());
		
		sb.append("\n");
		
		genAttrTableGetAndSet(sb, "node", tableFactory.originalNodeAttrTable());
		genAttrTableGetAndSet(sb, "edge", tableFactory.originalEdgeAttrTable());		
		
		writeFile("attr_tbl_cmd" + incExtension, sb);
	}
	
	/**
	 * Creates the commands for creating attribute tables
	 */
	protected void genAttrTableCmdOld() {
		StringBuffer sb;
		Map maps[]         = new Map[]    { nodeAttrMap,           edgeAttrMap };
		Map ty_maps[]      = new Map[]    { nodeTypeMap,           edgeTypeMap };
		String tbl_names[] = new String[] { parameters.getTableNodeAttrs(), 
				parameters.getTableEdgeAttrs() };
		String col_names[] = new String[] { parameters.getColNodesId(),
				parameters.getColEdgesId() };
		String names[]     = new String[] { "node",               "edge" };
		String defines[]   = new String[] { "GR_HAVE_NODE_ATTR",  "GR_HAVE_EDGE_ATTR" };
		
		sb = new StringBuffer();
		sb.append("/*\n * This file was generated by grgen, don't edit\n */\n\n");
		
		// create node & edge attributes create table commands
		for (int i = 0; i < maps.length; ++i) {
			
			if (maps[i].size() > 0)
				sb.append("#define " + defines[i] + " 1\n");
			else
				sb.append("#undef " + defines[i] + "\n");
			
			sb.append("\n/**\n * The SQL command for creating the " + names[i] + " attribute table\n */\n");
			sb.append("static const char *cmd_create_" + names[i] + "_attr = \n");
			sb.append("\"CREATE TABLE " + tbl_names[i] + " (\" \\\n");
			sb.append("\"" + col_names[i] + " " + getIdType() + " NOT NULL PRIMARY KEY\" \\\n");
			for(Iterator it = maps[i].keySet().iterator(); it.hasNext();) {
				Entity ent = (Entity) it.next();
				Type ty = ent.getType();
				int id = getTypeId(ty_maps[i], ent.getOwner());
				String name = ent.getIdent() + "_" + id;
				
				sb.append("\", " + name + " " + getSQLType(ty) + " \" \\\n");
			}
			sb.append("\")\";\n\n");
		}
		
		sb.append("\n/** The boolean True Value */\n");
		addStringDefine(sb, "GR_BOOLEAN_TRUE", getTrueValue());
		
		sb.append("\n/** The boolean False Value */\n");
		addStringDefine(sb, "GR_BOOLEAN_FALSE", getFalseValue());
		
		sb.append("\n");
		
		// create get table
		for (int i = 0; i < maps.length; ++i) {
			String[] lines = new String[maps[i].size()];
			
			for(Iterator it = maps[i].keySet().iterator(); it.hasNext();) {
				Entity ent = (Entity) it.next();
				Type ty = ent.getType();
				int id = getTypeId(ty_maps[i], ent.getOwner());
				String name = ent.getIdent() + "_" + id;
				int index = ((Integer) maps[i].get(ent)).intValue();
				
				lines[index] = "\"" +
					"SELECT " + name + " FROM " + tbl_names[i] +
					" WHERE " + col_names[i] + " = " +
					firstIdMarker("%d", getIdType()) + "\"";
			}
			sb.append("/** The table of all get commaned for " + names[i] + " attributes. */\n");
			sb.append("static prepared_query_t cmd_get_" + names[i] + "_attr[] = {\n");
			for(int j = 0; j < lines.length; ++j) {
				sb.append("  { " + lines[j] + ", -1 },\n");
			}
			sb.append("  { NULL, -1 },\n");
			sb.append("};\n\n");
		}
		
		// create set table
		for (int i = 0; i < maps.length; ++i) {
			String[] lines = new String[maps[i].size()];
			
			for(Iterator it = maps[i].keySet().iterator(); it.hasNext();) {
				Entity ent = (Entity) it.next();
				Type ty = ent.getType();
				int id = getTypeId(ty_maps[i], ent.getOwner());
				String name = ent.getIdent() + "_" + id;
				int index = ((Integer) maps[i].get(ent)).intValue();
				
				lines[index] = "\"" +
					"UPDATE " + tbl_names[i] + " SET " + name + " = " + firstIdMarker("%s", getSQLType(ty)) +
					" WHERE " + col_names[i] + " = " + nextIdMarker("%d", getIdType()) + "\"";
			}
			sb.append("/** The table of all set commaned for " + names[i] + " attributes. */\n");
			sb.append("static prepared_query_t cmd_set_" + names[i] + "_attr[] = {\n");
			for(int j = 0; j < lines.length; ++j) {
				sb.append("  { " + lines[j] + ", -1 },\n");
			}
			sb.append("  { NULL, -1 },\n");
			sb.append("};\n\n");
		}
		
		writeFile("attr_tbl_cmd" + incExtension, sb);
	}

	/**
	 * Returns the first Id marker.
	 *
	 * @param fmt    A string containing the type for non-prepared execution
	 * @param p_fmt  A string containing the type for prepared execution
	 * @return
	 */
	String firstIdMarker(String fmt, String p_fmt) {
		return fmt;
	}
	
	/**
	 * Returns the next Id marker.
	 *
	 * @param fmt    A string containing the type for non-prepared execution
	 * @param p_fmt  A string containing the type for prepared execution
	 * @return
	 */
	String nextIdMarker(String fmt, String p_fmt) {
		return fmt;
	}
	
}

