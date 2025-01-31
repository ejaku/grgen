/*
 * GrGen: graph rewrite generator tool -- release GrGen.NET 7.1
 * Copyright (C) 2003-2025 Universitaet Karlsruhe, Institut fuer Programmstrukturen und Datenorganisation, LS Goos; and free programmers
 * licensed under LGPL v3 (see LICENSE.txt included in the packaging of this file)
 * www.grgen.net
 */

/**
 * @author buchwald
 */

package de.unika.ipd.grgen.ast.pattern;

import java.awt.Color;
import java.util.Collection;
import java.util.Vector;

import de.unika.ipd.grgen.ast.BaseNode;
import de.unika.ipd.grgen.ast.decl.pattern.NodeDeclNode;
import de.unika.ipd.grgen.ast.util.DeclarationResolver;
import de.unika.ipd.grgen.parser.Coords;

public class ExactNode extends BaseNode
{
	static {
		setName(ExactNode.class, "exact");
	}

	private Vector<NodeDeclNode> children = new Vector<NodeDeclNode>();

	private Vector<BaseNode> childrenUnresolved = new Vector<BaseNode>();

	public ExactNode(Coords coords)
	{
		super(coords);
	}

	public void addChild(BaseNode child)
	{
		assert(!isResolved());
		becomeParent(child);
		childrenUnresolved.add(child);
	}

	/** returns children of this node */
	@Override
	public Collection<BaseNode> getChildren()
	{
		return getValidVersionVector(childrenUnresolved, children);
	}

	/** returns names of the children, same order as in getChildren */
	@Override
	public Collection<String> getChildrenNames()
	{
		Vector<String> childrenNames = new Vector<String>();
		// nameless children
		return childrenNames;
	}

	private static final DeclarationResolver<NodeDeclNode> childrenResolver =
			new DeclarationResolver<NodeDeclNode>(NodeDeclNode.class);

	/** @see de.unika.ipd.grgen.ast.BaseNode#resolveLocal() */
	@Override
	protected boolean resolveLocal()
	{
		boolean successfullyResolved = true;
		for(int i = 0; i < childrenUnresolved.size(); ++i) {
			children.add(childrenResolver.resolve(childrenUnresolved.get(i), this));
			successfullyResolved = children.get(i) != null && successfullyResolved;
		}
		return successfullyResolved;
	}

	/**
	 * Check whether all children are of node type.
	 */
	@Override
	protected boolean checkLocal()
	{
		if(children.isEmpty()) {
			this.reportError("The exact statement is empty.");
			return false;
		}

		return true;
	}

	public Collection<NodeDeclNode> getExactNodes()
	{
		return children;
	}

	@Override
	public Color getNodeColor()
	{
		return Color.PINK;
	}
}
