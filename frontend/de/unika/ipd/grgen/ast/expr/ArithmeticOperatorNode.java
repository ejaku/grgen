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

import java.util.Collection;
import java.util.HashMap;
import java.util.Map;
import java.util.Vector;

import de.unika.ipd.grgen.ast.decl.executable.OperatorDeclNode;
import de.unika.ipd.grgen.ast.expr.map.MapInitNode;
import de.unika.ipd.grgen.ast.expr.set.SetInitNode;
import de.unika.ipd.grgen.ast.type.DeclaredTypeNode;
import de.unika.ipd.grgen.ast.type.container.MapTypeNode;
import de.unika.ipd.grgen.ast.type.container.SetTypeNode;
import de.unika.ipd.grgen.ir.IR;
import de.unika.ipd.grgen.ir.stmt.EvalStatement;
import de.unika.ipd.grgen.ir.stmt.map.MapAddItem;
import de.unika.ipd.grgen.ir.stmt.map.MapRemoveItem;
import de.unika.ipd.grgen.ir.stmt.set.SetAddItem;
import de.unika.ipd.grgen.ir.stmt.set.SetRemoveItem;
import de.unika.ipd.grgen.ir.expr.Expression;
import de.unika.ipd.grgen.ir.expr.Operator;
import de.unika.ipd.grgen.ir.expr.Qualification;
import de.unika.ipd.grgen.parser.Coords;

/**
 * An arithmetic operator.
 */
public class ArithmeticOperatorNode extends OperatorNode
{
	static {
		setName(ArithmeticOperatorNode.class, "arithmetic operator");
	}

	/** maps an operator id to IR opcode, filled with code beyond */
	private static Map<Integer, Integer> irOpCodeMap = new HashMap<Integer, Integer>();

	static {
		assocOpCode(OperatorDeclNode.COND, Operator.COND);
		assocOpCode(OperatorDeclNode.LOG_OR, Operator.LOG_OR);
		assocOpCode(OperatorDeclNode.LOG_AND, Operator.LOG_AND);
		assocOpCode(OperatorDeclNode.BIT_OR, Operator.BIT_OR);
		assocOpCode(OperatorDeclNode.BIT_XOR, Operator.BIT_XOR);
		assocOpCode(OperatorDeclNode.BIT_AND, Operator.BIT_AND);
		assocOpCode(OperatorDeclNode.EQ, Operator.EQ);
		assocOpCode(OperatorDeclNode.NE, Operator.NE);
		assocOpCode(OperatorDeclNode.SE, Operator.SE);
		assocOpCode(OperatorDeclNode.LT, Operator.LT);
		assocOpCode(OperatorDeclNode.LE, Operator.LE);
		assocOpCode(OperatorDeclNode.GT, Operator.GT);
		assocOpCode(OperatorDeclNode.GE, Operator.GE);
		assocOpCode(OperatorDeclNode.SHL, Operator.SHL);
		assocOpCode(OperatorDeclNode.SHR, Operator.SHR);
		assocOpCode(OperatorDeclNode.BIT_SHR, Operator.BIT_SHR);
		assocOpCode(OperatorDeclNode.ADD, Operator.ADD);
		assocOpCode(OperatorDeclNode.SUB, Operator.SUB);
		assocOpCode(OperatorDeclNode.MUL, Operator.MUL);
		assocOpCode(OperatorDeclNode.DIV, Operator.DIV);
		assocOpCode(OperatorDeclNode.MOD, Operator.MOD);
		assocOpCode(OperatorDeclNode.LOG_NOT, Operator.LOG_NOT);
		assocOpCode(OperatorDeclNode.BIT_NOT, Operator.BIT_NOT);
		assocOpCode(OperatorDeclNode.NEG, Operator.NEG);
		assocOpCode(OperatorDeclNode.IN, Operator.IN);
		assocOpCode(OperatorDeclNode.EXCEPT, Operator.EXCEPT);
	}

	private QualIdentNode target = null; // if !null it's a set/map union/except which is to be broken up

	/**
	 * @param coords Source code coordinates.
	 * @param opId ID of the operator.
	 */
	public ArithmeticOperatorNode(Coords coords, int opId)
	{
		super(coords, opId);
	}

	public ArithmeticOperatorNode(Coords coords, int opId, ExprNode op1, ExprNode op2)
	{
		super(coords, opId);
		children.add(op1);
		children.add(op2);
	}

	/** returns children of this node */
	@Override
	public Collection<ExprNode> getChildren()
	{
		return children;
	}

	/** returns names of the children, same order as in getChildren */
	@Override
	public Collection<String> getChildrenNames()
	{
		Vector<String> childrenNames = new Vector<String>();
		// nameless children
		return childrenNames;
	}

