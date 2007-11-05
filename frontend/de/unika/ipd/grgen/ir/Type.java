/*
  GrGen: graph rewrite generator tool.
  Copyright (C) 2005  IPD Goos, Universit"at Karlsruhe, Germany

  This library is free software; you can redistribute it and/or
  modify it under the terms of the GNU Lesser General Public
  License as published by the Free Software Foundation; either
  version 2.1 of the License, or (at your option) any later version.

  This library is distributed in the hope that it will be useful,
  but WITHOUT ANY WARRANTY; without even the implied warranty of
  MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the GNU
  Lesser General Public License for more details.

  You should have received a copy of the GNU Lesser General Public
  License along with this library; if not, write to the Free Software
  Foundation, Inc., 51 Franklin St, Fifth Floor, Boston, MA  02110-1301  USA
*/


/**
 * @author shack
 * @version $Id$
 */
package de.unika.ipd.grgen.ir;

import java.util.Comparator;


/**
 * A node representing a type.
 * Subclasses will be primitive type (string, int, boolean)
 * group type
 * action type
 * graph type (node and edge)
 */
public abstract class Type extends Identifiable implements Comparable {

	public static final int IS_UNKNOWN = 0;
	public static final int IS_INTEGER = 1;
	public static final int IS_FLOAT = 2;
	public static final int IS_DOUBLE = 3;
	public static final int IS_BOOLEAN = 4;
	public static final int IS_STRING  = 5;
	public static final int IS_TYPE  = 6;
	public static final int IS_OBJECT = 7;
	
	/** The identifier used to declare this type */
	private Ident ident;

  /**
   * Make a new type.
   * @param name The name of the type (test, group, ...).
   * @param ident The identifier used to declare that type.
   */
  public Type(String name, Ident ident) {
  	super(name, ident);
  }
 
  /**
   * Decides, if two types are equal.
   * @param t The other type.
   * @return true, if the types are equal.
   */
  public boolean isEqual(Type t) {
  	return t == this;
  }
  
  /**
   * Compute, if this type is castable to another type.
   * You do not have to check, if <code>t == this</code>.
   * @param t The other type.
   * @return true, if this type is castable.
   */
  protected boolean castableTo(Type t) {
  	return false;
  }
  
  /**
   * Checks, if this type is castable to another type.
   * This method is final, to implement the castability, overwrite
   * <code>castableTo</code>. It is called by this method.
   * @param t The other type.
   * @return true, if this type can be casted to <code>t</code>, false
   * otherwise.
   */
  public final boolean isCastableTo(Type t) {
  	return isEqual(t) || castableTo(t);
  }
  
  /**
   * Check, if this type is a void type.
   * In fact, there can be more void types, so use this method to check
   * for a void type.
   * @return true, if the type is void.
   */
  public boolean isVoid() {
  	return false;
  }
  
  /**
   * Return a classification of a type for the IR.
   * @return either IS_UNKNOWN, IS_INTEGER, IS_BOOLEAN, IS_STRING or IS_TYPE
   */
  public int classify() {
  	return IS_UNKNOWN;
  }
  
	private static final Comparator<Type> COMPARATOR = new Comparator() {

		public int compare(Object o1, Object o2) {
			Type t1 = (Type) o1;
			Type t2 = (Type) o2;
			
			if(t1.isEqual(t2))
				return 0;

			if((t1 instanceof InheritanceType) && (t2 instanceof InheritanceType)) {
				int distT1 = ((InheritanceType) t1).getMaxDist();
				int distT2 = ((InheritanceType) t2).getMaxDist();

				if(distT1 < distT2)
					return -1;
				else if(distT1 > distT2)
					return 1;
			}
			
			return t1.getIdent().compareTo(t2.getIdent());
		}
	};
	
	static final Comparator<Type> getComparator() {
		return COMPARATOR;
	}
	
	public int compareTo(Object obj) {
		return COMPARATOR.compare(this, (Type) obj);
	}
	
}
