/*
 * GrGen: graph rewrite generator tool -- release GrGen.NET 8.1
 * Copyright (C) 2003-2026 Universitaet Karlsruhe, Institut fuer Programmstrukturen und Datenorganisation, LS Goos; and free programmers
 * licensed under LGPL v3, some components/parts use different licenses (see LICENSE.txt included in the packaging of this file)
 * www.grgen.de / www.grgen.net
 */

/// <summary>
/// @author Sebastian Hack
/// </summary>

namespace de.unika.ipd.grgen.ast.pattern
{

	using System.Collections.Generic;
	using System.Diagnostics;

	using BaseNode = de.unika.ipd.grgen.ast.BaseNode;
	using EdgeDeclNode = de.unika.ipd.grgen.ast.decl.pattern.EdgeDeclNode;
	using NodeDeclNode = de.unika.ipd.grgen.ast.decl.pattern.NodeDeclNode;
	using NodeTypeNode = de.unika.ipd.grgen.ast.model.type.NodeTypeNode;
	using Checker = de.unika.ipd.grgen.ast.util.Checker;
	using de.unika.ipd.grgen.ast.util;
	using TypeChecker = de.unika.ipd.grgen.ast.util.TypeChecker;
	using PatternGraphBase = de.unika.ipd.grgen.ir.pattern.PatternGraphBase;

	/// <summary>
	/// AST node representing nodes
	/// that occur without any edge connection to the rest of the graph.
	/// children: NODE:NodeDeclNode|IdentNode
	/// </summary>
	public class SingleNodeConnNode : ConnectionCharacter
	{
		static SingleNodeConnNode()
		{
			SetClassName(typeof(SingleNodeConnNode), "single node");
		}

		private NodeDeclNode node;
		public BaseNode nodeUnresolved;

		public SingleNodeConnNode(BaseNode node)
			: base(node.Coords)
		{
			this.nodeUnresolved = node;
			BecomeParent(this.nodeUnresolved);
		}

		public SingleNodeConnNode(NodeDeclNode node, BaseNode parent)
			: this(node)
		{
			parent.BecomeParent(this);

			Resolve();
			Check();
		}

		public virtual SingleNodeConnNode CloneForAuto(PatternGraphLhsNode parent)
		{
			return new SingleNodeConnNode(this.node.CloneForAuto(parent), parent);
		}

		/// <summary>
		/// returns children of this node </summary>
		public override ICollection<BaseNode> Children
		{
			get
			{
				IList<BaseNode> children = new List<BaseNode>();
				children.Add(GetValidVersion(nodeUnresolved, node));
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
				childrenNames.Add("node");
				return childrenNames;
			}
		}

		private static readonly DeclarationResolver<NodeDeclNode> nodeResolver =
				new DeclarationResolver<NodeDeclNode>(typeof(NodeDeclNode)); // optional

		/// <seealso cref="de.unika.ipd.grgen.ast.BaseNode.resolveLocal() "/>
		protected internal override bool ResolveLocal()
		{
			bool res = FixupDefinition(nodeUnresolved, nodeUnresolved.Scope);
			if(!res)
				return false;

			node = nodeResolver.Resolve(nodeUnresolved, this);
			return node != null;
		}

		/// <summary>
		/// Get the node child of this node. </summary>
		/// <returns> The node child.  </returns>
		public virtual NodeDeclNode Node
		{
			get
			{
				Debug.Assert(IsResolved());

				return node;
			}
		}

		/// <seealso cref="de.unika.ipd.grgen.ast.pattern.ConnectionCharacter.addToGraph(de.unika.ipd.grgen.ir.pattern.PatternGraphBase) "/>
		public override void AddToGraph(PatternGraphBase patternGraph)
		{
			Debug.Assert(IsResolved());

			patternGraph.AddSingleNode(node.IRNode);
		}

		private static Checker nodeChecker = new TypeChecker(typeof(NodeTypeNode));

		/// <seealso cref="de.unika.ipd.grgen.ast.BaseNode.checkLocal() "/>
		protected internal override bool CheckLocal()
		{
			return nodeChecker.Check(node, error);
		}

		/// <seealso cref="de.unika.ipd.grgen.ast.pattern.ConnectionCharacter.addEdge(java.util.Set) "/>
		public override void AddEdge(ISet<EdgeDeclNode> set)
		{
			// no edge available
		}

		public override EdgeDeclNode Edge
		{
			get
			{
				return null;
			}
		}

		public override NodeDeclNode Src
		{
			get
			{
				Debug.Assert(IsResolved());

				return node;
			}
			set
			{
				// no edge available a source could be set
			}
		}


		public override NodeDeclNode Tgt
		{
			get
			{
				return null;
			}
			set
			{
				// no edge available a target could be set
			}
		}


		/// <seealso cref="de.unika.ipd.grgen.ast.pattern.ConnectionCharacter.addNodes(java.util.Set) "/>
		public override void AddNodes(ISet<NodeDeclNode> set)
		{
			Debug.Assert(IsResolved());

			set.Add(node);
		}

		public static string KindStr
		{
			get
			{
				return "single node connection";
			}
		}
	}

}
