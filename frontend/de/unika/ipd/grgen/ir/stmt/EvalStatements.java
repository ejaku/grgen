/*
 * GrGen: graph rewrite generator tool -- release GrGen.NET 8.1
 * Copyright (C) 2003-2026 Universitaet Karlsruhe, Institut fuer Programmstrukturen und Datenorganisation, LS Goos; and free programmers
 * licensed under LGPL v3, some components/parts use different licenses (see LICENSE.txt included in the packaging of this file)
 * www.grgen.de / www.grgen.net
 */

package de.unika.ipd.grgen.ir.stmt;

import java.util.ArrayList;
import java.util.List;

import de.unika.ipd.grgen.ir.*;

public class EvalStatements extends IR
{
	public List<EvalStatement> evalStatements = new ArrayList<EvalStatement>();

	public EvalStatements(String name)
	{
		super(name);
	}

	/**
	 * Method collectNeededEntities extracts the nodes, edges, and variables occurring in this Expression.
	 * We don't collect global variables (::-prefixed), as no entities and no processing are needed for them at all, they are only accessed.
	 * @param needs A NeededEntities instance aggregating the needed elements.
	 */
	public void collectNeededEntities(NeededEntities needs)
	{
		for(EvalStatement evalStatement : evalStatements) {
			evalStatement.collectNeededEntities(needs);
		}
	}
}
