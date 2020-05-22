/*
 * GrGen: graph rewrite generator tool -- release GrGen.NET 5.0
 * Copyright (C) 2003-2020 Universitaet Karlsruhe, Institut fuer Programmstrukturen und Datenorganisation, LS Goos; and free programmers
 * licensed under LGPL v3 (see LICENSE.txt included in the packaging of this file)
 * www.grgen.net
 */

/**
 * @author Moritz Kroll
 */

package de.unika.ipd.grgen.ast.decl;

import java.util.Collection;
import java.util.LinkedHashSet;
import java.util.Vector;

import de.unika.ipd.grgen.ast.BaseNode;
import de.unika.ipd.grgen.ast.CollectNode;
import de.unika.ipd.grgen.ast.ConstructorParamNode;
import de.unika.ipd.grgen.ast.IdentNode;
import de.unika.ipd.grgen.ast.type.ConstructorTypeNode;
import de.unika.ipd.grgen.ast.type.TypeNode;
import de.unika.ipd.grgen.ir.Constructor;
import de.unika.ipd.grgen.ir.ConstructorParam;
import de.unika.ipd.grgen.ir.IR;

/**
 * A compound type constructor declaration.
 */
public class ConstructorDeclNode extends DeclNode
{
	static {
		setName(ConstructorDeclNode.class, "constructor declaration");
	}

	private static final TypeNode constructorType = new ConstructorTypeNode();

	private CollectNode<ConstructorParamNode> parameters;

	public ConstructorDeclNode(IdentNode n, CollectNode<ConstructorParamNode> params)
	{
		super(n, constructorType);

		parameters = becomeParent(params);
	}

	public TypeNode getDeclType()
	{
		return constructorType;
	}

	@Override
	protected boolean checkLocal()
	{
		return true; // nothing to be checked locally
	}

	@Override
	public Collection<BaseNode> getChildren()
	{
		Vector<BaseNode> children = new Vector<BaseNode>();
		children.add(parameters);
		return children;
	}

	@Override
	public Collection<String> getChildrenNames()
	{
		Vector<String> childrenNames = new Vector<String>();
		childrenNames.add("parameters");
		return childrenNames;
	}

	public CollectNode<ConstructorParamNode> getParameters()
	{
		return parameters;
	}

	@Override
	protected boolean resolveLocal()
	{
		return true; // nothing to be resolved locally
	}

	public static String getKindStr()
	{
		return "constructor declaration";
	}

	public static String getUseStr()
	{
		return "constructor access";
	}

	public Constructor getConstructor()
	{
		return checkIR(Constructor.class);
	}

	@Override
	protected IR constructIR()
	{
		LinkedHashSet<ConstructorParam> params = new LinkedHashSet<ConstructorParam>();
		for(ConstructorParamNode param : parameters.getChildren()) {
			params.add(param.checkIR(ConstructorParam.class));
		}

		return new Constructor(params);
	}
}
