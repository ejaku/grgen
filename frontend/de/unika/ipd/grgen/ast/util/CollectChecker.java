/*
 * GrGen: graph rewrite generator tool -- release GrGen.NET 5.0
 * Copyright (C) 2003-2020 Universitaet Karlsruhe, Institut fuer Programmstrukturen und Datenorganisation, LS Goos; and free programmers
 * licensed under LGPL v3 (see LICENSE.txt included in the packaging of this file)
 * www.grgen.net
 */

/**
 * @author shack
 */

package de.unika.ipd.grgen.ast.util;

import de.unika.ipd.grgen.ast.BaseNode;
import de.unika.ipd.grgen.ast.CollectNode;
import de.unika.ipd.grgen.util.report.ErrorReporter;

/**
 * A checker that checks if the node is a collection node
 * and if so applies a contained child checker to all the children
 */
public class CollectChecker implements Checker
{
	/** The checker to apply to the children of the collect node to be checked by this checker */
	private Checker childChecker;

	/** Create checker with the checker to apply to the children */
	public CollectChecker(Checker childChecker)
	{
		this.childChecker = childChecker;
	}

	/** Check if the node is a collect node and if so apply the child checker to all children.
	 *  @see de.unika.ipd.grgen.ast.check.Checker#check(de.unika.ipd.grgen.ast.BaseNode, de.unika.ipd.grgen.util.report.ErrorReporter) */
	@Override
	public boolean check(BaseNode bn, ErrorReporter reporter)
	{
		if(bn instanceof CollectNode<?>) {
			boolean result = true;
			for(BaseNode child : bn.getChildren()) {
				result = childChecker.check(child, reporter) && result;
			}
			return result;
		} else {
			bn.reportError("Not a collect node"); // TODO: WTF? why report to the node??
			return false;
		}
	}
}
