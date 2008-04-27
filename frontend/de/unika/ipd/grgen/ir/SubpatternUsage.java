/*
 * GrGen: graph rewrite generator tool -- release GrGen.NET v2 beta
 * Copyright (C) 2008 Universität Karlsruhe, Institut für Programmstrukturen und Datenorganisation, LS Goos
 * licensed under GPL v3 (see LICENSE.txt included in the packaging of this file)
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
