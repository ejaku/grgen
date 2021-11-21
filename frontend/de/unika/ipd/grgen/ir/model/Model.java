/*
 * GrGen: graph rewrite generator tool -- release GrGen.NET 6.1
 * Copyright (C) 2003-2021 Universitaet Karlsruhe, Institut fuer Programmstrukturen und Datenorganisation, LS Goos; and free programmers
 * licensed under LGPL v3 (see LICENSE.txt included in the packaging of this file)
 * www.grgen.net
 */

/**
 * Model.java
 *
 * @author Sebastian Hack
 */

package de.unika.ipd.grgen.ir.model;

import java.util.ArrayList;
import java.util.Collection;
import java.util.Collections;
import java.util.LinkedHashSet;
import java.util.LinkedList;
import java.util.List;
import java.util.Map;
import java.util.Set;

import de.unika.ipd.grgen.ir.Ident;
import de.unika.ipd.grgen.ir.Identifiable;
import de.unika.ipd.grgen.ir.executable.ExternalFunction;
import de.unika.ipd.grgen.ir.executable.ExternalProcedure;
import de.unika.ipd.grgen.ir.model.type.EdgeType;
import de.unika.ipd.grgen.ir.model.type.EnumType;
import de.unika.ipd.grgen.ir.model.type.ExternalObjectType;
import de.unika.ipd.grgen.ir.model.type.InheritanceType;
import de.unika.ipd.grgen.ir.model.type.InternalObjectType;
import de.unika.ipd.grgen.ir.model.type.InternalTransientObjectType;
import de.unika.ipd.grgen.ir.model.type.NodeType;
import de.unika.ipd.grgen.ir.model.type.PackageType;
import de.unika.ipd.grgen.ir.type.Type;
import de.unika.ipd.grgen.ir.type.basic.PrimitiveType;

public class Model extends Identifiable implements NodeEdgeEnumBearer
{
	private List<Model> usedModels = new LinkedList<Model>();
	private List<PackageType> packages = new LinkedList<PackageType>();
	private List<Type> types = new LinkedList<Type>();

	private Set<NodeType> nodeTypes = new LinkedHashSet<NodeType>();
	private Set<EdgeType> edgeTypes = new LinkedHashSet<EdgeType>();
	private Set<InternalObjectType> objectTypes = new LinkedHashSet<InternalObjectType>();
	private Set<InternalTransientObjectType> transientObjectTypes = new LinkedHashSet<InternalTransientObjectType>();
	private Set<EnumType> enumTypes = new LinkedHashSet<EnumType>();
	private Set<Index> indices = new LinkedHashSet<Index>();
	private Set<ExternalObjectType> externalObjectTypes = new LinkedHashSet<ExternalObjectType>();
	private Set<ExternalFunction> externalFuncs = new LinkedHashSet<ExternalFunction>();
	private Set<ExternalProcedure> externalProcs = new LinkedHashSet<ExternalProcedure>();
	private boolean isEmitClassDefined;
	private boolean isEmitGraphClassDefined;
	private boolean isCopyClassDefined;
	private boolean isEqualClassDefined;
	private boolean isLowerClassDefined;
	private boolean isUniqueDefined;
	private boolean isUniqueIndexDefined;
	private boolean areFunctionsParallel;
	private int isoParallel;
	private int sequencesParallel;
	private Collection<NodeType> allNodeTypes;
	private Collection<EdgeType> allEdgeTypes;
	private Collection<InternalObjectType> allObjectTypes;
	private Collection<InternalTransientObjectType> allTransientObjectTypes;
	private Collection<InheritanceType> allGraphElementTypes;
	private Collection<InheritanceType> allInheritanceTypes;

	public Model(Ident ident, boolean isEmitClassDefined, boolean isEmitGraphClassDefined, boolean isCopyClassDefined,
			boolean isEqualClassDefined, boolean isLowerClassDefined,
			boolean isUniqueDefined, boolean isUniqueIndexDefined,
			boolean areFunctionsParallel, int isoParallel, int sequencesParallel)
	{
		super("model", ident);

		this.isEmitClassDefined = isEmitClassDefined;
		this.isEmitGraphClassDefined = isEmitGraphClassDefined;
		this.isCopyClassDefined = isCopyClassDefined;
		this.isEqualClassDefined = isEqualClassDefined;
		this.isLowerClassDefined = isLowerClassDefined;
		this.isUniqueDefined = isUniqueDefined;
		this.isUniqueIndexDefined = isUniqueIndexDefined;
		this.areFunctionsParallel = areFunctionsParallel;
		this.isoParallel = isoParallel;
		this.sequencesParallel = sequencesParallel;
	}

