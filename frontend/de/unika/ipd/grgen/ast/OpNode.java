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
package de.unika.ipd.grgen.ast;

import java.util.Vector;

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

	protected Vector<ExprNode> children = new Vector<ExprNode>();

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
	@Override
	protected boolean checkLocal() {
		boolean res = true;
		TypeNode type = getType();
		int arity = OperatorSignature.getArity(opId);

		if(children.size() != arity) {
			reportError("Wrong operator arity: " + children.size());
			res = false;
		}

		// Here the error must have been already reported
		if(type.isEqual(BasicTypeNode.errorType))
			res = false;

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
			ExprNode op = children.get(i);
			TypeNode type = op.getType();
			if(type instanceof InheritanceTypeNode) type = OperatorSignature.TYPE;
			argTypes[i] = type;
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
					ExprNode child = children.get(i);
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
	@Override
	public TypeNode getType() {
		return getOperator().getResultType();
	}
}
