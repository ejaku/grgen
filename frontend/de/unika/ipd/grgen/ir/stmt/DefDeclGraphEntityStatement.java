/*
 * GrGen: graph rewrite generator tool -- release GrGen.NET 7.1
 * Copyright (C) 2003-2025 Universitaet Karlsruhe, Institut fuer Programmstrukturen und Datenorganisation, LS Goos; and free programmers
 * licensed under LGPL v3 (see LICENSE.txt included in the packaging of this file)
 * www.grgen.net
 */

/**
 * @author Edgar Jakumeit
 */

package de.unika.ipd.grgen.ir.stmt;

import de.unika.ipd.grgen.ir.*;
import de.unika.ipd.grgen.ir.pattern.GraphEntity;

/**
 * Represents a declaration of a local variable of graph element type in the IR.
 */
public class DefDeclGraphEntityStatement extends EvalStatement
{
	private GraphEntity target;

	public DefDeclGraphEntityStatement(GraphEntity target)
	{
		super("def decl graph entity");
		this.target = target;
	}

	public GraphEntity getTarget()
	{
		return target;
	}

	@Override
	public String toString()
	{
		return target.getIdent().toString();
	}

	@Override
	public void collectNeededEntities(NeededEntities needs)
	{
		//needs.add(target); needed?
	}
}
