/**
 * Created on Apr 7, 2004
 *
 * @author Sebastian Hack
 * @version $Id$
 */
package de.unika.ipd.grgen.be.sql.stmt;

import de.unika.ipd.grgen.be.sql.meta.*;
import de.unika.ipd.grgen.util.*;

import de.unika.ipd.grgen.be.sql.TypeID;
import de.unika.ipd.grgen.ir.EdgeType;
import de.unika.ipd.grgen.ir.NodeType;
import java.util.Arrays;
import java.util.HashMap;
import java.util.Iterator;
import java.util.List;
import java.util.Map;


/**
 * A default implementation for the statement factory.
 */
public class DefaultStatementFactory extends Base
	implements TypeStatementFactory, OpFactory, Opcodes {

	/** Operator map. */
	private final Map opMap = new HashMap();
	
	/** A factory to make types. */
	private final TypeFactory typeFactory;
	
	/** Put an operator to the operator map. */
	private void put(int opcode, Op op) {
		opMap.put(new Integer(opcode), op);
	}

	public DefaultStatementFactory(TypeFactory typeFactory) {
		this.typeFactory = typeFactory;
		
		put(MUL, new DefaultOp(2, 1, "*"));
		put(DIV, new DefaultOp(2, 1, "/"));
		
		put(ADD, new DefaultOp(2, 2, "+"));
		put(SUB, new DefaultOp(2, 2, "-"));
		
		put(EQ, new DefaultOp(2, 3, "="));
		put(NE, new DefaultOp(2, 3, "<>"));
		put(LT, new DefaultOp(2, 3, "<"));
		put(LE, new DefaultOp(2, 3, "<="));
		put(GT, new DefaultOp(2, 3, ">"));
		put(GE, new DefaultOp(2, 3, ">="));

		put(BETWEEN_AND, new BetweenOpcode());
		put(SET_IN, new InOpcode());
		
		put(NOT, new DefaultOp(1, 4, "NOT"));
		put(AND, new DefaultOp(2, 5, "AND"));
		put(OR, new DefaultOp(2, 6, "OR"));
		put(EXISTS, new DefaultOp(1, 3, "EXISTS"));
	}
	
	
	private static class DefaultOp implements Op {
		
		int arity;
		int priority;
		String text;
		
		DefaultOp(int arity, int priority, String text) {
			this.arity = arity;
			this.priority = priority;
			this.text = text;
		}
		
		/**
		 * @see de.unika.ipd.grgen.be.sql.meta.Op#arity()
		 */
		public int arity() {
			return arity;
		}
		
		/**
		 * @see de.unika.ipd.grgen.be.sql.meta.Op#priority()
		 */
		public int priority() {
			return priority;
		}
		
		public String text() {
			return text;
		}
		
		protected void dumpSubTerm(Term term, StringBuffer sb) {
			boolean braces = term.getOp().priority() > priority();

			sb.append(braces ? "(" : "");
			term.dump(sb);
			sb.append(braces ? ")" : "");
		}

		public StringBuffer dump(StringBuffer sb, Term[] operands) {
			switch(arity()) {
			case 1:
				sb.append(text);
				sb.append(" ");
				dumpSubTerm(operands[0], sb);
				break;
			case 2:
				dumpSubTerm(operands[0], sb);
				sb.append(" ");
				sb.append(text);
				sb.append(" ");
				dumpSubTerm(operands[1], sb);
				break;
			}
			return sb;
		}
	}
	
	private static class BetweenOpcode extends DefaultOp {
		BetweenOpcode() {
			super(3, 3, "BETWEEN");
		}
		
		public StringBuffer dump(StringBuffer sb, Term[] operands) {
			assert operands.length == arity();
			operands[0].dump(sb);
			sb.append(" BETWEEN ");
			operands[1].dump(sb);
			sb.append(" AND ");
			operands[2].dump(sb);
			return sb;
		}
	}
	
	private static class CondOpcode extends DefaultOp {
		CondOpcode() {
			super(3, 7, "COND");
		}
		
		public StringBuffer dump(StringBuffer sb, Term[] operands) {
			assert operands.length == arity();
			sb.append("CASE WHEN ");
			operands[0].dump(sb);
			sb.append(" THEN ");
			operands[1].dump(sb);
			sb.append(" ELSE ");
			operands[2].dump(sb);
			sb.append(" END");
			return sb;
		}
	}
	
	private static class InOpcode extends DefaultOp {
		InOpcode() {
			super(INFINITE_ARITY, 3, "IN");
		}
		
		public StringBuffer dump(StringBuffer sb, Term[] operands) {
			operands[0].dump(sb);
			sb.append(" IN (");
			for(int i = 1; i < operands.length; i++) {
				sb.append(i != 1 ? "," : "");
				operands[i].dump(sb);
			}
			return sb.append(")");
		}
	}
	
	public Op getOp(int opcode) {
		Integer key = new Integer(opcode);
		assert opMap.containsKey(key) : "Illegal opcode";
		return (Op) opMap.get(key);
	}

	/**
	 * Normal terms.
	 */
	protected static class DefaultTerm extends DefaultDebug implements Term {
		
		Term[] operands;
		Op opcode;
		
		DefaultTerm(Op opcode, Term[] operands) {
			super(opcode.text());
			setChildren(operands);
			this.operands = operands;
			this.opcode = opcode;
		}
		
		/**
		 * @see de.unika.ipd.grgen.be.sql.meta.Term#getOperand(int)
		 */
		public Term getOperand(int i) {
			return i >= 0 && i < operands.length ? operands[i] : null;
		}
		
		/**
		 * @see de.unika.ipd.grgen.be.sql.meta.Term#operandCount()
		 */
		public int operandCount() {
			return operands.length;
		}
		
		/**
		 * @see de.unika.ipd.grgen.be.sql.meta.MetaBase#dump(java.lang.StringBuffer)
		 */
		public StringBuffer dump(StringBuffer sb) {
			return opcode.dump(sb, operands);
		}
		
		public Op getOp() {
			return opcode;
		}
		
	}
	
	private static class ConstantOpcode implements Op {
		
		private final String text;
		
		public int priority() {
			return 0;
		}
		
		public int arity() {
			return 0;
		}
		
		public String text() {
			return text;
		}
		
		ConstantOpcode(String text) {
			this.text = text;
		}
		
		public StringBuffer dump(StringBuffer sb, Term[] operands) {
			return sb.append(text);
		}
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
		
		public StringBuffer dump(StringBuffer sb, Term[] operands) {
			return query.dump(sb);
		}
		
		public int priority() {
			return 10;
		}
	}
	
	/**
	 * Constant terms.
	 */
	protected static class ConstantTerm extends DefaultTerm {
		
		protected static final Term[] EMPTY = new Term[0];
		 
		protected static final Term NULL = new ConstantTerm("NULL");
		
		ConstantTerm(String str) {
			super(new ConstantOpcode(str), EMPTY);
		}
		
		ConstantTerm(int integer) {
			super(new ConstantOpcode(Integer.toString(integer)), EMPTY);
		}
		
		ConstantTerm(boolean bool) {
			super(new ConstantOpcode(bool ? "TRUE" : "FALSE"), EMPTY);
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
	
	private Term makeCond(TypeIdTable table, int tid, boolean isRoot, boolean[][] matrix) {
		
		boolean useBetween = true;
		int compatTypesCount = 1;
		Column col = table.colTypeId();
		
		if(isRoot)
			return constant(true);
		
		for(int i = 0; i < matrix.length; i++)
			compatTypesCount += matrix[i][tid] ? 1 : 0;

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
				if(matrix[i][tid])
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
	public Term isA(TypeIdTable table, NodeType nt, TypeID typeID) {
		return makeCond(table, typeID.getId(nt), nt.isRoot(),
										typeID.getNodeTypeIsAMatrix());
	}

	/**
	 * @see de.unika.ipd.grgen.be.sql.stmt.TypeStatementFactory#isA(de.unika.ipd.grgen.ir.Edge, de.unika.ipd.grgen.be.sql.meta.Column, de.unika.ipd.grgen.be.sql.TypeID)
	 */
	public Term isA(TypeIdTable table, EdgeType et, TypeID typeID) {
		return makeCond(table, typeID.getId(et), et.isRoot(),
										typeID.getEdgeTypeIsAMatrix());
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
		return new ConstantTerm(bool);
	}

	private static class DefaultQuery extends DefaultDebug implements Query {
		List columns;
		List relations;
		Term cond;
		
		DefaultQuery(List columns, List relations, Term cond) {
			super("query");
			setChildren(relations);
			this.columns = columns;
			this.relations = relations;
			this.cond = cond;
		}
		
		DefaultQuery(List columns, Relation relation) {
			this(columns, Arrays.asList(new Relation[] { relation }), null);
		}
		
		public int columnCount() {
			return columns.size();
		}
		
		public Column getColumn(int i) {
			return (Column) columns.get(i);
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
		
		
		public StringBuffer dump(StringBuffer sb) {
			int i = 0;
			
			sb.append("SELECT ");
			
			if (!columns.iterator().hasNext()) {
				sb.append("1");
			} else {
				for (Iterator it = columns.iterator(); it.hasNext(); i++) {
					Column c = (Column) it.next();
					sb.append(i > 0 ? ", " : "");
					c.dump(sb);
				}
			}
			
			i = 0;
			sb.append("\nFROM ");
			for(Iterator it = relations.iterator(); it.hasNext(); i++) {
				Relation r = (Relation) it.next();
				sb.append(i > 0 ? ", " : "");
				r.dump(sb);
			}
			
			if(cond != null) {
				sb.append("\n\tWHERE ");
				cond.dump(sb);
			}
			
			return sb;
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
	public Query simpleQuery(List columns, List relations, Term cond) {
		return new DefaultQuery(columns, relations, cond);
	}
	
	public Query explicitQuery(List columns, Relation relation) {
		return new DefaultQuery(columns, relation);
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
		
		public StringBuffer dump(StringBuffer sb) {
			sb.append("");
			left.dump(sb);

			switch(kind) {
			case LEFT_OUTER:
				sb.append(" LEFT");
				break;
			case RIGHT_OUTER:
				sb.append(" RIGHT");
				break;
			default:
				sb.append(" INNER");
			}
			
			sb.append(" JOIN ");
			right.dump(sb);
			sb.append(" ON ");
			return cond.dump(sb);
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
					type = typeFactory.getIntType();
					break;
				default:
					type = col.getType();
			}
		}
		
		public StringBuffer dump(StringBuffer sb) {
			sb.append(aggregate);
			sb.append("(");
			col.dump(sb);
			sb.append(")");
			return sb;
		}
		
		public String debugInfo() {
			return aggregate + "(" + col.debugInfo() + ")";
		}
		
		public String getAliasName() {
			return aggregate + "(" + col.getAliasName() + ")";
		}
		
		public String getDeclName() {
			return col.getDeclName();
		}
		
		public StringBuffer dumpDecl(StringBuffer sb) {
			return col.dumpDecl(sb);
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
	
}


