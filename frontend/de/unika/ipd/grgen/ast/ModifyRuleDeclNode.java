/*
 GrGen: graph rewrite generator tool.
 Copyright (C) 2007  IPD Goos, Universit"at Karlsruhe, Germany

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
 * @version $Id$
 */
package de.unika.ipd.grgen.ast;

import de.unika.ipd.grgen.ir.*;

import de.unika.ipd.grgen.ast.util.CollectPairResolver;
import de.unika.ipd.grgen.ast.util.DeclarationPairResolver;

import java.util.Collection;
import java.util.HashSet;
import java.util.Set;
import java.util.Vector;


public class ModifyRuleDeclNode extends RuleDeclNode {
	static {
		setName(RuleDeclNode.class, "modify rule declaration");
	}
	CollectNode<IdentNode> deleteUnresolved;
	CollectNode<ConstraintDeclNode> delete;
	RuleTypeNode type;


	public ModifyRuleDeclNode(IdentNode id, PatternGraphNode left, GraphNode right, CollectNode<AssignNode> eval,
			CollectNode<BaseNode> params, CollectNode<IdentNode> rets, CollectNode<IdentNode> dels) {
		super(id, left, right, eval, params, rets);
		this.deleteUnresolved = dels;
		becomeParent(this.deleteUnresolved);
	}

	/** returns children of this node */
	public Collection<BaseNode> getChildren() {
		Vector<BaseNode> children = new Vector<BaseNode>();
		children.add(ident);
		children.add(getValidVersion(typeUnresolved, type));
		children.add(param);
		children.add(returnFormalParameters);
		children.add(pattern);
		children.add(right);
		children.add(eval);
		children.add(getValidVersion(deleteUnresolved, delete));
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
		childrenNames.add("right");
		childrenNames.add("eval");
		childrenNames.add("delete");
		return childrenNames;
	}
	
	private static final CollectPairResolver<ConstraintDeclNode> deleteResolver = new CollectPairResolver<ConstraintDeclNode>(
		new DeclarationPairResolver<NodeDeclNode, EdgeDeclNode>(NodeDeclNode.class, EdgeDeclNode.class));

	/** @see de.unika.ipd.grgen.ast.BaseNode#resolveLocal() */
	protected boolean resolveLocal() {
		delete = deleteResolver.resolve(deleteUnresolved);
		type = typeResolver.resolve(typeUnresolved, this);
		
		return delete != null && type != null;
	}

	protected Set<DeclNode> getDelete() {
		assert isResolved();

		Set<DeclNode> res = new HashSet<DeclNode>();

		for (ConstraintDeclNode x : delete.getChildren()) {
			res.add(x);
		}
		return res;
	}

	protected boolean checkReturnedElemsNotDeleted(PatternGraphNode left, GraphNode right) {
		assert isResolved();

		boolean res = true;

		Collection<DeclNode> deletedElems = new HashSet<DeclNode>();
		for (ConstraintDeclNode x: delete.getChildren()) {
			deletedElems.add(x);
		}

		for (BaseNode x : right.returns.getChildren()) {
			IdentNode ident = (IdentNode) x;
			DeclNode retElem = ident.getDecl();

			if (((retElem instanceof NodeDeclNode) || (retElem instanceof EdgeDeclNode))
				&& deletedElems.contains(retElem)) {
				res = false;

				String nodeOrEdge = "";
				if (retElem instanceof NodeDeclNode) {
					nodeOrEdge = "node";
				} else if (retElem instanceof NodeDeclNode) {
					nodeOrEdge = "edge";
				} else {
					nodeOrEdge = "element";
				}

				if (left.getNodes().contains(retElem) || getParamDecls().contains(retElem)) {
					ident.reportError("The deleted " + nodeOrEdge + " \"" + ident + "\" must not be returned");
				} else {
					assert false: "the " + nodeOrEdge + " \"" + ident + "\", that is" +
						"neither a parameter, nor contained in LHS, nor in " +
						"RHS, occurs in a return";
				}
			}
		}
		return res;
	}

