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
import java.util.Iterator;
import java.util.Set;
import java.util.Vector;
import java.util.LinkedList;
import java.util.HashSet;
import java.util.LinkedHashSet;
import de.unika.ipd.grgen.ast.util.Checker;
import de.unika.ipd.grgen.ast.util.CollectChecker;
import de.unika.ipd.grgen.ast.util.SimpleChecker;
import de.unika.ipd.grgen.util.report.ErrorReporter;
import de.unika.ipd.grgen.ir.IR;
import de.unika.ipd.grgen.ir.InheritanceType;
import de.unika.ipd.grgen.ir.Entity;
import de.unika.ipd.grgen.ir.Node;
import de.unika.ipd.grgen.ir.Edge;
import de.unika.ipd.grgen.ir.Expression;
import de.unika.ipd.grgen.ir.PatternGraph;
import de.unika.ipd.grgen.ir.Test;
import de.unika.ipd.grgen.ir.MatchingAction;


/**
 * AST node class representing tests
 */
public class TestDeclNode extends ActionDeclNode {
	static {
		setName(TestDeclNode.class, "test declaration");
	}

	// TODO: check types
	CollectNode<BaseNode> param;
	CollectNode ret;
	PatternGraphNode pattern;
	CollectNode<BaseNode> neg;

	private static final TypeNode testType = new TestTypeNode();

	protected TestDeclNode(IdentNode id, TypeNode type, PatternGraphNode pattern, CollectNode neg, CollectNode params, CollectNode rets) {
		super(id, type);
		this.param = params;
		becomeParent(this.param);
		this.ret = rets;
		becomeParent(this.ret);
		this.pattern = pattern;
		becomeParent(this.pattern);
		this.neg = neg;
		becomeParent(this.neg);
	}

	public TestDeclNode(IdentNode id, PatternGraphNode pattern, CollectNode neg, CollectNode params, CollectNode rets) {
		this(id, testType, pattern, neg, params, rets);
	}

	/** returns children of this node */
	public Collection<BaseNode> getChildren() {
		Vector<BaseNode> children = new Vector<BaseNode>();
		children.add(ident);
		children.add(typeUnresolved);
		children.add(param);
		children.add(ret);
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

	/** @see de.unika.ipd.grgen.ast.BaseNode#resolve() */
	protected boolean resolve() {
		if(isResolved()) {
			return resolutionResult();
		}

		debug.report(NOTE, "resolve in: " + getId() + "(" + getClass() + ")");
		boolean successfullyResolved = true;
		nodeResolvedSetResult(successfullyResolved); // local result

		successfullyResolved = ident.resolve() && successfullyResolved;
		successfullyResolved = typeUnresolved.resolve() && successfullyResolved;
		successfullyResolved = param.resolve() && successfullyResolved;
		successfullyResolved = ret.resolve() && successfullyResolved;
		successfullyResolved = pattern.resolve() && successfullyResolved;
		successfullyResolved = neg.resolve() && successfullyResolved;
		return successfullyResolved;
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
	protected boolean checkReturnParams(CollectNode<BaseNode> typeReturns, CollectNode actualReturns) {
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
			Iterator<BaseNode> itAR = actualReturns.children.iterator();

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
						BaseNode type = ((IdentNode)node).getDecl().getDeclType();
						res = (type instanceof NodeTypeNode) || (type instanceof EdgeTypeNode);
						if (!res) {
							node.reportError("\"" + node + "\" is neither a node nor an edge type");
						}
					}
					return res;
				}
			}
		);

		Checker negChecker = new CollectChecker(new SimpleChecker(PatternGraphNode.class));
		boolean childs = (new SimpleChecker(PatternGraphNode.class)).check(pattern, error)
			& negChecker.check(neg, error)
			& retDeclarationChecker.check(ret, error);

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
			returnParams = checkReturnParams(ret, pattern.returns);
		}

		return childs && edgeReUse && returnParams;
	}


	protected void constructIRaux(MatchingAction ma, BaseNode aReturns) {
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
		for(BaseNode n : aReturns.getChildren()) {
			IdentNode aReturnAST = (IdentNode)n;
			Entity aReturn = (Entity)aReturnAST.getDecl().checkIR(Entity.class);

			// actual return-parameter
			ma.addReturn(aReturn);
		}
	}

	protected IR constructIR() {
		PatternGraph left = pattern.getPatternGraph();
		Test test = new Test(getIdentNode().getIdent(), left);

		constructIRaux(test, ret);

		return test;
	}

	@Override
	public BaseNode getDeclType()
	{
		return typeUnresolved;
	}
}

