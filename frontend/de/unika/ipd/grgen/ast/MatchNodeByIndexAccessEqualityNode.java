/*
 * GrGen: graph rewrite generator tool -- release GrGen.NET 4.5
 * Copyright (C) 2003-2017 Universitaet Karlsruhe, Institut fuer Programmstrukturen und Datenorganisation, LS Goos; and free programmers
 * licensed under LGPL v3 (see LICENSE.txt included in the packaging of this file)
 * www.grgen.net
 */

/**
 * @author Edgar Jakumeit
 */
package de.unika.ipd.grgen.ast;

import java.util.Collection;
import java.util.Vector;

import de.unika.ipd.grgen.ast.exprevals.*;
import de.unika.ipd.grgen.ast.util.DeclarationResolver;
import de.unika.ipd.grgen.ir.IR;
import de.unika.ipd.grgen.ir.Index;
import de.unika.ipd.grgen.ir.IndexAccessEquality;
import de.unika.ipd.grgen.ir.Node;
import de.unika.ipd.grgen.ir.exprevals.Expression;


public class MatchNodeByIndexAccessEqualityNode extends NodeDeclNode implements NodeCharacter  {
	static {
		setName(MatchNodeByIndexAccessEqualityNode.class, "match node by index access equality decl");
	}

	private IdentNode indexUnresolved;
	private IndexDeclNode index;
	private ExprNode expr;

	public MatchNodeByIndexAccessEqualityNode(IdentNode id, BaseNode type, int context,
			IdentNode index, ExprNode expr, PatternGraphNode directlyNestingLHSGraph) {
		super(id, type, false, context, TypeExprNode.getEmpty(), directlyNestingLHSGraph);
		this.indexUnresolved = index;
		becomeParent(this.indexUnresolved);
		this.expr = expr;
		becomeParent(this.expr);
	}

	/** returns children of this node */
	@Override
	public Collection<BaseNode> getChildren() {
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
	public Collection<String> getChildrenNames() {
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
	protected boolean resolveLocal() {
		boolean successfullyResolved = super.resolveLocal();
		index = indexResolver.resolve(indexUnresolved, this);
		successfullyResolved &= index!=null;
		successfullyResolved &= expr.resolve();
		return successfullyResolved;
	}

	/** @see de.unika.ipd.grgen.ast.BaseNode#checkLocal() */
	@Override
	protected boolean checkLocal() {
		boolean res = super.checkLocal();
		if((context&CONTEXT_LHS_OR_RHS)==CONTEXT_RHS) {
			reportError("Can't employ match node by index on RHS");
			return false;
		}
		AttributeIndexDeclNode attributeIndex = index instanceof AttributeIndexDeclNode ? (AttributeIndexDeclNode)index : null;
		IncidenceCountIndexDeclNode incidenceCountIndex = index instanceof IncidenceCountIndexDeclNode ? (IncidenceCountIndexDeclNode)index : null;
		TypeNode expectedIndexAccessType = attributeIndex!=null ? attributeIndex.member.getDeclType() : IntTypeNode.intType;
		TypeNode indexAccessType = expr.getType();
		if(!indexAccessType.isCompatibleTo(expectedIndexAccessType)) {
			String expTypeName = expectedIndexAccessType instanceof DeclaredTypeNode ? ((DeclaredTypeNode)expectedIndexAccessType).getIdentNode().toString() : expectedIndexAccessType.toString();
			String typeName = indexAccessType instanceof DeclaredTypeNode ? ((DeclaredTypeNode)indexAccessType).getIdentNode().toString() : indexAccessType.toString();
			ident.reportError("Cannot convert type used in accessing index from \""
					+ typeName + "\" to \"" + expTypeName + "\" in match node by index access");
			return false;
		}
		TypeNode expectedEntityType = getDeclType();
		TypeNode entityType = attributeIndex!=null ? attributeIndex.type : incidenceCountIndex.getType();
		if(!entityType.isCompatibleTo(expectedEntityType) && !expectedEntityType.isCompatibleTo(entityType)) {
			String expTypeName = expectedEntityType instanceof DeclaredTypeNode ? ((DeclaredTypeNode)expectedEntityType).getIdentNode().toString() : expectedEntityType.toString();
			String typeName = entityType instanceof DeclaredTypeNode ? ((DeclaredTypeNode)entityType).getIdentNode().toString() : entityType.toString();
			ident.reportError("Cannot convert index type from \""
					+ typeName + "\" to pattern element type \"" + expTypeName + "\" in match node by index access");
			return false;
		}
		return res;
	}

	/** @see de.unika.ipd.grgen.ast.BaseNode#constructIR() */
	@Override
	protected IR constructIR() {
		Node node = (Node)super.constructIR();
		if (isIRAlreadySet()) { // break endless recursion in case of cycle in usage
			return getIR();
		} else{
			setIR(node);
		}
		node.setIndex(new IndexAccessEquality(index.checkIR(Index.class), 
				expr.checkIR(Expression.class)));
		return node;
	}
}
