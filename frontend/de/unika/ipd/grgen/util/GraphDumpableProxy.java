/*
 * GrGen: graph rewrite generator tool -- release GrGen.NET 2.0
 * Copyright (C) 2008 Universitaet Karlsruhe, Institut fuer Programmstrukturen und Datenorganisation, LS Goos
 * licensed under GPL v3 (see LICENSE.txt included in the packaging of this file)
 */

/**
 * @author shack
 * @version $Id$
 */
package de.unika.ipd.grgen.util;

import java.awt.Color;

/**
 * A Graph Dumpable proxy class.
 */
public class GraphDumpableProxy implements GraphDumpable {

	/** The GraphDumpable to be proxy for. */
	private GraphDumpable gd;

	public GraphDumpableProxy(GraphDumpable gd) {
		this.gd = gd;
	}

	/**
	 * Get the proxied object.
	 * @return The proxied GraphDumpable object.
	 */
	protected GraphDumpable getGraphDumpable() {
		return gd;
	}

  /**
   * @see de.unika.ipd.grgen.util.GraphDumpable#getNodeId()
   */
  public String getNodeId() {
  	return gd.getNodeId();
  }

  /**
   * @see de.unika.ipd.grgen.util.GraphDumpable#getNodeColor()
   */
  public Color getNodeColor() {
		return gd.getNodeColor();
  }

  /**
   * @see de.unika.ipd.grgen.util.GraphDumpable#getNodeShape()
   */
  public int getNodeShape() {
    return gd.getNodeShape();
  }

  /**
   * @see de.unika.ipd.grgen.util.GraphDumpable#getNodeLabel()
   */
  public String getNodeLabel() {
    return gd.getNodeLabel();
  }

  /**
   * @see de.unika.ipd.grgen.util.GraphDumpable#getNodeInfo()
   */
  public String getNodeInfo() {
    return gd.getNodeInfo();
  }

  /**
   * @see de.unika.ipd.grgen.util.GraphDumpable#getEdgeLabel(int)
   */
  public String getEdgeLabel(int edge) {
    return gd.getEdgeLabel(edge);
  }

}
