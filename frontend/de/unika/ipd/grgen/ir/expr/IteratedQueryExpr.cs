/*
 * GrGen: graph rewrite generator tool -- release GrGen.NET 8.1
 * Copyright (C) 2003-2026 Universitaet Karlsruhe, Institut fuer Programmstrukturen und Datenorganisation, LS Goos; and free programmers
 * licensed under LGPL v3, some components/parts use different licenses (see LICENSE.txt included in the packaging of this file)
 * www.grgen.de / www.grgen.net
 */

/// <summary>
/// @author Edgar Jakumeit
/// </summary>

namespace de.unika.ipd.grgen.ir.expr
{
	using Ident = de.unika.ipd.grgen.ir.Ident;
	using Rule = de.unika.ipd.grgen.ir.executable.Rule;
	using Type = de.unika.ipd.grgen.ir.type.Type;

	public class IteratedQueryExpr : Expression
	{
		internal Ident iteratedName;

		public IteratedQueryExpr(Ident iteratedName, Rule iterated, Type targetType)
			: base("iterated query", targetType)
		{
			this.iteratedName = iteratedName;
		}

		public virtual Ident IteratedName
		{
			get
			{
				return iteratedName;
			}
		}
	}

}
