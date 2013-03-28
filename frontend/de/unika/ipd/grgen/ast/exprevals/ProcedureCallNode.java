/*
 * GrGen: graph rewrite generator tool -- release GrGen.NET 3.6
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
import de.unika.ipd.grgen.parser.Coords;

public class ProcedureCallNode extends EvalStatementNode
{
	static {
		setName(ProcedureCallNode.class, "procedure call eval statement");
	}

	private String procedureName;
	private CollectNode<ExprNode> params;
	private EvalStatementNode result;

	public ProcedureCallNode(Coords coords, String procedureName, CollectNode<ExprNode> params)
	{
		super(coords);
		this.procedureName = procedureName;
		this.params = becomeParent(params);
	}

	@Override
	public Collection<? extends BaseNode> getChildren() {
		Vector<BaseNode> children = new Vector<BaseNode>();
		children.add(params);
		if(isResolved())
			children.add(result);
		return children;
	}

	@Override
	public Collection<String> getChildrenNames() {
		Vector<String> childrenNames = new Vector<String>();
		childrenNames.add("params");
		if(isResolved())
			childrenNames.add("result");
		return childrenNames;
	}

	protected boolean resolveLocal() {
		if(procedureName.equals("rem")) {
			if(params.size() != 1) {
				reportError("rem(value) takes one parameter.");
				return false;
			}
			else {
				result = new GraphRemoveNode(getCoords(), params.get(0));
			}
		}
		else if(procedureName.equals("clear")) {
			if(params.size() != 0) {
				reportError("clear() takes no parameters.");
				return false;
			}
			else {
				result = new GraphClearNode(getCoords());
			}
		}
		else if(procedureName.equals("vfree")) {
			if(params.size() != 1) {
				reportError("vfree(value) takes one parameter.");
				return false;
			}
			else {
				result = new VFreeStatementNode(getCoords(), params.get(0));
			}
		}
		else if(procedureName.equals("vfreenonreset")) {
			if(params.size() != 1) {
				reportError("vfreenonreset(value) takes one parameter.");
				return false;
			}
			else {
				result = new VFreeNonResetStatementNode(getCoords(), params.get(0));
			}
		}
		else if(procedureName.equals("vreset")) {
			if(params.size() != 1) {
				reportError("vreset(value) takes one parameter.");
				return false;
			}
			else {
				result = new VResetStatementNode(getCoords(), params.get(0));
			}
		}
		else if(procedureName.equals("record")) {
			if(params.size() != 1) {
				reportError("record(value) takes one parameter.");
				return false;
			}
			else {
				result = new RecordStatementNode(getCoords(), params.get(0));
			}
		}
		else if(procedureName.equals("emit")) {
			if(params.size() != 1) {
				reportError("emit(value) takes one parameter.");
				return false;
			}
			else {
				result = new EmitStatementNode(getCoords(), params.get(0));
			}
		}
		else if(procedureName.equals("merge")) {
			if(params.size() < 2 || params.size() > 3) {
				reportError("merge(target,source,oldSourceName) takes two or three parameters.");
				return false;
			}
			else {
				if(params.size() == 2)
					result = new GraphMergeNode(getCoords(), params.get(0), params.get(1), null);
				else
					result = new GraphMergeNode(getCoords(), params.get(0), params.get(1), params.get(2));
			}
		}
		else if(procedureName.equals("redirectSource")) {
			if(params.size() < 2 || params.size() > 3) {
				reportError("redirectSource(edge,newSource,oldSourceName) takes two or three parameters.");
				return false;
			}
			else {
				if(params.size() == 2)
					result = new GraphRedirectSourceNode(getCoords(), params.get(0), params.get(1), null);
				else
					result = new GraphRedirectSourceNode(getCoords(), params.get(0), params.get(1), params.get(2));
			}
		}
		else if(procedureName.equals("redirectTarget")) {
			if(params.size() < 2 || params.size() > 3) {
				reportError("redirectTarget(edge,newTarget,oldTargetName) takes two or three parameters.");
				return false;
			}
			else {
				if(params.size() == 2)
					result = new GraphRedirectTargetNode(getCoords(), params.get(0), params.get(1), null);
				else
					result = new GraphRedirectTargetNode(getCoords(), params.get(0), params.get(1), params.get(2));
			}
		}
		else if(procedureName.equals("redirectSourceAndTarget")) {
			if(params.size() != 3 && params.size() != 5) {
				reportError("redirectSourceAndTarget(edge,newSource,newTarget,oldSourceName,oldTargetName) takes three or five parameters.");
				return false;
			}
			else {
				if(params.size() == 3)
					result = new GraphRedirectSourceAndTargetNode(getCoords(), params.get(0), params.get(1), params.get(2), null, null);
				else
					result = new GraphRedirectSourceAndTargetNode(getCoords(), params.get(0), params.get(1), params.get(2), params.get(3), params.get(4));
			}
		}
		else if(procedureName.equals("pauseTransaction")) {
			if(params.size() != 0) {
				reportError("pauseTransaction() takes no parameters.");
				return false;
			}
			else {
				result = new PauseTransactionNode(getCoords());
			}
		}
		else if(procedureName.equals("resumeTransaction")) {
			if(params.size() != 0) {
				reportError("resumeTransaction() takes no parameters.");
				return false;
			}
			else {
				result = new ResumeTransactionNode(getCoords());
			}
		}
		else if(procedureName.equals("commitTransaction")) {
			if(params.size() != 1) {
				reportError("commitTransaction(transactionId) takes one parameter.");
				return false;
			}
			else {
				result = new CommitTransactionNode(getCoords(), params.get(0));
			}
		}
		else if(procedureName.equals("rollbackTransaction")) {
			if(params.size() != 1) {
				reportError("rollbackTransaction(transactionId) takes one parameter.");
				return false;
			}
			else {
				result = new RollbackTransactionNode(getCoords(), params.get(0));
			}
		}
		else {
			reportError("no procedure named \"" + procedureName + "\" known");
			return false;
		}

		return true;
	}

	@Override
	protected boolean checkLocal() {
		return true;
	}

	@Override
	protected IR constructIR() {
		return result.getIR();
	}
}
