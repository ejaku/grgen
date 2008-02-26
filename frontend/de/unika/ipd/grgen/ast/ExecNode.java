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



import java.awt.Color;
import java.util.Collection;
import java.util.LinkedHashSet;
import java.util.Set;
import java.util.Vector;

import de.unika.ipd.grgen.ir.Exec;
import de.unika.ipd.grgen.ir.GraphEntity;
import de.unika.ipd.grgen.ir.IR;
import de.unika.ipd.grgen.parser.Coords;

/**
 *
 */
public class ExecNode extends BaseNode {
	static {
		setName(ExecNode.class, "exec");
	}

	private StringBuilder sb = new StringBuilder();

	private CollectNode<CallActionNode> callActions = new CollectNode<CallActionNode>();

	public ExecNode(Coords coords) {
		super(coords);
		becomeParent(callActions);
	}

	public void append(Object n) {
		assert(!isResolved());
		sb.append(n);
	}

	public String getXGRSString() {
		return sb.toString();
	}

	public void addCallAction(CallActionNode n) {
		assert(!isResolved());
		becomeParent(n);
		callActions.addChild(n);
	}

	/** returns children of this node */
	public Collection<? extends BaseNode> getChildren() {
		Vector<BaseNode> res = new Vector<BaseNode>();
		res.add(callActions);
		return res;
	}

	/** returns names of the children, same order as in getChildren */
	public Collection<String> getChildrenNames() {
		Vector<String> childrenNames = new Vector<String>();
		childrenNames.add("actions");
		return childrenNames;
	}

	/** @see de.unika.ipd.grgen.ast.BaseNode#resolveLocal() */
	protected boolean resolveLocal() {
		return true;
	}

	protected boolean checkLocal() {
		return true;
	}

	public Color getNodeColor() {
		return Color.PINK;
	}

	protected IR constructIR() {
		Set<GraphEntity> parameters = new LinkedHashSet<GraphEntity>();
		for(CallActionNode callActionNode : callActions.getChildren())
			for(DeclNode param : callActionNode.getParams().getChildren())
			parameters.add((GraphEntity) param.getIR());
		Exec res= new Exec(getXGRSString(), parameters);
		return res;
	}
}



