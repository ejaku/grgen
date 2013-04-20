/*
 * GrGen: graph rewrite generator tool -- release GrGen.NET 4.0
 * Copyright (C) 2003-2013 Universitaet Karlsruhe, Institut fuer Programmstrukturen und Datenorganisation, LS Goos; and free programmers
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
import de.unika.ipd.grgen.ir.Type;
import de.unika.ipd.grgen.ir.exprevals.ExternalProcedure;

import java.util.Collection;
import java.util.Vector;


/**
 * AST node class representing external procedure declarations
 */
public class ExternalProcedureDeclNode extends ProcedureBase {
	static {
		setName(ExternalProcedureDeclNode.class, "external procedure declaration");
	}

	protected CollectNode<BaseNode> paramTypesUnresolved;
	protected CollectNode<TypeNode> paramTypes;

	private static final ExternalProcedureTypeNode externalProcedureType = 
		new ExternalProcedureTypeNode();

	public ExternalProcedureDeclNode(IdentNode id, CollectNode<BaseNode> paramTypesUnresolved, CollectNode<BaseNode> rets) {
		super(id, externalProcedureType);
		this.paramTypesUnresolved = paramTypesUnresolved;
		becomeParent(this.paramTypesUnresolved);
		this.retsUnresolved = rets;
		becomeParent(this.retsUnresolved);
	}

	/** returns children of this node */
	@Override
	public Collection<BaseNode> getChildren() {
		Vector<BaseNode> children = new Vector<BaseNode>();
		children.add(ident);
		children.add(getValidVersion(paramTypesUnresolved, paramTypes));
		children.add(getValidVersion(retsUnresolved, returnTypes));
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
	
		return externalProcedureType;
	}

	public Vector<TypeNode> getParameterTypes() {
		assert isResolved();

		return paramTypes.children;
	}

	@Override
	protected IR constructIR() {
		ExternalProcedure externalProc = new ExternalProcedure(getIdentNode().toString(),
				getIdentNode().getIdent());
		for(TypeNode retType : returnTypes.getChildren()) {
			externalProc.addReturnType(retType.checkIR(Type.class));
		}
		for(TypeNode param : paramTypes.getChildren()) {
			externalProc.addParameterType(param.checkIR(Type.class));
		}
		return externalProc;
	}
	
	public static String getKindStr() {
		return "external procedure declaration";
	}

	public static String getUseStr() {
		return "procedure";
	}
}


