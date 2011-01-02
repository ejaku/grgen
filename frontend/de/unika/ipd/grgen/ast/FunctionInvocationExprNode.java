/*
 * GrGen: graph rewrite generator tool -- release GrGen.NET 2.7
 * Copyright (C) 2003-2011 Universitaet Karlsruhe, Institut fuer Programmstrukturen und Datenorganisation, LS Goos
 * licensed under LGPL v3 (see LICENSE.txt included in the packaging of this file)
 * www.grgen.net
 */

/**
 * @author Edgar Jakumeit
 * @version $Id$
 */

package de.unika.ipd.grgen.ast;

import java.util.Collection;
import java.util.Vector;

import de.unika.ipd.grgen.ir.IR;

public class FunctionInvocationExprNode extends ExprNode
{
	static {
		setName(FunctionInvocationExprNode.class, "function invocation expression");
	}

	static TypeNode functionTypeNode = new TypeNode() {
		public Collection<BaseNode> getChildren() {
			Vector<BaseNode> children = new Vector<BaseNode>();
			// no children
			return children;
		}

		public Collection<String> getChildrenNames() {
			Vector<String> childrenNames = new Vector<String>();
			// no children
			return childrenNames;
		}
	};

	private IdentNode functionIdent;
	private CollectNode<ExprNode> params;
	private ExprNode result;

	public FunctionInvocationExprNode(IdentNode functionIdent, CollectNode<ExprNode> params)
	{
		super(functionIdent.getCoords());
		this.functionIdent = becomeParent(functionIdent);
		this.params = becomeParent(params);
	}

	@Override
	public Collection<? extends BaseNode> getChildren() {
		Vector<BaseNode> children = new Vector<BaseNode>();
		//children.add(methodIdent);	// HACK: We don't have a declaration, so avoid failure during check phase
		children.add(params);
		if(isResolved())
			children.add(result);
		return children;
	}

	@Override
	public Collection<String> getChildrenNames() {
		Vector<String> childrenNames = new Vector<String>();
		//childrenNames.add("methodIdent");
		childrenNames.add("params");
		if(isResolved())
			childrenNames.add("result");
		return childrenNames;
	}

	protected boolean resolveLocal() {
		String functionName = functionIdent.toString();

		if(functionName.equals("min")) {
			if(params.size() != 2) {
				reportError("min(a,b) takes two parameters.");
				return false;
			}
			else
				result = new MinExprNode(getCoords(), params.get(0), params.get(1));
		}
		else if(functionName.equals("max")) {
			if(params.size() != 2) {
				reportError("max(a,b) takes two parameters.");
				return false;
			}
			else
				result = new MaxExprNode(getCoords(), params.get(0), params.get(1));
		}		
		else {
			reportError("no function " +functionName + " known");
			return false;
		}
		return true;
	}

	@Override
	protected boolean checkLocal() {
		return true;
	}

	@Override
	public TypeNode getType() {
		return result.getType();
	}

	protected ExprNode getResult() {
		return result;
	}

	@Override
	protected IR constructIR() {
		return result.getIR();
	}
}
