/*
 * GrGen: graph rewrite generator tool -- release GrGen.NET 8.1
 * Copyright (C) 2003-2026 Universitaet Karlsruhe, Institut fuer Programmstrukturen und Datenorganisation, LS Goos; and free programmers
 * licensed under LGPL v3, some components/parts use different licenses (see LICENSE.txt included in the packaging of this file)
 * www.grgen.de / www.grgen.net
 */

/// <summary>
/// @author Edgar Jakumeit
/// </summary>

namespace de.unika.ipd.grgen.ast.stmt.invocation
{
	using de.unika.ipd.grgen.ast;
	using ExprNode = de.unika.ipd.grgen.ast.expr.ExprNode;
	using BuiltinProcedureInvocationBaseNode = de.unika.ipd.grgen.ast.stmt.BuiltinProcedureInvocationBaseNode;
	using CommitTransactionProcNode = de.unika.ipd.grgen.ast.stmt.procenv.CommitTransactionProcNode;
	using DebugAddProcNode = de.unika.ipd.grgen.ast.stmt.procenv.DebugAddProcNode;
	using DebugEmitProcNode = de.unika.ipd.grgen.ast.stmt.procenv.DebugEmitProcNode;
	using DebugHaltProcNode = de.unika.ipd.grgen.ast.stmt.procenv.DebugHaltProcNode;
	using DebugHighlightProcNode = de.unika.ipd.grgen.ast.stmt.procenv.DebugHighlightProcNode;
	using DebugRemProcNode = de.unika.ipd.grgen.ast.stmt.procenv.DebugRemProcNode;
	using DeleteFileProcNode = de.unika.ipd.grgen.ast.stmt.procenv.DeleteFileProcNode;
	using ExportProcNode = de.unika.ipd.grgen.ast.stmt.procenv.ExportProcNode;
	using PauseTransactionProcNode = de.unika.ipd.grgen.ast.stmt.procenv.PauseTransactionProcNode;
	using ResumeTransactionProcNode = de.unika.ipd.grgen.ast.stmt.procenv.ResumeTransactionProcNode;
	using RollbackTransactionProcNode = de.unika.ipd.grgen.ast.stmt.procenv.RollbackTransactionProcNode;
	using StartTransactionProcNode = de.unika.ipd.grgen.ast.stmt.procenv.StartTransactionProcNode;
	using SynchronizationEnterProcNode = de.unika.ipd.grgen.ast.stmt.procenv.SynchronizationEnterProcNode;
	using SynchronizationExitProcNode = de.unika.ipd.grgen.ast.stmt.procenv.SynchronizationExitProcNode;
	using SynchronizationTryEnterProcNode = de.unika.ipd.grgen.ast.stmt.procenv.SynchronizationTryEnterProcNode;
	using ResolvingEnvironment = de.unika.ipd.grgen.ast.util.ResolvingEnvironment;
	using ParserEnvironment = de.unika.ipd.grgen.parser.ParserEnvironment;

	public class PackageProcedureInvocationDecisionNode : ProcedureInvocationDecisionNode
	{
		static PackageProcedureInvocationDecisionNode()
		{
			SetClassName(typeof(PackageProcedureInvocationDecisionNode), "package procedure invocation decision");
		}

		private string package_;

		public PackageProcedureInvocationDecisionNode(string package_, IdentNode procedureIdent,
				CollectNode<ExprNode> arguments, int context, ParserEnvironment env)
			: base(procedureIdent, arguments, context, env)
		{
			this.package_ = package_;
		}

		protected internal override bool ResolveLocal()
		{
			ResolvingEnvironment resolvingEnvironment = new ResolvingEnvironment(env, error, Coords);
			result = Decide(package_ + "::" + procedureIdent.ToString(), arguments, resolvingEnvironment);
			return result != null;
		}

