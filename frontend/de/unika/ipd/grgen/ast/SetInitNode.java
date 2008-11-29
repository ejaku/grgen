/*
 * GrGen: graph rewrite generator tool -- release GrGen.NET 2.0
 * Copyright (C) 2008 Universitaet Karlsruhe, Institut fuer Programmstrukturen und Datenorganisation, LS Goos
 * licensed under GPL v3 (see LICENSE.txt included in the packaging of this file)
 */

/**
 * @author Edgar Jakumeit
 * @version $Id: MapInitNode.java 22956 2008-10-16 20:35:04Z buchwald $
 */

package de.unika.ipd.grgen.ast;

import java.util.Collection;
import java.util.Vector;

import de.unika.ipd.grgen.ast.util.MemberResolver;
import de.unika.ipd.grgen.ir.IR;
import de.unika.ipd.grgen.ir.Entity;
import de.unika.ipd.grgen.ir.SetInit;
import de.unika.ipd.grgen.ir.SetItem;
import de.unika.ipd.grgen.ir.SetType;
import de.unika.ipd.grgen.parser.Coords;

public class SetInitNode extends ExprNode
{
	static {
		setName(SetInitNode.class, "set init");
	}

	CollectNode<SetItemNode> setItems = new CollectNode<SetItemNode>();

	// if set init node is used in model, for member init then lhs != null
	// if set init node is used in actions, for anonymous const set then setType != null
	BaseNode lhsUnresolved;
	DeclNode lhs;
	SetTypeNode setType;

	public SetInitNode(Coords coords, IdentNode member, SetTypeNode setType) {
		super(coords);

		if(member!=null) {
			lhsUnresolved = becomeParent(member);
		} else { // mapType!=null
			this.setType = setType;
		}
	}

	public Collection<? extends BaseNode> getChildren() {
		Vector<BaseNode> children = new Vector<BaseNode>();
		children.add(setItems);
		return children;
	}

	public Collection<String> getChildrenNames() {
		Vector<String> childrenNames = new Vector<String>();
		childrenNames.add("setItems");
		return childrenNames;
	}

	public void addSetItem(SetItemNode item) {
		setItems.addChild(item);
	}

	private static final MemberResolver<DeclNode> lhsResolver = new MemberResolver<DeclNode>();

	protected boolean resolveLocal() {
		if(lhsUnresolved!=null) {
			if(!lhsResolver.resolve(lhsUnresolved)) return false;
			lhs = lhsResolver.getResult(DeclNode.class);
			return lhsResolver.finish();
		} else {
			return setType.resolve();
		}
	}

	protected boolean checkLocal() {
		boolean success = true;

		SetTypeNode setType;
		if(lhs!=null) {
			TypeNode type = lhs.getDeclType();
			assert type instanceof SetTypeNode: "Lhs should be a Set<Value>";
			setType = (SetTypeNode) type;
		} else {
			setType = this.setType;
		}

		for(SetItemNode item : setItems.getChildren()) {
			if (item.valueExpr.getType() != setType.valueType) {
				ExprNode oldValueExpr = item.valueExpr;
				item.valueExpr = item.valueExpr.adjustType(setType.valueType, getCoords());
				item.switchParenthood(oldValueExpr, item.valueExpr);
				if(item.valueExpr == ConstNode.getInvalid()) {
					success = false;
					item.valueExpr.reportError("Value type \"" + oldValueExpr.getType()
							+ "\" of initializer doesn't fit to value type \""
							+ setType.valueType + "\" of set.");
				}
			}
		}
		
		if(!isConstant() && lhs!=null) {
			reportError("Only constant items allowed in set initialization in model");
			success = false;
		}

		return success;
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
	
	public boolean isEnumValue(ExprNode expr) {
		if(!(expr instanceof DeclExprNode))
			return false;
		if(!(((DeclExprNode)expr).declUnresolved instanceof EnumExprNode))
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

	public TypeNode getType() {
		if(lhs!=null) {
			TypeNode type = lhs.getDeclType();
			return (SetTypeNode) type;
		} else {
			return setType;
		}
	}
	
	public CollectNode<SetItemNode> getItems() {
		return setItems; 
	}

	protected IR constructIR() {
		Vector<SetItem> items = new Vector<SetItem>();
		for(SetItemNode item : setItems.getChildren()) {
			items.add(item.getSetItem());
		}
		Entity member = lhs!=null ? lhs.getEntity() : null;
		SetType type = setType!=null ? (SetType)setType.getIR() : null;
		return new SetInit(items, member, type, isConstant());
	}

	public SetInit getSetInit() {
		return checkIR(SetInit.class);
	}
}
