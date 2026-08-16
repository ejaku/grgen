/*
 * GrGen: graph rewrite generator tool -- release GrGen.NET 8.1
 * Copyright (C) 2003-2026 Universitaet Karlsruhe, Institut fuer Programmstrukturen und Datenorganisation, LS Goos; and free programmers
 * licensed under LGPL v3, some components/parts use different licenses (see LICENSE.txt included in the packaging of this file)
 * www.grgen.de / www.grgen.net
 */

namespace de.unika.ipd.grgen.ast.expr
{

	using System.Collections.Generic;

	using de.unika.ipd.grgen.ast;
	using ProcedureInvocationDecisionNode = de.unika.ipd.grgen.ast.stmt.invocation.ProcedureInvocationDecisionNode;
	using ProcedureOrBuiltinProcedureInvocationBaseNode = de.unika.ipd.grgen.ast.stmt.invocation.ProcedureOrBuiltinProcedureInvocationBaseNode;
	using TypeNode = de.unika.ipd.grgen.ast.type.TypeNode;
	using BasicTypeNode = de.unika.ipd.grgen.ast.type.basic.BasicTypeNode;
	using IR = de.unika.ipd.grgen.ir.IR;
	using ProjectionExpr = de.unika.ipd.grgen.ir.expr.ProjectionExpr;
	using ProcedureInvocationBase = de.unika.ipd.grgen.ir.stmt.invocation.ProcedureInvocationBase;
	using Coords = de.unika.ipd.grgen.parser.Coords;

	public class ProjectionExprNode : ExprNode
	{
		static ProjectionExprNode()
		{
			SetClassName(typeof(ProjectionExprNode), "projection expr");
		}

		private int index;
		private ProcedureOrBuiltinProcedureInvocationBaseNode procedure;

		public ProjectionExprNode(Coords coords, int index)
			: base(coords)
		{

			this.index = index;
		}

		public virtual ProcedureOrBuiltinProcedureInvocationBaseNode Procedure
		{
			set
			{
				this.procedure = value;
				BecomeParent(value);
			}
		}

		public override ICollection<BaseNode> Children
		{
			get
			{
				IList<BaseNode> children = new List<BaseNode>();
				return children;
			}
		}

		public override ICollection<string> ChildrenNames
		{
			get
			{
				IList<string> childrenNames = new List<string>();
				return childrenNames;
			}
		}

		protected internal override bool CheckLocal()
		{
			return true;
		}

		protected internal override IR ConstructIR()
		{
			return new ProjectionExpr(index,
					procedure is ProcedureInvocationDecisionNode ? null
							: procedure.CheckIR<ProcedureInvocationBase>(typeof(ProcedureInvocationBase)).ProcedureBase,
					procedure.Type[index].IRType);
		}

		public override TypeNode Type
		{
			get
			{
				if(index >= procedure.Type.Count)
					return BasicTypeNode.GetErrorType(IdentNode.Invalid);

				return procedure.Type[index];
			}
		}
	}

}
