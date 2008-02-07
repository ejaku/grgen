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
import de.unika.ipd.grgen.ast.util.DeclarationTypeResolver;
import de.unika.ipd.grgen.util.report.ErrorReporter;


/**
 * AST node class representing tests
 */
public class TestDeclNode extends ActionDeclNode {
	static {
		setName(TestDeclNode.class, "test declaration");
	}

	CollectNode<ConstraintDeclNode> param;
	CollectNode<IdentNode> returnFormalParameters;
	PatternGraphNode pattern;
	CollectNode<PatternGraphNode> neg;
	TestTypeNode type;

	private static final TypeNode testType = new TestTypeNode();

	protected TestDeclNode(IdentNode id, TypeNode type, PatternGraphNode pattern, CollectNode<PatternGraphNode> neg, CollectNode<ConstraintDeclNode> params, CollectNode<IdentNode> rets) {
		super(id, type);
		this.param = params;
		becomeParent(this.param);
		this.returnFormalParameters = rets;
		becomeParent(this.returnFormalParameters);
		this.pattern = pattern;
		becomeParent(this.pattern);
		this.neg = neg;
		becomeParent(this.neg);
	}

	public TestDeclNode(IdentNode id, PatternGraphNode pattern, CollectNode<PatternGraphNode> neg, CollectNode<ConstraintDeclNode> params, CollectNode<IdentNode> rets) {
		this(id, testType, pattern, neg, params, rets);
	}

	/** returns children of this node */
	public Collection<BaseNode> getChildren() {
		Vector<BaseNode> children = new Vector<BaseNode>();
		children.add(ident);
		children.add(getValidVersion(typeUnresolved, type));
		children.add(param);
		children.add(returnFormalParameters);
		children.add(pattern);
		children.add(neg);
		return children;
	}

	/** returns names of the children, same order as in getChildren */
	public Collection<String> getChildrenNames() {
		Vector<String> childrenNames = new Vector<String>();
		childrenNames.add("ident");
		childrenNames.add("type");
		childrenNames.add("param");
		childrenNames.add("ret");
		childrenNames.add("pattern");
		childrenNames.add("neg");
		return childrenNames;
	}

	/** @see de.unika.ipd.grgen.ast.BaseNode#resolveLocal() */
	protected boolean resolveLocal() {
		DeclarationTypeResolver<TestTypeNode> typeResolver =
			new DeclarationTypeResolver<TestTypeNode>(TestTypeNode.class);
		type = typeResolver.resolve(typeUnresolved, this);
		return type != null;
	}

	protected Collection<GraphNode> getGraphs() {
		Collection<GraphNode> res = new LinkedList<GraphNode>();
		res.add(pattern);
		for (BaseNode n : neg.getChildren()) {
			res.add((GraphNode)n);
		}
		return res;
	}

	protected Collection<GraphNode> getNegativeGraphs() {
		Collection<GraphNode> res = new LinkedList<GraphNode>();
		for (BaseNode n : neg.getChildren()) {
			res.add((GraphNode)n);
		}
		return res;
	}

	/**
	 * check if actual return entities are conformant
	 * to the formal return parameters.
	 */
	// TODO: check types
	protected boolean checkReturnParams(CollectNode<IdentNode> typeReturns, CollectNode<IdentNode> actualReturns) {
		boolean returnTypes = true;

		/*
		 System.out.println("\n*** this          = " + this.getClass());
		 System.out.println("    this          = " + this.getChildren());
		 System.out.println("*** typeReturns   = "   + typeReturns);
		 System.out.println("    typeReturns   = "   + typeReturns.getChildren());
		 System.out.println("*** actualReturns = " + actualReturns);
		 System.out.println("    actualReturns = " + actualReturns.getChildren());
		 */

		if(actualReturns.children.size() != typeReturns.children.size()) {
			error.error(getCoords(), "Actual and formal return-parameter count mismatch (" +
							actualReturns.children.size() + " vs. " + typeReturns.children.size() +")");
			returnTypes = false;
		} else {
			Iterator<IdentNode> itAR = actualReturns.children.iterator();

			for(BaseNode n : typeReturns.getChildren()) {
				IdentNode       tReturnAST  = (IdentNode)n;
				InheritanceType tReturn     = (InheritanceType)tReturnAST.getDecl().getDeclType().checkIR(InheritanceType.class);

				IdentNode       aReturnAST  = itAR.next();
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
				public boolean check(BaseNode node, ErrorReporter reporter) {
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
						TypeNode type = ((IdentNode)node).getDecl().getDeclType();
						res = (type instanceof NodeTypeNode) || (type instanceof DirectedEdgeTypeNode);
						if (!res) {
							node.reportError("\"" + node + "\" is neither a node nor an edge type");
						}
					}
					return res;
				}
			}
		);

