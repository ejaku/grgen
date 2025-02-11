/*
 * GrGen: graph rewrite generator tool -- release GrGen.NET 7.1
 * Copyright (C) 2003-2025 Universitaet Karlsruhe, Institut fuer Programmstrukturen und Datenorganisation, LS Goos; and free programmers
 * licensed under LGPL v3 (see LICENSE.txt included in the packaging of this file)
 * www.grgen.net
 */

package de.unika.ipd.grgen.ast.util;

import de.unika.ipd.grgen.ast.BaseNode;

public class Quadruple<R extends BaseNode, S extends BaseNode, T extends BaseNode, U extends BaseNode>
{
	public R first = null;
	public S second = null;
	public T third = null;
	public U fourth = null;
}
