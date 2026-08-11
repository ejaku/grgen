/*
 * GrGen: graph rewrite generator tool -- release GrGen.NET 8.1
 * Copyright (C) 2003-2026 Universitaet Karlsruhe, Institut fuer Programmstrukturen und Datenorganisation, LS Goos; and free programmers
 * licensed under LGPL v3, some components/parts use different licenses (see LICENSE.txt included in the packaging of this file)
 * www.grgen.de / www.grgen.net
 */

/// <summary>
/// @author shack
/// </summary>

namespace de.unika.ipd.grgen.util
{
/// <summary>
/// A walker calling a visitor after ascending from the last child
/// </summary>
public class PostWalker : PrePostWalker
{
	/// <summary>
	/// Make a new post walker. </summary>
	/// <param name="post"> The visitor to use after ascending from the last child of a node </param>
	public PostWalker(Visitor post)
		: base(new VisitorAnonymousInnerClass(), post)
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
