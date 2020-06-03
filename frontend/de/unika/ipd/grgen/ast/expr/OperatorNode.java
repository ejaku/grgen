/*
 * GrGen: graph rewrite generator tool -- release GrGen.NET 5.0
 * Copyright (C) 2003-2020 Universitaet Karlsruhe, Institut fuer Programmstrukturen und Datenorganisation, LS Goos; and free programmers
 * licensed under LGPL v3 (see LICENSE.txt included in the packaging of this file)
 * www.grgen.net
 */

/**
 * @author Sebastian Hack
 */

package de.unika.ipd.grgen.ast.expr;

import java.util.Vector;

import de.unika.ipd.grgen.ast.decl.executable.OperatorDeclNode;
import de.unika.ipd.grgen.ast.model.type.EdgeTypeNode;
import de.unika.ipd.grgen.ast.model.type.ExternalTypeNode;
import de.unika.ipd.grgen.ast.model.type.NodeTypeNode;
import de.unika.ipd.grgen.ast.type.TypeNode;
import de.unika.ipd.grgen.ast.type.basic.BasicTypeNode;
import de.unika.ipd.grgen.ast.type.basic.ByteTypeNode;
import de.unika.ipd.grgen.ast.type.basic.ShortTypeNode;
import de.unika.ipd.grgen.parser.Coords;

/**
 * Expression operators.
 */
public abstract class OperatorNode extends ExprNode
{
	/** The ID of the operator. */
	private int opId;

	/** The corresponding operator declaration. */
	private OperatorDeclNode operator;

	public Vector<ExprNode> children = new Vector<ExprNode>();

	/**
	 * Make a new operator node.
	 * @param coords The source coordinates of that node.
	 * @param opId The operator ID.
	 */
	public OperatorNode(Coords coords, int opId)
	{
		super(coords);
		this.opId = opId;
	}

	public void addChild(ExprNode n)
	{
		becomeParent(n);
		children.add(n);
	}

	/** @see de.unika.ipd.grgen.ast.BaseNode#checkLocal() */
	@Override
	protected boolean checkLocal()
	{
		boolean res = true;
		TypeNode type = getType();
		int arity = OperatorDeclNode.getArity(opId);

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
	 * operands' types (this is done via {@link Operator#getNearestOperator(int, TypeNode[])}).
	 * If no such operator is found, an error message is reported.
	 * @return The proper operator for this node, <code>null</code> otherwise.
	 */
	private OperatorDeclNode computeOperator()
	{
		OperatorDeclNode operator = null;
		Vector<TypeNode> argTypes = new Vector<TypeNode>();

		for(int i = 0; i < children.size(); i++) {
			ExprNode op = children.get(i);
			TypeNode type = op.getType();
			if(type instanceof NodeTypeNode || type instanceof EdgeTypeNode)
				type = BasicTypeNode.typeType;
			if(type instanceof ExternalTypeNode && children.size() < 3) // keep real ext type for cond
				type = BasicTypeNode.typeType;
			if(type instanceof ByteTypeNode || type instanceof ShortTypeNode)
				if(children.size() < 3)
					type = BasicTypeNode.intType;
			argTypes.add(type);
		}

		operator = OperatorDeclNode.getNearestOperator(opId, argTypes);
		if(!operator.isValid()) {
			StringBuffer params = new StringBuffer();
			boolean errorReported = false;

			params.append('(');
			for(int i = 0; i < children.size(); i++) {
				if(argTypes.get(i).isEqual(BasicTypeNode.errorType)) {
					errorReported = true;
				} else {
					params.append((i > 0 ? ", " : "") + argTypes.get(i).toString());
				}
			}
			params.append(')');

			if(!errorReported) {
				reportError("No such operator " + OperatorDeclNode.getName(opId) + params);
			}
		} else {
			// Insert implicit type casts for the arguments that need them.
			TypeNode[] opTypes = operator.getOperandTypes();
			assert(opTypes.length == argTypes.size());
			for(int i = 0; i < argTypes.size(); i++) {
				if(!argTypes.get(i).isEqual(opTypes[i])) {
					ExprNode child = children.get(i);
					ExprNode adjusted = child.adjustType(opTypes[i]);
					becomeParent(adjusted);
					children.set(i, adjusted);
				}
			}
		}

		return operator;
	}

	public final OperatorDeclNode getOperator()
	{
		if(operator == null) {
			operator = computeOperator();
		}

		return operator;
	}

	public final int getOpId()
	{
		return opId;
	}

	/**
	 * Get the type of this expression.
	 * @see de.unika.ipd.grgen.ast.expr.ExprNode#getType()
	 * If a proper operator for this node can be found, the type of this
	 * node is the result type of the operator, else it's the error type
	 * {@link BasicTypeNode#errorType}.
	 */
	@Override
	public TypeNode getType()
	{
		return getOperator().getResultType();
	}
}
