/*
 * GrGen: graph rewrite generator tool -- release GrGen.NET v2 beta
 * Copyright (C) 2008 Universitaet Karlsruhe, Institut fuer Programmstrukturen und Datenorganisation, LS Goos
 * licensed under GPL v3 (see LICENSE.txt included in the packaging of this file)
 */

/**
 * @author Sebastian Hack
 * @version $Id$
 */
package de.unika.ipd.grgen.ast;

import java.util.Collection;
import java.util.HashMap;
import java.util.Map;
import java.util.Vector;

import de.unika.ipd.grgen.ir.Expression;
import de.unika.ipd.grgen.ir.IR;
import de.unika.ipd.grgen.ir.Operator;
import de.unika.ipd.grgen.parser.Coords;

/**
 * An arithmetic operator.
 */
public class ArithmeticOpNode extends OpNode {
	static {
		setName(ArithmeticOpNode.class, "arithmetic operator");
	}

	/** maps an operator id to IR opcode, filled with code beyond */
	private static Map<Integer, Integer> irOpCodeMap = new HashMap<Integer, Integer>();

	static {
		assocOpCode(OperatorSignature.COND, Operator.COND);
		assocOpCode(OperatorSignature.LOG_OR, Operator.LOG_OR);
		assocOpCode(OperatorSignature.LOG_AND, Operator.LOG_AND);
		assocOpCode(OperatorSignature.BIT_OR, Operator.BIT_OR);
		assocOpCode(OperatorSignature.BIT_XOR, Operator.BIT_XOR);
		assocOpCode(OperatorSignature.BIT_AND, Operator.BIT_AND);
		assocOpCode(OperatorSignature.EQ, Operator.EQ);
		assocOpCode(OperatorSignature.NE, Operator.NE);
		assocOpCode(OperatorSignature.LT, Operator.LT);
		assocOpCode(OperatorSignature.LE, Operator.LE);
		assocOpCode(OperatorSignature.GT, Operator.GT);
		assocOpCode(OperatorSignature.GE, Operator.GE);
		assocOpCode(OperatorSignature.SHL, Operator.SHL);
		assocOpCode(OperatorSignature.SHR, Operator.SHR);
		assocOpCode(OperatorSignature.BIT_SHR, Operator.BIT_SHR);
		assocOpCode(OperatorSignature.ADD, Operator.ADD);
		assocOpCode(OperatorSignature.SUB, Operator.SUB);
		assocOpCode(OperatorSignature.MUL, Operator.MUL);
		assocOpCode(OperatorSignature.DIV, Operator.DIV);
		assocOpCode(OperatorSignature.MOD, Operator.MOD);
		assocOpCode(OperatorSignature.LOG_NOT, Operator.LOG_NOT);
		assocOpCode(OperatorSignature.BIT_NOT, Operator.BIT_NOT);
		assocOpCode(OperatorSignature.NEG, Operator.NEG);
	}


	/**
	 * @param coords Source code coordinates.
	 * @param opId ID of the operator.
	 */
	public ArithmeticOpNode(Coords coords, int opId) {
		super(coords, opId);
	}

	/** returns children of this node */
	public Collection<ExprNode> getChildren() {
		return children;
	}

	/** returns names of the children, same order as in getChildren */
	public Collection<String> getChildrenNames() {
		Vector<String> childrenNames = new Vector<String>();
		// nameless children
		return childrenNames;
	}

	/** @see de.unika.ipd.grgen.ast.BaseNode#resolveLocal() */
	protected boolean resolveLocal() {
		return true;
	}


	/** @see de.unika.ipd.grgen.ast.ExprNode#evaluate() */
	public ExprNode evaluate() {
		int n = children.size();
		ExprNode[] args = new ExprNode[n];

		for(int i = 0; i < n; i++) {
			ExprNode c = children.get(i);
			args[i] = c.evaluate();
		}

		return getOperator().evaluate(this, args);
	}

	/** All children of this expression node must be expression nodes, too.
	 *  @see de.unika.ipd.grgen.ast.BaseNode#checkLocal() */
	protected boolean checkLocal() {
		return super.checkLocal();
	}

	/** @see de.unika.ipd.grgen.ast.BaseNode#constructIR() */
	protected IR constructIR() {
		DeclaredTypeNode type = (DeclaredTypeNode) getType();
		Operator op = new Operator(type.getPrimitiveType(), getIROpCode(getOpId()));

		for(BaseNode n : children) {
			Expression ir = n.checkIR(Expression.class);
			op.addOperand(ir);
		}

		return op;
	}

	private static void assocOpCode(int id, int opcode) {
		irOpCodeMap.put(new Integer(id), new Integer(opcode));
	}

	/** Maps an operator ID to an IR opcode. */
	private static int getIROpCode(int opId) {
		return irOpCodeMap.get(new Integer(opId)).intValue();
	}

	public String toString() {
		return OperatorSignature.getName(getOpId());
	}
}
