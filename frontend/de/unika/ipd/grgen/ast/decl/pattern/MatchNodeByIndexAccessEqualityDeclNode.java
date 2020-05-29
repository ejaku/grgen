/*
 * GrGen: graph rewrite generator tool -- release GrGen.NET 5.0
 * Copyright (C) 2003-2020 Universitaet Karlsruhe, Institut fuer Programmstrukturen und Datenorganisation, LS Goos; and free programmers
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
import de.unika.ipd.grgen.ast.model.decl.IndexDeclNode;
import de.unika.ipd.grgen.ast.model.type.InheritanceTypeNode;
import de.unika.ipd.grgen.ast.pattern.PatternGraphNode;
import de.unika.ipd.grgen.ast.type.TypeNode;
import de.unika.ipd.grgen.ast.util.DeclarationResolver;
import de.unika.ipd.grgen.ir.IR;
import de.unika.ipd.grgen.ir.expr.Expression;
import de.unika.ipd.grgen.ir.model.Index;
import de.unika.ipd.grgen.ir.pattern.IndexAccessEquality;
import de.unika.ipd.grgen.ir.pattern.Node;

public class MatchNodeByIndexAccessEqualityDeclNode extends MatchNodeByIndexDeclNode
{
	static {
		setName(MatchNodeByIndexAccessEqualityDeclNode.class, "match node by index access equality decl");
	}

	private ExprNode expr;

	public MatchNodeByIndexAccessEqualityDeclNode(IdentNode id, BaseNode type, int context,
			IdentNode index, ExprNode expr, PatternGraphNode directlyNestingLHSGraph)
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
		children.add(getValidVersion(typeUnresolved, typeNodeDecl, typeTypeDecl));
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

	private static DeclarationResolver<IndexDeclNode> indexResolver =
			new DeclarationResolver<IndexDeclNode>(IndexDeclNode.class);

	/** @see de.unika.ipd.grgen.ast.BaseNode#resolveLocal() */
	@Override
	protected boolean resolveLocal()
	{
		boolean successfullyResolved = super.resolveLocal();
		index = indexResolver.resolve(indexUnresolved, this);
		successfullyResolved &= index != null;
		successfullyResolved &= expr.resolve();
		return successfullyResolved;
	}

	/** @see de.unika.ipd.grgen.ast.BaseNode#checkLocal() */
	@Override
	protected boolean checkLocal()
	{
		boolean res = super.checkLocal();
		if((context & CONTEXT_LHS_OR_RHS) == CONTEXT_RHS) {
			reportError("Can't employ match node by index on RHS");
			return false;
		}
		TypeNode expectedIndexAccessType = index.getExpectedAccessType();
		TypeNode indexAccessType = expr.getType();
		if(!indexAccessType.isCompatibleTo(expectedIndexAccessType)) {
			String expTypeName = expectedIndexAccessType.getTypeName();
			String typeName = indexAccessType.getTypeName();
			ident.reportError("Cannot convert type used in accessing index from \"" + typeName
					+ "\" to \"" + expTypeName + "\" in match node by index access");
			return false;
		}
		TypeNode expectedEntityType = getDeclType();
		InheritanceTypeNode entityType = index.getType();
		if(!entityType.isCompatibleTo(expectedEntityType) && !expectedEntityType.isCompatibleTo(entityType)) {
			String expTypeName = expectedEntityType.getTypeName();
			String typeName = entityType.getTypeName();
			ident.reportError("Cannot convert index type from \"" + typeName
					+ "\" to pattern element type \"" + expTypeName + "\" in match node by index access");
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

		Node node = (Node)super.constructIR();

		setIR(node);

		node.setIndex(new IndexAccessEquality(index.checkIR(Index.class), expr.checkIR(Expression.class)));
		return node;
	}
}
