/*
 * GrGen: graph rewrite generator tool -- release GrGen.NET 4.5
 * Copyright (C) 2003-2019 Universitaet Karlsruhe, Institut fuer Programmstrukturen und Datenorganisation, LS Goos; and free programmers
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
import de.unika.ipd.grgen.ir.containers.SetInit;
import de.unika.ipd.grgen.ir.containers.SetItem;
import de.unika.ipd.grgen.ir.containers.SetType;
import de.unika.ipd.grgen.parser.Coords;

//TODO: there's a lot of code which could be handled in a common way regarding the containers set|map|array|deque
//should be unified in abstract base classes and algorithms working on them

public class SetInitNode extends ExprNode
{
	static {
		setName(SetInitNode.class, "set init");
	}

	private CollectNode<SetItemNode> setItems = new CollectNode<SetItemNode>();

	// if set init node is used in model, for member init
	//     then lhs != null, setType == null
	// if set init node is used in actions, for anonymous const set with specified type
	//     then lhs == null, setType != null -- adjust type of set items to this type
	// if set init node is used in actions, for anonymous const set without specified type
	//     then lhs == null, setType == null -- determine set type from first item, all items must be exactly of this type
	private BaseNode lhsUnresolved;
	private DeclNode lhs;
	private SetTypeNode setType;

	public SetInitNode(Coords coords, IdentNode member, SetTypeNode setType) {
		super(coords);

		if(member!=null) {
			lhsUnresolved = becomeParent(member);
		} else {
			this.setType = setType;
		}
	}

	@Override
	public Collection<? extends BaseNode> getChildren() {
		Vector<BaseNode> children = new Vector<BaseNode>();
		children.add(setItems);
		return children;
	}

	@Override
	public Collection<String> getChildrenNames() {
		Vector<String> childrenNames = new Vector<String>();
		childrenNames.add("setItems");
		return childrenNames;
	}

	public void addSetItem(SetItemNode item) {
		setItems.addChild(item);
	}

	private static final MemberResolver<DeclNode> lhsResolver = new MemberResolver<DeclNode>();

	@Override
	protected boolean resolveLocal() {
		if(lhsUnresolved!=null) {
			if(!lhsResolver.resolve(lhsUnresolved)) return false;
			lhs = lhsResolver.getResult(DeclNode.class);
			return lhsResolver.finish();
		} else if(setType!=null) {
			return setType.resolve();
		} else {
			return true;
		}
	}

	@Override
	protected boolean checkLocal() {
		boolean success = true;

		SetTypeNode setType;
		if(lhs!=null) {
			TypeNode type = lhs.getDeclType();
			assert type instanceof SetTypeNode: "Lhs should be a Set<Value>";
			setType = (SetTypeNode) type;
		} else if(this.setType!=null) {
			setType = this.setType;
		} else {
			TypeNode setTypeNode = getSetType();
			if(setTypeNode instanceof SetTypeNode) {
				setType = (SetTypeNode)setTypeNode;
			} else {
				return false;
			}
		}

		for(SetItemNode item : setItems.getChildren()) {
			if(item.valueExpr.getType() != setType.valueType) {
				if(this.setType!=null) {
					ExprNode oldValueExpr = item.valueExpr;
					item.valueExpr = item.valueExpr.adjustType(setType.valueType, getCoords());
					item.switchParenthoodOfItem(oldValueExpr, item.valueExpr);
					if(item.valueExpr == ConstNode.getInvalid()) {
						success = false;
						item.valueExpr.reportError("Value type \"" + oldValueExpr.getType()
								+ "\" of initializer doesn't fit to value type \""
								+ setType.valueType + "\" of set.");
					}
				} else {
					success = false;
					item.valueExpr.reportError("Value type \"" + item.valueExpr.getType()
							+ "\" of initializer doesn't fit to value type \""
							+ setType.valueType + "\" of set (all items must be of exactly the same type).");
				}
			}
		}

		if(lhs==null && this.setType==null) {
			this.setType = setType;
		}

		if(!isConstant() && lhs!=null) {
			reportError("Only constant items allowed in set initialization in model");
			success = false;
		}

		return success;
	}

	protected TypeNode getSetType() {
		TypeNode itemTypeNode = setItems.getChildren().iterator().next().valueExpr.getType();
		IdentNode itemTypeIdent = ((DeclaredTypeNode)itemTypeNode).getIdentNode();
		return SetTypeNode.getSetType(itemTypeIdent);
	}

	/**
	 * Checks whether the set only contains constants.
	 * @return True, if all set items are constant.
	 */
	public boolean isConstant() {
		for(SetItemNode item : setItems.getChildren()) {
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

	public boolean contains(ConstNode node) {
		for(SetItemNode item : setItems.getChildren()) {
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
		} else if(setType!=null) {
			return setType;
		} else {
			return getSetType();
		}
	}

	public CollectNode<SetItemNode> getItems() {
		return setItems;
	}

	@Override
	protected IR constructIR() {
		Vector<SetItem> items = new Vector<SetItem>();
		for(SetItemNode item : setItems.getChildren()) {
			items.add(item.getSetItem());
		}
		Entity member = lhs!=null ? lhs.getEntity() : null;
		SetType type = setType!=null ? setType.checkIR(SetType.class) : null;
		return new SetInit(items, member, type, isConstant());
	}

	public SetInit getSetInit() {
		return checkIR(SetInit.class);
	}

	public static String getUseStr() {
		return "set initialization";
	}
}
