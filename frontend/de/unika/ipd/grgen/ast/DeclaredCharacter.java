/*
 * GrGen: graph rewrite generator tool -- release GrGen.NET 2.0
 * Copyright (C) 2008 Universitaet Karlsruhe, Institut fuer Programmstrukturen und Datenorganisation, LS Goos
 * licensed under GPL v3 (see LICENSE.txt included in the packaging of this file)
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
