/*
 * GrGen: graph rewrite generator tool -- release GrGen.NET v2 beta
 * Copyright (C) 2008 Universität Karlsruhe, Institut für Programmstrukturen und Datenorganisation, LS Goos
 * licensed under GPL v3 (see LICENSE.txt included in the packaging of this file)
 */

/**
 * Created on Mar 8, 2004
 *
 * @author Sebastian Hack
 * @version $Id$
 */
package de.unika.ipd.grgen.be;

/**
 * A type model that uses IDs.
 */
public interface IDTypeModel extends TypeID {

	String getTypeName(boolean forNode, int obj);

	int[] getSuperTypes(boolean forNode, int obj);

	int[] getSubTypes(boolean forNode, int obj);

	int getRootType(boolean forNode);

	int[] getIDs(boolean forNode);
}
