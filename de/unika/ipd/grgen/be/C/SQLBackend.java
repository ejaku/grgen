/**
 * @author Sebastian Hack
 * @version $Id$
 */
package de.unika.ipd.grgen.be.C;

import de.unika.ipd.grgen.be.sql.stmt.*;
import de.unika.ipd.grgen.ir.*;
import java.util.*;

import de.unika.ipd.grgen.be.rewrite.RewriteGenerator;
import de.unika.ipd.grgen.be.rewrite.RewriteHandler;
import de.unika.ipd.grgen.be.rewrite.SPORewriteGenerator;
import de.unika.ipd.grgen.be.sql.NewExplicitJoinGenerator;
import de.unika.ipd.grgen.be.sql.PreferencesSQLParameters;
import de.unika.ipd.grgen.be.sql.SQLGenerator;
import de.unika.ipd.grgen.be.sql.SQLParameters;
import de.unika.ipd.grgen.be.sql.meta.TypeFactory;
import de.unika.ipd.grgen.util.report.ErrorReporter;

/**
 * A generator to generate SQL statements for a grgen specification.
 */
public abstract class SQLBackend extends CBackend {
	
	/** if 0, the query should not be limited. */
	protected int limitQueryResults;
	
	/** Name of the database. */
	protected String dbName;
	
	protected final String dbNamePrefix = "gr_";
	
	protected final String stmtPrefix = "stmt_";
	
	protected final String nodeTypeIsAFunc = "node_type_is_a";
	
	protected final String edgeTypeIsAFunc = "edge_type_is_a";
	
	private final RewriteGenerator rewriteGenerator = new SPORewriteGenerator();
	
	protected final SQLParameters parameters = new PreferencesSQLParameters();
	
	protected final TypeFactory typeFactory = new DefaultTypeFactory();
	
	protected TypeStatementFactory stmtFactory = new DefaultStatementFactory(typeFactory);
	
