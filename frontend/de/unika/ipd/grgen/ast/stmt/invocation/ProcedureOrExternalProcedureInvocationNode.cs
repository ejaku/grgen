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
	using ExternalProcedureDeclNode = de.unika.ipd.grgen.ast.decl.executable.ExternalProcedureDeclNode;
	using ProcedureDeclBaseNode = de.unika.ipd.grgen.ast.decl.executable.ProcedureDeclBaseNode;
	using ProcedureDeclNode = de.unika.ipd.grgen.ast.decl.executable.ProcedureDeclNode;
	using ExprNode = de.unika.ipd.grgen.ast.expr.ExprNode;
	using EvalStatementNode = de.unika.ipd.grgen.ast.stmt.EvalStatementNode;
	using TypeNode = de.unika.ipd.grgen.ast.type.TypeNode;
	using de.unika.ipd.grgen.ast.util;
	using de.unika.ipd.grgen.ast.util;
	using Type = de.unika.ipd.grgen.ir.type.Type;
	using IR = de.unika.ipd.grgen.ir.IR;
	using ExternalProcedure = de.unika.ipd.grgen.ir.executable.ExternalProcedure;
	using Procedure = de.unika.ipd.grgen.ir.executable.Procedure;
	using Expression = de.unika.ipd.grgen.ir.expr.Expression;
	using ExternalProcedureInvocation = de.unika.ipd.grgen.ir.stmt.invocation.ExternalProcedureInvocation;
	using ProcedureInvocation = de.unika.ipd.grgen.ir.stmt.invocation.ProcedureInvocation;

	/// <summary>
	/// Invocation of a procedure or an external procedure
	/// </summary>
	public class ProcedureOrExternalProcedureInvocationNode : ProcedureInvocationBaseNode
	{
		static ProcedureOrExternalProcedureInvocationNode()
		{
			SetClassName(typeof(ProcedureOrExternalProcedureInvocationNode), "procedure or external procedure invocation");
		}

		private IdentNode procedureOrExternalProcedureUnresolved;
		private ExternalProcedureDeclNode externalProcedureDecl;
		private ProcedureDeclNode procedureDecl;

		public ProcedureOrExternalProcedureInvocationNode(IdentNode procedureOrExternalProcedureUnresolved,
				CollectNode<ExprNode> arguments, int context)
			: base(procedureOrExternalProcedureUnresolved.Coords, arguments, context)
		{
			this.procedureOrExternalProcedureUnresolved = BecomeParent(procedureOrExternalProcedureUnresolved);
		}

		public override ICollection<BaseNode> Children
		{
			get
			{
				IList<BaseNode> children = new List<BaseNode>();
				children.Add(GetValidVersion(procedureOrExternalProcedureUnresolved, procedureDecl, externalProcedureDecl));
				children.Add(arguments);
				return children;
			}
		}

		public override ICollection<string> ChildrenNames
		{
			get
			{
				IList<string> childrenNames = new List<string>();
				childrenNames.Add("procedure or external procedure");
				childrenNames.Add("arguments");
				return childrenNames;
			}
		}

		private static readonly DeclarationPairResolver<ProcedureDeclNode, ExternalProcedureDeclNode> resolver =
				new DeclarationPairResolver<ProcedureDeclNode, ExternalProcedureDeclNode>(typeof(ProcedureDeclNode), typeof(ExternalProcedureDeclNode));

		protected internal override bool ResolveLocal()
		{
			if(!(procedureOrExternalProcedureUnresolved is PackageIdentNode))
				FixupDefinition(procedureOrExternalProcedureUnresolved, procedureOrExternalProcedureUnresolved.Scope);
			Pair<ProcedureDeclNode, ExternalProcedureDeclNode> resolved = resolver.Resolve(procedureOrExternalProcedureUnresolved, this);
			if(resolved == null)
			{
				procedureOrExternalProcedureUnresolved.ReportError("Unknown procedure called."
						+ " (Maybe a misspelled procedure name? Or is a function call intended?"
						+ " An assignment target within parenthesis denotes a procedure call, as in "
						+ "(var) = " + procedureOrExternalProcedureUnresolved + "(...)).");
				return false;
			}
			procedureDecl = resolved.fst;
			externalProcedureDecl = resolved.snd;
			return true;
		}

		protected internal override bool CheckLocal()
		{
			if((context & BaseNode.CONTEXT_FUNCTION_OR_PROCEDURE) == BaseNode.CONTEXT_FUNCTION)
			{
				ReportError("Procedure call not allowed in function or pattern part context (attempted on " + procedureOrExternalProcedureUnresolved + ").");
				return false;
			}
			return CheckSignatureAdhered();
		}

		public override bool CheckStatementLocal(bool isLHS, DeclNode root, EvalStatementNode enclosingLoop)
		{
			return true;
		}

		/// <summary>
		/// Check whether the usage adheres to the signature of the declaration </summary>
		private bool CheckSignatureAdhered()
		{
			ProcedureDeclBaseNode pb = procedureDecl != null ? (ProcedureDeclBaseNode)procedureDecl : externalProcedureDecl;
			return CheckSignatureAdhered(pb, procedureOrExternalProcedureUnresolved, false);
		}

		public override IList<TypeNode> Type
		{
			get
			{
				Debug.Assert(IsResolved());
				return procedureDecl != null ? procedureDecl.ResultTypes : externalProcedureDecl.ResultTypes;
			}
		}

		public virtual int NumReturnTypes
		{
			get
			{
				if(procedureDecl != null)
					return procedureDecl.resultTypesCollectNode.Size();
				else
					return externalProcedureDecl.resultTypesCollectNode.Size();
			}
		}

		public virtual IdentNode Ident
		{
			get
			{
				return procedureOrExternalProcedureUnresolved;
			}
		}

		protected internal override IR ConstructIR()
		{
			if(procedureDecl != null)
			{
				ProcedureInvocation pi = new ProcedureInvocation(procedureDecl.CheckIR<Procedure>(typeof(Procedure)));
				foreach(ExprNode argument in arguments.ChildrenExact)
				{
					ExprNode argumentEvaluated = argument.Evaluate();
					pi.AddArgument(argumentEvaluated.CheckIR<Expression>(typeof(Expression)));
				}
				foreach(TypeNode type in procedureDecl.resultTypesCollectNode.ChildrenExact)
					pi.AddReturnType(type.CheckIR<Type>(typeof(Type)));
				return pi;
			}
			else
			{
				ExternalProcedureInvocation epi = new ExternalProcedureInvocation(
						externalProcedureDecl.CheckIR<ExternalProcedure>(typeof(ExternalProcedure)));
				foreach(ExprNode argument in arguments.ChildrenExact)
				{
					ExprNode argumentEvaluated = argument.Evaluate();
					epi.AddArgument(argumentEvaluated.CheckIR<Expression>(typeof(Expression)));
				}
				foreach(TypeNode type in externalProcedureDecl.resultTypesCollectNode.ChildrenExact)
					epi.AddReturnType(type.CheckIR<Type>(typeof(Type)));
				return epi;
			}
		}
	}

}
