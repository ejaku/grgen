/*
 * GrGen: graph rewrite generator tool -- release GrGen.NET 5.0
 * Copyright (C) 2003-2020 Universitaet Karlsruhe, Institut fuer Programmstrukturen und Datenorganisation, LS Goos; and free programmers
 * licensed under LGPL v3 (see LICENSE.txt included in the packaging of this file)
 * www.grgen.net
 */

/**
 * @author Moritz Kroll
 */

package de.unika.ipd.grgen.ast.expr;

import java.util.Collection;
import java.util.Vector;

import de.unika.ipd.grgen.ast.*;
import de.unika.ipd.grgen.ast.decl.DeclNode;
import de.unika.ipd.grgen.ast.decl.TypeDeclNode;
import de.unika.ipd.grgen.ast.model.decl.MemberDeclNode;
import de.unika.ipd.grgen.ast.type.DefinedMatchTypeNode;
import de.unika.ipd.grgen.ast.type.MatchTypeNode;
import de.unika.ipd.grgen.ast.type.TypeNode;
import de.unika.ipd.grgen.ast.type.basic.BasicTypeNode;
import de.unika.ipd.grgen.ast.type.basic.UntypedExecVarTypeNode;
import de.unika.ipd.grgen.ast.util.Resolver;
import de.unika.ipd.grgen.ir.Entity;
import de.unika.ipd.grgen.ir.IR;
import de.unika.ipd.grgen.ir.expr.Expression;
import de.unika.ipd.grgen.ir.expr.GraphEntityExpression;
import de.unika.ipd.grgen.ir.expr.MatchAccess;
import de.unika.ipd.grgen.ir.expr.Qualification;
import de.unika.ipd.grgen.ir.expr.VariableExpression;
import de.unika.ipd.grgen.parser.Coords;

public class MemberAccessExprNode extends ExprNode
{
	static {
		setName(MemberAccessExprNode.class, "member access expression");
	}

	private ExprNode targetExpr; // resulting from primary expression, most often an IdentExprNode
	private IdentNode memberIdent;
	private DeclNode member;

	public MemberAccessExprNode(Coords coords, ExprNode targetExpr, IdentNode memberIdent)
	{
		super(coords);
		this.targetExpr = becomeParent(targetExpr);
		this.memberIdent = becomeParent(memberIdent);
	}

	@Override
	public Collection<? extends BaseNode> getChildren()
	{
		Vector<BaseNode> children = new Vector<BaseNode>();
		if(isResolved() && resolutionResult()) {
			if(targetExpr.getType() instanceof MatchTypeNode || targetExpr.getType() instanceof DefinedMatchTypeNode) {
				return children; // behave like a nop in case we're a match access
			}
		}
		children.add(targetExpr);
		children.add(memberIdent);
		return children;
	}

	@Override
	public Collection<String> getChildrenNames()
	{
		Vector<String> childrenNames = new Vector<String>();
		childrenNames.add("targetExpr");
		childrenNames.add("memberIdent");
		return childrenNames;
	}

	@Override
	protected boolean resolveLocal()
	{
		if(!targetExpr.resolve()) {
			return false;
		}

		if(targetExpr instanceof IdentExprNode) {
			IdentExprNode identExpr = (IdentExprNode)targetExpr;
			if(identExpr.decl instanceof TypeDeclNode) {
				TypeDeclNode typeNode = (TypeDeclNode)identExpr.decl;
				reportError("Member access requires an entity, is given type " + typeNode.getIdentNode());
			}
		}
		if(targetExpr instanceof TypeofNode) {
			TypeofNode typeofExpr = (TypeofNode)targetExpr;
			reportError("Member access requires an entity, is given type from typeof("
					+ typeofExpr.getEntity().getDecl().getIdentNode() + ")");
		}

		TypeNode ownerType = targetExpr.getType();

		if(ownerType instanceof UntypedExecVarTypeNode) {
			member = new MemberDeclNode(memberIdent, BasicTypeNode.untypedType, false);
			member.resolve();
			setCheckVisited();
			return true;
		}

		member = Resolver.resolveMember(ownerType, memberIdent);

		return member != null;
	}

	@Override
	protected boolean checkLocal()
	{
		return true;
	}

	public final ExprNode getTarget()
	{
		return targetExpr; // resulting from primary expression, most often an IdentExprNode
	}

	public final MemberDeclNode getDecl()
	{
		assert isResolved();

		return member instanceof MemberDeclNode ? (MemberDeclNode)member : null;
	}

	@Override
	public TypeNode getType()
	{
		TypeNode declType = null;
		if(targetExpr.getType() instanceof MatchTypeNode || targetExpr.getType() instanceof DefinedMatchTypeNode) {
			declType = member.getDeclType();
		} else {
			declType = member.getDecl().getDeclType(); // untyped exec var type in case owner is an untyped exec var
		}
		return declType;
	}

	@Override
	protected IR constructIR()
	{
		if(targetExpr.getType() instanceof MatchTypeNode || targetExpr.getType() instanceof DefinedMatchTypeNode) {
			return new MatchAccess(targetExpr.checkIR(Expression.class), member.checkIR(Entity.class));
		}

		if(targetExpr.getIR() instanceof VariableExpression) {
			return new Qualification(targetExpr.checkIR(VariableExpression.class).getVariable(),
					member.checkIR(Entity.class));
		} else if(targetExpr.getIR() instanceof GraphEntityExpression) {
			return new Qualification(targetExpr.checkIR(GraphEntityExpression.class).getGraphEntity(),
					member.checkIR(Entity.class));
		} else {
			return new Qualification(targetExpr.checkIR(Expression.class), // normally a Cast (or an untyped exec var)
					member.checkIR(Entity.class));
		}
	}

	public static String getKindStr()
	{
		return "member";
	}

	public static String getUseStr()
	{
		return "member access";
	}
}
