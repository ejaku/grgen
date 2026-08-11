/*
 * GrGen: graph rewrite generator tool -- release GrGen.NET 8.1
 * Copyright (C) 2003-2026 Universitaet Karlsruhe, Institut fuer Programmstrukturen und Datenorganisation, LS Goos; and free programmers
 * licensed under LGPL v3, some components/parts use different licenses (see LICENSE.txt included in the packaging of this file)
 * www.grgen.de / www.grgen.net
 */

/// <summary>
/// @author shack
/// </summary>
namespace de.unika.ipd.grgen.ast.pattern
{
	using System;
	using System.Collections.Generic;

	using BaseNode = de.unika.ipd.grgen.ast.BaseNode;
	using de.unika.ipd.grgen.ast;
	using EdgeDeclNode = de.unika.ipd.grgen.ast.decl.pattern.EdgeDeclNode;
	using EdgeTypeChangeDeclNode = de.unika.ipd.grgen.ast.decl.pattern.EdgeTypeChangeDeclNode;
	using NodeDeclNode = de.unika.ipd.grgen.ast.decl.pattern.NodeDeclNode;
	using NodeTypeChangeDeclNode = de.unika.ipd.grgen.ast.decl.pattern.NodeTypeChangeDeclNode;
	using SubpatternUsageDeclNode = de.unika.ipd.grgen.ast.decl.pattern.SubpatternUsageDeclNode;
	using VarDeclNode = de.unika.ipd.grgen.ast.decl.pattern.VarDeclNode;
	using ExprNode = de.unika.ipd.grgen.ast.expr.ExprNode;
	using EvalStatementNode = de.unika.ipd.grgen.ast.stmt.EvalStatementNode;
	using EvalStatementsNode = de.unika.ipd.grgen.ast.stmt.EvalStatementsNode;
	using de.unika.ipd.grgen.util.collection;
	using IR = de.unika.ipd.grgen.ir.IR;
	using EvalStatements = de.unika.ipd.grgen.ir.stmt.EvalStatements;
	using ImperativeStmt = de.unika.ipd.grgen.ir.stmt.ImperativeStmt;
	using Edge = de.unika.ipd.grgen.ir.pattern.Edge;
	using Node = de.unika.ipd.grgen.ir.pattern.Node;
	using OrderedReplacements = de.unika.ipd.grgen.ir.pattern.OrderedReplacements;
	using PatternGraphRhs = de.unika.ipd.grgen.ir.pattern.PatternGraphRhs;
	using SubpatternUsage = de.unika.ipd.grgen.ir.pattern.SubpatternUsage;
	using Variable = de.unika.ipd.grgen.ir.pattern.Variable;
	using Coords = de.unika.ipd.grgen.parser.Coords;

	/// <summary>
	/// AST node that represents a graph pattern as it appears within the rewrite part of some rule
	/// </summary>
	public class PatternGraphRhsNode : PatternGraphBaseNode
	{
		static PatternGraphRhsNode()
		{
			SetClassName(typeof(PatternGraphRhsNode), "pattern graph rhs");
		}

		protected internal CollectNode<SubpatternReplNode> subpatternRepls;
		public CollectNode<EvalStatementsNode> evals;
		protected internal CollectNode<OrderedReplacementsNode> orderedReplacements;
		public CollectNode<BaseNode> imperativeStmts;


		/// <summary>
		/// A new pattern node </summary>
		/// <param name="connections"> A collection containing connection nodes </param>
		public PatternGraphRhsNode(string nameOfGraph, Coords coords,
				CollectNode<BaseNode> connections, CollectNode<BaseNode> @params,
				CollectNode<SubpatternUsageDeclNode> subpatterns, CollectNode<SubpatternReplNode> subpatternRepls,
				CollectNode<OrderedReplacementsNode> orderedReplacements, CollectNode<ExprNode> returns,
				CollectNode<BaseNode> imperativeStmts, int context, PatternGraphLhsNode directlyNestingLHSGraph)
			: base(nameOfGraph, coords, connections, @params, subpatterns, returns, context)
		{
			this.subpatternRepls = subpatternRepls;
			BecomeParent(this.subpatternRepls);
			this.orderedReplacements = orderedReplacements;
			BecomeParent(this.orderedReplacements);
			this.imperativeStmts = imperativeStmts;
			BecomeParent(imperativeStmts);
			this.context = context;

			this.directlyNestingLHSGraph = directlyNestingLHSGraph;
			if(@params != null)
				AddParamsToConnections(@params); // treat non-var parameters like connections
		}

