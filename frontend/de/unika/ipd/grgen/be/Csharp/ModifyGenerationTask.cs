/*
 * GrGen: graph rewrite generator tool -- release GrGen.NET 8.1
 * Copyright (C) 2003-2026 Universitaet Karlsruhe, Institut fuer Programmstrukturen und Datenorganisation, LS Goos; and free programmers
 * licensed under LGPL v3, some components/parts use different licenses (see LICENSE.txt included in the packaging of this file)
 * www.grgen.de / www.grgen.net
 */

/// <summary>
/// The task specifies what rewrite part to generate (for the SearchPlanBackend2 backend).
/// @author Edgar Jakumeit
/// </summary>

namespace de.unika.ipd.grgen.be.Csharp
{

using System.Collections.Generic;

using de.unika.ipd.grgen.ir;
using Expression = de.unika.ipd.grgen.ir.expr.Expression;
using OrderedReplacement = de.unika.ipd.grgen.ir.pattern.OrderedReplacement;
using OrderedReplacements = de.unika.ipd.grgen.ir.pattern.OrderedReplacements;
using PatternGraphLhs = de.unika.ipd.grgen.ir.pattern.PatternGraphLhs;
using PatternGraphRhs = de.unika.ipd.grgen.ir.pattern.PatternGraphRhs;
using EvalStatements = de.unika.ipd.grgen.ir.stmt.EvalStatements;

public class ModifyGenerationTask
{
	public const int TYPE_OF_TASK_NONE = 0;
	public const int TYPE_OF_TASK_MODIFY = 1;
	public const int TYPE_OF_TASK_CREATION = 2;
	public const int TYPE_OF_TASK_DELETION = 3;

	internal int typeOfTask;
	internal PatternGraphLhs left;
	internal PatternGraphRhs right;
	internal IList<Entity> parameters;
	internal ICollection<EvalStatements> evals;
	internal IList<Entity> replParameters;
	internal IList<Expression> returns;
	internal bool isSubpattern;
	internal bool mightThereBeDeferredExecs;

	public ModifyGenerationTask()
	{
		typeOfTask = TYPE_OF_TASK_NONE;
		left = null;
		right = null;
		parameters = null;
		evals = null;
		replParameters = null;
		returns = null;
		isSubpattern = false;
		mightThereBeDeferredExecs = false;
	}

	public virtual bool IsEmitHereNeeded()
	{
		foreach(OrderedReplacements orderedReps in right.OrderedReplacements)
		{
			foreach(OrderedReplacement orderedRep in orderedReps.orderedReplacements)
			{
				if(orderedRep is Emit)
					return true;
			}
		}
		return false;
	}
}

}
