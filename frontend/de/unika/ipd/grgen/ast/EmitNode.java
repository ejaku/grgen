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
 * @version $Id$
 */
package de.unika.ipd.grgen.ast;


import de.unika.ipd.grgen.ir.Emit;
import de.unika.ipd.grgen.ir.IR;
import de.unika.ipd.grgen.parser.Coords;
import java.awt.Color;
import java.util.ArrayList;
import java.util.Collection;
import java.util.List;
import java.util.Vector;

/**
 *
 */
public class EmitNode extends BaseNode {
	static {
		setName(EmitNode.class, "emit");
	}

	private Vector<BaseNode> childrenUnresolved = new Vector<BaseNode>();

	public EmitNode(Coords coords) {
		super(coords);
	}

	public void addChild(BaseNode n) {
		assert(!isResolved());
		becomeParent(n);
		childrenUnresolved.add(n);
	}

	/** returns children of this node */
	public Collection<BaseNode> getChildren() {
		return childrenUnresolved;
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
		nodeResolvedSetResult(successfullyResolved); // local result

		for(int i=0; i<childrenUnresolved.size(); ++i) {
			successfullyResolved = (childrenUnresolved.get(i)!=null ? childrenUnresolved.get(i).resolve() : false) && successfullyResolved;
		}

		return successfullyResolved;
	}

	protected boolean checkLocal() {
		if (childrenUnresolved.isEmpty()) {
			this.reportError("Emit statement is empty");
			return false;
		}

		return true;
	}

	public Color getNodeColor() {
		return Color.PINK;
	}

	protected IR constructIR() {
		List<IR> arguments = new ArrayList<IR>();
		for(BaseNode child : getChildren())
			arguments.add(child.getIR());
		Emit res= new Emit(arguments);
		return res;
	}
}
