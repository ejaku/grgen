/*
 * GrGen: graph rewrite generator tool -- release GrGen.NET 8.1
 * Copyright (C) 2003-2026 Universitaet Karlsruhe, Institut fuer Programmstrukturen und Datenorganisation, LS Goos; and free programmers
 * licensed under LGPL v3, some components/parts use different licenses (see LICENSE.txt included in the packaging of this file)
 * www.grgen.de / www.grgen.net
 */

/// <summary>
/// @author shack
/// </summary>

namespace de.unika.ipd.grgen.ast.util
{
	using BaseNode = de.unika.ipd.grgen.ast.BaseNode;
	using ErrorReporter = de.unika.ipd.grgen.util.report.ErrorReporter;

	/// <summary>
	/// Interface for something, that can check an AST node
	/// </summary>
	public interface Checker
	{
		/// <summary>
		/// Check some AST node </summary>
		/// <param name="bn"> The AST node to check </param>
		/// <returns> true if the check succeeded, false if not. </returns>
		bool Check(BaseNode bn, ErrorReporter reporter);
	}

}
