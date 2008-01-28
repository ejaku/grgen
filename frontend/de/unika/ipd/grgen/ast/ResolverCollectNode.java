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
 * @file CollectNode.java
 * @author shack
 * @date Jul 21, 2003
 * @version $Id: CollectNode.java 17542 2008-01-26 02:26:59Z rubino $
 */
package de.unika.ipd.grgen.ast;

import java.awt.Color;
import java.util.Collection;
import java.util.Vector;

import de.unika.ipd.grgen.ast.util.GenResolver;

/**
 * An AST node that represents a collection of other nodes.
 * children: *:BaseNode
 *
 * Normally AST nodes contain a fixed number of children,
 * which are accessed by their fixed index within the children vector.
 * This node collects a statically unknown number of children AST nodes,
 * originating in unbounded list constructs in the parsing syntax.
 */
public class ResolverCollectNode<S extends BaseNode, T extends S> extends BaseNode {
	static {
		setName(ResolverCollectNode.class, "collect");
	}

	Vector<T> children = new Vector<T>();
	Vector<S> childrenUnresolved = new Vector<S>();

	public void addChild(S n) {
		becomeParent(n);
		childrenUnresolved.add(n);
	}

	/** returns children of this node */
	public Collection<? extends S> getChildren() {
		return getValidVersionVector();
	}
	
	/** returns children of this node */
	public Collection<T> getResolvedChildren() {
		return children;
	}

	/** returns names of the children, same order as in getChildren */
	public Collection<String> getChildrenNames() {
		Vector<String> childrenNames = new Vector<String>();
		// nameless children
		return childrenNames;
	}

	/** @see de.unika.ipd.grgen.ast.BaseNode#resolve() */
	protected boolean resolve() {
		if(isResolved()) {
			return resolutionResult();
		}

		debug.report(NOTE, "resolve in: " + getId() + "(" + getClass() + ")");
		boolean successfullyResolved = true;
		// local resolution done via call to resolveChildren from parent node
		nodeResolvedSetResult(successfullyResolved); // local result

		for(int i=0; i<children.size(); ++i) {
			successfullyResolved = children.get(i).resolve() && successfullyResolved;
		}
		return successfullyResolved;
	}

	protected boolean resolveChildren(GenResolver<T> resolver) {
		debug.report(NOTE, "resolve children in: " + getId() + "(" + getClass() + ")");
		boolean successfullyResolved = true;
		for(int i=0; i<childrenUnresolved.size(); ++i) {
			T resolved = resolver.resolve(childrenUnresolved.get(i), this);
			successfullyResolved = resolved!=null && successfullyResolved;
			
			// don't know if Vector support null values
			if (resolved != null) {
				children.add(resolved);
			}
		}
		return successfullyResolved;
	}

	/** @see de.unika.ipd.grgen.ast.BaseNode#checkLocal() */
	protected boolean checkLocal() {
		return true;
	}

	public Color getNodeColor() {
		return Color.GRAY;
	}
	
	public String toString() {
		Vector<? extends S> tmp = getValidVersionVector();
		return tmp.toString();
	}
	
	/**
	 * Return new vector containing elements of the currently valid member vector.
	 * Currently valid depends on vector was already resolved.
	 * 
	 * NOTE: {@link BaseNode#getValidVersionVector(Vector, Vector)} don't work
	 *       with Java 1.5.
	 */
	private  Vector<? extends S> getValidVersionVector() {
		if(isResolved()){
			return children;
		}
		return childrenUnresolved;
	}
}
