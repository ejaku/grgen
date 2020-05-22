/*
 * GrGen: graph rewrite generator tool -- release GrGen.NET 5.0
 * Copyright (C) 2003-2020 Universitaet Karlsruhe, Institut fuer Programmstrukturen und Datenorganisation, LS Goos; and free programmers
 * licensed under LGPL v3 (see LICENSE.txt included in the packaging of this file)
 * www.grgen.net
 */

/**
 * @author Moritz Kroll, Edgar Jakumeit
 */

package de.unika.ipd.grgen.ast.stmt;

import de.unika.ipd.grgen.ast.*;
import de.unika.ipd.grgen.ast.decl.DeclNode;
import de.unika.ipd.grgen.ast.decl.executable.FunctionDeclNode;
import de.unika.ipd.grgen.ast.decl.executable.ProcedureDeclNode;
import de.unika.ipd.grgen.ast.decl.executable.SubpatternDeclNode;
import de.unika.ipd.grgen.ast.expr.ExprNode;
import de.unika.ipd.grgen.ast.pattern.OrderedReplacementNode;
import de.unika.ipd.grgen.ast.type.TypeNode;
import de.unika.ipd.grgen.parser.Coords;

public abstract class EvalStatementNode extends OrderedReplacementNode
{
	public EvalStatementNode(Coords coords)
	{
		super(coords);
	}

	protected boolean checkType(ExprNode value, TypeNode targetType, String statement, String parameter)
	{
		TypeNode givenType = value.getType();
		TypeNode expectedType = targetType;
		if(!givenType.isCompatibleTo(expectedType)) {
			String givenTypeName = givenType.getTypeName();
			String expectedTypeName = expectedType.getTypeName();
			reportError("Cannot convert parameter " + parameter + " of " + statement + " from \"" + givenTypeName
					+ "\" to \"" + expectedTypeName + "\"");
			return false;
		}
		return true;
	}

	public abstract boolean checkStatementLocal(boolean isLHS, DeclNode root, EvalStatementNode enclosingLoop);

