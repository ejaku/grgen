/*
 * GrGen: graph rewrite generator tool -- release GrGen.NET 7.2
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
import de.unika.ipd.grgen.ast.decl.executable.OperatorDeclNode;
import de.unika.ipd.grgen.ast.expr.ExprNode;
import de.unika.ipd.grgen.ast.model.decl.IndexDeclNode;
import de.unika.ipd.grgen.ast.model.type.InheritanceTypeNode;
import de.unika.ipd.grgen.ast.type.TypeNode;
import de.unika.ipd.grgen.ast.util.DeclarationResolver;
import de.unika.ipd.grgen.ir.expr.Expression;
import de.unika.ipd.grgen.ir.model.Index;
import de.unika.ipd.grgen.ir.pattern.IndexAccessOrdering;

public class MatchByIndexAccessOrderingPartNode extends BaseNode
{
	static {
		setName(MatchByIndexAccessOrderingPartNode.class, "match by index access ordering part");
	}

	private IdentNode indexUnresolved;
	public IndexDeclNode index;

	private OperatorDeclNode.Operator comp;
	private ExprNode expr;
	private OperatorDeclNode.Operator comp2;
	private ExprNode expr2;

	ConstraintDeclNode wholeNodeDecl;

	public MatchByIndexAccessOrderingPartNode(IdentNode index,
			OperatorDeclNode.Operator comp, ExprNode expr,
			OperatorDeclNode.Operator comp2, ExprNode expr2,
			ConstraintDeclNode wholeNodeDecl)
	{
		super(index.getCoords());
		this.indexUnresolved = index;
		becomeParent(this.indexUnresolved);
		this.comp = comp;
		this.expr = expr;
		becomeParent(this.expr);
		this.comp2 = comp2;
		this.expr2 = expr2;
		becomeParent(this.expr);
		this.wholeNodeDecl = wholeNodeDecl;
	}

	/** returns children of this node */
	@Override
	public Collection<BaseNode> getChildren()
	{
		Vector<BaseNode> children = new Vector<BaseNode>();
		children.add(getValidVersion(indexUnresolved, index));
		if(expr != null)
			children.add(expr);
		if(expr2 != null)
			children.add(expr2);
		return children;
	}

	/** returns names of the children, same order as in getChildren */
	@Override
	public Collection<String> getChildrenNames()
	{
		Vector<String> childrenNames = new Vector<String>();
		childrenNames.add("index");
		if(expr != null)
			childrenNames.add("expression");
		if(expr2 != null)
			childrenNames.add("expression2");
		return childrenNames;
	}

	private static DeclarationResolver<IndexDeclNode> indexResolver =
			new DeclarationResolver<IndexDeclNode>(IndexDeclNode.class);

	/** @see de.unika.ipd.grgen.ast.BaseNode#resolveLocal() */
	@Override
	protected boolean resolveLocal()
	{
		boolean successfullyResolved = true;
		index = indexResolver.resolve(indexUnresolved, this);
		successfullyResolved &= index != null;
		if(expr != null)
			successfullyResolved &= expr.resolve();
		if(expr2 != null)
			successfullyResolved &= expr2.resolve();
		return successfullyResolved;
	}

	/** @see de.unika.ipd.grgen.ast.BaseNode#checkLocal() */
	@Override
	protected boolean checkLocal()
	{
		boolean res = true;
		String kindStr = wholeNodeDecl instanceof MatchNodeByIndexAccessMultipleDeclNode ? "node" : "edge";
		if(expr != null) {
			TypeNode expectedIndexAccessType = index.getExpectedAccessType();
			TypeNode indexAccessType = expr.getType();
			if(!indexAccessType.isCompatibleTo(expectedIndexAccessType)) {
				String expTypeName = expectedIndexAccessType.getTypeName();
				String typeName = indexAccessType.getTypeName();
				expr.reportError("Cannot convert type used in accessing index from " + typeName
						+ " to the expected " + expTypeName
						+ " (in match " + kindStr + wholeNodeDecl.emptyWhenAnonymousPostfix(" ") + " by index access of " + index.toStringWithDeclarationCoords() + ").");
				res = false;
			}
			if(expr2 != null) { // TODO: distinguish lower and upper bound
				TypeNode indexAccessType2 = expr2.getType();
				if(!indexAccessType2.isCompatibleTo(expectedIndexAccessType)) {
					String expTypeName = expectedIndexAccessType.getTypeName();
					String typeName = indexAccessType2.getTypeName();
					expr2.reportError("Cannot convert type used in accessing index from " + typeName
							+ " to the expected " + expTypeName
							+ " (in match " + kindStr + wholeNodeDecl.emptyWhenAnonymousPostfix(" ") + " by index access of " + index.toStringWithDeclarationCoords() + ").");
					res = false;
				}
			}
		}
		TypeNode expectedEntityType = wholeNodeDecl.getDeclType();
		InheritanceTypeNode entityType = index.getType();
		if(!entityType.isCompatibleTo(expectedEntityType) && !expectedEntityType.isCompatibleTo(entityType)) {
			String expTypeName = expectedEntityType.toStringWithDeclarationCoords();
			String typeName = entityType.toStringWithDeclarationCoords();
			wholeNodeDecl.ident.reportError("Cannot convert index type from " + typeName
					+ " to the expected pattern element type " + expTypeName
					+ " (in match " + kindStr + wholeNodeDecl.emptyWhenAnonymousPostfix(" ") + " by index access of " + index.toStringWithDeclarationCoords() + ").");
			res = false;
		}
		if(comp == OperatorDeclNode.Operator.LT || comp == OperatorDeclNode.Operator.LE) {
			if(expr2 != null && (comp2 == OperatorDeclNode.Operator.LT || comp2 == OperatorDeclNode.Operator.LE)) {
				reportError("Two upper bounds are not supported"
						+ " (in match " + kindStr + wholeNodeDecl.emptyWhenAnonymousPostfix(" ") + " by index access of " + index.getIdentNode() + ").");
				res = false;
			}
		}
		if(comp == OperatorDeclNode.Operator.GT || comp == OperatorDeclNode.Operator.GE) {
			if(expr2 != null && (comp2 == OperatorDeclNode.Operator.GT || comp2 == OperatorDeclNode.Operator.GE)) {
				reportError("Two lower bounds are not supported"
						+ " (in match " + kindStr + wholeNodeDecl.emptyWhenAnonymousPostfix(" ") + " by index access of " + index.getIdentNode() + ").");
				res = false;
			}
		}
		return res;
	}

	protected IndexAccessOrdering constructIRPart()
	{
		if(expr != null)
			expr = expr.evaluate();
		if(expr2 != null)
			expr2 = expr2.evaluate();
		return new IndexAccessOrdering(index.checkIR(Index.class), true,
				comp, expr != null ? expr.checkIR(Expression.class) : null,
				comp2, expr2 != null ? expr2.checkIR(Expression.class) : null);
	}
}
