/**
 * @author shack
 * @version $Id$
 */
package de.unika.ipd.grgen.ir;

import java.util.HashSet;
import java.util.Iterator;
import java.util.LinkedList;
import java.util.List;
import java.util.Set;

import de.unika.ipd.grgen.util.MultiIterator;

/**
 * A IR class that represents types that inherit from other types.
 */
public abstract class InheritanceType extends CompoundType {

	private Set inherits;
	private List orderedInherits;

  /**
   * @param name The name of the type.
   * @param ident The identifier, declaring this type;
   */
  protected InheritanceType(String name, Ident ident) {
    super(name, ident);
    inherits = new HashSet();
    orderedInherits = new LinkedList();
  }
  
  /**
   * Is this inheritance type the root of a ingeritance hierachy.
   * @return true, if this type does not inherit from some other type.
   */
  public boolean isRoot() {
  	return inherits.isEmpty();
  }

	/**
	 * Add a type, this type inherits from.
	 * @param t The supertype.
	 */
	public void addInherits(InheritanceType t) {
		inherits.add(t);
		orderedInherits.add(t);
	}
	
	/**
	 * Get an iterator over all types, this type inherits from.
	 * @return The iterator.
	 */
	public Iterator getInherits() {
		return orderedInherits.iterator();
	}
	
	/**
	 * Check, if this type is a direct sub type of another type.
	 * This means, that this type inherited from the other type.
	 * @param t The other type.
	 * @return true, iff this type inherited from <code>t</code>.
	 */
	public boolean isDirectSubTypeOf(InheritanceType t) {
		return inherits.contains(t);
	}
	
	/**
	 * Check, if this type is a direct super type of another type.
	 * @param t The other type
	 * @return true, iff <code>t</code> inherits from this type.
	 */
	public boolean isDirectSuperTypeOf(InheritanceType t) {
		return t.isDirectSubTypeOf(this);
	}
	
	/**
	 * Check, if this inheritance type is castable to another one.
	 * This means, that this type must be a sub type <code>t</code>.
	 * @see de.unika.ipd.grgen.ir.Type#castableTo(de.unika.ipd.grgen.ir.Type)
	 */
	protected boolean castableTo(Type t) {
		boolean res = false;
		
		if(t instanceof InheritanceType) {
			InheritanceType ty = (InheritanceType) t;
			
			if(isDirectSubTypeOf(ty))
				res = true;
			else {
				for(Iterator it = getInherits(); it.hasNext();) {
					InheritanceType inh = (InheritanceType) it.next();
					if(inh.castableTo(ty)) {
						res = true;
						break;
					}
				}
			}
		}
		
		return res;
	}

  /**
   * @see de.unika.ipd.grgen.util.Walkable#getWalkableChildren()
   */
  public Iterator getWalkableChildren() {
		return new MultiIterator(new Iterator[] {
			super.getWalkableChildren(),
			inherits.iterator()
		});
  }

}
