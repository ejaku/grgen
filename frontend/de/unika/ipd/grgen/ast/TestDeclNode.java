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
import java.util.*;

import de.unika.ipd.grgen.ast.util.Checker;
import de.unika.ipd.grgen.ast.util.CollectChecker;
import de.unika.ipd.grgen.ast.util.SimpleChecker;
import de.unika.ipd.grgen.util.report.ErrorReporter;

/**
 * AST node class representing tests
 */
public class TestDeclNode extends ActionDeclNode
{
	static {
		setName(TestDeclNode.class, "test declaration");
	}

	protected static final int PARAM = LAST + 1;
	protected static final int RET = LAST + 2;
	protected static final int PATTERN = LAST + 3;
	protected static final int NEG = LAST + 4;

	private static final String[] childrenNames =
		addChildrenNames(new String[] { "param", "ret", "test", "neg" });
	
	private static final TypeNode testType = new TestTypeNode();

	protected TestDeclNode(IdentNode id, TypeNode type, BaseNode pattern, BaseNode neg, CollectNode params, CollectNode rets) {
		super(id, type);
		addChild(params);
		addChild(rets);
		addChild(pattern);
		addChild(neg);
		setChildrenNames(childrenNames);
	}

	public TestDeclNode(IdentNode id, PatternGraphNode pattern, CollectNode neg, CollectNode params, CollectNode rets) {
		this(id, testType, pattern, neg, params, rets);
	}

	/** implementation of Walkable @see de.unika.ipd.grgen.util.Walkable#getWalkableChildren() */
	public Collection<? extends BaseNode> getWalkableChildren() {
		return children;
	}
	
	/** @see de.unika.ipd.grgen.ast.BaseNode#resolve() */
	protected boolean resolve() {
		if(isResolved()) {
			return resolutionResult();
		}
		
		debug.report(NOTE, "resolve in: " + getId() + "(" + getClass() + ")");
		boolean successfullyResolved = true;
		nodeResolvedSetResult(successfullyResolved); // local result
		
		successfullyResolved = getChild(IDENT).resolve() && successfullyResolved;
		successfullyResolved = getChild(TYPE).resolve() && successfullyResolved;
		successfullyResolved = getChild(PARAM).resolve() && successfullyResolved;
		successfullyResolved = getChild(RET).resolve() && successfullyResolved;
		successfullyResolved = getChild(PATTERN).resolve() && successfullyResolved;
		successfullyResolved = getChild(NEG).resolve() && successfullyResolved;
		return successfullyResolved;
	}
	
	/** @see de.unika.ipd.grgen.ast.BaseNode#check() */
	protected boolean check() {
		if(!resolutionResult()) {
			return false;
		}
		if(isChecked()) {
			return getChecked();
		}
		
		boolean successfullyChecked = checkLocal();
		nodeCheckedSetResult(successfullyChecked);
		
		successfullyChecked = getChild(IDENT).check() && successfullyChecked;
		successfullyChecked = getChild(TYPE).check() && successfullyChecked;
		successfullyChecked = getChild(PARAM).check() && successfullyChecked;
		successfullyChecked = getChild(RET).check() && successfullyChecked;
		successfullyChecked = getChild(PATTERN).check() && successfullyChecked;
		successfullyChecked = getChild(NEG).check() && successfullyChecked;
		return successfullyChecked;
	}
	
	protected Collection<GraphNode> getGraphs() {
		Collection<GraphNode> res = new LinkedList<GraphNode>();
		CollectNode negs  = (CollectNode) getChild(NEG);
		res.add((GraphNode) getChild(PATTERN));
		for (BaseNode n : negs.getChildren())
			res.add((GraphNode)n);
		return res;
	}

