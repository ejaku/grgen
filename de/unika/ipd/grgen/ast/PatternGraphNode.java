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
import java.util.HashSet;

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
import java.util.Collection;

public class PatternGraphNode extends GraphNode {
	
	/** Index of the conditions collect node. */
	private static final int CONDITIONS = RETURN + 1;
	
	/** Index of the hom statements collect node. */
	private static final int HOMS = CONDITIONS + 1;

	/** Conditions checker. */
	private static final Checker conditionsChecker =
		new CollectChecker(new SimpleChecker(ExprNode.class));
	
	/** Homomorphic checker. */
	private static final Checker homChecker =
		new CollectChecker(new SimpleChecker(HomNode.class));
	
	static {
		setName(PatternGraphNode.class, "pattern_graph");
	}
	
	/**
	 * A new pattern node
	 * @param connections A collection containing connection nodes
	 * @param conditions A collection of conditions.
	 */
	public PatternGraphNode(Coords coords, BaseNode connections, BaseNode conditions, CollectNode returns, CollectNode homs) {
		super(coords, connections, returns);
		addChild(conditions);
		addChild(homs);
	}

	public Collection<BaseNode> getHoms() {
		return getChild(HOMS).getChildren();
	}
	
	protected boolean check() {
		boolean childs = super.check() &&
			checkChild(CONDITIONS, conditionsChecker) &&
			checkChild(HOMS, homChecker);
		
		boolean expr = true;
		boolean homcheck = true;
		if(childs) {
			for(BaseNode n : getChild(CONDITIONS).getChildren()) {
				// Must go right, since it is checked 5 lines above.
				ExprNode exp = (ExprNode)n;
				if(!exp.getType().isEqual(BasicTypeNode.booleanType)) {
					exp.reportError("Expression must be of type boolean");
					expr = false;
				}
			}
		
			HashSet<DeclNode> hom_ents = new HashSet<DeclNode>();
			for(BaseNode n : getChild(HOMS).getChildren()) {
				HomNode hom = (HomNode)n;
				
				for(BaseNode m : n.getChildren()) {
					DeclNode decl = (DeclNode)m;
					
					if(hom_ents.contains(decl)) {
						hom.reportError(m.toString() +
								" is contained in multiple hom statements");
						homcheck = false;
					}
				}
				for(BaseNode m : n.getChildren()) {
					DeclNode decl = (DeclNode)m;
					
					hom_ents.add(decl);
				}
			}
		
		}
		
		return childs && expr && homcheck;
	}
	
	/**
	 * Get the correctly casted IR object.
	 * @return The IR object.
	 */
	public PatternGraph getPatternGraph() {
		return (PatternGraph) checkIR(PatternGraph.class);
	}
	
	
	protected IR constructIR() {
		PatternGraph gr = new PatternGraph();
		
		for(BaseNode n : getChild(CONNECTIONS).getChildren()) {
			ConnectionCharacter conn = (ConnectionCharacter)n;
			conn.addToGraph(gr);
		}
		
		for(BaseNode n : getChild(CONDITIONS).getChildren()) {
			ExprNode expr = (ExprNode)n;
			expr = expr.evaluate();
			gr.addCondition((Expression) expr.checkIR(Expression.class));
		}
		
		/* genarate type conditions from dynamic type checks via typeof */
		for(GraphEntity n : gr.getNodes()) {
			if(n.inheritsType()) {
				Expression e1 = new Typeof(n);
				Expression e2 = new Typeof(n.getTypeof());

				Operator op = new Operator(BasicTypeNode.booleanType.getPrimitiveType(), Operator.GE);
				op.addOperand(e1);
				op.addOperand(e2);
				
				gr.addCondition(op);
			}
		}
		//TODO iterate over both sets in a single 'for' statement
		for(GraphEntity n : gr.getEdges()) {
			if(n.inheritsType()) {
				Expression e1 = new Typeof(n);
				Expression e2 = new Typeof(n.getTypeof());

				Operator op = new Operator(BasicTypeNode.booleanType.getPrimitiveType(), Operator.GE);
				op.addOperand(e1);
				op.addOperand(e2);
				
				gr.addCondition(op);
			}
		}
		
		for(BaseNode n : getChild(HOMS).getChildren()) {
			HomNode hom = (HomNode)n;
			HashSet<GraphEntity> hom_set = new HashSet<GraphEntity>();

			for(BaseNode m : hom.getChildren()) {
				DeclNode decl = (DeclNode)m;
				hom_set.add((GraphEntity) decl.checkIR(GraphEntity.class));
			}

			gr.addHomomorphic(hom_set);
		}
		
		return gr;
	}
	
}

