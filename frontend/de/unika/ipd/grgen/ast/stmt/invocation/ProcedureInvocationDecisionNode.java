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
import de.unika.ipd.grgen.ast.stmt.procenv.EmitProcNode;
import de.unika.ipd.grgen.ast.stmt.procenv.RecordProcNode;
import de.unika.ipd.grgen.ast.type.TypeNode;
import de.unika.ipd.grgen.ast.type.executable.ProcedureTypeNode;
import de.unika.ipd.grgen.ast.util.ResolvingEnvironment;
import de.unika.ipd.grgen.ir.IR;
import de.unika.ipd.grgen.parser.ParserEnvironment;

public class ProcedureInvocationDecisionNode extends ProcedureInvocationBaseNode
{
	static {
		setName(ProcedureInvocationDecisionNode.class, "procedure invocation decision");
	}

	static TypeNode procedureTypeNode = new ProcedureTypeNode();

	protected IdentNode procedureIdent;
	protected BuiltinProcedureInvocationBaseNode result;

	ParserEnvironment env;

	public ProcedureInvocationDecisionNode(IdentNode procedureIdent,
			CollectNode<ExprNode> arguments, int context, ParserEnvironment env)
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

	@Override
	protected boolean resolveLocal()
	{
		ResolvingEnvironment resolvingEnvironment = new ResolvingEnvironment(env, error, getCoords());
		result = decide(procedureIdent.toString(), arguments, resolvingEnvironment);
		return result != null;
	}
	
