/**
 * @author Sebastian Hack
 * @version $Id$
 */
package de.unika.ipd.grgen.ast;

import java.util.Iterator;

import de.unika.ipd.grgen.ast.util.Checker;
import de.unika.ipd.grgen.ast.util.CollectChecker;
import de.unika.ipd.grgen.ast.util.SimpleChecker;
import de.unika.ipd.grgen.ir.EnumType;
import de.unika.ipd.grgen.ir.IR;
import de.unika.ipd.grgen.ir.Ident;

/**
 * An enumeration type AST node.
 */
public class EnumTypeNode extends BasicTypeNode {
	
	static {
		setName(EnumTypeNode.class, "enum type");
	}
	
	/**
	 * Index of the elemets' collect node.
	 */
	private static final int ELEMENTS = 0;
	
	private static final Checker childrenChecker = 
	 	new CollectChecker(new SimpleChecker(IdentNode.class));
	
  /**
   * @see de.unika.ipd.grgen.ast.BaseNode#check()
   */
  protected boolean check() {
    return checkChild(ELEMENTS, childrenChecker);
  }

  /**
   * @see de.unika.ipd.grgen.ast.BaseNode#constructIR()
   */
  protected IR constructIR() {
  	Ident name = (Ident) getIdentNode().checkIR(Ident.class);
  	EnumType ty = new EnumType(name);
  	for(Iterator i = getChild(ELEMENTS).getChildren(); i.hasNext();) {
  		BaseNode child = (BaseNode) i.next();
			Ident id = (Ident) child.checkIR(Ident.class);
  		ty.addItem(id);
  	}
    return ty;
  }

  /**
   * @see de.unika.ipd.grgen.ast.TypeNode#coercible(de.unika.ipd.grgen.ast.TypeNode)
   * Enums are not coercible to any type.
   */
  protected boolean coercible(TypeNode t) {
    return false;
  }

}
