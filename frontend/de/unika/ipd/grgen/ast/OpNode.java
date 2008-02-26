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

import java.util.Vector;

import de.unika.ipd.grgen.ir.Operator;
import de.unika.ipd.grgen.parser.Coords;

/**
 * Expression operators.
 */
public abstract class OpNode extends ExprNode
{
	/** The ID of the operator. */
	private int opId;

	/** The corresponding operator. */
	private OperatorSignature operator;

	Vector<ExprNode> children = new Vector<ExprNode>();

	/**
	 * Make a new operator node.
	 * @param coords The source coordinates of that node.
	 * @param opId The operator ID.
	 */
	public OpNode(Coords coords, int opId) {
		super(coords);
		this.opId = opId;
	}

	public void addChild(ExprNode n) {
		becomeParent(n);
		children.add(n);
	}

	/** @see de.unika.ipd.grgen.ast.BaseNode#checkLocal() */
	protected boolean checkLocal() {
		boolean res = true;
		TypeNode type = getType();
		int arity = OperatorSignature.getArity(opId);

		if(children.size() != arity) {
			reportError("Wrong operator arity: " + children.size());
			res = false;
		}

		//Here The error must have been already reported
		if ( type.isEqual(BasicTypeNode.errorType) ) {
			res = false;
		} else if(!type.isBasic() && !(arity == 3 && (type instanceof EnumTypeNode))) {
			res = false;
			reportError("Result must be a basic type not: " + getType());
		}

		return res;
	}

	/**
	 * Determine the operator that will be used with this operator node.
	 * The method gets the operand types of this node and determines the
	 * operator, that will need the least implicit type casts using the
	 * operands' types (this is done via {@link Operator#getNearest(int, TypeNode[])}).
	 * If no such operator is found, an error message is reported.
	 * @return The proper operator for this node, <code>null</code> otherwise.
	 */
	private OperatorSignature computeOperator() {
		OperatorSignature operator = null;
		int n = children.size();
		TypeNode[] argTypes = new TypeNode[n];

		for(int i = 0; i < n; i++) {
			ExprNode op = (ExprNode) children.get(i);
			argTypes[i] = op.getType();
		}

		operator = OperatorSignature.getNearest(opId, argTypes);
		if(!operator.isValid())	{
			StringBuffer params = new StringBuffer();
			boolean errorReported = false;

			params.append('(');
			for(int i = 0; i < n; i++) {
				if (argTypes[i].isEqual(BasicTypeNode.errorType)) {
					errorReported = true;
				} else {
					params.append((i > 0 ? ", " : "") + argTypes[i].toString());
				}
			}
			params.append(')');

			if (!errorReported) {
				reportError("No such operator " + OperatorSignature.getName(opId) + params);
			}
		} else {
			// Insert implicit type casts for the arguments that need them.
			TypeNode[] opTypes = operator.getOperandTypes();
			assert (opTypes.length == argTypes.length);
			for(int i = 0; i < argTypes.length; i++) {
				if(!argTypes[i].isEqual(opTypes[i])) {
					ExprNode child = (ExprNode) children.get(i);
					ExprNode adjusted = child.adjustType(opTypes[i]);
					becomeParent(adjusted);
					children.set(i, adjusted);
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

	protected final int getOpId() {
		return opId;
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
}
