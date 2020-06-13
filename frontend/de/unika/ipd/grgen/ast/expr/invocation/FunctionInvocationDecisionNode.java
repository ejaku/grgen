/*
 * GrGen: graph rewrite generator tool -- release GrGen.NET 5.0
 * Copyright (C) 2003-2020 Universitaet Karlsruhe, Institut fuer Programmstrukturen und Datenorganisation, LS Goos; and free programmers
 * licensed under LGPL v3 (see LICENSE.txt included in the packaging of this file)
 * www.grgen.net
 */

/**
 * @author Edgar Jakumeit
 */

package de.unika.ipd.grgen.ast.expr.invocation;

import java.util.Collection;
import java.util.Vector;

import de.unika.ipd.grgen.ast.*;
import de.unika.ipd.grgen.ast.expr.BuiltinFunctionInvocationBaseNode;
import de.unika.ipd.grgen.ast.expr.CopyExprNode;
import de.unika.ipd.grgen.ast.expr.ExprNode;
import de.unika.ipd.grgen.ast.expr.IdentExprNode;
import de.unika.ipd.grgen.ast.expr.graph.AdjacentNodeExprNode;
import de.unika.ipd.grgen.ast.expr.graph.BoundedReachableEdgeExprNode;
import de.unika.ipd.grgen.ast.expr.graph.BoundedReachableNodeExprNode;
import de.unika.ipd.grgen.ast.expr.graph.BoundedReachableNodeWithRemainingDepthExprNode;
import de.unika.ipd.grgen.ast.expr.graph.CanonizeExprNode;
import de.unika.ipd.grgen.ast.expr.graph.CountAdjacentNodeExprNode;
import de.unika.ipd.grgen.ast.expr.graph.CountBoundedReachableEdgeExprNode;
import de.unika.ipd.grgen.ast.expr.graph.CountBoundedReachableNodeExprNode;
import de.unika.ipd.grgen.ast.expr.graph.CountEdgesExprNode;
import de.unika.ipd.grgen.ast.expr.graph.CountIncidentEdgeExprNode;
import de.unika.ipd.grgen.ast.expr.graph.CountNodesExprNode;
import de.unika.ipd.grgen.ast.expr.graph.CountReachableEdgeExprNode;
import de.unika.ipd.grgen.ast.expr.graph.CountReachableNodeExprNode;
import de.unika.ipd.grgen.ast.expr.graph.DefinedSubgraphExprNode;
import de.unika.ipd.grgen.ast.expr.graph.EdgeByNameExprNode;
import de.unika.ipd.grgen.ast.expr.graph.EdgeByUniqueExprNode;
import de.unika.ipd.grgen.ast.expr.graph.EdgesExprNode;
import de.unika.ipd.grgen.ast.expr.graph.EmptyExprNode;
import de.unika.ipd.grgen.ast.expr.graph.EqualsAnyExprNode;
import de.unika.ipd.grgen.ast.expr.graph.IncidentEdgeExprNode;
import de.unika.ipd.grgen.ast.expr.graph.InducedSubgraphExprNode;
import de.unika.ipd.grgen.ast.expr.graph.IsAdjacentNodeExprNode;
import de.unika.ipd.grgen.ast.expr.graph.IsBoundedReachableEdgeExprNode;
import de.unika.ipd.grgen.ast.expr.graph.IsBoundedReachableNodeExprNode;
import de.unika.ipd.grgen.ast.expr.graph.IsIncidentEdgeExprNode;
import de.unika.ipd.grgen.ast.expr.graph.IsReachableEdgeExprNode;
import de.unika.ipd.grgen.ast.expr.graph.IsReachableNodeExprNode;
import de.unika.ipd.grgen.ast.expr.graph.NodeByNameExprNode;
import de.unika.ipd.grgen.ast.expr.graph.NodeByUniqueExprNode;
import de.unika.ipd.grgen.ast.expr.graph.NodesExprNode;
import de.unika.ipd.grgen.ast.expr.graph.OppositeExprNode;
import de.unika.ipd.grgen.ast.expr.graph.ReachableEdgeExprNode;
import de.unika.ipd.grgen.ast.expr.graph.ReachableNodeExprNode;
import de.unika.ipd.grgen.ast.expr.graph.SizeExprNode;
import de.unika.ipd.grgen.ast.expr.graph.SourceExprNode;
import de.unika.ipd.grgen.ast.expr.graph.TargetExprNode;
import de.unika.ipd.grgen.ast.expr.graph.UniqueofExprNode;
import de.unika.ipd.grgen.ast.expr.procenv.RandomNode;
import de.unika.ipd.grgen.ast.type.TypeNode;
import de.unika.ipd.grgen.ast.type.executable.FunctionTypeNode;
import de.unika.ipd.grgen.ast.util.ResolvingEnvironment;
import de.unika.ipd.grgen.ir.IR;
import de.unika.ipd.grgen.parser.ParserEnvironment;
import de.unika.ipd.grgen.util.Direction;

