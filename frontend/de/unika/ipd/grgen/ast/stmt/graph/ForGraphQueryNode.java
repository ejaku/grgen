/*
 * GrGen: graph rewrite generator tool -- release GrGen.NET 7.1
 * Copyright (C) 2003-2025 Universitaet Karlsruhe, Institut fuer Programmstrukturen und Datenorganisation, LS Goos; and free programmers
 * licensed under LGPL v3 (see LICENSE.txt included in the packaging of this file)
 * www.grgen.net
 */

/**
 * @author Edgar Jakumeit
 */

package de.unika.ipd.grgen.ast.stmt.graph;

import de.unika.ipd.grgen.ast.*;
import de.unika.ipd.grgen.ast.decl.pattern.VarDeclNode;
import de.unika.ipd.grgen.ast.model.type.EdgeTypeNode;
import de.unika.ipd.grgen.ast.model.type.NodeTypeNode;
import de.unika.ipd.grgen.ast.stmt.EvalStatementNode;
import de.unika.ipd.grgen.ast.stmt.NestingStatementNode;
import de.unika.ipd.grgen.ast.type.TypeNode;
import de.unika.ipd.grgen.parser.Coords;

/**
 * AST node representing a for lookup of a neighborhood function.
 */
public abstract class ForGraphQueryNode extends NestingStatementNode
{
	static {
		setName(ForGraphQueryNode.class, "ForGraphQuery");
	}

	BaseNode iterationVariableUnresolved;
	VarDeclNode iterationVariable;

	protected ForGraphQueryNode(Coords coords, BaseNode iterationVariable, CollectNode<EvalStatementNode> loopedStatements)
	{
		super(coords, loopedStatements);
		this.iterationVariableUnresolved = iterationVariable;
		becomeParent(this.iterationVariableUnresolved);
	}

	protected boolean resolveIterationVariable(String forType)
	{
		boolean successfullyResolved = true;

		if(iterationVariableUnresolved instanceof VarDeclNode) {
			iterationVariable = (VarDeclNode)iterationVariableUnresolved;
		} else {
			reportError("Error in resolving iteration variable of for " + forType + " loop.");
			successfullyResolved = false;
		}

		if(!iterationVariable.resolve())
			successfullyResolved = false;

		return successfullyResolved;
	}

	protected boolean checkIterationVariable(String forType)
	{
		TypeNode iterationVariableType = iterationVariable.getDeclType();
		if(!(iterationVariableType instanceof NodeTypeNode)
				&& !(iterationVariableType instanceof EdgeTypeNode)) {
			reportError("Iteration variable of for " + forType + " loop must be of type Node or Edge"
					+ " (but is of type " + iterationVariableType.toStringWithDeclarationCoords() + ").");
			return false;
		}
		
		return true;
	}
}
