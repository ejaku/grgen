/*
 * GrGen: graph rewrite generator tool -- release GrGen.NET 4.3
 * Copyright (C) 2003-2014 Universitaet Karlsruhe, Institut fuer Programmstrukturen und Datenorganisation, LS Goos; and free programmers
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
	
	public IdentNode functionIdent;
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

		if(functionName.equals("minMath")) {
			if(params.size() != 2) {
				reportError("Math::min(.,.) takes two parameters.");
				return false;
			}
			else
				result = new MinExprNode(getCoords(), params.get(0), params.get(1));
		}
		else if(functionName.equals("maxMath")) {
			if(params.size() != 2) {
				reportError("Math::max(.,.) takes two parameters.");
				return false;
			}
			else
				result = new MaxExprNode(getCoords(), params.get(0), params.get(1));
		}
		else if(functionName.equals("sinMath")) {
			if(params.size() != 1) {
				reportError("Math::sin(.) takes one parameter.");
				return false;
			}
			else
				result = new SinCosTanExprNode(getCoords(), SinCosTanExprNode.SIN, params.get(0));
		}
		else if(functionName.equals("cosMath")) {
			if(params.size() != 1) {
				reportError("Math::cos(.) takes one parameter.");
				return false;
			}
			else
				result = new SinCosTanExprNode(getCoords(), SinCosTanExprNode.COS, params.get(0));
		}
		else if(functionName.equals("tanMath")) {
			if(params.size() != 1) {
				reportError("Math::tan(.) takes one parameter.");
				return false;
			}
			else
				result = new SinCosTanExprNode(getCoords(), SinCosTanExprNode.TAN, params.get(0));
		}
		else if(functionName.equals("arcsinMath")) {
			if(params.size() != 1) {
				reportError("Math::arcsin(.) takes one parameter.");
				return false;
			}
			else
				result = new ArcSinCosTanExprNode(getCoords(), ArcSinCosTanExprNode.ARC_SIN, params.get(0));
		}
		else if(functionName.equals("arccosMath")) {
			if(params.size() != 1) {
				reportError("Math::arccos(.) takes one parameter.");
				return false;
			}
			else
				result = new ArcSinCosTanExprNode(getCoords(), ArcSinCosTanExprNode.ARC_COS, params.get(0));
		}
		else if(functionName.equals("arctanMath")) {
			if(params.size() != 1) {
				reportError("Math::arctan(.) takes one parameter.");
				return false;
			}
			else
				result = new ArcSinCosTanExprNode(getCoords(), ArcSinCosTanExprNode.ARC_TAN, params.get(0));
		}
		else if(functionName.equals("powMath")) {
			if(params.size() == 2) {
				result = new PowExprNode(getCoords(), params.get(0), params.get(1));
			} else if(params.size() == 1) {
				result = new PowExprNode(getCoords(), params.get(0));
			} else {
				reportError("Math::pow(.,.)/Math::pow(.) takes one or two parameters (one means base e).");
				return false;
			}
		}
		else if(functionName.equals("logMath")) {
			if(params.size() == 2) {
				result = new LogExprNode(getCoords(), params.get(0), params.get(1));
			} else if(params.size() == 1) {
				result = new LogExprNode(getCoords(), params.get(0));
			} else {
				reportError("Math::log(.,.)/Math::log(.) takes one or two parameters (one means base e).");
				return false;
			}
		}
		else if(functionName.equals("absMath")) {
			if(params.size() != 1) {
				reportError("Math::abs(.) takes one parameter.");
				return false;
			}
			else
				result = new AbsExprNode(getCoords(), params.get(0));
		}
		else if(functionName.equals("piMath")) {
			if(params.size() != 0) {
				reportError("Math::pi() takes no parameters.");
				return false;
			}
			else
				result = new PiExprNode(getCoords());
		}
		else if(functionName.equals("eMath")) {
			if(params.size() != 0) {
				reportError("Math::e() takes no parameters.");
				return false;
			}
			else
				result = new EExprNode(getCoords());
		}
		else if(functionName.equals("ceilMath")) {
			if(params.size() != 1) {
				reportError("Math::ceil(.) takes one parameter.");
				return false;
			}
			else
				result = new CeilExprNode(getCoords(), params.get(0));
		}
		else if(functionName.equals("floorMath")) {
			if(params.size() != 1) {
				reportError("Math::floor(.) takes one parameter.");
				return false;
			}
			else
				result = new FloorExprNode(getCoords(), params.get(0));
		}
		else if(functionName.equals("roundMath")) {
			if(params.size() != 1) {
				reportError("Math::round(.) takes one parameter.");
				return false;
			}
			else
				result = new RoundExprNode(getCoords(), params.get(0));
		}
		else if(functionName.equals("truncateMath")) {
			if(params.size() != 1) {
				reportError("Math::truncate(.) takes one parameter.");
				return false;
			}
			else
				result = new TruncateExprNode(getCoords(), params.get(0));
		}
		else if(functionName.equals("sgnMath")) {
			if(params.size() != 1) {
				reportError("Math::sgn(.) takes one parameter.");
				return false;
			}
			else
				result = new SgnExprNode(getCoords(), params.get(0));
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
				result = new NodesExprNode(getCoords(), params.size()==1 ? params.get(0) : new IdentExprNode(env.getNodeRoot()), env.getNodeRoot());
			}
		}
		else if(functionName.equals("edges")) {
			if(params.size() > 1) {
				reportError("edges() takes one or none parameter.");
				return false;
			}
			else {
				result = new EdgesExprNode(getCoords(), params.size()==1 ? params.get(0) : new IdentExprNode(env.getDirectedEdgeRoot()), env.getDirectedEdgeRoot());
			}
		}
		else if(functionName.equals("countNodes")) {
			if(params.size() > 1) {
				reportError("countNodes() takes one or none parameter.");
				return false;
			}
			else {
				result = new CountNodesExprNode(getCoords(), params.size()==1 ? params.get(0) : new IdentExprNode(env.getNodeRoot()));
			}
		}
		else if(functionName.equals("countEdges")) {
			if(params.size() > 1) {
				reportError("countEdges() takes one or none parameter.");
				return false;
			}
			else {
				result = new CountEdgesExprNode(getCoords(), params.size()==1 ? params.get(0) : new IdentExprNode(env.getDirectedEdgeRoot()));
			}
		}
		else if(functionName.equals("nowTime")) {
			if(params.size() > 0) {
				reportError("Time::now() takes no parameters.");
				return false;
			}
			else {
				result = new NowExprNode(getCoords());
			}
		}
		else if(functionName.equals("empty")) {
			if(params.size() > 0) {
				reportError("empty() takes no parameters.");
				return false;
			}
			else {
				result = new EmptyExprNode(getCoords());
			}
		}
		else if(functionName.equals("size")) {
			if(params.size() > 0) {
				reportError("size() takes no parameters.");
				return false;
			}
			else {
				result = new SizeExprNode(getCoords());
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
		else if(functionName.equals("nodeByName")) {			
			if(params.size() == 1) {
				result = new NodeByNameExprNode(getCoords(), params.get(0), env.getNodeRoot());
			}
			else {
				reportError(functionName + "() takes 1 parameter.");
				return false;
			}
		}
		else if(functionName.equals("edgeByName")) {			
			if(params.size() == 1) {
				result = new EdgeByNameExprNode(getCoords(), params.get(0), env.getDirectedEdgeRoot());
			}
			else {
				reportError(functionName + "() takes 1 parameter.");
				return false;
			}
		}
		else if(functionName.equals("nodeByUnique")) {			
			if(params.size() == 1) {
				result = new NodeByUniqueExprNode(getCoords(), params.get(0), env.getNodeRoot());
			}
			else {
				reportError(functionName + "() takes 1 parameter.");
				return false;
			}
		}
		else if(functionName.equals("edgeByUnique")) {			
			if(params.size() == 1) {
				result = new EdgeByUniqueExprNode(getCoords(), params.get(0), env.getDirectedEdgeRoot());
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
			
			if(params.size() == 1) {
				result = new IncidentEdgeExprNode(getCoords(), params.get(0), new IdentExprNode(env.getDirectedEdgeRoot()), direction, new IdentExprNode(env.getNodeRoot()), env.getDirectedEdgeRoot());
			}
			else if(params.size() == 2) {
				result = new IncidentEdgeExprNode(getCoords(), params.get(0), params.get(1), direction, new IdentExprNode(env.getNodeRoot()), env.getDirectedEdgeRoot());
			}
			else if(params.size() == 3) {
				result = new IncidentEdgeExprNode(getCoords(), params.get(0), params.get(1), direction, params.get(2), env.getDirectedEdgeRoot());
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
				result = new AdjacentNodeExprNode(getCoords(), params.get(0), new IdentExprNode(env.getDirectedEdgeRoot()), direction, new IdentExprNode(env.getNodeRoot()), env.getNodeRoot());
			}
			else if(params.size() == 2) {
				result = new AdjacentNodeExprNode(getCoords(), params.get(0), params.get(1), direction, new IdentExprNode(env.getNodeRoot()), env.getNodeRoot());
			}
			else if(params.size() == 3) {
				result = new AdjacentNodeExprNode(getCoords(), params.get(0), params.get(1), direction, params.get(2), env.getNodeRoot());
			}
			else {
				reportError(functionName + "() takes 1-3 parameters.");
				return false;
			}
		}
		else if(functionName.equals("countIncoming")
				|| functionName.equals("countOutgoing")
				|| functionName.equals("countIncident")) {
			int direction;
			if(functionName.equals("countIncoming"))
				direction = CountIncidentEdgeExprNode.INCOMING;
			else if(functionName.equals("countOutgoing"))
				direction = CountIncidentEdgeExprNode.OUTGOING;
			else
				direction = CountIncidentEdgeExprNode.INCIDENT;
			
			if(params.size() == 1) {
				result = new CountIncidentEdgeExprNode(getCoords(), params.get(0), new IdentExprNode(env.getDirectedEdgeRoot()), direction, new IdentExprNode(env.getNodeRoot()));
			}
			else if(params.size() == 2) {
				result = new CountIncidentEdgeExprNode(getCoords(), params.get(0), params.get(1), direction, new IdentExprNode(env.getNodeRoot()));
			}
			else if(params.size() == 3) {
				result = new CountIncidentEdgeExprNode(getCoords(), params.get(0), params.get(1), direction, params.get(2));
			}
			else {
				reportError(functionName + "() takes 1-3 parameters.");
				return false;
			}
		}
		else if(functionName.equals("countAdjacentIncoming")
				|| functionName.equals("countAdjacentOutgoing")
				|| functionName.equals("countAdjacent")) {
			int direction;
			if(functionName.equals("countAdjacentIncoming"))
				direction = CountAdjacentNodeExprNode.INCOMING;
			else if(functionName.equals("countAdjacentOutgoing"))
				direction = CountAdjacentNodeExprNode.OUTGOING;
			else
				direction = CountAdjacentNodeExprNode.ADJACENT;
			
			if(params.size() == 1) {
				result = new CountAdjacentNodeExprNode(getCoords(), params.get(0), new IdentExprNode(env.getDirectedEdgeRoot()), direction, new IdentExprNode(env.getNodeRoot()));
			}
			else if(params.size() == 2) {
				result = new CountAdjacentNodeExprNode(getCoords(), params.get(0), params.get(1), direction, new IdentExprNode(env.getNodeRoot()));
			}
			else if(params.size() == 3) {
				result = new CountAdjacentNodeExprNode(getCoords(), params.get(0), params.get(1), direction, params.get(2));
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
				result = new ReachableEdgeExprNode(getCoords(), params.get(0), new IdentExprNode(env.getDirectedEdgeRoot()), direction, new IdentExprNode(env.getNodeRoot()), env.getDirectedEdgeRoot());
			}
			else if(params.size() == 2) {
				result = new ReachableEdgeExprNode(getCoords(), params.get(0), params.get(1), direction, new IdentExprNode(env.getNodeRoot()), env.getDirectedEdgeRoot());
			}
			else if(params.size() == 3) {
				result = new ReachableEdgeExprNode(getCoords(), params.get(0), params.get(1), direction, params.get(2), env.getDirectedEdgeRoot());
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
				result = new ReachableNodeExprNode(getCoords(), params.get(0), new IdentExprNode(env.getDirectedEdgeRoot()), direction, new IdentExprNode(env.getNodeRoot()), env.getNodeRoot());
			}
			else if(params.size() == 2) {
				result = new ReachableNodeExprNode(getCoords(), params.get(0), params.get(1), direction, new IdentExprNode(env.getNodeRoot()), env.getNodeRoot());
			}
			else if(params.size() == 3) {
				result = new ReachableNodeExprNode(getCoords(), params.get(0), params.get(1), direction, params.get(2), env.getNodeRoot());
			}
			else {
				reportError(functionName + "() takes 1-3 parameters.");
				return false;
			}
		}
		else if(functionName.equals("countReachableEdgesIncoming")
				|| functionName.equals("countReachableEdgesOutgoing")
				|| functionName.equals("countReachableEdges")) {
			int direction;
			if(functionName.equals("countReachableEdgesIncoming"))
				direction = CountReachableEdgeExprNode.INCOMING;
			else if(functionName.equals("countReachableEdgesOutgoing"))
				direction = CountReachableEdgeExprNode.OUTGOING;
			else
				direction = CountReachableEdgeExprNode.INCIDENT;
			
			if(params.size() == 1) {
				result = new CountReachableEdgeExprNode(getCoords(), params.get(0), new IdentExprNode(env.getDirectedEdgeRoot()), direction, new IdentExprNode(env.getNodeRoot()));
			}
			else if(params.size() == 2) {
				result = new CountReachableEdgeExprNode(getCoords(), params.get(0), params.get(1), direction, new IdentExprNode(env.getNodeRoot()));
			}
			else if(params.size() == 3) {
				result = new CountReachableEdgeExprNode(getCoords(), params.get(0), params.get(1), direction, params.get(2));
			}
			else {
				reportError(functionName + "() takes 1-3 parameters.");
				return false;
			}
		}
		else if(functionName.equals("countReachableIncoming")
				|| functionName.equals("countReachableOutgoing")
				|| functionName.equals("countReachable")) {
			int direction;
			if(functionName.equals("countReachableIncoming"))
				direction = CountReachableNodeExprNode.INCOMING;
			else if(functionName.equals("countReachableOutgoing"))
				direction = CountReachableNodeExprNode.OUTGOING;
			else
				direction = CountReachableNodeExprNode.ADJACENT;

			if(params.size() == 1) {
				result = new CountReachableNodeExprNode(getCoords(), params.get(0), new IdentExprNode(env.getDirectedEdgeRoot()), direction, new IdentExprNode(env.getNodeRoot()));
			}
			else if(params.size() == 2) {
				result = new CountReachableNodeExprNode(getCoords(), params.get(0), params.get(1), direction, new IdentExprNode(env.getNodeRoot()));
			}
			else if(params.size() == 3) {
				result = new CountReachableNodeExprNode(getCoords(), params.get(0), params.get(1), direction, params.get(2));
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
		else if(functionName.equals("boundedReachableEdgesIncoming")
				|| functionName.equals("boundedReachableEdgesOutgoing")
				|| functionName.equals("boundedReachableEdges")) {
			int direction;
			if(functionName.equals("boundedReachableEdgesIncoming"))
				direction = BoundedReachableEdgeExprNode.INCOMING;
			else if(functionName.equals("boundedReachableEdgesOutgoing"))
				direction = BoundedReachableEdgeExprNode.OUTGOING;
			else
				direction = BoundedReachableEdgeExprNode.INCIDENT;
			
			if(params.size() == 2) {
				result = new BoundedReachableEdgeExprNode(getCoords(), params.get(0), params.get(1), new IdentExprNode(env.getDirectedEdgeRoot()), direction, new IdentExprNode(env.getNodeRoot()), env.getDirectedEdgeRoot());
			}
			else if(params.size() == 3) {
				result = new BoundedReachableEdgeExprNode(getCoords(), params.get(0), params.get(1), params.get(2), direction, new IdentExprNode(env.getNodeRoot()), env.getDirectedEdgeRoot());
			}
			else if(params.size() == 4) {
				result = new BoundedReachableEdgeExprNode(getCoords(), params.get(0), params.get(1), params.get(2), direction, params.get(3), env.getDirectedEdgeRoot());
			}
			else {
				reportError(functionName + "() takes 2-4 parameters.");
				return false;
			}
		}
		else if(functionName.equals("boundedReachableIncoming")
				|| functionName.equals("boundedReachableOutgoing")
				|| functionName.equals("boundedReachable")) {
			int direction;
			if(functionName.equals("boundedReachableIncoming"))
				direction = BoundedReachableNodeExprNode.INCOMING;
			else if(functionName.equals("boundedReachableOutgoing"))
				direction = BoundedReachableNodeExprNode.OUTGOING;
			else
				direction = BoundedReachableNodeExprNode.ADJACENT;

			if(params.size() == 2) {
				result = new BoundedReachableNodeExprNode(getCoords(), params.get(0), params.get(1), new IdentExprNode(env.getDirectedEdgeRoot()), direction, new IdentExprNode(env.getNodeRoot()), env.getNodeRoot());
			}
			else if(params.size() == 3) {
				result = new BoundedReachableNodeExprNode(getCoords(), params.get(0), params.get(1), params.get(2), direction, new IdentExprNode(env.getNodeRoot()), env.getNodeRoot());
			}
			else if(params.size() == 4) {
				result = new BoundedReachableNodeExprNode(getCoords(), params.get(0), params.get(1), params.get(2), direction, params.get(3), env.getNodeRoot());
			}
			else {
				reportError(functionName + "() takes 2-4 parameters.");
				return false;
			}
		}
		else if(functionName.equals("boundedReachableWithRemainingDepthIncoming")
				|| functionName.equals("boundedReachableWithRemainingDepthOutgoing")
				|| functionName.equals("boundedReachableWithRemainingDepth")) {
			int direction;
			if(functionName.equals("boundedReachableWithRemainingDepthIncoming"))
				direction = BoundedReachableNodeWithRemainingDepthExprNode.INCOMING;
			else if(functionName.equals("boundedReachableWithRemainingDepthOutgoing"))
				direction = BoundedReachableNodeWithRemainingDepthExprNode.OUTGOING;
			else
				direction = BoundedReachableNodeWithRemainingDepthExprNode.ADJACENT;

			if(params.size() == 2) {
				result = new BoundedReachableNodeWithRemainingDepthExprNode(getCoords(), params.get(0), params.get(1), new IdentExprNode(env.getDirectedEdgeRoot()), direction, new IdentExprNode(env.getNodeRoot()), env.getNodeRoot());
			}
			else if(params.size() == 3) {
				result = new BoundedReachableNodeWithRemainingDepthExprNode(getCoords(), params.get(0), params.get(1), params.get(2), direction, new IdentExprNode(env.getNodeRoot()), env.getNodeRoot());
			}
			else if(params.size() == 4) {
				result = new BoundedReachableNodeWithRemainingDepthExprNode(getCoords(), params.get(0), params.get(1), params.get(2), direction, params.get(3), env.getNodeRoot());
			}
			else {
				reportError(functionName + "() takes 2-4 parameters.");
				return false;
			}
		}
		else if(functionName.equals("countBoundedReachableEdgesIncoming")
				|| functionName.equals("countBoundedReachableEdgesOutgoing")
				|| functionName.equals("countBoundedReachableEdges")) {
			int direction;
			if(functionName.equals("countBoundedReachableEdgesIncoming"))
				direction = CountBoundedReachableEdgeExprNode.INCOMING;
			else if(functionName.equals("countBoundedReachableEdgesOutgoing"))
				direction = CountBoundedReachableEdgeExprNode.OUTGOING;
			else
				direction = CountBoundedReachableEdgeExprNode.INCIDENT;
			
			if(params.size() == 2) {
				result = new CountBoundedReachableEdgeExprNode(getCoords(), params.get(0), params.get(1), new IdentExprNode(env.getDirectedEdgeRoot()), direction, new IdentExprNode(env.getNodeRoot()));
			}
			else if(params.size() == 3) {
				result = new CountBoundedReachableEdgeExprNode(getCoords(), params.get(0), params.get(1), params.get(2), direction, new IdentExprNode(env.getNodeRoot()));
			}
			else if(params.size() == 4) {
				result = new CountBoundedReachableEdgeExprNode(getCoords(), params.get(0), params.get(1), params.get(2), direction, params.get(3));
			}
			else {
				reportError(functionName + "() takes 2-4 parameters.");
				return false;
			}
		}
		else if(functionName.equals("countBoundedReachableIncoming")
				|| functionName.equals("countBoundedReachableOutgoing")
				|| functionName.equals("countBoundedReachable")) {
			int direction;
			if(functionName.equals("countBoundedReachableIncoming"))
				direction = CountBoundedReachableNodeExprNode.INCOMING;
			else if(functionName.equals("countBoundedReachableOutgoing"))
				direction = CountBoundedReachableNodeExprNode.OUTGOING;
			else
				direction = CountBoundedReachableNodeExprNode.ADJACENT;

			if(params.size() == 2) {
				result = new CountBoundedReachableNodeExprNode(getCoords(), params.get(0), params.get(1), new IdentExprNode(env.getDirectedEdgeRoot()), direction, new IdentExprNode(env.getNodeRoot()));
			}
			else if(params.size() == 3) {
				result = new CountBoundedReachableNodeExprNode(getCoords(), params.get(0), params.get(1), params.get(2), direction, new IdentExprNode(env.getNodeRoot()));
			}
			else if(params.size() == 4) {
				result = new CountBoundedReachableNodeExprNode(getCoords(), params.get(0), params.get(1), params.get(2), direction, params.get(3));
			}
			else {
				reportError(functionName + "() takes 2-4 parameters.");
				return false;
			}
		}
		else if(functionName.equals("isBoundedReachableIncoming")
				|| functionName.equals("isBoundedReachableOutgoing")
				|| functionName.equals("isBoundedReachable")) {
			int direction;
			if(functionName.equals("isBoundedReachableIncoming"))
				direction = IsBoundedReachableNodeExprNode.INCOMING;
			else if(functionName.equals("isBoundedReachableOutgoing"))
				direction = IsBoundedReachableNodeExprNode.OUTGOING;
			else
				direction = IsBoundedReachableNodeExprNode.ADJACENT;
			
			if(params.size() == 3) {
				result = new IsBoundedReachableNodeExprNode(getCoords(), params.get(0), params.get(1), params.get(2), new IdentExprNode(env.getDirectedEdgeRoot()), direction, new IdentExprNode(env.getNodeRoot()));
			}
			else if(params.size() == 4) {
				result = new IsBoundedReachableNodeExprNode(getCoords(), params.get(0), params.get(1), params.get(2), params.get(3), direction, new IdentExprNode(env.getNodeRoot()));
			}
			else if(params.size() == 5) {
				result = new IsBoundedReachableNodeExprNode(getCoords(), params.get(0), params.get(1), params.get(2), params.get(3), direction, params.get(4));
			}
			else {
				reportError(functionName + "() takes 3-5 parameters.");
				return false;
			}
		}
		else if(functionName.equals("isBoundedReachableEdgesIncoming")
				|| functionName.equals("isBoundedReachableEdgesOutgoing")
				|| functionName.equals("isBoundedReachableEdges")) {
			int direction;
			if(functionName.equals("isBoundedReachableEdgesIncoming"))
				direction = IsBoundedReachableEdgeExprNode.INCOMING;
			else if(functionName.equals("isBoundedReachableEdgesOutgoing"))
				direction = IsBoundedReachableEdgeExprNode.OUTGOING;
			else
				direction = IsBoundedReachableEdgeExprNode.INCIDENT;
			
			if(params.size() == 3) {
				result = new IsBoundedReachableEdgeExprNode(getCoords(), params.get(0), params.get(1), params.get(2), new IdentExprNode(env.getDirectedEdgeRoot()), direction, new IdentExprNode(env.getNodeRoot()));
			}
			else if(params.size() == 4) {
				result = new IsBoundedReachableEdgeExprNode(getCoords(), params.get(0), params.get(1), params.get(2), params.get(3), direction, new IdentExprNode(env.getNodeRoot()));
			}
			else if(params.size() == 5) {
				result = new IsBoundedReachableEdgeExprNode(getCoords(), params.get(0), params.get(1), params.get(2), params.get(3), direction, params.get(4));
			}
			else {
				reportError(functionName + "() takes 3-5 parameters.");
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
		else if(functionName.equals("equalsAny")) {
			if(params.size() != 2) {
				reportError("equalsAny(.,.) takes two parameters.");
				return false;
			}
			else
				result = new EqualsAnyExprNode(getCoords(), params.get(0), params.get(1), true);
		}
		else if(functionName.equals("equalsAnyStructurally")) {
			if(params.size() != 2) {
				reportError("equalsAnyStructurally(.,.) takes two parameters.");
				return false;
			}
			else
				result = new EqualsAnyExprNode(getCoords(), params.get(0), params.get(1), false);
		}
		else if(functionName.equals("existsFile")) {
			if(params.size() != 1) {
				reportError("File::exists(.) takes one parameter.");
				return false;
			}
			else
				result = new ExistsFileExprNode(getCoords(), params.get(0));
		}
		else if(functionName.equals("importFile")) {
			if(params.size() != 1) {
				reportError("File::import(.) takes one parameter.");
				return false;
			}
			else
				result = new ImportExprNode(getCoords(), params.get(0));
		}
		else if(functionName.equals("copy")) {
			if(params.size() != 1) {
				reportError("copy(.) takes one parameter.");
				return false;
			}
			else
				result = new CopyExprNode(getCoords(), params.get(0));
		}
		else if(functionName.equals("canonize")) {
			if(params.size() != 1) {
				reportError("canonize(.) takes one parameter.");
				return false;
			}
			else
				result = new CanonizeExprNode(getCoords(), params.get(0));
		}
		else if(functionName.equals("uniqueof")) {
			if(params.size() > 1) {
				reportError("uniqueof(.) takes none or one parameter.");
				return false;
			}
			else if(params.size()==1)
				result = new UniqueofExprNode(getCoords(), params.get(0));
			else
				result = new UniqueofExprNode(getCoords(), null);				
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
