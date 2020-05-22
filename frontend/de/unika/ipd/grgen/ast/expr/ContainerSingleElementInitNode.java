/*
 * GrGen: graph rewrite generator tool -- release GrGen.NET 5.0
 * Copyright (C) 2003-2020 Universitaet Karlsruhe, Institut fuer Programmstrukturen und Datenorganisation, LS Goos; and free programmers
 * licensed under LGPL v3 (see LICENSE.txt included in the packaging of this file)
 * www.grgen.net
 */

/**
 * @author Edgar Jakumeit
 */

package de.unika.ipd.grgen.ast.expr;

import java.util.Collection;
import java.util.Vector;

import de.unika.ipd.grgen.ast.*;
import de.unika.ipd.grgen.ast.expr.ConstNode;
import de.unika.ipd.grgen.ast.expr.ContainerInitNode;
import de.unika.ipd.grgen.ast.expr.ExprNode;
import de.unika.ipd.grgen.ir.expr.Expression;
import de.unika.ipd.grgen.parser.Coords;

public abstract class ContainerSingleElementInitNode extends ContainerInitNode
{
	static {
		setName(ContainerSingleElementInitNode.class, "container single element init");
	}

	protected CollectNode<ExprNode> containerItems = new CollectNode<ExprNode>();

	
	public ContainerSingleElementInitNode(Coords coords)
	{
		super(coords);
	}
	
	public void addItem(ExprNode item)
	{
		containerItems.addChild(item);
	}

	@Override
	public Collection<? extends BaseNode> getChildren()
	{
		Vector<BaseNode> children = new Vector<BaseNode>();
		children.add(containerItems);
		return children;
	}

	@Override
	public Collection<String> getChildrenNames()
	{
		Vector<String> childrenNames = new Vector<String>();
		childrenNames.add("containerItems");
		return childrenNames;
	}

	protected boolean checkContainerItems()
	{
		boolean success = true;

		TypeNode containerElementType = getContainerType().getElementType();
		for(ExprNode item : containerItems.getChildren()) {
			if(item.getType() != containerElementType) {
				if(!isInitInModel()) {
					ExprNode oldValueExpr = item;
					ExprNode newValueExpr = item.adjustType(containerElementType, getCoords());
					containerItems.replace(oldValueExpr, newValueExpr);
					if(newValueExpr == ConstNode.getInvalid()) {
						success = false;
						item.reportError("Value type \"" + oldValueExpr.getType()
								+ "\" of initializer doesn't fit to value type \"" + containerElementType
								+ "\" of the container (" + getContainerType() + ").");
					}
				} else {
					success = false;
					item.reportError("Value type \"" + item.getType()
							+ "\" of initializer doesn't fit to value type \"" + containerElementType
							+ "\" of the container (" + getContainerType()
							+ " -- all items must be of exactly the same type).");
				}
			}
		}

		return success;
	}

	/**
	 * Checks whether the set only contains constants.
	 * @return True, if all set items are constant.
	 */
	public boolean isConstant()
	{
		for(ExprNode item : containerItems.getChildren()) {
			if(!(item instanceof ConstNode || isEnumValue(item)))
				return false;
		}
		return true;
	}

	public boolean contains(ConstNode node)
	{
		for(ExprNode item : containerItems.getChildren()) {
			if(item instanceof ConstNode) {
				ConstNode itemConst = (ConstNode)item;
				if(node.getValue().equals(itemConst.getValue()))
					return true;
			}
		}
		return false;
	}
	
	protected CollectNode<ExprNode> getItems()
	{
		return containerItems;
	}

	protected Vector<Expression> constructItems()
	{
		Vector<Expression> items = new Vector<Expression>();
		for(ExprNode item : containerItems.getChildren()) {
			items.add(item.checkIR(Expression.class));
		}
		return items;
	}
}
