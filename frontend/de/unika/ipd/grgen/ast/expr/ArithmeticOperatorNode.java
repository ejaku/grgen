/*
 * GrGen: graph rewrite generator tool -- release GrGen.NET 8.1
 * Copyright (C) 2003-2026 Universitaet Karlsruhe, Institut fuer Programmstrukturen und Datenorganisation, LS Goos; and free programmers
 * licensed under LGPL v3, some components/parts use different licenses (see LICENSE.txt included in the packaging of this file)
 * www.grgen.de / www.grgen.net
 */

/**
 * @author Sebastian Hack
 */

package de.unika.ipd.grgen.ast.expr;

import java.util.Collection;
import java.util.HashMap;
import java.util.Map;
import java.util.Vector;

import de.unika.ipd.grgen.ast.BaseNode;
import de.unika.ipd.grgen.ast.decl.executable.Operator;
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
import de.unika.ipd.grgen.ir.expr.OperatorCode;
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
	private static Map<Operator, OperatorCode> irOpCodeMap =
			new HashMap<Operator, OperatorCode>();

	static {
		assocOpCode(Operator.COND, OperatorCode.COND);
		assocOpCode(Operator.LOG_OR, OperatorCode.LOG_OR);
		assocOpCode(Operator.LOG_AND, OperatorCode.LOG_AND);
		assocOpCode(Operator.BIT_OR, OperatorCode.BIT_OR);
		assocOpCode(Operator.BIT_XOR, OperatorCode.BIT_XOR);
		assocOpCode(Operator.BIT_AND, OperatorCode.BIT_AND);
		assocOpCode(Operator.EQ, OperatorCode.EQ);
		assocOpCode(Operator.NE, OperatorCode.NE);
		assocOpCode(Operator.SE, OperatorCode.SE);
		assocOpCode(Operator.LT, OperatorCode.LT);
		assocOpCode(Operator.LE, OperatorCode.LE);
		assocOpCode(Operator.GT, OperatorCode.GT);
		assocOpCode(Operator.GE, OperatorCode.GE);
		assocOpCode(Operator.SHL, OperatorCode.SHL);
		assocOpCode(Operator.SHR, OperatorCode.SHR);
		assocOpCode(Operator.BIT_SHR, OperatorCode.BIT_SHR);
		assocOpCode(Operator.ADD, OperatorCode.ADD);
		assocOpCode(Operator.SUB, OperatorCode.SUB);
		assocOpCode(Operator.MUL, OperatorCode.MUL);
		assocOpCode(Operator.DIV, OperatorCode.DIV);
		assocOpCode(Operator.MOD, OperatorCode.MOD);
		assocOpCode(Operator.LOG_NOT, OperatorCode.LOG_NOT);
		assocOpCode(Operator.BIT_NOT, OperatorCode.BIT_NOT);
		assocOpCode(Operator.NEG, OperatorCode.NEG);
		assocOpCode(Operator.IN, OperatorCode.IN);
		assocOpCode(Operator.EXCEPT, OperatorCode.EXCEPT);
	}

	private QualIdentNode target = null; // if !null it's a set/map union/except which is to be broken up

	/**
	 * @param coords Source code coordinates.
	 * @param opId ID of the operator.
	 */
	public ArithmeticOperatorNode(Coords coords, Operator operator)
	{
		super(coords, operator);
	}

	public ArithmeticOperatorNode(Coords coords, Operator operator, ExprNode op1, ExprNode op2)
	{
		super(coords, operator);
		children.add(op1);
		children.add(op2);
	}

	/** returns children of this node */
	@Override
	public Collection<BaseNode> getChildren()
	{
		return new Vector<BaseNode>(children);
	}

	public Collection<ExprNode> getChildrenExact()
	{
		return children;
	}

	public Vector<ExprNode> getChildrenAsVector()
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
			
			first = replaceIRSetMapOrExceptByIRAddRemove(qual, previous, first);

			return first;
		}

		if(getOperatorDecl().getOperator() == Operator.INDEX) {
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
		de.unika.ipd.grgen.ir.expr.Operator op = new de.unika.ipd.grgen.ir.expr.Operator(type.getIRType(), getIROpCode(getOperator()));

		for(ExprNode child : children) {
			Expression ir = child.checkIR(Expression.class);
			op.addOperand(ir);
		}

		return op;
	}

	private EvalStatement replaceIRSetMapOrExceptByIRAddRemove(Qualification qual,
			EvalStatement previous, EvalStatement first)
	{
		if(getOperatorDecl().getOperator() == Operator.BIT_OR) {
			if(children.get(1).getType() instanceof SetTypeNode) {
				SetInitNode initNode = (SetInitNode)children.get(1);
				for(ExprNode item : initNode.getItems().getChildrenExact()) {
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
				for(ExprPairNode item : initNode.getItems().getChildrenExact()) {
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
					for(ExprNode item : initNode.getItems().getChildrenExact()) {
						MapRemoveItem remItem = new MapRemoveItem(qual,
								item.checkIR(Expression.class));
						if(first == null)
							first = remItem;
						if(previous != null)
							previous.setNext(remItem);
						previous = remItem;
					}
				} else { // handle normal case set \ set
					for(ExprNode item : initNode.getItems().getChildrenExact()) {
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
				for(ExprPairNode item : initNode.getItems().getChildrenExact()) {
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

	private static void assocOpCode(Operator operator, OperatorCode opcode)
	{
		irOpCodeMap.put(operator, opcode);
	}

	/** Maps an operator ID to an IR opcode. */
	private static OperatorCode getIROpCode(Operator operator)
	{
		return irOpCodeMap.get(operator);
	}

	@Override
	public String toString()
	{
		return OperatorDeclNode.getName(getOperator());
	}
}
