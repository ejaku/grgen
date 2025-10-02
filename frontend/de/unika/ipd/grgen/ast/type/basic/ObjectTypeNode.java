/*
 * GrGen: graph rewrite generator tool -- release GrGen.NET 8.0
 * Copyright (C) 2003-2025 Universitaet Karlsruhe, Institut fuer Programmstrukturen und Datenorganisation, LS Goos; and free programmers
 * licensed under LGPL v3 (see LICENSE.txt included in the packaging of this file)
 * www.grgen.de / www.grgen.net
 */

/**
 * Represents the basic type 'object'
 *
 * @author G. Veit Batz
 */

package de.unika.ipd.grgen.ast.type.basic;

import de.unika.ipd.grgen.ir.IR;
import de.unika.ipd.grgen.ir.type.basic.ObjectType;

public class ObjectTypeNode extends BasicTypeNode
{
	static {
		setName(ObjectTypeNode.class, "object type");
	}

	/**
	 * Singleton class representing the only constant value 'null' that
	 * the basic type 'object' has.
	 */
	// TODO: No instance is ever used! Probably useless...
	public static class Value
	{
		public static Value NULL = new Value() {
			@Override
			public String toString()
			{
				return "Const null";
			}
		};

		private Value()
		{
		}
	}

	public ObjectTypeNode()
	{
	}

	@Override
	protected IR constructIR()
	{
		return new ObjectType(getIdentNode().getIdent());
	}

	@Override
	public String toString()
	{
		return "object";
	}
}
