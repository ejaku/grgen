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

import de.unika.ipd.grgen.ir.*;

import java.util.*;

import de.unika.ipd.grgen.ast.BaseNode;
import de.unika.ipd.grgen.ast.ConnectionCharacter;
import de.unika.ipd.grgen.ast.ExprNode;
import de.unika.ipd.grgen.ast.GraphNode;
import de.unika.ipd.grgen.ast.util.Checker;
import de.unika.ipd.grgen.ast.util.CollectChecker;
import de.unika.ipd.grgen.ast.util.SimpleChecker;
import de.unika.ipd.grgen.parser.Coords;
import de.unika.ipd.grgen.parser.SymbolTable;
import java.util.Map.Entry;

/**
 * AST node that represents a graph pattern as it appears within the pattern
 * part of some rule Extension of the graph pattern of the rewrite part
 */
public class PatternGraphNode extends GraphNode
{
	public static final int MOD_DPO = 1;
	public static final int MOD_EXACT = 2;
	public static final int MOD_INDUCED = 4;

	/** The modifiers for this type. An ORed combination of the constants above. */
	private int modifiers = 0;

	/** used to add an dangling edge to a PatternGraph. */
	private static final int INCOMING = 0;
	private static final int OUTGOING = 1;

	/** Index of the conditions collect node. */
	private static final int CONDITIONS = RETURN + 1;

	/** Index of the hom statements collect node. */
	private static final int HOMS = CONDITIONS + 1;

	/** Index of the induced statements collect node. */
	private static final int DPO = CONDITIONS + 2;

	/** Index of the exact statements collect node. */
	private static final int EXACT = CONDITIONS + 3;

	/** Index of the induced statements collect node. */
	private static final int INDUCED = CONDITIONS + 4;

	/** Conditions checker. */
	private static final Checker conditionsChecker = new CollectChecker(
			new SimpleChecker(ExprNode.class));

	/** Homomorphic checker. */
	private static final Checker homChecker = new CollectChecker(
			new SimpleChecker(HomNode.class));

	/** DPO checker. */
	private static final Checker dpoChecker = new CollectChecker(
			new SimpleChecker(DpoNode.class));

	/** Exact checker. */
	private static final Checker exactChecker = new CollectChecker(
			new SimpleChecker(ExactNode.class));

	/** Induced checker. */
	private static final Checker inducedChecker = new CollectChecker(
			new SimpleChecker(InducedNode.class));

	/**
	 * TODO
	 *  Map to a set of edges -> don't count edges twice
	 */
	private Map<NodeCharacter, Set<ConnectionNode>> singleNodeNegMap =
		new LinkedHashMap<NodeCharacter, Set<ConnectionNode>>();

	/**
	 * TODO
	 * map each pair of nodes to a pattern graph
	 */
	private Map<List<NodeCharacter>, PatternGraph> doubleNodeNegMap =
		new LinkedHashMap<List<NodeCharacter>, PatternGraph>();

	static {
		setName(PatternGraphNode.class, "pattern_graph");
	}

	/**
	 * A new pattern node
	 * @param connections A collection containing connection nodes
	 * @param conditions A collection of conditions.
	 */
	public PatternGraphNode(Coords coords, CollectNode connections,
			CollectNode conditions, CollectNode returns, CollectNode homs,
			CollectNode dpo, CollectNode exact, CollectNode induced,
			int modifiers) {
		super(coords, connections, returns);
		addChild(conditions);
		addChild(homs);
		addChild(dpo);
		addChild(exact);
		addChild(induced);
		this.modifiers = modifiers;
	}

	/** @see de.unika.ipd.grgen.ast.BaseNode#doResolve() */
	protected boolean doResolve() {
		if (isResolved()) {
			return getResolve();
		}

		debug.report(NOTE, "resolve in: " + getId() + "(" + getClass() + ")");
		boolean successfullyResolved = true;
		setResolved(successfullyResolved); // local result

		successfullyResolved = getChild(CONNECTIONS).doResolve() && successfullyResolved;
		successfullyResolved = getChild(RETURN).doResolve() && successfullyResolved;
		successfullyResolved = getChild(CONDITIONS).doResolve() && successfullyResolved;
		successfullyResolved = getChild(HOMS).doResolve() && successfullyResolved;
		successfullyResolved = getChild(DPO).doResolve() && successfullyResolved;
		successfullyResolved = getChild(EXACT).doResolve() && successfullyResolved;
		successfullyResolved = getChild(INDUCED).doResolve() && successfullyResolved;
		return successfullyResolved;
	}

