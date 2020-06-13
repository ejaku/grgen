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
import de.unika.ipd.grgen.ast.type.executable.ExternalProcedureTypeNode;
import de.unika.ipd.grgen.ast.util.CollectResolver;
import de.unika.ipd.grgen.ast.util.DeclarationTypeResolver;
import de.unika.ipd.grgen.ir.IR;
import de.unika.ipd.grgen.ir.executable.ExternalProcedure;
import de.unika.ipd.grgen.ir.executable.ExternalProcedureMethod;
import de.unika.ipd.grgen.ir.type.Type;

import java.util.Collection;
import java.util.Vector;

/**
 * AST node class representing external procedure declarations
 */
public class ExternalProcedureDeclNode extends ProcedureDeclBaseNode
{
	static {
		setName(ExternalProcedureDeclNode.class, "external procedure declaration");
	}

	protected CollectNode<BaseNode> parameterTypesUnresolved;
	protected CollectNode<TypeNode> parameterTypesCollectNode;

	boolean isMethod;

	private static final ExternalProcedureTypeNode externalProcedureType = new ExternalProcedureTypeNode();


	public ExternalProcedureDeclNode(IdentNode id, CollectNode<BaseNode> paramTypesUnresolved,
			CollectNode<BaseNode> rets, boolean isMethod)
	{
		super(id, externalProcedureType);
		this.parameterTypesUnresolved = paramTypesUnresolved;
		becomeParent(this.parameterTypesUnresolved);
		this.resultsUnresolved = rets;
		becomeParent(this.resultsUnresolved);
		this.isMethod = isMethod;
	}

	/** returns children of this node */
	@Override
	public Collection<BaseNode> getChildren()
	{
		Vector<BaseNode> children = new Vector<BaseNode>();
		children.add(ident);
		children.add(getValidVersion(parameterTypesUnresolved, parameterTypesCollectNode));
		children.add(getValidVersion(resultsUnresolved, resultTypesCollectNode));
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

	private static final CollectResolver<TypeNode> parametersTypeResolver =
			new CollectResolver<TypeNode>(new DeclarationTypeResolver<TypeNode>(TypeNode.class));

	/** @see de.unika.ipd.grgen.ast.BaseNode#resolveLocal() */
	@Override
	protected boolean resolveLocal()
	{
		parameterTypesCollectNode = parametersTypeResolver.resolve(parameterTypesUnresolved, this);
		
		parameterTypes = parameterTypesCollectNode.getChildrenAsVector();
		
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

		return externalProcedureType;
	}

	@Override
	protected IR constructIR()
	{
		ExternalProcedure externalProc = isMethod
				? new ExternalProcedureMethod(getIdentNode().toString(), getIdentNode().getIdent())
				: new ExternalProcedure(getIdentNode().toString(), getIdentNode().getIdent());
		for(TypeNode retType : resultTypesCollectNode.getChildren()) {
			externalProc.addReturnType(retType.checkIR(Type.class));
		}
		for(TypeNode param : parameterTypesCollectNode.getChildren()) {
			externalProc.addParameterType(param.checkIR(Type.class));
		}
		return externalProc;
	}

	public static String getKindStr()
	{
		return "procedure";
	}
}
