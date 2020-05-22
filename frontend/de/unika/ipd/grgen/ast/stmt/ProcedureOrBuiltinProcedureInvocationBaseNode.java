/*
 * GrGen: graph rewrite generator tool -- release GrGen.NET 5.0
 * Copyright (C) 2003-2020 Universitaet Karlsruhe, Institut fuer Programmstrukturen und Datenorganisation, LS Goos; and free programmers
 * licensed under LGPL v3 (see LICENSE.txt included in the packaging of this file)
 * www.grgen.net
 */

/**
 * @author Edgar Jakumeit
 */

package de.unika.ipd.grgen.ast.stmt;

import java.util.Vector;

import de.unika.ipd.grgen.ast.type.TypeNode;
import de.unika.ipd.grgen.parser.Coords;

/** base class for builtin and real procedures calls */
public abstract class ProcedureOrBuiltinProcedureInvocationBaseNode extends EvalStatementNode
{
	static {
		setName(ProcedureOrBuiltinProcedureInvocationBaseNode.class, "procedure or builtin procedure invocation base");
	}

	private static final Vector<TypeNode> emptyReturn = new Vector<TypeNode>();

	public ProcedureOrBuiltinProcedureInvocationBaseNode(Coords coords)
	{
		super(coords);
	}

	/** @see de.unika.ipd.grgen.ast.BaseNode#resolveLocal() */
	@Override
	protected boolean resolveLocal()
	{
		boolean res = true;
		for(TypeNode typeNode : getType()) {
			res &= typeNode.resolve();
		}
		return res;
	}

	// default is a procedure without returns, overwrite if return is not empty
	public Vector<TypeNode> getType()
	{
		return emptyReturn;
	}
}
