/*
 * GrGen: graph rewrite generator tool -- release GrGen.NET 5.0
 * Copyright (C) 2003-2020 Universitaet Karlsruhe, Institut fuer Programmstrukturen und Datenorganisation, LS Goos; and free programmers
 * licensed under LGPL v3 (see LICENSE.txt included in the packaging of this file)
 * www.grgen.net
 */

/**
 * TypeExprOp.java
 *
 * @author Sebastian Hack
 */

package de.unika.ipd.grgen.ir.type;

import java.util.ArrayList;
import java.util.Collection;
import java.util.HashSet;
import java.util.List;

import de.unika.ipd.grgen.ir.model.type.InheritanceType;

public class TypeExprSetOperator extends TypeExpr
{
	public enum SetOperator
	{
		UNION,
		DIFFERENCE,
		INTERSECT
	}
	
	private final SetOperator op;

	private final List<TypeExpr> children = new ArrayList<TypeExpr>();

	public TypeExprSetOperator(SetOperator op)
	{
		this.op = op;
	}

	public final void addOperand(TypeExpr operand)
	{
		children.add(operand);
	}

	@Override
	public Collection<InheritanceType> evaluate()
	{
		Collection<InheritanceType> res = new HashSet<InheritanceType>();
		assert children.size() == 2 : "Arity 2 required";

		Collection<InheritanceType> lhs = children.get(0).evaluate();
		Collection<InheritanceType> rhs = children.get(1).evaluate();

		res.addAll(lhs);

		switch(op) {
		case UNION:
			res.addAll(rhs);
			break;
		case DIFFERENCE:
			res.removeAll(rhs);
			break;
		case INTERSECT:
			res.retainAll(rhs);
			break;
		}

		return res;
	}
}
