/*
 * GrGen: graph rewrite generator tool -- release GrGen.NET 6.1
 * Copyright (C) 2003-2021 Universitaet Karlsruhe, Institut fuer Programmstrukturen und Datenorganisation, LS Goos; and free programmers
 * licensed under LGPL v3 (see LICENSE.txt included in the packaging of this file)
 * www.grgen.net
 */

package de.unika.ipd.grgen.ast.pattern;

import java.util.Collection;
import java.util.LinkedList;

import de.unika.ipd.grgen.ast.BaseNode;
import de.unika.ipd.grgen.ast.CollectNode;
import de.unika.ipd.grgen.ir.IR;
import de.unika.ipd.grgen.ir.pattern.OrderedReplacement;
import de.unika.ipd.grgen.ir.pattern.OrderedReplacements;
import de.unika.ipd.grgen.parser.Coords;

public class OrderedReplacementsNode extends BaseNode
{
	public String name;
	public CollectNode<OrderedReplacementNode> orderedReplacements;

	public OrderedReplacementsNode(Coords coords, String name)
	{
		super(coords);
		this.name = name;
		orderedReplacements = new CollectNode<OrderedReplacementNode>();
	}

	public void addChild(OrderedReplacementNode c)
	{
		orderedReplacements.addChild(c);
	}

	@Override
	public Collection<OrderedReplacementNode> getChildren()
	{
		return orderedReplacements.getChildren();
	}

	@Override
	protected Collection<String> getChildrenNames()
	{
		LinkedList<String> res = new LinkedList<String>();
		for(int i = 0; i < getChildren().size(); ++i) {
			res.add("eval" + i);
		}
		return res;
	}

	@Override
	protected boolean resolveLocal()
	{
		return true;
	}

	@Override
	protected boolean checkLocal()
	{
		return true;
	}

	public boolean noExecStatement()
	{
		boolean res = true;
		for(OrderedReplacementNode orderedReplacement : orderedReplacements.getChildren()) {
			res &= orderedReplacement.noExecStatement(true);
		}
		return res;
	}

	@Override
	protected IR constructIR()
	{
		OrderedReplacements ors = new OrderedReplacements(name);

		for(OrderedReplacementNode orderedReplacement : orderedReplacements.getChildren()) {
			ors.orderedReplacements.add((OrderedReplacement)orderedReplacement.getIR());
		}

		return ors;
	}
}
