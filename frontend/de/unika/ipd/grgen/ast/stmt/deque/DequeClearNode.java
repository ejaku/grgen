/*
 * GrGen: graph rewrite generator tool -- release GrGen.NET 5.2
 * Copyright (C) 2003-2021 Universitaet Karlsruhe, Institut fuer Programmstrukturen und Datenorganisation, LS Goos; and free programmers
 * licensed under LGPL v3 (see LICENSE.txt included in the packaging of this file)
 * www.grgen.net
 */

/**
 * @author Edgar Jakumeit
 */

package de.unika.ipd.grgen.ast.stmt.deque;

import de.unika.ipd.grgen.ast.decl.pattern.VarDeclNode;
import de.unika.ipd.grgen.ast.expr.QualIdentNode;
import de.unika.ipd.grgen.ir.expr.Qualification;
import de.unika.ipd.grgen.ir.pattern.Variable;
import de.unika.ipd.grgen.ir.stmt.deque.DequeClear;
import de.unika.ipd.grgen.ir.stmt.deque.DequeVarClear;
import de.unika.ipd.grgen.ir.IR;
import de.unika.ipd.grgen.parser.Coords;

public class DequeClearNode extends DequeProcedureMethodInvocationBaseNode
{
	static {
		setName(DequeClearNode.class, "deque clear statement");
	}

	public DequeClearNode(Coords coords, QualIdentNode target)
	{
		super(coords, target);
	}

	public DequeClearNode(Coords coords, VarDeclNode targetVar)
	{
		super(coords, targetVar);
	}

	@Override
	protected IR constructIR()
	{
		if(target != null)
			return new DequeClear(target.checkIR(Qualification.class));
		else
			return new DequeVarClear(targetVar.checkIR(Variable.class));
	}
}
