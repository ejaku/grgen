/**
 * @author Sebastian Hack
 * @version $Id$
 */
package de.unika.ipd.grgen.ast;

import de.unika.ipd.grgen.parser.Coords;
import de.unika.ipd.grgen.ir.IR;
import de.unika.ipd.grgen.ir.Assignment;

/**
 * An expression node, denoting an assignment.
 */
public class AssignNode extends BaseNode {
	static {
		setName(AssignNode.class, "Assign");
	}
	
	private static final int LHS = 0;
	private static final int RHS = 1;
	
	/**
	 * @param coords The source code coordinates of = operator.
	 * @param qual The left hand side.
	 * @param expr The expression, that is assigned.
	 */
	public AssignNode(Coords coords, BaseNode qual, BaseNode expr) {
		super(coords);
		addChild(qual);
		addChild(expr);
	}
	
	protected IR constructIR() {
		return new Assignment(getChild(LHS).constructIR(),
							  getChild(RHS).constructIR());
	}
}
