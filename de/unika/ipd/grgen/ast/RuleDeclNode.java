/**
 * @author Sebastian Hack, Daniel Grund
 * @version $Id$
 */
package de.unika.ipd.grgen.ast;

import de.unika.ipd.grgen.ast.util.Checker;
import de.unika.ipd.grgen.ast.util.CollectChecker;
import de.unika.ipd.grgen.ast.PatternGraphNode;
import de.unika.ipd.grgen.ast.util.SimpleChecker;
import de.unika.ipd.grgen.ir.Graph;
import de.unika.ipd.grgen.ir.IR;
import de.unika.ipd.grgen.ir.PatternGraph;
import de.unika.ipd.grgen.ir.Rule;
import java.util.Iterator;
import java.util.Set;

/**
 * AST node for a replacement rule.
 */
public class RuleDeclNode extends ActionDeclNode {
	
	private static final int LEFT = LAST + 1;
	private static final int RIGHT = LAST + 2;
	private static final int NEG = LAST + 3;
	private static final int COND = LAST + 4;
	private static final int EVAL = LAST + 5;
	
	
	private static final String[] childrenNames = {
		declChildrenNames[0], declChildrenNames[1],
			"left", "right", "neg", "eval"
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
		addChild(right);
		addChild(neg);
		addChild(eval);
  }
  
	/**
	 * Check, if the rule type node is right.
	 * The children of a rule type are
	 * 1) a pattern for the left side.
	 * 2) a pattern for the right side.
	 * @see de.unika.ipd.grgen.ast.BaseNode#check()
	 */
	protected boolean check() {
		boolean childTypes = checkChild(LEFT, GraphNode.class)
			&& checkChild(NEG, negChecker)
			&& checkChild(RIGHT, GraphNode.class);
		
		boolean redirs = false, homomorphic = false;
		
		if(childTypes) {
			redirs = true;
			
			GraphNode left = (GraphNode) getChild(LEFT);
			GraphNode right = (GraphNode) getChild(RIGHT);
			
			Set leftNodes = left.getNodes();
			Set rightNodes = right.getNodes();
			
		}
		
		homomorphic = true;
		if(childTypes) {
			//Nodes that occur in the NAC part but not in the left side of a rule
			//may not be mapped non-injectively.
			CollectNode negs  = (CollectNode) getChild(NEG);
			GraphNode left = (GraphNode) getChild(LEFT);
			for (Iterator negsIt = negs.getChildren(); negsIt.hasNext();) {
				GraphNode neg = (GraphNode) negsIt.next();
				Set s = neg.getNodes();
				s.removeAll(left.getNodes());
				for (Iterator it = s.iterator(); it.hasNext();) {
					NodeDeclNode nd = (NodeDeclNode) it.next();
					if (nd.hasHomomorphicNodes()) {
						nd.reportError("Node must not have homomorphic nodes (because it is used in a negative section but not in the pattern)");
						homomorphic = false;
					}
				}
			}
		}
		
		return childTypes && redirs && homomorphic;
	}
	
  /**
	 * @see de.unika.ipd.grgen.ast.BaseNode#constructIR()
	 */
  protected IR constructIR() {
		PatternGraph left = ((PatternGraphNode) getChild(LEFT)).getPatternGraph();
		Graph right = ((GraphNode) getChild(RIGHT)).getGraph();
		
		Rule rule = new Rule(getIdentNode().getIdent(), left, right);
		
		// add negative parts to the IR
		for (Iterator negsIt = getChild(NEG).getChildren(); negsIt.hasNext();) {
			PatternGraph neg = ((PatternGraphNode) negsIt.next()).getPatternGraph();
			rule.addNegGraph(neg);
		}
		// NOW! after all graphs are added, call coalesceAnonymousEdges
		rule.coalesceAnonymousEdges();
		
		// add Eval statments to the IR
		for(Iterator it = getChild(EVAL).getChildren(); it.hasNext();) {
			AssignNode eval = (AssignNode) it.next();
			rule.getEvaluation().add(eval.getIR());
		}
		
		return rule;
  }
	
}
