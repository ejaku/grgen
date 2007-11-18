/*
  GrGen: graph rewrite generator tool.
  Copyright (C) 2005  IPD Goos, Universit"at Karlsruhe, Germany

  This library is free software; you can redistribute it and/or
  modify it under the terms of the GNU Lesser General Public
  License as published by the Free Software Foundation; either
  version 2.1 of the License, or (at your option) any later version.

  This library is distributed in the hope that it will be useful,
  but WITHOUT ANY WARRANTY; without even the implied warranty of
  MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the GNU
  Lesser General Public License for more details.

  You should have received a copy of the GNU Lesser General Public
  License along with this library; if not, write to the Free Software
  Foundation, Inc., 51 Franklin St, Fifth Floor, Boston, MA  02110-1301  USA
*/


/**
 * Created on Apr 13, 2004
 *
 * @author Sebastian Hack
 * @version $Id$
 */
package de.unika.ipd.grgen.util;

import de.unika.ipd.grgen.ast.BaseNode;
import java.awt.Color;
import java.util.Arrays;
import java.util.Collections;
import java.util.Collection;



/**
 * Some base class for a graph dumpable thing.
 */
public abstract class DefaultGraphDumpable extends Base implements GraphDumpable, Walkable {

	private Collection<BaseNode> children = null;
	
	private final Color color;
	private final int shape;
	private final String label;
	private final String info;
	
	
	protected DefaultGraphDumpable(String label, String info, Color col, int shape) {
		this.label = label;
		this.shape = shape;
		this.color = col;
		this.info = info;
	}
	
	protected DefaultGraphDumpable(String label, String info, Color col) {
		this(label, info, col, GraphDumper.DEFAULT);
	}
	
	protected DefaultGraphDumpable(String label, String info) {
		this(label, info, Color.WHITE);
	}

	protected DefaultGraphDumpable(String label) {
		this(label, null);
	}
	
	protected final void setChildren(Collection children) {
		this.children = children;
	}

	protected final void setChildren(Object[] children) {
		setChildren(Arrays.asList(children));
	}
	
	/**
	 * @see de.unika.ipd.grgen.util.GraphDumpable#getNodeId()
	 */
	public String getNodeId() {
		return getId();
	}

	/**
	 * @see de.unika.ipd.grgen.util.GraphDumpable#getNodeColor()
	 */
	public Color getNodeColor() {
		return color;
	}

	/**
	 * @see de.unika.ipd.grgen.util.GraphDumpable#getNodeShape()
	 */
	public int getNodeShape() {
		return shape;
	}

	/**
	 * @see de.unika.ipd.grgen.util.GraphDumpable#getNodeLabel()
	 */
	public String getNodeLabel() {
		return label;
	}

	/**
	 * @see de.unika.ipd.grgen.util.GraphDumpable#getNodeInfo()
	 */
	public String getNodeInfo() {
		return info;
	}

	/**
	 * @see de.unika.ipd.grgen.util.GraphDumpable#getEdgeLabel(int)
	 */
	public String getEdgeLabel(int edge) {
		return "" + edge;
	}

	/**
	 * @see de.unika.ipd.grgen.util.Walkable#getWalkableChildren()
	 */
	public Collection<BaseNode> getWalkableChildren() {
		Collection<BaseNode> empty = Collections.emptySet();
		return children == null ? empty : children;
	}
}
