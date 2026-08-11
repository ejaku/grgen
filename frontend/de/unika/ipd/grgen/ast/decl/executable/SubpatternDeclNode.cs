/*
 * GrGen: graph rewrite generator tool -- release GrGen.NET 8.1
 * Copyright (C) 2003-2026 Universitaet Karlsruhe, Institut fuer Programmstrukturen und Datenorganisation, LS Goos; and free programmers
 * licensed under LGPL v3, some components/parts use different licenses (see LICENSE.txt included in the packaging of this file)
 * www.grgen.de / www.grgen.net
 */

/// <summary>
/// @author Edgar Jakumeit
/// </summary>

namespace de.unika.ipd.grgen.ast.decl.executable
{

	using System.Collections.Generic;
	using System.Diagnostics;

	using BaseNode = de.unika.ipd.grgen.ast.BaseNode;
	using de.unika.ipd.grgen.ast;
	using IdentNode = de.unika.ipd.grgen.ast.IdentNode;
	using AlternativeCaseDeclNode = de.unika.ipd.grgen.ast.decl.pattern.AlternativeCaseDeclNode;
	using AlternativeDeclNode = de.unika.ipd.grgen.ast.decl.pattern.AlternativeDeclNode;
	using IteratedDeclNode = de.unika.ipd.grgen.ast.decl.pattern.IteratedDeclNode;
	using ModifyDeclNode = de.unika.ipd.grgen.ast.decl.pattern.ModifyDeclNode;
	using RhsDeclNode = de.unika.ipd.grgen.ast.decl.pattern.RhsDeclNode;
	using SubpatternUsageDeclNode = de.unika.ipd.grgen.ast.decl.pattern.SubpatternUsageDeclNode;
	using VarDeclNode = de.unika.ipd.grgen.ast.decl.pattern.VarDeclNode;
	using ExprNode = de.unika.ipd.grgen.ast.expr.ExprNode;
	using PatternGraphRhsNode = de.unika.ipd.grgen.ast.pattern.PatternGraphRhsNode;
	using OrderedReplacementsNode = de.unika.ipd.grgen.ast.pattern.OrderedReplacementsNode;
	using PatternGraphLhsNode = de.unika.ipd.grgen.ast.pattern.PatternGraphLhsNode;
	using SubpatternReplNode = de.unika.ipd.grgen.ast.pattern.SubpatternReplNode;
	using EvalStatementsNode = de.unika.ipd.grgen.ast.stmt.EvalStatementsNode;
	using TypeNode = de.unika.ipd.grgen.ast.type.TypeNode;
	using SubpatternTypeNode = de.unika.ipd.grgen.ast.type.executable.SubpatternTypeNode;
	using de.unika.ipd.grgen.ast.util;
	using IR = de.unika.ipd.grgen.ir.IR;
	using Rule = de.unika.ipd.grgen.ir.executable.Rule;
	using RuleKind = de.unika.ipd.grgen.ir.executable.Rule.RuleKind;
	using PatternGraphLhs = de.unika.ipd.grgen.ir.pattern.PatternGraphLhs;
	using PatternGraphRhs = de.unika.ipd.grgen.ir.pattern.PatternGraphRhs;
	using EvalStatements = de.unika.ipd.grgen.ir.stmt.EvalStatements;

	/// <summary>
	/// AST node for a pattern with replacements.
	/// </summary>
	public class SubpatternDeclNode : TopLevelMatcherDeclNode
	{
		static SubpatternDeclNode()
		{
			SetClassName(typeof(SubpatternDeclNode), "subpattern declaration");
		}

		public RhsDeclNode right;
		private SubpatternTypeNode type;

		/// <summary>
		/// Type for this declaration. </summary>
		private static readonly TypeNode subpatternType = new SubpatternTypeNode();

		/// <summary>
		/// Make a new rule. </summary>
		/// <param name="id"> The identifier of this rule. </param>
		/// <param name="left"> The left hand side (The pattern to match). </param>
		/// <param name="right"> The right hand side. </param>
		public SubpatternDeclNode(IdentNode id, PatternGraphLhsNode left, RhsDeclNode right)
			: base(id, subpatternType, left)
		{
			this.right = right;
			BecomeParent(this.right);
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
				children.Add(pattern);
				if(right != null)
					children.Add(right);
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
				childrenNames.Add("pattern");
				if(right != null)
					childrenNames.Add("right");
				return childrenNames;
			}
		}

