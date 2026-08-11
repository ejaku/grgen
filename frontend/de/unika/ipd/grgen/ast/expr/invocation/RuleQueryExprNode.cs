/*
 * GrGen: graph rewrite generator tool -- release GrGen.NET 8.1
 * Copyright (C) 2003-2026 Universitaet Karlsruhe, Institut fuer Programmstrukturen und Datenorganisation, LS Goos; and free programmers
 * licensed under LGPL v3, some components/parts use different licenses (see LICENSE.txt included in the packaging of this file)
 * www.grgen.de / www.grgen.net
 */

/// <summary>
/// @author Edgar Jakumeit
/// </summary>

namespace de.unika.ipd.grgen.ast.expr.invocation
{

	using System.Collections.Generic;

	using de.unika.ipd.grgen.ast;
	using ExprNode = de.unika.ipd.grgen.ast.expr.ExprNode;
	using TypeNode = de.unika.ipd.grgen.ast.type.TypeNode;
	using IR = de.unika.ipd.grgen.ir.IR;
	using RuleQueryExpr = de.unika.ipd.grgen.ir.expr.invocation.RuleQueryExpr;
	using Coords = de.unika.ipd.grgen.parser.Coords;

	public class RuleQueryExprNode : ExprNode
	{
		static RuleQueryExprNode()
		{
			SetClassName(typeof(RuleQueryExprNode), "rule query");
		}

		private CallActionNode callAction;

		private TypeNode arrayOfMatchTypeUnresolved;
		private TypeNode arrayOfMatchType;

		public RuleQueryExprNode(Coords coords, CallActionNode callAction, TypeNode arrayOfMatchType)
			: base(coords)
		{

			this.callAction = BecomeParent(callAction);
			this.arrayOfMatchTypeUnresolved = BecomeParent(arrayOfMatchType);
		}

		public override ICollection<BaseNode> Children
		{
			get
			{
				IList<BaseNode> children = new List<BaseNode>();
				children.Add(callAction);
				children.Add(GetValidVersion(arrayOfMatchTypeUnresolved, arrayOfMatchType));
				return children;
			}
		}

		public override ICollection<string> ChildrenNames
		{
			get
			{
				IList<string> childrenNames = new List<string>();
				childrenNames.Add("callAction");
				childrenNames.Add("arrayOfMatchType");
				return childrenNames;
			}
		}

		protected internal override bool ResolveLocal()
		{
			if(arrayOfMatchTypeUnresolved.Resolve())
				arrayOfMatchType = arrayOfMatchTypeUnresolved;
			return arrayOfMatchType != null;
		}

		protected internal override bool CheckLocal()
		{
			return true;
		}

		public virtual CallActionNode CallAction
		{
			get
			{
				return callAction;
			}
		}

		protected internal override IR ConstructIR()
		{
			return new RuleQueryExpr(Type.IRType);
		}

		public override TypeNode Type
		{
			get
			{
				return arrayOfMatchType;
			}
		}
	}

}
