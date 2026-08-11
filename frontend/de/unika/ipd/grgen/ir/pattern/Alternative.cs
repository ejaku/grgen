/*
 * GrGen: graph rewrite generator tool -- release GrGen.NET 8.1
 * Copyright (C) 2003-2026 Universitaet Karlsruhe, Institut fuer Programmstrukturen und Datenorganisation, LS Goos; and free programmers
 * licensed under LGPL v3, some components/parts use different licenses (see LICENSE.txt included in the packaging of this file)
 * www.grgen.de / www.grgen.net
 */

/// <summary>
/// @author Edgar Jakumeit
/// </summary>

namespace de.unika.ipd.grgen.ir.pattern
{

using System.Collections.Generic;

using Ident = de.unika.ipd.grgen.ir.Ident;
using Identifiable = de.unika.ipd.grgen.ir.Identifiable;
using Rule = de.unika.ipd.grgen.ir.executable.Rule;

/// <summary>
/// Represents an alternative statement in the IR.
/// </summary>
public class Alternative : Identifiable
{
	public Alternative(Ident ident)
		: base("alternative", ident)
	{
	}

	internal IList<Rule> alternativeCases = new List<Rule>();

	/// <summary>
	/// Was the replacement code already called by means of an alternative replacement declaration? </summary>
	public bool wasReplacementAlreadyCalled;

	public virtual ICollection<Rule> AlternativeCases
	{
		get
		{
			return alternativeCases;
		}
	}

	public virtual void AddAlternativeCase(Rule alternativeCaseRule)
	{
		alternativeCases.Add(alternativeCaseRule);
	}

	public virtual string NameOfGraph
	{
		get
		{
			return Ident.ToString();
		}
	}
}

}
