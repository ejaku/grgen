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
	using CollectBaseNode = de.unika.ipd.grgen.ast.CollectBaseNode;
	using ErrorReporter = de.unika.ipd.grgen.util.report.ErrorReporter;

	/// <summary>
	/// A checker that checks if the node is a collection node
	/// and if so applies a contained child checker to all the children
	/// </summary>
	public class CollectChecker : Checker
	{
		/// <summary>
		/// The checker to apply to the children of the collect node to be checked by this checker </summary>
		private Checker childChecker;

		/// <summary>
		/// Create checker with the checker to apply to the children </summary>
		public CollectChecker(Checker childChecker)
		{
			this.childChecker = childChecker;
		}

		/// <summary>
		/// Check if the node is a collect node and if so apply the child checker to all children. </summary>
		///  <seealso cref="de.unika.ipd.grgen.ast.check.Checker.check(de.unika.ipd.grgen.ast.BaseNode, de.unika.ipd.grgen.util.report.ErrorReporter) "/>
		public virtual bool Check(BaseNode bn, ErrorReporter reporter)
		{
			if(bn is CollectBaseNode)
			{
				bool result = true;
				foreach(BaseNode child in bn.Children)
					result = childChecker.Check(child, reporter) && result;
				return result;
			}
			else
			{
				bn.ReportError("Not a collect node."); // TODO: WTF? why report to the node??
				return false;
			}
		}
	}

}
