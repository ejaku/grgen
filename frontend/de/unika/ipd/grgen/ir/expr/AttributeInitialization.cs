/*
 * GrGen: graph rewrite generator tool -- release GrGen.NET 8.1
 * Copyright (C) 2003-2026 Universitaet Karlsruhe, Institut fuer Programmstrukturen und Datenorganisation, LS Goos; and free programmers
 * licensed under LGPL v3, some components/parts use different licenses (see LICENSE.txt included in the packaging of this file)
 * www.grgen.de / www.grgen.net
 */

namespace de.unika.ipd.grgen.ir.expr
{
using Entity = de.unika.ipd.grgen.ir.Entity;
using IR = de.unika.ipd.grgen.ir.IR;
using NeededEntities = de.unika.ipd.grgen.ir.NeededEntities;
using BaseInternalObjectType = de.unika.ipd.grgen.ir.model.type.BaseInternalObjectType;

/// <summary>
/// Class for initializing a single attribute of a type
/// </summary>
public class AttributeInitialization : IR
{
	public InternalObjectInit init;
	public BaseInternalObjectType owner;
	public Entity attribute;
	public Expression expr;

	public AttributeInitialization()
		: base("attribute init")
	{
	}

	public virtual void CollectNeededEntities(NeededEntities needs)
	{
		expr.CollectNeededEntities(needs);
	}
}

}
