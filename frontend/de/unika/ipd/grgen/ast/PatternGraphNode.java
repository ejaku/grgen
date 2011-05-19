/*
 * GrGen: graph rewrite generator tool -- release GrGen.NET 3.0
 * Copyright (C) 2003-2011 Universitaet Karlsruhe, Institut fuer Programmstrukturen und Datenorganisation, LS Goos
 * licensed under LGPL v3 (see LICENSE.txt included in the packaging of this file)
 * www.grgen.net
 */

/**
 * PatternGraphNode.java
 *
 * @author Sebastian Hack
 */

package de.unika.ipd.grgen.ast;

import java.util.Collection;
import java.util.Collections;
import java.util.HashSet;
import java.util.LinkedHashMap;
import java.util.LinkedHashSet;
import java.util.LinkedList;
import java.util.List;
import java.util.Map;
import java.util.Set;
import java.util.Vector;

import de.unika.ipd.grgen.ir.Alternative;
import de.unika.ipd.grgen.ir.Edge;
import de.unika.ipd.grgen.ir.EvalStatement;
import de.unika.ipd.grgen.ir.Expression;
import de.unika.ipd.grgen.ir.GraphEntity;
import de.unika.ipd.grgen.ir.GraphEntityExpression;
import de.unika.ipd.grgen.ir.IR;
import de.unika.ipd.grgen.ir.NeededEntities;
import de.unika.ipd.grgen.ir.Node;
import de.unika.ipd.grgen.ir.Variable;
import de.unika.ipd.grgen.ir.Operator;
import de.unika.ipd.grgen.ir.PatternGraph;
import de.unika.ipd.grgen.ir.Rule;
import de.unika.ipd.grgen.ir.SubpatternUsage;
import de.unika.ipd.grgen.ir.Typeof;
import de.unika.ipd.grgen.parser.Coords;
import de.unika.ipd.grgen.parser.SymbolTable;


/**
 * AST node that represents a graph pattern as it appears within the pattern
 * part of some rule Extension of the graph pattern of the rewrite part
 */
// TODO: a pattern graph is not a graph, factor the common stuff out into a base class
public class PatternGraphNode extends GraphNode {
	static {
		setName(PatternGraphNode.class, "pattern_graph");
	}

	public static final int MOD_DANGLING = 1; // dangling+identification=dpo
	public static final int MOD_IDENTIFICATION = 2;
	public static final int MOD_EXACT = 4;
	public static final int MOD_INDUCED = 8;
	public static final int MOD_PATTERN_LOCKED = 16;
	public static final int MOD_PATTERNPATH_LOCKED = 32;

	/** The modifiers for this type. An ORed combination of the constants above. */
	private int modifiers = 0;

	private CollectNode<ExprNode> conditions;
	protected CollectNode<AlternativeNode> alts;
	protected CollectNode<IteratedNode> iters;
	protected CollectNode<PatternGraphNode> negs; // NACs
	private CollectNode<PatternGraphNode> idpts; // PACs
	private CollectNode<HomNode> homs;
	private CollectNode<ExactNode> exact;
	private CollectNode<InducedNode> induced;

	// Cache variable
	private Collection<Set<ConstraintDeclNode>> homSets = null;

	/**
	 *  Map an edge to his homomorphic set.
	 *
	 *  NOTE: Use getCorrespondentHomSet() to get the homomorphic set.
	 */
	private Map<EdgeDeclNode, Set<EdgeDeclNode>> edgeHomMap =
		new LinkedHashMap<EdgeDeclNode, Set<EdgeDeclNode>>();

	/**
	 *  Map a node to his homomorphic set.
	 *
	 *  NOTE: Use getCorrespondentHomSet() to get the homomorphic set.
	 */
	private Map<NodeDeclNode, Set<NodeDeclNode>> nodeHomMap =
		new LinkedHashMap<NodeDeclNode, Set<NodeDeclNode>>();

	/** All nodes which needed a single node NAC. */
	private Set<NodeDeclNode> singleNodeNegNodes =
		new LinkedHashSet<NodeDeclNode>();

	/** All node pairs which needed a double node NAC. */
	private Set<List<NodeDeclNode>> doubleNodeNegPairs =
		new LinkedHashSet<List<NodeDeclNode>>();

	/** Map a homomorphic set to a set of edges (of the NAC). */
	private Map<Set<NodeDeclNode>, Set<ConnectionNode>> singleNodeNegMap =
		new LinkedHashMap<Set<NodeDeclNode>, Set<ConnectionNode>>();

	/**
	 * Map each pair of homomorphic sets of nodes to a set of edges (of the
	 * NAC).
	 */
	private Map<List<Set<NodeDeclNode>>, Set<ConnectionNode>> doubleNodeNegMap =
		new LinkedHashMap<List<Set<NodeDeclNode>>, Set<ConnectionNode>>();
	
	// counts number of implicit single and double node negative patterns
	// created from pattern modifiers, in order to get unique negative names
	int implicitNegCounter = 0;
	

	public PatternGraphNode(String nameOfGraph, Coords coords,
			CollectNode<BaseNode> connections, CollectNode<BaseNode> params, 
			CollectNode<VarDeclNode> defVariablesToBeYieldedTo,
			CollectNode<SubpatternUsageNode> subpatterns, CollectNode<OrderedReplacementNode> orderedReplacements,
			CollectNode<AlternativeNode> alts, CollectNode<IteratedNode> iters,
			CollectNode<PatternGraphNode> negs, CollectNode<PatternGraphNode> idpts,
			CollectNode<ExprNode> conditions, 
			CollectNode<EvalStatementNode> yieldsEvals,
			CollectNode<ExprNode> returns,
			CollectNode<HomNode> homs, CollectNode<ExactNode> exact,
			CollectNode<InducedNode> induced, int modifiers, int context) {
		super(nameOfGraph, coords, connections, params, defVariablesToBeYieldedTo, subpatterns, orderedReplacements,
				yieldsEvals, returns, null, context, null);
		this.alts = alts;
		becomeParent(this.alts);
		this.iters = iters;
		becomeParent(this.iters);
		this.negs = negs;
		becomeParent(this.negs);
		this.idpts = idpts;
		becomeParent(this.idpts);
		this.conditions = conditions;
		becomeParent(this.conditions);
		this.homs = homs;
		becomeParent(this.homs);
		this.exact = exact;
		becomeParent(this.exact);
		this.induced = induced;
		becomeParent(this.induced);
		this.modifiers = modifiers;
		
		directlyNestingLHSGraph = this;
		addParamsToConnections(params);
	}

