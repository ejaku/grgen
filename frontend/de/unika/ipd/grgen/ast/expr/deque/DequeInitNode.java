/*
 * GrGen: graph rewrite generator tool -- release GrGen.NET 5.0
 * Copyright (C) 2003-2020 Universitaet Karlsruhe, Institut fuer Programmstrukturen und Datenorganisation, LS Goos; and free programmers
 * licensed under LGPL v3 (see LICENSE.txt included in the packaging of this file)
 * www.grgen.net
 */

/**
 * @author Edgar Jakumeit
 */

package de.unika.ipd.grgen.ast.expr.deque;

import java.util.Collection;
import java.util.Vector;

import de.unika.ipd.grgen.ast.*;
import de.unika.ipd.grgen.ast.expr.ConstNode;
import de.unika.ipd.grgen.ast.expr.DeclExprNode;
import de.unika.ipd.grgen.ast.expr.ExprNode;
import de.unika.ipd.grgen.ast.typedecl.DequeTypeNode;
import de.unika.ipd.grgen.ast.util.MemberResolver;
import de.unika.ipd.grgen.ir.IR;
import de.unika.ipd.grgen.ir.expr.Expression;
import de.unika.ipd.grgen.ir.expr.deque.DequeInit;
import de.unika.ipd.grgen.ir.Entity;
import de.unika.ipd.grgen.ir.typedecl.DequeType;
import de.unika.ipd.grgen.parser.Coords;

//TODO: there's a lot of code which could be handled in a common way regarding the containers set|map|array|deque
//should be unified in abstract base classes and algorithms working on them

public class DequeInitNode extends ExprNode
{
	static {
		setName(DequeInitNode.class, "deque init");
	}

	private CollectNode<ExprNode> dequeItems = new CollectNode<ExprNode>();

	// if deque init node is used in model, for member init
	//     then lhs != null, dequeType == null
	// if deque init node is used in actions, for anonymous const deque with specified type
	//     then lhs == null, dequeType != null -- adjust type of deque items to this type
	private BaseNode lhsUnresolved;
	private DeclNode lhs;
	private DequeTypeNode dequeType;

	public DequeInitNode(Coords coords, IdentNode member, DequeTypeNode dequeType)
	{
		super(coords);

		if(member != null) {
			lhsUnresolved = becomeParent(member);
		} else {
			this.dequeType = dequeType;
		}
	}

	@Override
	public Collection<? extends BaseNode> getChildren()
	{
		Vector<BaseNode> children = new Vector<BaseNode>();
		children.add(dequeItems);
		return children;
	}

	@Override
	public Collection<String> getChildrenNames()
	{
		Vector<String> childrenNames = new Vector<String>();
		childrenNames.add("dequeItems");
		return childrenNames;
	}

	public void addDequeItem(ExprNode item)
	{
		dequeItems.addChild(item);
	}

	private static final MemberResolver<DeclNode> lhsResolver = new MemberResolver<DeclNode>();

	@Override
	protected boolean resolveLocal()
	{
		if(lhsUnresolved != null) {
			if(!lhsResolver.resolve(lhsUnresolved))
				return false;
			lhs = lhsResolver.getResult(DeclNode.class);
			return lhsResolver.finish();
		} else {
			if(dequeType == null)
				dequeType = createDequeType();
			return dequeType.resolve();
		}
	}

	@Override
	protected boolean checkLocal()
	{
		boolean success = true;

		DequeTypeNode dequeType;
		if(lhs != null) {
			TypeNode type = lhs.getDeclType();
			assert type instanceof DequeTypeNode : "Lhs should be a Deque<Value>";
			dequeType = (DequeTypeNode)type;
		} else {
			dequeType = this.dequeType;
		}

		for(ExprNode item : dequeItems.getChildren()) {
			if(item.getType() != dequeType.valueType) {
				if(this.dequeType != null) {
					ExprNode oldValueExpr = item;
					ExprNode newValueExpr = item.adjustType(dequeType.valueType, getCoords());
					dequeItems.replace(oldValueExpr, newValueExpr);
					if(newValueExpr == ConstNode.getInvalid()) {
						success = false;
						item.reportError("Value type \"" + oldValueExpr.getType()
								+ "\" of initializer doesn't fit to value type \"" + dequeType.valueType
								+ "\" of deque.");
					}
				} else {
					success = false;
					item.reportError("Value type \"" + item.getType()
							+ "\" of initializer doesn't fit to value type \"" + dequeType.valueType
							+ "\" of deque (all items must be of exactly the same type).");
				}
			}
		}

		if(lhs == null && this.dequeType == null) {
			this.dequeType = dequeType;
		}

		if(!isConstant() && lhs != null) {
			reportError("Only constant items allowed in deque initialization in model");
			success = false;
		}

		return success;
	}

	protected DequeTypeNode createDequeType()
	{
		TypeNode itemTypeNode = dequeItems.getChildren().iterator().next().getType();
		IdentNode itemTypeIdent = ((DeclaredTypeNode)itemTypeNode).getIdentNode();
		return new DequeTypeNode(itemTypeIdent);
	}

	/**
	 * Checks whether the set only contains constants.
	 * @return True, if all set items are constant.
	 */
	protected boolean isConstant()
	{
		for(ExprNode item : dequeItems.getChildren()) {
			if(!(item instanceof ConstNode || isEnumValue(item)))
				return false;
		}
		return true;
	}

	protected boolean isEnumValue(ExprNode expr)
	{
		if(!(expr instanceof DeclExprNode))
			return false;
		if(!(((DeclExprNode)expr).isEnumValue()))
			return false;
		return true;
	}

	protected boolean contains(ConstNode node)
	{
		for(ExprNode item : dequeItems.getChildren()) {
			if(item instanceof ConstNode) {
				ConstNode itemConst = (ConstNode)item;
				if(node.getValue().equals(itemConst.getValue()))
					return true;
			}
		}
		return false;
	}

	@Override
	public TypeNode getType()
	{
		assert(isResolved());
		if(lhs != null) {
			TypeNode type = lhs.getDeclType();
			return (DequeTypeNode)type;
		} else {
			return dequeType;
		}
	}

	protected CollectNode<ExprNode> getItems()
	{
		return dequeItems;
	}

	@Override
	protected IR constructIR()
	{
		Vector<Expression> items = new Vector<Expression>();
		for(ExprNode item : dequeItems.getChildren()) {
			items.add(item.checkIR(Expression.class));
		}
		Entity member = lhs != null ? lhs.getEntity() : null;
		DequeType type = dequeType != null ? dequeType.checkIR(DequeType.class) : null;
		return new DequeInit(items, member, type, isConstant());
	}

	public DequeInit getDequeInit()
	{
		return checkIR(DequeInit.class);
	}

	public static String getUseStr()
	{
		return "deque initialization";
	}
}
