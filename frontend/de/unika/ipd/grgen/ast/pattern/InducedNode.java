/*
 * GrGen: graph rewrite generator tool -- release GrGen.NET 5.0
 * Copyright (C) 2003-2020 Universitaet Karlsruhe, Institut fuer Programmstrukturen und Datenorganisation, LS Goos; and free programmers
 * licensed under LGPL v3 (see LICENSE.txt included in the packaging of this file)
 * www.grgen.net
 */

/**
 * @author buchwald
 */

package de.unika.ipd.grgen.ast.pattern;

import java.awt.Color;
import java.util.Collection;
import java.util.LinkedHashSet;
import java.util.Set;
import java.util.Vector;

import de.unika.ipd.grgen.ast.BaseNode;
import de.unika.ipd.grgen.ast.decl.pattern.NodeDeclNode;
import de.unika.ipd.grgen.ast.util.DeclarationResolver;
import de.unika.ipd.grgen.parser.Coords;

public class InducedNode extends BaseNode
{
	static {
		setName(InducedNode.class, "induced");
	}

	private Vector<NodeDeclNode> children = new Vector<NodeDeclNode>();

	private Vector<BaseNode> childrenUnresolved = new Vector<BaseNode>();

	public InducedNode(Coords coords)
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
			this.reportError("Induced statement is empty");
			return false;
		}

		Set<NodeDeclNode> nodes = new LinkedHashSet<NodeDeclNode>();
		for(NodeDeclNode inducedNode : children) {
			// coords of occurrence are not available
			if(nodes.contains(inducedNode)) {
				reportWarning("Multiple occurrence of " + inducedNode.getUseString() + " "
						+ inducedNode.getIdentNode().getSymbol().getText() + " in a single induced statement");
			}
			nodes.add(inducedNode);
		}

		return true;
	}

	public Set<NodeDeclNode> getInducedNodesSet()
	{
		Set<NodeDeclNode> nodes = new LinkedHashSet<NodeDeclNode>();
		for(NodeDeclNode inducedNode : children) {
			nodes.add(inducedNode);
		}
		return nodes;
	}

	@Override
	public Color getNodeColor()
	{
		return Color.PINK;
	}
}
