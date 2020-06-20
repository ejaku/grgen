/*
 * GrGen: graph rewrite generator tool -- release GrGen.NET 5.0
 * Copyright (C) 2003-2020 Universitaet Karlsruhe, Institut fuer Programmstrukturen und Datenorganisation, LS Goos; and free programmers
 * licensed under LGPL v3 (see LICENSE.txt included in the packaging of this file)
 * www.grgen.net
 */

/**
 * The task specifies what rewrite part to generate (for the SearchPlanBackend2 backend).
 * @author Edgar Jakumeit
 */

package de.unika.ipd.grgen.be.Csharp;

import java.util.Collection;
import java.util.List;

import de.unika.ipd.grgen.ir.*;
import de.unika.ipd.grgen.ir.expr.Expression;
import de.unika.ipd.grgen.ir.pattern.OrderedReplacement;
import de.unika.ipd.grgen.ir.pattern.OrderedReplacements;
import de.unika.ipd.grgen.ir.pattern.PatternGraphLhs;
import de.unika.ipd.grgen.ir.pattern.PatternGraphRhs;
import de.unika.ipd.grgen.ir.stmt.EvalStatements;

class ModifyGenerationTask
{
	public static final int TYPE_OF_TASK_NONE = 0;
	public static final int TYPE_OF_TASK_MODIFY = 1;
	public static final int TYPE_OF_TASK_CREATION = 2;
	public static final int TYPE_OF_TASK_DELETION = 3;

	int typeOfTask;
	PatternGraphLhs left;
	PatternGraphRhs right;
	List<Entity> parameters;
	Collection<EvalStatements> evals;
	List<Entity> replParameters;
	List<Expression> returns;
	boolean isSubpattern;
	boolean mightThereBeDeferredExecs;

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
	
	public boolean isEmitHereNeeded()
	{
		for(OrderedReplacements orderedReps : right.getOrderedReplacements()) {
			for(OrderedReplacement orderedRep : orderedReps.orderedReplacements) {
				if(orderedRep instanceof Emit) { // emithere
					return true;
				}
			}
		}
		return false;
	}
}
