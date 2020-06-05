/*
 * GrGen: graph rewrite generator tool -- release GrGen.NET 5.0
 * Copyright (C) 2003-2020 Universitaet Karlsruhe, Institut fuer Programmstrukturen und Datenorganisation, LS Goos; and free programmers
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
	protected BuiltinProcedureInvocationBaseNode decide()
	{
		String procedureName = procedureIdent.toString();
		switch(package_ + "::" + procedureName) {
		case "Transaction::start":
			if(arguments.size() != 0) {
				reportError("Transaction::start() takes no parameters.");
				return null;
			} else
				return new StartTransactionProcNode(getCoords());
		case "File::export":
			if(arguments.size() == 1) {
				return new ExportProcNode(getCoords(), arguments.get(0), null);
			} else if(arguments.size() == 2) {
				return new ExportProcNode(getCoords(), arguments.get(1), arguments.get(0));
			} else {
				reportError("File::export() takes 1 (filepath) or 2 (graph, filepath) parameters.");
				return null;
			}
		case "File::delete":
			if(arguments.size() == 1) {
				return new DeleteFileProcNode(getCoords(), arguments.get(0));
			} else {
				reportError("File::delete() takes 1 (filepath) parameters.");
				return null;
			}
		case "Debug::add":
			if(arguments.size() >= 1) {
				DebugAddProcNode add = new DebugAddProcNode(getCoords());
				for(ExprNode param : arguments.getChildren()) {
					add.addExpression(param);
				}
				return add;
			} else {
				reportError("Debug::add() takes at least one parameter, the message/computation entered.");
				return null;
			}
		case "Debug::rem":
			if(arguments.size() >= 1) {
				DebugRemProcNode rem = new DebugRemProcNode(getCoords());
				for(ExprNode param : arguments.getChildren()) {
					rem.addExpression(param);
				}
				return rem;
			} else {
				reportError("Debug::rem() takes at least one parameter, the message/computation left.");
				return null;
			}
		case "Debug::emit":
			if(arguments.size() >= 1) {
				DebugEmitProcNode emit = new DebugEmitProcNode(getCoords());
				for(ExprNode param : arguments.getChildren()) {
					emit.addExpression(param);
				}
				return emit;
			} else {
				reportError("Debug::emit() takes at least one parameter, the message to report.");
				return null;
			}
		case "Debug::halt":
			if(arguments.size() >= 1) {
				DebugHaltProcNode halt = new DebugHaltProcNode(getCoords());
				for(ExprNode param : arguments.getChildren()) {
					halt.addExpression(param);
				}
				return halt;
			} else {
				reportError("Debug::halt() takes at least one parameter, the message to report.");
				return null;
			}
		case "Debug::highlight":
			if(arguments.size() % 2 == 1) {
				DebugHighlightProcNode highlight = new DebugHighlightProcNode(getCoords());
				for(ExprNode param : arguments.getChildren()) {
					highlight.addExpression(param);
				}
				return highlight;
			} else {
				reportError("Debug::highlight() takes an odd number of parameters, first the message, then a series of pairs of the value to highlight followed by its annotation.");
				return null;
			}
		case "Transaction::pause":
			if(arguments.size() != 0) {
				reportError("Transaction::pause() takes no parameters.");
				return null;
			} else {
				return new PauseTransactionProcNode(getCoords());
			}
		case "Transaction::resume":
			if(arguments.size() != 0) {
				reportError("Transaction::resume() takes no parameters.");
				return null;
			} else {
				return new ResumeTransactionProcNode(getCoords());
			}
		case "Transaction::commit":
			if(arguments.size() != 1) {
				reportError("Transaction::commit(transactionId) takes one parameter.");
				return null;
			} else {
				return new CommitTransactionProcNode(getCoords(), arguments.get(0));
			}
		case "Transaction::rollback":
			if(arguments.size() != 1) {
				reportError("Transaction::rollback(transactionId) takes one parameter.");
				return null;
			} else {
				return new RollbackTransactionProcNode(getCoords(), arguments.get(0));
			}
		default:
			reportError("no computation " + procedureName + " known");
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
				reportError("package procedure call not allowed in function or lhs context (built-in-procedure)");
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
