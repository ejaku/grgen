/*
 * GrGen: graph rewrite generator tool -- release GrGen.NET 4.4
 * Copyright (C) 2003-2014 Universitaet Karlsruhe, Institut fuer Programmstrukturen und Datenorganisation, LS Goos; and free programmers
 * licensed under LGPL v3 (see LICENSE.txt included in the packaging of this file)
 * www.grgen.net
 */

/**
 * @author Sebastian Hack, Edgar Jakumeit
 */
package de.unika.ipd.grgen.ast.exprevals;

import java.util.Collection;
import java.util.Vector;

import de.unika.ipd.grgen.ast.*;
import de.unika.ipd.grgen.ir.exprevals.AssignmentBase;
import de.unika.ipd.grgen.ir.exprevals.ProcedureInvocationBase;
import de.unika.ipd.grgen.ir.exprevals.ReturnAssignment;
import de.unika.ipd.grgen.ir.IR;
import de.unika.ipd.grgen.parser.Coords;

/**
 * AST node representing an assignment of procedure invocation return values.
 */
public class ReturnAssignmentNode extends EvalStatementNode {
	static {
		setName(ReturnAssignmentNode.class, "Return Assign");
	}

	ProcedureOrExternalProcedureInvocationNode procedure;
	ProcedureInvocationNode builtinProcedure;
	ProcedureMethodInvocationDecisionNode procedureMethod;
	CollectNode<EvalStatementNode> targets;
	int context;
	
	public ReturnAssignmentNode(Coords coords, ProcedureOrExternalProcedureInvocationNode procedure,
			CollectNode<EvalStatementNode> targets, int context) {
		super(coords);
		this.procedure = procedure;
		becomeParent(this.procedure);
		this.targets = targets;
		becomeParent(this.targets);
		this.context = context;
	}

	public ReturnAssignmentNode(Coords coords, ProcedureInvocationNode builtinProcedure,
			CollectNode<EvalStatementNode> targets, int context) {
		super(coords);
		this.builtinProcedure = builtinProcedure;
		becomeParent(this.builtinProcedure);
		this.targets = targets;
		becomeParent(this.targets);
		this.context = context;
	}

	public ReturnAssignmentNode(Coords coords, ProcedureMethodInvocationDecisionNode procedureMethod,
			CollectNode<EvalStatementNode> targets, int context) {
		super(coords);
		this.procedureMethod = procedureMethod;
		becomeParent(this.builtinProcedure);
		this.targets = targets;
		becomeParent(this.targets);
		this.context = context;
	}

	/** returns children of this node */
	@Override
	public Collection<BaseNode> getChildren() {
		Vector<BaseNode> children = new Vector<BaseNode>();
		children.add(procedure!=null ? procedure : builtinProcedure != null ? builtinProcedure : procedureMethod);
		children.add(targets);
		return children;
	}

	/** returns names of the children, same order as in getChildren */
	@Override
	public Collection<String> getChildrenNames() {
		Vector<String> childrenNames = new Vector<String>();
		childrenNames.add("lhs");
		childrenNames.add("rhs");
		return childrenNames;
	}

	/** @see de.unika.ipd.grgen.ast.BaseNode#resolveLocal() */
	@Override
	protected boolean resolveLocal() {
		return true;
	}

	/** @see de.unika.ipd.grgen.ast.BaseNode#checkLocal() */
	@Override
	protected boolean checkLocal() {
		// targets is one of AssignNode, AssignVisitedNode, AssignIndexedNode
		// with QualIdentNode or IdentExprNode as owner/target
		// or a ConnectionNode or a SingleNodeConnNode or a VarDeclNode
		// and finally a projection expr node as source -- maybe with a cast prefix after type adjust
		if(procedure!=null) {
			if(targets.children.size() != procedure.getNumReturnTypes() && targets.children.size()!=0) {
				procedure.reportError("Expected " + procedure.getNumReturnTypes() + " procedure return variables, given " + targets.children.size());
				return false;
			}
		} else if(builtinProcedure!=null) {
			if(targets.children.size() != builtinProcedure.getNumReturnTypes() && targets.children.size()!=0) {
				builtinProcedure.reportError("Expected " + builtinProcedure.getNumReturnTypes() + " procedure return variables, given " + targets.children.size());
				return false;
			}
		} else { //procedureMethod!=null
			if(targets.children.size() != procedureMethod.getNumReturnTypes() && targets.children.size()!=0) {
				procedureMethod.reportError("Expected " + procedureMethod.getNumReturnTypes() + " procedure return variables, given " + targets.children.size());
				return false;
			}			
		}
		// hint: the types are checked in the singular assignments
		return true;
	}

	public boolean checkStatementLocal(boolean isLHS, DeclNode root, EvalStatementNode enclosingLoop) {
		return true;
	}

	/**
	 * Construct the immediate representation from an assignment node.
	 * @see de.unika.ipd.grgen.ast.BaseNode#constructIR()
	 */
	@Override
	protected IR constructIR() {	
		ReturnAssignment retAssign;
		if(procedure != null) {
			retAssign = new ReturnAssignment(
					procedure.checkIR(ProcedureInvocationBase.class));
		} else if(builtinProcedure != null) {
			retAssign = new ReturnAssignment(
					builtinProcedure.checkIR(ProcedureInvocationBase.class));
		} else {
			retAssign = new ReturnAssignment(
					procedureMethod.checkIR(ProcedureInvocationBase.class));
		}
		for(EvalStatementNode target : targets.getChildren()) {
			retAssign.addAssignment(target.checkIR(AssignmentBase.class));
		}
		return retAssign;
	}
}
