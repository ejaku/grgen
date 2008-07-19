/*
 * GrGen: graph rewrite generator tool -- release GrGen.NET 2.0
 * Copyright (C) 2008 Universitaet Karlsruhe, Institut fuer Programmstrukturen und Datenorganisation, LS Goos
 * licensed under GPL v3 (see LICENSE.txt included in the packaging of this file)
 */

/**
 * @author Sebastian Hack
 * @version $Id$
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
