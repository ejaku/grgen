/*
 * GrGen: graph rewrite generator tool -- release GrGen.NET 8.1
 * Copyright (C) 2003-2026 Universitaet Karlsruhe, Institut fuer Programmstrukturen und Datenorganisation, LS Goos; and free programmers
 * licensed under LGPL v3, some components/parts use different licenses (see LICENSE.txt included in the packaging of this file)
 * www.grgen.de / www.grgen.net
 */

/// <summary>
/// @author Sebastian Buchwald, Edgar Jakumeit
/// </summary>

namespace de.unika.ipd.grgen.ast.decl.pattern
{
	using System.Collections.Generic;
	using System.Diagnostics;

	using BaseNode = de.unika.ipd.grgen.ast.BaseNode;
	using de.unika.ipd.grgen.ast;
	using IdentNode = de.unika.ipd.grgen.ast.IdentNode;
	using DeclNode = de.unika.ipd.grgen.ast.decl.DeclNode;
	using ConnectionCharacter = de.unika.ipd.grgen.ast.pattern.ConnectionCharacter;
	using ConnectionNode = de.unika.ipd.grgen.ast.pattern.ConnectionNode;
	using PatternGraphRhsNode = de.unika.ipd.grgen.ast.pattern.PatternGraphRhsNode;
	using PatternGraphLhsNode = de.unika.ipd.grgen.ast.pattern.PatternGraphLhsNode;
	using SingleNodeConnNode = de.unika.ipd.grgen.ast.pattern.SingleNodeConnNode;
	using de.unika.ipd.grgen.ast.util;
	using de.unika.ipd.grgen.ast.util;
	using de.unika.ipd.grgen.ast.util;
	using Entity = de.unika.ipd.grgen.ir.Entity;
	using Edge = de.unika.ipd.grgen.ir.pattern.Edge;
	using Node = de.unika.ipd.grgen.ir.pattern.Node;
	using OrderedReplacement = de.unika.ipd.grgen.ir.pattern.OrderedReplacement;
	using OrderedReplacements = de.unika.ipd.grgen.ir.pattern.OrderedReplacements;
	using PatternGraphBase = de.unika.ipd.grgen.ir.pattern.PatternGraphBase;
	using PatternGraphLhs = de.unika.ipd.grgen.ir.pattern.PatternGraphLhs;
	using PatternGraphRhs = de.unika.ipd.grgen.ir.pattern.PatternGraphRhs;
	using SubpatternDependentReplacement = de.unika.ipd.grgen.ir.pattern.SubpatternDependentReplacement;
	using SubpatternUsage = de.unika.ipd.grgen.ir.pattern.SubpatternUsage;


	/// <summary>
	/// AST node for a modify right-hand side.
	/// </summary>
	public class ModifyDeclNode : RhsDeclNode
	{
		static ModifyDeclNode()
		{
			SetClassName(typeof(ModifyDeclNode), "modify declaration");
		}

		private CollectNode<IdentNode> deletesUnresolved;
		private CollectNode<DeclNode> deletes = new CollectNode<DeclNode>();


		/// <summary>
		/// Make a new modify right-hand side. </summary>
		/// <param name="id"> The identifier of this RHS. </param>
		/// <param name="patternGraph"> The right hand side graph. </param>
		public ModifyDeclNode(IdentNode id, PatternGraphRhsNode patternGraph, CollectNode<IdentNode> deletes)
			: base(id, patternGraph)
		{
			this.deletesUnresolved = deletes;
			BecomeParent(this.deletesUnresolved);
		}

		/// <summary>
		/// returns children of this node </summary>
		public override ICollection<BaseNode> Children
		{
			get
			{
				IList<BaseNode> children = new List<BaseNode>();
				children.Add(ident);
				children.Add(GetValidVersion(typeUnresolved, type));
				children.Add(patternGraph);
				children.Add(GetValidVersionCollectNode(deletesUnresolved, deletes));
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
				childrenNames.Add("ident");
				childrenNames.Add("type");
				childrenNames.Add("right");
				childrenNames.Add("delete");
				return childrenNames;
			}
		}

