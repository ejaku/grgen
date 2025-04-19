/*
 * GrGen: graph rewrite generator tool -- release GrGen.NET 7.2
 * Copyright (C) 2003-2025 Universitaet Karlsruhe, Institut fuer Programmstrukturen und Datenorganisation, LS Goos; and free programmers
 * licensed under LGPL v3 (see LICENSE.txt included in the packaging of this file)
 * www.grgen.net
 */

/**
 * @author Edgar Jakumeit
 */

package de.unika.ipd.grgen.ir.pattern;

import java.util.ArrayList;

import de.unika.ipd.grgen.ir.FilterInvocation;
import de.unika.ipd.grgen.ir.FilterInvocationBase;
import de.unika.ipd.grgen.ir.FilterInvocationLambdaExpression;
import de.unika.ipd.grgen.ir.NeededEntities;
import de.unika.ipd.grgen.ir.executable.Rule;
import de.unika.ipd.grgen.ir.expr.Expression;
import de.unika.ipd.grgen.ir.stmt.EvalStatement;

public class IteratedFiltering extends EvalStatement
{
	Rule actionOrSubpattern;
	Rule iterated;
	ArrayList<FilterInvocationBase> filterInvocations = new ArrayList<FilterInvocationBase>();

	public IteratedFiltering(String name, Rule actionOrSubpattern, Rule iterated)
	{
		super(name);
		this.actionOrSubpattern = actionOrSubpattern;
		this.iterated = iterated;
	}

	public void addFilterInvocation(FilterInvocationBase filterInvocation)
	{
		filterInvocations.add(filterInvocation);
	}

	public Rule getActionOrSubpattern()
	{
		return actionOrSubpattern;
	}

	public Rule getIterated()
	{
		return iterated;
	}

	public ArrayList<FilterInvocationBase> getFilterInvocations()
	{
		return filterInvocations;
	}

	public FilterInvocationBase getFilterInvocation(int i)
	{
		return filterInvocations.get(i);
	}

	@Override
	public void collectNeededEntities(NeededEntities needs)
	{
		for(FilterInvocationBase filterInvocation : filterInvocations) {
			if(filterInvocation instanceof FilterInvocation) {
				FilterInvocation fi = (FilterInvocation)filterInvocation;
				for(Expression filterArgument : fi.getFilterArguments()) {
					filterArgument.collectNeededEntities(needs);
				}
			} else {
				FilterInvocationLambdaExpression file = (FilterInvocationLambdaExpression)filterInvocation;
				file.collectNeededEntities(needs);
			}
		}
	}
}
