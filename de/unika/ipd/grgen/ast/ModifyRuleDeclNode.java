package de.unika.ipd.grgen.ast;

import java.util.Collection;
import java.util.HashSet;

import de.unika.ipd.grgen.ast.util.Checker;
import de.unika.ipd.grgen.ast.util.CollectChecker;
import de.unika.ipd.grgen.ast.util.CollectResolver;
import de.unika.ipd.grgen.ast.util.DeclResolver;
import de.unika.ipd.grgen.ast.util.MultChecker;
import de.unika.ipd.grgen.ast.util.Resolver;
import de.unika.ipd.grgen.ir.Assignment;
import de.unika.ipd.grgen.ir.Edge;
import de.unika.ipd.grgen.ir.Entity;
import de.unika.ipd.grgen.ir.Graph;
import de.unika.ipd.grgen.ir.IR;
import de.unika.ipd.grgen.ir.Node;
import de.unika.ipd.grgen.ir.PatternGraph;
import de.unika.ipd.grgen.ir.Rule;

public class ModifyRuleDeclNode extends RuleDeclNode {
	
	private static final int DELETE = LAST + 7;
	
	private static final Resolver deleteResolver =
		new CollectResolver(
		new DeclResolver(
							   new Class[] { NodeDeclNode.class, EdgeDeclNode.class }));
	
	private static final Checker deleteChecker =
		new CollectChecker(
		new MultChecker(
							  new Class[] { NodeDeclNode.class, EdgeDeclNode.class }));
	
	public ModifyRuleDeclNode(IdentNode id, BaseNode left, BaseNode right,
							  BaseNode neg, BaseNode eval, CollectNode params, CollectNode rets, CollectNode dels) {
		super(id, left, right, neg, eval, params, rets);
		addChild(dels);
		addResolver(DELETE, deleteResolver);
	}
	
	@Override
		protected boolean check() {
		return super.check() && checkChild(DELETE, deleteChecker);
	}
	
	@Override
		protected IR constructIR() {
		PatternGraph left = ((PatternGraphNode) getChild(PATTERN)).getPatternGraph();
		Graph right = ((GraphNode) getChild(RIGHT)).getGraph();
		
		Collection<Entity> deleteSet = new HashSet<Entity>();
		for(BaseNode n : getChild(DELETE).getChildren()) {
			deleteSet.add((Entity)n.checkIR(Entity.class));
		}
		
		for(Node n : left.getNodes()) {
			if(!deleteSet.contains(n))
				right.addSingleNode(n);
		}
		for(Edge e : left.getEdges()) {
			if(!deleteSet.contains(e) &&
			   !deleteSet.contains(left.getSource(e)) &&
			   !deleteSet.contains(left.getTarget(e)))
				right.addConnection(left.getSource(e), e, left.getTarget(e));
		}
		
		Rule rule = new Rule(getIdentNode().getIdent(), left, right);
		
		constructIRaux(rule, ((GraphNode)getChild(RIGHT)).getReturn());
		
		// add Params to the IR
		for(BaseNode n : getChild(PARAM).getChildren()) {
			DeclNode param = (DeclNode)n;
			if(!deleteSet.contains(param.getIR()))
				if(param instanceof NodeCharacter) {
					right.addSingleNode(((NodeCharacter)param).getNode());
				}
				else if (param instanceof EdgeCharacter) {
					Edge e = ((EdgeCharacter)param).getEdge();
					if(!deleteSet.contains(e) &&
					   !deleteSet.contains(left.getSource(e)) &&
					   !deleteSet.contains(left.getTarget(e)))
						right.addConnection(left.getSource(e),e, left.getTarget((e)));
				}
				else
					throw new IllegalArgumentException("unknown Class: " + n);
		}
		
		// add Eval statments to the IR
		for(BaseNode n : getChild(EVAL).getChildren()) {
			AssignNode eval = (AssignNode)n;
			rule.addEval((Assignment) eval.checkIR(Assignment.class));
		}
		
		return rule;
	}
	
}
