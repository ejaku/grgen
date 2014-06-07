/*
 * GrGen: graph rewrite generator tool -- release GrGen.NET 4.4
 * Copyright (C) 2003-2014 Universitaet Karlsruhe, Institut fuer Programmstrukturen und Datenorganisation, LS Goos; and free programmers
 * licensed under LGPL v3 (see LICENSE.txt included in the packaging of this file)
 * www.grgen.net
 */

/**
 * @author Edgar Jakumeit
 */

package de.unika.ipd.grgen.ast.containers;

import java.util.Collection;
import java.util.Vector;

import de.unika.ipd.grgen.ast.*;
import de.unika.ipd.grgen.ast.exprevals.*;
import de.unika.ipd.grgen.ast.util.MemberResolver;
import de.unika.ipd.grgen.ir.IR;
import de.unika.ipd.grgen.ir.Entity;
import de.unika.ipd.grgen.ir.containers.DequeInit;
import de.unika.ipd.grgen.ir.containers.DequeItem;
import de.unika.ipd.grgen.ir.containers.DequeType;
import de.unika.ipd.grgen.parser.Coords;

//TODO: there's a lot of code which could be handled in a common way regarding the containers set|map|array|deque
//should be unified in abstract base classes and algorithms working on them

public class DequeInitNode extends ExprNode
{
	static {
		setName(DequeInitNode.class, "deque init");
	}

	private CollectNode<DequeItemNode> dequeItems = new CollectNode<DequeItemNode>();

	// if deque init node is used in model, for member init
	//     then lhs != null, dequeType == null
	// if deque init node is used in actions, for anonymous const deque with specified type
	//     then lhs == null, dequeType != null -- adjust type of deque items to this type
	// if qeque init node is used in actions, for anonymous const deque without specified type
	//     then lhs == null, dequeType == null -- determine deque type from first item, all items must be exactly of this type
	private BaseNode lhsUnresolved;
	private DeclNode lhs;
	private DequeTypeNode dequeType;

	public DequeInitNode(Coords coords, IdentNode member, DequeTypeNode dequeType) {
		super(coords);

		if(member!=null) {
			lhsUnresolved = becomeParent(member);
		} else {
			this.dequeType = dequeType;
		}
	}

	@Override
	public Collection<? extends BaseNode> getChildren() {
		Vector<BaseNode> children = new Vector<BaseNode>();
		children.add(dequeItems);
		return children;
	}

	@Override
	public Collection<String> getChildrenNames() {
		Vector<String> childrenNames = new Vector<String>();
		childrenNames.add("dequeItems");
		return childrenNames;
	}

	public void addDequeItem(DequeItemNode item) {
		dequeItems.addChild(item);
	}

	private static final MemberResolver<DeclNode> lhsResolver = new MemberResolver<DeclNode>();

	@Override
	protected boolean resolveLocal() {
		if(lhsUnresolved!=null) {
			if(!lhsResolver.resolve(lhsUnresolved)) return false;
			lhs = lhsResolver.getResult(DeclNode.class);
			return lhsResolver.finish();
		} else if(dequeType!=null) {
			return dequeType.resolve();
		} else {
			return true;
		}
	}

	@Override
	protected boolean checkLocal() {
		boolean success = true;

		DequeTypeNode dequeType;
		if(lhs!=null) {
			TypeNode type = lhs.getDeclType();
			assert type instanceof DequeTypeNode: "Lhs should be a Deque<Value>";
			dequeType = (DequeTypeNode) type;
		} else if(this.dequeType!=null) {
			dequeType = this.dequeType;
		} else {
			TypeNode dequeTypeNode = getDequeType();
			if(dequeTypeNode instanceof DequeTypeNode) {
				dequeType = (DequeTypeNode)dequeTypeNode;
			} else {
				return false;
			}
		}

		for(DequeItemNode item : dequeItems.getChildren()) {
			if(item.valueExpr.getType() != dequeType.valueType) {
				if(this.dequeType!=null) {
					ExprNode oldValueExpr = item.valueExpr;
					item.valueExpr = item.valueExpr.adjustType(dequeType.valueType, getCoords());
					item.switchParenthoodOfItem(oldValueExpr, item.valueExpr);
					if(item.valueExpr == ConstNode.getInvalid()) {
						success = false;
						item.valueExpr.reportError("Value type \"" + oldValueExpr.getType()
								+ "\" of initializer doesn't fit to value type \""
								+ dequeType.valueType + "\" of deque.");
					}
				} else {
					success = false;
					item.valueExpr.reportError("Value type \"" + item.valueExpr.getType()
							+ "\" of initializer doesn't fit to value type \""
							+ dequeType.valueType + "\" of deque (all items must be of exactly the same type).");
				}
			}
		}

		if(lhs==null && this.dequeType==null) {
			this.dequeType = dequeType;
		}

		if(!isConstant() && lhs!=null) {
			reportError("Only constant items allowed in deque initialization in model");
			success = false;
		}

		return success;
	}

	protected TypeNode getDequeType() {
		TypeNode itemTypeNode = dequeItems.getChildren().iterator().next().valueExpr.getType();
		IdentNode itemTypeIdent = ((DeclaredTypeNode)itemTypeNode).getIdentNode();
		return DequeTypeNode.getDequeType(itemTypeIdent);
	}

	/**
	 * Checks whether the set only contains constants.
	 * @return True, if all set items are constant.
	 */
	protected boolean isConstant() {
		for(DequeItemNode item : dequeItems.getChildren()) {
			if(!(item.valueExpr instanceof ConstNode || isEnumValue(item.valueExpr)))
				return false;
		}
		return true;
	}

	protected boolean isEnumValue(ExprNode expr) {
		if(!(expr instanceof DeclExprNode))
			return false;
		if(!(((DeclExprNode)expr).isEnumValue()))
			return false;
		return true;
	}

	protected boolean contains(ConstNode node) {
		for(DequeItemNode item : dequeItems.getChildren()) {
			if(item.valueExpr instanceof ConstNode) {
				ConstNode itemConst = (ConstNode) item.valueExpr;
				if(node.getValue().equals(itemConst.getValue()))
					return true;
			}
		}
		return false;
	}

	@Override
	public TypeNode getType() {
		assert(isResolved());
		if(lhs!=null) {
			TypeNode type = lhs.getDeclType();
			return (SetTypeNode) type;
		} else if(dequeType!=null) {
			return dequeType;
		} else {
			return getDequeType();
		}
	}

	protected CollectNode<DequeItemNode> getItems() {
		return dequeItems;
	}

	@Override
	protected IR constructIR() {
		Vector<DequeItem> items = new Vector<DequeItem>();
		for(DequeItemNode item : dequeItems.getChildren()) {
			items.add(item.getDequeItem());
		}
		Entity member = lhs!=null ? lhs.getEntity() : null;
		DequeType type = dequeType!=null ? dequeType.checkIR(DequeType.class) : null;
		return new DequeInit(items, member, type, isConstant());
	}

	public DequeInit getDequeInit() {
		return checkIR(DequeInit.class);
	}

	public static String getUseStr() {
		return "deque initialization";
	}
}