		private static BuiltinProcedureInvocationBaseNode Decide(string procedureName, CollectNode<ExprNode> arguments,
				ResolvingEnvironment env)
		{
			switch(procedureName)
			{
			case "Transaction::start":
				if(arguments.Size() != 0)
				{
					env.ReportError("Transaction::start() expects 0 arguments (given are " + arguments.Size() + " arguments).");
					return null;
				}
				else
					return new StartTransactionProcNode(env.Coords);
			case "File::export":
				if(arguments.Size() == 1)
					return new ExportProcNode(env.Coords, arguments.Get(0), null);
				else if(arguments.Size() == 2)
					return new ExportProcNode(env.Coords, arguments.Get(1), arguments.Get(0));
				else
				{
					env.ReportError("File::export() expects 1 (filepath) or 2 (graph, filepath) arguments (given are " + arguments.Size() + " arguments).");
					return null;
				}
			case "File::delete":
				if(arguments.Size() == 1)
					return new DeleteFileProcNode(env.Coords, arguments.Get(0));
				else
				{
					env.ReportError("File::delete() expects 1 (filepath) argument (given are " + arguments.Size() + " arguments).");
					return null;
				}
			case "Debug::add":
				if(arguments.Size() >= 1)
				{
					DebugAddProcNode add = new DebugAddProcNode(env.Coords);
					foreach(ExprNode param in arguments.ChildrenExact)
						add.AddExpression(param);
					return add;
				}
				else
				{
					env.ReportError("Debug::add() expects at least one argument, the message/computation entered (given are " + arguments.Size() + " arguments).");
					return null;
				}
			case "Debug::rem":
				if(arguments.Size() >= 1)
				{
					DebugRemProcNode rem = new DebugRemProcNode(env.Coords);
					foreach(ExprNode param in arguments.ChildrenExact)
						rem.AddExpression(param);
					return rem;
				}
				else
				{
					env.ReportError("Debug::rem() expects at least one argument, the message/computation left (given are " + arguments.Size() + " arguments).");
					return null;
				}
			case "Debug::emit":
				if(arguments.Size() >= 1)
				{
					DebugEmitProcNode emit = new DebugEmitProcNode(env.Coords);
					foreach(ExprNode param in arguments.ChildrenExact)
						emit.AddExpression(param);
					return emit;
				}
				else
				{
					env.ReportError("Debug::emit() expects at least one argument, the message to report (given are " + arguments.Size() + " arguments).");
					return null;
				}
			case "Debug::halt":
				if(arguments.Size() >= 1)
				{
					DebugHaltProcNode halt = new DebugHaltProcNode(env.Coords);
					foreach(ExprNode param in arguments.ChildrenExact)
						halt.AddExpression(param);
					return halt;
				}
				else
				{
					env.ReportError("Debug::halt() expects at least one argument, the message to report (given are " + arguments.Size() + " arguments).");
					return null;
				}
			case "Debug::highlight":
				if(arguments.Size() % 2 == 1)
				{
					DebugHighlightProcNode highlight = new DebugHighlightProcNode(env.Coords);
					foreach(ExprNode param in arguments.ChildrenExact)
						highlight.AddExpression(param);
					return highlight;
				}
				else
				{
					env.ReportError("Debug::highlight() expects an odd number of arguments, first the message, then a series of pairs of the value to highlight followed by its annotation (given are " + arguments.Size() + " arguments).");
					return null;
				}
			case "Transaction::pause":
				if(arguments.Size() != 0)
				{
					env.ReportError("Transaction::pause() expects 0 arguments (given are " + arguments.Size() + " arguments).");
					return null;
				}
				else
					return new PauseTransactionProcNode(env.Coords);
			case "Transaction::resume":
				if(arguments.Size() != 0)
				{
					env.ReportError("Transaction::resume() expects 0 arguments (given are " + arguments.Size() + " arguments).");
					return null;
				}
				else
					return new ResumeTransactionProcNode(env.Coords);
			case "Transaction::commit":
				if(arguments.Size() != 1)
				{
					env.ReportError("Transaction::commit(transactionId) expects 1 argument (given are " + arguments.Size() + " arguments).");
					return null;
				}
				else
					return new CommitTransactionProcNode(env.Coords, arguments.Get(0));
			case "Transaction::rollback":
				if(arguments.Size() != 1)
				{
					env.ReportError("Transaction::rollback(transactionId) expects 1 argument (given are " + arguments.Size() + " arguments).");
					return null;
				}
				else
					return new RollbackTransactionProcNode(env.Coords, arguments.Get(0));
			case "Synchronization::enter":
				if(arguments.Size() != 1)
				{
					env.ReportError("Synchronization::enter(criticalSectionObject) expects 1 argument (given are " + arguments.Size() + " arguments).");
					return null;
				}
				else
					return new SynchronizationEnterProcNode(env.Coords, arguments.Get(0));
			case "Synchronization::tryenter":
				if(arguments.Size() != 1)
				{
					env.ReportError("Synchronization::tryenter(criticalSectionObject) expects 1 argument (given are " + arguments.Size() + " arguments).");
					return null;
				}
				else
					return new SynchronizationTryEnterProcNode(env.Coords, arguments.Get(0));
			case "Synchronization::exit":
				if(arguments.Size() != 1)
				{
					env.ReportError("Synchronization::exit(criticalSectionObject) expects 1 argument (given are " + arguments.Size() + " arguments).");
					return null;
				}
				else
					return new SynchronizationExitProcNode(env.Coords, arguments.Get(0));
			default:
				env.ReportError("A procedure of name " + procedureName + " is not known.");
				return null;
			}
		}

		protected internal override bool CheckLocal()
		{
			if((context & BaseNode.CONTEXT_FUNCTION_OR_PROCEDURE) == BaseNode.CONTEXT_FUNCTION)
			{
				if(IsDebugProcedure()) // allowed exceptions
					return true;
				else
				{
					ReportError("A package procedure call (built-in-procedure " + procedureIdent + ") is not allowed in function or pattern part context.");
					return false;
				}
			}
			return true;
		}

		// procedures for debugging purpose, allowed also on lhs
		public override bool IsEmitOrDebugProcedure()
		{
			return IsEmitProcedure() || IsDebugProcedure();
		}

		protected internal override bool IsDebugProcedure()
		{
			switch(package_ + "::" + procedureIdent.ToString())
			{
			case "Debug::add":
			case "Debug::rem":
			case "Debug::emit":
			case "Debug::halt":
			case "Debug::highlight":
				return true;
			default:
				return false;
			}
		}
	}

}