		private static readonly CollectTripleResolver<NodeDeclNode, EdgeDeclNode, SubpatternUsageDeclNode> deleteResolver =
			new CollectTripleResolver<NodeDeclNode, EdgeDeclNode, SubpatternUsageDeclNode>(
				new DeclarationTripleResolver<NodeDeclNode, EdgeDeclNode, SubpatternUsageDeclNode>(
					typeof(NodeDeclNode), typeof(EdgeDeclNode), typeof(SubpatternUsageDeclNode)));

		/// <seealso cref="de.unika.ipd.grgen.ast.BaseNode.resolveLocal() "/>
		protected internal override bool ResolveLocal()
		{
			Triple<CollectNode<NodeDeclNode>, CollectNode<EdgeDeclNode>, CollectNode<SubpatternUsageDeclNode>> resolve =
				deleteResolver.Resolve(deletesUnresolved);

			if(resolve != null)
			{
				if(resolve.first != null)
				{
					foreach(NodeDeclNode node in resolve.first.ChildrenExact)
						deletes.AddChild(node);
				}

				if(resolve.second != null)
				{
					foreach(EdgeDeclNode edge in resolve.second.ChildrenExact)
						deletes.AddChild(edge);
				}

				if(resolve.third != null)
				{
					foreach(SubpatternUsageDeclNode sub in resolve.third.ChildrenExact)
						deletes.AddChild(sub);
				}

				BecomeParent(deletes);
			}

			return base.ResolveLocal() && resolve != null;
		}

		public override bool CheckAgainstLhsPattern(PatternGraphLhsNode pattern)
		{
			WarnIfElementAppearsInsideAndOutsideOfDelete(pattern);
			return true;
		}

		public override PatternGraphRhs GetIRPatternGraph(PatternGraphLhs left)
		{
			PatternGraphRhs right = patternGraph.IRPatternGraphRhs;

			ISet<Entity> elementsToDelete = InsertElementsToDeleteToLhsIfNotFromLhs(left, right);

			InsertLhsElementsToRhs(left, elementsToDelete, right);

			InsertElementsFromTypeofToRhsIfNotYetContained(right, elementsToDelete);

			foreach(SubpatternUsage sub in left.SubpatternUsages)
			{
				if(!IsSubpatternRewritePartUsed(sub, right) && !IsSubpatternUsageToBeDeleted(sub))
					right.AddSubpatternUsage(sub); // keep subpattern
			}

			InsertElementsFromEvalsIntoRhs(left, right);
			InsertElementsFromOrderedReplacementsIntoRhs(left, right);

			return right;
		}

		private ISet<Entity> InsertElementsToDeleteToLhsIfNotFromLhs(PatternGraphLhs left, PatternGraphBase right)
		{
			HashSet<Entity> elementsToDelete = new HashSet<Entity>();

			foreach(DeclNode delete in deletes.ChildrenExact)
			{
				if(delete is SubpatternUsageDeclNode)
					continue;

				ConstraintDeclNode element = (ConstraintDeclNode)delete;
				Entity entity = element.CheckIR<Entity>(typeof(Entity));
				elementsToDelete.Add(entity);

				if(element.defEntityToBeYieldedTo)
					entity.PatternGraphDefYieldedIsToBeDeleted = right;

				if(entity is Node)
				{
					Node node = element.CheckIR<Node>(typeof(Node));
					if(!left.HasNode(node) && node.directlyNestingLHSGraph != left)
					{
						left.AddSingleNode(node);
						left.AddHomToAll(node);
					}
				}
				else
				{
					Edge edge = element.CheckIR<Edge>(typeof(Edge));
					if(!left.HasEdge(edge) && edge.directlyNestingLHSGraph != left)
					{
						left.AddSingleEdge(edge);
						left.AddHomToAll(edge);
					}
				}
			}

			return elementsToDelete;
		}

		// inserts to be kept nodes/edges and to be deleted nodes/edges, to be created nodes/edges are already contained
		private static void InsertLhsElementsToRhs(PatternGraphLhs left, ISet<Entity> elementsToDelete, PatternGraphRhs right)
		{
			foreach(Node lhsNode in left.Nodes)
			{
				if(!elementsToDelete.Contains(lhsNode))
					right.AddSingleNode(lhsNode);
				else
					right.AddDeletedElement(lhsNode);
			}
			foreach(Edge lhsEdge in left.Edges)
			{
				if(!elementsToDelete.Contains(lhsEdge)
					&& !elementsToDelete.Contains(left.GetSource(lhsEdge))
					&& !elementsToDelete.Contains(left.GetTarget(lhsEdge)))
				{
					right.AddConnection(left.GetSource(lhsEdge), lhsEdge, left.GetTarget(lhsEdge),
							lhsEdge.HasFixedDirection(), false, false);
				}
				else
					right.AddDeletedElement(lhsEdge);
			}
		}

