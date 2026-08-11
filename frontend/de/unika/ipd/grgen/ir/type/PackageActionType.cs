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

using ActionsBearer = de.unika.ipd.grgen.ir.ActionsBearer;
using Ident = de.unika.ipd.grgen.ir.Ident;
using FilterFunction = de.unika.ipd.grgen.ir.executable.FilterFunction;
using Function = de.unika.ipd.grgen.ir.executable.Function;
using MatchClassFilterFunction = de.unika.ipd.grgen.ir.executable.MatchClassFilterFunction;
using Procedure = de.unika.ipd.grgen.ir.executable.Procedure;
using Rule = de.unika.ipd.grgen.ir.executable.Rule;
using Sequence = de.unika.ipd.grgen.ir.executable.Sequence;
using PrimitiveType = de.unika.ipd.grgen.ir.type.basic.PrimitiveType;

/// <summary>
/// A package type, for packages from the actions (in contrast to the models).
/// </summary>
public class PackageActionType : PrimitiveType, ActionsBearer
{
	private readonly List<Rule> subpatternRules = new List<Rule>();

	private readonly List<Rule> actionRules = new List<Rule>();

	private readonly List<FilterFunction> filterFunctions = new List<FilterFunction>();

	private readonly List<DefinedMatchType> matchClasses = new List<DefinedMatchType>();

	private readonly List<MatchClassFilterFunction> matchClassFilterFunctions = new List<MatchClassFilterFunction>();

	private readonly List<Function> functions = new List<Function>();

	private readonly List<Procedure> procedures = new List<Procedure>();

	private readonly List<Sequence> sequences = new List<Sequence>();

	/// <summary>
	/// Make a new package action type. </summary>
	///  <param name="ident"> The identifier of this package.  </param>
	public PackageActionType(Ident ident)
		: base("package action type", ident)
	{
	}

	/// <summary>
	/// Add a subpattern-rule to the unit. </summary>
	public virtual void AddSubpatternRule(Rule subpatternRule)
	{
		subpatternRules.Add(subpatternRule);
	}

	public virtual ICollection<Rule> SubpatternRules
	{
		get
		{
			return subpatternRules.AsReadOnly();
		}
	}

	/// <summary>
	/// Add an action-rule to the unit. </summary>
	public virtual void AddActionRule(Rule actionRule)
	{
		actionRules.Add(actionRule);
	}

	public virtual ICollection<Rule> ActionRules
	{
		get
		{
			return actionRules.AsReadOnly();
		}
	}

	/// <summary>
	/// Add a filter function to the unit. </summary>
	public virtual void AddFilterFunction(FilterFunction filterFunction)
	{
		filterFunctions.Add(filterFunction);
	}

	public virtual ICollection<FilterFunction> FilterFunctions
	{
		get
		{
			return filterFunctions.AsReadOnly();
		}
	}

	/// <summary>
	/// Add a match class to the unit. </summary>
	public virtual void AddMatchClass(DefinedMatchType matchClass)
	{
		matchClasses.Add(matchClass);
	}

	public virtual ICollection<DefinedMatchType> MatchClasses
	{
		get
		{
			return matchClasses.AsReadOnly();
		}
	}

	/// <summary>
	/// Add a match filter function to the unit. </summary>
	public virtual void AddMatchClassFilterFunction(MatchClassFilterFunction matchClassFilterFunction)
	{
		matchClassFilterFunctions.Add(matchClassFilterFunction);
	}

	public virtual ICollection<MatchClassFilterFunction> MatchClassFilterFunctions
	{
		get
		{
			return matchClassFilterFunctions.AsReadOnly();
		}
	}

	/// <summary>
	/// Add a function to the unit. </summary>
	public virtual void AddFunction(Function function)
	{
		functions.Add(function);
	}

	public virtual ICollection<Function> Functions
	{
		get
		{
			return functions.AsReadOnly();
		}
	}

	/// <summary>
	/// Add a procedure to the unit. </summary>
	public virtual void AddProcedure(Procedure procedure)
	{
		procedures.Add(procedure);
	}

	public virtual ICollection<Procedure> Procedures
	{
		get
		{
			return procedures.AsReadOnly();
		}
	}

	/// <summary>
	/// Add a sequence to the unit. </summary>
	public virtual void AddSequence(Sequence sequence)
	{
		sequences.Add(sequence);
	}

	public virtual ICollection<Sequence> Sequences
	{
		get
		{
			return sequences.AsReadOnly();
		}
	}
}

}
