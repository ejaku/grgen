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

import java.util.ArrayList;
import java.util.Collection;
import java.util.Collections;
import java.util.LinkedHashMap;
import java.util.LinkedHashSet;
import java.util.LinkedList;
import java.util.List;
import java.util.Map;
import java.util.Set;

import de.unika.ipd.grgen.util.Base;

/**
 * A IR class that represents types that inherit from other types.
 */
public abstract class InheritanceType extends CompoundType {
	public static final int ABSTRACT = 1;
	public static final int CONST = 2;

	private static int nextTypeID = 0;
	private static ArrayList<InheritanceType> inheritanceTypesByID = new ArrayList<InheritanceType>();

	private int typeID;
	private int maxDist = -1;
	private final Set<InheritanceType> directSuperTypes = new LinkedHashSet<InheritanceType>();
	private final Set<InheritanceType> directSubTypes = new LinkedHashSet<InheritanceType>();

	private Set<InheritanceType> allSuperTypes = null;

	/** The list of member initializers */
	private List<MemberInit> memberInitializers = new LinkedList<MemberInit>();

	/**
	 * Collection containing all members defined in that type and in its supertype.
	 * This field is used for caching.
	 */
	private Map<String, Entity> allMembers = null;

	/** The type modifiers. */
	private final int modifiers;

	/**
	 * The name of the external implementation of this type or null.
	 */
	private String externalName = null;


	/**
	 * @param name The name of the type.
	 * @param ident The identifier, declaring this type.
	 * @param modifiers The modifiers for this type.
	 * @param externalName The name of the external implementation of this type or null.
	 */
	protected InheritanceType(String name, Ident ident, int modifiers, String externalName) {
		super(name, ident);
		this.modifiers = modifiers;
		this.externalName = externalName;
		typeID = nextTypeID++;
		inheritanceTypesByID.add(this);
	}

	/***
	 * (Used in SearchPlanBackend2.java)
	 * @return a unique type identifier starting with zero.
	 */
	public int getTypeID() {
		return typeID;
	}

	public static InheritanceType getByTypeID(int typeID) {
		return inheritanceTypesByID.get(typeID);
	}

	/**
	 * Is this inheritance type the root of an inheritance hierarchy.
	 * @return true, if this type does not inherit from some other type.
	 */
	public boolean isRoot() {
		return directSuperTypes.isEmpty();
	}

	/**
	 * Add a type, this type inherits from.
	 * @param t The supertype.
	 */
	public void addDirectSuperType(InheritanceType t) {
		directSuperTypes.add(t);
		t.directSubTypes.add(this);
	}

	/**
	 * Get an iterator over all types, this type directly inherits from.
	 * @return The iterator.
	 */
	public Collection<InheritanceType> getDirectSuperTypes() {
		return Collections.unmodifiableCollection(directSuperTypes);
	}

	/**
	 * Returns all super types of this type (not including itself).
	 */
	public Collection<InheritanceType> getAllSuperTypes() {
		if(allSuperTypes==null) {
			allSuperTypes = new LinkedHashSet<InheritanceType>();

			for(InheritanceType type : directSuperTypes) {
				allSuperTypes.addAll(type.getAllSuperTypes());
				allSuperTypes.add(type);
			}
		}
		return Collections.unmodifiableCollection(allSuperTypes);
	}

	/**
	 * Get all subtypes of this type.
	 * @return An iterator iterating over all sub types of this one.
	 */
	public Collection<InheritanceType> getDirectSubTypes() {
		return Collections.unmodifiableCollection(directSubTypes);
	}


	/**
	 * Method getAllMembers computes the transitive closure of the members (attributes) of a type.
	 * @return   a Collection containing all members defined in that type and in its supertype.
	 */
	public Collection<Entity> getAllMembers() {
		if( allMembers == null ) {
			allMembers = new LinkedHashMap<String, Entity>();

			// add members of the current type
			for(Entity member : getMembers())
				allMembers.put(member.getIdent().toString(), member);

			// add the members of the supertype
			for(InheritanceType superType : getAllSuperTypes())
				for(Entity member : superType.getMembers())
					if(allMembers.containsKey(member.getIdent().toString()))
						Base.error.error(member.toString() + " of " + member.getOwner() + " already defined. " +
											 "It is also declared in " + allMembers.get(member.getIdent().toString()).getOwner() + ".");
					else
						allMembers.put(member.getIdent().toString(), member);
		}

		return allMembers.values();
	}

	/**
	 * Adds a member initializer to this type.
	 * @param init The member initializer to add.
	 */
	public void addMemberInit(MemberInit init) {
		memberInitializers.add(init);
	}

	/**
	 * Get all member initializers of this type.
	 * @return An collection containing all member initializers of this type.
	 */
	public Collection<MemberInit> getMemberInits() {
		return memberInitializers;
	}


	/**
	 * Check, if this type is a direct sub type of another type.
	 * This means, that this type inherited from the other type.
	 * @param t The other type.
	 * @return true, iff this type inherited from <code>t</code>.
	 */
	public boolean isDirectSubTypeOf(InheritanceType t) {
		return directSuperTypes.contains(t);
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
		if(!(t instanceof InheritanceType))
			return false;

		InheritanceType ty = (InheritanceType) t;

		if(isDirectSubTypeOf(ty))
			return true;

		for(InheritanceType inh : getDirectSuperTypes()) {
			if(inh.castableTo(ty))
				return true;
		}

		return false;
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

			for(InheritanceType inh : directSuperTypes) {
				int dist = inh.getMaxDist() + 1;
				maxDist = dist > maxDist ? dist : maxDist;
			}
		}

		return maxDist;
	}

	public final String getExternalName() {
		return externalName;
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

	public void addFields(Map<String, Object> fields) {
		super.addFields(fields);
		fields.put("inherits", directSuperTypes.iterator());
		fields.put("const", Boolean.valueOf(isConst()));
		fields.put("abstract ", Boolean.valueOf(isAbstract()));
	}
}
