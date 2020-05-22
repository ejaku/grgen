/*
 * GrGen: graph rewrite generator tool -- release GrGen.NET 5.0
 * Copyright (C) 2003-2020 Universitaet Karlsruhe, Institut fuer Programmstrukturen und Datenorganisation, LS Goos; and free programmers
 * licensed under LGPL v3 (see LICENSE.txt included in the packaging of this file)
 * www.grgen.net
 */

package de.unika.ipd.grgen.ast.expr.numeric;

import de.unika.ipd.grgen.ast.expr.ConstNode;
import de.unika.ipd.grgen.ast.expr.string.StringConstNode;
import de.unika.ipd.grgen.ast.type.BasicTypeNode;
import de.unika.ipd.grgen.ast.type.TypeNode;
import de.unika.ipd.grgen.parser.Coords;

/**
 * An single precision floating point constant.
 */
public class FloatConstNode extends ConstNode
{
	public FloatConstNode(Coords coords, double v)
	{
		super(coords, "float", new Float(v));
	}

	@Override
	public TypeNode getType()
	{
		return BasicTypeNode.floatType;
	}

	@Override
	protected ConstNode doCastTo(TypeNode type)
	{
		Float value = (Float)getValue();

		if(type.isEqual(BasicTypeNode.byteType)) {
			return new ByteConstNode(getCoords(), (byte)(float)value);
		} else if(type.isEqual(BasicTypeNode.shortType)) {
			return new ShortConstNode(getCoords(), (short)(float)value);
		} else if(type.isEqual(BasicTypeNode.intType)) {
			return new IntConstNode(getCoords(), (int)(float)value);
		} else if(type.isEqual(BasicTypeNode.longType)) {
			return new LongConstNode(getCoords(), (long)(float)value);
		} else if(type.isEqual(BasicTypeNode.doubleType)) {
			return new DoubleConstNode(getCoords(), value);
		} else if(type.isEqual(BasicTypeNode.stringType)) {
			return new StringConstNode(getCoords(), value.toString());
		} else
			throw new UnsupportedOperationException();
	}
}
