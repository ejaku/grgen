/*
 * GrGen: graph rewrite generator tool -- release GrGen.NET 8.1
 * Copyright (C) 2003-2026 Universitaet Karlsruhe, Institut fuer Programmstrukturen und Datenorganisation, LS Goos; and free programmers
 * licensed under LGPL v3, some components/parts use different licenses (see LICENSE.txt included in the packaging of this file)
 * www.grgen.de / www.grgen.net
 */

/// <summary>
/// @author Edgar Jakumeit
/// </summary>

namespace de.unika.ipd.grgen.ast.stmt.procenv
{

	using System.Collections.Generic;

	using de.unika.ipd.grgen.ast;
	using DeclNode = de.unika.ipd.grgen.ast.decl.DeclNode;
	using ExprNode = de.unika.ipd.grgen.ast.expr.ExprNode;
	using BuiltinProcedureInvocationBaseNode = de.unika.ipd.grgen.ast.stmt.BuiltinProcedureInvocationBaseNode;
	using EvalStatementNode = de.unika.ipd.grgen.ast.stmt.EvalStatementNode;
	using TypeNode = de.unika.ipd.grgen.ast.type.TypeNode;
	using BasicTypeNode = de.unika.ipd.grgen.ast.type.basic.BasicTypeNode;
	using IR = de.unika.ipd.grgen.ir.IR;
	using Expression = de.unika.ipd.grgen.ir.expr.Expression;
	using SynchronizationTryEnterProc = de.unika.ipd.grgen.ir.stmt.procenv.SynchronizationTryEnterProc;
	using Coords = de.unika.ipd.grgen.parser.Coords;

	public class SynchronizationTryEnterProcNode : BuiltinProcedureInvocationBaseNode
	{
		static SynchronizationTryEnterProcNode()
		{
			SetClassName(typeof(SynchronizationTryEnterProcNode), "synchronization try enter procedure");
		}

		private ExprNode criticalSectionObjectExpr;

		internal IList<TypeNode> returnTypes;

		public SynchronizationTryEnterProcNode(Coords coords, ExprNode criticalSectionObjectExpr)
			: base(coords)
		{

			this.criticalSectionObjectExpr = BecomeParent(criticalSectionObjectExpr);
		}

		public override ICollection<BaseNode> Children
		{
			get
			{
				IList<BaseNode> children = new List<BaseNode>();
				children.Add(criticalSectionObjectExpr);
				return children;
			}
		}

		public override ICollection<string> ChildrenNames
		{
			get
			{
				IList<string> childrenNames = new List<string>();
				childrenNames.Add("criticalSectionObjectExpr");
				return childrenNames;
			}
		}

		protected internal override bool CheckLocal()
		{
			TypeNode criticalSectionObjectExprType = criticalSectionObjectExpr.Type;
			if(!criticalSectionObjectExprType.IsLockableType())
			{
				criticalSectionObjectExpr.ReportError("The Synchronization::tryenter procedure expects as argument (criticalSectionObject)"
						+ " a value that is not of basic type (with exception of type object)"
						+ " (but is given a value of type " + criticalSectionObjectExprType.ToStringWithDeclarationCoords() + ").");
				return false;
			}
			return true;
		}

		public override bool CheckStatementLocal(bool isLHS, DeclNode root, EvalStatementNode enclosingLoop)
		{
			return true;
		}

		protected internal override IR ConstructIR()
		{
			criticalSectionObjectExpr = criticalSectionObjectExpr.Evaluate();
			SynchronizationTryEnterProc tryEnter = new SynchronizationTryEnterProc(BasicTypeNode.booleanType.IRType, criticalSectionObjectExpr.CheckIR<Expression>(typeof(Expression)));
			return tryEnter;
		}

		public override IList<TypeNode> Type
		{
			get
			{
				if(returnTypes == null)
				{
					returnTypes = new List<TypeNode>();
					returnTypes.Add(BasicTypeNode.booleanType);
				}
				return returnTypes;
			}
		}
	}

}
