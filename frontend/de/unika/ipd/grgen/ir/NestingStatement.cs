/*
 * GrGen: graph rewrite generator tool -- release GrGen.NET 8.1
 * Copyright (C) 2003-2026 Universitaet Karlsruhe, Institut fuer Programmstrukturen und Datenorganisation, LS Goos; and free programmers
 * licensed under LGPL v3, some components/parts use different licenses (see LICENSE.txt included in the packaging of this file)
 * www.grgen.de / www.grgen.net
 */

/// <summary>
/// @author Edgar Jakumeit
/// </summary>

namespace de.unika.ipd.grgen.ir
{

	using System.Collections.Generic;

	using EvalStatement = de.unika.ipd.grgen.ir.stmt.EvalStatement;

	/// <summary>
	/// Represents an IR object containing nested statements
	/// (both top-level non-statement objects as well as block nesting statements).
	/// </summary>
	public interface NestingStatement
	{
		void AddStatement(EvalStatement loopedStatement);
		ICollection<EvalStatement> Statements {get;}
	}

}
