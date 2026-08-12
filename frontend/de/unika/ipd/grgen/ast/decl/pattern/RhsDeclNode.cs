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
	using IdentNode = de.unika.ipd.grgen.ast.IdentNode;
	using DeclNode = de.unika.ipd.grgen.ast.decl.DeclNode;
	using ConnectionCharacter = de.unika.ipd.grgen.ast.pattern.ConnectionCharacter;
	using ConnectionNode = de.unika.ipd.grgen.ast.pattern.ConnectionNode;
	using PatternGraphRhsNode = de.unika.ipd.grgen.ast.pattern.PatternGraphRhsNode;
	using PatternGraphLhsNode = de.unika.ipd.grgen.ast.pattern.PatternGraphLhsNode;
	using RhsTypeNode = de.unika.ipd.grgen.ast.type.RhsTypeNode;
	using TypeNode = de.unika.ipd.grgen.ast.type.TypeNode;
	using de.unika.ipd.grgen.ast.util;
	using IR = de.unika.ipd.grgen.ir.IR;
	using NeededEntities = de.unika.ipd.grgen.ir.NeededEntities;
	using Needs = de.unika.ipd.grgen.ir.NeededEntities.Needs;
	using EvalStatement = de.unika.ipd.grgen.ir.stmt.EvalStatement;
	using EvalStatements = de.unika.ipd.grgen.ir.stmt.EvalStatements;
	using Emit = de.unika.ipd.grgen.ir.Emit;
	using Edge = de.unika.ipd.grgen.ir.pattern.Edge;
	using Node = de.unika.ipd.grgen.ir.pattern.Node;
	using OrderedReplacement = de.unika.ipd.grgen.ir.pattern.OrderedReplacement;
	using OrderedReplacements = de.unika.ipd.grgen.ir.pattern.OrderedReplacements;
	using PatternGraphLhs = de.unika.ipd.grgen.ir.pattern.PatternGraphLhs;
	using PatternGraphRhs = de.unika.ipd.grgen.ir.pattern.PatternGraphRhs;
	using Variable = de.unika.ipd.grgen.ir.pattern.Variable;

	/// <summary>
	/// AST node for a replacement right-hand side.
	/// </summary>
	public abstract class RhsDeclNode : DeclNode
	{
		static RhsDeclNode()
		{
			SetClassName(typeof(RhsDeclNode), "right-hand side declaration");
		}

		public PatternGraphRhsNode patternGraph;
		protected internal RhsTypeNode type;

		/// <summary>
		/// Type for this declaration. </summary>
		protected internal static readonly TypeNode rhsType = new RhsTypeNode();

		// Cache variables
		private ISet<ConstraintDeclNode> elementsToDelete;
		private ISet<NodeDeclNode> nodesToReuse;
		private ISet<ConnectionNode> connectionsToReuse; // edgesToReuse in connection form


		/// <summary>
		/// Make a new right-hand side. </summary>
		/// <param name="id"> The identifier of this RHS. </param>
		/// <param name="patternGraph"> The right hand side graph. </param>
		protected internal RhsDeclNode(IdentNode id, PatternGraphRhsNode patternGraph)
			: base(id, rhsType)
		{
			this.patternGraph = patternGraph;
			BecomeParent(this.patternGraph);
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
				return childrenNames;
			}
		}

		public virtual PatternGraphRhsNode RhsGraph
		{
			get
			{
				return patternGraph;
			}
		}

		public virtual ISet<ConstraintDeclNode> GetMaybeDeletedElements(PatternGraphLhsNode pattern)
		{
			// add deleted entities
			ISet<ConstraintDeclNode> maybeDeletedElements = new LinkedHashSet<ConstraintDeclNode>();
			maybeDeletedElements.AddAll(GetElementsToDelete(pattern));

			// extract deleted nodes, then add homomorphic nodes
			ISet<NodeDeclNode> nodes = new LinkedHashSet<NodeDeclNode>();
			foreach(ConstraintDeclNode maybeDeletedElement in maybeDeletedElements)
			{
				if(maybeDeletedElement is NodeDeclNode)
					nodes.Add((NodeDeclNode)maybeDeletedElement);
			}
			foreach(NodeDeclNode node in nodes)
				maybeDeletedElements.AddAll(pattern.GetHomomorphic(node));

			// add edges resulting from deleted nodes (only needed if a deleted node exists)
			if(nodes.Count > 0)
				maybeDeletedElements.AddAll(GetMaybeDeletedEdgesResultingFromMaybeDeletedNodes(maybeDeletedElements, pattern));

			// extract deleted edges, then add homomorphic edges
			ISet<EdgeDeclNode> edges = new LinkedHashSet<EdgeDeclNode>();
			foreach(ConstraintDeclNode maybeDeletedElement in maybeDeletedElements)
			{
				if(maybeDeletedElement is EdgeDeclNode)
					edges.Add((EdgeDeclNode)maybeDeletedElement);
			}
			foreach(EdgeDeclNode edge in edges)
				maybeDeletedElements.AddAll(pattern.GetHomomorphic(edge));

			return maybeDeletedElements;
		}

		private ISet<ConstraintDeclNode> GetMaybeDeletedEdgesResultingFromMaybeDeletedNodes(ISet<ConstraintDeclNode> maybeDeletedNodes, PatternGraphLhsNode pattern)
		{
			ISet<ConstraintDeclNode> edgesResultingFromMaybeDeletedNodes = new HashSet<ConstraintDeclNode>();

			// edges of deleted nodes are deleted, too --> add them
			ISet<ConnectionNode> connections = GetConnectionsNotDeleted(pattern);
			foreach(ConnectionNode connection in connections)
			{
				if(SourceOrTargetNodeIncluded(connection.Edge, pattern, new HashSet<DeclNode>(maybeDeletedNodes)))
					edgesResultingFromMaybeDeletedNodes.Add(connection.Edge);
			}

			// nodes of dangling edges are homomorphic to all other nodes,
			// especially the deleted ones :-)
			foreach(ConnectionNode connection in connections)
			{
				EdgeDeclNode edge = connection.Edge;
				while(edge is EdgeTypeChangeDeclNode)
					edge = ((EdgeTypeChangeDeclNode)edge).OldEdge;
				bool srcIsDummy = true;
				bool tgtIsDummy = true;
				foreach(ConnectionNode innerConn in connections)
				{
					if(edge.Equals(innerConn.Edge))
					{
						srcIsDummy &= innerConn.Src.IsDummy();
						tgtIsDummy &= innerConn.Tgt.IsDummy();
					}
				}

				// so maybe the dangling edge is deleted by one of the node deletions --> add it
				if(srcIsDummy || tgtIsDummy)
					edgesResultingFromMaybeDeletedNodes.Add(edge);
			}

			return edgesResultingFromMaybeDeletedNodes;
		}

		protected internal abstract ISet<ConnectionNode> GetConnectionsNotDeleted(PatternGraphLhsNode pattern);

		protected internal static readonly DeclarationTypeResolver<RhsTypeNode> typeResolver =
				new DeclarationTypeResolver<RhsTypeNode>(typeof(RhsTypeNode));

		/// <seealso cref="de.unika.ipd.grgen.ast.BaseNode.resolveLocal() "/>
		protected internal override bool ResolveLocal()
		{
			type = typeResolver.Resolve(typeUnresolved, this);

			return type != null;
		}

		/// <summary>
		/// Edges as replacement parameters are not really needed but very troublesome, keep them out for now.
		/// </summary>
		private bool CheckEdgeParameters()
		{
			bool res = true;

			foreach(DeclNode replParam in patternGraph.ParamDecls)
			{
				if(replParam is EdgeDeclNode)
				{
					replParam.ReportError("Edges are not supported as rewrite parameters"
							+ " (but the rewrite parameter " + replParam.Ident + " is an edge).");
					res = false;
				}
			}

			return res;
		}

		/// <seealso cref="de.unika.ipd.grgen.ast.BaseNode.checkLocal()"/>
		protected internal override bool CheckLocal()
		{
			return CheckEdgeParameters();
		}

		public abstract bool CheckAgainstLhsPattern(PatternGraphLhsNode pattern);

		/// <seealso cref="de.unika.ipd.grgen.ast.BaseNode.constructIR()"/>
		protected internal override IR ConstructIR()
		{
			Debug.Assert(false);

			return null;
		}

		protected internal virtual void InsertElementsFromEvalsIntoRhs(PatternGraphLhs left, PatternGraphRhs right)
		{
			// insert all elements, which are used in eval statements (of the right hand side) and
			// neither declared on the local left hand nor on the right hand side to the right hand side
			// further code (PatternGraph::insertElementsFromRhsDeclaredInNestingLhsToLocalLhs)
			// will add them to the left hand side, too

			NeededEntities needs = new NeededEntities(NeededEntities.Needs.NODES | NeededEntities.Needs.EDGES | NeededEntities.Needs.VARS);
			ICollection<EvalStatements> evalStatements = patternGraph.EvalStatements;
			foreach(EvalStatements evalStatement in evalStatements)
				evalStatement.CollectNeededEntities(needs);

			foreach(Node neededNode in needs.nodes)
			{
				if(neededNode.directlyNestingLHSGraph != left)
				{
					if(!right.DeletedElements.Contains(neededNode))
					{
						if(!right.HasNode(neededNode))
						{
							right.AddSingleNode(neededNode);
							right.AddHomToAll(neededNode);
						}
					}
				}
			}
			foreach(Edge neededEdge in needs.edges)
			{
				if(neededEdge.directlyNestingLHSGraph != left)
				{
					if(!right.DeletedElements.Contains(neededEdge))
					{
						if(!right.HasEdge(neededEdge))
						{
							right.AddSingleEdge(neededEdge);
							right.AddHomToAll(neededEdge);
						}
					}
				}
			}
			foreach(Variable neededVariable in needs.variables)
			{
				if(neededVariable.directlyNestingLHSGraph != left)
				{
					if(!right.HasVar(neededVariable))
						right.AddVariable(neededVariable);
				}
			}
		}

		protected internal virtual void InsertElementsFromOrderedReplacementsIntoRhs(PatternGraphLhs left, PatternGraphRhs right)
		{
			// insert all elements, which are used in ordered replacements (of the right hand side) and
			// neither declared on the local left hand nor on the right hand side to the right hand side
			// further code (PatternGraph::insertElementsFromRhsDeclaredInNestingLhsToLocalLhs)
			// will add them to the left hand side, too

			NeededEntities needs = new NeededEntities(NeededEntities.Needs.NODES | NeededEntities.Needs.EDGES | NeededEntities.Needs.VARS);
			ICollection<OrderedReplacements> evalStatements = patternGraph.OrderedReplacements;
			foreach(OrderedReplacements evalStatement in evalStatements)
			{
				foreach(OrderedReplacement orderedReplacement in evalStatement.orderedReplacements)
				{
					if(orderedReplacement is EvalStatement)
						((EvalStatement)orderedReplacement).CollectNeededEntities(needs);
					else if(orderedReplacement is Emit)
						((Emit)orderedReplacement).CollectNeededEntities(needs);
				}
			}

			foreach(Node neededNode in needs.nodes)
			{
				if(neededNode.directlyNestingLHSGraph != left)
				{
					if(!right.DeletedElements.Contains(neededNode))
					{
						if(!right.HasNode(neededNode))
						{
							right.AddSingleNode(neededNode);
							right.AddHomToAll(neededNode);
						}
					}
				}
			}
			foreach(Edge neededEdge in needs.edges)
			{
				if(neededEdge.directlyNestingLHSGraph != left)
				{
					if(!right.DeletedElements.Contains(neededEdge))
					{
						if(!right.HasEdge(neededEdge))
						{
							right.AddSingleEdge(neededEdge);
							right.AddHomToAll(neededEdge);
						}
					}
				}
			}
			foreach(Variable neededVariable in needs.variables)
			{
				if(neededVariable.directlyNestingLHSGraph != left)
				{
					if(!right.HasVar(neededVariable))
						right.AddVariable(neededVariable);
				}
			}
		}

		public abstract PatternGraphRhs GetIRPatternGraph(PatternGraphLhs left);

		public override TypeNode DeclType
		{
			get
			{
				Debug.Assert(IsResolved());

				return type;
			}
		}

		/// <summary>
		/// Returns all elements that are to be deleted.
		/// </summary>
		public virtual ISet<ConstraintDeclNode> GetElementsToDelete(PatternGraphLhsNode pattern)
		{
			if(elementsToDelete == null)
				elementsToDelete = Collections.UnmodifiableSet(GetElementsToDeleteImpl(pattern));
			return elementsToDelete;
		}

		protected internal abstract ISet<ConstraintDeclNode> GetElementsToDeleteImpl(PatternGraphLhsNode pattern);

		/// <summary>
		/// Returns all to be reused edges (with their nodes, in the form of a connection),
		/// that excludes new edges of the right-hand side, those are to be created.
		/// </summary>
		public virtual ISet<ConnectionNode> GetConnectionsToReuse(PatternGraphLhsNode pattern)
		{
			if(connectionsToReuse == null)
				connectionsToReuse = Collections.UnmodifiableSet(GetConnectionsToReuseImpl(pattern));
			return connectionsToReuse;
		}

		protected internal abstract ISet<ConnectionNode> GetConnectionsToReuseImpl(PatternGraphLhsNode pattern);

		/// <summary>
		/// Returns all to be reused nodes, that excludes new nodes of the right-hand side, those are to be created.
		/// </summary>
		public virtual ISet<NodeDeclNode> GetNodesToReuse(PatternGraphLhsNode pattern)
		{
			if(nodesToReuse == null)
				nodesToReuse = Collections.UnmodifiableSet(GetNodesToReuseImpl(pattern));
			return nodesToReuse;
		}

		protected internal abstract ISet<NodeDeclNode> GetNodesToReuseImpl(PatternGraphLhsNode pattern);


		protected internal static bool SourceOrTargetNodeIncluded(EdgeDeclNode edge, PatternGraphLhsNode pattern, ICollection<DeclNode> collection)
		{
			foreach(ConnectionCharacter connectionCharacter in pattern.Connections)
			{
				if(connectionCharacter is ConnectionNode)
				{
					ConnectionNode connection = (ConnectionNode)connectionCharacter;
					if(connection.Edge.Equals(edge))
					{
						if(collection.Contains(connection.Src) || collection.Contains(connection.Tgt))
							return true;
					}
				}
			}
			return false;
		}
	}

}
