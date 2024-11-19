/*
 * GrGen: graph rewrite generator tool -- release GrGen.NET 7.0
 * Copyright (C) 2003-2024 Universitaet Karlsruhe, Institut fuer Programmstrukturen und Datenorganisation, LS Goos; and free programmers
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
import de.unika.ipd.grgen.ast.UnitNode;
import de.unika.ipd.grgen.ast.expr.ExprNode;
import de.unika.ipd.grgen.ast.pattern.PatternGraphLhsNode;
import de.unika.ipd.grgen.ast.type.TypeExprNode;
import de.unika.ipd.grgen.ast.type.TypeNode;
import de.unika.ipd.grgen.ast.type.basic.IntTypeNode;
import de.unika.ipd.grgen.ir.IR;
import de.unika.ipd.grgen.ir.expr.Expression;
import de.unika.ipd.grgen.ir.pattern.Edge;
import de.unika.ipd.grgen.ir.pattern.UniqueLookup;

public class MatchEdgeByUniqueLookupDeclNode extends EdgeDeclNode
{
	static {
		setName(MatchEdgeByUniqueLookupDeclNode.class, "match edge by unique lookup decl");
	}

	private ExprNode expr;

	public MatchEdgeByUniqueLookupDeclNode(IdentNode id, BaseNode type, int context,
			ExprNode expr, PatternGraphLhsNode directlyNestingLHSGraph)
	{
		super(id, type, CopyKind.None, context, TypeExprNode.getEmpty(), directlyNestingLHSGraph);
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
		if((context & CONTEXT_LHS_OR_RHS) == CONTEXT_RHS) {
			reportError("Cannot employ match edge by unique index lookup in the rewrite part"
					+ emptyWhenAnonymous(" (as it occurs in match edge " + getIdentNode() + ")") + ".");
			return false;
		}
		TypeNode expectedLookupType = IntTypeNode.intType;
		TypeNode lookupType = expr.getType();
		if(!lookupType.isCompatibleTo(expectedLookupType)) {
			String expTypeName = expectedLookupType.getTypeName();
			String typeName = lookupType.getTypeName();
			ident.reportError("Cannot convert type used in accessing unique index from " + typeName
					+ " to the expected " + expTypeName + " in match edge" + emptyWhenAnonymousPostfix(" ") + " by unique index lookup.");
			return false;
		}
		if(!UnitNode.getRoot().getModel().IsUniqueIndexDefined()) {
			reportError("The match edge by unique index lookup expects a model with a unique index, but the required index unique; declaration is missing in the model specification.");
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
		edge.setUniqueIndexAccess(new UniqueLookup(expr.checkIR(Expression.class)));
		return edge;
	}
}
