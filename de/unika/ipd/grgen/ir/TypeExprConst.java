/**
 * TypeExprConst.java
 *
 * @author Sebastian Hack
 */

package de.unika.ipd.grgen.ir;

import de.unika.ipd.grgen.util.ReadOnlyCollection;
import java.util.Collection;
import java.util.HashSet;

public class TypeExprConst extends TypeExpr {
	
	private final Collection types = new HashSet();
	
	public void addOperand(InheritanceType t) {
		types.add(t);
	}
	
	public Collection evaluate() {
		return ReadOnlyCollection.getSingle(types);
	}
	
}

