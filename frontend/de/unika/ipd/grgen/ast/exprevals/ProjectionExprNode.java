/*
 * GrGen: graph rewrite generator tool -- release GrGen.NET 4.0
 * Copyright (C) 2003-2013 Universitaet Karlsruhe, Institut fuer Programmstrukturen und Datenorganisation, LS Goos; and free programmers
 * licensed under LGPL v3 (see LICENSE.txt included in the packaging of this file)
 * www.grgen.net
 */

package de.unika.ipd.grgen.ast.exprevals;

import java.util.Collection;
import java.util.Vector;

import de.unika.ipd.grgen.ast.*;
import de.unika.ipd.grgen.ir.IR;
import de.unika.ipd.grgen.ir.exprevals.ComputationInvocationBase;
import de.unika.ipd.grgen.ir.exprevals.ProjectionExpr;
import de.unika.ipd.grgen.parser.Coords;

public class ProjectionExprNode extends ExprNode {
	static {
		setName(ProjectionExprNode.class, "projection expr");
	}

	private int index;
	private ComputationInvocationBaseNode computation;
	
	public ProjectionExprNode(Coords coords, int index) {
		super(coords);

		this.index = index;
	}

	public void setComputation(ComputationInvocationBaseNode computation) {
		this.computation = computation;
		becomeParent(computation);
	}

	public Collection<? extends BaseNode> getChildren() {
		Vector<BaseNode> children = new Vector<BaseNode>();
		return children;
	}

	public Collection<String> getChildrenNames() {
		Vector<String> childrenNames = new Vector<String>();
		return childrenNames;
	}

	@Override
	protected boolean checkLocal() {
		return true;
	}

	@Override
	protected IR constructIR() {
		return new ProjectionExpr(index, 
				computation.checkIR(ComputationInvocationBase.class).getComputationBase(), 
				computation.getType().get(index).getType());
	}

	@Override
	public TypeNode getType() {
		return computation.getType().get(index);
	}
}
