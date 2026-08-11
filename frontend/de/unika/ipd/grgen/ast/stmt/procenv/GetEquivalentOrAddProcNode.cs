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
	using GraphTypeNode = de.unika.ipd.grgen.ast.type.basic.GraphTypeNode;
	using ArrayTypeNode = de.unika.ipd.grgen.ast.type.container.ArrayTypeNode;
	using IR = de.unika.ipd.grgen.ir.IR;
	using Expression = de.unika.ipd.grgen.ir.expr.Expression;
	using GetEquivalentOrAddProc = de.unika.ipd.grgen.ir.stmt.procenv.GetEquivalentOrAddProc;
	using Coords = de.unika.ipd.grgen.parser.Coords;

	public class GetEquivalentOrAddProcNode : BuiltinProcedureInvocationBaseNode
	{
		static GetEquivalentOrAddProcNode()
		{
			SetClassName(typeof(GetEquivalentOrAddProcNode), "get equivalent or add procedure");
		}

		private ExprNode subgraphExpr;
		private ExprNode subgraphArrayExpr;
		private bool includingAttributes;

		internal IList<TypeNode> returnTypes;

		public GetEquivalentOrAddProcNode(Coords coords, ExprNode subgraphExpr,
				ExprNode subgraphArrayExpr, bool includingAttributes)
			: base(coords)
		{
			this.subgraphExpr = subgraphExpr;
			BecomeParent(this.subgraphExpr);
			this.subgraphArrayExpr = subgraphArrayExpr;
			BecomeParent(this.subgraphArrayExpr);
			this.includingAttributes = includingAttributes;
		}

		public override ICollection<BaseNode> Children
		{
			get
			{
				IList<BaseNode> children = new List<BaseNode>();
				children.Add(subgraphExpr);
				children.Add(subgraphArrayExpr);
				return children;
			}
		}

		public override ICollection<string> ChildrenNames
		{
			get
			{
				IList<string> childrenNames = new List<string>();
				childrenNames.Add("subgraphExpr");
				childrenNames.Add("subgraphArrayExpr");
				return childrenNames;
			}
		}

		protected internal override bool CheckLocal()
		{
			TypeNode subgraphExprType = subgraphExpr.Type;
			if(!(subgraphExprType is GraphTypeNode))
			{
				subgraphExpr.ReportError("The " + Name() + " procedure expects as 1. argument (subgraph)"
						+ " a value of type graph"
						+ " (but is given a value of type " + subgraphExprType.ToStringWithDeclarationCoords() + ").");
				return false;
			}
			TypeNode subgraphArrayExprType = subgraphArrayExpr.Type;
			if(!(subgraphArrayExprType is ArrayTypeNode))
			{
				subgraphArrayExpr.ReportError("The " + Name() + " procedure expects as 2. argument"
						+ " a value of type array<graph>"
						+ " (but is given a value of type " + subgraphArrayExprType.ToStringWithDeclarationCoords() + ").");
				return false;
			}
			TypeNode subgraphArrayExprValueType = ((ArrayTypeNode)subgraphArrayExprType).valueType;
			if(!(subgraphArrayExprValueType is GraphTypeNode))
			{
				subgraphArrayExpr.ReportError("The " + Name() + " procedure expects as 2. argument"
						+ " a value of type array<graph>"
						+ " (but is given a value of type " + subgraphArrayExprType.ToStringWithDeclarationCoords() + ").");
				return false;
			}
			return true;
		}

		public virtual string Name()
		{
			return includingAttributes ? "getEquivalentOrAdd" : "getEquivalentStructurallyOrAdd";
		}

		public override bool CheckStatementLocal(bool isLHS, DeclNode root, EvalStatementNode enclosingLoop)
		{
			return true;
		}

		protected internal override IR ConstructIR()
		{
			subgraphExpr = subgraphExpr.Evaluate();
			subgraphArrayExpr = subgraphArrayExpr.Evaluate();
			GetEquivalentOrAddProc getEquivalentOrAdd = new GetEquivalentOrAddProc(BasicTypeNode.graphType.GetIRType(),
					subgraphExpr.CheckIR<Expression>(typeof(Expression)),
					subgraphArrayExpr.CheckIR<Expression>(typeof(Expression)),
					includingAttributes);
			return getEquivalentOrAdd;
		}

		public override IList<TypeNode> Type
		{
			get
			{
				if(returnTypes == null)
				{
					returnTypes = new List<TypeNode>();
					returnTypes.Add(BasicTypeNode.graphType);
				}
				return returnTypes;
			}
		}
	}

}
