/*
 * GrGen: graph rewrite generator tool -- release GrGen.NET 6.7
 * Copyright (C) 2003-2023 Universitaet Karlsruhe, Institut fuer Programmstrukturen und Datenorganisation, LS Goos; and free programmers
 * licensed under LGPL v3 (see LICENSE.txt included in the packaging of this file)
 * www.grgen.net
 */

/**
 * @author Edgar Jakumeit
 */

package de.unika.ipd.grgen.ir.executable;

import de.unika.ipd.grgen.ir.IR;

/**
 * An auto-supplied filter.
 */
public class FilterAutoSupplied extends IR implements Filter
{
	protected String name;

	/** The action we're a filter for */
	protected Rule action;

	public FilterAutoSupplied(String name)
	{
		super(name);
		this.name = name;
	}

	public void setAction(Rule action)
	{
		this.action = action;
	}

	@Override
	public Rule getAction()
	{
		return action;
	}

	public String getFilterName()
	{
		return name;
	}
}
