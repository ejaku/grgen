/*
 * GrGen: graph rewrite generator tool -- release GrGen.NET 2.0
 * Copyright (C) 2008 Universitaet Karlsruhe, Institut fuer Programmstrukturen und Datenorganisation, LS Goos
 * licensed under GPL v3 (see LICENSE.txt included in the packaging of this file)
 */

/**
 * @author Moritz Kroll, Edgar Jakumeit
 * @version $Id: MapInit.java 22945 2008-10-16 16:02:13Z moritz $
 */

package de.unika.ipd.grgen.ir;

public class MapRemoveItem extends EvalStatement {
	Qualification target;
	Expression keyExpr;
	
	public MapRemoveItem(Qualification target, Expression keyExpr) {
		super("map remove item");
		this.target = target;
		this.keyExpr = keyExpr;
	}
	
	public Qualification getTarget() {
		return target;
	}
	
	public Expression getKeyExpr() {
		return keyExpr;
	}
}
