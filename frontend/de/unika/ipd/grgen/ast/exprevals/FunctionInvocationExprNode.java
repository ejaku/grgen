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

	static TypeNode functionTypeNode = new FunctionTypeNode();
	
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
		else if(functionName.equals("sin")) {
			if(params.size() != 1) {
				reportError("sin(.) takes one parameter.");
				return false;
			}
			else
				result = new SinCosTanExprNode(getCoords(), SinCosTanExprNode.SIN, params.get(0));
		}
		else if(functionName.equals("cos")) {
			if(params.size() != 1) {
				reportError("cos(.) takes one parameter.");
				return false;
			}
			else
				result = new SinCosTanExprNode(getCoords(), SinCosTanExprNode.COS, params.get(0));
		}
		else if(functionName.equals("tan")) {
			if(params.size() != 1) {
				reportError("tan(.) takes one parameter.");
				return false;
			}
			else
				result = new SinCosTanExprNode(getCoords(), SinCosTanExprNode.TAN, params.get(0));
		}
		else if(functionName.equals("pow")) {
			if(params.size() == 2) {
				result = new PowExprNode(getCoords(), params.get(0), params.get(1));
			} else if(params.size() == 1) {
				result = new PowExprNode(getCoords(), params.get(0));
			} else {
				reportError("pow(.,.)/pow(.) takes one or two parameters (one means base e).");
				return false;
			}
		}
		else if(functionName.equals("log")) {
			if(params.size() == 2) {
				result = new LogExprNode(getCoords(), params.get(0), params.get(1));
			} else if(params.size() == 1) {
				result = new LogExprNode(getCoords(), params.get(0));
			} else {
				reportError("log(.,.)/log(.) takes one or two parameters (one means base e).");
				return false;
			}
		}
		else if(functionName.equals("abs")) {
			if(params.size() != 1) {
				reportError("abs(.) takes one parameter.");
				return false;
			}
			else
				result = new AbsExprNode(getCoords(), params.get(0));
		}
		else if(functionName.equals("random")) {
			if(params.size() == 1) {
				result = new RandomNode(getCoords(), params.get(0));
			} else if(params.size() == 0) {
				result = new RandomNode(getCoords(), null);
			} else {
				reportError("random(.)/random() takes one or no parameters.");
				return false;
			}
		}
		else if(functionName.equals("nodes")) {
			if(params.size() > 1) {
				reportError("nodes() takes one or none parameter.");
				return false;
			}
			else {
				result = new NodesExprNode(getCoords(), params.size()==1 ? params.get(0) : new IdentExprNode(env.getNodeRoot()));
			}
		}
		else if(functionName.equals("edges")) {
			if(params.size() > 1) {
				reportError("edges() takes one or none parameter.");
				return false;
			}
			else {
				result = new EdgesExprNode(getCoords(), params.size()==1 ? params.get(0) : new IdentExprNode(env.getDirectedEdgeRoot()));
			}
		}
		else if(functionName.equals("source")) {			
			if(params.size() == 1) {
				result = new SourceExprNode(getCoords(), params.get(0), env.getNodeRoot());
			}
			else {
				reportError(functionName + "() takes 1 parameter.");
				return false;
			}
		}
		else if(functionName.equals("target")) {			
			if(params.size() == 1) {
				result = new TargetExprNode(getCoords(), params.get(0), env.getNodeRoot());
			}
			else {
				reportError(functionName + "() takes 1 parameter.");
				return false;
			}
		}
		else if(functionName.equals("opposite")) {			
			if(params.size() == 2) {
				result = new OppositeExprNode(getCoords(), params.get(0), params.get(1), env.getNodeRoot());
			}
			else {
				reportError(functionName + "() takes 2 parameters.");
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
			
			if(params.size() == 1) {
				result = new IncidentEdgeExprNode(getCoords(), params.get(0), new IdentExprNode(env.getDirectedEdgeRoot()), direction, new IdentExprNode(env.getNodeRoot()));
			}
			else if(params.size() == 2) {
				result = new IncidentEdgeExprNode(getCoords(), params.get(0), params.get(1), direction, new IdentExprNode(env.getNodeRoot()));
			}
			else if(params.size() == 3) {
				result = new IncidentEdgeExprNode(getCoords(), params.get(0), params.get(1), direction, params.get(2));
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
			
			if(params.size() == 1) {
				result = new AdjacentNodeExprNode(getCoords(), params.get(0), new IdentExprNode(env.getDirectedEdgeRoot()), direction, new IdentExprNode(env.getNodeRoot()));
			}
			else if(params.size() == 2) {
				result = new AdjacentNodeExprNode(getCoords(), params.get(0), params.get(1), direction, new IdentExprNode(env.getNodeRoot()));
			}
			else if(params.size() == 3) {
				result = new AdjacentNodeExprNode(getCoords(), params.get(0), params.get(1), direction, params.get(2));
			}
			else {
				reportError(functionName + "() takes 1-3 parameters.");
				return false;
			}
		}
		else if(functionName.equals("isIncoming")
				|| functionName.equals("isOutgoing")
				|| functionName.equals("isIncident")) {
			int direction;
			if(functionName.equals("isIncoming"))
				direction = IsIncidentEdgeExprNode.INCOMING;
			else if(functionName.equals("isOutgoing"))
				direction = IsIncidentEdgeExprNode.OUTGOING;
			else
				direction = IsIncidentEdgeExprNode.INCIDENT;
			
			if(params.size() == 2) {
				result = new IsIncidentEdgeExprNode(getCoords(), params.get(0), params.get(1), new IdentExprNode(env.getDirectedEdgeRoot()), direction, new IdentExprNode(env.getNodeRoot()));
			}
			else if(params.size() == 3) {
				result = new IsIncidentEdgeExprNode(getCoords(), params.get(0), params.get(1), params.get(2), direction, new IdentExprNode(env.getNodeRoot()));
			}
			else if(params.size() == 4) {
				result = new IsIncidentEdgeExprNode(getCoords(), params.get(0), params.get(1), params.get(2), direction, params.get(3));
			}
			else {
				reportError(functionName + "() takes 2-4 parameters.");
				return false;
			}
		}
		else if(functionName.equals("isAdjacentIncoming")
				|| functionName.equals("isAdjacentOutgoing")
				|| functionName.equals("isAdjacent")) {
			int direction;
			if(functionName.equals("isReachableEdgesIncoming"))
				direction = IsAdjacentNodeExprNode.INCOMING;
			else if(functionName.equals("isReachableEdgesOutgoing"))
				direction = IsAdjacentNodeExprNode.OUTGOING;
			else
				direction = IsAdjacentNodeExprNode.ADJACENT;
			
			if(params.size() == 2) {
				result = new IsAdjacentNodeExprNode(getCoords(), params.get(0), params.get(1), new IdentExprNode(env.getDirectedEdgeRoot()), direction, new IdentExprNode(env.getNodeRoot()));
			}
			else if(params.size() == 3) {
				result = new IsAdjacentNodeExprNode(getCoords(), params.get(0), params.get(1), params.get(2), direction, new IdentExprNode(env.getNodeRoot()));
			}
			else if(params.size() == 4) {
				result = new IsAdjacentNodeExprNode(getCoords(), params.get(0), params.get(1), params.get(2), direction, params.get(3));
			}
			else {
				reportError(functionName + "() takes 2-4 parameters.");
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
			
			if(params.size() == 1) {
				result = new ReachableEdgeExprNode(getCoords(), params.get(0), new IdentExprNode(env.getDirectedEdgeRoot()), direction, new IdentExprNode(env.getNodeRoot()));
			}
			else if(params.size() == 2) {
				result = new ReachableEdgeExprNode(getCoords(), params.get(0), params.get(1), direction, new IdentExprNode(env.getNodeRoot()));
			}
			else if(params.size() == 3) {
				result = new ReachableEdgeExprNode(getCoords(), params.get(0), params.get(1), direction, params.get(2));
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

			if(params.size() == 1) {
				result = new ReachableNodeExprNode(getCoords(), params.get(0), new IdentExprNode(env.getDirectedEdgeRoot()), direction, new IdentExprNode(env.getNodeRoot()));
			}
			else if(params.size() == 2) {
				result = new ReachableNodeExprNode(getCoords(), params.get(0), params.get(1), direction, new IdentExprNode(env.getNodeRoot()));
			}
			else if(params.size() == 3) {
				result = new ReachableNodeExprNode(getCoords(), params.get(0), params.get(1), direction, params.get(2));
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
			
			if(params.size() == 2) {
				result = new IsReachableNodeExprNode(getCoords(), params.get(0), params.get(1), new IdentExprNode(env.getDirectedEdgeRoot()), direction, new IdentExprNode(env.getNodeRoot()));
			}
			else if(params.size() == 3) {
				result = new IsReachableNodeExprNode(getCoords(), params.get(0), params.get(1), params.get(2), direction, new IdentExprNode(env.getNodeRoot()));
			}
			else if(params.size() == 4) {
				result = new IsReachableNodeExprNode(getCoords(), params.get(0), params.get(1), params.get(2), direction, params.get(3));
			}
			else {
				reportError(functionName + "() takes 2-4 parameters.");
				return false;
			}
		}
		else if(functionName.equals("isReachableEdgesIncoming")
				|| functionName.equals("isReachableEdgesOutgoing")
				|| functionName.equals("isReachableEdges")) {
			int direction;
			if(functionName.equals("isReachableEdgesIncoming"))
				direction = IsReachableEdgeExprNode.INCOMING;
			else if(functionName.equals("isReachableEdgesOutgoing"))
				direction = IsReachableEdgeExprNode.OUTGOING;
			else
				direction = IsReachableEdgeExprNode.INCIDENT;
			
			if(params.size() == 2) {
				result = new IsReachableEdgeExprNode(getCoords(), params.get(0), params.get(1), new IdentExprNode(env.getDirectedEdgeRoot()), direction, new IdentExprNode(env.getNodeRoot()));
			}
			else if(params.size() == 3) {
				result = new IsReachableEdgeExprNode(getCoords(), params.get(0), params.get(1), params.get(2), direction, new IdentExprNode(env.getNodeRoot()));
			}
			else if(params.size() == 4) {
				result = new IsReachableEdgeExprNode(getCoords(), params.get(0), params.get(1), params.get(2), direction, params.get(3));
			}
			else {
				reportError(functionName + "() takes 2-4 parameters.");
				return false;
			}
		}
		else if(functionName.equals("inducedSubgraph")) {
			if(params.size() != 1) {
				reportError("inducedSubgraph(.) takes one parameter.");
				return false;
			}
			else
				result = new InducedSubgraphExprNode(getCoords(), params.get(0));
		}
		else if(functionName.equals("definedSubgraph")) {
			if(params.size() != 1) {
				reportError("definedSubgraph(.) takes one parameter.");
				return false;
			}
			else
				result = new DefinedSubgraphExprNode(getCoords(), params.get(0));
		}
		else if(functionName.equals("canonize")) {
			if(params.size() != 1) {
				reportError("canonize(.) takes one parameter.");
				return false;
			}
			else
				result = new CanonizeExprNode(getCoords(), params.get(0));
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
