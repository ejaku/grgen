/**
 * @author Sebastian Hack
 * @version $Id$
 */
package de.unika.ipd.grgen.ir;

import java.util.Iterator;
import java.util.Vector;

/**
 * An operator in an expression.
 */
public class Operator extends Expression
{
	public static final int COND = 0;
	public static final int LOG_OR = 1;
	public static final int LOG_AND = 2;
	public static final int BIT_OR = 3;
	public static final int BIT_XOR = 4;
	public static final int BIT_AND = 5;
	public static final int EQ = 6;
	public static final int NE = 7;
	public static final int LT = 8;
	public static final int LE = 9;
	public static final int GT = 10;
	public static final int GE = 11;
	public static final int SHL = 12;
	public static final int SHR = 13;
	public static final int BIT_SHR = 14;
	public static final int ADD = 15;
	public static final int SUB = 16;
	public static final int MUL = 17;
	public static final int DIV = 18;
	public static final int MOD = 19;
	public static final int LOG_NOT = 20;
	public static final int BIT_NOT = 21;
	public static final int NEG = 22;
	public static final int CAST = 23;

	private static final String[] opNames = {
		"COND",	"LOG_OR", "LOG_AND", "BIT_OR", "BIT_XOR", "BIT_AND",
		"EQ", "LT", "LE", "GT", "GE", "SHL", "SHR", "BIT_SHR", "ADD",
		"SUB", "MUL", "DIV", "MOD", "LOG_NOT", "BIT_NOT", "NEG", "CAST",
	};
	
	/** The operands of the expression. */
	protected Vector operands = new Vector();
	
	/** The opcode of the operator. */
	private int opCode;
	
	

  /**
   * @param type The type of the operator.
   */
  public Operator(PrimitiveType type, int opCode) {
    super("operator", type);
    this.opCode = opCode;
	}
	
  /**
   * Get the opcode of this operator.
   * @return The opcode.
   */
	public int getOpCode() {
		return opCode;
	}
	
	/**
	 * Get the number of operands.
	 * @return The number of operands.
	 */
	public int operandCount() {
		return operands.size();
	}
	
	/**
	 * Get the ith operand.
	 * @param index The index of the operand
	 * @return The operand, if <code>index</code> was valid, <code>null</code> if not.
	 */
	public Expression getOperand(int index) {
		return index >= 0 || index < operands.size() ? (Expression) operands.get(index) : null; 
	}

	/**
	 * Add an operand to the expression.
	 * @param e An operand.
	 */
	public void addOperand(Expression e) {
		operands.add(e);
	}
	
	/**
	 * @see de.unika.ipd.grgen.util.Walkable#getWalkableChildren()
	 */
	public Iterator getWalkableChildren() {
		return operands.iterator();
	}

	
	public String getEdgeLabel(int edge)
	{
		return "op " + edge;
	}
	
	public String getNodeLabel()
	{
		return getType().getIdent() + " " + opNames[opCode] + "(" + opCode + ")";
	}

}
