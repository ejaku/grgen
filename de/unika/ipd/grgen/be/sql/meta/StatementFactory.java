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
	 * Make a simple query.
	 * It will follow the 
	 * <pre>SELECT ... FROM ... WHERE ... </pre>
	 * scheme. 
	 * @param columns Columns to project from.
	 * @param relations The relations to join over.
	 * @param cond The join condition.
	 * @return The query.
	 */
	Query simpleQuery(List columns, List relations, Term cond);
	
	/**
	 * Make a query with explicitly given joins. 
	 * @param columns The columns to project from. 
	 * @param relation The relation.
	 * @return The query.
	 */
	Query explicitQuery(List columns, Relation relation);

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
	
}
