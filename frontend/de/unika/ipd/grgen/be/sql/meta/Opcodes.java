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

