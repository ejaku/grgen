/*
 * GrGen: graph rewrite generator tool -- release GrGen.NET 5.0
 * Copyright (C) 2003-2020 Universitaet Karlsruhe, Institut fuer Programmstrukturen und Datenorganisation, LS Goos; and free programmers
 * licensed under LGPL v3 (see LICENSE.txt included in the packaging of this file)
 * www.grgen.net
 */

/**
 * @author Sebastian Hack, Edgar Jakumeit
 */
package de.unika.ipd.grgen.ast.stmt;

import java.util.Collection;
import java.util.Vector;

import de.unika.ipd.grgen.ast.*;
import de.unika.ipd.grgen.ast.decl.DeclNode;
import de.unika.ipd.grgen.ast.stmt.invocation.ProcedureInvocationDecisionNode;
import de.unika.ipd.grgen.ast.stmt.invocation.ProcedureMethodInvocationDecisionNode;
import de.unika.ipd.grgen.ast.stmt.invocation.ProcedureOrExternalProcedureInvocationNode;
import de.unika.ipd.grgen.ir.stmt.AssignmentBase;
import de.unika.ipd.grgen.ir.stmt.ReturnAssignment;
import de.unika.ipd.grgen.ir.stmt.invocation.ProcedureOrBuiltinProcedureInvocationBase;
import de.unika.ipd.grgen.ir.IR;
import de.unika.ipd.grgen.parser.Coords;

/**
 * AST node representing an assignment of procedure invocation return values.
 */
public class ReturnAssignmentNode extends EvalStatementNode
{
	static {
		setName(ReturnAssignmentNode.class, "Return Assign");
	}

	ProcedureOrExternalProcedureInvocationNode procedure;
	ProcedureInvocationDecisionNode builtinProcedure;
	ProcedureMethodInvocationDecisionNode procedureMethod;
	CollectNode<EvalStatementNode> targets;
	int context;

	public ReturnAssignmentNode(Coords coords, ProcedureOrExternalProcedureInvocationNode procedure,
			CollectNode<EvalStatementNode> targets, int context)
	{
		super(coords);
		this.procedure = procedure;
		becomeParent(this.procedure);
		this.targets = targets;
		becomeParent(this.targets);
		this.context = context;
	}

	public ReturnAssignmentNode(Coords coords, ProcedureInvocationDecisionNode builtinProcedure,
			CollectNode<EvalStatementNode> targets, int context)
	{
		super(coords);
		this.builtinProcedure = builtinProcedure;
		becomeParent(this.builtinProcedure);
		this.targets = targets;
		becomeParent(this.targets);
		this.context = context;
	}

	public ReturnAssignmentNode(Coords coords, ProcedureMethodInvocationDecisionNode procedureMethod,
			CollectNode<EvalStatementNode> targets, int context)
	{
		super(coords);
		this.procedureMethod = procedureMethod;
		becomeParent(this.builtinProcedure);
		this.targets = targets;
		becomeParent(this.targets);
		this.context = context;
	}

	/** returns children of this node */
	@Override
	public Collection<BaseNode> getChildren()
	{
		Vector<BaseNode> children = new Vector<BaseNode>();
		children.add(procedure != null ? procedure : builtinProcedure != null ? builtinProcedure : procedureMethod);
		children.add(targets);
		return children;
	}

	/** returns names of the children, same order as in getChildren */
	@Override
	public Collection<String> getChildrenNames()
	{
		Vector<String> childrenNames = new Vector<String>();
		childrenNames.add("lhs");
		childrenNames.add("rhs");
		return childrenNames;
	}

	/** @see de.unika.ipd.grgen.ast.BaseNode#resolveLocal() */
	@Override
	protected boolean resolveLocal()
	{
		return true;
	}

	/** @see de.unika.ipd.grgen.ast.BaseNode#checkLocal() */
	@Override
	protected boolean checkLocal()
	{
		// targets is one of AssignNode, AssignVisitedNode, AssignIndexedNode
		// with QualIdentNode or IdentExprNode as owner/target
		// or a ConnectionNode or a SingleNodeConnNode or a VarDeclNode
		// and finally a projection expr node as source -- maybe with a cast prefix after type adjust
		if(procedure != null) {
			if(targets.size() != procedure.getNumReturnTypes() && targets.size() != 0) {
				procedure.reportError("Expected " + procedure.getNumReturnTypes()
						+ " procedure return variables, given " + targets.size());
				return false;
			}
		} else if(builtinProcedure != null) {
			if(targets.size() != builtinProcedure.getNumReturnTypes() && targets.size() != 0) {
				builtinProcedure.reportError("Expected " + builtinProcedure.getNumReturnTypes()
						+ " procedure return variables, given " + targets.size());
				return false;
			}
		} else { //procedureMethod!=null
			if(targets.size() != procedureMethod.getNumReturnTypes() && targets.size() != 0) {
				procedureMethod.reportError("Expected " + procedureMethod.getNumReturnTypes()
						+ " procedure return variables, given " + targets.size());
				return false;
			}
		}
		// hint: the types are checked in the singular assignments
		return true;
	}

	@Override
	public boolean checkStatementLocal(boolean isLHS, DeclNode root, EvalStatementNode enclosingLoop)
	{
		return true;
	}

	/**
	 * Construct the immediate representation from an assignment node.
	 * @see de.unika.ipd.grgen.ast.BaseNode#constructIR()
	 */
	@Override
	protected IR constructIR()
	{
		ReturnAssignment retAssign;
		if(procedure != null) {
			retAssign = new ReturnAssignment(procedure.checkIR(ProcedureOrBuiltinProcedureInvocationBase.class));
		} else if(builtinProcedure != null) {
			retAssign = new ReturnAssignment(builtinProcedure.checkIR(ProcedureOrBuiltinProcedureInvocationBase.class));
		} else {
			retAssign = new ReturnAssignment(procedureMethod.checkIR(ProcedureOrBuiltinProcedureInvocationBase.class));
		}
		for(EvalStatementNode target : targets.getChildren()) {
			retAssign.addAssignment(target.checkIR(AssignmentBase.class));
		}
		return retAssign;
	}
}
