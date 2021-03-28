/*
 * GrGen: graph rewrite generator tool -- release GrGen.NET 6.0
 * Copyright (C) 2003-2021 Universitaet Karlsruhe, Institut fuer Programmstrukturen und Datenorganisation, LS Goos; and free programmers
 * licensed under LGPL v3 (see LICENSE.txt included in the packaging of this file)
 * www.grgen.net
 */

/**
 * @author Edgar Jakumeit
 */

package de.unika.ipd.grgen.ir;

import de.unika.ipd.grgen.ir.executable.Rule;

public class FilterInvocationBase extends Identifiable
{
	protected Rule iteratedAction;

	public FilterInvocationBase(String name, Ident ident, Rule iteratedAction)
	{
		super(name, ident);
		this.iteratedAction = iteratedAction;
	}
	
	public Rule getIteratedAction()
	{
		return iteratedAction;
	}
}
