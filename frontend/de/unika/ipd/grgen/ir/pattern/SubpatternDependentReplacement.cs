/*
 * GrGen: graph rewrite generator tool -- release GrGen.NET 8.1
 * Copyright (C) 2003-2026 Universitaet Karlsruhe, Institut fuer Programmstrukturen und Datenorganisation, LS Goos; and free programmers
 * licensed under LGPL v3, some components/parts use different licenses (see LICENSE.txt included in the packaging of this file)
 * www.grgen.de / www.grgen.net
 */

/// <summary>
/// @author Edgar Jakumeit
/// </summary>

namespace de.unika.ipd.grgen.ir.pattern
{

using System.Collections.Generic;

using Ident = de.unika.ipd.grgen.ir.Ident;
using Identifiable = de.unika.ipd.grgen.ir.Identifiable;
using Expression = de.unika.ipd.grgen.ir.expr.Expression;

public class SubpatternDependentReplacement : Identifiable, OrderedReplacement
{
	internal SubpatternUsage subpatternUsage;
	internal IList<Expression> replConnections;

	public SubpatternDependentReplacement(string name, Ident ident,
			SubpatternUsage subpatternUsage, IList<Expression> replConnections)
		: base(name, ident)
	{
		this.subpatternUsage = subpatternUsage;
		this.replConnections = replConnections;
	}

	public virtual SubpatternUsage SubpatternUsage
	{
		get
		{
			return subpatternUsage;
		}
	}

	public virtual IList<Expression> ReplConnections
	{
		get
		{
			return replConnections;
		}
	}
}

}
