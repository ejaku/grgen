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
 * TypeConstraintExprNode.java
 *
 * @author Sebastian Hack
 */

package de.unika.ipd.grgen.ast;

import de.unika.ipd.grgen.ir.IR;
import de.unika.ipd.grgen.ir.TypeExpr;
import de.unika.ipd.grgen.ir.TypeExprSetOperator;
import de.unika.ipd.grgen.parser.Coords;
import java.awt.Color;

/**
 * AST node representing type expressions.
 */
public class TypeExprNode extends BaseNode {
	
	static {
		setName(TypeExprNode.class, "type constraint expr");
	}
	
	public static final int SET = 0;
	public static final int SUBTYPES = 1;
	public static final int UNION = 2;
	public static final int DIFFERENCE = 3;
	public static final int INTERSECT = 4;
	public static final int LAST = INTERSECT;
	
	protected static final String[] opName = {
		"const", "subtypes", "union", "diff", "intersect"
	};
	
	protected static final int[] irOp = {
		-1, -1, TypeExprSetOperator.UNION,
			TypeExprSetOperator.DIFFERENCE, TypeExprSetOperator.INTERSECT
	};
			
	
	/** Opcode of the set operation. */
	private final int op;
	
	private static final TypeExprNode EMPTY =
		new TypeConstraintNode(Coords.getInvalid(), new CollectNode());
	
	public static final TypeExprNode getEmpty() {
		return EMPTY;
	}
	
  /**
	 * Make a new expression
	 */
  public TypeExprNode(Coords coords, int op) {
		super(coords);
		this.op = op;
		assert op >= 0 && op <= LAST : "Illegal type constraint expr opcode";
  }
	
	public TypeExprNode(Coords coords, int op, TypeExprNode op0, TypeExprNode op1) {
		this(coords, op);
		addChild(op0);
		addChild(op1);
	}
	
  private TypeExprNode(int op) {
		this(Coords.getBuiltin(), op);
  }

  /**
	 * @see de.unika.ipd.grgen.util.GraphDumpable#getNodeColor()
	 */
  public Color getNodeColor() {
		return Color.CYAN;
  }
	
	public String getNodeLabel() {
		return "type expr " + opName[op];
	}
	
	protected boolean check() {
		// Check, if the node has a valid arity.
		int arity = children();
		boolean arityOk = arity == 2;
		
		if(!arityOk)
			reportError("Type constraint expression has wrong arity: " + arity);

		// check the child node types
		boolean typesOk = checkAllChildren(TypeExprNode.class);
		
		return arityOk && typesOk;
	}

	protected IR constructIR() {
		TypeExpr lhs = (TypeExpr) getChild(0).checkIR(TypeExpr.class);

		TypeExpr rhs = (TypeExpr) getChild(1).checkIR(TypeExpr.class);
		
		TypeExprSetOperator expr = new TypeExprSetOperator(irOp[op]);
		expr.addOperand(lhs);
		expr.addOperand(rhs);
		
		return expr;
	}
  
}

