/*
 * GrGen: graph rewrite generator tool -- release GrGen.NET 8.1
 * Copyright (C) 2003-2026 Universitaet Karlsruhe, Institut fuer Programmstrukturen und Datenorganisation, LS Goos; and free programmers
 * licensed under LGPL v3, some components/parts use different licenses (see LICENSE.txt included in the packaging of this file)
 * www.grgen.de / www.grgen.net
 */

/// <summary>
/// @author Moritz Kroll
/// </summary>

namespace de.unika.ipd.grgen.ast
{

	using System.Collections.Generic;

	using DeclNode = de.unika.ipd.grgen.ast.decl.DeclNode;
	using ConstNode = de.unika.ipd.grgen.ast.expr.ConstNode;
	using ExprNode = de.unika.ipd.grgen.ast.expr.ExprNode;
	using TypeNode = de.unika.ipd.grgen.ast.type.TypeNode;
	using de.unika.ipd.grgen.ast.util;
	using ConstructorParam = de.unika.ipd.grgen.ir.ConstructorParam;
	using Entity = de.unika.ipd.grgen.ir.Entity;
	using IR = de.unika.ipd.grgen.ir.IR;
	using Expression = de.unika.ipd.grgen.ir.expr.Expression;

	/// <summary>
	/// AST node representing a parameter of a constructor.
	/// children: LHS:IdentNode, RHS:optional ExprNode
	/// </summary>
	public class ConstructorParamNode : BaseNode
	{
		static ConstructorParamNode()
		{
			SetClassName(typeof(ConstructorParamNode), "constructor parameter declaration");
		}

		private IdentNode lhsUnresolved;
		public DeclNode lhs;
		public ExprNode rhs;

		public ConstructorParamNode(IdentNode paramNode, ExprNode expr)
			: base(paramNode.Coords)
		{
			lhsUnresolved = BecomeParent(paramNode);
			rhs = BecomeParent(expr);
		}

		public override ICollection<BaseNode> Children
		{
			get
			{
				IList<BaseNode> children = new List<BaseNode>();
				children.Add(GetValidVersion(lhsUnresolved, lhs));
				if(rhs != null)
					children.Add(rhs);
				return children;
			}
		}

		public override ICollection<string> ChildrenNames
		{
			get
			{
				IList<string> childrenNames = new List<string>();
				childrenNames.Add("lhs");
				if(rhs != null)
					childrenNames.Add("rhs");
				return childrenNames;
			}
		}

		private static readonly MemberResolver<DeclNode> lhsResolver = new MemberResolver<DeclNode>();

		protected internal override bool ResolveLocal()
		{
			if(!lhsResolver.Resolve(lhsUnresolved))
				return false;
			lhs = lhsResolver.GetResult(typeof(DeclNode));

			return lhsResolver.Finish();
		}

		protected internal override bool CheckLocal()
		{
			return rhs == null || TypeCheckLocal();
		}

		/// <summary>
		/// Checks whether the expression has a type equal, compatible or castable
		/// to the type of the target. Inserts implicit cast if compatible. </summary>
		/// <returns> true, if the types are equal or compatible, false otherwise </returns>
		private bool TypeCheckLocal()
		{
			TypeNode targetType = lhs.DeclType;
			TypeNode exprType = rhs.Type;

			if(exprType.IsEqual(targetType))
				return true;

			rhs = BecomeParent(rhs.AdjustType(targetType, Coords));
			return rhs != ConstNode.Invalid;
		}

		protected internal override IR ConstructIR()
		{
			if(rhs != null)
				rhs = rhs.Evaluate();
			return new ConstructorParam(lhs.CheckIR(typeof(Entity)), rhs != null ? rhs.CheckIR(typeof(Expression)) : null);
		}
	}

}
