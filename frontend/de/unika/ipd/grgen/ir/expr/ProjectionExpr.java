/*
 * GrGen: graph rewrite generator tool -- release GrGen.NET 5.0
 * Copyright (C) 2003-2020 Universitaet Karlsruhe, Institut fuer Programmstrukturen und Datenorganisation, LS Goos; and free programmers
 * licensed under LGPL v3 (see LICENSE.txt included in the packaging of this file)
 * www.grgen.net
 */

/**
 * @author Edgar Jakumeit
 */

package de.unika.ipd.grgen.ir.expr;

import de.unika.ipd.grgen.ir.executable.ProcedureBase;
import de.unika.ipd.grgen.ir.type.Type;

public class ProjectionExpr extends Expression
{
	private int index;
	private ProcedureBase procedure;
	private String projectedValueVarName;

	public ProjectionExpr(int index, ProcedureBase procedure, Type type)
	{
		super("projection expr", type);
		this.index = index;
		this.procedure = procedure;
	}

	public int getIndex()
	{
		return index;
	}

	public ProcedureBase getProcedure()
	{
		return procedure;
	}

	public String getProjectedValueVarName()
	{
		return projectedValueVarName;
	}

	public void setProjectedValueVarName(String projectedValueVarName)
	{
		this.projectedValueVarName = projectedValueVarName;
	}
}
