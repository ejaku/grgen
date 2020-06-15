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
import de.unika.ipd.grgen.ir.type.Type;
import de.unika.ipd.grgen.ir.type.container.ArrayType;
import de.unika.ipd.grgen.ir.type.container.DequeType;
import de.unika.ipd.grgen.ir.type.container.MapType;
import de.unika.ipd.grgen.ir.expr.Expression;
import de.unika.ipd.grgen.ir.expr.IndexedAccessExpr;
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

	/** maps an operator to an IR opcode, filled with code beyond */
	private static Map<OperatorDeclNode.Operator, Operator.OperatorCode> irOpCodeMap =
			new HashMap<OperatorDeclNode.Operator, Operator.OperatorCode>();

	static {
		assocOpCode(OperatorDeclNode.Operator.COND, Operator.OperatorCode.COND);
		assocOpCode(OperatorDeclNode.Operator.LOG_OR, Operator.OperatorCode.LOG_OR);
		assocOpCode(OperatorDeclNode.Operator.LOG_AND, Operator.OperatorCode.LOG_AND);
		assocOpCode(OperatorDeclNode.Operator.BIT_OR, Operator.OperatorCode.BIT_OR);
		assocOpCode(OperatorDeclNode.Operator.BIT_XOR, Operator.OperatorCode.BIT_XOR);
		assocOpCode(OperatorDeclNode.Operator.BIT_AND, Operator.OperatorCode.BIT_AND);
		assocOpCode(OperatorDeclNode.Operator.EQ, Operator.OperatorCode.EQ);
		assocOpCode(OperatorDeclNode.Operator.NE, Operator.OperatorCode.NE);
		assocOpCode(OperatorDeclNode.Operator.SE, Operator.OperatorCode.SE);
		assocOpCode(OperatorDeclNode.Operator.LT, Operator.OperatorCode.LT);
		assocOpCode(OperatorDeclNode.Operator.LE, Operator.OperatorCode.LE);
		assocOpCode(OperatorDeclNode.Operator.GT, Operator.OperatorCode.GT);
		assocOpCode(OperatorDeclNode.Operator.GE, Operator.OperatorCode.GE);
		assocOpCode(OperatorDeclNode.Operator.SHL, Operator.OperatorCode.SHL);
		assocOpCode(OperatorDeclNode.Operator.SHR, Operator.OperatorCode.SHR);
		assocOpCode(OperatorDeclNode.Operator.BIT_SHR, Operator.OperatorCode.BIT_SHR);
		assocOpCode(OperatorDeclNode.Operator.ADD, Operator.OperatorCode.ADD);
		assocOpCode(OperatorDeclNode.Operator.SUB, Operator.OperatorCode.SUB);
		assocOpCode(OperatorDeclNode.Operator.MUL, Operator.OperatorCode.MUL);
		assocOpCode(OperatorDeclNode.Operator.DIV, Operator.OperatorCode.DIV);
		assocOpCode(OperatorDeclNode.Operator.MOD, Operator.OperatorCode.MOD);
		assocOpCode(OperatorDeclNode.Operator.LOG_NOT, Operator.OperatorCode.LOG_NOT);
		assocOpCode(OperatorDeclNode.Operator.BIT_NOT, Operator.OperatorCode.BIT_NOT);
		assocOpCode(OperatorDeclNode.Operator.NEG, Operator.OperatorCode.NEG);
		assocOpCode(OperatorDeclNode.Operator.IN, Operator.OperatorCode.IN);
		assocOpCode(OperatorDeclNode.Operator.EXCEPT, Operator.OperatorCode.EXCEPT);
	}

	private QualIdentNode target = null; // if !null it's a set/map union/except which is to be broken up

	/**
	 * @param coords Source code coordinates.
	 * @param opId ID of the operator.
	 */
	public ArithmeticOperatorNode(Coords coords, OperatorDeclNode.Operator operator)
	{
		super(coords, operator);
	}

	public ArithmeticOperatorNode(Coords coords, OperatorDeclNode.Operator operator, ExprNode op1, ExprNode op2)
	{
		super(coords, operator);
		children.add(op1);
		children.add(op2);
	}

	/** returns children of this node */
	@Override
	public Vector<ExprNode> getChildren()
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

		return getOperatorDecl().evaluate(this, args);
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

		if(getOperatorDecl().getOperator() == OperatorDeclNode.Operator.INDEX) {
			Expression texp = children.get(0).checkIR(Expression.class);
			Type type = texp.getType();
			Type accessedType;
			if(type instanceof MapType)
				accessedType = ((MapType)type).getValueType();
			else if(type instanceof DequeType)
				accessedType = ((DequeType)type).getValueType();
			else if(type instanceof ArrayType)
				accessedType = ((ArrayType)type).getValueType();
			else
				accessedType = type; // assuming untypedType
			return new IndexedAccessExpr(texp,
					children.get(1).checkIR(Expression.class), accessedType);
		}

		DeclaredTypeNode type = (DeclaredTypeNode)getType();
		Operator op = new Operator(type.getType(), getIROpCode(getOperator()));

		for(ExprNode child : children) {
			Expression ir = child.checkIR(Expression.class);
			op.addOperand(ir);
		}

		return op;
	}

	private EvalStatement replaceSetMapOrExceptByAddRemove(Qualification qual,
			EvalStatement previous, EvalStatement first)
	{
		if(getOperatorDecl().getOperator() == OperatorDeclNode.Operator.BIT_OR) {
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

	private static void assocOpCode(OperatorDeclNode.Operator operator, Operator.OperatorCode opcode)
	{
		irOpCodeMap.put(operator, opcode);
	}

	/** Maps an operator ID to an IR opcode. */
	private static Operator.OperatorCode getIROpCode(OperatorDeclNode.Operator operator)
	{
		return irOpCodeMap.get(operator);
	}

	@Override
	public String toString()
	{
		return OperatorDeclNode.getName(getOperator());
	}
}
