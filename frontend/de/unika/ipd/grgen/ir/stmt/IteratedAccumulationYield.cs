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
using Rule = de.unika.ipd.grgen.ir.executable.Rule;
using Variable = de.unika.ipd.grgen.ir.pattern.Variable;

/// <summary>
/// Represents an accumulation yielding of an iterated match def variable in the IR.
/// </summary>
public class IteratedAccumulationYield : BlockNestingStatement
{
	private Variable iterationVar;
	private Rule iterated;

	public IteratedAccumulationYield(Variable accumulationVar, Rule iterated)
		: base("iterated accumulation yield")
	{
		this.iterationVar = accumulationVar;
		this.iterated = iterated;
	}

	public virtual Variable IterationVar
	{
		get
		{
		return iterationVar;
		}
	}

	public virtual Rule Iterated
	{
		get
		{
		return iterated;
		}
	}

	public override void CollectNeededEntities(NeededEntities needs)
	{
		foreach(EvalStatement accumulationStatement in statements)
			accumulationStatement.CollectNeededEntities(needs);
		if(needs.variables != null)
			needs.variables.Remove(iterationVar);
	}
}

}
