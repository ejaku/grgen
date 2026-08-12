/*
 * GrGen: graph rewrite generator tool -- release GrGen.NET 8.1
 * Copyright (C) 2003-2026 Universitaet Karlsruhe, Institut fuer Programmstrukturen und Datenorganisation, LS Goos; and free programmers
 * licensed under LGPL v3, some components/parts use different licenses (see LICENSE.txt included in the packaging of this file)
 * www.grgen.de / www.grgen.net
 */

/// <summary>
/// @author Sebastian Hack
/// </summary>

namespace de.unika.ipd.grgen.ast.decl.pattern
{

	using System.Collections.Generic;
	using System.Diagnostics;

	using BaseNode = de.unika.ipd.grgen.ast.BaseNode;
	using IdentNode = de.unika.ipd.grgen.ast.IdentNode;
	using DeclNode = de.unika.ipd.grgen.ast.decl.DeclNode;
	using TypeDeclNode = de.unika.ipd.grgen.ast.decl.TypeDeclNode;
	using InheritanceTypeNode = de.unika.ipd.grgen.ast.model.type.InheritanceTypeNode;
	using NodeTypeNode = de.unika.ipd.grgen.ast.model.type.NodeTypeNode;
	using NameOrAttributeInitializationNode = de.unika.ipd.grgen.ast.pattern.NameOrAttributeInitializationNode;
	using PatternGraphLhsNode = de.unika.ipd.grgen.ast.pattern.PatternGraphLhsNode;
	using TypeExprNode = de.unika.ipd.grgen.ast.type.TypeExprNode;
	using TypeNode = de.unika.ipd.grgen.ast.type.TypeNode;
	using Checker = de.unika.ipd.grgen.ast.util.Checker;
	using de.unika.ipd.grgen.ast.util;
	using de.unika.ipd.grgen.ast.util;
	using TypeChecker = de.unika.ipd.grgen.ast.util.TypeChecker;
	using IR = de.unika.ipd.grgen.ir.IR;
	using Expression = de.unika.ipd.grgen.ir.expr.Expression;
	using NodeType = de.unika.ipd.grgen.ir.model.type.NodeType;
	using NameOrAttributeInitialization = de.unika.ipd.grgen.ir.pattern.NameOrAttributeInitialization;
	using Node = de.unika.ipd.grgen.ir.pattern.Node;
	using Color = de.unika.ipd.grgen.util.Color;

	/// <summary>
	/// Declaration of a node.
	/// </summary>
	public class NodeDeclNode : ConstraintDeclNode
	{
		static NodeDeclNode()
		{
			SetClassName(typeof(NodeDeclNode), "node");
		}

		protected internal NodeDeclNode typeNodeDecl = null;
		protected internal TypeDeclNode typeTypeDecl = null;

		private static DeclarationPairResolver<NodeDeclNode, TypeDeclNode> typeResolver =
				new DeclarationPairResolver<NodeDeclNode, TypeDeclNode>(typeof(NodeDeclNode), typeof(TypeDeclNode));

		public NodeDeclNode(IdentNode id, BaseNode type, CopyKind copyKind, int context, TypeExprNode constr,
				PatternGraphLhsNode directlyNestingLHSGraph, bool maybeNull, bool defEntityToBeYieldedTo)
			: base(id, type, copyKind, context, constr, directlyNestingLHSGraph, maybeNull, defEntityToBeYieldedTo)
		{
		}

		public NodeDeclNode(IdentNode id, BaseNode type, CopyKind copyKind, int context, TypeExprNode constr,
				PatternGraphLhsNode directlyNestingLHSGraph)
			: this(id, type, copyKind, context, constr, directlyNestingLHSGraph, false, false)
		{
		}

		public virtual NodeDeclNode CloneForAuto(PatternGraphLhsNode directlyNestingLhsGraph)
		{
			//new IdentNode(env.define(ParserEnvironment.ENTITIES, i.getText(), getCoords(i)));	
			NodeDeclNode clone = new NodeDeclNode(ident, typeUnresolved,
					copyKind, context, constraints, directlyNestingLhsGraph, maybeNull, defEntityToBeYieldedTo);
			clone.Resolve();
			if(typeNodeDecl != null)
			{
				ReportError("A typeof node cannot be used in an auto statement"
						+ " (as is the case for " + Ident + ").");
			}
			return clone;
		}

		public override TypeNode DeclType
		{
			get
			{
				return DeclInhType;
			}
		}

		public override InheritanceTypeNode DeclInhType
		{
			get
			{
				return DeclNodeType;
			}
		}

		/// <summary>
		/// The TYPE child could be a node in case the type is
		///  inherited dynamically via the typeof/copy operator 
		/// </summary>
		public virtual NodeTypeNode DeclNodeType
		{
			get
			{
				Debug.Assert(IsResolved());
				DeclNode curr = GetValidResolvedVersion<DeclNode>(typeNodeDecl, typeTypeDecl);
				TypeNode type = curr.DeclType;
				//assert type != null;
				return (NodeTypeNode)type;
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
				children.Add(GetValidVersion(typeUnresolved, typeNodeDecl, typeTypeDecl));
				children.Add(constraints);
				children.Add(nameOrAttributeInits);
				if(initialization != null)
					children.Add(initialization);
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
				childrenNames.Add("constraints");
				childrenNames.Add("nameOrAttributeInits");
				if(initialization != null)
					childrenNames.Add("initialization expression");
				return childrenNames;
			}
		}

