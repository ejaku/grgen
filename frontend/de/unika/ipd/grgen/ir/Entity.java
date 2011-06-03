/*
 * GrGen: graph rewrite generator tool -- release GrGen.NET 3.0
 * Copyright (C) 2003-2011 Universitaet Karlsruhe, Institut fuer Programmstrukturen und Datenorganisation, LS Goos
 * licensed under LGPL v3 (see LICENSE.txt included in the packaging of this file)
 * www.grgen.net
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

	/** Is the entity a defined entity only, to be filled with yields from nested patterns? */
	protected boolean isDefToBeYieldedTo = false;

	/** Only in case of isDefToBeYieldedTo: gives the pattern graph in which the entity is to be deleted (can't use LHS\RHS for deciding this)*/
	protected PatternGraph patternGraphDefYieldedIsToBeDeleted = null; // todo: DELETE=LHS\RHS does not work any more due to nesting and def entities, switch to delete annotations in AST, IR

	/** Context of the declaration */
	int context;


	/**
	 * Make a new entity of a given type
	 * @param name The name of the entity.
	 * @param ident The declaring identifier.
	 * @param type The type used in the declaration.
	 * @param isConst Is the entity constant.
	 * @param isDefToBeYieldedTo Is the entity a defined entity only, to be filled with yields from nested patterns.
	 * @param context The context of the declaration
	 */
	public Entity(String name, Ident ident, Type type, boolean isConst, boolean isDefToBeYieldedTo, int context) {
		super(name, ident);
		setChildrenNames(childrenNames);
		this.type = type;
		this.isConst = isConst;
		this.isDefToBeYieldedTo = isDefToBeYieldedTo;
		this.context = context;
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

	/** @return true, if this is a retyped entity, i.e. the result of a retype, else false */
	public boolean isRetyped() {
		return false;
	}

	/** @return true, if this is a constant entity, else false */
	public boolean isConst() {
		return isConst;
	}

	/** @return true, if this is a defined only entity to be filled from nested patterns, else false */
	public boolean isDefToBeYieldedTo() {
		return isDefToBeYieldedTo;
	}

	public void setPatternGraphDefYieldedIsToBeDeleted(PatternGraph graph) {
		assert isDefToBeYieldedTo;
		patternGraphDefYieldedIsToBeDeleted = graph;
	}

	/** @return the pattern graph this defined entity to be yielded to is to be deleted, else null; null if not isDefToBeYieldedTo*/
	public PatternGraph patternGraphDefYieldedIsToBeDeleted() {
		return patternGraphDefYieldedIsToBeDeleted;
	}

	/** The only walkable child here is the type
	 *  @see de.unika.ipd.grgen.util.Walkable#getWalkableChildren() */
	public Set<? extends IR> getWalkableChildren() {
		return Collections.singleton(type);
	}
}
