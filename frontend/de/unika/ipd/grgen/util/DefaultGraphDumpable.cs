/*
 * GrGen: graph rewrite generator tool -- release GrGen.NET 8.1
 * Copyright (C) 2003-2026 Universitaet Karlsruhe, Institut fuer Programmstrukturen und Datenorganisation, LS Goos; and free programmers
 * licensed under LGPL v3, some components/parts use different licenses (see LICENSE.txt included in the packaging of this file)
 * www.grgen.de / www.grgen.net
 */

/// <summary>
/// @author Sebastian Hack
/// </summary>

namespace de.unika.ipd.grgen.util
{

using System.Collections.Generic;

using BaseNode = de.unika.ipd.grgen.ast.BaseNode;

/// <summary>
/// Some base class for a graph dumpable thing.
/// </summary>
public abstract class DefaultGraphDumpable : Base, GraphDumpable, Walkable
{
	private ICollection<BaseNode> children = null;

	private readonly Color color;
	private readonly int shape;
	private readonly string label;
	private readonly string info;

	protected internal DefaultGraphDumpable(string label, string info, Color col, int shape)
	{
		this.label = label;
		this.shape = shape;
		this.color = col;
		this.info = info;
	}

	protected internal DefaultGraphDumpable(string label, string info, Color col)
		: this(label, info, col, GraphDumper.DEFAULT)
	{
	}

	protected internal DefaultGraphDumpable(string label, string info)
		: this(label, info, Color.WHITE)
	{
	}

	protected internal DefaultGraphDumpable(string label)
		: this(label, null)
	{
	}

	protected internal void setChildren(ICollection<BaseNode> children)
	{
		this.children = children;
	}

	protected internal void SetChildren(BaseNode[] children)
	{
		setChildren(children);
	}

	/// <seealso cref="de.unika.ipd.grgen.util.GraphDumpable.getNodeId()"/>
	public virtual string NodeId
	{
		get
		{
			return Id;
		}
	}

	/// <seealso cref="de.unika.ipd.grgen.util.GraphDumpable.getNodeColor()"/>
	public virtual Color NodeColor
	{
		get
		{
			return color;
		}
	}

	/// <seealso cref="de.unika.ipd.grgen.util.GraphDumpable.getNodeShape()"/>
	public virtual int NodeShape
	{
		get
		{
			return shape;
		}
	}

	/// <seealso cref="de.unika.ipd.grgen.util.GraphDumpable.getNodeLabel()"/>
	public virtual string NodeLabel
	{
		get
		{
			return label;
		}
	}

	/// <seealso cref="de.unika.ipd.grgen.util.GraphDumpable.getNodeInfo()"/>
	public virtual string NodeInfo
	{
		get
		{
			return info;
		}
	}

	/// <seealso cref="de.unika.ipd.grgen.util.GraphDumpable.getEdgeLabel(int)"/>
	public virtual string GetEdgeLabel(int edge)
	{
		return "" + edge;
	}

	/// <seealso cref="de.unika.ipd.grgen.util.Walkable.getWalkableChildren()"/>
	public virtual ICollection<BaseNode> WalkableChildren
	{
		get
		{
			ICollection<BaseNode> empty = Collections.EmptySet();
			return children == null ? empty : children;
		}
	}
}

}
