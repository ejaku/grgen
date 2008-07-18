/*
 * GrGen: graph rewrite generator tool -- release GrGen.NET v2 beta
 * Copyright (C) 2008 Universitaet Karlsruhe, Institut fuer Programmstrukturen und Datenorganisation, LS Goos
 * licensed under GPL v3 (see LICENSE.txt included in the packaging of this file)
 */

/**
 * @author Rubino Geiss
 * @version $Id$
 */
package de.unika.ipd.grgen.ir;

import java.util.Collections;
import java.util.LinkedHashSet;
import java.util.Set;

/**
 * A XGRS in an emit statement.
 */
public class Exec extends IR implements ImperativeStmt {
	private Set<Expression> parameters = new LinkedHashSet<Expression>();

	private String xgrsString;

	public Exec(String xgrsString, Set<Expression> parameters) {
		super("exec");
		this.xgrsString = xgrsString;
		this.parameters = parameters;
	}


	/**
	 * Returns XGRS as an String
	 */
	public String getXGRSString() {
		return xgrsString;
	}

	/**
	 * Returns Parameters
	 */
	public Set<Expression> getArguments() {
		return Collections.unmodifiableSet(parameters);
	}
}
