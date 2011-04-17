/*
 * GrGen: graph rewrite generator tool -- release GrGen.NET 3.0
 * Copyright (C) 2003-2011 Universitaet Karlsruhe, Institut fuer Programmstrukturen und Datenorganisation, LS Goos
 * licensed under LGPL v3 (see LICENSE.txt included in the packaging of this file)
 * www.grgen.net
 */

/**
 * @author Edgar Jakumeit
 * @version $Id$
 */
package de.unika.ipd.grgen.ast;

import de.unika.ipd.grgen.ast.util.CollectResolver;
import de.unika.ipd.grgen.ast.util.DeclarationTypeResolver;
import de.unika.ipd.grgen.ast.util.Resolver;
import de.unika.ipd.grgen.ir.IR;
import de.unika.ipd.grgen.ir.ExternalFunction;
import de.unika.ipd.grgen.ir.Type;

import java.util.Collection;
import java.util.Vector;


/**
 * AST node class representing external function declarations
 */
public class ExternalFunctionDeclNode extends DeclNode {
	static {
		setName(ExternalFunctionDeclNode.class, "external function declaration");
	}

	protected CollectNode<BaseNode> paramsUnresolved;
	protected CollectNode<TypeNode> params;
	protected BaseNode retUnresolved;
	protected TypeNode ret;

	private static final ExternalFunctionTypeNode externalFunctionType = new ExternalFunctionTypeNode();

	public ExternalFunctionDeclNode(IdentNode id, CollectNode<BaseNode> params, BaseNode ret) {
		super(id, externalFunctionType);
		this.paramsUnresolved = params;
		becomeParent(this.paramsUnresolved);
		this.retUnresolved = ret;
		becomeParent(this.retUnresolved);
	}

	/** returns children of this node */
	@Override
	public Collection<BaseNode> getChildren() {
		Vector<BaseNode> children = new Vector<BaseNode>();
		children.add(ident);
		children.add(getValidVersion(paramsUnresolved, params));
		children.add(getValidVersion(retUnresolved, ret));
		return children;
	}

	/** returns names of the children, same order as in getChildren */
	@Override
	public Collection<String> getChildrenNames() {
		Vector<String> childrenNames = new Vector<String>();
		childrenNames.add("ident");
		childrenNames.add("params");
		childrenNames.add("ret");
		return childrenNames;
	}

	private static final CollectResolver<TypeNode> paramsTypeResolver = new CollectResolver<TypeNode>(
    		new DeclarationTypeResolver<TypeNode>(TypeNode.class));
	private static final Resolver<TypeNode> retTypeResolver = 
    		new DeclarationTypeResolver<TypeNode>(TypeNode.class);

	/** @see de.unika.ipd.grgen.ast.BaseNode#resolveLocal() */
	@Override
	protected boolean resolveLocal() {
		params = paramsTypeResolver.resolve(paramsUnresolved, this);
		ret = retTypeResolver.resolve(retUnresolved, this);

		return params != null && ret != null;
	}

	/** @see de.unika.ipd.grgen.ast.BaseNode#resolveLocal() */
	@Override
	protected boolean checkLocal() {
		return true;
	}
	
	@Override
	public TypeNode getDeclType() {
		assert isResolved();

		return externalFunctionType;
	}

	@Override
	protected IR constructIR() {
		ExternalFunction externalFunc = new ExternalFunction(getIdentNode().toString(), 
				getIdentNode().getIdent(), ret.checkIR(Type.class));
		for(TypeNode param : params.getChildren()) {
			externalFunc.addParameterType(param.checkIR(Type.class));
		}
		return externalFunc;
	}
}


