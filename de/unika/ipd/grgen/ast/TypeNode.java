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
 * @date Jul 6, 2003
 * @author Sebastian Hack
 * @version $Id$
 */
package de.unika.ipd.grgen.ast;

import de.unika.ipd.grgen.ir.Type;
import java.awt.Color;
import java.util.Collection;
import java.util.HashSet;
import java.util.Set;
import java.util.Map;
import java.util.HashMap;

/**
 * An AST node representing a type
 */
public abstract class TypeNode extends BaseNode {


	/**
	 * A map, that maps each type to a set to all other types,
	 * that are castable to the type.
	 */
	protected static final Map<TypeNode, HashSet> castableMap = new HashMap<TypeNode, HashSet>();
	
	
	TypeNode() {
		super();
	}
	
	/**
	 * Check, if this type is compatible (implicitly castable) or equal
	 * to <code>t</code>.
	 * @param t A type.
	 * @return true, if this type is compatible or equal to <code>t</code>
	 */
	public boolean isCompatibleTo(TypeNode t) {
		Set<TypeNode> compat = new HashSet<TypeNode>();
		getCompatibleToTypes(compat);
		return (this.isEqual(t)) || compat.contains(t);
	}
	
	/**
	 * Check, if this type is only castable (explicitly castable)
	 * to <code>t</code>
	 * @param t A type.
	 * @return true, if this type is just castable to <code>t</code>.
	 */
	public boolean isCastableTo(TypeNode t) {
		Set<TypeNode> castable = new HashSet<TypeNode>();
		getCastableToTypes(castable);
		return castable.contains(t);
	}
	
	public Color getNodeColor() {
		return Color.MAGENTA;
	}
	
	/**
	 * Get the ir object as type.
	 * The cast must always succeed.
	 * @return The ir object as type.
	 */
	public Type getType() {
		return (Type) checkIR(Type.class);
	}
	
	/**
	 * Checks, if two types are equal.
	 * @param t The type to check for.
	 * @return true, if this and <code>t</code> are of the same type.
	 */
	public boolean isEqual(TypeNode t) {
		return getClass().equals(t.getClass());
	}
	
	/**
	 * Check, if the type is a basic type (integer, boolean, string, void).
	 * @return true, if the type is a basic type.
	 */
	public boolean isBasic() {
		return false;
	}
	
	/**
	 * Put all compatible types which are compatible to this one in a collection
	 * @param coll The collection to put the compatible types to.
	 */
	public final void getCompatibleToTypes(Collection<TypeNode> coll) {
		coll.add(this);
		doGetCompatibleToTypes(coll);
	}
	
	protected static void addTypeToMap(Map<TypeNode, HashSet> map, TypeNode index, TypeNode target)
	{
		if(!map.containsKey(index))
			map.put(index, new HashSet());
		
		Set<TypeNode> s = map.get(index);
		s.add(target);
	}
	
	public static void addCastability(TypeNode from, TypeNode to)
	{
		addTypeToMap(castableMap, from, to);
	}
	
	protected void doGetCompatibleToTypes(Collection<TypeNode> coll) {
	}
	
	/**
	 * Pit all types this one is castable (implicitly and explicitly) to
	 * into a collection.
	 * @param coll A collection they are put into.
	 */
	public final void getCastableToTypes(Collection<TypeNode> coll) {
		doGetCastableToTypes(coll);
		getCompatibleToTypes(coll);
	}
	
	private void doGetCastableToTypes(Collection<TypeNode> coll) {
		Object obj = castableMap.get(this);
		if(obj != null)
			coll.addAll((Collection) obj);
	}
	
	/**
	 * Cast a constant of this type to another type.
	 * @param constant The constant. Its type must be equal to this.
	 * @return A new constant, that represents <code>constant</code> in a new
	 * type.
	 */
	protected final ConstNode cast(TypeNode newType, ConstNode constant) {
		TypeNode constType = constant.getType();
		ConstNode res = ConstNode.getInvalid();
		
		if(isEqual(constType)) {
			if(newType.isEqual(constType))
				res = constant;
			else if(isCastableTo(newType))
				res = doCast(newType, constant);
			else
				res = ConstNode.getInvalid();
		}
		
		return res;
	}
	
	/**
	 * Implement this method for your types to implement casts
	 * of constants.
	 * @param constant A constant.
	 * @return The type casted constant.
	 */
	protected ConstNode doCast(TypeNode newType, ConstNode constant) {
		return ConstNode.getInvalid();
	}
	
}
