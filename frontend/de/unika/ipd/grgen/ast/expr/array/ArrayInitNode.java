/*
 * GrGen: graph rewrite generator tool -- release GrGen.NET 5.0
 * Copyright (C) 2003-2020 Universitaet Karlsruhe, Institut fuer Programmstrukturen und Datenorganisation, LS Goos; and free programmers
 * licensed under LGPL v3 (see LICENSE.txt included in the packaging of this file)
 * www.grgen.net
 */

/**
 * @author Edgar Jakumeit
 */

package de.unika.ipd.grgen.ast.expr.array;

import java.util.Collection;
import java.util.Vector;

import de.unika.ipd.grgen.ast.*;
import de.unika.ipd.grgen.ast.expr.ConstNode;
import de.unika.ipd.grgen.ast.expr.DeclExprNode;
import de.unika.ipd.grgen.ast.expr.ExprNode;
import de.unika.ipd.grgen.ast.typedecl.ArrayTypeNode;
import de.unika.ipd.grgen.ast.util.MemberResolver;
import de.unika.ipd.grgen.ir.IR;
import de.unika.ipd.grgen.ir.expr.Expression;
import de.unika.ipd.grgen.ir.expr.array.ArrayInit;
import de.unika.ipd.grgen.ir.Entity;
import de.unika.ipd.grgen.ir.typedecl.ArrayType;
import de.unika.ipd.grgen.parser.Coords;

//TODO: there's a lot of code which could be handled in a common way regarding the containers set|map|array|deque 
//should be unified in abstract base classes and algorithms working on them

public class ArrayInitNode extends ExprNode
{
	static {
		setName(ArrayInitNode.class, "array init");
	}

	private CollectNode<ExprNode> arrayItems = new CollectNode<ExprNode>();

	// if array init node is used in model, for member init
	//     then lhs != null, arrayType == null
	// if array init node is used in actions, for anonymous const array with specified type
	//     then lhs == null, arrayType != null -- adjust type of array items to this type
	private BaseNode lhsUnresolved;
	private DeclNode lhs;
	private ArrayTypeNode arrayType;

	public ArrayInitNode(Coords coords, IdentNode member, ArrayTypeNode arrayType)
	{
		super(coords);

		if(member != null) {
			lhsUnresolved = becomeParent(member);
		} else {
			this.arrayType = arrayType;
		}
	}

	@Override
	public Collection<? extends BaseNode> getChildren()
	{
		Vector<BaseNode> children = new Vector<BaseNode>();
		children.add(arrayItems);
		return children;
	}

	@Override
	public Collection<String> getChildrenNames()
	{
		Vector<String> childrenNames = new Vector<String>();
		childrenNames.add("arrayItems");
		return childrenNames;
	}

	public void addArrayItem(ExprNode item)
	{
		arrayItems.addChild(item);
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
			if(arrayType == null)
				arrayType = createArrayType();
			return arrayType.resolve();
		}
	}

	@Override
	protected boolean checkLocal()
	{
		boolean success = true;

		ArrayTypeNode arrayType;
		if(lhs != null) {
			TypeNode type = lhs.getDeclType();
			assert type instanceof ArrayTypeNode : "Lhs should be a Array<Value>";
			arrayType = (ArrayTypeNode)type;
		} else {
			arrayType = this.arrayType;
		}

		for(ExprNode item : arrayItems.getChildren()) {
			if(item.getType() != arrayType.valueType) {
				if(this.arrayType != null) {
					ExprNode oldValueExpr = item;
					ExprNode newValueExpr = item.adjustType(arrayType.valueType, getCoords());
					arrayItems.replace(oldValueExpr, newValueExpr);
					if(newValueExpr == ConstNode.getInvalid()) {
						success = false;
						item.reportError("Value type \"" + oldValueExpr.getType()
								+ "\" of initializer doesn't fit to value type \"" + arrayType.valueType
								+ "\" of array.");
					}
				} else {
					success = false;
					item.reportError("Value type \"" + item.getType()
							+ "\" of initializer doesn't fit to value type \"" + arrayType.valueType
							+ "\" of array (all items must be of exactly the same type).");
				}
			}
		}

		if(lhs == null && this.arrayType == null) {
			this.arrayType = arrayType;
		}

		if(!isConstant() && lhs != null) {
			reportError("Only constant items allowed in array initialization in model");
			success = false;
		}

		return success;
	}

	protected ArrayTypeNode createArrayType()
	{
		TypeNode itemTypeNode = arrayItems.getChildren().iterator().next().getType();
		IdentNode itemTypeIdent = ((DeclaredTypeNode)itemTypeNode).getIdentNode();
		return new ArrayTypeNode(itemTypeIdent);
	}

	/**
	 * Checks whether the set only contains constants.
	 * @return True, if all set items are constant.
	 */
	protected boolean isConstant()
	{
		for(ExprNode item : arrayItems.getChildren()) {
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
		for(ExprNode item : arrayItems.getChildren()) {
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
			return (ArrayTypeNode)type;
		} else {
			return arrayType;
		}
	}

	protected CollectNode<ExprNode> getItems()
	{
		return arrayItems;
	}

	@Override
	protected IR constructIR()
	{
		Vector<Expression> items = new Vector<Expression>();
		for(ExprNode item : arrayItems.getChildren()) {
			items.add(item.checkIR(Expression.class));
		}
		Entity member = lhs != null ? lhs.getEntity() : null;
		ArrayType type = arrayType != null ? arrayType.checkIR(ArrayType.class) : null;
		return new ArrayInit(items, member, type, isConstant());
	}

	public ArrayInit getArrayInit()
	{
		return checkIR(ArrayInit.class);
	}

	public static String getUseStr()
	{
		return "array initialization";
	}
}
