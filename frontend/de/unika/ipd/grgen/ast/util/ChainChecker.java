/*
 * GrGen: graph rewrite generator tool -- release GrGen.NET 2.6
 * Copyright (C) 2003-2010 Universitaet Karlsruhe, Institut fuer Programmstrukturen und Datenorganisation, LS Goos
 * licensed under LGPL v3 (see LICENSE.txt included in the packaging of this file)
 * www.grgen.net
 */

/**
 * @author Sebastian Hack
 * @version $Id$
 */
package de.unika.ipd.grgen.ast.util;

import de.unika.ipd.grgen.ast.BaseNode;
import de.unika.ipd.grgen.util.report.ErrorReporter;

/**
 * Checker containing list of checkers to apply one after the other to the node to check
 */
public class ChainChecker implements Checker
{
	/** The chain, i.e. list with the checkers to apply */
	private Checker[] checkers;

	/** Create checker with the list of checkers to apply */
	public ChainChecker(Checker[] checkers)
	{
		super();
		this.checkers = checkers;
	}

	/**
	 * Check the node with the checkers from the list, one after the other
	 * @see de.unika.ipd.grgen.ast.util.Checker#check(de.unika.ipd.grgen.ast.BaseNode, de.unika.ipd.grgen.util.report.ErrorReporter)
	 */
	public boolean check(BaseNode node, ErrorReporter reporter)
	{
		boolean res = true;

		for (int i = 0; i < checkers.length; i++) {
			boolean r = checkers[i].check(node, reporter);

			res = res && r;
		}

		return res;
	}
}
