/**
 * @author Sebastian Hack
 * @version $Id$
 */
package de.unika.ipd.grgen.be.C;
import de.unika.ipd.grgen.be.sql.meta.*;
import de.unika.ipd.grgen.ir.*;
import java.util.*;

import de.unika.ipd.grgen.Sys;
import de.unika.ipd.grgen.be.rewrite.RewriteGenerator;
import de.unika.ipd.grgen.be.rewrite.RewriteHandler;
import de.unika.ipd.grgen.be.rewrite.SPORewriteGenerator;
import de.unika.ipd.grgen.be.sql.NewExplicitJoinGenerator;
import de.unika.ipd.grgen.be.sql.PreferencesSQLParameters;
import de.unika.ipd.grgen.be.sql.SQLGenerator;
import de.unika.ipd.grgen.be.sql.SQLParameters;
import de.unika.ipd.grgen.be.sql.stmt.AttributeTable;
import de.unika.ipd.grgen.be.sql.stmt.DefaultMetaFactory;
import de.unika.ipd.grgen.be.sql.stmt.DefaultOp;
import de.unika.ipd.grgen.util.Util;
import java.io.ByteArrayOutputStream;
import java.io.File;
import java.io.PrintStream;

/**
 * A generator to generate SQL statements for a grgen specification.
 */
public abstract class SQLBackend extends CBackend	implements Dialect {
	
	private static final class MyDataType implements DataType {
		private final String text;
		
		MyDataType(String text) {
			this.text = text;
		}
		
		/**
		 * Get the SQL representation of the datatype.
		 * @return The SQL representation of the datatype.
		 */
		public String getText() {
			return text;
		}
		
		/**
		 * Print the meta construct.
		 * @param ps The print stream.
		 */
		public void dump(PrintStream ps) {
			ps.print(getText());
		}
		
		/**
		 * Get some special debug info for this object.
		 * This is mostly verbose stuff for dumping.
		 * @return Debug info.
		 */
		public String debugInfo() {
			return getText();
		}
	}
	
	protected static final String TYPE_ID = "id_type";
	protected static final String TYPE_INT = "int_type";
	protected static final String TYPE_BOOLEAN = "boolean_type";
	protected static final String TYPE_STRING = "string_type";
	protected static final String VALUE_TRUE = "true_value";
	protected static final String VALUE_FALSE = "false_value";
	protected static final String VALUE_NULL = "null_value";
	
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
	
	protected Sys system;
	
	protected final SQLGenerator sqlGen =
		new NewExplicitJoinGenerator(parameters, this, this);
	
	protected Map matchMap = new HashMap();
	
	protected MetaFactory factory;
	
	private DataType intType;
	
	private DataType stringType;
	
	private DataType booleanType;
	
	private DataType idType;
	
	private Op trueOp;
	
	private Op falseOp;
	
	protected abstract Properties getSQLProperties();
	
	/**
	 * Get the id datatype.
	 * @return The id datatype.
	 */
	public DataType getIdType() {
		if(idType == null)
			idType = new MyDataType((String) getSQLProperties().get(TYPE_ID));
		
		return idType;
	}
	
	/**
	 * Get the integer datatype.
	 * @return The integer datatype.
	 */
	public DataType getIntType() {
		if(intType == null)
			intType = new MyDataType((String) getSQLProperties().get(TYPE_ID));
		
		return intType;
	}
	
	/**
	 * Get the string datatype.
	 * @return The string datatype.
	 */
	public DataType getStringType() {
		if(stringType == null)
			stringType = new MyDataType((String) getSQLProperties().get(TYPE_STRING));
		
		return stringType;
	}
	
	/**
	 * Get the boolean datatype.
	 * @return The boolean datatype.
	 */
	public DataType getBooleanType() {
		if(booleanType == null)
			booleanType = new MyDataType((String) getSQLProperties().get(TYPE_BOOLEAN));
		
		return booleanType;
	}
	
