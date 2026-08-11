/*
 * GrGen: graph rewrite generator tool -- release GrGen.NET 8.1
 * Copyright (C) 2003-2026 Universitaet Karlsruhe, Institut fuer Programmstrukturen und Datenorganisation, LS Goos; and free programmers
 * licensed under LGPL v3, some components/parts use different licenses (see LICENSE.txt included in the packaging of this file)
 * www.grgen.de / www.grgen.net
 */

/// <summary>
/// Interface giving access to the state needed for generating expressions.
/// @author Moritz Kroll, Edgar Jakumeit
/// </summary>

namespace de.unika.ipd.grgen.be.Csharp
{

using System.Collections.Generic;

using Expression = de.unika.ipd.grgen.ir.expr.Expression;
using Model = de.unika.ipd.grgen.ir.model.Model;
using SourceBuilder = de.unika.ipd.grgen.util.SourceBuilder;

public interface ExpressionGenerationState
{
	IDictionary<Expression, string> MapExprToTempVar {get;}

	bool UseVarForResult();

	bool SwitchToVarForResultAfterFirstVarUsage();

	void SwitchToVarForResult();

	Model Model {get;}

	bool IsToBeParallelizedActionExisting();

	bool EmitProfilingInstrumentation();

	SourceBuilder PerElementMethodSourceBuilder {get;}
}

}
