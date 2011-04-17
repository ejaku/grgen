/*
 * GrGen: graph rewrite generator tool -- release GrGen.NET 3.0
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
import de.unika.ipd.grgen.parser.ParserEnvironment;

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
	
	ParserEnvironment env;

	public FunctionInvocationExprNode(IdentNode functionIdent, CollectNode<ExprNode> params, ParserEnvironment env)
	{
		super(functionIdent.getCoords());
		this.functionIdent = becomeParent(functionIdent);
		this.params = becomeParent(params);
		this.env = env;
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
				reportError("min(.,.) takes two parameters.");
				return false;
			}
			else
				result = new MinExprNode(getCoords(), params.get(0), params.get(1));
		}
		else if(functionName.equals("max")) {
			if(params.size() != 2) {
				reportError("max(.,.) takes two parameters.");
				return false;
			}
			else
				result = new MaxExprNode(getCoords(), params.get(0), params.get(1));
		}		
		else if(functionName.equals("pow")) {
			if(params.size() != 2) {
				reportError("pow(.,.) takes two parameters.");
				return false;
			}
			else
				result = new PowExprNode(getCoords(), params.get(0), params.get(1));
		}		
		else if(functionName.equals("incoming") || functionName.equals("outgoing")) {
			boolean outgoing = functionName.equals("outgoing");
			IdentNode first = null;
			IdentNode second = null;
			IdentNode third = null;
			if(params.size() >= 1) {
				if(!(params.get(0) instanceof IdentExprNode)) {
					reportError("first parameter of " + functionName + "() must be a graph entity (identifier)");
					return false;
				}
				first = ((IdentExprNode)params.get(0)).getIdent();
				
				if(params.size() >= 2) {
					if(!(params.get(1) instanceof IdentExprNode)) {
						reportError("second parameter of " + functionName + "() must be an edge type (identifier)");
						return false;
					}
					second = ((IdentExprNode)params.get(1)).getIdent();
					
					if(params.size() >= 3) {
						if(!(params.get(2) instanceof IdentExprNode)) {
							reportError("third parameter of " + functionName + "() must be a node type (identifier)");
							return false;
						}
						third = ((IdentExprNode)params.get(2)).getIdent();
					}
				}
			}
			if(params.size() == 1) {
				result = new IncidentEdgeExprNode(getCoords(), first, env.getDirectedEdgeRoot(), outgoing, env.getNodeRoot());
			}
			else  if(params.size() == 2) {
				result = new IncidentEdgeExprNode(getCoords(), first, second, outgoing, env.getNodeRoot());
			}
			else  if(params.size() == 3) {
				result = new IncidentEdgeExprNode(getCoords(), first, second, outgoing, third);
			}
			else {
				reportError(functionName + "() takes 1-3 parameters.");					
				return false;
			}
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
