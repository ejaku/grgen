/**
 * Created on Mar 9, 2004
 *
 * @author Sebastian Hack
 * @version $Id$
 */
package de.unika.ipd.grgen.be.sql;

import java.util.Collection;
import java.util.HashMap;
import java.util.HashSet;
import java.util.Iterator;
import java.util.List;
import java.util.Set;

import de.unika.ipd.grgen.ir.Condition;
import de.unika.ipd.grgen.ir.Edge;
import de.unika.ipd.grgen.ir.EdgeType;
import de.unika.ipd.grgen.ir.Graph;
import de.unika.ipd.grgen.ir.IR;
import de.unika.ipd.grgen.ir.MatchingAction;
import de.unika.ipd.grgen.ir.Node;
import de.unika.ipd.grgen.ir.NodeType;
import de.unika.ipd.grgen.ir.Operator;
import de.unika.ipd.grgen.ir.Rule;
import de.unika.ipd.grgen.util.Base;


/**
 * Generate SQL match and replace statements.
 */
public class SQLGenerator extends Base implements SQLMangler { 
	
	/** SQL parameters. */
	protected final SQLParameters parameters;
	
	/** Somebody who produces the type constraints. */
	protected final SQLFormatter formatter;
	
	/** And a type ID source. */
	protected final TypeID typeID;
	
	public SQLGenerator(SQLParameters parameters, 
			SQLFormatter constraint,
			TypeID typeID) {
		
		this.parameters = parameters;
		this.formatter = constraint;
		this.typeID = typeID;
	}
	
	/**
	 * Add something to a string buffer. If the string buffer is empty, <code>start</code>
	 * is appended. If it is not empty, <code>sep</code> is appended.
	 * Afterwards, <code>add</code> is appended.
	 * @param sb The string buffer to add to.
	 * @param start The start string.
	 * @param sep The seperator string.
	 * @param add The actual string to add.
	 */
	protected void addTo(StringBuffer sb, String start, String sep, String add) {
		sb.append(sb.length() == 0 ? start : sep);
		sb.append(add);
	}
	
	protected void addToCond(StringBuffer sb, String add) {
		addTo(sb, "", " AND ", add);
	}
	
	protected void addToList(StringBuffer sb, String table) {
		addTo(sb, "", ", ", table);
	}
	
	protected String getBreakLine() {
		return "\f";
	}
	
	/**
	 * Make an SQL table identifier out of a node.
	 * @param e The node to mangle.
	 * @return An identifier usable in SQL statements and unique for each node.
	 */
	public String mangleNode(Node n) {
		return "n" + n.getId();
	}

	/**
	 * Make an SQL table identifier out of an edge.
	 * @param e The edge to mangle.
	 * @return An identifier usable in SQL statements and unique for each edge.
	 */
	public String mangleEdge(Edge e) {
		return "e" + e.getId();
	}

	/**
	 * Make a SQL column expression for a node and a given column name.
	 * @param e The node.
	 * @param col The column.
	 * @return The column expression.
	 */
	public String getNodeCol(Node n, String col) {
		return mangleNode(n) + "." + col;
	}

	/**
	 * Mangle an identifiable object to a valid SQL identifier.
	 * @param id The identifiable object. 
	 * @return A valid SQL identifier.
	 */
	public String getEdgeCol(Edge e, String col) {
		return mangleEdge(e) + "." + col;
	}
	
	protected String join(String a, String b, String link) {
		if (a.length() == 0)
			return b;
		else if (b.length() == 0)
			return a;
		else
			return a + link + b;
	}
	
	protected String join(StringBuffer a, StringBuffer b, String link) {
		return join(a.toString(), b.toString(), link);
	}
	
	public String genMatchStatement(MatchingAction act, List matchedNodes, 
			List matchedEdges) {
		
		debug.entering();
		
		String bl = getBreakLine();
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
				parameters.getColEdgesSrcId(), parameters.getColEdgesTgtId()
		};
		
		Set workset = new HashSet();
		workset.addAll(nodes);
		HashMap edgeNotEx = new HashMap();
		
