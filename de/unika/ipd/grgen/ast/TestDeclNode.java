/**
 * @author shack
 * @version $Id$
 */
package de.unika.ipd.grgen.ast;

import de.unika.ipd.grgen.ast.util.Checker;
import de.unika.ipd.grgen.ast.util.CollectChecker;
import de.unika.ipd.grgen.ast.PatternGraphNode;
import de.unika.ipd.grgen.ast.util.SimpleChecker;
import de.unika.ipd.grgen.ir.IR;
import de.unika.ipd.grgen.ir.PatternGraph;
import de.unika.ipd.grgen.ir.Test;
import java.util.Iterator;
import java.util.Set;
import java.util.Collection;
import java.util.LinkedList;
import java.util.HashMap;
import java.util.Map;
import java.util.HashSet;

/**
 * A type that represents tests
 */
public class TestDeclNode extends ActionDeclNode {
	
	protected static final int PATTERN = LAST + 1;
	protected static final int NEG = LAST + 2;
	
	private static final String[] childrenNames =
		addChildrenNames(new String[] { "test", "neg" });
	
	private static final TypeNode testType = new TypeNode() { };
	
	private static final Checker condChecker =
		new CollectChecker(new SimpleChecker(ExprNode.class));
	
	static {
		setName(TestDeclNode.class, "test declaration");
		setName(testType.getClass(), "test type");
	}
	
	protected TestDeclNode(IdentNode id, TypeNode type) {
		super(id, testType);
	}
	
	public TestDeclNode(IdentNode id, BaseNode pattern, BaseNode neg) {
		super(id, testType);
		addChild(pattern);
		addChild(neg);
		setChildrenNames(childrenNames);
	}
	
	protected Collection getGraphs() {
		Collection res = new LinkedList();
		CollectNode negs  = (CollectNode) getChild(NEG);
		res.add((GraphNode) getChild(PATTERN));
		for (Iterator negsIt = negs.getChildren(); negsIt.hasNext();)
			res.add((GraphNode) negsIt.next());
		return res;
	}
	
	/**
	 * Method check
	 *
	 * @return   a boolean
	 *
	 */
	protected boolean check() {
		boolean childs = checkChild(PATTERN, PatternGraphNode.class)
			&& checkChild(NEG, negChecker);
		
		boolean homomorphic = true;
		if(childs) {
			//Nodes that occur in a NAC part but not in the left side of a rule
			//may not be mapped non-injectively.
			CollectNode negs  = (CollectNode) getChild(NEG);
			GraphNode left = (GraphNode) getChild(PATTERN);
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
		
		boolean edgeReUse = false;
		if (childs) {
			edgeReUse = true;
			//Check if reused names of edges connect the same nodes in the same direction for each usage
			GraphNode[] graphs = (GraphNode[]) getGraphs().toArray(new GraphNode[0]);
			Collection alreadyReported = new HashSet();
			
			for (int i=0; i<graphs.length; i++)
				for (int o=i+1; o<graphs.length; o++)
					for (Iterator iIter = graphs[i].getConnections(); iIter.hasNext();) {
						ConnectionCharacter iConn = (ConnectionCharacter) iIter.next();
						if (! (iConn instanceof ConnectionNode)) continue;

						for (Iterator oIter = graphs[o].getConnections(); oIter.hasNext();) {
							ConnectionCharacter oConn = (ConnectionCharacter) oIter.next();
							if (! (oConn instanceof ConnectionNode)) continue;
	
							if (iConn.getEdge().equals(oConn.getEdge()) && !alreadyReported.contains(iConn.getEdge()))
								if (iConn.getSrc() != oConn.getSrc() || iConn.getTgt() != oConn.getTgt()) {
									alreadyReported.add(iConn.getEdge());
									((ConnectionNode) oConn).reportError("Reused edge does not connect the same nodes");
									edgeReUse = false;
								}
						}
					}
		}
		return childs && homomorphic && edgeReUse;
	}
	
	protected IR constructIR() {
		PatternGraph gr = ((PatternGraphNode) getChild(PATTERN)).getPatternGraph();
		Test test = new Test(getIdentNode().getIdent(), gr);
		
		// add negative parts to the IR
		for (Iterator negsIt = getChild(NEG).getChildren(); negsIt.hasNext();) {
			PatternGraph neg = ((PatternGraphNode) negsIt.next()).getPatternGraph();
			test.addNegGraph(neg);
		}
		// after all graphs are added, call coalesceAnonymousEdges
		test.coalesceAnonymousEdges();
		
		return test;
	}
}

