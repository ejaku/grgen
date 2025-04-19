/*
 * GrGen: graph rewrite generator tool -- release GrGen.NET 7.2
 * Copyright (C) 2003-2025 Universitaet Karlsruhe, Institut fuer Programmstrukturen und Datenorganisation, LS Goos; and free programmers
 * licensed under LGPL v3 (see LICENSE.txt included in the packaging of this file)
 * www.grgen.net
 */

package de.unika.ipd.grgen.ast.expr.graph;

import de.unika.ipd.grgen.ast.*;
import de.unika.ipd.grgen.ast.expr.BuiltinFunctionInvocationBaseNode;
import de.unika.ipd.grgen.ast.model.decl.IndexDeclNode;
import de.unika.ipd.grgen.ast.type.TypeNode;
import de.unika.ipd.grgen.ast.util.DeclarationResolver;
import de.unika.ipd.grgen.ir.IR;
import de.unika.ipd.grgen.parser.Coords;

/**
 * A node yielding the graph elements (nodes or edges) from an index (base class without constraints, the constrained ones inherit from this one).
 */
public abstract class FromIndexAccessExprNode extends BuiltinFunctionInvocationBaseNode
{
	static {
		setName(FromIndexAccessExprNode.class, "from index access expr");
	}

	protected BaseNode indexUnresolved;
	protected IndexDeclNode index;

	protected FromIndexAccessExprNode(Coords coords, BaseNode index)
	{
		super(coords);
		this.indexUnresolved = index;
		becomeParent(this.indexUnresolved);
	}

	private static DeclarationResolver<IndexDeclNode> indexResolver =
			new DeclarationResolver<IndexDeclNode>(IndexDeclNode.class);

	/** @see de.unika.ipd.grgen.ast.BaseNode#resolveLocal() */
	protected boolean resolveLocal()
	{
		//boolean fixupWorked = fixupDefinition(indexUnresolved, indexUnresolved.getScope()); -- could be needed when used in a method in the model before the index declaration
		index = indexResolver.resolve(indexUnresolved, this);
		if(index == null) {
			int indexArgumentNumber = 1 + indexShift();
			reportError("The function " + shortSignature() + " expects as " + indexArgumentNumber + ". argument (index) a declared index (given is " + indexUnresolved.toStringWithDeclarationCoords() + ").");
		}
		return index != null;
	}

	/** @see de.unika.ipd.grgen.ast.BaseNode#checkLocal() */
	protected boolean checkLocal()
	{
		//note the early exit in checkLocal of FromIndexAccessMultipleFromToExprNode if the same index check fails (silently), when the parts FromIndexAccessFromToPartExprNode inheriting from this class are inspected 
		boolean res = true;
		TypeNode expectedEntityType = getRoot().getDecl().getDeclType();
		TypeNode entityType = index.getType();
		if(!entityType.isCompatibleTo(expectedEntityType)) {
			int indexArgumentNumber = 1 + indexShift();
			reportError("The function " + shortSignature() + " expects as " + indexArgumentNumber + ". argument (index) a value of type index on " + expectedEntityType.toStringWithDeclarationCoords()
					+ " (but is given a value of type index on " + entityType.toStringWithDeclarationCoords() + ").");
			return false;
		}	
		return res;
	}

	protected int indexShift() // the isIn(Nodes|Edges)FromIndex methods start with the candidate to be checked, shifting the regular parameter numbers by one
	{
		return 0;
	}

	protected abstract IdentNode getRoot();

	protected abstract String shortSignature();

	protected abstract IR constructIR();
}
