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
	int DIV = 1;
	int MOD = 2;
	int ADD = 3;
	int SUB = 4;
	
	int EQ = 5;
	int NE = 6;
	int LT = 7;
	int LE = 8;
	int GT = 9;
	int GE = 10;

	int BETWEEN_AND = 11;
	int SET_IN = 12;
	int SUBQUERY_IN = 13;
	
	int NOT = 14;
	int AND = 15;
	int OR = 16;
	
	int BIT_AND = 17;
	int BIT_OR = 18;
	int BIT_XOR = 19;
	int BIT_NOT = 20;
	int SHL = 21;
	int SHR = 22;
	int NEG = 23;
	
	int COND = 24;
	
}

