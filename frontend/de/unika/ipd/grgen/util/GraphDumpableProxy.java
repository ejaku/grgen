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
