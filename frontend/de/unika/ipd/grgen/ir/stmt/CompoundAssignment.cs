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
using Qualification = de.unika.ipd.grgen.ir.expr.Qualification;
using GraphEntity = de.unika.ipd.grgen.ir.pattern.GraphEntity;
using Variable = de.unika.ipd.grgen.ir.pattern.Variable;

/// <summary>
/// Represents a compound assignment statement in the IR.
/// </summary>
public class CompoundAssignment : EvalStatement
{
	public enum CompoundAssignmentType
	{
		NONE,
		UNION,
		INTERSECTION,
		WITHOUT,
		CONCATENATE,
		ASSIGN
	}

	/// <summary>
	/// The lhs of the assignment. </summary>
	private Qualification target;

	/// <summary>
	/// The operation of the compound assignment </summary>
	private CompoundAssignmentType operation;

	/// <summary>
	/// The rhs of the assignment. </summary>
	private Expression expr;

	public CompoundAssignment(Qualification target, CompoundAssignmentType compoundAssignmentType, Expression expr)
		: base("compound assignment")
	{
		this.target = target;
		this.operation = compoundAssignmentType;
		this.expr = expr;
	}

	public virtual Qualification Target
	{
		get
		{
			return target;
		}
	}

	public virtual Expression Expression
	{
		get
		{
			return expr;
		}
	}

	public virtual CompoundAssignmentType Operation
	{
		get
		{
			return operation;
		}
	}

	public override string ToString()
	{
		string res = Target.ToString();
		if(operation == CompoundAssignmentType.UNION)
			res += " |= ";
		else if(operation == CompoundAssignmentType.INTERSECTION)
			res += " &= ";
		else if(operation == CompoundAssignmentType.WITHOUT)
			res += " \\= ";
		else if(operation == CompoundAssignmentType.CONCATENATE)
			res += " += ";
		else
			res += " = ";
		res += Expression.ToString();
		return res;
	}

	public override void CollectNeededEntities(NeededEntities needs)
	{
		Entity entity = target.Owner;
		if(!IsGlobalVariable(entity))
			needs.Add((GraphEntity)entity);

		// Temporarily do not collect variables for target
		HashSet<Variable> varSet = needs.variables;
		needs.variables = null;
		target.CollectNeededEntities(needs);
		needs.variables = varSet;

		Expression.CollectNeededEntities(needs);
	}
}

}
