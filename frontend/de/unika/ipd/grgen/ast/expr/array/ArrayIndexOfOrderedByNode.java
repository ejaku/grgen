/*
 * GrGen: graph rewrite generator tool -- release GrGen.NET 8.0
 * Copyright (C) 2003-2025 Universitaet Karlsruhe, Institut fuer Programmstrukturen und Datenorganisation, LS Goos; and free programmers
 * licensed under LGPL v3 (see LICENSE.txt included in the packaging of this file)
 * www.grgen.de / www.grgen.net
 */

/**
 * @author Edgar Jakumeit
 */

package de.unika.ipd.grgen.ast.expr.array;

import java.util.Collection;
import java.util.Vector;

import de.unika.ipd.grgen.ast.*;
import de.unika.ipd.grgen.ast.decl.DeclNode;
import de.unika.ipd.grgen.ast.expr.ConstNode;
import de.unika.ipd.grgen.ast.expr.ExprNode;
import de.unika.ipd.grgen.ast.model.type.EnumTypeNode;
import de.unika.ipd.grgen.ast.model.type.InheritanceTypeNode;
import de.unika.ipd.grgen.ast.type.MatchTypeNode;
import de.unika.ipd.grgen.ast.type.TypeNode;
import de.unika.ipd.grgen.ast.type.basic.BasicTypeNode;
import de.unika.ipd.grgen.ast.type.container.ArrayTypeNode;
import de.unika.ipd.grgen.ast.util.Resolver;
import de.unika.ipd.grgen.ir.expr.Expression;
import de.unika.ipd.grgen.ir.expr.array.ArrayIndexOfOrderedByExpr;
import de.unika.ipd.grgen.ir.Entity;
import de.unika.ipd.grgen.ir.IR;
import de.unika.ipd.grgen.parser.Coords;

public class ArrayIndexOfOrderedByNode extends ArrayFunctionMethodInvocationBaseExprNode
{
	static {
		setName(ArrayIndexOfOrderedByNode.class, "array index of ordered by");
	}

	IdentNode attribute;
	private DeclNode member;
	private ExprNode valueExpr;

	public ArrayIndexOfOrderedByNode(Coords coords, ExprNode targetExpr, IdentNode attribute, ExprNode valueExpr)
	{
		super(coords, targetExpr);
		this.attribute = attribute;
		this.valueExpr = becomeParent(valueExpr);
	}

	@Override
	public Collection<? extends BaseNode> getChildren()
	{
		Vector<BaseNode> children = new Vector<BaseNode>();
		children.add(targetExpr);
		children.add(valueExpr);
		return children;
	}

	@Override
	public Collection<String> getChildrenNames()
	{
		Vector<String> childrenNames = new Vector<String>();
		childrenNames.add("targetExpr");
		childrenNames.add("valueExpr");
		return childrenNames;
	}

	@Override
	protected boolean checkLocal()
	{
		// target type already checked during resolving into this node
		ArrayTypeNode arrayType = getTargetType();
		if(!(arrayType.valueType instanceof InheritanceTypeNode)
				&& !(arrayType.valueType instanceof MatchTypeNode)) {
			targetExpr.reportError("The array function method indexOfOrderedBy can only be employed on an object of type array<nodes, edges, class objects, transient class objects, match types, match class types>"
					+ " (but is employed on an object of type " + arrayType.getTypeName() + ").");
			return false;
		}

		member = Resolver.resolveMember(arrayType.valueType, attribute);
		if(member == null)
			return false;

		TypeNode memberType = member.getDeclType();
		if(!(memberType.equals(BasicTypeNode.byteType))
				&& !(memberType.equals(BasicTypeNode.shortType))
				&& !(memberType.equals(BasicTypeNode.intType))
				&& !(memberType.equals(BasicTypeNode.longType))
				&& !(memberType.equals(BasicTypeNode.floatType))
				&& !(memberType.equals(BasicTypeNode.doubleType))
				&& !(memberType.equals(BasicTypeNode.stringType))
				&& !(memberType.equals(BasicTypeNode.booleanType))
				&& !(memberType instanceof EnumTypeNode)) {
			targetExpr.reportError("The array function method indexOfOrderedBy is only available for attributes of type byte, short, int, long, float, double, string, boolean, enum of a graph element"
					+ " (but is of type " + memberType.getTypeName() + ")");
		}

		TypeNode valueType = valueExpr.getType();
		if(!valueType.isEqual(memberType)) {
			ExprNode valueExprOld = valueExpr;
			valueExpr = becomeParent(valueExpr.adjustType(memberType, getCoords()));
			if(valueExpr == ConstNode.getInvalid()) {
				valueExprOld.reportError("The array function method indexOfOrderedBy expects as 1. argument (valueToSearchFor) a value of type " + memberType.getTypeName()
						+ " (but is given a value of type " + valueType.getTypeName() + ").");
				return false;
			}
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
		targetExpr = targetExpr.evaluate();
		valueExpr = valueExpr.evaluate();
		return new ArrayIndexOfOrderedByExpr(targetExpr.checkIR(Expression.class),
				member.checkIR(Entity.class),
				valueExpr.checkIR(Expression.class));
	}
}
