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
	using IR = de.unika.ipd.grgen.ir.IR;
	using Constant = de.unika.ipd.grgen.ir.expr.Constant;
	using Expression = de.unika.ipd.grgen.ir.expr.Expression;
	using AssertProc = de.unika.ipd.grgen.ir.stmt.procenv.AssertProc;
	using Type = de.unika.ipd.grgen.ir.type.Type;
	using Coords = de.unika.ipd.grgen.parser.Coords;

	public class AssertProcNode : BuiltinProcedureInvocationBaseNode
	{
		static AssertProcNode()
		{
			SetClassName(typeof(AssertProcNode), "assert procedure");
		}

		private CollectNode<ExprNode> exprs = new CollectNode<ExprNode>();
		internal bool isAlways;

		public AssertProcNode(Coords coords, bool isAlways)
			: base(coords)
		{

			this.exprs = BecomeParent(exprs);
			this.isAlways = isAlways;
		}

		public virtual void AddExpression(ExprNode expr)
		{
			exprs.AddChild(expr);
		}

		public override ICollection<BaseNode> Children
		{
			get
			{
				IList<BaseNode> children = new List<BaseNode>();
				children.Add(exprs);
				return children;
			}
		}

		public override ICollection<string> ChildrenNames
		{
			get
			{
				IList<string> childrenNames = new List<string>();
				childrenNames.Add("exprs");
				return childrenNames;
			}
		}

		protected internal override bool ResolveLocal()
		{
			return true;
		}

		protected internal override bool CheckLocal()
		{
			ExprNode condition = exprs.Get(0);
			TypeNode conditionType = condition.Type;
			if(!conditionType.IsEqual(BasicTypeNode.booleanType))
			{
				condition.ReportError("The " + AssertProcName() + " procedure expects as 1. argument (condition to assert on)"
						+ " a value of type boolean"
						+ " (but is given a value of type " + conditionType.ToStringWithDeclarationCoords() + ").");
				return false;
			}

			if(exprs.Size() >= 2)
			{
				ExprNode message = exprs.Get(1);
				TypeNode messageType = message.Type;
				if(!messageType.IsEqual(BasicTypeNode.stringType))
				{
					message.ReportError("The " + AssertProcName() + " procedure expects as 2. argument (message)"
							+ " a value of type string"
							+ " (but is given a value of type " + messageType.ToStringWithDeclarationCoords() + ").");
					return false;
				}
			}

			// regarding remaining parameters: any type goes, must be converted toString in implementation
			return true;
		}

		private string AssertProcName()
		{
			return isAlways ? "assertAlways" : "assert";
		}

		public override bool CheckStatementLocal(bool isLHS, DeclNode root, EvalStatementNode enclosingLoop)
		{
			return true;
		}

		protected internal override IR ConstructIR()
		{
			IList<Expression> expressions = new List<Expression>();
			foreach(ExprNode expr in exprs.ChildrenExact)
			{
				ExprNode exprEvaluated = expr.Evaluate();
				expressions.Add(exprEvaluated.CheckIR<Expression>(typeof(Expression)));
			}
			if(exprs.Size() == 1)
				expressions.Add(new Constant(BasicTypeNode.stringType.CheckIR<Type>(typeof(Type)), EscapeBackslashAndDoubleQuotes(Coords.ToString())));
			return new AssertProc(expressions, isAlways);
		}

		protected internal static string EscapeBackslashAndDoubleQuotes(string input)
		{
			return input.Replace("\\", "\\\\").Replace("\"", "\\\"");
		}
	}

}