	/** @see de.unika.ipd.grgen.ast.BaseNode#doCheck() */
	protected boolean doCheck() {
		if (!getResolve()) {
			return false;
		}
		if (isChecked()) {
			return getChecked();
		}

		boolean successfullyChecked = getCheck();
		if (successfullyChecked) {
			successfullyChecked = getTypeCheck();
		}
		successfullyChecked = getChild(CONNECTIONS).doCheck() && successfullyChecked;
		successfullyChecked = getChild(RETURN).doCheck() && successfullyChecked;
		successfullyChecked = getChild(CONDITIONS).doCheck() && successfullyChecked;
		successfullyChecked = getChild(HOMS).doCheck() && successfullyChecked;
		successfullyChecked = getChild(DPO).doCheck() && successfullyChecked;
		successfullyChecked = getChild(EXACT).doCheck() && successfullyChecked;
		successfullyChecked = getChild(INDUCED).doCheck() && successfullyChecked;
	

		return successfullyChecked;
	}

	public Collection<BaseNode> getHoms() {
		return getChild(HOMS).getChildren();
	}

	protected boolean check() {
		boolean childs = super.check()
				&& checkChild(CONDITIONS, conditionsChecker)
				&& checkChild(HOMS, homChecker) 
				&& checkChild(DPO, dpoChecker)
				&& checkChild(EXACT, exactChecker)
				&& checkChild(INDUCED, inducedChecker);

		boolean expr = true;
		boolean homcheck = true;
		if (childs) {
			for (BaseNode n : getChild(CONDITIONS).getChildren()) {
				// Must go right, since it is checked 5 lines above.
				ExprNode exp = (ExprNode) n;
				if (!exp.getType().isEqual(BasicTypeNode.booleanType)) {
					exp.reportError("Expression must be of type boolean");
					expr = false;
				}
			}

			HashSet<DeclNode> hom_ents = new HashSet<DeclNode>();
			for (BaseNode n : getHoms()) {
				HomNode hom = (HomNode) n;

				for (BaseNode m : n.getChildren()) {
					DeclNode decl = (DeclNode) m;

					if (hom_ents.contains(decl)) {
						hom.reportError(m.toString()
								+ " is contained in multiple hom statements");
						homcheck = false;
					}
				}
				for (BaseNode m : n.getChildren()) {
					DeclNode decl = (DeclNode) m;

					hom_ents.add(decl);
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

	/**
	 * Generates a type condition if the given graph entity inherits its type
	 * from another element via a typeof expression.
	 */
	private void genTypeCondsFromTypeof(PatternGraph gr, GraphEntity elem) {
		if (elem.inheritsType()) {
			Expression e1 = new Typeof(elem);
			Expression e2 = new Typeof(elem.getTypeof());

			Operator op = new Operator(BasicTypeNode.booleanType
					.getPrimitiveType(), Operator.GE);
			op.addOperand(e1);
			op.addOperand(e2);

			gr.addCondition(op);
		}
	}

	protected IR constructIR() {
		PatternGraph gr = new PatternGraph();

		for (BaseNode n : getChild(CONNECTIONS).getChildren()) {
			ConnectionCharacter conn = (ConnectionCharacter) n;
			conn.addToGraph(gr);
		}

		for (BaseNode n : getChild(CONDITIONS).getChildren()) {
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
			HashSet<GraphEntity> hom_set = new HashSet<GraphEntity>();

			for (BaseNode m : hom.getChildren()) {
				DeclNode decl = (DeclNode) m;
				hom_set.add((GraphEntity) decl.checkIR(GraphEntity.class));
			}

			gr.addHomomorphic(hom_set);
		}

		return gr;
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
	public Collection<PatternGraph> getImplicitNegGraphs(RuleDeclNode ruleNode) {
		Collection<PatternGraph> ret = new LinkedList<PatternGraph>();

		initDoubleNodeNegMap();
		addDoubleNodeNegGraphs(ret);

		initSingleNodeNegMap(ruleNode);
		addSingleNodeNegGraphs(ret);

		return ret;
	}

	private void initDoubleNodeNegMap() {
		Collection<BaseNode> inducedNodes = getChild(INDUCED).getChildren();
		if (isInduced()) {
			addToDoubleNodeMap(getInducedPatternNodes());

			for (BaseNode node : inducedNodes) {
				node.reportWarning("Induced statement occurs in induced pattern");
			}
			return;
		}

		for (BaseNode node : inducedNodes) {
			InducedNode inducedNode = (InducedNode) node;

			Set<NodeCharacter> nodes = new LinkedHashSet<NodeCharacter>();

			for (BaseNode inducedChild : inducedNode.getChildren()) {
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

			addToDoubleNodeMap(nodes);
		}

	}

	private void initSingleNodeNegMap(RuleDeclNode ruleNode) {
		Collection<BaseNode> dpoNodes = getChild(DPO).getChildren();
		Collection<BaseNode> exactNodes = getChild(EXACT).getChildren();
		Set<DeclNode> deletedNodes = ruleNode.getDelete();

		if (isExact()) {
			addToSingleNodeMap(getExactPatternNodes());

			if (isDPO()) {
				reportWarning("The keyword \"dpo\" is redundant for exact patterns");
			}

			for (BaseNode node : exactNodes) {
				node.reportWarning("Exact statement occurs in exact pattern");
			}

			for (BaseNode node : dpoNodes) {
				node.reportWarning("DPO statement occurs in exact pattern");
			}

			return;
		}

		if (isDPO()) {
			addToSingleNodeMap(getDpoPatternNodes(deletedNodes));

			for (BaseNode node : dpoNodes) {
				node.reportWarning("DPO statement occurs in DPO pattern");
			}

			for (BaseNode exactNode : exactNodes) {
				for (BaseNode exactChild : exactNode.getChildren()) {
					NodeDeclNode nodeDeclNode = (NodeDeclNode) exactChild;
					if (deletedNodes.contains(nodeDeclNode)) {
						exactNode.reportWarning("Exact statement for "
								+ nodeDeclNode.getUseString()
								+ " "
								+ nodeDeclNode.getIdentNode().getSymbol()
										.getText()
								+ " is redundant, since the pattern is DPO");

					}
				}
			}
		}

		//Set<NodeCharacter> genExactNodes = new LinkedHashSet<NodeCharacter>();
		Map<NodeCharacter, Integer> genExactNodes = new LinkedHashMap<NodeCharacter, Integer>();
		// exact Statements
		for (int i = 0; i < getChild(EXACT).getChildren().size(); i++) {
			BaseNode exactNode = getChild(EXACT).getChild(i);
			for (BaseNode exactChild : exactNode.getChildren()) {
				NodeDeclNode nodeDeclNode = (NodeDeclNode) exactChild;
				// coords of occurrence are not available
				if (genExactNodes.containsKey(nodeDeclNode)) {
					exactNode.reportWarning(nodeDeclNode.getUseString()
							+ " "
							+ nodeDeclNode.getIdentNode().getSymbol().getText()
							+ " already occurs in exact statement at "
							+ getChild(EXACT).getChild(
									genExactNodes.get(nodeDeclNode))
									.getCoords());
				} else {
					genExactNodes.put(nodeDeclNode, i);
				}
			}
		}

		Map<NodeCharacter, Integer> genDpoNodes = new LinkedHashMap<NodeCharacter, Integer>();
		// dpo Statements
		for (int i = 0; i < getChild(DPO).getChildren().size(); i++) {
			BaseNode dpoNode = getChild(DPO).getChild(i);

			for (BaseNode dpoChild : dpoNode.getChildren()) {
				NodeDeclNode nodeDeclNode = (NodeDeclNode) dpoChild;
				// coords of occurrence are not available
				if (genExactNodes.containsKey(nodeDeclNode)) {
					dpoNode.reportWarning(nodeDeclNode.getUseString()
							+ " "
							+ nodeDeclNode.getIdentNode().getSymbol().getText()
							+ " already occurs in exact statement at "
							+ getChild(EXACT).getChild(
									genExactNodes.get(nodeDeclNode))
									.getCoords());
				}
				if (genDpoNodes.containsKey(nodeDeclNode)) {
					dpoNode.reportWarning(nodeDeclNode.getUseString()
							+ " "
							+ nodeDeclNode.getIdentNode().getSymbol().getText()
							+ " already occurs in dpo statement at "
							+ getChild(DPO).getChild(
									genDpoNodes.get(nodeDeclNode)).getCoords());
				} else {
					genDpoNodes.put(nodeDeclNode, i);
				}
			}
		}
		addToSingleNodeMap(genDpoNodes.keySet());
		addToSingleNodeMap(genExactNodes.keySet());
	}

	/**
	 * Return the set of nodes needed for the singleNodeNegMap if the whole
	 * pattern is exact.
	 */
	private Set<NodeCharacter> getExactPatternNodes() {

		Set<NodeCharacter> nodes = new LinkedHashSet<NodeCharacter>();
		for (BaseNode n : getChild(CONNECTIONS).getChildren()) {
			ConnectionCharacter conn = (ConnectionCharacter) n;

			NodeCharacter cand = conn.getSrc();
			if (cand instanceof NodeDeclNode
					&& !((NodeDeclNode) cand).isDummy()) {
				nodes.add(cand);
			}
			cand = conn.getTgt();
			if (cand != null && cand instanceof NodeDeclNode
					&& !((NodeDeclNode) cand).isDummy()) {
				nodes.add(cand);
			}
		}

		return nodes;
	}

	/**
	 * Return the set of nodes needed for the singleNodeNegMap if the whole
	 * pattern is dpo.
	 */
	private Set<NodeCharacter> getDpoPatternNodes(Set<DeclNode> deletedEntities) {
		Set<NodeCharacter> deletedNodes = new LinkedHashSet<NodeCharacter>();

		for (DeclNode declNode : deletedEntities) {
			if (declNode instanceof NodeCharacter) {
				if (!(declNode instanceof NodeDeclNode)
						|| !((NodeDeclNode) declNode).isDummy()) {
					deletedNodes.add((NodeCharacter) declNode);
				}
			}
		}

		return deletedNodes;
	}

	private void addSingleNodeNegGraphs(Collection<PatternGraph> ret) {
		// add existing edges to the corresponding sets
		for (BaseNode n : getChild(CONNECTIONS).getChildren()) {
			Set<NodeCharacter> keySet = singleNodeNegMap.keySet();
			if (n instanceof ConnectionNode) {
				ConnectionNode conn = (ConnectionNode) n;
				if (keySet.contains(conn.getSrc())) {
					Set<ConnectionNode> edges = singleNodeNegMap.get(conn.getSrc());
					edges.add(conn);
					singleNodeNegMap.put(conn.getSrc(), edges);
				}
				if (keySet.contains(conn.getTgt())) {
					Set<ConnectionNode> edges = singleNodeNegMap.get(conn.getTgt());
					edges.add(conn);
					singleNodeNegMap.put(conn.getTgt(), edges);
				}
			}
		}

		BaseNode edgeRoot = getEdgeRootType();
		BaseNode nodeRoot = getNodeRootType();

		// generate and add pattern graphs
		for (Entry<NodeCharacter, Set<ConnectionNode>> entry : singleNodeNegMap.entrySet()) {
			for (int direction = INCOMING; direction <= OUTGOING; direction++) {
				PatternGraph neg = new PatternGraph();
				neg.addSingleNode(entry.getKey().getNode());
				for (ConnectionNode conn : entry.getValue()) {
					conn.addToGraph(neg);
				}

				EdgeDeclNode edge = getAnonymousEdgeDecl(edgeRoot);
				NodeDeclNode dummyNode = getAnonymousDummyNode(nodeRoot);

				ConnectionCharacter conn = null;
				if (direction == INCOMING) {
					conn = new ConnectionNode(dummyNode, edge,
							(NodeDeclNode) entry.getKey());
				} else {
					conn = new ConnectionNode((NodeDeclNode) entry.getKey(),
							edge, dummyNode);
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
	private void addToSingleNodeMap(Set<NodeCharacter> nodes) {
		for (NodeCharacter node : nodes) {
			if (!singleNodeNegMap.containsKey(node)) {
				Set<ConnectionNode> edgeSet = new HashSet<ConnectionNode>();
				singleNodeNegMap.put(node, edgeSet);
			}
		}
	}

	private NodeDeclNode getAnonymousDummyNode(BaseNode nodeRoot) {
		IdentNode nodeName = new IdentNode(getScope().defineAnonymous(
				"dummy_node", SymbolTable.getInvalid(), Coords.getBuiltin()));
		NodeDeclNode dummyNode = NodeDeclNode.getDummy(nodeName, nodeRoot);
		return dummyNode;
	}

	private EdgeDeclNode getAnonymousEdgeDecl(BaseNode edgeRoot) {
		IdentNode edgeName = new IdentNode(getScope().defineAnonymous("edge",
				SymbolTable.getInvalid(), Coords.getBuiltin()));
		EdgeDeclNode edge = new EdgeDeclNode(edgeName, edgeRoot);
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
		BaseNode model = root.getChild(UnitNode.MODELS).getChild(0);
		Collection<BaseNode> types = model.getChild(ModelNode.DECLS).getChildren();

		for (Iterator<BaseNode> it = types.iterator(); it.hasNext();) {
			BaseNode candidate = it.next();
			IdentNode ident = (IdentNode) candidate.getChild(DeclNode.IDENT);
			String name = ident.getSymbol().getText();
			if (name.equals("Node")) {
				nodeRoot = candidate;
			}
		}
		return nodeRoot;
	}

	/**
	 * Return the set of nodes needed for the doubleNodeNegMap if the whole
	 * pattern is induced.
	 */
	private Set<NodeCharacter> getInducedPatternNodes() {
		Set<NodeCharacter> nodes = new HashSet<NodeCharacter>();
		for (BaseNode n : getChild(CONNECTIONS).getChildren()) {
			ConnectionCharacter conn = (ConnectionCharacter) n;

			NodeCharacter cand = conn.getSrc();
			if (cand instanceof NodeDeclNode
					&& !((NodeDeclNode) cand).isDummy()) {
				nodes.add(cand);
			}
			cand = conn.getTgt();
			if (cand != null && cand instanceof NodeDeclNode
					&& !((NodeDeclNode) cand).isDummy()) {
				nodes.add(cand);
			}
		}

		return nodes;
	}

	/**
	 * @param negs
	 */
	private void addDoubleNodeNegGraphs(Collection<PatternGraph> negs) {
		// add existing edges to the corresponding pattern graph
		for (BaseNode n : getChild(CONNECTIONS).getChildren()) {
			if (n instanceof ConnectionNode) {
				ConnectionNode conn = (ConnectionNode) n;

				List<NodeCharacter> key = new LinkedList<NodeCharacter>();
				key.add(conn.getSrc());
				key.add(conn.getTgt());

				PatternGraph neg = doubleNodeNegMap.get(key);
				// neg == null for dangling edges
				if (neg != null) {
					conn.addToGraph(neg);
					doubleNodeNegMap.put(key, neg);
				}
			}
		}

		BaseNode edgeRoot = getEdgeRootType();

		// add another Edge of type edgeRoot to each NAC
		for (Entry<List<NodeCharacter>, PatternGraph> entry : doubleNodeNegMap.entrySet()) {
			// TODO check casts
			NodeDeclNode src = (NodeDeclNode) entry.getKey().get(0);
			NodeDeclNode tgt = (NodeDeclNode) entry.getKey().get(1);

			EdgeDeclNode edge = getAnonymousEdgeDecl(edgeRoot);

			ConnectionCharacter conn = new ConnectionNode(src, edge, tgt);

			conn.addToGraph(entry.getValue());
		}

		// finally add all pattern graphs
		for (PatternGraph n : doubleNodeNegMap.values()) {
			negs.add(n);
		}
	}

	private void addToDoubleNodeMap(Set<NodeCharacter> nodes) {
		for (NodeCharacter src : nodes) {
			for (NodeCharacter tgt : nodes) {
				List<NodeCharacter> key = new LinkedList<NodeCharacter>();
				key.add(src);
				key.add(tgt);

				if (!doubleNodeNegMap.containsKey(key)) {
					PatternGraph neg = new PatternGraph();
					doubleNodeNegMap.put(key, neg);
				}
			}
		}
	}

	private BaseNode getEdgeRootType() {
		// get root node
		BaseNode root = this;
		while (!root.isRoot()) {
			root = root.getParents().iterator().next();
		}

		// find an edgeRoot-type
		BaseNode edgeRoot = null;
		BaseNode model = root.getChild(UnitNode.MODELS).getChild(0);
		Collection<BaseNode> types = model.getChild(ModelNode.DECLS).getChildren();

		for (Iterator<BaseNode> it = types.iterator(); it.hasNext();) {
			BaseNode candidate = it.next();
			IdentNode ident = (IdentNode) candidate.getChild(DeclNode.IDENT);
			String name = ident.getSymbol().getText();
			if (name.equals("Edge")) {
				edgeRoot = candidate;
			}
		}
		return edgeRoot;
	}
}
