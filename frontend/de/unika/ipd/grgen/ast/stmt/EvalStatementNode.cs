/*
 * GrGen: graph rewrite generator tool -- release GrGen.NET 8.1
 * Copyright (C) 2003-2026 Universitaet Karlsruhe, Institut fuer Programmstrukturen und Datenorganisation, LS Goos; and free programmers
 * licensed under LGPL v3, some components/parts use different licenses (see LICENSE.txt included in the packaging of this file)
 * www.grgen.de / www.grgen.net
 */

/// <summary>
/// @author Moritz Kroll, Edgar Jakumeit
/// </summary>

namespace de.unika.ipd.grgen.ast.stmt
{
	using de.unika.ipd.grgen.ast;
	using DeclNode = de.unika.ipd.grgen.ast.decl.DeclNode;
	using FunctionDeclNode = de.unika.ipd.grgen.ast.decl.executable.FunctionDeclNode;
	using ProcedureDeclNode = de.unika.ipd.grgen.ast.decl.executable.ProcedureDeclNode;
	using SubpatternDeclNode = de.unika.ipd.grgen.ast.decl.executable.SubpatternDeclNode;
	using ExprNode = de.unika.ipd.grgen.ast.expr.ExprNode;
	using OrderedReplacementNode = de.unika.ipd.grgen.ast.pattern.OrderedReplacementNode;
	using TypeNode = de.unika.ipd.grgen.ast.type.TypeNode;
	using Coords = de.unika.ipd.grgen.parser.Coords;

	public abstract class EvalStatementNode : OrderedReplacementNode
	{
		public EvalStatementNode(Coords coords) : base(coords)
		{
		}

		protected internal virtual bool CheckType(ExprNode value, TypeNode targetType, string statement, string parameter)
		{
			TypeNode givenType = value.Type;
			TypeNode expectedType = targetType;
			if(!givenType.IsCompatibleTo(expectedType))
			{
				ReportError("Cannot convert parameter " + parameter + " of " + statement
						+ " from " + givenType.ToStringWithDeclarationCoords()
						+ " to the expected " + expectedType.ToStringWithDeclarationCoords() + ".");
				return false;
			}
			return true;
		}

		public abstract bool CheckStatementLocal(bool isLHS, DeclNode root, EvalStatementNode enclosingLoop);

