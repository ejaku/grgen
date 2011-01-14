/*
 * GrGen: graph rewrite generator tool -- release GrGen.NET 2.7
 * Copyright (C) 2003-2011 Universitaet Karlsruhe, Institut fuer Programmstrukturen und Datenorganisation, LS Goos
 * licensed under LGPL v3 (see LICENSE.txt included in the packaging of this file)
 * www.grgen.net
 */

/**
 * @version $Id$
 */
package de.unika.ipd.grgen.ir;

import java.util.Collection;
import java.util.Collections;
import java.util.Map;
import java.util.HashMap;

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
	protected HashMap<Graph, GraphEntity> retyped = null;

	/** The entity from which this one will inherit its dynamic type */
	protected GraphEntity typeof = null;
	protected boolean isCopy = false;

	/** The interface type of the parameter if any. */
	protected InheritanceType parameterInterfaceType = null;
	
	/** The storage from which to get the node or edge, if any */
	protected Variable storage = null;

	/** The storage attribute from which to get the node or edge, if any */
	protected Qualification storageAttribute = null;

	/** The accessor for the storagemap from which to get the node or edge, if any */
	protected GraphEntity accessor = null;

	protected Collection<? extends InheritanceType> constraints = Collections.emptySet();

	private boolean maybeDeleted;
	private boolean maybeRetyped;
	
	/** Dependencies because of match by storage access (element must be matched before storage map access with it)*/
	protected int dependencyLevel = 0;

	/**
	 * Make a new graph entity of a given type.
	 * @param name The name of the entity.
	 * @param ident The declaring identifier.
	 * @param type The type used in the declaration.
	 * @param maybeDeleted Indicates whether this element might be deleted due to homomorphy.
	 * @param maybeRetyped Indicates whether this element might be retyped due to homomorphy.
	 * @param context The context of the declaration.
	 */
	protected GraphEntity(String name, Ident ident, InheritanceType type, Annotations annots,
			boolean maybeDeleted, boolean maybeRetyped, int context) {
		super(name, ident, type, false, context);
		setChildrenNames(childrenNames);
		this.type = type;
		this.annotations = annots;
		this.maybeDeleted = maybeDeleted;
		this.maybeRetyped = maybeRetyped;
		this.context = context;
	}

	public InheritanceType getInheritanceType() {
		return type;
	}

	/** Sets the entity this one inherits its dynamic type from */
	public void setTypeof(GraphEntity typeof, boolean isCopy) {
		this.typeof = typeof;
		this.isCopy = isCopy;
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

	/** @return true, if this is a retyped entity, i.e. the result of a retype, else false */
	public boolean isRetyped() {
		return false;
	}

	/** 
	 * @return true, if this entity changes its type
	 * @param graph The graph where the entity is queried to change its type; 
	 * if null any graph will match, i.e. return is true as soon as one graph exists where type changes
	 */
	public boolean changesType(Graph graph) {
		if(graph==null) return this.retyped != null;
		return getRetypedEntity(graph) != null;
	}

	/**
	 * Sets the corresponding retyped version of this entity
	 * @param retyped The retyped version
	 * @param graph The graph where the entity gets retyped
	 */
	public void setRetypedEntity(GraphEntity retyped, Graph graph) {
		if(this.retyped==null) {
			this.retyped = new HashMap<Graph, GraphEntity>();
		}
		this.retyped.put(graph, retyped);
	}

	/**
	 * Returns the corresponding retyped version of this entity
	 * @param graph The graph where the entity might get retyped
	 * @return The retyped version or <code>null</code>
	 */
	public GraphEntity getRetypedEntity(Graph graph) {
		if(this.retyped==null) {
			return null;
		}
		return this.retyped.get(graph);
	}

	/** Get the entity from which this entity inherits its dynamic type */
	public GraphEntity getTypeof() {
		return typeof;
	}
	
	/** returns whether the inherited type / typeof is the extended version in fact,
	 * named copy, copying the attributes too  */
	public boolean isCopy() {
		return isCopy;
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
	
	public void setStorage(Variable storage) {
		this.storage = storage;
	}

	public void setStorageAttribute(Qualification storage) {
		this.storageAttribute = storage;
	}

	public Variable getStorage() {
		return storage;
	}

	public Qualification getStorageAttribute() {
		return storageAttribute;
	}

	public void setAccessor(GraphEntity accessor) {
		this.accessor = accessor;
	}

	public GraphEntity getAccessor() {
		return accessor;
	}
	
	public void incrementDependencyLevel() {
		++dependencyLevel;
	}
	
	public int getDependencyLevel() {
		return dependencyLevel;
	}

	public final Collection<InheritanceType> getConstraints() {
		return Collections.unmodifiableCollection(constraints);
	}

	public String getNodeInfo() {
		return super.getNodeInfo()
			+ "\nconstraints: " + getConstraints();
	}
}
