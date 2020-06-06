/*
 * GrGen: graph rewrite generator tool -- release GrGen.NET 5.0
 * Copyright (C) 2003-2020 Universitaet Karlsruhe, Institut fuer Programmstrukturen und Datenorganisation, LS Goos; and free programmers
 * licensed under LGPL v3 (see LICENSE.txt included in the packaging of this file)
 * www.grgen.net
 */

/**
 * @author Sebastian Hack
 */

package de.unika.ipd.grgen.util;

import java.awt.Color;
import java.util.Arrays;
import java.util.Collection;
import java.util.Collections;

import de.unika.ipd.grgen.ast.BaseNode;

/**
 * Some base class for a graph dumpable thing.
 */
public abstract class DefaultGraphDumpable extends Base implements GraphDumpable, Walkable
{
	private Collection<BaseNode> children = null;

	private final Color color;
	private final int shape;
	private final String label;
	private final String info;

	protected DefaultGraphDumpable(String label, String info, Color col, int shape)
	{
		this.label = label;
		this.shape = shape;
		this.color = col;
		this.info = info;
	}

	protected DefaultGraphDumpable(String label, String info, Color col)
	{
		this(label, info, col, GraphDumper.DEFAULT);
	}

	protected DefaultGraphDumpable(String label, String info)
	{
		this(label, info, Color.WHITE);
	}

	protected DefaultGraphDumpable(String label)
	{
		this(label, null);
	}

	protected final void setChildren(Collection<BaseNode> children)
	{
		this.children = children;
	}

	protected final void setChildren(BaseNode[] children)
	{
		setChildren(Arrays.asList(children));
	}

	/**
	 * @see de.unika.ipd.grgen.util.GraphDumpable#getNodeId()
	 */
	@Override
	public String getNodeId()
	{
		return getId();
	}

	/**
	 * @see de.unika.ipd.grgen.util.GraphDumpable#getNodeColor()
	 */
	@Override
	public Color getNodeColor()
	{
		return color;
	}

	/**
	 * @see de.unika.ipd.grgen.util.GraphDumpable#getNodeShape()
	 */
	@Override
	public int getNodeShape()
	{
		return shape;
	}

	/**
	 * @see de.unika.ipd.grgen.util.GraphDumpable#getNodeLabel()
	 */
	@Override
	public String getNodeLabel()
	{
		return label;
	}

	/**
	 * @see de.unika.ipd.grgen.util.GraphDumpable#getNodeInfo()
	 */
	@Override
	public String getNodeInfo()
	{
		return info;
	}

	/**
	 * @see de.unika.ipd.grgen.util.GraphDumpable#getEdgeLabel(int)
	 */
	@Override
	public String getEdgeLabel(int edge)
	{
		return "" + edge;
	}

	/**
	 * @see de.unika.ipd.grgen.util.Walkable#getWalkableChildren()
	 */
	@Override
	public Collection<BaseNode> getWalkableChildren()
	{
		Collection<BaseNode> empty = Collections.emptySet();
		return children == null ? empty : children;
	}
}
