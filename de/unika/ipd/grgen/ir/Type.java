/**
 * @author shack
 * @version $Id$
 */
package de.unika.ipd.grgen.ir;

/**
 * A node representing a type.
 * Subclasses will be primitive type (string, int, boolean)
 * group type
 * action type
 * graph type (node and edge) 
 */
public abstract class Type extends Identifiable {

	static final private Type global = 
		new Type("global type", Ident.get("global type")) {
	};
	
	static Type getGlobal() {
		return global;
	}

	/** The identifier used to declare this type */
	private Ident ident;

  /**
   * Make a new type.
   * @param name The name of the type (test, group, ...).
   * @param ident The identifier used to declare that type.
   */
  public Type(String name, Ident ident) {
  	super(name, ident);
  }
  
  /**
   * Decides, if two types are equal.
   * @param t The other type.
   * @return true, if the types are equal.
   */
  public boolean isEqual(Type t) {
  	return t == this;
  }
  
  /**
   * Compute, if this type is castable to another type.
   * You do not have to check, if <code>t == this</code>.
   * @param t The other type.
   * @return true, if this type is castable.
   */
  protected boolean castableTo(Type t) {
  	return false;
  }
  
  /**
   * Checks, if this type is castable to another type.
   * This method is final, to implement the castability, overwrite
   * <code>castableTo</code>. It is called by this method. 
   * @param t The other type.
   * @return true, if this type can be casted to <code>t</code>, false
   * otherwise.
   */
  public final boolean isCastableTo(Type t) {
  	return isEqual(t) || castableTo(t);
  }
}
