/**
 * TypeConstraintExprNode.java
 *
 * @author Sebastian Hack
 */

package de.unika.ipd.grgen.ast;

import de.unika.ipd.grgen.ir.IR;
import de.unika.ipd.grgen.ir.InheritanceType;
import de.unika.ipd.grgen.ir.TypeExpr;
import de.unika.ipd.grgen.ir.TypeExprSetOperator;
import de.unika.ipd.grgen.parser.Coords;
import java.awt.Color;

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
		new TypeConstNode(Coords.getInvalid(), new CollectNode());
	
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
	
	public TypeExprNode(Coords coords, int op, BaseNode op0, BaseNode op1) {
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
			reportError("type constraint expression has wrong arity: " + arity);

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

