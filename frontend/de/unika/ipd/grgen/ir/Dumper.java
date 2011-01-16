/*
 * GrGen: graph rewrite generator tool -- release GrGen.NET 2.7
 * Copyright (C) 2003-2011 Universitaet Karlsruhe, Institut fuer Programmstrukturen und Datenorganisation, LS Goos
 * licensed under LGPL v3 (see LICENSE.txt included in the packaging of this file)
 * www.grgen.net
 */

/**
 * Dump.java
 *
 * @author Sebastian Hack
 */

package de.unika.ipd.grgen.ir;

import java.awt.Color;
import java.util.Collection;
import java.util.LinkedList;

import de.unika.ipd.grgen.util.Formatter;
import de.unika.ipd.grgen.util.GraphDumpable;
import de.unika.ipd.grgen.util.GraphDumper;
import de.unika.ipd.grgen.util.GraphDumperFactory;


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

		if(act instanceof Rule && ((Rule)act).getRight()!=null) {
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

		if(act instanceof Rule && ((Rule)act).getRight()!=null) {
			Rule r = (Rule) act;
			graphs.add(r.getRight());
			Collection<EvalStatement> evals = r.getEvals();

			if(!evals.isEmpty()) {
				gd.beginSubgraph("evals");
				gd.edge(r.getRight(), evals.iterator().next(), "eval", GraphDumper.DASHED, Color.GRAY);
			}

			EvalStatement oldEvalStatement = null;
			for(EvalStatement e : evals) {
				if(e instanceof Assignment) {
					Assignment a = (Assignment) e;
					Expression target = a.getTarget();
					Expression expr = a.getExpression();

					if(compactCondEval) {
						dump(a.getId(), Formatter.formatConditionEval(target) + " = " + Formatter.formatConditionEval(expr), gd);
						if(oldEvalStatement != null)
							gd.edge(oldEvalStatement, a, "next", GraphDumper.DASHED, Color.RED);
					} else {
						gd.node(a);
						gd.node(target);
						gd.edge(a, target);
						dump(expr, gd);
						gd.edge(a, expr);
					}
				}
				else if(e instanceof AssignmentVar) {
					// TODO
				}
				else if(e instanceof AssignmentVisited) {
					// TODO
				}
				else if(e instanceof AssignmentIdentical) {
					// TODO
				}
				else if(e instanceof CompoundAssignmentChanged) {
					// MAP TODO
				}
				else if(e instanceof CompoundAssignmentChangedVar) {
					// MAP TODO
				}
				else if(e instanceof CompoundAssignmentChangedVisited) {
					// MAP TODO
				}
				else if(e instanceof CompoundAssignment) {
					// MAP TODO
				}
				else if(e instanceof CompoundAssignmentVarChanged) {
					// MAP TODO
				}
				else if(e instanceof CompoundAssignmentVarChangedVar) {
					// MAP TODO
				}
				else if(e instanceof CompoundAssignmentVarChangedVisited) {
					// MAP TODO
				}
				else if(e instanceof CompoundAssignmentVar) {
					// MAP TODO
				}
				else if(e instanceof MapAddItem) {
					// MAP TODO
				}
				else if(e instanceof MapRemoveItem) {
					// MAP TODO
				}
				else if(e instanceof SetAddItem) {
					// MAP TODO
				}
				else if(e instanceof SetRemoveItem) {
					// MAP TODO
				}
				else if(e instanceof MapVarAddItem) {
					// MAP TODO
				}
				else if(e instanceof MapVarRemoveItem) {
					// MAP TODO
				}
				else if(e instanceof SetVarAddItem) {
					// MAP TODO
				}
				else if(e instanceof SetVarRemoveItem) {
					// MAP TODO
				}
				else {
					throw new UnsupportedOperationException("Unknown EvalStatement \"" + e + "\"");
				}
				oldEvalStatement = e;
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
		for(Action act : unit.getActionRules()) {
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

		for(Action obj : unit.getActionRules()) {
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

