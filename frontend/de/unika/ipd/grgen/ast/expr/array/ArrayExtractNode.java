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
import de.unika.ipd.grgen.ast.type.DeclaredTypeNode;
import de.unika.ipd.grgen.ast.type.MatchTypeNode;
import de.unika.ipd.grgen.ast.type.TypeNode;
import de.unika.ipd.grgen.ast.type.container.ArrayTypeNode;
import de.unika.ipd.grgen.ast.type.container.ContainerTypeNode;
import de.unika.ipd.grgen.ast.util.Resolver;
import de.unika.ipd.grgen.ir.expr.Expression;
import de.unika.ipd.grgen.ir.expr.array.ArrayExtract;
import de.unika.ipd.grgen.ir.type.container.ArrayType;
import de.unika.ipd.grgen.ir.Entity;
import de.unika.ipd.grgen.ir.IR;
import de.unika.ipd.grgen.parser.Coords;

public class ArrayExtractNode extends ArrayFunctionMethodInvocationBaseExprNode
{
	static {
		setName(ArrayExtractNode.class, "array extract");
	}

	private IdentNode attribute;
	private DeclNode member;

	private ArrayTypeNode extractedArrayType;

	public ArrayExtractNode(Coords coords, ExprNode targetExpr, IdentNode attribute)
	{
		super(coords, targetExpr);
		this.attribute = attribute;
	}

	@Override
	protected boolean resolveLocal()
	{
		boolean ownerResolveResult = targetExpr.resolve();
		if(!ownerResolveResult) {
			// member can not be resolved due to inaccessible owner
			return false;
		}

		// target type already checked during resolving into this node
		ArrayTypeNode arrayType = getTargetType();
		if(!(arrayType.valueType instanceof InheritanceTypeNode)
				&& !(arrayType.valueType instanceof MatchTypeNode)) {
			targetExpr.reportError("The array function method extract can only be employed on an object of type array<match<T>, match<T.S>, match<class T>, array<T> where T extends node/edge>"
					+ " (but is employed on an object of type " + arrayType.getTypeName() + ").");
			return false;
		}

		TypeNode valueType = arrayType.valueType;

		member = Resolver.resolveMember(valueType, attribute);
		if(member == null)
			return false;

		TypeNode type = getTypeOfElementToBeExtracted();
		if(!(type instanceof DeclaredTypeNode)
				|| type instanceof ContainerTypeNode
				|| type instanceof MatchTypeNode) {
			reportError("The type " + type.getTypeName() + " of the element to be extracted"
					+ " is not an allowed type (basic type or node or edge class - set, map, array, deque, and match are forbidden).");
			return false;
		}

		DeclaredTypeNode declType = (DeclaredTypeNode)type;
		extractedArrayType = new ArrayTypeNode(declType.getIdentNode());

		return extractedArrayType.resolve();
	}

	@Override
	public TypeNode getType()
	{
		assert(isResolved());
		return extractedArrayType;
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
		return new ArrayExtract(targetExpr.checkIR(Expression.class), extractedArrayType.checkIR(ArrayType.class),
				accessedMember);
	}
}
