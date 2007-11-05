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
 * An expression appearing in conditions.
 */
public interface Term extends MetaBase {

	/**
	 * Get the number of operands.
	 * @return The number of operands.
	 */
	int operandCount();
	
	/**
	 * Get the operand at position <code>i</code>.
	 * @param i The position.
	 * @return The operand at position <code>i</code> or <code>null</code> if
	 * <code>i</code> is not in a valid range.
	 */
	Term getOperand(int i);
	
	/**
	 * Get the opcode of the term.
	 * @return The opcode.
	 */
	Op getOp();
	
}
