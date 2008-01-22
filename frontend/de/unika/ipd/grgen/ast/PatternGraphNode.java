/*
 GrGen: graph rewrite generator tool.
 Copyright (C) 2005  IPD Goos, Universit"at Karlsruhe, Germany

 This library is free software; you can redistribute it and/or
 modify it under the terms of the GNU Lesser General Public
 License as published by the Free Software Foundation; either
 version 2.1 of the License, or (at your option) any later version.

 This library is distributed in the hope that it will be useful,
 but WITHOUT ANY WARRANTY; without even the implied warranty of
 MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the GNU
 Lesser General Public License for more details.

 You should have received a copy of the GNU Lesser General Public
 License along with this library; if not, write to the Free Software
 Foundation, Inc., 51 Franklin St, Fifth Floor, Boston, MA  02110-1301  USA
 */

/**
 * PatternGraphNode.java
 *
 * @author Sebastian Hack
 */

package de.unika.ipd.grgen.ast;

import java.util.Collection;
import java.util.HashSet;
import java.util.Iterator;
import java.util.LinkedHashMap;
import java.util.LinkedHashSet;
import java.util.LinkedList;
import java.util.List;
import java.util.Map;
import java.util.Set;
import java.util.Vector;

import de.unika.ipd.grgen.ast.util.Checker;
import de.unika.ipd.grgen.ast.util.CollectChecker;
import de.unika.ipd.grgen.ast.util.SimpleChecker;
import de.unika.ipd.grgen.ir.Expression;
import de.unika.ipd.grgen.ir.GraphEntity;
import de.unika.ipd.grgen.ir.IR;
import de.unika.ipd.grgen.ir.Operator;
import de.unika.ipd.grgen.ir.PatternGraph;
import de.unika.ipd.grgen.ir.SubpatternUsage;
import de.unika.ipd.grgen.ir.Typeof;
import de.unika.ipd.grgen.parser.Coords;
import de.unika.ipd.grgen.parser.SymbolTable;


/**
 * AST node that represents a graph pattern as it appears within the pattern
 * part of some rule Extension of the graph pattern of the rewrite part
 */
public class PatternGraphNode extends GraphNode {
	static {
		setName(PatternGraphNode.class, "pattern_graph");
	}

	public static final int MOD_DPO = 1;
	public static final int MOD_EXACT = 2;
	public static final int MOD_INDUCED = 4;

	/** The modifiers for this type. An ORed combination of the constants above. */
	private int modifiers = 0;

	/** used to add a dangling edge to a NAC. */
	private static final int INCOMING = 0;
	private static final int OUTGOING = 1;

	CollectNode conditions;
	CollectNode homs;
	CollectNode exact;
	CollectNode induced;

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

	/** All nodes which needed a single node NAC. */
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

	public PatternGraphNode(Coords coords, CollectNode connections,
			CollectNode subpatterns, CollectNode conditions,
			CollectNode returns, CollectNode homs, CollectNode exact,
			CollectNode induced, int modifiers, int context) {
		super(coords, connections, subpatterns, returns, null, context);
		this.conditions = conditions;
		becomeParent(this.conditions);
		this.homs = homs;
		becomeParent(this.homs);
		this.exact = exact;
		becomeParent(this.exact);
		this.induced = induced;
		becomeParent(this.induced);
		this.modifiers = modifiers;
	}

	/** returns children of this node */
	public Collection<BaseNode> getChildren() {
		Vector<BaseNode> children = new Vector<BaseNode>();
		children.add(connections);
		children.add(returns);
		children.add(conditions);
		children.add(homs);
		children.add(exact);
		children.add(induced);
		return children;
	}

	/** returns names of the children, same order as in getChildren */
	public Collection<String> getChildrenNames() {
		Vector<String> childrenNames = new Vector<String>();
		childrenNames.add("connections");
		childrenNames.add("return");
		childrenNames.add("conditions");
		childrenNames.add("homs");
		childrenNames.add("exact");
		childrenNames.add("induced");
		return childrenNames;
	}

	/** @see de.unika.ipd.grgen.ast.BaseNode#resolve() */
	protected boolean resolve() {
		if (isResolved()) {
			return resolutionResult();
		}

		debug.report(NOTE, "resolve in: " + getId() + "(" + getClass() + ")");
		boolean successfullyResolved = true;
		nodeResolvedSetResult(successfullyResolved); // local result

		successfullyResolved = connections.resolve() && successfullyResolved;
		successfullyResolved = returns.resolve() && successfullyResolved;
		successfullyResolved = conditions.resolve() && successfullyResolved;
		successfullyResolved = homs.resolve() && successfullyResolved;
		successfullyResolved = exact.resolve() && successfullyResolved;
		successfullyResolved = induced.resolve() && successfullyResolved;
		return successfullyResolved;
	}

