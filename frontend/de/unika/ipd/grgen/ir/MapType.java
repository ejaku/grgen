/*
 * GrGen: graph rewrite generator tool -- release GrGen.NET 2.6
 * Copyright (C) 2003-2010 Universitaet Karlsruhe, Institut fuer Programmstrukturen und Datenorganisation, LS Goos
 * licensed under LGPL v3 (see LICENSE.txt included in the packaging of this file)
 * www.grgen.net
 */

/**
 * @author Moritz Kroll, Edgar Jakumeit
 * @version $Id$
 */

package de.unika.ipd.grgen.ir;

public class MapType extends Type {
	Type keyType;
	Type valueType;

	public MapType(Type keyType, Type valueType) {
		super("map type", null);
		this.keyType   = keyType;
		this.valueType = valueType;
	}

	public Type getKeyType() {
		return keyType;
	}

	public Type getValueType() {
		return valueType;
	}
}
