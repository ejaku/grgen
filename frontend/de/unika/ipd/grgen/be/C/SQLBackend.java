/*
 GrGen: graph rewrite generator tool.
 Copyright (C) 2005  IPD Goos, Universit"at Karlsruhe, Germany

 This library is free software; you can redistribute it and/or
 modify it under the terms of the GNU Lesser General Public
 License as published by the Free Software Foundation; either
 version 2.1 of the License, or (at your option) any later version.

 This library is distributed in the hope that it will be useful,
 but WITHOUT ANY WARRANTY; without even the implied warranty of
 MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the GNU
 Lesser General Public License for more details.

 You should have received a copy of the GNU Lesser General Public
 License along with this library; if not, write to the Free Software
 Foundation, Inc., 51 Franklin St, Fifth Floor, Boston, MA  02110-1301  USA
 */


/**
 * @author Sebastian Hack
 * @version $Id$
 */
package de.unika.ipd.grgen.be.C;

import java.io.ByteArrayOutputStream;
import java.io.File;
import java.io.PrintStream;
import java.util.Arrays;
import java.util.Collection;
import java.util.Collections;
import java.util.Comparator;
import java.util.HashMap;
import java.util.Iterator;
import java.util.LinkedList;
import java.util.List;
import java.util.Map;

import de.unika.ipd.grgen.Sys;
import de.unika.ipd.grgen.be.rewrite.RewriteGenerator;
import de.unika.ipd.grgen.be.rewrite.RewriteHandler;
import de.unika.ipd.grgen.be.rewrite.SPORewriteGenerator;
import de.unika.ipd.grgen.be.sql.NewExplicitJoinGenerator;
import de.unika.ipd.grgen.be.sql.PreferencesSQLParameters;
import de.unika.ipd.grgen.be.sql.SQLGenerator;
import de.unika.ipd.grgen.be.sql.SQLParameters;
import de.unika.ipd.grgen.be.sql.meta.Column;
import de.unika.ipd.grgen.be.sql.meta.DataType;
import de.unika.ipd.grgen.be.sql.meta.Dialect;
import de.unika.ipd.grgen.be.sql.meta.MarkerSource;
import de.unika.ipd.grgen.be.sql.meta.MetaFactory;
import de.unika.ipd.grgen.be.sql.meta.Query;
import de.unika.ipd.grgen.be.sql.meta.Statement;
import de.unika.ipd.grgen.be.sql.meta.Term;
import de.unika.ipd.grgen.be.sql.stmt.AttributeTable;
import de.unika.ipd.grgen.be.sql.stmt.DefaultMetaFactory;
import de.unika.ipd.grgen.ir.Assignment;
import de.unika.ipd.grgen.ir.Edge;
import de.unika.ipd.grgen.ir.Entity;
import de.unika.ipd.grgen.ir.Graph;
import de.unika.ipd.grgen.ir.IR;
import de.unika.ipd.grgen.ir.Identifiable;
import de.unika.ipd.grgen.ir.MatchingAction;
import de.unika.ipd.grgen.ir.Node;
import de.unika.ipd.grgen.ir.RetypedNode;
import de.unika.ipd.grgen.ir.Rule;
import de.unika.ipd.grgen.ir.Unit;
import de.unika.ipd.grgen.util.Util;

/**
 * A generator to generate SQL statements for a grgen specification.
 */
public abstract class SQLBackend extends CBackend implements Dialect {
	
	private final class PreparedStatements {
		
		protected final class Stmt {
			final int id;
			final String name;
			final Statement stmt;
			final Collection<DataType> types;
			final Collection<Entity> usedEntities;
			final String comment;
			final String idName;
			
			
			Stmt(String name,
				 Statement stmt,
				 Collection<DataType> types,
				 Collection<Entity> usedEntities,
				 String comment) {
				
				this.id = counter;
				this.name = name;
				this.stmt = stmt;
				this.types = types;
				this.usedEntities = usedEntities;
				this.comment = comment;
				this.idName = "PQ" + "_" + name.toUpperCase();
			}
			
			void printTypeArray(PrintStream ps) {
				if(!types.isEmpty()) {
					ps.println("static const gr_value_kind_t type_arr_"
								   + prefix + "_" + name + "[] = {");
					for(Iterator<DataType> it = types.iterator(); it.hasNext();) {
						DataType type = it.next();
						ps.print("  ");
						ps.print(getTypeMacroName(type));
						ps.println(", ");
					}
					ps.println("};\n");
				}
			}
			
