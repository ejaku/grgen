/*
 * GrGen: graph rewrite generator tool -- release GrGen.NET 8.1
 * Copyright (C) 2003-2026 Universitaet Karlsruhe, Institut fuer Programmstrukturen und Datenorganisation, LS Goos; and free programmers
 * licensed under LGPL v3, some components/parts use different licenses (see LICENSE.txt included in the packaging of this file)
 * www.grgen.de / www.grgen.net
 */

/// <summary>
/// @author shack
/// </summary>

namespace de.unika.ipd.grgen.ast
{

using System.Collections.Generic;

/// <summary>
/// An AST node that represents a collection of other nodes.
/// children: *:BaseNode
/// 
/// Normally AST nodes contain a fixed number of children,
/// which are accessed by their fixed index within the children list.
/// This node collects a statically unknown number of children AST nodes,
/// originating in unbounded list constructs in the parsing syntax.
/// </summary>
public class CollectNode<T> : CollectBaseNode where T : BaseNode
{
	static CollectNode()
	{
		SetClassName(typeof(CollectNode), "collect");
	}

	private IList<T> children = new List<T>();

	public virtual void AddChild(T n)
	{
		BecomeParent(n);
		children.Add(n);
	}

	public virtual void AddChildAtFront(T n)
	{
		BecomeParent(n);
		children.Insert(0, n);
	}

	/// <summary>
	/// returns children of this node </summary>
	public override ICollection<BaseNode> Children
	{
		get
		{
			return new List<BaseNode>(children);
		}
	}

	public virtual ICollection<T> ChildrenExact
	{
		get
		{
			return children;
		}
	}

	public virtual IList<T> ChildrenAsList
	{
		get
		{
			return children;
		}
	}

	public virtual T Get(int i)
	{
		return children[i];
	}

	public virtual T Set(int i, T n)
	{
		BecomeParent(n);
		return children[i] = n;
	}

	public virtual void Replace(T oldValue, T newValue)
	{
		children[children.IndexOf(oldValue)] = newValue;
		SwitchParenthood(oldValue, newValue);
	}

	public virtual int Size()
	{
		return children.Count;
	}

	/// <summary>
	/// returns names of the children, same order as in getChildren </summary>
	public override ICollection<string> ChildrenNames
	{
		get
		{
			IList<string> childrenNames = new List<string>();
			// nameless children
			return childrenNames;
		}
	}

	/// <seealso cref="de.unika.ipd.grgen.ast.BaseNode.resolveLocal() "/>
	protected internal override bool ResolveLocal()
	{
		return true; // local resolution done via call to resolveChildren from parent node
	}

	/// <seealso cref="de.unika.ipd.grgen.ast.BaseNode.checkLocal() "/>
	protected internal override bool CheckLocal()
	{
		return true;
	}

	public override string ToString()
	{
		return children.ToString();
	}
}

}
