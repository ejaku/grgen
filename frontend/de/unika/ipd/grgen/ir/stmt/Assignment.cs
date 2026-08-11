/*
 * GrGen: graph rewrite generator tool -- release GrGen.NET 8.1
 * Copyright (C) 2003-2026 Universitaet Karlsruhe, Institut fuer Programmstrukturen und Datenorganisation, LS Goos; and free programmers
 * licensed under LGPL v3, some components/parts use different licenses (see LICENSE.txt included in the packaging of this file)
 * www.grgen.de / www.grgen.net
 */

/// <summary>
/// @author Rubino Geiss
/// </summary>
namespace de.unika.ipd.grgen.ir.stmt
{

using System.Collections.Generic;

using de.unika.ipd.grgen.ir;
using Expression = de.unika.ipd.grgen.ir.expr.Expression;
using Qualification = de.unika.ipd.grgen.ir.expr.Qualification;
using GraphEntity = de.unika.ipd.grgen.ir.pattern.GraphEntity;
using Variable = de.unika.ipd.grgen.ir.pattern.Variable;
using DefinedMatchType = de.unika.ipd.grgen.ir.type.DefinedMatchType;
using MatchType = de.unika.ipd.grgen.ir.type.MatchType;

/// <summary>
/// Represents an assignment statement in the IR.
/// </summary>
public class Assignment : AssignmentBase
{
	/// <summary>
	/// The lhs of the assignment. </summary>
	private Qualification target;

	public Assignment(Qualification target, Expression expr)
		: base("assignment")
	{
		this.target = target;
		this.expr = expr;
	}

	protected internal Assignment(string name, Qualification target, Expression expr)
		: base(name)
	{
		this.target = target;
		this.expr = expr;
	}

	public virtual Qualification Target
	{
		get
		{
			return target;
		}
	}

	public override string ToString()
	{
		return Target + " = " + Expression;
	}

	public override void CollectNeededEntities(NeededEntities needs)
	{
		Entity entity = target.Owner;
		if(!IsGlobalVariable(entity)
				&& !(entity.Type is MatchType)
				&& !(entity.Type is DefinedMatchType))
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

		Expression.CollectNeededEntities(needs);
	}
}

}
