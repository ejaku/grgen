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

import de.unika.ipd.grgen.ast.util.DeclResolver;
import de.unika.ipd.grgen.ast.util.DeclTypeResolver;
import de.unika.ipd.grgen.ast.util.Resolver;
import de.unika.ipd.grgen.ir.EnumExpression;
import de.unika.ipd.grgen.ir.EnumItem;
import de.unika.ipd.grgen.ir.EnumType;
import de.unika.ipd.grgen.ir.IR;
import de.unika.ipd.grgen.parser.Coords;

public class EnumExprNode extends QualIdentNode implements DeclaredCharacter
{
	static {
		setName(EnumExprNode.class, "enum access expression");
	}
	
	private static final Resolver ownerResolver =
		new DeclTypeResolver(EnumTypeNode.class);
	
	private static final Resolver declResolver =
		new DeclResolver(EnumItemNode.class);
	
	public EnumExprNode(Coords c, BaseNode owner, BaseNode member) {
		super(c, owner, member);
	}
	
	/** @see de.unika.ipd.grgen.ast.BaseNode#doResolve() */
	protected boolean doResolve() {
		if(isResolved()) {
			return getResolve();
		}
		
		boolean successfullyResolved = resolve();
		successfullyResolved = getChild(OWNER).doResolve() && successfullyResolved;
		successfullyResolved = getChild(MEMBER).doResolve() && successfullyResolved;
		return successfullyResolved;
	}
	
	/**
	 * Resolve the identifier nodes.
	 * Each AST node can add resolvers to a list administrated by
	 * {@link BaseNode}. These resolvers replace the identifier node in the
	 * children of this node by something, that can be produced out of it.
	 * For example, an identifier representing a declared type is replaced by
	 * the declared type. The behaviour depends on the {@link Resolver}.
	 *
	 * This method first call all resolvers registered in this node
	 * and descends to the this node's children by invoking
	 * {@link #getResolve()} on each child.
	 *
	 * A base node subclass can overload this method, to apply another
	 * policy of resolution.
	 *
	 * @return true, if all resolvers finished their job and no error
	 * occurred, false, if there was some error.
	 */
	protected boolean resolve() {
		boolean res = false;
		IdentNode member = (IdentNode) getChild(MEMBER);
		
		ownerResolver.resolve(this, OWNER);
		BaseNode owner = getChild(OWNER);
		res = owner.getResolve();
		
		if(owner instanceof EnumTypeNode) {
			EnumTypeNode enumType = (EnumTypeNode) owner;
			enumType.fixupDefinition(member);
			declResolver.resolve(this, MEMBER);
			res = getChild(MEMBER).getResolve();
		} else {
			reportError("Left hand side of '::' is not an enum type");
			res = false;
		}
			
		setResolved(res);
		return res;
	}
		
	/**
	 * @see de.unika.ipd.grgen.ast.BaseNode#check()
	 */
	protected boolean check() {
		return checkChild(OWNER, EnumTypeNode.class)
			&& checkChild(MEMBER, EnumItemNode.class);
	}
	
	/**
	 * Build the IR of an enum expression.
	 * @return An enum expression IR object.
	 */
	protected IR constructIR() {
		EnumType et = (EnumType) getChild(OWNER).checkIR(EnumType.class);
		EnumItem it = (EnumItem) getChild(MEMBER).checkIR(EnumItem.class);
		return new EnumExpression(et, it);
	}
}
