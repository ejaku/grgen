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

using FilterInvocation = de.unika.ipd.grgen.ir.FilterInvocation;
using FilterInvocationBase = de.unika.ipd.grgen.ir.FilterInvocationBase;
using FilterInvocationLambdaExpression = de.unika.ipd.grgen.ir.FilterInvocationLambdaExpression;
using NeededEntities = de.unika.ipd.grgen.ir.NeededEntities;
using Rule = de.unika.ipd.grgen.ir.executable.Rule;
using Expression = de.unika.ipd.grgen.ir.expr.Expression;
using EvalStatement = de.unika.ipd.grgen.ir.stmt.EvalStatement;

public class IteratedFiltering : EvalStatement
{
	internal Rule actionOrSubpattern;
	internal Rule iterated;
	internal List<FilterInvocationBase> filterInvocations = new List<FilterInvocationBase>();

	public IteratedFiltering(string name, Rule actionOrSubpattern, Rule iterated)
		: base(name)
	{
		this.actionOrSubpattern = actionOrSubpattern;
		this.iterated = iterated;
	}

	public virtual void AddFilterInvocation(FilterInvocationBase filterInvocation)
	{
		filterInvocations.Add(filterInvocation);
	}

	public virtual Rule ActionOrSubpattern
	{
		get
		{
		return actionOrSubpattern;
		}
	}

	public virtual Rule Iterated
	{
		get
		{
		return iterated;
		}
	}

	public virtual List<FilterInvocationBase> FilterInvocations
	{
		get
		{
		return filterInvocations;
		}
	}

	public virtual FilterInvocationBase GetFilterInvocation(int i)
	{
		return filterInvocations[i];
	}

	public override void CollectNeededEntities(NeededEntities needs)
	{
		foreach(FilterInvocationBase filterInvocation in filterInvocations)
		{
			if(filterInvocation is FilterInvocation)
			{
				FilterInvocation fi = (FilterInvocation)filterInvocation;
				foreach(Expression filterArgument in fi.FilterArguments)
					filterArgument.CollectNeededEntities(needs);
			}
			else
			{
				FilterInvocationLambdaExpression file = (FilterInvocationLambdaExpression)filterInvocation;
				file.CollectNeededEntities(needs);
			}
		}
	}
}

}
