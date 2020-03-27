/*
 * GrGen: graph rewrite generator tool -- release GrGen.NET 4.5
 * Copyright (C) 2003-2020 Universitaet Karlsruhe, Institut fuer Programmstrukturen und Datenorganisation, LS Goos; and free programmers
 * licensed under LGPL v3 (see LICENSE.txt included in the packaging of this file)
 * www.grgen.net
 */

/**
 * @author Sebastian Hack
 */

package de.unika.ipd.grgen.ast;

import de.unika.ipd.grgen.ast.containers.ArrayTypeNode;
import de.unika.ipd.grgen.ast.containers.DequeTypeNode;
import de.unika.ipd.grgen.ast.containers.MapTypeNode;
import de.unika.ipd.grgen.ast.containers.SetTypeNode;
import de.unika.ipd.grgen.ir.Type;
import java.awt.Color;
import java.util.Collection;
import java.util.Collections;
import java.util.HashMap;
import java.util.HashSet;
import java.util.Map;
import java.util.Set;

/**
 * Base class for all AST nodes representing types.
 */
public abstract class TypeNode extends BaseNode {
	/** A map, that maps each basic type to a set of all other basic types
	 *  that are compatible to the type. */
	private static final Map<TypeNode, HashSet<TypeNode>> compatibleMap =
		new HashMap<TypeNode, HashSet<TypeNode>>();

	/** A map, that maps each type to a set of all other types
	 * that are castable to the type. */
	private static final Map<TypeNode, HashSet<TypeNode>> castableMap =
		new HashMap<TypeNode, HashSet<TypeNode>>();

	// Cache variables
	private Set<TypeNode> compatibleToTypes;
	private Set<TypeNode> castableToTypes;

	public static String getUseStr() {
		return "type";
	}

	/** @see de.unika.ipd.grgen.ast.BaseNode#resolveLocal() */
	@Override
	protected boolean resolveLocal() {
		return true;
	}

	/** @see de.unika.ipd.grgen.ast.BaseNode#checkLocal() */
	@Override
	protected boolean checkLocal() {
		return true;
	}

	/**
	 * Compute the distance of indirect type compatibility (where 'compatibility'
	 * means implicit castability of attribute types; accordingly the distance
	 * means the required number of implicit type casts).
	 * <br><bf>Note</bf> that this method only supports indirections of a
	 * distance upto two. If you need more you have to implement this!
	 *
	 * @param type	a TypeNode
	 *
	 * @return		the compatibility distance, Integer.MAX_VALUE if no compatibility could
	 * 				be found
	 */
	public int compatibilityDistance(TypeNode type) {
		if ( this.isEqual(type) )
			return 0;
		if ( this.isCompatibleTo(type) )
			return 1;

		for (TypeNode t : getCompatibleToTypes()) {
			if (t.isCompatibleTo(type))
				return 2;
		}

		return Integer.MAX_VALUE;
	}

	/**
	 * Check, if this type is compatible (implicitly castable) or equal to <code>t</code>.
	 * @param t A type.
	 * @return true, if this type is compatible or equal to <code>t</code>
	 */
	public boolean isCompatibleTo(TypeNode t) {
		if(isEqual(t))
			return true;

		return getCompatibleToTypes().contains(t);
	}

	/**
	 * Check, if this type is only castable (explicitly castable)
	 * to <code>t</code>
	 * @param t A type.
	 * @return true, if this type is just castable to <code>t</code>.
	 */
	public boolean isCastableTo(TypeNode t) {
		return getCastableToTypes().contains(t);
	}

	@Override
	public Color getNodeColor() {
		return Color.MAGENTA;
	}

	/**
	 * Get the IR object as type.
	 * The cast must always succeed.
	 * @return The IR object as type.
	 */
	public Type getType() {
		return checkIR(Type.class);
	}

