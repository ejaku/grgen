/*
 * GrGen: graph rewrite generator tool -- release GrGen.NET 5.0
 * Copyright (C) 2003-2020 Universitaet Karlsruhe, Institut fuer Programmstrukturen und Datenorganisation, LS Goos; and free programmers
 * licensed under LGPL v3 (see LICENSE.txt included in the packaging of this file)
 * www.grgen.net
 */

package de.unika.ipd.grgen.ast.type;

import java.util.Collection;
import java.util.Vector;

import de.unika.ipd.grgen.ast.BaseNode;
import de.unika.ipd.grgen.ir.IR;
import de.unika.ipd.grgen.ir.type.TypeExpr;
import de.unika.ipd.grgen.ir.type.TypeExprSetOperator;
import de.unika.ipd.grgen.parser.Coords;

/**
 * AST node representing binary type expressions.
 */
public class TypeBinaryExprNode extends TypeExprNode
{
	static {
		setName(TypeBinaryExprNode.class, "type binary expr");
	}

	private TypeExprNode lhs;
	private TypeExprNode rhs;

	public TypeBinaryExprNode(Coords coords, TypeOperator op, TypeExprNode op0, TypeExprNode op1)
	{
		super(coords, op);
		this.lhs = op0;
		becomeParent(this.lhs);
		this.rhs = op1;
		becomeParent(this.rhs);
	}

	/** returns children of this node */
	@Override
	public Collection<BaseNode> getChildren()
	{
		Vector<BaseNode> children = new Vector<BaseNode>();
		children.add(lhs);
		children.add(rhs);
		return children;
	}

	/** returns names of the children, same order as in getChildren */
	@Override
	public Collection<String> getChildrenNames()
	{
		Vector<String> childrenNames = new Vector<String>();
		childrenNames.add("lhs");
		childrenNames.add("rhs");
		return childrenNames;
	}

	/** @see de.unika.ipd.grgen.ast.BaseNode#resolveLocal() */
	@Override
	protected boolean resolveLocal()
	{
		return true;
	}

	@Override
	protected boolean checkLocal()
	{
		return true;
	}

	@Override
	protected IR constructIR()
	{
		TypeExpr lhs = this.lhs.checkIR(TypeExpr.class);
		TypeExpr rhs = this.rhs.checkIR(TypeExpr.class);

		TypeExprSetOperator expr = new TypeExprSetOperator(getSetOperator(op));
		expr.addOperand(lhs);
		expr.addOperand(rhs);

		return expr;
	}
	
	private static TypeExprSetOperator.SetOperator getSetOperator(TypeExprNode.TypeOperator op)
	{
		switch(op)
		{
		case UNION:
			return TypeExprSetOperator.SetOperator.UNION;
		case DIFFERENCE:
			return TypeExprSetOperator.SetOperator.DIFFERENCE;
		case INTERSECT:
			return TypeExprSetOperator.SetOperator.INTERSECT;
		default: // case SET - not used, only the set operators are mapped, internal error
			assert(false);
			return TypeExprSetOperator.SetOperator.UNION;
		}
	}
}
