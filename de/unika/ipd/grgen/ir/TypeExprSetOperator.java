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
	
	private final List children = new ArrayList();
	
	public TypeExprSetOperator(int op) {
		this.op = op;
		assert op >= 0 && op <= INTERSECT : "Illegal operand";
	}
	
	public final void addOperand(TypeExpr operand) {
		children.add(operand);
	}
	
	public Collection evaluate() {
		Collection res = new HashSet();
		assert children.size() == 2 : "Arity 2 required";
		
		Collection lhs = ((TypeExpr) children.get(0)).evaluate();
		Collection rhs = ((TypeExpr) children.get(1)).evaluate();
		
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