public class FunctionInvocationDecisionNode extends FunctionInvocationBaseNode
{
	static {
		setName(FunctionInvocationDecisionNode.class, "function invocation decision expression");
	}

	static TypeNode functionTypeNode = new FunctionTypeNode();

	public IdentNode functionIdent;
	private BuiltinFunctionInvocationBaseNode result;

	ParserEnvironment env;

	public FunctionInvocationDecisionNode(IdentNode functionIdent,
			CollectNode<ExprNode> arguments, ParserEnvironment env)
	{
		super(functionIdent.getCoords(), arguments);
		this.functionIdent = becomeParent(functionIdent);
		this.env = env;
	}

	@Override
	public Collection<? extends BaseNode> getChildren()
	{
		Vector<BaseNode> children = new Vector<BaseNode>();
		//children.add(methodIdent);	// HACK: We don't have a declaration, so avoid failure during check phase
		children.add(arguments);
		if(isResolved())
			children.add(result);
		return children;
	}

	@Override
	public Collection<String> getChildrenNames()
	{
		Vector<String> childrenNames = new Vector<String>();
		//childrenNames.add("methodIdent");
		childrenNames.add("params");
		if(isResolved())
			childrenNames.add("result");
		return childrenNames;
	}

	@Override
	protected boolean resolveLocal()
	{
		ResolvingEnvironment resolvingEnvironment = new ResolvingEnvironment(env, error, getCoords());
		result = decide(functionIdent.toString(), arguments, resolvingEnvironment);
		return result != null;
	}
	
