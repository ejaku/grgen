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
	using ExprNode = de.unika.ipd.grgen.ast.expr.ExprNode;
	using IndexDeclNode = de.unika.ipd.grgen.ast.model.decl.IndexDeclNode;
	using PatternGraphLhsNode = de.unika.ipd.grgen.ast.pattern.PatternGraphLhsNode;
	using EvalStatementNode = de.unika.ipd.grgen.ast.stmt.EvalStatementNode;
	using TypeNode = de.unika.ipd.grgen.ast.type.TypeNode;
	using de.unika.ipd.grgen.ast.util;
	using IR = de.unika.ipd.grgen.ir.IR;
	using Expression = de.unika.ipd.grgen.ir.expr.Expression;
	using Index = de.unika.ipd.grgen.ir.model.Index;
	using IndexAccessEquality = de.unika.ipd.grgen.ir.pattern.IndexAccessEquality;
	using Variable = de.unika.ipd.grgen.ir.pattern.Variable;
	using EvalStatement = de.unika.ipd.grgen.ir.stmt.EvalStatement;
	using ForIndexAccessEquality = de.unika.ipd.grgen.ir.stmt.graph.ForIndexAccessEquality;
	using Coords = de.unika.ipd.grgen.parser.Coords;

	// deprecated, TODO: purge
	public class ForIndexAccessEqualityYieldNode : ForIndexAccessNode
	{
		static ForIndexAccessEqualityYieldNode()
		{
			SetClassName(typeof(ForIndexAccessEqualityYieldNode), "for index access equality yield loop");
		}

		private ExprNode expr;

		public ForIndexAccessEqualityYieldNode(Coords coords, BaseNode iterationVariable, int context,
				IdentNode index, ExprNode expr, PatternGraphLhsNode directlyNestingLHSGraph,
				CollectNode<EvalStatementNode> loopedStatements)
			 : base(coords, iterationVariable, context, index, directlyNestingLHSGraph, loopedStatements)
		{
			this.expr = expr;
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
				children.Add(expr);
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
				childrenNames.Add("expression");
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

			if(!ResolveIterationVariable("index access equality"))
				successfullyResolved = false;

			index = indexResolver.Resolve(indexUnresolved, this);
			successfullyResolved &= index != null;
			successfullyResolved &= expr.Resolve();
			return successfullyResolved;
		}

		/// <seealso cref="de.unika.ipd.grgen.ast.BaseNode.checkLocal() "/>
		protected internal override bool CheckLocal()
		{
			if(!CheckIterationVariable("index access equality"))
				return false;

			bool res = true;
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
			return res;
		}

		public override bool CheckStatementLocal(bool isLHS, DeclNode root, EvalStatementNode enclosingLoop)
		{
			return true;
		}

		/// <seealso cref="de.unika.ipd.grgen.ast.BaseNode.constructIR() "/>
		protected internal override IR ConstructIR()
		{
			expr = expr.Evaluate();
			ForIndexAccessEquality fiae = new ForIndexAccessEquality(iterationVariable.CheckIR<Variable>(typeof(Variable)),
					new IndexAccessEquality(index.CheckIR<Index>(typeof(Index)), expr.CheckIR<Expression>(typeof(Expression))));
			foreach(EvalStatementNode accumulationStatement in statements.ChildrenExact)
				fiae.AddLoopedStatement(accumulationStatement.CheckIR<EvalStatement>(typeof(EvalStatement)));
			return fiae;
		}
	}

}
