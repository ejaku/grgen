/*
 * GrGen: graph rewrite generator tool -- release GrGen.NET 5.0
 * Copyright (C) 2003-2020 Universitaet Karlsruhe, Institut fuer Programmstrukturen und Datenorganisation, LS Goos; and free programmers
 * licensed under LGPL v3 (see LICENSE.txt included in the packaging of this file)
 * www.grgen.net
 */

/**
 * @author Edgar Jakumeit
 */
package de.unika.ipd.grgen.ast;

import java.util.Collection;
import java.util.Vector;

import de.unika.ipd.grgen.ast.expr.ExprNode;
import de.unika.ipd.grgen.ast.typedecl.StringTypeNode;
import de.unika.ipd.grgen.ir.Edge;
import de.unika.ipd.grgen.ir.IR;
import de.unika.ipd.grgen.ir.NameLookup;
import de.unika.ipd.grgen.ir.expr.Expression;

public class MatchEdgeByNameLookupNode extends EdgeDeclNode
{
	static {
		setName(MatchEdgeByNameLookupNode.class, "match edge by name lookup decl");
	}

	private ExprNode expr;

	public MatchEdgeByNameLookupNode(IdentNode id, BaseNode type, int context,
			ExprNode expr, PatternGraphNode directlyNestingLHSGraph)
	{
		super(id, type, false, context, TypeExprNode.getEmpty(), directlyNestingLHSGraph);
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
			reportError("Can't employ match edge by index on RHS");
			return false;
		}
		TypeNode expectedLookupType = StringTypeNode.stringType;
		TypeNode lookupType = expr.getType();
		if(!lookupType.isCompatibleTo(expectedLookupType)) {
			String expTypeName = expectedLookupType.getTypeName();
			String typeName = lookupType.getTypeName();
			ident.reportError("Cannot convert type used in accessing name map from \"" + typeName
					+ "\" to \"" + expTypeName + "\" in match edge by name lookup");
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

		edge.setNameMapAccess(new NameLookup(expr.checkIR(Expression.class)));
		return edge;
	}
}
