/*
 * GrGen: graph rewrite generator tool -- release GrGen.NET 2.0
 * Copyright (C) 2008 Universitaet Karlsruhe, Institut fuer Programmstrukturen und Datenorganisation, LS Goos
 * licensed under GPL v3 (see LICENSE.txt included in the packaging of this file)
 */

/**
 * @author Moritz Kroll
 * @version $Id$
 */
package de.unika.ipd.grgen.ir;

public class ConstructorParam extends IR {
	private Entity entity;
	private Expression defValue;
	
	public ConstructorParam(Entity entity, Expression defValue) {
		super("constructor param");
		this.entity = entity;
		this.defValue = defValue;
	}
	
	public Entity getEntity() {
		return entity;
	}
	
	public Expression getDefValue() {
		return defValue;
	}
}