	/** returns children of this node */
	@Override
	public Collection<BaseNode> getChildren() {
		Vector<BaseNode> children = new Vector<BaseNode>();
		children.add(getValidVersion(connectionsUnresolved, connections));
		children.add(params);
		children.add(defVariablesToBeYieldedTo);
		children.add(subpatterns);
		children.add(orderedReplacements);
		children.add(alts);
		children.add(iters);
		children.add(negs);
		children.add(idpts);
		children.add(returns);
		children.add(yieldsEvals);
		children.add(conditions);
		children.add(homs);
		children.add(exact);
		children.add(induced);
		return children;
	}

	/** returns names of the children, same order as in getChildren */
	@Override
	public Collection<String> getChildrenNames() {
		Vector<String> childrenNames = new Vector<String>();
		childrenNames.add("connections");
		childrenNames.add("params");
		childrenNames.add("defVariablesToBeYieldedTo");
		childrenNames.add("subpatterns");
		childrenNames.add("subpatternReplacements");
		childrenNames.add("alternatives");
		childrenNames.add("iters");
		childrenNames.add("negatives");
		childrenNames.add("independents");
		childrenNames.add("return");
		childrenNames.add("yieldsEvals");
		childrenNames.add("conditions");
		childrenNames.add("homs");
		childrenNames.add("exact");
		childrenNames.add("induced");
		return childrenNames;
	}

	/**
	 * @see GraphNode#getNodes()
	 */
	@Override
	public Set<NodeDeclNode> getNodes() {
		assert isResolved();

		if(nodes != null) return nodes;

		LinkedHashSet<NodeDeclNode> coll = new LinkedHashSet<NodeDeclNode>();

		for(BaseNode n : connections.getChildren()) {
			ConnectionCharacter conn = (ConnectionCharacter)n;
			conn.addNodes(coll);
		}

		for (HomNode homNode : homs.getChildren()) {
			for (BaseNode homElem : homNode.getChildren()) {
				if (homElem instanceof NodeDeclNode) {
					coll.add((NodeDeclNode) homElem);
				}
			}
		}

		nodes = Collections.unmodifiableSet(coll);
		return nodes;
	}

	/**
	 * @see GraphNode#getEdges()
	 */
	@Override
	public Set<EdgeDeclNode> getEdges() {
		assert isResolved();

		if(edges != null) return edges;

		LinkedHashSet<EdgeDeclNode> coll = new LinkedHashSet<EdgeDeclNode>();

		for(BaseNode n : connections.getChildren()) {
			ConnectionCharacter conn = (ConnectionCharacter)n;
			conn.addEdge(coll);
		}

		for (HomNode homNode : homs.getChildren()) {
			for (BaseNode homElem : homNode.getChildren()) {
				if (homElem instanceof EdgeDeclNode) {
					coll.add((EdgeDeclNode) homElem);
				}
			}
		}

		edges = Collections.unmodifiableSet(coll);
		return edges;
	}

	private void initHomMaps() {
		Collection<Set<ConstraintDeclNode>> homSets = getHoms();

		// Each node is homomorphic to itself.
		for (NodeDeclNode node : getNodes()) {
			Set<NodeDeclNode> homSet = new LinkedHashSet<NodeDeclNode>();
			homSet.add(node);
			nodeHomMap.put(node, homSet);
		}

		// Each edge is homomorphic to itself.
		for (EdgeDeclNode edge : getEdges()) {
			Set<EdgeDeclNode> homSet = new LinkedHashSet<EdgeDeclNode>();
			homSet.add(edge);
			edgeHomMap.put(edge, homSet);
		}

		for (Set<ConstraintDeclNode> homSet : homSets) {
			// Homomorphic nodes.
			if (homSet.iterator().next() instanceof NodeDeclNode) {
				for (ConstraintDeclNode elem : homSet) {
					NodeDeclNode node = (NodeDeclNode) elem;
					Set<NodeDeclNode> mapEntry = nodeHomMap.get(node);
					for (ConstraintDeclNode homomorphicNode : homSet) {
						mapEntry.add((NodeDeclNode) homomorphicNode);
					}
				}
			}

			// Homomorphic edges.
			if (homSet.iterator().next() instanceof EdgeDeclNode) {
				for (ConstraintDeclNode elem : homSet) {
					EdgeDeclNode edge = (EdgeDeclNode) elem;
					Set<EdgeDeclNode> mapEntry = edgeHomMap.get(edge);
					for (ConstraintDeclNode homomorphicEdge : homSet) {
						mapEntry.add((EdgeDeclNode) homomorphicEdge);
					}
				}
			}
		}
	}

	private PatternGraphNode getParentPatternGraph() {
		for (BaseNode parent : getParents()) {
			if (!(parent instanceof CollectNode<?>)) continue;

			for (BaseNode grandParent : parent.getParents()) {
				if (grandParent instanceof PatternGraphNode) {
					return (PatternGraphNode) grandParent;
				}
			}
		}

		return null;
	}

