/*
 * GrGen: graph rewrite generator tool -- release GrGen.NET 7.2
 * Copyright (C) 2003-2025 Universitaet Karlsruhe, Institut fuer Programmstrukturen und Datenorganisation, LS Goos; and free programmers
 * licensed under LGPL v3 (see LICENSE.txt included in the packaging of this file)
 * www.grgen.net
 */

/**
 * Interface giving access to the state needed for generating expressions.
 * @author Moritz Kroll, Edgar Jakumeit
 */

package de.unika.ipd.grgen.be.Csharp;

import java.util.Map;

import de.unika.ipd.grgen.ir.expr.Expression;
import de.unika.ipd.grgen.ir.model.Model;
import de.unika.ipd.grgen.util.SourceBuilder;

public interface ExpressionGenerationState
{
	Map<Expression, String> mapExprToTempVar();

	boolean useVarForResult();

	boolean switchToVarForResultAfterFirstVarUsage();

	void switchToVarForResult();

	Model model();

	boolean isToBeParallelizedActionExisting();

	boolean emitProfilingInstrumentation();

	SourceBuilder perElementMethodSourceBuilder();
}
