/**
 * ConstraintDeclNode.java
 *
 * @author Sebastian Hack
 */

package de.unika.ipd.grgen.ast;

import de.unika.ipd.grgen.ir.TypeExpr;

abstract class ConstraintDeclNode extends DeclNode {
	
	protected static final int CONSTRAINTS = LAST + 1;
	
	ConstraintDeclNode(IdentNode id, BaseNode type, BaseNode constraints) {
		super(id, type);
		addChild(constraints);
	}
	
	protected boolean check() {
		return super.check() && checkChild(CONSTRAINTS, TypeExprNode.class);
	}
	
	protected final TypeExpr getConstraints() {
		return (TypeExpr) getChild(CONSTRAINTS).checkIR(TypeExpr.class);
	}
	
}

