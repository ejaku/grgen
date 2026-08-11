/*
 * GrGen: graph rewrite generator tool -- release GrGen.NET 8.1
 * Copyright (C) 2003-2026 Universitaet Karlsruhe, Institut fuer Programmstrukturen und Datenorganisation, LS Goos; and free programmers
 * licensed under LGPL v3, some components/parts use different licenses (see LICENSE.txt included in the packaging of this file)
 * www.grgen.de / www.grgen.net
 */

/// <summary>
/// @author Edgar Jakumeit
/// </summary>

namespace de.unika.ipd.grgen.ast
{

	using System.Collections.Generic;

	using DeclNode = de.unika.ipd.grgen.ast.decl.DeclNode;
	using VarDeclNode = de.unika.ipd.grgen.ast.decl.pattern.VarDeclNode;
	using ExprNode = de.unika.ipd.grgen.ast.expr.ExprNode;
	using TypeNode = de.unika.ipd.grgen.ast.type.TypeNode;
	using FilterInvocationLambdaExpression = de.unika.ipd.grgen.ir.FilterInvocationLambdaExpression;
	using IR = de.unika.ipd.grgen.ir.IR;
	using Ident = de.unika.ipd.grgen.ir.Ident;
	using Rule = de.unika.ipd.grgen.ir.executable.Rule;
	using Expression = de.unika.ipd.grgen.ir.expr.Expression;
	using Variable = de.unika.ipd.grgen.ir.pattern.Variable;
	using Coords = de.unika.ipd.grgen.parser.Coords;

	public class FilterInvocationLambdaExpressionNode : FilterInvocationBaseNode
	{
		static FilterInvocationLambdaExpressionNode()
		{
			SetClassName(typeof(FilterInvocationLambdaExpressionNode), "filter invocation lambda expression");
		}

		internal string filterName;
		internal string assignEntity;
		internal TypeNode entityType;

		internal VarDeclNode initArrayAccessVar;
		internal ExprNode initExpr;

		internal VarDeclNode arrayAccessVar;
		internal VarDeclNode previousAccumulationAccessVar;
		internal VarDeclNode indexVar;
		internal VarDeclNode elementVar;
		internal ExprNode lambdaExpr;

		public FilterInvocationLambdaExpressionNode(IdentNode iteratedUnresolved,
				Coords coords, string filterName, string assignEntity,
				VarDeclNode arrayAccessVar, VarDeclNode indexVar, VarDeclNode elementVar, ExprNode lambdaExpr)
			: base(coords, iteratedUnresolved)
		{
			this.iteratedUnresolved = BecomeParent(iteratedUnresolved);
			this.filterName = filterName;
			this.assignEntity = assignEntity;
			this.arrayAccessVar = arrayAccessVar;
			this.indexVar = indexVar;
			this.elementVar = elementVar;
			this.lambdaExpr = lambdaExpr;
		}

		public FilterInvocationLambdaExpressionNode(IdentNode iteratedUnresolved,
				Coords coords, string filterName, string assignEntity,
				VarDeclNode initArrayAccessVar, ExprNode initExpr,
				VarDeclNode arrayAccessVar, VarDeclNode previousAccumulationAccessVar,
				VarDeclNode indexVar, VarDeclNode elementVar, ExprNode lambdaExpr)
			: base(coords, iteratedUnresolved)
		{
			this.iteratedUnresolved = BecomeParent(iteratedUnresolved);
			this.filterName = filterName;
			this.assignEntity = assignEntity;
			this.initArrayAccessVar = initArrayAccessVar;
			this.initExpr = initExpr;
			this.arrayAccessVar = arrayAccessVar;
			this.previousAccumulationAccessVar = previousAccumulationAccessVar;
			this.indexVar = indexVar;
			this.elementVar = elementVar;
			this.lambdaExpr = lambdaExpr;
		}

		public override ICollection<BaseNode> Children
		{
			get
			{
				IList<BaseNode> children = new List<BaseNode>();
				children.Add(GetValidVersion(iteratedUnresolved, iterated));
				if(initArrayAccessVar != null)
					children.Add(initArrayAccessVar);
				if(initExpr != null)
					children.Add(initExpr);
				if(arrayAccessVar != null)
					children.Add(arrayAccessVar);
				if(previousAccumulationAccessVar != null)
					children.Add(previousAccumulationAccessVar);
				if(indexVar != null)
					children.Add(indexVar);
				children.Add(elementVar);
				children.Add(lambdaExpr);
				return children;
			}
		}

		public override ICollection<string> ChildrenNames
		{
			get
			{
				IList<string> childrenNames = new List<string>();
				childrenNames.Add("iterated");
				if(initArrayAccessVar != null)
					childrenNames.Add("initArrayAccessVar");
				if(initExpr != null)
					childrenNames.Add("initExpr");
				if(arrayAccessVar != null)
					childrenNames.Add("arrayAccessVar");
				if(previousAccumulationAccessVar != null)
					childrenNames.Add("previousAccumulationAccessVar");
				if(indexVar != null)
					childrenNames.Add("indexVar");
				childrenNames.Add("elementVar");
				childrenNames.Add("lambdaExpr");
				return childrenNames;
			}
		}

		protected internal override bool ResolveLocal()
		{
			// owner
			bool iteratedOk = base.ResolveLocal();
			if(!iteratedOk)
				return false;
			return true;
		}

		protected internal override bool CheckLocal()
		{
			// member
			if(!string.ReferenceEquals(assignEntity, null))
			{
				DeclNode resolvedEntity = iterated.pattern.TryGetMember(assignEntity);
				if(resolvedEntity == null)
				{
					ReportError("Unknown entity " + assignEntity + " in " + iterated.Ident + ".");
					return false;
				}
				entityType = resolvedEntity.DeclType;
			}
			return true;
		}

		protected internal override IR ConstructIR()
		{
			FilterInvocationLambdaExpression filterInvocation;
			if(initExpr != null)
				initExpr = initExpr.Evaluate();
			lambdaExpr = lambdaExpr.Evaluate();
			string fullFilterName = filterName + "<" + assignEntity + ">";
			filterInvocation = new FilterInvocationLambdaExpression(fullFilterName, new Ident(fullFilterName, Coords),
					filterName, assignEntity, entityType != null ? entityType.IRType : null, iterated.CheckIR<Rule>(typeof(Rule)),
					initArrayAccessVar != null ? initArrayAccessVar.CheckIR<Variable>(typeof(Variable)) : null,
					initExpr != null ? initExpr.CheckIR<Expression>(typeof(Expression)) : null,
					arrayAccessVar != null ? arrayAccessVar.CheckIR<Variable>(typeof(Variable)) : null,
					previousAccumulationAccessVar != null ? previousAccumulationAccessVar.CheckIR<Variable>(typeof(Variable)) : null,
					indexVar != null ? indexVar.CheckIR(typeof(Variable)) : null, elementVar.CheckIR<Variable>(typeof(Variable)),
					lambdaExpr.CheckIR<Expression>(typeof(Expression)));
			return filterInvocation;
		}
	}

}
