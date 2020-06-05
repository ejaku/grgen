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

import java.util.Collection;
import java.util.Vector;

import de.unika.ipd.grgen.ast.*;
import de.unika.ipd.grgen.ast.decl.DeclNode;
import de.unika.ipd.grgen.ast.expr.ExprNode;
import de.unika.ipd.grgen.ast.stmt.BuiltinProcedureInvocationBaseNode;
import de.unika.ipd.grgen.ast.stmt.EvalStatementNode;
import de.unika.ipd.grgen.ast.stmt.graph.GraphAddCopyEdgeProcNode;
import de.unika.ipd.grgen.ast.stmt.graph.GraphAddCopyNodeProcNode;
import de.unika.ipd.grgen.ast.stmt.graph.GraphAddEdgeProcNode;
import de.unika.ipd.grgen.ast.stmt.graph.GraphAddNodeProcNode;
import de.unika.ipd.grgen.ast.stmt.graph.GraphClearProcNode;
import de.unika.ipd.grgen.ast.stmt.graph.GraphMergeProcNode;
import de.unika.ipd.grgen.ast.stmt.graph.GraphRedirectSourceAndTargetProcNode;
import de.unika.ipd.grgen.ast.stmt.graph.GraphRedirectSourceProcNode;
import de.unika.ipd.grgen.ast.stmt.graph.GraphRedirectTargetProcNode;
import de.unika.ipd.grgen.ast.stmt.graph.GraphRemoveProcNode;
import de.unika.ipd.grgen.ast.stmt.graph.GraphRetypeProcNode;
import de.unika.ipd.grgen.ast.stmt.graph.InsertCopyProcNode;
import de.unika.ipd.grgen.ast.stmt.graph.InsertDefinedSubgraphProcNode;
import de.unika.ipd.grgen.ast.stmt.graph.InsertInducedSubgraphProcNode;
import de.unika.ipd.grgen.ast.stmt.graph.InsertProcNode;
import de.unika.ipd.grgen.ast.stmt.graph.VAllocProcNode;
import de.unika.ipd.grgen.ast.stmt.graph.VFreeNonResetProcNode;
import de.unika.ipd.grgen.ast.stmt.graph.VFreeProcNode;
import de.unika.ipd.grgen.ast.stmt.graph.VResetProcNode;
import de.unika.ipd.grgen.ast.stmt.procenv.CommitTransactionProcNode;
import de.unika.ipd.grgen.ast.stmt.procenv.DebugAddProcNode;
import de.unika.ipd.grgen.ast.stmt.procenv.DebugEmitProcNode;
import de.unika.ipd.grgen.ast.stmt.procenv.DebugHaltProcNode;
import de.unika.ipd.grgen.ast.stmt.procenv.DebugHighlightProcNode;
import de.unika.ipd.grgen.ast.stmt.procenv.DebugRemProcNode;
import de.unika.ipd.grgen.ast.stmt.procenv.DeleteFileProcNode;
import de.unika.ipd.grgen.ast.stmt.procenv.EmitProcNode;
import de.unika.ipd.grgen.ast.stmt.procenv.ExportProcNode;
import de.unika.ipd.grgen.ast.stmt.procenv.PauseTransactionProcNode;
import de.unika.ipd.grgen.ast.stmt.procenv.RecordProcNode;
import de.unika.ipd.grgen.ast.stmt.procenv.ResumeTransactionProcNode;
import de.unika.ipd.grgen.ast.stmt.procenv.RollbackTransactionProcNode;
import de.unika.ipd.grgen.ast.stmt.procenv.StartTransactionProcNode;
import de.unika.ipd.grgen.ast.type.TypeNode;
import de.unika.ipd.grgen.ast.type.executable.ProcedureTypeNode;
import de.unika.ipd.grgen.ir.IR;
import de.unika.ipd.grgen.parser.ParserEnvironment;

public class ProcedureInvocationDecisionNode extends ProcedureInvocationBaseNode
{
	static {
		setName(ProcedureInvocationDecisionNode.class, "procedure invocation decision");
	}

	static TypeNode procedureTypeNode = new ProcedureTypeNode();

	private IdentNode procedureIdent;
	private BuiltinProcedureInvocationBaseNode result;

	ParserEnvironment env;

