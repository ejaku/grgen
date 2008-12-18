/*
 * GrGen: graph rewrite generator tool -- release GrGen.NET 2.1
 * Copyright (C) 2008 Universitaet Karlsruhe, Institut fuer Programmstrukturen und Datenorganisation, LS Goos
 * licensed under GPL v3 (see LICENSE.txt included in the packaging of this file)
 */

/**
 * @author Edgar Jakumeit
 * @version $Id: MapInit.java 22945 2008-10-16 16:02:13Z moritz $
 */

package de.unika.ipd.grgen.ir;

public class SetRemoveItem extends EvalStatement {
	Qualification target;
	Expression valueExpr;
	
	public SetRemoveItem(Qualification target, Expression valueExpr) {
		super("set remove item");
		this.target = target;
		this.valueExpr = valueExpr;
	}
	
	public Qualification getTarget() {
		return target;
	}
	
	public Expression getValueExpr() {
		return valueExpr;
	}
}
