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
 * Model.java
 *
 * @author Sebastian Hack
 */

package de.unika.ipd.grgen.ir;

import java.util.Collection;
import java.util.Collections;
import java.util.LinkedList;
import java.util.List;
import java.util.Map;

public class Model extends Identifiable {
	
	private boolean digestValid = false;
	
	private String digest = "";
	
	private List<Type> types = new LinkedList<Type>();
	
	public Model(Ident ident) {
		super("model", ident);
	}
	
	/**
	 * Add a type to the type model.
	 * @param type The type to add to the model.
	 */
	public void addType(Type type) {
		types.add(type);
		digestValid = false;
	}
	
	/**
	 * Get the types in the type model.
	 * @return The types in the type model.
	 */
	public Collection<Type> getTypes() {
		return Collections.unmodifiableCollection(types);
	}
	
	/**
	 * Canonicalize the type model.
	 */
	protected void canonicalizeLocal() {
		// Collections.sort(types, Identifiable.COMPARATOR);
		Collections.sort(types);
		
		for(Type ty : types) {
			ty.canonicalize();
			if (ty instanceof EdgeType)
					((EdgeType)ty).canonicalizeConnectionAsserts();
		}
	}
	
	void addToDigest(StringBuffer sb) {
		sb.append(this);
		sb.append('[');
		
		for(Type ty : types) {
			ty.addToDigest(sb);
		}
		
		sb.append(']');
	}
	
	public void addFields(Map<String, Object> fields) {
		super.addFields(fields);
		fields.put("types", types.iterator());
	}
	
	
	
	
}

