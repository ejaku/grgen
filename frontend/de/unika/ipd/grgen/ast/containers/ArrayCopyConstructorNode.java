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
import de.unika.ipd.grgen.ir.IR;
import de.unika.ipd.grgen.ir.containers.ArrayCopyConstructor;
import de.unika.ipd.grgen.ir.containers.ArrayType;
import de.unika.ipd.grgen.ir.exprevals.Expression;
import de.unika.ipd.grgen.parser.Coords;

//TODO: there's a lot of code which could be handled in a common way regarding the containers set|map|array|deque 
//should be unified in abstract base classes and algorithms working on them

public class ArrayCopyConstructorNode extends ExprNode
{
	static {
		setName(ArrayCopyConstructorNode.class, "array copy constructor");
	}

	private ArrayTypeNode arrayType;
	private ExprNode arrayToCopy;
	private BaseNode lhsUnresolved;

	public ArrayCopyConstructorNode(Coords coords, IdentNode member, ArrayTypeNode arrayType, ExprNode arrayToCopy) {
		super(coords);

		if(member!=null) {
			lhsUnresolved = becomeParent(member);
		} else {
			this.arrayType = arrayType;
		}
		this.arrayToCopy = arrayToCopy;
	}

	@Override
	public Collection<? extends BaseNode> getChildren() {
		Vector<BaseNode> children = new Vector<BaseNode>();
		children.add(arrayToCopy);
		return children;
	}

	@Override
	public Collection<String> getChildrenNames() {
		Vector<String> childrenNames = new Vector<String>();
		childrenNames.add("arrayToCopy");
		return childrenNames;
	}

	@Override
	protected boolean resolveLocal() {
		if(arrayType!=null) {
			return arrayType.resolve();
		} else {
			return true;
		}
	}

	@Override
	protected boolean checkLocal() {
		boolean success = true;

		if(lhsUnresolved!=null) {
			reportError("Array copy constructor not allowed in array initialization in model");
			success = false;
		} else {
			if(arrayToCopy.getType() instanceof ArrayTypeNode)
			{
				ArrayTypeNode sourceArrayType = (ArrayTypeNode)arrayToCopy.getType();
				success &= checkCopyConstructorTypes(arrayType.valueType, sourceArrayType.valueType, "Array", "");
			}
			else
			{
				reportError("Array copy constructor expects array type");
				success = false;
			}
		} 

		return success;
	}

	@Override
	public TypeNode getType() {
		assert(isResolved());
		return arrayType;
	}

	@Override
	protected IR constructIR() {
		return new ArrayCopyConstructor(arrayToCopy.checkIR(Expression.class), arrayType.checkIR(ArrayType.class));
	}

	public static String getUseStr() {
		return "array copy constructor";
	}
}
