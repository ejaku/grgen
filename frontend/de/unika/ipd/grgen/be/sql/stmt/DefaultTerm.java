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
 * DefaultTerm.java
 *
 * @author Sebastian Hack
 */

package de.unika.ipd.grgen.be.sql.stmt;
import de.unika.ipd.grgen.be.sql.meta.Op;
import de.unika.ipd.grgen.be.sql.meta.Term;
import de.unika.ipd.grgen.util.DefaultGraphDumpable;
import java.io.ByteArrayOutputStream;
import java.io.PrintStream;

public class DefaultTerm extends DefaultGraphDumpable implements Term {
	
	private static final Term[] NO_OPS = new Term[0];
	private final Term[] operands;
	private final Op opcode;
	private int hashCode = 0;
	
	public DefaultTerm(Op opcode, Term[] operands) {
		super(opcode.text());
		setChildren(operands);
		this.operands = operands;
		this.opcode = opcode;
	}
	
	public DefaultTerm(Op opcode) {
		this(opcode, NO_OPS);
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
	public void dump(PrintStream ps) {
		opcode.dump(ps, operands);
	}
	
	public Op getOp() {
		return opcode;
	}
	
	public String toString() {
		ByteArrayOutputStream bos = new ByteArrayOutputStream();
		PrintStream ps = new PrintStream(bos);
		opcode.dump(ps, operands);
		ps.flush();
		ps.close();
		return bos.toString();
	}
	
	/**
	 * Get some special debug info for this object.
	 * This is mostly verbose stuff for dumping.
	 * @return Debug info.
	 */
	public String debugInfo() {
		return toString();
	}
	
	/**
	 * Compare two terms for equality.
	 * @param obj Another term.
	 * @return true, if the operator and all the operands are the same.
	 */
	public boolean equals(Object obj) {
		if(obj instanceof Term) {
			Term t = (Term) obj;
			int res = 1;
			
			if(opcode.equals(t.getOp()) && t.operandCount() == operandCount()) {
				for(int i = 0; i < t.operandCount(); i++)
					res &= getOperand(i).equals(t.getOperand(i)) ? 1 : 0;
				
				return res == 1;
			}
		}
		
		return false;
	}
	
	public int hashCode() {
		if(hashCode == 0) {
			hashCode = 17;
			
			hashCode = 37 * hashCode + getOp().hashCode();
			for(int i = 0; i < operandCount(); i++)
				hashCode += 37 * hashCode + getOperand(i).hashCode();
		}

		return hashCode;
	}
	
}