	public Op constantOpcode(boolean value) {
		Op res;
		
		if(value == true) {
			if(trueOp == null)
				trueOp = DefaultOp.constant(Boolean.toString(value));
			
			res = trueOp;
		} else {
			if(falseOp == null)
				falseOp = DefaultOp.constant(Boolean.toString(value));
			
			res = falseOp;
		}
		
		return res;
	}
	
	/**
	 * Add a string define to a string buffer.
	 * The define must define a string, since the value parameter is
	 * formatted like a string.
	 * @param sb The string buffer.
	 * @param name The name of the define.
	 * @param value The define's value (must be a string constant).
	 */
	protected void addStringDefine(PrintStream ps, String name, String value) {
		addDefine(ps, name, formatString(value));
	}
	
	/**
	 * Add a define to a string buffer.
	 * @param sb The string buffer.
	 * @param name The name of the define.
	 * @param value The define's value.
	 */
	protected void addDefine(PrintStream ps, String name, String value) {
		ps.print("#define ");
		ps.print(formatId(name));
		ps.print(" ");
		ps.println(value);
	}
	
	/**
	 * Add C defines with all settings to a string buffer.
	 * @param sb The string buffer to add to.
	 */
	protected void addSettings(PrintStream ps) {
		addStringDefine(ps, "DBNAME", dbName);
		addStringDefine(ps, "DBNAME_PREFIX", dbNamePrefix);
		addStringDefine(ps, "STMT_PREFIX", stmtPrefix);
		addStringDefine(ps, "NODE_TYPE_IS_A_FUNC", nodeTypeIsAFunc);
		addStringDefine(ps, "EDGE_TYPE_IS_A_FUNC", edgeTypeIsAFunc);
		addStringDefine(ps, "TABLE_NEUTRAL", parameters.getTableNeutral());
		addStringDefine(ps, "TABLE_NODES", parameters.getTableNodes());
		addStringDefine(ps, "TABLE_EDGES", parameters.getTableEdges());
		addStringDefine(ps, "TABLE_NODE_ATTRS", parameters.getTableNodeAttrs());
		addStringDefine(ps, "TABLE_EDGE_ATTRS", parameters.getTableEdgeAttrs());
		addStringDefine(ps, "COL_NODES_ID", parameters.getColNodesId());
		addStringDefine(ps, "COL_NODES_TYPE_ID", parameters.getColNodesTypeId());
		addStringDefine(ps, "COL_EDGES_ID", parameters.getColEdgesId());
		addStringDefine(ps, "COL_EDGES_TYPE_ID", parameters.getColEdgesTypeId());
		addStringDefine(ps, "COL_EDGES_SRC_ID", parameters.getColEdgesSrcId());
		addStringDefine(ps, "COL_EDGES_TGT_ID", parameters.getColEdgesTgtId());
		addStringDefine(ps, "COL_NODE_ATTR_NODE_ID", parameters.getColNodeAttrNodeId());
		addStringDefine(ps, "COL_EDGE_ATTR_EDGE_ID", parameters.getColEdgeAttrEdgeId());
		
		// Dump the databases type corresponding to the ID type.
		addStringDefine(ps, "DB_ID_TYPE", getIdType().getText());
	}
	
	/**
	 * Output some data structures needed especially by the SQL backend.
	 * @param sb The string buffer to put it to.
	 */
	protected void makeActionTypes(PrintStream ps) {
		ps.println(
			"typedef struct {\n"
			  + "  MATCH_PROTOTYPE((*matcher));\n"
				+ "  FINISH_PROTOTYPE((*finisher));\n"
				+ "  const char **stmt;\n"
				+ "  const char *name;\n"
				+ "} action_impl_t;");
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
		private PrintStream ps;
		private Rule rule;
		private Map insertedNodesIndexMap = new HashMap();
		private Collection nodesToInsert = null;
		
		SQLRewriteHandler(Match match, PrintStream ps) {
			this.match = match;
			this.ps = ps;
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
				ps.print("  CHANGE_NODE_TYPE(GET_MATCH_NODE(" + nid + "), " + tid + ");");
				ps.print("\t/* change type of ");
				ps.print(n);
				ps.print(" to ");
				ps.print(n.getReplaceType().toString());
				ps.print(" (");
				ps.print(tid);
				ps.print(") */\n");
				
			}
		}
		
