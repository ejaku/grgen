/*
 * GrGen: graph rewrite generator tool -- release GrGen.NET 7.1
 * Copyright (C) 2003-2025 Universitaet Karlsruhe, Institut fuer Programmstrukturen und Datenorganisation, LS Goos; and free programmers
 * licensed under LGPL v3 (see LICENSE.txt included in the packaging of this file)
 * www.grgen.net
 */

/**
 * @author shack
 */

package de.unika.ipd.grgen.util;

/**
 * A walker calling a visitor after ascending from the last child
 */
public class PostWalker extends PrePostWalker
{
	/**
	 * Make a new post walker.
	 * @param post The visitor to use after ascending from the last child of a node
	 */
	public PostWalker(Visitor post)
	{
		super(
				new Visitor() {
					@Override
					public void visit(Walkable w)
					{
						// nothing to do
					}
				},
				post);
	}
}
