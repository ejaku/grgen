/*
 * GrGen: graph rewrite generator tool -- release GrGen.NET 2.6
 * Copyright (C) 2003-2010 Universitaet Karlsruhe, Institut fuer Programmstrukturen und Datenorganisation, LS Goos
 * licensed under LGPL v3 (see LICENSE.txt included in the packaging of this file)
 * www.grgen.net
 */

/**
 * @author Sebastian Hack
 * @version $Id$
 */
package de.unika.ipd.grgen.ir;

import java.util.ArrayList;
import java.util.Collection;
import java.util.List;

/**
 * An operator in an expression.
 */
public class Operator extends Expression {
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
	public static final int IN = 23;
	public static final int EXCEPT = 24;

	public static final String[] opNames = {
		"COND",	"LOG_OR", "LOG_AND", "BIT_OR", "BIT_XOR", "BIT_AND",
		"EQ", "NE", "LT", "LE", "GT", "GE", "SHL", "SHR", "BIT_SHR", "ADD",
		"SUB", "MUL", "DIV", "MOD", "LOG_NOT", "BIT_NOT", "NEG", "IN", "EXCEPT"
	};

	/** The operands of the expression. */
	protected List<Expression> operands = new ArrayList<Expression>();

	/** The opcode of the operator. */
	private int opCode;



	/** @param type The type of the operator. */
	public Operator(Type type, int opCode) {
		super("operator", type);
		this.opCode = opCode;
	}

	/** @return The opcode of this operator. */
	public int getOpCode() {
		return opCode;
	}

	/** @return The number of operands. */
	public int arity() {
		return operands.size();
	}

	/**
	 * Get the ith operand.
	 * @param index The index of the operand
	 * @return The operand, if <code>index</code> was valid, <code>null</code> if not.
	 */
	public Expression getOperand(int index) {
		return index >= 0 || index < operands.size() ? operands.get(index) : null;
	}

	/** Adds an operand e to the expression. */
	public void addOperand(Expression e) {
		operands.add(e);
	}

	public String getEdgeLabel(int edge) {
		return "op " + edge;
	}

	public String getNodeLabel() {
		return getType().getIdent() + " " + opNames[opCode].toLowerCase()
			+ "(" + opCode + ")";
	}

	public Collection<Expression> getWalkableChildren() {
		return operands;
	}

	/** @see de.unika.ipd.grgen.ir.Expression#collectNeededEntities() */
	public void collectNeededEntities(NeededEntities needs) {
		for(Expression child : getWalkableChildren())
			child.collectNeededEntities(needs);
	}
}
