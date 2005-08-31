/**
 * @author shack
 * @version $Id$
 */
package de.unika.ipd.grgen.ast;

import java.util.Collection;
import java.util.HashSet;
import java.util.Iterator;
import java.util.LinkedList;
import java.util.Set;

import de.unika.ipd.grgen.ast.util.Checker;
import de.unika.ipd.grgen.ast.util.CollectChecker;
import de.unika.ipd.grgen.ast.util.SimpleChecker;
import de.unika.ipd.grgen.ir.IR;
import de.unika.ipd.grgen.ir.PatternGraph;
import de.unika.ipd.grgen.ir.Test;

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
	
	protected Collection<GraphNode> getGraphs() {
		Collection<GraphNode> res = new LinkedList<GraphNode>();
		CollectNode negs  = (CollectNode) getChild(NEG);
		res.add((GraphNode) getChild(PATTERN));
		for (Iterator<BaseNode> negsIt = negs.getChildren(); negsIt.hasNext();)
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
		
		boolean edgeReUse = false;
		if (childs) {
			edgeReUse = true;
			//Check if reused names of edges connect the same nodes in the same direction for each usage
			GraphNode[] graphs = (GraphNode[]) getGraphs().toArray(new GraphNode[0]);
			Collection<EdgeCharacter> alreadyReported = new HashSet<EdgeCharacter>();
			
			for (int i=0; i<graphs.length; i++)
				for (int o=i+1; o<graphs.length; o++)
					for (Iterator<BaseNode> iIter = graphs[i].getConnections(); iIter.hasNext();) {
						ConnectionCharacter iConn = (ConnectionCharacter) iIter.next();
						if (! (iConn instanceof ConnectionNode)) continue;

						for (Iterator<BaseNode> oIter = graphs[o].getConnections(); oIter.hasNext();) {
							ConnectionCharacter oConn = (ConnectionCharacter) oIter.next();
							if (! (oConn instanceof ConnectionNode)) continue;
	
							if (iConn.getEdge().equals(oConn.getEdge()) && !alreadyReported.contains(iConn.getEdge())) {
								NodeDeclNode src, tgt;
							  src = oConn.getSrc();
								tgt = oConn.getTgt();
								if (src instanceof NodeTypeChangeNode) {
									src = ((NodeTypeChangeNode) src).getOldNode();
								}
								if (tgt instanceof NodeTypeChangeNode) {
									tgt = ((NodeTypeChangeNode) tgt).getOldNode();
								}
								
								if (iConn.getSrc() != src || iConn.getTgt() != tgt) {
									alreadyReported.add(iConn.getEdge());
									((ConnectionNode) oConn).reportError("Reused edge does not connect the same nodes");
									edgeReUse = false;
								}
							}
						}
					}
		}
		return childs && edgeReUse;
	}
	
	protected IR constructIR() {
		PatternGraph gr = ((PatternGraphNode) getChild(PATTERN)).getPatternGraph();
		Test test = new Test(getIdentNode().getIdent(), gr);
		
		// add negative parts to the IR
		for (Iterator<BaseNode> negsIt = getChild(NEG).getChildren(); negsIt.hasNext();) {
			PatternGraph neg = ((PatternGraphNode) negsIt.next()).getPatternGraph();
			test.addNegGraph(neg);
		}
		// after all graphs are added, call coalesceAnonymousEdges
		test.coalesceAnonymousEdges();
		
		return test;
	}
}

