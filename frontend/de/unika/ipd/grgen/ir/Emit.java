/*
 * GrGen: graph rewrite generator tool -- release GrGen.NET 3.0
 * Copyright (C) 2003-2011 Universitaet Karlsruhe, Institut fuer Programmstrukturen und Datenorganisation, LS Goos
 * licensed under LGPL v3 (see LICENSE.txt included in the packaging of this file)
 * www.grgen.net
 */

/**
 * @author Rubino Geiss
 * @version $Id$
 */
package de.unika.ipd.grgen.ir;


import java.util.Collections;
import java.util.List;

/**
 * An emit statement.
 */
public class Emit extends IR implements ImperativeStmt, OrderedReplacement {

	private List<Expression> arguments;

	public Emit(List<Expression> arguments) {
		super("emit");
		this.arguments = arguments;
	}

	/**
	 * Returns Arguments
	 */
	public List<Expression> getArguments() {
		return Collections.unmodifiableList(arguments);
	}
}
