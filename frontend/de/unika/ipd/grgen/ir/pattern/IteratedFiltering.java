/*
 * GrGen: graph rewrite generator tool -- release GrGen.NET 5.0
 * Copyright (C) 2003-2020 Universitaet Karlsruhe, Institut fuer Programmstrukturen und Datenorganisation, LS Goos; and free programmers
 * licensed under LGPL v3 (see LICENSE.txt included in the packaging of this file)
 * www.grgen.net
 */

/**
 * @author Edgar Jakumeit
 */

package de.unika.ipd.grgen.ir.pattern;

import java.util.ArrayList;

import de.unika.ipd.grgen.ir.FilterInvocation;
import de.unika.ipd.grgen.ir.NeededEntities;
import de.unika.ipd.grgen.ir.Rule;
import de.unika.ipd.grgen.ir.expr.Expression;
import de.unika.ipd.grgen.ir.stmt.EvalStatement;

public class IteratedFiltering extends EvalStatement
{
	Rule actionOrSubpattern;
	Rule iterated;
	ArrayList<FilterInvocation> filterInvocations = new ArrayList<FilterInvocation>();

	public IteratedFiltering(String name, Rule actionOrSubpattern, Rule iterated)
	{
		super(name);
		this.actionOrSubpattern = actionOrSubpattern;
		this.iterated = iterated;
	}

	public void addFilterInvocation(FilterInvocation filterInvocation)
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

	public ArrayList<FilterInvocation> getFilterInvocations()
	{
		return filterInvocations;
	}

	public FilterInvocation getFilterInvocation(int i)
	{
		return filterInvocations.get(i);
	}

	public void collectNeededEntities(NeededEntities needs)
	{
		for(FilterInvocation filterInvocation : filterInvocations) {
			for(Expression filterArgument : filterInvocation.filterArguments) {
				filterArgument.collectNeededEntities(needs);
			}
		}
	}
}
