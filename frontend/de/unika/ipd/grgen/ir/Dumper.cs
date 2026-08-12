/*
 * GrGen: graph rewrite generator tool -- release GrGen.NET 8.1
 * Copyright (C) 2003-2026 Universitaet Karlsruhe, Institut fuer Programmstrukturen und Datenorganisation, LS Goos; and free programmers
 * licensed under LGPL v3, some components/parts use different licenses (see LICENSE.txt included in the packaging of this file)
 * www.grgen.de / www.grgen.net
 */

/// <summary>
/// Dump.java
/// 
/// @author Sebastian Hack
/// </summary>

namespace de.unika.ipd.grgen.ir
{

	using System.Collections.Generic;

	using Action = de.unika.ipd.grgen.ir.executable.Action;
	using MatchingAction = de.unika.ipd.grgen.ir.executable.MatchingAction;
	using Rule = de.unika.ipd.grgen.ir.executable.Rule;
	using Expression = de.unika.ipd.grgen.ir.expr.Expression;
	using Operator = de.unika.ipd.grgen.ir.expr.Operator;
	using Model = de.unika.ipd.grgen.ir.model.Model;
	using InheritanceType = de.unika.ipd.grgen.ir.model.type.InheritanceType;
	using Edge = de.unika.ipd.grgen.ir.pattern.Edge;
	using PatternGraphBase = de.unika.ipd.grgen.ir.pattern.PatternGraphBase;
	using Node = de.unika.ipd.grgen.ir.pattern.Node;
	using PatternGraphLhs = de.unika.ipd.grgen.ir.pattern.PatternGraphLhs;
	using Assignment = de.unika.ipd.grgen.ir.stmt.Assignment;
	using EvalStatement = de.unika.ipd.grgen.ir.stmt.EvalStatement;
	using EvalStatements = de.unika.ipd.grgen.ir.stmt.EvalStatements;
	using Type = de.unika.ipd.grgen.ir.type.Type;
	using Formatter = de.unika.ipd.grgen.util.Formatter;
	using GraphDumpable = de.unika.ipd.grgen.util.GraphDumpable;
	using GraphDumper = de.unika.ipd.grgen.util.GraphDumper;
	using GraphDumperFactory = de.unika.ipd.grgen.util.GraphDumperFactory;
	using Color = de.unika.ipd.grgen.util.Color;

	/// <summary>
	/// A custom dumper for the IR.
	/// </summary>
	public class Dumper
	{
		/// <summary>
		/// Draw edges between graphs. </summary>
		private readonly bool interGraphEdges;
		/// <summary>
		/// Draw cond and eval as string not as expression tree </summary>
		private readonly bool compactCondEval = true;

		/// <summary>
		/// The factory to get a dumper from. </summary>
		private readonly GraphDumperFactory dumperFactory;

		public Dumper(GraphDumperFactory dumperFactory,
				bool interGraphEdges)
		{

			this.dumperFactory = dumperFactory;
			this.interGraphEdges = interGraphEdges;
		}

		private void Dump(PatternGraphBase patternGraph, GraphDumper dumper)
		{
			dumper.BeginSubgraph(patternGraph);

			foreach(Node node in patternGraph.Nodes)
			{
				GraphDumpable nodeDumpable = patternGraph.GetLocalDumpable(node);
				dumper.Node(nodeDumpable);
			}

			foreach(Edge edge in patternGraph.Edges)
			{
				GraphDumpable edgeDumpable = patternGraph.GetLocalDumpable(edge);
				GraphDumpable src = patternGraph.GetLocalDumpable(patternGraph.GetSource(edge));
				GraphDumpable tgt = patternGraph.GetLocalDumpable(patternGraph.GetTarget(edge));
				dumper.Node(edgeDumpable);
				dumper.Edge(src, edgeDumpable);
				dumper.Edge(edgeDumpable, tgt);
			}

			if(patternGraph is PatternGraphLhs)
			{
				PatternGraphLhs patternGraphLhs = (PatternGraphLhs)patternGraph;
				ICollection<Expression> conditions = patternGraphLhs.Conditions;

				if(conditions.Count > 0)
				{
					foreach(Expression expr in conditions)
						Dump(expr, dumper);
				}
			}

			dumper.EndSubgraph();
		}

