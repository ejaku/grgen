/*
 * GrGen: graph rewrite generator tool -- release GrGen.NET 8.1
 * Copyright (C) 2003-2026 Universitaet Karlsruhe, Institut fuer Programmstrukturen und Datenorganisation, LS Goos; and free programmers
 * licensed under LGPL v3, some components/parts use different licenses (see LICENSE.txt included in the packaging of this file)
 * www.grgen.de / www.grgen.net
 */

namespace de.unika.ipd.grgen.ast.pattern
{

	using System.Collections.Generic;
	using System.Diagnostics;

	using BaseNode = de.unika.ipd.grgen.ast.BaseNode;
	using EdgeDeclNode = de.unika.ipd.grgen.ast.decl.pattern.EdgeDeclNode;
	using NodeDeclNode = de.unika.ipd.grgen.ast.decl.pattern.NodeDeclNode;
	using DirectedEdgeTypeNode = de.unika.ipd.grgen.ast.model.type.DirectedEdgeTypeNode;
	using EdgeTypeNode = de.unika.ipd.grgen.ast.model.type.EdgeTypeNode;
	using NodeTypeNode = de.unika.ipd.grgen.ast.model.type.NodeTypeNode;
	using UndirectedEdgeTypeNode = de.unika.ipd.grgen.ast.model.type.UndirectedEdgeTypeNode;
	using TypeNode = de.unika.ipd.grgen.ast.type.TypeNode;
	using de.unika.ipd.grgen.ast.util;
	using TypeChecker = de.unika.ipd.grgen.ast.util.TypeChecker;
	using Coords = de.unika.ipd.grgen.parser.Coords;
	using Color = de.unika.ipd.grgen.util.Color;

	/// <summary>
	/// AST node that represents a totally homomorph node and a set of nodes it must be isomorph to
	/// </summary>
	public class TotallyHomNode : BaseNode
	{
		static TotallyHomNode()
		{
			SetClassName(typeof(TotallyHomNode), "totally homomorph");
		}

		internal NodeDeclNode node;
		internal EdgeDeclNode edge;
		internal IList<NodeDeclNode> childrenNode = new List<NodeDeclNode>();
		internal IList<EdgeDeclNode> childrenEdge = new List<EdgeDeclNode>();

		private BaseNode entityUnresolved;
		private IList<BaseNode> childrenUnresolved = new List<BaseNode>();

		public TotallyHomNode(Coords coords)
			: base(coords)
		{
		}

		public virtual BaseNode TotallyHom
		{
			set
			{
				Debug.Assert((this.entityUnresolved == null));
				BecomeParent(value);
				this.entityUnresolved = value;

			}
		}

		public virtual void AddChild(BaseNode child)
		{
			Debug.Assert((!IsResolved()));
			BecomeParent(child);
			childrenUnresolved.Add(child);
		}

		/// <summary>
		/// returns children of this node </summary>
		public override ICollection<BaseNode> Children
		{
			get
			{
				IList<BaseNode> children = new List<BaseNode>();
				children.Add(GetValidVersion(entityUnresolved, node, edge));
				((List<BaseNode>)children).AddRange(GetValidVersionList(childrenUnresolved, childrenNode, childrenEdge));
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
				childrenNames.Add("totally homomorph entity");
				// nameless isomorph children
				return childrenNames;
			}
		}

		private static readonly DeclarationPairResolver<NodeDeclNode, EdgeDeclNode> declResolver =
				new DeclarationPairResolver<NodeDeclNode, EdgeDeclNode>(typeof(NodeDeclNode), typeof(EdgeDeclNode));

		/// <seealso cref="de.unika.ipd.grgen.ast.BaseNode.resolveLocal() "/>
		protected internal override bool ResolveLocal()
		{
			Pair<NodeDeclNode, EdgeDeclNode> resolved = declResolver.Resolve(entityUnresolved, this);
			bool successfullyResolved = resolved != null;
			if(resolved != null)
			{
				if(resolved.fst != null)
					node = resolved.fst;
				else
					edge = resolved.snd;
			}

			for(int i = 0; i < childrenUnresolved.Count; ++i)
			{
				resolved = declResolver.Resolve(childrenUnresolved[i], this);
				successfullyResolved = (resolved != null) && successfullyResolved;
				if(resolved != null)
				{
					if(resolved.fst != null)
						childrenNode.Add(resolved.fst);
					if(resolved.snd != null)
						childrenEdge.Add(resolved.snd);
				}
			}

			return successfullyResolved;
		}

		private static readonly TypeChecker nodeTypeChecker = new TypeChecker(typeof(NodeTypeNode));
		private static readonly TypeChecker edgeTypeChecker = new TypeChecker(typeof(EdgeTypeNode));

		/// <summary>
		/// Check whether all children are of same type (node or edge)
		/// and additionally one entity may not be used in two different hom
		/// statements
		/// </summary>
		protected internal override bool CheckLocal()
		{
			if(node != null)
			{
				if(childrenEdge.Count > 0)
				{
					this.ReportError("The independent statement may only contain nodes or edges at a time"
							+ " (it specifies the node " + node.Ident + " to be totally homomorphic, but the edge " + childrenEdge[0] + " as exception to be isomorphic).");
					return false;
				}
			}
			if(edge != null)
			{
				if(childrenNode.Count > 0)
				{
					this.ReportError("The independent statement may only contain nodes or edges at a time"
							+ " (it specifies the edge " + edge.Ident + " to be totally homomorphic, but the node " + childrenNode[0] + " as exception to be isomorphic).");
					return false;
				}
			}

			bool successfullyChecked = true;
			foreach(NodeDeclNode node in childrenNode)
				successfullyChecked = nodeTypeChecker.Check(node, error) && successfullyChecked;
			foreach(EdgeDeclNode edge in childrenEdge)
				successfullyChecked = edgeTypeChecker.Check(edge, error) && successfullyChecked;
			if(edge != null)
				WarnEdgeTypes();

			return successfullyChecked;
		}

		/// <summary>
		/// Checks whether all edges are compatible to each other. </summary>
		private void WarnEdgeTypes()
		{
			bool isDirectedEdge = edge.DeclType is DirectedEdgeTypeNode;
			bool isUndirectedEdge = edge.DeclType is UndirectedEdgeTypeNode;

			for(int i = 0; i < childrenEdge.Count; i++)
			{
				TypeNode type = childrenEdge[i].DeclType;
				if(type is DirectedEdgeTypeNode)
					isDirectedEdge = true;
				if(type is UndirectedEdgeTypeNode)
					isUndirectedEdge = true;
			}

			if(isDirectedEdge && isUndirectedEdge)
				ReportWarning("The independent statement may only contain directed or undirected edges at a time.");
		}

		public override Color NodeColor
		{
			get
			{
				return Color.PINK;
			}
		}
	}

}
