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
	int NEG = 5;
	
	int EQ = 6;
	int NE = 7;
	int LT = 8;
	int LE = 9;
	int GT = 10;
	int GE = 11;

	int BETWEEN_AND = 12;
	int SET_IN = 13;
	int SUBQUERY_IN = 14;
	
	int NOT = 15;
	int AND = 16;
	int OR = 17;
	
	int BIT_AND = 18;
	int BIT_OR = 19;
	int BIT_XOR = 20;
	int BIT_NOT = 21;
	int SHL = 22;
	int SHR = 23;
	
	int COND = 24;
	int EXISTS = 25;
	int ISNULL = 26;
	
}

