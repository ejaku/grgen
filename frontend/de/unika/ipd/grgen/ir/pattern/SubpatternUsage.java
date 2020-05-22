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

import java.util.List;

import de.unika.ipd.grgen.ir.Ident;
import de.unika.ipd.grgen.ir.Identifiable;
import de.unika.ipd.grgen.ir.Rule;
import de.unika.ipd.grgen.ir.expr.Expression;

public class SubpatternUsage extends Identifiable
{
	public Rule subpatternAction;
	public List<Expression> subpatternConnections;
	List<Expression> subpatternYields;

	public SubpatternUsage(String name, Ident ident, Rule subpatternAction,
			List<Expression> connections, List<Expression> yields)
	{
		super(name, ident);
		this.subpatternAction = subpatternAction;
		this.subpatternConnections = connections;
		this.subpatternYields = yields;
	}

	public Rule getSubpatternAction()
	{
		return subpatternAction;
	}

	public List<Expression> getSubpatternConnections()
	{
		return subpatternConnections;
	}

	public List<Expression> getSubpatternYields()
	{
		return subpatternYields;
	}
}
