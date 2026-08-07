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

using de.unika.ipd.grgen.ir;
using Expression = de.unika.ipd.grgen.ir.expr.Expression;

/// <summary>
/// Represents an exec statement embedded within a computation in the IR.
/// </summary>
public class ExecStatement : EvalStatement
{
	private Exec exec;

	public ExecStatement(Exec exec)
		: base("exec statement")
	{
		this.exec = exec;
	}

	public virtual Exec Exec
	{
		get
		{
		return exec;
		}
	}

	public override void CollectNeededEntities(NeededEntities needs)
	{
		needs.NeedsGraph();
		foreach(Expression arg in Exec.GetArguments())
			arg.CollectNeededEntities(needs);
	}

	public virtual ISet<Entity> GetNeededEntities(bool forComputation)
	{
		return exec.GetNeededEntities(forComputation);
	}

	/// <summary>
	/// Returns XGRS as an String </summary>
	public virtual string XGRSString
	{
		get
		{
		return exec.XGRSString;
		}
	}

	public virtual int LineNr
	{
		get
		{
		return exec.LineNr;
		}
	}
}

}
