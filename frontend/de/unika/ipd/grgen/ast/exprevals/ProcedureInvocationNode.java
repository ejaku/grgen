/*
 * GrGen: graph rewrite generator tool -- release GrGen.NET 4.4
 * Copyright (C) 2003-2014 Universitaet Karlsruhe, Institut fuer Programmstrukturen und Datenorganisation, LS Goos; and free programmers
 * licensed under LGPL v3 (see LICENSE.txt included in the packaging of this file)
 * www.grgen.net
 */

/**
 * @author Edgar Jakumeit
 */

package de.unika.ipd.grgen.ast.exprevals;

import java.util.Collection;
import java.util.Vector;

import de.unika.ipd.grgen.ast.*;
import de.unika.ipd.grgen.ir.IR;
import de.unika.ipd.grgen.parser.ParserEnvironment;

public class ProcedureInvocationNode extends ProcedureInvocationBaseNode
{
	static {
		setName(ProcedureInvocationNode.class, "procedure invocation");
	}

	static TypeNode procedureTypeNode = new ProcedureTypeNode();
	
	private IdentNode procedureIdent;
	private CollectNode<ExprNode> params;
	private ProcedureInvocationBaseNode result;

	private int context;

	ParserEnvironment env;

	public ProcedureInvocationNode(IdentNode procedureIdent, CollectNode<ExprNode> params, int context, ParserEnvironment env)
	{
		super(procedureIdent.getCoords());
		this.procedureIdent = becomeParent(procedureIdent);
		this.params = becomeParent(params);
		this.context = context;
		this.env = env;
	}

	@Override
	public Collection<? extends BaseNode> getChildren() {
		Vector<BaseNode> children = new Vector<BaseNode>();
		//children.add(methodIdent);	// HACK: We don't have a declaration, so avoid failure during check phase
		children.add(params);
		if(isResolved())
			children.add(result);
		return children;
	}

	@Override
	public Collection<String> getChildrenNames() {
		Vector<String> childrenNames = new Vector<String>();
		//childrenNames.add("methodIdent");
		childrenNames.add("params");
		if(isResolved())
			childrenNames.add("result");
		return childrenNames;
	}

