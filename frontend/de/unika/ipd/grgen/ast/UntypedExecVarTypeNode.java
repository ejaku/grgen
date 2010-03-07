/*
 * GrGen: graph rewrite generator tool -- release GrGen.NET 2.6
 * Copyright (C) 2003-2010 Universitaet Karlsruhe, Institut fuer Programmstrukturen und Datenorganisation, LS Goos
 * licensed under LGPL v3 (see LICENSE.txt included in the packaging of this file)
 * www.grgen.net
 */

/**
 * @author Edgar Jakumeit
 * @version $Id: UntypedExecVarTypeNode.java 26740 2010-01-02 11:21:07Z eja $
 */
package de.unika.ipd.grgen.ast;

import de.unika.ipd.grgen.ir.IR;
import de.unika.ipd.grgen.ir.ObjectType;
import de.unika.ipd.grgen.ir.UntypedExecVarType;

import java.util.Collection;
import java.util.Vector;

public class UntypedExecVarTypeNode extends DeclaredTypeNode
{
	static {
		setName(UntypedExecVarTypeNode.class, "untyped exec variable type");
	}

	/** returns children of this node */
	@Override
	public Collection<BaseNode> getChildren() {
		Vector<BaseNode> children = new Vector<BaseNode>();
		// no children
		return children;
	}

	/** returns names of the children, same order as in getChildren */
	@Override
	public Collection<String> getChildrenNames() {
		Vector<String> childrenNames = new Vector<String>();
		// no children
		return childrenNames;
	}
	
	@Override
	protected boolean isCompatibleTo(TypeNode t) {
		// compatible to everything
		return true;
	}

	@Override
	protected boolean isCastableTo(TypeNode t) {
		return isCompatibleTo(t);
	}

	@Override
	protected IR constructIR() {
		// TODO: Check whether this is OK
		return new UntypedExecVarType(getIdentNode().getIdent());
	}

	@Override
	public String toString() {
		return "untyped";
	}
}
