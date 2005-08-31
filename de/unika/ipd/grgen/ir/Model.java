/**
 * Model.java
 *
 * @author Sebastian Hack
 */

package de.unika.ipd.grgen.ir;

import java.util.Collections;
import java.util.Iterator;
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
	public Iterator<Type> getTypes() {
		return types.iterator();
	}

	/**
	 * Canonicalize the type model.
	 */
	protected void canonicalizeLocal() {
		// Collections.sort(types, Identifiable.COMPARATOR);
		Collections.sort(types);
		
		for(Iterator<Type> it = types.iterator(); it.hasNext();) {
			Type ty = it.next();
			ty.canonicalize();
			if (ty instanceof EdgeType)
				((EdgeType)ty).canonicalizeConnectionAsserts();
		}
	}

	void addToDigest(StringBuffer sb) {
		sb.append(this);
		sb.append('[');

		for(Iterator<Type> it = types.iterator(); it.hasNext();) {
			Type ty = it.next();
			ty.addToDigest(sb);
		}

		sb.append(']');
	}
	
	public void addFields(Map<String, Object> fields) {
		super.addFields(fields);
		fields.put("types", types.iterator());
	}
	
	
	
	
}

