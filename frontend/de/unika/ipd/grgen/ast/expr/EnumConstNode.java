/*
 * GrGen: graph rewrite generator tool -- release GrGen.NET 5.0
 * Copyright (C) 2003-2020 Universitaet Karlsruhe, Institut fuer Programmstrukturen und Datenorganisation, LS Goos; and free programmers
 * licensed under LGPL v3 (see LICENSE.txt included in the packaging of this file)
 * www.grgen.net
 */

/**
 * @author Sebastian Hack
 */

package de.unika.ipd.grgen.ast.expr;

import de.unika.ipd.grgen.ast.*;
import de.unika.ipd.grgen.ast.expr.numeric.ByteConstNode;
import de.unika.ipd.grgen.ast.expr.numeric.DoubleConstNode;
import de.unika.ipd.grgen.ast.expr.numeric.FloatConstNode;
import de.unika.ipd.grgen.ast.expr.numeric.IntConstNode;
import de.unika.ipd.grgen.ast.expr.numeric.LongConstNode;
import de.unika.ipd.grgen.ast.expr.numeric.ShortConstNode;
import de.unika.ipd.grgen.ast.expr.string.StringConstNode;
import de.unika.ipd.grgen.ast.type.TypeNode;
import de.unika.ipd.grgen.ast.type.basic.BasicTypeNode;
import de.unika.ipd.grgen.ir.IR;
import de.unika.ipd.grgen.ir.expr.EnumExpression;
import de.unika.ipd.grgen.parser.Coords;

/**
 * An enum item value.
 */
public class EnumConstNode extends ConstNode
{
	/** The name of the enum item. */
	private IdentNode id;

	/**
	 * @param coords The source code coordinates.
	 * @param id The name of the enum item.
	 * @param value The value of the enum item.
	 */
	public EnumConstNode(Coords coords, IdentNode id, int value)
	{
		super(coords, "enum item", new Integer(value));
		this.id = id;
	}

	/** @see de.unika.ipd.grgen.ast.expr.ConstNode#doCastTo(de.unika.ipd.grgen.ast.type.TypeNode) */
	@Override
	protected ConstNode doCastTo(TypeNode type)
	{
		Integer value = (Integer)getValue();
		int unboxed = (int)value;

		if(type.isEqual(BasicTypeNode.byteType)) {
			return new ByteConstNode(getCoords(), (byte)unboxed);
		} else if(type.isEqual(BasicTypeNode.shortType)) {
			return new ShortConstNode(getCoords(), (short)unboxed);
		} else if(type.isEqual(BasicTypeNode.intType)) {
			return new IntConstNode(getCoords(), unboxed);
		} else if(type.isEqual(BasicTypeNode.longType)) {
			return new LongConstNode(getCoords(), unboxed);
		} else if(type.isEqual(BasicTypeNode.floatType)) {
			return new FloatConstNode(getCoords(), unboxed);
		} else if(type.isEqual(BasicTypeNode.doubleType)) {
			return new DoubleConstNode(getCoords(), unboxed);
		} else if(type.isEqual(BasicTypeNode.stringType)) {
			return new StringConstNode(getCoords(), id.toString());
		} else
			throw new UnsupportedOperationException();
	}

	/** @see de.unika.ipd.grgen.ast.expr.ExprNode#getType() */
	@Override
	public TypeNode getType()
	{
		return BasicTypeNode.enumItemType;
	}

	@Override
	public EnumExpression getConstant()
	{
		return checkIR(EnumExpression.class);
	}

	@Override
	protected IR constructIR()
	{
		// The EnumExpression is initialized later in EnumTypeNode.constructIR()
		// to break the circular dependency.
		return new EnumExpression((int)(Integer)value);
	}
}
