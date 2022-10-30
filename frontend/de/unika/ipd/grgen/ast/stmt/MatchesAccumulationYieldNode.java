/*
 * GrGen: graph rewrite generator tool -- release GrGen.NET 6.6
 * Copyright (C) 2003-2022 Universitaet Karlsruhe, Institut fuer Programmstrukturen und Datenorganisation, LS Goos; and free programmers
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
import de.unika.ipd.grgen.ast.decl.pattern.VarDeclNode;
import de.unika.ipd.grgen.ast.type.DefinedMatchTypeNode;
import de.unika.ipd.grgen.ast.type.MatchTypeActionNode;
import de.unika.ipd.grgen.ast.type.MatchTypeNode;
import de.unika.ipd.grgen.ast.type.TypeNode;
import de.unika.ipd.grgen.ast.type.container.ArrayTypeNode;
import de.unika.ipd.grgen.ast.util.DeclarationResolver;
import de.unika.ipd.grgen.ir.stmt.EvalStatement;
import de.unika.ipd.grgen.ir.stmt.MatchesAccumulationYield;
import de.unika.ipd.grgen.ir.IR;
import de.unika.ipd.grgen.ir.pattern.Variable;
import de.unika.ipd.grgen.parser.Coords;

/**
 * AST node representing an accumulation yielding of a matches variable.
 */
public class MatchesAccumulationYieldNode extends NestingStatementNode
{
	static {
		setName(MatchesAccumulationYieldNode.class, "MatchesAccumulationYield");
	}

	VarDeclNode iterationVariableUnresolved;
	IdentNode matchesContainerUnresolved;

	VarDeclNode iterationVariable;
	VarDeclNode matchesContainer;

	public MatchesAccumulationYieldNode(Coords coords, VarDeclNode iterationVariable, IdentNode matchesContainer,
			CollectNode<EvalStatementNode> accumulationStatements)
	{
		super(coords, accumulationStatements);
		this.iterationVariableUnresolved = iterationVariable;
		becomeParent(this.iterationVariableUnresolved);
		this.matchesContainerUnresolved = matchesContainer;
		becomeParent(this.matchesContainerUnresolved);
	}

	/** returns children of this node */
	@Override
	public Collection<BaseNode> getChildren()
	{
		Vector<BaseNode> children = new Vector<BaseNode>();
		children.add(getValidVersion(iterationVariableUnresolved, iterationVariable));
		children.add(getValidVersion(matchesContainerUnresolved, matchesContainer));
		children.add(statements);
		return children;
	}

	/** returns names of the children, same order as in getChildren */
	@Override
	public Collection<String> getChildrenNames()
	{
		Vector<String> childrenNames = new Vector<String>();
		childrenNames.add("iterationVariable");
		childrenNames.add("matchesContainer");
		childrenNames.add("accumulationStatements");
		return childrenNames;
	}

	private static final DeclarationResolver<VarDeclNode> matchesResolver =
			new DeclarationResolver<VarDeclNode>(VarDeclNode.class);

	/** @see de.unika.ipd.grgen.ast.BaseNode#resolveLocal() */
	@Override
	protected boolean resolveLocal()
	{
		boolean successfullyResolved = true;

		/*if(!(matchesContainerUnresolved.toString().equals("this"))) {
			reportError("for matches loop expects to iterate the matches stored in the this object (of type array<match<rule-name>> or array<class match<class match-class-name>>)");
		}*/

		matchesContainer = matchesResolver.resolve(matchesContainerUnresolved, this);
		if(matchesContainer == null)
			successfullyResolved = false;

		if(iterationVariableUnresolved instanceof VarDeclNode) {
			iterationVariable = (VarDeclNode)iterationVariableUnresolved;
		} else {
			reportError("Error in resolving the iteration variable of the for matches loop.");
			successfullyResolved = false;
		}

		if(!iterationVariable.resolve())
			successfullyResolved = false;

		return successfullyResolved;
	}

	@Override
	protected boolean checkLocal()
	{
		TypeNode matchesContainerType = matchesContainer.getDeclType();
		if(!(matchesContainerType instanceof ArrayTypeNode)) {
			reportError("The for matches loop expects to iterate an array of matches (of type array<match<rule-name>> or array<match<class match-class-name>>), but is given: "
					+ matchesContainerType.toStringWithDeclarationCoords());
			return false;
		}
		TypeNode matchesArrayValueType = ((ArrayTypeNode)matchesContainerType).valueType;

		MatchTypeActionNode matchesContainerActionMatchType = matchesArrayValueType instanceof MatchTypeActionNode
				? (MatchTypeActionNode)matchesArrayValueType
				: null;
		DefinedMatchTypeNode matchesContainerDefinedMatchType = matchesArrayValueType instanceof DefinedMatchTypeNode
				? (DefinedMatchTypeNode)matchesArrayValueType
				: null;
		MatchTypeNode matchesContainerMatchType = matchesContainerActionMatchType != null
				? (MatchTypeNode)matchesContainerActionMatchType : (MatchTypeNode)matchesContainerDefinedMatchType;
		if(matchesContainerActionMatchType == null && matchesContainerDefinedMatchType == null) {
			reportError("The for matches loop expects to iterate an array of matches (of type array<match<rule-name>> or array<match<class match-class-name>>), but is given as array element type: "
					+ matchesArrayValueType.toStringWithDeclarationCoords());
			return false;
		}

		TypeNode iterationVariableType = iterationVariable.getDeclType();
		MatchTypeActionNode iterationVariableActionMatchType = iterationVariableType instanceof MatchTypeActionNode
				? (MatchTypeActionNode)iterationVariableType
				: null;
		DefinedMatchTypeNode iterationVariableDefinedMatchType = iterationVariableType instanceof DefinedMatchTypeNode
				? (DefinedMatchTypeNode)iterationVariableType
				: null;
		MatchTypeNode iterationVariableMatchType = iterationVariableActionMatchType != null
				? (MatchTypeNode)iterationVariableActionMatchType : (MatchTypeNode)iterationVariableDefinedMatchType;
		if(iterationVariableActionMatchType == null && iterationVariableDefinedMatchType == null) {
			reportError("The for matches loop expects an iteration variable of match type (match<rule-name> or match<class match-class-name>), but is given: "
					+ iterationVariableType.toStringWithDeclarationCoords());
			return false;
		}

		if(!iterationVariableMatchType.isEqual(matchesContainerMatchType)) {
			reportError("The iteration variable of the for matches loop is of type " + iterationVariableMatchType.toStringWithDeclarationCoords()
					+ " but the matches container is of type " + matchesContainerMatchType.toStringWithDeclarationCoords());
			//"(defined by the rule referenced by the filter function)" "(defined by the match class referenced by the match class filter function)"
			return false;
		}

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
		MatchesAccumulationYield may = new MatchesAccumulationYield(iterationVariable.checkIR(Variable.class),
				matchesContainer.checkIR(Variable.class));
		for(EvalStatementNode accumulationStatement : statements.getChildren()) {
			may.addStatement(accumulationStatement.checkIR(EvalStatement.class));
		}
		return may;
	}
}
