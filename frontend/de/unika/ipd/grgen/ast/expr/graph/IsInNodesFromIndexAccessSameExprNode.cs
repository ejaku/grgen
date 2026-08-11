/*
 * GrGen: graph rewrite generator tool -- release GrGen.NET 8.1
 * Copyright (C) 2003-2026 Universitaet Karlsruhe, Institut fuer Programmstrukturen und Datenorganisation, LS Goos; and free programmers
 * licensed under LGPL v3, some components/parts use different licenses (see LICENSE.txt included in the packaging of this file)
 * www.grgen.de / www.grgen.net
 */

namespace de.unika.ipd.grgen.ast.expr.graph
{

	using System.Collections.Generic;

	using de.unika.ipd.grgen.ast;
	using ExprNode = de.unika.ipd.grgen.ast.expr.ExprNode;
	using TypeNode = de.unika.ipd.grgen.ast.type.TypeNode;
	using BasicTypeNode = de.unika.ipd.grgen.ast.type.basic.BasicTypeNode;
	using IR = de.unika.ipd.grgen.ir.IR;
	using Expression = de.unika.ipd.grgen.ir.expr.Expression;
	using IsInNodesFromIndexAccessSameExpr = de.unika.ipd.grgen.ir.expr.graph.IsInNodesFromIndexAccessSameExpr;
	using Index = de.unika.ipd.grgen.ir.model.Index;
	using IndexAccessEquality = de.unika.ipd.grgen.ir.pattern.IndexAccessEquality;
	using Coords = de.unika.ipd.grgen.parser.Coords;

	/// <summary>
	/// A node yielding whether the given node is in the nodes from an index by accessing using a comparison for equality.
	/// </summary>
	public class IsInNodesFromIndexAccessSameExprNode : FromIndexAccessSameExprNode
	{
		static IsInNodesFromIndexAccessSameExprNode()
		{
			SetClassName(typeof(IsInNodesFromIndexAccessSameExprNode), "is in nodes from index access same expr");
		}

		private ExprNode candidateExpr;

		public IsInNodesFromIndexAccessSameExprNode(Coords coords, ExprNode candidateExpr, BaseNode index, ExprNode expr)
			: base(coords, index, expr)
		{
			this.candidateExpr = candidateExpr;
			BecomeParent(this.candidateExpr);
		}

		/// <summary>
		/// returns children of this node </summary>
		public override ICollection<BaseNode> Children
		{
			get
			{
				IList<BaseNode> children = new List<BaseNode>();
				children.Add(candidateExpr);
				children.Add(GetValidVersion(indexUnresolved, index));
				children.Add(expr);
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
				childrenNames.Add("candidateExpr");
				childrenNames.Add("index");
				childrenNames.Add("expr");
				return childrenNames;
			}
		}

		/// <seealso cref="de.unika.ipd.grgen.ast.BaseNode.resolveLocal() "/>
		protected internal override bool ResolveLocal()
		{
			bool successfullyResolved = base.ResolveLocal();
			successfullyResolved &= candidateExpr.Resolve();
			successfullyResolved &= Type.Resolve();
			return successfullyResolved;
		}

		/// <seealso cref="de.unika.ipd.grgen.ast.BaseNode.checkLocal() "/>
		protected internal override bool CheckLocal()
		{
			bool res = base.CheckLocal();
			TypeNode indexedEntityRootType = Root.Decl.DeclType;
			TypeNode candidateType = candidateExpr.Type;
			if(!candidateType.IsCompatibleTo(indexedEntityRootType))
			{
				ReportError("The function " + ShortSignature() + " expects as 1. argument (candidateExpr) a value of type " + indexedEntityRootType
						+ " (but is given a value of type " + candidateType.ToStringWithDeclarationCoords() + ").");
				return false;
			}
			return res;
		}

		protected internal override int IndexShift()
		{
			return 1;
		}

		protected internal override IdentNode Root
		{
			get
			{
				return NodeRoot;
			}
		}

		protected internal override string ShortSignature()
		{
			return "isInNodesFromIndexSame(.,.,.)";
		}

		public override TypeNode Type
		{
			get
			{
				return BasicTypeNode.booleanType;
			}
		}

		protected internal override IR ConstructIR()
		{
			candidateExpr = candidateExpr.Evaluate();
			expr = expr.Evaluate();
			return new IsInNodesFromIndexAccessSameExpr(candidateExpr.CheckIR<Expression>(typeof(Expression)),
					new IndexAccessEquality(index.CheckIR<Index>(typeof(Index)), expr.CheckIR<Expression>(typeof(Expression))),
					Type.IRType);
		}
	}

}
