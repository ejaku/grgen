/**
 * @author shack
 * @version $Id$
 */
package de.unika.ipd.grgen.ir;

import java.util.*;

import de.unika.ipd.grgen.util.MultiIterator;

/**
 * A IR class that represents types that inherit from other types.
 */
public abstract class InheritanceType extends CompoundType {

	public static final int ABSTRACT = 1;
	public static final int CONST = 2;
	
	private int maxDist = -1;
	private final List orderedSuperTypes = new LinkedList();
	private final Set superTypes = new HashSet();
	private final Set subTypes = new HashSet();
	
	/** The type modifiers. */
	private final int modifiers;

  /**
   * @param name The name of the type.
   * @param ident The identifier, declaring this type;
   */
  protected InheritanceType(String name, Ident ident, int modifiers) {
    super(name, ident);
    this.modifiers = modifiers;
  }
  
  /**
   * Is this inheritance type the root of a ingeritance hierachy.
   * @return true, if this type does not inherit from some other type.
   */
  public boolean isRoot() {
  	return superTypes.isEmpty();
  }

	/**
	 * Add a type, this type inherits from.
	 * @param t The supertype.
	 */
	public void addSuperType(InheritanceType t) {
		superTypes.add(t);
		orderedSuperTypes.add(t);
		t.subTypes.add(this);
	}
	
	/**
	 * Get an iterator over all types, this type inherits from.
	 * @return The iterator.
	 */
	public Iterator getSuperTypes() {
		return orderedSuperTypes.iterator();
	}
	
	/**
	 * Get all subtypes of this type.
	 * @return An iterator iterating over all sub types of this one.
	 */
	public Iterator getSubTypes() {
		return subTypes.iterator();
	}
	
	/**
	 * Check, if this type is a direct sub type of another type.
	 * This means, that this type inherited from the other type.
	 * @param t The other type.
	 * @return true, iff this type inherited from <code>t</code>.
	 */
	public boolean isDirectSubTypeOf(InheritanceType t) {
		return superTypes.contains(t);
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
				for(Iterator it = getSuperTypes(); it.hasNext();) {
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
			superTypes.iterator()
		});
  }
  
  /**
   * Get the maximum distance to the root inheritance type.
   * This method returns the length of the longest path (considering the inheritance
   * relation) from this type to the root type.
   * @return The length of the longest path to the root type.
   */
  public final int getMaxDist() {

  	if(maxDist == -1) {
  		maxDist = 0;
  		
  		for(Iterator it = orderedSuperTypes.iterator(); it.hasNext();) {
  			InheritanceType inh = (InheritanceType) it.next();
  			int dist = inh.getMaxDist() + 1;
  			maxDist = dist > maxDist ? dist : maxDist;
  		}
  	}
  	
  	return maxDist;
  }
  
  /**
   * Check, if this type is abstract.
   * If a type is abstract, no entities of this types may be instantiated.
   * Its body must also be empty.
   * @return true, if this type is abstract, false if not.
   */
  public final boolean isAbstract() {
  	return (modifiers & ABSTRACT) != 0;
  }
  
  /**
   * Check, if this type is const.
   * Members of entities of a const type may not be modified.
   * @return true, if this type is const, false if not.
   */
  public final boolean isConst() {
  	return (modifiers & CONST) != 0;
  }
	
	public void addFields(Map fields) {
		super.addFields(fields);
		fields.put("inherits", superTypes.iterator());
		fields.put("const", Boolean.valueOf(isConst()));
		fields.put("abstract ", Boolean.valueOf(isAbstract()));
	}
	

}