		private static void InsertElementsFromTypeofToRhsIfNotYetContained(PatternGraphRhs right, ISet<Entity> elementsToDelete)
		{
			foreach(Node rhsNode in right.Nodes)
			{
				if(rhsNode.InheritsType())
				{
					Node nodeFromTypeof = (Node)rhsNode.Typeof;
					if(!elementsToDelete.Contains(nodeFromTypeof))
						right.AddNodeIfNotYetContained(nodeFromTypeof);
				}
			}
			foreach(Edge rhsEdge in right.Edges)
			{
				if(rhsEdge.InheritsType())
				{
					Edge edgeFromTypeof = (Edge)rhsEdge.Typeof;
					if(!elementsToDelete.Contains(edgeFromTypeof))
						right.AddEdgeIfNotYetContained(edgeFromTypeof);
				}
			}
		}

		private static bool IsSubpatternRewritePartUsed(SubpatternUsage sub, PatternGraphRhs right)
		{
			foreach(OrderedReplacements orderedRepls in right.OrderedReplacements)
			{
				foreach(OrderedReplacement orderedRepl in orderedRepls.orderedReplacements)
				{
					if(!(orderedRepl is SubpatternDependentReplacement))
						continue;

					SubpatternDependentReplacement subRepl = (SubpatternDependentReplacement)orderedRepl;
					if(sub == subRepl.SubpatternUsage)
						return true;
				}
			}
			return false;
		}

		private bool IsSubpatternUsageToBeDeleted(SubpatternUsage subpatternUsage)
		{
			foreach(DeclNode delete in deletes.ChildrenExact)
			{
				if(!(delete is SubpatternUsageDeclNode))
					continue;

				SubpatternUsage subpatternUsageToBeDeleted = delete.CheckIR<SubpatternUsage>(typeof(SubpatternUsage));
				if(subpatternUsage == subpatternUsageToBeDeleted)
					return true;
			}
			return false;
		}

		protected internal override ISet<ConstraintDeclNode> GetElementsToDeleteImpl(PatternGraphLhsNode pattern)
		{
			Debug.Assert(IsResolved());

			LinkedHashSet<ConstraintDeclNode> elementsToDelete = new LinkedHashSet<ConstraintDeclNode>();

			foreach(DeclNode delete in deletes.ChildrenExact)
			{
				if(!(delete is SubpatternUsageDeclNode))
					elementsToDelete.Add((ConstraintDeclNode)delete);
			}

			// add edges with deleted source or target
			foreach(ConnectionCharacter connectionCharacter in pattern.Connections)
			{
				if(!(connectionCharacter is ConnectionNode))
					continue;

				ConnectionNode connection = (ConnectionNode)connectionCharacter;
				if(elementsToDelete.Contains(connection.Src) || elementsToDelete.Contains(connection.Tgt))
					elementsToDelete.Add(connection.Edge);
			}
			foreach(ConnectionCharacter connectionCharacter in patternGraph.Connections)
			{
				if(!(connectionCharacter is ConnectionNode))
					continue;

				ConnectionNode connection = (ConnectionNode)connectionCharacter;
				if(elementsToDelete.Contains(connection.Src) || elementsToDelete.Contains(connection.Tgt))
					elementsToDelete.Add(connection.Edge);
			}

			return elementsToDelete;
		}

