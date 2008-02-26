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
			ExprNode c = (ExprNode) children.get(i);
			args[i] = c.evaluate();
		}

		return getOperator().evaluate(this, args);
	}

	public boolean isConst() {
		return evaluate() instanceof ConstNode;
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
			Expression ir = (Expression) n.checkIR(Expression.class);
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
