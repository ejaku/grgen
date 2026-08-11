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
	using de.unika.ipd.grgen.ast.util;
	using TypeChecker = de.unika.ipd.grgen.ast.util.TypeChecker;
	using Coords = de.unika.ipd.grgen.parser.Coords;

	/// <summary>
	/// AST node that represents a set of potentially homomorph nodes
	/// children: *:IdentNode resolved to NodeDeclNode|EdgeDeclNoe
	/// </summary>
	public class HomNode : BaseNode
	{
		static HomNode()
		{
			SetClassName(typeof(HomNode), "homomorph");
		}

		private IList<NodeDeclNode> childrenNode = new List<NodeDeclNode>();
		private IList<EdgeDeclNode> childrenEdge = new List<EdgeDeclNode>();

		private IList<BaseNode> childrenUnresolved = new List<BaseNode>();

		public HomNode(Coords coords)
			: base(coords)
		{
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
				return GetValidVersionList(childrenUnresolved, childrenNode, childrenEdge);
			}
		}

		/// <summary>
		/// returns names of the children, same order as in getChildren </summary>
		public override ICollection<string> ChildrenNames
		{
			get
			{
				IList<string> childrenNames = new List<string>();
				// nameless children
				return childrenNames;
			}
		}

		public virtual IList<NodeDeclNode> HomNodes
		{
			get
			{
				return childrenNode;
			}
		}

		public virtual IList<EdgeDeclNode> HomEdges
		{
			get
			{
				return childrenEdge;
			}
		}

		private static readonly DeclarationPairResolver<NodeDeclNode, EdgeDeclNode> declResolver =
				new DeclarationPairResolver<NodeDeclNode, EdgeDeclNode>(typeof(NodeDeclNode), typeof(EdgeDeclNode));

		/// <seealso cref="de.unika.ipd.grgen.ast.BaseNode.resolveLocal() "/>
		protected internal override bool ResolveLocal()
		{
			bool successfullyResolved = true;

			for(int i = 0; i < childrenUnresolved.Count; ++i)
			{
				Pair<NodeDeclNode, EdgeDeclNode> resolved = declResolver.Resolve(childrenUnresolved[i], this);
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
			if(childrenNode.Count == 0 && childrenEdge.Count == 0)
			{
				this.ReportError("The hom statement is empty.");
				return false;
			}
			if(childrenNode.Count > 0 && childrenEdge.Count > 0)
			{
				this.ReportError("The hom statement may only contain nodes or edges at a time"
						+ " (this is violated by node " + childrenNode[0] + " and edge " + childrenEdge[0] + ").");
				return false;
			}

			bool successfullyChecked = true;
			foreach(NodeDeclNode node in childrenNode)
				successfullyChecked = nodeTypeChecker.Check(node, error) && successfullyChecked;
			foreach(EdgeDeclNode edge in childrenEdge)
				successfullyChecked = edgeTypeChecker.Check(edge, error) && successfullyChecked;
			WarnEdgeTypes();

			return successfullyChecked;
		}

		/// <summary>
		/// Checks whether all edges are compatible to each other. </summary>
		private void WarnEdgeTypes()
		{
			bool isDirectedEdge = false;
			bool isUndirectedEdge = false;

			for(int i = 0; i < childrenEdge.Count; i++)
			{
				TypeNode type = childrenEdge[i].DeclType;
				if(type is DirectedEdgeTypeNode)
					isDirectedEdge = true;
				if(type is UndirectedEdgeTypeNode)
					isUndirectedEdge = true;
			}

			if(isDirectedEdge && isUndirectedEdge)
				ReportWarning("The hom statement may only contain directed or undirected edges at a time.");
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
