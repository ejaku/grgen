/*
 * GrGen: graph rewrite generator tool -- release GrGen.NET 8.1
 * Copyright (C) 2003-2026 Universitaet Karlsruhe, Institut fuer Programmstrukturen und Datenorganisation, LS Goos; and free programmers
 * licensed under LGPL v3, some components/parts use different licenses (see LICENSE.txt included in the packaging of this file)
 * www.grgen.de / www.grgen.net
 */

/// <summary>
/// @author Edgar Jakumeit
/// </summary>
namespace de.unika.ipd.grgen.ast.decl.pattern
{

using System.Collections.Generic;

using BaseNode = de.unika.ipd.grgen.ast.BaseNode;
using IdentNode = de.unika.ipd.grgen.ast.IdentNode;
using Operator = de.unika.ipd.grgen.ast.decl.executable.Operator;
using ExprNode = de.unika.ipd.grgen.ast.expr.ExprNode;
using InheritanceTypeNode = de.unika.ipd.grgen.ast.model.type.InheritanceTypeNode;
using PatternGraphLhsNode = de.unika.ipd.grgen.ast.pattern.PatternGraphLhsNode;
using TypeNode = de.unika.ipd.grgen.ast.type.TypeNode;
using IR = de.unika.ipd.grgen.ir.IR;
using Expression = de.unika.ipd.grgen.ir.expr.Expression;
using Index = de.unika.ipd.grgen.ir.model.Index;
using Edge = de.unika.ipd.grgen.ir.pattern.Edge;
using IndexAccessOrdering = de.unika.ipd.grgen.ir.pattern.IndexAccessOrdering;

public class MatchEdgeByIndexAccessOrderingDeclNode : MatchEdgeByIndexDeclNode
{
	static MatchEdgeByIndexAccessOrderingDeclNode()
	{
		SetClassName(typeof(MatchEdgeByIndexAccessOrderingDeclNode), "match edge by index access ordering decl");
	}

	private bool ascending;
	private Operator comp;
	private ExprNode expr;
	private Operator comp2;
	private ExprNode expr2;

	public MatchEdgeByIndexAccessOrderingDeclNode(IdentNode id, BaseNode type, int context,
			bool ascending, IdentNode index,
			Operator comp, ExprNode expr,
			Operator comp2, ExprNode expr2,
			PatternGraphLhsNode directlyNestingLHSGraph)
		: base(id, type, context, index, directlyNestingLHSGraph)
	{
		this.ascending = ascending;
		this.comp = comp;
		this.expr = expr;
		BecomeParent(this.expr);
		this.comp2 = comp2;
		this.expr2 = expr2;
		BecomeParent(this.expr);
	}

	/// <summary>
	/// returns children of this node </summary>
	public override ICollection<BaseNode> Children
	{
		get
		{
			IList<BaseNode> children = new List<BaseNode>();
			children.Add(ident);
			children.Add(GetValidVersion(typeUnresolved, typeEdgeDecl, typeTypeDecl));
			children.Add(constraints);
			children.Add(GetValidVersion(indexUnresolved, index));
			if(expr != null)
				children.Add(expr);
			if(expr2 != null)
				children.Add(expr2);
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
			childrenNames.Add("ident");
			childrenNames.Add("type");
			childrenNames.Add("constraints");
			childrenNames.Add("index");
			if(expr != null)
				childrenNames.Add("expression");
			if(expr2 != null)
				childrenNames.Add("expression2");
			return childrenNames;
		}
	}

	/// <seealso cref="de.unika.ipd.grgen.ast.BaseNode.resolveLocal() "/>
	protected internal override bool ResolveLocal()
	{
		bool successfullyResolved = base.ResolveLocal();
		if(expr != null)
			successfullyResolved &= expr.Resolve();
		if(expr2 != null)
			successfullyResolved &= expr2.Resolve();
		return successfullyResolved;
	}

	/// <seealso cref="de.unika.ipd.grgen.ast.BaseNode.checkLocal() "/>
	protected internal override bool CheckLocal()
	{
		bool res = base.CheckLocal();
		if(expr != null)
		{
			TypeNode expectedIndexAccessType = index.ExpectedAccessType;
			TypeNode indexAccessType = expr.Type;
			if(!indexAccessType.IsCompatibleTo(expectedIndexAccessType))
			{
				string expTypeName = expectedIndexAccessType.TypeName;
				string typeName = indexAccessType.TypeName;
				expr.ReportError("Cannot convert type used in accessing index from " + typeName
						+ " to the expected " + expTypeName
						+ " (in match edge" + EmptyWhenAnonymousPostfix(" ") + " by index access of " + index.ToStringWithDeclarationCoords() + ").");
				res = false;
			}
			if(expr2 != null)
			{ // TODO: distinguish lower and upper bound
				TypeNode indexAccessType2 = expr2.Type;
				if(!indexAccessType2.IsCompatibleTo(expectedIndexAccessType))
				{
					string expTypeName = expectedIndexAccessType.TypeName;
					string typeName = indexAccessType2.TypeName;
					expr2.ReportError("Cannot convert type used in accessing index from " + typeName
							+ " to the expected " + expTypeName
							+ " (in match edge" + EmptyWhenAnonymousPostfix(" ") + " by index access of " + index.ToStringWithDeclarationCoords() + ").");
					res = false;
				}
			}
		}
		TypeNode expectedEntityType = DeclType;
		InheritanceTypeNode entityType = index.Type;
		if(!entityType.IsCompatibleTo(expectedEntityType) && !expectedEntityType.IsCompatibleTo(entityType))
		{
			string expTypeName = expectedEntityType.ToStringWithDeclarationCoords();
			string typeName = entityType.ToStringWithDeclarationCoords();
			ident.ReportError("Cannot convert index type from " + typeName
					+ " to the expected pattern element type " + expTypeName
					+ " (in match edge" + EmptyWhenAnonymousPostfix(" ") + " by index access of " + index.ToStringWithDeclarationCoords() + ").");
			res = false;
		}
		if(comp == Operator.LT || comp == Operator.LE)
		{
			if(expr2 != null && (comp2 == Operator.LT || comp2 == Operator.LE))
			{
				ReportError("Two upper bounds are not supported"
						+ " (in match edge" + EmptyWhenAnonymousPostfix(" ") + " by index access of " + index.Ident + ").");
				res = false;
			}
		}
		if(comp == Operator.GT || comp == Operator.GE)
		{
			if(expr2 != null && (comp2 == Operator.GT || comp2 == Operator.GE))
			{
				ReportError("Two lower bounds are not supported"
						+ " (in match edge" + EmptyWhenAnonymousPostfix(" ") + " by index access of " + index.Ident + ").");
				res = false;
			}
		}
		return res;
	}

	/// <seealso cref="de.unika.ipd.grgen.ast.BaseNode.constructIR() "/>
	protected internal override IR ConstructIR()
	{
		if(IsIRAlreadySet()) // break endless recursion in case of cycle in usage
			return IR;

		Edge edge = (Edge)base.ConstructIR();

		IR = edge;

		if(expr != null)
			expr = expr.Evaluate();
		if(expr2 != null)
			expr2 = expr2.Evaluate();
		edge.Index = new IndexAccessOrdering(index.CheckIR(typeof(Index)), ascending,
				comp, expr != null ? expr.CheckIR(typeof(Expression)) : null,
				comp2, expr2 != null ? expr2.CheckIR(typeof(Expression)) : null);
		return edge;
	}
}

}
