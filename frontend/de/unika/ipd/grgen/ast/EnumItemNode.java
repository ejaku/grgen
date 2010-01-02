/*
 * GrGen: graph rewrite generator tool -- release GrGen.NET 2.6
 * Copyright (C) 2003-2010 Universitaet Karlsruhe, Institut fuer Programmstrukturen und Datenorganisation, LS Goos
 * licensed under LGPL v3 (see LICENSE.txt included in the packaging of this file)
 * www.grgen.net
 */

/**
 * @author Sebastian Hack
 * @version $Id$
 */
package de.unika.ipd.grgen.ast;

import java.util.Collection;
import java.util.HashSet;
import java.util.Vector;

import de.unika.ipd.grgen.ast.util.DeclarationTypeResolver;
import de.unika.ipd.grgen.ir.EnumItem;
import de.unika.ipd.grgen.ir.IR;
import de.unika.ipd.grgen.util.Walkable;

/**
 * A class for enum items.
 */
public class EnumItemNode extends MemberDeclNode {
	static {
		setName(EnumItemNode.class, "enum item");
	}

	private ExprNode value;
	private EnumConstNode constValue;

	/** Position of this item in the enum. */
	private final int pos;

	/**
	 * Make a new enum item node.
	 */
	public EnumItemNode(IdentNode identifier, IdentNode type, ExprNode value, int pos) {
		super(identifier, type, true);
		this.value = value;
		becomeParent(this.value);
		this.pos = pos;
	}

	/** returns children of this node */
	@Override
	public Collection<BaseNode> getChildren() {
		Vector<BaseNode> children = new Vector<BaseNode>();
		children.add(ident);
		children.add(getValidVersion(typeUnresolved, type));
		children.add(value);
		return children;
	}

	/** returns names of the children, same order as in getChildren */
	@Override
	public Collection<String> getChildrenNames() {
		Vector<String> childrenNames = new Vector<String>();
		childrenNames.add("ident");
		childrenNames.add("type");
		childrenNames.add("value");
		return childrenNames;
	}

	private static final DeclarationTypeResolver<EnumTypeNode> typeResolver = new DeclarationTypeResolver<EnumTypeNode>(EnumTypeNode.class);

	/** @see de.unika.ipd.grgen.ast.BaseNode#resolveLocal() */
	@Override
	protected boolean resolveLocal() {
		type = typeResolver.resolve(typeUnresolved, this);
		return type != null;
	}

	/**
	 * Check the validity of the initialisation expression.
	 * @return true, if the init expression is ok, false if not.
	 */
	@Override
	protected boolean checkLocal() {
		boolean res = super.checkLocal();
		// Check, if this enum item was defined with a latter one.
		// This may not be.
		HashSet<EnumItemNode> visitedEnumItems = new HashSet<EnumItemNode>();
		if(!checkValue(value, visitedEnumItems))
			return false;

		ExprNode newValue = value.evaluate();
		if (!(newValue instanceof ConstNode)) {
			reportError("Initialization of enum item is not constant");
			return false;
		}

		// Adjust the values type to int, else emit an error.
		if(!newValue.getType().isCompatibleTo(BasicTypeNode.intType)) {
			reportError("The type of the initializer must be integer");
			return false;
		}

		newValue = ((ConstNode) newValue).castTo(BasicTypeNode.intType);
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
	private boolean checkValue(Walkable cur, HashSet<EnumItemNode> visitedEnumItems) {
		EnumItemNode enumItem = null;
		if(cur instanceof EnumItemNode) {
			enumItem = (EnumItemNode) cur;
			if(pos == enumItem.pos) {
				reportError("Enum item must not depend on its own value");
				return false;
			}
			else if(pos < enumItem.pos) {
				reportError("Enum item must not depend on a following one");
				return false;
			}
			else if(visitedEnumItems.contains(enumItem)) {
				reportError("Circular dependency found on value of enum item \"" + enumItem.getIdentNode() + "\"");
				return false;
			}
			visitedEnumItems.add(enumItem);
		}
		else if(cur instanceof EnumTypeNode)	// EnumTypeNode has all EnumItemNodes as children => don't check them
			return true;
		else if(cur instanceof EnumExprNode)	// Enum item from another, already declared enum => skip it
			return true;

		for (Walkable child : cur.getWalkableChildren()) {
			if(!checkValue(child, visitedEnumItems))
				return false;
		}

		// If cur is an EnumItemNode, mark it as unvisited again
		// (needed for "a, b, c = a * b")
		if(enumItem != null) visitedEnumItems.remove(enumItem);

		return true;
	}

	/** @return The type node of the declaration */
	@Override
	public TypeNode getDeclType() {
		assert isResolved();

		return type;
	}

	protected ExprNode getValue() {
		if(constValue != null) return constValue;

		if(!(value instanceof ConstNode)) return value;

		Object obj = ((ConstNode)value).getValue();
		int v = ((Integer) obj).intValue();
		debug.report(NOTE, "result: " + value);

		constValue = new EnumConstNode(getCoords(), getIdentNode(), v);
		return constValue;
	}

	protected final EnumItem getItem() {
		return checkIR(EnumItem.class);
	}

	/**
	 * @see de.unika.ipd.grgen.ast.BaseNode#constructIR()
	 */
	@Override
	protected IR constructIR() {
		EnumConstNode c = (EnumConstNode) getValue();

		return new EnumItem(ident.getIdent(), c.getConstant());
	}

	public static String getUseStr() {
		return "enum item";
	}
}
