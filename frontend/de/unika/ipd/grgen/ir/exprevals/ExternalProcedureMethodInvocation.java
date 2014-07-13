/*
 * GrGen: graph rewrite generator tool -- release GrGen.NET 4.4
 * Copyright (C) 2003-2014 Universitaet Karlsruhe, Institut fuer Programmstrukturen und Datenorganisation, LS Goos; and free programmers
 * licensed under LGPL v3 (see LICENSE.txt included in the packaging of this file)
 * www.grgen.net
 */

/**
 * @author Edgar Jakumeit
 */

package de.unika.ipd.grgen.ir.exprevals;

import java.util.ArrayList;
import java.util.List;

import de.unika.ipd.grgen.ir.GraphEntity;
import de.unika.ipd.grgen.ir.Variable;

/**
 * An external procedure method invocation.
 */
public class ExternalProcedureMethodInvocation extends ProcedureInvocationBase {
	/** The owner of the procedure method. */
	private Qualification ownerQual;
	private Variable ownerVar;

	/** The arguments of the procedure invocation. */
	protected List<Expression> arguments = new ArrayList<Expression>();

	/** The procedure of the procedure method invocation expression. */
	protected ExternalProcedure externalProcedure;

	public ExternalProcedureMethodInvocation(Qualification ownerQual, ExternalProcedure externalProcedure) {
		super("external procedure method invocation");

		this.ownerQual = ownerQual;
		this.externalProcedure = externalProcedure;
	}

	public ExternalProcedureMethodInvocation(Variable ownerVar, ExternalProcedure externalProcedure) {
		super("external procedure method invocation");

		this.ownerVar = ownerVar;
		this.externalProcedure = externalProcedure;
	}

	public Qualification getOwnerQual() {
		return ownerQual;
	}

	public Variable getOwnerVar() {
		return ownerVar;
	}

	public ProcedureBase getProcedureBase() {
		return externalProcedure;
	}

	public ExternalProcedure getExternalProc() {
		return externalProcedure;
	}
	
	/** @see de.unika.ipd.grgen.ir.Expression#collectNeededEntities() */
	public void collectNeededEntities(NeededEntities needs) {
		if(ownerQual!=null) {
			ownerQual.collectNeededEntities(needs);
			if(ownerQual.getOwner()!=null)
				if(ownerQual.getOwner() instanceof GraphEntity)
					needs.add((GraphEntity)ownerQual.getOwner());
		} else {
			if(!isGlobalVariable(ownerVar))
				needs.add(ownerVar);
		}
		for(Expression child : getWalkableChildren())
			child.collectNeededEntities(needs);
	}
}
