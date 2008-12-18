/*
 * GrGen: graph rewrite generator tool -- release GrGen.NET 2.1
 * Copyright (C) 2008 Universitaet Karlsruhe, Institut fuer Programmstrukturen und Datenorganisation, LS Goos
 * licensed under GPL v3 (see LICENSE.txt included in the packaging of this file)
 */

/**
 * @author Moritz Kroll, Edgar Jakumeit
 * @version $Id: MapInit.java 22945 2008-10-16 16:02:13Z moritz $
 */

package de.unika.ipd.grgen.ir;

public class MapSizeExpr extends Expression {
	Expression targetExpr;
	
	public MapSizeExpr(Expression targetExpr) {
		super("map size expression", IntType.getType());
		this.targetExpr = targetExpr;
	}
	
	public void collectNeededEntities(NeededEntities needs) {
		needs.add(this);
		targetExpr.collectNeededEntities(needs);
	}
	
	public Expression getTargetExpr() {
		return targetExpr;
	}	
}
