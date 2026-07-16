/*
 * GrGen: graph rewrite generator tool -- release GrGen.NET 8.1
 * Copyright (C) 2003-2026 Universitaet Karlsruhe, Institut fuer Programmstrukturen und Datenorganisation, LS Goos; and free programmers
 * licensed under LGPL v3, some components/parts use different licenses (see LICENSE.txt included in the packaging of this file)
 * www.grgen.de / www.grgen.net
 */

package de.unika.ipd.grgen.ast.type.basic;

import java.util.Collection;
import java.util.List;
import java.util.ArrayList;

import de.unika.ipd.grgen.ast.BaseNode;
import de.unika.ipd.grgen.ast.IdentNode;
import de.unika.ipd.grgen.ast.type.TypeNode;
import de.unika.ipd.grgen.ir.IR;
import de.unika.ipd.grgen.ir.type.basic.VoidType;

/**
 * The error basic type. It is compatible to no other type.
 * TODO: Why compatible to no other type? The error node within an compiler
 * should be compatible to every other node, to protect against error avalanches
 */
public class ErrorTypeNode extends TypeNode
{
	static {
		setName(ErrorTypeNode.class, "error type");
	}

	private IdentNode id;

	public ErrorTypeNode(IdentNode id)
	{
		this.id = id;
		setCoords(id.getCoords());
	}

	/** returns children of this node */
	@Override
	public Collection<BaseNode> getChildren()
	{
		List<BaseNode> children = new ArrayList<BaseNode>();
		// no children
		return children;
	}

	/** returns names of the children, same order as in getChildren */
	@Override
	public Collection<String> getChildrenNames()
	{
		List<String> childrenNames = new ArrayList<String>();
		// no children
		return childrenNames;
	}

	@Override
	protected IR constructIR()
	{
		return new VoidType(id.getIRIdent());
	}

	public static String getKindStr()
	{
		return "error type";
	}

	@Override
	public String toString()
	{
		return "error type";
	}
}
