/**
 * @author Sebastian Hack
 * @version $Id$
 */
package de.unika.ipd.grgen.ir;

/**
 * The void type. 
 */
public class VoidType extends PrimitiveType {

  public VoidType(String name) {
    super("void type", Ident.get(name));
  }

	public boolean isVoid() {
		return true;
	}
	
	public boolean isEqual(Type t) {
		return t.isVoid();
	}

}
