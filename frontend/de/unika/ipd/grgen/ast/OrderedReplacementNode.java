/*
 * GrGen: graph rewrite generator tool -- release GrGen.NET 5.0
 * Copyright (C) 2003-2020 Universitaet Karlsruhe, Institut fuer Programmstrukturen und Datenorganisation, LS Goos; and free programmers
 * licensed under LGPL v3 (see LICENSE.txt included in the packaging of this file)
 * www.grgen.net
 */

/**
 * @author Edgar Jakumeit
 */

package de.unika.ipd.grgen.ast;

import de.unika.ipd.grgen.parser.Coords;

public abstract class OrderedReplacementNode extends BaseNode {
	// no functionality, allows ordering of subpattern replacement nodes and emit here nodes
	// in one container of the ordered replacement node type

	protected OrderedReplacementNode(Coords coords) {
		super(coords);
	}

	protected OrderedReplacementNode() {
		super();
	}

	public boolean noExecStatement(boolean inEvalHereContext) {
		boolean res = true;
		for(BaseNode child : getChildren()) {
			if(!(child instanceof OrderedReplacementNode)) {
				continue;
			}
			OrderedReplacementNode orderedReplacement = (OrderedReplacementNode)child;
			res &= orderedReplacement.noExecStatement(inEvalHereContext);
		}
		return res;
	}
}
