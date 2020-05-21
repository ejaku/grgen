/*
 * GrGen: graph rewrite generator tool -- release GrGen.NET 5.0
 * Copyright (C) 2003-2020 Universitaet Karlsruhe, Institut fuer Programmstrukturen und Datenorganisation, LS Goos; and free programmers
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

public class FunctionInvocationDecisionNode extends FunctionInvocationBaseNode
{
	static {
		setName(FunctionInvocationDecisionNode.class, "function invocation decision expression");
	}

	static TypeNode functionTypeNode = new FunctionTypeNode();

	public IdentNode functionIdent;
	private BuiltinFunctionInvocationBaseNode result;

	ParserEnvironment env;

	public FunctionInvocationDecisionNode(IdentNode functionIdent, CollectNode<ExprNode> arguments, ParserEnvironment env)
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

	protected boolean resolveLocal()
	{
		String functionName = functionIdent.toString();

		if(functionName.equals("minMath")) {
			if(arguments.size() != 2) {
				reportError("Math::min(.,.) takes two parameters.");
				return false;
			} else
				result = new MinExprNode(getCoords(), arguments.get(0), arguments.get(1));
		} else if(functionName.equals("maxMath")) {
			if(arguments.size() != 2) {
				reportError("Math::max(.,.) takes two parameters.");
				return false;
			} else
				result = new MaxExprNode(getCoords(), arguments.get(0), arguments.get(1));
		} else if(functionName.equals("sinMath")) {
			if(arguments.size() != 1) {
				reportError("Math::sin(.) takes one parameter.");
				return false;
			} else
				result = new SinCosTanExprNode(getCoords(), SinCosTanExprNode.TrigonometryFunctionType.sin,
						arguments.get(0));
		} else if(functionName.equals("cosMath")) {
			if(arguments.size() != 1) {
				reportError("Math::cos(.) takes one parameter.");
				return false;
			} else
				result = new SinCosTanExprNode(getCoords(), SinCosTanExprNode.TrigonometryFunctionType.cos,
						arguments.get(0));
		} else if(functionName.equals("tanMath")) {
			if(arguments.size() != 1) {
				reportError("Math::tan(.) takes one parameter.");
				return false;
			} else
				result = new SinCosTanExprNode(getCoords(), SinCosTanExprNode.TrigonometryFunctionType.tan,
						arguments.get(0));
		} else if(functionName.equals("arcsinMath")) {
			if(arguments.size() != 1) {
				reportError("Math::arcsin(.) takes one parameter.");
				return false;
			} else
				result = new ArcSinCosTanExprNode(getCoords(), ArcSinCosTanExprNode.ArcusTrigonometryFunctionType.arcsin,
						arguments.get(0));
		} else if(functionName.equals("arccosMath")) {
			if(arguments.size() != 1) {
				reportError("Math::arccos(.) takes one parameter.");
				return false;
			} else
				result = new ArcSinCosTanExprNode(getCoords(), ArcSinCosTanExprNode.ArcusTrigonometryFunctionType.arccos,
						arguments.get(0));
		} else if(functionName.equals("arctanMath")) {
			if(arguments.size() != 1) {
				reportError("Math::arctan(.) takes one parameter.");
				return false;
			} else
				result = new ArcSinCosTanExprNode(getCoords(), ArcSinCosTanExprNode.ArcusTrigonometryFunctionType.arctan,
						arguments.get(0));
		} else if(functionName.equals("sqrMath")) {
			if(arguments.size() == 1) {
				result = new SqrExprNode(getCoords(), arguments.get(0));
			} else {
				reportError("Math::sqr(.) takes one parameter.");
				return false;
			}
		} else if(functionName.equals("sqrtMath")) {
			if(arguments.size() == 1) {
				result = new SqrtExprNode(getCoords(), arguments.get(0));
			} else {
				reportError("Math::sqrt(.) takes one parameter.");
				return false;
			}
		} else if(functionName.equals("powMath")) {
			if(arguments.size() == 2) {
				result = new PowExprNode(getCoords(), arguments.get(0), arguments.get(1));
			} else if(arguments.size() == 1) {
				result = new PowExprNode(getCoords(), arguments.get(0));
			} else {
				reportError("Math::pow(.,.)/Math::pow(.) takes one or two parameters (one means base e).");
				return false;
			}
		} else if(functionName.equals("logMath")) {
			if(arguments.size() == 2) {
				result = new LogExprNode(getCoords(), arguments.get(0), arguments.get(1));
			} else if(arguments.size() == 1) {
				result = new LogExprNode(getCoords(), arguments.get(0));
			} else {
				reportError("Math::log(.,.)/Math::log(.) takes one or two parameters (one means base e).");
				return false;
			}
		} else if(functionName.equals("absMath")) {
			if(arguments.size() != 1) {
				reportError("Math::abs(.) takes one parameter.");
				return false;
			} else
				result = new AbsExprNode(getCoords(), arguments.get(0));
		} else if(functionName.equals("piMath")) {
			if(arguments.size() != 0) {
				reportError("Math::pi() takes no parameters.");
				return false;
			} else
				result = new PiExprNode(getCoords());
		} else if(functionName.equals("eMath")) {
			if(arguments.size() != 0) {
				reportError("Math::e() takes no parameters.");
				return false;
			} else
				result = new EExprNode(getCoords());
		} else if(functionName.equals("byteMinMath")) {
			if(arguments.size() != 0) {
				reportError("Math::byteMin() takes no parameters.");
				return false;
			} else
				result = new ByteMinExprNode(getCoords());
		} else if(functionName.equals("byteMaxMath")) {
			if(arguments.size() != 0) {
				reportError("Math::byteMax() takes no parameters.");
				return false;
			} else
				result = new ByteMaxExprNode(getCoords());
		} else if(functionName.equals("shortMinMath")) {
			if(arguments.size() != 0) {
				reportError("Math::shortMin() takes no parameters.");
				return false;
			} else
				result = new ShortMinExprNode(getCoords());
		} else if(functionName.equals("shortMaxMath")) {
			if(arguments.size() != 0) {
				reportError("Math::shortMax() takes no parameters.");
				return false;
			} else
				result = new ShortMaxExprNode(getCoords());
		} else if(functionName.equals("intMinMath")) {
			if(arguments.size() != 0) {
				reportError("Math::intMin() takes no parameters.");
				return false;
			} else
				result = new IntMinExprNode(getCoords());
		} else if(functionName.equals("intMaxMath")) {
			if(arguments.size() != 0) {
				reportError("Math::intMax() takes no parameters.");
				return false;
			} else
				result = new IntMaxExprNode(getCoords());
		} else if(functionName.equals("longMinMath")) {
			if(arguments.size() != 0) {
				reportError("Math::longMin() takes no parameters.");
				return false;
			} else
				result = new LongMinExprNode(getCoords());
		} else if(functionName.equals("longMaxMath")) {
			if(arguments.size() != 0) {
				reportError("Math::longMax() takes no parameters.");
				return false;
			} else
				result = new LongMaxExprNode(getCoords());
		} else if(functionName.equals("floatMinMath")) {
			if(arguments.size() != 0) {
				reportError("Math::floatMin() takes no parameters.");
				return false;
			} else
				result = new FloatMinExprNode(getCoords());
		} else if(functionName.equals("floatMaxMath")) {
			if(arguments.size() != 0) {
				reportError("Math::floatMax() takes no parameters.");
				return false;
			} else
				result = new FloatMaxExprNode(getCoords());
		} else if(functionName.equals("doubleMinMath")) {
			if(arguments.size() != 0) {
				reportError("Math::doubleMin() takes no parameters.");
				return false;
			} else
				result = new DoubleMinExprNode(getCoords());
		} else if(functionName.equals("doubleMaxMath")) {
			if(arguments.size() != 0) {
				reportError("Math::doubleMax() takes no parameters.");
				return false;
			} else
				result = new DoubleMaxExprNode(getCoords());
		} else if(functionName.equals("ceilMath")) {
			if(arguments.size() != 1) {
				reportError("Math::ceil(.) takes one parameter.");
				return false;
			} else
				result = new CeilExprNode(getCoords(), arguments.get(0));
		} else if(functionName.equals("floorMath")) {
			if(arguments.size() != 1) {
				reportError("Math::floor(.) takes one parameter.");
				return false;
			} else
				result = new FloorExprNode(getCoords(), arguments.get(0));
		} else if(functionName.equals("roundMath")) {
			if(arguments.size() != 1) {
				reportError("Math::round(.) takes one parameter.");
				return false;
			} else
				result = new RoundExprNode(getCoords(), arguments.get(0));
		} else if(functionName.equals("truncateMath")) {
			if(arguments.size() != 1) {
				reportError("Math::truncate(.) takes one parameter.");
				return false;
			} else
				result = new TruncateExprNode(getCoords(), arguments.get(0));
		} else if(functionName.equals("sgnMath")) {
			if(arguments.size() != 1) {
				reportError("Math::sgn(.) takes one parameter.");
				return false;
			} else
				result = new SgnExprNode(getCoords(), arguments.get(0));
		} else if(functionName.equals("random")) {
			if(arguments.size() == 1) {
				result = new RandomNode(getCoords(), arguments.get(0));
			} else if(arguments.size() == 0) {
				result = new RandomNode(getCoords(), null);
			} else {
				reportError("random(.)/random() takes one or no parameters.");
				return false;
			}
		} else if(functionName.equals("nodes")) {
			if(arguments.size() > 1) {
				reportError("nodes() takes one or none parameter.");
				return false;
			} else {
				result = new NodesExprNode(getCoords(),
						arguments.size() == 1 ? arguments.get(0) : new IdentExprNode(env.getNodeRoot()));
			}
		} else if(functionName.equals("edges")) {
			if(arguments.size() > 1) {
				reportError("edges() takes one or none parameter.");
				return false;
			} else {
				result = new EdgesExprNode(getCoords(),
						arguments.size() == 1 ? arguments.get(0) : new IdentExprNode(env.getArbitraryEdgeRoot()));
			}
		} else if(functionName.equals("countNodes")) {
			if(arguments.size() > 1) {
				reportError("countNodes() takes one or none parameter.");
				return false;
			} else {
				result = new CountNodesExprNode(getCoords(),
						arguments.size() == 1 ? arguments.get(0) : new IdentExprNode(env.getNodeRoot()));
			}
		} else if(functionName.equals("countEdges")) {
			if(arguments.size() > 1) {
				reportError("countEdges() takes one or none parameter.");
				return false;
			} else {
				result = new CountEdgesExprNode(getCoords(),
						arguments.size() == 1 ? arguments.get(0) : new IdentExprNode(env.getArbitraryEdgeRoot()));
			}
		} else if(functionName.equals("nowTime")) {
			if(arguments.size() > 0) {
				reportError("Time::now() takes no parameters.");
				return false;
			} else {
				result = new NowExprNode(getCoords());
			}
		} else if(functionName.equals("empty")) {
			if(arguments.size() > 0) {
				reportError("empty() takes no parameters.");
				return false;
			} else {
				result = new EmptyExprNode(getCoords());
			}
		} else if(functionName.equals("size")) {
			if(arguments.size() > 0) {
				reportError("size() takes no parameters.");
				return false;
			} else {
				result = new SizeExprNode(getCoords());
			}
		} else if(functionName.equals("source")) {
			if(arguments.size() == 1) {
				result = new SourceExprNode(getCoords(), arguments.get(0), env.getNodeRoot());
			} else {
				reportError(functionName + "() takes 1 parameter.");
				return false;
			}
		} else if(functionName.equals("target")) {
			if(arguments.size() == 1) {
				result = new TargetExprNode(getCoords(), arguments.get(0), env.getNodeRoot());
			} else {
				reportError(functionName + "() takes 1 parameter.");
				return false;
			}
		} else if(functionName.equals("opposite")) {
			if(arguments.size() == 2) {
				result = new OppositeExprNode(getCoords(), arguments.get(0), arguments.get(1), env.getNodeRoot());
			} else {
				reportError(functionName + "() takes 2 parameters.");
				return false;
			}
		} else if(functionName.equals("nodeByName")) {
			if(arguments.size() >= 1 && arguments.size() <= 2) {
				result = new NodeByNameExprNode(getCoords(), arguments.get(0),
						arguments.size() == 2 ? arguments.get(1) : new IdentExprNode(env.getNodeRoot()));
			} else {
				reportError(functionName + "() takes one or two parameters.");
				return false;
			}
		} else if(functionName.equals("edgeByName")) {
			if(arguments.size() >= 1 && arguments.size() <= 2) {
				result = new EdgeByNameExprNode(getCoords(), arguments.get(0),
						arguments.size() == 2 ? arguments.get(1) : new IdentExprNode(env.getArbitraryEdgeRoot()));
			} else {
				reportError(functionName + "() takes one or two parameters.");
				return false;
			}
		} else if(functionName.equals("nodeByUnique")) {
			if(arguments.size() >= 1 && arguments.size() <= 2) {
				result = new NodeByUniqueExprNode(getCoords(), arguments.get(0),
						arguments.size() == 2 ? arguments.get(1) : new IdentExprNode(env.getNodeRoot()));
			} else {
				reportError(functionName + "() takes one or two parameters.");
				return false;
			}
		} else if(functionName.equals("edgeByUnique")) {
			if(arguments.size() >= 1 && arguments.size() <= 2) {
				result = new EdgeByUniqueExprNode(getCoords(), arguments.get(0),
						arguments.size() == 2 ? arguments.get(1) : new IdentExprNode(env.getArbitraryEdgeRoot()));
			} else {
				reportError(functionName + "() takes one or two parameters.");
				return false;
			}
		} else if(functionName.equals("incoming")
				|| functionName.equals("outgoing")
				|| functionName.equals("incident")) {
			int direction;
			if(functionName.equals("incoming"))
				direction = IncidentEdgeExprNode.INCOMING;
			else if(functionName.equals("outgoing"))
				direction = IncidentEdgeExprNode.OUTGOING;
			else
				direction = IncidentEdgeExprNode.INCIDENT;

			if(arguments.size() == 1) {
				result = new IncidentEdgeExprNode(getCoords(), arguments.get(0),
						new IdentExprNode(env.getArbitraryEdgeRoot()), direction, new IdentExprNode(env.getNodeRoot()));
			} else if(arguments.size() == 2) {
				result = new IncidentEdgeExprNode(getCoords(), arguments.get(0), arguments.get(1), direction,
						new IdentExprNode(env.getNodeRoot()));
			} else if(arguments.size() == 3) {
				result = new IncidentEdgeExprNode(getCoords(), arguments.get(0), arguments.get(1), direction, arguments.get(2));
			} else {
				reportError(functionName + "() takes 1-3 parameters.");
				return false;
			}
		} else if(functionName.equals("adjacentIncoming")
				|| functionName.equals("adjacentOutgoing")
				|| functionName.equals("adjacent")) {
			int direction;
			if(functionName.equals("adjacentIncoming"))
				direction = AdjacentNodeExprNode.INCOMING;
			else if(functionName.equals("adjacentOutgoing"))
				direction = AdjacentNodeExprNode.OUTGOING;
			else
				direction = AdjacentNodeExprNode.ADJACENT;

			if(arguments.size() == 1) {
				result = new AdjacentNodeExprNode(getCoords(), arguments.get(0),
						new IdentExprNode(env.getArbitraryEdgeRoot()), direction, new IdentExprNode(env.getNodeRoot()));
			} else if(arguments.size() == 2) {
				result = new AdjacentNodeExprNode(getCoords(), arguments.get(0), arguments.get(1), direction,
						new IdentExprNode(env.getNodeRoot()));
			} else if(arguments.size() == 3) {
				result = new AdjacentNodeExprNode(getCoords(), arguments.get(0), arguments.get(1), direction, arguments.get(2));
			} else {
				reportError(functionName + "() takes 1-3 parameters.");
				return false;
			}
		} else if(functionName.equals("countIncoming")
				|| functionName.equals("countOutgoing")
				|| functionName.equals("countIncident")) {
			int direction;
			if(functionName.equals("countIncoming"))
				direction = CountIncidentEdgeExprNode.INCOMING;
			else if(functionName.equals("countOutgoing"))
				direction = CountIncidentEdgeExprNode.OUTGOING;
			else
				direction = CountIncidentEdgeExprNode.INCIDENT;

			if(arguments.size() == 1) {
				result = new CountIncidentEdgeExprNode(getCoords(), arguments.get(0),
						new IdentExprNode(env.getArbitraryEdgeRoot()), direction, new IdentExprNode(env.getNodeRoot()));
			} else if(arguments.size() == 2) {
				result = new CountIncidentEdgeExprNode(getCoords(), arguments.get(0), arguments.get(1), direction,
						new IdentExprNode(env.getNodeRoot()));
			} else if(arguments.size() == 3) {
				result = new CountIncidentEdgeExprNode(getCoords(), arguments.get(0), arguments.get(1), direction,
						arguments.get(2));
			} else {
				reportError(functionName + "() takes 1-3 parameters.");
				return false;
			}
		} else if(functionName.equals("countAdjacentIncoming")
				|| functionName.equals("countAdjacentOutgoing")
				|| functionName.equals("countAdjacent")) {
			int direction;
			if(functionName.equals("countAdjacentIncoming"))
				direction = CountAdjacentNodeExprNode.INCOMING;
			else if(functionName.equals("countAdjacentOutgoing"))
				direction = CountAdjacentNodeExprNode.OUTGOING;
			else
				direction = CountAdjacentNodeExprNode.ADJACENT;

			if(arguments.size() == 1) {
				result = new CountAdjacentNodeExprNode(getCoords(), arguments.get(0),
						new IdentExprNode(env.getArbitraryEdgeRoot()), direction, new IdentExprNode(env.getNodeRoot()));
			} else if(arguments.size() == 2) {
				result = new CountAdjacentNodeExprNode(getCoords(), arguments.get(0), arguments.get(1), direction,
						new IdentExprNode(env.getNodeRoot()));
			} else if(arguments.size() == 3) {
				result = new CountAdjacentNodeExprNode(getCoords(), arguments.get(0), arguments.get(1), direction,
						arguments.get(2));
			} else {
				reportError(functionName + "() takes 1-3 parameters.");
				return false;
			}
		} else if(functionName.equals("isIncoming")
				|| functionName.equals("isOutgoing")
				|| functionName.equals("isIncident")) {
			int direction;
			if(functionName.equals("isIncoming"))
				direction = IsIncidentEdgeExprNode.INCOMING;
			else if(functionName.equals("isOutgoing"))
				direction = IsIncidentEdgeExprNode.OUTGOING;
			else
				direction = IsIncidentEdgeExprNode.INCIDENT;

			if(arguments.size() == 2) {
				result = new IsIncidentEdgeExprNode(getCoords(), arguments.get(0), arguments.get(1),
						new IdentExprNode(env.getArbitraryEdgeRoot()), direction, new IdentExprNode(env.getNodeRoot()));
			} else if(arguments.size() == 3) {
				result = new IsIncidentEdgeExprNode(getCoords(), arguments.get(0), arguments.get(1), arguments.get(2), direction,
						new IdentExprNode(env.getNodeRoot()));
			} else if(arguments.size() == 4) {
				result = new IsIncidentEdgeExprNode(getCoords(), arguments.get(0), arguments.get(1), arguments.get(2), direction,
						arguments.get(3));
			} else {
				reportError(functionName + "() takes 2-4 parameters.");
				return false;
			}
		} else if(functionName.equals("isAdjacentIncoming")
				|| functionName.equals("isAdjacentOutgoing")
				|| functionName.equals("isAdjacent")) {
			int direction;
			if(functionName.equals("isReachableEdgesIncoming"))
				direction = IsAdjacentNodeExprNode.INCOMING;
			else if(functionName.equals("isReachableEdgesOutgoing"))
				direction = IsAdjacentNodeExprNode.OUTGOING;
			else
				direction = IsAdjacentNodeExprNode.ADJACENT;

			if(arguments.size() == 2) {
				result = new IsAdjacentNodeExprNode(getCoords(), arguments.get(0), arguments.get(1),
						new IdentExprNode(env.getArbitraryEdgeRoot()), direction, new IdentExprNode(env.getNodeRoot()));
			} else if(arguments.size() == 3) {
				result = new IsAdjacentNodeExprNode(getCoords(), arguments.get(0), arguments.get(1), arguments.get(2), direction,
						new IdentExprNode(env.getNodeRoot()));
			} else if(arguments.size() == 4) {
				result = new IsAdjacentNodeExprNode(getCoords(), arguments.get(0), arguments.get(1), arguments.get(2), direction,
						arguments.get(3));
			} else {
				reportError(functionName + "() takes 2-4 parameters.");
				return false;
			}
		} else if(functionName.equals("reachableEdgesIncoming")
				|| functionName.equals("reachableEdgesOutgoing")
				|| functionName.equals("reachableEdges")) {
			int direction;
			if(functionName.equals("reachableEdgesIncoming"))
				direction = ReachableEdgeExprNode.INCOMING;
			else if(functionName.equals("reachableEdgesOutgoing"))
				direction = ReachableEdgeExprNode.OUTGOING;
			else
				direction = ReachableEdgeExprNode.INCIDENT;

			if(arguments.size() == 1) {
				result = new ReachableEdgeExprNode(getCoords(), arguments.get(0),
						new IdentExprNode(env.getArbitraryEdgeRoot()), direction, new IdentExprNode(env.getNodeRoot()));
			} else if(arguments.size() == 2) {
				result = new ReachableEdgeExprNode(getCoords(), arguments.get(0), arguments.get(1), direction,
						new IdentExprNode(env.getNodeRoot()));
			} else if(arguments.size() == 3) {
				result = new ReachableEdgeExprNode(getCoords(), arguments.get(0), arguments.get(1), direction, arguments.get(2));
			} else {
				reportError(functionName + "() takes 1-3 parameters.");
				return false;
			}
		} else if(functionName.equals("reachableIncoming")
				|| functionName.equals("reachableOutgoing")
				|| functionName.equals("reachable")) {
			int direction;
			if(functionName.equals("reachableIncoming"))
				direction = ReachableNodeExprNode.INCOMING;
			else if(functionName.equals("reachableOutgoing"))
				direction = ReachableNodeExprNode.OUTGOING;
			else
				direction = ReachableNodeExprNode.ADJACENT;

			if(arguments.size() == 1) {
				result = new ReachableNodeExprNode(getCoords(), arguments.get(0),
						new IdentExprNode(env.getArbitraryEdgeRoot()), direction, new IdentExprNode(env.getNodeRoot()));
			} else if(arguments.size() == 2) {
				result = new ReachableNodeExprNode(getCoords(), arguments.get(0), arguments.get(1), direction,
						new IdentExprNode(env.getNodeRoot()));
			} else if(arguments.size() == 3) {
				result = new ReachableNodeExprNode(getCoords(), arguments.get(0), arguments.get(1), direction, arguments.get(2));
			} else {
				reportError(functionName + "() takes 1-3 parameters.");
				return false;
			}
		} else if(functionName.equals("countReachableEdgesIncoming")
				|| functionName.equals("countReachableEdgesOutgoing")
				|| functionName.equals("countReachableEdges")) {
			int direction;
			if(functionName.equals("countReachableEdgesIncoming"))
				direction = CountReachableEdgeExprNode.INCOMING;
			else if(functionName.equals("countReachableEdgesOutgoing"))
				direction = CountReachableEdgeExprNode.OUTGOING;
			else
				direction = CountReachableEdgeExprNode.INCIDENT;

			if(arguments.size() == 1) {
				result = new CountReachableEdgeExprNode(getCoords(), arguments.get(0),
						new IdentExprNode(env.getArbitraryEdgeRoot()), direction, new IdentExprNode(env.getNodeRoot()));
			} else if(arguments.size() == 2) {
				result = new CountReachableEdgeExprNode(getCoords(), arguments.get(0), arguments.get(1), direction,
						new IdentExprNode(env.getNodeRoot()));
			} else if(arguments.size() == 3) {
				result = new CountReachableEdgeExprNode(getCoords(), arguments.get(0), arguments.get(1), direction,
						arguments.get(2));
			} else {
				reportError(functionName + "() takes 1-3 parameters.");
				return false;
			}
		} else if(functionName.equals("countReachableIncoming")
				|| functionName.equals("countReachableOutgoing")
				|| functionName.equals("countReachable")) {
			int direction;
			if(functionName.equals("countReachableIncoming"))
				direction = CountReachableNodeExprNode.INCOMING;
			else if(functionName.equals("countReachableOutgoing"))
				direction = CountReachableNodeExprNode.OUTGOING;
			else
				direction = CountReachableNodeExprNode.ADJACENT;

			if(arguments.size() == 1) {
				result = new CountReachableNodeExprNode(getCoords(), arguments.get(0),
						new IdentExprNode(env.getArbitraryEdgeRoot()), direction, new IdentExprNode(env.getNodeRoot()));
			} else if(arguments.size() == 2) {
				result = new CountReachableNodeExprNode(getCoords(), arguments.get(0), arguments.get(1), direction,
						new IdentExprNode(env.getNodeRoot()));
			} else if(arguments.size() == 3) {
				result = new CountReachableNodeExprNode(getCoords(), arguments.get(0), arguments.get(1), direction,
						arguments.get(2));
			} else {
				reportError(functionName + "() takes 1-3 parameters.");
				return false;
			}
		} else if(functionName.equals("isReachableIncoming")
				|| functionName.equals("isReachableOutgoing")
				|| functionName.equals("isReachable")) {
			int direction;
			if(functionName.equals("isReachableIncoming"))
				direction = IsReachableNodeExprNode.INCOMING;
			else if(functionName.equals("isReachableOutgoing"))
				direction = IsReachableNodeExprNode.OUTGOING;
			else
				direction = IsReachableNodeExprNode.ADJACENT;

			if(arguments.size() == 2) {
				result = new IsReachableNodeExprNode(getCoords(), arguments.get(0), arguments.get(1),
						new IdentExprNode(env.getArbitraryEdgeRoot()), direction, new IdentExprNode(env.getNodeRoot()));
			} else if(arguments.size() == 3) {
				result = new IsReachableNodeExprNode(getCoords(), arguments.get(0), arguments.get(1), arguments.get(2),
						direction, new IdentExprNode(env.getNodeRoot()));
			} else if(arguments.size() == 4) {
				result = new IsReachableNodeExprNode(getCoords(), arguments.get(0), arguments.get(1), arguments.get(2),
						direction, arguments.get(3));
			} else {
				reportError(functionName + "() takes 2-4 parameters.");
				return false;
			}
		} else if(functionName.equals("isReachableEdgesIncoming")
				|| functionName.equals("isReachableEdgesOutgoing")
				|| functionName.equals("isReachableEdges")) {
			int direction;
			if(functionName.equals("isReachableEdgesIncoming"))
				direction = IsReachableEdgeExprNode.INCOMING;
			else if(functionName.equals("isReachableEdgesOutgoing"))
				direction = IsReachableEdgeExprNode.OUTGOING;
			else
				direction = IsReachableEdgeExprNode.INCIDENT;

			if(arguments.size() == 2) {
				result = new IsReachableEdgeExprNode(getCoords(), arguments.get(0), arguments.get(1),
						new IdentExprNode(env.getArbitraryEdgeRoot()), direction, new IdentExprNode(env.getNodeRoot()));
			} else if(arguments.size() == 3) {
				result = new IsReachableEdgeExprNode(getCoords(), arguments.get(0), arguments.get(1), arguments.get(2),
						direction, new IdentExprNode(env.getNodeRoot()));
			} else if(arguments.size() == 4) {
				result = new IsReachableEdgeExprNode(getCoords(), arguments.get(0), arguments.get(1), arguments.get(2),
						direction, arguments.get(3));
			} else {
				reportError(functionName + "() takes 2-4 parameters.");
				return false;
			}
		} else if(functionName.equals("boundedReachableEdgesIncoming")
				|| functionName.equals("boundedReachableEdgesOutgoing")
				|| functionName.equals("boundedReachableEdges")) {
			int direction;
			if(functionName.equals("boundedReachableEdgesIncoming"))
				direction = BoundedReachableEdgeExprNode.INCOMING;
			else if(functionName.equals("boundedReachableEdgesOutgoing"))
				direction = BoundedReachableEdgeExprNode.OUTGOING;
			else
				direction = BoundedReachableEdgeExprNode.INCIDENT;

			if(arguments.size() == 2) {
				result = new BoundedReachableEdgeExprNode(getCoords(), arguments.get(0), arguments.get(1),
						new IdentExprNode(env.getArbitraryEdgeRoot()), direction, new IdentExprNode(env.getNodeRoot()));
			} else if(arguments.size() == 3) {
				result = new BoundedReachableEdgeExprNode(getCoords(), arguments.get(0), arguments.get(1), arguments.get(2),
						direction, new IdentExprNode(env.getNodeRoot()));
			} else if(arguments.size() == 4) {
				result = new BoundedReachableEdgeExprNode(getCoords(), arguments.get(0), arguments.get(1), arguments.get(2),
						direction, arguments.get(3));
			} else {
				reportError(functionName + "() takes 2-4 parameters.");
				return false;
			}
		} else if(functionName.equals("boundedReachableIncoming")
				|| functionName.equals("boundedReachableOutgoing")
				|| functionName.equals("boundedReachable")) {
			int direction;
			if(functionName.equals("boundedReachableIncoming"))
				direction = BoundedReachableNodeExprNode.INCOMING;
			else if(functionName.equals("boundedReachableOutgoing"))
				direction = BoundedReachableNodeExprNode.OUTGOING;
			else
				direction = BoundedReachableNodeExprNode.ADJACENT;

			if(arguments.size() == 2) {
				result = new BoundedReachableNodeExprNode(getCoords(), arguments.get(0), arguments.get(1),
						new IdentExprNode(env.getArbitraryEdgeRoot()), direction, new IdentExprNode(env.getNodeRoot()));
			} else if(arguments.size() == 3) {
				result = new BoundedReachableNodeExprNode(getCoords(), arguments.get(0), arguments.get(1), arguments.get(2),
						direction, new IdentExprNode(env.getNodeRoot()));
			} else if(arguments.size() == 4) {
				result = new BoundedReachableNodeExprNode(getCoords(), arguments.get(0), arguments.get(1), arguments.get(2),
						direction, arguments.get(3));
			} else {
				reportError(functionName + "() takes 2-4 parameters.");
				return false;
			}
		} else if(functionName.equals("boundedReachableWithRemainingDepthIncoming")
				|| functionName.equals("boundedReachableWithRemainingDepthOutgoing")
				|| functionName.equals("boundedReachableWithRemainingDepth")) {
			int direction;
			if(functionName.equals("boundedReachableWithRemainingDepthIncoming"))
				direction = BoundedReachableNodeWithRemainingDepthExprNode.INCOMING;
			else if(functionName.equals("boundedReachableWithRemainingDepthOutgoing"))
				direction = BoundedReachableNodeWithRemainingDepthExprNode.OUTGOING;
			else
				direction = BoundedReachableNodeWithRemainingDepthExprNode.ADJACENT;

			if(arguments.size() == 2) {
				result = new BoundedReachableNodeWithRemainingDepthExprNode(getCoords(), arguments.get(0), arguments.get(1),
						new IdentExprNode(env.getArbitraryEdgeRoot()), direction, new IdentExprNode(env.getNodeRoot()));
			} else if(arguments.size() == 3) {
				result = new BoundedReachableNodeWithRemainingDepthExprNode(getCoords(), arguments.get(0), arguments.get(1),
						arguments.get(2), direction, new IdentExprNode(env.getNodeRoot()));
			} else if(arguments.size() == 4) {
				result = new BoundedReachableNodeWithRemainingDepthExprNode(getCoords(), arguments.get(0), arguments.get(1),
						arguments.get(2), direction, arguments.get(3));
			} else {
				reportError(functionName + "() takes 2-4 parameters.");
				return false;
			}
		} else if(functionName.equals("countBoundedReachableEdgesIncoming")
				|| functionName.equals("countBoundedReachableEdgesOutgoing")
				|| functionName.equals("countBoundedReachableEdges")) {
			int direction;
			if(functionName.equals("countBoundedReachableEdgesIncoming"))
				direction = CountBoundedReachableEdgeExprNode.INCOMING;
			else if(functionName.equals("countBoundedReachableEdgesOutgoing"))
				direction = CountBoundedReachableEdgeExprNode.OUTGOING;
			else
				direction = CountBoundedReachableEdgeExprNode.INCIDENT;

			if(arguments.size() == 2) {
				result = new CountBoundedReachableEdgeExprNode(getCoords(), arguments.get(0), arguments.get(1),
						new IdentExprNode(env.getArbitraryEdgeRoot()), direction, new IdentExprNode(env.getNodeRoot()));
			} else if(arguments.size() == 3) {
				result = new CountBoundedReachableEdgeExprNode(getCoords(), arguments.get(0), arguments.get(1), arguments.get(2),
						direction, new IdentExprNode(env.getNodeRoot()));
			} else if(arguments.size() == 4) {
				result = new CountBoundedReachableEdgeExprNode(getCoords(), arguments.get(0), arguments.get(1), arguments.get(2),
						direction, arguments.get(3));
			} else {
				reportError(functionName + "() takes 2-4 parameters.");
				return false;
			}
		} else if(functionName.equals("countBoundedReachableIncoming")
				|| functionName.equals("countBoundedReachableOutgoing")
				|| functionName.equals("countBoundedReachable")) {
			int direction;
			if(functionName.equals("countBoundedReachableIncoming"))
				direction = CountBoundedReachableNodeExprNode.INCOMING;
			else if(functionName.equals("countBoundedReachableOutgoing"))
				direction = CountBoundedReachableNodeExprNode.OUTGOING;
			else
				direction = CountBoundedReachableNodeExprNode.ADJACENT;

			if(arguments.size() == 2) {
				result = new CountBoundedReachableNodeExprNode(getCoords(), arguments.get(0), arguments.get(1),
						new IdentExprNode(env.getArbitraryEdgeRoot()), direction, new IdentExprNode(env.getNodeRoot()));
			} else if(arguments.size() == 3) {
				result = new CountBoundedReachableNodeExprNode(getCoords(), arguments.get(0), arguments.get(1), arguments.get(2),
						direction, new IdentExprNode(env.getNodeRoot()));
			} else if(arguments.size() == 4) {
				result = new CountBoundedReachableNodeExprNode(getCoords(), arguments.get(0), arguments.get(1), arguments.get(2),
						direction, arguments.get(3));
			} else {
				reportError(functionName + "() takes 2-4 parameters.");
				return false;
			}
		} else if(functionName.equals("isBoundedReachableIncoming")
				|| functionName.equals("isBoundedReachableOutgoing")
				|| functionName.equals("isBoundedReachable")) {
			int direction;
			if(functionName.equals("isBoundedReachableIncoming"))
				direction = IsBoundedReachableNodeExprNode.INCOMING;
			else if(functionName.equals("isBoundedReachableOutgoing"))
				direction = IsBoundedReachableNodeExprNode.OUTGOING;
			else
				direction = IsBoundedReachableNodeExprNode.ADJACENT;

			if(arguments.size() == 3) {
				result = new IsBoundedReachableNodeExprNode(getCoords(), arguments.get(0), arguments.get(1), arguments.get(2),
						new IdentExprNode(env.getArbitraryEdgeRoot()), direction, new IdentExprNode(env.getNodeRoot()));
			} else if(arguments.size() == 4) {
				result = new IsBoundedReachableNodeExprNode(getCoords(), arguments.get(0), arguments.get(1), arguments.get(2),
						arguments.get(3), direction, new IdentExprNode(env.getNodeRoot()));
			} else if(arguments.size() == 5) {
				result = new IsBoundedReachableNodeExprNode(getCoords(), arguments.get(0), arguments.get(1), arguments.get(2),
						arguments.get(3), direction, arguments.get(4));
			} else {
				reportError(functionName + "() takes 3-5 parameters.");
				return false;
			}
		} else if(functionName.equals("isBoundedReachableEdgesIncoming")
				|| functionName.equals("isBoundedReachableEdgesOutgoing")
				|| functionName.equals("isBoundedReachableEdges")) {
			int direction;
			if(functionName.equals("isBoundedReachableEdgesIncoming"))
				direction = IsBoundedReachableEdgeExprNode.INCOMING;
			else if(functionName.equals("isBoundedReachableEdgesOutgoing"))
				direction = IsBoundedReachableEdgeExprNode.OUTGOING;
			else
				direction = IsBoundedReachableEdgeExprNode.INCIDENT;

			if(arguments.size() == 3) {
				result = new IsBoundedReachableEdgeExprNode(getCoords(), arguments.get(0), arguments.get(1), arguments.get(2),
						new IdentExprNode(env.getArbitraryEdgeRoot()), direction, new IdentExprNode(env.getNodeRoot()));
			} else if(arguments.size() == 4) {
				result = new IsBoundedReachableEdgeExprNode(getCoords(), arguments.get(0), arguments.get(1), arguments.get(2),
						arguments.get(3), direction, new IdentExprNode(env.getNodeRoot()));
			} else if(arguments.size() == 5) {
				result = new IsBoundedReachableEdgeExprNode(getCoords(), arguments.get(0), arguments.get(1), arguments.get(2),
						arguments.get(3), direction, arguments.get(4));
			} else {
				reportError(functionName + "() takes 3-5 parameters.");
				return false;
			}
		} else if(functionName.equals("inducedSubgraph")) {
			if(arguments.size() != 1) {
				reportError("inducedSubgraph(.) takes one parameter.");
				return false;
			} else
				result = new InducedSubgraphExprNode(getCoords(), arguments.get(0));
		} else if(functionName.equals("definedSubgraph")) {
			if(arguments.size() != 1) {
				reportError("definedSubgraph(.) takes one parameter.");
				return false;
			} else
				result = new DefinedSubgraphExprNode(getCoords(), arguments.get(0));
		} else if(functionName.equals("equalsAny")) {
			if(arguments.size() != 2) {
				reportError("equalsAny(.,.) takes two parameters.");
				return false;
			} else
				result = new EqualsAnyExprNode(getCoords(), arguments.get(0), arguments.get(1), true);
		} else if(functionName.equals("equalsAnyStructurally")) {
			if(arguments.size() != 2) {
				reportError("equalsAnyStructurally(.,.) takes two parameters.");
				return false;
			} else
				result = new EqualsAnyExprNode(getCoords(), arguments.get(0), arguments.get(1), false);
		} else if(functionName.equals("existsFile")) {
			if(arguments.size() != 1) {
				reportError("File::exists(.) takes one parameter.");
				return false;
			} else
				result = new ExistsFileExprNode(getCoords(), arguments.get(0));
		} else if(functionName.equals("importFile")) {
			if(arguments.size() != 1) {
				reportError("File::import(.) takes one parameter.");
				return false;
			} else
				result = new ImportExprNode(getCoords(), arguments.get(0));
		} else if(functionName.equals("copy")) {
			if(arguments.size() != 1) {
				reportError("copy(.) takes one parameter.");
				return false;
			} else
				result = new CopyExprNode(getCoords(), arguments.get(0));
		} else if(functionName.equals("canonize")) {
			if(arguments.size() != 1) {
				reportError("canonize(.) takes one parameter.");
				return false;
			} else
				result = new CanonizeExprNode(getCoords(), arguments.get(0));
		} else if(functionName.equals("uniqueof")) {
			if(arguments.size() > 1) {
				reportError("uniqueof(.) takes none or one parameter.");
				return false;
			} else if(arguments.size() == 1)
				result = new UniqueofExprNode(getCoords(), arguments.get(0));
			else
				result = new UniqueofExprNode(getCoords(), null);
		} else {
			reportError("no function " + functionName + " known");
			return false;
		}
		return true;
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

	protected ExprNode getResult()
	{
		return result;
	}

	@Override
	protected IR constructIR()
	{
		return result.getIR();
	}
}
