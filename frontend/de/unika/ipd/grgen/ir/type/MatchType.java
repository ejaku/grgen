/*
 * GrGen: graph rewrite generator tool -- release GrGen.NET 7.1
 * Copyright (C) 2003-2025 Universitaet Karlsruhe, Institut fuer Programmstrukturen und Datenorganisation, LS Goos; and free programmers
 * licensed under LGPL v3 (see LICENSE.txt included in the packaging of this file)
 * www.grgen.net
 */

/**
 * @author Edgar Jakumeit
 */

package de.unika.ipd.grgen.ir.type;

import de.unika.ipd.grgen.ir.ContainedInPackage;
import de.unika.ipd.grgen.ir.Ident;
import de.unika.ipd.grgen.ir.executable.Rule;

public class MatchType extends Type implements ContainedInPackage
{
	private String packageContainedIn;
	private Rule action;

	public MatchType(Ident ident)
	{
		super("match type", ident);
	}

	public void setAction(Rule action)
	{
		this.action = action;
	}

	@Override
	public String getPackageContainedIn()
	{
		return packageContainedIn;
	}

	public void setPackageContainedIn(String packageContainedIn)
	{
		this.packageContainedIn = packageContainedIn;
	}

	public Rule getAction()
	{
		return action;
	}

	/** @see de.unika.ipd.grgen.ir.type.Type#classify() */
	@Override
	public TypeClass classify()
	{
		return TypeClass.IS_MATCH;
	}
}
