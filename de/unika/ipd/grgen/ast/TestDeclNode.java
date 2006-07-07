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
		for (BaseNode n : negs.getChildren())
			res.add((GraphNode)n);
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
					for (BaseNode iBN : graphs[i].getConnections()) {
						ConnectionCharacter iConn = (ConnectionCharacter)iBN;
						if (! (iConn instanceof ConnectionNode)) continue;

						for (BaseNode oBN : graphs[o].getConnections()) {
							ConnectionCharacter oConn = (ConnectionCharacter)oBN;
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
		for (BaseNode n : getChild(NEG).getChildren()) {
			PatternGraph neg = ((PatternGraphNode)n).getPatternGraph();
			test.addNegGraph(neg);
		}
		// after all graphs are added, call coalesceAnonymousEdges
		test.coalesceAnonymousEdges();
		
		return test;
	}
}

