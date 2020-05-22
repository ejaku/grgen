/*
 * GrGen: graph rewrite generator tool -- release GrGen.NET 5.0
 * Copyright (C) 2003-2020 Universitaet Karlsruhe, Institut fuer Programmstrukturen und Datenorganisation, LS Goos; and free programmers
 * licensed under LGPL v3 (see LICENSE.txt included in the packaging of this file)
 * www.grgen.net
 */

/**
 * @author Rubino Geiss
 */

package de.unika.ipd.grgen.ast.expr;

import de.unika.ipd.grgen.ast.type.BasicTypeNode;
import de.unika.ipd.grgen.ast.type.TypeNode;
import de.unika.ipd.grgen.ir.IR;
import de.unika.ipd.grgen.ir.expr.Constant;
import de.unika.ipd.grgen.parser.Coords;

/**
 * The null constant.
 */
public class NullConstNode extends ConstNode
{
	private TypeNode type;

	public NullConstNode(Coords coords)
	{
		super(coords, "null", Value.NULL);
		type = BasicTypeNode.nullType;
	}

	/**
	 * Singleton class representing the only constant value 'null' that
	 * the basic type 'object' has.
	 */
	public static class Value
	{
		public static Value NULL = new Value() {
			public String toString()
			{
				return "Const null";
			}
		};

		private Value()
		{
		}

		public boolean equals(Object val)
		{
			return(this == val);
		}
	}

	@Override
	public TypeNode getType()
	{
		return type;
	}

	@Override
	public String toString()
	{
		return "Const (" + type + ") null";
	}

	@Override
	protected IR constructIR()
	{
		return new Constant(getType().getType(), null);
	}

	/** @see de.unika.ipd.grgen.ast.expr.ConstNode#doCastTo(de.unika.ipd.grgen.ast.type.TypeNode) */
	@Override
	protected ConstNode doCastTo(TypeNode type)
	{
		NullConstNode castedNull = new NullConstNode(getCoords());
		castedNull.type = type;
		return castedNull;
	}
}
