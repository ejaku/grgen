/*
 * GrGen: graph rewrite generator tool -- release GrGen.NET 6.7
 * Copyright (C) 2003-2023 Universitaet Karlsruhe, Institut fuer Programmstrukturen und Datenorganisation, LS Goos; and free programmers
 * licensed under LGPL v3 (see LICENSE.txt included in the packaging of this file)
 * www.grgen.net
 */

/**
 * @author Edgar Jakumeit
 */

package de.unika.ipd.grgen.ir.expr.deque;

import de.unika.ipd.grgen.ir.NeededEntities;
import de.unika.ipd.grgen.ir.expr.Expression;
import de.unika.ipd.grgen.ir.type.container.DequeType;

public class DequeCopyConstructor extends Expression
{
	private Expression dequeToCopy;
	private DequeType dequeType;

	public DequeCopyConstructor(Expression dequeToCopy, DequeType dequeType)
	{
		super("deque copy constructor", dequeType);
		this.dequeToCopy = dequeToCopy;
		this.dequeType = dequeType;
	}

	@Override
	public void collectNeededEntities(NeededEntities needs)
	{
		needs.add(this);
		needs.needsGraph();
		dequeToCopy.collectNeededEntities(needs);
	}

	public Expression getDequeToCopy()
	{
		return dequeToCopy;
	}

	public DequeType getDequeType()
	{
		return dequeType;
	}
}
