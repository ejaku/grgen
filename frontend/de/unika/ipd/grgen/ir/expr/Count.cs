/*
 * GrGen: graph rewrite generator tool -- release GrGen.NET 8.1
 * Copyright (C) 2003-2026 Universitaet Karlsruhe, Institut fuer Programmstrukturen und Datenorganisation, LS Goos; and free programmers
 * licensed under LGPL v3, some components/parts use different licenses (see LICENSE.txt included in the packaging of this file)
 * www.grgen.de / www.grgen.net
 */

namespace de.unika.ipd.grgen.ir.expr
{
	using Rule = de.unika.ipd.grgen.ir.executable.Rule;
	using Type = de.unika.ipd.grgen.ir.type.Type;

	public class Count : Expression
	{
		private Rule iterated;

		public Count(Rule iterated, Type type)
			: base("count", type)
		{
			this.iterated = iterated;
		}

		public virtual Rule Iterated
		{
			get
			{
				return iterated;
			}
		}
	}

}
