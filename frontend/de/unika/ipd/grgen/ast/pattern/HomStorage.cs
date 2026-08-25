/*
 * GrGen: graph rewrite generator tool -- release GrGen.NET 8.1
 * Copyright (C) 2003-2026 Universitaet Karlsruhe, Institut fuer Programmstrukturen und Datenorganisation, LS Goos; and free programmers
 * licensed under LGPL v3, some components/parts use different licenses (see LICENSE.txt included in the packaging of this file)
 * www.grgen.de / www.grgen.net
 */

/// <summary>
/// HomStorage.java
/// 
/// @author Sebastian Buchwald, Edgar Jakumeit
/// </summary>

namespace de.unika.ipd.grgen.ast.pattern
{

	using System.Collections.Generic;

	using BaseNode = de.unika.ipd.grgen.ast.BaseNode;
	using ConstraintDeclNode = de.unika.ipd.grgen.ast.decl.pattern.ConstraintDeclNode;
	using EdgeDeclNode = de.unika.ipd.grgen.ast.decl.pattern.EdgeDeclNode;
	using NodeDeclNode = de.unika.ipd.grgen.ast.decl.pattern.NodeDeclNode;

	/// <summary>
	/// AST node that stores node/edge is-homomorphic-to information
	/// </summary>
	public class HomStorage
	{
		/// <summary>
		/// Stores the sets of homomorphic elements (not equivalent to the contents of the hom statements) </summary>
		private ICollection<ISet<ConstraintDeclNode>> homSets =
			new LinkedHashSet<ISet<ConstraintDeclNode>>();

		/// <summary>
		/// Map an edge to its homomorphic set. </summary>
		private IDictionary<EdgeDeclNode, ISet<EdgeDeclNode>> edgeToHomEdges =
			new LinkedHashMap<EdgeDeclNode, ISet<EdgeDeclNode>>();

		/// <summary>
		/// Map a node to its homomorphic set. </summary>
		private IDictionary<NodeDeclNode, ISet<NodeDeclNode>> nodeToHomNodes =
			new LinkedHashMap<NodeDeclNode, ISet<NodeDeclNode>>();

		private ISet<NodeDeclNode> emptyHomNodeSet = new LinkedHashSet<NodeDeclNode>();
		private ISet<EdgeDeclNode> emptyHomEdgeSet = new LinkedHashSet<EdgeDeclNode>();


		// Don't call in PatternGraphNode constructor / until all pattern graphs were constructed
		// as it accesses the parents of the pattern graph
		public HomStorage(PatternGraphLhsNode patternGraph)
		{
			// fill with own homomorphic sets
			if(patternGraph.IsIdentification())
			{
				// Split one hom statement into two parts, so deleted and reuse nodes/edges can't be matched homomorphically.
				// This behavior is required for DPO-semantic / more exactly the identification condition.
				ISet<ConstraintDeclNode> deletedEntities = patternGraph.Rule.DeletedElements;
				foreach(HomNode homNode in patternGraph.homs.ChildrenExact)
				{
					ISet<ConstraintDeclNode> deleteHomSet = GetDeleteHomSet(homNode.Children, deletedEntities);
					AddIfNonTrivialHomSet(homSets, deleteHomSet);
					ISet<ConstraintDeclNode> reuseHomSet = GetReuseHomSet(homNode.Children, deletedEntities);
					AddIfNonTrivialHomSet(homSets, reuseHomSet);
				}
			}
			else
			{
				foreach(HomNode homNode in patternGraph.homs.ChildrenExact)
				{
					ISet<ConstraintDeclNode> homSet = GetHomSet(homNode.Children);
					AddIfNonTrivialHomSet(homSets, homSet);
				}
			}

			// then add inherited homomorphic sets
			for(PatternGraphLhsNode parent = patternGraph.ParentPatternGraph; parent != null;
					parent = parent.ParentPatternGraph)
			{
				foreach(ISet<ConstraintDeclNode> parentHomSet in parent.Homs)
				{
					ISet<ConstraintDeclNode> inheritedHomSet = GetInheritedHomSet(parentHomSet,
							patternGraph.Nodes, patternGraph.Edges);
					AddIfNonTrivialHomSet(homSets, inheritedHomSet);
				}
			}

			InitElementsToHomElements(patternGraph.Nodes, patternGraph.Edges);
		}

		private static ISet<ConstraintDeclNode> GetDeleteHomSet<T1>(ICollection<T1> homChildren,
				ISet<ConstraintDeclNode> deletedElements) where T1 : de.unika.ipd.grgen.ast.BaseNode
		{
			// homs between deleted entities
			HashSet<ConstraintDeclNode> deleteHomSet = new HashSet<ConstraintDeclNode>();

			foreach(BaseNode homChild in homChildren)
			{
				ConstraintDeclNode decl = (ConstraintDeclNode)homChild;
				if(deletedElements.Contains(decl))
					deleteHomSet.Add(decl);
			}

			return deleteHomSet;
		}