	private void initHomSets() {
		homSets = new LinkedHashSet<Set<ConstraintDeclNode>>();
		Set<NodeDeclNode> nodes = getNodes();
		Set<EdgeDeclNode> edges = getEdges();

		// Own homomorphic sets.
		for (HomNode homNode : homs.getChildren()) {
			homSets.addAll(splitHoms(homNode.getChildren()));
		}

		// Inherited homomorphic sets.
		for (PatternGraphNode parent = getParentPatternGraph(); parent != null;
				parent = parent.getParentPatternGraph()) {
			for (Set<ConstraintDeclNode> parentHomSet : parent.getHoms()) {
				Set<ConstraintDeclNode> inheritedHomSet = new LinkedHashSet<ConstraintDeclNode>();
				if (parentHomSet.iterator().next() instanceof NodeDeclNode) {
					for (ConstraintDeclNode homNode : parentHomSet) {
						if (nodes.contains(homNode)) {
							inheritedHomSet.add(homNode);
						}
					}
					if (inheritedHomSet.size() > 1) {
						homSets.add(inheritedHomSet);
					}
				} else {
					for (ConstraintDeclNode homEdge : parentHomSet) {
						if (edges.contains(homEdge)) {
							inheritedHomSet.add(homEdge);
						}
					}
					if (inheritedHomSet.size() > 1) {
						homSets.add(inheritedHomSet);
					}
				}
			}
		}
	}

	protected Collection<Set<ConstraintDeclNode>> getHoms() {
		if (homSets == null) {
			initHomSets();
		}

		return homSets;
	}

	/**
	 * Warn if two homomorphic elements can never be matched homomorphic,
	 * because they have incompatible types.
	 */
	private void warnOnSuperfluousHoms() {
		Collection<Set<ConstraintDeclNode>> homSets = getHoms();

		for (Set<ConstraintDeclNode> homSet : homSets) {
			Set<ConstraintDeclNode> alreadyProcessed = new LinkedHashSet<ConstraintDeclNode>();

			for (ConstraintDeclNode elem1 : homSet) {
				InheritanceTypeNode type1 = elem1.getDeclType();
				Collection<InheritanceTypeNode> subTypes1 = type1.getAllSubTypes();
				for (ConstraintDeclNode elem2 : homSet) {
					if (elem1 == elem2 || alreadyProcessed.contains(elem2))
						continue;

					InheritanceTypeNode type2 = elem2.getDeclType();
					Collection<InheritanceTypeNode> subTypes2 = type2.getAllSubTypes();

					boolean hasCommonSubType = type1.isA(type2) || type2.isA(type1);

					if (hasCommonSubType)
						continue;

					for (TypeNode typeNode2 : subTypes2) {
						if (subTypes1.contains(typeNode2)) {
							hasCommonSubType = true;
							break;
						}
					}

					if (!hasCommonSubType) {
						// search hom statement
						HomNode hom = null;
						for (HomNode homNode : homs.getChildren()) {
							Collection<BaseNode> homChildren = homNode.getChildren();
							if (homChildren.contains(elem1) && homChildren.contains(elem2)) {
								hom = homNode;
								break;
							}
						}

						hom.reportWarning(elem1.ident + " and " + elem2.ident
								+ " have no common subtype and thus can never match the same element");
					}
				}

				alreadyProcessed.add(elem1);
			}
		}
	}

	boolean noRewriteInIteratedOrAlternativeNestedInNegativeOrIndependent() {
		boolean result = true;
		for(PatternGraphNode pattern : negs.getChildren()) {
			for(IteratedNode iter : pattern.iters.getChildren()) {
				if(iter.right.size()>0) {
					iter.right.children.get(0).reportError("An iterated contained within a negative can't possess a rewrite part (the negative is a pure negative application condition)");
					result = false;
				}
			}
			for(AlternativeNode alt : pattern.alts.getChildren()) {
				for(AlternativeCaseNode altCase : alt.getChildren()) {
					if(altCase.right.size()>0) {
						altCase.right.children.get(0).reportError("An alternative case contained within a negative can't possess a rewrite part (the negative is a pure negative application condition)");
						result = false;
					}
				}
			}
		}
		for(PatternGraphNode pattern : idpts.getChildren()) {
			for(IteratedNode iter : pattern.iters.getChildren()) {
				if(iter.right.size()>0) {
					iter.right.children.get(0).reportError("An iterated contained within an independent can't possess a rewrite part (the independent is a pure positive application condition)");
					result = false;
				}
			}
			for(AlternativeNode alt : pattern.alts.getChildren()) {
				for(AlternativeCaseNode altCase : alt.getChildren()) {
					if(altCase.right.size()>0) {
						altCase.right.children.get(0).reportError("An alternative case contained within an independent can't possess a rewrite part (the independent is a pure positive application condition)");
						result = false;
					}
				}
			}
		}
		return result;
	}
	
	@Override
	protected boolean checkLocal() {
		boolean childs = super.checkLocal();

		boolean expr = true;
		if (childs) {
			for (ExprNode exp : conditions.getChildren()) {
				if (!exp.getType().isEqual(BasicTypeNode.booleanType)) {
					exp.reportError("Expression must be of type boolean");
					expr = false;
				}
			}
		}

		boolean noReturnInNegOrIdpt = true;
		if((context & CONTEXT_NEGATIVE) == CONTEXT_NEGATIVE
			|| (context & CONTEXT_INDEPENDENT) == CONTEXT_INDEPENDENT) {
			if(returns.size()!=0) {
				reportError("return not allowed in negative or independent block");
				noReturnInNegOrIdpt = false;
			}
		}

		warnOnSuperfluousHoms();

		return childs && expr && noReturnInNegOrIdpt && noRewriteInIteratedOrAlternativeNestedInNegativeOrIndependent();
	}

	/**
	 * Get the correctly casted IR object.
	 *
	 * @return The IR object.
	 */
	protected PatternGraph getPatternGraph() {
		return checkIR(PatternGraph.class);
	}

	/** NOTE: Use this only in DPO-Mode,i.e. if the pattern is part of a rule */
	private RuleDeclNode getRule() {
		for (BaseNode parent : getParents()) {
			if (parent instanceof RuleDeclNode) {
				return (RuleDeclNode) parent;
			}
		}
		assert false;
		return null;
	}

	/**
	 * Generates a type condition if the given graph entity inherits its type
	 * from another element via a typeof expression.
	 */
	private void genTypeCondsFromTypeof(PatternGraph gr, GraphEntity elem) {
		if (elem.inheritsType()) {
			assert !elem.isCopy(); // must extend this function and lgsp nodes if left hand side copy/copyof are wanted meaning compare attributes of exact dynamic types

			Expression e1 = new Typeof(elem);
			Expression e2 = new Typeof(elem.getTypeof());

			Operator op = new Operator(BasicTypeNode.booleanType.getPrimitiveType(), Operator.GE);
			op.addOperand(e1);
			op.addOperand(e2);

			gr.addCondition(op);
		}
	}

