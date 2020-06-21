/*
 * GrGen: graph rewrite generator tool -- release GrGen.NET 5.0
 * Copyright (C) 2003-2020 Universitaet Karlsruhe, Institut fuer Programmstrukturen und Datenorganisation, LS Goos; and free programmers
 * licensed under LGPL v3 (see LICENSE.txt included in the packaging of this file)
 * www.grgen.net
 */

/**
 * @author shack
 */

package de.unika.ipd.grgen.ir;

import java.util.Collection;
import java.util.HashMap;
import java.util.Iterator;
import java.util.Map;

import de.unika.ipd.grgen.ir.executable.Rule;
import de.unika.ipd.grgen.ir.pattern.Edge;
import de.unika.ipd.grgen.ir.pattern.PatternGraphBase;
import de.unika.ipd.grgen.ir.pattern.GraphEntity;
import de.unika.ipd.grgen.ir.pattern.Node;
import de.unika.ipd.grgen.ir.pattern.PatternGraphLhs;
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

		public PrefixNode(GraphDumpable dumpable, String prefix)
		{
			super(dumpable);
			this.prefix = prefix;
		}

		/**
		 * @see de.unika.ipd.grgen.util.GraphDumpable#getNodeId()
		 */
		@Override
		public String getNodeId()
		{
			return prefix + getGraphDumpable().getNodeId();
		}

		@Override
		public String toString()
		{
			return getNodeId();
		}
	}

	private void dumpGraph(PatternGraphBase patternGraph, String prefix)
	{
		Map<Entity, DumpVisitor.PrefixNode> prefixMap = new HashMap<Entity, DumpVisitor.PrefixNode>();
		Collection<Node> nodes = patternGraph.getNodes();

		dumper.beginSubgraph(patternGraph);

		for(Iterator<Node> it = nodes.iterator(); it.hasNext();) {
			Node node = it.next();
			debug.report(NOTE, "node: " + node);
			PrefixNode prefixNode = new PrefixNode(node, prefix);
			prefixMap.put(node, prefixNode);
			dumper.node(prefixNode);
		}

		Collection<Edge> edges = patternGraph.getEdges();

		for(Iterator<Edge> it = edges.iterator(); it.hasNext();) {
			Edge edge = it.next();
			PrefixNode prefixFrom, prefixTo, prefixEdge;

			prefixEdge = new PrefixNode(edge, prefix);
			prefixMap.put(edge, prefixEdge);

			debug.report(NOTE, "true edge from: " + patternGraph.getSource(edge)
					+ " to: " + patternGraph.getTarget(edge));

			prefixFrom = prefixMap.get(patternGraph.getSource(edge));
			prefixTo = prefixMap.get(patternGraph.getTarget(edge));

			debug.report(NOTE, "edge from: " + prefixFrom + " to: " + prefixTo);

			dumper.node(prefixEdge);
			dumper.edge(prefixFrom, prefixEdge);
			dumper.edge(prefixEdge, prefixTo);
		}

		if(patternGraph instanceof PatternGraphLhs) {
			PatternGraphLhs patternGraphLhs = (PatternGraphLhs)patternGraph;

			for(Collection<? extends GraphEntity> homSet : patternGraphLhs.getHomomorphic()) {
				if(!homSet.isEmpty()) {
					for(Iterator<? extends GraphEntity> homIt1 = homSet.iterator(); homIt1.hasNext();) {
						Entity hom1 = homIt1.next();
						for(Iterator<? extends GraphEntity> homIt2 = homSet.iterator(); homIt2.hasNext();) {
							Entity hom2 = homIt2.next();
							PrefixNode prefixFrom = prefixMap.get(hom1);
							PrefixNode prefixTo = prefixMap.get(hom2);
							dumper.edge(prefixFrom, prefixTo, "hom", GraphDumper.DASHED);
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
	@Override
	public void visit(Walkable walkable)
	{
		assert walkable instanceof IR : "must have an ir object to dump";

		if(walkable instanceof Node || walkable instanceof Edge || walkable instanceof PatternGraphBase) {
			return;
		}

		if(walkable instanceof Rule && ((Rule)walkable).getRight() != null) {
			Rule rule = (Rule)walkable;
			dumper.beginSubgraph(rule);
			if(rule.getRight() == null) {
				dumpGraph(rule.getPattern(), "");
				dumper.endSubgraph();
			}
			dumpGraph(rule.getLeft(), "l");
			dumpGraph(rule.getRight(), "r");

			// Draw edges from left nodes that occur also on the right side.
			Iterator<Node> commonNodes = rule.getCommonNodes().iterator();
			while(commonNodes.hasNext()) {
				Node node = commonNodes.next();
				PrefixNode prefixLeft = new PrefixNode(node, "l");
				PrefixNode prefixRight = new PrefixNode(node, "r");

				dumper.edge(prefixLeft, prefixRight, null, GraphDumper.DOTTED);
			}

			Iterator<Edge> commonEdges = rule.getCommonEdges().iterator();
			while(commonEdges.hasNext()) {
				Edge edge = commonEdges.next();
				PrefixNode prefixLeft = new PrefixNode(edge, "l");
				PrefixNode prefixRight = new PrefixNode(edge, "r");

				dumper.edge(prefixLeft, prefixRight, null, GraphDumper.DOTTED);
			}

			// dump evalations
			//dumper.beginSubgraph(r);
			//dumper.endSubgraph();

			dumper.endSubgraph();
		} else {
			super.visit(walkable);
		}
	}
}
