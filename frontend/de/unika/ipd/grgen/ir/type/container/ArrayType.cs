/*
 * GrGen: graph rewrite generator tool -- release GrGen.NET 8.1
 * Copyright (C) 2003-2026 Universitaet Karlsruhe, Institut fuer Programmstrukturen und Datenorganisation, LS Goos; and free programmers
 * licensed under LGPL v3, some components/parts use different licenses (see LICENSE.txt included in the packaging of this file)
 * www.grgen.de / www.grgen.net
 */

/// <summary>
/// @author Edgar Jakumeit
/// </summary>

namespace de.unika.ipd.grgen.ir.type.container
{
using BaseInternalObjectType = de.unika.ipd.grgen.ir.model.type.BaseInternalObjectType;
using Type = de.unika.ipd.grgen.ir.type.Type;

public class ArrayType : ContainerType
{
	public Type valueType;

	public ArrayType(Type valueType)
		: base("array type")
	{
		this.valueType = valueType;
	}

	public virtual Type ValueType
	{
		get
		{
		return valueType;
		}
	}

	public override string ToString()
	{
		return "array<" + valueType + ">";
	}

	/// <seealso cref="de.unika.ipd.grgen.ir.type.Type.classify() "/>
	public override TypeClass Classify()
	{
		return TypeClass.IS_ARRAY;
	}

	public override Type ElementType
	{
		get
		{
		return valueType;
		}
	}

	public override bool ContainsBaseInternalObjectType()
	{
		return valueType is BaseInternalObjectType;
	}
}

}
