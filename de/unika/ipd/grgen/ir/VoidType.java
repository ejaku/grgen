/**
 * @author Sebastian Hack
 * @version $Id$
 */
package de.unika.ipd.grgen.ir;

/**
 * The void type.
 */
public class VoidType extends PrimitiveType {

  public VoidType(Ident ident) {
    super("void type", ident);
  }

	public boolean isVoid() {
		return true;
	}
	
	public boolean isEqual(Type t) {
		return t.isVoid();
	}

}