		/// <seealso cref="de.unika.ipd.grgen.ast.BaseNode.resolveLocal() "/>
		protected internal override bool ResolveLocal()
		{
			Pair<NodeDeclNode, TypeDeclNode> resolved = typeResolver.Resolve(typeUnresolved, this);
			if(resolved == null)
				return false;

			typeNodeDecl = resolved.fst;
			typeTypeDecl = resolved.snd;

			TypeDeclNode typeDecl;

			if(typeNodeDecl != null)
			{
				HashSet<NodeDeclNode> visited = new HashSet<NodeDeclNode>();
				NodeDeclNode prev = typeNodeDecl;
				NodeDeclNode cur = typeNodeDecl.typeNodeDecl;

				while(cur != null)
				{
					if(visited.Contains(cur))
					{
						ReportError("Circular typeof/copy not allowed"
								+ " (as is the case for " + Kind + " " + Ident + ").");
						return false;
					}
					visited.Add(cur);
					prev = cur;
					cur = cur.typeNodeDecl;
				}

				if(prev.typeTypeDecl == null && !prev.Resolve())
					return false;
				typeDecl = prev.typeTypeDecl;
			}
			else
				typeDecl = typeTypeDecl;

			if(!typeDecl.Resolve())
				return false;
			if(!(typeDecl.DeclType is NodeTypeNode))
			{
				typeUnresolved.ReportError("Type of node" + this.EmptyWhenAnonymousPostfix(" ") + " must be a node type"
						+ " (given is " + typeDecl.DeclType.Kind + " " + typeDecl.DeclType.TypeName
						+ " - use -edge-> syntax for edges, var for variables, ref for containers).");
				return false;
			}
			return true;
		}

		/// <summary>
		/// Warn on typeofs of new created graph nodes (with known type).
		/// </summary>
		private void WarnOnTypeofOfRhsNodes()
		{
			if((context & CONTEXT_LHS_OR_RHS) == CONTEXT_LHS)
				return;

			// As long as we're typed with a rhs edge we change our type to the type of that node,
			// the first time we do so we emit a warning to the user (further steps will be warned by the elements reached there)
			bool firstTime = true;
			while(InheritsType() && (typeNodeDecl.context & CONTEXT_LHS_OR_RHS) == CONTEXT_RHS)
			{
				if(firstTime)
				{
					firstTime = false;
					ReportWarning("Type of node " + typeNodeDecl.ident + " is statically known"
							+ " (to be " + typeNodeDecl.DeclType.TypeName + ", the typeof is thus pointless).");
				}
				typeTypeDecl = typeNodeDecl.typeTypeDecl;
				typeNodeDecl = typeNodeDecl.typeNodeDecl;
			}
			// either reached a statically known type by walking rhs elements
			// or reached a lhs element (with statically unknown type as it matches any subtypes)
		}

		private static readonly Checker typeChecker = new TypeChecker(typeof(NodeTypeNode));

		protected internal override bool CheckLocal()
		{
			WarnOnTypeofOfRhsNodes();

			return base.CheckLocal()
				& typeChecker.Check(GetValidResolvedVersion<DeclNode>(typeNodeDecl, typeTypeDecl), error);
		}

		/// <summary>
		/// Yields a dummy <code>NodeDeclNode</code> needed as
		/// dummy tgt or src node for dangling edges.
		/// </summary>
		public static NodeDeclNode GetDummy(IdentNode id, BaseNode type, int context,
				PatternGraphLhsNode directlyNestingLHSGraph)
		{
			return new DummyNodeDeclNode(id, type, context, directlyNestingLHSGraph);
		}

		public virtual bool IsDummy()
		{
			return false;
		}

		/// <seealso cref="de.unika.ipd.grgen.util.GraphDumpable.getNodeColor() "/>
		public override Color NodeColor
		{
			get
			{
				return Color.GREEN;
			}
		}

		/// <summary>
		/// Get the IR object correctly casted. </summary>
		/// <returns> The node IR object. </returns>
		public virtual Node IRNode
		{
			get
			{
				return CheckIR<Node>(typeof(Node));
			}
		}

		public bool InheritsType()
		{
			Debug.Assert(IsResolved());

			return typeNodeDecl != null;
		}

		/// <seealso cref="de.unika.ipd.grgen.ast.BaseNode.constructIR()"/>
		protected internal override IR ConstructIR()
		{
			NodeTypeNode tn = DeclNodeType;
			NodeType nt = tn.IRNodeType;
			IdentNode ident = Ident;

			Node node = new Node(ident.IRIdent, nt, ident.Annotations,
					directlyNestingLHSGraph != null ? directlyNestingLHSGraph.IRPatternGraphLhs : null,
					IsMaybeDeleted(), IsMaybeRetyped(), defEntityToBeYieldedTo, context);
			node.Constraints = IRConstraints.Evaluate();

			if(node.Constraints.Contains(node.InheritanceType)) // TODO: supertype? only subtypes allowed
			{
				ReportError("The own node type may not be contained in the type constraint list"
						+ " (but " + node.Type + " is contained for " + Ident + ").");
			}

			if(InheritsType())
				node.SetTypeofCopy(typeNodeDecl.CheckIR<Node>(typeof(Node)), copyKind);

			node.MaybeNull = maybeNull;

			if(initialization != null)
			{
				initialization = initialization.Evaluate();
				node.Initialization = initialization.CheckIR<Expression>(typeof(Expression));
			}

			foreach(NameOrAttributeInitializationNode nain in nameOrAttributeInits.ChildrenExact)
			{
				nain.ownerIR = node;
				node.AddNameOrAttributeInitialization(nain.CheckIR<NameOrAttributeInitialization>(typeof(NameOrAttributeInitialization)));
			}

			return node;
		}

		public static string KindStr
		{
			get
			{
				return "node";
			}
		}
	}

}
