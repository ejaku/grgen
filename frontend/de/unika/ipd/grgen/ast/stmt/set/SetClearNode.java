/*
 * GrGen: graph rewrite generator tool -- release GrGen.NET 6.6
 * Copyright (C) 2003-2022 Universitaet Karlsruhe, Institut fuer Programmstrukturen und Datenorganisation, LS Goos; and free programmers
 * licensed under LGPL v3 (see LICENSE.txt included in the packaging of this file)
 * www.grgen.net
 */

/**
 * @author Edgar Jakumeit
 */

package de.unika.ipd.grgen.ast.stmt.set;

import de.unika.ipd.grgen.ast.decl.pattern.VarDeclNode;
import de.unika.ipd.grgen.ast.expr.QualIdentNode;
import de.unika.ipd.grgen.ir.IR;
import de.unika.ipd.grgen.ir.expr.Qualification;
import de.unika.ipd.grgen.ir.pattern.Variable;
import de.unika.ipd.grgen.ir.stmt.set.SetClear;
import de.unika.ipd.grgen.ir.stmt.set.SetVarClear;
import de.unika.ipd.grgen.parser.Coords;

public class SetClearNode extends SetProcedureMethodInvocationBaseNode
{
	static {
		setName(SetClearNode.class, "set clear statement");
	}

	public SetClearNode(Coords coords, QualIdentNode target)
	{
		super(coords, target);
	}

	public SetClearNode(Coords coords, VarDeclNode targetVar)
	{
		super(coords, targetVar);
	}

	@Override
	protected IR constructIR()
	{
		if(target != null)
			return new SetClear(target.checkIR(Qualification.class));
		else
			return new SetVarClear(targetVar.checkIR(Variable.class));
	}
}
