/*
 * GrGen: graph rewrite generator tool -- release GrGen.NET 5.0
 * Copyright (C) 2003-2020 Universitaet Karlsruhe, Institut fuer Programmstrukturen und Datenorganisation, LS Goos; and free programmers
 * licensed under LGPL v3 (see LICENSE.txt included in the packaging of this file)
 * www.grgen.net
 */

/**
 * @author Edgar Jakumeit
 */

package de.unika.ipd.grgen.ast.expr.set;

import java.util.Vector;

import de.unika.ipd.grgen.ast.*;
import de.unika.ipd.grgen.ast.expr.ContainerSingleElementInitNode;
import de.unika.ipd.grgen.ast.typedecl.SetTypeNode;
import de.unika.ipd.grgen.ast.util.MemberResolver;
import de.unika.ipd.grgen.ir.IR;
import de.unika.ipd.grgen.ir.expr.Expression;
import de.unika.ipd.grgen.ir.expr.set.SetInit;
import de.unika.ipd.grgen.ir.Entity;
import de.unika.ipd.grgen.ir.typedecl.SetType;
import de.unika.ipd.grgen.parser.Coords;

public class SetInitNode extends ContainerSingleElementInitNode
{
	static {
		setName(SetInitNode.class, "set init");
	}

	// if set init node is used in model, for member init
	//     then lhs != null, setType == null
	// if set init node is used in actions, for anonymous const set with specified type
	//     then lhs == null, setType != null -- adjust type of set items to this type
	private BaseNode lhsUnresolved;
	private DeclNode lhs;
	private SetTypeNode setType;

	public SetInitNode(Coords coords, IdentNode member, SetTypeNode setType)
	{
		super(coords);

		if(member != null) {
			lhsUnresolved = becomeParent(member);
		} else {
			this.setType = setType;
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
			if(setType == null)
				setType = createSetType();
			return setType.resolve();
		}
	}

	@Override
	protected boolean checkLocal()
	{
		boolean success = checkContainerItems();
		
		if(!isConstant() && lhs != null) {
			reportError("Only constant items allowed in set initialization in model");
			success = false;
		}

		return success;
	}

	protected SetTypeNode createSetType()
	{
		TypeNode itemTypeNode = containerItems.getChildren().iterator().next().getType();
		IdentNode itemTypeIdent = ((DeclaredTypeNode)itemTypeNode).getIdentNode();
		return new SetTypeNode(itemTypeIdent);
	}

	@Override
	public SetTypeNode getContainerType()
	{
		assert(isResolved());
		if(lhs != null) {
			TypeNode type = lhs.getDeclType();
			return (SetTypeNode)type;
		} else {
			return setType;
		}
	}

	@Override
	public boolean isInitInModel()
	{
		return setType == null;
	}

	@Override
	protected IR constructIR()
	{
		Vector<Expression> items = constructItems();
		Entity member = lhs != null ? lhs.getEntity() : null;
		SetType type = setType != null ? setType.checkIR(SetType.class) : null;
		return new SetInit(items, member, type, isConstant());
	}

	public SetInit getSetInit()
	{
		return checkIR(SetInit.class);
	}

	public static String getUseStr()
	{
		return "set initialization";
	}
}
