/**
 * @author shack
 * @version $Id$
 */
package de.unika.ipd.grgen.ir;
import java.util.HashSet;
import java.util.Iterator;
import java.util.Set;
import java.util.Map;

/**
 * An edge type.
 */
public class EdgeType extends InheritanceType {
	
	/** The connection assertions. */
	private final Set connectionAsserts = new HashSet();
	
	/**
	 * Make a new edge type.
	 * @param ident The identifier declaring this type.
	 */
	public EdgeType(Ident ident, int modifiers) {
		super("edge type", ident, modifiers);
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
	
	
	
}
