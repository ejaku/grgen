/**
 * ReadOnlyIterator.java
 *
 * @author Sebastian Hack
 */

package de.unika.ipd.grgen.util;

import java.util.Iterator;

public final class ReadOnlyIterator implements Iterator {
	
	private final Iterator it;

	public ReadOnlyIterator(Iterator it) {
		this.it = it;
	}
	
	public void remove() {
	}
	
	public boolean hasNext() {
		return it.hasNext();
	}
	
	public Object next() {
		return it.next();
	}
}

