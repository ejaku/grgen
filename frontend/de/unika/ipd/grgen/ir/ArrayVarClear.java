/*
 * GrGen: graph rewrite generator tool -- release GrGen.NET 3.5
 * Copyright (C) 2003-2012 Universitaet Karlsruhe, Institut fuer Programmstrukturen und Datenorganisation, LS Goos
 * licensed under LGPL v3 (see LICENSE.txt included in the packaging of this file)
 * www.grgen.net
 */

/**
 * @author Edgar Jakumeit
 */

package de.unika.ipd.grgen.ir;

public class ArrayVarClear extends EvalStatement {
	Variable target;

	public ArrayVarClear(Variable target) {
		super("array var clear");
		this.target = target;
	}

	public Variable getTarget() {
		return target;
	}

	public void collectNeededEntities(NeededEntities needs)
	{
		needs.add(target);

		if(getNext()!=null) {
			getNext().collectNeededEntities(needs);
		}
	}
}
