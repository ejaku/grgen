/*
 * GrGen: graph rewrite generator tool -- release GrGen.NET 8.1
 * Copyright (C) 2003-2026 Universitaet Karlsruhe, Institut fuer Programmstrukturen und Datenorganisation, LS Goos; and free programmers
 * licensed under LGPL v3, some components/parts use different licenses (see LICENSE.txt included in the packaging of this file)
 * www.grgen.de / www.grgen.net
 */

/**
 * @author shack
 */

package de.unika.ipd.grgen.ast;

import java.awt.Color;
import java.util.Collection;
import java.util.List;
import java.util.ArrayList;

/**
 * Dummy AST node, that is used in the case of an error.
 * children: none
 */
public class ErrorNode extends BaseNode
{
	static {
		setName(ErrorNode.class, "error node");
	}

	protected ErrorNode()
	{
		super();
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

	/** @see de.unika.ipd.grgen.ast.BaseNode#resolveLocal() */
	@Override
	protected boolean resolveLocal()
	{
		return true;
	}

	/** @see de.unika.ipd.grgen.ast.BaseNode#checkLocal() */
	@Override
	protected boolean checkLocal()
	{
		return true;
	}

	@Override
	public Color getNodeColor()
	{
		return Color.RED;
	}

	@Override
	public String getNodeLabel()
	{
		return "Error";
	}

	@Override
	public final boolean isError()
	{
		return true;
	}
}
