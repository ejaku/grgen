/*
 * GrGen: graph rewrite generator tool -- release GrGen.NET 3.0
 * Copyright (C) 2003-2011 Universitaet Karlsruhe, Institut fuer Programmstrukturen und Datenorganisation, LS Goos
 * licensed under LGPL v3 (see LICENSE.txt included in the packaging of this file)
 * www.grgen.net
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
import java.util.Collections;
import java.util.HashMap;
import java.util.HashSet;
import java.util.Map;

/**
 * Base class for all AST nodes representing types.
 */
public abstract class TypeNode extends BaseNode {
	/** A map, that maps each basic type to a set of all other basic types,
	 *  that are compatible to the type. */
	private static final Map<TypeNode, HashSet<TypeNode>> compatibleMap =
		new HashMap<TypeNode, HashSet<TypeNode>>();

	/** A map, that maps each type to a set of all other types,
	 * that are castable to the type. */
	private static final Map<TypeNode, HashSet<TypeNode>> castableMap =
		new HashMap<TypeNode, HashSet<TypeNode>>();

	// Cache variables
	private Collection<TypeNode> compatibleToTypes;
	private Collection<TypeNode> castableToTypes;

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
	 * @return		the compatibility distance or -1 if no compatibility could
	 * 				be found
	 */
	protected int compatibilityDist(TypeNode type) {
		if ( this.isEqual(type) ) {
			return 0;
		}
		if ( this.isCompatibleTo(type) ) {
			return 1;
		}

		for (TypeNode t : getCompatibleToTypes()) {
			if (t.isCompatibleTo(type)) {
				return 2;
			}
		}

		return -1;
	}

	/**
	 * Check, if this type is compatible (implicitly castable) or equal to <code>t</code>.
	 * @param t A type.
	 * @return true, if this type is compatible or equal to <code>t</code>
	 */
	protected boolean isCompatibleTo(TypeNode t) {
		if(isEqual(t)) return true;

		return getCompatibleToTypes().contains(t);
	}

	/**
	 * Check, if this type is only castable (explicitly castable)
	 * to <code>t</code>
	 * @param t A type.
	 * @return true, if this type is just castable to <code>t</code>.
	 */
	protected boolean isCastableTo(TypeNode t) {
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
	protected boolean isEqual(TypeNode t) {
		return t == this;
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
	protected final Collection<TypeNode> getCompatibleToTypes() {
		if(compatibleToTypes != null) return compatibleToTypes;

		HashSet<TypeNode> coll = new HashSet<TypeNode>();
		coll.add(this);
		doGetCompatibleToTypes(coll);
		compatibleToTypes = Collections.unmodifiableSet(coll);
		return compatibleToTypes;
	}

	private static void addTypeToMap(Map<TypeNode, HashSet<TypeNode>> map,
									   TypeNode index, TypeNode target) {
		HashSet<TypeNode> s = map.get(index);

		if(s == null)
			map.put(index, s = new HashSet<TypeNode>());

		s.add(target);
	}

	/**
	 * Add a compatibility to the compatibility map.
	 * @param a The first type.
	 * @param b The second type.
	 */
	public static void addCompatibility(TypeNode a, TypeNode b) {
		addTypeToMap(compatibleMap, a, b);
	}

	public static void addCastability(TypeNode from, TypeNode to) {
		addTypeToMap(castableMap, from, to);
	}

	/**
	 * @see de.unika.ipd.grgen.ast.TypeNode#getCompatibleTypes(java.util.Collection)
	 */
	protected void doGetCompatibleToTypes(Collection<TypeNode> coll) {
		debug.report(NOTE, "compatible types to " + getName() + ":");

		Collection<TypeNode> compat = compatibleMap.get(this);
		if(compat == null) return;

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
		if(castableToTypes != null) return castableToTypes;

		HashSet<TypeNode> coll = new HashSet<TypeNode>();
		doGetCastableToTypes(coll);
		coll.addAll(getCompatibleToTypes());

		castableToTypes = Collections.unmodifiableSet(coll);
		return castableToTypes;
	}

	private void doGetCastableToTypes(Collection<TypeNode> coll) {
		Collection<TypeNode> castable = castableMap.get(this);
		if(castable != null)
			coll.addAll(castable);
	}
}

