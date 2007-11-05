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
 * Created on Mar 9, 2004
 *
 * @author Sebastian Hack
 * @version $Id$
 */
package de.unika.ipd.grgen.be.sql;

import de.unika.ipd.grgen.be.sql.meta.*;
import de.unika.ipd.grgen.be.sql.stmt.*;
import de.unika.ipd.grgen.ir.*;
import java.util.*;

import de.unika.ipd.grgen.Sys;
import de.unika.ipd.grgen.be.TypeID;
import de.unika.ipd.grgen.util.Base;
import de.unika.ipd.grgen.util.GraphDumper;
import de.unika.ipd.grgen.util.VCGDumper;
import java.io.File;
import java.io.PrintStream;


/**
 * Generate SQL match and replace statements.
 */
public class SQLGenerator extends Base {
	
	/** The attribute key for limit. */
	protected static final String KEY_LIMIT = "limit";
	
	/** SQL parameters. */
	protected final SQLParameters parameters;
	
	/** The marker source factory. */
	protected final MarkerSourceFactory markerSourceFactory;
	
	/** And a type ID source. */
	protected final TypeID typeID;
	
	public SQLGenerator(SQLParameters parameters,
						MarkerSourceFactory msf,
						TypeID typeID) {
		
		this.parameters = parameters;
		this.markerSourceFactory = msf;
		this.typeID = typeID;
	}
	
	
	public static final class MatchCtx {
		public final List<IR> matchedNodes = new LinkedList<IR>();
		public final List<IR> matchedEdges = new LinkedList<IR>();
		
		final MatchingAction action;
		final MetaFactory factory;
		final Sys system;
		
		private MatchCtx(Sys system, MatchingAction action, MetaFactory factory) {
			this.system = system;
			this.action = action;
			this.factory = factory;
		}
	}
	
	public final MatchCtx makeMatchContext(Sys system, MatchingAction action,
										   MetaFactory factory) {
		return new MatchCtx(system, action, factory);
	}
	
	public final Query genMatchStatement(MatchCtx matchCtx) {
		
		Sys system = matchCtx.system;
		Query q = makeMatchStatement(matchCtx);
		
		if(matchCtx.system.backendEmitDebugFiles()) {
			PrintStream ps;
			
			String debFilePart = "stmt_" + matchCtx.action.getIdent();
			ps = new PrintStream(system.createDebugFile(new File(debFilePart + ".txt")));
			q.dump(ps);
			ps.close();
			
			ps = new PrintStream(system.createDebugFile(new File(debFilePart + ".vcg")));
			GraphDumper dumper = new VCGDumper(ps);
			q.graphDump(dumper);
		}
		
		return q;
	}
	
	protected Query makeMatchStatement(MatchCtx ctx) {
		MatchingAction act = ctx.action;
		MetaFactory factory = ctx.factory;
		List<IR> matchedNodes = ctx.matchedNodes;
		List<IR> matchedEdges = ctx.matchedEdges;
		
		Graph gr  = act.getPattern();
		Query q = makeQuery(ctx, gr, new LinkedList(), false);
		
		// create subQueries for negative parts
		for(Graph neg : act.getNegs()) {
			Query inner = makeQuery(ctx, neg, q.getRelations(), true);
			
			// simplify select part of inner query, because existence of tuples is sufficient
			// in an 'exists' condition
			inner.clearColumns();
			
			// add the inner query to the where part of the outer.
			Term notEx = factory.expression(Opcodes.NOT,
											factory.expression(Opcodes.EXISTS,
															   factory.expression(inner)));
			
			Term cond = q.getCondition();
			q.setCondition(cond == null ? notEx :
							   factory.expression(Opcodes.AND, cond, notEx));
		}
		
		return q;
	}
	