	@Override
	protected IR constructIR() {
		PatternGraph gr = new PatternGraph(nameOfGraph, modifiers);
		gr.setDirectlyNestingLHSGraph(gr);
		
		// mark this node as already visited
		setIR(gr);

		for (BaseNode connection : connections.getChildren()) {
			ConnectionCharacter conn = (ConnectionCharacter) connection;
			conn.addToGraph(gr);
		}

		for(VarDeclNode n : defVariablesToBeYieldedTo.getChildren()) {
			gr.addVariable(n.checkIR(Variable.class));
		}

		for(BaseNode subpatternUsage : subpatterns.getChildren()) {
			gr.addSubpatternUsage(subpatternUsage.checkIR(SubpatternUsage.class));
		}

		// add subpattern usage connection elements only mentioned there to the IR
		// (they're declared in an enclosing graph and locally only show up in the subpattern usage connection)
		for(BaseNode n : subpatterns.getChildren()) {
			List<Expression> connections = n.checkIR(SubpatternUsage.class).getSubpatternConnections();
			for(Expression e : connections) {
				if(e instanceof GraphEntityExpression) {
					GraphEntity connection = ((GraphEntityExpression)e).getGraphEntity();
					if(connection instanceof Node) {
						addNodeIfNotYetContained(gr, (Node)connection);
					} else if(connection instanceof Edge) {
						addEdgeIfNotYetContained(gr, (Edge)connection);
					} else {
						assert(false);
					}
				} else {
					NeededEntities needs = new NeededEntities(false, false, true, false, false, false);
					e.collectNeededEntities(needs);
					for(Variable neededVariable : needs.variables) {
						if(!gr.hasVar(neededVariable)) {
							gr.addVariable(neededVariable);
						}
					}
				}
			}
		}

		// add subpattern usage yield elements only mentioned there to the IR
		// (they're declared in an enclosing graph and locally only show up in the subpattern usage yield)
		for(BaseNode n : subpatterns.getChildren()) {
			List<Expression> yields = n.checkIR(SubpatternUsage.class).getSubpatternYields();
			for(Expression e : yields) {
				if(e instanceof GraphEntityExpression) {
					GraphEntity connection = ((GraphEntityExpression)e).getGraphEntity();
					if(connection instanceof Node) {
						addNodeIfNotYetContained(gr, (Node)connection);
					} else if(connection instanceof Edge) {
						addEdgeIfNotYetContained(gr, (Edge)connection);
					} else {
						assert(false);
					}
				} else {
					NeededEntities needs = new NeededEntities(false, false, true, false, false, false);
					e.collectNeededEntities(needs);
					for(Variable neededVariable : needs.variables) {
						if(!gr.hasVar(neededVariable)) {
							gr.addVariable(neededVariable);
						}
					}
				}
			}
		}

		for(AlternativeNode alternativeNode : alts.getChildren()) {
			Alternative alternative = alternativeNode.checkIR(Alternative.class);
			gr.addAlternative(alternative);
		}

		for(IteratedNode iterNode : iters.getChildren()) {
			Rule iter = iterNode.checkIR(Rule.class);
			gr.addIterated(iter);
		}

		for (ExprNode expr : conditions.getChildren()) {
			ExprNode exprEvaluated = expr.evaluate(); // compile time evaluation (constant folding)
			if(exprEvaluated instanceof BoolConstNode) {
				if((Boolean)((BoolConstNode) exprEvaluated).getValue()) {
					exprEvaluated.reportWarning("Condition is always true");
					continue;
				}
				exprEvaluated.reportWarning("Condition is always false, pattern will never match");
			}

			gr.addCondition(exprEvaluated.checkIR(Expression.class));
		}

		// add yield assignments to the IR
		for (EvalStatement n : getYieldEvalStatements()) {
			gr.addYield(n);
		}
		
		// generate type conditions from dynamic type checks via typeof
		// add elements only mentioned in typeof to the pattern
		Set<Node> nodesToAdd = new HashSet<Node>();
		Set<Edge> edgesToAdd = new HashSet<Edge>();
		for (GraphEntity n : gr.getNodes()) {
			genTypeCondsFromTypeof(gr, n);
			
			if (n.inheritsType()) {
				nodesToAdd.add((Node)n.getTypeof());
			}
		}
		for (GraphEntity e : gr.getEdges()) {
			genTypeCondsFromTypeof(gr, e);

			if (e.inheritsType()) {
				edgesToAdd.add((Edge)e.getTypeof());
			}
		}

		// add Condition elements only mentioned there to the IR
		// (they're declared in an enclosing graph and locally only show up in the condition)
		NeededEntities needs = new NeededEntities(true, true, true, false, false, true);
		for(Expression cond : gr.getConditions()) {
			cond.collectNeededEntities(needs);
		}
		addNeededEntities(gr, needs);

		// add Yielded elements only mentioned there to the IR
		// (they're declared in an enclosing graph and locally only show up in the yield)
		needs = new NeededEntities(true, true, true, false, false, true);
		for(EvalStatement yield : gr.getYields()) {
			yield.collectNeededEntities(needs);
		}
		addNeededEntities(gr, needs);

		for (Set<ConstraintDeclNode> homSet : getHoms()) {
            // homSet is not empty
			if (homSet.iterator().next() instanceof NodeDeclNode) {
				HashSet<Node> homSetIR = new HashSet<Node>();
	    		for (DeclNode decl : homSet) {
	    			homSetIR.add(decl.checkIR(Node.class));
	    		}
	            gr.addHomomorphicNodes(homSetIR);
            }
			// homSet is not empty
            if (homSet.iterator().next() instanceof EdgeDeclNode) {
				HashSet<Edge> homSetIR = new HashSet<Edge>();
	    		for (DeclNode decl : homSet) {
	    			homSetIR.add(decl.checkIR(Edge.class));
	    		}
	            gr.addHomomorphicEdges(homSetIR);
            }
        }

		// add elements only mentioned in hom-declaration to the IR
		// (they're declared in an enclosing graph and locally only show up in the hom-declaration)
		Collection<Collection<? extends GraphEntity>> homSets = gr.getHomomorphic();
		for(Collection<? extends GraphEntity> homSet : homSets)	{
			for(GraphEntity entity : homSet) {
				if(entity instanceof Node) {
					addNodeIfNotYetContained(gr, (Node)entity);
				} else {
					addEdgeIfNotYetContained(gr, (Edge)entity);
				}
			}
		}

		// add elements only mentioned in "map by / draw from storage" entities to the IR
		// (they're declared in an enclosing graph and locally only show up in the "map by / draw from storage" node)
		for(Node node : gr.getNodes()) {
			if(node.getStorage()!=null) {
				if(!gr.hasVar(node.getStorage())) {
					gr.addVariable(node.getStorage());
				}
			}

			if(node.getStorageAttribute()!=null) {		
				if(node.getStorageAttribute().getOwner() instanceof Node) {
					nodesToAdd.add((Node)node.getStorageAttribute().getOwner());
				} else if(node.getStorageAttribute().getOwner() instanceof Edge) {
					addEdgeIfNotYetContained(gr, (Edge)node.getStorageAttribute().getOwner());					
				}
			}

			if(node.getAccessor()!=null && node.getAccessor() instanceof Node) {
				nodesToAdd.add((Node)node.getAccessor());
			} else if(node.getAccessor()!=null && node.getAccessor() instanceof Edge) {
				addEdgeIfNotYetContained(gr, (Edge)node.getAccessor());					
			}
		}
		
		for(Edge edge : gr.getEdges()) {
			if(edge.getStorage()!=null) {
				if(!gr.hasVar(edge.getStorage())) {
					gr.addVariable(edge.getStorage());
				}
			}

			if(edge.getStorageAttribute()!=null) {		
				if(edge.getStorageAttribute().getOwner() instanceof Node) {
					addNodeIfNotYetContained(gr, (Node)edge.getStorageAttribute().getOwner());					
				} else if(edge.getStorageAttribute().getOwner() instanceof Edge) {
					edgesToAdd.add((Edge)edge.getStorageAttribute().getOwner());
				}
			}

			if(edge.getAccessor()!=null && edge.getAccessor() instanceof Node) {
				addNodeIfNotYetContained(gr, (Node)edge.getAccessor());					
			} else if(edge.getAccessor()!=null && edge.getAccessor() instanceof Edge) {
				edgesToAdd.add((Edge)edge.getAccessor());
			}
		}

		// add elements which we could not be added before because their container was iterated over
		for(Node n : nodesToAdd)
			addNodeIfNotYetContained(gr, n);
		for(Edge e : edgesToAdd)
			addEdgeIfNotYetContained(gr, e);
		
		// add negative parts to the IR
		for (PatternGraphNode pgn : negs.getChildren()) {
			PatternGraph neg = pgn.getPatternGraph();
			gr.addNegGraph(neg);
		}

		// add independent parts to the IR
		for (PatternGraphNode pgn : idpts.getChildren()) {
			PatternGraph idpt = pgn.getPatternGraph();
			gr.addIdptGraph(idpt);
		}
		
		// ensure def to be yielded to elements are hom to all others
		// so backend doing some fake search planning for them is not scheduling checks for them
		for (Node node : gr.getNodes()) {
			if(node.isDefToBeYieldedTo())
				gr.addHomToAll(node);
		}
		for (Edge edge : gr.getEdges()) {
			if(edge.isDefToBeYieldedTo())
				gr.addHomToAll(edge);
		}

		return gr;
	}

