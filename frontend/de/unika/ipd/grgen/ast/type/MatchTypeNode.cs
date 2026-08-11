/*
 * GrGen: graph rewrite generator tool -- release GrGen.NET 8.1
 * Copyright (C) 2003-2026 Universitaet Karlsruhe, Institut fuer Programmstrukturen und Datenorganisation, LS Goos; and free programmers
 * licensed under LGPL v3, some components/parts use different licenses (see LICENSE.txt included in the packaging of this file)
 * www.grgen.de / www.grgen.net
 */

/// <summary>
/// @author Edgar Jakumeit
/// </summary>

namespace de.unika.ipd.grgen.ast.type
{

using System;
using System.Collections.Generic;

using MemberAccessor = de.unika.ipd.grgen.ast.MemberAccessor;
using DeclNode = de.unika.ipd.grgen.ast.decl.DeclNode;

// base class for the different match types (action, iterated, defined=match class)
public abstract class MatchTypeNode : DeclaredTypeNode, MemberAccessor
{
	static MatchTypeNode()
	{
		SetClassName(typeof(MatchTypeNode), "match type");
	}

	public override string Name
	{
		get
		{
			return TypeName;
		}
	}

	public override abstract DeclNode TryGetMember(string name);

	public abstract ISet<DeclNode> Entities {get;}

	// get set of names of contained entities excluding anonymous entities
	public virtual ISet<string> NamesOfEntities
	{
		get
		{
			ISet<string> set = new HashSet<string>();
			foreach(DeclNode entity in Entities)
			{
				string name = entity.ident.ToString();
				if(!name.StartsWith("$", StringComparison.Ordinal))
					set.Add(name);
			}
			return set;
		}
	}
}

}
