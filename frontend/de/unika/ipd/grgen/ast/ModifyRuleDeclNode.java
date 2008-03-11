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

import java.util.Collection;
import java.util.HashSet;
import java.util.Vector;

import de.unika.ipd.grgen.ir.Assignment;
import de.unika.ipd.grgen.ir.Edge;
import de.unika.ipd.grgen.ir.Entity;
import de.unika.ipd.grgen.ir.IR;
import de.unika.ipd.grgen.ir.Node;
import de.unika.ipd.grgen.ir.PatternGraph;
import de.unika.ipd.grgen.ir.Rule;


public class ModifyRuleDeclNode extends RuleDeclNode {
	static {
		setName(RuleDeclNode.class, "modify rule declaration");
	}

	private ModifyRhsDeclNode right;

	public ModifyRuleDeclNode(IdentNode id, PatternGraphNode left, ModifyRhsDeclNode right,
			CollectNode<IdentNode> rets, boolean isPattern) {
		super(id, left, right, rets, isPattern);
		this.right = right;
		becomeParent(getRight());
	}

	/** returns children of this node */
	public Collection<BaseNode> getChildren() {
		Vector<BaseNode> children = new Vector<BaseNode>();
		children.add(ident);
		children.add(getValidVersion(typeUnresolved, type));
		children.add(returnFormalParameters);
		children.add(pattern);
		children.add(right);
		return children;
	}

	/** returns names of the children, same order as in getChildren */
	public Collection<String> getChildrenNames() {
		Vector<String> childrenNames = new Vector<String>();
		childrenNames.add("ident");
		childrenNames.add("type");
		childrenNames.add("ret");
		childrenNames.add("pattern");
		childrenNames.add("right");
		return childrenNames;
	}

	/** @see de.unika.ipd.grgen.ast.BaseNode#resolveLocal() */
	protected boolean resolveLocal() {
		type = typeResolver.resolve(typeUnresolved, this);

		return type != null;
	}

	protected boolean checkReturnedElemsNotDeleted(PatternGraphNode left, GraphNode right) {
		assert isResolved();

		boolean res = true;

		Collection<DeclNode> deletedElems = new HashSet<DeclNode>();
		for (ConstraintDeclNode x: this.right.delete.getChildren()) {
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

				if (left.getNodes().contains(retElem) || pattern.getParamDecls().contains(retElem)) {
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



	@Override
	protected boolean checkLocal() {
		right.warnElemAppearsInsideAndOutsideDelete(pattern);
		return super.checkLocal();
	}

	@Override
	protected IR constructIR() {
		PatternGraph left = pattern.getPatternGraph();

		// return if the pattern graph already constructed the IR object
		// that may happens in recursive patterns
		if (isIRAlreadySet()) {
			return getIR();
		}

		PatternGraph right = this.right.graph.getGraph();

		// return if the pattern graph already constructed the IR object
		// that may happens in recursive patterns
		if (isIRAlreadySet()) {
			return getIR();
		}

		Collection<Entity> deleteSet = new HashSet<Entity>();
		for(BaseNode n : this.right.delete.getChildren()) {
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
		constructIRaux(rule, this.right.graph.returns);

		// add Params to the IR
		for(DeclNode decl : pattern.getParamDecls()) {
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
		for(AssignNode n : this.right.eval.getChildren()) {
			rule.addEval((Assignment) n.checkIR(Assignment.class));
		}

		return rule;
	}

	@Override
	protected ModifyRhsDeclNode getRight()
    {
    	return right;
    }
}

