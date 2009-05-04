/*
 * GrGen: graph rewrite generator tool -- release GrGen.NET 2.5
 * Copyright (C) 2009 Universitaet Karlsruhe, Institut fuer Programmstrukturen und Datenorganisation, LS Goos
 * licensed under GPL v3 (see LICENSE.txt included in the packaging of this file)
 */

/**
 * TypeExprOp.java
 *
 * @author Sebastian Hack
 */

package de.unika.ipd.grgen.ir;

import java.util.ArrayList;
import java.util.Collection;
import java.util.HashSet;
import java.util.List;

public class TypeExprSetOperator extends TypeExpr {

	public static final int UNION = 0;
	public static final int DIFFERENCE = 1;
	public static final int INTERSECT = 2;

	private final int op;

	private final List<TypeExpr> children = new ArrayList<TypeExpr>();

	public TypeExprSetOperator(int op) {
		this.op = op;
		assert op >= 0 && op <= INTERSECT : "Illegal operand";
	}

	public final void addOperand(TypeExpr operand) {
		children.add(operand);
	}

	public Collection<InheritanceType> evaluate() {
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

