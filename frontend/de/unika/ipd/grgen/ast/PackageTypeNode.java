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
import java.util.Vector;

import de.unika.ipd.grgen.ast.util.CollectResolver;
import de.unika.ipd.grgen.ast.util.DeclarationResolver;
import de.unika.ipd.grgen.ir.IR;
import de.unika.ipd.grgen.ir.Ident;
import de.unika.ipd.grgen.ir.PackageType;

/**
 * A package type AST node.
 */
public class PackageTypeNode extends CompoundTypeNode {
	static {
		setName(PackageTypeNode.class, "package type");
	}

	private CollectNode<IdentNode> declsUnresolved;
	protected CollectNode<TypeDeclNode> decls;

	public PackageTypeNode(CollectNode<IdentNode> decls) {
		this.declsUnresolved = decls;
		becomeParent(this.declsUnresolved);
	}

	/** returns children of this node */
	@Override
	public Collection<BaseNode> getChildren() {
		Vector<BaseNode> children = new Vector<BaseNode>();
		children.add(getValidVersion(declsUnresolved, decls));
		return children;
	}

	/** returns names of the children, same order as in getChildren */
	@Override
	public Collection<String> getChildrenNames() {
		Vector<String> childrenNames = new Vector<String>();
		childrenNames.add("decls");
		return childrenNames;
	}

	private static CollectResolver<TypeDeclNode> declsResolver = new CollectResolver<TypeDeclNode>(
			new DeclarationResolver<TypeDeclNode>(TypeDeclNode.class));

	/** @see de.unika.ipd.grgen.ast.BaseNode#resolveLocal() */
	@Override
	protected boolean resolveLocal() {
		decls = declsResolver.resolve(declsUnresolved, this);
		return decls != null;
	}

	public CollectNode<TypeDeclNode> getTypeDecls() {
		return decls;
	}

	/** @see de.unika.ipd.grgen.ast.BaseNode#constructIR() */
	@Override
	protected IR constructIR() {
		Ident id = getIdentNode().checkIR(Ident.class);
		PackageType pt = new PackageType(id);
		for(TypeDeclNode typeDecl : decls.getChildren()) {
			pt.addType(typeDecl.getDeclType().getType());
		}
		return pt;
	}

	@Override
	public String toString() {
		return "package " + getIdentNode();
	}

	public static String getKindStr() {
		return "package type";
	}

	public static String getUseStr() {
		return "package";
	}
}
