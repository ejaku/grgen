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
	using EdgeTypeNode = de.unika.ipd.grgen.ast.model.type.EdgeTypeNode;
	using InheritanceTypeNode = de.unika.ipd.grgen.ast.model.type.InheritanceTypeNode;
	using NameOrAttributeInitializationNode = de.unika.ipd.grgen.ast.pattern.NameOrAttributeInitializationNode;
	using PatternGraphLhsNode = de.unika.ipd.grgen.ast.pattern.PatternGraphLhsNode;
	using TypeExprNode = de.unika.ipd.grgen.ast.type.TypeExprNode;
	using TypeNode = de.unika.ipd.grgen.ast.type.TypeNode;
	using Checker = de.unika.ipd.grgen.ast.util.Checker;
	using de.unika.ipd.grgen.ast.util;
	using de.unika.ipd.grgen.ast.util;
	using TypeChecker = de.unika.ipd.grgen.ast.util.TypeChecker;
	using Expression = de.unika.ipd.grgen.ir.expr.Expression;
	using EdgeType = de.unika.ipd.grgen.ir.model.type.EdgeType;
	using Edge = de.unika.ipd.grgen.ir.pattern.Edge;
	using NameOrAttributeInitialization = de.unika.ipd.grgen.ir.pattern.NameOrAttributeInitialization;
	using IR = de.unika.ipd.grgen.ir.IR;

	public class EdgeDeclNode : ConstraintDeclNode
	{
		static EdgeDeclNode()
		{
			SetClassName(typeof(EdgeDeclNode), "edge");
		}

		protected internal EdgeDeclNode typeEdgeDecl = null;
		protected internal TypeDeclNode typeTypeDecl = null;

		protected internal static readonly DeclarationPairResolver<EdgeDeclNode, TypeDeclNode> typeResolver =
				new DeclarationPairResolver<EdgeDeclNode, TypeDeclNode>(typeof(EdgeDeclNode), typeof(TypeDeclNode));

		public EdgeDeclNode(IdentNode id, BaseNode type, CopyKind copyKind, int context, TypeExprNode constraints,
				PatternGraphLhsNode directlyNestingLHSGraph, bool maybeNull, bool defEntityToBeYieldedTo)
			: base(id, type, copyKind, context, constraints, directlyNestingLHSGraph, maybeNull, defEntityToBeYieldedTo)
		{
		}

		public EdgeDeclNode(IdentNode id, BaseNode type, CopyKind copyKind, int context, TypeExprNode constraints,
				PatternGraphLhsNode directlyNestingLHSGraph)
			: this(id, type, copyKind, context, constraints, directlyNestingLHSGraph, false, false)
		{
		}

		public virtual EdgeDeclNode CloneForAuto(PatternGraphLhsNode directlyNestingLhsGraph)
		{
			EdgeDeclNode clone = new EdgeDeclNode(ident, typeUnresolved,
					copyKind, context, constraints, directlyNestingLhsGraph, maybeNull, defEntityToBeYieldedTo);
			clone.Resolve();
			if(typeEdgeDecl != null)
			{
				ReportError("A typeof edge cannot be used in an auto statement"
						+ " (as is the case for " + Ident + ").");
			}
			return clone;
		}

		/// <summary>
		/// Create EdgeDeclNode and immediately resolve and check it.
		/// NOTE: Use this to create and insert an EdgeDeclNode into the AST after
		/// the AST is already checked.
		/// </summary>
		public EdgeDeclNode(IdentNode id, TypeDeclNode type, int declLocation, BaseNode parent,
				PatternGraphLhsNode directlyNestingLHSGraph)
			: this(id, type, CopyKind.None, declLocation, TypeExprNode.Empty, directlyNestingLHSGraph)
		{
			parent.BecomeParent(this);

			Resolve();
			Check();
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
				return DeclEdgeType;
			}
		}

		/// <summary>
		/// The TYPE child could be an edge in case the type is
		///  inherited dynamically via the typeof/copy operator 
		/// </summary>
		public virtual EdgeTypeNode DeclEdgeType
		{
			get
			{
				Debug.Assert(IsResolved());
				DeclNode curr = GetValidResolvedVersion(typeEdgeDecl, typeTypeDecl);
				TypeNode type = curr.DeclType;
				//assert curr.getDeclType() != null;
				return (EdgeTypeNode)type;
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
				children.Add(GetValidVersion(typeUnresolved, typeEdgeDecl, typeTypeDecl));
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
			Pair<EdgeDeclNode, TypeDeclNode> resolved = typeResolver.Resolve(typeUnresolved, this);
			if(resolved == null)
				return false;

			typeEdgeDecl = resolved.fst;
			typeTypeDecl = resolved.snd;

			TypeDeclNode typeDecl;

			if(typeEdgeDecl != null)
			{
				HashSet<EdgeDeclNode> visited = new HashSet<EdgeDeclNode>();
				EdgeDeclNode prev = typeEdgeDecl;
				EdgeDeclNode cur = typeEdgeDecl.typeEdgeDecl;
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
					cur = cur.typeEdgeDecl;
				}
				typeDecl = prev.typeTypeDecl;
			}
			else
				typeDecl = typeTypeDecl;

			if(!typeDecl.Resolve())
				return false;
			if(!(typeDecl.DeclType is EdgeTypeNode))
			{
				typeUnresolved.ReportError("Type of edge" + this.EmptyWhenAnonymousPostfix(" ") + " must be an edge type"
						+ " (given is " + typeDecl.DeclType.Kind + " " + typeDecl.DeclType.TypeName + ").");
				return false;
			}

			return true;
		}

		/// <summary>
		/// Warn on typeofs of new created graph edges (with known type).
		/// </summary>
		private void WarnOnTypeofOfRhsEdges()
		{
			if((context & CONTEXT_LHS_OR_RHS) == CONTEXT_LHS)
				return;

			// As long as we're typed with a rhs edge we change our type to the type of that edge,
			// the first time we do so we emit a warning to the user (further steps will be warned by the elements reached there)
			bool firstTime = true;
			while(InheritsType() && (typeEdgeDecl.context & CONTEXT_LHS_OR_RHS) == CONTEXT_RHS)
			{
				if(firstTime)
				{
					firstTime = false;
					ReportWarning("Type of edge " + typeEdgeDecl.ident + " is statically known"
								+ " (to be " + typeEdgeDecl.DeclType.TypeName + ", the typeof is thus pointless).");
				}
				typeTypeDecl = typeEdgeDecl.typeTypeDecl;
				typeEdgeDecl = typeEdgeDecl.typeEdgeDecl;
			}
			// either reached a statically known type by walking rhs elements
			// or reached a lhs element (with statically unknown type as it matches any subtypes)
		}

		private static readonly Checker typeChecker = new TypeChecker(typeof(EdgeTypeNode));

		protected internal override bool CheckLocal()
		{
			WarnOnTypeofOfRhsEdges();

			return base.CheckLocal()
					& typeChecker.Check(GetValidResolvedVersion(typeEdgeDecl, typeTypeDecl), error);
		}

		/// <summary>
		/// Edges have more info to give </summary>
		/// <seealso cref="de.unika.ipd.grgen.util.GraphDumpableNode.getNodeInfo()"/>
		protected internal override string ExtraNodeInfo()
		{
			return "";
		}

		/// <seealso cref="de.unika.ipd.grgen.util.GraphDumpable.getNodeColor()"/>
		public override Color NodeColor
		{
			get
			{
				return Color.YELLOW;
			}
		}

		/// <summary>
		/// Get the IR object correctly casted. </summary>
		/// <returns> The edge IR object. </returns>
		public virtual Edge IREdge
		{
			get
			{
				return CheckIR(typeof(Edge));
			}
		}

		public bool InheritsType()
		{
			Debug.Assert(IsResolved());

			return typeEdgeDecl != null;
		}

		/// <seealso cref="de.unika.ipd.grgen.ast.BaseNode.constructIR() "/>
		protected internal override IR ConstructIR()
		{
			TypeNode tn = DeclType;
			EdgeType et = tn.CheckIR(typeof(EdgeType));
			IdentNode ident = Ident;

			Edge edge = new Edge(ident.IRIdent, et, ident.Annotations,
					directlyNestingLHSGraph != null ? directlyNestingLHSGraph.IRPatternGraphLhs : null,
					IsMaybeDeleted(), IsMaybeRetyped(), defEntityToBeYieldedTo, context);
			edge.SetConstraints(IRConstraints.Evaluate());

			if(edge.GetConstraints().Contains(edge.InheritanceType)) // TODO: supertype? only subtypes allowed
			{
				ReportError("The own edge type may not be contained in the type constraint list"
						+ " (but " + edge.Type + " is contained for " + Ident + ").");
			}

			if(InheritsType())
				edge.SetTypeofCopy(typeEdgeDecl.CheckIR(typeof(Edge)), copyKind);

			edge.MaybeNull = maybeNull;

			if(initialization != null)
			{
				initialization = initialization.Evaluate();
				edge.Initialization = initialization.CheckIR(typeof(Expression));
			}

			foreach(NameOrAttributeInitializationNode nain in nameOrAttributeInits.ChildrenExact)
			{
				nain.ownerIR = edge;
				edge.AddNameOrAttributeInitialization(nain.CheckIR(typeof(NameOrAttributeInitialization)));
			}

			return edge;
		}

		public static string KindStr
		{
			get
			{
				return "edge";
			}
		}
	}

}
