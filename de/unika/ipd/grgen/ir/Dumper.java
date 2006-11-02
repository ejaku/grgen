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
 * Dump.java
 *
 * @author Sebastian Hack
 */

package de.unika.ipd.grgen.ir;

import de.unika.ipd.grgen.util.GraphDumpable;
import de.unika.ipd.grgen.util.GraphDumper;
import de.unika.ipd.grgen.util.GraphDumperFactory;
import java.util.Collection;
import java.util.Iterator;
import java.util.LinkedList;


/**
 * A custim dumper for the IR.
 */
public class Dumper {
	
	/** Draw edges between graphs. */
	private final boolean interGraphEdges;
	
	/** The factory to get a dumper from. */
	private final GraphDumperFactory dumperFactory;
	
	public Dumper(GraphDumperFactory dumperFactory,
				  boolean interGraphEdges) {
		
		this.dumperFactory = dumperFactory;
		this.interGraphEdges = interGraphEdges;
	}
	
	private final void dump(Graph g, GraphDumper gd) {
		gd.beginSubgraph(g);
		
		for(Iterator it = g.getNodes().iterator(); it.hasNext();) {
			Node n = (Node) it.next();
			gd.node(g.getLocalDumpable(n));
		}
		
		for(Iterator it = g.getEdges().iterator(); it.hasNext();) {
			Edge e = (Edge) it.next();
			GraphDumpable edge = g.getLocalDumpable(e);
			GraphDumpable src = g.getLocalDumpable(g.getSource(e));
			GraphDumpable tgt = g.getLocalDumpable(g.getTarget(e));
			gd.node(edge);
			gd.edge(src, edge);
			gd.edge(edge, tgt);
		}
		
		if(g instanceof PatternGraph) {
			PatternGraph pg = (PatternGraph) g;
			Collection conds = pg.getConditions();
			
			if(!conds.isEmpty()) {
				for(Iterator i = conds.iterator(); i.hasNext();) {
					Expression expr = (Expression) i.next();
					dump(expr, gd);
				}
			}
		}
		
		gd.endSubgraph();
	}
	
	public final void dump(MatchingAction act, GraphDumper gd) {
		Graph pattern = act.getPattern();
		Collection<Graph> graphs = new LinkedList<Graph>();
		
		if(act instanceof Rule) {
			Rule r = (Rule) act;
			graphs.add(r.getRight());
		}
		
		graphs.addAll(act.getNegs());
		
		gd.beginSubgraph(act);
		dump(pattern, gd);
		
		for(Iterator<Graph> it = graphs.iterator(); it.hasNext();) {
			Graph g = it.next();
			dump(g, gd);
			
			if(interGraphEdges) {
				for(Iterator nt = g.getNodes().iterator(); nt.hasNext();) {
					Node n = (Node) nt.next();
					if(pattern.hasNode(n))
						gd.edge(pattern.getLocalDumpable(n), g.getLocalDumpable(n), "",
								GraphDumper.DOTTED);
				}
				
				for(Iterator nt = g.getEdges().iterator(); nt.hasNext();) {
					Edge e = (Edge) nt.next();
					if(pattern.hasEdge(e))
						gd.edge(pattern.getLocalDumpable(e), g.getLocalDumpable(e), "",
								GraphDumper.DOTTED);
				}
			}
		}
		
		if(act instanceof Rule) {
			Rule r = (Rule) act;
			graphs.add(r.getRight());
			Collection<Assignment> evals = r.getEvals();
			
			if(!evals.isEmpty())
				gd.beginSubgraph("evals");
			
			for(Assignment a : evals) {
				Qualification target = a.getTarget();
				Expression expr = a.getExpression();
				
				gd.node(a);
				gd.node(target);
				gd.edge(a, target);
				dump(expr, gd);
				gd.edge(a, expr);
			}
			
			if(!evals.isEmpty())
				gd.endSubgraph();
		}
		
		gd.endSubgraph();
	}
	
	public final void dump(Expression expr, GraphDumper gd) {
		gd.node(expr);
		if(expr instanceof Operator) {
			Operator op = (Operator) expr;
			for(int i = 0; i < op.arity(); i++) {
				Expression e = op.getOperand(i);
				dump(e, gd);
				gd.edge(expr, e);
			}
		}
	}
	
	public final void dumpComplete(Unit unit, String fileName) {
		GraphDumper curr = dumperFactory.get(fileName);
		
		curr.begin();
		for(Iterator<Action> it = unit.getActions().iterator(); it.hasNext();) {
			Object obj = it.next();
			
			if(obj instanceof MatchingAction) {
				MatchingAction act = (MatchingAction) obj;
				dump(act, curr);
			}
			
		}
		
		curr.finish();
	}
	
	public final void dump(Unit unit) {
		
		for(Iterator<Action> it = unit.getActions().iterator(); it.hasNext();) {
			Object obj = it.next();
			
			if(obj instanceof MatchingAction) {
				MatchingAction act = (MatchingAction) obj;
				String main = act.toString().replace(' ', '_');
				
				GraphDumper curr = dumperFactory.get(main);
				
				curr.begin();
				dump(act, curr);
				curr.finish();
			}
		}
	}
	
}

