/*
 * GrGen: graph rewrite generator tool -- release GrGen.NET 8.1
 * Copyright (C) 2003-2026 Universitaet Karlsruhe, Institut fuer Programmstrukturen und Datenorganisation, LS Goos; and free programmers
 * licensed under LGPL v3, some components/parts use different licenses (see LICENSE.txt included in the packaging of this file)
 * www.grgen.de / www.grgen.net
 */

package de.unika.ipd.grgen.ast.stmt.graph;

import java.util.Collection;
import java.util.List;
import java.util.ArrayList;

import de.unika.ipd.grgen.ast.*;
import de.unika.ipd.grgen.ast.decl.DeclNode;
import de.unika.ipd.grgen.ast.expr.ExprNode;
import de.unika.ipd.grgen.ast.model.type.NodeTypeNode;
import de.unika.ipd.grgen.ast.stmt.BuiltinProcedureInvocationBaseNode;
import de.unika.ipd.grgen.ast.stmt.EvalStatementNode;
import de.unika.ipd.grgen.ast.type.TypeNode;
import de.unika.ipd.grgen.ir.IR;
import de.unika.ipd.grgen.ir.expr.Expression;
import de.unika.ipd.grgen.ir.stmt.graph.GraphAddCopyNodeProc;
import de.unika.ipd.grgen.parser.Coords;

/**
 * A node for adding a copy of a node to graph.
 */
public class GraphAddCopyNodeProcNode extends BuiltinProcedureInvocationBaseNode
{
	static {
		setClassName(GraphAddCopyNodeProcNode.class, "graph add copy node procedure");
	}

	private ExprNode oldNode;

	List<TypeNode> returnTypes;

	private boolean deep;

	public GraphAddCopyNodeProcNode(Coords coords, ExprNode nodeType, boolean deep)
	{
		super(coords);
		this.oldNode = nodeType;
		becomeParent(this.oldNode);
		this.deep = deep;
	}

	/** returns children of this node */
	@Override
	public Collection<BaseNode> getChildren()
	{
		List<BaseNode> children = new ArrayList<BaseNode>();
		children.add(oldNode);
		return children;
	}

	/** returns names of the children, same order as in getChildren */
	@Override
	public Collection<String> getChildrenNames()
	{
		List<String> childrenNames = new ArrayList<String>();
		childrenNames.add("old node");
		return childrenNames;
	}

	/** @see de.unika.ipd.grgen.ast.BaseNode#checkLocal() */
	@Override
	protected boolean checkLocal()
	{
		TypeNode oldNodeType = oldNode.getType();
		if(!(oldNodeType instanceof NodeTypeNode)) {
			reportError("The addCopy procedure expects as argument (oldNode)"
					+ " a value of type Node"
					+ " (but is given a value of type " + oldNodeType.toStringWithDeclarationCoords() + ").");
			return false;
		}
		return true;
	}

	@Override
	public boolean checkStatementLocal(boolean isLHS, DeclNode root, EvalStatementNode enclosingLoop)
	{
		return true;
	}

	@Override
	protected IR constructIR()
	{
		oldNode = oldNode.evaluate();
		GraphAddCopyNodeProc addCopyNode = new GraphAddCopyNodeProc(oldNode.checkIR(Expression.class),
				oldNode.getType().getIRType(), deep);
		return addCopyNode;
	}

	@Override
	public List<TypeNode> getType()
	{
		if(returnTypes == null) {
			returnTypes = new ArrayList<TypeNode>();
			returnTypes.add(oldNode.getType());
		}
		return returnTypes;
	}
}