	public static boolean checkStatements(boolean isLHS, DeclNode root, EvalStatementNode enclosingLoop,
			CollectNode<EvalStatementNode> evals, boolean evalsAreTopLevel)
	{
		// check computation statement structure
		boolean res = true;

		EvalStatementNode last = null;
		boolean returnPassed = false;
		for(EvalStatementNode eval : evals.getChildren()) {
			if(returnPassed) {
				eval.reportError("no statements allowed after a return statement (at the same nesting level) (dead code)");
				res = false;
			}

			res &= eval.checkStatementLocal(isLHS, root, enclosingLoop);
			last = eval;

			if(eval instanceof ConditionStatementNode) {
				ConditionStatementNode csn = (ConditionStatementNode)eval;
				res &= checkStatements(isLHS, root, enclosingLoop, csn.statements, false);
				res &= checkStatements(isLHS, root, enclosingLoop, csn.falseCaseStatements, false);
			} else if(eval instanceof NestingStatementNode) {
				NestingStatementNode nsn = (NestingStatementNode)eval;
				res &= checkStatements(isLHS, root, nsn, nsn.statements, false);
			} else if(eval instanceof ReturnStatementNode) {
				returnPassed = true;
			} else if(eval instanceof ReturnAssignmentNode) {
				if(root instanceof FunctionDeclNode || isLHS) {
					ReturnAssignmentNode node = (ReturnAssignmentNode)eval;
					if(node.builtinProcedure == null
						|| (!node.builtinProcedure.getProcedureName().equals("emit")
							&& !node.builtinProcedure.getProcedureName().equals("emitdebug")
							&& !node.builtinProcedure.getProcedureName().equals("addDebug")
							&& !node.builtinProcedure.getProcedureName().equals("remDebug")
							&& !node.builtinProcedure.getProcedureName().equals("emitDebug")
							&& !node.builtinProcedure.getProcedureName().equals("haltDebug")
							&& !node.builtinProcedure.getProcedureName().equals("highlightDebug"))) {
						if(root instanceof FunctionDeclNode)
							eval.reportError("procedure call not allowed in function (only emit/emitdebug and the Debug package functions)");
						else
							eval.reportError("procedure call not allowed in yield (only emit/emitdebug and Debug package functions)");
						res = false;
					}
				}
			} else if(eval instanceof ExecStatementNode) {
				if(root instanceof SubpatternDeclNode) {
					eval.reportError("An exec inside an eval is forbidden in a subpattern -- move it outside the eval"
							+ " (so it becomes a deferred exec, executed at the end of rewriting, on the by-then current graph and the local entities valid at the end of its local rewriting).");
				}
			}
		}

		if(evalsAreTopLevel) {
			if(root instanceof FunctionDeclNode) {
				if(!(last instanceof ReturnStatementNode)) {
					if(last instanceof ConditionStatementNode) {
						if(!allCasesEndWithReturn((ConditionStatementNode)last)) {
							last.reportError("all cases of the if in the function must end with a return statement");
							res = false;
						}
					} else {
						if(last != null && last.getCoords().hasLocation())
							last.reportError("function must end with a return statement");
						else
							root.reportError("function must end with a return statement");
						res = false;
					}
				}
			}
			if(root instanceof ProcedureDeclNode) {
				if(!(last instanceof ReturnStatementNode)) {
					if(last instanceof ConditionStatementNode) {
						if(!allCasesEndWithReturn((ConditionStatementNode)last)) {
							last.reportError("all cases of the if in the procedure must end with a return statement");
							res = false;
						}
					} else {
						if(last != null && last.getCoords().hasLocation())
							last.reportError("procedure must end with a return statement");
						else
							root.reportError("procedure must end with a return statement");
						res = false;
					}
				}
			}
		}

		// TODO: check for def before use in computations, of computations entities
		// did for assignment targets and indexed assignment targets (see "Variables (node,edge,var,ref) of computations must be declared before they can be assigned");
		// but this is far from sufficient, needed for other kinds of assignments, too
		// and for reads, expressions, too 
		// -- massive externsion/refactoring needed (or clever hack?) cause grgen was built for not distinguishing order of entities

		return res;
	}

	public static boolean allCasesEndWithReturn(ConditionStatementNode condition)
	{
		boolean allEndWithReturn = true;

		EvalStatementNode last = null;
		for(EvalStatementNode eval : condition.statements.getChildren()) {
			last = eval;
		}
		if(!(last instanceof ReturnStatementNode)) {
			if(last instanceof ConditionStatementNode) {
				allEndWithReturn &= allCasesEndWithReturn((ConditionStatementNode)last);
			} else {
				return false;
			}
		}

		last = null;
		for(EvalStatementNode eval : condition.falseCaseStatements.getChildren()) {
			last = eval;
		}
		if(!(last instanceof ReturnStatementNode)) {
			if(last instanceof ConditionStatementNode) {
				allEndWithReturn &= allCasesEndWithReturn((ConditionStatementNode)last);
			} else {
				return false;
			}
		}

		return allEndWithReturn;
	}

	public boolean iteratedNotReferenced(String iterName)
	{
		boolean res = true;
		for(BaseNode child : getChildren()) {
			if(child instanceof ExprNode) {
				res &= ((ExprNode)child).iteratedNotReferenced(iterName);
			}
		}
		return res;
	}

	@Override
	public boolean noExecStatement(boolean inEvalHereContext)
	{
		boolean res = true;
		for(BaseNode child : getChildren()) {
			if(!(child instanceof EvalStatementNode)) {
				continue;
			}
			EvalStatementNode evalStatement = (EvalStatementNode)child;
			res &= evalStatement.noExecStatement(inEvalHereContext);
		}
		return res;
	}
}
