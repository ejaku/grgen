/*
 * GrGen: graph rewrite generator tool -- release GrGen.NET 5.0
 * Copyright (C) 2003-2020 Universitaet Karlsruhe, Institut fuer Programmstrukturen und Datenorganisation, LS Goos; and free programmers
 * licensed under LGPL v3 (see LICENSE.txt included in the packaging of this file)
 * www.grgen.net
 */

package de.unika.ipd.grgen.ir.pattern;

import java.util.LinkedList;
import java.util.List;

import de.unika.ipd.grgen.ir.IR;

public class OrderedReplacements extends IR
{
	public List<OrderedReplacement> orderedReplacements = new LinkedList<OrderedReplacement>();

	public OrderedReplacements(String name)
	{
		super(name);
	}
}
