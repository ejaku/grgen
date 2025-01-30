/*
 * GrGen: graph rewrite generator tool -- release GrGen.NET 7.1
 * Copyright (C) 2003-2025 Universitaet Karlsruhe, Institut fuer Programmstrukturen und Datenorganisation, LS Goos; and free programmers
 * licensed under LGPL v3 (see LICENSE.txt included in the packaging of this file)
 * www.grgen.net
 */

/**
 * @author Edgar Jakumeit
 */

package de.unika.ipd.grgen.ast.stmt.invocation;

import de.unika.ipd.grgen.ast.*;
import de.unika.ipd.grgen.ast.expr.ExprNode;
import de.unika.ipd.grgen.ast.stmt.BuiltinProcedureInvocationBaseNode;
import de.unika.ipd.grgen.ast.stmt.procenv.CommitTransactionProcNode;
import de.unika.ipd.grgen.ast.stmt.procenv.DebugAddProcNode;
import de.unika.ipd.grgen.ast.stmt.procenv.DebugEmitProcNode;
import de.unika.ipd.grgen.ast.stmt.procenv.DebugHaltProcNode;
import de.unika.ipd.grgen.ast.stmt.procenv.DebugHighlightProcNode;
import de.unika.ipd.grgen.ast.stmt.procenv.DebugRemProcNode;
import de.unika.ipd.grgen.ast.stmt.procenv.DeleteFileProcNode;
import de.unika.ipd.grgen.ast.stmt.procenv.ExportProcNode;
import de.unika.ipd.grgen.ast.stmt.procenv.PauseTransactionProcNode;
import de.unika.ipd.grgen.ast.stmt.procenv.ResumeTransactionProcNode;
import de.unika.ipd.grgen.ast.stmt.procenv.RollbackTransactionProcNode;
import de.unika.ipd.grgen.ast.stmt.procenv.StartTransactionProcNode;
import de.unika.ipd.grgen.ast.stmt.procenv.SynchronizationEnterProcNode;
import de.unika.ipd.grgen.ast.stmt.procenv.SynchronizationExitProcNode;
import de.unika.ipd.grgen.ast.stmt.procenv.SynchronizationTryEnterProcNode;
import de.unika.ipd.grgen.ast.util.ResolvingEnvironment;
import de.unika.ipd.grgen.parser.ParserEnvironment;

public class PackageProcedureInvocationDecisionNode extends ProcedureInvocationDecisionNode
{
	static {
		setName(PackageProcedureInvocationDecisionNode.class, "package procedure invocation decision");
	}

	private String package_;

	public PackageProcedureInvocationDecisionNode(String package_, IdentNode procedureIdent,
			CollectNode<ExprNode> arguments, int context, ParserEnvironment env)
	{
		super(procedureIdent, arguments, context, env);
		this.package_ = package_;
	}

	@Override
	protected boolean resolveLocal()
	{
		ResolvingEnvironment resolvingEnvironment = new ResolvingEnvironment(env, error, getCoords());
		result = decide(package_ + "::" + procedureIdent.toString(), arguments, resolvingEnvironment);
		return result != null;
	}

