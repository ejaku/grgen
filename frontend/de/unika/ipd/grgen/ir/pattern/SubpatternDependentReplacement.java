/*
 * GrGen: graph rewrite generator tool -- release GrGen.NET 7.1
 * Copyright (C) 2003-2025 Universitaet Karlsruhe, Institut fuer Programmstrukturen und Datenorganisation, LS Goos; and free programmers
 * licensed under LGPL v3 (see LICENSE.txt included in the packaging of this file)
 * www.grgen.net
 */

/**
 * @author Edgar Jakumeit
 */

package de.unika.ipd.grgen.ir.pattern;

import java.util.List;

import de.unika.ipd.grgen.ir.Ident;
import de.unika.ipd.grgen.ir.Identifiable;
import de.unika.ipd.grgen.ir.expr.Expression;

public class SubpatternDependentReplacement extends Identifiable implements OrderedReplacement
{
	SubpatternUsage subpatternUsage;
	List<Expression> replConnections;

	public SubpatternDependentReplacement(String name, Ident ident,
			SubpatternUsage subpatternUsage, List<Expression> replConnections)
	{
		super(name, ident);
		this.subpatternUsage = subpatternUsage;
		this.replConnections = replConnections;
	}

	public SubpatternUsage getSubpatternUsage()
	{
		return subpatternUsage;
	}

	public List<Expression> getReplConnections()
	{
		return replConnections;
	}
}
