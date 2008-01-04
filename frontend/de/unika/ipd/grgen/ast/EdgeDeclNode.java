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
 * @author Sebastian Hack
 * @version $Id$
 */
package de.unika.ipd.grgen.ast;

import de.unika.ipd.grgen.ast.util.Checker;
import de.unika.ipd.grgen.ast.util.DeclarationPairResolver;
import de.unika.ipd.grgen.ast.util.Pair;
import de.unika.ipd.grgen.ast.util.SimpleChecker;
import de.unika.ipd.grgen.ast.util.TypeChecker;
import de.unika.ipd.grgen.ir.Edge;
import de.unika.ipd.grgen.ir.EdgeType;
import de.unika.ipd.grgen.ir.IR;
import java.awt.Color;
import java.util.Collection;
import java.util.Vector;

public class EdgeDeclNode extends ConstraintDeclNode implements EdgeCharacter
{
	static {
		setName(EdgeDeclNode.class, "edge declaration");
	}
	
	protected EdgeDeclNode typeEdgeDecl = null;
	protected TypeDeclNode typeTypeDecl = null;
	
	protected static final DeclarationPairResolver<EdgeDeclNode,TypeDeclNode> typeResolver =
			new DeclarationPairResolver<EdgeDeclNode,TypeDeclNode>(EdgeDeclNode.class, TypeDeclNode.class);
	
	public EdgeDeclNode(IdentNode n, BaseNode e, TypeExprNode constraints) {
		super(n, e, constraints);
		setName("edge");
	}
	
	public EdgeDeclNode(IdentNode n, BaseNode e) {
		this(n, e, TypeExprNode.getEmpty());
	}
	
	/**
	 * Create EdgeDeclNode and immediately resolve and check it.
	 * NOTE: Use this to create and insert an EdgeDeclNode into the AST after
	 * the AST is already checked.
	 * TODO Change type of type iff CollectNode support generics 
	 */
	public EdgeDeclNode(IdentNode n, BaseNode type, boolean resolvedAndChecked) {
		this(n, type, TypeExprNode.getEmpty());
		assert(resolvedAndChecked);
		resolve();
		check();
	}
	
	/** The TYPE child could be an edge in case the type is
	 *  inherited dynamically via the typeof operator */
	public BaseNode getDeclType() {
		DeclNode curr = getValidResolvedVersion(typeEdgeDecl, typeTypeDecl);
		return curr.getDeclType();
	}
	
	/** returns children of this node */
	public Collection<BaseNode> getChildren() {
		Vector<BaseNode> children = new Vector<BaseNode>();
		children.add(ident);
		children.add(getValidVersion(typeUnresolved, typeEdgeDecl, typeTypeDecl));
		children.add(constraints);
		return children;
	}
	
	/** returns names of the children, same order as in getChildren */
	public Collection<String> getChildrenNames() {
		Vector<String> childrenNames = new Vector<String>();
		childrenNames.add("ident"); 
		childrenNames.add("type");
		childrenNames.add("constraints");
		return childrenNames;
	}
	
	/** @see de.unika.ipd.grgen.ast.BaseNode#resolve() */
	protected boolean resolve() {
		if(isResolved()) {
			return resolutionResult();
		}
		
		debug.report(NOTE, "resolve in: " + getId() + "(" + getClass() + ")");
		boolean successfullyResolved = true;
		Pair<EdgeDeclNode, TypeDeclNode> resolved = typeResolver.resolve(typeUnresolved, this);
		successfullyResolved = (resolved.fst != null || resolved.snd != null) && successfullyResolved;
		typeEdgeDecl = resolved.fst;
		typeTypeDecl = resolved.snd;
		nodeResolvedSetResult(successfullyResolved); // local result
		if(!successfullyResolved) {
			debug.report(NOTE, "resolve error");
		}
		
		successfullyResolved = ident.resolve() && successfullyResolved;
		if(typeEdgeDecl != null){
			successfullyResolved = typeEdgeDecl.resolve() && successfullyResolved;
		}
		if(typeTypeDecl != null){
			successfullyResolved = typeTypeDecl.resolve() && successfullyResolved;
		}
		successfullyResolved = constraints.resolve() && successfullyResolved;
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
		
		boolean childrenChecked = true;
		if(!visitedDuringCheck()) {
			setCheckVisited();
			
			childrenChecked = ident.check() && childrenChecked;
			childrenChecked = getValidResolvedVersion(typeEdgeDecl, typeTypeDecl).check() && childrenChecked;
			childrenChecked = constraints.check() && childrenChecked;
		}
		
		boolean locallyChecked = checkLocal();
		nodeCheckedSetResult(locallyChecked);
		
		return childrenChecked && locallyChecked;
	}
	
	protected boolean checkLocal() {
		Checker typeChecker = new TypeChecker(EdgeTypeNode.class);
		return super.checkLocal()
			&& (new SimpleChecker(IdentNode.class)).check(ident, error)
			&& typeChecker.check(getValidResolvedVersion(typeEdgeDecl, typeTypeDecl), error);
	}
	
	/**
	 * Edges have more info to give
	 * @see de.unika.ipd.grgen.util.GraphDumpableNode#getNodeInfo()
	 */
	protected String extraNodeInfo() {
		return "";
	}
	
	/**
	 * @see de.unika.ipd.grgen.util.GraphDumpable#getNodeColor()
	 */
	public Color getNodeColor() {
		return Color.YELLOW;
	}
	
	/**
	 * Get the IR object correctly casted.
	 * @return The edge IR object.
	 */
	public Edge getEdge() {
		return (Edge) checkIR(Edge.class);
	}
	
	protected boolean inheritsType() {
		return typeEdgeDecl != null;
	}
	
	/** @see de.unika.ipd.grgen.ast.BaseNode#constructIR() */
	protected IR constructIR() {
		// This must be ok after checking all nodes.
		TypeNode tn = (TypeNode) getDeclType();
		EdgeType et = (EdgeType) tn.checkIR(EdgeType.class);
		IdentNode ident = getIdentNode();
		
		Edge edge = new Edge(ident.getIdent(), et, ident.getAttributes());
		edge.setConstraints(getConstraints());
		
		if(inheritsType()) {
			edge.setTypeof((Edge)typeEdgeDecl.checkIR(Edge.class));
		}
		
		return edge;
	}

	public static String getKindStr() {
		return "edge declaration";
	}

	public static String getUseStr() {
		return "edge";
	}
}