		public virtual void AddEvals(CollectNode<EvalStatementsNode> evals)
		{
			this.evals = evals;
			BecomeParent(this.evals);
		}

		/// <summary>
		/// returns children of this node </summary>
		public override ICollection<BaseNode> Children
		{
			get
			{
				IList<BaseNode> children = new List<BaseNode>();
				children.Add(GetValidVersionCollectNode(connectionsUnresolved, connections));
				children.Add(@params);
				children.Add(defVariablesToBeYieldedTo);
				children.Add(subpatterns);
				children.Add(subpatternRepls);
				children.Add(orderedReplacements);
				children.Add(evals);
				children.Add(returns);
				children.Add(imperativeStmts);
				return children;
			}
		}

		/// <summary>
		/// returns names of the children, same order as in getChildren </summary>
		public override ICollection<string> ChildrenNames
		{
			get
			{
				IList<string> childrenNames = new List<string>();
				childrenNames.Add("connections");
				childrenNames.Add("params");
				childrenNames.Add("defVariablesToBeYieldedTo");
				childrenNames.Add("subpatterns");
				childrenNames.Add("subpatternReplacements");
				childrenNames.Add("orderedReplacements");
				childrenNames.Add("evals");
				childrenNames.Add("returns");
				childrenNames.Add("imperativeStmts");
				return childrenNames;
			}
		}

		/// <seealso cref="de.unika.ipd.grgen.ast.BaseNode.resolveLocal() "/>
		protected internal override bool ResolveLocal()
		{
			ReplaceSubpatternReplacementsIntoOrderedReplacements();

			return base.ResolveLocal();
		}

		// replace subpattern replacement node placeholder just specifying position in ordered list 
		// by subpattern replacement node from unordered list with correct arguments
		// move missing replacement nodes to the begin of the ordered list, it is the base list for further processing
		private void ReplaceSubpatternReplacementsIntoOrderedReplacements()
		{
			IList<SubpatternReplNode> subpatternReplsToDelete = new List<SubpatternReplNode>();
			foreach(SubpatternReplNode subpatternRepl in subpatternRepls.ChildrenExact)
			{
				foreach(OrderedReplacementsNode orderedRepls in orderedReplacements.ChildrenExact)
				{
					if(orderedRepls.ChildrenExact.Count > 0)
					{
						IList<OrderedReplacementNode> orderedReplsToDelete = new List<OrderedReplacementNode>();
						IEnumerator<OrderedReplacementNode> subCand = orderedRepls.ChildrenExact.GetEnumerator();
	// JAVA TO C# CONVERTER TASK: Java iterators are only converted within the context of 'while' and 'for' loops:
						OrderedReplacementNode orderedRepl = subCand.Next();
						if(orderedRepl is SubpatternReplNode)
						{
							SubpatternReplNode orderedSubpatternRepl = (SubpatternReplNode)orderedRepl;
							string orderedSubpatternReplName = orderedSubpatternRepl.SubpatternIdent.ToString();
							string subpatternReplName = subpatternRepl.SubpatternIdent.ToString();
							if(orderedSubpatternReplName.Equals(subpatternReplName))
							{
								orderedReplsToDelete.Add(orderedRepl);
								orderedRepls.AddChild(subpatternRepl);
								subpatternReplsToDelete.Add(subpatternRepl);
							}
						}
						orderedRepls.ChildrenExact.RemoveAll(orderedReplsToDelete);
					}
				}
			}
			subpatternRepls.ChildrenExact.RemoveAll(subpatternReplsToDelete);
			for(int i = subpatternRepls.ChildrenExact.Count - 1; i >= 0; --i)
			{
				SubpatternReplNode subpatternRepl = subpatternRepls.Get(i);
				OrderedReplacementsNode orderedRepls = new OrderedReplacementsNode(subpatternRepl.Coords,
						subpatternRepl.SubpatternIdent.IRIdent.ToString());
				orderedRepls.AddChild(subpatternRepl);
				orderedReplacements.AddChildAtFront(orderedRepls);
			}
			subpatternRepls.ChildrenExact.Clear();
		}

