/*
 * GrGen: graph rewrite generator tool -- release GrGen.NET 5.0
 * Copyright (C) 2003-2020 Universitaet Karlsruhe, Institut fuer Programmstrukturen und Datenorganisation, LS Goos; and free programmers
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
import de.unika.ipd.grgen.ast.typedecl.ArrayTypeNode;
import de.unika.ipd.grgen.ast.util.DeclarationResolver;
import de.unika.ipd.grgen.ir.exprevals.MatchesAccumulationYield;
import de.unika.ipd.grgen.ir.exprevals.EvalStatement;
import de.unika.ipd.grgen.ir.IR;
import de.unika.ipd.grgen.ir.Variable;
import de.unika.ipd.grgen.parser.Coords;

/**
 * AST node representing an accumulation yielding of a matches variable.
 */
public class MatchesAccumulationYieldNode extends NestingStatementNode
{
	static {
		setName(MatchesAccumulationYieldNode.class, "MatchesAccumulationYield");
	}

	BaseNode iterationVariableUnresolved;
	IdentNode matchesContainerUnresolved;

	VarDeclNode iterationVariable;
	VarDeclNode matchesContainer;

	public MatchesAccumulationYieldNode(Coords coords, BaseNode iterationVariable, IdentNode matchesContainer,
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
			reportError("error in resolving iteration variable of for matches loop.");
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
			reportError("for matches loop expects to iterate an array of matches (of type array<match<rule-name>> or array<class match<class match-class-name>>), but is given: "
					+ matchesContainerType.toString());
			return false;
		}
		TypeNode matchesArrayValueType = ((ArrayTypeNode)matchesContainerType).valueType;

		MatchTypeNode matchesContainerMatchType = matchesArrayValueType instanceof MatchTypeNode
				? (MatchTypeNode)matchesArrayValueType
				: null;
		DefinedMatchTypeNode matchesContainerDefinedMatchType = matchesArrayValueType instanceof DefinedMatchTypeNode
				? (DefinedMatchTypeNode)matchesArrayValueType
				: null;
		if(matchesContainerMatchType == null && matchesContainerDefinedMatchType == null) {
			reportError("for matches loop expects to iterate an array of matches (of type array<match<rule-name>> or array<class match<class match-class-name>>), but is given as array element type: "
					+ matchesArrayValueType.toString());
			return false;
		}

		TypeNode iterationVariableType = iterationVariable.getDeclType();
		MatchTypeNode iterationVariableMatchType = iterationVariableType instanceof MatchTypeNode
				? (MatchTypeNode)iterationVariableType
				: null;
		DefinedMatchTypeNode iterationVariableDefinedMatchType = iterationVariableType instanceof DefinedMatchTypeNode
				? (DefinedMatchTypeNode)iterationVariableType
				: null;
		if(iterationVariableMatchType == null && iterationVariableDefinedMatchType == null) {
			reportError("for matches loop expects an iteration variable of matches type (match<rule-name> or match<class match-class-name>), but is given: "
					+ iterationVariableType.toString());
			return false;
		}

		if(matchesContainerMatchType != null && iterationVariableDefinedMatchType != null) {
			reportError("for matches loop has an iteration variable of type match<rule-name> but a matches container of value type match<class match-class-name>): "
					+ iterationVariableDefinedMatchType.toString() + " vs. "
					+ matchesContainerMatchType.toString());
			return false;
		}
		if(matchesContainerDefinedMatchType != null && iterationVariableMatchType != null) {
			reportError("for matches loop has an iteration variable of type match<class match-class-name> but a matches container of value type match<rule-name>): "
					+ iterationVariableMatchType.toString() + " vs. "
					+ matchesContainerDefinedMatchType.toString());
			return false;
		}

		if(matchesContainerMatchType != null && iterationVariableMatchType != null) {
			if(!iterationVariableMatchType.isEqual(matchesContainerMatchType)) {
				reportError("The iteration variable of the for matches loop iterates a different match type than the matches container (defined by the rule referenced by the filter function): "
						+ iterationVariableMatchType.toString() + " vs. "
						+ matchesContainerMatchType.toString());
				return false;
			}
		} else /*if(matchesContainerDefinedMatchType!=null && iterationVariableDefinedMatchType!=null)*/ {
			if(!iterationVariableDefinedMatchType.isEqual(matchesContainerDefinedMatchType)) {
				reportError("The iteration variable of the for matches loop iterates a different match class type than the matches container (defined by the match class referenced by the match class filter function): "
						+ iterationVariableDefinedMatchType.toString() + " vs. "
						+ matchesContainerDefinedMatchType.toString());
				return false;
			}
		}

		return true;
	}

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
			may.addAccumulationStatement(accumulationStatement.checkIR(EvalStatement.class));
		}
		return may;
	}
}
