/*
 * GrGen: graph rewrite generator tool -- release GrGen.NET 5.0
 * Copyright (C) 2003-2020 Universitaet Karlsruhe, Institut fuer Programmstrukturen und Datenorganisation, LS Goos; and free programmers
 * licensed under LGPL v3 (see LICENSE.txt included in the packaging of this file)
 * www.grgen.net
 */

/**
 * @author Edgar Jakumeit
 */

package de.unika.ipd.grgen.ast.decl;

import java.util.Collection;
import java.util.Vector;

import de.unika.ipd.grgen.ast.BaseNode;
import de.unika.ipd.grgen.ast.CollectNode;
import de.unika.ipd.grgen.ast.IdentNode;
import de.unika.ipd.grgen.ast.MatchClassFilterCharacter;
import de.unika.ipd.grgen.ast.PackageIdentNode;
import de.unika.ipd.grgen.ast.decl.pattern.NodeDeclNode;
import de.unika.ipd.grgen.ast.decl.pattern.VarDeclNode;
import de.unika.ipd.grgen.ast.pattern.ConnectionNode;
import de.unika.ipd.grgen.ast.pattern.SingleNodeConnNode;
import de.unika.ipd.grgen.ast.stmt.EvalStatementNode;
import de.unika.ipd.grgen.ast.type.DefinedMatchTypeNode;
import de.unika.ipd.grgen.ast.type.FilterFunctionTypeNode;
import de.unika.ipd.grgen.ast.type.TypeNode;
import de.unika.ipd.grgen.ast.util.DeclarationTypeResolver;
import de.unika.ipd.grgen.ir.DefinedMatchType;
import de.unika.ipd.grgen.ir.Entity;
import de.unika.ipd.grgen.ir.IR;
import de.unika.ipd.grgen.ir.MatchClassFilterFunction;
import de.unika.ipd.grgen.ir.MatchClassFilterFunctionExternal;
import de.unika.ipd.grgen.ir.MatchClassFilterFunctionInternal;
import de.unika.ipd.grgen.ir.stmt.EvalStatement;

/**
 * AST node class representing match class filter function declarations
 */
public class MatchClassFilterFunctionDeclNode extends DeclNode implements MatchClassFilterCharacter
{
	static {
		setName(MatchClassFilterFunctionDeclNode.class, "match class filter function declaration");
	}

	protected CollectNode<BaseNode> paramsUnresolved;
	protected CollectNode<DeclNode> params;

	public CollectNode<EvalStatementNode> evals;
	static final FilterFunctionTypeNode filterFunctionType = new FilterFunctionTypeNode();

	protected IdentNode matchTypeUnresolved;
	public DefinedMatchTypeNode matchType;

	public MatchClassFilterFunctionDeclNode(IdentNode id, CollectNode<EvalStatementNode> evals,
			CollectNode<BaseNode> params, IdentNode matchType)
	{
		super(id, filterFunctionType);
		this.evals = evals;
		becomeParent(this.evals);
		this.paramsUnresolved = params;
		becomeParent(this.paramsUnresolved);
		this.matchTypeUnresolved = matchType;
	}

	/** returns children of this node */
	@Override
	public Collection<BaseNode> getChildren()
	{
		Vector<BaseNode> children = new Vector<BaseNode>();
		children.add(ident);
		if(evals != null)
			children.add(evals);
		children.add(paramsUnresolved);
		children.add(matchTypeUnresolved);
		return children;
	}

	/** returns names of the children, same order as in getChildren */
	@Override
	public Collection<String> getChildrenNames()
	{
		Vector<String> childrenNames = new Vector<String>();
		childrenNames.add("ident");
		if(evals != null)
			childrenNames.add("evals");
		childrenNames.add("params");
		childrenNames.add("matchType");
		return childrenNames;
	}

	private static final DeclarationTypeResolver<DefinedMatchTypeNode> matchTypeResolver =
			new DeclarationTypeResolver<DefinedMatchTypeNode>(DefinedMatchTypeNode.class);

	/** @see de.unika.ipd.grgen.ast.BaseNode#resolveLocal() */
	@Override
	protected boolean resolveLocal()
	{
		if(!(matchTypeUnresolved instanceof PackageIdentNode)) {
			fixupDefinition(matchTypeUnresolved, matchTypeUnresolved.getScope());
		}
		matchType = matchTypeResolver.resolve(matchTypeUnresolved, this);
		return matchType != null;
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

	public String getFilterName()
	{
		return getIdentNode().toString();
	}

	public DefinedMatchTypeNode getMatchTypeNode()
	{
		return matchType;
	}

	/** Returns the IR object for this match class function filter node. */
	public MatchClassFilterFunction getMatchClassFilterFunction()
	{
		return checkIR(MatchClassFilterFunction.class);
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

		MatchClassFilterFunction filterFunction;
		if(evals != null)
			filterFunction = new MatchClassFilterFunctionInternal(getIdentNode().toString(), getIdentNode().getIdent());
		else
			filterFunction = new MatchClassFilterFunctionExternal(getIdentNode().toString(), getIdentNode().getIdent());

		// mark this node as already visited
		setIR(filterFunction);

		DefinedMatchType definedMatchType = matchType.checkIR(DefinedMatchType.class);
		filterFunction.setMatchClass(definedMatchType);
		definedMatchType.addMatchClassFilter(filterFunction);

		// add Params to the IR
		for(DeclNode decl : params.getChildren()) {
			filterFunction.addParameter(decl.checkIR(Entity.class));
		}

		if(evals != null) {
			// add Computation Statements to the IR
			for(EvalStatementNode eval : evals.getChildren()) {
				((MatchClassFilterFunctionInternal)filterFunction).addComputationStatement(eval.checkIR(EvalStatement.class));
			}
		}

		return filterFunction;
	}

	public static String getKindStr()
	{
		return "match class filter function declaration";
	}

	public static String getUseStr()
	{
		return "match class filter function";
	}
}
