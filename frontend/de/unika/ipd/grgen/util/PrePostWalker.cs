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

using System.Collections.Generic;

/// <summary>
/// A walker calling visitors
/// pre before descending to the first child
/// post after ascending from the last child.
/// </summary>
public class PrePostWalker : Base, Walker
{
	private ISet<Walkable> visited;
	private Visitor pre, post;

	/// <summary>
	/// Creates PrePostWalker </summary>
	/// <param name="pre"> Visitor called before descending to the first child </param>
	/// <param name="post"> Visitor called after ascending from the last child </param>
	public PrePostWalker(Visitor pre, Visitor post)
	{
		this.pre = pre;
		this.post = post;
		visited = new HashSet<Walkable>();
	}

	public virtual void Reset()
	{
		visited.Clear();
	}

	public virtual void Walk(Walkable node)
	{
		if(!visited.Contains(node))
		{
			if(node != null)
			{
				visited.Add(node);

				if(pre != null)
					pre.Visit(node);

				foreach(Walkable p in node.WalkableChildren)
					Walk(p);

				if(post != null)
					post.Visit(node);
			}
			else
				Base.error.Error("Internal error: node was null, while walking.");
		}
	}
}

}
