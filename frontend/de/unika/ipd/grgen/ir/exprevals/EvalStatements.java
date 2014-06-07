/*
 * GrGen: graph rewrite generator tool -- release GrGen.NET 4.4
 * Copyright (C) 2003-2014 Universitaet Karlsruhe, Institut fuer Programmstrukturen und Datenorganisation, LS Goos; and free programmers
 * licensed under LGPL v3 (see LICENSE.txt included in the packaging of this file)
 * www.grgen.net
 */

package de.unika.ipd.grgen.ir.exprevals;

import java.util.LinkedList;
import java.util.List;

import de.unika.ipd.grgen.ir.*;

public class EvalStatements extends IR
{
	public List<EvalStatement> evalStatements = new LinkedList<EvalStatement>();

	public EvalStatements(String name) {
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
