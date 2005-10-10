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
 * @author Sebastian Hack, Daniel Grund
 * @version $Id$
 */
package de.unika.ipd.grgen.ast;

import de.unika.ipd.grgen.ast.util.Checker;
import de.unika.ipd.grgen.ast.util.CollectChecker;
import de.unika.ipd.grgen.ast.util.SimpleChecker;
import de.unika.ipd.grgen.ir.Assignment;
import de.unika.ipd.grgen.ir.Graph;
import de.unika.ipd.grgen.ir.IR;
import de.unika.ipd.grgen.ir.PatternGraph;
import de.unika.ipd.grgen.ir.Rule;
import java.util.Collection;
import java.util.Iterator;

/**
 * AST node for a replacement rule.
 */
public class RuleDeclNode extends TestDeclNode {
	
	private static final int RIGHT = LAST + 3;
	private static final int EVAL = LAST + 4;
	
	private static final String[] childrenNames = {
		declChildrenNames[0], declChildrenNames[1],
			"left", "neg", "right", "eval"
	};
	
	/** Type for this declaration. */
	private static final TypeNode ruleType = new TypeNode() { };
	
	private static final Checker evalChecker =
		new CollectChecker(new SimpleChecker(ExprNode.class));
	
	static {
		setName(RuleDeclNode.class, "rule declaration");
		setName(ruleType.getClass(), "rule type");
	}
	
	/**
	 * Make a new rule.
	 * @param id The identifier of this rule.
	 * @param left The left hand side (The pattern to match).
	 * @param right The right hand side.
	 * @param neg The context preventing the rule to match.
	 * @param eval The evaluations.
	 */
  public RuleDeclNode(IdentNode id, BaseNode left, BaseNode right, BaseNode neg,
											BaseNode eval) {
		
		super(id, ruleType);
		setChildrenNames(childrenNames);
		addChild(left);
		addChild(neg);
		addChild(right);
		addChild(eval);
  }
  
	protected Collection<GraphNode> getGraphs() {
		Collection<GraphNode> res = super.getGraphs();
		res.add((GraphNode) getChild(RIGHT));
		return res;
	}

	private boolean __recursive_find_decl(BaseNode x, DeclNode decl) {
		for(Iterator<BaseNode> i = x.getChildren(); i.hasNext();) {
			BaseNode b = i.next();
		
			if(b instanceof EdgeDeclNode) {
				
				EdgeDeclNode decl2 = (EdgeDeclNode)b;
				
				if(decl2 == decl)
					return true;
			} else
				
			if(b instanceof NodeDeclNode) {
				NodeDeclNode decl2 = (NodeDeclNode)b;
				
				if(decl2 == decl)
					return true;
				
			} else
				
			if(__recursive_find_decl(b, decl))
				return true;
		}

		return false;
	}
	
	private NodeTypeNode __recursive_find_new_type(BaseNode x, NodeDeclNode node) {
		for(Iterator<BaseNode> i = x.getChildren(); i.hasNext();) {
			BaseNode b = i.next();
			NodeTypeNode new_type;
		
			if(b instanceof NodeTypeChangeNode) {
				NodeTypeChangeNode ncn = (NodeTypeChangeNode)b;
				
				if(node == ncn.getChild(0))
					return (NodeTypeNode)ncn.getChild(1);
					
				
			} else

			if(b instanceof NodeDeclNode)
				return null;
			else
			if(b instanceof EdgeDeclNode)
				return null;
			else
				
			if((new_type = __recursive_find_new_type(b, node)) != null)
				return new_type;
		}

		return null;
	}
	
	private boolean __recursive_find_member_in_type(NodeTypeNode type, MemberDeclNode member) {
		/* iterate over members */
		for(Iterator<BaseNode> i = type.getChild(1).getChildren(); i.hasNext();) {
			MemberDeclNode member2 = (MemberDeclNode) i.next();
				
			if(member == member2)
				return true;
		}
		
		/* iterate over base types */
		for(Iterator<BaseNode> i = type.getChild(0).getChildren(); i.hasNext();) {
			NodeTypeNode next_type = (NodeTypeNode) i.next();
				
			if(__recursive_find_member_in_type(next_type, member))
				return true;
		}

		return false;
	}
	
	/**
	 * Check, if the rule type node is right.
	 * The children of a rule type are
	 * 1) a pattern for the left side.
	 * 2) a pattern for the right side.
	 * @see de.unika.ipd.grgen.ast.BaseNode#check()
	 */
	protected boolean check() {
		boolean childTypes = super.check()
			&& checkChild(RIGHT, GraphNode.class);
//
//		CollectNode evals = (CollectNode)getChild(EVAL);
//		GraphNode right = (GraphNode) getChild(RIGHT);
//
//		for(Iterator i = evals.getChildren(); i.hasNext();) {
//			AssignNode eval = (AssignNode) i.next();
//
//			QualIdentNode lhs = (QualIdentNode)eval.getChild(0);
//			ExprNode rhs = (ExprNode)eval.getChild(1);
//
//			/* check if qual on left hand side has an occurence in the replace graph */
//
//			BaseNode node_or_edge = (BaseNode)lhs.getChild(0);
//
//			if( node_or_edge instanceof EdgeDeclNode ) {
//				EdgeDeclNode decl = (EdgeDeclNode)node_or_edge;
//
//				if(!__recursive_find_decl(right, decl)) {
//					error.error(eval.getCoords(), "assignment to attribute of deleted edge");
//					return false;
//				}
//
//			} else
//			if( node_or_edge instanceof NodeDeclNode ) {
//				NodeDeclNode decl = (NodeDeclNode)node_or_edge;
//
//				if(!__recursive_find_decl(right, decl)) {
//					error.error(eval.getCoords(), "assignment to attribute of deleted node");
//					return false;
//				}
//
//				NodeTypeNode new_type = __recursive_find_new_type(right, decl);
//				MemberDeclNode member = (MemberDeclNode)lhs.getChild(1);
//				if(new_type != null) {
//					if(!__recursive_find_member_in_type(new_type, member)) {
//						error.error(eval.getCoords(), "assignment to attribute removed by type change");
//						return false;
//					}
//				}
//			} else
//				assert false;
//		 }
//
		return childTypes;
	}
	
  /**
	 * @see de.unika.ipd.grgen.ast.BaseNode#constructIR()
	 */
  protected IR constructIR() {
		PatternGraph left = ((PatternGraphNode) getChild(PATTERN)).getPatternGraph();
		Graph right = ((GraphNode) getChild(RIGHT)).getGraph();
		
		Rule rule = new Rule(getIdentNode().getIdent(), left, right);
		
		// add negative parts to the IR
		for (Iterator<BaseNode> negsIt = getChild(NEG).getChildren(); negsIt.hasNext();) {
			PatternGraph neg = ((PatternGraphNode) negsIt.next()).getPatternGraph();
			rule.addNegGraph(neg);
		}
		// NOW! after all graphs are added, call coalesceAnonymousEdges
		rule.coalesceAnonymousEdges();
		
		// add Eval statments to the IR
		for(Iterator<BaseNode> it = getChild(EVAL).getChildren(); it.hasNext();) {
			AssignNode eval = (AssignNode) it.next();
			rule.addEval((Assignment) eval.checkIR(Assignment.class));
		}
		
		return rule;
  }
	
}
