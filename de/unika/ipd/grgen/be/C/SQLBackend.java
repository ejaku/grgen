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
import java.util.prefs.Preferences;

import de.unika.ipd.grgen.ir.Edge;
import de.unika.ipd.grgen.ir.Graph;
import de.unika.ipd.grgen.ir.Identifiable;
import de.unika.ipd.grgen.ir.MatchingAction;
import de.unika.ipd.grgen.ir.Node;
import de.unika.ipd.grgen.ir.Rule;
import de.unika.ipd.grgen.ir.Unit;
import de.unika.ipd.grgen.ir.Entity;
import de.unika.ipd.grgen.ir.Type;
import de.unika.ipd.grgen.util.report.ErrorReporter;

/**
 * A generator to generate SQL statements for a grgen specification.
 */
public abstract class SQLBackend extends CBackend {

	/** if 0, the query should not be limited. */
  protected int limitQueryResults;

  /** Name of the database */
  protected String dbName;

  protected String dbNamePrefix;

  protected String stmtPrefix;

  protected String tableNodes;

  protected String tableEdges;

  protected String tableNodeAttrs;

  protected String tableEdgeAttrs;

  protected String colNodesId;

  protected String colNodesTypeId;

  protected String colEdgesId;

  protected String colEdgesTypeId;

  protected String colEdgesSrcId;

  protected String colEdgesTgtId;

  protected String colNodeAttrNodeId;

  protected String colEdgeAttrEdgeId;

  protected String edgeTypeIsAFunc;

  protected String nodeTypeIsAFunc;

  protected Map matchMap = new HashMap();


