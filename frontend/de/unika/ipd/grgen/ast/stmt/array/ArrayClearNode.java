/*
 * GrGen: graph rewrite generator tool -- release GrGen.NET 6.0
 * Copyright (C) 2003-2021 Universitaet Karlsruhe, Institut fuer Programmstrukturen und Datenorganisation, LS Goos; and free programmers
 * licensed under LGPL v3 (see LICENSE.txt included in the packaging of this file)
 * www.grgen.net
 */

/**
 * @author Edgar Jakumeit
 */

package de.unika.ipd.grgen.ast.stmt.array;

import de.unika.ipd.grgen.ast.decl.pattern.VarDeclNode;
import de.unika.ipd.grgen.ast.expr.QualIdentNode;
import de.unika.ipd.grgen.ir.expr.Qualification;
import de.unika.ipd.grgen.ir.pattern.Variable;
import de.unika.ipd.grgen.ir.stmt.array.ArrayClear;
import de.unika.ipd.grgen.ir.stmt.array.ArrayVarClear;
import de.unika.ipd.grgen.ir.IR;
import de.unika.ipd.grgen.parser.Coords;

public class ArrayClearNode extends ArrayProcedureMethodInvocationBaseNode
{
	static {
		setName(ArrayClearNode.class, "array clear statement");
	}

	public ArrayClearNode(Coords coords, QualIdentNode target)
	{
		super(coords, target);
	}

	public ArrayClearNode(Coords coords, VarDeclNode targetVar)
	{
		super(coords, targetVar);
	}

	@Override
	protected IR constructIR()
	{
		if(target != null)
			return new ArrayClear(target.checkIR(Qualification.class));
		else
			return new ArrayVarClear(targetVar.checkIR(Variable.class));
	}
}