		/// <summary>
		/// A pattern node contains just a collect node with connection nodes as its children. </summary>
		/// <seealso cref="de.unika.ipd.grgen.ast.BaseNode.checkLocal()"/>
		protected internal override bool CheckLocal()
		{
			return IsEdgeReuseOk() && NoExecStatementInEvalHere();
		}

		internal virtual bool NoExecStatementInEvalHere()
		{
			bool result = true;
			foreach(OrderedReplacementsNode orderedRepls in orderedReplacements.ChildrenExact)
				result &= orderedRepls.NoExecStatement();
			return result;
		}

		public virtual Pair<bool, NodeTypeChangeDeclNode> NoAmbiguousRetypes(NodeDeclNode node, NodeTypeChangeDeclNode retypeOfNode)
		{
			foreach(NodeDeclNode retypeCandidate in Nodes)
			{
				if(!(retypeCandidate is NodeTypeChangeDeclNode))
					continue;
				NodeTypeChangeDeclNode retype = (NodeTypeChangeDeclNode)retypeCandidate;
				bool mergeeIsSame = false;
				foreach(NodeDeclNode mergee in retype.Mergees)
				{
					if(mergee == node)
					{
						mergeeIsSame = true;
						break;
					}
				}
				if(retype.OldNode == node || mergeeIsSame)
				{
					if(retypeOfNode == null)
						retypeOfNode = retype;
					else
					{
						retype.ReportError("Two (and hence ambiguous) retype (/merge) statements for the same node are forbidden,"
								+ " other retype (/merge) statement at " + retypeOfNode.Coords + " (retyping node " + node.Ident + ").");
						return new Pair<bool, NodeTypeChangeDeclNode>(Convert.ToBoolean(false), retypeOfNode);
					}
				}
			}
			return new Pair<bool, NodeTypeChangeDeclNode>(Convert.ToBoolean(true), retypeOfNode);
		}

		public virtual Pair<bool, EdgeTypeChangeDeclNode> NoAmbiguousRetypes(EdgeDeclNode edge, EdgeTypeChangeDeclNode retypeOfEdge)
		{
			foreach(EdgeDeclNode retypeCandidate in Edges)
			{
				if(!(retypeCandidate is EdgeTypeChangeDeclNode))
					continue;
				EdgeTypeChangeDeclNode retype = (EdgeTypeChangeDeclNode)retypeCandidate;
				if(retype.OldEdge == edge)
				{
					if(retypeOfEdge == null)
						retypeOfEdge = retype;
					else
					{
						retype.ReportError("Two (and hence ambiguous) retype statements for the same edge are forbidden,"
								+ " other retype statement at " + retypeOfEdge.Coords + " (retyping edge " + edge.Ident + ").");
						return new Pair<bool, EdgeTypeChangeDeclNode>(Convert.ToBoolean(false), retypeOfEdge);
					}
				}
			}
			return new Pair<bool, EdgeTypeChangeDeclNode>(Convert.ToBoolean(true), retypeOfEdge);
		}

		protected internal virtual bool IteratedNotReferenced(string iterName)
		{
			bool res = true;
			foreach(EvalStatementsNode evalStatements in evals.ChildrenExact)
			{
				foreach(EvalStatementNode evalStatement in evalStatements.ChildrenExact)
					res &= evalStatement.IteratedNotReferenced(iterName);
			}
			return res;
		}

		protected internal virtual bool IteratedNotReferencedInDefElementInitialization(string iterName)
		{
			bool res = true;
			foreach(VarDeclNode var in defVariablesToBeYieldedTo.ChildrenExact)
			{
				if(var.initialization != null)
					res &= var.initialization.IteratedNotReferenced(iterName);
			}
			return res;
		}

		/// <summary>
		/// Get the correctly casted IR object. </summary>
		/// <returns> The IR object. </returns>
		public virtual PatternGraphRhs IRPatternGraphRhs
		{
			get
			{
				return CheckIR<PatternGraphRhs>(typeof(PatternGraphRhs));
			}
		}

