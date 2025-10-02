/*
 * GrGen: graph rewrite generator tool -- release GrGen.NET 8.0
 * Copyright (C) 2003-2025 Universitaet Karlsruhe, Institut fuer Programmstrukturen und Datenorganisation, LS Goos; and free programmers
 * licensed under LGPL v3 (see LICENSE.txt included in the packaging of this file)
 * www.grgen.de / www.grgen.net
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
import de.unika.ipd.grgen.ast.decl.executable.ExternalFunctionDeclNode;
import de.unika.ipd.grgen.ast.decl.executable.ExternalProcedureDeclNode;
import de.unika.ipd.grgen.ast.decl.executable.OperatorDeclNode;
import de.unika.ipd.grgen.ast.decl.executable.OperatorEvaluator;
import de.unika.ipd.grgen.ast.decl.executable.OperatorDeclNode.Operator;
import de.unika.ipd.grgen.ast.type.TypeNode;
import de.unika.ipd.grgen.ast.type.basic.BasicTypeNode;
import de.unika.ipd.grgen.ast.util.CollectResolver;
import de.unika.ipd.grgen.ast.util.DeclarationResolver;
import de.unika.ipd.grgen.ast.util.DeclarationTypeResolver;
import de.unika.ipd.grgen.ir.IR;
import de.unika.ipd.grgen.ir.executable.ExternalFunctionMethod;
import de.unika.ipd.grgen.ir.executable.ExternalProcedureMethod;
import de.unika.ipd.grgen.ir.model.type.ExternalObjectType;

/**
 * A class representing an external object type
 */
public class ExternalObjectTypeNode extends InheritanceTypeNode
{
	static {
		setName(ExternalObjectTypeNode.class, "external object type");
	}

	private CollectNode<ExternalObjectTypeNode> extend;

	/**
	 * Create a new external object type
	 * @param ext The collect node containing the types which are extended by this type.
	 */
	public ExternalObjectTypeNode(CollectNode<IdentNode> ext, CollectNode<BaseNode> body)
	{
		this.extendUnresolved = ext;
		becomeParent(this.extendUnresolved);
		this.bodyUnresolved = body;
		becomeParent(this.bodyUnresolved);

		// allow the conditional operator on the external type
		OperatorDeclNode.makeOp(OperatorDeclNode.Operator.COND, this,
				new TypeNode[] { BasicTypeNode.booleanType, this, this }, OperatorEvaluator.condEvaluator);
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

	private static final CollectResolver<ExternalObjectTypeNode> extendResolver =
			new CollectResolver<ExternalObjectTypeNode>(new DeclarationTypeResolver<ExternalObjectTypeNode>(ExternalObjectTypeNode.class));

	private static final CollectResolver<BaseNode> bodyResolver =
			new CollectResolver<BaseNode>(new DeclarationResolver<BaseNode>(ExternalFunctionDeclNode.class, ExternalProcedureDeclNode.class));

	/** @see de.unika.ipd.grgen.ast.BaseNode#resolveLocal() */
	@Override
	protected boolean resolveLocal()
	{
		OperatorDeclNode.makeOp(Operator.COND, this, new TypeNode[] { BasicTypeNode.booleanType, this, this }, OperatorEvaluator.condEvaluator);
		OperatorDeclNode.makeBinOp(Operator.EQ, BasicTypeNode.booleanType, this, this, OperatorEvaluator.emptyEvaluator);
		OperatorDeclNode.makeBinOp(Operator.NE, BasicTypeNode.booleanType, this, this, OperatorEvaluator.emptyEvaluator);
		OperatorDeclNode.makeBinOp(Operator.GE, BasicTypeNode.booleanType, this, this, OperatorEvaluator.emptyEvaluator);
		OperatorDeclNode.makeBinOp(Operator.GT, BasicTypeNode.booleanType, this, this, OperatorEvaluator.emptyEvaluator);
		OperatorDeclNode.makeBinOp(Operator.LE, BasicTypeNode.booleanType, this, this, OperatorEvaluator.emptyEvaluator);
		OperatorDeclNode.makeBinOp(Operator.LT, BasicTypeNode.booleanType, this, this, OperatorEvaluator.emptyEvaluator);

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
	 * Get the IR external object type for this AST node.
	 * @return The correctly casted IR external object type.
	 */
	protected ExternalObjectType getExternalObjectType()
	{
		return checkIR(ExternalObjectType.class);
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

		ExternalObjectType et = new ExternalObjectType(getDecl().getIdentNode().getIdent());

		setIR(et);

		constructIR(et);

		return et;
	}

	protected void constructIR(ExternalObjectType extType)
	{
		for(BaseNode child : body.getChildren()) {
			if(child instanceof ExternalFunctionDeclNode) {
				extType.addExternalFunctionMethod(child.checkIR(ExternalFunctionMethod.class));
			} else {
				extType.addExternalProcedureMethod(child.checkIR(ExternalProcedureMethod.class));
			}
		}
		for(InheritanceTypeNode inh : getExtends().getChildren()) {
			extType.addDirectSuperType(inh.getType());
		}
	}

	@Override
	protected CollectNode<? extends InheritanceTypeNode> getExtends()
	{
		return extend;
	}

	@Override
	public void doGetCompatibleToTypes(Collection<TypeNode> coll)
	{
		assert isResolved();

		for(ExternalObjectTypeNode inh : extend.getChildren()) {
			coll.add(inh);
			coll.addAll(inh.getCompatibleToTypes());
		}
	}

	public static String getKindStr()
	{
		return "external class";
	}

	@Override
	public Collection<ExternalObjectTypeNode> getDirectSuperTypes()
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
