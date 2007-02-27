/*
  GrGen: graph rewrite generator tool.
  Copyright (C) 2005  IPD Goos, Universit"at Karlsruhe, Germany

  This library is free software; you can redistribute it and/or
  modify it under the terms of the GNU Lesser General Public
  License as published by the Free Software Foundation; either
  version 2.1 of the License, or (at your option) any later version.

  This library is distributed in the hope that it will be useful,
  but WITHOUT ANY WARRANTY; without even the implied warranty of
  MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the GNU
  Lesser General Public License for more details.

  You should have received a copy of the GNU Lesser General Public
  License along with this library; if not, write to the Free Software
  Foundation, Inc., 51 Franklin St, Fifth Floor, Boston, MA  02110-1301  USA
*/


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
				
				for(InheritanceType inh : t.getDirectSubTypes())
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
