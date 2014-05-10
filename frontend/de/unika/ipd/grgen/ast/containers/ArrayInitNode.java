/*
 * GrGen: graph rewrite generator tool -- release GrGen.NET 4.3
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
import de.unika.ipd.grgen.ir.containers.ArrayInit;
import de.unika.ipd.grgen.ir.containers.ArrayItem;
import de.unika.ipd.grgen.ir.containers.ArrayType;
import de.unika.ipd.grgen.parser.Coords;

//TODO: there's a lot of code which could be handled in a common way regarding the containers set|map|array|deque 
//should be unified in abstract base classes and algorithms working on them

public class ArrayInitNode extends ExprNode
{
	static {
		setName(ArrayInitNode.class, "array init");
	}

	private CollectNode<ArrayItemNode> arrayItems = new CollectNode<ArrayItemNode>();

	// if array init node is used in model, for member init
	//     then lhs != null, arrayType == null
	// if array init node is used in actions, for anonymous const array with specified type
	//     then lhs == null, arrayType != null -- adjust type of array items to this type
	// if array init node is used in actions, for anonymous const array without specified type
	//     then lhs == null, arrayType == null -- determine array type from first item, all items must be exactly of this type
	private BaseNode lhsUnresolved;
	private DeclNode lhs;
	private ArrayTypeNode arrayType;

	public ArrayInitNode(Coords coords, IdentNode member, ArrayTypeNode arrayType) {
		super(coords);

		if(member!=null) {
			lhsUnresolved = becomeParent(member);
		} else {
			this.arrayType = arrayType;
		}
	}

	@Override
	public Collection<? extends BaseNode> getChildren() {
		Vector<BaseNode> children = new Vector<BaseNode>();
		children.add(arrayItems);
		return children;
	}

	@Override
	public Collection<String> getChildrenNames() {
		Vector<String> childrenNames = new Vector<String>();
		childrenNames.add("arrayItems");
		return childrenNames;
	}

	public void addArrayItem(ArrayItemNode item) {
		arrayItems.addChild(item);
	}

	private static final MemberResolver<DeclNode> lhsResolver = new MemberResolver<DeclNode>();

	@Override
	protected boolean resolveLocal() {
		if(lhsUnresolved!=null) {
			if(!lhsResolver.resolve(lhsUnresolved)) return false;
			lhs = lhsResolver.getResult(DeclNode.class);
			return lhsResolver.finish();
		} else if(arrayType!=null) {
			return arrayType.resolve();
		} else {
			return true;
		}
	}

	@Override
	protected boolean checkLocal() {
		boolean success = true;

		ArrayTypeNode arrayType;
		if(lhs!=null) {
			TypeNode type = lhs.getDeclType();
			assert type instanceof ArrayTypeNode: "Lhs should be a Array<Value>";
			arrayType = (ArrayTypeNode) type;
		} else if(this.arrayType!=null) {
			arrayType = this.arrayType;
		} else {
			TypeNode arrayTypeNode = getArrayType();
			if(arrayTypeNode instanceof ArrayTypeNode) {
				arrayType = (ArrayTypeNode)arrayTypeNode;
			} else {
				return false;
			}
		}

		for(ArrayItemNode item : arrayItems.getChildren()) {
			if(item.valueExpr.getType() != arrayType.valueType) {
				if(this.arrayType!=null) {
					ExprNode oldValueExpr = item.valueExpr;
					item.valueExpr = item.valueExpr.adjustType(arrayType.valueType, getCoords());
					item.switchParenthoodOfItem(oldValueExpr, item.valueExpr);
					if(item.valueExpr == ConstNode.getInvalid()) {
						success = false;
						item.valueExpr.reportError("Value type \"" + oldValueExpr.getType()
								+ "\" of initializer doesn't fit to value type \""
								+ arrayType.valueType + "\" of array.");
					}
				} else {
					success = false;
					item.valueExpr.reportError("Value type \"" + item.valueExpr.getType()
							+ "\" of initializer doesn't fit to value type \""
							+ arrayType.valueType + "\" of array (all items must be of exactly the same type).");
				}
			}
		}

		if(lhs==null && this.arrayType==null) {
			this.arrayType = arrayType;
		}

		if(!isConstant() && lhs!=null) {
			reportError("Only constant items allowed in array initialization in model");
			success = false;
		}

		return success;
	}

	protected TypeNode getArrayType() {
		TypeNode itemTypeNode = arrayItems.getChildren().iterator().next().valueExpr.getType();
		IdentNode itemTypeIdent = ((DeclaredTypeNode)itemTypeNode).getIdentNode();
		return ArrayTypeNode.getArrayType(itemTypeIdent);
	}

	/**
	 * Checks whether the set only contains constants.
	 * @return True, if all set items are constant.
	 */
	protected boolean isConstant() {
		for(ArrayItemNode item : arrayItems.getChildren()) {
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
		for(ArrayItemNode item : arrayItems.getChildren()) {
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
		} else if(arrayType!=null) {
			return arrayType;
		} else {
			return getArrayType();
		}
	}

	protected CollectNode<ArrayItemNode> getItems() {
		return arrayItems;
	}

	@Override
	protected IR constructIR() {
		Vector<ArrayItem> items = new Vector<ArrayItem>();
		for(ArrayItemNode item : arrayItems.getChildren()) {
			items.add(item.getArrayItem());
		}
		Entity member = lhs!=null ? lhs.getEntity() : null;
		ArrayType type = arrayType!=null ? arrayType.checkIR(ArrayType.class) : null;
		return new ArrayInit(items, member, type, isConstant());
	}

	public ArrayInit getArrayInit() {
		return checkIR(ArrayInit.class);
	}

	public static String getUseStr() {
		return "array initialization";
	}
}