  /**
   * Make a new SQL Generator.
   */
  public SQLBackend() {
    Preferences prefs = Preferences.userNodeForPackage(getClass());

    dbNamePrefix = prefs.get("dbNamePrefix", "gr_");
    stmtPrefix = prefs.get("statementPrefix", "stmt_");
    nodeTypeIsAFunc = prefs.get("nodeTypeIsAFunc", "node_type_is_a");
    edgeTypeIsAFunc = prefs.get("edgeTypeIsAFunc", "edge_type_is_a");
    tableNodes = prefs.get("tableNodes", "nodes");
    tableEdges = prefs.get("tableEdges", "edges");
    tableNodeAttrs = prefs.get("tableNodeAttrs", "node_attrs");
    tableEdgeAttrs = prefs.get("tableEdgeAttrs", "edge_attrs");
    colNodesId = prefs.get("colNodesId", "node_id");
    colNodesTypeId = prefs.get("colNodesTypeId", "type_id");
    colEdgesId = prefs.get("colEdgesId", "edge_id");
    colEdgesTypeId = prefs.get("colEdgesTypeId", "type_id");
    colEdgesSrcId = prefs.get("colEdgesSrcId", "src_id");
    colEdgesTgtId = prefs.get("colEdgesTgtId", "tgt_id");
    colNodeAttrNodeId = prefs.get("colNodeAttrNodeId", "node_id");
    colEdgeAttrEdgeId = prefs.get("colEdgeAttrEdgeId", "edge_id");
    
    limitQueryResults = prefs.getInt("limitQueryResults", 0);
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
    addStringDefine(sb, "TABLE_NODES", tableNodes);
    addStringDefine(sb, "TABLE_EDGES", tableEdges);
    addStringDefine(sb, "TABLE_NODE_ATTRS", tableNodeAttrs);
    addStringDefine(sb, "TABLE_EDGE_ATTRS", tableEdgeAttrs);
    addStringDefine(sb, "COL_NODES_ID", colNodesId);
    addStringDefine(sb, "COL_NODES_TYPE_ID", colNodesTypeId);
    addStringDefine(sb, "COL_EDGES_ID", colEdgesId);
    addStringDefine(sb, "COL_EDGES_TYPE_ID", colEdgesTypeId);
    addStringDefine(sb, "COL_EDGES_SRC_ID", colEdgesSrcId);
    addStringDefine(sb, "COL_EDGES_TGT_ID", colEdgesTgtId);
    addStringDefine(sb, "COL_NODE_ATTR_NODE_ID", colNodeAttrNodeId);
    addStringDefine(sb, "COL_EDGE_ATTR_EDGE_ID", colEdgeAttrEdgeId);
    
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

  /**
   * Make an SQL table identifier out of an edge.
   * @param e The edge to mangle.
   * @return An identifier usable in SQL statements and unique for
   * each edge.
   */
  private String mangleEdge(Edge e) {
    return "e" + e.getId();
  }

  private String mangleNode(Node n) {
    return "n" + n.getId();
  }

  private String getEdgeCol(Edge e, String col) {
    return mangleEdge(e) + "." + col;
  }

  private String getNodeCol(Node n, String col) {
    return mangleNode(n) + "." + col;
  }
  /**
   * Add something to a string buffer.
   * If the string buffer is empty, <code>start</code> is appended. If
   * it is not empty, <code>sep</code> is appended. Afterwards, 
   * <code>add</code> is appended.
   * @param sb The string buffer to add to.
   * @param start The start string.
   * @param sep The seperator string.
   * @param add The actual string to add.
   */
  private void addTo(StringBuffer sb, String start, String sep, String add) {
    if (sb.length() == 0)
      sb.append(start);
    else
      sb.append(sep);

    sb.append(add);
  }

  private void addToCond(StringBuffer sb, String add) {
    addTo(sb, "", " AND ", add);
  }

  private void addToList(StringBuffer sb, String table) {
    addTo(sb, "", ", ", table);
  }

  private String join(String a, String b, String link) {
    if (a.length() == 0)
      return b;
    else if (b.length() == 0)
      return a;
    else
      return a + link + b;
  }

  private String join(StringBuffer a, StringBuffer b, String link) {
    return join(a.toString(), b.toString(), link);
  }

  private void makeJoin(StringBuffer sb, Graph gr, Edge e1, Edge e2) {
    Node[] nodes =
      { gr.getSource(e1), gr.getTarget(e1), gr.getSource(e2), gr.getTarget(e2)};

    String[] names =
      {
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
    Set negatedEdges = gr.getNegatedEdges();
    
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
          if (e.isNegated())
            continue;

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

  /**
   * Make the finish code of a rule 
   * @param sb The string buffer to put the code to.
   * @param r The rule to make the finish code for.
   * @param id The id number of the rule.
   * @param m The match structure as supplied by 
   * {@link #genMatch(StringBuffer, MatchingAction, int)}.
   */
  protected void genRuleFinish(StringBuffer sb, Rule r, int id, Match m) {
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
            + getTypeId(nodeTypeMap, n.getNodeType()) + ");\n");
        insertedNodesIndexMap.put(n, new Integer(i));
      }

      /*
       * Now we can launch the redirections.
       */
      for (Iterator it = r.getRedirections().iterator(); it.hasNext();) {
        Rule.Redirection redir = (Rule.Redirection) it.next();
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
            + getTypeId(edgeTypeMap, redir.edgeType) + ", "
            + getTypeId(nodeTypeMap, redir.nodeType) + ");\n");
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
        int tid = getTypeId(nodeTypeMap, n.getReplaceType());
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
        continue;

      int etid = getTypeId(edgeTypeMap, e.getEdgeType());
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

    if (a instanceof Rule)
      genRuleFinish(sb, (Rule) a, id, m);

    sb.append("  return 1;\n}\n\n");
  }

  /**
   * @see de.unika.ipd.grgen.be.C.CBackend#genMatch(java.lang.StringBuffer, de.unika.ipd.grgen.ir.MatchingAction, int)
   */
  protected void genMatch(StringBuffer sb, MatchingAction a, int id) {
    String actionIdent = mangle(a);
    String stmtIdent = "stmt_" + actionIdent;
    String matchIdent = "match_" + actionIdent;
    String nodeNamesIdent = "node_names_" + actionIdent;
    String edgeNamesIdent = "edge_names_" + actionIdent;
    List nodes = new LinkedList();
    List edges = new LinkedList();
    Iterator it;
    int i;

    // Dump the SQL statement	
    sb.append("static const char *stmt_" + actionIdent + " = \n");
    sb.append(formatString(genMatchStatement(a, nodes, edges)) + ";\n\n");

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
  }

	/**
	 * returns the SQL type to a given IR type
	 * @param ty The IR type
	 * @return The SQL type
	 */
	private String getSQLType(Type ty)
	{
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

	/**
	 * Creates the commands for creating attribute tables
	 */
	protected void genAttrTableCmd() {
		StringBuffer sb;
		Map maps[]         = new Map[]    { nodeAttrMap,          edgeAttrMap };
		Map ty_maps[]      = new Map[]    { nodeTypeMap,          edgeTypeMap };
		String tbl_names[] = new String[] { tableNodeAttrs,       tableEdgeAttrs };
		String col_names[] = new String[] { colNodesId,           colEdgesId };
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
								
				lines[index] = "  \"SELECT " + name + " FROM " + tbl_names[i] + " WHERE " + col_names[i] + " = %d\",\n";
			}
			sb.append("/** The table of all get commaned for " + names[i] + " attributes. */\n");
			sb.append("static const char * cmd_get_" + names[i] + "_attr[] = {\n");
			for(int j = 0; j < lines.length; ++j) {
				sb.append(lines[j]);
			}
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

				lines[index] = "  \"UPDATE " + tbl_names[i] + " SET " + name + " = %s WHERE " + col_names[i] + " = %d\",\n";
			}
			sb.append("/** The table of all set commaned for " + names[i] + " attributes. */\n");
			sb.append("static const char * cmd_set_" + names[i] + "_attr[] = {\n");
			for(int j = 0; j < lines.length; ++j) {
				sb.append(lines[j]);
			}
			sb.append("};\n\n");
		}
					 		
		writeFile("attr_tbl_cmd" + incExtension, sb);
	}
	
}

