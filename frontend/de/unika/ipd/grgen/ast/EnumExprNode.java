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
 * EnumExprNpde.java
 *
 * @author Sebastian Hack
 */

package de.unika.ipd.grgen.ast;

import java.util.Collection;
import de.unika.ipd.grgen.ast.util.DeclResolver;
import de.unika.ipd.grgen.ast.util.DeclTypeResolver;
import de.unika.ipd.grgen.ast.util.Resolver;
import de.unika.ipd.grgen.ast.util.SimpleChecker;
import de.unika.ipd.grgen.ir.EnumExpression;
import de.unika.ipd.grgen.ir.EnumItem;
import de.unika.ipd.grgen.ir.EnumType;
import de.unika.ipd.grgen.ir.IR;
import de.unika.ipd.grgen.parser.Coords;

public class EnumExprNode extends QualIdentNode implements DeclaredCharacter {
	static {
		setName(EnumExprNode.class, "enum access expression");
	}

	public EnumExprNode(Coords c, BaseNode owner, IdentNode member) {
		super(c, owner, member);
	}

	/** returns children of this node */
	public Collection<BaseNode> getChildren() {
		return super.getChildren();
	}

	/** returns names of the children, same order as in getChildren */
	public Collection<String> getChildrenNames() {
		return super.getChildrenNames();
	}

	/** @see de.unika.ipd.grgen.ast.BaseNode#resolve() */
	protected boolean resolve() {
		if(isResolved()) {
			return resolutionResult();
		}

		boolean successfullyResolved = true;
		Resolver ownerResolver = new DeclTypeResolver(EnumTypeNode.class);
		BaseNode resolved = ownerResolver.resolve(owner);
		successfullyResolved = resolved!=null && successfullyResolved;
		owner = ownedResolutionResult(owner, resolved);

		if(owner instanceof EnumTypeNode) {
			EnumTypeNode enumType = (EnumTypeNode) owner;
			enumType.fixupDefinition((IdentNode)member);

			Resolver declResolver = new DeclResolver(EnumItemNode.class);
			resolved = declResolver.resolve(member);
			successfullyResolved = resolved!=null && successfullyResolved;
			member = ownedResolutionResult(member, resolved);
		} else {
			reportError("Left hand side of '::' is not an enum type");
			successfullyResolved = false;
		}
		nodeResolvedSetResult(successfullyResolved); // local result
		if(!successfullyResolved) {
			debug.report(NOTE, "resolve error");
		}

		successfullyResolved = owner.resolve() && successfullyResolved;
		successfullyResolved = member.resolve() && successfullyResolved;
		return successfullyResolved;
	}

	/** @see de.unika.ipd.grgen.ast.BaseNode#checkLocal() */
	protected boolean checkLocal() {
		return (new SimpleChecker(EnumTypeNode.class)).check(owner, error)
			& (new SimpleChecker(EnumItemNode.class)).check(member, error);
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