	public Collection<BaseNode> getHoms() {
		return homs.getChildren();
	}

	protected boolean checkLocal() {
		Checker conditionsChecker =  new CollectChecker(new SimpleChecker(ExprNode.class));
		Checker homChecker = new CollectChecker(new SimpleChecker(HomNode.class));
		Checker exactChecker = new CollectChecker(new SimpleChecker(ExactNode.class));
		Checker inducedChecker = new CollectChecker(new SimpleChecker(InducedNode.class));

		boolean childs = super.checkLocal()
			& conditionsChecker.check(conditions, error)
			& homChecker.check(homs, error)
			& exactChecker.check(exact, error)
			& inducedChecker.check(induced, error);

		boolean expr = true;
		boolean homcheck = true;
		if (childs) {
			for (BaseNode n : conditions.getChildren()) {
				// Must go right, since it is checked 5 lines above.
				ExprNode exp = (ExprNode) n;
				if (!exp.getType().isEqual(BasicTypeNode.booleanType)) {
					exp.reportError("Expression must be of type boolean");
					expr = false;
				}
			}

			HashSet<DeclNode> homEnts = new HashSet<DeclNode>();
			for (BaseNode n : getHoms()) {
				HomNode hom = (HomNode) n;

				for (BaseNode m : hom.getChildren()) {
					DeclNode decl = (DeclNode) m;

					if (homEnts.contains(decl)) {
						hom.reportError(m.toString()
											+ " is contained in multiple hom statements");
						homcheck = false;
					}
				}
				for (BaseNode m : hom.getChildren()) {
					DeclNode decl = (DeclNode) m;

					homEnts.add(decl);
				}
			}

		}

		return childs && expr && homcheck;
	}

