/**
 * Created on Mar 9, 2004
 *
 * @author Sebastian Hack
 * @version $Id$
 */
package de.unika.ipd.grgen.be.sql;

import java.io.File;
import java.io.FileOutputStream;
import java.io.IOException;
import java.io.PrintStream;
import java.util.Collection;
import java.util.HashMap;
import java.util.HashSet;
import java.util.Iterator;
import java.util.LinkedList;
import java.util.List;
import java.util.Map;
import java.util.Set;

import de.unika.ipd.grgen.be.sql.meta.Column;
import de.unika.ipd.grgen.be.sql.meta.Opcodes;
import de.unika.ipd.grgen.be.sql.meta.Query;
import de.unika.ipd.grgen.be.sql.meta.StatementFactory;
import de.unika.ipd.grgen.be.sql.meta.Term;
import de.unika.ipd.grgen.be.sql.stmt.AttributeTable;
import de.unika.ipd.grgen.be.sql.stmt.EdgeTable;
import de.unika.ipd.grgen.be.sql.stmt.GraphTableFactory;
import de.unika.ipd.grgen.be.sql.stmt.NodeTable;
import de.unika.ipd.grgen.be.sql.stmt.TypeIdTable;
import de.unika.ipd.grgen.be.sql.stmt.TypeStatementFactory;
import de.unika.ipd.grgen.ir.Constant;
import de.unika.ipd.grgen.ir.Edge;
import de.unika.ipd.grgen.ir.EdgeType;
import de.unika.ipd.grgen.ir.Entity;
import de.unika.ipd.grgen.ir.Expression;
import de.unika.ipd.grgen.ir.Graph;
import de.unika.ipd.grgen.ir.MatchingAction;
import de.unika.ipd.grgen.ir.Node;
import de.unika.ipd.grgen.ir.NodeType;
import de.unika.ipd.grgen.ir.Operator;
import de.unika.ipd.grgen.ir.Qualification;
import de.unika.ipd.grgen.util.Base;
import de.unika.ipd.grgen.util.GraphDumper;
import de.unika.ipd.grgen.util.VCGDumper;


/**
 * Generate SQL match and replace statements.
 */
public class SQLGenerator extends Base {
	
	/** SQL parameters. */
	protected final SQLParameters parameters;
	
	/** And a type ID source. */
	protected final TypeID typeID;
	