	protected boolean resolveLocal() {
		String procedureName = procedureIdent.toString();

		if(procedureName.equals("add")) {
			if(params.size() == 1) {
				result = new GraphAddNodeProcNode(getCoords(), params.get(0));
			} else if(params.size() == 3) {
				result = new GraphAddEdgeProcNode(getCoords(), params.get(0), params.get(1), params.get(2));
			} else {
				reportError(procedureName + "() takes 1 or 3 parameters.");
				return false;
			}
		}
		else if(procedureName.equals("retype")) {
			if(params.size() == 2) {
				result = new GraphRetypeProcNode(getCoords(), params.get(0), params.get(1));
			} else {
				reportError(procedureName + "() takes 2 parameters.");
				return false;
			}
		}
		else if(procedureName.equals("insert")) {
			if(params.size() != 1) {
				reportError("insert(.) takes one parameter.");
				return false;
			}
			else
				result = new InsertProcNode(getCoords(), params.get(0));
		}
		else if(procedureName.equals("insertCopy")) {
			if(params.size() != 2) {
				reportError("insertCopy(.,.) takes two parameters.");
				return false;
			}
			else
				result = new InsertCopyProcNode(getCoords(), params.get(0), params.get(1));
		}
		else if(procedureName.equals("insertInduced")) {
			if(params.size() != 2) {
				reportError("insertInduced(.,.) takes two parameters.");
				return false;
			}
			else
				result = new InsertInducedSubgraphProcNode(getCoords(), params.get(0), params.get(1));
		}
		else if(procedureName.equals("insertDefined")) {
			if(params.size() != 2) {
				reportError("insertDefined(.,.) takes two parameters.");
				return false;
			}
			else
				result = new InsertDefinedSubgraphProcNode(getCoords(), params.get(0), params.get(1));
		}
		else if(procedureName.equals("valloc")) {
			if(params.size() != 0) {
				reportError("valloc() takes no parameters.");
				return false;
			}
			else
				result = new VAllocProcNode(getCoords());
		}
		else if(procedureName.equals("startTransaction")) {
			if(params.size() != 0) {
				reportError("Transaction::start() takes no parameters.");
				return false;
			}
			else
				result = new StartTransactionProcNode(getCoords());
		}
		else if(procedureName.equals("rem")) {
			if(params.size() != 1) {
				reportError("rem(value) takes one parameter.");
				return false;
			}
			else {
				result = new GraphRemoveProcNode(getCoords(), params.get(0));
			}
		}
		else if(procedureName.equals("clear")) {
			if(params.size() != 0) {
				reportError("clear() takes no parameters.");
				return false;
			}
			else {
				result = new GraphClearProcNode(getCoords());
			}
		}
		else if(procedureName.equals("vfree")) {
			if(params.size() != 1) {
				reportError("vfree(value) takes one parameter.");
				return false;
			}
			else {
				result = new VFreeProcNode(getCoords(), params.get(0));
			}
		}
		else if(procedureName.equals("vfreenonreset")) {
			if(params.size() != 1) {
				reportError("vfreenonreset(value) takes one parameter.");
				return false;
			}
			else {
				result = new VFreeNonResetProcNode(getCoords(), params.get(0));
			}
		}
		else if(procedureName.equals("vreset")) {
			if(params.size() != 1) {
				reportError("vreset(value) takes one parameter.");
				return false;
			}
			else {
				result = new VResetProcNode(getCoords(), params.get(0));
			}
		}
		else if(procedureName.equals("record")) {
			if(params.size() != 1) {
				reportError("record(value) takes one parameter.");
				return false;
			}
			else {
				result = new RecordProcNode(getCoords(), params.get(0));
			}
		}
		else if(procedureName.equals("emit")) {
			if(params.size() != 1) {
				reportError("emit(value) takes one parameter.");
				return false;
			}
			else {
				result = new EmitProcNode(getCoords(), params.get(0));
			}
		}
		else if(procedureName.equals("exportFile")) {
			if(params.size() == 1) {
				result = new ExportProcNode(getCoords(), params.get(0), null);
			} else if(params.size() == 2) {
				result = new ExportProcNode(getCoords(), params.get(1), params.get(0));
			} else {
				reportError("File::export() takes 1 (filepath) or 2 (graph, filepath) parameters.");
				return false;
			}
		}
		else if(procedureName.equals("deleteFile")) {
			if(params.size() == 1) {
				result = new DeleteFileProcNode(getCoords(), params.get(0));
			} else {
				reportError("File::delete() takes 1 (filepath) parameters.");
				return false;
			}
		}
		else if(procedureName.equals("addDebug")) {
			if(params.size() >= 1) {
				DebugAddProcNode add = new DebugAddProcNode(getCoords());
				for(ExprNode param : params.getChildren()) {
					add.addExpression(param);
				}
				result = add;
			} else {
				reportError("Debug::add() takes at least one parameter, the message/computation entered.");
				return false;				
			}
		}
		else if(procedureName.equals("remDebug")) {
			if(params.size() >= 1) {
				DebugRemProcNode rem = new DebugRemProcNode(getCoords());
				for(ExprNode param : params.getChildren()) {
					rem.addExpression(param);
				}
				result = rem;
			} else {
				reportError("Debug::rem() takes at least one parameter, the message/computation left.");
				return false;				
			}
		}
		else if(procedureName.equals("emitDebug")) {
			if(params.size() >= 1) {
				DebugEmitProcNode rem = new DebugEmitProcNode(getCoords());
				for(ExprNode param : params.getChildren()) {
					rem.addExpression(param);
				}
				result = rem;
			} else {
				reportError("Debug::emit() takes at least one parameter, the message to report.");
				return false;				
			}
		}
		else if(procedureName.equals("haltDebug")) {
			if(params.size() >= 1) {
				DebugHaltProcNode rem = new DebugHaltProcNode(getCoords());
				for(ExprNode param : params.getChildren()) {
					rem.addExpression(param);
				}
				result = rem;
			} else {
				reportError("Debug::halt() takes at least one parameter, the message to report.");
				return false;				
			}
		}
		else if(procedureName.equals("highlightDebug")) {
			if(params.size() % 2 == 1) {
				DebugHighlightProcNode highlight = new DebugHighlightProcNode(getCoords());
				for(ExprNode param : params.getChildren()) {
					highlight.addExpression(param);
				}
				result = highlight;
			} else {
				reportError("Debug::highlight() takes an odd number of parameters, first the message, then a series of pairs of the value to highlight followed by its annotation.");
				return false;				
			}
		}
		else if(procedureName.equals("addCopy")) {
			if(params.size() == 1) {
				result = new GraphAddCopyNodeProcNode(getCoords(), params.get(0));
			} else if(params.size() == 3) {
				result = new GraphAddCopyEdgeProcNode(getCoords(), params.get(0), params.get(1), params.get(2));
			} else {
				reportError(procedureName + "() takes 1 or 3 parameters.");
				return false;
			}
		}
		else if(procedureName.equals("merge")) {
			if(params.size() < 2 || params.size() > 3) {
				reportError("merge(target,source,oldSourceName) takes two or three parameters.");
				return false;
			}
			else {
				if(params.size() == 2)
					result = new GraphMergeProcNode(getCoords(), params.get(0), params.get(1), null);
				else
					result = new GraphMergeProcNode(getCoords(), params.get(0), params.get(1), params.get(2));
			}
		}
		else if(procedureName.equals("redirectSource")) {
			if(params.size() < 2 || params.size() > 3) {
				reportError("redirectSource(edge,newSource,oldSourceName) takes two or three parameters.");
				return false;
			}
			else {
				if(params.size() == 2)
					result = new GraphRedirectSourceProcNode(getCoords(), params.get(0), params.get(1), null);
				else
					result = new GraphRedirectSourceProcNode(getCoords(), params.get(0), params.get(1), params.get(2));
			}
		}
		else if(procedureName.equals("redirectTarget")) {
			if(params.size() < 2 || params.size() > 3) {
				reportError("redirectTarget(edge,newTarget,oldTargetName) takes two or three parameters.");
				return false;
			}
			else {
				if(params.size() == 2)
					result = new GraphRedirectTargetProcNode(getCoords(), params.get(0), params.get(1), null);
				else
					result = new GraphRedirectTargetProcNode(getCoords(), params.get(0), params.get(1), params.get(2));
			}
		}
		else if(procedureName.equals("redirectSourceAndTarget")) {
			if(params.size() != 3 && params.size() != 5) {
				reportError("redirectSourceAndTarget(edge,newSource,newTarget,oldSourceName,oldTargetName) takes three or five parameters.");
				return false;
			}
			else {
				if(params.size() == 3)
					result = new GraphRedirectSourceAndTargetProcNode(getCoords(), params.get(0), params.get(1), params.get(2), null, null);
				else
					result = new GraphRedirectSourceAndTargetProcNode(getCoords(), params.get(0), params.get(1), params.get(2), params.get(3), params.get(4));
			}
		}
		else if(procedureName.equals("pauseTransaction")) {
			if(params.size() != 0) {
				reportError("Transaction::pause() takes no parameters.");
				return false;
			}
			else {
				result = new PauseTransactionProcNode(getCoords());
			}
		}
		else if(procedureName.equals("resumeTransaction")) {
			if(params.size() != 0) {
				reportError("Transaction::resume() takes no parameters.");
				return false;
			}
			else {
				result = new ResumeTransactionProcNode(getCoords());
			}
		}
		else if(procedureName.equals("commitTransaction")) {
			if(params.size() != 1) {
				reportError("Transaction::commit(transactionId) takes one parameter.");
				return false;
			}
			else {
				result = new CommitTransactionProcNode(getCoords(), params.get(0));
			}
		}
		else if(procedureName.equals("rollbackTransaction")) {
			if(params.size() != 1) {
				reportError("Transaction::rollback(transactionId) takes one parameter.");
				return false;
			}
			else {
				result = new RollbackTransactionProcNode(getCoords(), params.get(0));
			}
		}
		else {
			reportError("no computation " +procedureName + " known");
			return false;
		}
		return true;
	}

	@Override
	protected boolean checkLocal() {
		if((context&BaseNode.CONTEXT_FUNCTION_OR_PROCEDURE)==BaseNode.CONTEXT_FUNCTION
				&& !procedureIdent.toString().equals("emit")
				&& !procedureIdent.toString().equals("addDebug")
				&& !procedureIdent.toString().equals("remDebug")
				&& !procedureIdent.toString().equals("emitDebug")
				&& !procedureIdent.toString().equals("haltDebug")
				&& !procedureIdent.toString().equals("highlightDebug")) {
			reportError("procedure call not allowed in function or lhs context (built-in-procedure)");
			return false;
		}
		return true;
	}

	public boolean checkStatementLocal(boolean isLHS, DeclNode root, EvalStatementNode enclosingLoop) {
		return true;
	}

	protected ProcedureInvocationBaseNode getResult() {
		return result;
	}

	public Vector<TypeNode> getType() {
		return result.getType();
	}

	public int getNumReturnTypes() {
		return result.getType().size();
	}
	
	public String getProcedureName() {
		return procedureIdent.toString();
	}

	@Override
	protected IR constructIR() {
		return result.getIR();
	}
}
