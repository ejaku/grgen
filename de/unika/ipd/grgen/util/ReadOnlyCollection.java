/**
 * ReadOnlyCollection.java
 *
 * @author Sebastian Hack
 */

package de.unika.ipd.grgen.util;

import java.util.Collection;
import java.util.Iterator;

/**
 * A read only mascerade for sets.
 */
public class ReadOnlyCollection implements Collection {

	private final Collection coll;
	
	private final class ReadOnlyIterator implements Iterator {

		private final Iterator it = coll.iterator();
		
		public void remove() {
		}
		
		public boolean hasNext() {
			return it.hasNext();
		}
		
		public Object next() {
			return it.next();
		}
	}
	
	public ReadOnlyCollection(Collection coll) {
		this.coll = coll;
	}
	
	public int size() {
		return coll.size();
	}
	
	public void clear() {
	}
	
	public boolean isEmpty() {
		return coll.isEmpty();
	}
	
	public Object[] toArray() {
		return coll.toArray();
	}
	
	public boolean add(Object p1) {
		return false;
	}
	
	public boolean contains(Object p1) {
		return coll.contains(p1);
	}
	
	public boolean remove(Object p1) {
		return false;
	}
	
	public boolean addAll(Collection p1) {
		return false;
	}
	
	public boolean containsAll(Collection p1) {
		return coll.containsAll(p1);
	}
	
	public boolean removeAll(Collection p1) {
		return false;
	}
	
	public boolean retainAll(Collection p1) {
		return false;
	}
	
	public Iterator iterator() {
		return new ReadOnlyIterator();
	}
	
	public Object[] toArray(Object[] p1) {
		return coll.toArray(p1);
	}
	
	
	
	
}

