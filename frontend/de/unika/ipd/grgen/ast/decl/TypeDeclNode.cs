/*
 * GrGen: graph rewrite generator tool -- release GrGen.NET 8.1
 * Copyright (C) 2003-2026 Universitaet Karlsruhe, Institut fuer Programmstrukturen und Datenorganisation, LS Goos; and free programmers
 * licensed under LGPL v3, some components/parts use different licenses (see LICENSE.txt included in the packaging of this file)
 * www.grgen.de / www.grgen.net
 */

/// <summary>
/// @author Sebastian Hack
/// </summary>

namespace de.unika.ipd.grgen.ast.decl
{

	using System.Collections.Generic;
	using System.Diagnostics;

	using BaseNode = de.unika.ipd.grgen.ast.BaseNode;
	using IdentNode = de.unika.ipd.grgen.ast.IdentNode;
	using ArbitraryEdgeTypeNode = de.unika.ipd.grgen.ast.model.type.ArbitraryEdgeTypeNode;
	using DirectedEdgeTypeNode = de.unika.ipd.grgen.ast.model.type.DirectedEdgeTypeNode;
	using EdgeTypeNode = de.unika.ipd.grgen.ast.model.type.EdgeTypeNode;
	using InheritanceTypeNode = de.unika.ipd.grgen.ast.model.type.InheritanceTypeNode;
	using UndirectedEdgeTypeNode = de.unika.ipd.grgen.ast.model.type.UndirectedEdgeTypeNode;
	using DeclaredTypeNode = de.unika.ipd.grgen.ast.type.DeclaredTypeNode;
	using TypeNode = de.unika.ipd.grgen.ast.type.TypeNode;
	using de.unika.ipd.grgen.ast.util;
	using IR = de.unika.ipd.grgen.ir.IR;

	/// <summary>
	/// Declaration of a type.
	/// </summary>
	public class TypeDeclNode : DeclNode
	{
		static TypeDeclNode()
		{
			SetClassName(typeof(TypeDeclNode), "type declaration");
		}

		private DeclaredTypeNode type;

		public TypeDeclNode(IdentNode i, BaseNode t)
			: base(i, t)
		{

			// Set the declaration of the declared type node to this node.
			if(t is DeclaredTypeNode)
			{
				DeclaredTypeNode declTypeNode = (DeclaredTypeNode)t;
				declTypeNode.Decl = this;
			}
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
				return childrenNames;
			}
		}

		private static DeclarationTypeResolver<DeclaredTypeNode> typeResolver =
				new DeclarationTypeResolver<DeclaredTypeNode>(typeof(DeclaredTypeNode));

		/// <seealso cref="de.unika.ipd.grgen.ast.BaseNode.resolveLocal() "/>
		protected internal override bool ResolveLocal()
		{
			type = typeResolver.Resolve(typeUnresolved, this);

			return type != null;
		}

		/// <seealso cref="de.unika.ipd.grgen.ast.BaseNode.checkLocal() "/>
		protected internal override bool CheckLocal()
		{
			return CheckNoConflictingEdgeParents();
		}

		/// <summary>
		/// Checks whether an edge class extends a directed and an undirected edge
		/// class.
		/// </summary>
		/// <returns> Check pass without an error. </returns>
		private bool CheckNoConflictingEdgeParents()
		{
			if(!(type is EdgeTypeNode))
				return true;

			EdgeTypeNode edgeType = (EdgeTypeNode)type;

			InheritanceTypeNode extendEdge = ExtendsEdge(edgeType);
			InheritanceTypeNode extendUEdge = ExtendsUEdge(edgeType);

			if(extendEdge != null && extendUEdge != null)
			{
				ReportError("An edge class cannot extend a directed and an undirected edge class "
						+ "(but this occurs for " + Ident
						+ " with " + extendEdge.ToStringWithDeclarationCoords()
						+ " and " + extendUEdge.ToStringWithDeclarationCoords() + ")");
				return false;
			}
			if((type is ArbitraryEdgeTypeNode) && extendEdge != null)
			{
				ReportError("An arbitrary edge class cannot extend a directed edge class "
						+ "(but this occurs for " + Ident
						+ " with " + extendEdge.ToStringWithDeclarationCoords() + ")");
				return false;
			}
			if(type is ArbitraryEdgeTypeNode && extendUEdge != null)
			{
				ReportError("An arbitrary edge class cannot extend an undirected edge class "
						+ "(but this occurs for " + Ident
						+ " with " + extendUEdge.ToStringWithDeclarationCoords() + ")");
				return false;
			}
			if((type is UndirectedEdgeTypeNode) && extendEdge != null)
			{
				ReportError("An undirected edge class cannot extend a directed edge class "
						+ "(but this occurs for " + Ident
						+ " with " + extendEdge.ToStringWithDeclarationCoords() + ")");
				return false;
			}
			if(type is DirectedEdgeTypeNode && extendUEdge != null)
			{
				ReportError("A directed edge class cannot extend an undirected edge class "
						+ "(but this occurs for " + Ident
						+ " with " + extendUEdge.ToStringWithDeclarationCoords() + ")");
				return false;
			}

			return true;
		}

		private static InheritanceTypeNode ExtendsEdge(EdgeTypeNode edgeType)
		{
			foreach(InheritanceTypeNode inh in edgeType.DirectSuperTypes)
			{
				if(inh is DirectedEdgeTypeNode)
					return inh;
			}
			return null;
		}

		private static InheritanceTypeNode ExtendsUEdge(EdgeTypeNode edgeType)
		{
			foreach(InheritanceTypeNode inh in edgeType.DirectSuperTypes)
			{
				if(inh is UndirectedEdgeTypeNode)
					return inh;
			}
			return null;
		}

		/// <summary>
		/// A type declaration returns the declared type
		/// as result. </summary>
		/// <seealso cref="de.unika.ipd.grgen.ast.BaseNode.constructIR()"/>
		protected internal override IR ConstructIR()
		{
			return DeclType.IR;
		}

		public static new string KindStr
		{
			get
			{
				return "type";
			}
		}

		public override TypeNode DeclType
		{
			get
			{
				Debug.Assert(IsResolved());

				return type;
			}
		}
	}

}