		for (Iterator it = nodes.iterator(); it.hasNext();) {
			
			Node n = (Node) it.next();
			String mangledNode = mangleNode(n);
			String nodeCol = getNodeCol(n, parameters.getColNodesId());
			
			int typeId = typeID.getId((NodeType) n.getType());
			
			workset.remove(n);
			
			debug.report(NOTE, "node: " + n);
			
			// Add this node to the table and column list
			addToList(nodeTables, parameters.getTableNodes() + " AS " + mangledNode);
			addToList(nodeCols, nodeCol);
			
			// Add it also to the result list.
			matchedNodes.add(n);
			
			// Add node type constraint
			addToCond(nodeWhere, formatter.makeNodeTypeIsA(n, this));
			
			// addToCond(nodeWhere, nodeTypeIsAFunc + "("
			//		+ getNodeCol(n, colNodesTypeId) + ", " + typeId + ")" + bl);
			
			// Make this node unequal to all other nodes.
			for (Iterator iter = workset.iterator(); iter.hasNext();) {
				Node other = (Node) iter.next();
				
				// Just add an <>, if the other node is not homomorphic to n
				// If it was, we cannot node, if it is equal or not equal to n
				if(!n.isHomomorphic(other))
					addToCond(nodeWhere, nodeCol + " <> "
							+ getNodeCol(other, parameters.getColNodesId()) + bl);
			}
			
			// TODO check for conditions of nodes.
			if (act instanceof Rule) {
				Rule r = (Rule)act;
				Condition condition = r.getCondition();
				for(Iterator conds = condition.getWalkableChildren(); conds.hasNext(); ) {
					IR cond = (IR)conds.next();
					if(cond instanceof Operator) {
						Operator operator = (Operator)cond;
						debug.report(NOTE, operator + " opcode = "  + operator.getOpCode());
						String sqlOp = getOpSQL(operator);
						debug.report(NOTE, " sqlOp = " + sqlOp);
						for(Iterator ops = operator.getWalkableChildren(); ops.hasNext(); ) {
							IR operand = (IR)ops.next();
							debug.report(NOTE, " operand = " + operand);
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
					int edgeTypeId = typeID.getId((EdgeType) e.getType());
					
					debug.report(NOTE, "incident edge: " + e);
					
					// Ignore negated edges for now.
					// TODO Implement negated edges.
					if (e.isNegated()) {
						String condition =
							mangledEdge +
							(i==0 ? "." + parameters.getColEdgesSrcId() + " = " 
									: "." + parameters.getColEdgesTgtId() + " = ") 
							// mangledNode == src | tgt
							+ mangledNode + "." + parameters.getColNodesId() + bl + " AND "
							+ formatter.makeEdgeTypeIsA(e, this) + bl;
						
						if(edgeNotEx.containsKey(mangledEdge))
							edgeNotEx.put(mangledEdge,
									edgeNotEx.get(mangledEdge)+
									" AND " + condition);
						else edgeNotEx.put(mangledEdge, condition);
						
						continue;
					}
					
					// TODO check for conditions of edges.
					
					
					// Just add the edge to the columns and tables,
					// if it didn't occur before.
					if (!edges.contains(e)) {
						addToList(edgeTables, parameters.getTableEdges() + " AS " + mangledEdge);
						addToList(edgeCols, getEdgeCol(e, parameters.getColEdgesId()));
						edges.add(e);
						
						// Add edge type constraint
						addToCond(edgeWhere, formatter.makeEdgeTypeIsA(e, this) + bl);
						
						// Add it also to the edge result list.
						matchedEdges.add(e);
					}
					
					// Add = for all edges, that are incident to the current node.
					addToCond(nodeWhere, lastJoinOn + " = " + edgeCol + bl);
					lastJoinOn = edgeCol;
				}
			}
		}
		
		for (Iterator iter = edgeNotEx.keySet().iterator();iter.hasNext();) {
			String mangledEdge=(String)iter.next();
			addToCond(edgeWhere, "NOT EXISTS (" + bl +
					"  SELECT " + mangledEdge + "." + parameters.getColEdgesId() + bl
					+ " FROM edges AS " + mangledEdge + bl
					+ " WHERE "+ edgeNotEx.get(mangledEdge)+ ")"
			);
		}
		
		debug.leaving();
		
		int limitResults = parameters.getLimitQueryResults();
		
		return "SELECT "
		+ join(nodeCols, edgeCols, ", ") + bl + " FROM "
		+ join(nodeTables, edgeTables, ", ") + bl + " WHERE "
		+ join(nodeWhere, edgeWhere, " AND ")
		+ (limitResults != 0 ? " LIMIT " + limitResults : "");
	}
	
	/**
	 * Get the SQL representation of an IR operator.
	 * @param operator The IR operator.
	 * @return The cirrsponding SQL construct.
	 */
	protected String getOpSQL(Operator operator) {
		switch (operator.getOpCode()) {
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

		return null;
	}
	
}
