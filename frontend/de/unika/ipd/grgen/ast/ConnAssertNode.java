/*
 * GrGen: graph rewrite generator tool -- release GrGen.NET 2.5
 * Copyright (C) 2009 Universitaet Karlsruhe, Institut fuer Programmstrukturen und Datenorganisation, LS Goos
 * licensed under LGPL v3 (see LICENSE.txt included in the packaging of this file)
 */

/**
 * @version $Id$
 */
package de.unika.ipd.grgen.ast;

import java.util.Collection;
import java.util.Vector;

import de.unika.ipd.grgen.ast.util.DeclarationTypeResolver;
import de.unika.ipd.grgen.ir.ConnAssert;
import de.unika.ipd.grgen.ir.IR;
import de.unika.ipd.grgen.ir.NodeType;

/**
 * AST node that represents a Connection Assertion
 * children: SRC:IdentNode, SRCRANGE:RangeSpecNode, TGT:IdentNode, TGTRANGE:RangeSpecNode
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

	/**
	 * Construct a new connection assertion node.
	 */
	public ConnAssertNode(IdentNode src, RangeSpecNode srcRange,
						  IdentNode tgt, RangeSpecNode tgtRange) {
		super(src.getCoords());
		this.srcUnresolved = src;
		becomeParent(this.srcUnresolved);
		this.srcRange = srcRange;
		becomeParent(this.srcRange);
		this.tgtUnresolved = tgt;
		becomeParent(this.tgtUnresolved);
		this.tgtRange = tgtRange;
		becomeParent(this.tgtRange);
	}

	/** returns children of this node */
	@Override
	public Collection<BaseNode> getChildren() {
		Vector<BaseNode> children = new Vector<BaseNode>();
		children.add(getValidVersion(srcUnresolved, src));
		children.add(srcRange);
		children.add(getValidVersion(tgtUnresolved, tgt));
		children.add(tgtRange);
		return children;
	}

	/** returns names of the children, same order as in getChildren */
	@Override
	public Collection<String> getChildrenNames() {
		Vector<String> childrenNames = new Vector<String>();
		childrenNames.add("src");
		childrenNames.add("src range");
		childrenNames.add("tgt");
		childrenNames.add("tgt range");
		return childrenNames;
	}

	private static DeclarationTypeResolver<NodeTypeNode> nodeResolver =	new DeclarationTypeResolver<NodeTypeNode>(NodeTypeNode.class);

	/** @see de.unika.ipd.grgen.ast.BaseNode#resolveLocal() */
	@Override
	protected boolean resolveLocal() {
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
		long srcLower = srcRange.getLower();
		long srcUpper = srcRange.getUpper();
		NodeType srcType = src.checkIR(NodeType.class);

		long tgtLower = tgtRange.getLower();
		long tgtUpper = tgtRange.getUpper();
		NodeType tgtType = tgt.checkIR(NodeType.class);

		return new ConnAssert(srcType, srcLower, srcUpper,
							  tgtType, tgtLower, tgtUpper);
	}
}