		protected internal override ISet<ConnectionNode> GetConnectionsToReuseImpl(PatternGraphLhsNode pattern)
		{
			ISet<ConnectionNode> connectionsToReuse = new LinkedHashSet<ConnectionNode>();

			ISet<EdgeDeclNode> lhsEdges = pattern.Edges;
			foreach(ConnectionCharacter connectionCharacter in patternGraph.Connections)
			{
				if(!(connectionCharacter is ConnectionNode))
					continue;

				ConnectionNode connection = (ConnectionNode)connectionCharacter;
				EdgeDeclNode rhsEdge = connection.Edge;
				while(rhsEdge is EdgeTypeChangeDeclNode)
					rhsEdge = ((EdgeTypeChangeDeclNode)rhsEdge).OldEdge;

				// add connection only if source and target are reused
				if(lhsEdges.Contains(rhsEdge) && !SourceOrTargetNodeIncluded(rhsEdge, pattern, deletes.ChildrenExact))
					connectionsToReuse.Add(connection);
			}

			foreach(ConnectionCharacter connectionCharacter in pattern.Connections)
			{
				if(!(connectionCharacter is ConnectionNode))
					continue;

				ConnectionNode connection = (ConnectionNode)connectionCharacter;
				EdgeDeclNode lhsEdge = connection.Edge;
				while(lhsEdge is EdgeTypeChangeDeclNode)
					lhsEdge = ((EdgeTypeChangeDeclNode)lhsEdge).OldEdge;

				// add connection only if source and target are reused
				if(!deletes.ChildrenExact.Contains(lhsEdge)
						&& !SourceOrTargetNodeIncluded(lhsEdge, pattern, deletes.ChildrenExact))
				{
					connectionsToReuse.Add(connection);
				}
			}

			return connectionsToReuse;
		}

		protected internal override ISet<NodeDeclNode> GetNodesToReuseImpl(PatternGraphLhsNode pattern)
		{
			LinkedHashSet<NodeDeclNode> nodesToReuse = new LinkedHashSet<NodeDeclNode>();

			ISet<NodeDeclNode> lhsNodes = pattern.Nodes;
			ISet<NodeDeclNode> rhsNodes = patternGraph.Nodes;
			foreach(NodeDeclNode lhsNode in lhsNodes)
			{
				if(rhsNodes.Contains(lhsNode) || !deletes.ChildrenExact.Contains(lhsNode))
					nodesToReuse.Add(lhsNode);
			}

			return nodesToReuse;
		}

		private void WarnIfElementAppearsInsideAndOutsideOfDelete(PatternGraphLhsNode pattern)
		{
			ISet<ConstraintDeclNode> elementsToDelete = GetElementsToDelete(pattern);

			ISet<ConstraintDeclNode> alreadyReported = new HashSet<ConstraintDeclNode>();
			foreach(ConnectionCharacter connectionCharacter in patternGraph.Connections)
			{
				ConstraintDeclNode element = null;
				if(connectionCharacter is SingleNodeConnNode)
				{
					SingleNodeConnNode singleNodeConnection = (SingleNodeConnNode)connectionCharacter;
					element = singleNodeConnection.Node;
				}
				else
				{ //if(connectionCharacter instanceof ConnectionNode)
					ConnectionNode connection = (ConnectionNode)connectionCharacter;
					element = connection.Edge;
				}

				if(alreadyReported.Contains(element))
					continue;

				foreach(ConstraintDeclNode elementToDelete in elementsToDelete)
				{
					if(element.Equals(elementToDelete))
					{
						if(element.defEntityToBeYieldedTo)
							continue;

						connectionCharacter.ReportWarning("\"" + elementToDelete + "\" appears inside as well as outside a delete statement");
						alreadyReported.Add(element);
					}
				}
			}
		}

		protected internal override ISet<ConnectionNode> GetConnectionsNotDeleted(PatternGraphLhsNode pattern)
		{
			ISet<ConnectionNode> connectionsNotDeleted = new LinkedHashSet<ConnectionNode>();

			ISet<ConstraintDeclNode> elementsToDelete = GetElementsToDelete(pattern);

			foreach(ConnectionCharacter connectionCharacter in pattern.Connections)
			{
				if(!(connectionCharacter is ConnectionNode))
					continue;

				ConnectionNode connection = (ConnectionNode)connectionCharacter;
				if(!elementsToDelete.Contains(connection.Edge)
						&& !elementsToDelete.Contains(connection.Src)
						&& !elementsToDelete.Contains(connection.Tgt))
				{
					connectionsNotDeleted.Add(connection);
				}
			}

			return connectionsNotDeleted;
		}
	}

}
