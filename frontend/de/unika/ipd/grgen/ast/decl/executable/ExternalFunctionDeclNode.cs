/*
 * GrGen: graph rewrite generator tool -- release GrGen.NET 8.1
 * Copyright (C) 2003-2026 Universitaet Karlsruhe, Institut fuer Programmstrukturen und Datenorganisation, LS Goos; and free programmers
 * licensed under LGPL v3, some components/parts use different licenses (see LICENSE.txt included in the packaging of this file)
 * www.grgen.de / www.grgen.net
 */

/**
 * @author Edgar Jakumeit
 */

package de.unika.ipd.grgen.ast.decl.executable;

import de.unika.ipd.grgen.ast.*;
import de.unika.ipd.grgen.ast.type.TypeNode;
import de.unika.ipd.grgen.ast.type.executable.ExternalFunctionTypeNode;
import de.unika.ipd.grgen.ast.util.CollectResolver;
import de.unika.ipd.grgen.ast.util.DeclarationTypeResolver;
import de.unika.ipd.grgen.ir.IR;
import de.unika.ipd.grgen.ir.executable.ExternalFunction;
import de.unika.ipd.grgen.ir.executable.ExternalFunctionMethod;
import de.unika.ipd.grgen.ir.type.Type;

import java.util.Collection;
import java.util.List;
import java.util.ArrayList;

/**
 * AST node class representing external function declarations
 */
public class ExternalFunctionDeclNode extends FunctionDeclBaseNode
{
	static {
		setClassName(ExternalFunctionDeclNode.class, "external function declaration");
	}

	protected CollectNode<BaseNode> parameterTypesUnresolved;
	protected CollectNode<TypeNode> parameterTypesCollectNode;

	boolean isMethod;

	private static final ExternalFunctionTypeNode externalFunctionType = new ExternalFunctionTypeNode();


	public ExternalFunctionDeclNode(IdentNode id, CollectNode<BaseNode> paramTypesUnresolved, BaseNode ret,
			boolean isMethod)
	{
		super(id, externalFunctionType);
		this.parameterTypesUnresolved = paramTypesUnresolved;
		becomeParent(this.parameterTypesUnresolved);
		this.resultUnresolved = ret;
		becomeParent(this.resultUnresolved);
		this.isMethod = isMethod;
	}

	/** returns children of this node */
	@Override
	public Collection<BaseNode> getChildren()
	{
		List<BaseNode> children = new ArrayList<BaseNode>();
		children.add(ident);
		children.add(getValidVersionCollectNode(parameterTypesUnresolved, parameterTypesCollectNode));
		children.add(getValidVersion(resultUnresolved, resultType));
		return children;
	}

	/** returns names of the children, same order as in getChildren */
	@Override
	public Collection<String> getChildrenNames()
	{
		List<String> childrenNames = new ArrayList<String>();
		childrenNames.add("ident");
		childrenNames.add("paramTypes");
		childrenNames.add("ret");
		return childrenNames;
	}

	private static final CollectResolver<TypeNode> parametersTypeResolver =
			new CollectResolver<TypeNode>(new DeclarationTypeResolver<TypeNode>(TypeNode.class));

	/** @see de.unika.ipd.grgen.ast.BaseNode#resolveLocal() */
	@Override
	protected boolean resolveLocal()
	{
		parameterTypesCollectNode = parametersTypeResolver.resolve(parameterTypesUnresolved, this);
		
		parameterTypes = parameterTypesCollectNode.getChildrenAsList();
		
		return parameterTypesCollectNode != null & super.resolveLocal();
	}

	/** @see de.unika.ipd.grgen.ast.BaseNode#resolveLocal() */
	@Override
	protected boolean checkLocal()
	{
		return true;
	}

	@Override
	public TypeNode getDeclType()
	{
		assert isResolved();
		return externalFunctionType;
	}

	@Override
	protected IR constructIR()
	{
		ExternalFunction externalFunc = isMethod
				? new ExternalFunctionMethod(getIdent().toString(), getIdent().getIRIdent(), resultType.checkIR(Type.class))
				: new ExternalFunction(getIdent().toString(), getIdent().getIRIdent(), resultType.checkIR(Type.class));
		for(TypeNode param : parameterTypesCollectNode.getChildrenExact()) {
			externalFunc.addParameterType(param.checkIR(Type.class));
		}
		return externalFunc;
	}

	public static String getKindStr()
	{
		return "external function";
	}
}
