/*
 * GrGen: graph rewrite generator tool -- release GrGen.NET 2.6
 * Copyright (C) 2003-2009 Universitaet Karlsruhe, Institut fuer Programmstrukturen und Datenorganisation, LS Goos
 * licensed under LGPL v3 (see LICENSE.txt included in the packaging of this file)
 */

/**
 * @author Edgar Jakumeit
 * @version $Id$
 */
package de.unika.ipd.grgen.ir;

import java.util.List;

public class SubpatternDependentReplacement extends Identifiable implements OrderedReplacement{
	SubpatternUsage subpatternUsage;
	List<Expression> replConnections;

	public SubpatternDependentReplacement(String name, Ident ident,
			SubpatternUsage subpatternUsage, List<Expression> replConnections) {
		super(name, ident);
		this.subpatternUsage = subpatternUsage;
		this.replConnections = replConnections;
	}

	public SubpatternUsage getSubpatternUsage() {
		return subpatternUsage;
	}

	public List<Expression> getReplConnections() {
		return replConnections;
	}
}
