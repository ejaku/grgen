/**
 * Created on Mar 9, 2004
 *
 * @author Sebastian Hack
 * @version $Id$
 */
package de.unika.ipd.grgen.be.sql;
import java.io.File;
import java.io.OutputStream;
import java.io.PrintStream;
import java.util.ArrayList;
import java.util.Collection;
import java.util.HashMap;
import java.util.HashSet;
import java.util.Iterator;
import java.util.LinkedList;
import java.util.List;
import java.util.Map;
import java.util.Set;

import de.unika.ipd.grgen.Sys;
import de.unika.ipd.grgen.be.TypeID;
import de.unika.ipd.grgen.be.sql.meta.Aggregate;
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
	
	/** The attribute key for limit. */
	protected static final String KEY_LIMIT = "limit";
	
	/** SQL parameters. */
	protected final SQLParameters parameters;
	
	/** And a type ID source. */
	protected final TypeID typeID;
	
	public SQLGenerator(SQLParameters parameters, TypeID typeID) {
		
		this.parameters = parameters;
		this.typeID = typeID;
	}
	
	
	public static final class MatchCtx {
		public final List matchedNodes = new LinkedList();
		public final List matchedEdges = new LinkedList();
		
		final MatchingAction action;
		final GraphTableFactory tableFactory;
		final TypeStatementFactory stmtFactory;
		final Sys system;
		
		private MatchCtx(Sys system, MatchingAction action,
										 GraphTableFactory tableFactory,
										 TypeStatementFactory stmtFactory) {
			this.system = system;
			this.action = action;
			this.tableFactory = tableFactory;
			this.stmtFactory = stmtFactory;
		}
	}
	
	public final MatchCtx makeMatchContext(Sys system, MatchingAction action,
																				 GraphTableFactory tableFactory,
																				 TypeStatementFactory stmtFactory) {
		return new MatchCtx(system, action, tableFactory, stmtFactory);
	}
	
	public final String genMatchStatement(MatchCtx matchCtx) {
		
		StringBuffer sb = new StringBuffer();
		Query q = makeMatchStatement(matchCtx);
		q.dump(sb);
		String res = sb.toString();
		
		if(matchCtx.system.backendEmitDebugFiles()) {
			
			OutputStream os = matchCtx.system.createDebugFile(new File("stmt_" + matchCtx.action.getIdent() + ".txt"));
			PrintStream ps = new PrintStream(os);
			ps.println(res);
			
			os = matchCtx.system.createDebugFile(new File("stmt_" + matchCtx.action.getIdent() + ".vcg"));
			ps = new PrintStream(os);
			GraphDumper dumper = new VCGDumper(ps);
			q.graphDump(dumper);
		}
		return res;
	}
	
	protected Query makeMatchStatement(MatchCtx ctx) {
		MatchingAction act = ctx.action;
		TypeStatementFactory factory = ctx.stmtFactory;
		GraphTableFactory tableFactory = ctx.tableFactory;
		List matchedNodes = ctx.matchedNodes;
		List matchedEdges = ctx.matchedEdges;
		
		Graph gr  = act.getPattern();
		Query q = makeQuery(ctx, gr, new LinkedList(), true);
		
		// create subQueries for negative parts
		for(Iterator it = act.getNegs(); it.hasNext();) {
			Graph neg = (Graph) it.next();
			Query inner = makeQuery(ctx, neg, q.getRelations(), true);
			
			// simplify select part of inner query, because existence of tuples is sufficient
			// in an 'exists' condition
			inner.clearColumns();
			
			// add the inner query to the where part of the outer.
			Term notEx = factory.expression(Opcodes.NOT,
																			factory.expression(Opcodes.EXISTS,
																												 factory.expression(inner)));
			
			Term cond = q.getCondition();
			if (cond==null) {
				cond = notEx;
			} else {
				cond = factory.expression(Opcodes.AND, cond, notEx);
			}
			q.setCondition(cond);
		}
		return q;
	}
	
	protected Query makeQuery(MatchCtx ctx, Graph graph,
														List excludeTables, boolean isNeg) {
		
		MatchingAction act = ctx.action;
		TypeStatementFactory factory = ctx.stmtFactory;
		GraphTableFactory tableFactory = ctx.tableFactory;
		List matchedNodes = ctx.matchedNodes;
		List matchedEdges = ctx.matchedEdges;
		
		Collection nodes = graph.getNodes(new HashSet());
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
			if(!isNeg)
				matchedNodes.add(n);
			
			// Add node type constraint
			nodeCond = factory.expression(Opcodes.AND, nodeCond,
																		factory.isA(table, n, true, typeID));
			
			
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
			graph.getOutgoing(n, incidentSets[0]);
			graph.getIncoming(n, incidentSets[1]);
			
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
					
					// TODO check for conditions of edges.
					
					
					// Just add the edge to the columns and tables,
					// if it didn't occur before.
					if (!edges.contains(e)) {
						
						edgeTables.add(edgeTable);
						edgeCols.add(edgeTable.colId());
						edges.add(e);
						
						// Add edge type constraint
						edgeCond = factory.expression(Opcodes.AND, edgeCond,
																					factory.isA(edgeTable, e, false, typeID));
						
						// Add it also to the edge result list.
						if(!isNeg)
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
		
		nodeTables.addAll(edgeTables);
		nodeCols.addAll(edgeCols);
		nodeTables.removeAll(excludeTables);
		return factory.simpleQuery(nodeCols, nodeTables,
															 factory.expression(Opcodes.AND, nodeCond, edgeCond),
															 StatementFactory.NO_LIMIT);
	}
	
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
	
	/**
	 * Method genValidateStatements produces all validate statements for all
	 * connection assertion of a spec.
	 *
	 * for source:
	 * 1	SELECT src.node_id, count(src.node_id)
	 * 2	FROM nodes AS src, nodes AS tgt, edges
	 * 3	WHERE edges.src_id = src.node_id AND
	 * 4		edges.tgt_id = tgt.node_id AND
	 * 5		edges.type_id = $E_TYPE_ID$
	 * 5b		src.type_id = $SRC_TYPE_ID$
	 * 6	GROUP BY src.node_id
	 * 7	HAVING count(src.node_id) < $SRC_RANGE_LOWER$ OR
	 * 8		count(src.node_id) > $SRC_RANGE_UPPER$;
	 *
	 * for target:
	 * 1	SELECT tgt.node_id, count(tgt.node_id)
	 * 2	FROM nodes AS src, nodes AS tgt, edges
	 * 3	WHERE edges.src_id = src.node_id AND
	 * 4		edges.tgt_id = tgt.node_id AND
	 * 5		edges.type_id = $E_TYPE_ID$
	 * 5b		tgt.type_id = $TGT_TYPE_ID$
	 * 6	GROUP BY tgt.node_id
	 * 7	HAVING count(tgt.node_id) < $TGT_RANGE_LOWER$ OR
	 * 8		count(tgt.node_id) > $TGT_RANGE_UPPER$;
	 *
	 * @return  a List containing the src and tgt sql conn assert statements
	 * 			in an alternating order.
	 */
	public List genValidateStatements(
		List srcTypes, List srcRange,  List tgtTypes, List tgtRange, List edgeTypes,
		TypeStatementFactory stmtFactory, GraphTableFactory tableFactory) {
		
		List res = new ArrayList();
		List srcColumns = new LinkedList();
		List tgtColumns = new LinkedList();
		List relations = new LinkedList();
		List srcGroupBy = new LinkedList();
		List tgtGroupBy = new LinkedList();
		Term condStub;
		
		NodeTable srcTable  = tableFactory.nodeTable("src");
		NodeTable tgtTable  = tableFactory.nodeTable("tgt");
		EdgeTable edgeTable = tableFactory.originalEdgeTable();
		
		// 1	SELECT src.node_id, count(src.node_id)
		initValidStmtSrc(srcColumns, stmtFactory, srcTable);
		
		// 1	SELECT tgt.node_id, count(tgt.node_id)
		initValidStmtTgt(tgtColumns, stmtFactory, tgtTable);
		
		// 2: FROM nodes AS src, nodes AS tgt, edges
		relations.add(srcTable);
		relations.add(tgtTable);
		relations.add(edgeTable);
		
		condStub = initValidStmtCondStub(stmtFactory,
																		 srcTable, tgtTable, edgeTable);
		
		// 6	GROUP BY src.node_id
		srcGroupBy.add(srcTable.colId());
		
		// 6	GROUP BY tgt.node_id
		tgtGroupBy.add(tgtTable.colId());
		
		for(int i  = 0; i < srcTypes.size(); i++) {
			Term tmp1, srcCond, tgtCond, srcHaving, tgtHaving;
			NodeType srcType = (NodeType)srcTypes.get(i);
			NodeType tgtType = (NodeType)tgtTypes.get(i);
			int srcRangeLower = ((int[])srcRange.get(i))[0];
			int srcRangeUpper = ((int[])srcRange.get(i))[1];
			int tgtRangeLower = ((int[])tgtRange.get(i))[0];
			int tgtRangeUpper = ((int[])tgtRange.get(i))[1];
			EdgeType edgeType = (EdgeType)edgeTypes.get(i);
			
			// 5: edges.type_id = $E_TYPE_ID$
			tmp1 = stmtFactory.isA(edgeTable, edgeType, typeID);
			srcCond = stmtFactory.addExpression(Opcodes.AND, condStub, tmp1);
			tgtCond = stmtFactory.addExpression(Opcodes.AND, condStub, tmp1);
			
			// 5b:		src.type_id = $SRC_TYPE_ID$;
			srcCond = buildValidStmtTID(stmtFactory, srcTable, srcType, srcCond);
			
			// 5b:		tgt.type_id = $TGT_TYPE_ID$
			tgtCond = buildValidStmtTID(stmtFactory, tgtTable, tgtType, tgtCond);
			
			// 7:	HAVING count(src.node_id) < $SRC_RANGE_LOWER$ OR
			// 8:		count(src.node_id) > $SRC_RANGE_UPPER$;
			srcHaving = buildValidStmtHaving(stmtFactory, srcTable,
																			 srcRangeLower, srcRangeUpper);
			
			// 7:	HAVING count(tgt.node_id) < $TGT_RANGE_LOWER$ OR
			// 8:		count(tgt.node_id) > $TGT_RANGE_UPPER$;
			tgtHaving = buildValidStmtHaving(stmtFactory, tgtTable,
																			 tgtRangeLower, tgtRangeUpper);
			
			Query src = stmtFactory.simpleQuery(srcColumns, relations, srcCond,
																					srcGroupBy, srcHaving);
			Query tgt = stmtFactory.simpleQuery(tgtColumns, relations, tgtCond,
																					tgtGroupBy, tgtHaving);
			
			res.add(src.dump(new StringBuffer()));
			res.add(tgt.dump(new StringBuffer()));
		}
		return res;
	}
	
	private Term buildValidStmtTID(TypeStatementFactory stmtFactory,
																 NodeTable table, NodeType type, Term cond) {
		Term tmp = stmtFactory.isA(table, type, typeID);
		cond = stmtFactory.addExpression(Opcodes.AND, cond, tmp);
		
		return cond;
	}
	
	private Term buildValidStmtHaving(TypeStatementFactory stmtFactory,
																		NodeTable table, int lower, int upper) {
		Term tmp1, tmp2, tmp3;
		
		// 7:	HAVING count(src.node_id) < $LOWER$ OR
		Column c =  stmtFactory.aggregate(Aggregate.COUNT, table.colId());
		tmp3 = stmtFactory.expression(c);
		tmp2 = stmtFactory.constant(lower);
		tmp1 = stmtFactory.expression(Opcodes.LT, tmp3, tmp2);
		
		// 8:		count(src.node_id) > $UPPER$;
		tmp2 = stmtFactory.constant(upper);
		tmp2 = stmtFactory.expression(Opcodes.GT, tmp3, tmp2);
		
		// OR
		tmp1 = stmtFactory.expression(Opcodes.OR, tmp1, tmp2);
		
		return tmp1;
	}
	
	private Term initValidStmtCondStub(
		TypeStatementFactory stmtFactory,
		NodeTable srcTable, NodeTable tgtTable, EdgeTable edgeTable) {
		
		Term condStub, tmp;
		// 3: edges.src_id = src.node_id AND
		condStub = stmtFactory.expression(Opcodes.EQ,
																			stmtFactory.expression(srcTable.colId()),
																			stmtFactory.expression(edgeTable.colSrcId()));
		// 4: edges.tgt_id = tgt.node_id AND
		tmp = stmtFactory.expression(Opcodes.EQ,
																 stmtFactory.expression(tgtTable.colId()),
																 stmtFactory.expression(edgeTable.colTgtId()));
		condStub = stmtFactory.expression(Opcodes.AND, condStub, tmp);
		return condStub;
	}
	
	private void  initValidStmtSrc(
		List columns, TypeStatementFactory stmtFactory,	NodeTable srcTable) {
		
		// 1: SELECT src.node_id, count(src.node_id)
		columns.add(srcTable.colId());
		columns.add(stmtFactory.aggregate(Aggregate.COUNT, srcTable.colId()));
	}
	
	private void initValidStmtTgt(
		List columns, TypeStatementFactory stmtFactory, NodeTable tgtTable) {
		
		// 1: SELECT tgt.node_id, count(tgt.node_id)
		columns.add(tgtTable.colId());
		columns.add(stmtFactory.aggregate(Aggregate.COUNT, tgtTable.colId()));
	}
}




