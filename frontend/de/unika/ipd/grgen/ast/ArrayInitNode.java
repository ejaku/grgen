/*
 * GrGen: graph rewrite generator tool -- release GrGen.NET 3.0
 * Copyright (C) 2003-2011 Universitaet Karlsruhe, Institut fuer Programmstrukturen und Datenorganisation, LS Goos
 * licensed under LGPL v3 (see LICENSE.txt included in the packaging of this file)
 * www.grgen.net
 */

/**
 * @author Edgar Jakumeit
 */

package de.unika.ipd.grgen.ast;

import java.util.Collection;
import java.util.Vector;

import de.unika.ipd.grgen.ast.util.MemberResolver;
import de.unika.ipd.grgen.ir.IR;
import de.unika.ipd.grgen.ir.Entity;
import de.unika.ipd.grgen.ir.ArrayInit;
import de.unika.ipd.grgen.ir.ArrayItem;
import de.unika.ipd.grgen.ir.ArrayType;
import de.unika.ipd.grgen.parser.Coords;

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

	public ArrayInitNode(Coords coords, IdentNode member, ArrayTypeNode setType) {
		super(coords);

		if(member!=null) {
			lhsUnresolved = becomeParent(member);
		} else {
			this.arrayType = setType;
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
					item.switchParenthood(oldValueExpr, item.valueExpr);
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
		if(!(itemTypeNode instanceof DeclaredTypeNode)) {
			reportError("Array items have to be of basic or enum type");
			return BasicTypeNode.errorType;
		}
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
		if(!(((DeclExprNode)expr).declUnresolved instanceof EnumExprNode))
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
		ArrayType type = arrayType!=null ? (ArrayType)arrayType.getIR() : null;
		return new ArrayInit(items, member, type, isConstant());
	}

	protected ArrayInit getArrayInit() {
		return checkIR(ArrayInit.class);
	}

	public static String getUseStr() {
		return "array initialization";
	}
}
