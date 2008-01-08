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
import de.unika.ipd.grgen.ast.util.DeclTypeResolver;
import de.unika.ipd.grgen.ast.util.Resolver;
import de.unika.ipd.grgen.ast.util.SimpleChecker;
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

	BaseNode src;
	RangeSpecNode srcRange;
	BaseNode tgt;
	RangeSpecNode tgtRange;

	/**
	 * Construct a new connection assertion node.
	 */
	public ConnAssertNode(IdentNode src, RangeSpecNode srcRange,
						  IdentNode tgt, RangeSpecNode tgtRange) {
		super(src.getCoords());
		this.src = src;
		becomeParent(this.src);
		this.srcRange = srcRange;
		becomeParent(this.srcRange);
		this.tgt = tgt;
		becomeParent(this.tgt);
		this.tgtRange = tgtRange;
		becomeParent(this.tgtRange);
	}

	/** returns children of this node */
	public Collection<BaseNode> getChildren() {
		Vector<BaseNode> children = new Vector<BaseNode>();
		children.add(src);
		children.add(srcRange);
		children.add(tgt);
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

	/** @see de.unika.ipd.grgen.ast.BaseNode#resolve() */
	protected boolean resolve() {
		if(isResolved()) {
			return resolutionResult();
		}

		debug.report(NOTE, "resolve in: " + getId() + "(" + getClass() + ")");
		boolean successfullyResolved = true;
		Resolver nodeResolver = new DeclTypeResolver(NodeTypeNode.class);
		BaseNode resolved = nodeResolver.resolve(src);
		successfullyResolved = resolved!=null && successfullyResolved;
		src = ownedResolutionResult(src, resolved);
		resolved = nodeResolver.resolve(tgt);
		successfullyResolved = resolved!=null && successfullyResolved;
		tgt = ownedResolutionResult(tgt, resolved);
		nodeResolvedSetResult(successfullyResolved); // local result
		if(!successfullyResolved) {
			debug.report(NOTE, "resolve error");
		}

		successfullyResolved = src.resolve() && successfullyResolved;
		successfullyResolved = srcRange.resolve() && successfullyResolved;
		successfullyResolved = tgt.resolve() && successfullyResolved;
		successfullyResolved = tgtRange.resolve() && successfullyResolved;
		return successfullyResolved;
	}

	/**
	 * Check, if the AST node is correctly built.
	 * @see de.unika.ipd.grgen.ast.BaseNode#checkLocal()
	 */
	protected boolean checkLocal() {
		return (new SimpleChecker(NodeTypeNode.class)).check(src, error)
			& (new SimpleChecker(RangeSpecNode.class)).check(srcRange, error)
			& (new SimpleChecker(NodeTypeNode.class)).check(tgt, error)
			& (new SimpleChecker(RangeSpecNode.class)).check(tgtRange, error);
	}

	protected IR constructIR() {
		// TODO
		int srcLower = srcRange.getLower();
		int srcUpper = srcRange.getUpper();
		NodeType srcType = (NodeType)src.getIR();

		int tgtLower = tgtRange.getLower();
		int tgtUpper = tgtRange.getUpper();
		NodeType tgtType = (NodeType)tgt.getIR();

		return new ConnAssert(srcType, srcLower, srcUpper,
							  tgtType, tgtLower, tgtUpper);
	}
}
