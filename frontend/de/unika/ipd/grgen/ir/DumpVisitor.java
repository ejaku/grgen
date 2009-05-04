/*
 * GrGen: graph rewrite generator tool -- release GrGen.NET 2.5
 * Copyright (C) 2009 Universitaet Karlsruhe, Institut fuer Programmstrukturen und Datenorganisation, LS Goos
 * licensed under GPL v3 (see LICENSE.txt included in the packaging of this file)
 */

/**
 * @author shack
 * @version $Id$
 */
package de.unika.ipd.grgen.ir;

import java.util.Collection;
import java.util.HashMap;
import java.util.Iterator;
import java.util.Map;

import de.unika.ipd.grgen.util.GraphDumpVisitor;
import de.unika.ipd.grgen.util.GraphDumpable;
import de.unika.ipd.grgen.util.GraphDumpableProxy;
import de.unika.ipd.grgen.util.GraphDumper;
import de.unika.ipd.grgen.util.Walkable;

/**
 * A IR pretty graph dumper.
 */
public class DumpVisitor extends GraphDumpVisitor
{
	private class PrefixNode extends GraphDumpableProxy
	{
		private String prefix;

		public PrefixNode(GraphDumpable gd, String prefix)
		{
			super(gd);
			this.prefix = prefix;
		}

		/**
		 * @see de.unika.ipd.grgen.util.GraphDumpable#getNodeId()
		 */
		public String getNodeId()
		{
			return prefix + getGraphDumpable().getNodeId();
		}

		public String toString()
		{
			return getNodeId();
		}
	}

	private void dumpGraph(Graph gr, String prefix)
	{
		Map<Entity, DumpVisitor.PrefixNode> prefixMap = new HashMap<Entity, DumpVisitor.PrefixNode>();
		Collection<Node> nodes = gr.getNodes();

		dumper.beginSubgraph(gr);

		for (Iterator<Node> it = nodes.iterator(); it.hasNext();) {
			Node n = it.next();
			debug.report(NOTE, "node: " + n);
			PrefixNode pn = new PrefixNode(n, prefix);
			prefixMap.put(n, pn);
			dumper.node(pn);
		}

		Collection<Edge> edges = gr.getEdges();

		for (Iterator<Edge> it = edges.iterator(); it.hasNext();) {
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

			for (Collection<? extends GraphEntity> homSet : pg.getHomomorphic()) {
				if (!homSet.isEmpty()) {
					for (Iterator<? extends GraphEntity> homIt1 = homSet.iterator(); homIt1.hasNext(); ) {
						Entity hom1 = homIt1.next();
						for (Iterator<? extends GraphEntity> homIt2 = homSet.iterator(); homIt2.hasNext(); ) {
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
	public void visit(Walkable n)
	{
		assert n instanceof IR : "must have an ir object to dump";

		if (n instanceof Node || n instanceof Edge || n instanceof Graph) {
			return;
		}

		if (n instanceof Rule && ((Rule)n).getRight()!=null) {
			Rule r = (Rule) n;
			dumper.beginSubgraph(r);
			if(r.getRight()==null) {
				dumpGraph(r.getPattern(), "");
				dumper.endSubgraph();
			}
			dumpGraph(r.getLeft(), "l");
			dumpGraph(r.getRight(), "r");

			// Draw edges from left nodes that occur also on the right side.
			Iterator<Node> commonNodes = r.getCommonNodes().iterator();
			while (commonNodes.hasNext()) {
				Node node = commonNodes.next();
				PrefixNode left = new PrefixNode(node, "l");
				PrefixNode right = new PrefixNode(node, "r");

				dumper.edge(left, right, null, GraphDumper.DOTTED);
			}

			Iterator<Edge> commonEdges = r.getCommonEdges().iterator();
			while (commonEdges.hasNext()) {
				Edge edge = commonEdges.next();
				PrefixNode left = new PrefixNode(edge, "l");
				PrefixNode right = new PrefixNode(edge, "r");

				dumper.edge(left, right, null, GraphDumper.DOTTED);
			}

			// dump evalations
			//dumper.beginSubgraph(r);
			//dumper.endSubgraph();

			dumper.endSubgraph();
		} else {
			super.visit(n);
		}
	}
}
