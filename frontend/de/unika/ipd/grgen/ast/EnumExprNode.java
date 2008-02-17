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

import de.unika.ipd.grgen.ast.util.DeclarationResolver;
import de.unika.ipd.grgen.ast.util.DeclarationTypeResolver;
import de.unika.ipd.grgen.ir.EnumExpression;
import de.unika.ipd.grgen.ir.EnumItem;
import de.unika.ipd.grgen.ir.EnumType;
import de.unika.ipd.grgen.ir.IR;
import de.unika.ipd.grgen.parser.Coords;
import java.util.Collection;
import java.util.Vector;

public class EnumExprNode extends QualIdentNode implements DeclaredCharacter {
	static {
		setName(EnumExprNode.class, "enum access expression");
	}

	public EnumExprNode(Coords coords, IdentNode owner, IdentNode member) {
		super(coords, owner, member);
	}

	private EnumTypeNode owner;

	private EnumItemNode member;

	/** returns children of this node */
	public Collection<BaseNode> getChildren() {
		Vector<BaseNode> children = new Vector<BaseNode>();
		children.add(getValidVersion(ownerUnresolved, owner));
		children.add(getValidVersion(memberUnresolved, member));
		return children;
	}
	
	private static final DeclarationTypeResolver<EnumTypeNode> ownerResolver = new DeclarationTypeResolver<EnumTypeNode>(EnumTypeNode.class);

	private static final DeclarationResolver<EnumItemNode> memberResolver = new DeclarationResolver<EnumItemNode>(EnumItemNode.class);

	/** @see de.unika.ipd.grgen.ast.BaseNode#resolveLocal() */
	protected boolean resolveLocal() {
		boolean successfullyResolved = true;
		owner = ownerResolver.resolve(ownerUnresolved, this);
		successfullyResolved = owner!=null && successfullyResolved;

		if(owner != null) {
			owner.fixupDefinition(memberUnresolved);

			member = memberResolver.resolve(memberUnresolved, this);
			successfullyResolved = member!=null && successfullyResolved;
		} else {
			reportError("Left hand side of '::' is not an enum type");
			successfullyResolved = false;
		}

		return successfullyResolved;
	}

	/** @see de.unika.ipd.grgen.ast.BaseNode#checkLocal() */
	protected boolean checkLocal() {
		return true;
	}

	/** @see de.unika.ipd.grgen.ast.DeclaredCharacter#getDecl() */
	public EnumItemNode getDecl() {
		assert isResolved();

		return member;
	}

	public DeclNode getOwner() {
		assert isResolved();

		return DeclNode.getInvalid();
	}

	/**
	 * Build the IR of an enum expression.
	 * @return An enum expression IR object.
	 */
	protected IR constructIR() {
		EnumType et = (EnumType) owner.checkIR(EnumType.class);
		EnumItem it = (EnumItem) member.checkIR(EnumItem.class);
		return new EnumExpression(et, it);
	}
}

