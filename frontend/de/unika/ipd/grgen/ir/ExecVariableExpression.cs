/*
 * GrGen: graph rewrite generator tool -- release GrGen.NET 8.1
 * Copyright (C) 2003-2026 Universitaet Karlsruhe, Institut fuer Programmstrukturen und Datenorganisation, LS Goos; and free programmers
 * licensed under LGPL v3, some components/parts use different licenses (see LICENSE.txt included in the packaging of this file)
 * www.grgen.de / www.grgen.net
 */

/// <summary>
/// @author Moritz Kroll
/// </summary>

namespace de.unika.ipd.grgen.ir
{
using Expression = de.unika.ipd.grgen.ir.expr.Expression;

/// <summary>
/// A exec variable expression node.
/// </summary>
public class ExecVariableExpression : Expression
{
	private ExecVariable var;

	public ExecVariableExpression(ExecVariable var)
		: base("exec variable", var.Type)
	{
		this.var = var;
	}

	/// <summary>
	/// Returns the exec variable of this exec variable expression. </summary>
	public virtual ExecVariable Variable
	{
		get
		{
		return var;
		}
	}

	public override bool Equals(object other)
	{
		if(!(other is ExecVariableExpression))
			return false;
		return var == ((ExecVariableExpression)other).Variable;
	}

	public override int GetHashCode()
	{
		return var.GetHashCode();
	}
}

}
