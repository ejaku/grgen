/*
 * GrGen: graph rewrite generator tool -- release GrGen.NET 8.1
 * Copyright (C) 2003-2026 Universitaet Karlsruhe, Institut fuer Programmstrukturen und Datenorganisation, LS Goos; and free programmers
 * licensed under LGPL v3, some components/parts use different licenses (see LICENSE.txt included in the packaging of this file)
 * www.grgen.de / www.grgen.net
 */

/// <summary>
/// @author Sebastian Hack, Edgar Jakumeit
/// </summary>
namespace de.unika.ipd.grgen.ast.stmt
{

	using System.Collections.Generic;

	using de.unika.ipd.grgen.ast;
	using DeclNode = de.unika.ipd.grgen.ast.decl.DeclNode;
	using ProcedureInvocationBaseNode = de.unika.ipd.grgen.ast.stmt.invocation.ProcedureInvocationBaseNode;
	using ProcedureInvocationDecisionNode = de.unika.ipd.grgen.ast.stmt.invocation.ProcedureInvocationDecisionNode;
	using ProcedureMethodInvocationDecisionNode = de.unika.ipd.grgen.ast.stmt.invocation.ProcedureMethodInvocationDecisionNode;
	using ProcedureOrExternalProcedureInvocationNode = de.unika.ipd.grgen.ast.stmt.invocation.ProcedureOrExternalProcedureInvocationNode;
	using AssignmentBase = de.unika.ipd.grgen.ir.stmt.AssignmentBase;
	using ReturnAssignment = de.unika.ipd.grgen.ir.stmt.ReturnAssignment;
	using ProcedureOrBuiltinProcedureInvocationBase = de.unika.ipd.grgen.ir.stmt.invocation.ProcedureOrBuiltinProcedureInvocationBase;
	using IR = de.unika.ipd.grgen.ir.IR;
	using Coords = de.unika.ipd.grgen.parser.Coords;

	/// <summary>
	/// AST node representing an assignment of procedure invocation return values.
	/// </summary>
	public class ReturnAssignmentNode : EvalStatementNode
	{
		static ReturnAssignmentNode()
		{
			SetClassName(typeof(ReturnAssignmentNode), "Return Assign");
		}

		internal ProcedureOrExternalProcedureInvocationNode procedure;
		internal ProcedureInvocationDecisionNode builtinProcedure;
		internal ProcedureMethodInvocationDecisionNode procedureMethod;
		internal CollectNode<EvalStatementNode> targets;
		internal int context;

		public ReturnAssignmentNode(Coords coords, ProcedureOrExternalProcedureInvocationNode procedure,
				CollectNode<EvalStatementNode> targets, int context)
			: base(coords)
		{
			this.procedure = procedure;
			BecomeParent(this.procedure);
			this.targets = targets;
			BecomeParent(this.targets);
			this.context = context;
		}

		public ReturnAssignmentNode(Coords coords, ProcedureInvocationDecisionNode builtinProcedure,
				CollectNode<EvalStatementNode> targets, int context)
			: base(coords)
		{
			this.builtinProcedure = builtinProcedure;
			BecomeParent(this.builtinProcedure);
			this.targets = targets;
			BecomeParent(this.targets);
			this.context = context;
		}

		public ReturnAssignmentNode(Coords coords, ProcedureMethodInvocationDecisionNode procedureMethod,
				CollectNode<EvalStatementNode> targets, int context)
			: base(coords)
		{
			this.procedureMethod = procedureMethod;
			BecomeParent(this.procedureMethod);
			this.targets = targets;
			BecomeParent(this.targets);
			this.context = context;
		}

		/// <summary>
		/// returns children of this node </summary>
		public override ICollection<BaseNode> Children
		{
			get
			{
				IList<BaseNode> children = new List<BaseNode>();
				children.Add(ValidProcedure);
				children.Add(targets);
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
				childrenNames.Add("lhs");
				childrenNames.Add("rhs");
				return childrenNames;
			}
		}

		public virtual ProcedureInvocationBaseNode ValidProcedure
		{
			get
			{
				return procedure != null ?
						(ProcedureInvocationBaseNode)procedure :
						builtinProcedure != null ? (ProcedureInvocationBaseNode)builtinProcedure : (ProcedureInvocationBaseNode)procedureMethod;
			}
		}

		/// <seealso cref="de.unika.ipd.grgen.ast.BaseNode.resolveLocal() "/>
		protected internal override bool ResolveLocal()
		{
			return true;
		}

		/// <seealso cref="de.unika.ipd.grgen.ast.BaseNode.checkLocal() "/>
		protected internal override bool CheckLocal()
		{
			// targets is one of AssignNode, AssignVisitedNode, AssignIndexedNode
			// with QualIdentNode or IdentExprNode as owner/target
			// or a ConnectionNode or a SingleNodeConnNode or a VarDeclNode
			// and finally a projection expr node as source -- maybe with a cast prefix after type adjust
			if(procedure != null)
			{
				if(targets.Size() != procedure.NumReturnTypes && targets.Size() != 0)
				{
					procedure.ReportError("The call of procedure " + procedure.Ident
							+ " expects " + procedure.NumReturnTypes
							+ " procedure return variables, but given are " + targets.Size() + " return variables.");
					return false;
				}
			}
			else if(builtinProcedure != null)
			{
				if(targets.Size() != builtinProcedure.NumReturnTypes && targets.Size() != 0)
				{
					builtinProcedure.ReportError("The call of (builtin) procedure " + builtinProcedure.ProcedureName
							+ " expects " + builtinProcedure.NumReturnTypes
							+ " procedure return variables, but given are " + targets.Size() + " return variables.");
					return false;
				}
			}
			else
			{ //procedureMethod!=null
				if(targets.Size() != procedureMethod.NumReturnTypes && targets.Size() != 0)
				{
					procedureMethod.ReportError("The call of procedure method " + procedureMethod.Ident
							+ " expects " + procedureMethod.NumReturnTypes
							+ " procedure return variables, but given are " + targets.Size() + " return variables.");
					return false;
				}
			}
			// hint: the types are checked in the singular assignments
			return true;
		}

		public override bool CheckStatementLocal(bool isLHS, DeclNode root, EvalStatementNode enclosingLoop)
		{
			return true;
		}

		/// <summary>
		/// Construct the immediate representation from an assignment node. </summary>
		/// <seealso cref="de.unika.ipd.grgen.ast.BaseNode.constructIR()"/>
		protected internal override IR ConstructIR()
		{
			ReturnAssignment retAssign;
			retAssign = new ReturnAssignment(ValidProcedure.CheckIR(typeof(ProcedureOrBuiltinProcedureInvocationBase)));
			foreach(EvalStatementNode target in targets.ChildrenExact)
				retAssign.AddAssignment(target.CheckIR(typeof(AssignmentBase)));
			return retAssign;
		}
	}

}
