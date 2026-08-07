/*
 * GrGen: graph rewrite generator tool -- release GrGen.NET 8.1
 * Copyright (C) 2003-2026 Universitaet Karlsruhe, Institut fuer Programmstrukturen und Datenorganisation, LS Goos; and free programmers
 * licensed under LGPL v3, some components/parts use different licenses (see LICENSE.txt included in the packaging of this file)
 * www.grgen.de / www.grgen.net
 */

/// <summary>
/// @author Edgar Jakumeit
/// </summary>

namespace de.unika.ipd.grgen.ir.type
{
using ContainedInPackage = de.unika.ipd.grgen.ir.ContainedInPackage;
using Ident = de.unika.ipd.grgen.ir.Ident;
using Rule = de.unika.ipd.grgen.ir.executable.Rule;

public class MatchType : Type, ContainedInPackage
{
	private string packageContainedIn;
	private Rule action;

	public MatchType(Ident ident)
		: base("match type", ident)
	{
	}

	public virtual Rule Action
	{
		set
		{
		this.action = value;
		}
		get
		{
		return action;
		}
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



	/// <seealso cref="de.unika.ipd.grgen.ir.type.Type.classify() "/>
	public override TypeClass Classify()
	{
		return TypeClass.IS_MATCH;
	}
}

}
