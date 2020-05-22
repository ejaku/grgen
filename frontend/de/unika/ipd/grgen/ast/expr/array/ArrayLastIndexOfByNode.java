/*
 * GrGen: graph rewrite generator tool -- release GrGen.NET 5.0
 * Copyright (C) 2003-2020 Universitaet Karlsruhe, Institut fuer Programmstrukturen und Datenorganisation, LS Goos; and free programmers
 * licensed under LGPL v3 (see LICENSE.txt included in the packaging of this file)
 * www.grgen.net
 */

/**
 * @author Edgar Jakumeit
 */

package de.unika.ipd.grgen.ast.expr.array;

import java.util.Collection;
import java.util.Vector;

import de.unika.ipd.grgen.ast.*;
import de.unika.ipd.grgen.ast.expr.ConstNode;
import de.unika.ipd.grgen.ast.expr.ExprNode;
import de.unika.ipd.grgen.ast.model.decl.MemberDeclNode;
import de.unika.ipd.grgen.ast.model.type.InheritanceTypeNode;
import de.unika.ipd.grgen.ast.type.TypeNode;
import de.unika.ipd.grgen.ast.type.basic.BasicTypeNode;
import de.unika.ipd.grgen.ast.type.container.ArrayTypeNode;
import de.unika.ipd.grgen.ast.util.DeclarationResolver;
import de.unika.ipd.grgen.ir.expr.Expression;
import de.unika.ipd.grgen.ir.expr.array.ArrayLastIndexOfByExpr;
import de.unika.ipd.grgen.ir.Entity;
import de.unika.ipd.grgen.ir.IR;
import de.unika.ipd.grgen.parser.Coords;

public class ArrayLastIndexOfByNode extends ArrayFunctionMethodInvocationBaseExprNode
{
	static {
		setName(ArrayLastIndexOfByNode.class, "array last index of by");
	}

	private IdentNode attribute;
	private MemberDeclNode member;
	private ExprNode valueExpr;
	private ExprNode startIndexExpr;

	public ArrayLastIndexOfByNode(Coords coords, ExprNode targetExpr, IdentNode attribute, ExprNode valueExpr)
	{
		super(coords, targetExpr);
		this.attribute = attribute;
		this.valueExpr = becomeParent(valueExpr);
	}

	public ArrayLastIndexOfByNode(Coords coords, ExprNode targetExpr, IdentNode attribute, ExprNode valueExpr,
			ExprNode startIndexExpr)
	{
		super(coords, targetExpr);
		this.attribute = attribute;
		this.valueExpr = becomeParent(valueExpr);
		this.startIndexExpr = becomeParent(startIndexExpr);
	}

	@Override
	public Collection<? extends BaseNode> getChildren()
	{
		Vector<BaseNode> children = new Vector<BaseNode>();
		children.add(targetExpr);
		children.add(valueExpr);
		if(startIndexExpr != null)
			children.add(startIndexExpr);
		return children;
	}

	@Override
	public Collection<String> getChildrenNames()
	{
		Vector<String> childrenNames = new Vector<String>();
		childrenNames.add("targetExpr");
		childrenNames.add("valueExpr");
		if(startIndexExpr != null)
			childrenNames.add("startIndex");
		return childrenNames;
	}

	private static final DeclarationResolver<MemberDeclNode> memberResolver =
			new DeclarationResolver<MemberDeclNode>(MemberDeclNode.class);

	@Override
	protected boolean checkLocal()
	{
		// target type already checked during resolving into this node
		ArrayTypeNode arrayType = getTargetType();
		if(!(arrayType.valueType instanceof InheritanceTypeNode)) {
			reportError("lastIndexOfBy can only be employed on an array of nodes or edges.");
			return false;
		}

		ScopeOwner o = (ScopeOwner)arrayType.valueType;
		o.fixupDefinition(attribute);
		member = memberResolver.resolve(attribute, this);
		if(member == null)
			return false;

		if(member.isConst()) {
			reportError("lastIndexOfBy cannot be used on const attributes.");
		}

		TypeNode memberType = member.getDeclType();
		TypeNode valueType = valueExpr.getType();
		if(!valueType.isEqual(memberType)) {
			valueExpr = becomeParent(valueExpr.adjustType(memberType, getCoords()));
			if(valueExpr == ConstNode.getInvalid()) {
				valueExpr.reportError("Argument (value) to array lastIndexOfBy method must be of type "
						+ memberType.toString());
				return false;
			}
		}
		if(startIndexExpr != null && !startIndexExpr.getType().isEqual(BasicTypeNode.intType)) {
			startIndexExpr.reportError("Argument (start index) to array lastIndexOfBy expression must be of type int");
			return false;
		}
		return true;
	}

	@Override
	public TypeNode getType()
	{
		return BasicTypeNode.intType;
	}

	@Override
	protected IR constructIR()
	{
		if(startIndexExpr != null)
			return new ArrayLastIndexOfByExpr(targetExpr.checkIR(Expression.class),
					member.checkIR(Entity.class),
					valueExpr.checkIR(Expression.class),
					startIndexExpr.checkIR(Expression.class));
		else
			return new ArrayLastIndexOfByExpr(targetExpr.checkIR(Expression.class),
					member.checkIR(Entity.class),
					valueExpr.checkIR(Expression.class));
	}
}
