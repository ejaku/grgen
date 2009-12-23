/*
 * GrGen: graph rewrite generator tool -- release GrGen.NET 2.6
 * Copyright (C) 2003-2009 Universitaet Karlsruhe, Institut fuer Programmstrukturen und Datenorganisation, LS Goos
 * licensed under LGPL v3 (see LICENSE.txt included in the packaging of this file)
 */

/**
 * @author Sebastian Hack
 * @version $Id$
 */
package de.unika.ipd.grgen.ast;

import java.util.Collection;
import java.util.Vector;

import de.unika.ipd.grgen.ast.util.Checker;
import de.unika.ipd.grgen.ast.util.DeclarationTypeResolver;
import de.unika.ipd.grgen.ast.util.SimpleChecker;
import de.unika.ipd.grgen.ir.Entity;
import de.unika.ipd.grgen.ir.IR;
import de.unika.ipd.grgen.ir.Type;
import de.unika.ipd.grgen.parser.Scope;
import de.unika.ipd.grgen.parser.Symbol;

/**
 * A compound type member declaration.
 */
public class MemberDeclNode extends DeclNode {
	static {
		setName(MemberDeclNode.class, "member declaration");
	}

	protected TypeNode type;
	private boolean isConst;
	private BaseNode constInitializer;

	/**
	 * @param n Identifier which declared the member.
	 * @param t Type with which the member was declared.
	 */
	public MemberDeclNode(IdentNode n, BaseNode t, boolean isConst) {
		super(n, t);
		this.isConst = isConst;
	}

	/** returns children of this node */
	@Override
	public Collection<BaseNode> getChildren() {
		Vector<BaseNode> children = new Vector<BaseNode>();
		children.add(ident);
		children.add(getValidVersion(typeUnresolved, type));
		return children;
	}

	/** returns names of the children, same order as in getChildren */
	@Override
	public Collection<String> getChildrenNames() {
		Vector<String> childrenNames = new Vector<String>();
		childrenNames.add("ident");
		childrenNames.add("type");
		return childrenNames;
	}

	protected boolean isConst() {
		return isConst;
	}

	protected BaseNode getConstInitializer() {
		return constInitializer;
	}

	public void setConstInitializer(BaseNode init) {
		constInitializer = init;
	}

	/*
	 * This sets the symbol defintion to the right place, if the defintion is behind the actual position.
	 * TODO: extract and unify this method to a common place/code duplication
	 */
	public boolean fixupDefinition(IdentNode id) {
		Scope scope = id.getScope().getIdentNode().getScope();

		debug.report(NOTE, "Fixup " + id + " in scope " + scope);

		// Get the definition of the ident's symbol local to the owned scope.
		Symbol.Definition def = scope.getLocalDef(id.getSymbol());
		debug.report(NOTE, "definition is: " + def);

		// The result is true, if the definition's valid.
		boolean res = def.isValid();

		// If this definition is valid, i.e. it exists,
		// the definition of the ident is rewritten to this definition,
		// else, an error is emitted,
		// since this ident was supposed to be defined in this scope.
		if(res) {
			id.setSymDef(def);
		} else {
			id.reportError("Identifier \"" + id + "\" not declared in this scope: " + scope);
		}

		return res;
	}

	private static final DeclarationTypeResolver<TypeNode> typeResolver = new DeclarationTypeResolver<TypeNode>(TypeNode.class);

	/** @see de.unika.ipd.grgen.ast.BaseNode#resolveLocal() */
	@Override
	protected boolean resolveLocal() {
		if(typeUnresolved instanceof IdentNode)
			fixupDefinition((IdentNode)typeUnresolved);
		type = typeResolver.resolve(typeUnresolved, this);
		return type!=null;
	}

	/** @return The type node of the declaration */
	@Override
	public TypeNode getDeclType() {
		assert isResolved();

		return type;
	}

	private static final Checker typeChecker = new SimpleChecker(
			new Class[] { BasicTypeNode.class, EnumTypeNode.class, MapTypeNode.class, SetTypeNode.class});

	/** @see de.unika.ipd.grgen.ast.BaseNode#checkLocal() */
	@Override
	protected boolean checkLocal() {
		return typeChecker.check(type, error);
	}

	@Override
	protected IR constructIR() {
		Type type = getDeclType().checkIR(Type.class);
		return new Entity("entity", getIdentNode().getIdent(), type, isConst, 0);
	}

	public static String getKindStr() {
		return "member declaration";
	}

	public static String getUseStr() {
		return "member access";
	}
}
