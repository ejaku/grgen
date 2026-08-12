/*
 * GrGen: graph rewrite generator tool -- release GrGen.NET 8.1
 * Copyright (C) 2003-2026 Universitaet Karlsruhe, Institut fuer Programmstrukturen und Datenorganisation, LS Goos; and free programmers
 * licensed under LGPL v3, some components/parts use different licenses (see LICENSE.txt included in the packaging of this file)
 * www.grgen.de / www.grgen.net
 */

/// <summary>
/// @author buchwald
/// </summary>

namespace de.unika.ipd.grgen.ast.pattern
{

	using System.Collections.Generic;
	using System.Diagnostics;

	using BaseNode = de.unika.ipd.grgen.ast.BaseNode;
	using NodeDeclNode = de.unika.ipd.grgen.ast.decl.pattern.NodeDeclNode;
	using de.unika.ipd.grgen.ast.util;
	using Coords = de.unika.ipd.grgen.parser.Coords;
	using Color = de.unika.ipd.grgen.util.Color;

	public class ExactNode : BaseNode
	{
		static ExactNode()
		{
			SetClassName(typeof(ExactNode), "exact");
		}

		private IList<NodeDeclNode> children = new List<NodeDeclNode>();

		private IList<BaseNode> childrenUnresolved = new List<BaseNode>();

		public ExactNode(Coords coords)
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
				return GetValidVersionList(childrenUnresolved, children);
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

		private static readonly DeclarationResolver<NodeDeclNode> childrenResolver =
				new DeclarationResolver<NodeDeclNode>(typeof(NodeDeclNode));

		/// <seealso cref="de.unika.ipd.grgen.ast.BaseNode.resolveLocal() "/>
		protected internal override bool ResolveLocal()
		{
			bool successfullyResolved = true;
			for(int i = 0; i < childrenUnresolved.Count; ++i)
			{
				children.Add(childrenResolver.Resolve(childrenUnresolved[i], this));
				successfullyResolved = children[i] != null && successfullyResolved;
			}
			return successfullyResolved;
		}

		/// <summary>
		/// Check whether all children are of node type.
		/// </summary>
		protected internal override bool CheckLocal()
		{
			if(children.Count == 0)
			{
				this.ReportError("The exact statement is empty.");
				return false;
			}

			return true;
		}

		public virtual ICollection<NodeDeclNode> ExactNodes
		{
			get
			{
				return children;
			}
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
