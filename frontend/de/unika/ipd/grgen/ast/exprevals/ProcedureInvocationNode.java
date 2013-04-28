/*
 * GrGen: graph rewrite generator tool -- release GrGen.NET 4.0
 * Copyright (C) 2003-2013 Universitaet Karlsruhe, Institut fuer Programmstrukturen und Datenorganisation, LS Goos; and free programmers
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

	ParserEnvironment env;

	public ProcedureInvocationNode(IdentNode procedureIdent, CollectNode<ExprNode> params, ParserEnvironment env)
	{
		super(procedureIdent.getCoords());
		this.procedureIdent = becomeParent(procedureIdent);
		this.params = becomeParent(params);
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
				reportError("startTransaction() takes no parameters.");
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
		else if(procedureName.equals("highlight")) {
			HighlightProcNode highlight = new HighlightProcNode(getCoords());
			for(ExprNode param : params.getChildren()) {
				highlight.addExpressionToHighlight(param);
			}
			result = highlight;
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
				reportError("pauseTransaction() takes no parameters.");
				return false;
			}
			else {
				result = new PauseTransactionProcNode(getCoords());
			}
		}
		else if(procedureName.equals("resumeTransaction")) {
			if(params.size() != 0) {
				reportError("resumeTransaction() takes no parameters.");
				return false;
			}
			else {
				result = new ResumeTransactionProcNode(getCoords());
			}
		}
		else if(procedureName.equals("commitTransaction")) {
			if(params.size() != 1) {
				reportError("commitTransaction(transactionId) takes one parameter.");
				return false;
			}
			else {
				result = new CommitTransactionProcNode(getCoords(), params.get(0));
			}
		}
		else if(procedureName.equals("rollbackTransaction")) {
			if(params.size() != 1) {
				reportError("rollbackTransaction(transactionId) takes one parameter.");
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
