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

public class SubpatternDependentReplacement extends Identifiable {
	SubpatternUsage subpatternUsage;
	List<GraphEntity> replConnections;

	public SubpatternDependentReplacement(String name, Ident ident, SubpatternUsage subpatternUsage, List<GraphEntity> replConnections) {
		super(name, ident);
		this.subpatternUsage = subpatternUsage;
		this.replConnections = replConnections;
	}

	public SubpatternUsage getSubpatternUsage() {
		return subpatternUsage;
	}

	public List<GraphEntity> getReplConnections() {
		return replConnections;
	}
}
