/*
 * GrGen: graph rewrite generator tool -- release GrGen.NET 8.1
 * Copyright (C) 2003-2026 Universitaet Karlsruhe, Institut fuer Programmstrukturen und Datenorganisation, LS Goos; and free programmers
 * licensed under LGPL v3, some components/parts use different licenses (see LICENSE.txt included in the packaging of this file)
 * www.grgen.de / www.grgen.net
 */

/// <summary>
/// ConstraintDeclNode.java
/// 
/// @author Sebastian Hack
/// </summary>

namespace de.unika.ipd.grgen.ast.decl.pattern
{
	using System.Collections.Generic;

	using BaseNode = de.unika.ipd.grgen.ast.BaseNode;
	using de.unika.ipd.grgen.ast;
	using IdentNode = de.unika.ipd.grgen.ast.IdentNode;
	using DeclNode = de.unika.ipd.grgen.ast.decl.DeclNode;
	using ExprNode = de.unika.ipd.grgen.ast.expr.ExprNode;
	using EdgeTypeNode = de.unika.ipd.grgen.ast.model.type.EdgeTypeNode;
	using InheritanceTypeNode = de.unika.ipd.grgen.ast.model.type.InheritanceTypeNode;
	using NodeTypeNode = de.unika.ipd.grgen.ast.model.type.NodeTypeNode;
	using NameOrAttributeInitializationNode = de.unika.ipd.grgen.ast.pattern.NameOrAttributeInitializationNode;
	using PatternGraphLhsNode = de.unika.ipd.grgen.ast.pattern.PatternGraphLhsNode;
	using TypeExprNode = de.unika.ipd.grgen.ast.type.TypeExprNode;
	using TypeNode = de.unika.ipd.grgen.ast.type.TypeNode;
	using TypeExpr = de.unika.ipd.grgen.ir.type.TypeExpr;


	public abstract class ConstraintDeclNode : DeclNode
	{
		public enum CopyKind
		{
			None,
			Clone,
			Copy
		}

		protected internal TypeExprNode constraints;

		public int context; // context of declaration, contains CONTEXT_LHS if declaration is located on left hand side,
							// or CONTEXT_RHS if declaration is located on right hand side

		public PatternGraphLhsNode directlyNestingLHSGraph;
		public bool defEntityToBeYieldedTo;

		protected internal CopyKind copyKind;

		/// <summary>
		/// The retyped version of this element if any. </summary>
		protected internal ConstraintDeclNode retypedElem = null;

		public bool maybeDeleted = false;
		public bool maybeRetyped = false;
		protected internal bool maybeNull = false;

		internal ExprNode initialization = null;

		internal CollectNode<NameOrAttributeInitializationNode> nameOrAttributeInits =
				new CollectNode<NameOrAttributeInitializationNode>();

		protected internal ConstraintDeclNode(IdentNode id, BaseNode type, CopyKind copyKind, int context, TypeExprNode constraints,
				PatternGraphLhsNode directlyNestingLHSGraph, bool maybeNull, bool defEntityToBeYieldedTo)
			: base(id, type)
		{
			this.copyKind = copyKind;
			this.constraints = constraints;
			BecomeParent(this.constraints);
			this.context = context;
			this.directlyNestingLHSGraph = directlyNestingLHSGraph;
			this.maybeNull = maybeNull;
			this.defEntityToBeYieldedTo = defEntityToBeYieldedTo;
		}

		/// <summary>
		/// sets an expression to be used to initialize the graph entity, only used for local variables, not pattern elements </summary>
		public virtual ExprNode Initialization
		{
			set
			{
				this.initialization = value;
			}
		}

		public virtual void AddNameOrAttributeInitialization(NameOrAttributeInitializationNode nameOrAttributeInit)
		{
			this.nameOrAttributeInits.AddChild(nameOrAttributeInit);
		}

		protected internal override bool CheckLocal()
		{
			return InitializationIsWellTyped()
					& NoRhsConstraint()
					& NoLhsCopy()
					& NoLhsNameOrAttributeInit()
					& AtMostOneNameInit();
		}

