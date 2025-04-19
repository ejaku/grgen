/*
 * GrGen: graph rewrite generator tool -- release GrGen.NET 7.2
 * Copyright (C) 2003-2025 Universitaet Karlsruhe, Institut fuer Programmstrukturen und Datenorganisation, LS Goos; and free programmers
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
import de.unika.ipd.grgen.ir.expr.array.ArrayKeepOneForEachBy;
import de.unika.ipd.grgen.ir.Entity;
import de.unika.ipd.grgen.ir.IR;
import de.unika.ipd.grgen.parser.Coords;

public class ArrayKeepOneForEachByNode extends ArrayFunctionMethodInvocationBaseExprNode
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
	protected boolean checkLocal()
	{
		// target type already checked during resolving into this node
		ArrayTypeNode arrayType = getTargetType();
		if(!(arrayType.valueType instanceof InheritanceTypeNode)
				&& !(arrayType.valueType instanceof MatchTypeNode)) {
			targetExpr.reportError("The array function method keepOneForEachBy can only be employed on an object of type array<nodes, edges, class objects, transient class objects, match types, match class types>"
					+ " (but is employed on an object of type " + arrayType.getTypeName() + ").");
			return false;
		}

		TypeNode valueType = arrayType.valueType;
		member = Resolver.resolveMember(valueType, attribute);
		if(member == null)
			return false;

		TypeNode memberType = getTypeOfElementToBeExtracted();
		if(!memberType.isFilterableType()) {
			targetExpr.reportError("The array function method keepOneForEachBy is only available for attributes of type "
					+ TypeNode.getFilterableTypesAsString() + " (but is of type " + memberType.getTypeName() + ").");
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
		return new ArrayKeepOneForEachBy(targetExpr.checkIR(Expression.class),
				accessedMember);
	}
}
