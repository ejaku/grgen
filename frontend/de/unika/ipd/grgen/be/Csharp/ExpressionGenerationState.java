/*
 * GrGen: graph rewrite generator tool -- release GrGen.NET 4.5
 * Copyright (C) 2003-2020 Universitaet Karlsruhe, Institut fuer Programmstrukturen und Datenorganisation, LS Goos; and free programmers
 * licensed under LGPL v3 (see LICENSE.txt included in the packaging of this file)
 * www.grgen.net
 */

/**
 * Interface giving access to the state needed for generating expressions.
 * @author Moritz Kroll, Edgar Jakumeit
 */

package de.unika.ipd.grgen.be.Csharp;

import java.util.Map;

import de.unika.ipd.grgen.ir.*;
import de.unika.ipd.grgen.ir.exprevals.*;

public interface ExpressionGenerationState {
	Map<Expression, String> mapExprToTempVar();
	boolean useVarForResult();
	Model model();
	boolean isToBeParallelizedActionExisting();
	boolean emitProfilingInstrumentation();
}
