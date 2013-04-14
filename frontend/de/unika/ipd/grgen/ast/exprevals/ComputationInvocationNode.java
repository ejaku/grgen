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

public class ComputationInvocationNode extends ComputationInvocationBaseNode
{
	static {
		setName(ComputationInvocationNode.class, "computation invocation");
	}

	static TypeNode computationTypeNode = new ComputationTypeNode();
	
	private IdentNode computationIdent;
	private CollectNode<ExprNode> params;
	private ComputationInvocationBaseNode result;

	ParserEnvironment env;

	public ComputationInvocationNode(IdentNode computationIdent, CollectNode<ExprNode> params, ParserEnvironment env)
	{
		super(computationIdent.getCoords());
		this.computationIdent = becomeParent(computationIdent);
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
		String computationName = computationIdent.toString();

		if(computationName.equals("add")) {
			if(params.size() == 1) {
				result = new GraphAddNodeCompNode(getCoords(), params.get(0));
			} else if(params.size() == 3) {
				result = new GraphAddEdgeCompNode(getCoords(), params.get(0), params.get(1), params.get(2));
			} else {
				reportError(computationName + "() takes 1 or 3 parameters.");
				return false;
			}
		}
		else if(computationName.equals("retype")) {
			if(params.size() == 2) {
				result = new GraphRetypeCompNode(getCoords(), params.get(0), params.get(1));
			} else {
				reportError(computationName + "() takes 2 parameters.");
				return false;
			}
		}
		else if(computationName.equals("insertInduced")) {
			if(params.size() != 2) {
				reportError("insertInduced(.,.) takes two parameters.");
				return false;
			}
			else
				result = new InsertInducedSubgraphCompNode(getCoords(), params.get(0), params.get(1));
		}
		else if(computationName.equals("insertDefined")) {
			if(params.size() != 2) {
				reportError("insertDefined(.,.) takes two parameters.");
				return false;
			}
			else
				result = new InsertDefinedSubgraphCompNode(getCoords(), params.get(0), params.get(1));
		}
		else if(computationName.equals("valloc")) {
			if(params.size() != 0) {
				reportError("valloc() takes no parameters.");
				return false;
			}
			else
				result = new VAllocCompNode(getCoords());
		}
		else if(computationName.equals("startTransaction")) {
			if(params.size() != 0) {
				reportError("startTransaction() takes no parameters.");
				return false;
			}
			else
				result = new StartTransactionCompNode(getCoords());
		}
		else if(computationName.equals("rem")) {
			if(params.size() != 1) {
				reportError("rem(value) takes one parameter.");
				return false;
			}
			else {
				result = new GraphRemoveCompNode(getCoords(), params.get(0));
			}
		}
		else if(computationName.equals("clear")) {
			if(params.size() != 0) {
				reportError("clear() takes no parameters.");
				return false;
			}
			else {
				result = new GraphClearCompNode(getCoords());
			}
		}
		else if(computationName.equals("vfree")) {
			if(params.size() != 1) {
				reportError("vfree(value) takes one parameter.");
				return false;
			}
			else {
				result = new VFreeCompNode(getCoords(), params.get(0));
			}
		}
		else if(computationName.equals("vfreenonreset")) {
			if(params.size() != 1) {
				reportError("vfreenonreset(value) takes one parameter.");
				return false;
			}
			else {
				result = new VFreeNonResetCompNode(getCoords(), params.get(0));
			}
		}
		else if(computationName.equals("vreset")) {
			if(params.size() != 1) {
				reportError("vreset(value) takes one parameter.");
				return false;
			}
			else {
				result = new VResetCompNode(getCoords(), params.get(0));
			}
		}
		else if(computationName.equals("record")) {
			if(params.size() != 1) {
				reportError("record(value) takes one parameter.");
				return false;
			}
			else {
				result = new RecordCompNode(getCoords(), params.get(0));
			}
		}
		else if(computationName.equals("emit")) {
			if(params.size() != 1) {
				reportError("emit(value) takes one parameter.");
				return false;
			}
			else {
				result = new EmitCompNode(getCoords(), params.get(0));
			}
		}
		else if(computationName.equals("highlight")) {
			HighlightCompNode highlight = new HighlightCompNode(getCoords());
			for(ExprNode param : params.getChildren()) {
				highlight.addExpressionToHighlight(param);
			}
			result = highlight;
		}
		else if(computationName.equals("merge")) {
			if(params.size() < 2 || params.size() > 3) {
				reportError("merge(target,source,oldSourceName) takes two or three parameters.");
				return false;
			}
			else {
				if(params.size() == 2)
					result = new GraphMergeCompNode(getCoords(), params.get(0), params.get(1), null);
				else
					result = new GraphMergeCompNode(getCoords(), params.get(0), params.get(1), params.get(2));
			}
		}
		else if(computationName.equals("redirectSource")) {
			if(params.size() < 2 || params.size() > 3) {
				reportError("redirectSource(edge,newSource,oldSourceName) takes two or three parameters.");
				return false;
			}
			else {
				if(params.size() == 2)
					result = new GraphRedirectSourceCompNode(getCoords(), params.get(0), params.get(1), null);
				else
					result = new GraphRedirectSourceCompNode(getCoords(), params.get(0), params.get(1), params.get(2));
			}
		}
		else if(computationName.equals("redirectTarget")) {
			if(params.size() < 2 || params.size() > 3) {
				reportError("redirectTarget(edge,newTarget,oldTargetName) takes two or three parameters.");
				return false;
			}
			else {
				if(params.size() == 2)
					result = new GraphRedirectTargetCompNode(getCoords(), params.get(0), params.get(1), null);
				else
					result = new GraphRedirectTargetCompNode(getCoords(), params.get(0), params.get(1), params.get(2));
			}
		}
		else if(computationName.equals("redirectSourceAndTarget")) {
			if(params.size() != 3 && params.size() != 5) {
				reportError("redirectSourceAndTarget(edge,newSource,newTarget,oldSourceName,oldTargetName) takes three or five parameters.");
				return false;
			}
			else {
				if(params.size() == 3)
					result = new GraphRedirectSourceAndTargetCompNode(getCoords(), params.get(0), params.get(1), params.get(2), null, null);
				else
					result = new GraphRedirectSourceAndTargetCompNode(getCoords(), params.get(0), params.get(1), params.get(2), params.get(3), params.get(4));
			}
		}
		else if(computationName.equals("pauseTransaction")) {
			if(params.size() != 0) {
				reportError("pauseTransaction() takes no parameters.");
				return false;
			}
			else {
				result = new PauseTransactionCompNode(getCoords());
			}
		}
		else if(computationName.equals("resumeTransaction")) {
			if(params.size() != 0) {
				reportError("resumeTransaction() takes no parameters.");
				return false;
			}
			else {
				result = new ResumeTransactionCompNode(getCoords());
			}
		}
		else if(computationName.equals("commitTransaction")) {
			if(params.size() != 1) {
				reportError("commitTransaction(transactionId) takes one parameter.");
				return false;
			}
			else {
				result = new CommitTransactionCompNode(getCoords(), params.get(0));
			}
		}
		else if(computationName.equals("rollbackTransaction")) {
			if(params.size() != 1) {
				reportError("rollbackTransaction(transactionId) takes one parameter.");
				return false;
			}
			else {
				result = new RollbackTransactionCompNode(getCoords(), params.get(0));
			}
		}
		else {
			reportError("no computation " +computationName + " known");
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

	protected ComputationInvocationBaseNode getResult() {
		return result;
	}

	public Vector<TypeNode> getType() {
		return result.getType();
	}

	public int getNumReturnTypes() {
		return result.getType().size();
	}

	@Override
	protected IR constructIR() {
		return result.getIR();
	}
}