	/**
	 * Split one hom statement into two parts, so deleted and reuse nodes/edges
	 * can't be matched homomorphically.
	 *
	 * This behavior is required for DPO-semantic.
	 * If the rule is not DPO the (casted) original homomorphic set is returned.
	 * Only homomorphic set with two or more entities will returned.
	 *
	 * @param homChildren Children of a HomNode
	 */
	private Set<Set<ConstraintDeclNode>> splitHoms(Collection<? extends BaseNode> homChildren) {
		Set<Set<ConstraintDeclNode>> ret = new LinkedHashSet<Set<ConstraintDeclNode>>();
		if (isIdentification()) {
    		// homs between deleted entities
    		HashSet<ConstraintDeclNode> deleteHomSet = new HashSet<ConstraintDeclNode>();
    		// homs between reused entities
    		HashSet<ConstraintDeclNode> reuseHomSet = new HashSet<ConstraintDeclNode>();

    		for (BaseNode m : homChildren) {
    			ConstraintDeclNode decl = (ConstraintDeclNode) m;

    			Set<DeclNode> deletedEntities = getRule().getDelete();
    			if (deletedEntities.contains(decl)) {
    				deleteHomSet.add(decl);
    			} else {
    				reuseHomSet.add(decl);
    			}
    		}
    		if (deleteHomSet.size() > 1) {
    			ret.add(deleteHomSet);
    		}
    		if (reuseHomSet.size() > 1) {
    			ret.add(reuseHomSet);
    		}
    		return ret;
		}

		Set<ConstraintDeclNode> homSet = new LinkedHashSet<ConstraintDeclNode>();

		for (BaseNode m : homChildren) {
			ConstraintDeclNode decl = (ConstraintDeclNode) m;

			homSet.add(decl);
		}
		if (homSet.size() > 1) {
			ret.add(homSet);
		}
		return ret;
    }

	private boolean isInduced() {
		return (modifiers & MOD_INDUCED) != 0;
	}

	private boolean isDangling() {
		return (modifiers & MOD_DANGLING) != 0;
	}

	private boolean isIdentification() {
		return (modifiers & MOD_IDENTIFICATION) != 0;
	}

	private boolean isExact() {
		return (modifiers & MOD_EXACT) != 0;
	}

