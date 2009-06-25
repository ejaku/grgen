/*
 * GrGen: graph rewrite generator tool -- release GrGen.NET 2.5
 * Copyright (C) 2009 Universitaet Karlsruhe, Institut fuer Programmstrukturen und Datenorganisation, LS Goos
 * licensed under LGPL v3 (see LICENSE.txt included in the packaging of this file)
 */

/**
 * @author Edgar Jakumeit
 * @version $Id$
 */
package de.unika.ipd.grgen.ir;

import java.util.List;

public class SubpatternUsage extends Identifiable {
	MatchingAction subpatternAction;
	List<GraphEntity> subpatternConnections;

	public SubpatternUsage(String name, Ident ident, MatchingAction subpatternAction, List<GraphEntity> connections) {
		super(name, ident);
		this.subpatternAction = subpatternAction;
		this.subpatternConnections = connections;
	}

	public MatchingAction getSubpatternAction() {
		return subpatternAction;
	}

	public List<GraphEntity> getSubpatternConnections() {
		return subpatternConnections;
	}
}
