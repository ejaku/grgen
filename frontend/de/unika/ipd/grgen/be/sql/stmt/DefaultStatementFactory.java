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
 * Created on Apr 7, 2004
 *
 * @author Sebastian Hack
 * @version $Id$
 */
package de.unika.ipd.grgen.be.sql.stmt;

import de.unika.ipd.grgen.be.sql.meta.*;
import de.unika.ipd.grgen.util.*;
import java.util.*;

import de.unika.ipd.grgen.be.TypeID;
import de.unika.ipd.grgen.ir.GraphEntity;
import de.unika.ipd.grgen.ir.EdgeType;
import de.unika.ipd.grgen.ir.InheritanceType;
import de.unika.ipd.grgen.ir.NodeType;
import java.io.PrintStream;


/**
 * A default implementation for the statement factory.
 */
public class DefaultStatementFactory extends Base
	implements TypeStatementFactory, OpFactory, Opcodes {
	
	/** Operator map. */
	private final Map<Integer, Op> opMap = new HashMap<Integer, Op>();
	
	/** A factory to make types. */
	private final Dialect dialect;
	
	/** Put an operator to the operator map. */
	private void put(int opcode, Op op) {
		opMap.put(new Integer(opcode), op);
	}
	
	public DefaultStatementFactory(Dialect dialect) {
		this.dialect = dialect;
		
		put(NOT, new DefaultOp(1, 5, "NOT"));
		put(NEG, new DefaultOp(1, 1, "-"));
		put(BIT_NOT, new DefaultOp(1, 1, "~"));
		
		put(MUL, new DefaultOp(2, 1, "*"));
		put(DIV, new DefaultOp(2, 1, "/"));
		
		put(ADD, new DefaultOp(2, 2, "+"));
		put(SUB, new DefaultOp(2, 2, "-"));
		
		put(SHL, new DefaultOp(2, 3, "<<"));
		put(SHR, new DefaultOp(2, 3, ">>"));

		put(LT, new DefaultOp(2, 4, "<"));
		put(LE, new DefaultOp(2, 4, "<="));
		put(GT, new DefaultOp(2, 4, ">"));
		put(GE, new DefaultOp(2, 4, ">="));

		put(EQ, new DefaultOp(2, 5, "="));
		put(NE, new DefaultOp(2, 5, "<>"));

		put(BIT_AND, new DefaultOp(2, 6, "&"));
		put(BIT_XOR, new DefaultOp(2, 7, "#"));
		put(BIT_OR, new DefaultOp(2, 8, "|"));

		put(AND, new DefaultOp(2, 9, "AND"));
		put(OR, new DefaultOp(2, 10, "OR"));
		
		put(EXISTS, new DefaultOp(1, 4, "EXISTS"));
		put(BETWEEN_AND, new BetweenOpcode());
		put(SET_IN, new InOpcode());
		put(ISNULL, new IsNullOpcode());
	}
	
	
	private static class BetweenOpcode extends DefaultOp {
		BetweenOpcode() {
			super(3, 3, "BETWEEN");
		}
		
		public PrintStream dump(PrintStream ps, Term[] operands) {
			assert operands.length == arity();
			operands[0].dump(ps);
			ps.print(" BETWEEN ");
			operands[1].dump(ps);
			ps.print(" AND ");
			operands[2].dump(ps);
			return ps;
		}
	}
	
	private static class IsNullOpcode extends DefaultOp {
		IsNullOpcode() {
			super(1, 3, "ISNULL");
		}
		
		public PrintStream dump(PrintStream ps, Term[] operands) {
			assert operands.length == arity();
			operands[0].dump(ps);
			ps.print(" IS NULL");
			return ps;
		}
	}
	
	private static class CondOpcode extends DefaultOp {
		CondOpcode() {
			super(3, 7, "COND");
		}
		
		public PrintStream dump(PrintStream ps, Term[] operands) {
			assert operands.length == arity();
			ps.print("CASE WHEN ");
			operands[0].dump(ps);
			ps.print(" THEN ");
			operands[1].dump(ps);
			ps.print(" ELSE ");
			operands[2].dump(ps);
			ps.print(" END");
			return ps;
		}
	}
	
	private static class InOpcode extends DefaultOp {
		InOpcode() {
			super(INFINITE_ARITY, 3, "IN");
		}
		
		public PrintStream dump(PrintStream ps, Term[] operands) {
			operands[0].dump(ps);
			ps.print(" IN (");
			for(int i = 1; i < operands.length; i++) {
				ps.print(i != 1 ? "," : "");
				operands[i].dump(ps);
			}
			ps.print(")");
			return ps;
		}
	}
	
	public Op getOp(int opcode) {
		Integer key = new Integer(opcode);
		assert opMap.containsKey(key) : "Illegal opcode";
		return opMap.get(key);
	}
	
	protected static class SubqueryOpcode implements Op {
		final Query query;
		final static String text = "subquery";
		
		SubqueryOpcode(Query query) {
			this.query = query;
		}
		
		public String text() {
			return text;
		}
		
		public int arity() {
			return 0;
		}
		
		public PrintStream dump(PrintStream ps, Term[] operands) {
			query.dump(ps);
			return ps;
		}
		
		public int priority() {
			return 100;
		}
	}
	
	/**
	 * Constant terms.
	 */
	protected static class ConstantTerm extends DefaultTerm {
		
		protected static final Term[] EMPTY = new Term[0];
		
		protected static final Term NULL = new ConstantTerm("NULL");
		
		ConstantTerm(String str) {
			super(DefaultOp.constant(str), EMPTY);
		}
		
		ConstantTerm(int integer) {
			super(DefaultOp.constant(Integer.toString(integer)), EMPTY);
		}
		
		ConstantTerm(Op opcode) {
			super(opcode, EMPTY);
		}
		
		ConstantTerm(Query query) {
			super(new SubqueryOpcode(query), EMPTY);
		}
		
	}
	
	protected static class ColumnTerm extends ConstantTerm {
		
		protected Column col;
		
		ColumnTerm(Column col) {
			super(col.getAliasName());
			this.col = col;
		}
		
		/**
		 * @see de.unika.ipd.grgen.util.GraphDumpable#getNodeLabel()
		 */
		public String debugInfo() {
			return col.debugInfo();
		}
		
	}
	
	private Term makeCond(TypeIdTable table, InheritanceType ty, boolean isNode,
												TypeID typeID, Collection constraints) {
		
		boolean isRoot = ty.isRoot();
		
		if(isRoot)
			return constant(true);
		
		boolean useBetween = true;
		int compatTypesCount = 1;
		Column col = table.colTypeId();
		int tid = typeID.getId(ty, isNode);
		short[][] matrix = typeID.getIsAMatrix(isNode);
		
		boolean[] incompat = new boolean[matrix.length];
		for(Iterator constIt = constraints.iterator(); constIt.hasNext();) {
			InheritanceType inh = (InheritanceType) constIt.next();
			int id = typeID.getId(inh, isNode);
			incompat[id] = true;
		}
		
		for(int i = 0; i < matrix.length; i++)
			compatTypesCount += matrix[i][tid] > 0 && !incompat[i] ? 1 : 0;
		
		Term colExpr = expression(col);
		Term res = null;
		
		switch(compatTypesCount) {
			case 1:
				res = expression(EQ, colExpr, constant(tid));
				break;
			default:
				int[] compat = new int[compatTypesCount];
				compat[0] = tid;
				for(int i = 0, index = 1; i < matrix.length; i++) {
					if(matrix[i][tid] > 0 && !incompat[i])
						compat[index++] = i;
				}
				
				int[] setMembers = new int[compatTypesCount];
				int setMembersCount = 0;
				
				// Debug stuff
				// StringBuffer deb = new StringBuffer();
				// for(int i = 0; i < compat.length; i++) {
				// 	deb.append((i > 0 ? "," : "") + compat[i]);
				// }
				
				for(int i = 0; i < compat.length;) {
					
					// Search as long as the numbers a incrementing by one.
					int j;
					
					for(j = i + 1; j < compat.length && compat[j - 1] + 1 == compat[j] && useBetween; j++);
					
					// If there has been found a list use BETWEEN ... AND ...
					if(i != j - 1) {
						Term between = expression(BETWEEN_AND, colExpr,
																			constant(compat[i]), constant(compat[j - 1]));
						
						res = addExpression(OR, res, between);
					} else
						setMembers[setMembersCount++] = compat[i];
					
					i = j;
				}
				
				switch(setMembersCount) {
					case 0:
						break;
					case 1:
						res = addExpression(OR, res, addExpression(EQ, colExpr, constant(setMembers[0])));
						break;
					default:
						Term consts[] = new Term[setMembersCount + 1];
						consts[0] = colExpr;
						for(int i = 0; i < setMembersCount; i++)
							consts[i + 1] = constant(setMembers[i]);
						
						res = addExpression(OR, res, expression(SET_IN, consts));
				}
		}
		
		return res;
	}
	
	
	/**
	 * @see de.unika.ipd.grgen.be.sql.stmt.TypeStatementFactory#isA(de.unika.ipd.grgen.ir.Node, de.unika.ipd.grgen.be.sql.meta.Column, de.unika.ipd.grgen.be.sql.TypeID)
	 */
	public Term isA(TypeIdTable table, NodeType nodeType, TypeID typeID) {
		return makeCond(table, nodeType, true, typeID, Collections.EMPTY_SET);
	}
	
	/**
	 * @see de.unika.ipd.grgen.be.sql.stmt.TypeStatementFactory#isA(de.unika.ipd.grgen.ir.Edge, de.unika.ipd.grgen.be.sql.meta.Column, de.unika.ipd.grgen.be.sql.TypeID)
	 */
	public Term isA(TypeIdTable table, EdgeType edgeType, TypeID typeID) {
		return makeCond(table, edgeType, false, typeID, Collections.EMPTY_SET);
	}
	
	public Term isA(TypeIdTable table, GraphEntity ent,
									boolean isNode, TypeID typeID) {
		
		return makeCond(table, ent.getInheritanceType(), isNode,
										typeID, ent.getConstraints());
	}
	
	/**
	 * @see de.unika.ipd.grgen.be.sql.meta.StatementFactory#expression(int, de.unika.ipd.grgen.be.sql.meta.Term[])
	 */
	public Term expression(int opcode, Term[] operands) {
		return new DefaultTerm(getOp(opcode), operands);
	}
	
	/**
	 * @see de.unika.ipd.grgen.be.sql.meta.StatementFactory#expression(int, de.unika.ipd.grgen.be.sql.meta.Term, de.unika.ipd.grgen.be.sql.meta.Term, de.unika.ipd.grgen.be.sql.meta.Term)
	 */
	public Term expression(int op, Term exp0, Term exp1, Term exp2) {
		return expression(op, new Term[] { exp0, exp1, exp2 });
	}
	
	public Term addExpression(int op, Term exp0, Term exp1) {
		assert exp0 != null || exp1 != null : "Not both operands may be null";
		
		if(exp0 == null)
			return exp1;
		else if(exp1 == null)
			return exp0;
		else
			return expression(op, exp0, exp1);
	}
	
	/**
	 * @see de.unika.ipd.grgen.be.sql.meta.StatementFactory#expression(int, de.unika.ipd.grgen.be.sql.meta.Term, de.unika.ipd.grgen.be.sql.meta.Term)
	 */
	public Term expression(int op, Term exp0, Term exp1) {
		return expression(op, new Term[] { exp0, exp1 });
	}
	
	/**
	 * @see de.unika.ipd.grgen.be.sql.meta.StatementFactory#expression(int, de.unika.ipd.grgen.be.sql.meta.Term)
	 */
	public Term expression(int op, Term exp0) {
		return expression(op, new Term[] { exp0 });
	}
	
	/**
	 * @see de.unika.ipd.grgen.be.sql.meta.StatementFactory#expression(de.unika.ipd.grgen.be.sql.meta.Column)
	 */
	public Term expression(Column col) {
		return new ColumnTerm(col);
	}
	
	public Term markerExpression(MarkerSource ms, DataType type) {
		return ms.nextMarker(type);
	}
	
	/**
	 * @see de.unika.ipd.grgen.be.sql.meta.StatementFactory#constant(int)
	 */
	public Term constant(int integer) {
		return new ConstantTerm(integer);
	}
	
	/**
	 * @see de.unika.ipd.grgen.be.sql.meta.StatementFactory#constant(java.lang.String)
	 */
	public Term constant(String string) {
		return new ConstantTerm("\'" + string + "\'");
	}
	
	/**
	 * @see de.unika.ipd.grgen.be.sql.meta.StatementFactory#constant(boolean)
	 */
	public Term constant(boolean bool) {
		return new ConstantTerm(bool ? "TRUE" : "FALSE");
	}
	
	private static class DefaultQuery extends DefaultDebug implements Query {
		private final List<Column> columns;
		private final List relations;
		private final List groupBy;
		private final Term having;
		private final boolean distinct;
		private final int limit;
		private boolean insertLineBreaks = true;
		private Term cond;
		
		DefaultQuery(boolean distinct, List<Column> columns, List relations, Term cond,
								 List groupBy, Term having, int limit) {
			super("query");
			setChildren(relations);
			this.columns = columns;
			this.relations = relations;
			this.cond = cond;
			this.groupBy = groupBy;
			this.having = having;
			this.distinct = distinct;
			this.limit = limit;
		}
		
		DefaultQuery(boolean distinct, List<Column> columns, List relations,
								 Term cond, int limit) {
			this(distinct, columns, relations, cond, null, null, limit);
		}
		
		public int columnCount() {
			return columns.size();
		}
		
		public Column getColumn(int i) {
			return columns.get(i);
		}
		
		public void clearColumns() {
			columns.clear();
		}
		
		public List getRelations() {
			return relations;
		}
		
		public Term getCondition() {
			return cond;
		}
		
		public void setCondition(Term cond) {
			this.cond = cond;
		}
		
		void setLineBreaks(boolean yes) {
			insertLineBreaks = yes;
		}
		
		public void dump(PrintStream ps) {
			int i = 0;
			ps.print("SELECT ");
			if(distinct)
				ps.print("DISTINCT ");
			
			if(columns.isEmpty()) {
				ps.print("1");
			} else {
				for (Iterator<Column> it = columns.iterator(); it.hasNext(); i++) {
					Column c = it.next();
					ps.print(i > 0 ? ", " : "");
					c.dump(ps);
				}
			}
			
			i = 0;
			if(insertLineBreaks)
				ps.println();
			
			ps.print(" FROM ");
			for(Iterator<Relation> it = relations.iterator(); it.hasNext(); i++) {
				Relation r = it.next();
				ps.print(i > 0 ? ", " : "");
				r.dump(ps);
			}
			
			if(cond != null) {
				if(insertLineBreaks)
					ps.println();
				ps.print(" WHERE ");
				cond.dump(ps);
			}
			
			boolean haveGroupBy = groupBy != null && !groupBy.isEmpty();
			if(haveGroupBy) {
				ps.print(" GROUP BY ");
				i = 0;
				for(Iterator<Column> it = groupBy.iterator(); it.hasNext(); i++) {
					Column col = it.next();
					ps.print(i > 0 ? ", " : "");
					col.dump(ps);
				}
			}
			
			if(haveGroupBy && having != null) {
				ps.print(" HAVING ");
				having.dump(ps);
			}
			
			if(limit != NO_LIMIT) {
				ps.print(" LIMIT ");
				ps.print(limit);
			}
		}
		
		public void graphDump(GraphDumper dumper) {
			dumper.begin();
			Visitor visitor = new GraphDumpVisitor(dumper);
			Walker walker = new PostWalker(visitor);
			walker.walk(this);
			dumper.finish();
		}
		
	}
	
	/**
	 * @see de.unika.ipd.grgen.be.sql.meta.StatementFactory#simpleQuery(java.util.List, java.util.List, de.unika.ipd.grgen.be.sql.meta.Term)
	 */
	public Query simpleQuery(List<Column> columns, List relations, Term cond, int limit) {
		return new DefaultQuery(false, columns, relations, cond, NO_LIMIT);
	}
	
	public Query simpleQuery(List<Column> columns, List relations, Term cond,
													 List groupBy, Term having) {
		return new DefaultQuery(false, columns, relations, cond, groupBy, having, NO_LIMIT);
	}
	
	public Query explicitQuery(boolean distinct, List<Column> columns, Relation relation, List groupBy, Term having, int limit) {
		return new DefaultQuery(distinct, columns, Collections.singletonList(relation), null, groupBy, having, limit);
	}
	
	protected static class DefaultJoin extends DefaultDebug implements Join {
		
		private final Relation left, right;
		private final int kind;
		private Term cond;
		
		public DefaultJoin(int kind, Relation left, Relation right, Term cond) {
			super("join");
			setChildren(new Object[] { left, right, cond });
			this.left = left;
			this.right = right;
			this.cond = cond;
			this.kind = kind;
		}
		
		public Relation getLeft() {
			return left;
		}
		
		public Relation getRight() {
			return right;
		}
		
		public Term getCondition() {
			return cond;
		}
		
		public void setCondition(Term cond) {
			this.cond = cond;
		}
		
		public int columnCount() {
			return left.columnCount() + right.columnCount();
		}
		
		public Column getColumn(int i) {
			if(i >= left.columnCount())
				return right.getColumn(i - left.columnCount());
			else
				return left.getColumn(i);
		}
		
		
		public void dump(PrintStream ps) {
			left.dump(ps);
			
			ps.print("\n");
			switch(kind) {
				case LEFT_OUTER:
					ps.print(" LEFT");
					break;
				case RIGHT_OUTER:
					ps.print(" RIGHT");
					break;
				default:
					ps.print(" INNER");
			}
			
			ps.print(" JOIN ");
			right.dump(ps);
			ps.print(" ON ");
			cond.dump(ps);
		}
	}
	
	/**
	 * @see de.unika.ipd.grgen.be.sql.meta.StatementFactory#join(de.unika.ipd.grgen.be.sql.meta.Relation, de.unika.ipd.grgen.be.sql.meta.Relation, de.unika.ipd.grgen.be.sql.meta.Term)
	 */
	public Join join(int kind, Relation left, Relation right, Term cond) {
		return new DefaultJoin(kind, left, right, cond);
	}
	
	/**
	 * @see de.unika.ipd.grgen.be.sql.meta.StatementFactory#constantNull()
	 */
	public Term constantNull() {
		return ConstantTerm.NULL;
	}
	
	/**
	 * @see de.unika.ipd.grgen.be.sql.meta.StatementFactory#expression(de.unika.ipd.grgen.be.sql.meta.Query)
	 */
	public Term expression(Query query) {
		if(query instanceof DefaultQuery)
				((DefaultQuery) query).setLineBreaks(false);
		return new ConstantTerm(query);
	}
	
	private class AggregateColumn implements Aggregate {
		private final int aggregate;
		private final Column col;
		private final DataType type;
		
		AggregateColumn(int aggregate, Column col) {
			this.col = col;
			this.aggregate = aggregate;
			
			switch(aggregate) {
				case COUNT:
					type = dialect.getIntType();
					break;
				default:
					type = col.getType();
			}
		}
		
		public void dump(PrintStream ps) {
			ps.print(getAggregateName());
			ps.print("(");
			col.dump(ps);
			ps.print(")");
		}
		
		public String getAggregateName() {
			switch (aggregate) {
				case MIN: return "MIN";
				case MAX: return "MAX";
				case SUM: return "SUM";
				case COUNT: return "COUNT";
				default: throw new IllegalArgumentException();
			}
		}
		
		public String debugInfo() {
			return getAggregateName() + "(" + col.debugInfo() + ")";
		}
		
		public String getAliasName() {
			return getAggregateName() + "(" + col.getAliasName() + ")";
		}
		
		public String getDeclName() {
			return col.getDeclName();
		}
		
		public PrintStream dumpDecl(PrintStream ps) {
			return col.dumpDecl(ps);
		}
		
		public Relation getRelation() {
			return col.getRelation();
		}
		
		public DataType getType() {
			return type;
		}
	}
	
	public Aggregate aggregate(int which, Column col) {
		return new AggregateColumn(which, col);
	}
	
	private static abstract class ManipulationStmt extends DefaultDebug
		implements ManipulationStatement {
		
		protected final Table table;
		protected final Collection<Column> columns;
		protected final Collection<Term> exprs;
		protected final Term condition;
		
		ManipulationStmt(Table table, List<Column> columns, List<Term> exprs, Term condition) {
			assert columns.size() == exprs.size();
			this.table = table;
			this.columns = columns;
			this.exprs = exprs;
			this.condition = condition;
		}
		
		public Collection manipulatedColumns() {
			return Collections.unmodifiableCollection(columns);
		}
		
		public Table manipulatedTable() {
			return table;
		}
	}

	private static class InsertStmt extends ManipulationStmt {
		InsertStmt(Table table, List<Column> cols, List<Term> exprs) {
			super(table, cols, exprs, null);
		}

		public void dump(PrintStream ps) {
			ps.print("INSERT INTO ");
			table.dump(ps);
			ps.print(" (");
			
			int i = 0;
			for(Iterator<Column> it = columns.iterator(); it.hasNext();) {
				Column c = (Column) it.next();
				ps.print(c.getDeclName());

				if(it.hasNext())
					ps.print(", ");
			}
			
			ps.print(") VALUES (");
			for(Iterator<Term> it = exprs.iterator(); it.hasNext();) {
				Term t = (Term) it.next();
				t.dump(ps);

				if(it.hasNext())
					ps.print(", ");
			}
			ps.print(')');
		}
	}
	
	private static class UpdateStmt extends ManipulationStmt {
		UpdateStmt(Table table, List<Column> cols, List<Term> exprs, Term cond) {
			super(table, cols, exprs, cond);
		}

		public void dump(PrintStream ps) {
			ps.print("UPDATE ");
			table.dump(ps);
			ps.print(" SET ");
			
			int i = 0;
			Iterator<Column> it = columns.iterator();
			Iterator<Term> jt = exprs.iterator();
			for(;it.hasNext(); i++) {
				
				Column c = (Column) it.next();
				Term t = (Term) jt.next();
				
				ps.print(c.getDeclName());
				ps.print(" = (");
				t.dump(ps);
				ps.print(")");
				
				if(it.hasNext())
					ps.print(", ");
			}
			
			if(condition != null) {
				ps.print(" WHERE ");
				condition.dump(ps);
			}
		}
	}
	
	/**
	 * Make an update statement.
	 * @param rel The relation to update.
	 * @param columns A list of columns that shall be updated.
	 * @param exprs A list of terms that express the new value
	 * for each column. There must be exactly as many terms as
	 * there are columns.
	 * @param cond The condition for the update.
	 */
	public ManipulationStatement makeUpdate(Table table, List<Column> columns,
																					List<Term> exprs, Term cond) {
		
		assert columns.size() == exprs.size() && columns.size() > 0
			: "There must be as many terms as columns";
		
		return new UpdateStmt(table, columns, exprs, cond);
	}
	
	public ManipulationStatement makeInsert(Table table, List<Column> columns, List<Term> exprs) {
		
		assert columns.size() == exprs.size() && columns.size() > 0
			: "There must be as many terms as columns";
		
		return new InsertStmt(table, columns, exprs);
	}

}



