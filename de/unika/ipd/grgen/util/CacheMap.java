/**
 * CacheMap.java
 *
 * @author Sebastian Hack
 */

package de.unika.ipd.grgen.util;

import java.util.HashMap;
import java.util.Iterator;
import java.util.LinkedList;
import java.util.List;
import java.util.Map;

public class CacheMap extends HashMap {
	
	private int maxEntries;
	
	private final List queue = new LinkedList();
	
	public CacheMap(int maxEntries) {
		this.maxEntries = maxEntries;
	}
	
	/**
	 * Associates the specified value with the specified key in this map.
	 * If the map previously contained a mapping for this key, the old
	 * value is replaced.
	 *
	 * @param key key with which the specified value is to be associated.
	 * @param value value to be associated with the specified key.
	 * @return previous value associated with specified key, or <tt>null</tt>
	 *	       if there was no mapping for key.  A <tt>null</tt> return can
	 *	       also indicate that the HashMap previously associated
	 *	       <tt>null</tt> with the specified key.
	 */
	public Object put(Object key, Object value) {
		// Make place in the table by deleting the oldest.
		if(size() >= maxEntries) {
			int last = queue.size() - 1;
			Object toDelete = queue.remove(last);
			super.remove(toDelete);
		}

		queue.add(key);
		return super.put(key, value);
	}
	
	/**
	 * Removes the mapping for this key from this map if present.
	 *
	 * @param  key key whose mapping is to be removed from the map.
	 * @return previous value associated with specified key, or <tt>null</tt>
	 *	       if there was no mapping for key.  A <tt>null</tt> return can
	 *	       also indicate that the map previously associated <tt>null</tt>
	 *	       with the specified key.
	 */
	public Object remove(Object key) {
		return null;
	}
	
	/**
	 * Copies all of the mappings from the specified map to this map
	 * These mappings will replace any mappings that
	 * this map had for any of the keys currently in the specified map.
	 *
	 * @param m mappings to be stored in this map.
	 * @throws NullPointerException if the specified map is null.
	 */
	public void putAll(Map m) {
		for(Iterator i = m.keySet().iterator(); i.hasNext();) {
			Object obj = i.next();
			put(obj, m.get(obj));
		}
	}
	

	
	
}

