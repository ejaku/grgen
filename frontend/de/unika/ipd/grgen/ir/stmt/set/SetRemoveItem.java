/*
 * GrGen: graph rewrite generator tool -- release GrGen.NET 5.0
 * Copyright (C) 2003-2020 Universitaet Karlsruhe, Institut fuer Programmstrukturen und Datenorganisation, LS Goos; and free programmers
 * licensed under LGPL v3 (see LICENSE.txt included in the packaging of this file)
 * www.grgen.net
 */

/**
 * @author Edgar Jakumeit
 */

package de.unika.ipd.grgen.ir.stmt.set;

import java.util.HashSet;
import de.unika.ipd.grgen.ir.*;
import de.unika.ipd.grgen.ir.expr.Expression;
import de.unika.ipd.grgen.ir.expr.Qualification;
import de.unika.ipd.grgen.ir.pattern.GraphEntity;
import de.unika.ipd.grgen.ir.pattern.Variable;
import de.unika.ipd.grgen.ir.stmt.ContainerQualProcedureMethodInvocationBase;

public class SetRemoveItem extends ContainerQualProcedureMethodInvocationBase
{
	Expression valueExpr;

	public SetRemoveItem(Qualification target, Expression valueExpr)
	{
		super("set remove item", target);
		this.valueExpr = valueExpr;
	}

	public Expression getValueExpr()
	{
		return valueExpr;
	}

	public void collectNeededEntities(NeededEntities needs)
	{
		Entity entity = target.getOwner();
		if(!isGlobalVariable(entity))
			needs.add((GraphEntity)entity);

		// Temporarily do not collect variables for target
		HashSet<Variable> varSet = needs.variables;
		needs.variables = null;
		target.collectNeededEntities(needs);
		needs.variables = varSet;

		valueExpr.collectNeededEntities(needs);

		if(getNext() != null) {
			getNext().collectNeededEntities(needs);
		}
	}
}
