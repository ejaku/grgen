/*
 * GrGen: graph rewrite generator tool -- release GrGen.NET 5.0
 * Copyright (C) 2003-2020 Universitaet Karlsruhe, Institut fuer Programmstrukturen und Datenorganisation, LS Goos; and free programmers
 * licensed under LGPL v3 (see LICENSE.txt included in the packaging of this file)
 * www.grgen.net
 */

/**
 * @author Sebastian Hack
 */

package de.unika.ipd.grgen.ir.expr;

import java.util.ArrayList;
import java.util.Collection;
import java.util.List;

import de.unika.ipd.grgen.ir.*;
import de.unika.ipd.grgen.ir.type.Type;

/**
 * An operator in an expression.
 */
public class Operator extends Expression
{
	public enum OperatorCode
	{
		COND,
		LOG_OR,
		LOG_AND,
		BIT_OR,
		BIT_XOR,
		BIT_AND,
		EQ,
		NE,
		LT,
		LE,
		GT,
		GE,
		SHL,
		SHR,
		BIT_SHR,
		ADD,
		SUB,
		MUL,
		DIV,
		MOD,
		LOG_NOT,
		BIT_NOT,
		NEG,
		IN,
		EXCEPT,
		SE
	}
	
	/** The operands of the expression. */
	protected List<Expression> operands = new ArrayList<Expression>();

	/** The opcode of the operator. */
	private OperatorCode opCode;

	/** @param type The type of the operator. */
	public Operator(Type type, OperatorCode opCode)
	{
		super("operator", type);
		this.opCode = opCode;
	}

	/** @return The opcode of this operator. */
	public OperatorCode getOpCode()
	{
		return opCode;
	}

	/** @return The number of operands. */
	public int arity()
	{
		return operands.size();
	}

	/**
	 * Get the ith operand.
	 * @param index The index of the operand
	 * @return The operand, if <code>index</code> was valid, <code>null</code> if not.
	 */
	public Expression getOperand(int index)
	{
		return index >= 0 || index < operands.size() ? operands.get(index) : null;
	}

	/** Adds an operand e to the expression. */
	public void addOperand(Expression e)
	{
		operands.add(e);
	}

	@Override
	public String getEdgeLabel(int edge)
	{
		return "op " + edge;
	}

	@Override
	public String getNodeLabel()
	{
		return getType().getIdent() + " " + opCode.toString().toLowerCase()
				+ "(" + opCode + ")";
	}

	public Collection<Expression> getWalkableChildren()
	{
		return operands;
	}

	/** @see de.unika.ipd.grgen.ir.expr.Expression#collectNeededEntities() */
	@Override
	public void collectNeededEntities(NeededEntities needs)
	{
		for(Expression child : getWalkableChildren()) {
			child.collectNeededEntities(needs);
		}
	}
}
