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

import de.unika.ipd.grgen.ast.util.DeclarationTypeResolver;
import de.unika.ipd.grgen.ir.IR;
import de.unika.ipd.grgen.ir.MatchType;
import de.unika.ipd.grgen.ir.Type;

public class MatchTypeNode extends DeclaredTypeNode {
	static {
		setName(MatchTypeNode.class, "match type");
	}

	@Override
	public String getName() {
		return "match<" + valueTypeUnresolved.toString() + "> type";
	}

	private static HashMap<String, MatchTypeNode> matchTypes = new HashMap<String, MatchTypeNode>();

	public static MatchTypeNode getMatchType(IdentNode valueTypeIdent) {
		String keyStr = valueTypeIdent.toString();
		MatchTypeNode matchTypeNode = matchTypes.get(keyStr);

		if(matchTypeNode == null)
			matchTypes.put(keyStr, matchTypeNode = new MatchTypeNode(valueTypeIdent));

		return matchTypeNode;
	}

	private IdentNode valueTypeUnresolved;
	protected TypeNode valueType;

	// the match type node instances are created in ParserEnvironment as needed
	public MatchTypeNode(IdentNode valueTypeIdent) {
		valueTypeUnresolved = becomeParent(valueTypeIdent);
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

	private static final DeclarationTypeResolver<TypeNode> typeResolver = new DeclarationTypeResolver<TypeNode>(TypeNode.class);

	@Override
	protected boolean resolveLocal() {
		if(valueTypeUnresolved instanceof IdentNode)
			fixupDefinition((IdentNode)valueTypeUnresolved, valueTypeUnresolved.getScope());
		valueType = typeResolver.resolve(valueTypeUnresolved, this);

		if(valueType == null) return false;

		return true;
	}

	@Override
	protected IR constructIR() {
		Type vt = valueType.getType();

		// return if the keyType or valueType construction already constructed the IR object
		if (isIRAlreadySet()) {
			return (MatchType)getIR();
		}

		return new MatchType(vt);
	}
}
