/*
 * GrGen: graph rewrite generator tool -- release GrGen.NET 2.6
 * Copyright (C) 2003-2010 Universitaet Karlsruhe, Institut fuer Programmstrukturen und Datenorganisation, LS Goos
 * licensed under LGPL v3 (see LICENSE.txt included in the packaging of this file)
 * www.grgen.net
 */

/**
 * @date Jul 20, 2003
 * @author Sebastian Hack
 * @version $Id$
 */
package de.unika.ipd.grgen.util;

/**
 * A Visitor
 */
public interface Visitor
{
	void visit(Walkable n);
}
