/*
 * GrGen: graph rewrite generator tool -- release GrGen.NET 8.0
 * Copyright (C) 2003-2025 Universitaet Karlsruhe, Institut fuer Programmstrukturen und Datenorganisation, LS Goos; and free programmers
 * licensed under LGPL v3 (see LICENSE.txt included in the packaging of this file)
 * www.grgen.de / www.grgen.net
 */

package de.unika.ipd.grgen.ast.expr;

import java.util.Collection;
import java.util.Vector;

import de.unika.ipd.grgen.ast.*;
import de.unika.ipd.grgen.ast.stmt.invocation.ProcedureInvocationDecisionNode;
import de.unika.ipd.grgen.ast.stmt.invocation.ProcedureOrBuiltinProcedureInvocationBaseNode;
import de.unika.ipd.grgen.ast.type.TypeNode;
import de.unika.ipd.grgen.ast.type.basic.BasicTypeNode;
import de.unika.ipd.grgen.ir.IR;
import de.unika.ipd.grgen.ir.expr.ProjectionExpr;
import de.unika.ipd.grgen.ir.stmt.invocation.ProcedureInvocationBase;
import de.unika.ipd.grgen.parser.Coords;

public class ProjectionExprNode extends ExprNode
{
	static {
		setName(ProjectionExprNode.class, "projection expr");
	}

	private int index;
	private ProcedureOrBuiltinProcedureInvocationBaseNode procedure;

	public ProjectionExprNode(Coords coords, int index)
	{
		super(coords);

		this.index = index;
	}

	public void setProcedure(ProcedureOrBuiltinProcedureInvocationBaseNode procedure)
	{
		this.procedure = procedure;
		becomeParent(procedure);
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

	@Override
	protected IR constructIR()
	{
		return new ProjectionExpr(index,
				procedure instanceof ProcedureInvocationDecisionNode ? null
						: procedure.checkIR(ProcedureInvocationBase.class).getProcedureBase(),
				procedure.getType().get(index).getType());
	}

	@Override
	public TypeNode getType()
	{
		if(index >= procedure.getType().size()) {
			return BasicTypeNode.getErrorType(IdentNode.getInvalid());
		}

		return procedure.getType().get(index);
	}
}
