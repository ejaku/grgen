/**
 * ReadOnlyCollection.java
 *
 * @author Sebastian Hack
 */

package de.unika.ipd.grgen.util;

import de.unika.ipd.grgen.util.CacheMap;
import java.util.Collection;
import java.util.Iterator;
import java.util.LinkedList;
import java.util.Map;

/**
 * A read only mascerade for collections.
 */
public class ReadOnlyCollection implements Collection {

	private final Collection coll;
	private final boolean strict;
	
	private static final Map cache = new CacheMap(127);
	
	public static final Collection EMPTY
		= new ReadOnlyCollection(new LinkedList(), false);
	
	public static Collection get(Collection coll) {
		if(cache.containsKey(coll))
			return (Collection) cache.get(coll);
		else {
			Collection res = new ReadOnlyCollection(coll, true);
			cache.put(coll, res);
			return res;
		}
	}
	
	public static Collection getSingle(Collection coll) {
		return new ReadOnlyCollection(coll, true);
	}
	
	protected ReadOnlyCollection(Collection coll, boolean strict) {
		this.coll = coll;
		this.strict = strict;
	}
	
	private final void modify() {
		if(strict)
			throw new UnsupportedOperationException("collection not writable");
	}
	
	public int size() {
		return coll.size();
	}
	
	public void clear() {
		modify();
	}
	
	public boolean isEmpty() {
		return coll.isEmpty();
	}
	
	public Object[] toArray() {
		return coll.toArray();
	}
	
	public boolean add(Object p1) {
		modify();
		return false;
	}
	
	public boolean contains(Object p1) {
		return coll.contains(p1);
	}
	
	public boolean remove(Object p1) {
		modify();
		return false;
	}
	
	public boolean addAll(Collection p1) {
		modify();
		return false;
	}
	
	public boolean containsAll(Collection p1) {
		return coll.containsAll(p1);
	}
	
	public boolean removeAll(Collection p1) {
		modify();
		return false;
	}
	
	public boolean retainAll(Collection p1) {
		modify();
		return false;
	}
	
	public Iterator iterator() {
		return new ReadOnlyIterator(coll.iterator());
	}
	
	public Object[] toArray(Object[] p1) {
		return coll.toArray(p1);
	}
	
	public String toString() {
		return coll.toString();
	}
	
	
	
}