	/** @see de.unika.ipd.grgen.ast.BaseNode#resolveLocal() */
	@Override
	protected boolean resolveLocal()
	{
		return true;
	}

	/** @see de.unika.ipd.grgen.ast.expr.ExprNode#evaluate() */
	@Override
	public ExprNode evaluate()
	{
		int n = children.size();
		ExprNode[] args = new ExprNode[n];

		for(int i = 0; i < n; i++) {
			ExprNode c = children.get(i);
			args[i] = c.evaluate();
			children.set(i, args[i]);
		}

		return getOperator().evaluate(this, args);
	}

	/** mark to break set/map assignment of set/map expression up into set/map add/remove to/from target statements */
	public void markToBreakUpIntoStateChangingOperations(QualIdentNode target)
	{
		this.target = target;
	}

	/** @see de.unika.ipd.grgen.ast.BaseNode#constructIR() */
	@Override
	protected IR constructIR()
	{
		if(target != null) {
			Qualification qual = target.checkIR(Qualification.class);
			EvalStatement previous = null;
			EvalStatement first = null;
			if(children.get(0).getIR() instanceof EvalStatement) {
				first = children.get(0).checkIR(EvalStatement.class);
				previous = first;
				while(previous.getNext() != null) {
					previous = previous.getNext();
				}
			}
			
			first = replaceSetMapOrExceptByAddRemove(qual, previous, first);

			return first;
		}

		DeclaredTypeNode type = (DeclaredTypeNode)getType();
		Operator op = new Operator(type.getType(), getIROpCode(getOpId()));

		for(ExprNode child : children) {
			Expression ir = child.checkIR(Expression.class);
			op.addOperand(ir);
		}

		return op;
	}

	private EvalStatement replaceSetMapOrExceptByAddRemove(Qualification qual,
			EvalStatement previous, EvalStatement first)
	{
		if(getOperator().getOpId() == OperatorDeclNode.BIT_OR) {
			if(children.get(1).getType() instanceof SetTypeNode) {
				SetInitNode initNode = (SetInitNode)children.get(1);
				for(ExprNode item : initNode.getItems().getChildren()) {
					SetAddItem addItem = new SetAddItem(qual,
							item.checkIR(Expression.class));
					if(first == null)
						first = addItem;
					if(previous != null)
						previous.setNext(addItem);
					previous = addItem;
				}
			} else { //if(children.get(1).getType() instanceof MapTypeNode)
				MapInitNode initNode = (MapInitNode)children.get(1);
				for(ExprPairNode item : initNode.getItems().getChildren()) {
					MapAddItem addItem = new MapAddItem(qual,
							item.keyExpr.checkIR(Expression.class),
							item.valueExpr.checkIR(Expression.class));
					if(first == null)
						first = addItem;
					if(previous != null)
						previous.setNext(addItem);
					previous = addItem;
				}
			}
		} else { //if(getOperator().getOpId()==OperatorSignature.EXCEPT) // only BIT_OR/EXCEPT are marked
			if(children.get(1).getType() instanceof SetTypeNode) {
				SetInitNode initNode = (SetInitNode)children.get(1);
				if(children.get(0).getType() instanceof MapTypeNode) { // handle map \ set
					for(ExprNode item : initNode.getItems().getChildren()) {
						MapRemoveItem remItem = new MapRemoveItem(qual,
								item.checkIR(Expression.class));
						if(first == null)
							first = remItem;
						if(previous != null)
							previous.setNext(remItem);
						previous = remItem;
					}
				} else { // handle normal case set \ set
					for(ExprNode item : initNode.getItems().getChildren()) {
						SetRemoveItem remItem = new SetRemoveItem(qual,
								item.checkIR(Expression.class));
						if(first == null)
							first = remItem;
						if(previous != null)
							previous.setNext(remItem);
						previous = remItem;
					}
				}
			} else { //if(children.get(1).getType() instanceof MapTypeNode)
				MapInitNode initNode = (MapInitNode)children.get(1);
				for(ExprPairNode item : initNode.getItems().getChildren()) {
					MapRemoveItem remItem = new MapRemoveItem(qual,
							item.keyExpr.checkIR(Expression.class));
					if(first == null)
						first = remItem;
					if(previous != null)
						previous.setNext(remItem);
					previous = remItem;
				}
			}
		}
		return first;
	}

	private static void assocOpCode(int id, int opcode)
	{
		irOpCodeMap.put(new Integer(id), new Integer(opcode));
	}

	/** Maps an operator ID to an IR opcode. */
	private static int getIROpCode(int opId)
	{
		return irOpCodeMap.get(new Integer(opId)).intValue();
	}

	@Override
	public String toString()
	{
		return OperatorDeclNode.getName(getOpId());
	}
}