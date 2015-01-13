/*
 * GrGen: graph rewrite generator tool -- release GrGen.NET 4.4
 * Copyright (C) 2003-2015 Universitaet Karlsruhe, Institut fuer Programmstrukturen und Datenorganisation, LS Goos; and free programmers
 * licensed under LGPL v3 (see LICENSE.txt included in the packaging of this file)
 * www.grgen.net
 */

/**
 * @author Edgar Jakumeit
 */

package de.unika.ipd.grgen.ir.exprevals;

import java.util.Set;

import de.unika.ipd.grgen.ir.*;

/**
 * Represents an exec statement embedded within a computation in the IR.
 */
public class ExecStatement extends EvalStatement {

	private Exec exec;

	public ExecStatement(Exec exec) {
		super("exec statement");
		this.exec = exec;
	}

	public Exec getExec() {
		return exec;
	}

	public void collectNeededEntities(NeededEntities needs)
	{
		needs.needsGraph();
		for(Expression arg : getExec().getArguments()) {
			arg.collectNeededEntities(needs);
		}
	}
	
	public Set<Entity> getNeededEntities(boolean forComputation) {
		return exec.getNeededEntities(forComputation);
	}
	
	/** Returns XGRS as an String */
	public String getXGRSString() {
		return exec.getXGRSString();
	}
	
	public int getLineNr() {
		return exec.getLineNr();
	}
}
