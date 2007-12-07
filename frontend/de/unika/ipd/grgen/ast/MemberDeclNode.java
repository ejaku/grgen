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

import de.unika.ipd.grgen.ast.util.Checker;
import de.unika.ipd.grgen.ast.util.DeclTypeResolver;
import de.unika.ipd.grgen.ast.util.MultChecker;
import de.unika.ipd.grgen.ast.util.Resolver;
import de.unika.ipd.grgen.ir.Entity;
import de.unika.ipd.grgen.ir.Expression;
import de.unika.ipd.grgen.ir.IR;
import de.unika.ipd.grgen.ir.MemberInit;
import de.unika.ipd.grgen.ir.Type;

/**
 * A compound type member declaration.
 */
public class MemberDeclNode extends DeclNode {
	
	static {
		setName(MemberDeclNode.class, "member declaration");
	}
	
	private static final Resolver typeResolver =
		new DeclTypeResolver(TypeNode.class);
	
	private static final Checker typeChecker =
		new MultChecker(new Class[] { BasicTypeNode.class, EnumTypeNode.class });
		
	/**
	 * @param n Identifier which declared the member.
	 * @param t Type with which the member was declared.
	 */
	public MemberDeclNode(IdentNode n, BaseNode t) {
		super(n, t);
		addResolver(TYPE, typeResolver);
	}
	
	
	
	/**
	 * @see de.unika.ipd.grgen.ast.BaseNode#check()
	 */
	protected boolean check() {
		return checkChild(TYPE, typeChecker);
	}
	
	protected IR constructIR() {
		Type type = (Type) getDeclType().checkIR(Type.class);
		return new Entity("entity", getIdentNode().getIdent(), type);
	}
	
}
