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
	using VarDeclNode = de.unika.ipd.grgen.ast.decl.pattern.VarDeclNode;
	using ExprNode = de.unika.ipd.grgen.ast.expr.ExprNode;
	using QualIdentNode = de.unika.ipd.grgen.ast.expr.QualIdentNode;
	using ExternalObjectTypeNode = de.unika.ipd.grgen.ast.model.type.ExternalObjectTypeNode;
	using EvalStatementNode = de.unika.ipd.grgen.ast.stmt.EvalStatementNode;
	using TypeNode = de.unika.ipd.grgen.ast.type.TypeNode;
	using de.unika.ipd.grgen.ast.util;
	using IR = de.unika.ipd.grgen.ir.IR;
	using ExternalProcedure = de.unika.ipd.grgen.ir.executable.ExternalProcedure;
	using Expression = de.unika.ipd.grgen.ir.expr.Expression;
	using Qualification = de.unika.ipd.grgen.ir.expr.Qualification;
	using Variable = de.unika.ipd.grgen.ir.pattern.Variable;
	using ExternalProcedureMethodInvocation = de.unika.ipd.grgen.ir.stmt.invocation.ExternalProcedureMethodInvocation;
	using Type = de.unika.ipd.grgen.ir.type.Type;

	/// <summary>
	/// Invocation of an external procedure method
	/// </summary>
	public class ExternalProcedureMethodInvocationNode : ProcedureInvocationBaseNode
	{
		static ExternalProcedureMethodInvocationNode()
		{
			SetClassName(typeof(ExternalProcedureMethodInvocationNode), "external procedure method invocation");
		}

		internal VarDeclNode targetVar = null;
		internal QualIdentNode targetQual = null;

		internal IdentNode externalProcedureUnresolved;
		internal ExternalProcedureDeclNode externalProcedureDecl;

		public ExternalProcedureMethodInvocationNode(VarDeclNode targetVar,
				IdentNode procedureOrExternalProcedureUnresolved, CollectNode<ExprNode> arguments, int context)
			: base(procedureOrExternalProcedureUnresolved.Coords, arguments, context)
		{
			this.targetVar = BecomeParent(targetVar);
			this.externalProcedureUnresolved = BecomeParent(procedureOrExternalProcedureUnresolved);
		}

		public ExternalProcedureMethodInvocationNode(QualIdentNode targetQual,
				IdentNode procedureOrExternalProcedureUnresolved, CollectNode<ExprNode> arguments, int context)
			: base(procedureOrExternalProcedureUnresolved.Coords, arguments, context)
		{
			this.targetQual = BecomeParent(targetQual);
			this.externalProcedureUnresolved = BecomeParent(procedureOrExternalProcedureUnresolved);
		}

		public override ICollection<BaseNode> Children
		{
			get
			{
				IList<BaseNode> children = new List<BaseNode>();
				children.Add(ValidTarget);
				children.Add(GetValidVersion(externalProcedureUnresolved, externalProcedureDecl));
				children.Add(arguments);
				return children;
			}
		}

		public override ICollection<string> ChildrenNames
		{
			get
			{
				IList<string> childrenNames = new List<string>();
				childrenNames.Add("target");
				childrenNames.Add("external procedure");
				childrenNames.Add("arguments");
				return childrenNames;
			}
		}

		protected internal virtual BaseNode ValidTarget
		{
			get
			{
				return targetQual != null ? (BaseNode)targetQual : (BaseNode)targetVar;
			}
		}

		private static readonly DeclarationResolver<ExternalProcedureDeclNode> resolver =
				new DeclarationResolver<ExternalProcedureDeclNode>(typeof(ExternalProcedureDeclNode));

		protected internal override bool ResolveLocal()
		{
			bool successfullyResolved = true;
			TypeNode ownerType = targetVar != null ? targetVar.DeclType : targetQual.Decl.DeclType;
			if(ownerType is ExternalObjectTypeNode)
			{
				if(ownerType is ScopeOwner)
				{
					ScopeOwner o = (ScopeOwner)ownerType;
					o.FixupDefinition(externalProcedureUnresolved);

					externalProcedureDecl = resolver.Resolve(externalProcedureUnresolved, this);
					if(externalProcedureDecl == null)
					{
						externalProcedureUnresolved.ReportError("Unknown external procedure method called."
								+ " (Maybe a misspelled procedure name? Or is a function call intended?"
								+ " An assignment target within parenthesis denotes a procedure call, as in "
								+ "(var) = " + ValidTarget + "." + externalProcedureUnresolved + "(...)).");
						return false;
					}

					successfullyResolved = externalProcedureDecl != null && successfullyResolved;
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
				ReportError("Left hand side of '.' is not an external type"
						+ " (type " + ownerType.ToStringWithDeclarationCoords() + ").");
				successfullyResolved = false;
			}

			return successfullyResolved;
		}

		protected internal override bool CheckLocal()
		{
			if((context & BaseNode.CONTEXT_FUNCTION_OR_PROCEDURE) == BaseNode.CONTEXT_FUNCTION)
			{
				ReportError("External procedure method call not allowed in function or pattern part context (attempted on " + externalProcedureUnresolved + ").");
				return false;
			}
			return CheckSignatureAdhered(externalProcedureDecl, externalProcedureUnresolved, true);
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
				return externalProcedureDecl.ResultTypes;
			}
		}

		public virtual int NumReturnTypes
		{
			get
			{
				return externalProcedureDecl.resultTypesCollectNode.Size();
			}
		}

		protected internal override IR ConstructIR()
		{
			ExternalProcedureMethodInvocation epi;
			if(targetQual != null)
			{
				epi = new ExternalProcedureMethodInvocation(targetQual.CheckIR(typeof(Qualification)),
						externalProcedureDecl.CheckIR(typeof(ExternalProcedure)));
			}
			else
			{
				epi = new ExternalProcedureMethodInvocation(targetVar.CheckIR(typeof(Variable)),
						externalProcedureDecl.CheckIR(typeof(ExternalProcedure)));
			}
			foreach(ExprNode argument in arguments.ChildrenExact)
			{
				ExprNode argumentEvaluated = argument.Evaluate();
				epi.AddArgument(argumentEvaluated.CheckIR(typeof(Expression)));
			}
			foreach(TypeNode type in externalProcedureDecl.resultTypesCollectNode.ChildrenExact)
				epi.AddReturnType(type.CheckIR(typeof(Type)));
			return epi;
		}
	}

}
