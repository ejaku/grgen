/*
 * GrGen: graph rewrite generator tool -- release GrGen.NET 6.7
 * Copyright (C) 2003-2023 Universitaet Karlsruhe, Institut fuer Programmstrukturen und Datenorganisation, LS Goos; and free programmers
 * licensed under LGPL v3 (see LICENSE.txt included in the packaging of this file)
 * www.grgen.net
 */

/**
 * @author Edgar Jakumeit
 */

package de.unika.ipd.grgen.ast.expr.set;

import java.util.Collection;
import java.util.Vector;

import de.unika.ipd.grgen.ast.*;
import de.unika.ipd.grgen.ast.expr.ExprNode;
import de.unika.ipd.grgen.ast.type.TypeNode;
import de.unika.ipd.grgen.ast.type.container.SetTypeNode;
import de.unika.ipd.grgen.ir.IR;
import de.unika.ipd.grgen.ir.expr.Expression;
import de.unika.ipd.grgen.ir.expr.set.SetCopyConstructor;
import de.unika.ipd.grgen.ir.type.container.SetType;
import de.unika.ipd.grgen.parser.Coords;

public class SetCopyConstructorNode extends ExprNode
{
	static {
		setName(SetCopyConstructorNode.class, "set copy constructor");
	}

	private SetTypeNode setType;
	private ExprNode setToCopy;
	private BaseNode lhsUnresolved;

	public SetCopyConstructorNode(Coords coords, IdentNode member, SetTypeNode setType, ExprNode setToCopy)
	{
		super(coords);

		if(member != null) {
			lhsUnresolved = becomeParent(member);
		} else {
			this.setType = setType;
		}
		this.setToCopy = setToCopy;
	}

	@Override
	public Collection<? extends BaseNode> getChildren()
	{
		Vector<BaseNode> children = new Vector<BaseNode>();
		children.add(setToCopy);
		return children;
	}

	@Override
	public Collection<String> getChildrenNames()
	{
		Vector<String> childrenNames = new Vector<String>();
		childrenNames.add("setToCopy");
		return childrenNames;
	}

	@Override
	protected boolean resolveLocal()
	{
		if(setType != null) {
			return setType.resolve();
		} else {
			return true;
		}
	}

	@Override
	protected boolean checkLocal()
	{
		boolean success = true;

		if(lhsUnresolved != null) {
			reportError("A set copy constructor is not allowed in a set initialization in the model.");
			success = false;
		} else {
			if(setToCopy.getType() instanceof SetTypeNode) {
				SetTypeNode sourceSetType = (SetTypeNode)setToCopy.getType();
				success &= checkCopyConstructorTypes(setType.valueType, sourceSetType.valueType, "set", false);
			} else {
				reportError("A set copy constructor expects a value of set type to copy"
						+ " (but is given " + setToCopy.getType().getTypeName() + ").");
				success = false;
			}
		}

		return success;
	}

	@Override
	public TypeNode getType()
	{
		assert(isResolved());
		return setType;
	}

	@Override
	protected IR constructIR()
	{
		setToCopy = setToCopy.evaluate();
		return new SetCopyConstructor(setToCopy.checkIR(Expression.class), setType.checkIR(SetType.class));
	}

	public static String getKindStr()
	{
		return "set copy constructor";
	}
}
