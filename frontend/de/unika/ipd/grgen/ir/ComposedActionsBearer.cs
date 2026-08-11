/*
 * GrGen: graph rewrite generator tool -- release GrGen.NET 8.1
 * Copyright (C) 2003-2026 Universitaet Karlsruhe, Institut fuer Programmstrukturen und Datenorganisation, LS Goos; and free programmers
 * licensed under LGPL v3, some components/parts use different licenses (see LICENSE.txt included in the packaging of this file)
 * www.grgen.de / www.grgen.net
 */

/// <summary>
/// @author Edgar Jakumeit
/// </summary>

namespace de.unika.ipd.grgen.ir
{

	using System.Collections.Generic;

	using FilterFunction = de.unika.ipd.grgen.ir.executable.FilterFunction;
	using Function = de.unika.ipd.grgen.ir.executable.Function;
	using MatchClassFilterFunction = de.unika.ipd.grgen.ir.executable.MatchClassFilterFunction;
	using Procedure = de.unika.ipd.grgen.ir.executable.Procedure;
	using Rule = de.unika.ipd.grgen.ir.executable.Rule;
	using Sequence = de.unika.ipd.grgen.ir.executable.Sequence;
	using DefinedMatchType = de.unika.ipd.grgen.ir.type.DefinedMatchType;

	/// <summary>
	/// Offers all the actions in the unit including all the packages for flat iteration.
	/// TODO: offer this by implementing iterators instead of collection building
	/// </summary>
	public class ComposedActionsBearer : ActionsBearer
	{
		internal Unit unit;

		internal List<Rule> subpatRules;
		internal List<Rule> rules;
		internal List<FilterFunction> filterFunctions;
		internal List<DefinedMatchType> matchClasses;
		internal List<MatchClassFilterFunction> matchClassFilterFunctions;
		internal List<Function> functions;
		internal List<Procedure> procedures;
		internal List<Sequence> sequences;

		public ComposedActionsBearer(Unit unit)
		{
			this.unit = unit;
		}

		public virtual ICollection<Rule> SubpatternRules
		{
			get
			{
				if(subpatRules == null)
				{
					List<Rule> subpatRules = new List<Rule>(unit.SubpatternRules);
					foreach(ActionsBearer p in unit.Packages)
						subpatRules.AddRange(p.SubpatternRules);
					this.subpatRules = subpatRules;
				}
				return subpatRules.AsReadOnly();
			}
		}

		public virtual ICollection<Rule> ActionRules
		{
			get
			{
				if(rules == null)
				{
					List<Rule> rules = new List<Rule>(unit.ActionRules);
					foreach(ActionsBearer p in unit.Packages)
						rules.AddRange(p.ActionRules);
					this.rules = rules;
				}
				return rules.AsReadOnly();
			}
		}

		public virtual ICollection<FilterFunction> FilterFunctions
		{
			get
			{
				if(filterFunctions == null)
				{
					List<FilterFunction> filterFunctions = new List<FilterFunction>(unit.FilterFunctions);
					foreach(ActionsBearer p in unit.Packages)
						filterFunctions.AddRange(p.FilterFunctions);
					this.filterFunctions = filterFunctions;
				}
				return filterFunctions.AsReadOnly();
			}
		}

		public virtual ICollection<DefinedMatchType> MatchClasses
		{
			get
			{
				if(matchClasses == null)
				{
					List<DefinedMatchType> matchClasses = new List<DefinedMatchType>(unit.MatchClasses);
					foreach(ActionsBearer p in unit.Packages)
						matchClasses.AddRange(p.MatchClasses);
					this.matchClasses = matchClasses;
				}
				return matchClasses.AsReadOnly();
			}
		}

		public virtual ICollection<MatchClassFilterFunction> MatchClassFilterFunctions
		{
			get
			{
				if(matchClassFilterFunctions == null)
				{
					List<MatchClassFilterFunction> matchClassFilterFunctions = new List<MatchClassFilterFunction>(unit.MatchClassFilterFunctions);
					foreach(ActionsBearer p in unit.Packages)
						matchClassFilterFunctions.AddRange(p.MatchClassFilterFunctions);
					this.matchClassFilterFunctions = matchClassFilterFunctions;
				}
				return matchClassFilterFunctions.AsReadOnly();
			}
		}

		public virtual ICollection<Function> Functions
		{
			get
			{
				if(functions == null)
				{
					List<Function> functions = new List<Function>(unit.Functions);
					foreach(ActionsBearer p in unit.Packages)
						functions.AddRange(p.Functions);
					this.functions = functions;
				}
				return functions.AsReadOnly();
			}
		}

		public virtual ICollection<Procedure> Procedures
		{
			get
			{
				if(procedures == null)
				{
					List<Procedure> procedures = new List<Procedure>(unit.Procedures);
					foreach(ActionsBearer p in unit.Packages)
						procedures.AddRange(p.Procedures);
					this.procedures = procedures;
				}
				return procedures.AsReadOnly();
			}
		}

		public virtual ICollection<Sequence> Sequences
		{
			get
			{
				if(sequences == null)
				{
					List<Sequence> sequences = new List<Sequence>(unit.Sequences);
					foreach(ActionsBearer p in unit.Packages)
						sequences.AddRange(p.Sequences);
					this.sequences = sequences;
				}
				return sequences.AsReadOnly();
			}
		}
	}

}
