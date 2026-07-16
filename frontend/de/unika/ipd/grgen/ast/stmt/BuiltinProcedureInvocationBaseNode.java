/*
 * GrGen: graph rewrite generator tool -- release GrGen.NET 8.1
 * Copyright (C) 2003-2026 Universitaet Karlsruhe, Institut fuer Programmstrukturen und Datenorganisation, LS Goos; and free programmers
 * licensed under LGPL v3, some components/parts use different licenses (see LICENSE.txt included in the packaging of this file)
 * www.grgen.de / www.grgen.net
 */

/**
 * @author Edgar Jakumeit
 */

package de.unika.ipd.grgen.ast.stmt;

import java.util.ArrayList;
import java.util.List;

import de.unika.ipd.grgen.ast.stmt.invocation.ProcedureOrBuiltinProcedureInvocationBaseNode;
import de.unika.ipd.grgen.ast.type.TypeNode;
import de.unika.ipd.grgen.parser.Coords;

/** base class for builtin procedures calls */
public abstract class BuiltinProcedureInvocationBaseNode extends ProcedureOrBuiltinProcedureInvocationBaseNode
{
	static {
		setName(BuiltinProcedureInvocationBaseNode.class, "builtin procedure invocation base");
	}

	private static final List<TypeNode> emptyReturn = new ArrayList<TypeNode>();

	public BuiltinProcedureInvocationBaseNode(Coords coords)
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
	@Override
	public List<TypeNode> getType()
	{
		return emptyReturn;
	}
}
