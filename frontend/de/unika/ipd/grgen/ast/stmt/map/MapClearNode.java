/*
 * GrGen: graph rewrite generator tool -- release GrGen.NET 8.0
 * Copyright (C) 2003-2025 Universitaet Karlsruhe, Institut fuer Programmstrukturen und Datenorganisation, LS Goos; and free programmers
 * licensed under LGPL v3 (see LICENSE.txt included in the packaging of this file)
 * www.grgen.de / www.grgen.net
 */

/**
 * @author Edgar Jakumeit
 */

package de.unika.ipd.grgen.ast.stmt.map;

import de.unika.ipd.grgen.ast.decl.pattern.VarDeclNode;
import de.unika.ipd.grgen.ast.expr.QualIdentNode;
import de.unika.ipd.grgen.ir.IR;
import de.unika.ipd.grgen.ir.expr.Qualification;
import de.unika.ipd.grgen.ir.pattern.Variable;
import de.unika.ipd.grgen.ir.stmt.map.MapClear;
import de.unika.ipd.grgen.ir.stmt.map.MapVarClear;
import de.unika.ipd.grgen.parser.Coords;

public class MapClearNode extends MapProcedureMethodInvocationBaseNode
{
	static {
		setName(MapClearNode.class, "map clear statement");
	}

	public MapClearNode(Coords coords, QualIdentNode target)
	{
		super(coords, target);
	}

	public MapClearNode(Coords coords, VarDeclNode targetVar)
	{
		super(coords, targetVar);
	}

	@Override
	protected IR constructIR()
	{
		if(target != null)
			return new MapClear(target.checkIR(Qualification.class));
		else
			return new MapVarClear(targetVar.checkIR(Variable.class));
	}
}
