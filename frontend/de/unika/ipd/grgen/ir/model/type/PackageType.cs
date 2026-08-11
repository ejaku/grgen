/*
 * GrGen: graph rewrite generator tool -- release GrGen.NET 8.1
 * Copyright (C) 2003-2026 Universitaet Karlsruhe, Institut fuer Programmstrukturen und Datenorganisation, LS Goos; and free programmers
 * licensed under LGPL v3, some components/parts use different licenses (see LICENSE.txt included in the packaging of this file)
 * www.grgen.de / www.grgen.net
 */

/// <summary>
/// @author Edgar Jakumeit
/// </summary>

namespace de.unika.ipd.grgen.ir.model.type
{

using System.Collections.Generic;
using System.Diagnostics;
using System.Text;

using Ident = de.unika.ipd.grgen.ir.Ident;
using NodeEdgeEnumBearer = de.unika.ipd.grgen.ir.model.NodeEdgeEnumBearer;
using Type = de.unika.ipd.grgen.ir.type.Type;
using PrimitiveType = de.unika.ipd.grgen.ir.type.basic.PrimitiveType;

/// <summary>
/// A package type.
/// </summary>
public class PackageType : PrimitiveType, NodeEdgeEnumBearer
{
	private List<Type> types = new List<Type>();
	private ISet<NodeType> nodeTypes = new LinkedHashSet<NodeType>();
	private ISet<EdgeType> edgeTypes = new LinkedHashSet<EdgeType>();
	private ISet<InternalObjectType> objectTypes = new LinkedHashSet<InternalObjectType>();
	private ISet<InternalTransientObjectType> transientObjectTypes = new LinkedHashSet<InternalTransientObjectType>();
	private ISet<EnumType> enumTypes = new LinkedHashSet<EnumType>();

	/// <summary>
	/// Make a new package type. </summary>
	///  <param name="ident"> The identifier of this package.  </param>
	public PackageType(Ident ident)
		: base("package type", ident)
	{
	}

	/// <summary>
	/// Add the given type to the type model. </summary>
	public virtual void AddType(Type type)
	{
		types.Add(type);
		if(type is NodeType)
		{
			NodeType nt = (NodeType)type;
			nt.PackageContainedIn = Ident.ToString();
			nodeTypes.Add(nt);
		}
		else if(type is EdgeType)
		{
			EdgeType et = (EdgeType)type;
			et.PackageContainedIn = Ident.ToString();
			edgeTypes.Add(et);
		}
		else if(type is InternalObjectType)
		{
			InternalObjectType ot = (InternalObjectType)type;
			ot.PackageContainedIn = Ident.ToString();
			objectTypes.Add(ot);
		}
		else if(type is InternalTransientObjectType)
		{
			InternalTransientObjectType tot = (InternalTransientObjectType)type;
			tot.PackageContainedIn = Ident.ToString();
			transientObjectTypes.Add(tot);
		}
		else if(type is EnumType)
		{
			EnumType enut = (EnumType)type;
			enut.PackageContainedIn = Ident.ToString();
			enumTypes.Add(enut);
		}
		else
			Debug.Assert(false, "Unexpected type added to package: " + type);
	}

	public virtual ICollection<Type> Types
	{
		get
		{
			return types.AsReadOnly();
		}
	}

	public virtual ICollection<NodeType> NodeTypes
	{
		get
		{
			return Collections.UnmodifiableSet(nodeTypes);
		}
	}

	public virtual ICollection<EdgeType> EdgeTypes
	{
		get
		{
			return Collections.UnmodifiableSet(edgeTypes);
		}
	}

	public virtual ICollection<InternalObjectType> ObjectTypes
	{
		get
		{
			return Collections.UnmodifiableSet(objectTypes);
		}
	}

	public virtual ICollection<InternalTransientObjectType> TransientObjectTypes
	{
		get
		{
			return Collections.UnmodifiableSet(transientObjectTypes);
		}
	}

	public virtual ICollection<EnumType> EnumTypes
	{
		get
		{
			return Collections.UnmodifiableSet(enumTypes);
		}
	}

	/// <summary>
	/// Canonicalize the type model. </summary>
	protected internal override void CanonicalizeLocal()
	{
		//Collections.sort(types, Identifiable.COMPARATOR);
		//Collections.sort(types);

		foreach(Type ty in types)
		{
			ty.Canonicalize();
			if(ty is EdgeType)
				((EdgeType)ty).CanonicalizeConnectionAsserts();
		}
	}

	public override void AddToDigest(StringBuilder sb)
	{
		sb.Append(this);
		sb.Append('[');

		foreach(Type ty in types)
			ty.AddToDigest(sb);

		sb.Append(']');
	}
}

}
