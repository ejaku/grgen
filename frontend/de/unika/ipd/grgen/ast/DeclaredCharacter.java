/*
 * GrGen: graph rewrite generator tool -- release GrGen.NET 2.5
 * Copyright (C) 2009 Universitaet Karlsruhe, Institut fuer Programmstrukturen und Datenorganisation, LS Goos
 * licensed under LGPL v3 (see LICENSE.txt included in the packaging of this file)
 */

/**
 * @author Sebastian Hack
 * @version $Id$
 */
package de.unika.ipd.grgen.ast;

/**
 * Something that has been declared.
 */
public interface DeclaredCharacter {

	/**
	 * Get the declaration of this declaration character.
	 * @return
	 */
	DeclNode getDecl();

}