	public SQLGenerator(SQLParameters parameters, TypeID typeID) {
		
		this.parameters = parameters;
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
	protected void addTo(StringBuffer sb, CharSequence start, CharSequence sep, 
			CharSequence add) {
			
		sb.append(sb.length() == 0 ? start : sep);
		sb.append(add);
	}
	
	protected void addToCond(StringBuffer sb, CharSequence add) {
		addTo(sb, "", " AND ", add);
	}
	
	protected void addToList(StringBuffer sb, CharSequence table) {
		addTo(sb, "", ", ", table);
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
	

	public final String genMatchStatement(MatchingAction act, List matchedNodes, 
			List matchedEdges, GraphTableFactory tableFactory, TypeStatementFactory factory) {
		StringBuffer sb = new StringBuffer();
		Query q = makeMatchStatement(act, matchedNodes, matchedEdges, tableFactory, factory);
		q.dump(sb);
		String res = sb.toString();
		
		if(enableDebug) {
			writeFile(new File("stmt_" + act.getIdent() + ".txt"), res);
			
			try {
				FileOutputStream fos = new FileOutputStream(new File("stmt_" + act.getIdent() + ".vcg"));
				PrintStream ps = new PrintStream(fos);
				GraphDumper dumper = new VCGDumper(ps);
				q.graphDump(dumper);
			} catch(IOException io) {
			}
		}		
		return res; 
	}
	
	protected Query makeMatchStatement(MatchingAction act, List matchedNodes, 
			List matchedEdges, GraphTableFactory tableFactory, TypeStatementFactory factory) {
		
		debug.entering();
		
		Graph gr = act.getPattern();
		Collection nodes = gr.getNodes(new HashSet());
		Collection edges = new HashSet();
		
		List nodeTables = new LinkedList();
		List edgeTables = new LinkedList();
		List nodeCols = new LinkedList();
		List edgeCols = new LinkedList();
		
		Map nodeTableMap = new HashMap();
		
		Term nodeCond = factory.constant(true);
		Term edgeCond = factory.constant(true);
		
		
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
		
		for(Iterator it = nodes.iterator(); it.hasNext();) {
			
			Node n = (Node) it.next();
			NodeTable table = tableFactory.nodeTable(n);
			Column col = table.colId();
			Term nodeColExpr = factory.expression(col);
			
			
			//- String nodeCol = getNodeCol(n, parameters.getColNodesId());
			
			int typeId = typeID.getId((NodeType) n.getType());
			
			workset.remove(n);
			
			debug.report(NOTE, "node: " + n);
			
			// Add this node to the table and column list
			nodeTables.add(table);
			nodeCols.add(table.colId());
			nodeTableMap.put(n, table);
			
			// Add it also to the result list.
			matchedNodes.add(n);
			
			// Add node type constraint
			nodeCond = factory.expression(Opcodes.AND, nodeCond, 
					factory.isA(n, tableFactory, typeID));
			// TODO Do the right thing here.
			
			
			// Make this node unequal to all other nodes.
			for (Iterator iter = workset.iterator(); iter.hasNext();) {
				Node other = (Node) iter.next();
				NodeTable otherNodeTable = tableFactory.nodeTable(other);
				
				// Just add an <>, if the other node is not homomorphic to n
				// If it was, we cannot node, if it is equal or not equal to n
				if(!n.isHomomorphic(other)) {
					nodeCond = factory.expression(Opcodes.NE, nodeColExpr, 
							factory.expression(otherNodeTable.colId()));
				}
			}
			
			incidentSets[0].clear();
			incidentSets[1].clear();
			gr.getOutgoing(n, incidentSets[0]);
			gr.getIncoming(n, incidentSets[1]);
			
			Term lastColExpr = nodeColExpr;
		
			// Make this node equal to all source and target nodes of the
			// outgoing and incoming edges.
			for(int i = 0; i < incidentSets.length; i++) {
				boolean src = i == 0;
				
				for (Iterator iter = incidentSets[i].iterator(); iter.hasNext();) {
					Edge e = (Edge) iter.next();
					EdgeTable edgeTable = tableFactory.edgeTable(e);
					Column edgeCol = edgeTable.colId();
					int edgeTypeId = typeID.getId((EdgeType) e.getType());
					
					debug.report(NOTE, "incident edge: " + e);
					
					// Ignore negated edges for now.
					if(e.isNegated())
						continue;
					
					
					// TODO check for conditions of edges.
					
					
					// Just add the edge to the columns and tables,
					// if it didn't occur before.
					if (!edges.contains(e)) {
						
						edgeTables.add(edgeTable);
						edgeCols.add(edgeTable.colId());
						edges.add(e);
						
						// Add edge type constraint
						edgeCond = factory.expression(Opcodes.AND, edgeCond, 
								factory.isA(e, tableFactory, typeID));
						
						// Add it also to the edge result list.
						matchedEdges.add(e);
					}
					
					Term edgeColExpr = factory.expression(edgeTable.colEndId(src)); 
					
					// Add = for all edges, that are incident to the current node.
					nodeCond = factory.expression(Opcodes.AND, nodeCond,
							factory.expression(Opcodes.EQ, lastColExpr, edgeColExpr));
									
					lastColExpr = edgeColExpr;
				}
			}
		}
		
		/*
		for (Iterator iter = edgeNotEx.keySet().iterator();iter.hasNext();) {
			String mangledEdge=(String)iter.next();
			addToCond(edgeWhere, "NOT EXISTS (" + bl +
					"  SELECT " + mangledEdge + "." + parameters.getColEdgesId() + bl
					+ " FROM edges AS " + mangledEdge + bl
					+ " WHERE "+ edgeNotEx.get(mangledEdge)+ ")"
			);
		}
		*/
		
		debug.leaving();

		nodeTables.addAll(edgeTables);
		nodeCols.addAll(edgeCols);
		
		return factory.simpleQuery(nodeCols, nodeTables,
				factory.expression(Opcodes.AND, nodeCond, edgeCond));
	}
	
	/*
	protected String makeMatchStatement(MatchingAction act, List matchedNodes, 
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
		
		StringBuffer condExpr = new StringBuffer();
		genCondClause(act, condExpr);
		
		return "SELECT "
		+ join(nodeCols, edgeCols, ", ") + bl + " FROM "
		+ join(nodeTables, edgeTables, ", ") + bl + " WHERE "
		+ join(nodeWhere, edgeWhere, " AND ")
		+ (condExpr.length() == 0 ? "" : " AND ") + condExpr
		+ (limitResults != 0 ? " LIMIT " + limitResults : "");
	} */

	/**
	 * Get the SQL opcode of an IR operator.
	 * @param operator The IR operator.
	 * @return The corresponding SQL opcode.
	 */
	protected final int getOpSQL(Operator operator) {
		switch (operator.getOpCode()) {
		case Operator.COND:      return Opcodes.COND;
		case Operator.LOG_OR:    return Opcodes.OR;
		case Operator.LOG_AND:   return Opcodes.AND;
		case Operator.BIT_OR:    return Opcodes.BIT_OR;
		case Operator.BIT_XOR:   return Opcodes.BIT_XOR;
		case Operator.BIT_AND:   return Opcodes.BIT_AND;
		case Operator.EQ:        return Opcodes.EQ;
		case Operator.NE:        return Opcodes.NE;
		case Operator.LT:        return Opcodes.LT;
		case Operator.LE:        return Opcodes.LE;
		case Operator.GT:        return Opcodes.GT;
		case Operator.GE:        return Opcodes.GE;
		case Operator.SHL:       return Opcodes.SHL;
		case Operator.SHR:       return Opcodes.SHR;
		case Operator.BIT_SHR:   return Opcodes.SHR;
		case Operator.ADD:       return Opcodes.ADD;
		case Operator.SUB:       return Opcodes.SUB;
		case Operator.MUL:       return Opcodes.MUL;
		case Operator.DIV:       return Opcodes.DIV;
		case Operator.MOD:       return Opcodes.MOD;
		case Operator.LOG_NOT:   return Opcodes.NOT;
		case Operator.BIT_NOT:   return Opcodes.BIT_NOT;
		case Operator.NEG:       return Opcodes.NEG;
		}
		
		return -1;
	}
	
	protected Term genExprSQL(Expression expr, StatementFactory factory,
			GraphTableFactory tableFactory) {
		return genExprSQL(expr, factory, tableFactory, null);
	}

	protected Term genExprSQL(Expression expr, StatementFactory factory,
			GraphTableFactory tableFactory, Collection usedEntities) {
			
		Term res = null;
		
		if(expr instanceof Operator) {
			Operator op = (Operator) expr;
			Term[] operands = new Term[op.operandCount()];
			
			for(int i = 0; i < op.operandCount(); i++)
				operands[i] = genExprSQL(op.getOperand(i), factory, tableFactory, usedEntities);
			
			res = factory.expression(getOpSQL(op), operands);
		} else if(expr instanceof Constant) {
			Constant cnst = (Constant) expr;
			Object value = cnst.getValue();
			
			if(value instanceof Integer)
				res = factory.constant(((Integer) value).intValue());
			else if(value instanceof String) 
				res = factory.constant((String) value);
			else if(value instanceof Boolean) 
				res = factory.constant(((Boolean) value).booleanValue());
			
		} else if(expr instanceof Qualification) {
			
			Qualification qual = (Qualification) expr;
			Entity owner = qual.getOwner();
			Entity member = qual.getMember();
			
			assert owner instanceof Node || owner instanceof Edge 
				: "Owner must be a node or an edge";
				
			boolean isNode = owner instanceof Node;
			TypeIdTable table;
			AttributeTable attrTable;
			
			if(owner instanceof Node) {
				table = tableFactory.nodeTable((Node) owner);
				attrTable = tableFactory.nodeAttrTable((Node) owner);
			} else {
				table = tableFactory.edgeTable((Edge) owner);
				attrTable = tableFactory.edgeAttrTable((Edge) owner);
			}
			
			Column memberCol = attrTable.colEntity(member);
			assert memberCol != null : "Member column must exist";
			
			res = factory.expression(memberCol);
			
			if(usedEntities != null)
				usedEntities.add(owner);
		}
		
		return res;
	}
	
}
