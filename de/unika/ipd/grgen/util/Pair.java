/**
 * Pair.java
 *
 * @author Created by Omnicore CodeGuide
 */

package de.unika.ipd.grgen.util;

public class Pair<T,S>
{
	public T first;
	public S second;
	
	public Pair() {
		first = null;
		second = null;
	}

	public Pair(T f, S s) {
		first = f;
		second = s;
	}
}

