/*
 * GrGen: graph rewrite generator tool -- release GrGen.NET 4.5
 * Copyright (C) 2003-2017 Universitaet Karlsruhe, Institut fuer Programmstrukturen und Datenorganisation, LS Goos; and free programmers
 * licensed under LGPL v3 (see LICENSE.txt included in the packaging of this file)
 * www.grgen.net
 */

/**
 * @author Edgar Jakumeit
 */

package de.unika.ipd.grgen.ir;

import java.util.List;

import de.unika.ipd.grgen.ir.exprevals.*;

public class SubpatternUsage extends Identifiable {
	Rule subpatternAction;
	List<Expression> subpatternConnections;
	List<Expression> subpatternYields;

	public SubpatternUsage(String name, Ident ident, Rule subpatternAction,
			List<Expression> connections, List<Expression> yields) {
		super(name, ident);
		this.subpatternAction = subpatternAction;
		this.subpatternConnections = connections;
		this.subpatternYields = yields;
	}

	public Rule getSubpatternAction() {
		return subpatternAction;
	}

	public List<Expression> getSubpatternConnections() {
		return subpatternConnections;
	}

	public List<Expression> getSubpatternYields() {
		return subpatternYields;
	}
}
