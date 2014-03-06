/*
 * GrGen: graph rewrite generator tool -- release GrGen.NET 4.3
 * Copyright (C) 2003-2014 Universitaet Karlsruhe, Institut fuer Programmstrukturen und Datenorganisation, LS Goos; and free programmers
 * licensed under LGPL v3 (see LICENSE.txt included in the packaging of this file)
 * www.grgen.net
 */

/**
 * @author Edgar Jakumeit
 */

package de.unika.ipd.grgen.ir;

/**
 * An attribute index.
 */
public class AttributeIndex extends Index {
	public InheritanceType type;
	public Entity entity;
	
	/**
	 * @param name The name of the attribute index.
	 * @param ident The identifier that identifies this object.
	 */
	public AttributeIndex(String name, Ident ident, InheritanceType type, Entity entity) {
		super(name, ident);
		this.type = type;
		this.entity = entity;
	}
}
