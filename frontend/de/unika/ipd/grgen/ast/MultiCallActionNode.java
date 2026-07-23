/*
 * GrGen: graph rewrite generator tool -- release GrGen.NET 8.1
 * Copyright (C) 2003-2026 Universitaet Karlsruhe, Institut fuer Programmstrukturen und Datenorganisation, LS Goos; and free programmers
 * licensed under LGPL v3, some components/parts use different licenses (see LICENSE.txt included in the packaging of this file)
 * www.grgen.de / www.grgen.net
 */

/**
 * @author Edgar Jakumeit
 */

package de.unika.ipd.grgen.ast;

import java.util.Collection;
import java.util.List;
import java.util.ArrayList;

import de.unika.ipd.grgen.ast.decl.executable.MatchClassFilterFunctionDeclNode;
import de.unika.ipd.grgen.ast.type.DefinedMatchTypeNode;
import de.unika.ipd.grgen.ir.Bad;
import de.unika.ipd.grgen.ir.IR;
import de.unika.ipd.grgen.parser.Coords;

/**
 * Call of multiple actions.
 */
public class MultiCallActionNode extends BaseNode
{
	static {
		setClassName(MultiCallActionNode.class, "multiple call action");
	}

	private CollectNode<CallActionNode> actionCalls;

	private CollectNode<BaseNode> matchClassFilterFunctionsUnresolved;
	protected CollectNode<MatchTypeQualIdentNode> matchClassFilterFunctions;

	public MultiCallActionNode(Coords coords, CollectNode<CallActionNode> actionCalls,
			CollectNode<BaseNode> matchClassFilterFunctions)
	{
		super(coords);
		this.actionCalls = actionCalls;
		this.matchClassFilterFunctionsUnresolved = matchClassFilterFunctions;
	}

	/** returns children of this node */
	@Override
	public Collection<BaseNode> getChildren()
	{
		List<BaseNode> children = new ArrayList<BaseNode>();
		children.add(actionCalls);
		children.add(getValidVersionCollectNode(matchClassFilterFunctionsUnresolved, matchClassFilterFunctions));
		return children;
	}

	/** returns names of the children, same order as in getChildren */
	@Override
	public Collection<String> getChildrenNames()
	{
		List<String> childrenNames = new ArrayList<String>();
		childrenNames.add("actionCalls");
		childrenNames.add("matchClassFilter");
		return childrenNames;
	}

	/** @see de.unika.ipd.grgen.ast.BaseNode#resolveLocal() */
	@Override
	protected boolean resolveLocal()
	{
		matchClassFilterFunctions = new CollectNode<MatchTypeQualIdentNode>();
		for(BaseNode matchClassFilterFunctionUnresolved : matchClassFilterFunctionsUnresolved.getChildrenExact()) {
			matchClassFilterFunctions.addChild((MatchTypeQualIdentNode)matchClassFilterFunctionUnresolved);
		}

		return true;
	}

	/** @see de.unika.ipd.grgen.ast.BaseNode#checkLocal() */
	@Override
	protected boolean checkLocal()
	{
		return true; // only checking of children necessary
	}

	/** check after the IR is built */
	protected boolean checkPost()
	{
		boolean res = true;

		// all actions must implement the match classes of the employed filters
		for(MatchTypeQualIdentNode matchClassFilterReference : matchClassFilterFunctions.getChildrenExact()) {
			MatchClassFilterFunctionDeclNode matchClassFilter =
					(MatchClassFilterFunctionDeclNode)matchClassFilterReference.getMember();
			String matchClassReferencedByFilterFunction = matchClassFilter.matchType.getIdent().toString();

			for(CallActionNode actionCall : actionCalls.getChildrenExact()) {
				checkWhetherCalledActionImplementsMatchClass(matchClassReferencedByFilterFunction, matchClassFilter,
						actionCall);
			}
		}

		return res;
	}

	public static void checkWhetherCalledActionImplementsMatchClass(String matchClassReferencedByFilterFunction,
			MatchClassFilterFunctionDeclNode filterFunction, CallActionNode actionCall)
	{
		boolean isMatchClassOfFilterImplementedByAction = false;
		for(DefinedMatchTypeNode matchType : actionCall.getAction().getImplementedMatchClasses()) {
			String matchClassImplementedByAction = matchType.getIdent().toString();
			if(matchClassReferencedByFilterFunction.equals(matchClassImplementedByAction)) {
				isMatchClassOfFilterImplementedByAction = true;
			}
		}

		if(!isMatchClassOfFilterImplementedByAction) {
			StringBuilder matchClassesImplementedByAction = new StringBuilder();
			if(actionCall.getAction().getImplementedMatchClasses().isEmpty()) {
				matchClassesImplementedByAction.append("no match classes");
			} else {
				boolean first = true;
				for(DefinedMatchTypeNode matchType : actionCall.getAction().getImplementedMatchClasses()) {
					String matchTypeNameImplementedByAction = matchType.getTypeName();
					if(first) {
						first = false;
					} else {
						matchClassesImplementedByAction.append(",");
					}
					matchClassesImplementedByAction.append(matchTypeNameImplementedByAction);
				}
			}
			
			// TODO: print coordinates of match class, requires input of match class type instead of only string
			if(filterFunction != null) {
				actionCall.reportError("The called filter function " + filterFunction.toStringWithDeclarationCoords()
						+ " is defined for match class " + matchClassReferencedByFilterFunction + "."
						+ " The action " + actionCall.getAction().toStringWithDeclarationCoords()
						+ " it is applied on does not implement the match class"
						+ " (it implements " + matchClassesImplementedByAction + ").");
			} else {
				actionCall.reportError("The multi rule query is defined to return match class " + matchClassReferencedByFilterFunction + "."
						+ " The action " + actionCall.getAction().toStringWithDeclarationCoords()
						+ " called in the multi rule query does not implement the match class"
						+ " (it implements " + matchClassesImplementedByAction + ").");
			}
		}
	}

	/** @see de.unika.ipd.grgen.ast.BaseNode#constructIR() */
	@Override
	protected IR constructIR()
	{
		assert false;
		return Bad.getBadObject();
	}
}
