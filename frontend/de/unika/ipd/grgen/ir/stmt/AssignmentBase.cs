/*
 * GrGen: graph rewrite generator tool -- release GrGen.NET 8.1
 * Copyright (C) 2003-2026 Universitaet Karlsruhe, Institut fuer Programmstrukturen und Datenorganisation, LS Goos; and free programmers
 * licensed under LGPL v3, some components/parts use different licenses (see LICENSE.txt included in the packaging of this file)
 * www.grgen.de / www.grgen.net
 */

namespace de.unika.ipd.grgen.ir.stmt
{
using Expression = de.unika.ipd.grgen.ir.expr.Expression;

/// <summary>
/// Gives access to the expression of an assignment statement in the IR.
/// </summary>
public abstract class AssignmentBase : EvalStatement
{
	/// <summary>
	/// The rhs of the assignment. </summary>
	protected internal Expression expr;

	public AssignmentBase(string name)
		: base(name)
	{
	}

	public virtual Expression Expression
	{
		get
		{
			return expr;
		}
		set
		{
			this.expr = value;
		}
	}

}

}
