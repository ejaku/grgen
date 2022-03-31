/*
 * GrGen: graph rewrite generator tool -- release GrGen.NET 6.6
 * Copyright (C) 2003-2022 Universitaet Karlsruhe, Institut fuer Programmstrukturen und Datenorganisation, LS Goos; and free programmers
 * licensed under LGPL v3 (see LICENSE.txt included in the packaging of this file)
 * www.grgen.net
 */

package de.unika.ipd.grgen.ast.expr.numeric;

import de.unika.ipd.grgen.ast.expr.ConstNode;
import de.unika.ipd.grgen.ast.expr.string.StringConstNode;
import de.unika.ipd.grgen.ast.type.TypeNode;
import de.unika.ipd.grgen.ast.type.basic.BasicTypeNode;
import de.unika.ipd.grgen.parser.Coords;

/**
 * An double precision floating point constant.
 */
public class DoubleConstNode extends ConstNode
{
	public DoubleConstNode(Coords coords, double v)
	{
		super(coords, "double", new Double(v));
	}

	@Override
	public TypeNode getType()
	{
		return BasicTypeNode.doubleType;
	}

	@Override
	protected ConstNode doCastTo(TypeNode type)
	{
		Double value = (Double)getValue();
		double unboxed = value.doubleValue();

		if(type.isEqual(BasicTypeNode.byteType)) {
			return new ByteConstNode(getCoords(), (byte)unboxed);
		} else if(type.isEqual(BasicTypeNode.shortType)) {
			return new ShortConstNode(getCoords(), (short)unboxed);
		} else if(type.isEqual(BasicTypeNode.intType)) {
			return new IntConstNode(getCoords(), (int)unboxed);
		} else if(type.isEqual(BasicTypeNode.longType)) {
			return new LongConstNode(getCoords(), (long)unboxed);
		} else if(type.isEqual(BasicTypeNode.floatType)) {
			return new FloatConstNode(getCoords(), (float)unboxed);
		} else if(type.isEqual(BasicTypeNode.stringType)) {
			return new StringConstNode(getCoords(), value.toString());
		} else
			throw new UnsupportedOperationException();
	}
}