	/**
	 * Get all implicit NACs.
	 *
	 * @return The Collection for the NACs.
	 */
	protected Collection<PatternGraph> getImplicitNegGraphs() {
		Collection<PatternGraph> ret = new LinkedList<PatternGraph>();

		initDoubleNodeNegMap();
		addDoubleNodeNegGraphs(ret);

		initSingleNodeNegMap();
		addSingleNodeNegGraphs(ret);

		return ret;
	}

	private void initDoubleNodeNegMap() {
		Collection<InducedNode> inducedNodes = induced.getChildren();
		if (isInduced()) {
			addToDoubleNodeMap(getNodes());

			for (BaseNode node : inducedNodes) {
				node.reportWarning("Induced statement occurs in induced pattern");
			}
			return;
		}

		Map<Set<NodeDeclNode>, Integer> genInducedSets =
			new LinkedHashMap<Set<NodeDeclNode>, Integer>();

		for (int i = 0; i < induced.getChildren().size(); i++) {
			BaseNode inducedNode = induced.children.get(i);
			Set<NodeDeclNode> nodes = new LinkedHashSet<NodeDeclNode>();

			for (BaseNode inducedChild : inducedNode.getChildren()) {
				// This cast must be ok after checking.
				NodeDeclNode nodeDeclNode = (NodeDeclNode) inducedChild;

				// coords of occurrence are not available
				if (nodes.contains(nodeDeclNode)) {
					inducedNode.reportWarning("Multiple occurrence of "
							+ nodeDeclNode.getUseString() + " "
							+ nodeDeclNode.getIdentNode().getSymbol().getText()
							+ " in a single induced statement");
				} else {
					nodes.add(nodeDeclNode);
				}
			}

			if (genInducedSets.containsKey(nodes)) {
				BaseNode oldOcc = induced.children.get(genInducedSets.get(nodes));
				inducedNode.reportWarning("Same induced statement also occurs at " + oldOcc.getCoords());
			} else {
				addToDoubleNodeMap(nodes);
				genInducedSets.put(nodes, i);
			}
		}

		warnRedundantInducedStatement(genInducedSets);
	}

	/**
	 * warn if an induced statement is redundant.
	 *
	 * Algorithm:
	 * Input: Sets V_i of nodes
	 * for each V_i
	 *   K_i = all pairs of nodes of V_i
	 * for each i
	 *   for each k_i of K_i
	 *     for each K_j
	 *       if k_i \in K_j: mark k_i
	 *   if all k_i marked: warn
	 *
	 * @param genInducedSets Set of all induced statements
	 */
	private void warnRedundantInducedStatement(
		Map<Set<NodeDeclNode>, Integer> genInducedSets) {
		Map<Map<List<NodeDeclNode>, Boolean>, Integer> inducedEdgeMap =
			new LinkedHashMap<Map<List<NodeDeclNode>, Boolean>, Integer>();

		// create all pairs of nodes (->edges)
		for (Map.Entry<Set<NodeDeclNode>, Integer> nodeMapEntry : genInducedSets.entrySet()) {
			// if the Boolean is true -> edge is marked
			Map<List<NodeDeclNode>, Boolean> markedMap = new LinkedHashMap<List<NodeDeclNode>, Boolean>();
			for (NodeDeclNode src : nodeMapEntry.getKey()) {
				for (NodeDeclNode tgt : nodeMapEntry.getKey()) {
					List<NodeDeclNode> edge = new LinkedList<NodeDeclNode>();
					edge.add(src);
					edge.add(tgt);

					markedMap.put(edge, false);
				}
			}

			inducedEdgeMap.put(markedMap, nodeMapEntry.getValue());
		}

		for (Map.Entry<Map<List<NodeDeclNode>, Boolean>, Integer> candidate : inducedEdgeMap.entrySet()) {
			Set<Integer> witnesses = new LinkedHashSet<Integer>();

			for (Map.Entry<List<NodeDeclNode>, Boolean> candidateMarkedMap : candidate.getKey().entrySet()) {
				// TODO also mark witness edge (and candidate as witness)
				if (!candidateMarkedMap.getValue().booleanValue()) {
					for (Map.Entry<Map<List<NodeDeclNode>, Boolean>, Integer> witness : inducedEdgeMap.entrySet()) {
						if (candidate != witness) {
							// if witness contains edge
							if (witness.getKey().containsKey(candidateMarkedMap.getKey())) {
								// mark Edge
								candidateMarkedMap.setValue(true);
								// add witness
								witnesses.add(witness.getValue());
							}
						}
					}
				}
			}

			// all edges marked?
			boolean allMarked = true;
			for (boolean edgeMarked : candidate.getKey().values()) {
				allMarked = allMarked && edgeMarked;
			}
			if (allMarked) {
				String witnessesLoc = "";
				for (Integer index : witnesses) {
					witnessesLoc += induced.children.get(index).getCoords() + " ";
				}
				witnessesLoc = witnessesLoc.trim();
				induced.children.get(candidate.getValue()).reportWarning(
					"Induced statement is redundant, since covered by statement(s) at "
						+ witnessesLoc);
			}
		}
	}

