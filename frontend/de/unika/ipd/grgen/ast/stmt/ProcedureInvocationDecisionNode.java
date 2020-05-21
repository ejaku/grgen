/*
 * GrGen: graph rewrite generator tool -- release GrGen.NET 5.0
 * Copyright (C) 2003-2020 Universitaet Karlsruhe, Institut fuer Programmstrukturen und Datenorganisation, LS Goos; and free programmers
 * licensed under LGPL v3 (see LICENSE.txt included in the packaging of this file)
 * www.grgen.net
 */

/**
 * @author Edgar Jakumeit
 */

package de.unika.ipd.grgen.ast.stmt;

import java.util.Collection;
import java.util.Vector;

import de.unika.ipd.grgen.ast.*;
import de.unika.ipd.grgen.ast.expr.ExprNode;
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
import de.unika.ipd.grgen.ast.typedecl.ProcedureTypeNode;
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
		String procedureName = procedureIdent.toString();

		if(procedureName.equals("add")) {
			if(arguments.size() == 1) {
				result = new GraphAddNodeProcNode(getCoords(), arguments.get(0));
			} else if(arguments.size() == 3) {
				result = new GraphAddEdgeProcNode(getCoords(), arguments.get(0), arguments.get(1), arguments.get(2));
			} else {
				reportError(procedureName + "() takes 1 or 3 parameters.");
				return false;
			}
		} else if(procedureName.equals("retype")) {
			if(arguments.size() == 2) {
				result = new GraphRetypeProcNode(getCoords(), arguments.get(0), arguments.get(1));
			} else {
				reportError(procedureName + "() takes 2 parameters.");
				return false;
			}
		} else if(procedureName.equals("insert")) {
			if(arguments.size() != 1) {
				reportError("insert(.) takes one parameter.");
				return false;
			} else
				result = new InsertProcNode(getCoords(), arguments.get(0));
		} else if(procedureName.equals("insertCopy")) {
			if(arguments.size() != 2) {
				reportError("insertCopy(.,.) takes two parameters.");
				return false;
			} else
				result = new InsertCopyProcNode(getCoords(), arguments.get(0), arguments.get(1));
		} else if(procedureName.equals("insertInduced")) {
			if(arguments.size() != 2) {
				reportError("insertInduced(.,.) takes two parameters.");
				return false;
			} else
				result = new InsertInducedSubgraphProcNode(getCoords(), arguments.get(0), arguments.get(1));
		} else if(procedureName.equals("insertDefined")) {
			if(arguments.size() != 2) {
				reportError("insertDefined(.,.) takes two parameters.");
				return false;
			} else
				result = new InsertDefinedSubgraphProcNode(getCoords(), arguments.get(0), arguments.get(1));
		} else if(procedureName.equals("valloc")) {
			if(arguments.size() != 0) {
				reportError("valloc() takes no parameters.");
				return false;
			} else
				result = new VAllocProcNode(getCoords());
		} else if(procedureName.equals("startTransaction")) {
			if(arguments.size() != 0) {
				reportError("Transaction::start() takes no parameters.");
				return false;
			} else
				result = new StartTransactionProcNode(getCoords());
		} else if(procedureName.equals("rem")) {
			if(arguments.size() != 1) {
				reportError("rem(value) takes one parameter.");
				return false;
			} else {
				result = new GraphRemoveProcNode(getCoords(), arguments.get(0));
			}
		} else if(procedureName.equals("clear")) {
			if(arguments.size() != 0) {
				reportError("clear() takes no parameters.");
				return false;
			} else {
				result = new GraphClearProcNode(getCoords());
			}
		} else if(procedureName.equals("vfree")) {
			if(arguments.size() != 1) {
				reportError("vfree(value) takes one parameter.");
				return false;
			} else {
				result = new VFreeProcNode(getCoords(), arguments.get(0));
			}
		} else if(procedureName.equals("vfreenonreset")) {
			if(arguments.size() != 1) {
				reportError("vfreenonreset(value) takes one parameter.");
				return false;
			} else {
				result = new VFreeNonResetProcNode(getCoords(), arguments.get(0));
			}
		} else if(procedureName.equals("vreset")) {
			if(arguments.size() != 1) {
				reportError("vreset(value) takes one parameter.");
				return false;
			} else {
				result = new VResetProcNode(getCoords(), arguments.get(0));
			}
		} else if(procedureName.equals("record")) {
			if(arguments.size() != 1) {
				reportError("record(value) takes one parameter.");
				return false;
			} else {
				result = new RecordProcNode(getCoords(), arguments.get(0));
			}
		} else if(procedureName.equals("emit")) {
			if(arguments.size() >= 1) {
				EmitProcNode emit = new EmitProcNode(getCoords(), false);
				for(ExprNode param : arguments.getChildren()) {
					emit.addExpression(param);
				}
				result = emit;
			} else {
				reportError("emit() takes at least one parameter.");
				return false;
			}
		} else if(procedureName.equals("emitdebug")) {
			if(arguments.size() >= 1) {
				EmitProcNode emit = new EmitProcNode(getCoords(), true);
				for(ExprNode param : arguments.getChildren()) {
					emit.addExpression(param);
				}
				result = emit;
			} else {
				reportError("emitdebug() takes at least one parameter.");
				return false;
			}
		} else if(procedureName.equals("exportFile")) {
			if(arguments.size() == 1) {
				result = new ExportProcNode(getCoords(), arguments.get(0), null);
			} else if(arguments.size() == 2) {
				result = new ExportProcNode(getCoords(), arguments.get(1), arguments.get(0));
			} else {
				reportError("File::export() takes 1 (filepath) or 2 (graph, filepath) parameters.");
				return false;
			}
		} else if(procedureName.equals("deleteFile")) {
			if(arguments.size() == 1) {
				result = new DeleteFileProcNode(getCoords(), arguments.get(0));
			} else {
				reportError("File::delete() takes 1 (filepath) parameters.");
				return false;
			}
		} else if(procedureName.equals("addDebug")) {
			if(arguments.size() >= 1) {
				DebugAddProcNode add = new DebugAddProcNode(getCoords());
				for(ExprNode param : arguments.getChildren()) {
					add.addExpression(param);
				}
				result = add;
			} else {
				reportError("Debug::add() takes at least one parameter, the message/computation entered.");
				return false;
			}
		} else if(procedureName.equals("remDebug")) {
			if(arguments.size() >= 1) {
				DebugRemProcNode rem = new DebugRemProcNode(getCoords());
				for(ExprNode param : arguments.getChildren()) {
					rem.addExpression(param);
				}
				result = rem;
			} else {
				reportError("Debug::rem() takes at least one parameter, the message/computation left.");
				return false;
			}
		} else if(procedureName.equals("emitDebug")) {
			if(arguments.size() >= 1) {
				DebugEmitProcNode emit = new DebugEmitProcNode(getCoords());
				for(ExprNode param : arguments.getChildren()) {
					emit.addExpression(param);
				}
				result = emit;
			} else {
				reportError("Debug::emit() takes at least one parameter, the message to report.");
				return false;
			}
		} else if(procedureName.equals("haltDebug")) {
			if(arguments.size() >= 1) {
				DebugHaltProcNode halt = new DebugHaltProcNode(getCoords());
				for(ExprNode param : arguments.getChildren()) {
					halt.addExpression(param);
				}
				result = halt;
			} else {
				reportError("Debug::halt() takes at least one parameter, the message to report.");
				return false;
			}
		} else if(procedureName.equals("highlightDebug")) {
			if(arguments.size() % 2 == 1) {
				DebugHighlightProcNode highlight = new DebugHighlightProcNode(getCoords());
				for(ExprNode param : arguments.getChildren()) {
					highlight.addExpression(param);
				}
				result = highlight;
			} else {
				reportError("Debug::highlight() takes an odd number of parameters, first the message, then a series of pairs of the value to highlight followed by its annotation.");
				return false;
			}
		} else if(procedureName.equals("addCopy")) {
			if(arguments.size() == 1) {
				result = new GraphAddCopyNodeProcNode(getCoords(), arguments.get(0));
			} else if(arguments.size() == 3) {
				result = new GraphAddCopyEdgeProcNode(getCoords(), arguments.get(0), arguments.get(1), arguments.get(2));
			} else {
				reportError(procedureName + "() takes 1 or 3 parameters.");
				return false;
			}
		} else if(procedureName.equals("merge")) {
			if(arguments.size() < 2 || arguments.size() > 3) {
				reportError("merge(target,source,oldSourceName) takes two or three parameters.");
				return false;
			} else {
				if(arguments.size() == 2)
					result = new GraphMergeProcNode(getCoords(), arguments.get(0), arguments.get(1), null);
				else
					result = new GraphMergeProcNode(getCoords(), arguments.get(0), arguments.get(1), arguments.get(2));
			}
		} else if(procedureName.equals("redirectSource")) {
			if(arguments.size() < 2 || arguments.size() > 3) {
				reportError("redirectSource(edge,newSource,oldSourceName) takes two or three parameters.");
				return false;
			} else {
				if(arguments.size() == 2)
					result = new GraphRedirectSourceProcNode(getCoords(), arguments.get(0), arguments.get(1), null);
				else
					result = new GraphRedirectSourceProcNode(getCoords(), arguments.get(0), arguments.get(1), arguments.get(2));
			}
		} else if(procedureName.equals("redirectTarget")) {
			if(arguments.size() < 2 || arguments.size() > 3) {
				reportError("redirectTarget(edge,newTarget,oldTargetName) takes two or three parameters.");
				return false;
			} else {
				if(arguments.size() == 2)
					result = new GraphRedirectTargetProcNode(getCoords(), arguments.get(0), arguments.get(1), null);
				else
					result = new GraphRedirectTargetProcNode(getCoords(), arguments.get(0), arguments.get(1), arguments.get(2));
			}
		} else if(procedureName.equals("redirectSourceAndTarget")) {
			if(arguments.size() != 3 && arguments.size() != 5) {
				reportError("redirectSourceAndTarget(edge,newSource,newTarget,oldSourceName,oldTargetName) takes three or five parameters.");
				return false;
			} else {
				if(arguments.size() == 3)
					result = new GraphRedirectSourceAndTargetProcNode(getCoords(), arguments.get(0), arguments.get(1),
							arguments.get(2), null, null);
				else
					result = new GraphRedirectSourceAndTargetProcNode(getCoords(), arguments.get(0), arguments.get(1),
							arguments.get(2), arguments.get(3), arguments.get(4));
			}
		} else if(procedureName.equals("pauseTransaction")) {
			if(arguments.size() != 0) {
				reportError("Transaction::pause() takes no parameters.");
				return false;
			} else {
				result = new PauseTransactionProcNode(getCoords());
			}
		} else if(procedureName.equals("resumeTransaction")) {
			if(arguments.size() != 0) {
				reportError("Transaction::resume() takes no parameters.");
				return false;
			} else {
				result = new ResumeTransactionProcNode(getCoords());
			}
		} else if(procedureName.equals("commitTransaction")) {
			if(arguments.size() != 1) {
				reportError("Transaction::commit(transactionId) takes one parameter.");
				return false;
			} else {
				result = new CommitTransactionProcNode(getCoords(), arguments.get(0));
			}
		} else if(procedureName.equals("rollbackTransaction")) {
			if(arguments.size() != 1) {
				reportError("Transaction::rollback(transactionId) takes one parameter.");
				return false;
			} else {
				result = new RollbackTransactionProcNode(getCoords(), arguments.get(0));
			}
		} else {
			reportError("no computation " + procedureName + " known");
			return false;
		}
		return true;
	}

	@Override
	protected boolean checkLocal()
	{
		if((context & BaseNode.CONTEXT_FUNCTION_OR_PROCEDURE) == BaseNode.CONTEXT_FUNCTION
				&& !procedureIdent.toString().equals("emit")
				&& !procedureIdent.toString().equals("emitdebug")
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
