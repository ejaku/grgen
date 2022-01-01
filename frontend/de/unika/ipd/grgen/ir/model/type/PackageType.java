/*
 * GrGen: graph rewrite generator tool -- release GrGen.NET 6.5
 * Copyright (C) 2003-2022 Universitaet Karlsruhe, Institut fuer Programmstrukturen und Datenorganisation, LS Goos; and free programmers
 * licensed under LGPL v3 (see LICENSE.txt included in the packaging of this file)
 * www.grgen.net
 */

/**
 * @author Edgar Jakumeit
 */

package de.unika.ipd.grgen.ir.model.type;

import java.util.Collection;
import java.util.Collections;
import java.util.LinkedHashSet;
import java.util.LinkedList;
import java.util.List;
import java.util.Set;

import de.unika.ipd.grgen.ir.Ident;
import de.unika.ipd.grgen.ir.model.NodeEdgeEnumBearer;
import de.unika.ipd.grgen.ir.type.Type;
import de.unika.ipd.grgen.ir.type.basic.PrimitiveType;

/**
 * A package type.
 */
public class PackageType extends PrimitiveType implements NodeEdgeEnumBearer
{
	private List<Type> types = new LinkedList<Type>();
	private Set<NodeType> nodeTypes = new LinkedHashSet<NodeType>();
	private Set<EdgeType> edgeTypes = new LinkedHashSet<EdgeType>();
	private Set<InternalObjectType> objectTypes = new LinkedHashSet<InternalObjectType>();
	private Set<InternalTransientObjectType> transientObjectTypes = new LinkedHashSet<InternalTransientObjectType>();
	private Set<EnumType> enumTypes = new LinkedHashSet<EnumType>();

	/** Make a new package type.
	 *  @param ident The identifier of this package. */
	public PackageType(Ident ident)
	{
		super("package type", ident);
	}

	/** Add the given type to the type model. */
	public void addType(Type type)
	{
		types.add(type);
		if(type instanceof NodeType) {
			NodeType nt = (NodeType)type;
			nt.setPackageContainedIn(getIdent().toString());
			nodeTypes.add(nt);
		} else if(type instanceof EdgeType) {
			EdgeType et = (EdgeType)type;
			et.setPackageContainedIn(getIdent().toString());
			edgeTypes.add(et);
		} else if(type instanceof InternalObjectType) {
			InternalObjectType ot = (InternalObjectType)type;
			ot.setPackageContainedIn(getIdent().toString());
			objectTypes.add(ot);
		} else if(type instanceof InternalTransientObjectType) {
			InternalTransientObjectType tot = (InternalTransientObjectType)type;
			tot.setPackageContainedIn(getIdent().toString());
			transientObjectTypes.add(tot);
		} else if(type instanceof EnumType) {
			EnumType enut = (EnumType)type;
			enut.setPackageContainedIn(getIdent().toString());
			enumTypes.add(enut);
		} else
			assert false : "Unexpected type added to package: " + type;
	}

	public Collection<Type> getTypes()
	{
		return Collections.unmodifiableCollection(types);
	}

	@Override
	public Collection<NodeType> getNodeTypes()
	{
		return Collections.unmodifiableCollection(nodeTypes);
	}

	@Override
	public Collection<EdgeType> getEdgeTypes()
	{
		return Collections.unmodifiableCollection(edgeTypes);
	}

	@Override
	public Collection<InternalObjectType> getObjectTypes()
	{
		return Collections.unmodifiableCollection(objectTypes);
	}

	@Override
	public Collection<InternalTransientObjectType> getTransientObjectTypes()
	{
		return Collections.unmodifiableCollection(transientObjectTypes);
	}

	@Override
	public Collection<EnumType> getEnumTypes()
	{
		return Collections.unmodifiableCollection(enumTypes);
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

	@Override
	public void addToDigest(StringBuffer sb)
	{
		sb.append(this);
		sb.append('[');

		for(Type ty : types) {
			ty.addToDigest(sb);
		}

		sb.append(']');
	}
}
