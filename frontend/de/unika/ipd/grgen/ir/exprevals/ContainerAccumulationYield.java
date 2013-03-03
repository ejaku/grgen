/*
 * GrGen: graph rewrite generator tool -- release GrGen.NET 3.6
 * Copyright (C) 2003-2013 Universitaet Karlsruhe, Institut fuer Programmstrukturen und Datenorganisation, LS Goos; and free programmers
 * licensed under LGPL v3 (see LICENSE.txt included in the packaging of this file)
 * www.grgen.net
 */

/**
 * @author Edgar Jakumeit
 */

package de.unika.ipd.grgen.ir.exprevals;

import java.util.Collection;
import java.util.LinkedList;

import de.unika.ipd.grgen.ast.BaseNode;
import de.unika.ipd.grgen.ir.*;

/**
 * Represents an accumulation yielding of a container variable in the IR.
 */
public class ContainerAccumulationYield extends EvalStatement {

	private Variable iterationVar;
	private Variable indexVar;
	private Variable containerVar;
	private Collection<EvalStatement> accumulationStatements = new LinkedList<EvalStatement>();

	public ContainerAccumulationYield(Variable iterationVar, Variable indexVar, 
			Variable containerVar) {
		super("container accumulation yield");
		this.iterationVar = iterationVar;
		this.indexVar = indexVar;
		this.containerVar = containerVar;
	}

	public void addAccumulationStatement(EvalStatement accumulationStatement) {
		accumulationStatements.add(accumulationStatement);
	}

	public Variable getIterationVar() {
		return iterationVar;
	}

	public Variable getIndexVar() {
		return indexVar;
	}

	public Variable getContainer() {
		return containerVar;
	}

	public Collection<EvalStatement> getAccumulationStatements() {
		return accumulationStatements;
	}

	public void collectNeededEntities(NeededEntities needs)
	{
		if(!isGlobalVariable(containerVar) && (containerVar.getContext()&BaseNode.CONTEXT_COMPUTATION)!=BaseNode.CONTEXT_COMPUTATION)
			needs.add(containerVar);
		for(EvalStatement accumulationStatement : accumulationStatements)
			accumulationStatement.collectNeededEntities(needs);
		needs.variables.remove(iterationVar);
		if(indexVar != null)
			needs.variables.remove(indexVar);
	}
}
