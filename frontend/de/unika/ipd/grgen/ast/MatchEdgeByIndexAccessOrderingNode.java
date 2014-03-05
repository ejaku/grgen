/*
 * GrGen: graph rewrite generator tool -- release GrGen.NET 4.2
 * Copyright (C) 2003-2014 Universitaet Karlsruhe, Institut fuer Programmstrukturen und Datenorganisation, LS Goos; and free programmers
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
import de.unika.ipd.grgen.ir.Edge;
import de.unika.ipd.grgen.ir.IR;
import de.unika.ipd.grgen.ir.Index;
import de.unika.ipd.grgen.ir.IndexAccessOrdering;
import de.unika.ipd.grgen.ir.exprevals.Expression;


public class MatchEdgeByIndexAccessOrderingNode extends EdgeDeclNode implements EdgeCharacter  {
	static {
		setName(MatchEdgeByIndexAccessOrderingNode.class, "match edge by index access ordering decl");
	}

	private boolean ascending;
	private IdentNode indexUnresolved;
	private IndexDeclNode index;
	private int comp;
	private ExprNode expr;
	private int comp2;
	private ExprNode expr2;

	public MatchEdgeByIndexAccessOrderingNode(IdentNode id, BaseNode type, int context,
			boolean ascending, IdentNode index, 
			int comp, ExprNode expr, 
			int comp2, ExprNode expr2, 
			PatternGraphNode directlyNestingLHSGraph) {
		super(id, type, false, context, TypeExprNode.getEmpty(), directlyNestingLHSGraph);
		this.ascending = ascending;
		this.indexUnresolved = index;
		becomeParent(this.indexUnresolved);
		this.comp = comp;
		this.expr = expr;
		becomeParent(this.expr);
		this.comp2 = comp2;
		this.expr2 = expr2;
		becomeParent(this.expr);
	}

	/** returns children of this node */
	@Override
	public Collection<BaseNode> getChildren() {
		Vector<BaseNode> children = new Vector<BaseNode>();
		children.add(ident);
		children.add(getValidVersion(typeUnresolved, typeEdgeDecl, typeTypeDecl));
		children.add(constraints);
		children.add(getValidVersion(indexUnresolved, index));
		if(expr!=null)
			children.add(expr);
		if(expr2!=null)
			children.add(expr2);
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
		if(expr!=null)
			childrenNames.add("expression");
		if(expr2!=null)
			childrenNames.add("expression2");
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
		if(expr!=null)
			successfullyResolved &= expr.resolve();
		if(expr2!=null)
			successfullyResolved &= expr2.resolve();
		return successfullyResolved;
	}

	/** @see de.unika.ipd.grgen.ast.BaseNode#checkLocal() */
	@Override
	protected boolean checkLocal() {
		boolean res = super.checkLocal();
		if((context&CONTEXT_LHS_OR_RHS)==CONTEXT_RHS) {
			reportError("Can't employ match edge by index on RHS");
			return false;
		}
		AttributeIndexDeclNode attributeIndex = index instanceof AttributeIndexDeclNode ? (AttributeIndexDeclNode)index : null;
		IncidenceIndexDeclNode incidenceIndex = index instanceof IncidenceIndexDeclNode ? (IncidenceIndexDeclNode)index : null;
		if(expr!=null) {
			TypeNode expectedIndexAccessType = attributeIndex!=null ? attributeIndex.member.getDeclType() : IntTypeNode.intType;
			TypeNode indexAccessType = expr.getType();
			if(!indexAccessType.isCompatibleTo(expectedIndexAccessType)) {
				String expTypeName = expectedIndexAccessType instanceof DeclaredTypeNode ? ((DeclaredTypeNode)expectedIndexAccessType).getIdentNode().toString() : expectedIndexAccessType.toString();
				String typeName = indexAccessType instanceof DeclaredTypeNode ? ((DeclaredTypeNode)indexAccessType).getIdentNode().toString() : indexAccessType.toString();
				ident.reportError("Cannot convert type used in accessing index from \""
						+ typeName + "\" to \"" + expTypeName + "\" in match edge by index access");
				return false;
			}
			if(expr2!=null) {
				TypeNode indexAccessType2 = expr2.getType();
				if(!indexAccessType2.isCompatibleTo(expectedIndexAccessType)) {
					String expTypeName = expectedIndexAccessType instanceof DeclaredTypeNode ? ((DeclaredTypeNode)expectedIndexAccessType).getIdentNode().toString() : expectedIndexAccessType.toString();
					String typeName = indexAccessType2 instanceof DeclaredTypeNode ? ((DeclaredTypeNode)indexAccessType2).getIdentNode().toString() : indexAccessType2.toString();
					ident.reportError("Cannot convert type used in accessing index from \""
							+ typeName + "\" to \"" + expTypeName + "\" in match edge by index access");
					return false;
				}
			}
		}
		TypeNode expectedEntityType = getDeclType();
		TypeNode entityType = attributeIndex!=null ? attributeIndex.type : incidenceIndex.getType();
		if(!entityType.isCompatibleTo(expectedEntityType) && !expectedEntityType.isCompatibleTo(entityType)) {
			String expTypeName = expectedEntityType instanceof DeclaredTypeNode ? ((DeclaredTypeNode)expectedEntityType).getIdentNode().toString() : expectedEntityType.toString();
			String typeName = entityType instanceof DeclaredTypeNode ? ((DeclaredTypeNode)entityType).getIdentNode().toString() : entityType.toString();
			ident.reportError("Cannot convert index type from \""
					+ typeName + "\" to pattern element type \"" + expTypeName + "\" in match edge by index access");
			return false;
		}
		if(comp==OperatorSignature.LT || comp==OperatorSignature.LE) {
			if(expr2!=null && (comp2==OperatorSignature.LT || comp2==OperatorSignature.LE)) {
				reportError("Match edge by index does not support two lower bounds");
				return false;
			}
		}
		if(comp==OperatorSignature.GT || comp==OperatorSignature.GE) {
			if(expr2!=null && (comp2==OperatorSignature.GT || comp2==OperatorSignature.GE)) {
				reportError("Match edge by index does not support two upper bounds");
				return false;
			}
		}
		return res;
	}

	/** @see de.unika.ipd.grgen.ast.BaseNode#constructIR() */
	@Override
	protected IR constructIR() {
		Edge edge = (Edge)super.constructIR();
		if (isIRAlreadySet()) { // break endless recursion in case of cycle in usage
			return getIR();
		} else{
			setIR(edge);
		}
		edge.setIndex(new IndexAccessOrdering(index.checkIR(Index.class), ascending, 
				comp, expr!=null ? expr.checkIR(Expression.class) : null, 
				comp2, expr2!=null ? expr2.checkIR(Expression.class) : null));
		return edge;
	}
}
