/**
 * @author Sebastian Hack
 * @version $Id$
 */
package de.unika.ipd.grgen.ast;

import de.unika.ipd.grgen.ir.Assignment;
import de.unika.ipd.grgen.ir.IR;
import de.unika.ipd.grgen.ir.Qualification;
import de.unika.ipd.grgen.parser.Coords;

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
	
	/**
	 * @see de.unika.ipd.grgen.ast.BaseNode#check()
	 */
	protected boolean check() {
		if(checkChild(LHS, QualIdentNode.class)) {
			boolean res = true;
			QualIdentNode qual = (QualIdentNode) getChild(LHS);
			DeclNode owner = qual.getOwner();
			BaseNode ty = owner.getDeclType(); 
			
			if(ty instanceof InheritanceTypeNode) {
				InheritanceTypeNode inhTy = (InheritanceTypeNode) ty;
				
				if(inhTy.isConst()) { 
					error.error(getCoords(), "assignment to a const type object not allowed");
					res = false;
				}
			}
			
			return res;
		}
		
		return false; 
	}
	
	/**
	 * Construct the immediate representation from an assignment node.
	 * @see de.unika.ipd.grgen.ast.BaseNode#constructIR()
	 */
	protected IR constructIR() {
		return new Assignment((Qualification) getChild(LHS).constructIR(),
			getChild(RHS).constructIR());
	}

}
