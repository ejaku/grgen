/*
 * GrGen: graph rewrite generator tool -- release GrGen.NET 6.5
 * Copyright (C) 2003-2022 Universitaet Karlsruhe, Institut fuer Programmstrukturen und Datenorganisation, LS Goos; and free programmers
 * licensed under LGPL v3 (see LICENSE.txt included in the packaging of this file)
 * www.grgen.net
 */

/**
 * @author Sebastian Hack
 */

package de.unika.ipd.grgen.ast.expr;

import de.unika.ipd.grgen.ast.expr.string.StringConstNode;
import de.unika.ipd.grgen.ast.type.TypeNode;
import de.unika.ipd.grgen.ast.type.basic.BasicTypeNode;
import de.unika.ipd.grgen.parser.Coords;

/**
 * A boolean constant.
 */
public class BoolConstNode extends ConstNode
{
	public BoolConstNode(Coords coords, boolean value)
	{
		super(coords, "boolean", new Boolean(value));
	}

	@Override
	public TypeNode getType()
	{
		return BasicTypeNode.booleanType;
	}

	/** @see de.unika.ipd.grgen.ast.expr.ConstNode#doCastTo(de.unika.ipd.grgen.ast.type.TypeNode) */
	@Override
	protected ConstNode doCastTo(TypeNode type)
	{
		Boolean value = (Boolean)getValue();

		if(type.isEqual(BasicTypeNode.stringType)) {
			return new StringConstNode(getCoords(), value.toString());
		} else
			throw new UnsupportedOperationException();
	}
}
