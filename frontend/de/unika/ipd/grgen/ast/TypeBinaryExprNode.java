/*
 * GrGen: graph rewrite generator tool -- release GrGen.NET 2.1
 * Copyright (C) 2008 Universitaet Karlsruhe, Institut fuer Programmstrukturen und Datenorganisation, LS Goos
 * licensed under GPL v3 (see LICENSE.txt included in the packaging of this file)
 */

package de.unika.ipd.grgen.ast;

import java.util.Collection;
import java.util.Vector;

import de.unika.ipd.grgen.ir.IR;
import de.unika.ipd.grgen.ir.TypeExpr;
import de.unika.ipd.grgen.ir.TypeExprSetOperator;
import de.unika.ipd.grgen.parser.Coords;

/**
 * AST node representing binary type expressions.
 */
public class TypeBinaryExprNode extends TypeExprNode {
	static {
		setName(TypeBinaryExprNode.class, "type binary expr");
	}

	TypeExprNode lhs;
	TypeExprNode rhs;

	public TypeBinaryExprNode(Coords coords, int op, TypeExprNode op0, TypeExprNode op1) {
		super(coords, op);
		this.lhs = op0;
		becomeParent(this.lhs);
		this.rhs = op1;
		becomeParent(this.rhs);
	}

	/** returns children of this node */
	public Collection<BaseNode> getChildren() {
		Vector<BaseNode> children = new Vector<BaseNode>();
		children.add(lhs);
		children.add(rhs);
		return children;
	}

	/** returns names of the children, same order as in getChildren */
	public Collection<String> getChildrenNames() {
		Vector<String> childrenNames = new Vector<String>();
		childrenNames.add("lhs");
		childrenNames.add("rhs");
		return childrenNames;
	}

	/** @see de.unika.ipd.grgen.ast.BaseNode#resolveLocal() */
	protected boolean resolveLocal() {
		return true;
	}

	protected boolean checkLocal() {
		return true;
	}

	protected IR constructIR() {
		TypeExpr lhs = this.lhs.checkIR(TypeExpr.class);
		TypeExpr rhs = this.rhs.checkIR(TypeExpr.class);

		TypeExprSetOperator expr = new TypeExprSetOperator(irOp[op]);
		expr.addOperand(lhs);
		expr.addOperand(rhs);

		return expr;
	}
}

