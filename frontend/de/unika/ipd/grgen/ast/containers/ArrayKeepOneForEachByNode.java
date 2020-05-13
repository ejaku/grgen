/*
 * GrGen: graph rewrite generator tool -- release GrGen.NET 5.0
 * Copyright (C) 2003-2020 Universitaet Karlsruhe, Institut fuer Programmstrukturen und Datenorganisation, LS Goos; and free programmers
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
import de.unika.ipd.grgen.ast.util.Resolver;
import de.unika.ipd.grgen.ir.containers.ArrayKeepOneForEachBy;
import de.unika.ipd.grgen.ir.exprevals.Expression;
import de.unika.ipd.grgen.ir.Entity;
import de.unika.ipd.grgen.ir.IR;
import de.unika.ipd.grgen.parser.Coords;

public class ArrayKeepOneForEachByNode extends ContainerFunctionMethodInvocationBaseExprNode
{
	static {
		setName(ArrayKeepOneForEachByNode.class, "array keep one for each by");
	}

	private IdentNode attribute;
	private DeclNode member;

	public ArrayKeepOneForEachByNode(Coords coords, ExprNode targetExpr, IdentNode attribute)
	{
		super(coords, targetExpr);
		this.attribute = attribute;
	}

	@Override
	public Collection<? extends BaseNode> getChildren()
	{
		Vector<BaseNode> children = new Vector<BaseNode>();
		children.add(targetExpr);
		return children;
	}

	@Override
	public Collection<String> getChildrenNames()
	{
		Vector<String> childrenNames = new Vector<String>();
		childrenNames.add("targetExpr");
		return childrenNames;
	}

	@Override
	protected boolean checkLocal()
	{
		TypeNode targetType = targetExpr.getType();
		if(!(targetType instanceof ArrayTypeNode)) {
			targetExpr.reportError("This argument to array keepOneForEach expression must be of type array<T>");
			return false;
		}

		ArrayTypeNode arrayType = (ArrayTypeNode)targetType;
		if(!(arrayType.valueType instanceof InheritanceTypeNode)
				&& !(arrayType.valueType instanceof MatchTypeNode)
				&& !(arrayType.valueType instanceof DefinedMatchTypeNode)) {
			reportError("keepOneForEach can only be employed on an array of nodes or edges or an array of match types.");
			return false;
		}

		TypeNode valueType = arrayType.valueType;
		member = Resolver.resolveMember(valueType, attribute);
		if(member == null)
			return false;

		TypeNode memberType = getTypeOfElementToBeExtracted();
		if(!memberType.isFilterableType()) {
			targetExpr.reportError("array method keepOneForEach only available for graph element attributes of type "
					+ TypeNode.getFilterableTypesAsString());
			return false;
		}

		return true;
	}

	@Override
	public TypeNode getType()
	{
		return((ArrayTypeNode)targetExpr.getType());
	}

	private TypeNode getTypeOfElementToBeExtracted()
	{
		if(member != null)
			return member.getDeclType();
		return null;
	}

	@Override
	protected IR constructIR()
	{
		Entity accessedMember = null;
		if(member != null)
			accessedMember = member.checkIR(Entity.class);

		return new ArrayKeepOneForEachBy(targetExpr.checkIR(Expression.class),
				accessedMember);
	}
}
