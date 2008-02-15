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

import de.unika.ipd.grgen.util.Formatter;
import de.unika.ipd.grgen.util.GraphDumpable;
import de.unika.ipd.grgen.util.GraphDumper;
import de.unika.ipd.grgen.util.GraphDumperFactory;
import java.awt.Color;
import java.util.Collection;
import java.util.LinkedList;


/**
 * A custom dumper for the IR.
 */
public class Dumper {
	/** Draw edges between graphs. */
	private final boolean interGraphEdges;
	/** Draw cond and eval as string not as expression tree */
	private final boolean compactCondEval = true;
	
	
	/** The factory to get a dumper from. */
	private final GraphDumperFactory dumperFactory;
	
	public Dumper(GraphDumperFactory dumperFactory,
				  boolean interGraphEdges) {
		
		this.dumperFactory = dumperFactory;
		this.interGraphEdges = interGraphEdges;
	}
	
	private void dump(Graph g, GraphDumper gd) {
		gd.beginSubgraph(g);
		
		for(Node n : g.getNodes()) {
			gd.node(g.getLocalDumpable(n));
		}
		
		for(Edge e : g.getEdges()) {
			GraphDumpable edge = g.getLocalDumpable(e);
			GraphDumpable src = g.getLocalDumpable(g.getSource(e));
			GraphDumpable tgt = g.getLocalDumpable(g.getTarget(e));
			gd.node(edge);
			gd.edge(src, edge);
			gd.edge(edge, tgt);
		}
		
		if(g instanceof PatternGraph) {
			PatternGraph pg = (PatternGraph) g;
			Collection<Expression> conds = pg.getConditions();
			
			if(!conds.isEmpty()) {
				for(Expression expr : conds) {
					dump(expr, gd);
				}
			}
		}
		
		gd.endSubgraph();
	}
	
	public final void dump(MatchingAction act, GraphDumper gd) {
		PatternGraph pattern = act.getPattern();
		Collection<Graph> graphs = new LinkedList<Graph>();
		Graph right = null;
		
		if(act instanceof Rule) {
			right = ((Rule) act).getRight();
			graphs.add(right);
		}
		
		graphs.addAll(pattern.getNegs());
		
		gd.beginSubgraph(act);
		dump(pattern, gd);
		
		for(Graph g : graphs) {
			dump(g, gd);
			if(g == right)
				gd.edge(pattern, g, g.getNodeLabel().toLowerCase(), GraphDumper.DASHED, Color.green);
			else
				gd.edge(pattern, g, g.getNodeLabel().toLowerCase(), GraphDumper.DASHED, Color.red);
			
			if(interGraphEdges) {
				for(Node n : g.getNodes()) {
					if(pattern.hasNode(n))
						gd.edge(pattern.getLocalDumpable(n), g.getLocalDumpable(n), "",
								GraphDumper.DOTTED);
				}
				
				for(Edge e : g.getEdges()) {
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
			
			if(!evals.isEmpty()) {
				gd.beginSubgraph("evals");
				gd.edge(r.getRight(), evals.iterator().next(), "eval", GraphDumper.DASHED, Color.GRAY);
			}
			
			Assignment oldAsign = null;
			for(Assignment a : evals) {
				Qualification target = a.getTarget();
				Expression expr = a.getExpression();
				
				if(compactCondEval) {
					dump(a.getId(), Formatter.formatConditionEval(target) + " = " + Formatter.formatConditionEval(expr), gd);
					if(oldAsign != null)
						gd.edge(oldAsign, a, "next", GraphDumper.DASHED, Color.RED);
				} else {
					gd.node(a);
					gd.node(target);
					gd.edge(a, target);
					dump(expr, gd);
					gd.edge(a, expr);
				}
				oldAsign = a;
			}
			
			if(!evals.isEmpty())
				gd.endSubgraph();
		}
		
		gd.endSubgraph();
	}
	
	public final void dump(final String id, final String s, GraphDumper gd) {
		gd.node(new GraphDumpable() {
					public String getNodeId() {	return id; }
					
					public Color getNodeColor() { return Color.ORANGE; }
					
					public int getNodeShape() { return GraphDumper.BOX; }
					
					public String getNodeLabel() { return s; }
					
					public String getNodeInfo() { return null; }
					
					public String getEdgeLabel(int edge) { return null; }
				});
	}

	public final void dump(final String s, final String fromId,
			final String toId, GraphDumper gd) {
		gd.edge(new GraphDumpable() {
					public String getNodeId() {	return fromId; }
					
					public Color getNodeColor() { return Color.ORANGE; }
					
					public int getNodeShape() { return GraphDumper.BOX; }
					
					public String getNodeLabel() { return fromId; }
					
					public String getNodeInfo() { return null; }
					
					public String getEdgeLabel(int edge) { return null; }
				},
				new GraphDumpable() {
					public String getNodeId() {	return toId; }
					
					public Color getNodeColor() { return Color.ORANGE; }
					
					public int getNodeShape() { return GraphDumper.BOX; }
					
					public String getNodeLabel() { return fromId; }
					
					public String getNodeInfo() { return null; }
					
					public String getEdgeLabel(int edge) { return null; }
				}, s);
	}
	
	public final void dump(Expression expr, GraphDumper gd) {
		if(compactCondEval) {
			dump(expr.getId(), Formatter.formatConditionEval(expr), gd);
		} else {
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
	}
	
	public final void dumpComplete(Unit unit, String fileName) {
		GraphDumper curr = dumperFactory.get(fileName);
		
		curr.begin();
		for(Action act : unit.getActions()) {
			if(act instanceof MatchingAction) {
				MatchingAction mact = (MatchingAction) act;
				dump(mact, curr);
			}
		}
		
		curr.finish();
		
		curr = dumperFactory.get(fileName + "Model");
		curr.begin();
		
		for(Model model : unit.getModels()) {
			for(Type type : model.getTypes()) {
				String typeName = type.getIdent().toString(); 
				dump(typeName, typeName, curr);
			}
			for(Type type : model.getTypes()) {
				String typeName = type.getIdent().toString();
				if(type instanceof InheritanceType) {
					InheritanceType inhType = (InheritanceType) type;
					for(InheritanceType superType : inhType.getDirectSuperTypes()) {
						String superTypeName = superType.getIdent().toString();
						dump("", typeName, superTypeName, curr);
					}					
				}
			}
		}
		
		curr.finish();
	}
	
	public final void dump(Unit unit) {
		
		for(Action obj : unit.getActions()) {
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