	/**
	 * Get the correctly casted IR object.
	 *
	 * @return The IR object.
	 */
	public PatternGraph getPatternGraph() {
		return (PatternGraph) checkIR(PatternGraph.class);
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
			Expression e1 = new Typeof(elem);
			Expression e2 = new Typeof(elem.getTypeof());

			Operator op = new Operator(BasicTypeNode.booleanType.getPrimitiveType(), Operator.GE);
			op.addOperand(e1);
			op.addOperand(e2);

			gr.addCondition(op);
		}
	}

	protected IR constructIR() {
		PatternGraph gr = new PatternGraph();

		for (BaseNode n : connections.getChildren()) {
			ConnectionCharacter conn = (ConnectionCharacter) n;
			conn.addToGraph(gr);
		}

		for(BaseNode n : subpatterns.getChildren()) {
			gr.addSubpatternUsage((SubpatternUsage)n.getIR());
		}
		
		for (BaseNode n : conditions.getChildren()) {
			ExprNode expr = (ExprNode) n;
			expr = expr.evaluate();
			gr.addCondition((Expression) expr.checkIR(Expression.class));
		}

		/* generate type conditions from dynamic type checks via typeof */
		for (GraphEntity n : gr.getNodes()) {
			genTypeCondsFromTypeof(gr, n);
		}
		for (GraphEntity e : gr.getEdges()) {
			genTypeCondsFromTypeof(gr, e);
		}

		for (BaseNode n : getHoms()) {
			HomNode hom = (HomNode) n;
			Set<Set<DeclNode>> homSets = splitHoms(hom.getChildren());
			for (Set<DeclNode> homSet : homSets) {
	            HashSet<GraphEntity> homSetIR = new HashSet<GraphEntity>();
	    		for (DeclNode decl : homSet) {
	    			homSetIR.add((GraphEntity) decl.checkIR(GraphEntity.class));
	    		}
	            gr.addHomomorphic(homSetIR);
            }
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
	private Set<Set<DeclNode>> splitHoms(Collection<? extends BaseNode> homChildren) {
		Set<Set<DeclNode>> ret = new LinkedHashSet<Set<DeclNode>>();
		if (isDPO()) {
    		// homs between deleted entities
    		HashSet<DeclNode> deleteHomSet = new HashSet<DeclNode>();
    		// homs between reused entities
    		HashSet<DeclNode> reuseHomSet = new HashSet<DeclNode>();

    		for (BaseNode m : homChildren) {
    			DeclNode decl = (DeclNode) m;

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

		HashSet<DeclNode> homSet = new HashSet<DeclNode>();

		for (BaseNode m : homChildren) {
			DeclNode decl = (DeclNode) m;

			homSet.add(decl);
		}
		if (homSet.size() > 1) {
			ret.add(homSet);
		}
		return ret;
    }

	public final boolean isInduced() {
		return (modifiers & MOD_INDUCED) != 0;
	}

	public final boolean isDPO() {
		return (modifiers & MOD_DPO) != 0;
	}

	public final boolean isExact() {
		return (modifiers & MOD_EXACT) != 0;
	}

	/**
	 * Get all implicit NACs.
	 *
	 * @return The Collection for the NACs.
	 */
	public Collection<PatternGraph> getImplicitNegGraphs() {
		Collection<PatternGraph> ret = new LinkedList<PatternGraph>();

		initDoubleNodeNegMap();
		addDoubleNodeNegGraphs(ret);

		initSingleNodeNegMap();
		addSingleNodeNegGraphs(ret);

		return ret;
	}

	private void initDoubleNodeNegMap() {
		Collection<BaseNode> inducedNodes = induced.getChildren();
		if (isInduced()) {
			addToDoubleNodeMap(getAllPatternNodes());

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
				if (!candidateMarkedMap.getValue()) {
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

			// all Edges marked?
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
		Collection<BaseNode> exactNodes = exact.getChildren();

		if (isExact()) {
			addToSingleNodeMap(getAllPatternNodes());

			if (isDPO()) {
				reportWarning("The keyword \"dpo\" is redundant for exact patterns");
			}

			for (BaseNode node : exactNodes) {
				node.reportWarning("Exact statement occurs in exact pattern");
			}

			return;
		}

		if (isDPO()) {
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
								+ " is redundant, since the pattern is DPO");
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

	/** Return all nodes of the pattern. */
	private Set<NodeDeclNode> getAllPatternNodes() {

		Set<NodeDeclNode> nodes = new LinkedHashSet<NodeDeclNode>();
		for (BaseNode n : connections.getChildren()) {
			ConnectionCharacter conn = (ConnectionCharacter) n;

			// This cast must be ok after checking.
			NodeDeclNode cand = (NodeDeclNode) conn.getSrc();
			if (!cand.isDummy()) {
				nodes.add(cand);
			}
			// This cast must be ok after checking.
			cand = (NodeDeclNode) conn.getTgt();
			if (cand != null && !cand.isDummy()) {
				nodes.add(cand);
			}
		}

		return nodes;
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
		// add existing edges to the corresponding sets
		for (BaseNode n : connections.getChildren()) {
			if (n instanceof ConnectionNode) {
				ConnectionNode conn = (ConnectionNode) n;
				NodeDeclNode src = conn.getSrc();
				if (singleNodeNegNodes.contains(src)) {
					Set<NodeDeclNode> homSet = getCorrespondentHomSet(src);
					Set<ConnectionNode> edges = singleNodeNegMap.get(homSet);
					edges.add(conn);
					singleNodeNegMap.put(homSet, edges);
				}
				NodeDeclNode tgt = conn.getTgt();
				if (singleNodeNegNodes.contains(tgt)) {
					Set<NodeDeclNode> homSet = getCorrespondentHomSet(tgt);
					Set<ConnectionNode> edges = singleNodeNegMap.get(homSet);
					edges.add(conn);
					singleNodeNegMap.put(homSet, edges);
				}
			}
		}

		BaseNode edgeRoot = getEdgeRootType();
		BaseNode nodeRoot = getNodeRootType();

		// generate and add pattern graphs
		for (NodeDeclNode singleNodeNegNode : singleNodeNegNodes) {
			for (int direction = INCOMING; direction <= OUTGOING; direction++) {
				Set<EdgeDeclNode> allNegEdges = new LinkedHashSet<EdgeDeclNode>();
				Set<NodeDeclNode> allNegNodes = new LinkedHashSet<NodeDeclNode>();
				Set<ConnectionNode> edgeSet = singleNodeNegMap.get(getCorrespondentHomSet(singleNodeNegNode));
				PatternGraph neg = new PatternGraph();

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

				ConnectionCharacter conn = null;
				if (direction == INCOMING) {
					conn = new ConnectionNode(dummyNode, edge, singleNodeNegNode, true);
				} else {
					conn = new ConnectionNode(singleNodeNegNode, edge, dummyNode, true);
				}
				conn.addToGraph(neg);

				ret.add(neg);
			}
		}
	}

	/**
	 * Add a set of nodes to the singleNodeMap.
	 *
	 * @param nodes Set of Nodes.
	 */
	private void addToSingleNodeMap(Set<NodeDeclNode> nodes) {
		for (NodeDeclNode node : nodes) {
			singleNodeNegNodes.add(node);
			Set<NodeDeclNode> homSet = getCorrespondentHomSet(node);
			if (!singleNodeNegMap.containsKey(homSet)) {
				Set<ConnectionNode> edgeSet = new HashSet<ConnectionNode>();
				singleNodeNegMap.put(homSet, edgeSet);
			}
		}
	}

	/** Return the correspondent homomorphic set. */
	private Set<NodeDeclNode> getCorrespondentHomSet(NodeDeclNode node)
    {
	    if (nodeHomMap.containsKey(node)) {
	    	return nodeHomMap.get(node);
	    }
		Set<NodeDeclNode> ret = new LinkedHashSet<NodeDeclNode>();
		for (BaseNode homNode : homs.getChildren()) {
	        if (homNode.getChildren().contains(node)) {
	        	Set<Set<DeclNode>> homSets = splitHoms(homNode.getChildren());
	        	for (Set<DeclNode> homSet : homSets) {
		        	if (homSet.contains(node)) {
    	        		for (DeclNode n : homSet) {
    	        			// This cast must be ok after checking.
    	        			ret.add((NodeDeclNode) n);
    	                }
	        		}
                }
	        	nodeHomMap.put(node, ret);
	        	return ret;
	        }
        }
		// homomorphic set contains only the node
		ret.add(node);
		nodeHomMap.put(node, ret);
		return ret;
    }

	/** Return the correspondent homomorphic set. */
	private Set<EdgeDeclNode> getCorrespondentHomSet(EdgeDeclNode edge)
    {
		if (edgeHomMap.containsKey(edge)) {
			return edgeHomMap.get(edge);
		}
	    Set<EdgeDeclNode> ret = new LinkedHashSet<EdgeDeclNode>();
		for (BaseNode homNode : homs.getChildren()) {
	        if (homNode.getChildren().contains(edge)) {
	        	Set<Set<DeclNode>> homSets = splitHoms(homNode.getChildren());
	        	for (Set<DeclNode> homSet : homSets) {
		        	if (homSet.contains(edge)) {
    	        		for (DeclNode n : homSet) {
    	        			// This cast must be ok after checking.
    	        			ret.add((EdgeDeclNode) n);
    	                }
		        	}
                }
	        	edgeHomMap.put(edge, ret);
	        	return ret;
	        }
        }
		// homomorphic set contains only the edge
		ret.add(edge);
		edgeHomMap.put(edge, ret);
		return ret;
    }

	private NodeDeclNode getAnonymousDummyNode(BaseNode nodeRoot, int context) {
		IdentNode nodeName = new IdentNode(getScope().defineAnonymous(
				"dummy_node", SymbolTable.getInvalid(), Coords.getBuiltin()));
		NodeDeclNode dummyNode = NodeDeclNode.getDummy(nodeName, nodeRoot, context);
		return dummyNode;
	}

	private EdgeDeclNode getAnonymousEdgeDecl(BaseNode edgeRoot, int context) {
		IdentNode edgeName = new IdentNode(getScope().defineAnonymous(
				"edge", SymbolTable.getInvalid(), Coords.getBuiltin()));
		EdgeDeclNode edge = new EdgeDeclNode(edgeName, edgeRoot, context, true);
		return edge;
	}

	private BaseNode getNodeRootType() {
		// get root node
		BaseNode root = this;
		while (!root.isRoot()) {
			root = root.getParents().iterator().next();
		}

		// find an edgeRoot-type and nodeRoot
		BaseNode nodeRoot = null;
		BaseNode model = ((UnitNode) root).models.children.firstElement();
		Collection<BaseNode> types = ((ModelNode) model).decls.children;

		for (Iterator<BaseNode> it = types.iterator(); it.hasNext();) {
			BaseNode candidate = it.next();
			IdentNode ident = (IdentNode) ((DeclNode) candidate).ident;
			String name = ident.getSymbol().getText();
			if (name.equals("Node")) {
				nodeRoot = candidate;
			}
		}
		return nodeRoot;
	}

	/**
	 * @param negs
	 */
	private void addDoubleNodeNegGraphs(Collection<PatternGraph> ret) {
		// add existing edges to the corresponding pattern graph
		for (BaseNode n : connections.getChildren()) {
			if (n instanceof ConnectionNode) {
				ConnectionNode conn = (ConnectionNode) n;

				List<Set<NodeDeclNode>> key = new LinkedList<Set<NodeDeclNode>>();
				key.add(getCorrespondentHomSet(conn.getSrc()));
				key.add(getCorrespondentHomSet(conn.getTgt()));

				Set<ConnectionNode> edges = doubleNodeNegMap.get(key);
				// edges == null if conn is a dangling edge or one of the nodes
				// is not induced
				if (edges != null) {
					edges.add(conn);
					doubleNodeNegMap.put(key, edges);
				}
			}
		}

		BaseNode edgeRoot = getEdgeRootType();

		for (List<NodeDeclNode> pair : doubleNodeNegPairs) {
			NodeDeclNode src = pair.get(0);
			NodeDeclNode tgt = pair.get(1);

			List<Set<NodeDeclNode>> key = new LinkedList<Set<NodeDeclNode>>();
			key.add(getCorrespondentHomSet(src));
			key.add(getCorrespondentHomSet(tgt));
			Set<EdgeDeclNode> allNegEdges = new LinkedHashSet<EdgeDeclNode>();
			Set<NodeDeclNode> allNegNodes = new LinkedHashSet<NodeDeclNode>();
			Set<ConnectionNode> edgeSet = doubleNodeNegMap.get(key);

			PatternGraph neg = new PatternGraph();

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

			ConnectionCharacter conn = new ConnectionNode(src, edge, tgt, true);

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
	    	Set<GraphEntity> homSet = new LinkedHashSet<GraphEntity>();
	    	Set<NodeDeclNode> homNodes = getCorrespondentHomSet(node);

	    	for (NodeDeclNode homNode : homNodes) {
	            if (allNegNodes.contains(homNode)) {
	    			homSet.add((GraphEntity) homNode.checkIR(GraphEntity.class));
	            }
	        }
	    	if (homSet.size() > 1) {
	    		neg.addHomomorphic(homSet);
	    	}
	    }

    	// inherit homomorphic edges
	    for (EdgeDeclNode edge : allNegEdges) {
	    	Set<GraphEntity> homSet = new LinkedHashSet<GraphEntity>();
	    	Set<EdgeDeclNode> homEdges = getCorrespondentHomSet(edge);

	    	for (EdgeDeclNode homEdge : homEdges) {
	            if (allNegEdges.contains(homEdge)) {
	    			homSet.add((GraphEntity) homEdge.checkIR(GraphEntity.class));
	            }
	        }
	    	if (homSet.size() > 1) {
	    		neg.addHomomorphic(homSet);
	    	}
	    }
    }

	private void addToDoubleNodeMap(Set<NodeDeclNode> nodes) {
		for (NodeDeclNode src : nodes) {
			for (NodeDeclNode tgt : nodes) {
				List<NodeDeclNode> pair = new LinkedList<NodeDeclNode>();
				pair.add(src);
				pair.add(tgt);
				doubleNodeNegPairs.add(pair);

				List<Set<NodeDeclNode>> key = new LinkedList<Set<NodeDeclNode>>();
				key.add(getCorrespondentHomSet(src));
				key.add(getCorrespondentHomSet(tgt));

				if (!doubleNodeNegMap.containsKey(key)) {
					Set<ConnectionNode> edges = new LinkedHashSet<ConnectionNode>();
					doubleNodeNegMap.put(key, edges);
				}
			}
		}
	}

	// TODO Change return-type iff CollectNode support generics
	private BaseNode getEdgeRootType() {
		// get root node
		BaseNode root = this;
		while (!root.isRoot()) {
			root = root.getParents().iterator().next();
		}

		// find an edgeRoot-type
		BaseNode edgeRoot = null;
		BaseNode model = ((UnitNode) root).models.children.firstElement();
		Collection<BaseNode> types = ((ModelNode) model).decls.children;

		for (Iterator<BaseNode> it = types.iterator(); it.hasNext();) {
			BaseNode candidate = it.next();
			IdentNode ident = (IdentNode) ((DeclNode) candidate).ident;
			String name = ident.getSymbol().getText();
			if (name.equals("Edge")) {
				edgeRoot = candidate;
			}
		}
		return edgeRoot;
	}
}
