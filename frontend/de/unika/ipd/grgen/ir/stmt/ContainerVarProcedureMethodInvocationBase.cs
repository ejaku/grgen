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
using NeededEntities = de.unika.ipd.grgen.ir.NeededEntities;
using Variable = de.unika.ipd.grgen.ir.pattern.Variable;

public abstract class ContainerVarProcedureMethodInvocationBase : BuiltinProcedureInvocationBase
{
	protected internal Variable target;

	protected internal ContainerVarProcedureMethodInvocationBase(string name, Variable target)
		: base(name)
	{
		this.target = target;
	}

	public virtual Variable Target
	{
		get
		{
			return target;
		}
	}

	public override void CollectNeededEntities(NeededEntities needs)
	{
		if(!IsGlobalVariable(target))
			needs.Add(target);

		if(Next != null)
			Next.CollectNeededEntities(needs);
	}
}

}
