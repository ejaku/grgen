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
package de.unika.ipd.grgen.be.sql.meta;

import java.util.List;


/**
 * A factory for creating SQL meta data structures.
 */
public interface StatementFactory {

	String COUNT = "count";
	String SUM = "sum";

	int NO_LIMIT = -1;

	/**
	 * Make an expression with a variable amount of operands.
	 * @param op The opcode.
	 * @param operands The operands.
	 * @return The expression.
	 */
	Term expression(int opcode, Term[] operands);

	/**
	 * Make an expression with three arguments.
	 * This should basically only be a helper function for
	 * {@link #expression(Opcode, Term[])}.
	 * @param op The opcode.
	 * @param exp0 operand 1.
	 * @param exp1 operand 2.
	 * @param exp2 operand 3.
	 * @return The expression.
	 */
	Term expression(int opcode, Term exp0, Term exp1, Term exp2);

	/**
	 * Construct an expression from two others.
	 * If <code>exp0</code> is <code>null</code> then the result will be
	 * <code>exp1</code> and the opcode is ignored. If <code>exp0</code> is not
	 * <code>null</code>, then the result is the same as obtained with
	 * {@link #expression(Opcode, Term, Term)}. The same is true vice versa.
	 * You may <i>not</i> pass two <code>null</code> values.
	 * This is useful if you construct expression successively and you can not
	 * be sure that the expression you wnat to extend was created.
	 * @param op The opcode.
	 * @param exp0 operand 1.
	 * @param exp1 operand 2.
	 * @return The expression.
	 */
	Term addExpression(int opcode, Term exp0, Term exp1);

	/**
	 * Make an expression with two arguments.
	 * This should basically only be a helper function for
	 * {@link #expression(Opcode, Term[])}.
	 * @param op The opcode.
	 * @param exp0 operand 1.
	 * @param exp1 operand 2.
	 * @return The expression.
	 */
	Term expression(int opcode, Term exp0, Term exp1);

	/**
	 * Make an expression with one argument.
	 * This should basically only be a helper function for
	 * {@link #expression(Opcode, Term[])}.
	 * @param op The opcode.
	 * @param exp0 The operand.
	 * @return The expression.
	 */
	Term expression(int opcode, Term exp0);

	/**
	 * Make an expression out of a column.
	 * @param col The column of a relation.
	 * @return The column as expression.
	 */
	Term expression(Column col);

	/**
	 * Make an expression of a query.
	 * This will basically result in a subquery.
	 * @param query The query.
	 * @return The value of the query.
	 */
	Term expression(Query query);

	/**
	 * Make an expression of a marker.
	 * This can be used to generate prepared statement with placeholders.
	 * @param markerSource The marker source.
	 * @param type The data type the marker shall represent.
	 * @return The corresponding term.
	 */
	Term markerExpression(MarkerSource markerSource, DataType type);
	
	/**
	 * Make a constant from an integer.
	 * @param integer The integer.
	 * @return An expression.
	 */
	Term constant(int integer);

	/**
	 * Make a constant from a string.
	 * @param integer The string.
	 * @return An expression.
	 */
	Term constant(String string);

	/**
	 * Make a constant from a bool.
	 * @param integer The bool.
	 * @return An expression.
	 */
	Term constant(boolean bool);

	/**
	 * The constant <tt>NULL</tt>
	 * @return An expression.
	 */
	Term constantNull();

	/**
	 * Get a coumn that results from the application of an
	 * aggregate function such as <code>sum</code> or <code>count</code>.
	 * @param which aggregate function. See {@link Aggregate}.
	 * @param col The column the aggregate shall be applied to.
	 * @return The column defined by the aggregate.
	 */
	Aggregate aggregate(int which, Column col);

	/**
	 * Make a simple query.
	 * It will follow the
	 * <pre>SELECT ... FROM ... WHERE ... </pre>
	 * scheme.
	 * @param columns Columns to project from.
	 * @param relations The relations to join over.
	 * @param cond The join condition.
	 * @param limit How many tuples may be returned (set to NO_LIMIT) if the
	 * limit part shall be omitted.
	 * @return The query.
	 */
	Query simpleQuery(List<Column> columns, List relations, Term cond, int limit);

	/**
	 * Make a simple query.
	 * It will follow the
	 * <pre>SELECT ... FROM ... WHERE ... GROUP BY ... HAVING ...</pre>
	 * @param columns Columns to project from.
	 * @param relations The relations to join over.
	 * @param cond The join condition.
	 * @param groupBy A list of columns making the group by clause.
	 * @param having The having condition.
	 * @return The query.
	 */
	Query simpleQuery(List<Column> columns, List relations, Term cond,
										List groupBy, Term having);
	
	/**
	 * Make a query with explicitly given joins.
	 * @param distinct if true, the query will be distinct, i.e.
	 * duplicate tuples are deleted.
	 * @param columns The columns to project from.
	 * @param relation The relation.
	 * @param limit How many tuples may be returned (set to NO_LIMIT) if the
	 * limit part shall be omitted.
	 * @return The query.
	 */
	Query explicitQuery(boolean distinct, List<Column> columns,
											Relation relation, List groupBy, Term having, int limit);

	/**
	 * Join over two relations.
	 * This makes an <i>join</i>.
	 * @param kind The kind of join ({@link INNER}, {@link LEFT_OUTER} or
	 * {@link RIGHT_OUTER}).
	 * @param left The left relation.
	 * @param right The right  relation.
	 * @param cond The join condition.
	 * @return The relation expressing the join.
	 */
	Join join(int kind, Relation left, Relation right, Term cond);
	
	/**
	 * Make an update statement.
	 * @param rel The relation to update.
	 * @param columns A list of columns that shall be updated.
	 * @param exprs A list of terms that express the new value
	 * for each column. There must be exactly as many terms as
	 * there are columns.
	 * @param cond The condition for the update.
	 * @return A manipulation statement.
	 * @note This method expects <code>columns.size() == exprs.size()</code>.
	 */
	ManipulationStatement makeUpdate(Table table, List<Column> columns, List<Term> exprs, Term cond);
	
	ManipulationStatement makeInsert(Table table, List<Column> columns, List<Term> exprs);
}
