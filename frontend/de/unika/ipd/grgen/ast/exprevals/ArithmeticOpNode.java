/*
 * GrGen: graph rewrite generator tool -- release GrGen.NET 4.5
 * Copyright (C) 2003-2020 Universitaet Karlsruhe, Institut fuer Programmstrukturen und Datenorganisation, LS Goos; and free programmers
 * licensed under LGPL v3 (see LICENSE.txt included in the packaging of this file)
 * www.grgen.net
 */

/**
 * @author Sebastian Hack
 */

package de.unika.ipd.grgen.ast.exprevals;

import java.util.Collection;
import java.util.HashMap;
import java.util.Map;
import java.util.Vector;

import de.unika.ipd.grgen.ast.*;
import de.unika.ipd.grgen.ast.containers.*;
import de.unika.ipd.grgen.ir.exprevals.EvalStatement;
import de.unika.ipd.grgen.ir.exprevals.Expression;
import de.unika.ipd.grgen.ir.IR;
import de.unika.ipd.grgen.ir.containers.MapAddItem;
import de.unika.ipd.grgen.ir.containers.MapRemoveItem;
import de.unika.ipd.grgen.ir.exprevals.Operator;
import de.unika.ipd.grgen.ir.exprevals.Qualification;
import de.unika.ipd.grgen.ir.containers.SetAddItem;
import de.unika.ipd.grgen.ir.containers.SetRemoveItem;
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
		assocOpCode(OperatorSignature.SE, Operator.SE);
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
		assocOpCode(OperatorSignature.IN, Operator.IN);
		assocOpCode(OperatorSignature.EXCEPT, Operator.EXCEPT);
	}

	private QualIdentNode target = null; // if !null it's a set/map union/except which is to be broken up

	/**
	 * @param coords Source code coordinates.
	 * @param opId ID of the operator.
	 */
	public ArithmeticOpNode(Coords coords, int opId) {
		super(coords, opId);
	}

	public ArithmeticOpNode(Coords coords, int opId, ExprNode op1, ExprNode op2) {
		super(coords, opId);
		children.add(op1);
		children.add(op2);
	}

	/** returns children of this node */
	@Override
	public Collection<ExprNode> getChildren() {
		return children;
	}

	/** returns names of the children, same order as in getChildren */
	@Override
	public Collection<String> getChildrenNames() {
		Vector<String> childrenNames = new Vector<String>();
		// nameless children
		return childrenNames;
	}

	/** @see de.unika.ipd.grgen.ast.BaseNode#resolveLocal() */
	@Override
	protected boolean resolveLocal() {
		return true;
	}


	/** @see de.unika.ipd.grgen.ast.ExprNode#evaluate() */
	@Override
	public ExprNode evaluate() {
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
	protected void markToBreakUpIntoStateChangingOperations(QualIdentNode target) {
		this.target = target;
	}

	/** @see de.unika.ipd.grgen.ast.BaseNode#constructIR() */
	@Override
	protected IR constructIR() {
		if(target!=null) {
			Qualification qual = target.checkIR(Qualification.class);
			EvalStatement previous = null;
			EvalStatement first = null;
			if(children.get(0).getIR() instanceof EvalStatement) {
				first = children.get(0).checkIR(EvalStatement.class);
				previous = first;
				while(previous.getNext()!=null) {
					previous = previous.getNext();
				}
			}
			if(getOperator().getOpId()==OperatorSignature.BIT_OR) {
				if(children.get(1).getType() instanceof SetTypeNode) {
					SetInitNode initNode = (SetInitNode)children.get(1);
					for(SetItemNode item : initNode.getItems().getChildren()) {
						SetAddItem addItem = new SetAddItem(qual,
								item.valueExpr.checkIR(Expression.class));
						if(first==null) first = addItem;
						if(previous!=null) previous.setNext(addItem);
						previous = addItem;
					}
				}
				else { //if(children.get(1).getType() instanceof MapTypeNode)
					MapInitNode initNode = (MapInitNode)children.get(1);
					for(MapItemNode item : initNode.getItems().getChildren()) {
						MapAddItem addItem = new MapAddItem(qual,
								item.keyExpr.checkIR(Expression.class),
								item.valueExpr.checkIR(Expression.class));
						if(first==null) first = addItem;
						if(previous!=null) previous.setNext(addItem);
						previous = addItem;
					}
				}
			}
			else { //if(getOperator().getOpId()==OperatorSignature.EXCEPT) // only BIT_OR/EXCEPT are marked
				if(children.get(1).getType() instanceof SetTypeNode) {
					SetInitNode initNode = (SetInitNode)children.get(1);
					if(children.get(0).getType() instanceof MapTypeNode) { // handle map \ set
						for(SetItemNode item : initNode.getItems().getChildren()) {
							MapRemoveItem remItem = new MapRemoveItem(qual,
									item.valueExpr.checkIR(Expression.class));
							if(first==null) first = remItem;
							if(previous!=null) previous.setNext(remItem);
							previous = remItem;
						}
					} else { // handle normal case set \ set
						for(SetItemNode item : initNode.getItems().getChildren()) {
							SetRemoveItem remItem = new SetRemoveItem(qual,
									item.valueExpr.checkIR(Expression.class));
							if(first==null) first = remItem;
							if(previous!=null) previous.setNext(remItem);
							previous = remItem;
						}
					}
				}
				else { //if(children.get(1).getType() instanceof MapTypeNode)
					MapInitNode initNode = (MapInitNode)children.get(1);
					for(MapItemNode item : initNode.getItems().getChildren()) {
						MapRemoveItem remItem = new MapRemoveItem(qual,
								item.keyExpr.checkIR(Expression.class));
						if(first==null) first = remItem;
						if(previous!=null) previous.setNext(remItem);
						previous = remItem;
					}
				}
			}

			return first;
		}

		DeclaredTypeNode type = (DeclaredTypeNode) getType();
		Operator op = new Operator(type.getType(), getIROpCode(getOpId()));

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

	@Override
	public String toString() {
		return OperatorSignature.getName(getOpId());
	}
}
