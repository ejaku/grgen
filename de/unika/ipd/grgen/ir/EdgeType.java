/**
 * @author shack
 * @version $Id$
 */
package de.unika.ipd.grgen.ir;
import java.util.HashSet;
import java.util.Iterator;
import java.util.Set;

/**
 *
 */
public class EdgeType extends InheritanceType {
	
	private Set cas;
	
	/**
	 * Make a new edge type.
	 * @param ident The identifier declaring this type.
	 */
	public EdgeType(Ident ident, int modifiers) {
		super("edge type", ident, modifiers);
		cas = new HashSet();
	}
	
	public void addConnAssert(ConnAssert ca) {
		cas.add(ca);
	}
	
	public Iterator getConnAsserts() {
		return cas.iterator();
	}
}
