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
import de.unika.ipd.grgen.ast.util.Resolver;
import de.unika.ipd.grgen.ast.util.SimpleChecker;
import de.unika.ipd.grgen.ir.Entity;
import de.unika.ipd.grgen.ir.IR;
import de.unika.ipd.grgen.ir.Type;

/**
 * A compound type member declaration.
 */
public class MemberDeclNode extends DeclNode
{
	static {
		setName(MemberDeclNode.class, "member declaration");
	}

	protected static final Resolver typeResolver =
		new DeclTypeResolver(TypeNode.class);

	private static final Checker typeChecker =
		new SimpleChecker(new Class[] { BasicTypeNode.class, EnumTypeNode.class });

	/**
	 * @param n Identifier which declared the member.
	 * @param t Type with which the member was declared.
	 */
	public MemberDeclNode(IdentNode n, BaseNode t) {
		super(n, t);
	}

  	/** @see de.unika.ipd.grgen.ast.BaseNode#resolve() */
	protected boolean resolve() {
		if(isResolved()) {
			return resolutionResult();
		}
		
		debug.report(NOTE, "resolve in: " + getId() + "(" + getClass() + ")");
		boolean successfullyResolved = true;
		successfullyResolved = typeResolver.resolve(this, TYPE) && successfullyResolved;
		nodeResolvedSetResult(successfullyResolved); // local result
		if(!successfullyResolved) {
			debug.report(NOTE, "resolve error");
		}
		
		successfullyResolved = getChild(IDENT).resolve() && successfullyResolved;
		successfullyResolved = getChild(TYPE).resolve() && successfullyResolved;
		return successfullyResolved;
	}

	/** @see de.unika.ipd.grgen.ast.BaseNode#check() */
	protected boolean check() {
		if(!resolutionResult()) {
			return false;
		}
		if(isChecked()) {
			return getChecked();
		}
		
		boolean successfullyChecked = checkLocal();
		nodeCheckedSetResult(successfullyChecked);
		if(successfullyChecked) {
			assert(!isTypeChecked());
			successfullyChecked = typeCheckLocal();
			nodeTypeCheckedSetResult(successfullyChecked);
		}
		
		successfullyChecked = getChild(IDENT).check() && successfullyChecked;
		successfullyChecked = getChild(TYPE).check() && successfullyChecked;
		return successfullyChecked;
	}
	
	/**
	 * @see de.unika.ipd.grgen.ast.BaseNode#checkLocal()
	 */
	protected boolean checkLocal() {
		return checkChild(TYPE, typeChecker);
	}

	protected IR constructIR() {
		Type type = (Type) getDeclType().checkIR(Type.class);
		return new Entity("entity", getIdentNode().getIdent(), type);
	}
}
