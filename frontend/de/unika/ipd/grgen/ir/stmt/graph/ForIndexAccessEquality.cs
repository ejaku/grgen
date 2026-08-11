/*
 * GrGen: graph rewrite generator tool -- release GrGen.NET 8.1
 * Copyright (C) 2003-2026 Universitaet Karlsruhe, Institut fuer Programmstrukturen und Datenorganisation, LS Goos; and free programmers
 * licensed under LGPL v3, some components/parts use different licenses (see LICENSE.txt included in the packaging of this file)
 * www.grgen.de / www.grgen.net
 */

/// <summary>
/// @author Edgar Jakumeit
/// </summary>

namespace de.unika.ipd.grgen.ir.stmt.graph
{

using System.Collections.Generic;

using de.unika.ipd.grgen.ir;
using IndexAccessEquality = de.unika.ipd.grgen.ir.pattern.IndexAccessEquality;
using Variable = de.unika.ipd.grgen.ir.pattern.Variable;
using EvalStatement = de.unika.ipd.grgen.ir.stmt.EvalStatement;

/// <summary>
/// Represents a for index access for a certain value in the IR.
/// deprecated, TODO: purge
/// </summary>
public class ForIndexAccessEquality : EvalStatement
{
	private Variable iterationVar;
	private IndexAccessEquality iae;
	private List<EvalStatement> statements = new List<EvalStatement>();

	public ForIndexAccessEquality(Variable iterationVar,
			IndexAccessEquality iae)
		: base("for index access equality")
	{
		this.iterationVar = iterationVar;
		this.iae = iae;
	}

	public virtual void AddLoopedStatement(EvalStatement loopedStatement)
	{
		statements.Add(loopedStatement);
	}

	public virtual Variable IterationVar
	{
		get
		{
			return iterationVar;
		}
	}

	public virtual IndexAccessEquality IndexAcccessEquality
	{
		get
		{
			return iae;
		}
	}

	public virtual ICollection<EvalStatement> LoopedStatements
	{
		get
		{
			return statements.AsReadOnly();
		}
	}

	public override void CollectNeededEntities(NeededEntities needs)
	{
		iae.CollectNeededEntities(needs);
		foreach(EvalStatement loopedStatement in statements)
			loopedStatement.CollectNeededEntities(needs);
		if(needs.variables != null)
			needs.variables.Remove(iterationVar);
	}
}

}
