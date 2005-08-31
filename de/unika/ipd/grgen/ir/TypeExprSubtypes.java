/**
 * TypeExprSubtypes.java
 *
 * @author Sebastian Hack
 */

package de.unika.ipd.grgen.ir;

import java.util.Collection;
import java.util.HashSet;
import java.util.Iterator;
import java.util.LinkedList;

public class TypeExprSubtypes extends TypeExpr {
	
	private final InheritanceType type;
	
	private Collection<InheritanceType> result = null;
	
	public TypeExprSubtypes(InheritanceType type) {
		this.type = type;
	}
	
	public Collection<InheritanceType> evaluate() {
		if(result == null) {
			Collection<InheritanceType> res = new HashSet<InheritanceType>();
			LinkedList<InheritanceType> worklist = new LinkedList<InheritanceType>();
			
			worklist.add(type);
			
			while(!worklist.isEmpty()) {
				InheritanceType t = worklist.removeFirst();
				
				for(InheritanceType inh : t.getSubTypes())
					if(!res.contains(inh)) {
						res.add(inh);
						worklist.add(inh);
					}
			}
			result = res;
		}
		return result;
	}
	
}
