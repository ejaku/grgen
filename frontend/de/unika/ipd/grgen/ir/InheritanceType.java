/*
 * GrGen: graph rewrite generator tool -- release GrGen.NET 4.3
 * Copyright (C) 2003-2014 Universitaet Karlsruhe, Institut fuer Programmstrukturen und Datenorganisation, LS Goos; and free programmers
 * licensed under LGPL v3 (see LICENSE.txt included in the packaging of this file)
 * www.grgen.net
 */

/**
 * @author shack
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

import de.unika.ipd.grgen.ir.exprevals.*;
import de.unika.ipd.grgen.ir.containers.*;


/**
 * Abstract base class for types that inherit from other types.
 */
public abstract class InheritanceType extends CompoundType {
	public static final int ABSTRACT = 1;
	public static final int CONST = 2;

	private static int nextTypeID = 0;
	private static ArrayList<InheritanceType> inheritanceTypesByID = new ArrayList<InheritanceType>();

	private int typeID;
	private int nodeOrEdgeTypeID;
	private int maxDist = -1;
	private final Set<InheritanceType> directSuperTypes = new LinkedHashSet<InheritanceType>();
	private final Set<InheritanceType> directSubTypes = new LinkedHashSet<InheritanceType>();

	private Set<InheritanceType> allSuperTypes = null;
	private Set<InheritanceType> allSubTypes = null;

	private List<Constructor> constructors = new LinkedList<Constructor>();

	/** The list of member initializers */
	private List<MemberInit> memberInitializers = new LinkedList<MemberInit>();

	private List<MapInit> mapInitializers = new LinkedList<MapInit>();
	private List<SetInit> setInitializers = new LinkedList<SetInit>();
	private List<ArrayInit> arrayInitializers = new LinkedList<ArrayInit>();
	private List<DequeInit> dequeInitializers = new LinkedList<DequeInit>();

	/** Collection containing all members defined in that type and in its supertype.
	 *  This field is used for caching. */
	private Map<String, Entity> allMembers = null;

	private Map<String, FunctionMethod> allFunctionMethods = null;
	private Map<String, ProcedureMethod> allProcedureMethods = null;

	/** Map between overriding and overridden members */
	private Map<Entity, Entity> overridingMembers = null;

	/** The type modifiers. */
	private final int modifiers;

	/** The name of the external implementation of this type or null. */
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

	/** @return a unique type identifier starting with zero. (Used in SearchPlanBackend2.java) */
	public int getTypeID() {
		return typeID;
	}

	public static InheritanceType getByTypeID(int typeID) {
		return inheritanceTypesByID.get(typeID);
	}

	/** @return a unique type identifier starting with zero, for the nodes, or the edges. Only unique for either the nodes, or the edges, not for both.*/
	public int getNodeOrEdgeTypeID(boolean isNode) {
		return nodeOrEdgeTypeID;
	}

	/** @return a unique type identifier starting with zero, for the nodes, or the edges. Only unique for either the nodes, or the edges, not for both.*/
	public void setNodeOrEdgeTypeID(boolean isNode, int nodeOrEdgeTypeID) {
		this.nodeOrEdgeTypeID = nodeOrEdgeTypeID;
	}

	/** @return true, if this type does not inherit from some other types, being the root of an inheritance hierarchy. */
	public boolean isRoot() {
		return directSuperTypes.isEmpty();
	}

	/** Adds a supertype, this type should inherit from. */
	public void addDirectSuperType(InheritanceType t) {
		assert allSubTypes == null && allSuperTypes == null: "wrong order of calls";
		if(allSubTypes != null || allSuperTypes != null) // todo: remove this constraint/work around it
			error.error(t.getIdent().getCoords(), "A container in a type must not reference a subtype");
		directSuperTypes.add(t);
		t.directSubTypes.add(this);
	}

	/** @return Set of all types, this type directly inherits from. */
	public Set<InheritanceType> getDirectSuperTypes() {
		return Collections.unmodifiableSet(directSuperTypes);
	}

	/** @return Set of all super types this type inherits from (not including itself). */
	public Set<InheritanceType> getAllSuperTypes() {
		if(allSuperTypes==null) {
			allSuperTypes = new LinkedHashSet<InheritanceType>();

			for(InheritanceType type : directSuperTypes) {
				allSuperTypes.addAll(type.getAllSuperTypes());
				allSuperTypes.add(type);
			}
		}
		return Collections.unmodifiableSet(allSuperTypes);
	}

	/** @return Set of all sub types this type inherits from (including itself). */
	public Set<InheritanceType> getAllSubTypes() {
		if(allSubTypes==null) {
			allSubTypes = new LinkedHashSet<InheritanceType>();
			allSubTypes.add(this);

			for(InheritanceType type : directSubTypes) {
				allSubTypes.addAll(type.getAllSubTypes());
				allSubTypes.add(type);
			}
		}
		return Collections.unmodifiableSet(allSubTypes);
	}