		public void Dump(MatchingAction matchingAction, GraphDumper dumper)
		{
			PatternGraphLhs pattern = matchingAction.Pattern;
			List<PatternGraphBase> patternGraphs = new List<PatternGraphBase>();
			PatternGraphBase right = null;

			if(matchingAction is Rule && ((Rule)matchingAction).Right != null)
			{
				right = ((Rule)matchingAction).Right;
				patternGraphs.Add(right);
			}

			patternGraphs.AddRange(pattern.Negs);

			dumper.BeginSubgraph(matchingAction);
			Dump(pattern, dumper);

			foreach(PatternGraphBase patternGraph in patternGraphs)
			{
				Dump(patternGraph, dumper);
				if(patternGraph == right)
					dumper.Edge(pattern, patternGraph, patternGraph.NodeLabel.ToLower(), GraphDumper.DASHED, Color.GREEN);
				else
					dumper.Edge(pattern, patternGraph, patternGraph.NodeLabel.ToLower(), GraphDumper.DASHED, Color.RED);

				if(interGraphEdges)
				{
					foreach(Node node in patternGraph.Nodes)
					{
						if(pattern.HasNode(node))
							dumper.Edge(pattern.GetLocalDumpable(node), patternGraph.GetLocalDumpable(node), "",
									GraphDumper.DOTTED);
					}

					foreach(Edge edge in patternGraph.Edges)
					{
						if(pattern.HasEdge(edge))
							dumper.Edge(pattern.GetLocalDumpable(edge), patternGraph.GetLocalDumpable(edge), "",
									GraphDumper.DOTTED);
					}
				}
			}

			if(matchingAction is Rule && ((Rule)matchingAction).Right != null)
			{
				Rule rule = (Rule)matchingAction;
				patternGraphs.Add(rule.Right);
				ICollection<EvalStatement> evals = new List<EvalStatement>();
				foreach(EvalStatements evalStatements in rule.Evals)
				{
					foreach(EvalStatement evalStatement in evalStatements.evalStatements)
						evals.Add(evalStatement);
				}

				if(evals.Count > 0)
				{
					dumper.BeginSubgraph("evals");
					dumper.Edge(rule.Right, evals.GetEnumerator().Next(), "eval", GraphDumper.DASHED, Color.GRAY);
				}

				EvalStatement oldEvalStatement = null;
				foreach(EvalStatement eval in evals)
				{
					if(eval is Assignment)
					{
						Assignment assignment = (Assignment)eval;
						Expression target = assignment.Target;
						Expression expr = assignment.Expression;

						if(compactCondEval)
						{
							Dump(assignment.Id,
									Formatter.FormatConditionEval(target) + " = " + Formatter.FormatConditionEval(expr),
									dumper);
							if(oldEvalStatement != null)
								dumper.Edge(oldEvalStatement, assignment, "next", GraphDumper.DASHED, Color.RED);
						}
						else
						{
							dumper.Node(assignment);
							dumper.Node(target);
							dumper.Edge(assignment, target);
							Dump(expr, dumper);
							dumper.Edge(assignment, expr);
						}
					}
					else
					{
						// just swallow, it's on the ones who need this to re-enable and implement
						// throw new UnsupportedOperationException("Unknown EvalStatement \"" + e + "\"");
					}
					oldEvalStatement = eval;
				}

				if(evals.Count > 0)
					dumper.EndSubgraph();
			}

			dumper.EndSubgraph();
		}

		public static void Dump(in string id, in string s, GraphDumper dumper)
		{
			dumper.Node(new GraphDumpableAnonymousInnerClass(id, s));
		}

		private class GraphDumpableAnonymousInnerClass : GraphDumpable
		{
			private string id;
			private string s;

			public GraphDumpableAnonymousInnerClass(string id, string s)
			{
				this.id = id;
				this.s = s;
			}

			public string NodeId
			{
				get
				{
					return id;
				}
			}

			public Color NodeColor
			{
				get
				{
					return Color.ORANGE;
				}
			}