		private static ISet<ConstraintDeclNode> GetReuseHomSet<T1>(ICollection<T1> homChildren,
				ISet<ConstraintDeclNode> deletedElements) where T1 : de.unika.ipd.grgen.ast.BaseNode
		{
			// homs between reused entities
			HashSet<ConstraintDeclNode> reuseHomSet = new HashSet<ConstraintDeclNode>();

			foreach(BaseNode homChild in homChildren)
			{
				ConstraintDeclNode decl = (ConstraintDeclNode)homChild;
				if(!deletedElements.Contains(decl))
					reuseHomSet.Add(decl);
			}

			return reuseHomSet;
		}

		private static ISet<ConstraintDeclNode> GetHomSet<T1>(ICollection<T1> homChildren) where T1 : de.unika.ipd.grgen.ast.BaseNode
		{
			// simply the entities from the hom statements
			ISet<ConstraintDeclNode> homSet = new LinkedHashSet<ConstraintDeclNode>();

			foreach(BaseNode homChild in homChildren)
			{
				ConstraintDeclNode decl = (ConstraintDeclNode)homChild;
				homSet.Add(decl);
			}

			return homSet;
		}

		private static ISet<ConstraintDeclNode> GetInheritedHomSet(ISet<ConstraintDeclNode> parentHomSet,
				ISet<NodeDeclNode> nodes, ISet<EdgeDeclNode> edges)
		{
			ISet<ConstraintDeclNode> inheritedHomSet = new LinkedHashSet<ConstraintDeclNode>();

			foreach(ConstraintDeclNode homParent in parentHomSet)
			{
				if(homParent is NodeDeclNode) // note that either all elements in the homSet are nodes or all elements in the homSet are edges
				{
					NodeDeclNode homNode = (NodeDeclNode)homParent;
					if(nodes.Contains(homNode))
						inheritedHomSet.Add(homNode);
				}
				else
				{
					EdgeDeclNode homEdge = (EdgeDeclNode)homParent;
					if(edges.Contains(homEdge))
						inheritedHomSet.Add(homEdge);
				}
			}

			return inheritedHomSet;
		}

		private static void AddIfNonTrivialHomSet(ICollection<ISet<ConstraintDeclNode>> collectionToAddTo,
				ISet<ConstraintDeclNode> setToAdd)
		{
			if(setToAdd.Count > 1)
				collectionToAddTo.Add(setToAdd);
		}

		private void InitElementsToHomElements(ISet<NodeDeclNode> nodes, ISet<EdgeDeclNode> edges)
		{
			// Each node is homomorphic to itself (trivial hom).
			foreach(NodeDeclNode node in nodes)
			{
				ISet<NodeDeclNode> homSet = new LinkedHashSet<NodeDeclNode>();
				homSet.Add(node);
				nodeToHomNodes[node] = homSet;
			}

			// Each edge is homomorphic to itself (trivial hom).
			foreach(EdgeDeclNode edge in edges)
			{
				ISet<EdgeDeclNode> homSet = new LinkedHashSet<EdgeDeclNode>();
				homSet.Add(edge);
				edgeToHomEdges[edge] = homSet;
			}

			foreach(ISet<ConstraintDeclNode> homSet in homSets)
			{
				if(EnumeratorHelper.GetFirstElement(homSet) is NodeDeclNode)
					FillHomNodesInNodesToHomNodes(homSet);
				else
					FillHomEdgesInEdgesToHomEdges(homSet);
			}
		}

		private void FillHomNodesInNodesToHomNodes(ISet<ConstraintDeclNode> homSet)
		{
			foreach(ConstraintDeclNode elem in homSet)
			{
				NodeDeclNode node = (NodeDeclNode)elem;
				ISet<NodeDeclNode> mapEntry = nodeToHomNodes[node];
				foreach(ConstraintDeclNode homomorphicNode in homSet)
					mapEntry.Add((NodeDeclNode)homomorphicNode);
			}
		}

		private void FillHomEdgesInEdgesToHomEdges(ISet<ConstraintDeclNode> homSet)
		{
			foreach(ConstraintDeclNode elem in homSet)
			{
				EdgeDeclNode edge = (EdgeDeclNode)elem;
				ISet<EdgeDeclNode> mapEntry = edgeToHomEdges[edge];
				foreach(ConstraintDeclNode homomorphicEdge in homSet)
					mapEntry.Add((EdgeDeclNode)homomorphicEdge);
			}
		}

		public virtual ICollection<ISet<ConstraintDeclNode>> Homs
		{
			get
			{
				return homSets;
			}
		}

		/// <summary>
		/// Return the correspondent homomorphic set. </summary>
		public virtual ISet<NodeDeclNode> GetHomomorphic(NodeDeclNode node)
		{
			ISet<NodeDeclNode> homSet;
			nodeToHomNodes.TryGetValue(node, out homSet);

			// If the node isn't part of the pattern, return empty set.
			if(homSet == null)
				return emptyHomNodeSet;
			else
				return homSet;
		}

		/// <summary>
		/// Return the correspondent homomorphic set. </summary>
		public virtual ISet<EdgeDeclNode> GetHomomorphic(EdgeDeclNode edge)
		{
			ISet<EdgeDeclNode> homSet;
			edgeToHomEdges.TryGetValue(edge, out homSet);

			// If the edge isn't part of the pattern, return empty set.
			if(homSet == null)
				return emptyHomEdgeSet;
			else
				return homSet;
		}
	}

}
