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
 * DefaultOp.java
 *
 * @author Sebastian Hack
 */

package de.unika.ipd.grgen.be.sql.stmt;

import de.unika.ipd.grgen.be.sql.meta.Op;
import de.unika.ipd.grgen.be.sql.meta.Term;
import java.io.PrintStream;

public class DefaultOp implements Op {

	private final int arity;
	private final int priority;
	private final String text;
	
	public DefaultOp(int arity, int priority, String text) {
		this.arity = arity;
		this.priority = priority;
		this.text = text;
	}
	
	public DefaultOp(String text) {
		this(0, 0, text);
	}
	
	public static final Op constant(String text) {
		return new DefaultOp(text);
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
	
	protected void dumpSubTerm(Term term, PrintStream ps) {
		boolean braces = term.getOp().priority() > priority();
		
		ps.print(braces ? "(" : "");
		term.dump(ps);
		ps.print(braces ? ")" : "");
	}
	
	public PrintStream dump(PrintStream ps, Term[] operands) {
		switch(arity()) {
			case 0:
				ps.print(text);
				break;
			case 1:
				ps.print(text);
				ps.print(" ");
				dumpSubTerm(operands[0], ps);
				break;
			case 2:
				dumpSubTerm(operands[0], ps);
				ps.print(" ");
				ps.print(text);
				ps.print(" ");
				dumpSubTerm(operands[1], ps);
				break;
		}
		return ps;
	}
	
	/**
	 * Compare tow opcodes.
	 * Two opcodes are equal if they have the same textual representation.
	 * @param obj Another object.
	 * @return true, if the opcodes are equal, false if not.
	 */
	public boolean equals(Object obj) {
		if(obj instanceof Op) {
			Op op = (Op) obj;
			return text.equals(op.text());
		}
		
		return false;
	}
	
	public int hashCode() {
		return text.hashCode();
	}
}