		public static bool CheckStatements(bool isLHS, DeclNode root, EvalStatementNode enclosingLoop,
				CollectNode<EvalStatementNode> evals, bool evalsAreTopLevel)
		{
			// check computation statement structure
			bool res = true;

			EvalStatementNode last = null;
			bool returnPassed = false;
			foreach(EvalStatementNode eval in evals.ChildrenExact)
			{
				if(returnPassed)
				{
					eval.ReportError("No statements allowed after a return statement (at the same nesting level; these statements would not be executed).");
					res = false;
				}

				res &= eval.CheckStatementLocal(isLHS, root, enclosingLoop);
				last = eval;

				if(eval is ConditionStatementNode)
				{
					ConditionStatementNode csn = (ConditionStatementNode)eval;
					res &= CheckStatements(isLHS, root, enclosingLoop, csn.statements, false);
					res &= CheckStatements(isLHS, root, enclosingLoop, csn.falseCaseStatements, false);
				}
				else if(eval is NestingStatementNode)
				{
					NestingStatementNode nsn = (NestingStatementNode)eval;
					res &= CheckStatements(isLHS, root, nsn, nsn.statements, false);
				}
				else if(eval is ReturnStatementNode)
					returnPassed = true;
				else if(eval is ReturnAssignmentNode)
				{
					if(root is FunctionDeclNode || isLHS)
					{
						ReturnAssignmentNode returnAssignment = (ReturnAssignmentNode)eval;
						if(returnAssignment.builtinProcedure == null
							|| (!returnAssignment.builtinProcedure.IsEmitOrDebugProcedure()))
						{
							if(root is FunctionDeclNode) // TODO: report name of procedure that is attempted to be called
								eval.ReportError("A procedure call is not allowed in a function (only emit/emitdebug/assert/assertAlways and the Debug package functions are admissible).");
							else
								eval.ReportError("A procedure call is not allowed in a yield block (only emit/emitdebug/assert/assertAlways and the Debug package functions are admissible).");
							res = false;
						}
					}
				}
				else if(eval is ExecStatementNode)
				{
					if(root is SubpatternDeclNode)
						eval.ReportError("An exec inside an eval is forbidden in a subpattern -- move it outside of the eval"
								+ " (so it becomes a deferred exec, executed at the end of rewriting, on the by-then current graph and the local entities valid at the end of its local rewriting).");
				}
			}

			if(evalsAreTopLevel)
			{
				if(root is FunctionDeclNode)
				{
					if(!(last is ReturnStatementNode) && ((FunctionDeclNode)root).functionAuto == null)
					{
						if(last is ConditionStatementNode)
						{
							if(!AllCasesEndWithReturn((ConditionStatementNode)last))
							{
								last.ReportError("All cases of a terminating if in a function must end with a return statement (missing in " + root.Kind + " " + root.Ident + ").");
								res = false;
							}
						}
						else
						{
							if(last != null && last.Coords.HasLocation())
								last.ReportError("A function must end with a return statement (missing in " + root.Kind + " " + root.Ident + ").");
							else
								root.ReportError("A function must end with a return statement (missing in " + root.Kind + " " + root.Ident + ").");
							res = false;
						}
					}
				}
				if(root is ProcedureDeclNode)
				{
					if(!(last is ReturnStatementNode))
					{
						if(last is ConditionStatementNode)
						{
							if(!AllCasesEndWithReturn((ConditionStatementNode)last))
							{
								last.ReportError("All cases of a terminating if in a procedure must end with a return statement (missing in " + root.Kind + " " + root.Ident + ").");
								res = false;
							}
						}
						else
						{
							if(last != null && last.Coords.HasLocation())
								last.ReportError("A procedure must end with a return statement (missing in " + root.Kind + " " + root.Ident + ").");
							else
								root.ReportError("A procedure must end with a return statement (missing in " + root.Kind + " " + root.Ident + ").");
							res = false;
						}
					}
				}
			}

			// TODO: check for def before use in computations, of computations entities
			// did for assignment targets and indexed assignment targets (see "Variables (node,edge,var,ref) of computations must be declared before they can be assigned");
			// but this is far from sufficient, needed for other kinds of assignments, too
			// and for reads, expressions, too 
			// -- massive externsion/refactoring needed (or clever hack?) cause grgen was built for not distinguishing order of entities

			return res;
		}

		public static bool AllCasesEndWithReturn(ConditionStatementNode condition)
		{
			bool allEndWithReturn = true;

			EvalStatementNode last = null;
			foreach(EvalStatementNode eval in condition.statements.ChildrenExact)
				last = eval;
			if(!(last is ReturnStatementNode))
			{
				if(last is ConditionStatementNode)
					allEndWithReturn &= AllCasesEndWithReturn((ConditionStatementNode)last);
				else
					return false;
			}

			last = null;
			foreach(EvalStatementNode eval in condition.falseCaseStatements.ChildrenExact)
				last = eval;
			if(!(last is ReturnStatementNode))
			{
				if(last is ConditionStatementNode)
					allEndWithReturn &= AllCasesEndWithReturn((ConditionStatementNode)last);
				else
					return false;
			}

			return allEndWithReturn;
		}

		public virtual bool IteratedNotReferenced(string iterName)
		{
			bool res = true;
			foreach(BaseNode child in Children)
			{
				if(child is ExprNode)
					res &= ((ExprNode)child).IteratedNotReferenced(iterName);
			}
			return res;
		}

		public override bool NoExecStatement(bool inEvalHereContext)
		{
			bool res = true;
			foreach(BaseNode child in Children)
			{
				if(!(child is EvalStatementNode))
					continue;
				EvalStatementNode evalStatement = (EvalStatementNode)child;
				res &= evalStatement.NoExecStatement(inEvalHereContext);
			}
			return res;
		}
	}

}
