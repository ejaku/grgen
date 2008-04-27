/*
 * GrGen: graph rewrite generator tool -- release GrGen.NET v2 beta
 * Copyright (C) 2008 Universität Karlsruhe, Institut für Programmstrukturen und Datenorganisation, LS Goos
 * licensed under GPL v3 (see LICENSE.txt included in the packaging of this file)
 */

/**
 * @author Sebastian Hack
 * @version $Id$
 */
package de.unika.ipd.grgen.ast;

import java.util.Collection;
import java.util.HashSet;
import java.util.LinkedHashMap;
import java.util.Map;

import de.unika.ipd.grgen.ir.InheritanceType;

/**
 * Base class for compound types, that allow inheritance.
 */
public abstract class InheritanceTypeNode extends CompoundTypeNode
{
	public static final int MOD_CONST = 1;
	public static final int MOD_ABSTRACT = 2;

	protected CollectNode<IdentNode> extendUnresolved;
	protected CollectNode<BaseNode> bodyUnresolved;

	/**
	 * The modifiers for this type.
	 * An ORed combination of the constants above.
	 */
	private int modifiers = 0;

	/**
	 * The name of the external implementation of this type or null.
	 */
	private String externalName = null;

	/** Maps all member (attribute) names to their declarations. */
	private Map<String, DeclNode> allMembers = null;

	/** Contains all super types of this type (not including this itself) */
	private Collection<InheritanceTypeNode> allSuperTypes = null;

	public boolean isA(InheritanceTypeNode type) {
		assert type != null;
		return this==type || getAllSuperTypes().contains(type);
	}

	/** Returns all super types of this type (not including itself). */
	public Collection<InheritanceTypeNode> getAllSuperTypes() {
		if(allSuperTypes==null) {
			allSuperTypes = new HashSet<InheritanceTypeNode>();

			for(InheritanceTypeNode type : getDirectSuperTypes()) {
				allSuperTypes.addAll(type.getAllSuperTypes());
				allSuperTypes.add(type);
			}
		}
		return allSuperTypes;
	}

	/** @see de.unika.ipd.grgen.ast.BaseNode#checkLocal() */
	protected boolean checkLocal()
	{
		boolean res = true;
		getAllSuperTypes();

		for(DeclNode member : getAllMembers().values())
			if(member instanceof AbstractMemberDeclNode && !isAbstract()) {
				error.error(getIdentNode().getCoords(),
						getUseStr() + " \"" + getIdentNode() + "\" must be declared abstract, because member \"" +
						member + "\" is abstract.");
				res = false;
			}

		return res;
	}

	/**
	 * Get the IR object as type.
	 * The cast must always succeed.
	 * @return The IR object as type.
	 */
	public InheritanceType getType() {
		return checkIR(InheritanceType.class);
	}

	public void setModifiers(int modifiers) {
		this.modifiers = modifiers;
	}

	public final boolean isAbstract() {
		return (modifiers & MOD_ABSTRACT) != 0;
	}

	public final boolean isConst() {
		return (modifiers & MOD_CONST) != 0;
	}

	protected final int getIRModifiers() {
		return (isAbstract() ? InheritanceType.ABSTRACT : 0)
			| (isConst() ? InheritanceType.CONST : 0);
	}

	public void setExternalName(String extName) {
		externalName = extName;
	}

	public final String getExternalName() {
		return externalName;
	}

	public abstract Collection<? extends InheritanceTypeNode> getDirectSuperTypes();

	protected abstract void getMembers(Map<String, DeclNode> members);

	/** Returns all members (including inherited ones) of this type. */
	public Map<String, DeclNode> getAllMembers()
	{
		if(allMembers==null) {
			allMembers = new LinkedHashMap<String, DeclNode>();

			for(InheritanceTypeNode superType : getDirectSuperTypes()) {
				allMembers.putAll(superType.getAllMembers());
			}

			getMembers(allMembers);
		}

		return allMembers;
	}
}
