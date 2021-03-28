/*
 * GrGen: graph rewrite generator tool -- release GrGen.NET 6.0
 * Copyright (C) 2003-2021 Universitaet Karlsruhe, Institut fuer Programmstrukturen und Datenorganisation, LS Goos; and free programmers
 * licensed under LGPL v3 (see LICENSE.txt included in the packaging of this file)
 * www.grgen.net
 */

/**
 * @author Edgar Jakumeit
 */

package de.unika.ipd.grgen.ast.stmt;

import java.util.Collection;
import java.util.Vector;

import de.unika.ipd.grgen.ast.*;
import de.unika.ipd.grgen.ast.decl.DeclNode;
import de.unika.ipd.grgen.ast.decl.pattern.EdgeDeclNode;
import de.unika.ipd.grgen.ast.decl.pattern.IteratedDeclNode;
import de.unika.ipd.grgen.ast.decl.pattern.NodeDeclNode;
import de.unika.ipd.grgen.ast.decl.pattern.VarDeclNode;
import de.unika.ipd.grgen.ast.util.DeclarationResolver;
import de.unika.ipd.grgen.ir.IR;
import de.unika.ipd.grgen.ir.executable.Rule;
import de.unika.ipd.grgen.ir.stmt.EvalStatement;
import de.unika.ipd.grgen.ir.stmt.IteratedAccumulationYield;
import de.unika.ipd.grgen.ir.pattern.Variable;
import de.unika.ipd.grgen.parser.Coords;

/**
 * AST node representing an accumulation yielding of an iterated match def variable.
 */
public class IteratedAccumulationYieldNode extends NestingStatementNode
{
	static {
		setName(IteratedAccumulationYieldNode.class, "IteratedAccumulationYield");
	}

	BaseNode iterationVariableUnresolved;
	IdentNode iteratedUnresolved;

	VarDeclNode iterationVariable;
	IteratedDeclNode iterated;

	public IteratedAccumulationYieldNode(Coords coords, BaseNode iterationVariable, IdentNode iterated,
			CollectNode<EvalStatementNode> accumulationStatements)
	{
		super(coords, accumulationStatements);
		this.iterationVariableUnresolved = iterationVariable;
		becomeParent(this.iterationVariableUnresolved);
		this.iteratedUnresolved = iterated;
		becomeParent(this.iteratedUnresolved);
	}

	/** returns children of this node */
	@Override
	public Collection<BaseNode> getChildren()
	{
		Vector<BaseNode> children = new Vector<BaseNode>();
		children.add(getValidVersion(iterationVariableUnresolved, iterationVariable));
		children.add(getValidVersion(iteratedUnresolved, iterated));
		children.add(statements);
		return children;
	}

	/** returns names of the children, same order as in getChildren */
	@Override
	public Collection<String> getChildrenNames()
	{
		Vector<String> childrenNames = new Vector<String>();
		childrenNames.add("iterationVariable");
		childrenNames.add("iterated");
		childrenNames.add("accumulationStatements");
		return childrenNames;
	}

	private static final DeclarationResolver<IteratedDeclNode> iteratedResolver =
			new DeclarationResolver<IteratedDeclNode>(IteratedDeclNode.class);

	/** @see de.unika.ipd.grgen.ast.BaseNode#resolveLocal() */
	@Override
	protected boolean resolveLocal()
	{
		boolean successfullyResolved = true;

		iterated = iteratedResolver.resolve(iteratedUnresolved, this);
		if(iterated == null)
			successfullyResolved = false;

		if(iterationVariableUnresolved instanceof VarDeclNode) {
			iterationVariable = (VarDeclNode)iterationVariableUnresolved;
		//} else if(accumulationVariableUnresolved instanceof ConstraintDeclNode) {
		//	accumulationGraphElement = (ConstraintDeclNode)accumulationVariableUnresolved;
		} else {
			reportError("error in resolving iteration variable of iterated accumulation yield.");
			successfullyResolved = false;
		}

		if((iterationVariable.context & BaseNode.CONTEXT_LHS_OR_RHS) == CONTEXT_RHS) {
			reportError("An iterated accumulation loop can only be used within a yield block in the pattern.");
			successfullyResolved = false;
		}

		boolean iterationVariableFound = false;
		for(VarDeclNode var : iterated.pattern.getDefVariablesToBeYieldedTo().getChildren()) {
			if(iterationVariable.toString().equals(var.toString())) {
				iterationVariable.typeUnresolved = var.typeUnresolved;
				iterationVariableFound = true;
			}
		}
		for(NodeDeclNode node : iterated.pattern.getNodes()) {
			if(iterationVariable.toString().equals(node.toString())) {
				iterationVariable.typeUnresolved = node.typeUnresolved;
				iterationVariableFound = true;
			}
		}
		for(EdgeDeclNode edge : iterated.pattern.getEdges()) {
			if(iterationVariable.toString().equals(edge.toString())) {
				iterationVariable.typeUnresolved = edge.typeUnresolved;
				iterationVariableFound = true;
			}
		}

		if(!iterationVariableFound) {
			reportError("can't find iteration variable in iterated");
			successfullyResolved = false;
		}

		if(!iterationVariable.resolve())
			successfullyResolved = false;

		return successfullyResolved;
	}

	@Override
	protected boolean checkLocal()
	{
		return true;
	}

	@Override
	public boolean checkStatementLocal(boolean isLHS, DeclNode root, EvalStatementNode enclosingLoop)
	{
		return true;
	}

	@Override
	protected IR constructIR()
	{
		IteratedAccumulationYield iay = new IteratedAccumulationYield(iterationVariable.checkIR(Variable.class),
				iterated.checkIR(Rule.class));
		for(EvalStatementNode accumulationStatement : statements.getChildren()) {
			iay.addStatement(accumulationStatement.checkIR(EvalStatement.class));
		}
		return iay;
	}
}
