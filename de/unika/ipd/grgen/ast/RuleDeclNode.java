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
		boolean childTypes = super.check() && checkChild(RIGHT, GraphNode.class)
			&& checkChild(EVAL, evalChecker);
		
		boolean returnParams = true;
		if(((GraphNode)getChild(PATTERN)).getReturn().children() > 0) {
			error.error(this.getCoords(), "no return in pattern parts of rules allowed");
			returnParams = false;
		}
		
		return childTypes && returnParams;
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

