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

	NodeTypeNode src;
	BaseNode srcUnresolved;
	RangeSpecNode srcRange;
	NodeTypeNode tgt;
	BaseNode tgtUnresolved;
	RangeSpecNode tgtRange;

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
	public Collection<BaseNode> getChildren() {
		Vector<BaseNode> children = new Vector<BaseNode>();
		children.add(getValidVersion(srcUnresolved, src));
		children.add(srcRange);
		children.add(getValidVersion(tgtUnresolved, tgt));
		children.add(tgtRange);
		return children;
	}

	/** returns names of the children, same order as in getChildren */
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
	protected boolean resolveLocal() {
		src = nodeResolver.resolve(srcUnresolved, this);
		tgt = nodeResolver.resolve(tgtUnresolved, this);

		return src != null && tgt != null;
	}

	/**
	 * Check, if the AST node is correctly built.
	 * @see de.unika.ipd.grgen.ast.BaseNode#checkLocal()
	 */
	protected boolean checkLocal() {
		return true;
	}

	protected IR constructIR() {
		long srcLower = srcRange.getLower();
		long srcUpper = srcRange.getUpper();
		NodeType srcType = (NodeType)src.getIR();

		long tgtLower = tgtRange.getLower();
		long tgtUpper = tgtRange.getUpper();
		NodeType tgtType = (NodeType)tgt.getIR();

		return new ConnAssert(srcType, srcLower, srcUpper,
							  tgtType, tgtLower, tgtUpper);
	}
}
