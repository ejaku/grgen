/*
 * GrGen: graph rewrite generator tool -- release GrGen.NET 7.1
 * Copyright (C) 2003-2025 Universitaet Karlsruhe, Institut fuer Programmstrukturen und Datenorganisation, LS Goos; and free programmers
 * licensed under LGPL v3 (see LICENSE.txt included in the packaging of this file)
 * www.grgen.net
 */

package de.unika.ipd.grgen.ir.pattern;

import java.util.Collection;
import java.util.Collections;
import java.util.Map;
import java.util.HashMap;
import java.util.Vector;

import de.unika.ipd.grgen.ast.decl.pattern.ConstraintDeclNode.CopyKind;
import de.unika.ipd.grgen.ir.Entity;
import de.unika.ipd.grgen.ir.Ident;
import de.unika.ipd.grgen.ir.expr.Expression;
import de.unika.ipd.grgen.ir.model.type.InheritanceType;
import de.unika.ipd.grgen.ir.type.TypeExpr;
import de.unika.ipd.grgen.util.Annotations;

/**
 * Abstract base class for entities occurring in graphs
 */
public abstract class GraphEntity extends Entity
{
	/** Type of the entity. */
	protected final InheritanceType type;

	/** The annotations of this entity. */
	protected final Annotations annotations;

	/** The retyped version of this entity if any. */
	protected HashMap<PatternGraphBase, GraphEntity> retyped = null;

	/** The entity from which this one will inherit its dynamic type */
	protected GraphEntity typeof = null;
	protected CopyKind copyKind = CopyKind.None;

	/** The interface type of the parameter if any. */
	protected InheritanceType parameterInterfaceType = null;

	/** The storage from which to get the node or edge, if any (i.e. not null)*/
	public StorageAccess storageAccess = null;

	/** The index to the storage from which to get the node or edge, if any (i.e. not null)*/
	public StorageAccessIndex storageAccessIndex = null;

	/** The index from which to get the node or edge, if any (i.e. not null)*/
	public IndexAccess indexAccess = null;

	/** The indices from which to get the node or edge when using a multiple index query, if any (i.e. not empty)*/
	public Vector<IndexAccessOrdering> multipleIndexAccesses = new Vector<IndexAccessOrdering>();

	/** The name map access used to get the node or edge, if any (i.e. not null)*/
	public NameLookup nameMapAccess = null;

	/** The unique index access used to get the node or edge, if any (i.e. not null)*/
	public UniqueLookup uniqueIndexAccess = null;

	protected Collection<? extends InheritanceType> constraints = Collections.emptySet();

	private boolean maybeDeleted;
	private boolean maybeRetyped;

	// null or an expression used to initialize the node
	public Expression initialization;

	public Vector<NameOrAttributeInitialization> nameOrAttributeInitialization = new Vector<NameOrAttributeInitialization>();

	/** Dependencies because of match by storage access (element must be matched before storage map access with it)*/
	protected int dependencyLevel = 0;

	/**
	 * Make a new graph entity of a given type.
	 * @param name The name of the entity.
	 * @param ident The declaring identifier.
	 * @param type The type used in the declaration.
	 * @param maybeDeleted Indicates whether this element might be deleted due to homomorphy.
	 * @param maybeRetyped Indicates whether this element might be retyped due to homomorphy.
	 * @param isDefToBeYieldedTo Is the entity a defined entity only, to be filled with yields from nested patterns.
	 * @param context The context of the declaration.
	 */
	protected GraphEntity(String name, Ident ident, InheritanceType type, Annotations annots,
			boolean maybeDeleted, boolean maybeRetyped, boolean isDefToBeYieldedTo, int context)
	{
		super(name, ident, type, false, isDefToBeYieldedTo, context);
		setChildrenNames(childrenNames);
		this.type = type;
		this.annotations = annots;
		this.maybeDeleted = maybeDeleted;
		this.maybeRetyped = maybeRetyped;
		this.context = context;
	}

	public InheritanceType getInheritanceType()
	{
		return type;
	}

	/** Sets the entity this one inherits its dynamic type from */
	public void setTypeofCopy(GraphEntity typeof, CopyKind copyKind)
	{
		this.typeof = typeof;
		this.copyKind = copyKind;
	}

	/** Sets the type constraints for this entity */
	public void setConstraints(TypeExpr expr)
	{
		this.constraints = expr.evaluate();
	}

	/** @return The annotations. */
	@Override
	public Annotations getAnnotations()
	{
		return annotations;
	}

