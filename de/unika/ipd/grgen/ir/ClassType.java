/**
 * @author shack
 * @version $Id$
 */
package de.unika.ipd.grgen.ir;

import java.util.Iterator;
import java.util.LinkedList;
import java.util.List;

import de.unika.ipd.grgen.util.MultiIterator;

/**
 * A class that represents a class.
 * That is a compound with inheritance. 
 */
public class ClassType extends CompoundType {

	private List extendsTypes;

  /**
   * Make a new class.
   * @param name The name of the class type.
   * @param ident The ident used to declare this clas.
   */
  public ClassType(String name, Ident ident) {
    super(name, ident);
    extendsTypes = new LinkedList();
  }

  /**
   * @see de.unika.ipd.grgen.util.Walkable#getWalkableChildren()
   */
  public Iterator getWalkableChildren() {
  	return new MultiIterator(new Iterator[] {
	  	super.getWalkableChildren(),
	  	extendsTypes.iterator()
  	});
  }
}