	private static BuiltinFunctionInvocationBaseNode decide(String functionName, CollectNode<ExprNode> arguments,
			ResolvingEnvironment env)
	{
		switch(functionName) {
		case "random":
			if(arguments.size() == 1)
				return new RandomNode(env.getCoords(), arguments.get(0));
			else if(arguments.size() == 0)
				return new RandomNode(env.getCoords(), null);
			else {
				env.reportError("random(.)/random() takes one or no parameters.");
				return null;
			}
		case "nodes":
			if(arguments.size() > 1) {
				env.reportError("nodes() takes one or none parameter.");
				return null;
			} else {
				return new NodesExprNode(env.getCoords(),
						arguments.size() == 1 ? arguments.get(0) : new IdentExprNode(env.getParserEnvironment().getNodeRoot()));
			}
		case "edges":
			if(arguments.size() > 1) {
				env.reportError("edges() takes one or none parameter.");
				return null;
			} else {
				return new EdgesExprNode(env.getCoords(),
						arguments.size() == 1 ? arguments.get(0) : new IdentExprNode(env.getParserEnvironment().getArbitraryEdgeRoot()));
			}
		case "countNodes":
			if(arguments.size() > 1) {
				env.reportError("countNodes() takes one or none parameter.");
				return null;
			} else {
				return new CountNodesExprNode(env.getCoords(),
						arguments.size() == 1 ? arguments.get(0) : new IdentExprNode(env.getParserEnvironment().getNodeRoot()));
			}
		case "countEdges":
			if(arguments.size() > 1) {
				env.reportError("countEdges() takes one or none parameter.");
				return null;
			} else {
				return new CountEdgesExprNode(env.getCoords(),
						arguments.size() == 1 ? arguments.get(0) : new IdentExprNode(env.getParserEnvironment().getArbitraryEdgeRoot()));
			}
		case "empty":
			if(arguments.size() > 0) {
				env.reportError("empty() takes no parameters.");
				return null;
			} else
				return new EmptyExprNode(env.getCoords());
		case "size":
			if(arguments.size() > 0) {
				env.reportError("size() takes no parameters.");
				return null;
			} else
				return new SizeExprNode(env.getCoords());
		case "source":
			if(arguments.size() == 1)
				return new SourceExprNode(env.getCoords(), arguments.get(0), env.getParserEnvironment().getNodeRoot());
			else {
				env.reportError(functionName + "() takes 1 parameter.");
				return null;
			}
		case "target":
			if(arguments.size() == 1)
				return new TargetExprNode(env.getCoords(), arguments.get(0), env.getParserEnvironment().getNodeRoot());
			else {
				env.reportError(functionName + "() takes 1 parameter.");
				return null;
			}
		case "opposite":
			if(arguments.size() == 2)
				return new OppositeExprNode(env.getCoords(), arguments.get(0), arguments.get(1), env.getParserEnvironment().getNodeRoot());
			else {
				env.reportError(functionName + "() takes 2 parameters.");
				return null;
			}
		case "nodeByName":
			if(arguments.size() >= 1 && arguments.size() <= 2) {
				return new NodeByNameExprNode(env.getCoords(), arguments.get(0),
						arguments.size() == 2 ? arguments.get(1) : new IdentExprNode(env.getParserEnvironment().getNodeRoot()));
			} else {
				env.reportError(functionName + "() takes one or two parameters.");
				return null;
			}
		case "edgeByName":
			if(arguments.size() >= 1 && arguments.size() <= 2) {
				return new EdgeByNameExprNode(env.getCoords(), arguments.get(0),
						arguments.size() == 2 ? arguments.get(1) : new IdentExprNode(env.getParserEnvironment().getArbitraryEdgeRoot()));
			} else {
				env.reportError(functionName + "() takes one or two parameters.");
				return null;
			}
		case "nodeByUnique":
			if(arguments.size() >= 1 && arguments.size() <= 2) {
				return new NodeByUniqueExprNode(env.getCoords(), arguments.get(0),
						arguments.size() == 2 ? arguments.get(1) : new IdentExprNode(env.getParserEnvironment().getNodeRoot()));
			} else {
				env.reportError(functionName + "() takes one or two parameters.");
				return null;
			}
		case "edgeByUnique":
			if(arguments.size() >= 1 && arguments.size() <= 2) {
				return new EdgeByUniqueExprNode(env.getCoords(), arguments.get(0),
						arguments.size() == 2 ? arguments.get(1) : new IdentExprNode(env.getParserEnvironment().getArbitraryEdgeRoot()));
			} else {
				env.reportError(functionName + "() takes one or two parameters.");
				return null;
			}
		case "incoming":
		case "outgoing":
		case "incident":
		{
			Direction direction = getDirection(functionName);
			if(arguments.size() == 1) {
				return new IncidentEdgeExprNode(env.getCoords(), arguments.get(0),
						new IdentExprNode(env.getParserEnvironment().getArbitraryEdgeRoot()), direction, new IdentExprNode(env.getParserEnvironment().getNodeRoot()));
			} else if(arguments.size() == 2) {
				return new IncidentEdgeExprNode(env.getCoords(), arguments.get(0), arguments.get(1), direction,
						new IdentExprNode(env.getParserEnvironment().getNodeRoot()));
			} else if(arguments.size() == 3) {
				return new IncidentEdgeExprNode(env.getCoords(), arguments.get(0), arguments.get(1), direction, arguments.get(2));
			} else {
				env.reportError(functionName + "() takes 1-3 parameters.");
				return null;
			}
		}
		case "adjacentIncoming":
		case "adjacentOutgoing":
		case "adjacent":
		{
			Direction direction = getDirection(functionName);
			if(arguments.size() == 1) {
				return new AdjacentNodeExprNode(env.getCoords(), arguments.get(0),
						new IdentExprNode(env.getParserEnvironment().getArbitraryEdgeRoot()), direction, new IdentExprNode(env.getParserEnvironment().getNodeRoot()));
			} else if(arguments.size() == 2) {
				return new AdjacentNodeExprNode(env.getCoords(), arguments.get(0), arguments.get(1), direction,
						new IdentExprNode(env.getParserEnvironment().getNodeRoot()));
			} else if(arguments.size() == 3) {
				return new AdjacentNodeExprNode(env.getCoords(), arguments.get(0), arguments.get(1), direction, arguments.get(2));
			} else {
				env.reportError(functionName + "() takes 1-3 parameters.");
				return null;
			}
		}
		case "countIncoming":
		case "countOutgoing":
		case "countIncident":
		{
			Direction direction = getDirection(functionName);
			if(arguments.size() == 1) {
				return new CountIncidentEdgeExprNode(env.getCoords(), arguments.get(0),
						new IdentExprNode(env.getParserEnvironment().getArbitraryEdgeRoot()), direction, new IdentExprNode(env.getParserEnvironment().getNodeRoot()));
			} else if(arguments.size() == 2) {
				return new CountIncidentEdgeExprNode(env.getCoords(), arguments.get(0), arguments.get(1), direction,
						new IdentExprNode(env.getParserEnvironment().getNodeRoot()));
			} else if(arguments.size() == 3) {
				return new CountIncidentEdgeExprNode(env.getCoords(), arguments.get(0), arguments.get(1), direction,
						arguments.get(2));
			} else {
				env.reportError(functionName + "() takes 1-3 parameters.");
				return null;
			}
		}
		case "countAdjacentIncoming":
		case "countAdjacentOutgoing":
		case "countAdjacent":
		{
			Direction direction = getDirection(functionName);
			if(arguments.size() == 1) {
				return new CountAdjacentNodeExprNode(env.getCoords(), arguments.get(0),
						new IdentExprNode(env.getParserEnvironment().getArbitraryEdgeRoot()), direction, new IdentExprNode(env.getParserEnvironment().getNodeRoot()));
			} else if(arguments.size() == 2) {
				return new CountAdjacentNodeExprNode(env.getCoords(), arguments.get(0), arguments.get(1), direction,
						new IdentExprNode(env.getParserEnvironment().getNodeRoot()));
			} else if(arguments.size() == 3) {
				return new CountAdjacentNodeExprNode(env.getCoords(), arguments.get(0), arguments.get(1), direction,
						arguments.get(2));
			} else {
				env.reportError(functionName + "() takes 1-3 parameters.");
				return null;
			}
		}
		case "isIncoming":
		case "isOutgoing":
		case "isIncident":
		{
			Direction direction = getDirection(functionName);
			if(arguments.size() == 2) {
				return new IsIncidentEdgeExprNode(env.getCoords(), arguments.get(0), arguments.get(1),
						new IdentExprNode(env.getParserEnvironment().getArbitraryEdgeRoot()), direction, new IdentExprNode(env.getParserEnvironment().getNodeRoot()));
			} else if(arguments.size() == 3) {
				return new IsIncidentEdgeExprNode(env.getCoords(), arguments.get(0), arguments.get(1), arguments.get(2), direction,
						new IdentExprNode(env.getParserEnvironment().getNodeRoot()));
			} else if(arguments.size() == 4) {
				return new IsIncidentEdgeExprNode(env.getCoords(), arguments.get(0), arguments.get(1), arguments.get(2), direction,
						arguments.get(3));
			} else {
				env.reportError(functionName + "() takes 2-4 parameters.");
				return null;
			}
		}
		case "isAdjacentIncoming":
		case "isAdjacentOutgoing":
		case "isAdjacent":
		{
			Direction direction = getDirection(functionName);
			if(arguments.size() == 2) {
				return new IsAdjacentNodeExprNode(env.getCoords(), arguments.get(0), arguments.get(1),
						new IdentExprNode(env.getParserEnvironment().getArbitraryEdgeRoot()), direction, new IdentExprNode(env.getParserEnvironment().getNodeRoot()));
			} else if(arguments.size() == 3) {
				return new IsAdjacentNodeExprNode(env.getCoords(), arguments.get(0), arguments.get(1), arguments.get(2), direction,
						new IdentExprNode(env.getParserEnvironment().getNodeRoot()));
			} else if(arguments.size() == 4) {
				return new IsAdjacentNodeExprNode(env.getCoords(), arguments.get(0), arguments.get(1), arguments.get(2), direction,
						arguments.get(3));
			} else {
				env.reportError(functionName + "() takes 2-4 parameters.");
				return null;
			}
		}
		case "reachableEdgesIncoming":
		case "reachableEdgesOutgoing":
		case "reachableEdges":
		{
			Direction direction = getDirection(functionName);
			if(arguments.size() == 1) {
				return new ReachableEdgeExprNode(env.getCoords(), arguments.get(0),
						new IdentExprNode(env.getParserEnvironment().getArbitraryEdgeRoot()), direction, new IdentExprNode(env.getParserEnvironment().getNodeRoot()));
			} else if(arguments.size() == 2) {
				return new ReachableEdgeExprNode(env.getCoords(), arguments.get(0), arguments.get(1), direction,
						new IdentExprNode(env.getParserEnvironment().getNodeRoot()));
			} else if(arguments.size() == 3) {
				return new ReachableEdgeExprNode(env.getCoords(), arguments.get(0), arguments.get(1), direction, arguments.get(2));
			} else {
				env.reportError(functionName + "() takes 1-3 parameters.");
				return null;
			}
		}
		case "reachableIncoming":
		case "reachableOutgoing":
		case "reachable":
		{
			Direction direction = getDirection(functionName);
			if(arguments.size() == 1) {
				return new ReachableNodeExprNode(env.getCoords(), arguments.get(0),
						new IdentExprNode(env.getParserEnvironment().getArbitraryEdgeRoot()), direction, new IdentExprNode(env.getParserEnvironment().getNodeRoot()));
			} else if(arguments.size() == 2) {
				return new ReachableNodeExprNode(env.getCoords(), arguments.get(0), arguments.get(1), direction,
						new IdentExprNode(env.getParserEnvironment().getNodeRoot()));
			} else if(arguments.size() == 3) {
				return new ReachableNodeExprNode(env.getCoords(), arguments.get(0), arguments.get(1), direction, arguments.get(2));
			} else {
				env.reportError(functionName + "() takes 1-3 parameters.");
				return null;
			}
		}
		case "countReachableEdgesIncoming":
		case "countReachableEdgesOutgoing":
		case "countReachableEdges":
		{
			Direction direction = getDirection(functionName);
			if(arguments.size() == 1) {
				return new CountReachableEdgeExprNode(env.getCoords(), arguments.get(0),
						new IdentExprNode(env.getParserEnvironment().getArbitraryEdgeRoot()), direction, new IdentExprNode(env.getParserEnvironment().getNodeRoot()));
			} else if(arguments.size() == 2) {
				return new CountReachableEdgeExprNode(env.getCoords(), arguments.get(0), arguments.get(1), direction,
						new IdentExprNode(env.getParserEnvironment().getNodeRoot()));
			} else if(arguments.size() == 3) {
				return new CountReachableEdgeExprNode(env.getCoords(), arguments.get(0), arguments.get(1), direction,
						arguments.get(2));
			} else {
				env.reportError(functionName + "() takes 1-3 parameters.");
				return null;
			}
		}
		case "countReachableIncoming":
		case "countReachableOutgoing":
		case "countReachable":
		{
			Direction direction = getDirection(functionName);
			if(arguments.size() == 1) {
				return new CountReachableNodeExprNode(env.getCoords(), arguments.get(0),
						new IdentExprNode(env.getParserEnvironment().getArbitraryEdgeRoot()), direction, new IdentExprNode(env.getParserEnvironment().getNodeRoot()));
			} else if(arguments.size() == 2) {
				return new CountReachableNodeExprNode(env.getCoords(), arguments.get(0), arguments.get(1), direction,
						new IdentExprNode(env.getParserEnvironment().getNodeRoot()));
			} else if(arguments.size() == 3) {
				return new CountReachableNodeExprNode(env.getCoords(), arguments.get(0), arguments.get(1), direction,
						arguments.get(2));
			} else {
				env.reportError(functionName + "() takes 1-3 parameters.");
				return null;
			}
		}
		case "isReachableIncoming":
		case "isReachableOutgoing":
		case "isReachable":
		{
			Direction direction = getDirection(functionName);
			if(arguments.size() == 2) {
				return new IsReachableNodeExprNode(env.getCoords(), arguments.get(0), arguments.get(1),
						new IdentExprNode(env.getParserEnvironment().getArbitraryEdgeRoot()), direction, new IdentExprNode(env.getParserEnvironment().getNodeRoot()));
			} else if(arguments.size() == 3) {
				return new IsReachableNodeExprNode(env.getCoords(), arguments.get(0), arguments.get(1), arguments.get(2),
						direction, new IdentExprNode(env.getParserEnvironment().getNodeRoot()));
			} else if(arguments.size() == 4) {
				return new IsReachableNodeExprNode(env.getCoords(), arguments.get(0), arguments.get(1), arguments.get(2),
						direction, arguments.get(3));
			} else {
				env.reportError(functionName + "() takes 2-4 parameters.");
				return null;
			}
		}
		case "isReachableEdgesIncoming":
		case "isReachableEdgesOutgoing":
		case "isReachableEdges":
		{
			Direction direction = getDirection(functionName);
			if(arguments.size() == 2) {
				return new IsReachableEdgeExprNode(env.getCoords(), arguments.get(0), arguments.get(1),
						new IdentExprNode(env.getParserEnvironment().getArbitraryEdgeRoot()), direction, new IdentExprNode(env.getParserEnvironment().getNodeRoot()));
			} else if(arguments.size() == 3) {
				return new IsReachableEdgeExprNode(env.getCoords(), arguments.get(0), arguments.get(1), arguments.get(2),
						direction, new IdentExprNode(env.getParserEnvironment().getNodeRoot()));
			} else if(arguments.size() == 4) {
				return new IsReachableEdgeExprNode(env.getCoords(), arguments.get(0), arguments.get(1), arguments.get(2),
						direction, arguments.get(3));
			} else {
				env.reportError(functionName + "() takes 2-4 parameters.");
				return null;
			}
		}
		case "boundedReachableEdgesIncoming":
		case "boundedReachableEdgesOutgoing":
		case "boundedReachableEdges":
		{
			Direction direction = getDirection(functionName);
			if(arguments.size() == 2) {
				return new BoundedReachableEdgeExprNode(env.getCoords(), arguments.get(0), arguments.get(1),
						new IdentExprNode(env.getParserEnvironment().getArbitraryEdgeRoot()), direction, new IdentExprNode(env.getParserEnvironment().getNodeRoot()));
			} else if(arguments.size() == 3) {
				return new BoundedReachableEdgeExprNode(env.getCoords(), arguments.get(0), arguments.get(1), arguments.get(2),
						direction, new IdentExprNode(env.getParserEnvironment().getNodeRoot()));
			} else if(arguments.size() == 4) {
				return new BoundedReachableEdgeExprNode(env.getCoords(), arguments.get(0), arguments.get(1), arguments.get(2),
						direction, arguments.get(3));
			} else {
				env.reportError(functionName + "() takes 2-4 parameters.");
				return null;
			}
		}
		case "boundedReachableIncoming":
		case "boundedReachableOutgoing":
		case "boundedReachable":
		{
			Direction direction = getDirection(functionName);
			if(arguments.size() == 2) {
				return new BoundedReachableNodeExprNode(env.getCoords(), arguments.get(0), arguments.get(1),
						new IdentExprNode(env.getParserEnvironment().getArbitraryEdgeRoot()), direction, new IdentExprNode(env.getParserEnvironment().getNodeRoot()));
			} else if(arguments.size() == 3) {
				return new BoundedReachableNodeExprNode(env.getCoords(), arguments.get(0), arguments.get(1), arguments.get(2),
						direction, new IdentExprNode(env.getParserEnvironment().getNodeRoot()));
			} else if(arguments.size() == 4) {
				return new BoundedReachableNodeExprNode(env.getCoords(), arguments.get(0), arguments.get(1), arguments.get(2),
						direction, arguments.get(3));
			} else {
				env.reportError(functionName + "() takes 2-4 parameters.");
				return null;
			}
		}
		case "boundedReachableWithRemainingDepthIncoming":
		case "boundedReachableWithRemainingDepthOutgoing":
		case "boundedReachableWithRemainingDepth":
		{
			Direction direction = getDirection(functionName);
			if(arguments.size() == 2) {
				return new BoundedReachableNodeWithRemainingDepthExprNode(env.getCoords(), arguments.get(0), arguments.get(1),
						new IdentExprNode(env.getParserEnvironment().getArbitraryEdgeRoot()), direction, new IdentExprNode(env.getParserEnvironment().getNodeRoot()));
			} else if(arguments.size() == 3) {
				return new BoundedReachableNodeWithRemainingDepthExprNode(env.getCoords(), arguments.get(0), arguments.get(1),
						arguments.get(2), direction, new IdentExprNode(env.getParserEnvironment().getNodeRoot()));
			} else if(arguments.size() == 4) {
				return new BoundedReachableNodeWithRemainingDepthExprNode(env.getCoords(), arguments.get(0), arguments.get(1),
						arguments.get(2), direction, arguments.get(3));
			} else {
				env.reportError(functionName + "() takes 2-4 parameters.");
				return null;
			}
		}
		case "countBoundedReachableEdgesIncoming":
		case "countBoundedReachableEdgesOutgoing":
		case "countBoundedReachableEdges":
		{
			Direction direction = getDirection(functionName);
			if(arguments.size() == 2) {
				return new CountBoundedReachableEdgeExprNode(env.getCoords(), arguments.get(0), arguments.get(1),
						new IdentExprNode(env.getParserEnvironment().getArbitraryEdgeRoot()), direction, new IdentExprNode(env.getParserEnvironment().getNodeRoot()));
			} else if(arguments.size() == 3) {
				return new CountBoundedReachableEdgeExprNode(env.getCoords(), arguments.get(0), arguments.get(1), arguments.get(2),
						direction, new IdentExprNode(env.getParserEnvironment().getNodeRoot()));
			} else if(arguments.size() == 4) {
				return new CountBoundedReachableEdgeExprNode(env.getCoords(), arguments.get(0), arguments.get(1), arguments.get(2),
						direction, arguments.get(3));
			} else {
				env.reportError(functionName + "() takes 2-4 parameters.");
				return null;
			}
		}
		case "countBoundedReachableIncoming":	
		case "countBoundedReachableOutgoing":
		case "countBoundedReachable":
		{
			Direction direction = getDirection(functionName);
			if(arguments.size() == 2) {
				return new CountBoundedReachableNodeExprNode(env.getCoords(), arguments.get(0), arguments.get(1),
						new IdentExprNode(env.getParserEnvironment().getArbitraryEdgeRoot()), direction, new IdentExprNode(env.getParserEnvironment().getNodeRoot()));
			} else if(arguments.size() == 3) {
				return new CountBoundedReachableNodeExprNode(env.getCoords(), arguments.get(0), arguments.get(1), arguments.get(2),
						direction, new IdentExprNode(env.getParserEnvironment().getNodeRoot()));
			} else if(arguments.size() == 4) {
				return new CountBoundedReachableNodeExprNode(env.getCoords(), arguments.get(0), arguments.get(1), arguments.get(2),
						direction, arguments.get(3));
			} else {
				env.reportError(functionName + "() takes 2-4 parameters.");
				return null;
			}
		}
		case "isBoundedReachableIncoming":
		case "isBoundedReachableOutgoing":
		case "isBoundedReachable":
		{
			Direction direction = getDirection(functionName);
			if(arguments.size() == 3) {
				return new IsBoundedReachableNodeExprNode(env.getCoords(), arguments.get(0), arguments.get(1), arguments.get(2),
						new IdentExprNode(env.getParserEnvironment().getArbitraryEdgeRoot()), direction, new IdentExprNode(env.getParserEnvironment().getNodeRoot()));
			} else if(arguments.size() == 4) {
				return new IsBoundedReachableNodeExprNode(env.getCoords(), arguments.get(0), arguments.get(1), arguments.get(2),
						arguments.get(3), direction, new IdentExprNode(env.getParserEnvironment().getNodeRoot()));
			} else if(arguments.size() == 5) {
				return new IsBoundedReachableNodeExprNode(env.getCoords(), arguments.get(0), arguments.get(1), arguments.get(2),
						arguments.get(3), direction, arguments.get(4));
			} else {
				env.reportError(functionName + "() takes 3-5 parameters.");
				return null;
			}
		}
		case "isBoundedReachableEdgesIncoming":
		case "isBoundedReachableEdgesOutgoing":
		case "isBoundedReachableEdges":
		{
			Direction direction = getDirection(functionName);
			if(arguments.size() == 3) {
				return new IsBoundedReachableEdgeExprNode(env.getCoords(), arguments.get(0), arguments.get(1), arguments.get(2),
						new IdentExprNode(env.getParserEnvironment().getArbitraryEdgeRoot()), direction, new IdentExprNode(env.getParserEnvironment().getNodeRoot()));
			} else if(arguments.size() == 4) {
				return new IsBoundedReachableEdgeExprNode(env.getCoords(), arguments.get(0), arguments.get(1), arguments.get(2),
						arguments.get(3), direction, new IdentExprNode(env.getParserEnvironment().getNodeRoot()));
			} else if(arguments.size() == 5) {
				return new IsBoundedReachableEdgeExprNode(env.getCoords(), arguments.get(0), arguments.get(1), arguments.get(2),
						arguments.get(3), direction, arguments.get(4));
			} else {
				env.reportError(functionName + "() takes 3-5 parameters.");
				return null;
			}
		}
		case "inducedSubgraph":
			if(arguments.size() != 1) {
				env.reportError("inducedSubgraph(.) takes one parameter.");
				return null;
			} else
				return new InducedSubgraphExprNode(env.getCoords(), arguments.get(0));
		case "definedSubgraph":
			if(arguments.size() != 1) {
				env.reportError("definedSubgraph(.) takes one parameter.");
				return null;
			} else
				return new DefinedSubgraphExprNode(env.getCoords(), arguments.get(0));
		case "equalsAny":
			if(arguments.size() != 2) {
				env.reportError("equalsAny(.,.) takes two parameters.");
				return null;
			} else
				return new EqualsAnyExprNode(env.getCoords(), arguments.get(0), arguments.get(1), true);
		case "equalsAnyStructurally":
			if(arguments.size() != 2) {
				env.reportError("equalsAnyStructurally(.,.) takes two parameters.");
				return null;
			} else
				return new EqualsAnyExprNode(env.getCoords(), arguments.get(0), arguments.get(1), false);
		case "copy":
			if(arguments.size() != 1) {
				env.reportError("copy(.) takes one parameter.");
				return null;
			} else
				return new CopyExprNode(env.getCoords(), arguments.get(0));
		case "canonize":
			if(arguments.size() != 1) {
				env.reportError("canonize(.) takes one parameter.");
				return null;
			} else
				return new CanonizeExprNode(env.getCoords(), arguments.get(0));
		case "uniqueof":
			if(arguments.size() > 1) {
				env.reportError("uniqueof(.) takes none or one parameter.");
				return null;
			} else if(arguments.size() == 1)
				return new UniqueofExprNode(env.getCoords(), arguments.get(0));
			else
				return new UniqueofExprNode(env.getCoords(), null);
		default:
			env.reportError("no function " + functionName + " known");
			return null;
		}
	}

