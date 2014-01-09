/*
 * GrGen: graph rewrite generator tool -- release GrGen.NET 4.2
 * Copyright (C) 2003-2014 Universitaet Karlsruhe, Institut fuer Programmstrukturen und Datenorganisation, LS Goos; and free programmers
 * licensed under LGPL v3 (see LICENSE.txt included in the packaging of this file)
 * www.grgen.net
 */

/**
 * @author Edgar Jakumeit
 */

package de.unika.ipd.grgen.ir.exprevals;

import de.unika.ipd.grgen.ir.Entity;
import de.unika.ipd.grgen.ir.GraphEntity;

/**
 * A procedure method invocation.
 */
public class ProcedureMethodInvocation extends ProcedureInvocationBase {
	/** The owner of the procedure method. */
	private Entity owner;

	/** The procedure of the procedure method invocation. */
	protected Procedure procedure;


	public ProcedureMethodInvocation(Entity owner, Procedure procedure) {
		super("procedure method invocation");

		this.owner = owner;
		this.procedure = procedure;
	}

	public Entity getOwner() {
		return owner;
	}

	public ProcedureBase getProcedureBase() {
		return procedure;
	}

	public Procedure getProcedure() {
		return procedure;
	}
	
	/** @see de.unika.ipd.grgen.ir.Expression#collectNeededEntities() */
	public void collectNeededEntities(NeededEntities needs) {
		if(!isGlobalVariable(owner))
			needs.add((GraphEntity) owner);
		for(Expression child : getWalkableChildren())
			child.collectNeededEntities(needs);
	}
}
