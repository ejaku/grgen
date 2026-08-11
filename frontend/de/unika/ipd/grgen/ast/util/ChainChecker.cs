/*
 * GrGen: graph rewrite generator tool -- release GrGen.NET 8.1
 * Copyright (C) 2003-2026 Universitaet Karlsruhe, Institut fuer Programmstrukturen und Datenorganisation, LS Goos; and free programmers
 * licensed under LGPL v3, some components/parts use different licenses (see LICENSE.txt included in the packaging of this file)
 * www.grgen.de / www.grgen.net
 */

/// <summary>
/// @author Sebastian Hack
/// </summary>
namespace de.unika.ipd.grgen.ast.util
{
	using BaseNode = de.unika.ipd.grgen.ast.BaseNode;
	using ErrorReporter = de.unika.ipd.grgen.util.report.ErrorReporter;

	/// <summary>
	/// Checker containing list of checkers to apply one after the other to the node to check
	/// </summary>
	public class ChainChecker : Checker
	{
		/// <summary>
		/// The chain, i.e. list with the checkers to apply </summary>
		private Checker[] checkers;

		/// <summary>
		/// Create checker with the list of checkers to apply </summary>
		public ChainChecker(Checker[] checkers)
			: base()
		{
			this.checkers = checkers;
		}

		/// <summary>
		/// Check the node with the checkers from the list, one after the other </summary>
		/// <seealso cref="de.unika.ipd.grgen.ast.util.Checker.check(de.unika.ipd.grgen.ast.BaseNode, de.unika.ipd.grgen.util.report.ErrorReporter)"/>
		public virtual bool Check(BaseNode bn, ErrorReporter reporter)
		{
			bool res = true;

			for(int i = 0; i < checkers.Length; i++)
			{
				bool r = checkers[i].Check(bn, reporter);

				res = res && r;
			}

			return res;
		}
	}

}
