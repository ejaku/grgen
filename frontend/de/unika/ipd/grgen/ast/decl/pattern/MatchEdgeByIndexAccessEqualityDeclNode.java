/*
 * GrGen: graph rewrite generator tool -- release GrGen.NET 7.1
 * Copyright (C) 2003-2025 Universitaet Karlsruhe, Institut fuer Programmstrukturen und Datenorganisation, LS Goos; and free programmers
 * licensed under LGPL v3 (see LICENSE.txt included in the packaging of this file)
 * www.grgen.net
 */

/**
 * @author Edgar Jakumeit
 */
package de.unika.ipd.grgen.ast.decl.pattern;

import java.util.Collection;
import java.util.Vector;

import de.unika.ipd.grgen.ast.BaseNode;
import de.unika.ipd.grgen.ast.IdentNode;
import de.unika.ipd.grgen.ast.expr.ExprNode;
import de.unika.ipd.grgen.ast.model.type.InheritanceTypeNode;
import de.unika.ipd.grgen.ast.pattern.PatternGraphLhsNode;
import de.unika.ipd.grgen.ast.type.TypeNode;
import de.unika.ipd.grgen.ir.IR;
import de.unika.ipd.grgen.ir.expr.Expression;
import de.unika.ipd.grgen.ir.model.Index;
import de.unika.ipd.grgen.ir.pattern.Edge;
import de.unika.ipd.grgen.ir.pattern.IndexAccessEquality;

public class MatchEdgeByIndexAccessEqualityDeclNode extends MatchEdgeByIndexDeclNode
{
	static {
		setName(MatchEdgeByIndexAccessEqualityDeclNode.class, "match edge by index access equality decl");
	}

	private ExprNode expr;

	public MatchEdgeByIndexAccessEqualityDeclNode(IdentNode id, BaseNode type, int context,
			IdentNode index, ExprNode expr, PatternGraphLhsNode directlyNestingLHSGraph)
	{
		super(id, type, context, index, directlyNestingLHSGraph);
		this.expr = expr;
		becomeParent(this.expr);
	}

	/** returns children of this node */
	@Override
	public Collection<BaseNode> getChildren()
	{
		Vector<BaseNode> children = new Vector<BaseNode>();
		children.add(ident);
		children.add(getValidVersion(typeUnresolved, typeEdgeDecl, typeTypeDecl));
		children.add(constraints);
		children.add(getValidVersion(indexUnresolved, index));
		children.add(expr);
		return children;
	}

	/** returns names of the children, same order as in getChildren */
	@Override
	public Collection<String> getChildrenNames()
	{
		Vector<String> childrenNames = new Vector<String>();
		childrenNames.add("ident");
		childrenNames.add("type");
		childrenNames.add("constraints");
		childrenNames.add("index");
		childrenNames.add("expression");
		return childrenNames;
	}

	/** @see de.unika.ipd.grgen.ast.BaseNode#resolveLocal() */
	@Override
	protected boolean resolveLocal()
	{
		boolean successfullyResolved = super.resolveLocal();
		successfullyResolved &= expr.resolve();
		return successfullyResolved;
	}

	/** @see de.unika.ipd.grgen.ast.BaseNode#checkLocal() */
	@Override
	protected boolean checkLocal()
	{
		boolean res = super.checkLocal();
		TypeNode expectedIndexAccessType = index.getExpectedAccessType();
		TypeNode indexAccessType = expr.getType();
		if(!indexAccessType.isCompatibleTo(expectedIndexAccessType)) {
			String expTypeName = expectedIndexAccessType.getTypeName();
			String typeName = indexAccessType.getTypeName();
			ident.reportError("Cannot convert type used in accessing index from " + typeName
					+ " to the expected " + expTypeName
					+ " (in match edge" + emptyWhenAnonymousPostfix(" ") + " by index access of " + index.toStringWithDeclarationCoords() + ").");
			return false;
		}
		TypeNode expectedEntityType = getDeclType();
		InheritanceTypeNode entityType = index.getType();
		if(!entityType.isCompatibleTo(expectedEntityType) && !expectedEntityType.isCompatibleTo(entityType)) {
			String expTypeName = expectedEntityType.toStringWithDeclarationCoords();
			String typeName = entityType.toStringWithDeclarationCoords();
			ident.reportError("Cannot convert index type from " + typeName
					+ " to the expected pattern element type " + expTypeName
					+ " (in match edge" + emptyWhenAnonymousPostfix(" ") + " by index access of " + index.toStringWithDeclarationCoords() + ").");
			return false;
		}
		return res;
	}

	/** @see de.unika.ipd.grgen.ast.BaseNode#constructIR() */
	@Override
	protected IR constructIR()
	{
		if(isIRAlreadySet()) { // break endless recursion in case of cycle in usage
			return getIR();
		}

		Edge edge = (Edge)super.constructIR();

		setIR(edge);

		expr = expr.evaluate();
		edge.setIndex(new IndexAccessEquality(index.checkIR(Index.class), expr.checkIR(Expression.class)));
		return edge;
	}
}
