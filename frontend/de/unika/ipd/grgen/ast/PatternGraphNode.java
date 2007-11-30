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

import java.util.HashMap;
import java.util.HashSet;
import java.util.Iterator;
import java.util.LinkedList;
import java.util.List;
import java.util.Map;
import java.util.Set;

import de.unika.ipd.grgen.ast.BaseNode;
import de.unika.ipd.grgen.ast.ConnectionCharacter;
import de.unika.ipd.grgen.ast.ExprNode;
import de.unika.ipd.grgen.ast.GraphNode;
import de.unika.ipd.grgen.ast.util.Checker;
import de.unika.ipd.grgen.ast.util.CollectChecker;
import de.unika.ipd.grgen.ast.util.SimpleChecker;
import de.unika.ipd.grgen.ir.GraphEntity;
import de.unika.ipd.grgen.ir.Expression;
import de.unika.ipd.grgen.ir.IR;
import de.unika.ipd.grgen.ir.Operator;
import de.unika.ipd.grgen.ir.PatternGraph;
import de.unika.ipd.grgen.ir.Typeof;
import de.unika.ipd.grgen.parser.Coords;
import de.unika.ipd.grgen.parser.SymbolTable;

import java.util.Collection;
import java.util.Map.Entry;

public class PatternGraphNode extends GraphNode {

	public static final int MOD_INDUCED = 1;

	public static final int MOD_DPO = 2;

	/**
	 * The modifiers for this type. An ORed combination of the constants above.
	 */
	private int modifiers = 0;

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
	 * 
	 * @param connections
	 *            A collection containing connection nodes
	 * @param conditions
	 *            A collection of conditions.
	 */
	public PatternGraphNode(Coords coords, BaseNode connections,
			BaseNode conditions, CollectNode returns, CollectNode homs,
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
			for(BaseNode n : getHoms()) {
				HomNode hom = (HomNode)n;
				
				for(BaseNode m : n.getChildren()) {
					DeclNode decl = (DeclNode)m;
					
					if(hom_ents.contains(decl)) {
						hom.reportError(m.toString() +
								" is contained in multiple hom statements");
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
		if(elem.inheritsType()) {
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
		for(GraphEntity n : gr.getNodes())
			genTypeCondsFromTypeof(gr, n);
		for(GraphEntity e : gr.getEdges())
			genTypeCondsFromTypeof(gr, e);
		
		for(BaseNode n : getHoms()) {
			HomNode hom = (HomNode)n;
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
	public Collection<PatternGraph> getImplicitNegGraphs() {
		Collection<PatternGraph> ret = new LinkedList<PatternGraph>();

		if (isInduced()) {
			addInducedNegGraphs(ret);
		}

		return ret;
	}

	/**
	 * Add NACs required for the "induced"-semantic.
	 * 
	 * @param negs
	 *            The collection for the NACs.
	 */
	protected void addInducedNegGraphs(Collection<PatternGraph> negs) {
		// map each pair of nodes to a pattern graph
		Map<List<NodeCharacter>, PatternGraph> negMap = new HashMap<List<NodeCharacter>, PatternGraph>();

		Set<NodeCharacter> nodes = new HashSet<NodeCharacter>();
		for (BaseNode n : getChild(CONNECTIONS).getChildren()) {
			ConnectionCharacter conn = (ConnectionCharacter) n;

			nodes.add(conn.getSrc());
			if (conn.getTgt() != null) {
				nodes.add(conn.getTgt());
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
				conn.addToGraph(neg);
				negMap.put(key, neg);
			}
		}

		// get root node
		BaseNode root = this;
		while (!root.isRoot()) {
			root = root.getParents().next();
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

		// add another Edge of type edgeRoot to each NAC
		for (Entry<List<NodeCharacter>, PatternGraph> entry : negMap.entrySet()) {
			// TODO check casts
			NodeDeclNode src = (NodeDeclNode) entry.getKey().get(0);
			NodeDeclNode tgt = (NodeDeclNode) entry.getKey().get(1);

			IdentNode edgeName = new IdentNode(getScope().defineAnonymous(
					"edge", SymbolTable.getInvalid(), Coords.getBuiltin()));
			EdgeDeclNode edge = new EdgeDeclNode(edgeName, edgeRoot);

			ConnectionCharacter conn = new ConnectionNode(src, edge, tgt);

			conn.addToGraph(entry.getValue());
		}

		// finally add all pattern graphs
		for (PatternGraph n : negMap.values()) {
			negs.add(n);
		}
	}
}