	private void initSingleNodeNegMap() {
		Collection<ExactNode> exactNodes = exact.getChildren();

		if (isExact()) {
			addToSingleNodeMap(getNodes());

			if (isDangling() && !isIdentification()) {
				reportWarning("The keyword \"dangling\" is redundant for exact patterns");
			}

			for (ExactNode node : exactNodes) {
				node.reportWarning("Exact statement occurs in exact pattern");
			}

			return;
		}

		if (isDangling()) {
			Set<DeclNode> deletedNodes = getRule().getDelete();
			addToSingleNodeMap(getDpoPatternNodes(deletedNodes));

			for (BaseNode exactNode : exactNodes) {
				for (BaseNode exactChild : exactNode.getChildren()) {
					// This cast must be ok after checking.
					NodeDeclNode nodeDeclNode = (NodeDeclNode) exactChild;
					if (deletedNodes.contains(nodeDeclNode)) {
						exactNode.reportWarning("Exact statement for "
								+ nodeDeclNode.getUseString()
								+ " "
								+ nodeDeclNode.getIdentNode().getSymbol().getText()
								+ " is redundant, since the pattern is declared \"dangling\" or \"dpo\"");
					}
				}
			}
		}

		Map<NodeDeclNode, Integer> genExactNodes = new LinkedHashMap<NodeDeclNode, Integer>();
		// exact Statements
		for (int i = 0; i < exact.getChildren().size(); i++) {
			BaseNode exactNode = exact.children.get(i);
			for (BaseNode exactChild : exactNode.getChildren()) {
				// This cast must be ok after checking.
				NodeDeclNode nodeDeclNode = (NodeDeclNode) exactChild;
				// coords of occurrence are not available
				if (genExactNodes.containsKey(nodeDeclNode)) {
					exactNode.reportWarning(nodeDeclNode.getUseString()
							+ " "
							+ nodeDeclNode.getIdentNode().getSymbol().getText()
							+ " already occurs in exact statement at "
							+ exact.children.get(genExactNodes.get(nodeDeclNode)).getCoords());
				} else {
					genExactNodes.put(nodeDeclNode, i);
				}
			}
		}

		addToSingleNodeMap(genExactNodes.keySet());
	}

	/**
	 * Return the set of nodes needed for the singleNodeNegMap if the whole
	 * pattern is dpo.
	 */
	private Set<NodeDeclNode> getDpoPatternNodes(Set<DeclNode> deletedEntities) {
		Set<NodeDeclNode> deletedNodes = new LinkedHashSet<NodeDeclNode>();

		for (DeclNode declNode : deletedEntities) {
			if (declNode instanceof NodeDeclNode) {
				NodeDeclNode node = (NodeDeclNode) declNode;
				if (!node.isDummy()) {
					deletedNodes.add(node);
				}
			}
		}

		return deletedNodes;
	}

	private void addSingleNodeNegGraphs(Collection<PatternGraph> ret) {
		assert isResolved();

		// add existing edges to the corresponding sets
		for (BaseNode n : connections.getChildren()) {
			if (n instanceof ConnectionNode) {
				ConnectionNode conn = (ConnectionNode) n;
				NodeDeclNode src = conn.getSrc();
				if (singleNodeNegNodes.contains(src)) {
					Set<NodeDeclNode> homSet = getHomomorphic(src);
					Set<ConnectionNode> edges = singleNodeNegMap.get(homSet);
					edges.add(conn);
					singleNodeNegMap.put(homSet, edges);
				}
				NodeDeclNode tgt = conn.getTgt();
				if (singleNodeNegNodes.contains(tgt)) {
					Set<NodeDeclNode> homSet = getHomomorphic(tgt);
					Set<ConnectionNode> edges = singleNodeNegMap.get(homSet);
					edges.add(conn);
					singleNodeNegMap.put(homSet, edges);
				}
			}
		}

		BaseNode edgeRoot = getArbitraryEdgeRootType();
		BaseNode nodeRoot = getNodeRootType();

		// generate and add pattern graphs
		for (NodeDeclNode singleNodeNegNode : singleNodeNegNodes) {
//			for (int direction = INCOMING; direction <= OUTGOING; direction++) {
				Set<EdgeDeclNode> allNegEdges = new LinkedHashSet<EdgeDeclNode>();
				Set<NodeDeclNode> allNegNodes = new LinkedHashSet<NodeDeclNode>();
				Set<ConnectionNode> edgeSet = singleNodeNegMap.get(getHomomorphic(singleNodeNegNode));
				PatternGraph neg = new PatternGraph("implneg_"+implicitNegCounter, 0);
				++implicitNegCounter;
				neg.setDirectlyNestingLHSGraph(neg);

				// add edges to NAC
				for (ConnectionNode conn : edgeSet) {
					conn.addToGraph(neg);

					allNegEdges.add(conn.getEdge());
					allNegNodes.add(conn.getSrc());
					allNegNodes.add(conn.getTgt());
				}

				addInheritedHomSet(neg, allNegEdges, allNegNodes);

				// add another edge of type edgeRoot to the NAC
				EdgeDeclNode edge = getAnonymousEdgeDecl(edgeRoot, context);
				NodeDeclNode dummyNode = getAnonymousDummyNode(nodeRoot, context);

				ConnectionNode conn = new ConnectionNode(singleNodeNegNode, edge, dummyNode, ConnectionNode.ARBITRARY, this);
				conn.addToGraph(neg);

				ret.add(neg);
//			}
		}
	}

	/**
	 * Add a set of nodes to the singleNodeMap.
	 *
	 * @param nodes Set of Nodes.
	 */
	private void addToSingleNodeMap(Set<NodeDeclNode> nodes) {
		for (NodeDeclNode node : nodes) {
			if (node.isDummy()) continue;

			singleNodeNegNodes.add(node);
			Set<NodeDeclNode> homSet = getHomomorphic(node);
			if (!singleNodeNegMap.containsKey(homSet)) {
				Set<ConnectionNode> edgeSet = new HashSet<ConnectionNode>();
				singleNodeNegMap.put(homSet, edgeSet);
			}
		}
	}

	/** Return the correspondent homomorphic set. */
	protected Set<NodeDeclNode> getHomomorphic(NodeDeclNode node)
    {
		if (!nodeHomMap.containsKey(node)) {
	    	initHomMaps();
	    }

		Set<NodeDeclNode> homSet = nodeHomMap.get(node);

		if (homSet == null) {
			// If the node isn't part of the pattern, return empty set.
			homSet = new LinkedHashSet<NodeDeclNode>();
		}

		return homSet;
    }

	/** Return the correspondent homomorphic set. */
	protected Set<EdgeDeclNode> getHomomorphic(EdgeDeclNode edge)
    {
		if (!edgeHomMap.containsKey(edge)) {
			initHomMaps();
		}

		Set<EdgeDeclNode> homSet = edgeHomMap.get(edge);

		if (homSet == null) {
			// If the edge isn't part of the pattern, return empty set.
			homSet = new LinkedHashSet<EdgeDeclNode>();
		}

		return homSet;
    }

