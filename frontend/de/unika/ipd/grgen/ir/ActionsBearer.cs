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
	/// A type bearing all the different actions available in the rules language.
	/// </summary>
	public interface ActionsBearer
	{
		ICollection<Rule> SubpatternRules {get;}

		ICollection<Rule> ActionRules {get;}

		ICollection<FilterFunction> FilterFunctions {get;}

		ICollection<DefinedMatchType> MatchClasses {get;}

		ICollection<MatchClassFilterFunction> MatchClassFilterFunctions {get;}

		ICollection<Function> Functions {get;}

		ICollection<Procedure> Procedures {get;}

		ICollection<Sequence> Sequences {get;}
	}

}
