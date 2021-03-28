/*
 * GrGen: graph rewrite generator tool -- release GrGen.NET 6.0
 * Copyright (C) 2003-2021 Universitaet Karlsruhe, Institut fuer Programmstrukturen und Datenorganisation, LS Goos; and free programmers
 * licensed under LGPL v3 (see LICENSE.txt included in the packaging of this file)
 * www.grgen.net
 */

/**
 * @author Edgar Jakumeit
 */

package de.unika.ipd.grgen.ast.type.basic;

import de.unika.ipd.grgen.ast.BaseNode;
import de.unika.ipd.grgen.ast.type.TypeNode;
import de.unika.ipd.grgen.ir.IR;
import de.unika.ipd.grgen.ir.type.basic.UntypedExecVarType;

import java.util.Collection;
import java.util.Vector;

public class UntypedExecVarTypeNode extends BasicTypeNode
{
	static {
		setName(UntypedExecVarTypeNode.class, "untyped exec variable type");
	}

	// TODO: No instance is ever used! Probably useless...
	public static class Value
	{
		public static Value NULL = new Value() {
			@Override
			public String toString()
			{
				return "Untyped null";
			}
		};

		private Value()
		{
		}
	}

	public UntypedExecVarTypeNode()
	{
	}

	/** returns children of this node */
	@Override
	public Collection<BaseNode> getChildren()
	{
		Vector<BaseNode> children = new Vector<BaseNode>();
		// no children
		return children;
	}

	/** returns names of the children, same order as in getChildren */
	@Override
	public Collection<String> getChildrenNames()
	{
		Vector<String> childrenNames = new Vector<String>();
		// no children
		return childrenNames;
	}

	@Override
	public boolean isCompatibleTo(TypeNode t)
	{
		// compatible to everything
		return true;
	}

	@Override
	public boolean isCastableTo(TypeNode t)
	{
		return isCompatibleTo(t);
	}

	@Override
	protected IR constructIR()
	{
		return new UntypedExecVarType(getIdentNode().getIdent());
	}

	@Override
	public String toString()
	{
		return "untyped";
	}
}
