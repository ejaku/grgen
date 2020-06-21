/*
 * GrGen: graph rewrite generator tool -- release GrGen.NET 5.0
 * Copyright (C) 2003-2020 Universitaet Karlsruhe, Institut fuer Programmstrukturen und Datenorganisation, LS Goos; and free programmers
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

import de.unika.ipd.grgen.ir.executable.Action;
import de.unika.ipd.grgen.ir.executable.MatchingAction;
import de.unika.ipd.grgen.ir.executable.Rule;
import de.unika.ipd.grgen.ir.expr.Expression;
import de.unika.ipd.grgen.ir.expr.Operator;
import de.unika.ipd.grgen.ir.model.Model;
import de.unika.ipd.grgen.ir.model.type.InheritanceType;
import de.unika.ipd.grgen.ir.pattern.Edge;
import de.unika.ipd.grgen.ir.pattern.PatternGraphBase;
import de.unika.ipd.grgen.ir.pattern.Node;
import de.unika.ipd.grgen.ir.pattern.PatternGraphLhs;
import de.unika.ipd.grgen.ir.stmt.Assignment;
import de.unika.ipd.grgen.ir.stmt.EvalStatement;
import de.unika.ipd.grgen.ir.stmt.EvalStatements;
import de.unika.ipd.grgen.ir.type.Type;
import de.unika.ipd.grgen.util.Formatter;
import de.unika.ipd.grgen.util.GraphDumpable;
import de.unika.ipd.grgen.util.GraphDumper;
import de.unika.ipd.grgen.util.GraphDumperFactory;

/**
 * A custom dumper for the IR.
 */
public class Dumper
{
	/** Draw edges between graphs. */
	private final boolean interGraphEdges;
	/** Draw cond and eval as string not as expression tree */
	private final boolean compactCondEval = true;

	/** The factory to get a dumper from. */
	private final GraphDumperFactory dumperFactory;

	public Dumper(GraphDumperFactory dumperFactory,
			boolean interGraphEdges)
	{

		this.dumperFactory = dumperFactory;
		this.interGraphEdges = interGraphEdges;
	}

	private void dump(PatternGraphBase patternGraph, GraphDumper dumper)
	{
		dumper.beginSubgraph(patternGraph);

		for(Node node : patternGraph.getNodes()) {
			GraphDumpable nodeDumpable = patternGraph.getLocalDumpable(node);
			dumper.node(nodeDumpable);
		}

		for(Edge edge : patternGraph.getEdges()) {
			GraphDumpable edgeDumpable = patternGraph.getLocalDumpable(edge);
			GraphDumpable src = patternGraph.getLocalDumpable(patternGraph.getSource(edge));
			GraphDumpable tgt = patternGraph.getLocalDumpable(patternGraph.getTarget(edge));
			dumper.node(edgeDumpable);
			dumper.edge(src, edgeDumpable);
			dumper.edge(edgeDumpable, tgt);
		}

		if(patternGraph instanceof PatternGraphLhs) {
			PatternGraphLhs patternGraphLhs = (PatternGraphLhs)patternGraph;
			Collection<Expression> conditions = patternGraphLhs.getConditions();

			if(!conditions.isEmpty()) {
				for(Expression expr : conditions) {
					dump(expr, dumper);
				}
			}
		}

		dumper.endSubgraph();
	}

	public final void dump(MatchingAction matchingAction, GraphDumper dumper)
	{
		PatternGraphLhs pattern = matchingAction.getPattern();
		Collection<PatternGraphBase> patternGraphs = new LinkedList<PatternGraphBase>();
		PatternGraphBase right = null;

		if(matchingAction instanceof Rule && ((Rule)matchingAction).getRight() != null) {
			right = ((Rule)matchingAction).getRight();
			patternGraphs.add(right);
		}

		patternGraphs.addAll(pattern.getNegs());

		dumper.beginSubgraph(matchingAction);
		dump(pattern, dumper);

		for(PatternGraphBase patternGraph : patternGraphs) {
			dump(patternGraph, dumper);
			if(patternGraph == right)
				dumper.edge(pattern, patternGraph, patternGraph.getNodeLabel().toLowerCase(), GraphDumper.DASHED, Color.green);
			else
				dumper.edge(pattern, patternGraph, patternGraph.getNodeLabel().toLowerCase(), GraphDumper.DASHED, Color.red);

			if(interGraphEdges) {
				for(Node node : patternGraph.getNodes()) {
					if(pattern.hasNode(node))
						dumper.edge(pattern.getLocalDumpable(node), patternGraph.getLocalDumpable(node), "",
								GraphDumper.DOTTED);
				}

				for(Edge edge : patternGraph.getEdges()) {
					if(pattern.hasEdge(edge))
						dumper.edge(pattern.getLocalDumpable(edge), patternGraph.getLocalDumpable(edge), "",
								GraphDumper.DOTTED);
				}
			}
		}

		if(matchingAction instanceof Rule && ((Rule)matchingAction).getRight() != null) {
			Rule rule = (Rule)matchingAction;
			patternGraphs.add(rule.getRight());
			Collection<EvalStatement> evals = new LinkedList<EvalStatement>();
			for(EvalStatements evalStatements : rule.getEvals()) {
				for(EvalStatement evalStatement : evalStatements.evalStatements) {
					evals.add(evalStatement);
				}
			}

			if(!evals.isEmpty()) {
				dumper.beginSubgraph("evals");
				dumper.edge(rule.getRight(), evals.iterator().next(), "eval", GraphDumper.DASHED, Color.GRAY);
			}

			EvalStatement oldEvalStatement = null;
			for(EvalStatement eval : evals) {
				if(eval instanceof Assignment) {
					Assignment assignment = (Assignment)eval;
					Expression target = assignment.getTarget();
					Expression expr = assignment.getExpression();

					if(compactCondEval) {
						dump(assignment.getId(),
								Formatter.formatConditionEval(target) + " = " + Formatter.formatConditionEval(expr),
								dumper);
						if(oldEvalStatement != null)
							dumper.edge(oldEvalStatement, assignment, "next", GraphDumper.DASHED, Color.RED);
					} else {
						dumper.node(assignment);
						dumper.node(target);
						dumper.edge(assignment, target);
						dump(expr, dumper);
						dumper.edge(assignment, expr);
					}
				} else {
					// just swallow, it's on the ones who need this to re-enable and implement
					// throw new UnsupportedOperationException("Unknown EvalStatement \"" + e + "\"");
				}
				oldEvalStatement = eval;
			}

			if(!evals.isEmpty())
				dumper.endSubgraph();
		}

		dumper.endSubgraph();
	}

