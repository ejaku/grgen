/*
 * GrGen: graph rewrite generator tool -- release GrGen.NET 4.4
 * Copyright (C) 2003-2016 Universitaet Karlsruhe, Institut fuer Programmstrukturen und Datenorganisation, LS Goos; and free programmers
 * licensed under LGPL v3 (see LICENSE.txt included in the packaging of this file)
 * www.grgen.net
 */

/**
 * @author Moritz Kroll
 */

package de.unika.ipd.grgen.ir;

import de.unika.ipd.grgen.ir.exprevals.*;;

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
