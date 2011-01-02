/*
 * GrGen: graph rewrite generator tool -- release GrGen.NET 2.7
 * Copyright (C) 2003-2011 Universitaet Karlsruhe, Institut fuer Programmstrukturen und Datenorganisation, LS Goos
 * licensed under LGPL v3 (see LICENSE.txt included in the packaging of this file)
 * www.grgen.net
 */

/**
 * @author Edgar Jakumeit
 * @version $Id: ExternalType.java 26740 2010-01-02 11:21:07Z eja $
 */
package de.unika.ipd.grgen.ir;

/**
 * IR class that represents external types.
 */
public class ExternalType extends InheritanceType {
	/**
	 * Make a new external type.
	 * @param ident The identifier that declares this type.
	 */
	public ExternalType(Ident ident) {
		super("node type", ident, 0, null);
	}
	
	/** Return a classification of a type for the IR. */
	public int classify() {
		return IS_EXTERNAL_TYPE;
	}
}
