/*
 * GrGen: graph rewrite generator tool -- release GrGen.NET 8.1
 * Copyright (C) 2003-2026 Universitaet Karlsruhe, Institut fuer Programmstrukturen und Datenorganisation, LS Goos; and free programmers
 * licensed under LGPL v3, some components/parts use different licenses (see LICENSE.txt included in the packaging of this file)
 * www.grgen.de / www.grgen.net
 */

/// <summary>
/// @author Sebastian Hack
/// </summary>

namespace de.unika.ipd.grgen.util
{
	/// <summary>
	/// A walker calling a visitor before descending to the first child
	/// </summary>
	public class PreWalker : PrePostWalker
	{
		/// <summary>
		/// Make a new pre walker. </summary>
		/// <param name="pre"> The visitor to use before descending to the first child of a node. </param>
		public PreWalker(Visitor pre)
			: base(pre, new VisitorAnonymousInnerClass())
		{
		}

		private class VisitorAnonymousInnerClass : Visitor
		{
			public void visit(Walkable w)
			{
				// nothing to do
			}
		}
	}

}