		boolean childs = retDeclarationChecker.check(returnFormalParameters, error);

		//Check if reused names of edges connect the same nodes in the same direction for each usage
		boolean edgeReUse = false;
		if (childs) {
			edgeReUse = true;

			//get the negative graphs and the pattern of this TestDeclNode
			Collection<GraphNode> leftHandGraphs = getNegativeGraphs();
			leftHandGraphs.add(pattern);

			GraphNode[] graphs = leftHandGraphs.toArray(new GraphNode[0]);
			Collection<EdgeCharacter> alreadyReported = new HashSet<EdgeCharacter>();

			for (int i=0; i<graphs.length; i++) {
				for (int o=i+1; o<graphs.length; o++) {
					for (BaseNode iBN : graphs[i].getConnections()) {
						ConnectionCharacter iConn = (ConnectionCharacter)iBN;
						if (! (iConn instanceof ConnectionNode)) {
							continue;
						}

						for (BaseNode oBN : graphs[o].getConnections()) {
							ConnectionCharacter oConn = (ConnectionCharacter)oBN;
							if (! (oConn instanceof ConnectionNode)) {
								continue;
							}

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
								if ( (iSrc instanceof NodeDeclNode) && ((NodeDeclNode)iSrc).isDummy() ) {
									continue;
								}
								if ( (oSrc instanceof NodeDeclNode) && ((NodeDeclNode)oSrc).isDummy() ) {
									continue;
								}
								if ( (iTgt instanceof NodeDeclNode)	&& ((NodeDeclNode)iTgt).isDummy() ) {
									continue;
								}
								if ( (oTgt instanceof NodeDeclNode) && ((NodeDeclNode)oTgt).isDummy() ) {
									continue;
								}

								if ( iSrc != oSrc || iTgt != oTgt ) {
									alreadyReported.add(iConn.getEdge());
									((ConnectionNode) oConn).reportError("Reused edge does not connect the same nodes");
									edgeReUse = false;
								}
							}
						}
					}
				}
			}
		}

		boolean returnParams = true;
		if(! (this instanceof RuleDeclNode)) {
			returnParams = checkReturnParams(returnFormalParameters, pattern.returns);
		}

		return childs && edgeReUse && returnParams;
	}


	protected void constructIRaux(MatchingAction ma, CollectNode<IdentNode> aReturns) {
		PatternGraph patternGraph = ma.getPattern();

		// add negative parts to the IR
		for (BaseNode n : neg.getChildren()) {
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
					neg.addSingleEdge(neededEdge);	// TODO: maybe we loose context here
					neg.addHomToAll(neededEdge);
				}
			}
			ma.addNegGraph(neg);
		}

		// add Params to the IR
		for(BaseNode n : param.getChildren()) {
			DeclNode param = (DeclNode)n;
			ma.addParameter((Entity) param.checkIR(Entity.class));
			if(param instanceof NodeCharacter) {
				patternGraph.addSingleNode(((NodeCharacter)param).getNode());
			} else if (param instanceof EdgeCharacter) {
				Edge e = ((EdgeCharacter)param).getEdge();
				patternGraph.addSingleEdge(e);
			} else {
				throw new IllegalArgumentException("unknown Class: " + n);
			}
		}

		// add Return-Params to the IR
		for(IdentNode aReturnAST : aReturns.getChildren()) {
			Entity aReturn = (Entity)aReturnAST.getDecl().checkIR(Entity.class);
			// actual return-parameter
			ma.addReturn(aReturn);
		}
	}

	protected IR constructIR() {
		PatternGraph left = pattern.getPatternGraph();
		Test test = new Test(getIdentNode().getIdent(), left);

		constructIRaux(test, pattern.returns);

		return test;
	}

	@Override
		public TypeNode getDeclType() {
		assert isResolved();
		
		return type;
	}
}


