/**
 * @author shack
 * @version $Id$
 */
package de.unika.ipd.grgen.ir;
import java.util.Map;



/**
 * A node representing a type.
 * Subclasses will be primitive type (string, int, boolean)
 * group type
 * action type
 * graph type (node and edge)
 */
public abstract class Type extends Identifiable {

	public static final int IS_UNKNOWN = 0;
	public static final int IS_INTEGER = 1;
	public static final int IS_BOOLEAN = 2;
	public static final int IS_STRING  = 3;

	static final private Type global =
		new Type("global type", Ident.get("global type")) {
		public boolean isGlobal() {
			return true;
		}
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
  
  /**
   * Check, if this type is a void type.
   * In fact, there can be more void types, so use this method to check
   * for a void type.
   * @return true, if the type is void.
   */
  public boolean isVoid() {
  	return false;
  }
  
  /**
   * Return a classification of a type for the IR.
   * @return either IS_UNKNOWN, IS_INTEGER, IS_BOOLEAN or IS_STRING
   */
  public int classify() {
  	return IS_UNKNOWN;
  }
  
  /**
   * Is this type the global type?
   * @return true, if this type is the global type, false if not.
   */
  public boolean isGlobal() {
  	return false;
  }

}
