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
 * Created on Apr 8, 2004
 *
 * @author Sebastian Hack
 * @version $Id$
 */
package de.unika.ipd.grgen.be.sql.meta;


/**
 * An opcode.
 */
import java.io.PrintStream;

public interface Op {

	int INFINITE_ARITY = -1;
	
	/**
	 * Get the arity of an operator.
	 * @return The arity.
	 */
	int arity();
	
	/**
	 * Get the priority of an operator.
	 * @return The priority.
	 */
	int priority();
	
	/**
	 * Get a string representation of the operator.
	 * @return A string representation.
	 */
	String text();
	
	/**
	 * Dump the operator to a string buffer.
	 * @param sb The string buffer.
	 * @param operands Its operands.
	 * @return The string buffer.
	 */
	PrintStream dump(PrintStream ps, Term[] operands);
	
}
