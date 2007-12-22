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
 * ConstraintDeclNode.java
 *
 * @author Sebastian Hack
 */

package de.unika.ipd.grgen.ast;

import de.unika.ipd.grgen.ir.TypeExpr;

abstract class ConstraintDeclNode extends DeclNode
{
	protected static final int CONSTRAINTS = LAST + 1;
	
	ConstraintDeclNode(IdentNode id, BaseNode type, BaseNode constraints) {
		super(id, type);
		addChild(constraints);
	}
	
	/** @see de.unika.ipd.grgen.ast.BaseNode#doResolve() */
	protected abstract boolean doResolve();
	
	/** @see de.unika.ipd.grgen.ast.BaseNode#doCheck() */
	protected boolean doCheck() {
		assert(isResolved());
		if(!resolveResult) {
			return false;
		}
		if(isChecked()) {
			return getChecked();
		}
		
		boolean successfullyChecked = getCheck();
		if(successfullyChecked) {
			successfullyChecked = getTypeCheck();
		}
		successfullyChecked = getChild(IDENT).doCheck() && successfullyChecked;
		successfullyChecked = getChild(TYPE).doCheck() && successfullyChecked;
		successfullyChecked = getChild(CONSTRAINTS).doCheck() && successfullyChecked;
	
		return successfullyChecked;
	}
	
	protected boolean check() {
		return super.check() && checkChild(CONSTRAINTS, TypeExprNode.class);
	}
	
	protected final TypeExpr getConstraints() {
		return (TypeExpr) getChild(CONSTRAINTS).checkIR(TypeExpr.class);
	}
}

