/*
 * GrGen: graph rewrite generator tool -- release GrGen.NET 5.0
 * Copyright (C) 2003-2020 Universitaet Karlsruhe, Institut fuer Programmstrukturen und Datenorganisation, LS Goos; and free programmers
 * licensed under LGPL v3 (see LICENSE.txt included in the packaging of this file)
 * www.grgen.net
 */

/**
 * @author Edgar Jakumeit
 */

package de.unika.ipd.grgen.ast.expr.deque;

import java.util.Vector;

import de.unika.ipd.grgen.ast.*;
import de.unika.ipd.grgen.ast.decl.DeclNode;
import de.unika.ipd.grgen.ast.expr.ConstNode;
import de.unika.ipd.grgen.ast.expr.ContainerSingleElementInitNode;
import de.unika.ipd.grgen.ast.expr.ExprNode;
import de.unika.ipd.grgen.ast.type.DeclaredTypeNode;
import de.unika.ipd.grgen.ast.type.TypeNode;
import de.unika.ipd.grgen.ast.type.container.DequeTypeNode;
import de.unika.ipd.grgen.ast.util.MemberResolver;
import de.unika.ipd.grgen.ir.Entity;
import de.unika.ipd.grgen.ir.IR;
import de.unika.ipd.grgen.ir.expr.Expression;
import de.unika.ipd.grgen.ir.expr.deque.DequeInit;
import de.unika.ipd.grgen.ir.type.container.DequeType;
import de.unika.ipd.grgen.parser.Coords;

public class DequeInitNode extends ContainerSingleElementInitNode
{
	static {
		setName(DequeInitNode.class, "deque init");
	}

	// if deque init node is used in model, for member init
	//     then lhs != null, dequeType == null
	// if deque init node is used in actions, for anonymous const deque with specified type
	//     then lhs == null, dequeType != null -- adjust type of deque items to this type
	private BaseNode lhsUnresolved;
	private DeclNode lhs;
	private DequeTypeNode dequeType;

	public DequeInitNode(Coords coords, IdentNode member, DequeTypeNode dequeType)
	{
		super(coords);

		if(member != null) {
			lhsUnresolved = becomeParent(member);
		} else {
			this.dequeType = dequeType;
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
			if(dequeType == null)
				dequeType = createDequeType();
			return dequeType.resolve();
		}
	}

	@Override
	protected boolean checkLocal()
	{
		boolean success = checkContainerItems();

		if(!isConstant() && lhs != null) {
			reportError("Only constant items allowed in deque initialization in model");
			success = false;
		}

		return success;
	}

	protected DequeTypeNode createDequeType()
	{
		TypeNode itemTypeNode = containerItems.getChildren().iterator().next().getType();
		IdentNode itemTypeIdent = ((DeclaredTypeNode)itemTypeNode).getIdentNode();
		return new DequeTypeNode(itemTypeIdent);
	}

	@Override
	public DequeTypeNode getContainerType()
	{
		assert(isResolved());
		if(lhs != null) {
			TypeNode type = lhs.getDeclType();
			return (DequeTypeNode)type;
		} else {
			return dequeType;
		}
	}

	@Override
	public boolean isInitInModel()
	{
		return dequeType == null;
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
		DequeType type = dequeType != null ? dequeType.checkIR(DequeType.class) : null;
		return new DequeInit(items, member, type, isConstant());
	}

	public DequeInit getDequeInit()
	{
		return checkIR(DequeInit.class);
	}

	public static String getUseStr()
	{
		return "deque initialization";
	}
}
