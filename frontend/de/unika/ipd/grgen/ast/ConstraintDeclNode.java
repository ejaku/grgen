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
	
	/** @see de.unika.ipd.grgen.ast.BaseNode#resolve() */
	protected abstract boolean resolve();
	
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
		successfullyChecked = getChild(CONSTRAINTS).check() && successfullyChecked;
		return successfullyChecked;
	}
	
	protected boolean checkLocal() {
		return super.checkLocal() && checkChild(CONSTRAINTS, TypeExprNode.class);
	}
	
	protected final TypeExpr getConstraints() {
		return (TypeExpr) getChild(CONSTRAINTS).checkIR(TypeExpr.class);
	}
}

