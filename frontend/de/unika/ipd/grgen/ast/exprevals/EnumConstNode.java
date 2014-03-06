/*
 * GrGen: graph rewrite generator tool -- release GrGen.NET 4.3
 * Copyright (C) 2003-2014 Universitaet Karlsruhe, Institut fuer Programmstrukturen und Datenorganisation, LS Goos; and free programmers
 * licensed under LGPL v3 (see LICENSE.txt included in the packaging of this file)
 * www.grgen.net
 */

/**
 * @author Sebastian Hack
 */

package de.unika.ipd.grgen.ast.exprevals;

import de.unika.ipd.grgen.ast.*;
import de.unika.ipd.grgen.ir.exprevals.EnumExpression;
import de.unika.ipd.grgen.ir.IR;
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
	public EnumConstNode(Coords coords, IdentNode id, int value) {
		super(coords, "enum item", new Integer(value));
		this.id = id;
	}

	/** @see de.unika.ipd.grgen.ast.ConstNode#doCastTo(de.unika.ipd.grgen.ast.TypeNode) */
	@Override
	protected ConstNode doCastTo(TypeNode type) {
		int value = ((Integer) getValue()).intValue();

		if (type.isEqual(BasicTypeNode.byteType)) {
			return new ByteConstNode(getCoords(), (byte)value);
		} else if (type.isEqual(BasicTypeNode.shortType)) {
			return new ShortConstNode(getCoords(), (short)value);
		} else if (type.isEqual(BasicTypeNode.intType)) {
			return new IntConstNode(getCoords(), value);
		} else if (type.isEqual(BasicTypeNode.longType)) {
			return new LongConstNode(getCoords(), value);
		} else if (type.isEqual(BasicTypeNode.floatType)) {
			return new FloatConstNode(getCoords(), value);
		} else if (type.isEqual(BasicTypeNode.doubleType)) {
			return new DoubleConstNode(getCoords(), value);
		} else if (type.isEqual(BasicTypeNode.stringType)) {
			return new StringConstNode(getCoords(), id.toString());
		} else throw new UnsupportedOperationException();
	}

	/** @see de.unika.ipd.grgen.ast.ExprNode#getType() */
	@Override
	public TypeNode getType() {
		return BasicTypeNode.enumItemType;
	}

	@Override
	protected EnumExpression getConstant() {
		return checkIR(EnumExpression.class);
	}

	@Override
	protected IR constructIR() {
		// The EnumExpression is initialized later in EnumTypeNode.constructIR()
		// to break the circular dependency.
		return new EnumExpression((int)(Integer) value);
	}
}
