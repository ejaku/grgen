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
import de.unika.ipd.grgen.ir.containers.ArrayExtract;
import de.unika.ipd.grgen.ir.containers.ArrayType;
import de.unika.ipd.grgen.ir.exprevals.Expression;
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
				&& !(arrayType.valueType instanceof MatchTypeNode)
				&& !(arrayType.valueType instanceof DefinedMatchTypeNode)) {
			targetExpr.reportError("This argument to extract method call must be of type array<match<T>> or array<match<class T>> or array<T> where T extends node/edge");
			return false;
		}

		TypeNode valueType = arrayType.valueType;

		member = Resolver.resolveMember(valueType, attribute);
		if(member == null)
			return false;

		TypeNode type = getTypeOfElementToBeExtracted();
		if(!(type instanceof DeclaredTypeNode)
				|| type instanceof ContainerTypeNode 
				|| type instanceof MatchTypeNode || type instanceof DefinedMatchTypeNode) {
			reportError("The type " + type
					+ " is not an allowed type (basic type or node or edge class - set, map, array, deque are forbidden).");
			return false;
		}

		DeclaredTypeNode declType = (DeclaredTypeNode)type;
		extractedArrayType = new ArrayTypeNode(declType.getIdentNode());

		return extractedArrayType.resolve();
	}

	@Override
	protected boolean checkLocal()
	{
		return true;
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

		return new ArrayExtract(targetExpr.checkIR(Expression.class), extractedArrayType.checkIR(ArrayType.class),
				accessedMember);
	}
}
