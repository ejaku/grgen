/*
 * GrGen: graph rewrite generator tool -- release GrGen.NET 6.7
 * Copyright (C) 2003-2023 Universitaet Karlsruhe, Institut fuer Programmstrukturen und Datenorganisation, LS Goos; and free programmers
 * licensed under LGPL v3 (see LICENSE.txt included in the packaging of this file)
 * www.grgen.net
 */

/**
 * EnumExpression.java
 *
 * @author Sebastian Hack
 */

package de.unika.ipd.grgen.ir.expr;

import de.unika.ipd.grgen.ir.model.EnumItem;
import de.unika.ipd.grgen.ir.model.type.EnumType;

public class EnumExpression extends Constant
{
	private EnumItem item;

	// Constructor for later initialization when EnumType and EnumItem have been constructed.
	// See EnumTypeNode.constructIR().
	public EnumExpression(int value)
	{
		super(null, Integer.valueOf(value));
		setName("enum expression");
	}

	public EnumExpression(EnumType type, EnumItem item)
	{
		super(type, item.getValue().getValue());
		this.item = item;
		setName("enum expression");
	}

	public void lateInit(EnumType type, EnumItem item)
	{
		this.type = type;
		this.item = item;
	}

	public EnumItem getEnumItem()
	{
		return item;
	}

	/** @see de.unika.ipd.grgen.util.GraphDumpable#getNodeLabel() */
	@Override
	public String getNodeLabel()
	{
		return item + " " + getValue();
	}
}
