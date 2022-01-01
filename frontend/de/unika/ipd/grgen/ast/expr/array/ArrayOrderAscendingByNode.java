/*
 * GrGen: graph rewrite generator tool -- release GrGen.NET 6.5
 * Copyright (C) 2003-2022 Universitaet Karlsruhe, Institut fuer Programmstrukturen und Datenorganisation, LS Goos; and free programmers
 * licensed under LGPL v3 (see LICENSE.txt included in the packaging of this file)
 * www.grgen.net
 */

/**
 * @author Edgar Jakumeit
 */

package de.unika.ipd.grgen.ast.expr.array;

import de.unika.ipd.grgen.ast.*;
import de.unika.ipd.grgen.ast.decl.DeclNode;
import de.unika.ipd.grgen.ast.expr.ExprNode;
import de.unika.ipd.grgen.ast.model.type.InheritanceTypeNode;
import de.unika.ipd.grgen.ast.type.MatchTypeNode;
import de.unika.ipd.grgen.ast.type.TypeNode;
import de.unika.ipd.grgen.ast.type.container.ArrayTypeNode;
import de.unika.ipd.grgen.ast.util.Resolver;
import de.unika.ipd.grgen.ir.expr.Expression;
import de.unika.ipd.grgen.ir.expr.array.ArrayOrderAscendingBy;
import de.unika.ipd.grgen.ir.Entity;
import de.unika.ipd.grgen.ir.IR;
import de.unika.ipd.grgen.parser.Coords;

public class ArrayOrderAscendingByNode extends ArrayFunctionMethodInvocationBaseExprNode
{
	static {
		setName(ArrayOrderAscendingByNode.class, "array order ascending by");
	}

	private IdentNode attribute;
	private DeclNode member;

	public ArrayOrderAscendingByNode(Coords coords, ExprNode targetExpr, IdentNode attribute)
	{
		super(coords, targetExpr);
		this.attribute = attribute;
	}

	@Override
	protected boolean checkLocal()
	{
		// target type already checked during resolving into this node
		ArrayTypeNode arrayType = getTargetType();
		if(!(arrayType.valueType instanceof InheritanceTypeNode)
				&& !(arrayType.valueType instanceof MatchTypeNode)) {
			reportError("orderAscendingBy can only be employed on an array of nodes or edges or class objects or transient class objects or match types or match class types.");
			return false;
		}

		TypeNode valueType = arrayType.valueType;
		member = Resolver.resolveMember(valueType, attribute);
		if(member == null)
			return false;

		TypeNode memberType = getTypeOfElementToBeExtracted();
		if(!memberType.isOrderableType()) {
			targetExpr.reportError("array method orderAscendingBy only available for attributes of type "
					+ TypeNode.getOrderableTypesAsString());
			return false;
		}

		return true;
	}

	@Override
	public TypeNode getType()
	{
		return getTargetType();
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

		targetExpr = targetExpr.evaluate();
		return new ArrayOrderAscendingBy(targetExpr.checkIR(Expression.class),
				accessedMember);
	}
}
