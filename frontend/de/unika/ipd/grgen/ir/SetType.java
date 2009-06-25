/*
 * GrGen: graph rewrite generator tool -- release GrGen.NET 2.5
 * Copyright (C) 2009 Universitaet Karlsruhe, Institut fuer Programmstrukturen und Datenorganisation, LS Goos
 * licensed under LGPL v3 (see LICENSE.txt included in the packaging of this file)
 */

/**
 * @author Edgar Jakumeit
 * @version $Id: MapType.java 22945 2008-10-16 16:02:13Z moritz $
 */

package de.unika.ipd.grgen.ir;

public class SetType extends Type {
	Type valueType;
	
	public SetType(Type valueType) {
		super("set type", null);
		this.valueType = valueType;
	}
	
	public Type getValueType() {
		return valueType;
	}
}
