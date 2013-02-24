/*
 * GrGen: graph rewrite generator tool -- release GrGen.NET 3.6
 * Copyright (C) 2003-2013 Universitaet Karlsruhe, Institut fuer Programmstrukturen und Datenorganisation, LS Goos; and free programmers
 * licensed under LGPL v3 (see LICENSE.txt included in the packaging of this file)
 * www.grgen.net
 */

/**
 * @author Edgar Jakumeit
 */

package de.unika.ipd.grgen.ast.exprevals;

import java.util.Collection;
import java.util.Vector;

import de.unika.ipd.grgen.ast.*;
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
		else if(functionName.equals("nodes")) {
			if(params.size() > 1) {
				reportError("nodes() takes one or none parameter.");
				return false;
			}
			else {
				if(params.size() == 1) {
					if(!(params.get(0) instanceof IdentExprNode)) {
						reportError("parameter of " + functionName + "() must be a node type (identifier)");
						return false;
					}
				}
				IdentNode nodeType = params.size() == 1 ? ((IdentExprNode)params.get(0)).getIdent() : env.getNodeRoot();
				result = new NodesExprNode(getCoords(), nodeType);
			}
		}
		else if(functionName.equals("edges")) {
			if(params.size() > 1) {
				reportError("edges() takes one or none parameter.");
				return false;
			}
			else {
				if(params.size() == 1) {
					if(!(params.get(0) instanceof IdentExprNode)) {
						reportError("parameter of " + functionName + "() must be an edge type (identifier)");
						return false;
					}
				}
				IdentNode edgeType = params.size() == 1 ? ((IdentExprNode)params.get(0)).getIdent() : env.getDirectedEdgeRoot();
				result = new EdgesExprNode(getCoords(), edgeType);
			}
		}
		else if(functionName.equals("source")) {			
			if(params.size() == 1) {
				if(!(params.get(0) instanceof IdentExprNode)) {
					reportError("parameter of " + functionName + "() must be a graph entity (identifier)");
					return false;
				}
				result = new SourceExprNode(getCoords(), ((IdentExprNode)params.get(0)).getIdent(), env.getNodeRoot());
			}
			else {
				reportError(functionName + "() takes 1 parameter.");
				return false;
			}
		}
		else if(functionName.equals("target")) {			
			if(params.size() == 1) {
				if(!(params.get(0) instanceof IdentExprNode)) {
					reportError("parameter of " + functionName + "() must be a graph entity (identifier)");
					return false;
				}
				result = new TargetExprNode(getCoords(), ((IdentExprNode)params.get(0)).getIdent(), env.getNodeRoot());
			}
			else {
				reportError(functionName + "() takes 1 parameter.");
				return false;
			}
		}
		else if(functionName.equals("incoming")
				|| functionName.equals("outgoing")
				|| functionName.equals("incident")) {
			int direction;
			if(functionName.equals("incoming"))
				direction = IncidentEdgeExprNode.INCOMING;
			else if(functionName.equals("outgoing"))
				direction = IncidentEdgeExprNode.OUTGOING;
			else
				direction = IncidentEdgeExprNode.INCIDENT;
			
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
				result = new IncidentEdgeExprNode(getCoords(), first, env.getDirectedEdgeRoot(), direction, env.getNodeRoot());
			}
			else if(params.size() == 2) {
				result = new IncidentEdgeExprNode(getCoords(), first, second, direction, env.getNodeRoot());
			}
			else if(params.size() == 3) {
				result = new IncidentEdgeExprNode(getCoords(), first, second, direction, third);
			}
			else {
				reportError(functionName + "() takes 1-3 parameters.");
				return false;
			}
		}
		else if(functionName.equals("adjacentIncoming")
				|| functionName.equals("adjacentOutgoing")
				|| functionName.equals("adjacent")) {
			int direction;
			if(functionName.equals("adjacentIncoming"))
				direction = AdjacentNodeExprNode.INCOMING;
			else if(functionName.equals("adjacentOutgoing"))
				direction = AdjacentNodeExprNode.OUTGOING;
			else
				direction = AdjacentNodeExprNode.ADJACENT;
			
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
				result = new AdjacentNodeExprNode(getCoords(), first, env.getDirectedEdgeRoot(), direction, env.getNodeRoot());
			}
			else if(params.size() == 2) {
				result = new AdjacentNodeExprNode(getCoords(), first, second, direction, env.getNodeRoot());
			}
			else if(params.size() == 3) {
				result = new AdjacentNodeExprNode(getCoords(), first, second, direction, third);
			}
			else {
				reportError(functionName + "() takes 1-3 parameters.");
				return false;
			}
		}
		else if(functionName.equals("reachableEdgesIncoming")
				|| functionName.equals("reachableEdgesOutgoing")
				|| functionName.equals("reachableEdges")) {
			int direction;
			if(functionName.equals("reachableEdgesIncoming"))
				direction = ReachableEdgeExprNode.INCOMING;
			else if(functionName.equals("reachableEdgesOutgoing"))
				direction = ReachableEdgeExprNode.OUTGOING;
			else
				direction = ReachableEdgeExprNode.INCIDENT;
			
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
				result = new ReachableEdgeExprNode(getCoords(), first, env.getDirectedEdgeRoot(), direction, env.getNodeRoot());
			}
			else if(params.size() == 2) {
				result = new ReachableEdgeExprNode(getCoords(), first, second, direction, env.getNodeRoot());
			}
			else if(params.size() == 3) {
				result = new ReachableEdgeExprNode(getCoords(), first, second, direction, third);
			}
			else {
				reportError(functionName + "() takes 1-3 parameters.");
				return false;
			}
		}
		else if(functionName.equals("reachableIncoming")
				|| functionName.equals("reachableOutgoing")
				|| functionName.equals("reachable")) {
			int direction;
			if(functionName.equals("reachableIncoming"))
				direction = ReachableNodeExprNode.INCOMING;
			else if(functionName.equals("reachableOutgoing"))
				direction = ReachableNodeExprNode.OUTGOING;
			else
				direction = ReachableNodeExprNode.ADJACENT;
			
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
				result = new ReachableNodeExprNode(getCoords(), first, env.getDirectedEdgeRoot(), direction, env.getNodeRoot());
			}
			else if(params.size() == 2) {
				result = new ReachableNodeExprNode(getCoords(), first, second, direction, env.getNodeRoot());
			}
			else if(params.size() == 3) {
				result = new ReachableNodeExprNode(getCoords(), first, second, direction, third);
			}
			else {
				reportError(functionName + "() takes 1-3 parameters.");
				return false;
			}
		}
		else if(functionName.equals("isReachableIncoming")
				|| functionName.equals("isReachableOutgoing")
				|| functionName.equals("isReachable")) {
			int direction;
			if(functionName.equals("isReachableIncoming"))
				direction = IsReachableNodeExprNode.INCOMING;
			else if(functionName.equals("isReachableOutgoing"))
				direction = IsReachableNodeExprNode.OUTGOING;
			else
				direction = IsReachableNodeExprNode.ADJACENT;
			
			IdentNode first = null;
			IdentNode second = null;
			IdentNode third = null;
			IdentNode fourth = null;
			if(params.size() >= 2) {
				if(!(params.get(0) instanceof IdentExprNode)) {
					reportError("first parameter of " + functionName + "() must be a graph entity (identifier)");
					return false;
				}
				first = ((IdentExprNode)params.get(0)).getIdent();

				if(!(params.get(1) instanceof IdentExprNode)) {
					reportError("second parameter of " + functionName + "() must be a graph entity (identifier)");
					return false;
				}
				second = ((IdentExprNode)params.get(1)).getIdent();

				if(params.size() >= 3) {
					if(!(params.get(2) instanceof IdentExprNode)) {
						reportError("third parameter of " + functionName + "() must be an edge type (identifier)");
						return false;
					}
					third = ((IdentExprNode)params.get(2)).getIdent();

					if(params.size() >= 4) {
						if(!(params.get(3) instanceof IdentExprNode)) {
							reportError("fourth parameter of " + functionName + "() must be a node type (identifier)");
							return false;
						}
						fourth = ((IdentExprNode)params.get(3)).getIdent();
					}
				}
			}
			if(params.size() == 2) {
				result = new IsReachableNodeExprNode(getCoords(), first, second, env.getDirectedEdgeRoot(), direction, env.getNodeRoot());
			}
			else if(params.size() == 3) {
				result = new IsReachableNodeExprNode(getCoords(), first, second, third, direction, env.getNodeRoot());
			}
			else if(params.size() == 4) {
				result = new IsReachableNodeExprNode(getCoords(), first, second, third, direction, fourth);
			}
			else {
				reportError(functionName + "() takes 2-4 parameters.");
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
