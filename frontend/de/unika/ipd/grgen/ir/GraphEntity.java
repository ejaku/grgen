/*
 * GrGen: graph rewrite generator tool -- release GrGen.NET 2.5
 * Copyright (C) 2009 Universitaet Karlsruhe, Institut fuer Programmstrukturen und Datenorganisation, LS Goos
 * licensed under LGPL v3 (see LICENSE.txt included in the packaging of this file)
 */

/**
 * @version $Id$
 */
package de.unika.ipd.grgen.ir;

import java.util.Collection;
import java.util.Collections;
import java.util.Map;

import de.unika.ipd.grgen.util.Annotations;

/**
 * Abstract base class for entities occurring in graphs
 */
public abstract class GraphEntity extends Entity {

	/** Type of the entity. */
	protected final InheritanceType type;

	/** The annotations of this entity. */
	protected final Annotations annotations;

	/** The retyped version of this entity if any. */
	protected GraphEntity retyped = null;

	/** The entity from which this one will inherit its dynamic type */
	protected GraphEntity typeof = null;

	/** The interface type of the parameter if any. */
	protected InheritanceType parameterInterfaceType = null;

	protected Collection<? extends InheritanceType> constraints = Collections.emptySet();

	boolean maybeDeleted;
	boolean maybeRetyped;

	/**
	 * Make a new graph entity of a given type.
	 * @param name The name of the entity.
	 * @param ident The declaring identifier.
	 * @param type The type used in the declaration.
	 * @param maybeDeleted Indicates whether this element might be deleted due to homomorphy.
	 * @param maybeRetyped Indicates whether this element might be retyped due to homomorphy.
	 */
	protected GraphEntity(String name, Ident ident, InheritanceType type, Annotations annots,
			boolean maybeDeleted, boolean maybeRetyped) {
		super(name, ident, type, false);
		setChildrenNames(childrenNames);
		this.type = type;
		this.annotations = annots;
		this.maybeDeleted = maybeDeleted;
		this.maybeRetyped = maybeRetyped;
	}

	public InheritanceType getInheritanceType() {
		return type;
	}

	/** Sets the entity this one inherits its dynamic type from */
	public void setTypeof(GraphEntity typeof) {
		this.typeof = typeof;
	}

	/** Sets the type constraints for this entity */
	public void setConstraints(TypeExpr expr) {
		this.constraints = expr.evaluate();
	}

	/** @return The annotations. */
	public Annotations getAnnotations() {
		return annotations;
	}

	public boolean isMaybeDeleted() {
		return maybeDeleted;
	}

	public boolean isMaybeRetyped() {
		return maybeRetyped;
	}

	public void addFields(Map<String, Object> fields) {
		super.addFields(fields);
		fields.put("valid_types", constraints.iterator());
		fields.put("retyped", Collections.singleton(retyped));
		fields.put("typeof", Collections.singleton(typeof));
	}

	/** @return true, if this is a retyped entity */
	public boolean isRetyped() {
		return false;
	}

	/** @return true, if this entity changes its type */
	public boolean changesType() {
		return retyped != null;
	}

	/**
	 * Sets the corresponding retyped version of this entity
	 * @param retyped The retyped version
	 */
	public void setRetypedEntity(GraphEntity retyped) {
		this.retyped = retyped;
	}

	/**
	 * Returns the corresponding retyped version of this entity
	 * @return The retyped version or <code>null</code>
	 */
	public GraphEntity getRetypedEntity() {
		return this.retyped;
	}

	/** Get the entity from which this entity inherits its dynamic type */
	public GraphEntity getTypeof() {
		return typeof;
	}

	/** @return true, if this entity inherits its type from some other entitiy */
	public boolean inheritsType() {
		return typeof != null;
	}

	public void setParameterInterfaceType(InheritanceType type) {
		parameterInterfaceType = type;
	}
	
	public InheritanceType getParameterInterfaceType() {
		return parameterInterfaceType;
	}
	
	public final Collection<InheritanceType> getConstraints() {
		return Collections.unmodifiableCollection(constraints);
	}

	public String getNodeInfo() {
		return super.getNodeInfo()
			+ "\nconstraints: " + getConstraints();
	}
}
