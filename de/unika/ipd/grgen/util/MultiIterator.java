/**
 * @author shack
 * @version $Id$
 */
package de.unika.ipd.grgen.util;

import java.util.Collection;
import java.util.Iterator;
import java.util.NoSuchElementException;

/**
 * An iterator that iterates over several other iterators
 */
public class MultiIterator extends Base implements Iterator {

	/** Array of iterators */
	Iterator[] iterators;
	
	/** Current iterator index for <code>iterators</code> */
	int curr;

  /**
   * Make a new Multi iterator.
   * Each element of <code>iterators</code> must be an instance
   * of {@link Iterator}.
   * @param iterators Iterators to iterate over.
   */
  public MultiIterator(Iterator[] iterators) {
  	this.iterators = iterators;
		curr = 0;
  }
  
  public MultiIterator(Collection[] collections) {
  	this.iterators = new Iterator[collections.length];
  	for(int i = 0; i < collections.length; i++)
  		this.iterators[i] = collections[i].iterator();
  	curr = 0;
  }
  
  /**
   * @see java.util.Iterator#hasNext()
   */
  public boolean hasNext() {
  	boolean res = false;
  	for(int i = curr; i < iterators.length; i++)
  		if(iterators[i].hasNext()) {
  			res = true;
  			break;
  		}
  	return res;
  }

  /**
   * @see java.util.Iterator#next()
   */
  public Object next() {
  	
		Object res = null;
		Iterator i = iterators[curr];

		debug.report(NOTE, "curr: " + curr + ", iterators: " + iterators.length);

		if(i.hasNext())
			res = i.next();
		else {
			curr++;
			debug.report(NOTE, "iterator was empty. curr now: " + curr);
			while(curr < iterators.length) {
				i = iterators[curr];
				if(i.hasNext()) {
					res = i.next();
					break;
				} else
					curr++;
			}
		}
		
		if(res == null)
  		throw new NoSuchElementException();
  		
  	return res;
  }

  /**
   * @see java.util.Iterator#remove()
   */
  public void remove() {
  }

}
