/*
 * GrGen: graph rewrite generator tool -- release GrGen.NET 8.1
 * Copyright (C) 2003-2026 Universitaet Karlsruhe, Institut fuer Programmstrukturen und Datenorganisation, LS Goos; and free programmers
 * licensed under LGPL v3, some components/parts use different licenses (see LICENSE.txt included in the packaging of this file)
 * www.grgen.de / www.grgen.net
 */

/// <summary>
/// @author Moritz Kroll, Edgar Jakumeit
/// </summary>

namespace de.unika.ipd.grgen.ir.expr.map
{
using Expression = de.unika.ipd.grgen.ir.expr.Expression;
using IntType = de.unika.ipd.grgen.ir.type.basic.IntType;

public class MapSizeExpr : MapFunctionMethodInvocationBaseExpr
{
	public MapSizeExpr(Expression targetExpr)
		: base("map size expression", IntType.Type, targetExpr)
	{
	}
}

}
