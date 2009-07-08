/*
 * GrGen: graph rewrite generator tool -- release GrGen.NET 2.5
 * Copyright (C) 2009 Universitaet Karlsruhe, Institut fuer Programmstrukturen und Datenorganisation, LS Goos
 * licensed under LGPL v3 (see LICENSE.txt included in the packaging of this file)
 */

/**
 * @author Moritz Kroll
 * @version $Id$
 */

package de.unika.ipd.grgen.ast;

import java.util.Collection;
import java.util.Vector;

import de.unika.ipd.grgen.ast.util.DeclarationResolver;
import de.unika.ipd.grgen.ir.Entity;
import de.unika.ipd.grgen.ir.GraphEntityExpression;
import de.unika.ipd.grgen.ir.IR;
import de.unika.ipd.grgen.ir.Qualification;
import de.unika.ipd.grgen.parser.Coords;

public class MemberAccessExprNode extends ExprNode
{
	static {
		setName(MemberAccessExprNode.class, "member access expression");
	}

	private ExprNode targetExpr; // resulting from primary expression, most often an IdentExprNode
	private IdentNode memberIdent;
	private MemberDeclNode member;

	public MemberAccessExprNode(Coords coords, ExprNode targetExpr, IdentNode memberIdent) {
		super(coords);
		this.targetExpr  = becomeParent(targetExpr);
		this.memberIdent = becomeParent(memberIdent);
	}

	@Override
	public Collection<? extends BaseNode> getChildren() {
		Vector<BaseNode> children = new Vector<BaseNode>();
		children.add(targetExpr);
		children.add(memberIdent);
		return children;
	}

	@Override
	public Collection<String> getChildrenNames() {
		Vector<String> childrenNames = new Vector<String>();
		childrenNames.add("targetExpr");
		childrenNames.add("memberIdent");
		return childrenNames;
	}

	private static final DeclarationResolver<MemberDeclNode> memberResolver
	        = new DeclarationResolver<MemberDeclNode>(MemberDeclNode.class);

	@Override
	protected boolean resolveLocal() {
		if(!targetExpr.resolve()) return false;

		TypeNode ownerType = targetExpr.getType();
		if(!(ownerType instanceof ScopeOwner)) {
			reportError("Left hand side of '.' has no members.");
			return false;
		}

		if(!(ownerType instanceof InheritanceTypeNode)) {
			reportError("Only member access of nodes and edges supported.");
			return false;
		}

		ScopeOwner o = (ScopeOwner) ownerType;
		o.fixupDefinition(memberIdent);
		member = memberResolver.resolve(memberIdent, this);

		return member != null;
	}

	@Override
	protected boolean checkLocal() {
		return true;
	}

	protected final ExprNode getTarget() {
		return targetExpr; // resulting from primary expression, most often an IdentExprNode
	}

	protected final MemberDeclNode getDecl() {
		assert isResolved();

		return member;
	}

	@Override
	public TypeNode getType() {
		return member.getDecl().getDeclType();
	}

	@Override
	protected IR constructIR() {
		return new Qualification(targetExpr.checkIR(GraphEntityExpression.class).getGraphEntity(), member.checkIR(Entity.class));
	}

	public static String getKindStr() {
		return "member";
	}

	public static String getUseStr() {
		return "member access";
	}
}
