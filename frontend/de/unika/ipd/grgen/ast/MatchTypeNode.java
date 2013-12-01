/*
 * GrGen: graph rewrite generator tool -- release GrGen.NET 4.1
 * Copyright (C) 2003-2013 Universitaet Karlsruhe, Institut fuer Programmstrukturen und Datenorganisation, LS Goos; and free programmers
 * licensed under LGPL v3 (see LICENSE.txt included in the packaging of this file)
 * www.grgen.net
 */

/**
 * @author Edgar Jakumeit
 */

package de.unika.ipd.grgen.ast;

import java.util.Collection;
import java.util.HashMap;
import java.util.Vector;

import de.unika.ipd.grgen.ast.util.DeclarationResolver;
import de.unika.ipd.grgen.ir.IR;
import de.unika.ipd.grgen.ir.MatchType;
import de.unika.ipd.grgen.ir.Rule;

public class MatchTypeNode extends DeclaredTypeNode {
	static {
		setName(MatchTypeNode.class, "match type");
	}

	@Override
	public String getName() {
		return "match<" + actionUnresolved.toString() + "> type";
	}

	private static HashMap<String, MatchTypeNode> matchTypes = new HashMap<String, MatchTypeNode>();

	public static MatchTypeNode getMatchType(IdentNode valueTypeIdent) {
		String keyStr = valueTypeIdent.toString();
		MatchTypeNode matchTypeNode = matchTypes.get(keyStr);

		if(matchTypeNode == null)
			matchTypes.put(keyStr, matchTypeNode = new MatchTypeNode(valueTypeIdent));

		return matchTypeNode;
	}

	private IdentNode actionUnresolved;
	protected TestDeclNode action;

	// the match type node instances are created in ParserEnvironment as needed
	public MatchTypeNode(IdentNode actionIdent) {
		actionUnresolved = becomeParent(actionIdent);
	}

	@Override
	public Collection<BaseNode> getChildren() {
		Vector<BaseNode> children = new Vector<BaseNode>();
		// no children
		return children;
	}

	@Override
	public Collection<String> getChildrenNames() {
		Vector<String> childrenNames = new Vector<String>();
		// no children
		return childrenNames;
	}

	private static final DeclarationResolver<TestDeclNode> actionResolver = new DeclarationResolver<TestDeclNode>(TestDeclNode.class);

	@Override
	protected boolean resolveLocal() {
		if(actionUnresolved instanceof IdentNode)
			fixupDefinition((IdentNode)actionUnresolved, actionUnresolved.getScope());
		action = actionResolver.resolve(actionUnresolved, this);
		if(action == null) return false;
		return true;
	}
	
	public TestDeclNode getTest() {
		assert(isResolved());
		return action;
	}

	@Override
	protected IR constructIR() {
		Rule matchAction = action.getAction();

		// return if the keyType or valueType construction already constructed the IR object
		if (isIRAlreadySet()) {
			return (MatchType)getIR();
		}

		return new MatchType(matchAction);
	}
}
