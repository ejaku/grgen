/*
  GrGen: graph rewrite generator tool.
  Copyright (C) 2005  IPD Goos, Universit"at Karlsruhe, Germany

  This library is free software; you can redistribute it and/or
  modify it under the terms of the GNU Lesser General Public
  License as published by the Free Software Foundation; either
  version 2.1 of the License, or (at your option) any later version.

  This library is distributed in the hope that it will be useful,
  but WITHOUT ANY WARRANTY; without even the implied warranty of
  MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the GNU
  Lesser General Public License for more details.

  You should have received a copy of the GNU Lesser General Public
  License along with this library; if not, write to the Free Software
  Foundation, Inc., 51 Franklin St, Fifth Floor, Boston, MA  02110-1301  USA
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
