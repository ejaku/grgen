/**
 * @file TypeNode.java
 * @author shack
 * @date Jul 6, 2003
 */
package de.unika.ipd.grgen.ast;

import java.awt.Color;

import de.unika.ipd.grgen.ir.Type;

/**
 * An AST node representing a type 
 */
public abstract class TypeNode extends BaseNode {

	public TypeNode() {
		super();
	}

	/**
	 * Coercible method to be implemented by subclasses.
	 * The implementers do not have to check for type equality, that is done
	 * in {@link #isCoercible(TypeNode)}.
	 * @param t The type
	 * @return True, if <code>t</code> is castable to this type.
	 */	
	protected boolean coercible(TypeNode t) {
		return false;
	}
	
	/**
	 * Check, if this <code>t</code> is castable to this type or is equal
	 * to this type.
	 * @param t A type.
	 * @return true, if <code>t</code> is castable or equal to this type.
	 */
	public final boolean isCoercible(TypeNode t) {
		return isEqual(t) || coercible(t);
	}
	
	public Color getNodeColor() {
		return Color.MAGENTA;
	}
	
	/**
	 * Get the ir object as type.
	 * The cast must always succeed.
	 * @return The ir object as type.
	 */
	public Type getType() {
		return (Type) checkIR(Type.class);
	}
	
	/**
	 * Checks, if two types are equal.
	 * @param t The type to check for.
	 * @return true, if this and <code>t</code> are of the same type.
	 */
	public final boolean isEqual(TypeNode t) {
		return getClass().isInstance(t);
	}
	
	/** 
	 * Check, if the type is a basic type (integer, boolean, string, void).
	 * @return true, if the type is a basic type.
	 */
	public boolean isBasic() {
		return false;
	}
}
