/*
 * GrGen: graph rewrite generator tool -- release GrGen.NET 5.0
 * Copyright (C) 2003-2020 Universitaet Karlsruhe, Institut fuer Programmstrukturen und Datenorganisation, LS Goos; and free programmers
 * licensed under LGPL v3 (see LICENSE.txt included in the packaging of this file)
 * www.grgen.net
 */

/**
 * @author Edgar Jakumeit
 */

package de.unika.ipd.grgen.ast;

import java.util.Collection;
import java.util.Vector;

import de.unika.ipd.grgen.ir.Bad;
import de.unika.ipd.grgen.ir.IR;
import de.unika.ipd.grgen.parser.Coords;

/**
 * Call of multiple actions.
 */
public class MultiCallActionNode extends BaseNode {

	static {
		setName(MultiCallActionNode.class, "multiple call action");
	}

	private CollectNode<CallActionNode> actionCalls;
	
	private CollectNode<BaseNode> matchClassFilterFunctionsUnresolved;
	protected CollectNode<MatchTypeQualIdentNode> matchClassFilterFunctions;

	public MultiCallActionNode(Coords coords, CollectNode<CallActionNode> actionCalls, CollectNode<BaseNode> matchClassFilterFunctions) {
		super(coords);
		this.actionCalls = actionCalls;
		this.matchClassFilterFunctionsUnresolved = matchClassFilterFunctions;
	}

	/** returns children of this node */
	@Override
	public Collection<BaseNode> getChildren() {
		Vector<BaseNode> children = new Vector<BaseNode>();
		children.add(actionCalls);
		children.add(getValidVersion(matchClassFilterFunctionsUnresolved, matchClassFilterFunctions));
		return children;
	}

	/** returns names of the children, same order as in getChildren */
	@Override
	public Collection<String> getChildrenNames() {
		Vector<String> childrenNames = new Vector<String>();
		childrenNames.add("actionCalls");
		childrenNames.add("matchClassFilter");
		return childrenNames;
	}

	/** @see de.unika.ipd.grgen.ast.BaseNode#resolveLocal() */
	@Override
	protected boolean resolveLocal() {
		matchClassFilterFunctions = new CollectNode<MatchTypeQualIdentNode>();
		for(BaseNode matchClassFilterFunctionUnresolved : matchClassFilterFunctionsUnresolved.getChildren()) {
			matchClassFilterFunctions.addChild((MatchTypeQualIdentNode)matchClassFilterFunctionUnresolved);
		}
			
		return true;
	}

	/** @see de.unika.ipd.grgen.ast.BaseNode#checkLocal() */
	@Override
	protected boolean checkLocal() {
		return true; // only checking of children necessary
	}

	/** check after the IR is built */
	protected boolean checkPost() {
		boolean res = true;

		// all actions must implement the match classes of the employed filters
		for(MatchTypeQualIdentNode matchClassFilterReference : matchClassFilterFunctions.getChildren()) {
			MatchClassFilterFunctionDeclNode matchClassFilter = (MatchClassFilterFunctionDeclNode)matchClassFilterReference.getMember();
			String matchClassReferencedByFilterFunction = matchClassFilter.matchType.getIdentNode().toString();
			String nameOfFilterFunction = matchClassFilter.getIdentNode().toString();

			for(CallActionNode actionCall : actionCalls.getChildren()) {
				checkWhetherCalledActionImplementsMatchClass(matchClassReferencedByFilterFunction, nameOfFilterFunction,
						actionCall);
			}
		}

		return res;
	}

	public static void checkWhetherCalledActionImplementsMatchClass(String matchClassReferencedByFilterFunction,
			String nameOfFilterFunction, CallActionNode actionCall) {
		String nameOfAction = actionCall.getAction().getIdentNode().toString();
		boolean isMatchClassOfFilterImplementedByAction = false;
		for(DefinedMatchTypeNode matchType : actionCall.getAction().getImplementedMatchClasses())
		{
			String matchClassImplementedByAction = matchType.getIdentNode().toString();
			if(matchClassReferencedByFilterFunction.equals(matchClassImplementedByAction)) {
				isMatchClassOfFilterImplementedByAction = true;
			}
		}
		
		if(!isMatchClassOfFilterImplementedByAction) {
			if(actionCall.getAction().getImplementedMatchClasses().isEmpty()) {
				if(nameOfFilterFunction != null) {
					actionCall.reportError("Match filter " + nameOfFilterFunction + " is defined for match class " + matchClassReferencedByFilterFunction + ". "
							+ "The action " + nameOfAction + " implements no match classes.");
				} else {
					actionCall.reportError("The multi rule query is defined for match class " + matchClassReferencedByFilterFunction + ". "
							+ "The action " + nameOfAction + " implements no match classes.");
				}
			} else {
				StringBuilder matchClassesImplementedByAction = new StringBuilder();
				boolean first = true;
				for(DefinedMatchTypeNode matchType : actionCall.getAction().getImplementedMatchClasses())
				{
					String matchTypeNameImplementedByAction = matchType.getIdentNode().toString();
					if(first) {
						first = false;
					} else {
						matchClassesImplementedByAction.append(",");
					}
					matchClassesImplementedByAction.append(matchTypeNameImplementedByAction);
				}

				if(nameOfFilterFunction != null) {
					actionCall.reportError("Match filter " + nameOfFilterFunction + " is defined for match class " + matchClassReferencedByFilterFunction + ". "
							+ "The action " + nameOfAction + " implements only " + matchClassesImplementedByAction + ".");
				} else {
					actionCall.reportError("The multi rule query is defined for match class " + matchClassReferencedByFilterFunction + ". "
							+ "The action " + nameOfAction + " implements only " + matchClassesImplementedByAction + ".");
				}
			}
		}
	}

	/** @see de.unika.ipd.grgen.ast.BaseNode#constructIR() */
	@Override
	protected IR constructIR() {
		assert false;
		return Bad.getBad(); // TODO fix this
	}
}
