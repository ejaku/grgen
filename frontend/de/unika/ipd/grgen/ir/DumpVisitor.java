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
package de.unika.ipd.grgen.ir;

import java.util.*;

import de.unika.ipd.grgen.util.GraphDumpVisitor;
import de.unika.ipd.grgen.util.GraphDumpable;
import de.unika.ipd.grgen.util.GraphDumpableProxy;
import de.unika.ipd.grgen.util.GraphDumper;
import de.unika.ipd.grgen.util.Walkable;

/**
 * A IR pretty graph dumper.
 */
public class DumpVisitor extends GraphDumpVisitor {
	
	private class PrefixNode extends GraphDumpableProxy {
		private String prefix;
		
		public PrefixNode(GraphDumpable gd, String prefix) {
			super(gd);
			this.prefix = prefix;
		}
		
		/**
		 * @see de.unika.ipd.grgen.util.GraphDumpable#getNodeId()
		 */
		public String getNodeId() {
			return prefix + getGraphDumpable().getNodeId();
		}
		
		public String toString() {
			return getNodeId();
		}
	}
	
	private void dumpGraph(Graph gr, String prefix) {
		Map<Entity, DumpVisitor.PrefixNode> prefixMap = new HashMap<Entity, DumpVisitor.PrefixNode>();
		Collection<Node> nodes = gr.getNodes();
		
		dumper.beginSubgraph(gr);
		
		for(Iterator<Node> it = nodes.iterator(); it.hasNext();) {
			Node n = it.next();
			debug.report(NOTE, "node: " + n);
			PrefixNode pn = new PrefixNode(n, prefix);
			prefixMap.put(n, pn);
			dumper.node(pn);
			
		}

		Collection<Edge> edges = gr.getEdges();
		
		for(Iterator<Edge> it = edges.iterator(); it.hasNext();) {
			Edge edge = (Edge) it.next();
			PrefixNode from, to, e;
			
			e = new PrefixNode(edge, prefix);
			prefixMap.put(edge, e);
			
			debug.report(NOTE, "true edge from: " + gr.getSource(edge)
										 + " to: " + gr.getTarget(edge));
			
			from = prefixMap.get(gr.getSource(edge));
			to = prefixMap.get(gr.getTarget(edge));
			
			debug.report(NOTE, "edge from: " + from + " to: " + to);
			
			dumper.node(e);
			dumper.edge(from, e);
			dumper.edge(e, to);
		}
		
		if (gr instanceof PatternGraph) {
			PatternGraph pg = (PatternGraph) gr;
			
			for(Collection<GraphEntity> homSet : pg.getHomomorphic()) {
				if(!homSet.isEmpty()) {
					for(Iterator<GraphEntity> homIt1 = homSet.iterator(); homIt1.hasNext();) {
						Entity hom1 = homIt1.next();
						for(Iterator<GraphEntity> homIt2 = homSet.iterator(); homIt2.hasNext();) {
							Entity hom2 = homIt2.next();
							PrefixNode from = prefixMap.get(hom1);
							PrefixNode to = prefixMap.get(hom2);
							dumper.edge(from, to, "hom", GraphDumper.DASHED);
						}
					}
				}
			}
		}
		
		dumper.endSubgraph();
	}
	
	/**
	 * @see de.unika.ipd.grgen.util.Visitor#visit(de.unika.ipd.grgen.util.Walkable)
	 */
	public void visit(Walkable n) {
		assert n instanceof IR : "must have an ir object to dump";
		
		if(n instanceof Node || n instanceof Edge || n instanceof Graph)
			return;
		
		if(n instanceof Test) {
			Test test = (Test) n;
			dumper.beginSubgraph(test);
			dumpGraph(test.getPattern(), "");
			dumper.endSubgraph();
			
		}
		else if(n instanceof Rule) {
			Rule r = (Rule) n;
			dumper.beginSubgraph(r);
			dumpGraph(r.getLeft(), "l");
			dumpGraph(r.getRight(), "r");
			
			// Draw edges from left nodes that occur also on the right side.
			Iterator<IR> common = r.getCommonNodes().iterator();
			while(common.hasNext()) {
				Node node = (Node) common.next();
				PrefixNode left = new PrefixNode(node, "l");
				PrefixNode right = new PrefixNode(node, "r");
				
				dumper.edge(left, right, null, GraphDumper.DOTTED);
			}
			
			common = r.getCommonEdges().iterator();
			while(common.hasNext()) {
				Edge edge = (Edge) common.next();
				PrefixNode left = new PrefixNode(edge, "l");
				PrefixNode right = new PrefixNode(edge, "r");
				
				dumper.edge(left, right, null, GraphDumper.DOTTED);
			}
			
			// dump evalations
			//dumper.beginSubgraph(r);
			//dumper.endSubgraph();
			
			dumper.endSubgraph();
		}
		else
			super.visit(n);
		
	}
	
	
}