		private static DeclarationTypeResolver<SubpatternTypeNode> typeResolver =
				new DeclarationTypeResolver<SubpatternTypeNode>(typeof(SubpatternTypeNode));

		/// <seealso cref="de.unika.ipd.grgen.ast.BaseNode.resolveLocal() "/>
		protected internal override bool ResolveLocal()
		{
			type = typeResolver.Resolve(typeUnresolved, this);

			if(right == null && RewritePartRequired())
			{
				CollectNode<BaseNode> connections = new CollectNode<BaseNode>();
				CollectNode<VarDeclNode> defVariablesToBeYieldedTo = new CollectNode<VarDeclNode>();
				CollectNode<SubpatternUsageDeclNode> subpatterns = new CollectNode<SubpatternUsageDeclNode>();
				CollectNode<OrderedReplacementsNode> orderedReplacements = new CollectNode<OrderedReplacementsNode>();
				CollectNode<EvalStatementsNode> evalStatments = new CollectNode<EvalStatementsNode>();
				CollectNode<ExprNode> returnz = new CollectNode<ExprNode>();
				CollectNode<BaseNode> imperativeStmts = new CollectNode<BaseNode>();
				PatternGraphRhsNode patternGraph = new PatternGraphRhsNode(Ident.ToString(), Ident.Coords,
						connections, new CollectNode<BaseNode>(), subpatterns, new CollectNode<SubpatternReplNode>(),
						orderedReplacements, returnz, imperativeStmts,
						BaseNode.CONTEXT_PATTERN | BaseNode.CONTEXT_RHS, pattern);
				patternGraph.AddDefVariablesToBeYieldedTo(defVariablesToBeYieldedTo);
				patternGraph.AddEvals(evalStatments);
				right = new ModifyDeclNode(Ident, patternGraph, new CollectNode<IdentNode>());
				Ident.Decl = this;
			}

			return type != null;
		}

		private bool RewritePartRequired()
		{
			foreach(AlternativeDeclNode alt in pattern.alts.ChildrenExact)
			{
				foreach(AlternativeCaseDeclNode altCase in alt.ChildrenExact)
				{
					if(altCase.right != null)
						return true;
				}
			}

			foreach(IteratedDeclNode iter in pattern.iters.ChildrenExact)
			{
				if(iter.right != null)
					return true;
			}

			return false;
		}

		protected internal override bool CheckLocal()
		{
			bool nonActionIsOk = base.CheckNonAction(right);
			bool abstr = true;
			bool noAmbiguousRetypes = true;
			if(right != null)
			{
				abstr = NoAbstractElementInstantiated(right);
				noAmbiguousRetypes = NoAmbiguousRetypes(right);
			}
			return nonActionIsOk & abstr & noAmbiguousRetypes;
		}

		public virtual PatternGraphLhsNode Pattern
		{
			get
			{
				Debug.Assert(IsResolved());
				return pattern;
			}
		}

		/// <seealso cref="de.unika.ipd.grgen.ast.BaseNode.constructIR()"/>
		protected internal override IR ConstructIR()
		{
			// return if the pattern graph already constructed the IR object
			// that may happen in recursive patterns (and other usages/references)
			if(IsIRAlreadySet())
				return IR;

			Rule rule = new Rule(Ident.IRIdent, Rule.RuleKind.Subpattern);

			// mark this node as already visited
			IR = rule;

			PatternGraphLhs left = pattern.IRPatternGraphLhs;

			PatternGraphRhs rightPattern = null;
			if(this.right != null)
				rightPattern = this.right.GetIRPatternGraph(left);

			rule.Initialize(left, rightPattern);

			ConstructImplicitNegs(left);
			ConstructIRaux(rule, right);

			// add Eval statements to the IR
			if(this.right != null)
			{
				foreach(EvalStatements n in this.right.RhsGraph.EvalStatements)
					rule.AddEval(n);
			}

			return rule;
		}

		public override TypeNode DeclType
		{
			get
			{
				Debug.Assert(IsResolved());

				return type;
			}
		}

		public static string KindStr
		{
			get
			{
				return "(sub)pattern";
			}
		}
	}

}
