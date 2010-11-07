/*
 * GrGen: graph rewrite generator tool -- release GrGen.NET 2.6
 * Copyright (C) 2003-2010 Universitaet Karlsruhe, Institut fuer Programmstrukturen und Datenorganisation, LS Goos
 * licensed under LGPL v3 (see LICENSE.txt included in the packaging of this file)
 * www.grgen.net
 */

/**
 * @author Edgar Jakumeit
 * @version $Id: ExternalTypeNode.java 26740 2010-01-02 11:21:07Z eja $
 */
package de.unika.ipd.grgen.ast;

import java.util.Collection;
import java.util.Map;
import java.util.Vector;

import de.unika.ipd.grgen.ast.util.CollectResolver;
import de.unika.ipd.grgen.ast.util.DeclarationTypeResolver;
import de.unika.ipd.grgen.ir.IR;
import de.unika.ipd.grgen.ir.ExternalType;

/**
 * A class representing a node type
 */
public class ExternalTypeNode extends InheritanceTypeNode {
	static {
		setName(ExternalTypeNode.class, "external type");
	}

	private CollectNode<ExternalTypeNode> extend;

	/**
	 * Create a new external type
	 * @param ext The collect node containing the types which are extended by this type.
	 */
	public ExternalTypeNode(CollectNode<IdentNode> ext) {
		this.extendUnresolved = ext;
		becomeParent(this.extendUnresolved);
		body = new CollectNode<BaseNode>();
	}

	/** returns children of this node */
	@Override
	public Collection<BaseNode> getChildren() {
		Vector<BaseNode> children = new Vector<BaseNode>();
		children.add(getValidVersion(extendUnresolved, extend));
		return children;
	}

	/** returns names of the children, same order as in getChildren */
	@Override
	public Collection<String> getChildrenNames() {
		Vector<String> childrenNames = new Vector<String>();
		childrenNames.add("extends");
		return childrenNames;
	}

	private static final CollectResolver<ExternalTypeNode> extendResolver =	new CollectResolver<ExternalTypeNode>(
		new DeclarationTypeResolver<ExternalTypeNode>(ExternalTypeNode.class));

	/** @see de.unika.ipd.grgen.ast.BaseNode#resolveLocal() */
	@Override
	protected boolean resolveLocal() {
		extend = extendResolver.resolve(extendUnresolved, this);

		// Initialize direct sub types
		if (extend != null) {
			for (InheritanceTypeNode type : extend.getChildren()) {
				type.addDirectSubType(this);
			}
		}

		return extend != null;
	}

	/**
	 * Get the IR external type for this AST node.
	 * @return The correctly casted IR external type.
	 */
	protected ExternalType getExternalType() {
		return checkIR(ExternalType.class);
	}

	/**
	 * Construct IR object for this AST node.
	 * @see de.unika.ipd.grgen.ast.BaseNode#constructIR()
	 */
	@Override
	protected IR constructIR() {
		ExternalType et = new ExternalType(getDecl().getIdentNode().getIdent());

		constructIR(et);

		return et;
	}

	protected CollectNode<? extends InheritanceTypeNode> getExtends() {
		return extend;
	}

	@Override
	protected void doGetCompatibleToTypes(Collection<TypeNode> coll) {
		assert isResolved();

		for(ExternalTypeNode inh : extend.getChildren()) {
			coll.add(inh);
			coll.addAll(inh.getCompatibleToTypes());
		}
    }

	public static String getKindStr() {
		return "external type";
	}

	public static String getUseStr() {
		return "external type";
	}

	@Override
	protected Collection<ExternalTypeNode> getDirectSuperTypes() {
		assert isResolved();

	    return extend.getChildren();
    }

	@Override
	protected void getMembers(Map<String, DeclNode> members) {
	}
}

