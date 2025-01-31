/*
 * GrGen: graph rewrite generator tool -- release GrGen.NET 7.1
 * Copyright (C) 2003-2025 Universitaet Karlsruhe, Institut fuer Programmstrukturen und Datenorganisation, LS Goos; and free programmers
 * licensed under LGPL v3 (see LICENSE.txt included in the packaging of this file)
 * www.grgen.net
 */

/**
 * @author Rubino Geiss
 */

package de.unika.ipd.grgen.ir;

import java.util.Collections;
import java.util.List;

import de.unika.ipd.grgen.ir.expr.Expression;
import de.unika.ipd.grgen.ir.pattern.OrderedReplacement;
import de.unika.ipd.grgen.ir.stmt.ImperativeStmt;

/**
 * An emit statement.
 */
public class Emit extends IR implements ImperativeStmt, OrderedReplacement
{
	private List<Expression> arguments;
	private boolean isDebug;

	public Emit(List<Expression> arguments, boolean isDebug)
	{
		super("emit");
		this.arguments = arguments;
		this.isDebug = isDebug;
	}

	public boolean isDebug()
	{
		return isDebug;
	}

	/**
	 * Returns Arguments
	 */
	public List<Expression> getArguments()
	{
		return Collections.unmodifiableList(arguments);
	}

	public void collectNeededEntities(NeededEntities needs)
	{
		for(Expression expr : arguments) {
			expr.collectNeededEntities(needs);
		}
	}
}
