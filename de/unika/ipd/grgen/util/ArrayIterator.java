/**
 * @author shack
 * @version $Id$
 */
package de.unika.ipd.grgen.util;

import java.util.Iterator;

/**
 * Iterator for native arrays
 */
public class ArrayIterator implements Iterator {

	private Object[] arr;
	private int curr; 

  /**
   * Make new array iterator with an ordinary array
   */
  public ArrayIterator(Object[] arr) {
  	this.arr = arr;
  	this.curr = 0;
  }

  /**
   * @see java.util.Iterator#hasNext()
   */
  public boolean hasNext() {
  	return curr < arr.length;
  }

  /**
   * @see java.util.Iterator#next()
   */
  public Object next() {
  	return arr[curr++];
  }

  /**
   * @see java.util.Iterator#remove()
   */
  public void remove() {
  }

}
