/*
 * GrGen: graph rewrite generator tool -- release GrGen.NET 3.6
 * Copyright (C) 2003-2013 Universitaet Karlsruhe, Institut fuer Programmstrukturen und Datenorganisation, LS Goos; and free programmers
 * licensed under LGPL v3 (see LICENSE.txt included in the packaging of this file)
 * www.grgen.net
 */

/**
 * @author Moritz Kroll
 */

package de.unika.ipd.grgen.ast.exprevals;

import java.util.Collection;
import java.util.Vector;

import de.unika.ipd.grgen.ast.*;
import de.unika.ipd.grgen.ir.Constant;
import de.unika.ipd.grgen.ir.IR;

/**
 * An identifier expression.
 */
public class IdentExprNode extends DeclExprNode {
	static {
		setName(IdentExprNode.class, "ident expression");
	}

	public boolean yieldedTo = false;
	
	public IdentExprNode(IdentNode ident) {
		super(ident);
	}

	public IdentExprNode(IdentNode ident, boolean yieldedTo) {
		super(ident);
		this.yieldedTo = yieldedTo; 
	}

	public void setYieldedTo() {
		yieldedTo = true;
	}

	@Override
	protected boolean resolveLocal() {
		decl = ((DeclaredCharacter) declUnresolved).getDecl();
		if(decl instanceof TypeDeclNode)
			return true;

		return super.resolveLocal();
	}

	public IdentNode getIdent() {
		return (IdentNode) declUnresolved;
	}

	@Override
	public Collection<String> getChildrenNames() {
		Vector<String> childrenNames = new Vector<String>();
		childrenNames.add("ident");
		return childrenNames;
	}

	@Override
	protected IR constructIR() {
		BaseNode declNode = (BaseNode) decl;
		if(declNode instanceof TypeDeclNode)
			return new Constant(BasicTypeNode.typeType.getType(),
					((TypeDeclNode) decl).getDeclType().getIR());
		else
			return super.constructIR();
	}
}
