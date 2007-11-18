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
 * @file GraphDumpVisitor.java
 * @author shack
 * @date Jul 21, 2003
 */
package de.unika.ipd.grgen.util;

/**
 * A visitor that dumps graphs
 * Every object that is visited must implement Walkable and GraphDumpable
 * @see GraphDumpable
 * @see Walkable
 */
public class GraphDumpVisitor extends Base implements Visitor {

	protected GraphDumper dumper;
	
	public GraphDumpVisitor(GraphDumper dumper) {
		this.dumper = dumper;
	}
	
	public GraphDumpVisitor() {
	}
	
	public void setDumper(GraphDumper dumper) {
		this.dumper = dumper;
	}

  /**
   * @see de.unika.ipd.grgen.ast.Visitor#visit(de.unika.ipd.grgen.ast.BaseNode)
   */
  public void visit(Walkable n) {
  	GraphDumpable gd = (GraphDumpable) n;
		dumper.node(gd);
		
		int i = 0;
		for(GraphDumpable target : n.getWalkableChildren()) {
			dumper.edge(gd, target, gd.getEdgeLabel(i));
			i++;
		}
  }

}
