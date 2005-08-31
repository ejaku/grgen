/**
 * TypeExpr.java
 *
 * @author Sebastian Hack
 */

package de.unika.ipd.grgen.ir;

import java.util.Collection;

public abstract class TypeExpr extends IR {
	
	public TypeExpr() {
		super("type expr");
	}
	
	/**
	 * Evaluate this type expression by returning a set
	 * of all types that are represented by the expression.
	 * @return A collection of types that correspond to the
	 * expression.
	 */
	public abstract Collection<InheritanceType> evaluate();
	
}