	private static BuiltinProcedureInvocationBaseNode decide(String procedureName, CollectNode<ExprNode> arguments,
			ResolvingEnvironment env)
	{
		switch(procedureName) {
		case "add":
			if(arguments.size() == 1) {
				return new GraphAddNodeProcNode(env.getCoords(), arguments.get(0));
			} else if(arguments.size() == 3) {
				return new GraphAddEdgeProcNode(env.getCoords(), arguments.get(0), arguments.get(1), arguments.get(2));
			} else {
				env.reportError(procedureName + "() takes 1 or 3 parameters.");
				return null;
			}
		case "retype":
			if(arguments.size() == 2) {
				return new GraphRetypeProcNode(env.getCoords(), arguments.get(0), arguments.get(1));
			} else {
				env.reportError(procedureName + "() takes 2 parameters.");
				return null;
			}
		case "insert":
			if(arguments.size() != 1) {
				env.reportError("insert(.) takes one parameter.");
				return null;
			} else
				return new InsertProcNode(env.getCoords(), arguments.get(0));
		case "insertCopy":
			if(arguments.size() != 2) {
				env.reportError("insertCopy(.,.) takes two parameters.");
				return null;
			} else
				return new InsertCopyProcNode(env.getCoords(), arguments.get(0), arguments.get(1));
		case "insertInduced":
			if(arguments.size() != 2) {
				env.reportError("insertInduced(.,.) takes two parameters.");
				return null;
			} else
				return new InsertInducedSubgraphProcNode(env.getCoords(), arguments.get(0), arguments.get(1));
		case "insertDefined":
			if(arguments.size() != 2) {
				env.reportError("insertDefined(.,.) takes two parameters.");
				return null;
			} else
				return new InsertDefinedSubgraphProcNode(env.getCoords(), arguments.get(0), arguments.get(1));
		case "valloc":
			if(arguments.size() != 0) {
				env.reportError("valloc() takes no parameters.");
				return null;
			} else
				return new VAllocProcNode(env.getCoords());
		case "rem":
			if(arguments.size() != 1) {
				env.reportError("rem(value) takes one parameter.");
				return null;
			} else {
				return new GraphRemoveProcNode(env.getCoords(), arguments.get(0));
			}
		case "clear":
			if(arguments.size() != 0) {
				env.reportError("clear() takes no parameters.");
				return null;
			} else {
				return new GraphClearProcNode(env.getCoords());
			}
		case "vfree":
			if(arguments.size() != 1) {
				env.reportError("vfree(value) takes one parameter.");
				return null;
			} else {
				return new VFreeProcNode(env.getCoords(), arguments.get(0));
			}
		case "vfreenonreset":
			if(arguments.size() != 1) {
				env.reportError("vfreenonreset(value) takes one parameter.");
				return null;
			} else {
				return new VFreeNonResetProcNode(env.getCoords(), arguments.get(0));
			}
		case "vreset":
			if(arguments.size() != 1) {
				env.reportError("vreset(value) takes one parameter.");
				return null;
			} else {
				return new VResetProcNode(env.getCoords(), arguments.get(0));
			}
		case "record":
			if(arguments.size() != 1) {
				env.reportError("record(value) takes one parameter.");
				return null;
			} else {
				return new RecordProcNode(env.getCoords(), arguments.get(0));
			}
		case "emit":
			if(arguments.size() >= 1) {
				EmitProcNode emit = new EmitProcNode(env.getCoords(), false);
				for(ExprNode param : arguments.getChildren()) {
					emit.addExpression(param);
				}
				return emit;
			} else {
				env.reportError("emit() takes at least one parameter.");
				return null;
			}
		case "emitdebug":
			if(arguments.size() >= 1) {
				EmitProcNode emit = new EmitProcNode(env.getCoords(), true);
				for(ExprNode param : arguments.getChildren()) {
					emit.addExpression(param);
				}
				return emit;
			} else {
				env.reportError("emitdebug() takes at least one parameter.");
				return null;
			}
		case "addCopy":
			if(arguments.size() == 1) {
				return new GraphAddCopyNodeProcNode(env.getCoords(), arguments.get(0));
			} else if(arguments.size() == 3) {
				return new GraphAddCopyEdgeProcNode(env.getCoords(), arguments.get(0), arguments.get(1), arguments.get(2));
			} else {
				env.reportError(procedureName + "() takes 1 or 3 parameters.");
				return null;
			}
		case "merge":
			if(arguments.size() < 2 || arguments.size() > 3) {
				env.reportError("merge(target,source,oldSourceName) takes two or three parameters.");
				return null;
			} else {
				if(arguments.size() == 2)
					return new GraphMergeProcNode(env.getCoords(), arguments.get(0), arguments.get(1), null);
				else
					return new GraphMergeProcNode(env.getCoords(), arguments.get(0), arguments.get(1), arguments.get(2));
			}
		case "redirectSource":
			if(arguments.size() < 2 || arguments.size() > 3) {
				env.reportError("redirectSource(edge,newSource,oldSourceName) takes two or three parameters.");
				return null;
			} else {
				if(arguments.size() == 2)
					return new GraphRedirectSourceProcNode(env.getCoords(), arguments.get(0), arguments.get(1), null);
				else
					return new GraphRedirectSourceProcNode(env.getCoords(), arguments.get(0), arguments.get(1), arguments.get(2));
			}
		case "redirectTarget":
			if(arguments.size() < 2 || arguments.size() > 3) {
				env.reportError("redirectTarget(edge,newTarget,oldTargetName) takes two or three parameters.");
				return null;
			} else {
				if(arguments.size() == 2)
					return new GraphRedirectTargetProcNode(env.getCoords(), arguments.get(0), arguments.get(1), null);
				else
					return new GraphRedirectTargetProcNode(env.getCoords(), arguments.get(0), arguments.get(1), arguments.get(2));
			}
		case "redirectSourceAndTarget":
			if(arguments.size() != 3 && arguments.size() != 5) {
				env.reportError("redirectSourceAndTarget(edge,newSource,newTarget,oldSourceName,oldTargetName) takes three or five parameters.");
				return null;
			} else {
				if(arguments.size() == 3)
					return new GraphRedirectSourceAndTargetProcNode(env.getCoords(), arguments.get(0), arguments.get(1),
							arguments.get(2), null, null);
				else
					return new GraphRedirectSourceAndTargetProcNode(env.getCoords(), arguments.get(0), arguments.get(1),
							arguments.get(2), arguments.get(3), arguments.get(4));
			}
		default:
			env.reportError("no computation " + procedureName + " known");
			return null;
		}
	}

	@Override
	protected boolean checkLocal()
	{
		if((context & BaseNode.CONTEXT_FUNCTION_OR_PROCEDURE) == BaseNode.CONTEXT_FUNCTION) {
			if(isEmitProcedure()) { // allowed exceptions
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
		return isEmitProcedure();
	}

	protected boolean isEmitProcedure()
	{
		switch(procedureIdent.toString()) {
		case "emit":
		case "emitdebug":
			return true;
		default:
			return false;
		}
	}

	@Override
	public boolean checkStatementLocal(boolean isLHS, DeclNode root, EvalStatementNode enclosingLoop)
	{
		return true;
	}

	protected ProcedureOrBuiltinProcedureInvocationBaseNode getResult()
	{
		return result;
	}

	@Override
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
