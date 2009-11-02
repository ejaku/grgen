/*
 * GrGen: graph rewrite generator tool -- release GrGen.NET 2.6
 * Copyright (C) 2003-2009 Universitaet Karlsruhe, Institut fuer Programmstrukturen und Datenorganisation, LS Goos
 * licensed under LGPL v3 (see LICENSE.txt included in the packaging of this file)
 */

/**
 * @author shack
 * @version $Id$
 */
package de.unika.ipd.grgen.ir;

import java.util.Collections;
import java.util.Map;
import java.util.Set;

/**
 * An instantiation of a type.
 */
public class Entity extends Identifiable {

	protected static final String[] childrenNames = { "type" };

	/** Type of the entity. */
	protected final Type type;

	/** The entity's owner. */
	protected Type owner = null;

	/** Is the entity constant - (only) relevant in backend for node/edge attributes. */
	protected boolean isConst = false;


	/**
	 * Make a new entity of a given type
	 * @param name The name of the entity.
	 * @param ident The declaring identifier.
	 * @param type The type used in the declaration.
	 * @param isConst Is the entity constant.
	 */
	public Entity(String name, Ident ident, Type type, boolean isConst) {
		super(name, ident);
		setChildrenNames(childrenNames);
		this.type = type;
		this.isConst = isConst;
	}

	/** @return The entity's type. */
	public Type getType() {
		return type;
	}

	/** @return The entity's owner. */
	public Type getOwner() {
		return owner;
	}

	/**
	 * Set the owner of the entity.
	 * This function is just called from other IR classes.
	 * @param type The owner of the entity.
	 */
	protected void setOwner(Type type) {
		owner = type;
	}

	/** @return true, if the entity has an owner, else false */
	public boolean hasOwner() {
		return owner != null;
	}

	public void addFields(Map<String, Object> fields) {
		super.addFields(fields);
		fields.put("type", Collections.singleton(type));
		fields.put("owner", Collections.singleton(owner));
	}

	/** @return true, if this is a retyped entity, else false */
	public boolean isRetyped() {
		return false;
	}

	/** @return true, if this is a constant entity, else false */
	public boolean isConst() {
		return isConst;
	}

	/** The only walkable child here is the type
	 *  @see de.unika.ipd.grgen.util.Walkable#getWalkableChildren() */
	public Set<? extends IR> getWalkableChildren() {
		return Collections.singleton(type);
	}
}