	public ProcedureInvocationDecisionNode(IdentNode procedureIdent, CollectNode<ExprNode> arguments, int context,
			ParserEnvironment env)
	{
		super(procedureIdent.getCoords(), arguments, context);
		this.procedureIdent = becomeParent(procedureIdent);
		this.env = env;
	}

	@Override
	public Collection<? extends BaseNode> getChildren()
	{
		Vector<BaseNode> children = new Vector<BaseNode>();
		//children.add(methodIdent);	// HACK: We don't have a declaration, so avoid failure during check phase
		children.add(arguments);
		if(isResolved())
			children.add(result);
		return children;
	}

	@Override
	public Collection<String> getChildrenNames()
	{
		Vector<String> childrenNames = new Vector<String>();
		//childrenNames.add("methodIdent");
		childrenNames.add("params");
		if(isResolved())
			childrenNames.add("result");
		return childrenNames;
	}

	protected boolean resolveLocal()
	{
		result = decide(procedureIdent.toString());
		return result != null;
	}
	
	private BuiltinProcedureInvocationBaseNode decide(String procedureName)
	{
		switch(procedureName) {
		case "add":
			if(arguments.size() == 1) {
				return new GraphAddNodeProcNode(getCoords(), arguments.get(0));
			} else if(arguments.size() == 3) {
				return new GraphAddEdgeProcNode(getCoords(), arguments.get(0), arguments.get(1), arguments.get(2));
			} else {
				reportError(procedureName + "() takes 1 or 3 parameters.");
				return null;
			}
		case "retype":
			if(arguments.size() == 2) {
				return new GraphRetypeProcNode(getCoords(), arguments.get(0), arguments.get(1));
			} else {
				reportError(procedureName + "() takes 2 parameters.");
				return null;
			}
		case "insert":
			if(arguments.size() != 1) {
				reportError("insert(.) takes one parameter.");
				return null;
			} else
				return new InsertProcNode(getCoords(), arguments.get(0));
		case "insertCopy":
			if(arguments.size() != 2) {
				reportError("insertCopy(.,.) takes two parameters.");
				return null;
			} else
				return new InsertCopyProcNode(getCoords(), arguments.get(0), arguments.get(1));
		case "insertInduced":
			if(arguments.size() != 2) {
				reportError("insertInduced(.,.) takes two parameters.");
				return null;
			} else
				return new InsertInducedSubgraphProcNode(getCoords(), arguments.get(0), arguments.get(1));
		case "insertDefined":
			if(arguments.size() != 2) {
				reportError("insertDefined(.,.) takes two parameters.");
				return null;
			} else
				return new InsertDefinedSubgraphProcNode(getCoords(), arguments.get(0), arguments.get(1));
		case "valloc":
			if(arguments.size() != 0) {
				reportError("valloc() takes no parameters.");
				return null;
			} else
				return new VAllocProcNode(getCoords());
		case "Transaction::start":
			if(arguments.size() != 0) {
				reportError("Transaction::start() takes no parameters.");
				return null;
			} else
				return new StartTransactionProcNode(getCoords());
		case "rem":
			if(arguments.size() != 1) {
				reportError("rem(value) takes one parameter.");
				return null;
			} else {
				return new GraphRemoveProcNode(getCoords(), arguments.get(0));
			}
		case "clear":
			if(arguments.size() != 0) {
				reportError("clear() takes no parameters.");
				return null;
			} else {
				return new GraphClearProcNode(getCoords());
			}
		case "vfree":
			if(arguments.size() != 1) {
				reportError("vfree(value) takes one parameter.");
				return null;
			} else {
				return new VFreeProcNode(getCoords(), arguments.get(0));
			}
		case "vfreenonreset":
			if(arguments.size() != 1) {
				reportError("vfreenonreset(value) takes one parameter.");
				return null;
			} else {
				return new VFreeNonResetProcNode(getCoords(), arguments.get(0));
			}
		case "vreset":
			if(arguments.size() != 1) {
				reportError("vreset(value) takes one parameter.");
				return null;
			} else {
				return new VResetProcNode(getCoords(), arguments.get(0));
			}
		case "record":
			if(arguments.size() != 1) {
				reportError("record(value) takes one parameter.");
				return null;
			} else {
				return new RecordProcNode(getCoords(), arguments.get(0));
			}
		case "emit":
			if(arguments.size() >= 1) {
				EmitProcNode emit = new EmitProcNode(getCoords(), false);
				for(ExprNode param : arguments.getChildren()) {
					emit.addExpression(param);
				}
				return emit;
			} else {
				reportError("emit() takes at least one parameter.");
				return null;
			}
		case "emitdebug":
			if(arguments.size() >= 1) {
				EmitProcNode emit = new EmitProcNode(getCoords(), true);
				for(ExprNode param : arguments.getChildren()) {
					emit.addExpression(param);
				}
				return emit;
			} else {
				reportError("emitdebug() takes at least one parameter.");
				return null;
			}
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
		case "addCopy":
			if(arguments.size() == 1) {
				return new GraphAddCopyNodeProcNode(getCoords(), arguments.get(0));
			} else if(arguments.size() == 3) {
				return new GraphAddCopyEdgeProcNode(getCoords(), arguments.get(0), arguments.get(1), arguments.get(2));
			} else {
				reportError(procedureName + "() takes 1 or 3 parameters.");
				return null;
			}
		case "merge":
			if(arguments.size() < 2 || arguments.size() > 3) {
				reportError("merge(target,source,oldSourceName) takes two or three parameters.");
				return null;
			} else {
				if(arguments.size() == 2)
					return new GraphMergeProcNode(getCoords(), arguments.get(0), arguments.get(1), null);
				else
					return new GraphMergeProcNode(getCoords(), arguments.get(0), arguments.get(1), arguments.get(2));
			}
		case "redirectSource":
			if(arguments.size() < 2 || arguments.size() > 3) {
				reportError("redirectSource(edge,newSource,oldSourceName) takes two or three parameters.");
				return null;
			} else {
				if(arguments.size() == 2)
					return new GraphRedirectSourceProcNode(getCoords(), arguments.get(0), arguments.get(1), null);
				else
					return new GraphRedirectSourceProcNode(getCoords(), arguments.get(0), arguments.get(1), arguments.get(2));
			}
		case "redirectTarget":
			if(arguments.size() < 2 || arguments.size() > 3) {
				reportError("redirectTarget(edge,newTarget,oldTargetName) takes two or three parameters.");
				return null;
			} else {
				if(arguments.size() == 2)
					return new GraphRedirectTargetProcNode(getCoords(), arguments.get(0), arguments.get(1), null);
				else
					return new GraphRedirectTargetProcNode(getCoords(), arguments.get(0), arguments.get(1), arguments.get(2));
			}
		case "redirectSourceAndTarget":
			if(arguments.size() != 3 && arguments.size() != 5) {
				reportError("redirectSourceAndTarget(edge,newSource,newTarget,oldSourceName,oldTargetName) takes three or five parameters.");
				return null;
			} else {
				if(arguments.size() == 3)
					return new GraphRedirectSourceAndTargetProcNode(getCoords(), arguments.get(0), arguments.get(1),
							arguments.get(2), null, null);
				else
					return new GraphRedirectSourceAndTargetProcNode(getCoords(), arguments.get(0), arguments.get(1),
							arguments.get(2), arguments.get(3), arguments.get(4));
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
			if(isEmitOrDebugProcedure()) { // allowed exceptions
				return true;
			} else {
				reportError("procedure call not allowed in function or lhs context (built-in-procedure)");
				return false;
			}
		}
		return true;
	}
	
	// procedures for debugging purpose, allowed also on lhs
	public boolean isEmitOrDebugProcedure()
	{
		switch(procedureIdent.toString()) {
		case "emit":
		case "emitdebug":
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

	public boolean checkStatementLocal(boolean isLHS, DeclNode root, EvalStatementNode enclosingLoop)
	{
		return true;
	}

	protected ProcedureOrBuiltinProcedureInvocationBaseNode getResult()
	{
		return result;
	}

	public Vector<TypeNode> getType()
	{
		return result.getType();
	}

	public int getNumReturnTypes()
	{
		return result.getType().size();
	}

	public String getProcedureName()
	{
		return procedureIdent.toString();
	}

	@Override
	protected IR constructIR()
	{
		return result.getIR();
	}
}
