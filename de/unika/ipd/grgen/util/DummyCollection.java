/**
 * DummyCollection.java
 *
 * @author Sebastian Hack
 */

package de.unika.ipd.grgen.util;

import java.util.Collection;
import java.util.LinkedList;

/**
 * A collection which ignores all modification
 * attempts.
 */
public class DummyCollection extends ReadOnlyCollection {

	public static final Collection EMPTY = new DummyCollection(new LinkedList());
	
	public static final Collection get(Collection c) {
		return new DummyCollection(c);
	}
	
	private DummyCollection(Collection c) {
		super(c, false);
	}
	
}