		/**
		 * @see de.unika.ipd.grgen.be.rewrite.RewriteHandler#deleteEdges(java.util.Collection)
		 */
		public void deleteEdges(Collection edges) {
			for (Iterator it = edges.iterator(); it.hasNext();) {
				Edge e = (Edge) it.next();
				ps.print("  DELETE_EDGE(GET_MATCH_EDGE(" + match.edgeIndexMap.get(e) + "));");
				ps.print("\t/* delete ");
				ps.print(e.toString());
				ps.print(" */\n");
			}
		}
		
		/**
		 * @see de.unika.ipd.grgen.be.rewrite.RewriteHandler#deleteEdgesOfNodes(java.util.Collection)
		 */
		public void deleteEdgesOfNodes(Collection nodes) {
			for (Iterator it = nodes.iterator(); it.hasNext();) {
				Node n = (Node) it.next();
				Integer nid = (Integer) match.nodeIndexMap.get(n);
				ps.print("  DELETE_NODE_EDGES(GET_MATCH_NODE(" + nid + "));");
				ps.print("\t/* delete edges of ");
				ps.print(n.toString());
				ps.print(" */\n");
			}
		}
		
		/**
		 * @see de.unika.ipd.grgen.be.rewrite.RewriteHandler#deleteNodes(java.util.Collection)
		 */
		public void deleteNodes(Collection nodes) {
			for (Iterator it = nodes.iterator(); it.hasNext();) {
				Node n = (Node) it.next();
				Integer nid = (Integer) match.nodeIndexMap.get(n);
				ps.print("  DELETE_NODE(GET_MATCH_NODE(" + nid + "));");
				ps.print("\t/* delete ");
				ps.print(n);
				ps.print(" */\n");
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
				
				ps.print(
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
			ps.print("  gr_id_t inserted_nodes[" + nodes.size() + "];\n");
			
			/*
			 * Generate node creation statements and save the newly created
			 * IDs in the array.
			 */
			int i = 0;
			for (Iterator it = nodes.iterator(); it.hasNext(); i++) {
				Node n = (Node) it.next();
				ps.print("  inserted_nodes[" + i + "] = INSERT_NODE("
									 + getId(n.getNodeType()) + ");\n");
				insertedNodesIndexMap.put(n, new Integer(i));
			}
			
		}
		
		/**
		 * Generate an eval statement for some assignments.
		 * @param assigns A collection of assignments.
		 */
		public void generateEvals(Collection assigns) {
			
			for(Iterator it = assigns.iterator(); it.hasNext();) {
				Assignment a = (Assignment) it.next();
				
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
	protected void genFinish(PrintStream ps, MatchingAction a, int id) {
		String actionIdent = formatId(a.getIdent().toString());
		String finishIdent = "finish_" + actionIdent;
		
		Match m = (Match) matchMap.get(a);
		m.finishIdent = finishIdent;
		
		assert m != null : "A match must have been produced for " + a;
		
		ps.print("static FINISH_PROTOTYPE(" + finishIdent + ")\n{\n");
		
		if(a instanceof Rule) {
			Rule r = (Rule) a;
			rewriteGenerator.rewrite(r, new SQLRewriteHandler(m, ps));
		}
		
		// genRuleFinish(sb, (Rule) a, id, m);
		
		ps.print("  return 1;\n}\n\n");
	}
	
	protected static final class PreparedQueryData {
		final String name;
		final Statement stmt;
		final Collection types;
		final String comment;
		
		PreparedQueryData(String name, Statement stmt,
											Collection types, String comment) {
			this.name = name;
			this.stmt = stmt;
			this.types = types;
			this.comment = comment;
		}
		
		PreparedQueryData(String name, Statement stmt, Collection types) {
			this(name, stmt, types, null);
		}
		
		void printCall(PrintStream ps, String[] args) {
		}
		
		void printTypeArray(PrintStream ps) {
		}
		
		void printTableEntry(PrintStream ps) {
			ps.print("  { ");
			formatString(ps, Util.toString(stmt));
			ps.print(", ");
			ps.print(types.size());
			ps.println(" },");
		}
	}
		
	
	protected void makeEvals(PrintStream ps) {
		ByteArrayOutputStream bos = new ByteArrayOutputStream(8192);

		for(Iterator jt = unit.getActions(); jt.hasNext();) {
			Object obj = jt.next();
			
			if(obj instanceof Rule) {
				Rule r = (Rule) obj;
				int num = 0;
				
				PrintStream tps = new PrintStream(bos);
				
				for(Iterator i = r.getEvals().iterator(); i.hasNext(); num++) {
					Assignment assign = (Assignment) i.next();
					MarkerSource ms = getMarkerSource();
					Statement s = sqlGen.genEvalUpdateStmt(assign, factory, ms);
					String name = mangle(r) + '_' + num;
					
					bos.reset();
					s.dump(tps);
					tps.flush();
					
					ps.println("/* Argument type vector for eval statement " + num
										 + " in rule " + r.getIdent() + " */");
					
					ps.print("/* Eval statement " + num + " in rule ");
					ps.print(r.getIdent());
					ps.println(" */");
					ps.print("static const char *eval_stmt_");
					ps.print(mangle(r));
					ps.print('_');
					ps.print(num);
					ps.print(" = ");
					formatString(ps, bos.toString());
					ps.println(';');
					ps.println();
				}
			}
		}
	}
	
	/**
	 * @see de.unika.ipd.grgen.be.C.CBackend#genMatch(java.lang.StringBuffer, de.unika.ipd.grgen.ir.MatchingAction, int)
	 */
	protected void genMatch(PrintStream ps, MatchingAction a, int id) {
		// System.out.println(a.getIdent().toString());
		
		String actionIdent = mangle(a);
		String stmtIdent = "stmt_" + actionIdent;
		String matchIdent = "match_" + actionIdent;
		String nodeNamesIdent = "node_names_" + actionIdent;
		String edgeNamesIdent = "edge_names_" + actionIdent;
		Iterator it;
		
		SQLGenerator.MatchCtx matchCtx =
			sqlGen.makeMatchContext(system, a, factory);
		
		List nodes = matchCtx.matchedNodes;
		List edges = matchCtx.matchedEdges;
		
		// Dump the SQL statement
		ps.print("static const char *stmt_");
		ps.print(actionIdent);
		ps.println(" = ");
		ps.print(formatString(sqlGen.genMatchStatement(matchCtx)));
		ps.println(";\n");
		
		// Make an array of strings that contains the node names.
		ps.println("static const char *" + nodeNamesIdent + "[] = {");
		for (it = nodes.iterator(); it.hasNext();) {
			Identifiable node = (Identifiable) it.next();
			ps.print("  ");
			ps.print(formatString(node.getIdent().toString()));
			ps.println(",");
		}
		ps.println("};\n");
		
		// Make an array of strings that contains the edge names.
		ps.println("static const char *" + edgeNamesIdent + "[] = {");
		for (it = edges.iterator(); it.hasNext();) {
			Identifiable edge = (Identifiable) it.next();
			ps.print("  ");
			ps.print(formatString(edge.getIdent().toString()));
			ps.println(",");
		}
		ps.println("};\n\n");
		
		// Make the function that invokes the SQL statement.
		ps.println("static MATCH_PROTOTYPE(" + matchIdent + ")\n{");
		ps.println("  QUERY(" + id + ", " + stmtIdent + ");");
		
		ps.print("  MATCH_GET_RES(");
		ps.print(nodes.size());
		ps.print(", ");
		ps.print(edges.size());
		ps.print(", ");
		ps.print(nodeNamesIdent);
		ps.print(", ");
		ps.print(edgeNamesIdent);
	  ps.println(");");
		ps.println("}\n");
		
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
		
		// Emit an include file for Makefiles
		PrintStream ps = openFile("unit.mak");
		ps.println("#\n# generated by grgen, don't edit\n#");
		ps.println("UNIT_NAME = " + formatId(unit.getIdent().toString()));
		ps.println("DB_NAME = " + dbName);
		closeFile(ps);
		
		// Make some additional types needed for the action implementation.
		ps = openFile("action_types" + incExtension);
		makeActionTypes(ps);
		closeFile(ps);
		
		// Make action information
		ps = openFile("action_impl_map" + incExtension);
		ps.println("static const action_impl_t action_impl_map[] = {");
		
		Object[] matches = matchMap.values().toArray();
		Arrays.sort(matches, Match.comparator);
		for (int i = 0; i < matches.length; i++) {
			Match m = (Match) matches[i];
			ps.print("  { ");
			ps.print(m.matchIdent);
			ps.print(", ");
			ps.print(m.finishIdent);
			ps.print(", &");
			ps.print(m.stmtIdent);
			ps.print(", ");
			ps.print(formatString(m.stmtIdent));
			ps.println("},");
		}
		ps.println("};");
		closeFile(ps);
		
		// Emit the settings specified in the grgen config file.
		// these contain table and column names, etc.
		ps = openFile("settings" + incExtension);
		addSettings(ps);
		closeFile(ps);
		
		// creation of ATTR tables
		genAttrTableCmd();
	}
	
	/**
	 * Do some additional stuff on initialization.
	 */
	public void init(Unit unit, Sys system, File outputPath) {
		super.init(unit, system, outputPath);
		this.system = system;
		this.dbName = dbNamePrefix + unit.getIdent().toString();
		this.factory = new DefaultMetaFactory(this, parameters, nodeAttrMap, edgeAttrMap);
	}
	
	protected void genAttrTableGetAndSet(PrintStream ps, String name,
																			 AttributeTable table) {
		
		ps.print("#define GR_HAVE_");
		ps.print(name.toUpperCase());
		ps.print("_ATTR 1\n\n");
		
		ps.print("static const char *cmd_create_" + name + "_attr = \n\"");
		table.dumpDecl(ps);
		ps.print("\";\n\n");
		
		ps.print("static prepared_query_t cmd_get_");
		ps.print(name);
		ps.print("_attr[] = {\n");
		
		for(int i = 1; i < table.columnCount(); i++) {
			ps.print("  { \"");
			table.genGetStmt(ps, table.getColumn(i));
			ps.print("\", -1 },\n");
		}
		ps.print("  { NULL, -1 }\n");
		ps.print("};\n\n");
		
		ps.print("static prepared_query_t cmd_set_");
		ps.print(name);
		ps.print("_attr[] = {\n");
		
		for(int i = 1; i < table.columnCount(); i++) {
			ps.print("  { \"");
			table.genUpdateStmt(ps, table.getColumn(i));
			ps.print("\", -1 },\n");
		}
		ps.print("  { NULL, -1 }\n");
		ps.print("};\n\n");
	}
	
	protected void genAttrTableCmd() {
		PrintStream ps = 	openFile("attr_tbl_cmd" + incExtension);
		
		ps.print("\n/** The boolean True Value */\n");
		addStringDefine(ps, "GR_BOOLEAN_TRUE", constantOpcode(true).text());
		
		ps.print("\n/** The boolean False Value */\n");
		addStringDefine(ps, "GR_BOOLEAN_FALSE", constantOpcode(false).text());
		
		ps.print("\n");
		
		genAttrTableGetAndSet(ps, "node", factory.originalNodeAttrTable());
		genAttrTableGetAndSet(ps, "edge", factory.originalEdgeAttrTable());
		closeFile(ps);
		
	}
}