	protected Collection<GraphNode> getNegativeGraphs() {
		Collection<GraphNode> res = new LinkedList<GraphNode>();
		CollectNode negs  = (CollectNode) getChild(NEG);

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
			error.error(getCoords(), "Actual and formal return-parameter count mismatch (" +
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
					error.error(aReturnAST.getCoords(), "Actual return-parameter is not conformant to formal parameter (" +
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
	protected boolean checkLocal() {
		Checker retDeclarationChecker = new CollectChecker(
			new Checker() {
				public boolean check(BaseNode node, ErrorReporter reporter)
				{
					boolean res = true;

					if ( ! (node instanceof IdentNode) ) {
						//this should never be reached
						node.reportError("Not an identifier");
						return false;
					}
					if ( ((IdentNode)node).getDecl().equals(DeclNode.getInvalid()) ) {
						res = false;
						node.reportError("\"" + node + "\" is undeclared");
					} else {
						BaseNode type = ((IdentNode)node).getDecl().getDeclType();
						res = (type instanceof NodeTypeNode) || (type instanceof EdgeTypeNode);
						if (!res) node.reportError("\"" + node + "\" is neither a node nor an edge type");
					}
					return res;
				}
			}
		);

		Checker negChecker = new CollectChecker(new SimpleChecker(PatternGraphNode.class));
		boolean childs = (new SimpleChecker(PatternGraphNode.class)).check(getChild(PATTERN), error)
			& negChecker.check(getChild(NEG), error)
			& retDeclarationChecker.check(getChild(RET), error);

		//Check if reused names of edges connect the same nodes in the same direction for each usage
		boolean edgeReUse = false;
		if (childs) {
			edgeReUse = true;

			//get the negative graphs and the pattern of this TestDeclNode
			Collection<GraphNode> leftHandGraphs = getNegativeGraphs();
			leftHandGraphs.add((GraphNode)getChild(PATTERN));

			GraphNode[] graphs = (GraphNode[]) leftHandGraphs.toArray(new GraphNode[0]);
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

								NodeCharacter oSrc, oTgt, iSrc, iTgt;
								oSrc = oConn.getSrc();
								oTgt = oConn.getTgt();
								iSrc = iConn.getSrc();
								iTgt = iConn.getTgt();

								assert ! (oSrc instanceof NodeTypeChangeNode):
									"no type changes in test actions";
								assert ! (oTgt instanceof NodeTypeChangeNode):
									"no type changes in test actions";
								assert ! (iSrc instanceof NodeTypeChangeNode):
									"no type changes in test actions";
								assert ! (iTgt instanceof NodeTypeChangeNode):
									"no type changes in test actions";

								//check only if there's no dangling edge
								if ( (iSrc instanceof NodeDeclNode) && ((NodeDeclNode)iSrc).isDummy() ) continue;
								if ( (oSrc instanceof NodeDeclNode) && ((NodeDeclNode)oSrc).isDummy() ) continue;
								if ( (iTgt instanceof NodeDeclNode)	&& ((NodeDeclNode)iTgt).isDummy() ) continue;
								if ( (oTgt instanceof NodeDeclNode) && ((NodeDeclNode)oTgt).isDummy() ) continue;

								if ( iSrc != oSrc || iTgt != oTgt ) {
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
		PatternGraph patternGraph = ma.getPattern();

		// add negative parts to the IR
		for (BaseNode n : getChild(NEG).getChildren()) {
			PatternGraph neg = ((PatternGraphNode)n).getPatternGraph();

			// add Condition elements only mentioned in Condition to the IR
			Set<Node> neededNodes = new LinkedHashSet<Node>();
			Set<Edge> neededEdges = new LinkedHashSet<Edge>();

			for(Expression cond : neg.getConditions()) {
				cond.collectNodesnEdges(neededNodes, neededEdges);
			}
			for(Node neededNode : neededNodes) {
				if(!neg.hasNode(neededNode)) {
					neg.addSingleNode(neededNode);
					neg.addHomToAll(neededNode);
				}
			}
			for(Edge neededEdge : neededEdges) {
				if(!neg.hasEdge(neededEdge)) {
					neg.addSingleEdge(neededEdge);
					neg.addHomToAll(neededEdge);
				}
			}

			ma.addNegGraph(neg);
		}

		// add Params to the IR
		for(BaseNode n : getChild(PARAM).getChildren()) {
			DeclNode param = (DeclNode)n;
			ma.addParameter((Entity) param.checkIR(Entity.class));
			if(param instanceof NodeCharacter) {
				patternGraph.addSingleNode(((NodeCharacter)param).getNode());
			}
			else if (param instanceof EdgeCharacter) {
				Edge e = ((EdgeCharacter)param).getEdge();
				patternGraph.addSingleEdge(e);
			}
			else
				throw new IllegalArgumentException("unknown Class: " + n);
		}

		// add Return-Params to the IR
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