		/// <summary>
		/// Construct the IR object.
		/// It is a pattern graph and all the connections (children of the pattern AST node) are put into it. </summary>
		/// <seealso cref="de.unika.ipd.grgen.ast.BaseNode.constructIR()"/>
		protected internal override IR ConstructIR()
		{
			PatternGraphRhs patternGraph = new PatternGraphRhs(nameOfGraph);
			patternGraph.DirectlyNestingLHSGraph = directlyNestingLHSGraph.IRPatternGraphLhs;

			foreach(ConnectionCharacter connection in connections.ChildrenExact)
				connection.AddToGraph(patternGraph);

			foreach(VarDeclNode var in defVariablesToBeYieldedTo.ChildrenExact)
				patternGraph.AddVariable(var.CheckIR<Variable>(typeof(Variable)));

			foreach(SubpatternUsageDeclNode subUsage in subpatterns.ChildrenExact)
				patternGraph.AddSubpatternUsage(subUsage.CheckIR<SubpatternUsage>(typeof(SubpatternUsage)));

			foreach(OrderedReplacementsNode orderedRepls in orderedReplacements.ChildrenExact)
				patternGraph.AddOrderedReplacement((OrderedReplacements)orderedRepls.IR);

			// add subpattern usage connection elements only mentioned there to the IR
			// (they're declared in an enclosing pattern graph and locally only show up in the subpattern usage connection)
			foreach(OrderedReplacementsNode orderedRepls in orderedReplacements.ChildrenExact)
				PatternGraphBuilder.AddSubpatternReplacementUsageArguments(patternGraph, orderedRepls);

			// don't add elements only mentioned in ordered replacements here to the pattern, it prevents them from being deleted
			// in general we must be cautious with adding stuff to rhs because of that problem

			// don't add elements only mentioned in typeof here to the pattern, it prevents them from being deleted
			// in general we must be cautious with adding stuff to rhs because of that problem

			ISet<Node> nodesToAdd = new HashSet<Node>();
			ISet<Edge> edgesToAdd = new HashSet<Edge>();

			// add elements which we could not be added before because their container was iterated over
			foreach(Node node in nodesToAdd)
				patternGraph.AddNodeIfNotYetContained(node);
			foreach(Edge edge in edgesToAdd)
				patternGraph.AddEdgeIfNotYetContained(edge);

			foreach(BaseNode imperativeStmt in imperativeStmts.ChildrenExact)
				patternGraph.AddImperativeStmt((ImperativeStmt)imperativeStmt.IR);

			// add deferred exec elements only mentioned there to the IR
			// (they're declared in an enclosing pattern graph and locally only show up in the deferred exec)
			foreach(ImperativeStmt imperativeStmt in patternGraph.ImperativeStmts)
				PatternGraphBuilder.AddElementsUsedInDeferredExec(patternGraph, imperativeStmt);

			// ensure def to be yielded to elements are hom to all others
			// so backend doing some fake search planning for them is not scheduling checks for them
			foreach(Node node in patternGraph.Nodes)
			{
				if(node.IsDefToBeYieldedTo())
					patternGraph.AddHomToAll(node);
			}
			foreach(Edge edge in patternGraph.Edges)
			{
				if(edge.IsDefToBeYieldedTo())
					patternGraph.AddHomToAll(edge);
			}

			return patternGraph;
		}

		public virtual ICollection<OrderedReplacements> OrderedReplacements
		{
			get
			{
				ICollection<OrderedReplacements> ret = new List<OrderedReplacements>();

				foreach(OrderedReplacementsNode orderedRepls in orderedReplacements.ChildrenExact)
					ret.Add(orderedRepls.CheckIR<OrderedReplacements>(typeof(OrderedReplacements)));

				return ret;
			}
		}

		public virtual ICollection<EvalStatements> EvalStatements
		{
			get
			{
				ICollection<EvalStatements> ret = new List<EvalStatements>();

				foreach(EvalStatementsNode evalStatements in evals.ChildrenExact)
					ret.Add(evalStatements.CheckIR<EvalStatements>(typeof(EvalStatements)));

				return ret;
			}
		}
	}

}
