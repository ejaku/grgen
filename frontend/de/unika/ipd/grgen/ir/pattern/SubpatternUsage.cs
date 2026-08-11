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
using Rule = de.unika.ipd.grgen.ir.executable.Rule;
using Expression = de.unika.ipd.grgen.ir.expr.Expression;

public class SubpatternUsage : Identifiable
{
	public Rule subpatternAction;
	public IList<Expression> subpatternConnections;
	internal IList<Expression> subpatternYields;

	public SubpatternUsage(string name, Ident ident, Rule subpatternAction,
			IList<Expression> connections, IList<Expression> yields)
		: base(name, ident)
	{
		this.subpatternAction = subpatternAction;
		this.subpatternConnections = connections;
		this.subpatternYields = yields;
	}

	public virtual Rule SubpatternAction
	{
		get
		{
			return subpatternAction;
		}
	}

	public virtual IList<Expression> SubpatternConnections
	{
		get
		{
			return subpatternConnections;
		}
	}

	public virtual IList<Expression> SubpatternYields
	{
		get
		{
			return subpatternYields;
		}
	}
}

}
