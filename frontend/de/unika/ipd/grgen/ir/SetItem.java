/*
 * GrGen: graph rewrite generator tool -- release GrGen.NET 2.6
 * Copyright (C) 2003-2009 Universitaet Karlsruhe, Institut fuer Programmstrukturen und Datenorganisation, LS Goos
 * licensed under LGPL v3 (see LICENSE.txt included in the packaging of this file)
 */

/**
 * @author Edgar Jakumeit
 * @version $Id: MapItem.java 22945 2008-10-16 16:02:13Z moritz $
 */

package de.unika.ipd.grgen.ir;

public class SetItem extends IR {
	Expression valueExpr;

	public SetItem(Expression valueExpr) {
		super("set item");
		this.valueExpr = valueExpr;
	}

	public Expression getValueExpr() {
		return valueExpr;
	}
}