			void printTableEntry(PrintStream ps) {
				ps.print("  { ");
				formatString(ps, name);
				ps.print(", ");
				formatString(ps, Util.toString(stmt));
				ps.print(", ");
				ps.print(types.size());
				ps.print(", ");
				ps.print(types.isEmpty() ? "NULL" : "type_arr_" + prefix + "_" + name);
				ps.print(", -1 },");
				
				if(comment != null && comment.length() > 0) {
					ps.print(" /* ");
					ps.print(comment);
					ps.println(" */");
				} else
					ps.println();
			}
		}
		
		private final Collection<SQLBackend.PreparedStatements.Stmt> stmts = new LinkedList<SQLBackend.PreparedStatements.Stmt>();
		private final String prefix;
		private int counter = 0;
		
		public PreparedStatements(String prefix) {
			this.prefix = prefix;
		}
		
		public Stmt add(String name,
						Statement stmt,
						Collection<DataType> types,
						Collection<Entity> usedEntities,
						String comment) {
			Stmt q = new Stmt(name, stmt, types, usedEntities, comment);
			counter++;
			stmts.add(q);
			return q;
		}
		
		public Stmt add(String name,
						Statement stmt,
						Collection<DataType> types,
						Collection<Entity> usedEntities) {
			return add(name, stmt, types, usedEntities, "");
		}
		
		public Stmt add(String name,
						Statement stmt,
						Collection<DataType> types) {
			Collection<Entity> empty = Collections.emptySet();
			return add(name, stmt, types, empty, "");
		}
		
		public final void emit(PrintStream ps) {
			int i = 0;
			String upPrefix = prefix.toUpperCase();
			
			for(Iterator<SQLBackend.PreparedStatements.Stmt> it = stmts.iterator(); it.hasNext(); i++) {
				Stmt q = it.next();
				ps.println("#define " + q.idName + " " + i);
			}
			
			ps.println();
			for(Iterator<SQLBackend.PreparedStatements.Stmt> it = stmts.iterator(); it.hasNext();) {
				Stmt q = it.next();
				q.printTypeArray(ps);
			}
			
			ps.println();
			ps.println("static prepared_query_t prep_queries_" + prefix + "[] = {");
			for(Iterator<SQLBackend.PreparedStatements.Stmt> it = stmts.iterator(); it.hasNext();) {
				Stmt q = it.next();
				q.printTableEntry(ps);
			}
			ps.println("{ 0 }");
			ps.println("};\n");
			
			ps.println("#define NUM_PREP_QUERIES_" + upPrefix + " " + Integer.toString(i));
			ps.println("#define PREP_QUERIES_" + upPrefix + "_PREFIX \"" + prefix + "\"");
		}
		
		public final void writeToFile() {
			PrintStream ps = openFile("prep_queries_" + prefix + incExtension);
			emit(ps);
			closeFile(ps);
		}
	}
	
	
	protected static final class MyDataType implements DataType {
		private final String text;
		private final int typeId;
		private final Term init;
		
		MyDataType(String text, int typeId, Term init) {
			this.text = text;
			this.typeId = typeId;
			this.init = init;
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
		
		/**
		 * Return the type id for this type.
		 * @return A type id that represents this type.
		 */
		public int classify() {
			return typeId;
		}
		
		/**
		 * Give an expression that is a default initializer
		 * for this type.
		 * @return An expression that represents the default initializer
		 * for an item of this type.
		 */
		public Term initValue() {
			return init;
		}
	}
	
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
	
	protected final Map<MatchingAction, SQLBackend.Match> matchMap = new HashMap<MatchingAction, SQLBackend.Match>();
	
	protected final Map<Assignment, SQLBackend.PreparedStatements.Stmt> evalMap = new HashMap<Assignment, SQLBackend.PreparedStatements.Stmt>();
	
	protected MetaFactory factory;
	
	private final String trueValue;
	
	private final String falseValue;
	
	private final PreparedStatements evalStmts =
		new PreparedStatements("evals");
	
	private final PreparedStatements matchStmts =
		new PreparedStatements("matches");
	
	private final PreparedStatements modelStmts =
		new PreparedStatements("model");
	
