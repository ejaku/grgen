/*
 * GrGen: graph rewrite generator tool -- release GrGen.NET 5.0
 * Copyright (C) 2003-2020 Universitaet Karlsruhe, Institut fuer Programmstrukturen und Datenorganisation, LS Goos; and free programmers
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

import de.unika.ipd.grgen.ast.expr.ExprNode;
import de.unika.ipd.grgen.ast.expr.ExprPairNode;

/**
 * An AST node that represents a collection of other nodes.
 * children: *:BaseNode
 *
 * Normally AST nodes contain a fixed number of children,
 * which are accessed by their fixed index within the children vector.
 * This node collects a statically unknown number of children AST nodes,
 * originating in unbounded list constructs in the parsing syntax.
 */
public class CollectNode<T extends BaseNode> extends BaseNode
{
	static {
		setName(CollectNode.class, "collect");
	}

	private Vector<T> children = new Vector<T>();

	public void addChild(T n)
	{
		becomeParent(n);
		children.add(n);
	}

	public void addChildAtFront(T n)
	{
		becomeParent(n);
		children.add(0, n);
	}

	/** returns children of this node */
	@Override
	public Collection<T> getChildren()
	{
		return children;
	}

	public Vector<T> getChildrenAsVector()
	{
		return children;
	}

	public T get(int i)
	{
		return children.get(i);
	}

	public T set(int i, T n)
	{
		becomeParent(n);
		return children.set(i, n);
	}

	public void replace(T oldValue, T newValue)
	{
		children.set(children.indexOf(oldValue), newValue);
		switchParenthood(oldValue, newValue);
	}
	
	public int size()
	{
		return children.size();
	}

	/** returns names of the children, same order as in getChildren */
	@Override
	public Collection<String> getChildrenNames()
	{
		Vector<String> childrenNames = new Vector<String>();
		// nameless children
		return childrenNames;
	}

	/** @see de.unika.ipd.grgen.ast.BaseNode#resolveLocal() */
	@Override
	protected boolean resolveLocal()
	{
		return true; // local resolution done via call to resolveChildren from parent node
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
		return Color.GRAY;
	}

	@Override
	public String toString()
	{
		return children.toString();
	}

	public boolean noDefElement(String containingConstruct)
	{
		boolean res = true;
		for(BaseNode child : getChildren()) {
			if(child instanceof ExprNode)
				res &= ((ExprNode)child).noDefElement(containingConstruct);
			else if(child instanceof ExprPairNode)
				res &= ((ExprPairNode)child).noDefElement(containingConstruct);
		}
		return res;
	}

	public boolean noIteratedReference(String containingConstruct)
	{
		boolean res = true;
		for(BaseNode child : getChildren()) {
			if(child instanceof ExprNode)
				res &= ((ExprNode)child).noIteratedReference(containingConstruct);
			else if(child instanceof ExprPairNode)
				res &= ((ExprPairNode)child).noIteratedReference(containingConstruct);
		}
		return res;
	}

	public boolean iteratedNotReferenced(String iterName)
	{
		boolean res = true;
		for(BaseNode child : getChildren()) {
			if(child instanceof ExprNode)
				res &= ((ExprNode)child).iteratedNotReferenced(iterName);
			else if(child instanceof ExprPairNode)
				res &= ((ExprPairNode)child).iteratedNotReferenced(iterName);
		}
		return res;
	}
}
