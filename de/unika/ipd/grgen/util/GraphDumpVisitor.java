/**
 * @file GraphDumpVisitor.java
 * @author shack
 * @date Jul 21, 2003
 */
package de.unika.ipd.grgen.util;

import java.util.Iterator;

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
		
		Iterator it = n.getWalkableChildren();
		for(int i = 0; it.hasNext(); i++) {
			GraphDumpable target = (GraphDumpable) it.next();
			dumper.edge(gd, target, gd.getEdgeLabel(i));
		}
  }

}