	public boolean isMaybeDeleted()
	{
		return maybeDeleted;
	}

	public boolean isMaybeRetyped()
	{
		return maybeRetyped;
	}

	@Override
	public void addFields(Map<String, Object> fields)
	{
		super.addFields(fields);
		fields.put("valid_types", constraints.iterator());
		fields.put("retyped", Collections.singleton(retyped));
		fields.put("typeof", Collections.singleton(typeof));
	}

	/** @return true, if this is a retyped entity, i.e. the result of a retype, else false */
	@Override
	public boolean isRetyped()
	{
		return false;
	}

	/**
	 * @return true, if this entity changes its type
	 * @param patternGraph The pattern graph where the entity is queried to change its type;
	 * if null any pattern graph will match, i.e. return is true as soon as one pattern graph exists where type changes
	 */
	public boolean changesType(PatternGraphBase patternGraph)
	{
		if(patternGraph == null)
			return this.retyped != null;
		return getRetypedEntity(patternGraph) != null;
	}

	/**
	 * Sets the corresponding retyped version of this entity
	 * @param retyped The retyped version
	 * @param patternGraph The pattern graph where the entity gets retyped
	 */
	public void setRetypedEntity(GraphEntity retyped, PatternGraphBase patternGraph)
	{
		if(this.retyped == null) {
			this.retyped = new HashMap<PatternGraphBase, GraphEntity>();
		}
		this.retyped.put(patternGraph, retyped);
	}

	/**
	 * Returns the corresponding retyped version of this entity
	 * @param patternGraph The pattern graph where the entity might get retyped
	 * @return The retyped version or <code>null</code>
	 */
	public GraphEntity getRetypedEntity(PatternGraphBase patternGraph)
	{
		if(this.retyped == null) {
			return null;
		}
		return this.retyped.get(patternGraph);
	}

	/** Get the entity from which this entity inherits its dynamic type */
	public GraphEntity getTypeof()
	{
		return typeof;
	}

	/** returns whether the inherited type / typeof is the extended version in fact,
	 * named copy, copying the attributes too  */
	public CopyKind getCopy()
	{
		return copyKind;
	}

	/** @return true, if this entity inherits its type from some other entitiy */
	public boolean inheritsType()
	{
		return typeof != null;
	}

	public void setParameterInterfaceType(InheritanceType type)
	{
		parameterInterfaceType = type;
	}

	public InheritanceType getParameterInterfaceType()
	{
		return parameterInterfaceType;
	}

	public void setStorage(StorageAccess storageAccess)
	{
		this.storageAccess = storageAccess;
	}

	public void setStorageIndex(StorageAccessIndex storageAccessIndex)
	{
		this.storageAccessIndex = storageAccessIndex;
	}

	public void setIndex(IndexAccess indexAccess)
	{
		this.indexAccess = indexAccess;
	}
	
	public void addIndex(IndexAccessOrdering indexAccess)
	{
		this.multipleIndexAccesses.add(indexAccess);
	}

	public void setNameMapAccess(NameLookup nameMapAccess)
	{
		this.nameMapAccess = nameMapAccess;
	}

	public void setUniqueIndexAccess(UniqueLookup uniqueIndexAccess)
	{
		this.uniqueIndexAccess = uniqueIndexAccess;
	}

	public void setInitialization(Expression initialization)
	{
		this.initialization = initialization;
	}

	public void addNameOrAttributeInitialization(NameOrAttributeInitialization nai)
	{
		this.nameOrAttributeInitialization.add(nai);
	}

	public boolean hasNameInitialization()
	{
		for(NameOrAttributeInitialization nai : nameOrAttributeInitialization) {
			if(nai.attribute == null)
				return true;
		}
		return false;
	}

	public NameOrAttributeInitialization getNameInitialization()
	{
		for(NameOrAttributeInitialization nai : nameOrAttributeInitialization) {
			if(nai.attribute == null)
				return nai;
		}
		return null;
	}

	public boolean hasAttributeInitialization()
	{
		for(NameOrAttributeInitialization nai : nameOrAttributeInitialization) {
			if(nai.attribute != null)
				return true;
		}
		return false;
	}

	public void incrementDependencyLevel()
	{
		++dependencyLevel;
	}

	public int getDependencyLevel()
	{
		return dependencyLevel;
	}

	public final Collection<InheritanceType> getConstraints()
	{
		return Collections.unmodifiableCollection(constraints);
	}

	@Override
	public String getNodeInfo()
	{
		return super.getNodeInfo()
				+ "\nconstraints: " + getConstraints();
	}
}
