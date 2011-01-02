/*
 * GrGen: graph rewrite generator tool -- release GrGen.NET 2.7
 * Copyright (C) 2003-2011 Universitaet Karlsruhe, Institut fuer Programmstrukturen und Datenorganisation, LS Goos
 * licensed under LGPL v3 (see LICENSE.txt included in the packaging of this file)
 * www.grgen.net
 */

/**
 * @author Edgar Jakumeit
 * @version $Id: SubpatternReplNode.java 26712 2009-11-02 23:11:59Z eja $
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
}
