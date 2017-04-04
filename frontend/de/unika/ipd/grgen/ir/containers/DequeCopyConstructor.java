/*
 * GrGen: graph rewrite generator tool -- release GrGen.NET 4.5
 * Copyright (C) 2003-2017 Universitaet Karlsruhe, Institut fuer Programmstrukturen und Datenorganisation, LS Goos; and free programmers
 * licensed under LGPL v3 (see LICENSE.txt included in the packaging of this file)
 * www.grgen.net
 */

/**
 * @author Edgar Jakumeit
 */

package de.unika.ipd.grgen.ir.containers;

import de.unika.ipd.grgen.ir.exprevals.*;

public class DequeCopyConstructor extends Expression {
	private Expression dequeToCopy;
	private DequeType dequeType;

	public DequeCopyConstructor(Expression dequeToCopy, DequeType dequeType) {
		super("deque copy constructor", dequeType);
		this.dequeToCopy = dequeToCopy;
		this.dequeType = dequeType;
	}

	public void collectNeededEntities(NeededEntities needs) {
		needs.add(this);
		needs.needsGraph();
		dequeToCopy.collectNeededEntities(needs);
	}

	public Expression getDequeToCopy() {
		return dequeToCopy;
	}

	public DequeType getDequeType() {
		return dequeType;
	}
}