	public static Direction getDirection(String functionName)
	{
		switch(functionName) {
		case "adjacentIncoming":
		case "countAdjacentIncoming":
		case "isAdjacentIncoming":
		case "reachableIncoming":
		case "countReachableIncoming":
		case "isReachableIncoming":
		case "boundedReachableIncoming":
		case "boundedReachableWithRemainingDepthIncoming":
		case "countBoundedReachableIncoming":
		case "isBoundedReachableIncoming":
			return Direction.INCOMING;
		case "adjacentOutgoing":
		case "countAdjacentOutgoing":
		case "isAdjacentOutgoing":
		case "reachableOutgoing":
		case "countReachableOutgoing":
		case "isReachableOutgoing":
		case "boundedReachableOutgoing":
		case "boundedReachableWithRemainingDepthOutgoing":
		case "countBoundedReachableOutgoing":
		case "isBoundedReachableOutgoing":
			return Direction.OUTGOING;
		case "adjacent":
		case "countAdjacent":
		case "isAdjacent":
		case "reachable":
		case "countReachable":
		case "isReachable":
		case "boundedReachable":
		case "boundedReachableWithRemainingDepth":
		case "countBoundedReachable":
		case "isBoundedReachable":
			return Direction.INCIDENT;
		case "incoming":
		case "countIncoming":
		case "isIncoming":
		case "reachableEdgesIncoming":
		case "countReachableEdgesIncoming":
		case "isReachableEdgesIncoming":
		case "boundedReachableEdgesIncoming":
		case "countBoundedReachableEdgesIncoming":
		case "isBoundedReachableEdgesIncoming":
			return Direction.INCOMING;
		case "outgoing":
		case "countOutgoing":
		case "isOutgoing":
		case "reachableEdgesOutgoing":
		case "countReachableEdgesOutgoing":
		case "isReachableEdgesOutgoing":
		case "boundedReachableEdgesOutgoing":
		case "countBoundedReachableEdgesOutgoing":
		case "isBoundedReachableEdgesOutgoing":
			return Direction.OUTGOING;
		case "incident":
		case "countIncident":
		case "isIncident":
		case "reachableEdges":
		case "countReachableEdges":
		case "isReachableEdges":
		case "boundedReachableEdges":
		case "countBoundedReachableEdges":
		case "isBoundedReachableEdges":
			return Direction.INCIDENT;
		}
		
		return Direction.INVALID;
	}
	
	@Override
	protected boolean checkLocal()
	{
		return true;
	}

	@Override
	public TypeNode getType()
	{
		return result.getType();
	}

	public ExprNode getResult()
	{
		return result;
	}

	@Override
	protected IR constructIR()
	{
		return result.getIR();
	}
}
