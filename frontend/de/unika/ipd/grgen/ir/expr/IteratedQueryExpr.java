/*
 * GrGen: graph rewrite generator tool -- release GrGen.NET 5.0
 * Copyright (C) 2003-2020 Universitaet Karlsruhe, Institut fuer Programmstrukturen und Datenorganisation, LS Goos; and free programmers
 * licensed under LGPL v3 (see LICENSE.txt included in the packaging of this file)
 * www.grgen.net
 */

/**
 * @author Edgar Jakumeit
 */

package de.unika.ipd.grgen.ir.expr;

import de.unika.ipd.grgen.ir.Ident;
import de.unika.ipd.grgen.ir.NeededEntities;
import de.unika.ipd.grgen.ir.executable.Rule;
import de.unika.ipd.grgen.ir.type.Type;

public class IteratedQueryExpr extends Expression
{
	Ident iteratedName;

	public IteratedQueryExpr(Ident iteratedName, Rule iterated, Type targetType)
	{
		super("iterated query", targetType);
		this.iteratedName = iteratedName;
	}

	public Ident getIteratedName()
	{
		return iteratedName;
	}

	@Override
	public void collectNeededEntities(NeededEntities needs)
	{
	}
}