		private bool InitializationIsWellTyped()
		{
			if(initialization == null)
				return true;

			TypeNode targetType = DeclType;
			TypeNode exprType = initialization.Type;

			if(exprType.IsEqual(targetType))
				return true;

			if(targetType is NodeTypeNode && exprType is NodeTypeNode
					|| targetType is EdgeTypeNode && exprType is EdgeTypeNode)
			{
				ICollection<TypeNode> superTypes = new HashSet<TypeNode>();
				exprType.DoGetCompatibleToTypes(superTypes);
				if(superTypes.Contains(targetType))
					return true;
			}

			ReportError("Cannot initialize " + Kind + " " + Ident + " of type " + targetType.ToStringWithDeclarationCoords()
					+ " with a value of type " + exprType.ToStringWithDeclarationCoords() + ".");
			return false;
		}

		private bool NoRhsConstraint()
		{
			if((context & CONTEXT_LHS_OR_RHS) == CONTEXT_LHS)
				return true;

			if(constraints != TypeExprNode.Empty)
			{
				constraints.ReportError("A rewrite part element is not allowed to be type constrained (only pattern elements are)"
						+ " (but the rewrite part " + Kind + " " + Ident + " is endowed with a type constraint).");
				return false;
			}

			return true;
		}

		private bool NoLhsCopy()
		{
			if((context & CONTEXT_LHS_OR_RHS) == CONTEXT_RHS)
				return true;

			if(copyKind != CopyKind.None)
			{
				ReportError("A copy<> construct is not allowed in the pattern part"
						+ EmptyWhenAnonymous(" (but comes with the declaration of " + Kind + " " + Ident + ")") + ".");
				return false;
			}

			return true;
		}

		private bool NoLhsNameOrAttributeInit()
		{
			if((context & CONTEXT_LHS_OR_RHS) == CONTEXT_RHS)
				return true;

			if(nameOrAttributeInits.Size() > 0)
			{
				NameOrAttributeInitializationNode nameOrAttributeInit = nameOrAttributeInits.Get(0);
				if(nameOrAttributeInit.attributeUnresolved != null)
				{
					ReportError("An attribute initialization is not allowed in the pattern part (but occurs for " + nameOrAttributeInit.attributeUnresolved
							+ " of " + Kind + " " + Ident + ").");
				}
				else
					ReportError("A name initialization ($=) is not allowed in the pattern part (but occurs for " + Kind + " " + Ident + ").");
				return false;
			}

			return true;
		}

		private bool AtMostOneNameInit()
		{
			bool atMostOneNameInit = true;

			bool nameInitFound = false;
			foreach(NameOrAttributeInitializationNode nain in nameOrAttributeInits.ChildrenExact)
			{
				if(nain.attributeUnresolved == null)
				{
					if(!nameInitFound)
						nameInitFound = true;
					else
					{
						ReportError("Only one name initialization ($=) is allowed (but multiple ones are given for " + Kind + " " + Ident + ").");
						atMostOneNameInit = false;
					}
				}
			}

			return atMostOneNameInit;
		}

		protected internal TypeExpr IRConstraints
		{
			get
			{
				return constraints.CheckIR(typeof(TypeExpr));
			}
		}

		/// <summary>
		/// @returns True, if this element has eventually been deleted due to homomorphy </summary>
		protected internal virtual bool IsMaybeDeleted()
		{
			return maybeDeleted;
		}

		/// <summary>
		/// @returns True, if this element has eventually been retyped due to homomorphy </summary>
		protected internal virtual bool IsMaybeRetyped()
		{
			return maybeRetyped;
		}

		/// <summary>
		/// @returns the retyped version of this element or null. </summary>
		public virtual ConstraintDeclNode RetypedElement
		{
			get
			{
				return retypedElem;
			}
		}

		public abstract InheritanceTypeNode DeclInhType {get;}

		public static string KindStr
		{
			get
			{
				return "node or edge";
			}
		}
	}

}