	public void addUsedModel(Model model)
	{
		usedModels.add(model);
		for(Type type : model.getTypes())
			addType(type);
		for(PackageType pack : model.getPackages())
			addPackage(pack);
		for(ExternalFunction externalFunc : model.getExternalFunctions())
			addExternalFunction(externalFunc);
	}

	public void addPackage(PackageType p)
	{
		packages.add(p);
	}

	public Collection<PackageType> getPackages()
	{
		return Collections.unmodifiableCollection(packages);
	}

	/** Add the given type to the type model. */
	public void addType(Type type)
	{
		types.add(type);
		if(type instanceof NodeType)
			nodeTypes.add((NodeType)type);
		else if(type instanceof EdgeType)
			edgeTypes.add((EdgeType)type);
		else if(type instanceof EnumType)
			enumTypes.add((EnumType)type);
		else if(type instanceof ExternalObjectType)
			externalObjectTypes.add((ExternalObjectType)type);
		else if(type instanceof InternalObjectType)
			objectTypes.add((InternalObjectType)type);
		else if(type instanceof InternalTransientObjectType)
			transientObjectTypes.add((InternalTransientObjectType)type);
		else if(!(type instanceof PrimitiveType))
			assert false : "Unexpected type added to model: " + type;
	}

	public void addIndex(Index index)
	{
		indices.add(index);
	}

	public Collection<Index> getIndices()
	{
		return Collections.unmodifiableCollection(indices);
	}

	public void addExternalFunction(ExternalFunction externalFunc)
	{
		externalFuncs.add(externalFunc);
	}

	public Collection<ExternalFunction> getExternalFunctions()
	{
		return Collections.unmodifiableCollection(externalFuncs);
	}

	public void addExternalProcedure(ExternalProcedure externalProc)
	{
		externalProcs.add(externalProc);
	}

	public Collection<ExternalProcedure> getExternalProcedures()
	{
		return Collections.unmodifiableCollection(externalProcs);
	}

	/** @return The types in the type model. */
	public Collection<Type> getTypes()
	{
		return Collections.unmodifiableCollection(types);
	}

	@Override
	public Collection<NodeType> getNodeTypes()
	{
		return Collections.unmodifiableCollection(nodeTypes);
	}

	public Collection<NodeType> getAllNodeTypes()
	{
		if(allNodeTypes == null) {
			Collection<NodeType> allNodeTypes = new ArrayList<NodeType>();
			allNodeTypes.addAll(getNodeTypes());
			for(PackageType pt : getPackages()) {
				allNodeTypes.addAll(pt.getNodeTypes());
			}
			int typeID = 0;
			for(NodeType nt : allNodeTypes) {
				nt.setInheritanceTypeID(typeID);
				++typeID;
			}
			this.allNodeTypes = Collections.unmodifiableCollection(allNodeTypes);
		}
		return allNodeTypes;
	}

	@Override
	public Collection<EdgeType> getEdgeTypes()
	{
		return Collections.unmodifiableCollection(edgeTypes);
	}

	public Collection<EdgeType> getAllEdgeTypes()
	{
		if(allEdgeTypes == null) {
			Collection<EdgeType> allEdgeTypes = new ArrayList<EdgeType>();
			allEdgeTypes.addAll(getEdgeTypes());
			for(PackageType pt : getPackages()) {
				allEdgeTypes.addAll(pt.getEdgeTypes());
			}
			int typeID = 0;
			for(EdgeType et : allEdgeTypes) {
				et.setInheritanceTypeID(typeID);
				++typeID;
			}
			this.allEdgeTypes = Collections.unmodifiableCollection(allEdgeTypes);
		}
		return allEdgeTypes;
	}
	
	public Collection<InheritanceType> getAllGraphElementTypes()
	{
		if(allGraphElementTypes == null) {
			Collection<InheritanceType> allNodeAndEdgeTypes = new ArrayList<InheritanceType>();
			allNodeAndEdgeTypes.addAll(getAllNodeTypes());
			allNodeAndEdgeTypes.addAll(getAllEdgeTypes());
			this.allGraphElementTypes = Collections.unmodifiableCollection(allNodeAndEdgeTypes);
		}
		return allGraphElementTypes;
	}

	@Override
	public Collection<InternalObjectType> getObjectTypes()
	{
		return Collections.unmodifiableCollection(objectTypes);
	}

