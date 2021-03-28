/*
 * GrGen: graph rewrite generator tool -- release GrGen.NET 6.0
 * Copyright (C) 2003-2021 Universitaet Karlsruhe, Institut fuer Programmstrukturen und Datenorganisation, LS Goos; and free programmers
 * licensed under LGPL v3 (see LICENSE.txt included in the packaging of this file)
 * www.grgen.net
 */

package de.unika.ipd.grgen.ast.expr.graph;

import java.util.Collection;
import java.util.Vector;

import de.unika.ipd.grgen.ast.*;
import de.unika.ipd.grgen.ast.expr.ExprNode;
import de.unika.ipd.grgen.ast.model.type.EdgeTypeNode;
import de.unika.ipd.grgen.ast.model.type.NodeTypeNode;
import de.unika.ipd.grgen.ast.type.TypeNode;
import de.unika.ipd.grgen.ast.type.basic.BasicTypeNode;
import de.unika.ipd.grgen.ast.type.basic.UntypedExecVarTypeNode;
import de.unika.ipd.grgen.ir.IR;
import de.unika.ipd.grgen.ir.expr.Expression;
import de.unika.ipd.grgen.ir.expr.graph.Visited;
import de.unika.ipd.grgen.parser.Coords;

public class VisitedNode extends ExprNode
{
	static {
		setName(VisitedNode.class, "visited");
	}

	private ExprNode visitorIDExpr;
	private ExprNode entityExpr;

	public VisitedNode(Coords coords, ExprNode visitorIDExpr, ExprNode entityExpr)
	{
		super(coords);

		this.visitorIDExpr = visitorIDExpr;
		becomeParent(visitorIDExpr);

		this.entityExpr = entityExpr;
		becomeParent(entityExpr);
	}

	@Override
	public Collection<? extends BaseNode> getChildren()
	{
		Vector<BaseNode> children = new Vector<BaseNode>();
		children.add(visitorIDExpr);
		children.add(entityExpr);
		return children;
	}

	@Override
	public Collection<String> getChildrenNames()
	{
		Vector<String> childrenNames = new Vector<String>();
		childrenNames.add("visitorID");
		childrenNames.add("entity");
		return childrenNames;
	}

	@Override
	protected boolean resolveLocal()
	{
		return true;
	}

	@Override
	protected boolean checkLocal()
	{
		if(visitorIDExpr.getType() instanceof UntypedExecVarTypeNode) {
			return true;
		}
		if(!visitorIDExpr.getType().isEqual(BasicTypeNode.intType)) {
			visitorIDExpr.reportError("Visitor ID expression must be of type int");
			return false;
		}
		if(entityExpr.getType() instanceof UntypedExecVarTypeNode) {
			return true;
		}
		if(entityExpr.getType() instanceof EdgeTypeNode) {
			return true;
		}
		if(entityExpr.getType() instanceof NodeTypeNode) {
			return true;
		}
		reportError("visited entity expr must be of node or edge type");
		return true;
	}

	@Override
	protected IR constructIR()
	{
		visitorIDExpr = visitorIDExpr.evaluate();
		entityExpr = entityExpr.evaluate();
		return new Visited(visitorIDExpr.checkIR(Expression.class), entityExpr.checkIR(Expression.class));
	}

	@Override
	public TypeNode getType()
	{
		return BasicTypeNode.booleanType;
	}
}
