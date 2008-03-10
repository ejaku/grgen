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
 * @author Sebastian Buchwald
 * @version $Id: RhsDeclNode.java 18021 2008-03-09 12:13:04Z buchwald $
 */
package de.unika.ipd.grgen.ast;


import java.util.Collection;
import java.util.LinkedHashSet;
import java.util.Set;
import java.util.Vector;

import de.unika.ipd.grgen.ast.util.DeclarationTypeResolver;
import de.unika.ipd.grgen.ir.Assignment;
import de.unika.ipd.grgen.ir.IR;
import de.unika.ipd.grgen.ir.PatternGraph;


/**
 * AST node for a replacement right-hand side.
 */
public class RhsDeclNode extends DeclNode {
	static {
		setName(RhsDeclNode.class, "right-hand side declaration");
	}

	GraphNode graph;
	CollectNode<AssignNode> eval;
	RhsTypeNode type;

	/** Type for this declaration. */
	protected static final TypeNode rhsType = new RhsTypeNode();

	/**
	 * Make a new right-hand side.
	 * @param id The identifier of this RHS.
	 * @param graph The right hand side graph.
	 * @param eval The evaluations.
	 */
	public RhsDeclNode(IdentNode id, GraphNode graph, CollectNode<AssignNode> eval) {
		super(id, rhsType);
		this.graph = graph;
		becomeParent(this.graph);
		this.eval = eval;
		becomeParent(this.eval);
	}

	/** returns children of this node */
	public Collection<BaseNode> getChildren() {
		Vector<BaseNode> children = new Vector<BaseNode>();
		children.add(ident);
		children.add(getValidVersion(typeUnresolved, type));
		children.add(graph);
		children.add(eval);
		return children;
	}

	/** returns names of the children, same order as in getChildren */
	public Collection<String> getChildrenNames() {
		Vector<String> childrenNames = new Vector<String>();
		childrenNames.add("ident");
		childrenNames.add("type");
		childrenNames.add("right");
		childrenNames.add("eval");
		return childrenNames;
	}

	protected static final DeclarationTypeResolver<RhsTypeNode> typeResolver =	new DeclarationTypeResolver<RhsTypeNode>(RhsTypeNode.class);

	/** @see de.unika.ipd.grgen.ast.BaseNode#resolveLocal() */
	protected boolean resolveLocal() {
		type = typeResolver.resolve(typeUnresolved, this);

		return type != null;
	}

	/**
	 * @see de.unika.ipd.grgen.ast.BaseNode#checkLocal()
	 */
	protected boolean checkLocal() {
		return true;
	}

	/**
	 * @see de.unika.ipd.grgen.ast.BaseNode#constructIR()
	 */
	protected IR constructIR() {
		assert false;

		return null;
	}

	protected Collection<Assignment> getAssignments() {
		Collection<Assignment> ret = new LinkedHashSet<Assignment>();

		for (AssignNode n : eval.getChildren()) {
			ret.add((Assignment) n.checkIR(Assignment.class));
		}

		return ret;
	}

	protected PatternGraph getPatternGraph() {
		return graph.getGraph();
	}

	@Override
	public RhsTypeNode getDeclType() {
		assert isResolved();

		return type;
	}

	protected Set<DeclNode> getDelete(PatternGraphNode pattern) {
		Set<DeclNode> res = new LinkedHashSet<DeclNode>();

		for (BaseNode x : pattern.getEdges()) {
			assert (x instanceof DeclNode);
			if ( ! graph.getEdges().contains(x) ) {
				res.add((DeclNode)x);
			}
		}
		for (BaseNode x : pattern.getNodes()) {
			assert (x instanceof DeclNode);
			if ( ! graph.getNodes().contains(x) ) {
				res.add((DeclNode)x);
			}
		}
		for (BaseNode x : pattern.getParamDecls()) {
			assert (x instanceof DeclNode);
			if ( !( graph.getNodes().contains(x) || graph.getEdges().contains(x)) ) {
				res.add((DeclNode)x);
			}
		}

		return res;
	}
}