			public int NodeShape
			{
				get
				{
					return GraphDumper.BOX;
				}
			}

			public string NodeLabel
			{
				get
				{
					return s;
				}
			}

			public string NodeInfo
			{
				get
				{
					return null;
				}
			}

			public string getEdgeLabel(int edge)
			{
				return null;
			}
		}

		public static void Dump(in string s, in string fromId,
				in string toId, GraphDumper dumper)
		{
			dumper.Edge(new GraphDumpableAnonymousInnerClass2(fromId)
				, new GraphDumpableAnonymousInnerClass3(fromId, toId)
				, s);
		}

		private class GraphDumpableAnonymousInnerClass2 : GraphDumpable
		{
			private string fromId;

			public GraphDumpableAnonymousInnerClass2(string fromId)
			{
				this.fromId = fromId;
			}

			public string NodeId
			{
				get
				{
					return fromId;
				}
			}

			public Color NodeColor
			{
				get
				{
					return Color.ORANGE;
				}
			}

			public int NodeShape
			{
				get
				{
					return GraphDumper.BOX;
				}
			}

			public string NodeLabel
			{
				get
				{
					return fromId;
				}
			}

			public string NodeInfo
			{
				get
				{
					return null;
				}
			}

			public string getEdgeLabel(int edge)
			{
				return null;
			}
		}

		private class GraphDumpableAnonymousInnerClass3 : GraphDumpable
		{
			private string fromId;
			private string toId;

			public GraphDumpableAnonymousInnerClass3(string fromId, string toId)
			{
				this.fromId = fromId;
				this.toId = toId;
			}

			public string NodeId
			{
				get
				{
					return toId;
				}
			}

			public Color NodeColor
			{
				get
				{
					return Color.ORANGE;
				}
			}

			public int NodeShape
			{
				get
				{
					return GraphDumper.BOX;
				}
			}

			public string NodeLabel
			{
				get
				{
					return fromId;
				}
			}

			public string NodeInfo
			{
				get
				{
					return null;
				}
			}

			public string getEdgeLabel(int edge)
			{
				return null;
			}
		}

		public void Dump(Expression expr, GraphDumper dumper)
		{
			if(compactCondEval)
				Dump(expr.Id, Formatter.FormatConditionEval(expr), dumper);
			else
			{
				dumper.Node(expr);
				if(expr is Operator)
				{
					Operator op = (Operator)expr;
					for(int i = 0; i < op.Arity(); i++)
					{
						Expression operand = op.GetOperand(i);
						Dump(operand, dumper);
						dumper.Edge(expr, operand);
					}
				}
			}
		}

		public void DumpComplete(Unit unit, string fileName)
		{
			GraphDumper dumper = dumperFactory.Get(fileName);

			dumper.Begin();
			foreach(Action act in unit.ActionRules)
			{
				if(act is MatchingAction)
				{
					MatchingAction mact = (MatchingAction)act;
					Dump(mact, dumper);
				}
			}

			dumper.Finish();

			dumper = dumperFactory.Get(fileName + "Model");
			dumper.Begin();

			foreach(Model model in unit.Models)
			{
				foreach(Type type in model.Types)
				{
					string typeName = type.Ident.ToString();
					Dump(typeName, typeName, dumper);
				}
				foreach(Type type in model.Types)
				{
					string typeName = type.Ident.ToString();
					if(type is InheritanceType)
					{
						InheritanceType inhType = (InheritanceType)type;
						foreach(InheritanceType superType in inhType.DirectSuperTypes)
						{
							string superTypeName = superType.Ident.ToString();
							Dump("", typeName, superTypeName, dumper);
						}
					}
				}
			}

			dumper.Finish();
		}

		public void Dump(Unit unit)
		{
			foreach(Action act in unit.ActionRules)
			{
				if(act is MatchingAction)
				{
					MatchingAction matchingAction = (MatchingAction)act;
					string main = matchingAction.ToString().Replace(' ', '_');

					GraphDumper dumper = dumperFactory.Get(main);

					dumper.Begin();
					Dump(matchingAction, dumper);
					dumper.Finish();
				}
			}
		}
	}

}
