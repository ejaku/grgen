/*
 * GrGen: graph rewrite generator tool -- release GrGen.NET 4.4
 * Copyright (C) 2003-2016 Universitaet Karlsruhe, Institut fuer Programmstrukturen und Datenorganisation, LS Goos; and free programmers
 * licensed under LGPL v3 (see LICENSE.txt included in the packaging of this file)
 * www.grgen.net
 */

/**
 * @author Edgar Jakumeit
 */

package de.unika.ipd.grgen.ast.containers;

import java.util.Collection;
import java.util.Vector;

import de.unika.ipd.grgen.ast.*;
import de.unika.ipd.grgen.ast.exprevals.*;
import de.unika.ipd.grgen.ast.util.DeclarationResolver;
import de.unika.ipd.grgen.ir.containers.ArrayIndexOfOrderedByExpr;
import de.unika.ipd.grgen.ir.exprevals.Expression;
import de.unika.ipd.grgen.ir.Entity;
import de.unika.ipd.grgen.ir.IR;
import de.unika.ipd.grgen.parser.Coords;

public class ArrayIndexOfOrderedByNode extends ExprNode
{
	static {
		setName(ArrayIndexOfOrderedByNode.class, "array index of ordered by");
	}

	private ExprNode targetExpr;
	IdentNode attribute;
	private MemberDeclNode member;
	private ExprNode valueExpr;

	public ArrayIndexOfOrderedByNode(Coords coords, ExprNode targetExpr, IdentNode attribute, ExprNode valueExpr)
	{
		super(coords);
		this.targetExpr = becomeParent(targetExpr);
		this.attribute = attribute;
		this.valueExpr = becomeParent(valueExpr);
	}

	@Override
	public Collection<? extends BaseNode> getChildren() {
		Vector<BaseNode> children = new Vector<BaseNode>();
		children.add(targetExpr);
		children.add(valueExpr);
		return children;
	}

	@Override
	public Collection<String> getChildrenNames() {
		Vector<String> childrenNames = new Vector<String>();
		childrenNames.add("targetExpr");
		childrenNames.add("valueExpr");
		return childrenNames;
	}

	private static final DeclarationResolver<MemberDeclNode> memberResolver
		= new DeclarationResolver<MemberDeclNode>(MemberDeclNode.class);

	@Override
	protected boolean checkLocal() {
		TypeNode targetType = targetExpr.getType();
		if(!(targetType instanceof ArrayTypeNode)) {
			targetExpr.reportError("This argument to array indexOfOrderedBy expression must be of type array<T>");
			return false;
		}
		
		ArrayTypeNode arrayType = (ArrayTypeNode)targetType;
		if(!(arrayType.valueType instanceof InheritanceTypeNode)) {
			reportError("indexOfOrderedBy can only be employed on an array of nodes or edges.");
			return false;
		}

		ScopeOwner o = (ScopeOwner) arrayType.valueType;
		o.fixupDefinition(attribute);
		member = memberResolver.resolve(attribute, this);
		if(member == null)
			return false;

		if(member.isConst()) {
			reportError("indexOfOrderedBy cannot be used on const attributes.");
		}

		TypeNode memberType = member.getDeclType();
		if(!(memberType.equals(BasicTypeNode.byteType))
				&&!(memberType.equals(BasicTypeNode.shortType))
				&& !(memberType.equals(BasicTypeNode.intType))
				&& !(memberType.equals(BasicTypeNode.longType))
				&& !(memberType.equals(BasicTypeNode.floatType))
				&& !(memberType.equals(BasicTypeNode.doubleType))
				&& !(memberType.equals(BasicTypeNode.stringType))
				&& !(memberType.equals(BasicTypeNode.booleanType))
				&& !(memberType instanceof EnumTypeNode)) {
			targetExpr.reportError("array method indexOfOrderedBy only available for attributes of type byte,short,int,long,float,double,string,boolean,enum of a graph element");
		}

		TypeNode valueType = valueExpr.getType();
		if (!valueType.isEqual(memberType))
		{
			valueExpr = becomeParent(valueExpr.adjustType(memberType, getCoords()));
			if(valueExpr == ConstNode.getInvalid()) {
				valueExpr.reportError("Argument (value) to "
						+ "array indexOfOrderedBy method must be of type " +memberType.toString());
				return false;
			}
		}
		return true;
	}

	@Override
	public TypeNode getType() {
		return BasicTypeNode.intType;
	}

	@Override
	protected IR constructIR() {
		return new ArrayIndexOfOrderedByExpr(
				targetExpr.checkIR(Expression.class),
				member.checkIR(Entity.class),
				valueExpr.checkIR(Expression.class));
	}
}
