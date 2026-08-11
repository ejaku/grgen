/*
 * GrGen: graph rewrite generator tool -- release GrGen.NET 8.1
 * Copyright (C) 2003-2026 Universitaet Karlsruhe, Institut fuer Programmstrukturen und Datenorganisation, LS Goos; and free programmers
 * licensed under LGPL v3, some components/parts use different licenses (see LICENSE.txt included in the packaging of this file)
 * www.grgen.de / www.grgen.net
 */

/// <summary>
/// @author Edgar Jakumeit
/// </summary>
namespace de.unika.ipd.grgen.ast.stmt.graph
{

using System.Collections.Generic;

using de.unika.ipd.grgen.ast;
using DeclNode = de.unika.ipd.grgen.ast.decl.DeclNode;
using Operator = de.unika.ipd.grgen.ast.decl.executable.Operator;
using ExprNode = de.unika.ipd.grgen.ast.expr.ExprNode;
using IndexDeclNode = de.unika.ipd.grgen.ast.model.decl.IndexDeclNode;
using PatternGraphLhsNode = de.unika.ipd.grgen.ast.pattern.PatternGraphLhsNode;
using EvalStatementNode = de.unika.ipd.grgen.ast.stmt.EvalStatementNode;
using TypeNode = de.unika.ipd.grgen.ast.type.TypeNode;
using de.unika.ipd.grgen.ast.util;
using IR = de.unika.ipd.grgen.ir.IR;
using Expression = de.unika.ipd.grgen.ir.expr.Expression;
using Index = de.unika.ipd.grgen.ir.model.Index;
using IndexAccessOrdering = de.unika.ipd.grgen.ir.pattern.IndexAccessOrdering;
using Variable = de.unika.ipd.grgen.ir.pattern.Variable;
using EvalStatement = de.unika.ipd.grgen.ir.stmt.EvalStatement;
using ForIndexAccessOrdering = de.unika.ipd.grgen.ir.stmt.graph.ForIndexAccessOrdering;
using Coords = de.unika.ipd.grgen.parser.Coords;

//deprecated, TODO: purge
public class ForIndexAccessOrderingYieldNode : ForIndexAccessNode
{
	static ForIndexAccessOrderingYieldNode()
	{
		SetClassName(typeof(ForIndexAccessOrderingYieldNode), "for index access ordering yield loop");
	}

	private bool ascending;
	private Operator comp;
	private ExprNode expr;
	private Operator comp2;
	private ExprNode expr2;

	public ForIndexAccessOrderingYieldNode(Coords coords, BaseNode iterationVariable, int context,
			bool ascending, IdentNode index,
			Operator comp, ExprNode expr,
			Operator comp2, ExprNode expr2,
			PatternGraphLhsNode directlyNestingLHSGraph,
			CollectNode<EvalStatementNode> loopedStatements)
		 : base(coords, iterationVariable, context, index, directlyNestingLHSGraph, loopedStatements)
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
		children.Add(GetValidVersion(iterationVariableUnresolved, iterationVariable));
		children.Add(GetValidVersion(indexUnresolved, index));
		if(expr != null)
			children.Add(expr);
		if(expr2 != null)
			children.Add(expr2);
		children.Add(statements);
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
		childrenNames.Add("iterVar");
		childrenNames.Add("index");
		if(expr != null)
			childrenNames.Add("expression");
		if(expr2 != null)
			childrenNames.Add("expression2");
		childrenNames.Add("loopedStatements");
		return childrenNames;
		}
	}

	private static DeclarationResolver<IndexDeclNode> indexResolver =
			new DeclarationResolver<IndexDeclNode>(typeof(IndexDeclNode));

	/// <seealso cref="de.unika.ipd.grgen.ast.BaseNode.resolveLocal() "/>
	protected internal override bool ResolveLocal()
	{
		bool successfullyResolved = true;

		if(!ResolveIterationVariable("index access ordering"))
			successfullyResolved = false;

		index = indexResolver.Resolve(indexUnresolved, this);
		successfullyResolved &= index != null;
		if(expr != null)
			successfullyResolved &= expr.Resolve();
		if(expr2 != null)
			successfullyResolved &= expr2.Resolve();
		return successfullyResolved;
	}

	/// <seealso cref="de.unika.ipd.grgen.ast.BaseNode.checkLocal() "/>
	protected internal override bool CheckLocal()
	{
		if(!CheckIterationVariable("index access ordering"))
			return false;

		bool res = true;
		if(expr != null)
		{
			TypeNode expectedIndexAccessType = index.ExpectedAccessType;
			TypeNode indexAccessType = expr.Type;
			if(!indexAccessType.IsCompatibleTo(expectedIndexAccessType))
			{
				ReportError("Cannot convert type used in accessing index"
						+ " from " + indexAccessType.ToStringWithDeclarationCoords()
						+ " to the expected " + expectedIndexAccessType.ToStringWithDeclarationCoords()
						+ " in index access loop (on " + indexUnresolved + ").");
				return false;
			}
			if(expr2 != null)
			{
				TypeNode indexAccessType2 = expr2.Type;
				if(!indexAccessType2.IsCompatibleTo(expectedIndexAccessType))
				{
					ReportError("Cannot convert type used in accessing index"
							+ " from " + indexAccessType2.ToStringWithDeclarationCoords()
							+ " to the expected " + expectedIndexAccessType.ToStringWithDeclarationCoords()
							+ " in index access loop (on " + indexUnresolved + ").");
					return false;
				}
			}
		}
		TypeNode expectedEntityType = iterationVariable.DeclType;
		TypeNode entityType = index.Type;
		if(!entityType.IsCompatibleTo(expectedEntityType) && !expectedEntityType.IsCompatibleTo(entityType))
		{
			ReportError("Cannot convert index type"
					+ " from " + entityType.ToStringWithDeclarationCoords()
					+ " to the expected " + expectedEntityType.ToStringWithDeclarationCoords()
					+ " in index access loop (on " + indexUnresolved + ").");
			return false;
		}
		if(comp == Operator.LT || comp == Operator.LE)
		{
			if(expr2 != null && (comp2 == Operator.LT || comp2 == Operator.LE))
			{
				ReportError("The index access loop does not support two upper bounds"
						+ " (given when accessing " + indexUnresolved + ").");
				return false;
			}
		}
		if(comp == Operator.GT || comp == Operator.GE)
		{
			if(expr2 != null && (comp2 == Operator.GT || comp2 == Operator.GE))
			{
				ReportError("The index access loop does not support two lower bounds"
						+ " (given when accessing " + indexUnresolved + ").");
				return false;
			}
		}
		return res;
	}

	public override bool CheckStatementLocal(bool isLHS, DeclNode root, EvalStatementNode enclosingLoop)
	{
		return true;
	}

	/// <seealso cref="de.unika.ipd.grgen.ast.BaseNode.constructIR() "/>
	protected internal override IR ConstructIR()
	{
		if(expr != null)
			expr = expr.Evaluate();
		if(expr2 != null)
			expr2 = expr2.Evaluate();
		ForIndexAccessOrdering fiao = new ForIndexAccessOrdering(iterationVariable.CheckIR(typeof(Variable)),
				new IndexAccessOrdering(index.CheckIR(typeof(Index)), ascending,
						comp, expr != null ? expr.CheckIR(typeof(Expression)) : null,
						comp2, expr2 != null ? expr2.CheckIR(typeof(Expression)) : null));
		foreach(EvalStatementNode accumulationStatement in statements.ChildrenExact)
			fiao.AddLoopedStatement(accumulationStatement.CheckIR(typeof(EvalStatement)));
		return fiao;
	}
}

}
