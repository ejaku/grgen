/**
 * @author Sebastian Hack
 * @version $Id$
 */
package de.unika.ipd.grgen.ast;

import java.util.Iterator;

import de.unika.ipd.grgen.parser.Coords;

/**
 * An arithmetic operator.
 */
public class ArithmeticOpNode extends OpNode {
	
	/**
	 * @param coords Source code coordinates.
	 * @param opId ID of the operator.
	 */
	public ArithmeticOpNode(Coords coords, int opId) {
		super(coords, opId);
	}
	
	/**
	 * @see de.unika.ipd.grgen.ast.ExprNode#isConstant()
	 * An operator is constant, if all operands are constant.
	 */
	public boolean isConstant() {
		assertResolved();
		boolean res = true;
		for(Iterator it = getChildren(); it.hasNext();) {
			ExprNode operand = (ExprNode) it.next();
			if(!operand.isConstant()) {
				res = false;
				break;
			}
		}
		return res;
	}
	
	/**
	 * @see de.unika.ipd.grgen.ast.ExprNode#eval()
	 */
	protected ConstNode eval() {
		ConstNode res = ConstNode.getInvalid();
		int n = children();
		ConstNode[] args = new ConstNode[n];
		
		for(int i = 0; i < n; i++) {
			ExprNode c = (ExprNode) getChild(i);
			args[i] = c.evaluate();
		}
		
		return getOperator().evaluate(getCoords(), args);
	}
	
	/**
	 * @see de.unika.ipd.grgen.ast.BaseNode#check()
	 * All children must be expression nodes, too.
	 */
	protected boolean check() {
		return super.check()
			&& checkAllChildren(ExprNode.class);
	}
	
	
	
}
