/**
 * Dump.java
 *
 * @author Sebastian Hack
 */

package de.unika.ipd.grgen.ir;

import java.util.Collection;
import java.util.Iterator;
import java.util.LinkedList;

import de.unika.ipd.grgen.util.GraphDumpable;
import de.unika.ipd.grgen.util.GraphDumper;
import de.unika.ipd.grgen.util.GraphDumperFactory;

public class Dumper {
	
	
	private final boolean interGraphEdges;
	
	private final GraphDumperFactory dumperFactory;
	
	public Dumper(GraphDumperFactory dumperFactory,
								boolean interGraphEdges) {
		
		this.dumperFactory = dumperFactory;
		this.interGraphEdges = interGraphEdges;
	}
	
	private final void dump(Graph g, GraphDumper gd) {
		gd.beginSubgraph(g);
		
		for(Iterator it = g.getNodes(); it.hasNext();) {
			Node n = (Node) it.next();
			gd.node(g.getLocalDumpable(n));
		}
		
		for(Iterator it = g.getEdges(); it.hasNext();) {
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
		Collection graphs = new LinkedList();
		
		
		if(act instanceof Rule)
			graphs.add(((Rule) act).getRight());
		
		for(Iterator it = act.getNegs(); it.hasNext();)
			graphs.add(it.next());
		
		gd.beginSubgraph(act);
		dump(pattern, gd);
		
		for(Iterator it = graphs.iterator(); it.hasNext();) {
			Graph g = (Graph) it.next();
			dump(g, gd);
			
			if(interGraphEdges) {
				for(Iterator nt = g.getNodes(); nt.hasNext();) {
					Node n = (Node) nt.next();
					if(pattern.hasNode(n))
						gd.edge(pattern.getLocalDumpable(n), g.getLocalDumpable(n), "",
										GraphDumper.DOTTED);
				}
				
				for(Iterator nt = g.getEdges(); nt.hasNext();) {
					Edge e = (Edge) nt.next();
					if(pattern.hasEdge(e))
						gd.edge(pattern.getLocalDumpable(e), g.getLocalDumpable(e), "",
										GraphDumper.DOTTED);
				}
			}
		}
		
		gd.endSubgraph();
	}
	
	public final void dump(Expression expr, GraphDumper gd) {
		gd.node(expr);
		if(expr instanceof Operator) {
			Operator op = (Operator) expr;
			for(int i = 0; i < op.operandCount(); i++) {
				Expression e = op.getOperand(i);
				dump(e, gd);
				gd.edge(expr, e);
			}
		}
	}
	
	public final void dumpComplete(Unit unit, String fileName) {
		GraphDumper curr = dumperFactory.get(fileName);
		
		curr.begin();
		for(Iterator it = unit.getActions(); it.hasNext();) {
			Object obj = it.next();
			
			if(obj instanceof MatchingAction) {
				MatchingAction act = (MatchingAction) obj;
				dump(act, curr);
			}
			
		}
		
		curr.finish();
	}
	
	public final void dump(Unit unit) {
		
		for(Iterator it = unit.getActions(); it.hasNext();) {
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

