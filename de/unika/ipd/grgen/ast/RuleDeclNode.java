/**
 * @author Sebastian Hack, Daniel Grund
 * @version $Id$
 */
package de.unika.ipd.grgen.ast;

import java.util.Iterator;
import java.util.Set;

import de.unika.ipd.grgen.ast.util.Checker;
import de.unika.ipd.grgen.ast.util.CollectChecker;
import de.unika.ipd.grgen.ast.util.SimpleChecker;
import de.unika.ipd.grgen.ir.Expression;
import de.unika.ipd.grgen.ir.Graph;
import de.unika.ipd.grgen.ir.IR;
import de.unika.ipd.grgen.ir.Rule;

/**
 * AST node for a replacement rule.
 */
public class RuleDeclNode extends ActionDeclNode {

	private static final int LEFT = LAST + 1;
	private static final int RIGHT = LAST + 2;
	private static final int NEG = LAST + 3;
	private static final int REDIR = LAST + 4;
	private static final int COND = LAST + 5;
	private static final int EVAL = LAST + 6;

	
	private static final String[] childrenNames = {
		declChildrenNames[0], declChildrenNames[1],
		"left", "right", "neg", "redir", "cond", "eval"
	};
	
	/** Type for this declaration. */
	private static final TypeNode ruleType = new TypeNode() { };

	/** CollectNode checker for the redirections. */
	private static final Checker redirChecker =
		new CollectChecker(new SimpleChecker(RedirectionNode.class));
		
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
	 * @param redir The redirections.
	 * @param cond The conditions.
	 * @param eval The evaluations.
	 */
  public RuleDeclNode(IdentNode id, BaseNode left, BaseNode right, BaseNode neg,
    BaseNode redir, BaseNode cond, BaseNode eval) {
    
    super(id, ruleType);
    setChildrenNames(childrenNames);
    addChild(left);
    addChild(right);
    addChild(neg);
    addChild(redir);
    addChild(cond);
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
		boolean childTypes = checkChild(LEFT, PatternNode.class)
			&& checkChild(NEG, negChecker)
			&& checkChild(RIGHT, PatternNode.class)
			&& checkChild(REDIR, redirChecker)
			&& checkChild(COND, evalChecker);
			
		boolean cond = false, redirs = false, homomorphic = false;
			
		if(childTypes) {
			redirs = true;
			
			PatternNode left = (PatternNode) getChild(LEFT);
			PatternNode right = (PatternNode) getChild(RIGHT);

			Set leftNodes = left.getNodes();
			Set rightNodes = right.getNodes();
			
			/*
			 * Check, if the redirections are right.
			 * This means, that redirection from nodes may only be from
			 * the left hand side of a rule. The to nodes may only occur
			 * on the right hand side.
			 */
			for(Iterator it = getChild(REDIR).getChildren(); it.hasNext();) {
				RedirectionNode redir = (RedirectionNode) it.next();
				if(!redir.checkFrom(leftNodes)) {
					redir.reportError("From node should appear on the left hand side");
					redirs = false;
				}
					
				if(!redir.checkTo(rightNodes)) {
					redir.reportError("Redirection target should appear on the "
					  + "right hand side");
					redirs = false;
				}
			}
			
		}
		
		if(childTypes) {
			// All conditions must be of type boolean.
			cond = true;
			for(Iterator it = getChild(COND).getChildren(); it.hasNext();) {
				ExprNode e = (ExprNode) it.next();
				
				if(! e.getType().isCompatibleTo(BasicTypeNode.booleanType)) {
					e.reportError("expression must be of type boolean");
					cond = false;
				}
			}
		}

		homomorphic = true;
		if(childTypes) {
			//Nodes that occur in the NAC part but not in the left side of a rule
			//may not be mapped non-injectively.
			CollectNode negs  = (CollectNode) getChild(NEG);
			PatternNode left = (PatternNode) getChild(LEFT);
			for (Iterator negsIt = negs.getChildren(); negsIt.hasNext();) {
				PatternNode neg = (PatternNode) negsIt.next();
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
 		
		return childTypes && redirs && cond && homomorphic;
	}

  /**
   * @see de.unika.ipd.grgen.ast.BaseNode#constructIR()
   */
  protected IR constructIR() {
		Graph left = ((PatternNode) getChild(LEFT)).getGraph();
		Graph right = ((PatternNode) getChild(RIGHT)).getGraph();
		
		Rule rule = new Rule(getIdentNode().getIdent(), left, right);

		// add negative parts to the IR
		for (Iterator negsIt = getChild(NEG).getChildren(); negsIt.hasNext();) {
	        Graph neg = ((PatternNode) negsIt.next()).getGraph();
			rule.addNegGraph(neg);
		}
		// NOW! after all graphs are added, call coalesceAnonymousEdges
		rule.coalesceAnonymousEdges();
		
		// add Redirect statments to the IR
		for(Iterator it = getChild(REDIR).getChildren(); it.hasNext();) {
			RedirectionNode redir = (RedirectionNode) it.next();
			redir.addToRule(rule);
		}
		
		// add Cond statments to the IR
		for(Iterator it = getChild(COND).getChildren(); it.hasNext();) {
			OpNode op = (OpNode) it.next();
			rule.getCondition().add((Expression) op.checkIR(Expression.class));
		}
		
		// add Eval statments to the IR
		for(Iterator it = getChild(EVAL).getChildren(); it.hasNext();) {
			AssignNode eval = (AssignNode) it.next();
			rule.getEvaluation().add(eval.getIR());
		}
		
		return rule;
  }

}
