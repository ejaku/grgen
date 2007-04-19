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

import de.unika.ipd.grgen.ir.*;

import de.unika.ipd.grgen.ast.util.Checker;
import de.unika.ipd.grgen.ast.util.CollectChecker;
import de.unika.ipd.grgen.ast.util.SimpleChecker;
import java.util.Collection;
import java.util.HashSet;
import java.util.Iterator;
import java.util.LinkedList;

/**
 * A type that represents tests
 */
public class TestDeclNode extends ActionDeclNode {
	
	protected static final int PATTERN = LAST + 1;
	protected static final int NEG = LAST + 2;
	protected static final int PARAM = LAST + 3;
	protected static final int RET = LAST + 4;
	
	private static final String[] childrenNames =
		addChildrenNames(new String[] { "test", "neg", "param", "ret" });
	
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
	
	protected TestDeclNode(IdentNode id, TypeNode type, BaseNode pattern, BaseNode neg, CollectNode params, CollectNode rets) {
		super(id, type);
		addChild(pattern);
		addChild(neg);
		addChild(params);
		addChild(rets);
		setChildrenNames(childrenNames);
	}
	
	public TestDeclNode(IdentNode id, BaseNode pattern, BaseNode neg, CollectNode params, CollectNode rets) {
		this(id, testType, pattern, neg, params, rets);
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
	 * check if actual return entities are conformant
	 * to the formal return parameters.
	 */
	protected boolean checkReturnParams(BaseNode typeReturns, BaseNode actualReturns) {
		boolean returnTypes = true;
		
		/*
		 System.out.println("\n*** this          = " + this.getClass());
		 System.out.println("    this          = " + this.getChildren());
		 System.out.println("*** typeReturns   = "   + typeReturns);
		 System.out.println("    typeReturns   = "   + typeReturns.getChildren());
		 System.out.println("*** actualReturns = " + actualReturns);
		 System.out.println("    actualReturns = " + actualReturns.getChildren());
		 */
		
		
		if(actualReturns.children() != typeReturns.children()) {
			error.error(this.getCoords(), "actual and formal return-parameter count mismatch (" +
							actualReturns.children() + " vs. " + typeReturns.children() +")");
			returnTypes = false;
		} else {
			Iterator<BaseNode> itAR = actualReturns.getChildren().iterator();
			
			for(BaseNode n : typeReturns.getChildren()) {
				IdentNode       tReturnAST  = (IdentNode)n;
				InheritanceType tReturn     = (InheritanceType)tReturnAST.getDecl().getDeclType().checkIR(InheritanceType.class);
				
				IdentNode       aReturnAST  = (IdentNode)itAR.next();
				InheritanceType aReturnType = (InheritanceType)aReturnAST.getDecl().getDeclType().checkIR(InheritanceType.class);
				
				if(!aReturnType.isCastableTo(tReturn)) {
					error.error(aReturnAST.getCoords(), "actual return-parameter is not conformant to formal parameter (" +
									aReturnType + " not castable to " + tReturn + ")");
					returnTypes = false;
				}
			}
		}
		
		return returnTypes;
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
								NodeCharacter src, tgt;
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
		
		boolean returnParams = true;
		if(! (this instanceof RuleDeclNode))
			returnParams = checkReturnParams(getChild(RET), ((GraphNode)getChild(PATTERN)).getReturn());
		
		return childs && edgeReUse && returnParams;
	}
	
	
	protected void constructIRaux(MatchingAction ma, BaseNode aReturns) {
		// add negative parts to the IR
		for (BaseNode n : getChild(NEG).getChildren()) {
			PatternGraph neg = ((PatternGraphNode)n).getPatternGraph();
			ma.addNegGraph(neg);
		}
		
		// add Params to the IR
		for(BaseNode n : getChild(PARAM).getChildren()) {
			ParamDeclNode param = (ParamDeclNode)n;
			ma.addParameter((Entity) param.checkIR(Entity.class));
			if(param instanceof NodeCharacter) {
				ma.getPattern().addSingleNode(((NodeCharacter)param).getNode());
			}
			else if (param instanceof EdgeCharacter) {
				Edge e = ((EdgeCharacter)param).getEdge();
				ma.getPattern().addConnection(ma.getPattern().getSource(e), e,
											  ma.getPattern().getTarget((e)));
			}
			else
				throw new IllegalArgumentException("unknown Class: " + n);
		}
		
		// add Return-Prarams to the IR
		for(BaseNode n : aReturns.getChildren()) {
			IdentNode aReturnAST = (IdentNode)n;
			Entity aReturn = (Entity)aReturnAST.getDecl().checkIR(Entity.class);
			
			// actual return-parameter
			ma.addReturn(aReturn);
		}
	}
	
	protected IR constructIR() {
		PatternGraph left = ((PatternGraphNode) getChild(PATTERN)).getPatternGraph();
		Test test = new Test(getIdentNode().getIdent(), left);
		
		constructIRaux(test, getChild(RET));
		
		return test;
	}
}

