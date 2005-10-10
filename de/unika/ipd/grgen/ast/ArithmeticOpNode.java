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
		for(Iterator<BaseNode> it = getChildren(); it.hasNext();) {
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
