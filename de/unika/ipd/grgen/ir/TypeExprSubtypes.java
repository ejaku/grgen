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
import java.util.List;

public class TypeExprSubtypes extends TypeExpr {
	
	private final InheritanceType type;
	
	private Collection result = null;
	
	public TypeExprSubtypes(InheritanceType type) {
		this.type = type;
	}
	
	public Collection evaluate() {
		if(result == null) {
			Collection res = new HashSet();
			List worklist = new LinkedList();

			worklist.add(type);
			
			while(!worklist.isEmpty()) {
				InheritanceType t = (InheritanceType) worklist.remove(0);
				
				for(Iterator i = t.getSubTypes(); i.hasNext();) {
					InheritanceType inh = (InheritanceType) i.next();
					
					if(!res.contains(inh)) {
						res.add(inh);
						worklist.add(inh);
					}
				}
			}
			
			result = res;
		}
		
		return result;
	}

}
