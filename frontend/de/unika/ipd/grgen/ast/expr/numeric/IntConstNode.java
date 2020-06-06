/*
 * GrGen: graph rewrite generator tool -- release GrGen.NET 5.0
 * Copyright (C) 2003-2020 Universitaet Karlsruhe, Institut fuer Programmstrukturen und Datenorganisation, LS Goos; and free programmers
 * licensed under LGPL v3 (see LICENSE.txt included in the packaging of this file)
 * www.grgen.net
 */

/**
 * @author Sebastian Hack
 */

package de.unika.ipd.grgen.ast.expr.numeric;

import de.unika.ipd.grgen.ast.expr.ConstNode;
import de.unika.ipd.grgen.ast.expr.string.StringConstNode;
import de.unika.ipd.grgen.ast.type.TypeNode;
import de.unika.ipd.grgen.ast.type.basic.BasicTypeNode;
import de.unika.ipd.grgen.parser.Coords;

/**
 * An integer constant.
 */
public class IntConstNode extends ConstNode
{
	public IntConstNode(Coords coords, int v)
	{
		super(coords, "integer", new Integer(v));
	}

	@Override
	public TypeNode getType()
	{
		return BasicTypeNode.intType;
	}

	@Override
	protected ConstNode doCastTo(TypeNode type)
	{
		Integer value = (Integer)getValue();
		int unboxed = value.intValue();

		if(type.isEqual(BasicTypeNode.byteType)) {
			return new ByteConstNode(getCoords(), (byte)unboxed);
		} else if(type.isEqual(BasicTypeNode.shortType)) {
			return new ShortConstNode(getCoords(), (short)unboxed);
		} else if(type.isEqual(BasicTypeNode.longType)) {
			return new LongConstNode(getCoords(), unboxed);
		} else if(type.isEqual(BasicTypeNode.floatType)) {
			return new FloatConstNode(getCoords(), unboxed);
		} else if(type.isEqual(BasicTypeNode.doubleType)) {
			return new DoubleConstNode(getCoords(), unboxed);
		} else if(type.isEqual(BasicTypeNode.stringType)) {
			return new StringConstNode(getCoords(), value.toString());
		} else
			throw new UnsupportedOperationException();
	}
}
