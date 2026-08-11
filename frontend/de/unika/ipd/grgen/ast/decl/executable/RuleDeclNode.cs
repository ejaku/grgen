/*
 * GrGen: graph rewrite generator tool -- release GrGen.NET 8.1
 * Copyright (C) 2003-2026 Universitaet Karlsruhe, Institut fuer Programmstrukturen und Datenorganisation, LS Goos; and free programmers
 * licensed under LGPL v3, some components/parts use different licenses (see LICENSE.txt included in the packaging of this file)
 * www.grgen.de / www.grgen.net
 */

/// <summary>
/// @author Sebastian Hack, Daniel Grund
/// </summary>

namespace de.unika.ipd.grgen.ast.decl.executable
{

	using System.Collections.Generic;
	using System.Diagnostics;

	using BaseNode = de.unika.ipd.grgen.ast.BaseNode;
	using de.unika.ipd.grgen.ast;
	using EmitNode = de.unika.ipd.grgen.ast.EmitNode;
	using IdentNode = de.unika.ipd.grgen.ast.IdentNode;
	using DeclNode = de.unika.ipd.grgen.ast.decl.DeclNode;
	using ConstraintDeclNode = de.unika.ipd.grgen.ast.decl.pattern.ConstraintDeclNode;
	using EdgeDeclNode = de.unika.ipd.grgen.ast.decl.pattern.EdgeDeclNode;
	using NodeDeclNode = de.unika.ipd.grgen.ast.decl.pattern.NodeDeclNode;
	using RhsDeclNode = de.unika.ipd.grgen.ast.decl.pattern.RhsDeclNode;
	using VarDeclNode = de.unika.ipd.grgen.ast.decl.pattern.VarDeclNode;
	using DeclExprNode = de.unika.ipd.grgen.ast.expr.DeclExprNode;
	using ExprNode = de.unika.ipd.grgen.ast.expr.ExprNode;
	using MemberAccessExprNode = de.unika.ipd.grgen.ast.expr.MemberAccessExprNode;
	using InheritanceTypeNode = de.unika.ipd.grgen.ast.model.type.InheritanceTypeNode;
	using PatternGraphRhsNode = de.unika.ipd.grgen.ast.pattern.PatternGraphRhsNode;
	using PatternGraphLhsNode = de.unika.ipd.grgen.ast.pattern.PatternGraphLhsNode;
	using DefinedMatchTypeNode = de.unika.ipd.grgen.ast.type.DefinedMatchTypeNode;
	using TypeNode = de.unika.ipd.grgen.ast.type.TypeNode;
	using RuleTypeNode = de.unika.ipd.grgen.ast.type.executable.RuleTypeNode;
	using de.unika.ipd.grgen.ast.util;
	using IR = de.unika.ipd.grgen.ir.IR;
	using Rule = de.unika.ipd.grgen.ir.executable.Rule;
	using RuleKind = de.unika.ipd.grgen.ir.executable.Rule.RuleKind;
	using PatternGraphLhs = de.unika.ipd.grgen.ir.pattern.PatternGraphLhs;
	using PatternGraphRhs = de.unika.ipd.grgen.ir.pattern.PatternGraphRhs;
	using Variable = de.unika.ipd.grgen.ir.pattern.Variable;
	using EvalStatements = de.unika.ipd.grgen.ir.stmt.EvalStatements;
	using DefinedMatchType = de.unika.ipd.grgen.ir.type.DefinedMatchType;

	/// <summary>
	/// AST node for a replacement rule.
	/// </summary>
	public class RuleDeclNode : ActionDeclNode
	{
		static RuleDeclNode()
		{
			SetClassName(typeof(RuleDeclNode), "rule declaration");
		}

		public RhsDeclNode right;

		/// <summary>
		/// Type for this declaration. </summary>
		private RuleTypeNode type;
		private static readonly TypeNode ruleType = new RuleTypeNode();


