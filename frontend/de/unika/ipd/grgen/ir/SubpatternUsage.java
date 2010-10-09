/*
 * GrGen: graph rewrite generator tool -- release GrGen.NET 2.6
 * Copyright (C) 2003-2010 Universitaet Karlsruhe, Institut fuer Programmstrukturen und Datenorganisation, LS Goos
 * licensed under LGPL v3 (see LICENSE.txt included in the packaging of this file)
 * www.grgen.net
 */

/**
 * @author Edgar Jakumeit
 * @version $Id$
 */
package de.unika.ipd.grgen.ir;

import java.util.List;

public class SubpatternUsage extends Identifiable {
	Rule subpatternAction;
	List<Expression> subpatternConnections;

	public SubpatternUsage(String name, Ident ident, Rule subpatternAction, List<Expression> connections) {
		super(name, ident);
		this.subpatternAction = subpatternAction;
		this.subpatternConnections = connections;
	}

	public Rule getSubpatternAction() {
		return subpatternAction;
	}

	public List<Expression> getSubpatternConnections() {
		return subpatternConnections;
	}
}
