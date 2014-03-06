/*
 * GrGen: graph rewrite generator tool -- release GrGen.NET 4.3
 * Copyright (C) 2003-2014 Universitaet Karlsruhe, Institut fuer Programmstrukturen und Datenorganisation, LS Goos; and free programmers
 * licensed under LGPL v3 (see LICENSE.txt included in the packaging of this file)
 * www.grgen.net
 */

/**
 * @author Edgar Jakumeit
 */

package de.unika.ipd.grgen.ast.exprevals;

import java.util.Collection;
import java.util.Vector;

import de.unika.ipd.grgen.ast.*;
import de.unika.ipd.grgen.ir.exprevals.EvalStatement;
import de.unika.ipd.grgen.ir.IR;
import de.unika.ipd.grgen.ir.exprevals.MultiStatement;
import de.unika.ipd.grgen.parser.Coords;

/**
 * AST node representing a multi statement.
 * Just a container for statements invisible to the user, esp. it does _not_ open a block, 
 * used to break a return assignment to declarations into a series of declarations and a return assignment.
 */
public class MultiStatementNode extends EvalStatementNode {
	static {
		setName(MultiStatementNode.class, "MultiStatement");
	}

	CollectNode<EvalStatementNode> statements = new CollectNode<EvalStatementNode>();

	public MultiStatementNode() {
		super(Coords.getInvalid());
		becomeParent(this.statements);
	}

	public void addStatement(EvalStatementNode statement) {
		statements.addChild(statement);
	}
	
	/** returns children of this node */
	@Override
	public Collection<BaseNode> getChildren() {
		Vector<BaseNode> children = new Vector<BaseNode>();
		children.add(statements);
		return children;
	}

	/** returns names of the children, same order as in getChildren */
	@Override
	public Collection<String> getChildrenNames() {
		Vector<String> childrenNames = new Vector<String>();
		childrenNames.add("statements");
		return childrenNames;
	}

	@Override
	protected boolean checkLocal() {
		return true;
	}

	@Override
	protected boolean resolveLocal() {
		return true;
	}

	public boolean checkStatementLocal(boolean isLHS, DeclNode root, EvalStatementNode enclosingLoop) {
		return true;
	}

	@Override
	protected IR constructIR() {
		MultiStatement ms = new MultiStatement();
		for(EvalStatementNode statement : statements.children) 	
			ms.addStatement(statement.checkIR(EvalStatement.class));
		return ms;
	}
}
