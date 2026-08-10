/*
 * GrGen: graph rewrite generator tool -- release GrGen.NET 8.1
 * Copyright (C) 2003-2026 Universitaet Karlsruhe, Institut fuer Programmstrukturen und Datenorganisation, LS Goos; and free programmers
 * licensed under LGPL v3, some components/parts use different licenses (see LICENSE.txt included in the packaging of this file)
 * www.grgen.de / www.grgen.net
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
	private ArrayList<Model> usedModels = new ArrayList<Model>();
	private ArrayList<PackageType> packages = new ArrayList<PackageType>();
	private ArrayList<Type> types = new ArrayList<Type>();

	private Set<NodeType> nodeTypes = new LinkedHashSet<NodeType>();
	private Set<EdgeType> edgeTypes = new LinkedHashSet<EdgeType>();
	private Set<InternalObjectType> objectTypes = new LinkedHashSet<InternalObjectType>();
	private Set<InternalTransientObjectType> transientObjectTypes = new LinkedHashSet<InternalTransientObjectType>();
	private Set<EnumType> enumTypes = new LinkedHashSet<EnumType>();
	private Set<Index> indices = new LinkedHashSet<Index>();
	private Set<ExternalObjectType> externalObjectTypes = new LinkedHashSet<ExternalObjectType>();
	private Set<ExternalFunction> externalFuncs = new LinkedHashSet<ExternalFunction>();
	private Set<ExternalProcedure> externalProcs = new LinkedHashSet<ExternalProcedure>();
	private boolean isEmitClassDefined_;
	private boolean isEmitGraphClassDefined_;
	private boolean isCopyClassDefined_;
	private boolean isEqualClassDefined_;
	private boolean isLowerClassDefined_;
	private boolean isGraphofDefined_;
	private boolean isUniqueDefined_;
	private boolean isUniqueResulting_;
	private boolean isUniqueClassDefined_;
	private boolean isUniqueIndexDefined_;
	private boolean areFunctionsParallel_;
	private int isoParallel;
	private int sequencesParallel;
	private ArrayList<NodeType> allNodeTypes;
	private ArrayList<EdgeType> allEdgeTypes;
	private ArrayList<InternalObjectType> allObjectTypes;
	private ArrayList<InternalTransientObjectType> allTransientObjectTypes;
	private ArrayList<InheritanceType> allGraphElementTypes;
	private ArrayList<InheritanceType> allInheritanceTypes;

	public Model(Ident ident, boolean isEmitClassDefined, boolean isEmitGraphClassDefined, boolean isCopyClassDefined,
			boolean isEqualClassDefined, boolean isLowerClassDefined, boolean isGraphofDefined,
			boolean isUniqueDefined, boolean isUniqueClassDefined, boolean isUniqueIndexDefined,
			boolean areFunctionsParallel, int isoParallel, int sequencesParallel)
	{
		super("model", ident);

		this.isEmitClassDefined_ = isEmitClassDefined;
		this.isEmitGraphClassDefined_ = isEmitGraphClassDefined;
		this.isCopyClassDefined_ = isCopyClassDefined;
		this.isEqualClassDefined_ = isEqualClassDefined;
		this.isLowerClassDefined_ = isLowerClassDefined;
		this.isGraphofDefined_ = isGraphofDefined;
		this.isUniqueDefined_ = isUniqueDefined;
		this.isUniqueResulting_ = isUniqueDefined;
		this.isUniqueClassDefined_ = isUniqueClassDefined;
		this.isUniqueIndexDefined_ = isUniqueIndexDefined;
		this.areFunctionsParallel_ = areFunctionsParallel;
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
		return Collections.unmodifiableList(packages);
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
		return Collections.unmodifiableSet(indices);
	}

	public void addExternalFunction(ExternalFunction externalFunc)
	{
		externalFuncs.add(externalFunc);
	}

	public Collection<ExternalFunction> getExternalFunctions()
	{
		return Collections.unmodifiableSet(externalFuncs);
	}

	public void addExternalProcedure(ExternalProcedure externalProc)
	{
		externalProcs.add(externalProc);
	}

	public Collection<ExternalProcedure> getExternalProcedures()
	{
		return Collections.unmodifiableSet(externalProcs);
	}

	/** @return The types in the type model. */
	public Collection<Type> getTypes()
	{
		return Collections.unmodifiableList(types);
	}

	@Override
	public Collection<NodeType> getNodeTypes()
	{
		return Collections.unmodifiableSet(nodeTypes);
	}

	public Collection<NodeType> getAllNodeTypes()
	{
		if(allNodeTypes == null) {
			ArrayList<NodeType> allNodeTypes = new ArrayList<NodeType>();
			allNodeTypes.addAll(getNodeTypes());
			for(PackageType pt : getPackages()) {
				allNodeTypes.addAll(pt.getNodeTypes());
			}
			int typeID = 0;
			for(NodeType nt : allNodeTypes) {
				nt.setInheritanceTypeID(typeID);
				++typeID;
			}
			this.allNodeTypes = allNodeTypes;
		}
		return Collections.unmodifiableList(allNodeTypes);
	}

	@Override
	public Collection<EdgeType> getEdgeTypes()
	{
		return Collections.unmodifiableSet(edgeTypes);
	}

	public Collection<EdgeType> getAllEdgeTypes()
	{
		if(allEdgeTypes == null) {
			ArrayList<EdgeType> allEdgeTypes = new ArrayList<EdgeType>();
			allEdgeTypes.addAll(getEdgeTypes());
			for(PackageType pt : getPackages()) {
				allEdgeTypes.addAll(pt.getEdgeTypes());
			}
			int typeID = 0;
			for(EdgeType et : allEdgeTypes) {
				et.setInheritanceTypeID(typeID);
				++typeID;
			}
			this.allEdgeTypes = allEdgeTypes;
		}
		return Collections.unmodifiableList(allEdgeTypes);
	}
	
	public Collection<InheritanceType> getAllGraphElementTypes()
	{
		if(allGraphElementTypes == null) {
			ArrayList<InheritanceType> allNodeAndEdgeTypes = new ArrayList<InheritanceType>();
			allNodeAndEdgeTypes.addAll(getAllNodeTypes());
			allNodeAndEdgeTypes.addAll(getAllEdgeTypes());
			this.allGraphElementTypes = allNodeAndEdgeTypes;
		}
		return Collections.unmodifiableList(allGraphElementTypes);
	}

	@Override
	public Collection<InternalObjectType> getObjectTypes()
	{
		return Collections.unmodifiableSet(objectTypes);
	}

	public Collection<InternalObjectType> getAllObjectTypes()
	{
		if(allObjectTypes == null) {
			ArrayList<InternalObjectType> allObjectTypes = new ArrayList<InternalObjectType>();
			allObjectTypes.addAll(getObjectTypes());
			for(PackageType pt : getPackages()) {
				allObjectTypes.addAll(pt.getObjectTypes());
			}
			int typeID = 0;
			for(InternalObjectType ot : allObjectTypes) {
				ot.setInheritanceTypeID(typeID);
				++typeID;
			}
			this.allObjectTypes = allObjectTypes;
		}
		return Collections.unmodifiableList(allObjectTypes);
	}

	@Override
	public Collection<InternalTransientObjectType> getTransientObjectTypes()
	{
		return Collections.unmodifiableSet(transientObjectTypes);
	}

	public Collection<InternalTransientObjectType> getAllTransientObjectTypes()
	{
		if(allTransientObjectTypes == null) {
			ArrayList<InternalTransientObjectType> allTransientObjectTypes = new ArrayList<InternalTransientObjectType>();
			allTransientObjectTypes.addAll(getTransientObjectTypes());
			for(PackageType pt : getPackages()) {
				allTransientObjectTypes.addAll(pt.getTransientObjectTypes());
			}
			int typeID = 0;
			for(InternalTransientObjectType ot : allTransientObjectTypes) {
				ot.setInheritanceTypeID(typeID);
				++typeID;
			}
			this.allTransientObjectTypes = allTransientObjectTypes;
		}
		return Collections.unmodifiableList(allTransientObjectTypes);
	}

	public Collection<InheritanceType> getAllInheritanceTypes()
	{
		if(allInheritanceTypes == null) {
			ArrayList<InheritanceType> allInheritanceTypes = new ArrayList<InheritanceType>();
			allInheritanceTypes.addAll(getAllNodeTypes());
			allInheritanceTypes.addAll(getAllEdgeTypes());
			allInheritanceTypes.addAll(getAllObjectTypes());
			allInheritanceTypes.addAll(getAllTransientObjectTypes());
			this.allInheritanceTypes = allInheritanceTypes;
		}
		return Collections.unmodifiableList(allInheritanceTypes);
	}

	@Override
	public Collection<EnumType> getEnumTypes()
	{
		return Collections.unmodifiableSet(enumTypes);
	}

	public Collection<ExternalObjectType> getExternalObjectTypes()
	{
		return Collections.unmodifiableSet(externalObjectTypes);
	}

	public Collection<Model> getUsedModels()
	{
		return Collections.unmodifiableList(usedModels);
	}

	public boolean isEmitClassDefined()
	{
		return isEmitClassDefined_;
	}

	public boolean isEmitGraphClassDefined()
	{
		return isEmitGraphClassDefined_;
	}

	public boolean isCopyClassDefined()
	{
		return isCopyClassDefined_;
	}

	public boolean isEqualClassDefined()
	{
		return isEqualClassDefined_;
	}

	public boolean isLowerClassDefined()
	{
		return isLowerClassDefined_;
	}

	public boolean isGraphofDefined()
	{
		return isGraphofDefined_;
	}

	public boolean isUniqueDefined()
	{
		return isUniqueDefined_;
	}

	public void forceUniqueDefined()
	{
		isUniqueDefined_ = true;
	}

	public boolean isUniqueResulting()
	{
		return isUniqueResulting_;
	}

	public void forceUniqueResulting()
	{
		isUniqueResulting_ = true;
	}

	public boolean isUniqueClassDefined()
	{
		return isUniqueClassDefined_;
	}

	public boolean isUniqueIndexDefined()
	{
		return isUniqueIndexDefined_;
	}

	public void forceFunctionsParallel()
	{
		areFunctionsParallel_ = true;
	}

	public boolean areFunctionsParallel()
	{
		return areFunctionsParallel_;
	}

	public int getIsoParallel()
	{
		return isoParallel;
	}

	public int getSequencesParallel()
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
