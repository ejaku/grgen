/*
 * GrGen: graph rewrite generator tool -- release GrGen.NET 8.1
 * Copyright (C) 2003-2026 Universitaet Karlsruhe, Institut fuer Programmstrukturen und Datenorganisation, LS Goos; and free programmers
 * licensed under LGPL v3, some components/parts use different licenses (see LICENSE.txt included in the packaging of this file)
 * www.grgen.de / www.grgen.net
 */

/// <summary>
/// @author Edgar Jakumeit
/// </summary>

namespace de.unika.ipd.grgen.ir.stmt
{
	using de.unika.ipd.grgen.ir;
	using Variable = de.unika.ipd.grgen.ir.pattern.Variable;

	/// <summary>
	/// Represents an accumulation yielding of a matches variable in the IR.
	/// </summary>
	public class MatchesAccumulationYield : BlockNestingStatement
	{
		private Variable iterationVar;
		private Variable matchesVar;

		public MatchesAccumulationYield(Variable iterationVar, Variable matchesVar)
			: base("matches accumulation yield")
		{
			this.iterationVar = iterationVar;
			this.matchesVar = matchesVar;
		}

		public virtual Variable IterationVar
		{
			get
			{
				return iterationVar;
			}
		}

		public virtual Variable MatchesVar
		{
			get
			{
				return matchesVar;
			}
		}

		public override void CollectNeededEntities(NeededEntities needs)
		{
			if(!IsGlobalVariable(matchesVar))
				needs.Add(matchesVar);
			foreach(EvalStatement accumulationStatement in statements)
				accumulationStatement.CollectNeededEntities(needs);
			if(needs.variables != null)
				needs.variables.Remove(iterationVar);
		}
	}

}
