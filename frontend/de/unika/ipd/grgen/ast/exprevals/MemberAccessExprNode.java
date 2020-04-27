/*
 * GrGen: graph rewrite generator tool -- release GrGen.NET 5.0
 * Copyright (C) 2003-2020 Universitaet Karlsruhe, Institut fuer Programmstrukturen und Datenorganisation, LS Goos; and free programmers
 * licensed under LGPL v3 (see LICENSE.txt included in the packaging of this file)
 * www.grgen.net
 */

/**
 * @author Moritz Kroll
 */

package de.unika.ipd.grgen.ast.exprevals;

import java.util.Collection;
import java.util.Vector;

import de.unika.ipd.grgen.ast.*;
import de.unika.ipd.grgen.ast.util.DeclarationResolver;
import de.unika.ipd.grgen.ir.Entity;
import de.unika.ipd.grgen.ir.exprevals.GraphEntityExpression;
import de.unika.ipd.grgen.ir.IR;
import de.unika.ipd.grgen.ir.exprevals.Expression;
import de.unika.ipd.grgen.ir.exprevals.MatchAccess;
import de.unika.ipd.grgen.ir.exprevals.Qualification;
import de.unika.ipd.grgen.ir.exprevals.VariableExpression;
import de.unika.ipd.grgen.parser.Coords;

public class MemberAccessExprNode extends ExprNode
{
	static {
		setName(MemberAccessExprNode.class, "member access expression");
	}

	private ExprNode targetExpr; // resulting from primary expression, most often an IdentExprNode
	private IdentNode memberIdent;
	private DeclNode member;
	
	public MemberAccessExprNode(Coords coords, ExprNode targetExpr, IdentNode memberIdent) {
		super(coords);
		this.targetExpr  = becomeParent(targetExpr);
		this.memberIdent = becomeParent(memberIdent);
	}

	@Override
	public Collection<? extends BaseNode> getChildren() {
		Vector<BaseNode> children = new Vector<BaseNode>();
		if(isResolved() && resolutionResult()) {
			if(targetExpr.getType() instanceof MatchTypeNode
				|| targetExpr.getType() instanceof DefinedMatchTypeNode) {
				return children; // behave like a nop in case we're a match access
			}
		}
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
			reportError("Member access requires an entity, is given type from typeof(" + typeofExpr.getEntity().getDecl().getIdentNode() + ")");
		}
		
		TypeNode ownerType = targetExpr.getType();
		
		if(ownerType instanceof MatchTypeNode) {
			MatchTypeNode matchType = (MatchTypeNode)ownerType;
			if(!matchType.resolve()) {
				reportError("Unkown test/rule referenced by match type in filter function");
				return false;
			}
			TestDeclNode test = matchType.getTest();
			if(!test.resolve()) {
				reportError("Error in test/rule referenced by match type in filter function");
				return false;
			}
			member = matchType.tryGetMember(memberIdent.toString());
			if(member == null) {
				String memberName = memberIdent.toString();
				String actionName = test.getIdentNode().toString();
				reportError("Unknown member " + memberName + ", can't find in test/rule " + actionName + " referenced by match type in filter function");
				return false;
			}
			return true;
		}

		if(ownerType instanceof DefinedMatchTypeNode) {
			DefinedMatchTypeNode definedMatchType = (DefinedMatchTypeNode)ownerType;
			if(!definedMatchType.resolve()) {
				reportError("Unkown match class referenced by match class type in match class filter function");
				return false;
			}
			member = definedMatchType.tryGetMember(memberIdent.toString());
			if(member == null) {
				String memberName = memberIdent.toString();
				String matchClassName = definedMatchType.getIdentNode().toString();
				reportError("Unknown member " + memberName + ", can't find in match class type " + matchClassName + " referenced by match class filter function");
				return false;
			}
			return true;
		}

		if(ownerType instanceof UntypedExecVarTypeNode) {
			member = new MemberDeclNode(memberIdent, BasicTypeNode.untypedType, false);
			member.resolve();
			setCheckVisited();
			return true;
		}

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

		return member instanceof MemberDeclNode ? (MemberDeclNode)member : null;
	}

	@Override
	public TypeNode getType() {
		TypeNode declType = null;
		if(targetExpr.getType() instanceof MatchTypeNode || targetExpr.getType() instanceof DefinedMatchTypeNode) {
			declType = member.getDeclType();
		} else {
			declType = member.getDecl().getDeclType(); // untyped exec var type in case owner is an untyped exec var
		}
		return declType;
	}

	@Override
	protected IR constructIR() {
		if(targetExpr.getType() instanceof MatchTypeNode || targetExpr.getType() instanceof DefinedMatchTypeNode) {
			return new MatchAccess(targetExpr.checkIR(Expression.class), member.checkIR(Entity.class));
		}
		
		if(targetExpr.getIR() instanceof VariableExpression) {
			return new Qualification(
				targetExpr.checkIR(VariableExpression.class).getVariable(),
				member.checkIR(Entity.class));
		} else if(targetExpr.getIR() instanceof GraphEntityExpression) {
			return new Qualification(
				targetExpr.checkIR(GraphEntityExpression.class).getGraphEntity(), 
				member.checkIR(Entity.class));
		} else {
			return new Qualification(
				targetExpr.checkIR(Expression.class), // normally a Cast (or an untyped exec var)
				member.checkIR(Entity.class));
		}
	}

	public static String getKindStr() {
		return "member";
	}

	public static String getUseStr() {
		return "member access";
	}
}
