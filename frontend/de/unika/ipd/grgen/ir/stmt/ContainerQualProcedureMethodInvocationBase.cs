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

using Entity = de.unika.ipd.grgen.ir.Entity;
using NeededEntities = de.unika.ipd.grgen.ir.NeededEntities;
using Qualification = de.unika.ipd.grgen.ir.expr.Qualification;
using GraphEntity = de.unika.ipd.grgen.ir.pattern.GraphEntity;
using Variable = de.unika.ipd.grgen.ir.pattern.Variable;

public abstract class ContainerQualProcedureMethodInvocationBase : BuiltinProcedureInvocationBase
{
	protected internal Qualification target;

	protected internal ContainerQualProcedureMethodInvocationBase(string name, Qualification target)
		: base(name)
	{
		this.target = target;
	}

	public virtual Qualification Target
	{
		get
		{
		return target;
		}
	}

	public override void CollectNeededEntities(NeededEntities needs)
	{
		Entity entity = target.Owner;
		if(!IsGlobalVariable(entity))
		{
			if(entity is GraphEntity)
				needs.Add((GraphEntity)entity);
			else
				needs.Add((Variable)entity);
		}

		// Temporarily do not collect variables for target
		HashSet<Variable> varSet = needs.variables;
		needs.variables = null;
		target.CollectNeededEntities(needs);
		needs.variables = varSet;

		if(Next != null)
			Next.CollectNeededEntities(needs);
	}
}

}
