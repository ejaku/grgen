/*
 * GrGen: graph rewrite generator tool -- release GrGen.NET 4.4
 * Copyright (C) 2003-2016 Universitaet Karlsruhe, Institut fuer Programmstrukturen und Datenorganisation, LS Goos; and free programmers
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
import de.unika.ipd.grgen.ir.IR;
import de.unika.ipd.grgen.ir.containers.DequeCopyConstructor;
import de.unika.ipd.grgen.ir.containers.DequeType;
import de.unika.ipd.grgen.ir.exprevals.Expression;
import de.unika.ipd.grgen.parser.Coords;

//TODO: there's a lot of code which could be handled in a common way regarding the containers set|map|array|deque
//should be unified in abstract base classes and algorithms working on them

public class DequeCopyConstructorNode extends ExprNode
{
	static {
		setName(DequeInitNode.class, "deque copy constructor");
	}

	private DequeTypeNode dequeType;
	private ExprNode dequeToCopy;
	private BaseNode lhsUnresolved;

	public DequeCopyConstructorNode(Coords coords, IdentNode member, DequeTypeNode dequeType, ExprNode dequeToCopy) {
		super(coords);

		if(member!=null) {
			lhsUnresolved = becomeParent(member);
		} else {
			this.dequeType = dequeType;
		}
		this.dequeToCopy = dequeToCopy;
	}

	@Override
	public Collection<? extends BaseNode> getChildren() {
		Vector<BaseNode> children = new Vector<BaseNode>();
		children.add(dequeToCopy);
		return children;
	}

	@Override
	public Collection<String> getChildrenNames() {
		Vector<String> childrenNames = new Vector<String>();
		childrenNames.add("dequeToCopy");
		return childrenNames;
	}

	@Override
	protected boolean resolveLocal() {
		if(dequeType!=null) {
			return dequeType.resolve();
		} else {
			return true;
		}
	}

	@Override
	protected boolean checkLocal() {
		boolean success = true;

		if(lhsUnresolved!=null) {
			reportError("Deque copy constructor not allowed in deque initialization in model");
			success = false;
		} else {
			if(dequeToCopy.getType() instanceof DequeTypeNode)
			{
				DequeTypeNode sourceDequeType = (DequeTypeNode)dequeToCopy.getType();
				success &= checkCopyConstructorTypes(dequeType.valueType, sourceDequeType.valueType, "Deque", "");
			}
			else
			{
				reportError("Deque copy constructor expects deque type");
				success = false;
			}
		} 
		
		return success;
	}

	@Override
	public TypeNode getType() {
		assert(isResolved());
		return dequeType;
	}

	@Override
	protected IR constructIR() {
		return new DequeCopyConstructor(dequeToCopy.checkIR(Expression.class), dequeType.checkIR(DequeType.class));
	}

	public static String getUseStr() {
		return "deque copy constructor";
	}
}
