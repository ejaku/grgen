/*
 * GrGen: graph rewrite generator tool -- release GrGen.NET 4.0
 * Copyright (C) 2003-2013 Universitaet Karlsruhe, Institut fuer Programmstrukturen und Datenorganisation, LS Goos; and free programmers
 * licensed under LGPL v3 (see LICENSE.txt included in the packaging of this file)
 * www.grgen.net
 */

/**
 * @author Sebastian Hack
 */

package de.unika.ipd.grgen.util;

/**
 * A walker calling a visitor before descending to the first child
 */
public class PreWalker extends PrePostWalker
{
	/**
	 * Make a new pre walker.
	 * @param pre The visitor to use before descending to the first child of a node.
	 */
	public PreWalker(Visitor pre)
	{
		super(
			pre,
			new Visitor()
			{
				public void visit(Walkable w)
				{
				}
			}
		);
	}
}
