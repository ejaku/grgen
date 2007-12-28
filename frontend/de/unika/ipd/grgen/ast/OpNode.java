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

import de.unika.ipd.grgen.ir.Expression;
import de.unika.ipd.grgen.ir.IR;
import de.unika.ipd.grgen.ir.Operator;
import de.unika.ipd.grgen.parser.Coords;
import java.util.HashMap;
import java.util.Map;

/**
 * Expression operators.
 */
public abstract class OpNode extends ExprNode {
	private static Map<Integer, Integer> irOpCodeMap = new HashMap<Integer, Integer>();

	static {
		setName(OpNode.class, "operator");

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

	private static void assocOpCode(int id, int opcode) {
		irOpCodeMap.put(new Integer(id), new Integer(opcode));
	}

	/** The ID of the operator. */
	private int opId;

	/** The corresponding operator. */
	private OperatorSignature operator;

	/**
	 * Make a new operator node.
	 * @param coords The source coordinates of that node.
	 * @param opId The operator ID.
	 */
	public OpNode(Coords coords, int opId) {
		super(coords);
		this.opId = opId;

		//    for(int i = 0; i < OperatorSignature.getArity(opId); i++)
		//    	addResolver(i, identExprResolver);
	}

	public String toString() {
		return OperatorSignature.getName(opId);
	}

	/**
	 * @see de.unika.ipd.grgen.ast.BaseNode#checkLocal()
	 */
	protected boolean checkLocal() {
		boolean res = true;
		TypeNode type = getType();
		int arity = OperatorSignature.getArity(opId);

		if(children() != arity) {
			reportError("Wrong operator arity: " + children());
			res = false;
		}

		//Here The error must already have been reported
		if ( type.isEqual(BasicTypeNode.errorType) )
			res = false;
		else if(!type.isBasic() &&
				!(arity == 3 && (type instanceof EnumTypeNode))) {
			res = false;
			reportError("Result must be a basic type not: " + getType());
		}

		return res;
	}

	/**
	 * Determine the operator that will be used with this operator node.
	 * The method gets the operand types of this node and determines the
	 * operator, that will need the least implicit type casts using the
	 * operands' types (this is done via
	 * {@link Operator#getNearest(int, TypeNode[])}). If no such operator is
	 * found, an error message is reported.
	 * @return The proper operator for this node, <code>null</code> otherwise.
	 */
	private OperatorSignature computeOperator() {
		OperatorSignature operator = null;
		int n = children();
		TypeNode[] argTypes = new TypeNode[n];

		for(int i = 0; i < n; i++) {
			ExprNode op = (ExprNode) getChild(i);
			argTypes[i] = op.getType();
		}

		operator = OperatorSignature.getNearest(opId, argTypes);
		if(!operator.isValid())	{
			StringBuffer params = new StringBuffer();
			boolean errorReported = false;

			params.append('(');
			for(int i = 0; i < n; i++) {
				if (argTypes[i].isEqual(BasicTypeNode.errorType))
					errorReported = true;
				else
					params.append((i > 0 ? ", " : "") + argTypes[i].toString());

			}
			params.append(')');

			if (!errorReported)
				reportError("No such operator " + OperatorSignature.getName(opId) + params);
		} else {
			// Insert implicit type casts for the arguments that need them.
			TypeNode[] opTypes = operator.getOperandTypes();
			assert (opTypes.length == argTypes.length);
			for(int i = 0; i < argTypes.length; i++) {
				if(!argTypes[i].isEqual(opTypes[i])) {
					ExprNode child = (ExprNode) getChild(i);
					replaceChild(i, child.adjustType(opTypes[i])); //NOTE: changed argType to optype
				}
			}
		}

		return operator;
	}

	protected final OperatorSignature getOperator() {
		if(operator == null) {
			operator = computeOperator();
		}

		return operator;
	}

	/**
	 * Get the type of this expression.
	 * @see de.unika.ipd.grgen.ast.ExprNode#getType()
	 * If a proper operator for this node can be found, the type of this
	 * node is the result type of the operator, else it's the error type
	 * {@link BasicTypeNode#errorType}.
	 */
	public TypeNode getType() {
		return getOperator().getResultType();
	}

	/**
	 * Get an IR opcode for the ID of an operator.
	 * @param opId The ID of an operator.
	 * @return THe IT opcode for the operator ID.
	 */
	private static int getIROpCode(int opId) {
		return irOpCodeMap.get(new Integer(opId)).intValue();
	}

	/**
	 * @see de.unika.ipd.grgen.ast.BaseNode#constructIR()
	 */
	protected IR constructIR() {
		DeclaredTypeNode type = (DeclaredTypeNode) getType();
		Operator op = new Operator(type.getPrimitiveType(), getIROpCode(opId));

		for(BaseNode n : getChildren()) {
			Expression ir = (Expression) n.checkIR(Expression.class);
			op.addOperand(ir);
		}

		return op;
	}
}
