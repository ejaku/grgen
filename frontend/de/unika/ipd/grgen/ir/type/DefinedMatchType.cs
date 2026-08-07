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

using System.Collections.Generic;

using ContainedInPackage = de.unika.ipd.grgen.ir.ContainedInPackage;
using Ident = de.unika.ipd.grgen.ir.Ident;
using MatchClassFilter = de.unika.ipd.grgen.ir.executable.MatchClassFilter;
using Edge = de.unika.ipd.grgen.ir.pattern.Edge;
using Node = de.unika.ipd.grgen.ir.pattern.Node;
using PatternGraphLhs = de.unika.ipd.grgen.ir.pattern.PatternGraphLhs;
using Variable = de.unika.ipd.grgen.ir.pattern.Variable;

public class DefinedMatchType : CompoundType, ContainedInPackage
{
	private string packageContainedIn;
	private PatternGraphLhs pattern;
	private List<MatchClassFilter> matchClassFilters;

	public DefinedMatchType(string name, Ident ident, PatternGraphLhs pattern)
		: base(name, ident)
	{
		this.pattern = pattern;
		matchClassFilters = new List<MatchClassFilter>();
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


	public virtual void AddMatchClassFilter(MatchClassFilter filter)
	{
		matchClassFilters.Add(filter);
	}

	public virtual IList<MatchClassFilter> MatchClassFilters
	{
		get
		{
		return matchClassFilters.AsReadOnly();
		}
	}

	public virtual PatternGraphLhs PatternGraph
	{
		get
		{
		return pattern;
		}
	}

	public virtual ICollection<Node> Nodes
	{
		get
		{
		return pattern.Nodes;
		}
	}

	public virtual ICollection<Edge> Edges
	{
		get
		{
		return pattern.Edges;
		}
	}

	public virtual ICollection<Variable> Vars
	{
		get
		{
		return pattern.Vars;
		}
	}

	/// <seealso cref="de.unika.ipd.grgen.ir.type.Type.classify() "/>
	public override TypeClass Classify()
	{
		return TypeClass.IS_DEFINED_MATCH;
	}
}

}