	private NodeDeclNode getAnonymousDummyNode(BaseNode nodeRoot, int context) {
		IdentNode nodeName = new IdentNode(getScope().defineAnonymous(
				"dummy_node", SymbolTable.getInvalid(), Coords.getBuiltin()));
		NodeDeclNode dummyNode = NodeDeclNode.getDummy(nodeName, nodeRoot, context, this);
		return dummyNode;
	}

	private EdgeDeclNode getAnonymousEdgeDecl(BaseNode edgeRoot, int context) {
		IdentNode edgeName = new IdentNode(getScope().defineAnonymous(
				"edge", SymbolTable.getInvalid(), Coords.getBuiltin()));
		EdgeDeclNode edge = new EdgeDeclNode(edgeName, edgeRoot, context, this, this);
		return edge;
	}

	/**
	 * @param negs
	 */
	private void addDoubleNodeNegGraphs(Collection<PatternGraph> ret) {
		assert isResolved();

		// add existing edges to the corresponding pattern graph
		for (BaseNode n : connections.getChildren()) {
			if (n instanceof ConnectionNode) {
				ConnectionNode conn = (ConnectionNode) n;

				List<Set<NodeDeclNode>> key = new LinkedList<Set<NodeDeclNode>>();
				key.add(getHomomorphic(conn.getSrc()));
				key.add(getHomomorphic(conn.getTgt()));

				Set<ConnectionNode> edges = doubleNodeNegMap.get(key);
				// edges == null if conn is a dangling edge or one of the nodes
				// is not induced
				if (edges != null) {
					edges.add(conn);
					doubleNodeNegMap.put(key, edges);
				}
			}
		}

		BaseNode edgeRoot = getArbitraryEdgeRootType();

		for (List<NodeDeclNode> pair : doubleNodeNegPairs) {
			NodeDeclNode src = pair.get(0);
			NodeDeclNode tgt = pair.get(1);

			if (src.getId().compareTo(tgt.getId()) > 0) {
				continue;
			}

			List<Set<NodeDeclNode>> key = new LinkedList<Set<NodeDeclNode>>();
			key.add(getHomomorphic(src));
			key.add(getHomomorphic(tgt));
			List<Set<NodeDeclNode>> key2 = new LinkedList<Set<NodeDeclNode>>();
			key2.add(getHomomorphic(tgt));
			key2.add(getHomomorphic(src));
			Set<EdgeDeclNode> allNegEdges = new LinkedHashSet<EdgeDeclNode>();
			Set<NodeDeclNode> allNegNodes = new LinkedHashSet<NodeDeclNode>();
			Set<ConnectionNode> edgeSet = doubleNodeNegMap.get(key);
			edgeSet.addAll(doubleNodeNegMap.get(key2));

			PatternGraph neg = new PatternGraph("implneg_"+implicitNegCounter, 0);
			++implicitNegCounter;
			neg.setDirectlyNestingLHSGraph(neg);
			
			// add edges to the NAC
			for (ConnectionNode conn : edgeSet) {
				conn.addToGraph(neg);

				allNegEdges.add(conn.getEdge());
				allNegNodes.add(conn.getSrc());
				allNegNodes.add(conn.getTgt());
			}

			addInheritedHomSet(neg, allNegEdges, allNegNodes);

			// add another edge of type edgeRoot to the NAC
			EdgeDeclNode edge = getAnonymousEdgeDecl(edgeRoot, context);

			ConnectionCharacter conn = new ConnectionNode(src, edge, tgt, ConnectionNode.ARBITRARY, this);

			conn.addToGraph(neg);

			ret.add(neg);
		}
	}

	/**
     * Add all necessary homomorphic sets to a NAC.
     *
     * If an edge a-e->b is homomorphic to another edge c-f->d f only added if
     * a is homomorphic to c and b is homomorphic to d.
     */
    private void addInheritedHomSet(PatternGraph neg,
            Set<EdgeDeclNode> allNegEdges, Set<NodeDeclNode> allNegNodes)
    {
	    // inherit homomorphic nodes
	    for (NodeDeclNode node : allNegNodes) {
	    	Set<Node> homSet = new LinkedHashSet<Node>();
	    	Set<NodeDeclNode> homNodes = getHomomorphic(node);

	    	for (NodeDeclNode homNode : homNodes) {
	            if (allNegNodes.contains(homNode)) {
	    			homSet.add(homNode.checkIR(Node.class));
	            }
	        }
	    	if (homSet.size() > 1) {
	    		neg.addHomomorphicNodes(homSet);
	    	}
	    }

    	// inherit homomorphic edges
	    for (EdgeDeclNode edge : allNegEdges) {
	    	Set<Edge> homSet = new LinkedHashSet<Edge>();
	    	Set<EdgeDeclNode> homEdges = getHomomorphic(edge);

	    	for (EdgeDeclNode homEdge : homEdges) {
	            if (allNegEdges.contains(homEdge)) {
	    			homSet.add(homEdge.checkIR(Edge.class));
	            }
	        }
	    	if (homSet.size() > 1) {
	    		neg.addHomomorphicEdges(homSet);
	    	}
	    }
    }

	private void addToDoubleNodeMap(Set<NodeDeclNode> nodes) {
		for (NodeDeclNode src : nodes) {
			if (src.isDummy()) continue;

			for (NodeDeclNode tgt : nodes) {
				if (tgt.isDummy()) continue;

				List<NodeDeclNode> pair = new LinkedList<NodeDeclNode>();
				pair.add(src);
				pair.add(tgt);
				doubleNodeNegPairs.add(pair);

				List<Set<NodeDeclNode>> key = new LinkedList<Set<NodeDeclNode>>();
				key.add(getHomomorphic(src));
				key.add(getHomomorphic(tgt));

				if (!doubleNodeNegMap.containsKey(key)) {
					Set<ConnectionNode> edges = new LinkedHashSet<ConnectionNode>();
					doubleNodeNegMap.put(key, edges);
				}
			}
		}
	}
}