	protected boolean checkRhsReuse(PatternGraphNode left, GraphNode right) {
		boolean res = true;
		Collection<EdgeDeclNode> alreadyReported = new HashSet<EdgeDeclNode>();
		for (BaseNode rc : right.getConnections()) {
			if (!(rc instanceof ConnectionNode)) {
				continue;
			}
			boolean occursInLHS = false;
			ConnectionNode rConn = (ConnectionNode) rc;
			EdgeDeclNode re = (EdgeDeclNode) rConn.getEdge();
			
			if (re instanceof EdgeTypeChangeNode) {
				re = (EdgeDeclNode) ((EdgeTypeChangeNode)re).getOldEdge();
			}
			
			for (BaseNode lc : left.getConnections()) {
				if (!(lc instanceof ConnectionNode)) {
					continue;
				}

				ConnectionNode lConn = (ConnectionNode) lc;

				EdgeDeclNode le = (EdgeDeclNode) lConn.getEdge();

				if ( ! le.equals(re) ) {
					continue;
				}
				occursInLHS = true;

				if (lConn.getConnectionKind() != rConn.getConnectionKind()) {
					res = false;
					rConn.reportError("Reused edge does not have the same connection kind");
					// if you don't add to alreadyReported erroneous errors can occur,
					// e.g. lhs=x-e->y, rhs=y-e-x
					alreadyReported.add(re);
				}

				NodeDeclNode lSrc = lConn.getSrc();
				NodeDeclNode lTgt = lConn.getTgt();
				NodeDeclNode rSrc = rConn.getSrc();
				NodeDeclNode rTgt = rConn.getTgt();

				Collection<BaseNode> rhsNodes = right.getNodes();

				if (rSrc instanceof NodeTypeChangeNode) {
					rSrc = (NodeDeclNode) ((NodeTypeChangeNode)rSrc).getOldNode();
					rhsNodes.add(rSrc);
				}
				if (rTgt instanceof NodeTypeChangeNode) {
					rTgt = (NodeDeclNode) ((NodeTypeChangeNode)rTgt).getOldNode();
					rhsNodes.add(rTgt);
				}

				//check, whether reuse of nodes and edges is consistent with the LHS
				if ( rSrc.isDummy() ) {
					rConn.setSrc(lSrc);
				} else if ( ! rSrc.equals(lSrc) ) {
					res = false;
					rConn.reportError("Reused edge \"" + le + "\" does not connect the same nodes");
					alreadyReported.add(re);
				}

				if ( rTgt.isDummy() ) {
					rConn.setTgt(lTgt);
				} else if ( ! rTgt.equals(lTgt) ) {
					res = false;
					rConn.reportError("Reused edge \"" + le + "\" does not connect the same nodes");
					alreadyReported.add(re);
				}

				//check, whether RHS "adds" a node to a dangling end of a edge
				if ( ! alreadyReported.contains(re) ) {
					if ( lSrc.isDummy() && ! rSrc.isDummy() ) {
						res = false;
						rConn.reportError("Reused edge dangles on LHS, but has a source node on RHS");
						alreadyReported.add(re);
					}
					if ( lTgt.isDummy() && ! rTgt.isDummy() ) {
						res = false;
						rConn.reportError("Reused edge dangles on LHS, but has a target node on RHS");
						alreadyReported.add(re);
					}
				}
			}
			if (!occursInLHS) {
				// No error for ?--? needed, since ArbitraryEdgeType is abstract
				// alreadyReported can not be set here
				if (rConn.getConnectionKind() == ConnectionNode.ARBITRARY_DIRECTED) {
					res = false;
					rConn.reportError("New instances of <--> are not allowed in RHS");
				}
			}
		}
		return res;
	}

	private void warnElemAppearsInsideAndOutsideDelete() {
		Set<DeclNode> deletes = getDelete();
		GraphNode right = this.right;

		Set<BaseNode> alreadyReported = new HashSet<BaseNode>();
		for (BaseNode x : right.getConnections()) {
			BaseNode elem = BaseNode.getErrorNode();
			if (x instanceof SingleNodeConnNode) {
				elem = ((SingleNodeConnNode)x).getNode();
			} else if (x instanceof ConnectionNode) {
				elem = (BaseNode) ((ConnectionNode)x).getEdge();
			}

			if (alreadyReported.contains(elem)) {
				continue;
			}

			for (BaseNode y : deletes) {
				if (elem.equals(y)) {
					x.reportWarning("\"" + y + "\" appears inside as well as outside a delete statement");
					alreadyReported.add(elem);
				}
			}
		}
	}

	@Override
		protected boolean checkLocal() {
		warnElemAppearsInsideAndOutsideDelete();
		return super.checkLocal();
	}

	@Override
		protected IR constructIR() {
		PatternGraph left = pattern.getPatternGraph();
		Graph right = this.right.getGraph();

		Collection<Entity> deleteSet = new HashSet<Entity>();
		for(BaseNode n : delete.getChildren()) {
			deleteSet.add((Entity)n.checkIR(Entity.class));
		}

		for(Node n : left.getNodes()) {
			if(!deleteSet.contains(n)) {
				right.addSingleNode(n);
			}
		}
		for(Edge e : left.getEdges()) {
			if(!deleteSet.contains(e)
			   && !deleteSet.contains(left.getSource(e))
			   && !deleteSet.contains(left.getTarget(e))) {
				right.addConnection(left.getSource(e), e, left.getTarget(e));
			}
		}

		Rule rule = new Rule(getIdentNode().getIdent(), left, right);

		constructImplicitNegs(left);
		constructIRaux(rule, this.right.returns);

		// add Params to the IR
		for(DeclNode decl : getParamDecls()) {
			if(!deleteSet.contains(decl.getIR())) {
				if(decl instanceof NodeCharacter) {
					right.addSingleNode(((NodeCharacter)decl).getNode());
				} else if (decl instanceof EdgeCharacter) {
					Edge e = ((EdgeCharacter)decl).getEdge();
					if(!deleteSet.contains(e)
					   && !deleteSet.contains(left.getSource(e))
					   && !deleteSet.contains(left.getTarget(e))) {
						right.addSingleEdge(e); //TODO
						//right.addConnection(left.getSource(e),e, left.getTarget((e)));
					}
				} else {
					throw new IllegalArgumentException("unknown Class: " + decl);
				}
			}
		}

		// add Eval statements to the IR
		for(AssignNode n : eval.getChildren()) {
			rule.addEval((Assignment) n.checkIR(Assignment.class));
		}

		return rule;
	}

	@Override
		public RuleTypeNode getDeclType() {
		assert isResolved();
		
		return type;
	}
}

