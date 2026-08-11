/*
 * GrGen: graph rewrite generator tool -- release GrGen.NET 8.1
 * Copyright (C) 2003-2026 Universitaet Karlsruhe, Institut fuer Programmstrukturen und Datenorganisation, LS Goos; and free programmers
 * licensed under LGPL v3, some components/parts use different licenses (see LICENSE.txt included in the packaging of this file)
 * www.grgen.de / www.grgen.net
 */

namespace de.unika.ipd.grgen.ir.pattern
{

	using System.Collections.Generic;

	using IR = de.unika.ipd.grgen.ir.IR;

	public class OrderedReplacements : IR
	{
		public IList<OrderedReplacement> orderedReplacements = new List<OrderedReplacement>();

		public OrderedReplacements(string name)
			: base(name)
		{
		}
	}

}
