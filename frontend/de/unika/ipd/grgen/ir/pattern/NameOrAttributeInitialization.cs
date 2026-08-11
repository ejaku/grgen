/*
 * GrGen: graph rewrite generator tool -- release GrGen.NET 8.1
 * Copyright (C) 2003-2026 Universitaet Karlsruhe, Institut fuer Programmstrukturen und Datenorganisation, LS Goos; and free programmers
 * licensed under LGPL v3, some components/parts use different licenses (see LICENSE.txt included in the packaging of this file)
 * www.grgen.de / www.grgen.net
 */

namespace de.unika.ipd.grgen.ir.pattern
{
using Entity = de.unika.ipd.grgen.ir.Entity;
using IR = de.unika.ipd.grgen.ir.IR;
using NeededEntities = de.unika.ipd.grgen.ir.NeededEntities;
using Expression = de.unika.ipd.grgen.ir.expr.Expression;

/// <summary>
/// Class for initializing a single attribute of a type, or the name
/// </summary>
public class NameOrAttributeInitialization : IR
{
	public GraphEntity owner;
	public Entity attribute;
	public Expression expr;

	public NameOrAttributeInitialization()
		: base("name or attribute init")
	{
	}

	public virtual void CollectNeededEntities(NeededEntities needs)
	{
		expr.CollectNeededEntities(needs);
	}
}

}
