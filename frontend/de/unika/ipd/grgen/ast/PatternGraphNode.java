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
 * AST node that represents a graph pattern
 * as it appears within the pattern part of some rule
 * Extension of the graph pattern of the rewrite part
 */
public class PatternGraphNode extends GraphNode {

	public static final int MOD_INDUCED = 1;

	public static final int MOD_DPO = 2;

	/**
	 * The modifiers for this type. An ORed combination of the constants above.
	 */
	private int modifiers = 0;

	/** used to add an dangling edge to a PatternGraph. */
	private static final int INCOMING = 0;
	private static final int OUTGOING = 1;

	/** Index of the conditions collect node. */
	private static final int CONDITIONS = RETURN + 1;

	/** Index of the hom statements collect node. */
	private static final int HOMS = CONDITIONS + 1;

	/** Conditions checker. */
	private static final Checker conditionsChecker = new CollectChecker(
			new SimpleChecker(ExprNode.class));

	/** Homomorphic checker. */
	private static final Checker homChecker = new CollectChecker(
			new SimpleChecker(HomNode.class));

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
			int modifiers) {
		super(coords, connections, returns);
		addChild(conditions);
		addChild(homs);
		this.modifiers = modifiers;
	}

	public Collection<BaseNode> getHoms() {
		return getChild(HOMS).getChildren();
	}

	protected boolean check() {
		boolean childs = super.check()
				&& checkChild(CONDITIONS, conditionsChecker)
				&& checkChild(HOMS, homChecker);

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
		for (GraphEntity n : gr.getNodes())
			genTypeCondsFromTypeof(gr, n);
		for (GraphEntity e : gr.getEdges())
			genTypeCondsFromTypeof(gr, e);

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

	/**
	 * Get all implicit NACs.
	 *
	 * @return The Collection for the NACs.
	 */
	public Collection<PatternGraph> getImplicitNegGraphs(RuleDeclNode ruleNode) {
		Collection<PatternGraph> ret = new LinkedList<PatternGraph>();

		if (isInduced()) {
			addInducedNegGraphs(ret);
		}

		if (isDPO()) {
			addDpoNegGraphs(ret, ruleNode.getDelete());
		}

		return ret;
	}

	/**
	 * Add NACs required for the "dpo"-semantic.
	 *
	 * @param negs
	 *            The collection for the NACs.
	 * @param set
	 *            The set of a all deleted entities.
	 */
	protected void addDpoNegGraphs(Collection<PatternGraph> ret,
			Set<DeclNode> deletedEntities) {
		Set<NodeCharacter> deletedNodes = new HashSet<NodeCharacter>();
		// Map to a set of edges -> don't count edges twice
		Map<NodeCharacter, Set<ConnectionNode>> negMap = new LinkedHashMap<NodeCharacter, Set<ConnectionNode>>();

		for (DeclNode declNode : deletedEntities) {
			if (declNode instanceof NodeCharacter) {
				deletedNodes.add((NodeCharacter) declNode);
			}
		}

		// init map
		for (NodeCharacter node : deletedNodes) {
			Set<ConnectionNode> edgeSet = new HashSet<ConnectionNode>();
			negMap.put(node, edgeSet);
		}

		// add existing edges to the corresponding sets
		for (BaseNode n : getChild(CONNECTIONS).getChildren()) {
			Set<NodeCharacter> keySet = negMap.keySet();
			if (n instanceof ConnectionNode) {
				ConnectionNode conn = (ConnectionNode) n;
				if (keySet.contains(conn.getSrc())) {
					Set<ConnectionNode> edges = negMap.get(conn.getSrc());
					edges.add(conn);
					negMap.put(conn.getSrc(), edges);
				}
				if (keySet.contains(conn.getTgt())) {
					Set<ConnectionNode> edges = negMap.get(conn.getTgt());
					edges.add(conn);
					negMap.put(conn.getTgt(), edges);
				}
			}
		}

		BaseNode edgeRoot = getEdgeRootType();
		BaseNode nodeRoot = getNodeRootType();

		// generate and add pattern graphs
		for (Entry<NodeCharacter, Set<ConnectionNode>> entry : negMap
				.entrySet()) {
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
		Collection<BaseNode> types = model.getChild(ModelNode.DECLS)
				.getChildren();

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
	 * Add NACs required for the "induced"-semantic.
	 *
	 * @param negs
	 *            The collection for the NACs.
	 */
	protected void addInducedNegGraphs(Collection<PatternGraph> negs) {
		// map each pair of nodes to a pattern graph
		Map<List<NodeCharacter>, PatternGraph> negMap = new LinkedHashMap<List<NodeCharacter>, PatternGraph>();

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

		// init map
		for (NodeCharacter src : nodes) {
			for (NodeCharacter tgt : nodes) {
				List<NodeCharacter> key = new LinkedList<NodeCharacter>();
				key.add(src);
				key.add(tgt);

				PatternGraph neg = new PatternGraph();
				negMap.put(key, neg);
			}
		}

		// add existing edges to the corresponding pattern graph
		for (BaseNode n : getChild(CONNECTIONS).getChildren()) {
			if (n instanceof ConnectionNode) {
				ConnectionNode conn = (ConnectionNode) n;

				List<NodeCharacter> key = new LinkedList<NodeCharacter>();
				key.add(conn.getSrc());
				key.add(conn.getTgt());

				PatternGraph neg = negMap.get(key);
				// neg == null for dangling edges
				if (neg != null) {
					conn.addToGraph(neg);
					negMap.put(key, neg);
				}
			}
		}

		BaseNode edgeRoot = getEdgeRootType();

		// add another Edge of type edgeRoot to each NAC
		for (Entry<List<NodeCharacter>, PatternGraph> entry : negMap.entrySet()) {
			// TODO check casts
			NodeDeclNode src = (NodeDeclNode) entry.getKey().get(0);
			NodeDeclNode tgt = (NodeDeclNode) entry.getKey().get(1);

			EdgeDeclNode edge = getAnonymousEdgeDecl(edgeRoot);

			ConnectionCharacter conn = new ConnectionNode(src, edge, tgt);

			conn.addToGraph(entry.getValue());
		}

		// finally add all pattern graphs
		for (PatternGraph n : negMap.values()) {
			negs.add(n);
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
		Collection<BaseNode> types = model.getChild(ModelNode.DECLS)
				.getChildren();

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
