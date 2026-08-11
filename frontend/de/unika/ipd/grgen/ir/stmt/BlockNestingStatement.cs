/*
 * GrGen: graph rewrite generator tool -- release GrGen.NET 8.1
 * Copyright (C) 2003-2026 Universitaet Karlsruhe, Institut fuer Programmstrukturen und Datenorganisation, LS Goos; and free programmers
 * licensed under LGPL v3, some components/parts use different licenses (see LICENSE.txt included in the packaging of this file)
 * www.grgen.de / www.grgen.net
 */

/// <summary>
/// @author Edgar Jakumeit
/// </summary>

namespace de.unika.ipd.grgen.ir.stmt
{

using System.Collections.Generic;

using NestingStatement = de.unika.ipd.grgen.ir.NestingStatement;

/// <summary>
/// Represents a block nesting statement in the IR (non top-level statement containing nested statements).
/// </summary>
public abstract class BlockNestingStatement : EvalStatement, NestingStatement
{
	protected internal List<EvalStatement> statements = new List<EvalStatement>();

	protected internal BlockNestingStatement(string name)
		: base(name)
	{
	}

	public virtual void AddStatement(EvalStatement loopedStatement)
	{
		statements.Add(loopedStatement);
	}

	public virtual ICollection<EvalStatement> Statements
	{
		get
		{
			return statements.AsReadOnly();
		}
	}
}

}
