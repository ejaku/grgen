/*
 * GrGen: graph rewrite generator tool -- release GrGen.NET 6.7
 * Copyright (C) 2003-2023 Universitaet Karlsruhe, Institut fuer Programmstrukturen und Datenorganisation, LS Goos; and free programmers
 * licensed under LGPL v3 (see LICENSE.txt included in the packaging of this file)
 * www.grgen.net
 */

/**
 * @author Edgar Jakumeit
 */

package de.unika.ipd.grgen.ast.expr.array;

import java.util.Vector;

import de.unika.ipd.grgen.ast.*;
import de.unika.ipd.grgen.ast.decl.DeclNode;
import de.unika.ipd.grgen.ast.expr.ConstNode;
import de.unika.ipd.grgen.ast.expr.ContainerSingleElementInitNode;
import de.unika.ipd.grgen.ast.expr.ExprNode;
import de.unika.ipd.grgen.ast.type.DeclaredTypeNode;
import de.unika.ipd.grgen.ast.type.TypeNode;
import de.unika.ipd.grgen.ast.type.container.ArrayTypeNode;
import de.unika.ipd.grgen.ast.util.MemberResolver;
import de.unika.ipd.grgen.ir.Entity;
import de.unika.ipd.grgen.ir.IR;
import de.unika.ipd.grgen.ir.expr.Expression;
import de.unika.ipd.grgen.ir.expr.array.ArrayInit;
import de.unika.ipd.grgen.ir.type.container.ArrayType;
import de.unika.ipd.grgen.parser.Coords;

public class ArrayInitNode extends ContainerSingleElementInitNode
{
	static {
		setName(ArrayInitNode.class, "array init");
	}

	// if array init node is used in model, for member init
	//     then lhs != null, arrayType == null
	// if array init node is used in actions, for anonymous const array with specified type
	//     then lhs == null, arrayType != null -- adjust type of array items to this type
	private BaseNode lhsUnresolved;
	private DeclNode lhs;
	private ArrayTypeNode arrayType;

	public ArrayInitNode(Coords coords, IdentNode member, ArrayTypeNode arrayType)
	{
		super(coords);

		if(member != null) {
			lhsUnresolved = becomeParent(member);
		} else {
			this.arrayType = arrayType;
		}
	}

	private static final MemberResolver<DeclNode> lhsResolver = new MemberResolver<DeclNode>();

	@Override
	protected boolean resolveLocal()
	{
		if(lhsUnresolved != null) {
			if(!lhsResolver.resolve(lhsUnresolved))
				return false;
			lhs = lhsResolver.getResult(DeclNode.class);
			return lhsResolver.finish();
		} else {
			if(arrayType == null)
				arrayType = createArrayType();
			return arrayType.resolve();
		}
	}

	@Override
	protected boolean checkLocal()
	{
		boolean success = checkContainerItems();

		if(!isConstant() && lhs != null) {
			reportError("Only constant items are allowed in an array initialization in the model.");
			success = false;
		}

		return success;
	}

	protected ArrayTypeNode createArrayType()
	{
		TypeNode itemTypeNode = containerItems.getChildren().iterator().next().getType();
		IdentNode itemTypeIdent = ((DeclaredTypeNode)itemTypeNode).getIdentNode();
		return new ArrayTypeNode(itemTypeIdent);
	}

	@Override
	public ArrayTypeNode getContainerType()
	{
		assert(isResolved());
		if(lhs != null) {
			TypeNode type = lhs.getDeclType();
			return (ArrayTypeNode)type;
		} else {
			return arrayType;
		}
	}

	@Override
	public boolean isInitInModel()
	{
		return arrayType == null;
	}

	public ExprNode getAtIndex(ConstNode node)
	{
		Integer index = (Integer)node.getValue();
		if(index.intValue() < 0)
			return null;
		if(index.intValue() >= containerItems.size())
			return null;
		return containerItems.getChildrenAsVector().get(index.intValue());
	}

	@Override
	protected IR constructIR()
	{
		Vector<Expression> items = constructItems();
		Entity member = lhs != null ? lhs.getEntity() : null;
		ArrayType type = arrayType != null ? arrayType.checkIR(ArrayType.class) : null;
		return new ArrayInit(items, member, type, isConstant());
	}

	public ArrayInit getArrayInit()
	{
		return checkIR(ArrayInit.class);
	}

	public static String getKindStr()
	{
		return "array initialization";
	}
}
