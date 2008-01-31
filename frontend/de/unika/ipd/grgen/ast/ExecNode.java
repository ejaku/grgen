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



import de.unika.ipd.grgen.ast.BaseNode;
import de.unika.ipd.grgen.ast.util.DeclarationResolver;
import de.unika.ipd.grgen.ir.Exec;
import de.unika.ipd.grgen.ir.GraphEntity;
import de.unika.ipd.grgen.ir.IR;
import de.unika.ipd.grgen.parser.Coords;
import java.awt.Color;
import java.util.Collection;
import java.util.LinkedHashSet;
import java.util.Set;
import java.util.Vector;

/**
 *
 */
public class ExecNode extends BaseNode {
	static {
		setName(ExecNode.class, "exec");
	}

	private StringBuilder sb = new StringBuilder();

	private Vector<BaseNode> childrenUnresolved = new Vector<BaseNode>();
	private Vector<ConstraintDeclNode> children = new Vector<ConstraintDeclNode>();

	public ExecNode(Coords coords) {
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

	private static DeclarationResolver<ConstraintDeclNode> resolver = new DeclarationResolver<ConstraintDeclNode>(ConstraintDeclNode.class);

	/** @see de.unika.ipd.grgen.ast.BaseNode#resolveLocal() */
	protected boolean resolveLocal() {
		boolean successfullyResolved = true;
		for(BaseNode uc : childrenUnresolved) {
			final ConstraintDeclNode resolved = resolver.resolve(uc, this);
			if (resolved != null)
				children.add(resolved);
			else
				successfullyResolved = false;
		}
		return successfullyResolved;
	}

	protected boolean checkLocal() {
		return true;
	}

	public Color getNodeColor() {
		return Color.PINK;
	}

	protected IR constructIR() {
		Set<GraphEntity> parameters = new LinkedHashSet<GraphEntity>();
		for(BaseNode child : getChildren())
			parameters.add((GraphEntity) child.getIR());
		Exec res= new Exec(getXGRSString(), parameters);
		return res;
	}
}



