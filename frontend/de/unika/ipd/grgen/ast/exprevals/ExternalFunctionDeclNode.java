/*
 * GrGen: graph rewrite generator tool -- release GrGen.NET 4.4
 * Copyright (C) 2003-2014 Universitaet Karlsruhe, Institut fuer Programmstrukturen und Datenorganisation, LS Goos; and free programmers
 * licensed under LGPL v3 (see LICENSE.txt included in the packaging of this file)
 * www.grgen.net
 */

/**
 * @author Edgar Jakumeit
 */

package de.unika.ipd.grgen.ast.exprevals;

import de.unika.ipd.grgen.ast.*;
import de.unika.ipd.grgen.ast.util.CollectResolver;
import de.unika.ipd.grgen.ast.util.DeclarationTypeResolver;
import de.unika.ipd.grgen.ir.IR;
import de.unika.ipd.grgen.ir.exprevals.ExternalFunction;
import de.unika.ipd.grgen.ir.exprevals.ExternalFunctionMethod;
import de.unika.ipd.grgen.ir.Type;

import java.util.Collection;
import java.util.Vector;


/**
 * AST node class representing external function declarations
 */
public class ExternalFunctionDeclNode extends FunctionBase {
	static {
		setName(ExternalFunctionDeclNode.class, "external function declaration");
	}

	protected CollectNode<BaseNode> paramTypesUnresolved;
	protected CollectNode<TypeNode> paramTypes;

	private static final ExternalFunctionTypeNode externalFunctionType =
		new ExternalFunctionTypeNode();

	boolean isMethod;

	public ExternalFunctionDeclNode(IdentNode id, CollectNode<BaseNode> paramTypesUnresolved, BaseNode ret, boolean isMethod) {
		super(id, externalFunctionType);
		this.paramTypesUnresolved = paramTypesUnresolved;
		becomeParent(this.paramTypesUnresolved);
		this.retUnresolved = ret;
		becomeParent(this.retUnresolved);
		this.isMethod = isMethod;
	}

	/** returns children of this node */
	@Override
	public Collection<BaseNode> getChildren() {
		Vector<BaseNode> children = new Vector<BaseNode>();
		children.add(ident);
		children.add(getValidVersion(paramTypesUnresolved, paramTypes));
		children.add(getValidVersion(retUnresolved, ret));
		return children;
	}

	/** returns names of the children, same order as in getChildren */
	@Override
	public Collection<String> getChildrenNames() {
		Vector<String> childrenNames = new Vector<String>();
		childrenNames.add("ident");
		childrenNames.add("paramTypes");
		childrenNames.add("ret");
		return childrenNames;
	}

	private static final CollectResolver<TypeNode> paramsTypeResolver = new CollectResolver<TypeNode>(
    		new DeclarationTypeResolver<TypeNode>(TypeNode.class));

	/** @see de.unika.ipd.grgen.ast.BaseNode#resolveLocal() */
	@Override
	protected boolean resolveLocal() {
		paramTypes = paramsTypeResolver.resolve(paramTypesUnresolved, this);	
		return paramTypes != null && super.resolveLocal();
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

	public Vector<TypeNode> getParameterTypes() {
		assert isResolved();

		return paramTypes.children;
	}

	@Override
	protected IR constructIR() {
		ExternalFunction externalFunc = isMethod ? 
				new ExternalFunctionMethod(getIdentNode().toString(), getIdentNode().getIdent(), ret.checkIR(Type.class)) :
				new ExternalFunction(getIdentNode().toString(), getIdentNode().getIdent(), ret.checkIR(Type.class));
		for(TypeNode param : paramTypes.getChildren()) {
			externalFunc.addParameterType(param.checkIR(Type.class));
		}
		return externalFunc;
	}
	
	public static String getKindStr() {
		return "external function declaration";
	}

	public static String getUseStr() {
		return "external function";
	}
}