	/**
	 * Checks, if two types are equal.
	 * @param t The type to check for.
	 * @return true, if this and <code>t</code> are of the same type.
	 */
	public boolean isEqual(TypeNode t) {
		if(t == this)
			return true;
		else if(t instanceof SetTypeNode && this instanceof SetTypeNode)
			return ((SetTypeNode)t).valueType == ((SetTypeNode)this).valueType;
		else if(t instanceof MapTypeNode && this instanceof MapTypeNode)
			return ((MapTypeNode)t).keyType == ((MapTypeNode)this).keyType
				&& ((MapTypeNode)t).valueType == ((MapTypeNode)this).valueType;
		else if(t instanceof ArrayTypeNode && this instanceof ArrayTypeNode)
			return ((ArrayTypeNode)t).valueType == ((ArrayTypeNode)this).valueType;
		else if(t instanceof DequeTypeNode && this instanceof DequeTypeNode)
			return ((DequeTypeNode)t).valueType == ((DequeTypeNode)this).valueType;
		else
			return false;
	}

	/**
	 * Check, if the type is a basic type (integer, boolean, string, void).
	 * @return true, if the type is a basic type.
	 */
	public boolean isBasic() {
		return false;
	}

	/**
	 * Returns a collection of all compatible types which are compatible to this one.
	 */
	public final Collection<TypeNode> getCompatibleToTypes() {
		if(compatibleToTypes == null) {
			compatibleToTypes = new HashSet<TypeNode>();
			doGetCompatibleToTypes(compatibleToTypes);
			compatibleToTypes.add(this);
			compatibleToTypes = Collections.unmodifiableSet(compatibleToTypes);	
		}		
		return compatibleToTypes;
	}

	public static void addCompatibility(TypeNode a, TypeNode b) {
		if(compatibleMap.get(a)==null)
			compatibleMap.put(a, new HashSet<TypeNode>());
		compatibleMap.get(a).add(b);
	}

	public static void addCastability(TypeNode from, TypeNode to) {
		if(castableMap.get(from)==null)
			castableMap.put(from, new HashSet<TypeNode>());
		castableMap.get(from).add(to);
	}

	/**
	 * @see de.unika.ipd.grgen.ast.TypeNode#getCompatibleTypes(java.util.Collection)
	 */
	public void doGetCompatibleToTypes(Collection<TypeNode> coll) {
		debug.report(NOTE, "compatible types to " + getName() + ":");

		Collection<TypeNode> compat = compatibleMap.get(this);
		if(compat == null)
			return;

		if (debug.willReport(NOTE)) {
			for(BaseNode curNode : compat) {
				debug.report(NOTE, "" + curNode.getName());
			}
		}
		coll.addAll(compat);
	}

	/**
	 * Returns a collection of all types this one is castable (implicitly and explicitly) to.
	 */
	protected final Collection<TypeNode> getCastableToTypes() {
		if(castableToTypes == null) {
			castableToTypes = new HashSet<TypeNode>();
			doGetCastableToTypes(castableToTypes);
			castableToTypes.addAll(getCompatibleToTypes());
			castableToTypes = Collections.unmodifiableSet(castableToTypes);
		}
		return castableToTypes;
	}

	private void doGetCastableToTypes(Collection<TypeNode> coll) {
		Collection<TypeNode> castable = castableMap.get(this);
		if(castable != null)
			coll.addAll(castable);
	}
	
	public boolean isFilterableType() {
		if(isEqual(BasicTypeNode.byteType))
			return true;
		if(isEqual(BasicTypeNode.shortType))
			return true;
		if(isEqual(BasicTypeNode.intType))
			return true;
		if(isEqual(BasicTypeNode.longType))
			return true;
		if(isEqual(BasicTypeNode.floatType))
			return true;
		if(isEqual(BasicTypeNode.doubleType))
			return true;
		if(isEqual(BasicTypeNode.stringType))
			return true;
		return false;
	}
	
	public String getFilterableTypesAsString() {
		return "byte, short, int, long, float, double, string";
	}
}

