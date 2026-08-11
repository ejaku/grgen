/*
 * GrGen: graph rewrite generator tool -- release GrGen.NET 8.1
 * Copyright (C) 2003-2026 Universitaet Karlsruhe, Institut fuer Programmstrukturen und Datenorganisation, LS Goos; and free programmers
 * licensed under LGPL v3, some components/parts use different licenses (see LICENSE.txt included in the packaging of this file)
 * www.grgen.de / www.grgen.net
 */

/// <summary>
/// Represents the basic type 'object'
/// 
/// @author G. Veit Batz
/// </summary>

namespace de.unika.ipd.grgen.ast.type.basic
{
using IR = de.unika.ipd.grgen.ir.IR;
using ObjectType = de.unika.ipd.grgen.ir.type.basic.ObjectType;

public class ObjectTypeNode : BasicTypeNode
{
	static ObjectTypeNode()
	{
		SetClassName(typeof(ObjectTypeNode), "object type");
	}

	/// <summary>
	/// Singleton class representing the only constant value 'null' that
	/// the basic type 'object' has.
	/// </summary>
	// TODO: No instance is ever used! Probably useless...
	public class Value
	{
		public static Value NULL = new ValueAnonymousInnerClass();

		private class ValueAnonymousInnerClass : Value
		{
			private readonly Value outerInstance;

			public override string ToString()
			{
				return "Const null";
			}
		}

		internal Value()
		{
		}
	}

	public ObjectTypeNode()
	{
	}

	protected internal override IR ConstructIR()
	{
		return new ObjectType(Ident.IRIdent);
	}

	public override string ToString()
	{
		return "object";
	}
}

}
