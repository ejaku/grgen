/*
 * GrGen: graph rewrite generator tool -- release GrGen.NET 8.1
 * Copyright (C) 2003-2026 Universitaet Karlsruhe, Institut fuer Programmstrukturen und Datenorganisation, LS Goos; and free programmers
 * licensed under LGPL v3, some components/parts use different licenses (see LICENSE.txt included in the packaging of this file)
 * www.grgen.de / www.grgen.net
 */

/// <summary>
/// @author Edgar Jakumeit
/// </summary>

namespace de.unika.ipd.grgen.ast.stmt
{

	using System;
	using System.Collections.Generic;

	using de.unika.ipd.grgen.ast;
	using DeclNode = de.unika.ipd.grgen.ast.decl.DeclNode;
	using FilterFunctionDeclNode = de.unika.ipd.grgen.ast.decl.executable.FilterFunctionDeclNode;
	using FunctionDeclNode = de.unika.ipd.grgen.ast.decl.executable.FunctionDeclNode;
	using ProcedureDeclNode = de.unika.ipd.grgen.ast.decl.executable.ProcedureDeclNode;
	using ExprNode = de.unika.ipd.grgen.ast.expr.ExprNode;
	using TypeNode = de.unika.ipd.grgen.ast.type.TypeNode;
	using ReturnStatement = de.unika.ipd.grgen.ir.stmt.ReturnStatement;
	using ReturnStatementFilter = de.unika.ipd.grgen.ir.stmt.ReturnStatementFilter;
	using ReturnStatementProcedure = de.unika.ipd.grgen.ir.stmt.ReturnStatementProcedure;
	using IR = de.unika.ipd.grgen.ir.IR;
	using Expression = de.unika.ipd.grgen.ir.expr.Expression;
	using Coords = de.unika.ipd.grgen.parser.Coords;

	/// <summary>
	/// AST node representing a return statement (of function or procedure).
	/// </summary>
	public class ReturnStatementNode : EvalStatementNode
	{
		static ReturnStatementNode()
		{
			SetClassName(typeof(ReturnStatementNode), "ReturnStatement");
		}

		internal CollectNode<ExprNode> returnValueExprs;

		internal bool isFilterReturn = false;
		internal bool isFunctionReturn = false;

		public ReturnStatementNode(Coords coords, CollectNode<ExprNode> returnValueExprs)
			: base(coords)
		{
			this.returnValueExprs = returnValueExprs;
			BecomeParent(returnValueExprs);
		}

		/// <summary>
		/// returns children of this node </summary>
		public override ICollection<BaseNode> Children
		{
			get
			{
				IList<BaseNode> children = new List<BaseNode>();
				children.Add(returnValueExprs);
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
				childrenNames.Add("return value expressions");
				return childrenNames;
			}
		}

		protected internal override bool ResolveLocal()
		{
			return true;
		}

		protected internal override bool CheckLocal()
		{
			return true;
		}

		public override bool CheckStatementLocal(bool isLHS, DeclNode root, EvalStatementNode enclosingLoop)
		{
			if(!(root is FunctionDeclNode)
					&& !(root is ProcedureDeclNode)
					&& !(root is FilterFunctionDeclNode))
			{
				ReportError("A return statement must be nested inside a function or procedure or filter (or where do you want to return from otherwise?).");
				return false;
			}
			IList<TypeNode> retTypes;
			if(root is FilterFunctionDeclNode)
			{
				isFilterReturn = true;
				retTypes = new List<TypeNode>();
			}
			else if(root is FunctionDeclNode)
			{
				isFunctionReturn = true;
				FunctionDeclNode function = (FunctionDeclNode)root;
				retTypes = new List<TypeNode>();
				retTypes.Add(function.ResultType);
			}
			else
			{
				ProcedureDeclNode procedure = (ProcedureDeclNode)root;
				retTypes = procedure.ResultTypes;
			}
			return CheckReturns(retTypes, root);
		}

		/// <summary>
		/// Check if actual return arguments are conforming to the formal return parameters.
		/// </summary>
		protected internal virtual bool CheckReturns(IList<TypeNode> returnFormalParameters, DeclNode ident)
		{
			bool res = true;

			int declaredNumRets = returnFormalParameters.Count;
			int actualNumRets = returnValueExprs.Size();
			for(int i = 0; i < Math.Min(declaredNumRets, actualNumRets); ++i)
			{
				ExprNode retExpr = returnValueExprs.Get(i);
				TypeNode retExprType = retExpr.Type;
				TypeNode retDeclType = returnFormalParameters[i];
				if(!retExprType.IsCompatibleTo(retDeclType))
				{
					res = false;
					ReportError("Cannot convert the " + (i + 1) + ". return parameter"
							+ " from the type " + retExprType.TypeName
							+ " to the expected type " + retDeclType.TypeName
							+ retExprType.ToStringWithDeclarationCoordsIfCoordsAreOfInterest()
							+ retDeclType.ToStringWithDeclarationCoordsIfCoordsAreOfInterest()
							+ ".");
				}
			}

			//check the number of returned elements
			if(actualNumRets != declaredNumRets)
			{
				res = false;
				ReportError("Trying to return " + actualNumRets + " values, but expected are "
						+ declaredNumRets + " values (in " + ident + ").");
			}
			return res;
		}

		protected internal override IR ConstructIR()
		{
			if(isFilterReturn)
				return new ReturnStatementFilter();
			else if(isFunctionReturn)
			{
				ExprNode returnValueExpr = returnValueExprs.Get(0).Evaluate();
				return new ReturnStatement(returnValueExpr.CheckIR<Expression>(typeof(Expression)));
			}
			else
			{
				ReturnStatementProcedure rsp = new ReturnStatementProcedure();
				foreach(ExprNode returnValueExpr in returnValueExprs.ChildrenExact)
				{
					ExprNode returnValueExprEvaluated = returnValueExpr.Evaluate();
					rsp.AddReturnValueExpr(returnValueExprEvaluated.CheckIR<Expression>(typeof(Expression)));
				}
				return rsp;
			}
		}
	}

}
