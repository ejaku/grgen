/**
 * @author shack
 * @version $Id$
 */
package de.unika.ipd.grgen.ir;
import java.util.HashSet;
import java.util.Iterator;
import java.util.Set;
import java.util.Map;
import java.util.List;
import java.util.LinkedList;
import java.util.Collections;
import java.util.Comparator;

/**
 * An edge type.
 */
public class EdgeType extends InheritanceType {
	
	/** The connection assertions. */
	private final List connectionAsserts = new LinkedList();
	
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
	 * euivalent connection aasertions. The order of the sorting is given
	 * by the <code>compareTo</code> method.
	 *
	 */
	public void canonicalizeConnectionAsserts()
	{
		Collections.sort(connectionAsserts, new Comparator() {
			public int compare(Object o1, Object o2) {
				ConnAssert ca1 = (ConnAssert) o1;
				ConnAssert ca2 = (ConnAssert) o2;
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
	public Iterator getConnAsserts() {
		return connectionAsserts.iterator();
	}
	
	public void addFields(Map fields) {
		super.addFields(fields);
		fields.put("conn_asserts", connectionAsserts.iterator());
	}
	
	void addToDigest(StringBuffer sb) {

		super.addToDigest(sb);

		sb.append('[');
		int i = 0;
		for(Iterator it = connectionAsserts.iterator(); it.hasNext(); i++) {
			ConnAssert ca = (ConnAssert) it.next();
			if(i > 0)
				sb.append(',');
			sb.append(ca.toString());
		}
		 sb.append(']');
	}
}
