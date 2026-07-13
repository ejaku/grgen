/*
 * GrGen: graph rewrite generator tool -- release GrGen.NET 8.1
 * Copyright (C) 2003-2026 Universitaet Karlsruhe, Institut fuer Programmstrukturen und Datenorganisation, LS Goos; and free programmers
 * licensed under LGPL v3, some components/parts use different licenses (see LICENSE.txt included in the packaging of this file)
 * www.grgen.de / www.grgen.net
 */

/**
 * @author shack
 */

package de.unika.ipd.grgen.ir;

import java.util.Collection;
import java.util.HashMap;
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

		for(Node node : nodes) {
			debug.report(NOTE, "node: " + node);
			PrefixNode prefixNode = new PrefixNode(node, prefix);
			prefixMap.put(node, prefixNode);
			dumper.node(prefixNode);
		}

		Collection<Edge> edges = patternGraph.getEdges();

		for(Edge edge : edges) {
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

			for(Collection<GraphEntity> homSet : patternGraphLhs.getHomomorphic()) {
				if(!homSet.isEmpty()) {
					for(Entity hom1 : homSet) {
						for(Entity hom2 : homSet) {
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
			for(Node node : rule.getCommonNodes()) {
				PrefixNode prefixLeft = new PrefixNode(node, "l");
				PrefixNode prefixRight = new PrefixNode(node, "r");

				dumper.edge(prefixLeft, prefixRight, null, GraphDumper.DOTTED);
			}

			for(Edge edge : rule.getCommonEdges()) {
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
