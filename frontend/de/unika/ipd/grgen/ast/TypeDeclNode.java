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

import de.unika.ipd.grgen.ir.IR;

/**
 * Declaration of a type.
 */
public class TypeDeclNode extends DeclNode {
	
	static {
		setName(TypeDeclNode.class, "type declaration");
	}
	
	public TypeDeclNode(IdentNode i, BaseNode t) {
		super(i, t);
		
		// Set the declaration of the declared type node to this
		// node.
		if(t instanceof DeclaredTypeNode) {
			((DeclaredTypeNode) t).setDecl(this);
		}
	}
	
	/**
	 * The check succeeds, if the decl node check succeeds and the type
	 * of this declaration is instance of {@link DeclaredTypeNode}.
	 * @see de.unika.ipd.grgen.ast.BaseNode#check()
	 */
	protected boolean check() {
		return super.check() && checkChild(TYPE, DeclaredTypeNode.class);
	}
	
	/**
	 * A type declaration returns the declared type
	 * as result.
	 * @see de.unika.ipd.grgen.ast.BaseNode#constructIR()
	 */
	protected IR constructIR() {
		TypeNode declType = (TypeNode) getDeclType();
		return declType.getIR();
	}

	public static String getKindStr() {
		return "type declaration";
	}

	public static String getUseStr() {
		return "type";
	}

}