	private static BuiltinProcedureInvocationBaseNode decide(String procedureName, CollectNode<ExprNode> arguments,
			ResolvingEnvironment env)
	{
		switch(procedureName) {
		case "Transaction::start":
			if(arguments.size() != 0) {
				env.reportError("Transaction::start() expects 0 arguments (given are " + arguments.size() + " arguments).");
				return null;
			} else
				return new StartTransactionProcNode(env.getCoords());
		case "File::export":
			if(arguments.size() == 1) {
				return new ExportProcNode(env.getCoords(), arguments.get(0), null);
			} else if(arguments.size() == 2) {
				return new ExportProcNode(env.getCoords(), arguments.get(1), arguments.get(0));
			} else {
				env.reportError("File::export() expects 1 (filepath) or 2 (graph, filepath) arguments (given are " + arguments.size() + " arguments).");
				return null;
			}
		case "File::delete":
			if(arguments.size() == 1) {
				return new DeleteFileProcNode(env.getCoords(), arguments.get(0));
			} else {
				env.reportError("File::delete() expects 1 (filepath) argument (given are " + arguments.size() + " arguments).");
				return null;
			}
		case "Debug::add":
			if(arguments.size() >= 1) {
				DebugAddProcNode add = new DebugAddProcNode(env.getCoords());
				for(ExprNode param : arguments.getChildren()) {
					add.addExpression(param);
				}
				return add;
			} else {
				env.reportError("Debug::add() expects at least one argument, the message/computation entered (given are " + arguments.size() + " arguments).");
				return null;
			}
		case "Debug::rem":
			if(arguments.size() >= 1) {
				DebugRemProcNode rem = new DebugRemProcNode(env.getCoords());
				for(ExprNode param : arguments.getChildren()) {
					rem.addExpression(param);
				}
				return rem;
			} else {
				env.reportError("Debug::rem() expects at least one argument, the message/computation left (given are " + arguments.size() + " arguments).");
				return null;
			}
		case "Debug::emit":
			if(arguments.size() >= 1) {
				DebugEmitProcNode emit = new DebugEmitProcNode(env.getCoords());
				for(ExprNode param : arguments.getChildren()) {
					emit.addExpression(param);
				}
				return emit;
			} else {
				env.reportError("Debug::emit() expects at least one argument, the message to report (given are " + arguments.size() + " arguments).");
				return null;
			}
		case "Debug::halt":
			if(arguments.size() >= 1) {
				DebugHaltProcNode halt = new DebugHaltProcNode(env.getCoords());
				for(ExprNode param : arguments.getChildren()) {
					halt.addExpression(param);
				}
				return halt;
			} else {
				env.reportError("Debug::halt() expects at least one argument, the message to report (given are " + arguments.size() + " arguments).");
				return null;
			}
		case "Debug::highlight":
			if(arguments.size() % 2 == 1) {
				DebugHighlightProcNode highlight = new DebugHighlightProcNode(env.getCoords());
				for(ExprNode param : arguments.getChildren()) {
					highlight.addExpression(param);
				}
				return highlight;
			} else {
				env.reportError("Debug::highlight() expects an odd number of arguments, first the message, then a series of pairs of the value to highlight followed by its annotation (given are " + arguments.size() + " arguments).");
				return null;
			}
		case "Transaction::pause":
			if(arguments.size() != 0) {
				env.reportError("Transaction::pause() expects 0 arguments (given are " + arguments.size() + " arguments).");
				return null;
			} else {
				return new PauseTransactionProcNode(env.getCoords());
			}
		case "Transaction::resume":
			if(arguments.size() != 0) {
				env.reportError("Transaction::resume() expects 0 arguments (given are " + arguments.size() + " arguments).");
				return null;
			} else {
				return new ResumeTransactionProcNode(env.getCoords());
			}
		case "Transaction::commit":
			if(arguments.size() != 1) {
				env.reportError("Transaction::commit(transactionId) expects 1 argument (given are " + arguments.size() + " arguments).");
				return null;
			} else {
				return new CommitTransactionProcNode(env.getCoords(), arguments.get(0));
			}
		case "Transaction::rollback":
			if(arguments.size() != 1) {
				env.reportError("Transaction::rollback(transactionId) expects 1 argument (given are " + arguments.size() + " arguments).");
				return null;
			} else {
				return new RollbackTransactionProcNode(env.getCoords(), arguments.get(0));
			}
		case "Synchronization::enter":
			if(arguments.size() != 1) {
				env.reportError("Synchronization::enter(criticalSectionObject) expects 1 argument (given are " + arguments.size() + " arguments).");
				return null;
			} else {
				return new SynchronizationEnterProcNode(env.getCoords(), arguments.get(0));
			}
		case "Synchronization::tryenter":
			if(arguments.size() != 1) {
				env.reportError("Synchronization::tryenter(criticalSectionObject) expects 1 argument (given are " + arguments.size() + " arguments).");
				return null;
			} else
				return new SynchronizationTryEnterProcNode(env.getCoords(), arguments.get(0));
		case "Synchronization::exit":
			if(arguments.size() != 1) {
				env.reportError("Synchronization::exit(criticalSectionObject) expects 1 argument (given are " + arguments.size() + " arguments).");
				return null;
			} else {
				return new SynchronizationExitProcNode(env.getCoords(), arguments.get(0));
			}
		default:
			env.reportError("A procedure of name " + procedureName + " is not known.");
			return null;
		}
	}

	@Override
	protected boolean checkLocal()
	{
		if((context & BaseNode.CONTEXT_FUNCTION_OR_PROCEDURE) == BaseNode.CONTEXT_FUNCTION) {
			if(isDebugProcedure()) { // allowed exceptions
				return true;
			} else {
				reportError("A package procedure call (built-in-procedure " + procedureIdent + ") is not allowed in function or pattern part context.");
				return false;
			}
		}
		return true;
	}
	
	// procedures for debugging purpose, allowed also on lhs
	@Override
	public boolean isEmitOrDebugProcedure()
	{
		return isEmitProcedure() || isDebugProcedure();
	}

	public boolean isDebugProcedure()
	{
		switch(package_ + "::" + procedureIdent.toString()) {
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
