/*
 * GrGen: graph rewrite generator tool -- release GrGen.NET 4.0
 * Copyright (C) 2003-2013 Universitaet Karlsruhe, Institut fuer Programmstrukturen und Datenorganisation, LS Goos; and free programmers
 * licensed under LGPL v3 (see LICENSE.txt included in the packaging of this file)
 * www.grgen.net
 */

/**
 * @author Edgar Jakumeit
 */

package de.unika.ipd.grgen.ast.exprevals;

import java.util.Vector;

import de.unika.ipd.grgen.ast.*;
import de.unika.ipd.grgen.parser.Coords;

public abstract class ProcedureInvocationBaseNode extends EvalStatementNode
{
	static {
		setName(ProcedureInvocationBaseNode.class, "procedure invocation base");
	}
	
	private static final Vector<TypeNode> emptyReturn = new Vector<TypeNode>();

	public ProcedureInvocationBaseNode(Coords coords)
	{
		super(coords);
	}

	/** @see de.unika.ipd.grgen.ast.BaseNode#resolveLocal() */
	@Override
	protected boolean resolveLocal() {
		boolean res = true;
		for(TypeNode typeNode : getType()) {
			res &= typeNode.resolve();
		}
		return res;
	}

	// default is a procedure without returns, overwrite if return is not empty
	public Vector<TypeNode> getType() {
		return emptyReturn;
	}
}
