/*
 * GrGen: graph rewrite generator tool -- release GrGen.NET 3.0
 * Copyright (C) 2003-2011 Universitaet Karlsruhe, Institut fuer Programmstrukturen und Datenorganisation, LS Goos
 * licensed under LGPL v3 (see LICENSE.txt included in the packaging of this file)
 * www.grgen.net
 */

/**
 * @version $Id$
 */
package de.unika.ipd.grgen.ast;

import java.util.Collection;
import java.util.Vector;

import de.unika.ipd.grgen.ast.util.DeclarationTypeResolver;
import de.unika.ipd.grgen.parser.Coords;
import de.unika.ipd.grgen.ir.ConnAssert;
import de.unika.ipd.grgen.ir.IR;
import de.unika.ipd.grgen.ir.NodeType;

/**
 * AST node that represents a Connection Assertion
 * children: SRC:IdentNode, SRCRANGE:RangeSpecNode, TGT:IdentNode, TGTRANGE:RangeSpecNode
 * or
 * AST node that represents a "meta" Connection Assertion which tells to
 * inherit the connection assertions from the parent edges;
 * after resolving it gets replaced by the connection assertions of the parent nodes.
 */
public class ConnAssertNode extends BaseNode {
	static {
		setName(ConnAssertNode.class, "conn assert");
	}

	private NodeTypeNode src;
	private BaseNode srcUnresolved;
	private RangeSpecNode srcRange;
	private NodeTypeNode tgt;
	private BaseNode tgtUnresolved;
	private RangeSpecNode tgtRange;
	private boolean bothDirections;
	
	boolean copyExtends;
	
	/**
	 * Construct a new connection assertion node.
	 */
	public ConnAssertNode(IdentNode src, RangeSpecNode srcRange,
						  IdentNode tgt, RangeSpecNode tgtRange,
						  boolean bothDirections) {
		super(src.getCoords());
		this.srcUnresolved = src;
		becomeParent(this.srcUnresolved);
		this.srcRange = srcRange;
		becomeParent(this.srcRange);
		this.tgtUnresolved = tgt;
		becomeParent(this.tgtUnresolved);
		this.tgtRange = tgtRange;
		becomeParent(this.tgtRange);
		this.bothDirections = bothDirections;
		this.copyExtends = false;
	}

	/**
	 * Construct a new copy extends = inherit connection assertions from the parent connection assertion node.
	 */
	public ConnAssertNode(Coords coords) {
		super(coords);
		this.copyExtends = true;
	}

	/** returns children of this node */
	@Override
	public Collection<BaseNode> getChildren() {
		Vector<BaseNode> children = new Vector<BaseNode>();
		if(!copyExtends) {
			children.add(getValidVersion(srcUnresolved, src));
			children.add(srcRange);
			children.add(getValidVersion(tgtUnresolved, tgt));
			children.add(tgtRange);
		}
		return children;
	}

	/** returns names of the children, same order as in getChildren */
	@Override
	public Collection<String> getChildrenNames() {
		Vector<String> childrenNames = new Vector<String>();
		if(!copyExtends) {
			childrenNames.add("src");
			childrenNames.add("src range");
			childrenNames.add("tgt");
			childrenNames.add("tgt range");
		}
		return childrenNames;
	}

	private static DeclarationTypeResolver<NodeTypeNode> nodeResolver =	new DeclarationTypeResolver<NodeTypeNode>(NodeTypeNode.class);

	/** @see de.unika.ipd.grgen.ast.BaseNode#resolveLocal() */
	@Override
	protected boolean resolveLocal() {
		if(copyExtends) return true;
		
		src = nodeResolver.resolve(srcUnresolved, this);
		tgt = nodeResolver.resolve(tgtUnresolved, this);

		return src != null && tgt != null;
	}

	/**
	 * Check, if the AST node is correctly built.
	 * @see de.unika.ipd.grgen.ast.BaseNode#checkLocal()
	 */
	@Override
	protected boolean checkLocal() {
		return true;
	}

	@Override
	protected IR constructIR() {
		assert !copyExtends; // must have been replaced by copies of the connection assertions of the parents befor entering this phase
		
		long srcLower = srcRange.getLower();
		long srcUpper = srcRange.getUpper();
		NodeType srcType = src.checkIR(NodeType.class);

		long tgtLower = tgtRange.getLower();
		long tgtUpper = tgtRange.getUpper();
		NodeType tgtType = tgt.checkIR(NodeType.class);

		return new ConnAssert(srcType, srcLower, srcUpper,
							  tgtType, tgtLower, tgtUpper,
							  bothDirections);
	}
}