	/** Get all subtypes of this type. */
	public Set<InheritanceType> getDirectSubTypes() {
		return Collections.unmodifiableSet(directSubTypes);
	}

	/**
	 * Adds all members of the given type to allMembers, handling overwriting
	 * of abstract members including filling the overridingMembers map
	 */
	private void addMembers(InheritanceType type) {
		for(Entity member : type.getMembers()) {
			String memberName = member.getIdent().toString();
			Entity curMember = allMembers.get(memberName);
			if(curMember != null) {
				if(curMember.getType().isVoid()) {
					// we have an abstract member, it's OK to overwrite it
					overridingMembers.put(member, curMember);
				}
				else {
					error.error(member.getIdent().getCoords(), member.toString()
									+ " of " + member.getOwner()
									+ " already defined. It is also declared in "
									+ curMember.getOwner() + ".");
				}
			}
			allMembers.put(memberName, member);
		}
	}

	private void addFunctionMethods(InheritanceType type) {
		for(FunctionMethod fm : type.getFunctionMethods()) {
			// METHOD-TODO - what version is of relevance if alreay defined; override handling
			String functionName = fm.getIdent().toString();
			allFunctionMethods.put(functionName, fm);
		}
	}

	private void addProcedureMethods(InheritanceType type) {
		for(ProcedureMethod pm : type.getProcedureMethods()) {
			// METHOD-TODO - what version is of relevance if alreay defined; override handling
			String procedureName = pm.getIdent().toString();
			allProcedureMethods.put(procedureName, pm);
		}
	}

	/**
	 * Method getAllMembers computes the transitive closure of the members (attributes) of a type.
	 * @return   a Collection containing all members defined in that type and in its supertype.
	 */
	public Collection<Entity> getAllMembers() {
		if( allMembers == null ) {
			allMembers = new LinkedHashMap<String, Entity>();
			overridingMembers = new LinkedHashMap<Entity, Entity>();

			// add the members of the super types
			for(InheritanceType superType : getAllSuperTypes())
				addMembers(superType);

			// add members of the current type
			addMembers(this);
		}

		return allMembers.values();
	}

	public Collection<FunctionMethod> getAllFunctionMethods() {
		if( allFunctionMethods == null ) {
			allFunctionMethods = new LinkedHashMap<String, FunctionMethod>();
			//overridingMembers = new LinkedHashMap<Entity, Entity>(); METHOD-TODO

			// add the members of the super types
			for(InheritanceType superType : getAllSuperTypes())
				addFunctionMethods(superType);

			// add members of the current type
			addFunctionMethods(this);
		}

		return allFunctionMethods.values();
	}

	public Collection<ProcedureMethod> getAllProcedureMethods() {
		if( allProcedureMethods == null ) {
			allProcedureMethods = new LinkedHashMap<String, ProcedureMethod>();
			//overridingMembers = new LinkedHashMap<Entity, Entity>(); METHOD-TODO

			// add the members of the super types
			for(InheritanceType superType : getAllSuperTypes())
				addProcedureMethods(superType);

			// add members of the current type
			addProcedureMethods(this);
		}

		return allProcedureMethods.values();
	}

	/**
	 * Gets the overridden member for a given member, if one exists.
	 * @param overridingMember The member, which eventually overrides another member.
	 * @return The overridden member, or null, if no such exists.
	 */
	public Entity getOverriddenMember(Entity overridingMember) {
		return overridingMembers.get(overridingMember);
	}

	public FunctionMethod getOverriddenFunctionMethod(FunctionMethod overridingMember) {
		return null; // METHOD-TODO
	}

	public ProcedureMethod getOverriddenProcedureMethod(ProcedureMethod overridingMember) {
		return null; // METHOD-TODO
	}

	public void addConstructor(Constructor constr) {
		constructors.add(constr);
	}

	public Collection<Constructor> getConstructor() {
		return constructors;
	}

	/** Adds the given member initializer to this type. */
	public void addMemberInit(MemberInit init) {
		memberInitializers.add(init);
	}

	/** @return A collection containing all member initializers of this type. */
	public Collection<MemberInit> getMemberInits() {
		return memberInitializers;
	}

	public void addMapInit(MapInit init) {
		mapInitializers.add(init);
	}

	public Collection<MapInit> getMapInits() {
		return mapInitializers;
	}

	public void addSetInit(SetInit init) {
		setInitializers.add(init);
	}

	public Collection<SetInit> getSetInits() {
		return setInitializers;
	}

	public void addArrayInit(ArrayInit init) {
		arrayInitializers.add(init);
	}

	public Collection<ArrayInit> getArrayInits() {
		return arrayInitializers;
	}

	public void addDequeInit(DequeInit init) {
		dequeInitializers.add(init);
	}

	public Collection<DequeInit> getDequeInits() {
		return dequeInitializers;
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