	public static final void dump(final String id, final String s, GraphDumper dumper)
	{
		dumper.node(
			new GraphDumpable() {
				@Override
				public String getNodeId()
				{
					return id;
				}
	
				@Override
				public Color getNodeColor()
				{
					return Color.ORANGE;
				}
	
				@Override
				public int getNodeShape()
				{
					return GraphDumper.BOX;
				}
	
				@Override
				public String getNodeLabel()
				{
					return s;
				}
	
				@Override
				public String getNodeInfo()
				{
					return null;
				}
	
				@Override
				public String getEdgeLabel(int edge)
				{
					return null;
				}
			}
		);
	}

	public static final void dump(final String s, final String fromId,
			final String toId, GraphDumper dumper)
	{
		dumper.edge(
			new GraphDumpable() {
				@Override
				public String getNodeId()
				{
					return fromId;
				}
	
				@Override
				public Color getNodeColor()
				{
					return Color.ORANGE;
				}
	
				@Override
				public int getNodeShape()
				{
					return GraphDumper.BOX;
				}
	
				@Override
				public String getNodeLabel()
				{
					return fromId;
				}
	
				@Override
				public String getNodeInfo()
				{
					return null;
				}
	
				@Override
				public String getEdgeLabel(int edge)
				{
					return null;
				}
			},
			new GraphDumpable() {
				@Override
				public String getNodeId()
				{
					return toId;
				}
	
				@Override
				public Color getNodeColor()
				{
					return Color.ORANGE;
				}
	
				@Override
				public int getNodeShape()
				{
					return GraphDumper.BOX;
				}
	
				@Override
				public String getNodeLabel()
				{
					return fromId;
				}
	
				@Override
				public String getNodeInfo()
				{
					return null;
				}
	
				@Override
				public String getEdgeLabel(int edge)
				{
					return null;
				}
			},
			s
		);
	}

	public final void dump(Expression expr, GraphDumper dumper)
	{
		if(compactCondEval) {
			dump(expr.getId(), Formatter.formatConditionEval(expr), dumper);
		} else {
			dumper.node(expr);
			if(expr instanceof Operator) {
				Operator op = (Operator)expr;
				for(int i = 0; i < op.arity(); i++) {
					Expression operand = op.getOperand(i);
					dump(operand, dumper);
					dumper.edge(expr, operand);
				}
			}
		}
	}

	public final void dumpComplete(Unit unit, String fileName)
	{
		GraphDumper dumper = dumperFactory.get(fileName);

		dumper.begin();
		for(Action act : unit.getActionRules()) {
			if(act instanceof MatchingAction) {
				MatchingAction mact = (MatchingAction)act;
				dump(mact, dumper);
			}
		}

		dumper.finish();

		dumper = dumperFactory.get(fileName + "Model");
		dumper.begin();

		for(Model model : unit.getModels()) {
			for(Type type : model.getTypes()) {
				String typeName = type.getIdent().toString();
				dump(typeName, typeName, dumper);
			}
			for(Type type : model.getTypes()) {
				String typeName = type.getIdent().toString();
				if(type instanceof InheritanceType) {
					InheritanceType inhType = (InheritanceType)type;
					for(InheritanceType superType : inhType.getDirectSuperTypes()) {
						String superTypeName = superType.getIdent().toString();
						dump("", typeName, superTypeName, dumper);
					}
				}
			}
		}

		dumper.finish();
	}

	public final void dump(Unit unit)
	{
		for(Action act : unit.getActionRules()) {
			if(act instanceof MatchingAction) {
				MatchingAction matchingAction = (MatchingAction)act;
				String main = matchingAction.toString().replace(' ', '_');

				GraphDumper dumper = dumperFactory.get(main);

				dumper.begin();
				dump(matchingAction, dumper);
				dumper.finish();
			}
		}
	}
}
