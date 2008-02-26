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

