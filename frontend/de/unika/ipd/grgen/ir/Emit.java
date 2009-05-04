/*
 * GrGen: graph rewrite generator tool -- release GrGen.NET 2.5
 * Copyright (C) 2009 Universitaet Karlsruhe, Institut fuer Programmstrukturen und Datenorganisation, LS Goos
 * licensed under GPL v3 (see LICENSE.txt included in the packaging of this file)
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
public class Emit extends IR  implements ImperativeStmt {

	private List<Expression> arguments;
	private boolean isPre;

	public Emit(List<Expression> arguments, boolean isPre) {
		super("emit");
		this.arguments = arguments;
		this.isPre = isPre;
	}

	public boolean isPreEmit()	{
		return isPre;
	}

	/**
	 * Returns Arguments
	 */
	public List<Expression> getArguments() {
		return Collections.unmodifiableList(arguments);
	}
}
