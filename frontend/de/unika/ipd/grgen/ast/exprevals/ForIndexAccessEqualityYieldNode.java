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
import de.unika.ipd.grgen.ir.IR;
import de.unika.ipd.grgen.ir.Index;
import de.unika.ipd.grgen.ir.IndexAccessEquality;
import de.unika.ipd.grgen.ir.Variable;
import de.unika.ipd.grgen.ir.exprevals.EvalStatement;
import de.unika.ipd.grgen.ir.exprevals.Expression;
import de.unika.ipd.grgen.ir.exprevals.ForIndexAccessEquality;
import de.unika.ipd.grgen.parser.Coords;


public class ForIndexAccessEqualityYieldNode extends EvalStatementNode  {
	static {
		setName(ForIndexAccessEqualityYieldNode.class, "for index access equality yield loop");
	}

	private BaseNode iterationVariableUnresolved;
	private VarDeclNode iterationVariable;
	private IdentNode indexUnresolved;
	private IndexDeclNode index;
	private ExprNode expr;
	private CollectNode<EvalStatementNode> loopedStatements;

	public ForIndexAccessEqualityYieldNode(Coords coords, BaseNode iterationVariable, int context,
			IdentNode index, ExprNode expr, PatternGraphNode directlyNestingLHSGraph,
			CollectNode<EvalStatementNode> loopedStatements) {
		super(coords);
		this.iterationVariableUnresolved = iterationVariable;
		becomeParent(this.iterationVariableUnresolved);
		this.indexUnresolved = index;
		becomeParent(this.indexUnresolved);
		this.expr = expr;
		becomeParent(this.expr);
		this.loopedStatements = loopedStatements;
		becomeParent(this.loopedStatements);
	}

	/** returns children of this node */
	@Override
	public Collection<BaseNode> getChildren() {
		Vector<BaseNode> children = new Vector<BaseNode>();
		children.add(getValidVersion(iterationVariableUnresolved, iterationVariable));
		children.add(getValidVersion(indexUnresolved, index));
		children.add(expr);
		children.add(loopedStatements);
		return children;
	}

	/** returns names of the children, same order as in getChildren */
	@Override
	public Collection<String> getChildrenNames() {
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
	protected boolean resolveLocal() {
		boolean successfullyResolved = true;
		
		if(iterationVariableUnresolved instanceof VarDeclNode) {
			iterationVariable = (VarDeclNode)iterationVariableUnresolved;
		} else {
			reportError("error in resolving iteration variable of for function loop.");
			successfullyResolved = false;
		}

		if(!iterationVariable.resolve())
			successfullyResolved = false;

		index = indexResolver.resolve(indexUnresolved, this);
		successfullyResolved &= index!=null;
		successfullyResolved &= expr.resolve();
		return successfullyResolved;
	}

	/** @see de.unika.ipd.grgen.ast.BaseNode#checkLocal() */
	@Override
	protected boolean checkLocal() {
		if(!(iterationVariable.getDeclType() instanceof NodeTypeNode)
			&& !(iterationVariable.getDeclType() instanceof EdgeTypeNode))
		{
			reportError("iteration variable of for function loop must be of node or edge type.");
			return false;
		}

		boolean res = true;
		AttributeIndexDeclNode attributeIndex = index instanceof AttributeIndexDeclNode ? (AttributeIndexDeclNode)index : null;
		IncidenceIndexDeclNode incidenceIndex = index instanceof IncidenceIndexDeclNode ? (IncidenceIndexDeclNode)index : null;
		TypeNode expectedIndexAccessType = attributeIndex!=null ? attributeIndex.member.getDeclType() : IntTypeNode.intType;
		TypeNode indexAccessType = expr.getType();
		if(!indexAccessType.isCompatibleTo(expectedIndexAccessType)) {
			String expTypeName = expectedIndexAccessType instanceof DeclaredTypeNode ? ((DeclaredTypeNode)expectedIndexAccessType).getIdentNode().toString() : expectedIndexAccessType.toString();
			String typeName = indexAccessType instanceof DeclaredTypeNode ? ((DeclaredTypeNode)indexAccessType).getIdentNode().toString() : indexAccessType.toString();
			reportError("Cannot convert type used in accessing index from \""
					+ typeName + "\" to \"" + expTypeName + "\" in index access loop");
			return false;
		}
		TypeNode expectedEntityType = iterationVariable.getDeclType();
		TypeNode entityType = attributeIndex!=null ? attributeIndex.type : incidenceIndex.getType();
		if(!entityType.isCompatibleTo(expectedEntityType) && !expectedEntityType.isCompatibleTo(entityType)) {
			String expTypeName = expectedEntityType instanceof DeclaredTypeNode ? ((DeclaredTypeNode)expectedEntityType).getIdentNode().toString() : expectedEntityType.toString();
			String typeName = entityType instanceof DeclaredTypeNode ? ((DeclaredTypeNode)entityType).getIdentNode().toString() : entityType.toString();
			reportError("Cannot convert index type from \""
					+ typeName + "\" to type \"" + expTypeName + "\" in index access loop");
			return false;
		}
		return res;
	}

	public boolean checkStatementLocal(boolean isLHS, DeclNode root, EvalStatementNode enclosingLoop) {
		return true;
	}

	/** @see de.unika.ipd.grgen.ast.BaseNode#constructIR() */
	@Override
	protected IR constructIR() {
		ForIndexAccessEquality fiae = new ForIndexAccessEquality(
				iterationVariable.checkIR(Variable.class),
				new IndexAccessEquality(
						index.checkIR(Index.class), expr.checkIR(Expression.class)
				)
			);
		for(EvalStatementNode accumulationStatement : loopedStatements.children) 	
			fiae.addLoopedStatement(accumulationStatement.checkIR(EvalStatement.class));
		return fiae;
	}
}
