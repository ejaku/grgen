/*
 * GrGen: graph rewrite generator tool -- release GrGen.NET 8.1
 * Copyright (C) 2003-2026 Universitaet Karlsruhe, Institut fuer Programmstrukturen und Datenorganisation, LS Goos; and free programmers
 * licensed under LGPL v3, some components/parts use different licenses (see LICENSE.txt included in the packaging of this file)
 * www.grgen.de / www.grgen.net
 */

/// <summary>
/// @author shack
/// </summary>

namespace de.unika.ipd.grgen.ir.model.type
{
using ContainedInPackage = de.unika.ipd.grgen.ir.ContainedInPackage;
using Ident = de.unika.ipd.grgen.ir.Ident;

/// <summary>
/// IR class that represents node types.
/// </summary>
public class NodeType : InheritanceType, ContainedInPackage
{
	private string packageContainedIn;

	/// <summary>
	/// Make a new node type. </summary>
	/// <param name="ident"> The identifier that declares this type. </param>
	/// <param name="modifiers"> The modifiers for this type. </param>
	/// <param name="externalName"> The name of the external implementation of this type or null. </param>
	public NodeType(Ident ident, int modifiers, string externalName)
		: base("node type", ident, modifiers, externalName)
	{
	}

	/// <seealso cref="de.unika.ipd.grgen.ir.type.Type.classify() "/>
	public override TypeClass Classify()
	{
		return TypeClass.IS_NODE;
	}

	public virtual string PackageContainedIn
	{
		get
		{
			return packageContainedIn;
		}
		set
		{
			this.packageContainedIn = value;
		}
	}

}

}
