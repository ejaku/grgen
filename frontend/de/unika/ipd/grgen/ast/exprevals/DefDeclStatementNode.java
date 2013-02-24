/*
 * GrGen: graph rewrite generator tool -- release GrGen.NET 3.6
 * Copyright (C) 2003-2013 Universitaet Karlsruhe, Institut fuer Programmstrukturen und Datenorganisation, LS Goos; and free programmers
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
import de.unika.ipd.grgen.ir.exprevals.DefDeclGraphEntityStatement;
import de.unika.ipd.grgen.ir.exprevals.DefDeclVarStatement;
import de.unika.ipd.grgen.ir.GraphEntity;
import de.unika.ipd.grgen.ir.IR;
import de.unika.ipd.grgen.ir.Variable;
import de.unika.ipd.grgen.parser.Coords;

/**
 * AST node representing a def declaration statement node (a variable that can be assigned in an attribute evaluation statements).
 */
public class DefDeclStatementNode extends EvalStatementNode {
	static {
		setName(DefDeclStatementNode.class, "def decl statement");
	}

	BaseNode defDeclUnresolved;
	int context;
	
	VarDeclNode defDeclVar;
	ConstraintDeclNode defDeclGraphElement;

	public DefDeclStatementNode(Coords coords, BaseNode target, int context) {
		super(coords);
		this.defDeclUnresolved = target;
		becomeParent(this.defDeclUnresolved);
		this.context = context;
	}
	
	/** returns children of this node */
	@Override
	public Collection<BaseNode> getChildren() {
		Vector<BaseNode> children = new Vector<BaseNode>();
		children.add(getValidVersion(defDeclUnresolved, defDeclVar, defDeclGraphElement));
		return children;
	}

	/** returns names of the children, same order as in getChildren */
	@Override
	public Collection<String> getChildrenNames() {
		Vector<String> childrenNames = new Vector<String>();
		childrenNames.add("defDecl");
		return childrenNames;
	}

	/** @see de.unika.ipd.grgen.ast.BaseNode#resolveLocal() */
	@Override
	protected boolean resolveLocal() {
		boolean successfullyResolved = true;
		
		if(defDeclUnresolved instanceof VarDeclNode) {
			defDeclVar = (VarDeclNode)defDeclUnresolved;
		} else if(defDeclUnresolved instanceof SingleNodeConnNode) {
			SingleNodeConnNode sncn = (SingleNodeConnNode)defDeclUnresolved;
			defDeclGraphElement = (NodeDeclNode)sncn.nodeUnresolved;
		} else if(defDeclUnresolved instanceof ConstraintDeclNode) {
			defDeclGraphElement = (ConstraintDeclNode)defDeclUnresolved;
		} else {
			ConnectionNode cn = (ConnectionNode)defDeclUnresolved;
			defDeclGraphElement = ((EdgeDeclNode)cn.edgeUnresolved);
		}
		
		return successfullyResolved;
	}

	@Override
	protected boolean checkLocal() {
		return true;
	}

	@Override
	protected IR constructIR() {
		// potential initialization is attached to the Var or the GraphEntity
		if(defDeclVar!=null) {
			Variable var = defDeclVar.checkIR(Variable.class);	
			return new DefDeclVarStatement(var);
		} else {
			GraphEntity graphEntity = defDeclGraphElement.checkIR(GraphEntity.class);
			return new DefDeclGraphEntityStatement(graphEntity);
		}
	}
}
