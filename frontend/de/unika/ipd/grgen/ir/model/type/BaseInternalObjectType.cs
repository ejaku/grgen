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
using ContainedInPackage = de.unika.ipd.grgen.ir.ContainedInPackage;
using Ident = de.unika.ipd.grgen.ir.Ident;

/// <summary>
/// IR class that represents a base for internal (non-node/edge) object types (i.e. classes).
/// </summary>
public class BaseInternalObjectType : InheritanceType, ContainedInPackage
{
	private string packageContainedIn;

	/// <summary>
	/// Make a new base internal object type. </summary>
	/// <param name="ident"> The identifier that declares this type. </param>
	/// <param name="modifiers"> The modifiers for this type. </param>
	public BaseInternalObjectType(string name, Ident ident, int modifiers)
		: base(name, ident, modifiers, null)
	{
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
