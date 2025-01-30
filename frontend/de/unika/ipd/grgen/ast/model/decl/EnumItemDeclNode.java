/*
 * GrGen: graph rewrite generator tool -- release GrGen.NET 7.1
 * Copyright (C) 2003-2025 Universitaet Karlsruhe, Institut fuer Programmstrukturen und Datenorganisation, LS Goos; and free programmers
 * licensed under LGPL v3 (see LICENSE.txt included in the packaging of this file)
 * www.grgen.net
 */

/**
 * @author Sebastian Hack
 */

package de.unika.ipd.grgen.ast.model.decl;

import java.util.Collection;
import java.util.HashSet;
import java.util.Vector;

import de.unika.ipd.grgen.ast.*;
import de.unika.ipd.grgen.ast.expr.ConstNode;
import de.unika.ipd.grgen.ast.expr.EnumConstNode;
import de.unika.ipd.grgen.ast.expr.EnumExprNode;
import de.unika.ipd.grgen.ast.expr.ExprNode;
import de.unika.ipd.grgen.ast.model.type.EnumTypeNode;
import de.unika.ipd.grgen.ast.type.TypeNode;
import de.unika.ipd.grgen.ast.type.basic.BasicTypeNode;
import de.unika.ipd.grgen.ast.util.DeclarationTypeResolver;
import de.unika.ipd.grgen.ir.IR;
import de.unika.ipd.grgen.ir.model.EnumItem;
import de.unika.ipd.grgen.util.Walkable;

/**
 * A class for enum items.
 */
public class EnumItemDeclNode extends MemberDeclNode
{
	static {
		setName(EnumItemDeclNode.class, "enum item decl");
	}

	private ExprNode value;
	private EnumConstNode constValue;

	/** Position of this item in the enum. */
	private final int pos;

	/**
	 * Make a new enum item decl node.
	 */
	public EnumItemDeclNode(IdentNode identifier, IdentNode type, ExprNode value, int pos)
	{
		super(identifier, type, true);
		this.value = value;
		becomeParent(this.value);
		this.pos = pos;
	}

	/** returns children of this node */
	@Override
	public Collection<BaseNode> getChildren()
	{
		Vector<BaseNode> children = new Vector<BaseNode>();
		children.add(ident);
		children.add(getValidVersion(typeUnresolved, type));
		children.add(value);
		return children;
	}

	/** returns names of the children, same order as in getChildren */
	@Override
	public Collection<String> getChildrenNames()
	{
		Vector<String> childrenNames = new Vector<String>();
		childrenNames.add("ident");
		childrenNames.add("type");
		childrenNames.add("value");
		return childrenNames;
	}

	private static final DeclarationTypeResolver<EnumTypeNode> typeResolver =
			new DeclarationTypeResolver<EnumTypeNode>(EnumTypeNode.class);

	/** @see de.unika.ipd.grgen.ast.BaseNode#resolveLocal() */
	@Override
	protected boolean resolveLocal()
	{
		type = typeResolver.resolve(typeUnresolved, this);
		return type != null;
	}

	/**
	 * Check the validity of the initialisation expression.
	 * @return true, if the init expression is ok, false if not.
	 */
	@Override
	protected boolean checkLocal()
	{
		boolean res = super.checkLocal();
		// Check, if this enum item was defined with a latter one.
		// This may not be.
		HashSet<EnumItemDeclNode> visitedEnumItems = new HashSet<EnumItemDeclNode>();
		if(!checkValue(value, visitedEnumItems))
			return false;

		ExprNode newValue = value.evaluate();
		if(!(newValue instanceof ConstNode)) {
			reportError("The enum item " + ident + " expects a constant initialization expression.");
			return false;
		}

		// Adjust the values type to int, else emit an error.
		if(!newValue.getType().isCompatibleTo(BasicTypeNode.intType)) {
			reportError("The enum item " + ident + " expects an initialization expression of type int, but is given an expression of type " + newValue.getType().getTypeName() + ".");
			return false;
		}

		newValue = ((ConstNode)newValue).castTo(BasicTypeNode.intType);
		if(value != newValue) {
			switchParenthood(value, newValue);
			value = newValue;
		}

		return res;
	}

	/**
	 * Used to check the value of an EnumItemNode for circular dependencies
	 * and accesses to enum item declared after use
	 * @returns false, if an illegal use has been found
	 */
	private boolean checkValue(Walkable cur, HashSet<EnumItemDeclNode> visitedEnumItems)
	{
		EnumItemDeclNode enumItem = null;
		if(cur instanceof EnumItemDeclNode) {
			enumItem = (EnumItemDeclNode)cur;
			if(pos == enumItem.pos) {
				reportError("The enum item " + ident + " is not allowed to depend on its own value.");
				return false;
			} else if(pos < enumItem.pos) {
				reportError("The enum item " + ident + " is not allowed to depend on a following one.");
				return false;
			} else if(visitedEnumItems.contains(enumItem)) {
				reportError("Circular dependency found on value of enum item " + enumItem.getIdentNode() + ".");
				return false;
			}
			visitedEnumItems.add(enumItem);
		} else if(cur instanceof EnumTypeNode) // EnumTypeNode has all EnumItemNodes as children => don't check them
			return true;
		else if(cur instanceof EnumExprNode) // Enum item from another, already declared enum => skip it
			return true;

		for(Walkable child : cur.getWalkableChildren()) {
			if(!checkValue(child, visitedEnumItems))
				return false;
		}

		// If cur is an EnumItemNode, mark it as unvisited again
		// (needed for "a, b, c = a * b")
		if(enumItem != null)
			visitedEnumItems.remove(enumItem);

		return true;
	}

	/** @return The type node of the declaration */
	@Override
	public TypeNode getDeclType()
	{
		assert isResolved();

		return type;
	}

	public ExprNode getValue()
	{
		if(constValue != null)
			return constValue;

		if(!(value instanceof ConstNode))
			return value;

		Object obj = ((ConstNode)value).getValue();
		int v = ((Integer)obj).intValue();
		debug.report(NOTE, "result: " + value);

		constValue = new EnumConstNode(getCoords(), getIdentNode(), v);
		return constValue;
	}

	public final EnumItem getItem()
	{
		return checkIR(EnumItem.class);
	}

	/**
	 * @see de.unika.ipd.grgen.ast.BaseNode#constructIR()
	 */
	@Override
	protected IR constructIR()
	{
		EnumConstNode c = (EnumConstNode)getValue();

		return new EnumItem(ident.getIdent(), c.getConstant());
	}

	public static String getKindStr()
	{
		return "enum item";
	}
}
