/*
 * GrGen: graph rewrite generator tool -- release GrGen.NET 4.5
 * Copyright (C) 2003-2019 Universitaet Karlsruhe, Institut fuer Programmstrukturen und Datenorganisation, LS Goos; and free programmers
 * licensed under LGPL v3 (see LICENSE.txt included in the packaging of this file)
 * www.grgen.net
 */

/**
 * @author Edgar Jakumeit
 */

package de.unika.ipd.grgen.ir.containers;

import de.unika.ipd.grgen.ir.exprevals.*;

public class SetCopyConstructor extends Expression {
	private Expression setToCopy;
	private SetType setType;

	public SetCopyConstructor(Expression setToCopy, SetType setType) {
		super("set copy constructor", setType);
		this.setToCopy = setToCopy;
		this.setType = setType;
	}

	public void collectNeededEntities(NeededEntities needs) {
		needs.add(this);
		needs.needsGraph();
		setToCopy.collectNeededEntities(needs);
	}

	public Expression getSetToCopy() {
		return setToCopy;
	}

	public SetType getSetType() {
		return setType;
	}
}
