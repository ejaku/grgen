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
 * @author Rubino Geiss
 * @version $Id: ExactNode.java 17220 2008-01-08 16:23:55Z rubino $
 */
package de.unika.ipd.grgen.ast;



import de.unika.ipd.grgen.parser.Coords;
import java.awt.Color;
import java.util.Collection;
import java.util.Vector;

/**
 *
 */
public class XGRSNode extends BaseNode {
	static {
		setName(XGRSNode.class, "xgrs");
	}

	private StringBuilder sb = new StringBuilder();

	private Vector<IdentNode> children = new Vector<IdentNode>();

	private Vector<BaseNode> childrenUnresolved = new Vector<BaseNode>();

	public XGRSNode(Coords coords) {
		super(coords);
	}

	public void append(Object n) {
		assert(!isResolved());
		sb.append(n);
	}

	public String getXGRSString() {
		return sb.toString();
	}

	public void addParameter(BaseNode n) {
		assert(!isResolved());
		becomeParent(n);
		childrenUnresolved.add(n);
	}

	/** returns children of this node */
	public Collection<BaseNode> getChildren() {
		return getValidVersionVector(childrenUnresolved, children);
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
		/*
		 boolean successfullyResolved = true;
		 DeclarationResolver<NodeDeclNode> resolver = new DeclarationResolver<NodeDeclNode>(NodeDeclNode.class);
		 for(int i=0; i<childrenUnresolved.size(); ++i) {
		 children.add(resolver.resolve(childrenUnresolved.get(i), this));
		 successfullyResolved = children.get(i)!=null && successfullyResolved;
		 }
		 nodeResolvedSetResult(successfullyResolved); // local result
		 if(!successfullyResolved) {
		 debug.report(NOTE, "resolve error");
		 }

		 for(int i=0; i<children.size(); ++i) {
		 successfullyResolved = (children.get(i)!=null ? children.get(i).resolve() : false) && successfullyResolved;
		 }*/

		for(int i=0; i<children.size(); ++i)
			successfullyResolved = (childrenUnresolved.get(i)!=null ? childrenUnresolved.get(i).resolve() : false) && successfullyResolved;

		return successfullyResolved;
	}

	protected boolean checkLocal() {
		return true;
	}

	public Color getNodeColor() {
		return Color.PINK;
	}
}
