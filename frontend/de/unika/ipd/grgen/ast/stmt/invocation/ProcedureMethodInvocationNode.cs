/*
 * GrGen: graph rewrite generator tool -- release GrGen.NET 8.1
 * Copyright (C) 2003-2026 Universitaet Karlsruhe, Institut fuer Programmstrukturen und Datenorganisation, LS Goos; and free programmers
 * licensed under LGPL v3, some components/parts use different licenses (see LICENSE.txt included in the packaging of this file)
 * www.grgen.de / www.grgen.net
 */

/// <summary>
/// @author Edgar Jakumeit
/// </summary>

namespace de.unika.ipd.grgen.ast.stmt.invocation
{

	using System.Collections.Generic;
	using System.Diagnostics;

	using de.unika.ipd.grgen.ast;
	using DeclNode = de.unika.ipd.grgen.ast.decl.DeclNode;
	using ProcedureDeclNode = de.unika.ipd.grgen.ast.decl.executable.ProcedureDeclNode;
	using EdgeDeclNode = de.unika.ipd.grgen.ast.decl.pattern.EdgeDeclNode;
	using NodeDeclNode = de.unika.ipd.grgen.ast.decl.pattern.NodeDeclNode;
	using VarDeclNode = de.unika.ipd.grgen.ast.decl.pattern.VarDeclNode;
	using ExprNode = de.unika.ipd.grgen.ast.expr.ExprNode;
	using EvalStatementNode = de.unika.ipd.grgen.ast.stmt.EvalStatementNode;
	using TypeNode = de.unika.ipd.grgen.ast.type.TypeNode;
	using de.unika.ipd.grgen.ast.util;
	using Entity = de.unika.ipd.grgen.ir.Entity;
	using IR = de.unika.ipd.grgen.ir.IR;
	using Procedure = de.unika.ipd.grgen.ir.executable.Procedure;
	using Expression = de.unika.ipd.grgen.ir.expr.Expression;
	using ProcedureMethodInvocation = de.unika.ipd.grgen.ir.stmt.invocation.ProcedureMethodInvocation;
	using Type = de.unika.ipd.grgen.ir.type.Type;

	/// <summary>
	/// Invocation of a procedure method
	/// </summary>
	public class ProcedureMethodInvocationNode : ProcedureInvocationBaseNode
	{
		static ProcedureMethodInvocationNode()
		{
			SetClassName(typeof(ProcedureMethodInvocationNode), "procedure method invocation");
		}

		private IdentNode ownerUnresolved;
		private DeclNode owner;

		private IdentNode procedureUnresolved;
		private ProcedureDeclNode procedureDecl;

		public ProcedureMethodInvocationNode(IdentNode owner, IdentNode procedureOrExternalProcedureUnresolved,
				CollectNode<ExprNode> arguments, int context)
			: base(procedureOrExternalProcedureUnresolved.Coords, arguments, context)
		{
			this.ownerUnresolved = BecomeParent(owner);
			this.procedureUnresolved = BecomeParent(procedureOrExternalProcedureUnresolved);
		}

		public override ICollection<BaseNode> Children
		{
			get
			{
				IList<BaseNode> children = new List<BaseNode>();
				children.Add(GetValidVersion(ownerUnresolved, owner));
				children.Add(GetValidVersion(procedureUnresolved, procedureDecl));
				children.Add(arguments);
				return children;
			}
		}

		public override ICollection<string> ChildrenNames
		{
			get
			{
				IList<string> childrenNames = new List<string>();
				childrenNames.Add("owner");
				childrenNames.Add("procedure");
				childrenNames.Add("arguments");
				return childrenNames;
			}
		}

		private static readonly DeclarationResolver<DeclNode> ownerResolver =
				new DeclarationResolver<DeclNode>(typeof(DeclNode));
		private static readonly DeclarationResolver<ProcedureDeclNode> resolver =
				new DeclarationResolver<ProcedureDeclNode>(typeof(ProcedureDeclNode));

		protected internal override bool ResolveLocal()
		{
			/* 1) resolve left hand side identifier, yielding a declaration of a type owning a scope
			 * 2) the scope owned by the lhs allows the ident node of the right hand side to fix/find its definition therein
			 * 3) resolve now complete/correct right hand side identifier into its declaration */
			bool res = FixupDefinition(ownerUnresolved, ownerUnresolved.Scope);
			if(!res)
				return false;

			bool successfullyResolved = true;
			owner = ownerResolver.Resolve(ownerUnresolved, this);
			successfullyResolved = owner != null && successfullyResolved;
			bool ownerResolveResult = owner != null && owner.Resolve();

			if(!ownerResolveResult)
			{
				// member can not be resolved due to inaccessible owner
				return false;
			}

			if(ownerResolveResult && owner != null
					&& (owner is NodeDeclNode || owner is EdgeDeclNode || owner is VarDeclNode))
			{
				TypeNode ownerType = owner.DeclType;
				if(ownerType is ScopeOwner)
				{
					ScopeOwner o = (ScopeOwner)ownerType;
					res = o.FixupDefinition(procedureUnresolved);

					procedureDecl = resolver.Resolve(procedureUnresolved, this);
					if(procedureDecl == null)
					{
						procedureUnresolved.ReportError("Unknown procedure method called."
								+ " (Maybe a misspelled procedure name? Or is a function call intended?"
								+ " An assignment target within parenthesis denotes a procedure call, as in "
								+ "(var) = " + owner + "." + procedureUnresolved + "(...)).");
						return false;
					}

					successfullyResolved = procedureDecl != null && successfullyResolved;
				}
				else
				{
					ReportError("Left hand side of '.' does not own a scope"
							+ " (type " + ownerType.ToStringWithDeclarationCoords() + ").");
					successfullyResolved = false;
				}
			}
			else
			{
				ReportError("Left hand side of '.' is neither a node nor an edge nor a variable"
						+ (owner != null && owner.DeclType != null ? " (type " + owner.DeclType.ToStringWithDeclarationCoords() + ")." : "."));
				successfullyResolved = false;
			}

			return successfullyResolved;
		}

		protected internal override bool CheckLocal()
		{
			if((context & BaseNode.CONTEXT_FUNCTION_OR_PROCEDURE) == BaseNode.CONTEXT_FUNCTION)
			{
				ReportError("Procedure method call not allowed in function or pattern part context (attempted on " + procedureUnresolved + ").");
				return false;
			}
			return CheckSignatureAdhered(procedureDecl, procedureUnresolved, true);
		}

		public override bool CheckStatementLocal(bool isLHS, DeclNode root, EvalStatementNode enclosingLoop)
		{
			return true;
		}

		public override IList<TypeNode> Type
		{
			get
			{
				Debug.Assert(IsResolved());
				return procedureDecl.ResultTypes;
			}
		}

		public virtual int NumReturnTypes
		{
			get
			{
				return procedureDecl.resultTypesCollectNode.Size();
			}
		}

		protected internal override IR ConstructIR()
		{
			ProcedureMethodInvocation pmi = new ProcedureMethodInvocation(owner.CheckIR(typeof(Entity)),
					procedureDecl.CheckIR(typeof(Procedure)));
			foreach(ExprNode argument in arguments.ChildrenExact)
			{
				ExprNode argumentEvaluated = argument.Evaluate();
				pmi.AddArgument(argumentEvaluated.CheckIR(typeof(Expression)));
			}
			foreach(TypeNode type in procedureDecl.resultTypesCollectNode.ChildrenExact)
				pmi.AddReturnType(type.CheckIR(typeof(Type)));
			return pmi;
		}
	}

}
