/**
 * @date Jul 6, 2003
 * @author Sebastian Hack
 * @version $Id$
 */
package de.unika.ipd.grgen.ast;

import de.unika.ipd.grgen.ir.Type;
import java.awt.Color;
import java.util.Collection;
import java.util.HashSet;
import java.util.Set;

/**
 * An AST node representing a type
 */
public abstract class TypeNode extends BaseNode {
	
	TypeNode() {
		super();
	}
	
	/**
	 * Check, if this type is compatible (implicitly castable) or equal
	 * to <code>t</code>.
	 * @param t A type.
	 * @return true, if this type is compatible or equal to <code>t</code>
	 */
	public boolean isCompatibleTo(TypeNode t) {
		Set compat = new HashSet();
		getCompatibleToTypes(compat);
		return isEqual(t) || compat.contains(t);
	}
	
	/**
	 * Check, if this type is only castable (explicitly castable)
	 * to <code>t</code>
	 * @param t A type.
	 * @return true, if this type is just castable to <code>t</code>.
	 */
	public boolean isCastableTo(TypeNode t) {
		Set castable = new HashSet();
		getCastableToTypes(castable);
		return castable.contains(t);
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
	public boolean isEqual(TypeNode t) {
		return getClass().equals(t.getClass());
	}
	
	/**
	 * Check, if the type is a basic type (integer, boolean, string, void).
	 * @return true, if the type is a basic type.
	 */
	public boolean isBasic() {
		return false;
	}
	
	/**
	 * Put all compatible types which are compatible to this one in a collection
	 * @param coll The collection to put the compatible types to.
	 */
	public final void getCompatibleToTypes(Collection coll) {
		coll.add(this);
		doGetCompatibleToTypes(coll);
	}
	
	
	protected void doGetCompatibleToTypes(Collection coll) {
	}
	
	/**
	 * Pit all types this one is castable (implicitly and explicitly) to
	 * into a collection.
	 * @param coll A collection they are put into.
	 */
	public final void getCastableToTypes(Collection coll) {
		doGetCastableToTypes(coll);
		getCompatibleToTypes(coll);
	}
	
	/**
	 * This method must be implemented by subclasses.
	 * You need only put types into the collection, that are subject to
	 * explicit casts. The implicit ones are done by
	 * {@link #getCastableTypes(Collection)}.
	 * @param coll The collection to put them to.
	 */
	protected void doGetCastableToTypes(Collection coll) {
	}
	
	/**
	 * Cast a constant of this type to another type.
	 * @param constant The constant. Its type must be equal to this.
	 * @return A new constant, that represents <code>constant</code> in a new
	 * type.
	 */
	protected final ConstNode cast(TypeNode newType, ConstNode constant) {
		TypeNode constType = constant.getType();
		ConstNode res = ConstNode.getInvalid();
		
		if(isEqual(constType)) {
			if(newType.isEqual(constType))
				res = constant;
			else if(isCastableTo(newType))
				res = doCast(newType, constant);
			else
				res = ConstNode.getInvalid();
		}
		
		return res;
	}
	
	/**
	 * Implement this method for your types to implement casts
	 * of constants.
	 * @param constant A constant.
	 * @return The type casted constant.
	 */
	protected ConstNode doCast(TypeNode newType, ConstNode constant) {
		return ConstNode.getInvalid();
	}
	
}
