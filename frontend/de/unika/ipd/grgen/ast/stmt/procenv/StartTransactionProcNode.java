/*
 * GrGen: graph rewrite generator tool -- release GrGen.NET 5.0
 * Copyright (C) 2003-2020 Universitaet Karlsruhe, Institut fuer Programmstrukturen und Datenorganisation, LS Goos; and free programmers
 * licensed under LGPL v3 (see LICENSE.txt included in the packaging of this file)
 * www.grgen.net
 */

/**
 * @author Edgar Jakumeit
 */

package de.unika.ipd.grgen.ast.stmt.procenv;

import java.util.Collection;
import java.util.Vector;

import de.unika.ipd.grgen.ast.*;
import de.unika.ipd.grgen.ast.decl.DeclNode;
import de.unika.ipd.grgen.ast.stmt.BuiltinProcedureInvocationBaseNode;
import de.unika.ipd.grgen.ast.stmt.EvalStatementNode;
import de.unika.ipd.grgen.ast.type.TypeNode;
import de.unika.ipd.grgen.ast.type.basic.BasicTypeNode;
import de.unika.ipd.grgen.ir.IR;
import de.unika.ipd.grgen.ir.stmt.procenv.StartTransactionProc;
import de.unika.ipd.grgen.parser.Coords;

public class StartTransactionProcNode extends BuiltinProcedureInvocationBaseNode
{
	static {
		setName(StartTransactionProcNode.class, "start transaction procedure");
	}

	Vector<TypeNode> returnTypes;

	public StartTransactionProcNode(Coords coords)
	{
		super(coords);
	}

	@Override
	public Collection<? extends BaseNode> getChildren()
	{
		Vector<BaseNode> children = new Vector<BaseNode>();
		return children;
	}

	@Override
	public Collection<String> getChildrenNames()
	{
		Vector<String> childrenNames = new Vector<String>();
		return childrenNames;
	}

	@Override
	protected boolean checkLocal()
	{
		return true;
	}

	public boolean checkStatementLocal(boolean isLHS, DeclNode root, EvalStatementNode enclosingLoop)
	{
		return true;
	}

	@Override
	protected IR constructIR()
	{
		StartTransactionProc startTransaction = new StartTransactionProc(BasicTypeNode.intType.getType());
		return startTransaction;
	}

	@Override
	public Vector<TypeNode> getType()
	{
		if(returnTypes == null) {
			returnTypes = new Vector<TypeNode>();
			returnTypes.add(BasicTypeNode.intType);
		}
		return returnTypes;
	}
}
