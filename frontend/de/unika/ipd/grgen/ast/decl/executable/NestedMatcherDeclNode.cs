/*
 * GrGen: graph rewrite generator tool -- release GrGen.NET 8.1
 * Copyright (C) 2003-2026 Universitaet Karlsruhe, Institut fuer Programmstrukturen und Datenorganisation, LS Goos; and free programmers
 * licensed under LGPL v3, some components/parts use different licenses (see LICENSE.txt included in the packaging of this file)
 * www.grgen.de / www.grgen.net
 */

namespace de.unika.ipd.grgen.ast.decl.executable
{
	using System.Collections.Generic;

	using BaseNode = de.unika.ipd.grgen.ast.BaseNode;
	using IdentNode = de.unika.ipd.grgen.ast.IdentNode;
	using EdgeDeclNode = de.unika.ipd.grgen.ast.decl.pattern.EdgeDeclNode;
	using NodeDeclNode = de.unika.ipd.grgen.ast.decl.pattern.NodeDeclNode;
	using RhsDeclNode = de.unika.ipd.grgen.ast.decl.pattern.RhsDeclNode;
	using PatternGraphLhsNode = de.unika.ipd.grgen.ast.pattern.PatternGraphLhsNode;
	using TypeNode = de.unika.ipd.grgen.ast.type.TypeNode;


	/// <summary>
	/// Base class for nested pattern matching related ast nodes
	/// </summary>
	public abstract class NestedMatcherDeclNode : MatcherDeclNode
	{
		public RhsDeclNode right;

		public NestedMatcherDeclNode(IdentNode id, TypeNode type, PatternGraphLhsNode left, RhsDeclNode right)
			: base(id, type, left)
		{

			this.right = right;
			BecomeParent(this.right);
		}

		protected internal override bool CheckLocal()
		{
			bool nonActionIsOk = base.CheckNonAction(right);
			bool abstr = true;
			if(right != null)
				abstr = NoAbstractElementInstantiatedNestedPattern(right);
			return nonActionIsOk & abstr;
		}

		protected internal virtual bool NoAbstractElementInstantiatedNestedPattern(RhsDeclNode right)
		{
			bool abstr = true;

			foreach(NodeDeclNode node in right.patternGraph.Nodes)
			{
				if(!node.InheritsType() && node.DeclInhType.IsAbstract())
				{
					if((node.context & CONTEXT_PARAMETER) == CONTEXT_PARAMETER)
						continue;
					for(PatternGraphLhsNode pattern = this.pattern; pattern != null; pattern = GetParentPatternGraph(pattern))
					{
						if(pattern.Nodes.Contains(node))
							goto nodeAbstrLoopContinue;
					}
					node.ReportError("Instances of abstract node classes are not allowed (node" + node.EmptyWhenAnonymousPostfix(" ")
							+ " is declared with the abstract type " + node.DeclType.ToStringWithDeclarationCoords() + ").");
					abstr = false;
				}
		nodeAbstrLoopContinue:;
			}
	nodeAbstrLoopBreak:

			foreach(EdgeDeclNode edge in right.patternGraph.Edges)
			{
				if(!edge.InheritsType() && edge.DeclInhType.IsAbstract())
				{
					if((edge.context & CONTEXT_PARAMETER) == CONTEXT_PARAMETER)
						continue;
					for(PatternGraphLhsNode pattern = this.pattern; pattern != null; pattern = GetParentPatternGraph(pattern))
					{
						if(pattern.Edges.Contains(edge))
							goto edgeAbstrLoopContinue;
					}
					edge.ReportError("Instances of abstract edge classes are not allowed (edge" + edge.EmptyWhenAnonymousPostfix(" ")
							+ " is declared with the abstract type " + edge.DeclType.ToStringWithDeclarationCoords() + ").");
					abstr = false;
				}
		edgeAbstrLoopContinue:;
			}
	edgeAbstrLoopBreak:

			return abstr;
		}

		private static PatternGraphLhsNode GetParentPatternGraph(PatternGraphLhsNode pattern)
		{
			if(pattern == null)
				return null;

			LinkedList<ICollection<BaseNode>> queue = new LinkedList<ICollection<BaseNode>>();
			for(ICollection<BaseNode> parents = pattern.Parents; parents != null; parents = queue.First.Value, queue.RemoveFirst())
			{
				foreach(BaseNode parent in parents)
				{
					if(parent is PatternGraphLhsNode)
						return (PatternGraphLhsNode)parent;
					ICollection<BaseNode> grandParents = parent.Parents;
					if(grandParents != null && grandParents.Count > 0)
						queue.AddLast(grandParents);
				}
			}

			return null;
		}
	}

}
