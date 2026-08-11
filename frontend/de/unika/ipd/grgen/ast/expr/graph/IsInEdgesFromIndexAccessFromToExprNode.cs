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
	using IsInEdgesFromIndexAccessFromToExpr = de.unika.ipd.grgen.ir.expr.graph.IsInEdgesFromIndexAccessFromToExpr;
	using Index = de.unika.ipd.grgen.ir.model.Index;
	using IndexAccessOrdering = de.unika.ipd.grgen.ir.pattern.IndexAccessOrdering;
	using Coords = de.unika.ipd.grgen.parser.Coords;

	/// <summary>
	/// A node yielding whether the given edge is in the edges from an index by accessing a range from a certain value to a certain value (one or both may be optional).
	/// </summary>
	public class IsInEdgesFromIndexAccessFromToExprNode : FromIndexAccessFromToExprNode
	{
		static IsInEdgesFromIndexAccessFromToExprNode()
		{
			SetClassName(typeof(IsInEdgesFromIndexAccessFromToExprNode), "is in edges from index access from to expr");
		}

		private ExprNode candidateExpr;

		public IsInEdgesFromIndexAccessFromToExprNode(Coords coords, ExprNode candidateExpr, BaseNode index, ExprNode fromExpr, bool fromExclusive, ExprNode toExpr, bool toExclusive)
			: base(coords, index, fromExpr, fromExclusive, toExpr, toExclusive)
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
				if(fromExpr != null)
					children.Add(fromExpr);
				if(toExpr != null)
					children.Add(toExpr);
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
				if(fromExpr != null)
					childrenNames.Add("fromExpr");
				if(toExpr != null)
					childrenNames.Add("toExpr");
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
			TypeNode indexedEntityRootType = Root.Decl.GetDeclType();
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
				return EdgeRoot;
			}
		}

		protected internal override string ShortSignature()
		{
			return "isInEdgesFromIndex" + FromPart() + ToPart() + "(" + ".," + ArgumentsPart() + ")";
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
			if(fromExpr != null)
				fromExpr = fromExpr.Evaluate();
			if(toExpr != null)
				toExpr = toExpr.Evaluate();
			return new IsInEdgesFromIndexAccessFromToExpr(candidateExpr.CheckIR(typeof(Expression)),
					new IndexAccessOrdering(index.CheckIR(typeof(Index)), true,
							FromOperator(), fromExpr != null ? fromExpr.CheckIR(typeof(Expression)) : null,
							ToOperator(), toExpr != null ? toExpr.CheckIR(typeof(Expression)) : null),
					Type.IRType);
		}
	}

}
