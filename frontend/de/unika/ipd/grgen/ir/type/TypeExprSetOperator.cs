/*
 * GrGen: graph rewrite generator tool -- release GrGen.NET 8.1
 * Copyright (C) 2003-2026 Universitaet Karlsruhe, Institut fuer Programmstrukturen und Datenorganisation, LS Goos; and free programmers
 * licensed under LGPL v3, some components/parts use different licenses (see LICENSE.txt included in the packaging of this file)
 * www.grgen.de / www.grgen.net
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
import java.util.Set;

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
	public Set<InheritanceType> evaluate()
	{
		Set<InheritanceType> res = new HashSet<InheritanceType>();
		assert children.size() == 2 : "Arity 2 required"; // it could make sense to model this explicitly as a binary tree

		Collection<InheritanceType> lhs = children.get(0).evaluate();
		Collection<InheritanceType> rhs = children.get(1).evaluate();

		res.addAll(lhs);

		switch(op) { // note that types are taken literally and are not resolved to the union of their subtypes
		case UNION:
			res.addAll(rhs); // entity:T1\(T2+T2+T3) is evaluated/optimized to entity:T1\(T2+T3)
			break;
		case DIFFERENCE:
			assert(false); // not used yet, entity:T1\T2 is mapped to entity:T1 and a type constraint T2, entity:T1\(T2+T3) is mapped to entity:T1 and a type constraint T2+T3 (union) (note that it is checked that T is not contained in the constraints if entity:T) 
			res.removeAll(rhs);
			break;
		case INTERSECT:
			assert(false); // not used yet
			res.retainAll(rhs);
			break;
		}

		return res;
	}
}
