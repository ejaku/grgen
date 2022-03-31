/*
 * GrGen: graph rewrite generator tool -- release GrGen.NET 6.6
 * Copyright (C) 2003-2022 Universitaet Karlsruhe, Institut fuer Programmstrukturen und Datenorganisation, LS Goos; and free programmers
 * licensed under LGPL v3 (see LICENSE.txt included in the packaging of this file)
 * www.grgen.net
 */

/**
 * @author Edgar Jakumeit
 */

package de.unika.ipd.grgen.ast.decl.executable;

import java.util.Collection;
import java.util.Vector;

import de.unika.ipd.grgen.ast.*;
import de.unika.ipd.grgen.ast.decl.DeclNode;
import de.unika.ipd.grgen.ast.decl.pattern.NodeDeclNode;
import de.unika.ipd.grgen.ast.decl.pattern.VarDeclNode;
import de.unika.ipd.grgen.ast.pattern.ConnectionNode;
import de.unika.ipd.grgen.ast.pattern.SingleNodeConnNode;
import de.unika.ipd.grgen.ast.stmt.EvalStatementNode;
import de.unika.ipd.grgen.ast.type.TypeNode;
import de.unika.ipd.grgen.ast.type.basic.ErrorTypeNode;
import de.unika.ipd.grgen.ast.type.executable.FunctionTypeNode;
import de.unika.ipd.grgen.ir.stmt.EvalStatement;
import de.unika.ipd.grgen.ir.type.Type;
import de.unika.ipd.grgen.ir.Entity;
import de.unika.ipd.grgen.ir.IR;
import de.unika.ipd.grgen.ir.executable.Function;
import de.unika.ipd.grgen.ir.executable.FunctionMethod;

/**
 * AST node class representing function declarations
 */
public class FunctionDeclNode extends FunctionDeclBaseNode
{
	static {
		setName(FunctionDeclNode.class, "function declaration");
	}

	protected CollectNode<BaseNode> parametersUnresolved;
	protected CollectNode<DeclNode> parameters;

	public CollectNode<EvalStatementNode> evalStatements;
	public FunctionAutoNode functionAuto;

	boolean isMethod;

	protected static final FunctionTypeNode functionType = new FunctionTypeNode();


	public FunctionDeclNode(IdentNode id, CollectNode<EvalStatementNode> evals, FunctionAutoNode functionAuto,
			CollectNode<BaseNode> params, BaseNode ret, boolean isMethod)
	{
		super(id, functionType);
		this.evalStatements = evals;
		becomeParent(this.evalStatements);
		this.functionAuto = functionAuto;
		this.parametersUnresolved = params;
		becomeParent(this.parametersUnresolved);
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
		children.add(evalStatements);
		children.add(parametersUnresolved);
		children.add(getValidVersion(resultUnresolved, resultType));
		return children;
	}

	/** returns names of the children, same order as in getChildren */
	@Override
	public Collection<String> getChildrenNames()
	{
		Vector<String> childrenNames = new Vector<String>();
		childrenNames.add("ident");
		childrenNames.add("evals");
		childrenNames.add("params");
		childrenNames.add("ret");
		return childrenNames;
	}

	@Override
	protected boolean resolveLocal()
	{
		boolean result = super.resolveLocal();
		
		if(functionAuto != null)
			result &= functionAuto.resolveLocal();
		
		return result;
	}
	
	/** @see de.unika.ipd.grgen.ast.BaseNode#resolveLocal() */
	@Override
	protected boolean checkLocal()
	{
		parameters = new CollectNode<DeclNode>();
		for(BaseNode param : parametersUnresolved.getChildren()) {
			if(param instanceof ConnectionNode) {
				ConnectionNode conn = (ConnectionNode)param;
				parameters.addChild(conn.getEdge().getDecl());
			} else if(param instanceof SingleNodeConnNode) {
				NodeDeclNode node = ((SingleNodeConnNode)param).getNode();
				parameters.addChild(node);
			} else if(param instanceof VarDeclNode) {
				parameters.addChild((VarDeclNode)param);
			} else
				throw new UnsupportedOperationException("Unsupported parameter (" + param + ")");
		}

		parameterTypes = new Vector<TypeNode>();
		for(DeclNode decl : parameters.getChildren()) {
			parameterTypes.add(decl.getDeclType());
		}
		boolean res = true;
		for(TypeNode parameterType : parameterTypes) {
			if(parameterType == null || parameterType instanceof ErrorTypeNode) {
				res = false;
			}
		}

		if(functionAuto != null) {
			res &= functionAuto.checkLocal();
			res &= functionAuto.checkLocal(this);
		}

		return res;
	}

	/** Returns the IR object for this function node. */
	public Function getFunction()
	{
		return checkIR(Function.class);
	}

	@Override
	public TypeNode getDeclType()
	{
		assert isResolved();
		return functionType;
	}

	@Override
	protected IR constructIR()
	{
		// return if the IR object was already constructed
		// that may happen in recursive calls
		if(isIRAlreadySet()) {
			return getIR();
		}

		Function function = isMethod
				? new FunctionMethod(getIdentNode().toString(), getIdentNode().getIdent(), resultType.checkIR(Type.class))
				: new Function(getIdentNode().toString(), getIdentNode().getIdent(), resultType.checkIR(Type.class));

		// mark this node as already visited
		setIR(function);

		// add Params to the IR
		for(DeclNode decl : parameters.getChildren()) {
			function.addParameter(decl.checkIR(Entity.class));
		}

		// add Computation Statements to the IR
		if(functionAuto != null) {
			functionAuto.getStatements(this, function);
		} else {
			for(EvalStatementNode eval : evalStatements.getChildren()) {
				function.addStatement(eval.checkIR(EvalStatement.class));
			}
		}

		return function;
	}

	public static String getKindStr()
	{
		return "function";
	}
}