		/// <summary>
		/// Make a new rule. </summary>
		/// <param name="id"> The identifier of this rule. </param>
		/// <param name="left"> The left hand side (The pattern to match). </param>
		/// <param name="right"> The right hand side. </param>
		public RuleDeclNode(IdentNode id, PatternGraphLhsNode left, CollectNode<IdentNode> implementedMatchTypes,
				RhsDeclNode right, CollectNode<BaseNode> rets)
			: base(id, ruleType, left, implementedMatchTypes, rets)
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
				children.Add(GetValidVersionCollectNode(returnFormalParametersUnresolved, returnFormalParameters));
				children.Add(pattern);
				children.Add(GetValidVersionCollectNode(implementedMatchTypesUnresolved, implementedMatchTypes));
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
				childrenNames.Add("ret");
				childrenNames.Add("pattern");
				childrenNames.Add("implementedMatchTypes");
				childrenNames.Add("right");
				return childrenNames;
			}
		}

		protected internal static readonly DeclarationTypeResolver<RuleTypeNode> typeResolver =
				new DeclarationTypeResolver<RuleTypeNode>(typeof(RuleTypeNode));

		/// <seealso cref="de.unika.ipd.grgen.ast.BaseNode.resolveLocal() "/>
		protected internal override bool ResolveLocal()
		{
			bool matchAndReturnTypesAreOk = base.ResolveLocal();

			type = typeResolver.Resolve(typeUnresolved, this);

			bool filtersOk = true;
			foreach(FilterAutoDeclNode filter in filters)
			{
				if(filter is FilterAutoSuppliedDeclNode)
					filtersOk &= ((FilterAutoSuppliedDeclNode)filter).Resolve();
				else //if(filter instanceof FilterAutoGeneratedNode)
					filtersOk &= ((FilterAutoGeneratedDeclNode)filter).Resolve();
			}

			return matchAndReturnTypesAreOk
					& type != null
					& filtersOk;
		}

		public virtual ISet<ConstraintDeclNode> DeletedElements
		{
			get
			{
				return right.GetElementsToDelete(pattern);
			}
		}

		/// <summary>
		/// Check that only graph elements are returned, that are not deleted.
		/// 
		/// The check also consider the case that a node is returned and homomorphic
		/// matching is allowed with a deleted node.
		/// </summary>
		private bool CheckReturnedElementsNotDeleted()
		{
			Debug.Assert(IsResolved());

			bool valid = true;
			ISet<ConstraintDeclNode> deletedElements = right.GetElementsToDelete(pattern);
			ISet<ConstraintDeclNode> maybeDeletedElements = right.GetMaybeDeletedElements(pattern);

			foreach(ExprNode expr in right.patternGraph.returns.ChildrenExact)
			{
				HashSet<ConstraintDeclNode> potentiallyResultingElements = new HashSet<ConstraintDeclNode>();
				expr.GetPotentiallyResultingElements(potentiallyResultingElements);
				foreach(ConstraintDeclNode potentiallyResultingElement in potentiallyResultingElements)
					valid &= CheckReturnedElementNotDeleted(potentiallyResultingElement, deletedElements, maybeDeletedElements);
			}

			return valid;
		}

		private static bool CheckReturnedElementNotDeleted(ConstraintDeclNode retElem,
				ISet<ConstraintDeclNode> deletedElements, ISet<ConstraintDeclNode> maybeDeletedElements)
		{
			if(deletedElements.Contains(retElem))
			{
				retElem.ReportError("The deleted " + retElem.Kind + " " + retElem
						 + " is not allowed to be returned.");
				return false;
			}
			else if(maybeDeletedElements.Contains(retElem))
			{
				retElem.maybeDeleted = true;

				if(!retElem.Ident.Annotations.IsFlagSet("maybeDeleted"))
				{
					string errorMessage = "Returning " + retElem.Kind + " " + retElem + " that may be deleted.";
					errorMessage += " Possibly it is homomorphic with a deleted " + retElem.Kind;
					errorMessage += " (use a [maybeDeleted] annotation if you think that this does not cause problems)";

					if(retElem is EdgeDeclNode)
						errorMessage += ", or " + retElem + " is a dangling edge and a deleted node exists";
					errorMessage += ".";
					retElem.ReportError(errorMessage);
					return false;
				}
			}

			return true;
		}

		/// <summary>
		/// Check that only graph elements are returned, that are not retyped.
		/// 
		/// The check also consider the case that a node is returned and homomorphic
		/// matching is allowed with a retyped node.
		/// </summary>
		private bool CheckReturnedElementsNotRetyped()
		{
			Debug.Assert(IsResolved());

			bool valid = true;

			foreach(ExprNode expr in right.patternGraph.returns.ChildrenExact)
			{
				if(!(expr is DeclExprNode))
					continue;

				ConstraintDeclNode retElem = ((DeclExprNode)expr).ConstraintDecl;
				if(retElem == null)
					continue;

				if(retElem.RetypedElement != null)
				{
					valid = false;

					expr.ReportError("The retyped " + retElem.Kind + " " + retElem
							+ " is not allowed to be returned.");
				}
			}

			return valid;
		}

		/// <summary>
		/// Check that every graph element is retyped to at most one type.
		/// </summary>
		private bool CheckElementsNotRetypedToDifferentTypes()
		{
			Debug.Assert(IsResolved());

			bool valid = true;

			foreach(ISet<ConstraintDeclNode> homSet in pattern.Homs)
				valid &= CheckElementsInHomSetNotRetypedToDifferentTypes(homSet);

			return valid;
		}

		private static bool CheckElementsInHomSetNotRetypedToDifferentTypes(ISet<ConstraintDeclNode> homSet)
		{
			ConstraintDeclNode element = null;
			ConstraintDeclNode retypedElement = null;
			ConstraintDeclNode anotherElement = null;
			ConstraintDeclNode anotherRetypedElement = null;

			foreach(ConstraintDeclNode currentElement in homSet)
			{
				ConstraintDeclNode currentRetypedElement = currentElement.RetypedElement;

				if(currentRetypedElement != null)
				{
					InheritanceTypeNode currentType = currentRetypedElement.DeclInhType;

					if(retypedElement == null)
					{
						element = currentElement;
						retypedElement = currentRetypedElement;
					}
					else if(currentType != retypedElement.DeclType)
					{
						anotherElement = currentElement;
						anotherRetypedElement = currentRetypedElement;
						break;
					}
				}
			}

			bool multipleRetypes = anotherElement != null;
			if(multipleRetypes)
				retypedElement.ReportError("The " + element.Kind + " " + element
						+ " is retyped to " + retypedElement.DeclType.TypeName + ","
						+ " but the " + anotherElement.Kind + " " + anotherElement
						+ " it may be homomorphic to is retyped to " + anotherRetypedElement.DeclType.TypeName
						+ ".");

			return !multipleRetypes;
		}

		/// <summary>
		/// Check that only graph elements are retyped, that are not deleted.
		/// </summary>
		private bool CheckRetypedElementsNotDeleted()
		{
			Debug.Assert(IsResolved());

			bool valid = true;

			foreach(DeclNode decl in DeletedElements)
			{
				if(!(decl is ConstraintDeclNode))
					continue;

				ConstraintDeclNode retElem = ((ConstraintDeclNode)decl);

				if(retElem.RetypedElement != null)
				{
					valid = false;

					retElem.ReportError("The retyped " + retElem.Kind + " " + retElem
							+ " is not allowed to be deleted.");
				}
			}

			return valid;
		}

		private HashSet<ConstraintDeclNode> CollectNeededElements(ExprNode expr)
		{
			HashSet<ConstraintDeclNode> neededElements = new HashSet<ConstraintDeclNode>();
			if(expr is MemberAccessExprNode) // attribute access is decoupled via temporary variable, so deletion of element is ok
				return neededElements;

			foreach(BaseNode child in expr.Children)
			{
				if(child is ExprNode)
					neededElements.AddAll(CollectNeededElements((ExprNode)child));

				if(child is DeclExprNode)
					neededElements.Add(((DeclExprNode)child).ConstraintDecl);
				else if(child is ConstraintDeclNode)
					neededElements.Add((ConstraintDeclNode)child);
			}

			return neededElements;
		}

		/// <summary>
		/// Check that emit elements are not deleted.
		/// The check considers the case that parameters are deleted due to homomorphic matching.
		/// </summary>
		private bool CheckEmitElementsNotDeleted()
		{
			Debug.Assert(IsResolved());

			bool valid = true;
			ISet<ConstraintDeclNode> delete = right.GetElementsToDelete(pattern);
			ISet<ConstraintDeclNode> maybeDeleted = right.GetMaybeDeletedElements(pattern);

			foreach(BaseNode imperativeStmt in right.patternGraph.imperativeStmts.ChildrenExact)
			{
				if(!(imperativeStmt is EmitNode))
					continue;

				EmitNode emit = (EmitNode)imperativeStmt;
				foreach(BaseNode child in emit.Children)
				{
					ExprNode expr = (ExprNode)child;
					foreach(ConstraintDeclNode declNode in CollectNeededElements(expr))
						valid &= CheckEmitElementNotDeleted(declNode, expr, delete, maybeDeleted, emit);
				}
			}

			return valid;
		}

		private static bool CheckEmitElementNotDeleted(ConstraintDeclNode declNode, ExprNode expr,
				ISet<ConstraintDeclNode> delete, ISet<ConstraintDeclNode> maybeDeleted, EmitNode emit)
		{
			string emitVersion = emit.isDebug ? "emitdebug" : "emit";
			string emitHereVersion = emit.isDebug ? "emitheredebug" : "emithere";
			if(delete.Contains(declNode))
			{
				expr.ReportError("The deleted " + declNode.Kind + " " + declNode
						+ " is not allowed to be referenced in an " + emitVersion + " statement"
						+ " (you may use an " + emitHereVersion + " instead).");
				return false;
			}
			if(maybeDeleted.Contains(declNode))
			{
				declNode.maybeDeleted = true;

				if(!declNode.Ident.Annotations.IsFlagSet("maybeDeleted"))
				{
					string errorMessage = "The " + declNode.Kind + " " + declNode + " used in an " + emitVersion + " statement may be deleted.";
					errorMessage += " Possibly it is homomorphic with a deleted " + declNode.Kind;
					errorMessage += " (use a [maybeDeleted] annotation if you think that this does not cause problems)";

					if(declNode is EdgeDeclNode)
						errorMessage += ", or " + declNode + " is a dangling edge and a deleted node exists";

					errorMessage += " (you may use an " + emitHereVersion + " instead).";

					expr.ReportError(errorMessage);

					return false;
				}
			}

			return true;
		}

		private void CalcMaybeRetyped()
		{
			foreach(ISet<ConstraintDeclNode> homSet in pattern.Homs)
			{
				bool containsRetypedElem = false;
				foreach(ConstraintDeclNode elem in homSet)
				{
					if(elem.RetypedElement != null)
					{
						containsRetypedElem = true;
						break;
					}
				}

				// If there was one homomorphic element, which is retyped,
				// all non-retyped elements in the same hom group are marked
				// as maybeRetyped.
				if(containsRetypedElem)
				{
					foreach(ConstraintDeclNode elem in homSet)
					{
						if(elem.RetypedElement == null)
							elem.maybeRetyped = true;
					}
				}
			}
		}

		/// <summary>
		/// Check, if the rule type node is right.
		/// The children of a rule type are
		/// 1) a pattern for the left side.
		/// 2) a pattern for the right side. </summary>
		/// <seealso cref="de.unika.ipd.grgen.ast.BaseNode.checkLocal()"/>
		protected internal override bool CheckLocal()
		{
			bool leftHandGraphsOk = base.CheckLocal();

			bool rightHandGraphsOk = this.right.CheckAgainstLhsPattern(pattern);

			PatternGraphRhsNode right = this.right.patternGraph;

			// check if the pattern name equals the rule name
			// named rewrite parts are only allowed in subpatterns
			string ruleName = ident.ToString();
			if(!right.nameOfGraph.Equals(ruleName))
				this.right.ReportError("Named rewrite parts are not allowed in rules (only in (sub)patterns).");

			// check if parameters only exists for subpatterns
			if(right.@params.ChildrenExact.Count > 0)
				this.right.ReportError("Parameters for the rewrite part are not allowed in rules (only in (sub)patterns).");

			bool noReturnInPatternOk = true;
			if(pattern.returns.Size() > 0)
			{
				ReportError("A return statement is not allowed in the pattern part of a rule.");
				noReturnInPatternOk = false;
			}

			CalcMaybeRetyped();

			return leftHandGraphsOk
					& rightHandGraphsOk
					& CheckRhsReuse(this.right)
					& SameNumberOfRewriteParts(this.right, "rule")
					& noReturnInPatternOk
					& NoAbstractElementInstantiated(this.right)
					& CheckRetypedElementsNotDeleted()
					& CheckReturnedElementsNotDeleted()
					& CheckElementsNotRetypedToDifferentTypes()
					& CheckReturnedElementsNotRetyped()
					& CheckExecParamsNotDeleted(this.right)
					& CheckEmitElementsNotDeleted()
					& CheckReturns(right.returns)
					& NoAmbiguousRetypes(this.right);
		}

		public virtual NodeDeclNode TryGetNode(IdentNode ident)
		{
			foreach(NodeDeclNode node in pattern.Nodes)
			{
				if(node.ident.ToString().Equals(ident.ToString()))
					return node;
			}
			foreach(NodeDeclNode node in right.patternGraph.Nodes)
			{
				if(node.ident.ToString().Equals(ident.ToString()))
					return node;
			}
			return null;
		}

		public virtual EdgeDeclNode TryGetEdge(IdentNode ident)
		{
			foreach(EdgeDeclNode edge in pattern.Edges)
			{
				if(edge.ident.ToString().Equals(ident.ToString()))
					return edge;
			}
			foreach(EdgeDeclNode edge in right.patternGraph.Edges)
			{
				if(edge.ident.ToString().Equals(ident.ToString()))
					return edge;
			}
			return null;
		}

		public virtual VarDeclNode TryGetVar(IdentNode ident)
		{
			foreach(VarDeclNode var in pattern.defVariablesToBeYieldedTo.ChildrenExact)
			{
				if(var.ident.ToString().Equals(ident.ToString()))
					return var;
			}
			foreach(DeclNode varCand in pattern.ParamDecls)
			{
				if(!(varCand is VarDeclNode))
					continue;
				VarDeclNode var = (VarDeclNode)varCand;
				if(var.ident.ToString().Equals(ident.ToString()))
					return var;
			}
			foreach(VarDeclNode var in right.patternGraph.defVariablesToBeYieldedTo.ChildrenExact)
			{
				if(var.ident.ToString().Equals(ident.ToString()))
					return var;
			}
			foreach(DeclNode varCand in right.patternGraph.ParamDecls)
			{
				if(!(varCand is VarDeclNode))
					continue;
				VarDeclNode var = (VarDeclNode)varCand;
				if(var.ident.ToString().Equals(ident.ToString()))
					return var;
			}
			return null;
		}

		public static string KindStr
		{
			get
			{
				return "rule";
			}
		}

		/// <seealso cref="de.unika.ipd.grgen.ast.BaseNode.constructIR()"/>
		protected internal override IR ConstructIR()
		{
			// return if the pattern graph already constructed the IR object
			// that may happen in recursive patterns (and other usages/references)
			if(IsIRAlreadySet())
				return IR;

			Rule rule = new Rule(Ident.IRIdent, Rule.RuleKind.Rule);

			// mark this node as already visited
			IR = rule;

			PatternGraphLhs left = pattern.IRPatternGraphLhs;
			foreach(DeclNode varCand in pattern.ParamDecls)
			{
				if(!(varCand is VarDeclNode))
					continue;
				VarDeclNode var = (VarDeclNode)varCand;
				left.AddVariable(var.CheckIR(typeof(Variable)));
			}

			PatternGraphRhs right = this.right.GetIRPatternGraph(left);

			rule.Initialize(left, right);

			foreach(DefinedMatchTypeNode implementedMatchClassNode in implementedMatchTypes.ChildrenExact)
			{
				DefinedMatchType implementedMatchClass = implementedMatchClassNode.CheckIR(typeof(DefinedMatchType));
				rule.AddImplementedMatchClass(implementedMatchClass);
			}

			ConstructImplicitNegs(left);
			ConstructIRaux(rule, this.right.patternGraph.returns);

			// add eval statements to the IR
			foreach(EvalStatements evalStatement in this.right.RhsGraph.EvalStatements)
				rule.AddEval(evalStatement);

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
	}

}
