/*
 * GrGen: graph rewrite generator tool -- release GrGen.NET 5.0
 * Copyright (C) 2003-2020 Universitaet Karlsruhe, Institut fuer Programmstrukturen und Datenorganisation, LS Goos; and free programmers
 * licensed under LGPL v3 (see LICENSE.txt included in the packaging of this file)
 * www.grgen.net
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
import de.unika.ipd.grgen.ast.util.Resolver;
import de.unika.ipd.grgen.ir.IR;
import de.unika.ipd.grgen.ir.executable.ExternalFunction;
import de.unika.ipd.grgen.ir.executable.ExternalFunctionMethod;
import de.unika.ipd.grgen.ir.type.Type;

import java.util.Collection;
import java.util.Vector;

/**
 * AST node class representing external function declarations
 */
public class ExternalFunctionDeclNode extends FunctionOrOperatorDeclBaseNode
{
	static {
		setName(ExternalFunctionDeclNode.class, "external function declaration");
	}

	protected BaseNode resultUnresolved;

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
		Vector<BaseNode> children = new Vector<BaseNode>();
		children.add(ident);
		children.add(getValidVersion(parameterTypesUnresolved, parameterTypesCollectNode));
		children.add(getValidVersion(resultUnresolved, resultType));
		return children;
	}

	/** returns names of the children, same order as in getChildren */
	@Override
	public Collection<String> getChildrenNames()
	{
		Vector<String> childrenNames = new Vector<String>();
		childrenNames.add("ident");
		childrenNames.add("paramTypes");
		childrenNames.add("ret");
		return childrenNames;
	}

	private static final Resolver<TypeNode> resultTypeResolver =
			new DeclarationTypeResolver<TypeNode>(TypeNode.class);
	private static final CollectResolver<TypeNode> parametersTypeResolver =
			new CollectResolver<TypeNode>(new DeclarationTypeResolver<TypeNode>(TypeNode.class));

	/** @see de.unika.ipd.grgen.ast.BaseNode#resolveLocal() */
	@Override
	protected boolean resolveLocal()
	{
		resultType = resultTypeResolver.resolve(resultUnresolved, this);

		parameterTypesCollectNode = parametersTypeResolver.resolve(parameterTypesUnresolved, this);
		
		parameterTypes = parameterTypesCollectNode.getChildrenAsVector();
		
		return parameterTypesCollectNode != null & resultType != null;
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
				? new ExternalFunctionMethod(getIdentNode().toString(), getIdentNode().getIdent(), resultType.checkIR(Type.class))
				: new ExternalFunction(getIdentNode().toString(), getIdentNode().getIdent(), resultType.checkIR(Type.class));
		for(TypeNode param : parameterTypesCollectNode.getChildren()) {
			externalFunc.addParameterType(param.checkIR(Type.class));
		}
		return externalFunc;
	}

	public static String getKindStr()
	{
		return "external function declaration";
	}

	public static String getUseStr()
	{
		return "external function";
	}
}