	protected Query makeQuery(MatchCtx ctx, Graph graph,
							  List excludeTables, boolean isNeg) {
		
		MatchingAction act = ctx.action;
		MetaFactory factory = ctx.factory;
		List<IR> matchedNodes = ctx.matchedNodes;
		List<IR> matchedEdges = ctx.matchedEdges;
		
		Collection nodes = graph.getNodes();
		Collection<Edge> edges = new HashSet<Edge>();
		
		List nodeTables = new LinkedList();
		List<EdgeTable> edgeTables = new LinkedList<EdgeTable>();
		List<Column> nodeCols = new LinkedList<Column>();
		List<Column> edgeCols = new LinkedList<Column>();
		
		Map<Node, NodeTable> nodeTableMap = new HashMap<Node, NodeTable>();
		
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
		
		Set<IR> workset = new HashSet<IR>();
		workset.addAll(nodes);
		HashMap edgeNotEx = new HashMap();
		
		for(Iterator it = nodes.iterator(); it.hasNext();) {
			
			Node n = (Node) it.next();
			NodeTable table = factory.nodeTable(n);
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
			for (Iterator<IR> iter = workset.iterator(); iter.hasNext();) {
				Node other = (Node) iter.next();
				NodeTable otherNodeTable = factory.nodeTable(other);
				
				if (graph instanceof PatternGraph) {
					PatternGraph pattern = (PatternGraph) graph;

					// Just add an <>, if the other node is not homomorphic to n
					// If it was, we cannot node, if it is equal or not equal to n
					if(!pattern.isHomomorphic(n, other)) {
						nodeCond = factory.expression(Opcodes.NE, nodeColExpr,
													  factory.expression(otherNodeTable.colId()));
					}
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
				
				for (Iterator<Edge> iter = incidentSets[i].iterator(); iter.hasNext();) {
					Edge e = iter.next();
					EdgeTable edgeTable = factory.edgeTable(e);
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
	
	/**
	 * Make an update statement for an assignemt,
	 * @param a The assignment.
	 * @param factory A factory.
	 * @param ms A marker source.
	 * @param usedEntities A collection where all the used entities are put into.
	 * @return An update statement for the assignment.
	 */
	public ManipulationStatement genEvalUpdateStmt(Assignment a,
												   MetaFactory factory,
												   MarkerSource ms,
												   Collection usedEntities) {
		
		Qualification tgt = a.getTarget();
		Entity owner = tgt.getOwner();
		Expression expr = a.getExpression();
		Column col = getQualCol(tgt, factory, true);
		IdTable table = (IdTable) col.getRelation();
		UpdateQualGen qg = new UpdateQualGen(owner);
		
		Term term = genExprSQL(expr, ms, factory,
							   new HashSet(), qg);
		
		
		List<Column> cols = Collections.singletonList(col);
		List<Term> exprs = Collections.singletonList(term);
		
		Term condition = factory.expression(Opcodes.EQ,
											factory.expression(table.colId()),
											factory.markerExpression(ms, factory.getIdType()));
		
		usedEntities.addAll(qg.getMarkedEntities());
		usedEntities.add(owner);
		
		return factory.makeUpdate(table, cols, exprs, condition);
	}
	
	/**
	 * A scheme to make an SQL expression out of a qualification.
	 */
	protected static interface QualGenerator {
		
		/**
		 * Make a term from a qualification.
		 * @param qual The qualifiaction.
		 * @param ms A marker source that is probably needed.
		 * @param fact A statement factory.
		 * @param tableFact A table factory.
		 * @return A corresponding term.
		 */
		Term genQual(Qualification qual,
					 MarkerSource ms,
					 MetaFactory fact);
		
	}
	
	protected static class UpdateQualGen implements QualGenerator {
		
		private final Entity ent;
		private final Collection<Entity> markedEntites = new LinkedList<Entity>();
		
		public UpdateQualGen(Entity ent) {
			this.ent = ent;
		}
		
		public Term genQual(Qualification qual,
							MarkerSource ms,
							MetaFactory factory) {
			
			Entity owner = qual.getOwner();
			Term res = null;
			
			if(owner.equals(ent))
				res = qualOriginalColExpr.genQual(qual, ms, factory);
			else {
				res = qualSubqueryExpr.genQual(qual, ms, factory);
				markedEntites.add(owner);
			}
			
			return res;
		}
		
		public Collection<Entity> getMarkedEntities() {
			return markedEntites;
		}
	}
	
	protected static AttributeTable getQualAttrTable(Qualification qual,
													 GraphTableFactory factory,
													 boolean useOriginalTables) {
		Entity owner = qual.getOwner();
		Entity member = qual.getMember();
		boolean isNode = owner instanceof Node;
		AttributeTable attrTable;
		
		assert owner instanceof Node || owner instanceof Edge
			: "Owner must be a node or an edge";
		
		if(useOriginalTables) {
			attrTable = isNode
				? factory.originalNodeAttrTable()
				: factory.originalEdgeAttrTable();
		} else {
			attrTable = isNode
				? factory.nodeAttrTable((Node) owner)
				: factory.edgeAttrTable((Edge) owner);
		}
		
		return attrTable;
	}
	
	protected static QualGenerator qualColExpr = new QualGenerator() {
		public Term genQual(Qualification qual,
							MarkerSource ms,
							MetaFactory factory) {
			
			AttributeTable attrTable = getQualAttrTable(qual, factory, false);
			return factory.expression(attrTable.colEntity(qual.getMember()));
		}
	};
	
	protected static QualGenerator qualOriginalColExpr = new QualGenerator() {
		public Term genQual(Qualification qual,
							MarkerSource ms,
							MetaFactory factory) {
			AttributeTable attrTable = getQualAttrTable(qual, factory, true);
			return factory.expression(attrTable.colEntity(qual.getMember()));
		}
	};
	
	protected static QualGenerator qualSubqueryExpr = new QualGenerator() {
		public Term genQual(Qualification qual,
							MarkerSource ms,
							MetaFactory factory) {
			AttributeTable attrTable = getQualAttrTable(qual, factory, true);
			Column col = attrTable.colEntity(qual.getMember());
			Term cond = factory.expression(Opcodes.EQ,
										   factory.expression(attrTable.colId()),
										   factory.markerExpression(ms, factory.getIdType()));
			
			Query q = factory.simpleQuery(Collections.singletonList(col),
										  Collections.singletonList(attrTable),
										  cond,
										  TypeStatementFactory.NO_LIMIT);
			
			return factory.expression(q);
		}
	};
	
	protected Term genExprSQL(Expression expr, MetaFactory factory, Collection used) {
		return genExprSQL(expr, markerSourceFactory.getMarkerSource(),
						  factory, used, qualColExpr);
	}
	
	/**
	 * Generate a SQL term for an expression.
	 * @param expr The IR expression.
	 * @param factory The statement factory.
	 * @param tableFactory The table factory.
	 * @param usedEntities A collection where all entities occuring in
	 * the statement are recorded.
	 * @return The SQL term representing the expression.
	 */
	protected Term genExprSQL(Expression expr,
							  MarkerSource ms,
							  MetaFactory factory,
							  Collection usedEntities,
							  QualGenerator qualGen) {
		
		Term res = null;
		
		if(expr instanceof Operator) {
			Operator op = (Operator) expr;
			Term[] operands = new Term[op.arity()];
			
			for(int i = 0; i < op.arity(); i++)
				operands[i] = genExprSQL(op.getOperand(i), ms, factory,
										 usedEntities, qualGen);
			
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
			res = qualGen.genQual(qual, ms, factory);
			
			usedEntities.add(qual.getOwner());
		}
		
		return res;
	}
	
	/**
	 * Get the column for a qualification node.
	 * The qualification node must select an attribute from a
	 * node or an edge.
	 * @param qual The qualification.
	 * @param tableFactory The graph table factory.
	 * @return The column.
	 */
	protected final Column getQualCol(Qualification qual,
									  GraphTableFactory tableFactory,
									  boolean dontUseAliasTable) {
		Entity owner = qual.getOwner();
		Entity member = qual.getMember();
		
		assert owner instanceof Node || owner instanceof Edge
			: "Owner must be a node or an edge";
		
		AttributeTable attrTable;
		
		if(owner instanceof Node) {
			attrTable = dontUseAliasTable ?
				tableFactory.originalNodeAttrTable()
				: tableFactory.nodeAttrTable((Node) owner);
		} else {
			attrTable = dontUseAliasTable ?
				tableFactory.originalEdgeAttrTable()
				: tableFactory.edgeAttrTable((Edge) owner);
		}
		
		return attrTable.colEntity(member);
	}
	
}