	public Collection<InternalObjectType> getAllObjectTypes()
	{
		if(allObjectTypes == null) {
			Collection<InternalObjectType> allObjectTypes = new ArrayList<InternalObjectType>();
			allObjectTypes.addAll(getObjectTypes());
			for(PackageType pt : getPackages()) {
				allObjectTypes.addAll(pt.getObjectTypes());
			}
			int typeID = 0;
			for(InternalObjectType ot : allObjectTypes) {
				ot.setInheritanceTypeID(typeID);
				++typeID;
			}
			this.allObjectTypes = Collections.unmodifiableCollection(allObjectTypes);
		}
		return allObjectTypes;
	}

	@Override
	public Collection<InternalTransientObjectType> getTransientObjectTypes()
	{
		return Collections.unmodifiableCollection(transientObjectTypes);
	}

	public Collection<InternalTransientObjectType> getAllTransientObjectTypes()
	{
		if(allTransientObjectTypes == null) {
			Collection<InternalTransientObjectType> allTransientObjectTypes = new ArrayList<InternalTransientObjectType>();
			allTransientObjectTypes.addAll(getTransientObjectTypes());
			for(PackageType pt : getPackages()) {
				allTransientObjectTypes.addAll(pt.getTransientObjectTypes());
			}
			int typeID = 0;
			for(InternalTransientObjectType ot : allTransientObjectTypes) {
				ot.setInheritanceTypeID(typeID);
				++typeID;
			}
			this.allTransientObjectTypes = Collections.unmodifiableCollection(allTransientObjectTypes);
		}
		return allTransientObjectTypes;
	}

	public Collection<InheritanceType> getAllInheritanceTypes()
	{
		if(allInheritanceTypes == null) {
			Collection<InheritanceType> allInheritanceTypes = new ArrayList<InheritanceType>();
			allInheritanceTypes.addAll(getAllNodeTypes());
			allInheritanceTypes.addAll(getAllEdgeTypes());
			allInheritanceTypes.addAll(getAllObjectTypes());
			allInheritanceTypes.addAll(getAllTransientObjectTypes());
			this.allInheritanceTypes = Collections.unmodifiableCollection(allInheritanceTypes);
		}
		return allInheritanceTypes;
	}

	@Override
	public Collection<EnumType> getEnumTypes()
	{
		return Collections.unmodifiableCollection(enumTypes);
	}

	public Collection<ExternalObjectType> getExternalObjectTypes()
	{
		return Collections.unmodifiableCollection(externalObjectTypes);
	}

	public Collection<Model> getUsedModels()
	{
		return Collections.unmodifiableCollection(usedModels);
	}

	public boolean isEmitClassDefined()
	{
		return isEmitClassDefined;
	}

	public boolean isEmitGraphClassDefined()
	{
		return isEmitGraphClassDefined;
	}

	public boolean isCopyClassDefined()
	{
		return isCopyClassDefined;
	}

	public boolean isEqualClassDefined()
	{
		return isEqualClassDefined;
	}

	public boolean isLowerClassDefined()
	{
		return isLowerClassDefined;
	}

	public boolean isUniqueDefined()
	{
		return isUniqueDefined;
	}

	public void forceUniqueDefined()
	{
		isUniqueDefined = true;
	}

	public boolean isUniqueIndexDefined()
	{
		return isUniqueIndexDefined;
	}

	public void forceFunctionsParallel()
	{
		areFunctionsParallel = true;
	}

	public boolean areFunctionsParallel()
	{
		return areFunctionsParallel;
	}

	public int isoParallel()
	{
		return isoParallel;
	}

	public int sequencesParallel()
	{
		return sequencesParallel;
	}

	/** Canonicalize the type model. */
	@Override
	protected void canonicalizeLocal()
	{
		//Collections.sort(types, Identifiable.COMPARATOR);
		//Collections.sort(types);

		for(Type ty : types) {
			ty.canonicalize();
			if(ty instanceof EdgeType)
				((EdgeType)ty).canonicalizeConnectionAsserts();
		}
	}

	public void addToDigest(StringBuffer sb)
	{
		sb.append(this);
		sb.append('[');

		for(Model model : usedModels)
			model.addToDigest(sb);

		for(Type ty : types) {
			ty.addToDigest(sb);
		}

		sb.append(']');
	}

	@Override
	public void addFields(Map<String, Object> fields)
	{
		super.addFields(fields);
		fields.put("usedModels", usedModels.iterator());
		fields.put("types", types.iterator());
	}
}
