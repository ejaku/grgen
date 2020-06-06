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

import java.util.Collection;
import java.util.Vector;

import de.unika.ipd.grgen.ast.BaseNode;
import de.unika.ipd.grgen.ast.CollectNode;
import de.unika.ipd.grgen.ast.FilterCharacter;
import de.unika.ipd.grgen.ast.IdentNode;
import de.unika.ipd.grgen.ast.PackageIdentNode;
import de.unika.ipd.grgen.ast.decl.DeclNode;
import de.unika.ipd.grgen.ast.decl.pattern.NodeDeclNode;
import de.unika.ipd.grgen.ast.decl.pattern.VarDeclNode;
import de.unika.ipd.grgen.ast.pattern.ConnectionNode;
import de.unika.ipd.grgen.ast.pattern.SingleNodeConnNode;
import de.unika.ipd.grgen.ast.stmt.EvalStatementNode;
import de.unika.ipd.grgen.ast.type.TypeNode;
import de.unika.ipd.grgen.ast.type.executable.FilterFunctionTypeNode;
import de.unika.ipd.grgen.ast.util.DeclarationResolver;
import de.unika.ipd.grgen.ir.Entity;
import de.unika.ipd.grgen.ir.IR;
import de.unika.ipd.grgen.ir.executable.FilterFunction;
import de.unika.ipd.grgen.ir.executable.FilterFunctionExternal;
import de.unika.ipd.grgen.ir.executable.FilterFunctionInternal;
import de.unika.ipd.grgen.ir.stmt.EvalStatement;

/**
 * AST node class representing filter function declarations
 */
public class FilterFunctionDeclNode extends DeclNode implements FilterCharacter
{
	static {
		setName(FilterFunctionDeclNode.class, "filter function declaration");
	}

	protected CollectNode<BaseNode> paramsUnresolved;
	protected CollectNode<DeclNode> params;

	public CollectNode<EvalStatementNode> evalStatements;
	static final FilterFunctionTypeNode filterFunctionType = new FilterFunctionTypeNode();

	protected IdentNode actionUnresolved;
	public TestDeclNode action;

	public FilterFunctionDeclNode(IdentNode id, CollectNode<EvalStatementNode> evals, CollectNode<BaseNode> params,
			IdentNode action)
	{
		super(id, filterFunctionType);
		this.evalStatements = evals;
		becomeParent(this.evalStatements);
		this.paramsUnresolved = params;
		becomeParent(this.paramsUnresolved);
		this.actionUnresolved = action;
	}

	/** returns children of this node */
	@Override
	public Collection<BaseNode> getChildren()
	{
		Vector<BaseNode> children = new Vector<BaseNode>();
		children.add(ident);
		if(evalStatements != null)
			children.add(evalStatements);
		children.add(paramsUnresolved);
		children.add(actionUnresolved);
		return children;
	}

	/** returns names of the children, same order as in getChildren */
	@Override
	public Collection<String> getChildrenNames()
	{
		Vector<String> childrenNames = new Vector<String>();
		childrenNames.add("ident");
		if(evalStatements != null)
			childrenNames.add("evals");
		childrenNames.add("params");
		childrenNames.add("action");
		return childrenNames;
	}

	private static final DeclarationResolver<TestDeclNode> actionResolver =
			new DeclarationResolver<TestDeclNode>(TestDeclNode.class);

	/** @see de.unika.ipd.grgen.ast.BaseNode#resolveLocal() */
	@Override
	protected boolean resolveLocal()
	{
		if(!(actionUnresolved instanceof PackageIdentNode)) {
			fixupDefinition(actionUnresolved, actionUnresolved.getScope());
		}
		action = actionResolver.resolve(actionUnresolved, this);
		return action != null;
	}

	/** @see de.unika.ipd.grgen.ast.BaseNode#resolveLocal() */
	@Override
	protected boolean checkLocal()
	{
		params = new CollectNode<DeclNode>();
		for(BaseNode param : paramsUnresolved.getChildren()) {
			if(param instanceof ConnectionNode) {
				ConnectionNode conn = (ConnectionNode)param;
				params.addChild(conn.getEdge().getDecl());
			} else if(param instanceof SingleNodeConnNode) {
				NodeDeclNode node = ((SingleNodeConnNode)param).getNode();
				params.addChild(node);
			} else if(param instanceof VarDeclNode) {
				params.addChild((VarDeclNode)param);
			} else
				throw new UnsupportedOperationException("Unsupported parameter (" + param + ")");
		}

		return true;
	}

	@Override
	public String getFilterName()
	{
		return getIdentNode().toString();
	}

	@Override
	public TestDeclNode getActionNode()
	{
		return action;
	}

	/** Returns the IR object for this function filter node. */
	public FilterFunction getFilterFunction()
	{
		return checkIR(FilterFunction.class);
	}

	@Override
	public TypeNode getDeclType()
	{
		assert isResolved();

		return filterFunctionType;
	}

	public Vector<TypeNode> getParameterTypes()
	{
		assert isChecked();

		Vector<TypeNode> types = new Vector<TypeNode>();
		for(DeclNode decl : params.getChildren()) {
			types.add(decl.getDeclType());
		}

		return types;
	}

	@Override
	protected IR constructIR()
	{
		// return if the IR object was already constructed
		// that may happen in recursive calls
		if(isIRAlreadySet()) {
			return getIR();
		}

		FilterFunction filterFunction;
		if(evalStatements != null)
			filterFunction = new FilterFunctionInternal(getIdentNode().toString(), getIdentNode().getIdent());
		else
			filterFunction = new FilterFunctionExternal(getIdentNode().toString(), getIdentNode().getIdent());

		// mark this node as already visited
		setIR(filterFunction);

		filterFunction.setAction(action.getAction());
		action.getAction().addFilter(filterFunction);

		// add Params to the IR
		for(DeclNode decl : params.getChildren()) {
			filterFunction.addParameter(decl.checkIR(Entity.class));
		}

		if(evalStatements != null) {
			// add Computation Statements to the IR
			for(EvalStatementNode eval : evalStatements.getChildren()) {
				((FilterFunctionInternal)filterFunction).addComputationStatement(eval.checkIR(EvalStatement.class));
			}
		}

		return filterFunction;
	}

	public static String getKindStr()
	{
		return "filter function declaration";
	}

	public static String getUseStr()
	{
		return "filter function";
	}
}
