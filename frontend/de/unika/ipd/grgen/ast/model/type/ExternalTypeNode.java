/*
 * GrGen: graph rewrite generator tool -- release GrGen.NET 5.0
 * Copyright (C) 2003-2020 Universitaet Karlsruhe, Institut fuer Programmstrukturen und Datenorganisation, LS Goos; and free programmers
 * licensed under LGPL v3 (see LICENSE.txt included in the packaging of this file)
 * www.grgen.net
 */

/**
 * @author Edgar Jakumeit
 */

package de.unika.ipd.grgen.ast.model.type;

import java.util.Collection;
import java.util.Map;
import java.util.Vector;

import de.unika.ipd.grgen.ast.*;
import de.unika.ipd.grgen.ast.decl.DeclNode;
import de.unika.ipd.grgen.ast.decl.ExternalFunctionDeclNode;
import de.unika.ipd.grgen.ast.decl.ExternalProcedureDeclNode;
import de.unika.ipd.grgen.ast.type.OperatorSignature;
import de.unika.ipd.grgen.ast.type.TypeNode;
import de.unika.ipd.grgen.ast.type.basic.BasicTypeNode;
import de.unika.ipd.grgen.ast.util.CollectResolver;
import de.unika.ipd.grgen.ast.util.DeclarationResolver;
import de.unika.ipd.grgen.ast.util.DeclarationTypeResolver;
import de.unika.ipd.grgen.ir.ExternalFunctionMethod;
import de.unika.ipd.grgen.ir.ExternalProcedureMethod;
import de.unika.ipd.grgen.ir.IR;
import de.unika.ipd.grgen.ir.model.type.ExternalType;
import de.unika.ipd.grgen.ir.model.type.InheritanceType;

/**
 * A class representing a node type
 */
public class ExternalTypeNode extends InheritanceTypeNode
{
	static {
		setName(ExternalTypeNode.class, "external type");
	}

	private CollectNode<ExternalTypeNode> extend;

	/**
	 * Create a new external type
	 * @param ext The collect node containing the types which are extended by this type.
	 */
	public ExternalTypeNode(CollectNode<IdentNode> ext, CollectNode<BaseNode> body)
	{
		this.extendUnresolved = ext;
		becomeParent(this.extendUnresolved);
		this.bodyUnresolved = body;
		becomeParent(this.bodyUnresolved);

		// allow the conditional operator on the external type
		OperatorSignature.makeOp(OperatorSignature.COND, this,
				new TypeNode[] { BasicTypeNode.booleanType, this, this }, OperatorSignature.condEvaluator);
	}

	/** returns children of this node */
	@Override
	public Collection<BaseNode> getChildren()
	{
		Vector<BaseNode> children = new Vector<BaseNode>();
		children.add(getValidVersion(extendUnresolved, extend));
		children.add(getValidVersion(bodyUnresolved, body));
		return children;
	}

	/** returns names of the children, same order as in getChildren */
	@Override
	public Collection<String> getChildrenNames()
	{
		Vector<String> childrenNames = new Vector<String>();
		childrenNames.add("extends");
		childrenNames.add("body");
		return childrenNames;
	}

	private static final CollectResolver<ExternalTypeNode> extendResolver =
			new CollectResolver<ExternalTypeNode>(new DeclarationTypeResolver<ExternalTypeNode>(ExternalTypeNode.class));

	@SuppressWarnings("unchecked")
	private static final CollectResolver<BaseNode> bodyResolver =
			new CollectResolver<BaseNode>(new DeclarationResolver<BaseNode>(ExternalFunctionDeclNode.class, ExternalProcedureDeclNode.class));

	/** @see de.unika.ipd.grgen.ast.BaseNode#resolveLocal() */
	@Override
	protected boolean resolveLocal()
	{
		body = bodyResolver.resolve(bodyUnresolved, this);
		extend = extendResolver.resolve(extendUnresolved, this);

		// Initialize direct sub types
		if(extend != null) {
			for(InheritanceTypeNode type : extend.getChildren()) {
				type.addDirectSubType(this);
			}
		}

		return body != null && extend != null;
	}

	/**
	 * Get the IR external type for this AST node.
	 * @return The correctly casted IR external type.
	 */
	protected ExternalType getExternalType()
	{
		return checkIR(ExternalType.class);
	}

	/**
	 * Construct IR object for this AST node.
	 * @see de.unika.ipd.grgen.ast.BaseNode#constructIR()
	 */
	@Override
	protected IR constructIR()
	{
		if(isIRAlreadySet()) { // break endless recursion in case of a member of node/edge type
			return getIR();
		}

		ExternalType et = new ExternalType(getDecl().getIdentNode().getIdent());

		setIR(et);

		constructIR(et);

		return et;
	}

	protected void constructIR(ExternalType extType)
	{
		for(BaseNode child : body.getChildren()) {
			if(child instanceof ExternalFunctionDeclNode) {
				extType.addExternalFunctionMethod(child.checkIR(ExternalFunctionMethod.class));
			} else {
				extType.addExternalProcedureMethod(child.checkIR(ExternalProcedureMethod.class));
			}
		}
		for(InheritanceTypeNode inh : getExtends().getChildren()) {
			extType.addDirectSuperType((InheritanceType)inh.getType());
		}
	}

	protected CollectNode<? extends InheritanceTypeNode> getExtends()
	{
		return extend;
	}

	@Override
	public void doGetCompatibleToTypes(Collection<TypeNode> coll)
	{
		assert isResolved();

		for(ExternalTypeNode inh : extend.getChildren()) {
			coll.add(inh);
			coll.addAll(inh.getCompatibleToTypes());
		}
	}

	public static String getKindStr()
	{
		return "external type";
	}

	public static String getUseStr()
	{
		return "external type";
	}

	@Override
	public Collection<ExternalTypeNode> getDirectSuperTypes()
	{
		assert isResolved();

		return extend.getChildren();
	}

	@Override
	protected void getMembers(Map<String, DeclNode> members)
	{
		for(BaseNode child : body.getChildren()) {
			if(child instanceof ExternalFunctionDeclNode) {
				ExternalFunctionDeclNode function = (ExternalFunctionDeclNode)child;
				checkExternalFunctionOverride(function);
			} else if(child instanceof ExternalProcedureDeclNode) {
				ExternalProcedureDeclNode procedure = (ExternalProcedureDeclNode)child;
				checkExternalProcedureOverride(procedure);
			}
		}
	}

	private void checkExternalFunctionOverride(ExternalFunctionDeclNode function)
	{
		for(InheritanceTypeNode base : getAllSuperTypes()) {
			for(BaseNode baseChild : base.getBody().getChildren()) {
				if(baseChild instanceof ExternalFunctionDeclNode) {
					ExternalFunctionDeclNode functionBase = (ExternalFunctionDeclNode)baseChild;
					if(function.ident.toString().equals(functionBase.ident.toString()))
						checkSignatureAdhered(functionBase, function);
				}
			}
		}
	}
	
	private void checkExternalProcedureOverride(ExternalProcedureDeclNode procedure)
	{
		for(InheritanceTypeNode base : getAllSuperTypes()) {
			for(BaseNode baseChild : base.getBody().getChildren()) {
				if(baseChild instanceof ExternalProcedureDeclNode) {
					ExternalProcedureDeclNode procedureBase = (ExternalProcedureDeclNode)baseChild;
					if(procedure.ident.toString().equals(procedureBase.ident.toString()))
						checkSignatureAdhered(procedureBase, procedure);
				}
			}
		}
	}
}
