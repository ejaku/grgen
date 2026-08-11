/*
 * GrGen: graph rewrite generator tool -- release GrGen.NET 8.1
 * Copyright (C) 2003-2026 Universitaet Karlsruhe, Institut fuer Programmstrukturen und Datenorganisation, LS Goos; and free programmers
 * licensed under LGPL v3, some components/parts use different licenses (see LICENSE.txt included in the packaging of this file)
 * www.grgen.de / www.grgen.net
 */

namespace de.unika.ipd.grgen.ast.util
{
	using BaseNode = de.unika.ipd.grgen.ast.BaseNode;

	public class Quadruple<R, S, T, U> where R : de.unika.ipd.grgen.ast.BaseNode where S : de.unika.ipd.grgen.ast.BaseNode where T : de.unika.ipd.grgen.ast.BaseNode where U : de.unika.ipd.grgen.ast.BaseNode
	{
		public R first = null;
		public S second = null;
		public T third = null;
		public U fourth = null;
	}

}