	SQLBackend(String trueValue, String falseValue) {
		this.trueValue = trueValue;
		this.falseValue = falseValue;
	}
	
	
	/**
	 * Get a C macro name for an SQL datatype.
	 * @param dt The sql datatype.
	 * @return A C macro name for this datatype.
	 */
	protected static final String getTypeMacroName(DataType dt) {
		String res = "";
		switch(dt.classify()) {
			case DataType.ID:
				res = "id";
				break;
			case DataType.INT:
				res = "integer";
				break;
			case DataType.STRING:
				res = "string";
				break;
			case DataType.BOOLEAN:
				res = "boolean";
				break;
			default:
				assert false : "wrong classify value from datatype";
		}
		
		return "gr_kind_" + res;
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
		
		ps.println("\n/** The boolean True Value */");
		addDefine(ps, "GR_BOOLEAN_TRUE", trueValue);
		
		ps.println("\n/** The boolean False Value */");
		addDefine(ps, "GR_BOOLEAN_FALSE", falseValue);
		
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
				+ "  int prep_query_index;\n"
				//				+ "  const char **stmt;\n"
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
		protected final int id;
		protected final Map<Object, Integer> nodeIndexMap = new HashMap<Object, Integer>();
		protected final Map<Object, Integer> edgeIndexMap = new HashMap<Object, Integer>();
		protected final PreparedStatements.Stmt stmt;
		protected String matchIdent;
		protected String finishIdent;
		protected String stmtIdent;
		
		protected Match(int id, List<IR> nodes, List<IR> edges, PreparedStatements.Stmt stmt) {
			this.id = id;
			this.stmt = stmt;
			
			int i;
			Iterator<IR> it;
			
			for (i = 0, it = nodes.iterator(); it.hasNext(); i++)
				nodeIndexMap.put(it.next(), new Integer(i));
			
			for (i = 0, it = edges.iterator(); it.hasNext(); i++)
				edgeIndexMap.put(it.next(), new Integer(i));
		}
		
		protected static final Comparator<Match> comparator = new Comparator<Match>() {
			public int compare(Match m, Match n) {
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
		
		private final Match match;
		private final PrintStream ps;
		private Rule rule;
		private final Map<Node, Integer> insertedNodesIndexMap = new HashMap<Node, Integer>();
		private final Map<Edge, Integer> insertedEdgesIndexMap = new HashMap<Edge, Integer>();
		
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
		public void changeNodeTypes(Map<Node, Object> nodeTypeMap) {
			for(Iterator<Node> it = nodeTypeMap.keySet().iterator(); it.hasNext();) {
				Node n = it.next();
				Integer nid = match.nodeIndexMap.get(n);
				int tid = getId(n.getRetypedNode().getNodeType());
				ps.print("  CHANGE_NODE_TYPE(GET_MATCH_NODE(" + nid + "), " + tid + ");");
				ps.print("\t/* change type of ");
				ps.print(n);
				ps.print(" to ");
				ps.print(n.getRetypedNode().getType().toString());
				ps.print(" (");
				ps.print(tid);
				ps.print(") */\n");
				
			}
		}
		
		/**
		 * @see de.unika.ipd.grgen.be.rewrite.RewriteHandler#deleteEdges(java.util.Collection)
		 */
		public void deleteEdges(Collection<Edge> edges) {
			for (Iterator<Edge> it = edges.iterator(); it.hasNext();) {
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
		public void deleteEdgesOfNodes(Collection<Node> nodes) {
			for (Iterator<Node> it = nodes.iterator(); it.hasNext();) {
				Node n = (Node) it.next();
				Integer nid = match.nodeIndexMap.get(n);
				ps.print("  DELETE_NODE_EDGES(GET_MATCH_NODE(" + nid + "));");
				ps.print("\t/* delete edges of ");
				ps.print(n.toString());
				ps.print(" */\n");
			}
		}
		
		/**
		 * @see de.unika.ipd.grgen.be.rewrite.RewriteHandler#deleteNodes(java.util.Collection)
		 */
		public void deleteNodes(Collection<Node> nodes) {
			for (Iterator<Node> it = nodes.iterator(); it.hasNext();) {
				Node n = (Node) it.next();
				Integer nid = match.nodeIndexMap.get(n);
				ps.print("  DELETE_NODE(GET_MATCH_NODE(" + nid + "));");
				ps.print("\t/* delete ");
				ps.print(n);
				ps.print(" */\n");
			}
		}
		
		/**
		 * @see de.unika.ipd.grgen.be.rewrite.RewriteHandler#insertEdges(java.util.Collection)
		 */
		public void insertEdges(Collection<Edge> edges) {
			Graph right = rule.getRight();
			int i = 0;
			
			if(!edges.isEmpty())
				ps.println("  gr_id_t inserted_edges[" + edges.size() + "];");
			
			for (Iterator<Edge> it = edges.iterator(); it.hasNext(); i++) {
				Edge e = (Edge) it.next();
				
				int etid = getId(e.getEdgeType());
				Node src = right.getSource(e);
				Node tgt = right.getTarget(e);
				String leftNode, rightNode;
				
				insertedEdgesIndexMap.put(e, new Integer(i));
				
				ps.print("  inserted_edges[" + i + "] = INSERT_EDGE("
							 + etid + ", ");
				accessEntity(ps, src);
				ps.print(", ");
				accessEntity(ps, tgt);
				ps.println(");");
			}
			
		}
		
		public void accessEntity(PrintStream ps, Entity ent) {
			//Collection toInsert;
			
			if(ent instanceof Node) {
				Node n = (Node) ent;
				
				if(insertedNodesIndexMap.containsKey(n)) {
					ps.print("inserted_nodes[" + insertedNodesIndexMap.get(n) + "]");
				}
				else {
					if(n instanceof RetypedNode)
						n = ((RetypedNode)n).getOldNode();
					
					assert match.nodeIndexMap.containsKey(n);
					
					ps.print("GET_MATCH_NODE(" + match.nodeIndexMap.get(n) + ")");
				}
			} else if(ent instanceof Edge) {
				Edge e = (Edge) ent;
				if(insertedEdgesIndexMap.containsKey(e)) {
					ps.print("inserted_edges[" + insertedEdgesIndexMap.get(e) + "]");
				}
				else {
					assert match.edgeIndexMap.containsKey(e);
					
					ps.print("GET_MATCH_EDGE(" + match.edgeIndexMap.get(e) + ")");
				}
			}
		}
		
		/**
		 * @see de.unika.ipd.grgen.be.rewrite.RewriteHandler#insertNodes(java.util.Collection)
		 */
		public void insertNodes(Collection<Node> nodes) {
			/*
			 * We need an array to save the IDs of the inserted nodes, since
			 * they might be needed when inserting the new edges further down
			 * this routine.
			 */
			if(!nodes.isEmpty())
				ps.print("  gr_id_t inserted_nodes[" + nodes.size() + "];\n");
			
			/*
			 * Generate node creation statements and save the newly created
			 * IDs in the array.
			 */
			int i = 0;
			for (Iterator<Node> it = nodes.iterator(); it.hasNext(); i++) {
				Node n = (Node) it.next();
				ps.print("  inserted_nodes[" + i + "] = INSERT_NODE("
							 + getId(n.getNodeType()) + ");\n");
				insertedNodesIndexMap.put(n, new Integer(i));
			}
			
			// add retyped nodes of R
			for(Node n : rule.getRight().getNodes())
				if(n instanceof RetypedNode) {
					insertedNodesIndexMap.put(n, new Integer(i++));
				}
			
		}
		
		/**
		 * Generate an eval statement for some assignments.
		 * @param assigns A collection of assignments.
		 */
		public void generateEvals(Collection<Assignment> assigns) {
			
			for(Assignment a : assigns) {
				assert evalMap.containsKey(a);
				PreparedStatements.Stmt q = evalMap.get(a);
				int ents = q.usedEntities.size();
				int arrSize = ents > 1 ? ents : 1;
				
				ps.println("\n  {\n    gr_id_t evals_arr[" + arrSize + "];");
				int j = 0;
				for(Iterator<Entity> jt = q.usedEntities.iterator(); jt.hasNext(); j++) {
					Entity ent = jt.next();
					ps.print("    evals_arr[" + j + "] = ");
					accessEntity(ps, ent);
					ps.println(';');
				}
				
//				ps.print("    evals_arr[" + j + "] = ");
//				accessEntity(ps, a.getTarget().getOwner());
//				ps.println(';');
				
				ps.println("    EXECUTE_EVAL(" + q.idName + ", " + ents + ", evals_arr);");
				ps.println("\n  }");
			}
		}
		
		/**
		 * @see de.unika.ipd.grgen.be.rewrite.RewriteHandler#getRequiredRewriteGenerator()
		 */
		public Class<SPORewriteGenerator> getRequiredRewriteGenerator() {
			return SPORewriteGenerator.class;
		}
	}
	
	/**
	 * @see de.unika.ipd.grgen.be.C.CBackend#genFinish(java.lang.StringBuffer, de.unika.ipd.grgen.ir.MatchingAction, int)
	 */
	protected void genFinish(PrintStream ps, MatchingAction a, int id) {
		String actionIdent = formatId(a.getIdent().toString());
		String finishIdent = "finish_" + actionIdent;
		
		Match m = matchMap.get(a);
		m.finishIdent = finishIdent;
		
		assert m != null : "A match must have been produced for " + a;
		
		ps.print("static FINISH_PROTOTYPE(" + finishIdent + ")\n{\n");
		
		if(a instanceof Rule) {
			Rule r = (Rule) a;
			genEvals(r);
			rewriteGenerator.rewrite(r, new SQLRewriteHandler(m, ps));
		}
		
		// genRuleFinish(sb, (Rule) a, id, m);
		
		ps.print("  return 1;\n}\n\n");
	}
	
	
	/**
	 * Make all statements for all eval statements in a rule.
	 * The statements are enqueued in the prepared statements queue.
	 * @param r The rule to make the eval statements for.
	 */
	protected void genEvals(Rule r) {
		ByteArrayOutputStream bos = new ByteArrayOutputStream(8192);
		int num = 0;
		
		PrintStream tps = new PrintStream(bos);
		
		for(Assignment assign : r.getEvals()) {
			MarkerSource ms = getMarkerSource();
			Collection<Entity> usedEntities = new LinkedList<Entity>();
			Statement s = sqlGen.genEvalUpdateStmt(assign, factory, ms, usedEntities);
			String name = "eval_" + mangle(r) + '_' + num;
			
			bos.reset();
			s.dump(tps);
			tps.flush();
			
			evalMap.put(assign, evalStmts.add(name, s, ms.getTypes(), usedEntities));
			num++;
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
		
		SQLGenerator.MatchCtx matchCtx =
			sqlGen.makeMatchContext(system, a, factory);
		
		List<IR> nodes = matchCtx.matchedNodes;
		List<IR> edges = matchCtx.matchedEdges;
		
		
		
		// Dump the SQL statement
		/*
		 ps.print("static const char *stmt_");
		 ps.print(actionIdent);
		 ps.println(" = ");
		 ps.print(formatString(sqlGen.genMatchStatement(matchCtx)));
		 ps.println(";\n");
		 */
		
		Query stmt = sqlGen.genMatchStatement(matchCtx);
		Collection<DataType> emptyData = Collections.emptySet();
		Collection<Entity> emptyEntity = Collections.emptySet();
		
		PreparedStatements.Stmt prepStmt =
			matchStmts.add("match_" + actionIdent, stmt,
						   emptyData,	emptyEntity);
		
		// Make an array of strings that contains the node names.
		ps.println("static const char *" + nodeNamesIdent + "[] = {");
		for (Iterator<IR> it = nodes.iterator(); it.hasNext();) {
			Identifiable node = (Identifiable) it.next();
			ps.print("  ");
			ps.print(formatString(node.getIdent().toString()));
			ps.println(",");
		}
		ps.println("};\n");
		
		// Make an array of strings that contains the edge names.
		ps.println("static const char *" + edgeNamesIdent + "[] = {");
		for (Iterator<IR> it = edges.iterator(); it.hasNext();) {
			Identifiable edge = (Identifiable) it.next();
			ps.print("  ");
			ps.print(formatString(edge.getIdent().toString()));
			ps.println(",");
		}
		ps.println("};\n\n");
		
		// Make the function that invokes the SQL statement.
		ps.println("static MATCH_PROTOTYPE(" + matchIdent + ")\n{");
		ps.println("  QUERY(" + id + ");");
		
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
		
		Match m = new Match(id, nodes, edges, prepStmt);
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
		
		Match[] matches = (Match[]) matchMap.values().toArray(new Match[matchMap.size()]);
		Arrays.sort(matches, Match.comparator);
		for (int i = 0; i < matches.length; i++) {
			Match m = matches[i];
			ps.print("  { ");
			ps.print(m.matchIdent);
			ps.print(", ");
			ps.print(m.finishIdent);
			ps.print(", ");
			ps.print(m.stmt.idName);
			
			/* ps.print(", &");
			 ps.print(m.stmtIdent); */
			ps.print(", ");
			formatString(ps, m.stmtIdent);
			ps.println("},");
		}
		ps.println("};");
		closeFile(ps);
		
		// must be done before prepared queries are put out.
		ps = openFile("attr_tbl_cmd" + incExtension);
		genAttrTableCmds(ps, "node", nodeAttrMap, factory.originalNodeAttrTable());
		genAttrTableCmds(ps, "edge", edgeAttrMap, factory.originalEdgeAttrTable());
		closeFile(ps);
		
		modelStmts.writeToFile();
		evalStmts.writeToFile();
		matchStmts.writeToFile();
		
		// Emit the settings specified in the grgen config file.
		// these contain table and column names, etc.
		ps = openFile("settings" + incExtension);
		addSettings(ps);
		closeFile(ps);
		
		ps = openFile("type_names" + incExtension);
		dumpTypes(ps);
		closeFile(ps);
	}
	
	private final void dumpTypes(PrintStream ps) {
		final DataType[] arr = new DataType[4];
		
		arr[DataType.ID] = getIdType();
		arr[DataType.INT] = getIntType();
		arr[DataType.STRING] = getStringType();
		arr[DataType.BOOLEAN] = getBooleanType();
		
		ps.println("const static struct {");
		ps.println("  gr_value_kind_t kind;");
		ps.println("  const char *sql_type;");
		ps.println("} type_names[] = {");
		
		for(int i = 0; i < arr.length; i++) {
			ps.print("  { ");
			ps.print(getTypeMacroName(arr[i]));
			ps.print(", ");
			ps.print(formatString(arr[i].getText()));
			ps.print(" }");
			ps.println(i != arr.length - 1 ? ',' : ' ');
			
		}
		ps.println("};");
		closeFile(ps);
		
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
	
	/**
	 * Make getter and setter statements for each column in
	 * the table.
	 * This is used to produce the attribute access statements.
	 * The generated statements are queued using the
	 * {@link #addPreparedQuery(PreparedQueryData)} method.
	 * @param name A string to prefix the stmt name (use "node" or
	 * "edge" for instance.
	 * @param table The table to generate column access statements for.
	 */
	protected void genAttrTableCmds(PrintStream ps, String name,
									Map<Entity, Integer> attrMap, AttributeTable table) {
		
		int n = attrMap.size();
		int[] getIdMap = new int[n];
		int[] setIdMap = new int[n];
		Column colId = table.colId();
		
		ps.println("/* " + name + " table creation command */");
		ps.print("static const char *cmd_create_" + name + "_attr =\n  ");
		
		ByteArrayOutputStream bos = new ByteArrayOutputStream();
		PrintStream tps = new PrintStream(bos);
		table.dumpDecl(tps);
		tps.flush();
		tps.close();
		
		formatString(ps, bos.toString());
		ps.println(';');
		
		
		List<Column> cols = new LinkedList<Column>();
		List<Term> exprs = new LinkedList<Term>();
		MarkerSource ms = getMarkerSource();
		
		cols.add(colId);
		exprs.add(factory.markerExpression(ms, colId.getType()));
		
		for(Iterator<Entity> it = attrMap.keySet().iterator(); it.hasNext();) {
			Entity ent = it.next();
			int id = attrMap.get(ent).intValue();
			
			Statement s;
			Column col = table.colEntity(ent);
			PreparedStatements.Stmt prepStmt;
			String getName = name + "_attr_get_" + col.getDeclName();
			String setName = name + "_attr_set_" + col.getDeclName();
			
			ms = getMarkerSource();
			s = table.genGetStmt(factory, ms, col);
			prepStmt = modelStmts.add(getName, s, ms.getTypes());
			getIdMap[id] = prepStmt.id;
			
			ms = getMarkerSource();
			s = table.genUpdateStmt(factory, ms, col);
			prepStmt = modelStmts.add(setName, s, ms.getTypes());
			setIdMap[id] = prepStmt.id;
			
			cols.add(col);
			exprs.add(col.getType().initValue());
		}
		
		ps.println("\n#define GR_HAVE_" + name.toUpperCase() + "_ATTR 1\n");
		
		ps.println("static const char *cmd_init_" + name + "_attr = ");
		Statement initStmt = factory.makeInsert(table, cols, exprs);
		modelStmts.add("init_attr_" + name,
					   initStmt, Collections.singletonList(getIdType()));
		
		ps.print("  ");
		formatString(ps, Util.toString(initStmt));
		ps.println(";\n");
		
		// Make the attr set and get index tables
		ps.println("const static int cmd_get_" + name + "_attr_map[] = {");
		for(int i = 0; i < n; i++)
			ps.println("  " + getIdMap[i] + (i != n - 1 ? "," : ""));
		ps.println("};\n");
		
		ps.println("const static int cmd_set_" + name + "_attr_map[] = {");
		for(int i = 0; i < n; i++)
			ps.println("  " + setIdMap[i] + (i != n - 1 ? "," : ""));
		ps.println("};\n");
	}
	
}









