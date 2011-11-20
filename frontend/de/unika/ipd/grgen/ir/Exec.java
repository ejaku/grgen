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
import java.util.LinkedHashSet;
import java.util.Set;

/**
 * A XGRS in an exec statement.
 */
public class Exec extends IR implements ImperativeStmt {
	private Set<Expression> parameters = new LinkedHashSet<Expression>();
	private Set<Entity> neededEntities;
	private String xgrsString;
	private int lineNr;

	public Exec(String xgrsString, Set<Expression> parameters, int lineNr) {
		super("exec");
		this.xgrsString = xgrsString;
		this.parameters = parameters;
		this.lineNr = lineNr;
	}

	/** Returns XGRS as an String */
	public String getXGRSString() {
		return xgrsString;
	}
	
	public int getLineNr() {
		return lineNr;
	}

	/** Returns Parameters */
	public Set<Expression> getArguments() {
		return Collections.unmodifiableSet(parameters);
	}

	public Set<Entity> getNeededEntities() {
		if(neededEntities == null) {
			NeededEntities needs = new NeededEntities(false, false, false, true, false, false);  // collect all entities
			for(Expression param : getArguments())
				param.collectNeededEntities(needs);

			neededEntities = needs.entities;
		}
		return neededEntities;
	}
}