	protected final SQLGenerator sqlGen = enableNT
		? new NewExplicitJoinGenerator(parameters, this)
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
		addStringDefine(sb, "TABLE_NEUTRAL", parameters.getTableNeutral());
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
				sb.append("  CHANGE_NODE_TYPE(GET_MATCH_NODE(" + nid + "), " + tid + ");");
				sb.append("\t/* change type of ").append(n).append(" to ");
				sb.append(n.getReplaceType().toString()).append(" (").append(tid);
				sb.append(") */\n");
				
			}
		}
		
		/**
		 * @see de.unika.ipd.grgen.be.rewrite.RewriteHandler#deleteEdges(java.util.Collection)
		 */
		public void deleteEdges(Collection edges) {
			for (Iterator it = edges.iterator(); it.hasNext();) {
				Edge e = (Edge) it.next();
				sb.append("  DELETE_EDGE(GET_MATCH_EDGE(" + match.edgeIndexMap.get(e) + "));");
				sb.append("\t/* delete ").append(e.toString()).append(" */\n");
			}
		}
		
		/**
		 * @see de.unika.ipd.grgen.be.rewrite.RewriteHandler#deleteEdgesOfNodes(java.util.Collection)
		 */
		public void deleteEdgesOfNodes(Collection nodes) {
			for (Iterator it = nodes.iterator(); it.hasNext();) {
				Node n = (Node) it.next();
				Integer nid = (Integer) match.nodeIndexMap.get(n);
				sb.append("  DELETE_NODE_EDGES(GET_MATCH_NODE(" + nid + "));");
				sb.append("\t/* delete edges of ").append(n.toString()).append(" */\n");
			}
		}
		
		/**
		 * @see de.unika.ipd.grgen.be.rewrite.RewriteHandler#deleteNodes(java.util.Collection)
		 */
		public void deleteNodes(Collection nodes) {
			for (Iterator it = nodes.iterator(); it.hasNext();) {
				Node n = (Node) it.next();
				Integer nid = (Integer) match.nodeIndexMap.get(n);
				sb.append("  DELETE_NODE(GET_MATCH_NODE(" + nid + "));");
				sb.append("\t/* delete ").append(n).append(" */\n");
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
		// System.out.println(a.getIdent().toString());
		
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
		sb.append(formatString(sqlGen.genMatchStatement(a, nodes, edges, tableFactory, stmtFactory)) + ";\n\n");
		
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
		
		// create conn assert statements
		genValidateStatements();
	}
	
	/**
	 * Do some additional stuff on initialization.
	 */
	public void init(Unit unit, ErrorReporter reporter, String outputPath) {
		super.init(unit, reporter, outputPath);
		this.dbName = dbNamePrefix + unit.getIdent().toString();
		makeTypes();
		
		tableFactory = new DefaultGraphTableFactory(parameters, typeFactory,
													nodeAttrMap, edgeAttrMap);
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
	
	protected void genAttrTableGetAndSet(StringBuffer sb, String name,
										 AttributeTable table) {
		
		sb.append("#define GR_HAVE_").append(name.toUpperCase()).append("_ATTR 1\n\n");
		
		sb.append("static const char *cmd_create_" + name + "_attr = \n\"");
		table.dumpDecl(sb);
		sb.append("\";\n\n");
		
		sb.append("static prepared_query_t cmd_get_").append(name).append("_attr[] = {\n");
		
		for(int i = 1; i < table.columnCount(); i++) {
			sb.append("  { \"");
			table.genGetStmt(sb, table.getColumn(i));
			sb.append("\", -1 },\n");
		}
		sb.append("  { NULL, -1 }\n");
		sb.append("};\n\n");
		
		sb.append("static prepared_query_t cmd_set_").append(name).append("_attr[] = {\n");
		for(int i = 1; i < table.columnCount(); i++) {
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
	
	protected void genValidateStatements() {
		StringBuffer sb = new StringBuffer();
		List srcTypes = new ArrayList();
		List srcRange = new ArrayList();
		List tgtTypes = new ArrayList();
		List tgtRange = new ArrayList();
		List edgeTypes = new ArrayList();
		
		sb.append("\n/** The Validate Statements */\n");
		
		for(Iterator i = edgeTypeMap.keySet().iterator(); i.hasNext();) {
			EdgeType et = (EdgeType)i.next();
			for(Iterator j = et.getConnAsserts(); j.hasNext();) {
				ConnAssert ca = (ConnAssert)j .next();
				
				srcTypes.add(ca.getSrcType());
				srcRange.add(new int[] {ca.getSrcLower(), ca.getSrcUpper()});
				tgtTypes.add(ca.getTgtType());
				tgtRange.add(new int[] {ca.getTgtLower(), ca.getTgtUpper()});
				edgeTypes.add(et);
			}
		}
		
		List queries =
			sqlGen.genValidateStatements(srcTypes, srcRange,  tgtTypes, tgtRange,
										 edgeTypes, stmtFactory, tableFactory);
		
		sb.append("\ntypedef struct _valid_stmts {\n"+
					  "  const char* src_query;\n"+
					  "  const char* tgt_query;\n"+
					  "  const char* edge_type_name;\n"+
					  "  const char* src_type_name;\n"+
					  "  const char* tgt_type_name;\n"+
					  "  const int src_lower;\n"+
					  "  const int src_upper;\n"+
					  "  const int tgt_lower;\n"+
					  "  const int tgt_upper;\n"+
					  "} valid_stmts_t;\n\n"
				 );
		sb.append("#define VALID_STMT_DATA_LENGTH "+srcTypes.size()+"\n");
		sb.append("static valid_stmts_t valid_stmt_data["+srcTypes.size()+"] = {\n");
		
		for(int i = 0; i < srcTypes.size(); i++) {
			NodeType srcType = (NodeType)srcTypes.get(i);
			NodeType tgtType = (NodeType)tgtTypes.get(i);
			int srcRangeLower = ((int[])srcRange.get(i))[0];
			int srcRangeUpper = ((int[])srcRange.get(i))[1];
			int tgtRangeLower = ((int[])tgtRange.get(i))[0];
			int tgtRangeUpper = ((int[])tgtRange.get(i))[1];
			EdgeType edgeType = (EdgeType)edgeTypes.get(i);
			StringBuffer srcQuery = (StringBuffer)queries.get(2*i);
			StringBuffer tgtQuery = (StringBuffer)queries.get(2*i+1);
			
			sb.append("\n{\n");
			sb.append("  "+formatString(srcQuery.toString())+",\n");
			sb.append("  "+formatString(tgtQuery.toString())+",\n");
			sb.append("  \""+edgeType+"\",\n");
			sb.append("  \""+srcType+"\",\n");
			sb.append("  \""+tgtType+"\",\n");
			sb.append("  "+srcRangeLower+",\n");
			sb.append("  "+srcRangeUpper+",\n");
			sb.append("  "+tgtRangeLower+",\n");
			sb.append("  "+tgtRangeUpper+",\n");
			
			sb.append("},\n");
		}
		
		sb.append("\n};\n\n");
		
		writeFile("valid_stmt" + incExtension, sb);
	}
}




