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

import de.unika.ipd.grgen.ir.*;

import de.unika.ipd.grgen.ast.util.Checker;
import de.unika.ipd.grgen.ast.util.CollectChecker;
import de.unika.ipd.grgen.ast.util.SimpleChecker;
import java.util.Collection;
import java.util.HashSet;

/**
 * AST node for a replacement rule.
 */
public class RuleDeclNode extends TestDeclNode {
	
	protected static final int RIGHT = LAST + 5;
	protected static final int EVAL = LAST + 6;
	
	private static final String[] childrenNames = {
		declChildrenNames[0], declChildrenNames[1],
			"left", "neg", "params", "ret", "right", "eval"
	};
	
	/** Type for this declaration. */
	private static final TypeNode ruleType = new TypeNode() { };
	
	private static final Checker evalChecker =
		new CollectChecker(new SimpleChecker(AssignNode.class));
	
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
						BaseNode eval, CollectNode params, CollectNode rets) {
		
		super(id, ruleType, left, neg, params, rets);
		addChild(right);
		addChild(eval);
		setChildrenNames(childrenNames);
	}
	
	protected Collection<GraphNode> getGraphs() {
		Collection<GraphNode> res = super.getGraphs();
		res.add((GraphNode) getChild(RIGHT));
		return res;
	}
	
	
	/**
	 * Check, if the rule type node is right.
	 * The children of a rule type are
	 * 1) a pattern for the left side.
	 * 2) a pattern for the right side.
	 * @see de.unika.ipd.grgen.ast.BaseNode#check()
	 */
	protected boolean check() {

		boolean leftHandGraphsOk = super.check() && checkChild(RIGHT, GraphNode.class)
			&& checkChild(EVAL, evalChecker);
		
		
		/* Check wether the reused node and edges of the RHS are consistens with the LHS.
		 * If consistent, replace the dummys node  with the nodes the pattern edge is
		 * incident to (if there are no dummy nodes itself, of course). */
		PatternGraphNode left = (PatternGraphNode) getChild(PATTERN);
		GraphNode right = (GraphNode) getChild(RIGHT);
		
		boolean rightHandReuseOk = true;
		Collection<EdgeDeclNode> alreadyReported = new HashSet<EdgeDeclNode>();
		for (BaseNode lc : left.getConnections()) {
			for (BaseNode rc : right.getConnections()) {
				
				if (lc instanceof SingleNodeConnNode ||
						rc instanceof SingleNodeConnNode ) continue;

				ConnectionNode lConn = (ConnectionNode) lc;
				ConnectionNode rConn = (ConnectionNode) rc;
	
				EdgeDeclNode le = (EdgeDeclNode) lConn.getEdge();
				EdgeDeclNode re = (EdgeDeclNode) rConn.getEdge();
				
				if (re instanceof EdgeTypeChangeNode)
					re = (EdgeDeclNode) ((EdgeTypeChangeNode)re).getOldEdge();

				if ( ! le.equals(re) ) continue;

				NodeDeclNode lSrc = (NodeDeclNode) lConn.getSrc();
				NodeDeclNode lTgt = (NodeDeclNode) lConn.getTgt();
				NodeDeclNode rSrc = (NodeDeclNode) rConn.getSrc();
				NodeDeclNode rTgt = (NodeDeclNode) rConn.getTgt();
			
				if (rSrc instanceof NodeTypeChangeNode)
					rSrc = (NodeDeclNode) ((NodeTypeChangeNode)rSrc).getOldNode();
				if (rTgt instanceof NodeTypeChangeNode)
					rTgt = (NodeDeclNode) ((NodeTypeChangeNode)rTgt).getOldNode();
				
				if ( ! lSrc.isDummy() ) {
					if ( rSrc.isDummy() ) {
						if ( right.getNodes().contains(lSrc) ) {
							//replace the dummy src node by the src node of the pattern connection
							rConn.setSrc(lSrc);
						}
						else if ( ! alreadyReported.contains(re) ) {
							rightHandReuseOk = false;
							rConn.reportError("dangling reused edge in the RHS are " +
								"allowed only if all its incident nodes are reused, too");
							alreadyReported.add(re);
						}
					}
					else if (lSrc != rSrc) {
						rightHandReuseOk = false;
						rConn.reportError("reused edge does not connect the same nodes");
						alreadyReported.add(re);
					}
					
				}
				
				if ( ! lTgt.isDummy() ) {
					if ( rTgt.isDummy() ) {
						if ( right.getNodes().contains(lTgt) ) {
							//replace the dummy tgt node by the tgt node of the pattern connection
							rConn.setTgt(lTgt);
						}
						else if ( ! alreadyReported.contains(re) ) {
							rightHandReuseOk = false;
							rConn.reportError("dangling reused edge in the RHS are " +
								"allowed only if all its incident nodes are reused, too");
							alreadyReported.add(re);
						}
					}
					else if ( lTgt != rTgt ) {
						rightHandReuseOk = false;
						rConn.reportError("reused edge does not connect the same nodes");
						alreadyReported.add(re);
					}
				}

				if ( ! alreadyReported.contains(re) ) {
					if ( lSrc.isDummy() && ! rSrc.isDummy() ) {
						rightHandReuseOk = false;
						rConn.reportError("reused edge dangles on LHS, but has a src node on RHS");
						alreadyReported.add(re);
					}
					if ( lTgt.isDummy() && ! rTgt.isDummy() ) {
						rightHandReuseOk = false;
						rConn.reportError("reused edge dangles on LHS, but has a tgt node on RHS");
						alreadyReported.add(re);
					}
				}
			}
		}

		boolean noReturnInPatternOk = true;
		if(((GraphNode)getChild(PATTERN)).getReturn().children() > 0) {
			error.error(this.getCoords(), "no return in pattern parts of rules allowed");
			noReturnInPatternOk = false;
		}

		//check that only graph elems are returned that are not deleted
		boolean returnsInRhsOk = true;
		CollectNode returns = (CollectNode) right.getReturn();
		for (BaseNode x : returns.getChildren()) {

			IdentNode ident = (IdentNode) x;
			DeclNode retElem = ident.getDecl();

			if (retElem instanceof NodeDeclNode && !right.getNodes().contains(retElem) ) {
				returnsInRhsOk = false;
				if (
					left.getNodes().contains(retElem) ||
					getChild(PARAM).getChildren().contains(retElem)
				)
					((IdentNode)ident).reportError("deleted node \"" + ident + "\" must not be returned");
				else
					assert false: "the node \"" + ident + "\", that is even " +
						"not a parameter and neither contained in LHS nor in " +
						"RHS and, occurs in a return";
			}
			if (retElem instanceof EdgeDeclNode && !right.getNodes().contains(retElem) ) {
				returnsInRhsOk = false;
				if (
					left.getEdges().contains(retElem) ||
					getChild(PARAM).getChildren().contains(retElem)
				)
					((IdentNode)ident).reportError("deleted edge \"" + ident + "\" must not be returned");
				else
					assert false: "the edge \"" + ident + "\", that is even " +
						"not a parameter and neither contained in LHS nor in " +
						"RHS and, occurs in a return";
			}
		}
		
		return leftHandGraphsOk && rightHandReuseOk && noReturnInPatternOk && returnsInRhsOk;
	}
	
	
	/**
	 * @see de.unika.ipd.grgen.ast.BaseNode#constructIR()
	 */
	protected IR constructIR() {
		PatternGraph left = ((PatternGraphNode) getChild(PATTERN)).getPatternGraph();
		Graph right = ((GraphNode) getChild(RIGHT)).getGraph();
		
		Rule rule = new Rule(getIdentNode().getIdent(), left, right);
		
		constructIRaux(rule, ((GraphNode)getChild(RIGHT)).getReturn());
		
		// add Eval statments to the IR
		for(BaseNode n : getChild(EVAL).getChildren()) {
			AssignNode eval = (AssignNode)n;
			rule.addEval((Assignment) eval.checkIR(Assignment.class));
		}
				
		return rule;
	}
}


