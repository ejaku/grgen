/*
 * GrGen: graph rewrite generator tool -- release GrGen.NET 4.5
 * Copyright (C) 2003-2019 Universitaet Karlsruhe, Institut fuer Programmstrukturen und Datenorganisation, LS Goos; and free programmers
 * licensed under LGPL v3 (see LICENSE.txt included in the packaging of this file)
 * www.grgen.net
 */

/**
 * @author shack
 */

package de.unika.ipd.grgen.ast;

import java.awt.Color;
import java.util.Collection;
import java.util.Vector;

import de.unika.ipd.grgen.ast.containers.*;

/**
 * An AST node that represents a collection of other nodes.
 * children: *:BaseNode
 *
 * Normally AST nodes contain a fixed number of children,
 * which are accessed by their fixed index within the children vector.
 * This node collects a statically unknown number of children AST nodes,
 * originating in unbounded list constructs in the parsing syntax.
 */
public class CollectNode<T extends BaseNode> extends BaseNode {
	static {
		setName(CollectNode.class, "collect");
	}

	public Vector<T> children = new Vector<T>();

	public void addChild(T n) {
		becomeParent(n);
		children.add(n);
	}

	/** returns children of this node */
	@Override
	public Collection<T> getChildren() {
		return children;
	}

	public T get(int i) {
		return children.get(i);
	}

	public int size() {
		return children.size();
	}

	/** returns names of the children, same order as in getChildren */
	@Override
	public Collection<String> getChildrenNames() {
		Vector<String> childrenNames = new Vector<String>();
		// nameless children
		return childrenNames;
	}

	/** @see de.unika.ipd.grgen.ast.BaseNode#resolveLocal() */
	@Override
	protected boolean resolveLocal() {
		return true; // local resolution done via call to resolveChildren from parent node
	}

	/** @see de.unika.ipd.grgen.ast.BaseNode#checkLocal() */
	@Override
	protected boolean checkLocal() {
		return true;
	}

	@Override
	public Color getNodeColor() {
		return Color.GRAY;
	}

	@Override
	public String toString() {
		return children.toString();
	}
	
	public boolean noDefElementInCondition() {
		boolean res = true;
		for(BaseNode child : getChildren()) {
			if(child instanceof SetItemNode)
				res &= ((SetItemNode)child).noDefElementInCondition();
			else if(child instanceof MapItemNode)
				res &= ((MapItemNode)child).noDefElementInCondition();
			else if(child instanceof ArrayItemNode)
				res &= ((ArrayItemNode)child).noDefElementInCondition();
			else if(child instanceof DequeItemNode)
				res &= ((DequeItemNode)child).noDefElementInCondition();
		}
		return res;
	}
}
