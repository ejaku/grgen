/*
 GrGen: graph rewrite generator tool.
 Copyright (C) 2005  IPD Goos, Universit"at Karlsruhe, Germany

 This library is free software; you can redistribute it and/or
 modify it under the terms of the GNU Lesser General Public
 License as published by the Free Software Foundation; either
 version 2.1 of the License, or (at your option) any later version.

 This library is distributed in the hope that it will be useful,
 but WITHOUT ANY WARRANTY; without even the implied warranty of
 MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the GNU
 Lesser General Public License for more details.

 You should have received a copy of the GNU Lesser General Public
 License along with this library; if not, write to the Free Software
 Foundation, Inc., 51 Franklin St, Fifth Floor, Boston, MA  02110-1301  USA
 */


/**
 * @author Sebastian Hack
 * @version $Id$
 */
package de.unika.ipd.grgen.ast;

import de.unika.ipd.grgen.ast.util.DeclarationTypeResolver;
import de.unika.ipd.grgen.ir.EnumItem;
import de.unika.ipd.grgen.ir.IR;
import de.unika.ipd.grgen.util.Walkable;
import java.util.Collection;
import java.util.HashSet;
import java.util.Vector;

/**
 * A class for enum items.
 */
public class EnumItemNode extends MemberDeclNode {
	static {
		setName(EnumItemNode.class, "enum item");
	}

	private ExprNode value;
	private EnumTypeNode type;

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
	public Collection<BaseNode> getChildren() {
		Vector<BaseNode> children = new Vector<BaseNode>();
		children.add(ident);
		children.add(getValidVersion(typeUnresolved, type));
		children.add(value);
		return children;
	}

	/** returns names of the children, same order as in getChildren */
	public Collection<String> getChildrenNames() {
		Vector<String> childrenNames = new Vector<String>();
		childrenNames.add("ident");
		childrenNames.add("type");
		childrenNames.add("value");
		return childrenNames;
	}

	private static final DeclarationTypeResolver<EnumTypeNode> typeResolver = new DeclarationTypeResolver<EnumTypeNode>(EnumTypeNode.class);

	/** @see de.unika.ipd.grgen.ast.BaseNode#resolveLocal() */
	protected boolean resolveLocal() {
		type = typeResolver.resolve(typeUnresolved, this);
		return type != null;
	}

	/**
	 * Check the validity of the initialisation expression.
	 * @return true, if the init expression is ok, false if not.
	 */
	protected boolean checkLocal() {
		// Check, if this enum item was defined with a latter one.
		// This may not be.
		HashSet<EnumItemNode> visitedEnumItems = new HashSet<EnumItemNode>();
		if(!checkValue(value, visitedEnumItems))
			return false;

		if(!value.isConst()) {
			reportError("Initialization of enum item is not constant");
			return false;
		}

		// Adjust the values type to int, else emit an error.
		if(value.getType().isCompatibleTo(BasicTypeNode.intType)) {
			ExprNode adjusted = value.adjustType(BasicTypeNode.intType);
			becomeParent(adjusted);
			value = adjusted;
		} else {
			reportError("The type of the initializer must be integer");
			return false;
		}

		return true;
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

	protected ConstNode getValue() {
		// TODO are we allowed to cast to a ConstNode here???
		ConstNode res = value.getConst().castTo(BasicTypeNode.intType);
		debug.report(NOTE, "type: " + res.getType());

		Object obj = res.getValue();
		if ( ! (obj instanceof Integer) ) {
			return ConstNode.getInvalid();
		}

		int v = ((Integer) obj).intValue();
		debug.report(NOTE, "result: " + res);

		return new EnumConstNode(getCoords(), getIdentNode(), v);
	}

	protected EnumItem getItem() {
		return (EnumItem) checkIR(EnumItem.class);
	}

	/**
	 * @see de.unika.ipd.grgen.ast.BaseNode#constructIR()
	 */
	protected IR constructIR() {
		IdentNode id = ident;
		ConstNode c = getValue();

		assert ( ! c.equals(ConstNode.getInvalid()) ):
			"an error should have been reported in an earlier phase";

		c.castTo(BasicTypeNode.intType);
		return new EnumItem(id.getIdent(), c.getConstant());
	}

	public static String getUseStr() {
		return "enum item";
	}
}
