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
 * An expression that results from a declared identifier.
 */
public class DeclExprNode extends ExprNode {
	
	private static final int DECL = 0;
	
	static {
		setName(DeclExprNode.class, "decl expression");
	}
	
	/**
	 * Make a new declaration expression.
	 * @param coords The source code coordinates.
	 * @param declCharacter Some base node, that is a decl character.
	 */
	public DeclExprNode(BaseNode declCharacter) {
		super(declCharacter.getCoords());
		addChild(declCharacter);
	}
	
	/**
	 * @see de.unika.ipd.grgen.ast.ExprNode#getType()
	 */
	public TypeNode getType() {
		DeclaredCharacter c = (DeclaredCharacter) getChild(DECL);
		return (TypeNode) c.getDecl().getDeclType();
	}
	
	/**
	 * @see de.unika.ipd.grgen.ast.ExprNode#eval()
	 */
	protected ConstNode eval() {
		ConstNode res = ConstNode.getInvalid();
		DeclaredCharacter c = (DeclaredCharacter) getChild(DECL);
		DeclNode decl = c.getDecl();
		
		if(decl instanceof EnumItemNode) {
			res = ((EnumItemNode) decl).getValue();
		}
		
		return res;
	}
	
	/**
	 * @see de.unika.ipd.grgen.ast.BaseNode#check()
	 */
	protected boolean check() {
		return checkChild(DECL, DeclaredCharacter.class);
	}
	
	/**
	 * @see de.unika.ipd.grgen.ast.ExprNode#isConstant()
	 */
	public boolean isConstant() {
		DeclaredCharacter c = (DeclaredCharacter) getChild(DECL);
		return c.getDecl() instanceof EnumItemNode;
	}
	
	/**
	 * @see de.unika.ipd.grgen.ast.BaseNode#constructIR()
	 */
	protected IR constructIR() {
		return getChild(DECL).getIR();
	}
	
	
}
