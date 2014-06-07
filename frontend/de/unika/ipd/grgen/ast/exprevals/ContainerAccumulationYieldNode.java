/*
 * GrGen: graph rewrite generator tool -- release GrGen.NET 4.4
 * Copyright (C) 2003-2014 Universitaet Karlsruhe, Institut fuer Programmstrukturen und Datenorganisation, LS Goos; and free programmers
 * licensed under LGPL v3 (see LICENSE.txt included in the packaging of this file)
 * www.grgen.net
 */

/**
 * @author Edgar Jakumeit
 */

package de.unika.ipd.grgen.ast.exprevals;

import java.util.Collection;
import java.util.Vector;

import de.unika.ipd.grgen.ast.*;
import de.unika.ipd.grgen.ast.util.DeclarationResolver;
import de.unika.ipd.grgen.ir.exprevals.ContainerAccumulationYield;
import de.unika.ipd.grgen.ir.exprevals.EvalStatement;
import de.unika.ipd.grgen.ir.IR;
import de.unika.ipd.grgen.ir.Variable;
import de.unika.ipd.grgen.parser.Coords;

/**
 * AST node representing an accumulation yielding of a container variable.
 */
public class ContainerAccumulationYieldNode extends EvalStatementNode {
	static {
		setName(ContainerAccumulationYieldNode.class, "ContainerAccumulationYield");
	}

	BaseNode iterationVariableUnresolved;
	BaseNode iterationIndexUnresolved;
	IdentNode containerUnresolved;

	VarDeclNode iterationVariable;
	VarDeclNode iterationIndex;
	VarDeclNode container;
	CollectNode<EvalStatementNode> accumulationStatements;

	public ContainerAccumulationYieldNode(Coords coords, BaseNode iterationVariable, BaseNode iterationIndex,
			IdentNode container, CollectNode<EvalStatementNode> accumulationStatements) {
		super(coords);
		this.iterationVariableUnresolved = iterationVariable;
		becomeParent(this.iterationVariableUnresolved);
		this.iterationIndexUnresolved = iterationIndex;
		if(this.iterationIndexUnresolved!=null)
			becomeParent(this.iterationIndexUnresolved);
		this.containerUnresolved = container;
		becomeParent(this.containerUnresolved);
		this.accumulationStatements = accumulationStatements;
		becomeParent(this.accumulationStatements);
	}

	/** returns children of this node */
	@Override
	public Collection<BaseNode> getChildren() {
		Vector<BaseNode> children = new Vector<BaseNode>();
		children.add(getValidVersion(iterationVariableUnresolved, iterationVariable));
		if(iterationIndexUnresolved!=null)
			children.add(getValidVersion(iterationIndexUnresolved, iterationIndex));
		children.add(getValidVersion(containerUnresolved, container));
		children.add(accumulationStatements);
		return children;
	}

	/** returns names of the children, same order as in getChildren */
	@Override
	public Collection<String> getChildrenNames() {
		Vector<String> childrenNames = new Vector<String>();
		childrenNames.add("iterationVariable");
		if(iterationIndexUnresolved!=null)
			childrenNames.add("iterationIndex");
		childrenNames.add("container");
		childrenNames.add("accumulationStatements");
		return childrenNames;
	}

	private static final DeclarationResolver<VarDeclNode> containerResolver =
		new DeclarationResolver<VarDeclNode>(VarDeclNode.class);

	/** @see de.unika.ipd.grgen.ast.BaseNode#resolveLocal() */
	@Override
	protected boolean resolveLocal() {
		boolean successfullyResolved = true;

		container = containerResolver.resolve(containerUnresolved, this);
		if(container==null)
			successfullyResolved = false;

		if(iterationVariableUnresolved instanceof VarDeclNode) {
			iterationVariable = (VarDeclNode)iterationVariableUnresolved;
		} else {
			reportError("error in resolving iteration variable of container accumulation yield.");
			successfullyResolved = false;
		}

		if(iterationIndexUnresolved != null) {
			if(iterationIndexUnresolved instanceof VarDeclNode) {
				iterationIndex = (VarDeclNode)iterationIndexUnresolved;
			} else {
				reportError("error in resolving iteration index variable of container accumulation yield.");
				successfullyResolved = false;
			}
		}

		if(!iterationVariable.resolve())
			successfullyResolved = false;

		if(iterationIndex!=null)
			if(!iterationIndex.resolve())
				successfullyResolved = false;

		return successfullyResolved;
	}

	@Override
	protected boolean checkLocal() {
		return true;
	}

	public boolean checkStatementLocal(boolean isLHS, DeclNode root, EvalStatementNode enclosingLoop) {
		return true;
	}

	@Override
	protected IR constructIR() {
		ContainerAccumulationYield cay = new ContainerAccumulationYield(
				iterationVariable.checkIR(Variable.class),
				iterationIndex!=null ? iterationIndex.checkIR(Variable.class) : null,
				container.checkIR(Variable.class));
		for(EvalStatementNode accumulationStatement : accumulationStatements.children) 	
			cay.addAccumulationStatement(accumulationStatement.checkIR(EvalStatement.class));
		return cay;
	}
}
