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

	private Collection children = null;
	
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
	public Collection<? extends BaseNode> getWalkableChildren() {
		return children == null ? Collections.EMPTY_SET : children;
	}
}
