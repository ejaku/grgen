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



import de.unika.ipd.grgen.ast.util.DeclTypeResolver;
import de.unika.ipd.grgen.ast.util.Resolver;
import de.unika.ipd.grgen.ir.ConnAssert;
import de.unika.ipd.grgen.ir.IR;
import de.unika.ipd.grgen.ir.NodeType;

/**
 * AST node that represents a Connection Assertion
 * children: SRC:IdentNode, SRCRANGE:RangeSpecNode, TGT:IdentNode, TGTRANGE:RangeSpecNode
 */
public class ConnAssertNode extends BaseNode
{
	static {
		setName(ConnAssertNode.class, "conn assert");
	}
	
	/** names for the children. */
	private static final String[] childrenNames = {
		"src", "src range", "tgt", "tgt range"
	};
	
	/** Index of the source node. */
	private static final int SRC = 0;
	
	/** Index of the source node range. */
	private static final int SRCRANGE = 1;
	
	/** Index of the target node. */
	private static final int TGT = 2;
	
	/** Index of the target node range. */
	private static final int TGTRANGE = 3;
	
	
	/** Resolver for the nodes. */
	private static final Resolver nodeResolver =
		new DeclTypeResolver(NodeTypeNode.class);
	
	/**
	 * Construct a new connection assertion node.
	 */
	public ConnAssertNode(BaseNode src, BaseNode srcRange,
						  BaseNode tgt, BaseNode tgtRange) {
		super(src.getCoords());
		addChild(src);
		addChild(srcRange);
		addChild(tgt);
		addChild(tgtRange);
		setChildrenNames(childrenNames);
	}
	
	/** @see de.unika.ipd.grgen.ast.BaseNode#doResolve() */
	protected boolean doResolve() {
		if(isResolved()) {
			return getResolve();
		}
		
		debug.report(NOTE, "resolve in: " + getId() + "(" + getClass() + ")");
		boolean successfullyResolved = true;
		successfullyResolved = nodeResolver.resolve(this, SRC) && successfullyResolved;
		successfullyResolved = nodeResolver.resolve(this, TGT) && successfullyResolved;
		setResolved(successfullyResolved); // local result
		if(!successfullyResolved) {
			debug.report(NOTE, "resolve error");
		}
		
		successfullyResolved = getChild(SRC).doResolve() && successfullyResolved;
		successfullyResolved = getChild(SRCRANGE).doResolve() && successfullyResolved;
		successfullyResolved = getChild(TGT).doResolve() && successfullyResolved;
		successfullyResolved = getChild(TGTRANGE).doResolve() && successfullyResolved;
		return successfullyResolved;
	}
	
	/** @see de.unika.ipd.grgen.ast.BaseNode#doCheck() */
	protected boolean doCheck() {
		assert(isResolved());
		if(!resolveResult) {
			return false;
		}
		if(isChecked()) {
			return getChecked();
		}
		
		boolean successfullyChecked = getCheck();
		if(successfullyChecked) {
			successfullyChecked = getTypeCheck();
		}
		successfullyChecked = getChild(SRC).doCheck() && successfullyChecked;
		successfullyChecked = getChild(SRCRANGE).doCheck() && successfullyChecked;
		successfullyChecked = getChild(TGT).doCheck() && successfullyChecked;
		successfullyChecked = getChild(TGTRANGE).doCheck() && successfullyChecked;
		return successfullyChecked;
	}

	/**
	 * Check, if the AST node is correctly built.
	 * @see de.unika.ipd.grgen.ast.BaseNode#check()
	 */
	protected boolean check() {
		return checkChild(SRC, 		NodeTypeNode.class)
			&& checkChild(SRCRANGE,	RangeSpecNode.class)
			&& checkChild(TGT,		NodeTypeNode.class)
			&& checkChild(TGTRANGE,	RangeSpecNode.class);
	}
	
	protected IR constructIR() {
		// TODO
		RangeSpecNode srcRange = (RangeSpecNode)getChild(SRCRANGE);
		int srcLower = srcRange.getLower();
		int srcUpper = srcRange.getUpper();
		NodeType srcType = (NodeType)getChild(SRC).getIR();
		
		RangeSpecNode tgtRange = (RangeSpecNode)getChild(TGTRANGE);
		int tgtLower = tgtRange.getLower();
		int tgtUpper = tgtRange.getUpper();
		NodeType tgtType = (NodeType)getChild(TGT).getIR();
		
		return new ConnAssert(srcType, srcLower, srcUpper,
							  tgtType, tgtLower, tgtUpper);
	}
}
