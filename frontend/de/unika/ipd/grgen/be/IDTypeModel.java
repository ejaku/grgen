/*
 * GrGen: graph rewrite generator tool -- release GrGen.NET 2.6
 * Copyright (C) 2003-2010 Universitaet Karlsruhe, Institut fuer Programmstrukturen und Datenorganisation, LS Goos
 * licensed under LGPL v3 (see LICENSE.txt included in the packaging of this file)
 * www.grgen.net
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
