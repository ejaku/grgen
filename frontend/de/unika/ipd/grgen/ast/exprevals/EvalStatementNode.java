/*
 * GrGen: graph rewrite generator tool -- release GrGen.NET 4.0
 * Copyright (C) 2003-2013 Universitaet Karlsruhe, Institut fuer Programmstrukturen und Datenorganisation, LS Goos; and free programmers
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
				eval.reportError("no statements allowed after a return statement (at the same nesting level)");
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
			} else if(eval instanceof ForLookupNode) {
				ForLookupNode fln = (ForLookupNode)eval;
				res &= checkStatements(isLHS, root, fln, fln.loopedStatements, false);
			} else if(eval instanceof ContainerAccumulationYieldNode) {
				ContainerAccumulationYieldNode cayn = (ContainerAccumulationYieldNode)eval;
				res &= checkStatements(isLHS, root, cayn, cayn.accumulationStatements, false);
			} else if(eval instanceof IteratedAccumulationYieldNode) {
				IteratedAccumulationYieldNode iayn = (IteratedAccumulationYieldNode)eval;
				res &= checkStatements(isLHS, root, iayn, iayn.accumulationStatements, false);
			} else if(eval instanceof ReturnStatementNode) {
				returnPassed = true;
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
						last.reportError("function must end with a return statement");
						res = false;
					}
				}
			}
			if(root instanceof ComputationDeclNode) {
				if(!(last instanceof ReturnStatementNode)) {
					if(last instanceof ConditionStatementNode) {
						if(!allCasesEndWithReturn((ConditionStatementNode)last)) {
							last.reportError("all cases of the if in the computation must end with a return statement");
							res = false;
						}
					} else {
						last.reportError("computation must end with a return statement");
						res = false;
					}
				}
			}
		}

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
