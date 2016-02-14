/*
 * GrGen: graph rewrite generator tool -- release GrGen.NET 4.4
 * Copyright (C) 2003-2016 Universitaet Karlsruhe, Institut fuer Programmstrukturen und Datenorganisation, LS Goos; and free programmers
 * licensed under LGPL v3 (see LICENSE.txt included in the packaging of this file)
 * www.grgen.net
 */

/**
 * @author Moritz Kroll, Edgar Jakumeit
 */

package de.unika.ipd.grgen.ast.exprevals;

import de.unika.ipd.grgen.ast.*;
import de.unika.ipd.grgen.parser.Coords;

public abstract class EvalStatementNode extends OrderedReplacementNode
{
	public EvalStatementNode(Coords coords)
	{
		super(coords);
	}

	protected boolean checkType(ExprNode value, TypeNode targetType, String statement, String parameter) {
		TypeNode givenType = value.getType();
		TypeNode expectedType = targetType;
		if(!givenType.isCompatibleTo(expectedType)) {
			String givenTypeName;
			if(givenType instanceof InheritanceTypeNode)
				givenTypeName = ((InheritanceTypeNode) givenType).getIdentNode().toString();
			else
				givenTypeName = givenType.toString();
			String expectedTypeName;
			if(expectedType instanceof InheritanceTypeNode)
				expectedTypeName = ((InheritanceTypeNode) expectedType).getIdentNode().toString();
			else
				expectedTypeName = expectedType.toString();
			reportError("Cannot convert parameter " + parameter + " of " + statement + " from \""
					+ givenTypeName + "\" to \"" + expectedTypeName + "\"");
			return false;
		}
		return true;
	}
	
	public abstract boolean checkStatementLocal(boolean isLHS, DeclNode root, EvalStatementNode enclosingLoop);
	
	public static boolean checkStatements(boolean isLHS, DeclNode root, EvalStatementNode enclosingLoop, CollectNode<EvalStatementNode> evals, boolean evalsAreTopLevel) {
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
				res &= checkStatements(isLHS, root, enclosingLoop, csn.trueCaseStatements, false);
				res &= checkStatements(isLHS, root, enclosingLoop, csn.falseCaseStatements, false);
			} else if(eval instanceof WhileStatementNode) {
				WhileStatementNode wsn = (WhileStatementNode)eval;
				res &= checkStatements(isLHS, root, wsn, wsn.loopedStatements, false);
			} else if(eval instanceof DoWhileStatementNode) {
				DoWhileStatementNode dwsn = (DoWhileStatementNode)eval;
				res &= checkStatements(isLHS, root, dwsn, dwsn.loopedStatements, false);
			} else if(eval instanceof ForFunctionNode) {
				ForFunctionNode ffn = (ForFunctionNode)eval;
				res &= checkStatements(isLHS, root, ffn, ffn.loopedStatements, false);
			} else if(eval instanceof ContainerAccumulationYieldNode) {
				ContainerAccumulationYieldNode cayn = (ContainerAccumulationYieldNode)eval;
				res &= checkStatements(isLHS, root, cayn, cayn.accumulationStatements, false);
			} else if(eval instanceof IteratedAccumulationYieldNode) {
				IteratedAccumulationYieldNode iayn = (IteratedAccumulationYieldNode)eval;
				res &= checkStatements(isLHS, root, iayn, iayn.accumulationStatements, false);
			} else if(eval instanceof ReturnStatementNode) {
				returnPassed = true;
			} else if(eval instanceof ReturnAssignmentNode) {
				if(root instanceof FunctionDeclNode || isLHS) {
					ReturnAssignmentNode node = (ReturnAssignmentNode)eval;
					if(node.builtinProcedure==null 
							|| (!node.builtinProcedure.getProcedureName().equals("emit")
									&& !node.builtinProcedure.getProcedureName().equals("addDebug")
									&& !node.builtinProcedure.getProcedureName().equals("remDebug")
									&& !node.builtinProcedure.getProcedureName().equals("emitDebug")
									&& !node.builtinProcedure.getProcedureName().equals("haltDebug")
									&& !node.builtinProcedure.getProcedureName().equals("highlightDebug"))) {
						if(root instanceof FunctionDeclNode)
							eval.reportError("procedure call not allowed in function (only emit and the Debug package functions)");
						else
							eval.reportError("procedure call not allowed in yield (only emit and Debug package functions)");
						res = false;
					}
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
						if(last!=null && last.getCoords().hasLocation())
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
						if(last!=null && last.getCoords().hasLocation())
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
	
	public static boolean allCasesEndWithReturn(ConditionStatementNode condition) {
		boolean allEndWithReturn = true;
		
		EvalStatementNode last = null;
		for(EvalStatementNode eval : condition.trueCaseStatements.getChildren()) {
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
}
