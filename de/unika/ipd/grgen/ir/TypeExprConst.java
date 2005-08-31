/**
 * TypeExprConst.java
 *
 * @author Sebastian Hack
 */

package de.unika.ipd.grgen.ir;

import java.util.Collection;
import java.util.Collections;
import java.util.HashSet;

public class TypeExprConst extends TypeExpr {
	
	private final Collection<InheritanceType> types = new HashSet<InheritanceType>();
	
	public void addOperand(InheritanceType t) {
		types.add(t);
	}
	
	public Collection<InheritanceType> evaluate() {
		return Collections.unmodifiableCollection(types);
	}
	
}

