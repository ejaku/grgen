/**
 * Created on Apr 7, 2004
 *
 * @author Sebastian Hack
 * @version $Id$
 */
package de.unika.ipd.grgen.be.sql.meta;


/**
 * SQL operators.
 */
public interface Opcodes {
	
	int INVALID = -1;
	int MUL = 0;
	int DIV = MUL + 1;
	int MOD = DIV + 1;
	int ADD = MOD + 1;
	int SUB = ADD + 1;
	int NEG = SUB + 1;
	
	int EQ = NEG + 1;
	int NE = EQ + 1;
	int LT = NE + 1;
	int LE = LT + 1;
	int GT = LE + 1;
	int GE = GT + 1;

	int BETWEEN_AND = GE + 1;
	int SET_IN = BETWEEN_AND + 1;
	int SUBQUERY_IN = SET_IN + 1;
	
	int NOT = SUBQUERY_IN + 1;
	int AND = NOT + 1;
	int OR = AND + 1;
	
	int BIT_AND = OR + 1;
	int BIT_OR = BIT_AND + 1;
	int BIT_XOR = BIT_OR + 1;
	int BIT_NOT = BIT_XOR + 1;
	int SHL = BIT_NOT + 1;
	int SHR = SHL + 1;
	
	int COND = SHR + 1;
	int EXISTS = COND + 1;
	int ISNULL = EXISTS + 1;
	
}

