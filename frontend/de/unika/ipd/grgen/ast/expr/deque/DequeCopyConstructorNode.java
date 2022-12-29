/*
 * GrGen: graph rewrite generator tool -- release GrGen.NET 6.6
 * Copyright (C) 2003-2022 Universitaet Karlsruhe, Institut fuer Programmstrukturen und Datenorganisation, LS Goos; and free programmers
 * licensed under LGPL v3 (see LICENSE.txt included in the packaging of this file)
 * www.grgen.net
 */

/**
 * @author Edgar Jakumeit
 */

package de.unika.ipd.grgen.ast.expr.deque;

import java.util.Collection;
import java.util.Vector;

import de.unika.ipd.grgen.ast.*;
import de.unika.ipd.grgen.ast.expr.ExprNode;
import de.unika.ipd.grgen.ast.type.TypeNode;
import de.unika.ipd.grgen.ast.type.container.DequeTypeNode;
import de.unika.ipd.grgen.ir.IR;
import de.unika.ipd.grgen.ir.expr.Expression;
import de.unika.ipd.grgen.ir.expr.deque.DequeCopyConstructor;
import de.unika.ipd.grgen.ir.type.container.DequeType;
import de.unika.ipd.grgen.parser.Coords;

public class DequeCopyConstructorNode extends ExprNode
{
	static {
		setName(DequeInitNode.class, "deque copy constructor");
	}

	private DequeTypeNode dequeType;
	private ExprNode dequeToCopy;
	private BaseNode lhsUnresolved;

	public DequeCopyConstructorNode(Coords coords, IdentNode member, DequeTypeNode dequeType, ExprNode dequeToCopy)
	{
		super(coords);

		if(member != null) {
			lhsUnresolved = becomeParent(member);
		} else {
			this.dequeType = dequeType;
		}
		this.dequeToCopy = dequeToCopy;
	}

	@Override
	public Collection<? extends BaseNode> getChildren()
	{
		Vector<BaseNode> children = new Vector<BaseNode>();
		children.add(dequeToCopy);
		return children;
	}

	@Override
	public Collection<String> getChildrenNames()
	{
		Vector<String> childrenNames = new Vector<String>();
		childrenNames.add("dequeToCopy");
		return childrenNames;
	}

	@Override
	protected boolean resolveLocal()
	{
		if(dequeType != null) {
			return dequeType.resolve();
		} else {
			return true;
		}
	}

	@Override
	protected boolean checkLocal()
	{
		boolean success = true;

		if(lhsUnresolved != null) {
			reportError("A deque copy constructor is not allowed in a deque initialization in the model.");
			success = false;
		} else {
			if(dequeToCopy.getType() instanceof DequeTypeNode) {
				DequeTypeNode sourceDequeType = (DequeTypeNode)dequeToCopy.getType();
				success &= checkCopyConstructorTypes(dequeType.valueType, sourceDequeType.valueType, "deque", false);
			} else {
				reportError("A deque copy constructor expects a value of deque type to copy"
						+ " (but is given " + dequeToCopy.getType().getTypeName() + ").");
				success = false;
			}
		}

		return success;
	}

	@Override
	public TypeNode getType()
	{
		assert(isResolved());
		return dequeType;
	}

	@Override
	protected IR constructIR()
	{
		dequeToCopy = dequeToCopy.evaluate();
		return new DequeCopyConstructor(dequeToCopy.checkIR(Expression.class), dequeType.checkIR(DequeType.class));
	}

	public static String getKindStr()
	{
		return "deque copy constructor";
	}
}
