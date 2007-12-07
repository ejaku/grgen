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
 * @author shack
 * @version $Id$
 */
package de.unika.ipd.grgen.ir;
import java.util.*;

/**
 * An edge type.
 */
public class EdgeType extends InheritanceType {
	
	/** The connection assertions. */
	private final List<ConnAssert> connectionAsserts = new LinkedList<ConnAssert>();
		
	/**
	 * Make a new edge type.
	 * @param ident The identifier declaring this type.
	 */
	public EdgeType(Ident ident, int modifiers) {
		super("edge type", ident, modifiers);
	}
	
	/**
	 * Sorts the Connection assertion of this edge type, such that the
	 * computed graph model digest is stable according to semantically
	 * equivalent connection assertions. The order of the sorting is given
	 * by the <code>compareTo</code> method.
	 *
	 */
	public void canonicalizeConnectionAsserts() {
		Collections.sort(connectionAsserts, new Comparator<ConnAssert>() {
					public int compare(ConnAssert ca1, ConnAssert ca2) {
						return ca1.compareTo(ca2);
					}
				});
	}
	
	/**
	 * Add a connection assertion to this edge type.
	 * @param ca The connection assertion.
	 */
	public void addConnAssert(ConnAssert ca) {
		connectionAsserts.add(ca);
	}

	
	
	/**
	 * Get all connection assertions.
	 * @return An iterator iterating over all connection assertions.
	 */
	public Collection<ConnAssert> getConnAsserts() {
		return Collections.unmodifiableCollection(connectionAsserts);
	}
	
	public void addFields(Map<String, Object> fields) {
		super.addFields(fields);
		fields.put("conn_asserts", connectionAsserts.iterator());
	}
	
	void addToDigest(StringBuffer sb) {
		
		super.addToDigest(sb);
		
		sb.append('[');
		int i = 0;
		for(Iterator<ConnAssert> it = connectionAsserts.iterator(); it.hasNext(); i++) {
			ConnAssert ca = it.next();
			if(i > 0)
				sb.append(',');
			sb.append(ca.toString());
		}
		sb.append(']');
	}
}
