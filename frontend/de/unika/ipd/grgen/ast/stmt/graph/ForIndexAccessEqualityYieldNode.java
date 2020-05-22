/*
 * GrGen: graph rewrite generator tool -- release GrGen.NET 5.0
 * Copyright (C) 2003-2020 Universitaet Karlsruhe, Institut fuer Programmstrukturen und Datenorganisation, LS Goos; and free programmers
 * licensed under LGPL v3 (see LICENSE.txt included in the packaging of this file)
 * www.grgen.net
 */

/**
 * @author Edgar Jakumeit
 */
package de.unika.ipd.grgen.ast.stmt.graph;

import java.util.Collection;
import java.util.Vector;

import de.unika.ipd.grgen.ast.*;
import de.unika.ipd.grgen.ast.decl.DeclNode;
import de.unika.ipd.grgen.ast.expr.ExprNode;
import de.unika.ipd.grgen.ast.model.decl.AttributeIndexDeclNode;
import de.unika.ipd.grgen.ast.model.decl.IncidenceCountIndexDeclNode;
import de.unika.ipd.grgen.ast.model.decl.IndexDeclNode;
import de.unika.ipd.grgen.ast.pattern.PatternGraphNode;
import de.unika.ipd.grgen.ast.stmt.EvalStatementNode;
import de.unika.ipd.grgen.ast.type.TypeNode;
import de.unika.ipd.grgen.ast.type.basic.IntTypeNode;
import de.unika.ipd.grgen.ast.util.DeclarationResolver;
import de.unika.ipd.grgen.ir.IR;
import de.unika.ipd.grgen.ir.expr.Expression;
import de.unika.ipd.grgen.ir.model.Index;
import de.unika.ipd.grgen.ir.pattern.IndexAccessEquality;
import de.unika.ipd.grgen.ir.pattern.Variable;
import de.unika.ipd.grgen.ir.stmt.EvalStatement;
import de.unika.ipd.grgen.ir.stmt.graph.ForIndexAccessEquality;
import de.unika.ipd.grgen.parser.Coords;

public class ForIndexAccessEqualityYieldNode extends ForIndexAccessNode
{
	static {
		setName(ForIndexAccessEqualityYieldNode.class, "for index access equality yield loop");
	}

	private ExprNode expr;

	public ForIndexAccessEqualityYieldNode(Coords coords, BaseNode iterationVariable, int context,
			IdentNode index, ExprNode expr, PatternGraphNode directlyNestingLHSGraph,
			CollectNode<EvalStatementNode> loopedStatements)
	{
		super(coords, iterationVariable, context, index, directlyNestingLHSGraph, loopedStatements);
		this.expr = expr;
		becomeParent(this.expr);
	}

	/** returns children of this node */
	@Override
	public Collection<BaseNode> getChildren()
	{
		Vector<BaseNode> children = new Vector<BaseNode>();
		children.add(getValidVersion(iterationVariableUnresolved, iterationVariable));
		children.add(getValidVersion(indexUnresolved, index));
		children.add(expr);
		children.add(statements);
		return children;
	}

	/** returns names of the children, same order as in getChildren */
	@Override
	public Collection<String> getChildrenNames()
	{
		Vector<String> childrenNames = new Vector<String>();
		childrenNames.add("iterVar");
		childrenNames.add("index");
		childrenNames.add("expression");
		childrenNames.add("loopedStatements");
		return childrenNames;
	}

	private static DeclarationResolver<IndexDeclNode> indexResolver =
			new DeclarationResolver<IndexDeclNode>(IndexDeclNode.class);

	/** @see de.unika.ipd.grgen.ast.BaseNode#resolveLocal() */
	@Override
	protected boolean resolveLocal()
	{
		boolean successfullyResolved = true;

		if(!resolveIterationVariable("index access equality"))
			successfullyResolved = false;

		index = indexResolver.resolve(indexUnresolved, this);
		successfullyResolved &= index != null;
		successfullyResolved &= expr.resolve();
		return successfullyResolved;
	}

	/** @see de.unika.ipd.grgen.ast.BaseNode#checkLocal() */
	@Override
	protected boolean checkLocal()
	{
		if(!checkIterationVariable("index access equality"))
			return false;

		boolean res = true;
		AttributeIndexDeclNode attributeIndex = index instanceof AttributeIndexDeclNode ? (AttributeIndexDeclNode)index : null;
		IncidenceCountIndexDeclNode incidenceCountIndex = index instanceof IncidenceCountIndexDeclNode
				? (IncidenceCountIndexDeclNode)index
				: null;
		TypeNode expectedIndexAccessType = attributeIndex != null
				? attributeIndex.member.getDeclType()
				: IntTypeNode.intType;
		TypeNode indexAccessType = expr.getType();
		if(!indexAccessType.isCompatibleTo(expectedIndexAccessType)) {
			String expTypeName = expectedIndexAccessType.getTypeName();
			String typeName = indexAccessType.getTypeName();
			reportError("Cannot convert type used in accessing index from \"" + typeName + "\" to \"" + expTypeName
					+ "\" in index access loop");
			return false;
		}
		TypeNode expectedEntityType = iterationVariable.getDeclType();
		TypeNode entityType = attributeIndex != null ? attributeIndex.type : incidenceCountIndex.getType();
		if(!entityType.isCompatibleTo(expectedEntityType) && !expectedEntityType.isCompatibleTo(entityType)) {
			String expTypeName = expectedEntityType.getTypeName();
			String typeName = entityType.getTypeName();
			reportError("Cannot convert index type from \"" + typeName + "\" to type \"" + expTypeName
					+ "\" in index access loop");
			return false;
		}
		return res;
	}

	public boolean checkStatementLocal(boolean isLHS, DeclNode root, EvalStatementNode enclosingLoop)
	{
		return true;
	}

	/** @see de.unika.ipd.grgen.ast.BaseNode#constructIR() */
	@Override
	protected IR constructIR()
	{
		ForIndexAccessEquality fiae = new ForIndexAccessEquality(iterationVariable.checkIR(Variable.class),
				new IndexAccessEquality(index.checkIR(Index.class), expr.checkIR(Expression.class)));
		for(EvalStatementNode accumulationStatement : statements.getChildren()) {
			fiae.addLoopedStatement(accumulationStatement.checkIR(EvalStatement.class));
		}
		return fiae;
	}
}
